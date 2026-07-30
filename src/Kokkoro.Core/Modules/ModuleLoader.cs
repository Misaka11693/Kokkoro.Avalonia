using System.Reflection;

namespace Kokkoro.Core.Modules;

internal static class ModuleLoader
{
    private static readonly List<ModuleAssembly> _assemblies = [];

    /// <summary>
    /// 已加载的模块程序集。
    /// 首次访问时从应用程序目录加载模块程序集。
    /// </summary>
    public static IReadOnlyList<ModuleAssembly> ModuleAssemblys
    {
        get
        {
            if (_assemblies.Count == 0)
            {
                LoadModuleAssemblys();
            }

            return _assemblies;
        }
    }

    private static void LoadModuleAssemblys()
    {
        var modulePath = Path.Combine(AppContext.BaseDirectory);

        if (!Directory.Exists(modulePath))
            return;

        foreach (var file in Directory.EnumerateFiles(modulePath, "Kokkoro.*.dll"))
        {
            var assembly = LoadAssembly(file);
            var moduleTypes = GetModuleTypes(assembly, file);

            if (moduleTypes.Length > 1)
            {
                throw new InvalidOperationException(
                    $"程序集 '{assembly.FullName}' 包含多个 {nameof(IModule)} 实现。每个程序集只能定义一个模块入口。");
            }

            if (moduleTypes.Length == 1)
            {
                var moduleInstance = CreateModuleInstance(moduleTypes[0], file);
                _assemblies.Add(new ModuleAssembly
                {
                    Assembly = assembly,
                    Instance = moduleInstance,
                    SetupIndex = moduleInstance.SetupLevel
                });
            }
        }

        // 升序，小 Index 在前
        _assemblies.Sort((a, b) => a.SetupIndex.CompareTo(b.SetupIndex));
    }

    private static Assembly LoadAssembly(string file)
    {
        try
        {
            return Assembly.LoadFrom(file);
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            throw new InvalidOperationException($"加载模块程序集 '{file}' 失败。", ex);
        }
    }

    private static Type[] GetModuleTypes(Assembly assembly, string file)
    {
        try
        {
            return assembly
                .GetTypes()
                .Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsAbstract)
                .ToArray();
        }
        catch (ReflectionTypeLoadException ex)
        {
            var loaderErrors = ex.LoaderExceptions
                .Where(exception => exception is not null)
                .Select(exception => exception!.Message);
            var loaderDetails = string.Join(Environment.NewLine, loaderErrors);
            var message = $"读取模块程序集 '{file}' 的类型失败。程序集: '{assembly.FullName}'.";

            if (!string.IsNullOrWhiteSpace(loaderDetails))
            {
                message += $"{Environment.NewLine}加载器错误:{Environment.NewLine}{loaderDetails}";
            }

            throw new InvalidOperationException(message, ex);
        }
    }

    private static IModule CreateModuleInstance(Type moduleType, string file)
    {
        try
        {
            return (IModule)Activator.CreateInstance(moduleType)!;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            throw new InvalidOperationException(
                $"创建模块 '{moduleType.FullName}' 失败。程序集: '{file}'.", ex);
        }
    }
}
