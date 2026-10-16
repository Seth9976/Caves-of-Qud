using System;

namespace System.Web.Compilation
{
	// Token: 0x0200063C RID: 1596
	internal class BuildManagerRemoveEntryEventArgs : EventArgs
	{
		// Token: 0x17001583 RID: 5507
		// (get) Token: 0x0600449C RID: 17564 RVA: 0x000BA0E8 File Offset: 0x000B82E8
		// (set) Token: 0x0600449D RID: 17565 RVA: 0x000BA0F0 File Offset: 0x000B82F0
		public string EntryName { get; private set; }

		// Token: 0x17001584 RID: 5508
		// (get) Token: 0x0600449E RID: 17566 RVA: 0x000BA0F9 File Offset: 0x000B82F9
		// (set) Token: 0x0600449F RID: 17567 RVA: 0x000BA101 File Offset: 0x000B8301
		public HttpContext Context { get; private set; }

		// Token: 0x060044A0 RID: 17568 RVA: 0x000BA10A File Offset: 0x000B830A
		public BuildManagerRemoveEntryEventArgs(string entryName, HttpContext context)
		{
			this.EntryName = entryName;
			this.Context = context;
		}
	}
}
