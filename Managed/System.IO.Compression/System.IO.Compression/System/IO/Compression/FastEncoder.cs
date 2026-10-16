using System;

namespace System.IO.Compression
{
	// Token: 0x0200001D RID: 29
	internal sealed class FastEncoder
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00004463 File Offset: 0x00002663
		public FastEncoder()
		{
			this._inputWindow = new FastEncoderWindow();
			this._currentMatch = new Match();
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00004481 File Offset: 0x00002681
		internal int BytesInHistory
		{
			get
			{
				return this._inputWindow.BytesAvailable;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000448E File Offset: 0x0000268E
		internal DeflateInput UnprocessedInput
		{
			get
			{
				return this._inputWindow.UnprocessedInput;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000449B File Offset: 0x0000269B
		internal void FlushInput()
		{
			this._inputWindow.FlushWindow();
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000044A8 File Offset: 0x000026A8
		internal double LastCompressionRatio
		{
			get
			{
				return this._lastCompressionRatio;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000044B0 File Offset: 0x000026B0
		internal void GetBlock(DeflateInput input, OutputBuffer output, int maxBytesToCopy)
		{
			FastEncoder.WriteDeflatePreamble(output);
			this.GetCompressedOutput(input, output, maxBytesToCopy);
			this.WriteEndOfBlock(output);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000044C8 File Offset: 0x000026C8
		internal void GetCompressedData(DeflateInput input, OutputBuffer output)
		{
			this.GetCompressedOutput(input, output, -1);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000044D3 File Offset: 0x000026D3
		internal void GetBlockHeader(OutputBuffer output)
		{
			FastEncoder.WriteDeflatePreamble(output);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000044DB File Offset: 0x000026DB
		internal void GetBlockFooter(OutputBuffer output)
		{
			this.WriteEndOfBlock(output);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000044E4 File Offset: 0x000026E4
		private void GetCompressedOutput(DeflateInput input, OutputBuffer output, int maxBytesToCopy)
		{
			int bytesWritten = output.BytesWritten;
			int num = 0;
			int num2 = this.BytesInHistory + input.Count;
			do
			{
				int num3 = ((input.Count < this._inputWindow.FreeWindowSpace) ? input.Count : this._inputWindow.FreeWindowSpace);
				if (maxBytesToCopy >= 1)
				{
					num3 = Math.Min(num3, maxBytesToCopy - num);
				}
				if (num3 > 0)
				{
					this._inputWindow.CopyBytes(input.Buffer, input.StartIndex, num3);
					input.ConsumeBytes(num3);
					num += num3;
				}
				this.GetCompressedOutput(output);
			}
			while (this.SafeToWriteTo(output) && this.InputAvailable(input) && (maxBytesToCopy < 1 || num < maxBytesToCopy));
			int num4 = output.BytesWritten - bytesWritten;
			int num5 = this.BytesInHistory + input.Count;
			int num6 = num2 - num5;
			if (num4 != 0)
			{
				this._lastCompressionRatio = (double)num4 / (double)num6;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000045BC File Offset: 0x000027BC
		private void GetCompressedOutput(OutputBuffer output)
		{
			while (this._inputWindow.BytesAvailable > 0 && this.SafeToWriteTo(output))
			{
				this._inputWindow.GetNextSymbolOrMatch(this._currentMatch);
				if (this._currentMatch.State == MatchState.HasSymbol)
				{
					FastEncoder.WriteChar(this._currentMatch.Symbol, output);
				}
				else if (this._currentMatch.State == MatchState.HasMatch)
				{
					FastEncoder.WriteMatch(this._currentMatch.Length, this._currentMatch.Position, output);
				}
				else
				{
					FastEncoder.WriteChar(this._currentMatch.Symbol, output);
					FastEncoder.WriteMatch(this._currentMatch.Length, this._currentMatch.Position, output);
				}
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004674 File Offset: 0x00002874
		private bool InputAvailable(DeflateInput input)
		{
			return input.Count > 0 || this.BytesInHistory > 0;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000468A File Offset: 0x0000288A
		private bool SafeToWriteTo(OutputBuffer output)
		{
			return output.FreeBytes > 16;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004698 File Offset: 0x00002898
		private void WriteEndOfBlock(OutputBuffer output)
		{
			uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[256];
			int num2 = (int)(num & 31U);
			output.WriteBits(num2, num >> 5);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000046C0 File Offset: 0x000028C0
		internal static void WriteMatch(int matchLen, int matchPos, OutputBuffer output)
		{
			uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[254 + matchLen];
			int num2 = (int)(num & 31U);
			if (num2 <= 16)
			{
				output.WriteBits(num2, num >> 5);
			}
			else
			{
				output.WriteBits(16, (num >> 5) & 65535U);
				output.WriteBits(num2 - 16, num >> 21);
			}
			num = FastEncoderStatics.FastEncoderDistanceCodeInfo[FastEncoderStatics.GetSlot(matchPos)];
			output.WriteBits((int)(num & 15U), num >> 8);
			int num3 = (int)((num >> 4) & 15U);
			if (num3 != 0)
			{
				output.WriteBits(num3, (uint)(matchPos & (int)FastEncoderStatics.BitMask[num3]));
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004744 File Offset: 0x00002944
		internal static void WriteChar(byte b, OutputBuffer output)
		{
			uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[(int)b];
			output.WriteBits((int)(num & 31U), num >> 5);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004766 File Offset: 0x00002966
		internal static void WriteDeflatePreamble(OutputBuffer output)
		{
			output.WriteBytes(FastEncoderStatics.FastEncoderTreeStructureData, 0, FastEncoderStatics.FastEncoderTreeStructureData.Length);
			output.WriteBits(9, 34U);
		}

		// Token: 0x040000EE RID: 238
		private readonly FastEncoderWindow _inputWindow;

		// Token: 0x040000EF RID: 239
		private readonly Match _currentMatch;

		// Token: 0x040000F0 RID: 240
		private double _lastCompressionRatio;
	}
}
