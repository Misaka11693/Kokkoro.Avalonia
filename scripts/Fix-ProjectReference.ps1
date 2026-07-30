param(
    [string]$ProjectFile = "src/Kokkoro/Kokkoro.csproj",
    [string]$SolutionFile = "Kokkoro.slnx"
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

function Get-RepoRoot {
    param([string]$ScriptPath)

    return [System.IO.Path]::GetFullPath((Join-Path (Split-Path -Parent $ScriptPath) ".."))
}

function Resolve-PathSafe {
    param(
        [string]$BasePath,
        [string]$PathValue
    )

    if ([System.IO.Path]::IsPathRooted($PathValue)) {
        return [System.IO.Path]::GetFullPath($PathValue)
    }

    return [System.IO.Path]::GetFullPath((Join-Path $BasePath $PathValue))
}

function Remove-DuplicateUtf8Bom {
    param([string]$FilePath)

    $bytes = [System.IO.File]::ReadAllBytes($FilePath)
    $bom = [byte[]](0xEF, 0xBB, 0xBF)
    $removedCount = 0

    while ($bytes.Length -ge 6 `
        -and $bytes[0] -eq $bom[0] `
        -and $bytes[1] -eq $bom[1] `
        -and $bytes[2] -eq $bom[2] `
        -and $bytes[3] -eq $bom[0] `
        -and $bytes[4] -eq $bom[1] `
        -and $bytes[5] -eq $bom[2]) {
        $bytes = $bytes[3..($bytes.Length - 1)]
        $removedCount++
    }

    if ($removedCount -gt 0) {
        [System.IO.File]::WriteAllBytes($FilePath, $bytes)
    }

    return $removedCount
}

function Get-ProjectPathForSolution {
    param(
        [string]$ProjectAbsolutePath,
        [string]$SolutionAbsolutePath
    )

    $solutionDirectory = Split-Path -Parent $SolutionAbsolutePath
    $solutionDirectoryUri = New-Object System.Uri(($solutionDirectory.TrimEnd('\') + '\'))
    $projectUri = New-Object System.Uri($ProjectAbsolutePath)
    $relativePath = $solutionDirectoryUri.MakeRelativeUri($projectUri).ToString()
    return [System.Uri]::UnescapeDataString($relativePath).Replace("\", "/")
}

function Ensure-SlnxProjectReference {
    param(
        [string]$SolutionAbsolutePath,
        [string]$ProjectPathInSolution
    )

    $content = [System.IO.File]::ReadAllText($SolutionAbsolutePath)
    $escapedProjectPath = [regex]::Escape($ProjectPathInSolution)

    if ($content -match "<Project\s+Path=""$escapedProjectPath""(?:\s+Id=""[^""]+"")?\s*/>") {
        return $false
    }

    $projectLine = "  <Project Path=""$ProjectPathInSolution"" />"

    if ($content -notmatch '</Solution>') {
        throw 'Invalid solution file: missing </Solution>.'
    }

    $updatedContent = $content -replace '</Solution>', ($projectLine + "`r`n</Solution>")
    [System.IO.File]::WriteAllText($SolutionAbsolutePath, $updatedContent, [System.Text.UTF8Encoding]::new($false))
    return $true
}

$repoRoot = Get-RepoRoot -ScriptPath $PSCommandPath
$projectAbsolutePath = Resolve-PathSafe -BasePath $repoRoot -PathValue $ProjectFile
$solutionAbsolutePath = Resolve-PathSafe -BasePath $repoRoot -PathValue $SolutionFile

if (-not (Test-Path -LiteralPath $projectAbsolutePath)) {
    throw "Project file not found: $projectAbsolutePath"
}

if (-not (Test-Path -LiteralPath $solutionAbsolutePath)) {
    throw "Solution file not found: $solutionAbsolutePath"
}

$removedBomCount = Remove-DuplicateUtf8Bom -FilePath $projectAbsolutePath
$projectPathInSolution = Get-ProjectPathForSolution -ProjectAbsolutePath $projectAbsolutePath -SolutionAbsolutePath $solutionAbsolutePath
$solutionUpdated = Ensure-SlnxProjectReference -SolutionAbsolutePath $solutionAbsolutePath -ProjectPathInSolution $projectPathInSolution

Write-Host "Repo root: $repoRoot"
Write-Host "Project file: $projectAbsolutePath"
Write-Host "Solution file: $solutionAbsolutePath"

if ($removedBomCount -gt 0) {
    Write-Host "Removed duplicate UTF-8 BOM count: $removedBomCount"
}
else {
    Write-Host "UTF-8 BOM check passed: no duplicate BOM found"
}

if ($solutionUpdated) {
    Write-Host "Added solution project reference: $projectPathInSolution"
}
else {
    Write-Host "Solution project reference already exists: $projectPathInSolution"
}

Write-Host "Repair completed."
