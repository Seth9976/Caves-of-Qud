using System;
using System.Collections.Generic;
using System.IO;

namespace NVorbis
{
	// Token: 0x02000015 RID: 21
	internal class StreamReadBuffer : IDisposable
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00005860 File Offset: 0x00003A60
		internal StreamReadBuffer(Stream source, int initialSize, int maxSize, bool minimalRead)
		{
			StreamReadBuffer.StreamWrapper streamWrapper;
			if (!StreamReadBuffer._lockObjects.TryGetValue(source, ref streamWrapper))
			{
				StreamReadBuffer._lockObjects.Add(source, new StreamReadBuffer.StreamWrapper
				{
					Source = source
				});
				streamWrapper = StreamReadBuffer._lockObjects[source];
				if (source.CanSeek)
				{
					streamWrapper.EofOffset = source.Length;
				}
			}
			else
			{
				streamWrapper.RefCount++;
			}
			initialSize = 2 << (int)Math.Log((double)(initialSize - 1), 2.0);
			maxSize = 1 << (int)Math.Log((double)maxSize, 2.0);
			this._wrapper = streamWrapper;
			this._data = new byte[initialSize];
			this._maxSize = maxSize;
			this._minimalRead = minimalRead;
			this._savedBuffers = new List<StreamReadBuffer.SavedBuffer>();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000592C File Offset: 0x00003B2C
		public void Dispose()
		{
			if (--this._wrapper.RefCount == 0)
			{
				StreamReadBuffer._lockObjects.Remove(this._wrapper.Source);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00005967 File Offset: 0x00003B67
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x0000596F File Offset: 0x00003B6F
		public bool MinimalRead
		{
			get
			{
				return this._minimalRead;
			}
			set
			{
				this._minimalRead = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005978 File Offset: 0x00003B78
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00005980 File Offset: 0x00003B80
		public int MaxSize
		{
			get
			{
				return this._maxSize;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("Must be greater than zero.");
				}
				int num = 1 << (int)Math.Ceiling(Math.Log((double)value, 2.0));
				if (num < this._end)
				{
					if (num < this._end - this._discardCount)
					{
						throw new ArgumentOutOfRangeException("Must be greater than or equal to the number of bytes currently buffered.");
					}
					this.CommitDiscard();
					byte[] array = new byte[num];
					Buffer.BlockCopy(this._data, 0, array, 0, this._end);
					this._data = array;
				}
				this._maxSize = num;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00005A0B File Offset: 0x00003C0B
		public long BaseOffset
		{
			get
			{
				return this._baseOffset + (long)this._discardCount;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00005A1B File Offset: 0x00003C1B
		public int BytesFilled
		{
			get
			{
				return this._end - this._discardCount;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00005A2A File Offset: 0x00003C2A
		public int Length
		{
			get
			{
				return this._data.Length;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005A34 File Offset: 0x00003C34
		internal long BufferEndOffset
		{
			get
			{
				if (this._end - this._discardCount > 0)
				{
					return this._baseOffset + (long)this._discardCount + (long)this._maxSize;
				}
				return this._wrapper.Source.Length;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005A70 File Offset: 0x00003C70
		public int Read(long offset, byte[] buffer, int index, int count)
		{
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0 || index + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (offset >= this._wrapper.EofOffset)
			{
				return 0;
			}
			int num = this.EnsureAvailable(offset, ref count, false);
			Buffer.BlockCopy(this._data, num, buffer, index, count);
			return count;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005AF0 File Offset: 0x00003CF0
		internal int ReadByte(long offset)
		{
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset >= this._wrapper.EofOffset)
			{
				return -1;
			}
			int num = 1;
			int num2 = this.EnsureAvailable(offset, ref num, false);
			if (num == 1)
			{
				return (int)this._data[num2];
			}
			return -1;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005B38 File Offset: 0x00003D38
		private int EnsureAvailable(long offset, ref int count, bool isRecursion)
		{
			if (offset >= this._baseOffset && offset + (long)count < this._baseOffset + (long)this._end)
			{
				return (int)(offset - this._baseOffset);
			}
			if (count > this._maxSize)
			{
				throw new InvalidOperationException("Not enough room in the buffer!  Increase the maximum size and try again.");
			}
			this._versionCounter += 1L;
			if (!isRecursion)
			{
				for (int i = 0; i < this._savedBuffers.Count; i++)
				{
					long num = this._savedBuffers[i].BaseOffset - offset;
					if ((num < 0L && (long)this._savedBuffers[i].End + num > 0L) || (num > 0L && (long)count - num > 0L))
					{
						this.SwapBuffers(this._savedBuffers[i]);
						return this.EnsureAvailable(offset, ref count, true);
					}
				}
			}
			while (this._savedBuffers.Count > 0 && this._savedBuffers[0].VersionSaved + 25L < this._versionCounter)
			{
				this._savedBuffers[0].Buffer = null;
				this._savedBuffers.RemoveAt(0);
			}
			if (offset < this._baseOffset && !this._wrapper.Source.CanSeek)
			{
				throw new InvalidOperationException("Cannot seek before buffer on forward-only streams!");
			}
			int num2;
			int num3;
			this.CalcBuffer(offset, count, out num2, out num3);
			count = this.FillBuffer(offset, count, num2, num3);
			return (int)(offset - this._baseOffset);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005C9C File Offset: 0x00003E9C
		private void SaveBuffer()
		{
			this._savedBuffers.Add(new StreamReadBuffer.SavedBuffer
			{
				Buffer = this._data,
				BaseOffset = this._baseOffset,
				End = this._end,
				DiscardCount = this._discardCount,
				VersionSaved = this._versionCounter
			});
			this._data = null;
			this._end = 0;
			this._discardCount = 0;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005D0C File Offset: 0x00003F0C
		private void CreateNewBuffer(long offset, int count)
		{
			this.SaveBuffer();
			this._data = new byte[Math.Min(2 << (int)Math.Log((double)(count - 1), 2.0), this._maxSize)];
			this._baseOffset = offset;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005D4C File Offset: 0x00003F4C
		private void SwapBuffers(StreamReadBuffer.SavedBuffer savedBuffer)
		{
			this._savedBuffers.Remove(savedBuffer);
			this.SaveBuffer();
			this._data = savedBuffer.Buffer;
			this._baseOffset = savedBuffer.BaseOffset;
			this._end = savedBuffer.End;
			this._discardCount = savedBuffer.DiscardCount;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005D9C File Offset: 0x00003F9C
		private void CalcBuffer(long offset, int count, out int readStart, out int readEnd)
		{
			readStart = 0;
			readEnd = 0;
			if (offset < this._baseOffset)
			{
				if (offset + (long)this._maxSize <= this._baseOffset)
				{
					if (this._baseOffset - (offset + (long)this._maxSize) > (long)this._maxSize)
					{
						this.CreateNewBuffer(offset, count);
					}
					else
					{
						this.EnsureBufferSize(count, false, 0);
					}
					this._baseOffset = offset;
					readEnd = count;
					return;
				}
				readEnd = (int)(offset - this._baseOffset);
				this.EnsureBufferSize(Math.Min((int)(offset + (long)this._maxSize - this._baseOffset), this._end) - readEnd, true, readEnd);
				readEnd = (int)(offset - this._baseOffset) - readEnd;
				return;
			}
			else
			{
				if (offset >= this._baseOffset + (long)this._maxSize)
				{
					if (offset - (this._baseOffset + (long)this._maxSize) > (long)this._maxSize)
					{
						this.CreateNewBuffer(offset, count);
					}
					else
					{
						this.EnsureBufferSize(count, false, 0);
					}
					this._baseOffset = offset;
					readEnd = count;
					return;
				}
				readEnd = (int)(offset + (long)count - this._baseOffset);
				int num = Math.Max(readEnd - this._maxSize, 0);
				this.EnsureBufferSize(readEnd - num, true, num);
				readStart = this._end;
				readEnd = (int)(offset + (long)count - this._baseOffset);
				return;
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005EDC File Offset: 0x000040DC
		private void EnsureBufferSize(int reqSize, bool copyContents, int copyOffset)
		{
			byte[] array = this._data;
			if (reqSize > this._data.Length)
			{
				if (reqSize > this._maxSize)
				{
					if (!this._wrapper.Source.CanSeek && reqSize - this._discardCount > this._maxSize)
					{
						throw new InvalidOperationException("Not enough room in the buffer!  Increase the maximum size and try again.");
					}
					int num = reqSize - this._maxSize;
					copyOffset += num;
					reqSize = this._maxSize;
				}
				else
				{
					int i;
					for (i = this._data.Length; i < reqSize; i *= 2)
					{
					}
					reqSize = i;
				}
				if (reqSize > this._data.Length)
				{
					array = new byte[reqSize];
				}
			}
			if (copyContents)
			{
				if ((copyOffset > 0 && copyOffset < this._end) || (copyOffset == 0 && array != this._data))
				{
					Buffer.BlockCopy(this._data, copyOffset, array, 0, this._end - copyOffset);
					if ((this._discardCount -= copyOffset) < 0)
					{
						this._discardCount = 0;
					}
				}
				else if (copyOffset < 0 && -copyOffset < this._end)
				{
					if (array != this._data || this._end <= -copyOffset)
					{
						Buffer.BlockCopy(this._data, 0, array, -copyOffset, Math.Max(this._end, Math.Min(this._end, this._data.Length + copyOffset)));
					}
					else
					{
						this._end = copyOffset;
					}
					this._discardCount = 0;
				}
				else
				{
					this._end = copyOffset;
					this._discardCount = 0;
				}
				this._baseOffset += (long)copyOffset;
				this._end -= copyOffset;
				if (this._end > array.Length)
				{
					this._end = array.Length;
				}
			}
			else
			{
				this._discardCount = 0;
				this._end = 0;
			}
			this._data = array;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000607C File Offset: 0x0000427C
		private int FillBuffer(long offset, int count, int readStart, int readEnd)
		{
			long num = this._baseOffset + (long)readStart;
			int num2 = readEnd - readStart;
			lock (this._wrapper.LockObject)
			{
				num2 = this.PrepareStreamForRead(num2, num);
				this.ReadStream(readStart, num2, num);
				if (this._end < readStart + num2)
				{
					count = Math.Max(0, (int)(this._baseOffset + (long)this._end - offset));
				}
				else if (!this._minimalRead && this._end < this._data.Length)
				{
					num2 = this._data.Length - this._end;
					num2 = this.PrepareStreamForRead(num2, this._baseOffset + (long)this._end);
					this._end += this._wrapper.Source.Read(this._data, this._end, num2);
				}
			}
			return count;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006164 File Offset: 0x00004364
		private int PrepareStreamForRead(int readCount, long readOffset)
		{
			if (readCount > 0 && this._wrapper.Source.Position != readOffset)
			{
				if (readOffset < this._wrapper.EofOffset)
				{
					if (this._wrapper.Source.CanSeek)
					{
						this._wrapper.Source.Position = readOffset;
					}
					else
					{
						long num = readOffset - this._wrapper.Source.Position;
						if (num < 0L)
						{
							readCount = 0;
						}
						else
						{
							while ((num -= 1L) >= 0L)
							{
								if (this._wrapper.Source.ReadByte() == -1)
								{
									this._wrapper.EofOffset = this._wrapper.Source.Position;
									readCount = 0;
									break;
								}
							}
						}
					}
				}
				else
				{
					readCount = 0;
				}
			}
			return readCount;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006224 File Offset: 0x00004424
		private void ReadStream(int readStart, int readCount, long readOffset)
		{
			while (readCount > 0 && readOffset < this._wrapper.EofOffset)
			{
				int num = this._wrapper.Source.Read(this._data, readStart, readCount);
				if (num == 0)
				{
					break;
				}
				readStart += num;
				readOffset += (long)num;
				readCount -= num;
			}
			if (readStart > this._end)
			{
				this._end = readStart;
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006284 File Offset: 0x00004484
		public void DiscardThrough(long offset)
		{
			int num = (int)(offset - this._baseOffset);
			this._discardCount = Math.Max(num, this._discardCount);
			if (this._discardCount >= this._data.Length)
			{
				this.CommitDiscard();
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000062C4 File Offset: 0x000044C4
		private void CommitDiscard()
		{
			if (this._discardCount >= this._data.Length || this._discardCount >= this._end)
			{
				this._baseOffset += (long)this._discardCount;
				this._end = 0;
			}
			else
			{
				Buffer.BlockCopy(this._data, this._discardCount, this._data, 0, this._end - this._discardCount);
				this._baseOffset += (long)this._discardCount;
				this._end -= this._discardCount;
			}
			this._discardCount = 0;
		}

		// Token: 0x04000065 RID: 101
		private static Dictionary<Stream, StreamReadBuffer.StreamWrapper> _lockObjects = new Dictionary<Stream, StreamReadBuffer.StreamWrapper>();

		// Token: 0x04000066 RID: 102
		private StreamReadBuffer.StreamWrapper _wrapper;

		// Token: 0x04000067 RID: 103
		private int _maxSize;

		// Token: 0x04000068 RID: 104
		private byte[] _data;

		// Token: 0x04000069 RID: 105
		private long _baseOffset;

		// Token: 0x0400006A RID: 106
		private int _end;

		// Token: 0x0400006B RID: 107
		private int _discardCount;

		// Token: 0x0400006C RID: 108
		private bool _minimalRead;

		// Token: 0x0400006D RID: 109
		private long _versionCounter;

		// Token: 0x0400006E RID: 110
		private List<StreamReadBuffer.SavedBuffer> _savedBuffers;

		// Token: 0x02000016 RID: 22
		private class StreamWrapper
		{
			// Token: 0x0400006F RID: 111
			internal Stream Source;

			// Token: 0x04000070 RID: 112
			internal object LockObject = new object();

			// Token: 0x04000071 RID: 113
			internal long EofOffset = long.MaxValue;

			// Token: 0x04000072 RID: 114
			internal int RefCount = 1;
		}

		// Token: 0x02000017 RID: 23
		private class SavedBuffer
		{
			// Token: 0x04000073 RID: 115
			public byte[] Buffer;

			// Token: 0x04000074 RID: 116
			public long BaseOffset;

			// Token: 0x04000075 RID: 117
			public int End;

			// Token: 0x04000076 RID: 118
			public int DiscardCount;

			// Token: 0x04000077 RID: 119
			public long VersionSaved;
		}
	}
}
