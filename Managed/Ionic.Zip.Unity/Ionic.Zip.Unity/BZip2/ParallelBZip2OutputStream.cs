using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Ionic.BZip2
{
	// Token: 0x0200004B RID: 75
	public class ParallelBZip2OutputStream : Stream
	{
		// Token: 0x0600039C RID: 924 RVA: 0x0001511F File Offset: 0x0001331F
		public ParallelBZip2OutputStream(Stream output)
			: this(output, BZip2.MaxBlockSize, false)
		{
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0001512E File Offset: 0x0001332E
		public ParallelBZip2OutputStream(Stream output, int blockSize)
			: this(output, blockSize, false)
		{
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00015139 File Offset: 0x00013339
		public ParallelBZip2OutputStream(Stream output, bool leaveOpen)
			: this(output, BZip2.MaxBlockSize, leaveOpen)
		{
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00015148 File Offset: 0x00013348
		public ParallelBZip2OutputStream(Stream output, int blockSize, bool leaveOpen)
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
			this.leaveOpen = leaveOpen;
			this.combinedCRC = 0U;
			this.MaxWorkers = 16;
			this.EmitHeader();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001521C File Offset: 0x0001341C
		private void InitializePoolOfWorkItems()
		{
			this.toWrite = new Queue<int>();
			this.toFill = new Queue<int>();
			this.pool = new List<WorkItem>();
			int num = ParallelBZip2OutputStream.BufferPairsPerCore * Environment.ProcessorCount;
			num = Math.Min(num, this.MaxWorkers);
			for (int i = 0; i < num; i++)
			{
				this.pool.Add(new WorkItem(i, this.blockSize100k));
				this.toFill.Enqueue(i);
			}
			this.newlyCompressedBlob = new AutoResetEvent(false);
			this.currentlyFilling = -1;
			this.lastFilled = -1;
			this.lastWritten = -1;
			this.latestCompressed = -1;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x000152BA File Offset: 0x000134BA
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x000152C2 File Offset: 0x000134C2
		public int MaxWorkers
		{
			get
			{
				return this._maxWorkers;
			}
			set
			{
				if (value < 4)
				{
					throw new ArgumentException("MaxWorkers", "Value must be 4 or greater.");
				}
				this._maxWorkers = value;
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x000152E0 File Offset: 0x000134E0
		public override void Close()
		{
			if (this.pendingException != null)
			{
				this.handlingException = true;
				Exception ex = this.pendingException;
				this.pendingException = null;
				throw ex;
			}
			if (this.handlingException)
			{
				return;
			}
			if (this.output == null)
			{
				return;
			}
			Stream stream = this.output;
			try
			{
				this.FlushOutput(true);
			}
			finally
			{
				this.output = null;
				this.bw = null;
			}
			if (!this.leaveOpen)
			{
				stream.Close();
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00015364 File Offset: 0x00013564
		private void FlushOutput(bool lastInput)
		{
			if (this.emitting)
			{
				return;
			}
			if (this.currentlyFilling >= 0)
			{
				WorkItem workItem = this.pool[this.currentlyFilling];
				this.CompressOne(workItem);
				this.currentlyFilling = -1;
			}
			if (lastInput)
			{
				this.EmitPendingBuffers(true, false);
				this.EmitTrailer();
				return;
			}
			this.EmitPendingBuffers(false, false);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000153BD File Offset: 0x000135BD
		public override void Flush()
		{
			if (this.output != null)
			{
				this.FlushOutput(false);
				this.bw.Flush();
				this.output.Flush();
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000153EC File Offset: 0x000135EC
		private void EmitHeader()
		{
			byte[] array = new byte[] { 66, 90, 104, 0 };
			array[3] = (byte)(48 + this.blockSize100k);
			byte[] array2 = array;
			this.output.Write(array2, 0, array2.Length);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001542C File Offset: 0x0001362C
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

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x000154A6 File Offset: 0x000136A6
		public int BlockSize
		{
			get
			{
				return this.blockSize100k;
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000154B0 File Offset: 0x000136B0
		public override void Write(byte[] buffer, int offset, int count)
		{
			bool flag = false;
			if (this.output == null)
			{
				throw new IOException("the stream is not open");
			}
			if (this.pendingException != null)
			{
				this.handlingException = true;
				Exception ex = this.pendingException;
				this.pendingException = null;
				throw ex;
			}
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
			if (count == 0)
			{
				return;
			}
			if (!this.firstWriteDone)
			{
				this.InitializePoolOfWorkItems();
				this.firstWriteDone = true;
			}
			int num = 0;
			int num2 = count;
			for (;;)
			{
				this.EmitPendingBuffers(false, flag);
				flag = false;
				int num3;
				if (this.currentlyFilling >= 0)
				{
					num3 = this.currentlyFilling;
					goto IL_0106;
				}
				if (this.toFill.Count != 0)
				{
					num3 = this.toFill.Dequeue();
					this.lastFilled++;
					goto IL_0106;
				}
				flag = true;
				IL_0179:
				if (num2 <= 0)
				{
					goto Block_12;
				}
				continue;
				IL_0106:
				WorkItem workItem = this.pool[num3];
				workItem.ordinal = this.lastFilled;
				int num4 = workItem.Compressor.Fill(buffer, offset, num2);
				if (num4 != num2)
				{
					if (!ThreadPool.QueueUserWorkItem(new WaitCallback(this.CompressOne), workItem))
					{
						break;
					}
					this.currentlyFilling = -1;
					offset += num4;
				}
				else
				{
					this.currentlyFilling = num3;
				}
				num2 -= num4;
				num += num4;
				goto IL_0179;
			}
			throw new Exception("Cannot enqueue workitem");
			Block_12:
			this.totalBytesWrittenIn += (long)num;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001564C File Offset: 0x0001384C
		private void EmitPendingBuffers(bool doAll, bool mustWait)
		{
			if (this.emitting)
			{
				return;
			}
			this.emitting = true;
			if (doAll || mustWait)
			{
				this.newlyCompressedBlob.WaitOne();
			}
			do
			{
				int num = -1;
				int num2 = (doAll ? 200 : (mustWait ? (-1) : 0));
				int num3 = -1;
				do
				{
					if (Monitor.TryEnter(this.toWrite, num2))
					{
						num3 = -1;
						try
						{
							if (this.toWrite.Count > 0)
							{
								num3 = this.toWrite.Dequeue();
							}
						}
						finally
						{
							Monitor.Exit(this.toWrite);
						}
						if (num3 >= 0)
						{
							WorkItem workItem = this.pool[num3];
							if (workItem.ordinal != this.lastWritten + 1)
							{
								lock (this.toWrite)
								{
									this.toWrite.Enqueue(num3);
								}
								if (num == num3)
								{
									this.newlyCompressedBlob.WaitOne();
									num = -1;
								}
								else if (num == -1)
								{
									num = num3;
								}
							}
							else
							{
								num = -1;
								BitWriter bitWriter = workItem.bw;
								bitWriter.Flush();
								MemoryStream ms = workItem.ms;
								ms.Seek(0L, 0);
								long num4 = 0L;
								byte[] array = new byte[1024];
								int num5;
								while ((num5 = ms.Read(array, 0, array.Length)) > 0)
								{
									for (int i = 0; i < num5; i++)
									{
										this.bw.WriteByte(array[i]);
									}
									num4 += (long)num5;
								}
								if (bitWriter.NumRemainingBits > 0)
								{
									this.bw.WriteBits(bitWriter.NumRemainingBits, (uint)bitWriter.RemainingBits);
								}
								this.combinedCRC = (this.combinedCRC << 1) | (this.combinedCRC >> 31);
								this.combinedCRC ^= workItem.Compressor.Crc32;
								this.totalBytesWrittenOut += num4;
								bitWriter.Reset();
								this.lastWritten = workItem.ordinal;
								workItem.ordinal = -1;
								this.toFill.Enqueue(workItem.index);
								if (num2 == -1)
								{
									num2 = 0;
								}
							}
						}
					}
					else
					{
						num3 = -1;
					}
				}
				while (num3 >= 0);
			}
			while (doAll && this.lastWritten != this.latestCompressed);
			this.emitting = false;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00015884 File Offset: 0x00013A84
		private void CompressOne(object wi)
		{
			WorkItem workItem = (WorkItem)wi;
			try
			{
				workItem.Compressor.CompressAndWrite();
				lock (this.latestLock)
				{
					if (workItem.ordinal > this.latestCompressed)
					{
						this.latestCompressed = workItem.ordinal;
					}
				}
				lock (this.toWrite)
				{
					this.toWrite.Enqueue(workItem.index);
				}
				this.newlyCompressedBlob.Set();
			}
			catch (Exception ex)
			{
				lock (this.eLock)
				{
					if (this.pendingException != null)
					{
						this.pendingException = ex;
					}
				}
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0001596C File Offset: 0x00013B6C
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0001596F File Offset: 0x00013B6F
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00015972 File Offset: 0x00013B72
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

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00015992 File Offset: 0x00013B92
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00015999 File Offset: 0x00013B99
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x000159A1 File Offset: 0x00013BA1
		public override long Position
		{
			get
			{
				return this.totalBytesWrittenIn;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x000159A8 File Offset: 0x00013BA8
		public long BytesWrittenOut
		{
			get
			{
				return this.totalBytesWrittenOut;
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000159B0 File Offset: 0x00013BB0
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000159B7 File Offset: 0x00013BB7
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000159BE File Offset: 0x00013BBE
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000159C8 File Offset: 0x00013BC8
		[Conditional("Trace")]
		private void TraceOutput(ParallelBZip2OutputStream.TraceBits bits, string format, params object[] varParams)
		{
			if ((bits & this.desiredTrace) != ParallelBZip2OutputStream.TraceBits.None)
			{
				lock (this.outputLock)
				{
					int hashCode = Thread.CurrentThread.GetHashCode();
					Console.Write("{0:000} PBOS ", hashCode);
					Console.WriteLine(format, varParams);
				}
			}
		}

		// Token: 0x04000233 RID: 563
		private static readonly int BufferPairsPerCore = 4;

		// Token: 0x04000234 RID: 564
		private int _maxWorkers;

		// Token: 0x04000235 RID: 565
		private bool firstWriteDone;

		// Token: 0x04000236 RID: 566
		private int lastFilled;

		// Token: 0x04000237 RID: 567
		private int lastWritten;

		// Token: 0x04000238 RID: 568
		private int latestCompressed;

		// Token: 0x04000239 RID: 569
		private int currentlyFilling;

		// Token: 0x0400023A RID: 570
		private volatile Exception pendingException;

		// Token: 0x0400023B RID: 571
		private bool handlingException;

		// Token: 0x0400023C RID: 572
		private bool emitting;

		// Token: 0x0400023D RID: 573
		private Queue<int> toWrite;

		// Token: 0x0400023E RID: 574
		private Queue<int> toFill;

		// Token: 0x0400023F RID: 575
		private List<WorkItem> pool;

		// Token: 0x04000240 RID: 576
		private object latestLock = new object();

		// Token: 0x04000241 RID: 577
		private object eLock = new object();

		// Token: 0x04000242 RID: 578
		private object outputLock = new object();

		// Token: 0x04000243 RID: 579
		private AutoResetEvent newlyCompressedBlob;

		// Token: 0x04000244 RID: 580
		private long totalBytesWrittenIn;

		// Token: 0x04000245 RID: 581
		private long totalBytesWrittenOut;

		// Token: 0x04000246 RID: 582
		private bool leaveOpen;

		// Token: 0x04000247 RID: 583
		private uint combinedCRC;

		// Token: 0x04000248 RID: 584
		private Stream output;

		// Token: 0x04000249 RID: 585
		private BitWriter bw;

		// Token: 0x0400024A RID: 586
		private int blockSize100k;

		// Token: 0x0400024B RID: 587
		private ParallelBZip2OutputStream.TraceBits desiredTrace = ParallelBZip2OutputStream.TraceBits.Crc | ParallelBZip2OutputStream.TraceBits.Write;

		// Token: 0x0200004C RID: 76
		[Flags]
		private enum TraceBits : uint
		{
			// Token: 0x0400024D RID: 589
			None = 0U,
			// Token: 0x0400024E RID: 590
			Crc = 1U,
			// Token: 0x0400024F RID: 591
			Write = 2U,
			// Token: 0x04000250 RID: 592
			All = 4294967295U
		}
	}
}
