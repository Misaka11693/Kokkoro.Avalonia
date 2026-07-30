using System.Reflection;

namespace Kokkoro.Core.Modules;

internal static class ModuleLoader
{
    private static readonly List<ModuleAssembly> _assemblies = [];

    /// <summary>
    /// 已加载的模块程序集。
    /// 首次访问时自动加载 Modules 目录下的程序集。
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
                var moduleType = assembly.GetTypes().FirstOrDefault(t => typeof(IModule).IsAssignableFrom(t) && !t.IsAbstract);
                if (moduleType != null)
                {
                    var moduleInstance = (IModule)Activator.CreateInstance(moduleType)!;
                    _assemblies.Add(new ModuleAssembly { Assembly = assembly, Instance = moduleInstance });
                }
            }
        }

        // 升序，小 Index 在前
        _assemblies.Sort((a, b) => a.SetupIndex.CompareTo(b.SetupIndex));
    }
}
