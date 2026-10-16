using System;

namespace System.Reflection
{
	// Token: 0x020008D9 RID: 2265
	public static class ModuleExtensions
	{
		// Token: 0x06004B7D RID: 19325 RVA: 0x000F08AF File Offset: 0x000EEAAF
		public static bool HasModuleVersionId(this Module module)
		{
			Requires.NotNull(module, "module");
			return true;
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x000F08BD File Offset: 0x000EEABD
		public static Guid GetModuleVersionId(this Module module)
		{
			Requires.NotNull(module, "module");
			return module.ModuleVersionId;
		}
	}
}
