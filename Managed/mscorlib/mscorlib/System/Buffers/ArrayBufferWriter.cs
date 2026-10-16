using System;

namespace System.Buffers
{
	// Token: 0x02000AEE RID: 2798
	public sealed class ArrayBufferWriter<T> : IBufferWriter<T>
	{
		// Token: 0x06006381 RID: 25473 RVA: 0x0014CD2F File Offset: 0x0014AF2F
		public ArrayBufferWriter()
		{
			this._buffer = Array.Empty<T>();
			this._index = 0;
		}

		// Token: 0x06006382 RID: 25474 RVA: 0x0014CD49 File Offset: 0x0014AF49
		public ArrayBufferWriter(int initialCapacity)
		{
			if (initialCapacity <= 0)
			{
				throw new ArgumentException("initialCapacity");
			}
			this._buffer = new T[initialCapacity];
			this._index = 0;
		}

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x06006383 RID: 25475 RVA: 0x0014CD73 File Offset: 0x0014AF73
		public ReadOnlyMemory<T> WrittenMemory
		{
			get
			{
				return this._buffer.AsMemory<T>(0, this._index);
			}
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x06006384 RID: 25476 RVA: 0x0014CD8C File Offset: 0x0014AF8C
		public ReadOnlySpan<T> WrittenSpan
		{
			get
			{
				return this._buffer.AsSpan<T>(0, this._index);
			}
		}

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x06006385 RID: 25477 RVA: 0x0014CDA5 File Offset: 0x0014AFA5
		public int WrittenCount
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x06006386 RID: 25478 RVA: 0x0014CDAD File Offset: 0x0014AFAD
		public int Capacity
		{
			get
			{
				return this._buffer.Length;
			}
		}

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x06006387 RID: 25479 RVA: 0x0014CDB7 File Offset: 0x0014AFB7
		public int FreeCapacity
		{
			get
			{
				return this._buffer.Length - this._index;
			}
		}

		// Token: 0x06006388 RID: 25480 RVA: 0x0014CDC8 File Offset: 0x0014AFC8
		public void Clear()
		{
			this._buffer.AsSpan<T>(0, this._index).Clear();
			this._index = 0;
		}

		// Token: 0x06006389 RID: 25481 RVA: 0x0014CDF6 File Offset: 0x0014AFF6
		public void Advance(int count)
		{
			if (count < 0)
			{
				throw new ArgumentException("count");
			}
			if (this._index > this._buffer.Length - count)
			{
				ArrayBufferWriter<T>.ThrowInvalidOperationException_AdvancedTooFar(this._buffer.Length);
			}
			this._index += count;
		}

		// Token: 0x0600638A RID: 25482 RVA: 0x0014CE34 File Offset: 0x0014B034
		public Memory<T> GetMemory(int sizeHint = 0)
		{
			this.CheckAndResizeBuffer(sizeHint);
			return this._buffer.AsMemory<T>(this._index);
		}

		// Token: 0x0600638B RID: 25483 RVA: 0x0014CE4E File Offset: 0x0014B04E
		public Span<T> GetSpan(int sizeHint = 0)
		{
			this.CheckAndResizeBuffer(sizeHint);
			return this._buffer.AsSpan<T>(this._index);
		}

		// Token: 0x0600638C RID: 25484 RVA: 0x0014CE68 File Offset: 0x0014B068
		private void CheckAndResizeBuffer(int sizeHint)
		{
			if (sizeHint < 0)
			{
				throw new ArgumentException("sizeHint");
			}
			if (sizeHint == 0)
			{
				sizeHint = 1;
			}
			if (sizeHint > this.FreeCapacity)
			{
				int num = Math.Max(sizeHint, this._buffer.Length);
				if (this._buffer.Length == 0)
				{
					num = Math.Max(num, 256);
				}
				int num2 = checked(this._buffer.Length + num);
				Array.Resize<T>(ref this._buffer, num2);
			}
		}

		// Token: 0x0600638D RID: 25485 RVA: 0x0014CECE File Offset: 0x0014B0CE
		private static void ThrowInvalidOperationException_AdvancedTooFar(int capacity)
		{
			throw new InvalidOperationException(SR.Format("Cannot advance past the end of the buffer, which has a size of {0}.", capacity));
		}

		// Token: 0x04003A86 RID: 14982
		private T[] _buffer;

		// Token: 0x04003A87 RID: 14983
		private int _index;

		// Token: 0x04003A88 RID: 14984
		private const int DefaultInitialBufferSize = 256;
	}
}
