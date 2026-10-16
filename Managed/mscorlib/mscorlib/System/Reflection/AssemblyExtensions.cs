using System;

namespace System.Reflection
{
	// Token: 0x020008D5 RID: 2261
	public static class AssemblyExtensions
	{
		// Token: 0x06004B70 RID: 19312 RVA: 0x000F076C File Offset: 0x000EE96C
		public static Type[] GetExportedTypes(Assembly assembly)
		{
			Requires.NotNull(assembly, "assembly");
			return assembly.GetExportedTypes();
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x000F077F File Offset: 0x000EE97F
		public static Module[] GetModules(Assembly assembly)
		{
			Requires.NotNull(assembly, "assembly");
			return assembly.GetModules();
		}

		// Token: 0x06004B72 RID: 19314 RVA: 0x000F0792 File Offset: 0x000EE992
		public static Type[] GetTypes(Assembly assembly)
		{
			Requires.NotNull(assembly, "assembly");
			return assembly.GetTypes();
		}
	}
}
