using System;
using System.IO;
using System.Security;

namespace AiUnity.NLog.Core.Internal.FileAppenders
{
	// Token: 0x0200027D RID: 637
	[SecuritySafeCritical]
	internal class CountingSingleProcessFileAppender : BaseFileAppender
	{
		// Token: 0x060016D6 RID: 5846 RVA: 0x00079BC4 File Offset: 0x00077DC4
		public CountingSingleProcessFileAppender(string fileName, ICreateFileParameters parameters)
			: base(fileName, parameters)
		{
			FileInfo fileInfo = new FileInfo(fileName);
			if (fileInfo.Exists)
			{
				base.FileTouched(fileInfo.LastWriteTime);
				this.currentFileLength = fileInfo.Length;
			}
			else
			{
				base.FileTouched();
				this.currentFileLength = 0L;
			}
			this.file = base.CreateFileStream(false);
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00079C1D File Offset: 0x00077E1D
		public override void Close()
		{
			if (this.file != null)
			{
				this.file.Close();
				this.file = null;
			}
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00079C39 File Offset: 0x00077E39
		public override void Flush()
		{
			if (this.file == null)
			{
				return;
			}
			this.file.Flush();
			base.FileTouched();
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00079C55 File Offset: 0x00077E55
		public override bool GetFileInfo(out DateTime lastWriteTime, out long fileLength)
		{
			lastWriteTime = base.LastWriteTime;
			fileLength = this.currentFileLength;
			return true;
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x00079C6C File Offset: 0x00077E6C
		public override void Write(byte[] bytes)
		{
			if (this.file == null)
			{
				return;
			}
			this.currentFileLength += (long)bytes.Length;
			this.file.Write(bytes, 0, bytes.Length);
			base.FileTouched();
		}

		// Token: 0x04000DF8 RID: 3576
		public static readonly IFileAppenderFactory TheFactory = new CountingSingleProcessFileAppender.Factory();

		// Token: 0x04000DF9 RID: 3577
		private FileStream file;

		// Token: 0x04000DFA RID: 3578
		private long currentFileLength;

		// Token: 0x02001679 RID: 5753
		private class Factory : IFileAppenderFactory
		{
			// Token: 0x0600D82D RID: 55341 RVA: 0x004FD7E7 File Offset: 0x004FB9E7
			BaseFileAppender IFileAppenderFactory.Open(string fileName, ICreateFileParameters parameters)
			{
				return new CountingSingleProcessFileAppender(fileName, parameters);
			}
		}
	}
}
