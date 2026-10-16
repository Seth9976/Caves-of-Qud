using System;
using System.Diagnostics;

namespace System.IO.Compression
{
	// Token: 0x0200001F RID: 31
	internal sealed class FastEncoderWindow
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x0000490F File Offset: 0x00002B0F
		public FastEncoderWindow()
		{
			this.ResetWindow();
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000491D File Offset: 0x00002B1D
		public int BytesAvailable
		{
			get
			{
				return this._bufEnd - this._bufPos;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000492C File Offset: 0x00002B2C
		public DeflateInput UnprocessedInput
		{
			get
			{
				return new DeflateInput
				{
					Buffer = this._window,
					StartIndex = this._bufPos,
					Count = this._bufEnd - this._bufPos
				};
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000495E File Offset: 0x00002B5E
		public void FlushWindow()
		{
			this.ResetWindow();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004968 File Offset: 0x00002B68
		private void ResetWindow()
		{
			this._window = new byte[16646];
			this._prev = new ushort[8450];
			this._lookup = new ushort[2048];
			this._bufPos = 8192;
			this._bufEnd = this._bufPos;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000049BC File Offset: 0x00002BBC
		public int FreeWindowSpace
		{
			get
			{
				return 16384 - this._bufEnd;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000049CA File Offset: 0x00002BCA
		public void CopyBytes(byte[] inputBuffer, int startIndex, int count)
		{
			Array.Copy(inputBuffer, startIndex, this._window, this._bufEnd, count);
			this._bufEnd += count;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000049F0 File Offset: 0x00002BF0
		public void MoveWindows()
		{
			Array.Copy(this._window, this._bufPos - 8192, this._window, 0, 8192);
			for (int i = 0; i < 2048; i++)
			{
				int num = (int)(this._lookup[i] - 8192);
				if (num <= 0)
				{
					this._lookup[i] = 0;
				}
				else
				{
					this._lookup[i] = (ushort)num;
				}
			}
			for (int i = 0; i < 8192; i++)
			{
				long num2 = (long)((ulong)this._prev[i] - 8192UL);
				if (num2 <= 0L)
				{
					this._prev[i] = 0;
				}
				else
				{
					this._prev[i] = (ushort)num2;
				}
			}
			this._bufPos = 8192;
			this._bufEnd = this._bufPos;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004AAA File Offset: 0x00002CAA
		private uint HashValue(uint hash, byte b)
		{
			return (hash << 4) ^ (uint)b;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004AB4 File Offset: 0x00002CB4
		private uint InsertString(ref uint hash)
		{
			hash = this.HashValue(hash, this._window[this._bufPos + 2]);
			uint num = (uint)this._lookup[(int)(hash & 2047U)];
			this._lookup[(int)(hash & 2047U)] = (ushort)this._bufPos;
			this._prev[this._bufPos & 8191] = (ushort)num;
			return num;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004B18 File Offset: 0x00002D18
		private void InsertStrings(ref uint hash, int matchLen)
		{
			if (this._bufEnd - this._bufPos <= matchLen)
			{
				this._bufPos += matchLen - 1;
				return;
			}
			while (--matchLen > 0)
			{
				this.InsertString(ref hash);
				this._bufPos++;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004B68 File Offset: 0x00002D68
		internal bool GetNextSymbolOrMatch(Match match)
		{
			uint num = this.HashValue(0U, this._window[this._bufPos]);
			num = this.HashValue(num, this._window[this._bufPos + 1]);
			int num2 = 0;
			int num3;
			if (this._bufEnd - this._bufPos <= 3)
			{
				num3 = 0;
			}
			else
			{
				int num4 = (int)this.InsertString(ref num);
				if (num4 != 0)
				{
					num3 = this.FindMatch(num4, out num2, 32, 32);
					if (this._bufPos + num3 > this._bufEnd)
					{
						num3 = this._bufEnd - this._bufPos;
					}
				}
				else
				{
					num3 = 0;
				}
			}
			if (num3 < 3)
			{
				match.State = MatchState.HasSymbol;
				match.Symbol = this._window[this._bufPos];
				this._bufPos++;
			}
			else
			{
				this._bufPos++;
				if (num3 <= 6)
				{
					int num5 = 0;
					int num6 = (int)this.InsertString(ref num);
					int num7;
					if (num6 != 0)
					{
						num7 = this.FindMatch(num6, out num5, (num3 < 4) ? 32 : 8, 32);
						if (this._bufPos + num7 > this._bufEnd)
						{
							num7 = this._bufEnd - this._bufPos;
						}
					}
					else
					{
						num7 = 0;
					}
					if (num7 > num3)
					{
						match.State = MatchState.HasSymbolAndMatch;
						match.Symbol = this._window[this._bufPos - 1];
						match.Position = num5;
						match.Length = num7;
						this._bufPos++;
						num3 = num7;
						this.InsertStrings(ref num, num3);
					}
					else
					{
						match.State = MatchState.HasMatch;
						match.Position = num2;
						match.Length = num3;
						num3--;
						this._bufPos++;
						this.InsertStrings(ref num, num3);
					}
				}
				else
				{
					match.State = MatchState.HasMatch;
					match.Position = num2;
					match.Length = num3;
					this.InsertStrings(ref num, num3);
				}
			}
			if (this._bufPos == 16384)
			{
				this.MoveWindows();
			}
			return true;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004D38 File Offset: 0x00002F38
		private int FindMatch(int search, out int matchPos, int searchDepth, int niceLength)
		{
			int num = 0;
			int num2 = 0;
			int num3 = this._bufPos - 8192;
			byte b = this._window[this._bufPos];
			while (search > num3)
			{
				if (this._window[search + num] == b)
				{
					int num4 = 0;
					while (num4 < 258 && this._window[this._bufPos + num4] == this._window[search + num4])
					{
						num4++;
					}
					if (num4 > num)
					{
						num = num4;
						num2 = search;
						if (num4 > 32)
						{
							break;
						}
						b = this._window[this._bufPos + num4];
					}
				}
				if (--searchDepth == 0)
				{
					break;
				}
				search = (int)this._prev[search & 8191];
			}
			matchPos = this._bufPos - num2 - 1;
			if (num == 3 && matchPos >= 16384)
			{
				return 0;
			}
			return num;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004461 File Offset: 0x00002661
		[Conditional("DEBUG")]
		private void DebugAssertVerifyHashes()
		{
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004461 File Offset: 0x00002661
		[Conditional("DEBUG")]
		private void DebugAssertRecalculatedHashesAreEqual(int position1, int position2, string message = "")
		{
		}

		// Token: 0x04000103 RID: 259
		private byte[] _window;

		// Token: 0x04000104 RID: 260
		private int _bufPos;

		// Token: 0x04000105 RID: 261
		private int _bufEnd;

		// Token: 0x04000106 RID: 262
		private const int FastEncoderHashShift = 4;

		// Token: 0x04000107 RID: 263
		private const int FastEncoderHashtableSize = 2048;

		// Token: 0x04000108 RID: 264
		private const int FastEncoderHashMask = 2047;

		// Token: 0x04000109 RID: 265
		private const int FastEncoderWindowSize = 8192;

		// Token: 0x0400010A RID: 266
		private const int FastEncoderWindowMask = 8191;

		// Token: 0x0400010B RID: 267
		private const int FastEncoderMatch3DistThreshold = 16384;

		// Token: 0x0400010C RID: 268
		internal const int MaxMatch = 258;

		// Token: 0x0400010D RID: 269
		internal const int MinMatch = 3;

		// Token: 0x0400010E RID: 270
		private const int SearchDepth = 32;

		// Token: 0x0400010F RID: 271
		private const int GoodLength = 4;

		// Token: 0x04000110 RID: 272
		private const int NiceLength = 32;

		// Token: 0x04000111 RID: 273
		private const int LazyMatchThreshold = 6;

		// Token: 0x04000112 RID: 274
		private ushort[] _prev;

		// Token: 0x04000113 RID: 275
		private ushort[] _lookup;
	}
}
