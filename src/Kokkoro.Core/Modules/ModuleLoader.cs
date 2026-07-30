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
            var assembly = Assembly.LoadFrom(file);
            if (assembly != null)
            {
                var moduleTypes = assembly
                    .GetTypes()
                    .Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsAbstract)
                    .ToArray();

                if (moduleTypes.Length > 1)
                {
                    throw new InvalidOperationException(
                        $"程序集 '{assembly.FullName}' 包含多个 {nameof(IModule)} 实现。每个程序集只能定义一个模块入口。");
                }

                if (moduleTypes.Length == 1)
                {
                    var moduleInstance = (IModule)Activator.CreateInstance(moduleTypes[0])!;
                    _assemblies.Add(new ModuleAssembly
                    {
                        Assembly = assembly,
                        Instance = moduleInstance,
                        SetupIndex = moduleInstance.SetupLevel
                    });
                }
            }
        }

        // 升序，小 Index 在前
        _assemblies.Sort((a, b) => a.SetupIndex.CompareTo(b.SetupIndex));
    }
}
