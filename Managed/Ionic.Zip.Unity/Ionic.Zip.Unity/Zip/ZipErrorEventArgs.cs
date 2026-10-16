using System;

namespace Ionic.Zip
{
	// Token: 0x0200000E RID: 14
	public class ZipErrorEventArgs : ZipProgressEventArgs
	{
		// Token: 0x06000046 RID: 70 RVA: 0x0000244F File Offset: 0x0000064F
		private ZipErrorEventArgs()
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002458 File Offset: 0x00000658
		internal static ZipErrorEventArgs Saving(string archiveName, ZipEntry entry, Exception exception)
		{
			return new ZipErrorEventArgs
			{
				EventType = ZipProgressEventType.Error_Saving,
				ArchiveName = archiveName,
				CurrentEntry = entry,
				_exc = exception
			};
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000248B File Offset: 0x0000068B
		public Exception Exception
		{
			get
			{
				return this._exc;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002493 File Offset: 0x00000693
		public string FileName
		{
			get
			{
				return base.CurrentEntry.LocalFileName;
			}
		}

		// Token: 0x0400002A RID: 42
		private Exception _exc;
	}
}
