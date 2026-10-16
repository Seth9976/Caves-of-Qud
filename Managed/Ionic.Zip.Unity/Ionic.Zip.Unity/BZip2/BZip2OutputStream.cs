using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Ionic.BZip2
{
	// Token: 0x02000048 RID: 72
	public class BZip2OutputStream : Stream
	{
		// Token: 0x06000384 RID: 900 RVA: 0x00014CBB File Offset: 0x00012EBB
		public BZip2OutputStream(Stream output)
			: this(output, BZip2.MaxBlockSize, false)
		{
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00014CCA File Offset: 0x00012ECA
		public BZip2OutputStream(Stream output, int blockSize)
			: this(output, blockSize, false)
		{
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00014CD5 File Offset: 0x00012ED5
		public BZip2OutputStream(Stream output, bool leaveOpen)
			: this(output, BZip2.MaxBlockSize, leaveOpen)
		{
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00014CE4 File Offset: 0x00012EE4
		public BZip2OutputStream(Stream output, int blockSize, bool leaveOpen)
		{
			if (blockSize < BZip2.MinBlockSize || blockSize > BZip2.MaxBlockSize)
			{
				string text = string.Format("blockSize={0} is out of range; must be between {1} and {2}", blockSize, BZip2.MinBlockSize, BZip2.MaxBlockSize);
				throw new ArgumentException(text, "blockSize");
			}
			this.output = output;
			if (!this.output.CanWrite)
			{
				throw new ArgumentException("The stream is not writable.", "output");
			}
			this.bw = new BitWriter(this.output);
			this.blockSize100k = blockSize;
			this.compressor = new BZip2Compressor(this.bw, blockSize);
			this.leaveOpen = leaveOpen;
			this.combinedCRC = 0U;
			this.EmitHeader();
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00014DA4 File Offset: 0x00012FA4
		public override void Close()
		{
			if (this.output != null)
			{
				Stream stream = this.output;
				this.Finish();
				if (!this.leaveOpen)
				{
					stream.Close();
				}
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00014DD4 File Offset: 0x00012FD4
		public override void Flush()
		{
			if (this.output != null)
			{
				this.bw.Flush();
				this.output.Flush();
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00014DFC File Offset: 0x00012FFC
		private void EmitHeader()
		{
			byte[] array = new byte[] { 66, 90, 104, 0 };
			array[3] = (byte)(48 + this.blockSize100k);
			byte[] array2 = array;
			this.output.Write(array2, 0, array2.Length);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00014E3C File Offset: 0x0001303C
		private void EmitTrailer()
		{
			this.bw.WriteByte(23);
			this.bw.WriteByte(114);
			this.bw.WriteByte(69);
			this.bw.WriteByte(56);
			this.bw.WriteByte(80);
			this.bw.WriteByte(144);
			this.bw.WriteInt(this.combinedCRC);
			this.bw.FinishAndPad();
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00014EB8 File Offset: 0x000130B8
		private void Finish()
		{
			try
			{
				int totalBytesWrittenOut = this.bw.TotalBytesWrittenOut;
				this.compressor.CompressAndWrite();
				this.combinedCRC = (this.combinedCRC << 1) | (this.combinedCRC >> 31);
				this.combinedCRC ^= this.compressor.Crc32;
				this.EmitTrailer();
			}
			finally
			{
				this.output = null;
				this.compressor = null;
				this.bw = null;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00014F3C File Offset: 0x0001313C
		public int BlockSize
		{
			get
			{
				return this.blockSize100k;
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00014F44 File Offset: 0x00013144
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (offset < 0)
			{
				throw new IndexOutOfRangeException(string.Format("offset ({0}) must be > 0", offset));
			}
			if (count < 0)
			{
				throw new IndexOutOfRangeException(string.Format("count ({0}) must be > 0", count));
			}
			if (offset + count > buffer.Length)
			{
				throw new IndexOutOfRangeException(string.Format("offset({0}) count({1}) bLength({2})", offset, count, buffer.Length));
			}
			if (this.output == null)
			{
				throw new IOException("the stream is not open");
			}
			if (count == 0)
			{
				return;
			}
			int num = 0;
			int num2 = count;
			do
			{
				int num3 = this.compressor.Fill(buffer, offset, num2);
				if (num3 != num2)
				{
					int totalBytesWrittenOut = this.bw.TotalBytesWrittenOut;
					this.compressor.CompressAndWrite();
					this.combinedCRC = (this.combinedCRC << 1) | (this.combinedCRC >> 31);
					this.combinedCRC ^= this.compressor.Crc32;
					offset += num3;
				}
				num2 -= num3;
				num += num3;
			}
			while (num2 > 0);
			this.totalBytesWrittenIn += num;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00015045 File Offset: 0x00013245
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00015048 File Offset: 0x00013248
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0001504B File Offset: 0x0001324B
		public override bool CanWrite
		{
			get
			{
				if (this.output == null)
				{
					throw new ObjectDisposedException("BZip2Stream");
				}
				return this.output.CanWrite;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0001506B File Offset: 0x0001326B
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00015072 File Offset: 0x00013272
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0001507B File Offset: 0x0001327B
		public override long Position
		{
			get
			{
				return (long)this.totalBytesWrittenIn;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00015082 File Offset: 0x00013282
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00015089 File Offset: 0x00013289
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00015090 File Offset: 0x00013290
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00015098 File Offset: 0x00013298
		[Conditional("Trace")]
		private void TraceOutput(BZip2OutputStream.TraceBits bits, string format, params object[] varParams)
		{
			if ((bits & this.desiredTrace) != BZip2OutputStream.TraceBits.None)
			{
				int hashCode = Thread.CurrentThread.GetHashCode();
				Console.Write("{0:000} PBOS ", hashCode);
				Console.WriteLine(format, varParams);
			}
		}

		// Token: 0x04000221 RID: 545
		private int totalBytesWrittenIn;

		// Token: 0x04000222 RID: 546
		private bool leaveOpen;

		// Token: 0x04000223 RID: 547
		private BZip2Compressor compressor;

		// Token: 0x04000224 RID: 548
		private uint combinedCRC;

		// Token: 0x04000225 RID: 549
		private Stream output;

		// Token: 0x04000226 RID: 550
		private BitWriter bw;

		// Token: 0x04000227 RID: 551
		private int blockSize100k;

		// Token: 0x04000228 RID: 552
		private BZip2OutputStream.TraceBits desiredTrace = BZip2OutputStream.TraceBits.Crc | BZip2OutputStream.TraceBits.Write;

		// Token: 0x02000049 RID: 73
		[Flags]
		private enum TraceBits : uint
		{
			// Token: 0x0400022A RID: 554
			None = 0U,
			// Token: 0x0400022B RID: 555
			Crc = 1U,
			// Token: 0x0400022C RID: 556
			Write = 2U,
			// Token: 0x0400022D RID: 557
			All = 4294967295U
		}
	}
}
