using System;
using System.Collections.Generic;
using System.Text;

namespace Kokkoro.Core.Modules;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public sealed class ModuleAttribute : Attribute
{
    public ModuleAttribute(Type moduleType)
    {
        ModuleType = moduleType;
    }

    public Type ModuleType { get; }
}
