using System;

namespace MonoMod.Utils
{
	// Token: 0x02000440 RID: 1088
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	internal class DynDllImportAttribute : Attribute
	{
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x0004CCD4 File Offset: 0x0004AED4
		// (set) Token: 0x060016F1 RID: 5873 RVA: 0x0004CCDC File Offset: 0x0004AEDC
		public string LibraryName { get; set; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x0004CCE5 File Offset: 0x0004AEE5
		// (set) Token: 0x060016F3 RID: 5875 RVA: 0x0004CCED File Offset: 0x0004AEED
		public string[] EntryPoints { get; set; }

		// Token: 0x060016F4 RID: 5876 RVA: 0x0004CCF6 File Offset: 0x0004AEF6
		public DynDllImportAttribute(string libraryName, params string[] entryPoints)
		{
			this.LibraryName = libraryName;
			this.EntryPoints = entryPoints;
		}
	}
}
