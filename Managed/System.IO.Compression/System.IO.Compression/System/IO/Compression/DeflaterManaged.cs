using System;

namespace System.IO.Compression
{
	// Token: 0x0200001B RID: 27
	internal sealed class DeflaterManaged : IDisposable
	{
		// Token: 0x0600009A RID: 154 RVA: 0x000040BA File Offset: 0x000022BA
		internal DeflaterManaged()
		{
			this._deflateEncoder = new FastEncoder();
			this._copyEncoder = new CopyEncoder();
			this._input = new DeflateInput();
			this._output = new OutputBuffer();
			this._processingState = DeflaterManaged.DeflaterState.NotStarted;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000040F5 File Offset: 0x000022F5
		internal bool NeedsInput()
		{
			return this._input.Count == 0 && this._deflateEncoder.BytesInHistory == 0;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004114 File Offset: 0x00002314
		internal void SetInput(byte[] inputBuffer, int startIndex, int count)
		{
			this._input.Buffer = inputBuffer;
			this._input.Count = count;
			this._input.StartIndex = startIndex;
			if (count > 0 && count < 256)
			{
				DeflaterManaged.DeflaterState processingState = this._processingState;
				if (processingState != DeflaterManaged.DeflaterState.NotStarted)
				{
					if (processingState == DeflaterManaged.DeflaterState.CompressThenCheck)
					{
						this._processingState = DeflaterManaged.DeflaterState.HandlingSmallData;
						return;
					}
					if (processingState != DeflaterManaged.DeflaterState.CheckingForIncompressible)
					{
						return;
					}
				}
				this._processingState = DeflaterManaged.DeflaterState.StartingSmallData;
				return;
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004174 File Offset: 0x00002374
		internal int GetDeflateOutput(byte[] outputBuffer)
		{
			this._output.UpdateBuffer(outputBuffer);
			switch (this._processingState)
			{
			case DeflaterManaged.DeflaterState.NotStarted:
			{
				DeflateInput.InputState inputState = this._input.DumpState();
				OutputBuffer.BufferState bufferState = this._output.DumpState();
				this._deflateEncoder.GetBlockHeader(this._output);
				this._deflateEncoder.GetCompressedData(this._input, this._output);
				if (!this.UseCompressed(this._deflateEncoder.LastCompressionRatio))
				{
					this._input.RestoreState(inputState);
					this._output.RestoreState(bufferState);
					this._copyEncoder.GetBlock(this._input, this._output, false);
					this.FlushInputWindows();
					this._processingState = DeflaterManaged.DeflaterState.CheckingForIncompressible;
					goto IL_023A;
				}
				this._processingState = DeflaterManaged.DeflaterState.CompressThenCheck;
				goto IL_023A;
			}
			case DeflaterManaged.DeflaterState.SlowDownForIncompressible1:
				this._deflateEncoder.GetBlockFooter(this._output);
				this._processingState = DeflaterManaged.DeflaterState.SlowDownForIncompressible2;
				break;
			case DeflaterManaged.DeflaterState.SlowDownForIncompressible2:
				break;
			case DeflaterManaged.DeflaterState.StartingSmallData:
				this._deflateEncoder.GetBlockHeader(this._output);
				this._processingState = DeflaterManaged.DeflaterState.HandlingSmallData;
				goto IL_0223;
			case DeflaterManaged.DeflaterState.CompressThenCheck:
				this._deflateEncoder.GetCompressedData(this._input, this._output);
				if (!this.UseCompressed(this._deflateEncoder.LastCompressionRatio))
				{
					this._processingState = DeflaterManaged.DeflaterState.SlowDownForIncompressible1;
					this._inputFromHistory = this._deflateEncoder.UnprocessedInput;
					goto IL_023A;
				}
				goto IL_023A;
			case DeflaterManaged.DeflaterState.CheckingForIncompressible:
			{
				DeflateInput.InputState inputState2 = this._input.DumpState();
				OutputBuffer.BufferState bufferState2 = this._output.DumpState();
				this._deflateEncoder.GetBlock(this._input, this._output, 8072);
				if (!this.UseCompressed(this._deflateEncoder.LastCompressionRatio))
				{
					this._input.RestoreState(inputState2);
					this._output.RestoreState(bufferState2);
					this._copyEncoder.GetBlock(this._input, this._output, false);
					this.FlushInputWindows();
					goto IL_023A;
				}
				goto IL_023A;
			}
			case DeflaterManaged.DeflaterState.HandlingSmallData:
				goto IL_0223;
			default:
				goto IL_023A;
			}
			if (this._inputFromHistory.Count > 0)
			{
				this._copyEncoder.GetBlock(this._inputFromHistory, this._output, false);
			}
			if (this._inputFromHistory.Count == 0)
			{
				this._deflateEncoder.FlushInput();
				this._processingState = DeflaterManaged.DeflaterState.CheckingForIncompressible;
				goto IL_023A;
			}
			goto IL_023A;
			IL_0223:
			this._deflateEncoder.GetCompressedData(this._input, this._output);
			IL_023A:
			return this._output.BytesWritten;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000043C8 File Offset: 0x000025C8
		internal bool Finish(byte[] outputBuffer, out int bytesRead)
		{
			if (this._processingState == DeflaterManaged.DeflaterState.NotStarted)
			{
				bytesRead = 0;
				return true;
			}
			this._output.UpdateBuffer(outputBuffer);
			if (this._processingState == DeflaterManaged.DeflaterState.CompressThenCheck || this._processingState == DeflaterManaged.DeflaterState.HandlingSmallData || this._processingState == DeflaterManaged.DeflaterState.SlowDownForIncompressible1)
			{
				this._deflateEncoder.GetBlockFooter(this._output);
			}
			this.WriteFinal();
			bytesRead = this._output.BytesWritten;
			return true;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000442E File Offset: 0x0000262E
		private bool UseCompressed(double ratio)
		{
			return ratio <= 1.0;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000443F File Offset: 0x0000263F
		private void FlushInputWindows()
		{
			this._deflateEncoder.FlushInput();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000444C File Offset: 0x0000264C
		private void WriteFinal()
		{
			this._copyEncoder.GetBlock(null, this._output, true);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004461 File Offset: 0x00002661
		public void Dispose()
		{
		}

		// Token: 0x040000DC RID: 220
		private const int MinBlockSize = 256;

		// Token: 0x040000DD RID: 221
		private const int MaxHeaderFooterGoo = 120;

		// Token: 0x040000DE RID: 222
		private const int CleanCopySize = 8072;

		// Token: 0x040000DF RID: 223
		private const double BadCompressionThreshold = 1.0;

		// Token: 0x040000E0 RID: 224
		private readonly FastEncoder _deflateEncoder;

		// Token: 0x040000E1 RID: 225
		private readonly CopyEncoder _copyEncoder;

		// Token: 0x040000E2 RID: 226
		private readonly DeflateInput _input;

		// Token: 0x040000E3 RID: 227
		private readonly OutputBuffer _output;

		// Token: 0x040000E4 RID: 228
		private DeflaterManaged.DeflaterState _processingState;

		// Token: 0x040000E5 RID: 229
		private DeflateInput _inputFromHistory;

		// Token: 0x0200001C RID: 28
		private enum DeflaterState
		{
			// Token: 0x040000E7 RID: 231
			NotStarted,
			// Token: 0x040000E8 RID: 232
			SlowDownForIncompressible1,
			// Token: 0x040000E9 RID: 233
			SlowDownForIncompressible2,
			// Token: 0x040000EA RID: 234
			StartingSmallData,
			// Token: 0x040000EB RID: 235
			CompressThenCheck,
			// Token: 0x040000EC RID: 236
			CheckingForIncompressible,
			// Token: 0x040000ED RID: 237
			HandlingSmallData
		}
	}
}
