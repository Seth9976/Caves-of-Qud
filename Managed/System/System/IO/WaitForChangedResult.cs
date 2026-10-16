using System;

namespace System.IO
{
	/// <summary>Contains information on the change that occurred.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020004FD RID: 1277
	public struct WaitForChangedResult
	{
		// Token: 0x060029B2 RID: 10674 RVA: 0x0008F1D5 File Offset: 0x0008D3D5
		internal WaitForChangedResult(WatcherChangeTypes changeType, string name, string oldName, bool timedOut)
		{
			this.ChangeType = changeType;
			this.Name = name;
			this.OldName = oldName;
			this.TimedOut = timedOut;
		}

		/// <summary>Gets or sets the type of change that occurred.</summary>
		/// <returns>One of the <see cref="T:System.IO.WatcherChangeTypes" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x060029B3 RID: 10675 RVA: 0x0008F1F4 File Offset: 0x0008D3F4
		// (set) Token: 0x060029B4 RID: 10676 RVA: 0x0008F1FC File Offset: 0x0008D3FC
		public WatcherChangeTypes ChangeType { readonly get; set; }

		/// <summary>Gets or sets the name of the file or directory that changed.</summary>
		/// <returns>The name of the file or directory that changed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x060029B5 RID: 10677 RVA: 0x0008F205 File Offset: 0x0008D405
		// (set) Token: 0x060029B6 RID: 10678 RVA: 0x0008F20D File Offset: 0x0008D40D
		public string Name { readonly get; set; }

		/// <summary>Gets or sets the original name of the file or directory that was renamed.</summary>
		/// <returns>The original name of the file or directory that was renamed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x060029B7 RID: 10679 RVA: 0x0008F216 File Offset: 0x0008D416
		// (set) Token: 0x060029B8 RID: 10680 RVA: 0x0008F21E File Offset: 0x0008D41E
		public string OldName { readonly get; set; }

		/// <summary>Gets or sets a value indicating whether the wait operation timed out.</summary>
		/// <returns>true if the <see cref="M:System.IO.FileSystemWatcher.WaitForChanged(System.IO.WatcherChangeTypes)" /> method timed out; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x060029B9 RID: 10681 RVA: 0x0008F227 File Offset: 0x0008D427
		// (set) Token: 0x060029BA RID: 10682 RVA: 0x0008F22F File Offset: 0x0008D42F
		public bool TimedOut { readonly get; set; }

		// Token: 0x0400161C RID: 5660
		internal static readonly WaitForChangedResult TimedOutResult = new WaitForChangedResult((WatcherChangeTypes)0, null, null, true);
	}
}
