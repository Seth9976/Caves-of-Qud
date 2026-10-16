using System;

namespace System.Net
{
	// Token: 0x02000600 RID: 1536
	internal class SplitWritesState
	{
		// Token: 0x06003083 RID: 12419 RVA: 0x000A6E28 File Offset: 0x000A5028
		internal SplitWritesState(BufferOffsetSize[] buffers)
		{
			this._UserBuffers = buffers;
			this._LastBufferConsumed = 0;
			this._Index = 0;
			this._RealBuffers = null;
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x000A6E4C File Offset: 0x000A504C
		internal bool IsDone
		{
			get
			{
				if (this._LastBufferConsumed != 0)
				{
					return false;
				}
				for (int i = this._Index; i < this._UserBuffers.Length; i++)
				{
					if (this._UserBuffers[i].Size != 0)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x000A6E90 File Offset: 0x000A5090
		internal BufferOffsetSize[] GetNextBuffers()
		{
			int i = this._Index;
			int num = 0;
			int num2 = 0;
			int num3 = this._LastBufferConsumed;
			while (this._Index < this._UserBuffers.Length)
			{
				num2 = this._UserBuffers[this._Index].Size - this._LastBufferConsumed;
				num += num2;
				if (num > 65536)
				{
					num2 -= num - 65536;
					num = 65536;
					break;
				}
				num2 = 0;
				this._LastBufferConsumed = 0;
				this._Index++;
			}
			if (num == 0)
			{
				return null;
			}
			if (num3 == 0 && i == 0 && this._Index == this._UserBuffers.Length)
			{
				return this._UserBuffers;
			}
			int num4 = ((num2 == 0) ? (this._Index - i) : (this._Index - i + 1));
			if (this._RealBuffers == null || this._RealBuffers.Length != num4)
			{
				this._RealBuffers = new BufferOffsetSize[num4];
			}
			int num5 = 0;
			while (i < this._Index)
			{
				this._RealBuffers[num5++] = new BufferOffsetSize(this._UserBuffers[i].Buffer, this._UserBuffers[i].Offset + num3, this._UserBuffers[i].Size - num3, false);
				num3 = 0;
				i++;
			}
			if (num2 != 0)
			{
				this._RealBuffers[num5] = new BufferOffsetSize(this._UserBuffers[i].Buffer, this._UserBuffers[i].Offset + this._LastBufferConsumed, num2, false);
				if ((this._LastBufferConsumed += num2) == this._UserBuffers[this._Index].Size)
				{
					this._Index++;
					this._LastBufferConsumed = 0;
				}
			}
			return this._RealBuffers;
		}

		// Token: 0x04001C48 RID: 7240
		private const int c_SplitEncryptedBuffersSize = 65536;

		// Token: 0x04001C49 RID: 7241
		private BufferOffsetSize[] _UserBuffers;

		// Token: 0x04001C4A RID: 7242
		private int _Index;

		// Token: 0x04001C4B RID: 7243
		private int _LastBufferConsumed;

		// Token: 0x04001C4C RID: 7244
		private BufferOffsetSize[] _RealBuffers;
	}
}
