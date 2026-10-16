using System;

namespace MonoMod.Utils
{
	// Token: 0x02000441 RID: 1089
	internal sealed class DynDllMapping
	{
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x0004CD0C File Offset: 0x0004AF0C
		// (set) Token: 0x060016F6 RID: 5878 RVA: 0x0004CD14 File Offset: 0x0004AF14
		public string LibraryName { get; set; }

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x0004CD1D File Offset: 0x0004AF1D
		// (set) Token: 0x060016F8 RID: 5880 RVA: 0x0004CD25 File Offset: 0x0004AF25
		public int? Flags { get; set; }

		// Token: 0x060016F9 RID: 5881 RVA: 0x0004CD2E File Offset: 0x0004AF2E
		public DynDllMapping(string libraryName, int? flags = null)
		{
			if (libraryName == null)
			{
				throw new ArgumentNullException("libraryName");
			}
			this.LibraryName = libraryName;
			this.Flags = flags;
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0004CD54 File Offset: 0x0004AF54
		public static implicit operator DynDllMapping(string libraryName)
		{
			return new DynDllMapping(libraryName, null);
		}
	}
}
