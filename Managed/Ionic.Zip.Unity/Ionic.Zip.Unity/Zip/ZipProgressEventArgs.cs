using System;

namespace Ionic.Zip
{
	// Token: 0x02000009 RID: 9
	public class ZipProgressEventArgs : EventArgs
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000208E File Offset: 0x0000028E
		internal ZipProgressEventArgs()
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002096 File Offset: 0x00000296
		internal ZipProgressEventArgs(string archiveName, ZipProgressEventType flavor)
		{
			this._archiveName = archiveName;
			this._flavor = flavor;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000020AC File Offset: 0x000002AC
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000020B4 File Offset: 0x000002B4
		public int EntriesTotal
		{
			get
			{
				return this._entriesTotal;
			}
			set
			{
				this._entriesTotal = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000020BD File Offset: 0x000002BD
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000020C5 File Offset: 0x000002C5
		public ZipEntry CurrentEntry
		{
			get
			{
				return this._latestEntry;
			}
			set
			{
				this._latestEntry = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000020CE File Offset: 0x000002CE
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000020D6 File Offset: 0x000002D6
		public bool Cancel
		{
			get
			{
				return this._cancel;
			}
			set
			{
				this._cancel = this._cancel || value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000020EA File Offset: 0x000002EA
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000020F2 File Offset: 0x000002F2
		public ZipProgressEventType EventType
		{
			get
			{
				return this._flavor;
			}
			set
			{
				this._flavor = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000020FB File Offset: 0x000002FB
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002103 File Offset: 0x00000303
		public string ArchiveName
		{
			get
			{
				return this._archiveName;
			}
			set
			{
				this._archiveName = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000210C File Offset: 0x0000030C
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002114 File Offset: 0x00000314
		public long BytesTransferred
		{
			get
			{
				return this._bytesTransferred;
			}
			set
			{
				this._bytesTransferred = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000211D File Offset: 0x0000031D
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002125 File Offset: 0x00000325
		public long TotalBytesToTransfer
		{
			get
			{
				return this._totalBytesToTransfer;
			}
			set
			{
				this._totalBytesToTransfer = value;
			}
		}

		// Token: 0x04000020 RID: 32
		private int _entriesTotal;

		// Token: 0x04000021 RID: 33
		private bool _cancel;

		// Token: 0x04000022 RID: 34
		private ZipEntry _latestEntry;

		// Token: 0x04000023 RID: 35
		private ZipProgressEventType _flavor;

		// Token: 0x04000024 RID: 36
		private string _archiveName;

		// Token: 0x04000025 RID: 37
		private long _bytesTransferred;

		// Token: 0x04000026 RID: 38
		private long _totalBytesToTransfer;
	}
}
