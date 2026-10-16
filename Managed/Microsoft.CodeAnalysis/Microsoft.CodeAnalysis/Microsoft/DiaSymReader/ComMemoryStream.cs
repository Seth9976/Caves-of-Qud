using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.DiaSymReader
{
	// Token: 0x020000F3 RID: 243
	internal sealed class ComMemoryStream : IUnsafeComStream
	{
		// Token: 0x06000861 RID: 2145 RVA: 0x00018A9C File Offset: 0x00016C9C
		public ComMemoryStream(int chunkSize = 32768)
		{
			this._chunkSize = chunkSize;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00018AB8 File Offset: 0x00016CB8
		public void CopyTo(Stream stream)
		{
			if (stream.CanSeek)
			{
				stream.SetLength(stream.Position + (long)this._length);
			}
			int num = 0;
			int num2;
			for (int i = this._length; i > 0; i -= num2)
			{
				if (num < this._chunks.Count)
				{
					byte[] array = this._chunks[num];
					num2 = Math.Min(array.Length, i);
					stream.Write(array, 0, num2);
					num++;
				}
				else
				{
					num2 = i;
					for (int j = 0; j < num2; j++)
					{
						stream.WriteByte(0);
					}
				}
			}
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00018B41 File Offset: 0x00016D41
		public IEnumerable<ArraySegment<byte>> GetChunks()
		{
			int chunkIndex = 0;
			int bytesToCopy;
			for (int remainingBytes = this._length; remainingBytes > 0; remainingBytes -= bytesToCopy)
			{
				byte[] array;
				if (chunkIndex < this._chunks.Count)
				{
					array = this._chunks[chunkIndex];
					bytesToCopy = Math.Min(array.Length, remainingBytes);
					int num = chunkIndex;
					chunkIndex = num + 1;
				}
				else
				{
					array = new byte[remainingBytes];
					bytesToCopy = remainingBytes;
				}
				yield return new ArraySegment<byte>(array, 0, bytesToCopy);
			}
			yield break;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00018B54 File Offset: 0x00016D54
		private unsafe static void ZeroMemory(byte* dest, int count)
		{
			byte* ptr = dest;
			while (count-- > 0)
			{
				*(ptr++) = 0;
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00018B78 File Offset: 0x00016D78
		unsafe void IUnsafeComStream.Read(byte* pv, int cb, int* pcbRead)
		{
			int num = this._position / this._chunkSize;
			int num2 = this._position % this._chunkSize;
			int num3 = 0;
			int num4 = 0;
			for (;;)
			{
				int num5 = Math.Min(this._length - this._position, Math.Min(cb, this._chunkSize - num2));
				if (num5 == 0)
				{
					break;
				}
				if (num < this._chunks.Count)
				{
					Marshal.Copy(this._chunks[num], num2, (IntPtr)((void*)(pv + num3)), num5);
				}
				else
				{
					ComMemoryStream.ZeroMemory(pv + num3, num5);
				}
				num4 += num5;
				this._position += num5;
				cb -= num5;
				num3 += num5;
				num++;
				num2 = 0;
			}
			if (pcbRead != null)
			{
				*pcbRead = num4;
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00018C33 File Offset: 0x00016E33
		private int SetPosition(int newPos)
		{
			if (newPos < 0)
			{
				newPos = 0;
			}
			this._position = newPos;
			if (newPos > this._length)
			{
				this._length = newPos;
			}
			return newPos;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00018C54 File Offset: 0x00016E54
		unsafe void IUnsafeComStream.Seek(long dlibMove, int origin, long* plibNewPosition)
		{
			int num;
			switch (origin)
			{
			case 0:
				num = this.SetPosition((int)dlibMove);
				break;
			case 1:
				num = this.SetPosition(this._position + (int)dlibMove);
				break;
			case 2:
				num = this.SetPosition(this._length + (int)dlibMove);
				break;
			default:
				throw new ArgumentException(string.Format("{0} ({1}) is invalid.", "origin", origin), "origin");
			}
			if (plibNewPosition != null)
			{
				*plibNewPosition = (long)num;
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00018CCD File Offset: 0x00016ECD
		void IUnsafeComStream.SetSize(long libNewSize)
		{
			this._length = (int)libNewSize;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00018CD8 File Offset: 0x00016ED8
		void IUnsafeComStream.Stat(out STATSTG pstatstg, int grfStatFlag)
		{
			STATSTG statstg = default(STATSTG);
			statstg.cbSize = (long)this._length;
			pstatstg = statstg;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00018D04 File Offset: 0x00016F04
		unsafe void IUnsafeComStream.Write(byte* pv, int cb, int* pcbWritten)
		{
			int i = this._position / this._chunkSize;
			int num = this._position % this._chunkSize;
			int num2 = 0;
			for (;;)
			{
				int num3 = Math.Min(cb, this._chunkSize - num);
				if (num3 == 0)
				{
					break;
				}
				while (i >= this._chunks.Count)
				{
					this._chunks.Add(new byte[this._chunkSize]);
				}
				Marshal.Copy((IntPtr)((void*)(pv + num2)), this._chunks[i], num, num3);
				num2 += num3;
				cb -= num3;
				i++;
				num = 0;
			}
			this.SetPosition(this._position + num2);
			if (pcbWritten != null)
			{
				*pcbWritten = num2;
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00018DAA File Offset: 0x00016FAA
		void IUnsafeComStream.Commit(int grfCommitFlags)
		{
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00018DAC File Offset: 0x00016FAC
		void IUnsafeComStream.Clone(out IStream ppstm)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00018DB3 File Offset: 0x00016FB3
		unsafe void IUnsafeComStream.CopyTo(IStream pstm, long cb, int* pcbRead, int* pcbWritten)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00018DBA File Offset: 0x00016FBA
		void IUnsafeComStream.LockRegion(long libOffset, long cb, int lockType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00018DC1 File Offset: 0x00016FC1
		void IUnsafeComStream.Revert()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00018DC8 File Offset: 0x00016FC8
		void IUnsafeComStream.UnlockRegion(long libOffset, long cb, int lockType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000258 RID: 600
		internal const int STREAM_SEEK_SET = 0;

		// Token: 0x04000259 RID: 601
		internal const int STREAM_SEEK_CUR = 1;

		// Token: 0x0400025A RID: 602
		internal const int STREAM_SEEK_END = 2;

		// Token: 0x0400025B RID: 603
		private readonly int _chunkSize;

		// Token: 0x0400025C RID: 604
		private readonly List<byte[]> _chunks = new List<byte[]>();

		// Token: 0x0400025D RID: 605
		private int _position;

		// Token: 0x0400025E RID: 606
		private int _length;
	}
}
