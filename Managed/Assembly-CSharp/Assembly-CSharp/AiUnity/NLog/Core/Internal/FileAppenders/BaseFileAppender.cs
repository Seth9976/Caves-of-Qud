using System;
using System.IO;
using System.Security;
using System.Threading;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Time;

namespace AiUnity.NLog.Core.Internal.FileAppenders
{
	// Token: 0x0200027C RID: 636
	[SecuritySafeCritical]
	internal abstract class BaseFileAppender : IDisposable
	{
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x0007997F File Offset: 0x00077B7F
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00079988 File Offset: 0x00077B88
		public BaseFileAppender(string fileName, ICreateFileParameters createParameters)
		{
			this.CreateFileParameters = createParameters;
			this.FileName = fileName;
			this.OpenTime = TimeSource.Current.Time.ToLocalTime();
			this.LastWriteTime = DateTime.MinValue;
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x000799D7 File Offset: 0x00077BD7
		// (set) Token: 0x060016C5 RID: 5829 RVA: 0x000799DF File Offset: 0x00077BDF
		public string FileName { get; private set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x000799E8 File Offset: 0x00077BE8
		// (set) Token: 0x060016C7 RID: 5831 RVA: 0x000799F0 File Offset: 0x00077BF0
		public DateTime LastWriteTime { get; private set; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x000799F9 File Offset: 0x00077BF9
		// (set) Token: 0x060016C9 RID: 5833 RVA: 0x00079A01 File Offset: 0x00077C01
		public DateTime OpenTime { get; private set; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x00079A0A File Offset: 0x00077C0A
		// (set) Token: 0x060016CB RID: 5835 RVA: 0x00079A12 File Offset: 0x00077C12
		public ICreateFileParameters CreateFileParameters { get; private set; }

		// Token: 0x060016CC RID: 5836
		public abstract void Write(byte[] bytes);

		// Token: 0x060016CD RID: 5837
		public abstract void Flush();

		// Token: 0x060016CE RID: 5838
		public abstract void Close();

		// Token: 0x060016CF RID: 5839
		public abstract bool GetFileInfo(out DateTime lastWriteTime, out long fileLength);

		// Token: 0x060016D0 RID: 5840 RVA: 0x00079A1B File Offset: 0x00077C1B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00079A2A File Offset: 0x00077C2A
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x00079A38 File Offset: 0x00077C38
		protected void FileTouched()
		{
			this.LastWriteTime = TimeSource.Current.Time.ToLocalTime();
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00079A5D File Offset: 0x00077C5D
		protected void FileTouched(DateTime dateTime)
		{
			this.LastWriteTime = dateTime;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00079A68 File Offset: 0x00077C68
		protected FileStream CreateFileStream(bool allowConcurrentWrite)
		{
			int num = this.CreateFileParameters.ConcurrentWriteAttemptDelay;
			BaseFileAppender.Logger.Trace("Opening {0} with concurrentWrite={1}", new object[] { this.FileName, allowConcurrentWrite });
			for (int i = 0; i < this.CreateFileParameters.ConcurrentWriteAttempts; i++)
			{
				try
				{
					try
					{
						return this.TryCreateFileStream(allowConcurrentWrite);
					}
					catch (DirectoryNotFoundException)
					{
						if (!this.CreateFileParameters.CreateDirs)
						{
							throw;
						}
						Directory.CreateDirectory(Path.GetDirectoryName(this.FileName));
						return this.TryCreateFileStream(allowConcurrentWrite);
					}
				}
				catch (IOException)
				{
					if (!this.CreateFileParameters.ConcurrentWrites || !allowConcurrentWrite || i + 1 == this.CreateFileParameters.ConcurrentWriteAttempts)
					{
						throw;
					}
					int num2 = this.random.Next(num);
					BaseFileAppender.Logger.Warn("Attempt #{0} to open {1} failed. Sleeping for {2}ms", new object[] { i, this.FileName, num2 });
					num *= 2;
					Thread.Sleep(num2);
				}
			}
			throw new InvalidOperationException("Should not be reached.");
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00079B94 File Offset: 0x00077D94
		private FileStream TryCreateFileStream(bool allowConcurrentWrite)
		{
			FileShare fileShare = FileShare.Read;
			if (allowConcurrentWrite)
			{
				fileShare = FileShare.ReadWrite;
			}
			return new FileStream(this.FileName, FileMode.Append, FileAccess.Write, fileShare, this.CreateFileParameters.BufferSize);
		}

		// Token: 0x04000DF3 RID: 3571
		private readonly Random random = new Random();
	}
}
