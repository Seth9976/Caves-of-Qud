using System;
using System.Runtime.InteropServices;

namespace NAudio.Wave.Compression
{
	// Token: 0x0200016D RID: 365
	internal class AcmStreamHeader : IDisposable
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x00015C74 File Offset: 0x00013E74
		public AcmStreamHeader(IntPtr streamHandle, int sourceBufferLength, int destBufferLength)
		{
			this.streamHeader = new AcmStreamHeaderStruct();
			this.sourceBuffer = new byte[sourceBufferLength];
			this.hSourceBuffer = GCHandle.Alloc(this.sourceBuffer, 3);
			this.destBuffer = new byte[destBufferLength];
			this.hDestBuffer = GCHandle.Alloc(this.destBuffer, 3);
			this.streamHandle = streamHandle;
			this.firstTime = true;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00015CDC File Offset: 0x00013EDC
		private void Prepare()
		{
			this.streamHeader.cbStruct = Marshal.SizeOf(this.streamHeader);
			this.streamHeader.sourceBufferLength = this.sourceBuffer.Length;
			this.streamHeader.sourceBufferPointer = this.hSourceBuffer.AddrOfPinnedObject();
			this.streamHeader.destBufferLength = this.destBuffer.Length;
			this.streamHeader.destBufferPointer = this.hDestBuffer.AddrOfPinnedObject();
			MmException.Try(AcmInterop.acmStreamPrepareHeader(this.streamHandle, this.streamHeader, 0), "acmStreamPrepareHeader");
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00015D70 File Offset: 0x00013F70
		private void Unprepare()
		{
			this.streamHeader.sourceBufferLength = this.sourceBuffer.Length;
			this.streamHeader.sourceBufferPointer = this.hSourceBuffer.AddrOfPinnedObject();
			this.streamHeader.destBufferLength = this.destBuffer.Length;
			this.streamHeader.destBufferPointer = this.hDestBuffer.AddrOfPinnedObject();
			MmResult mmResult = AcmInterop.acmStreamUnprepareHeader(this.streamHandle, this.streamHeader, 0);
			if (mmResult != MmResult.NoError)
			{
				throw new MmException(mmResult, "acmStreamUnprepareHeader");
			}
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00015DF1 File Offset: 0x00013FF1
		public void Reposition()
		{
			this.firstTime = true;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00015DFC File Offset: 0x00013FFC
		public int Convert(int bytesToConvert, out int sourceBytesConverted)
		{
			this.Prepare();
			try
			{
				this.streamHeader.sourceBufferLength = bytesToConvert;
				this.streamHeader.sourceBufferLengthUsed = bytesToConvert;
				AcmStreamConvertFlags acmStreamConvertFlags = (this.firstTime ? (AcmStreamConvertFlags.BlockAlign | AcmStreamConvertFlags.Start) : AcmStreamConvertFlags.BlockAlign);
				MmException.Try(AcmInterop.acmStreamConvert(this.streamHandle, this.streamHeader, acmStreamConvertFlags), "acmStreamConvert");
				this.firstTime = false;
				sourceBytesConverted = this.streamHeader.sourceBufferLengthUsed;
			}
			finally
			{
				this.Unprepare();
			}
			return this.streamHeader.destBufferLengthUsed;
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00015E8C File Offset: 0x0001408C
		public byte[] SourceBuffer
		{
			get
			{
				return this.sourceBuffer;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00015E94 File Offset: 0x00014094
		public byte[] DestBuffer
		{
			get
			{
				return this.destBuffer;
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00015E9C File Offset: 0x0001409C
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00015EAB File Offset: 0x000140AB
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.sourceBuffer = null;
				this.destBuffer = null;
				this.hSourceBuffer.Free();
				this.hDestBuffer.Free();
			}
			this.disposed = true;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00015EE0 File Offset: 0x000140E0
		~AcmStreamHeader()
		{
			this.Dispose(false);
		}

		// Token: 0x0400080C RID: 2060
		private AcmStreamHeaderStruct streamHeader;

		// Token: 0x0400080D RID: 2061
		private byte[] sourceBuffer;

		// Token: 0x0400080E RID: 2062
		private GCHandle hSourceBuffer;

		// Token: 0x0400080F RID: 2063
		private byte[] destBuffer;

		// Token: 0x04000810 RID: 2064
		private GCHandle hDestBuffer;

		// Token: 0x04000811 RID: 2065
		private IntPtr streamHandle;

		// Token: 0x04000812 RID: 2066
		private bool firstTime;

		// Token: 0x04000813 RID: 2067
		private bool disposed;
	}
}
