using System;
using System.IO;
using System.Security;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;

namespace AiUnity.NLog.Core.Internal.FileAppenders
{
	// Token: 0x02000280 RID: 640
	[SecuritySafeCritical]
	internal class SingleProcessFileAppender : BaseFileAppender
	{
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x00079CAA File Offset: 0x00077EAA
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00079CB4 File Offset: 0x00077EB4
		public SingleProcessFileAppender(string fileName, ICreateFileParameters parameters)
			: base(fileName, parameters)
		{
			FileInfo fileInfo = new FileInfo(fileName);
			if (fileInfo.Exists)
			{
				base.FileTouched(fileInfo.LastWriteTime);
			}
			else
			{
				base.FileTouched();
			}
			this.file = base.CreateFileStream(false);
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00079CF9 File Offset: 0x00077EF9
		public override void Write(byte[] bytes)
		{
			if (this.file == null)
			{
				return;
			}
			this.file.Write(bytes, 0, bytes.Length);
			base.FileTouched();
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00079D1A File Offset: 0x00077F1A
		public override void Flush()
		{
			if (this.file == null)
			{
				return;
			}
			this.file.Flush();
			base.FileTouched();
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00079D36 File Offset: 0x00077F36
		public override void Close()
		{
			if (this.file == null)
			{
				return;
			}
			SingleProcessFileAppender.Logger.Trace("Closing '{0}'", new object[] { base.FileName });
			this.file.Close();
			this.file = null;
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00079D71 File Offset: 0x00077F71
		public override bool GetFileInfo(out DateTime lastWriteTime, out long fileLength)
		{
			if (this.file != null)
			{
				lastWriteTime = base.LastWriteTime;
				fileLength = this.file.Length;
				return true;
			}
			lastWriteTime = default(DateTime);
			fileLength = 0L;
			return false;
		}

		// Token: 0x04000DFB RID: 3579
		public static readonly IFileAppenderFactory TheFactory = new SingleProcessFileAppender.Factory();

		// Token: 0x04000DFC RID: 3580
		private FileStream file;

		// Token: 0x0200167A RID: 5754
		private class Factory : IFileAppenderFactory
		{
			// Token: 0x0600D82F RID: 55343 RVA: 0x004FD7F8 File Offset: 0x004FB9F8
			BaseFileAppender IFileAppenderFactory.Open(string fileName, ICreateFileParameters parameters)
			{
				return new SingleProcessFileAppender(fileName, parameters);
			}
		}
	}
}
