using System;

namespace System.Collections.Generic
{
	// Token: 0x020004C8 RID: 1224
	internal sealed class BitHelper
	{
		// Token: 0x0600279A RID: 10138 RVA: 0x000890A5 File Offset: 0x000872A5
		internal unsafe BitHelper(int* bitArrayPtr, int length)
		{
			this._arrayPtr = bitArrayPtr;
			this._length = length;
			this._useStackAlloc = true;
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x000890C2 File Offset: 0x000872C2
		internal BitHelper(int[] bitArray, int length)
		{
			this._array = bitArray;
			this._length = length;
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x000890D8 File Offset: 0x000872D8
		internal unsafe void MarkBit(int bitPosition)
		{
			int num = bitPosition / 32;
			if (num < this._length && num >= 0)
			{
				int num2 = 1 << bitPosition % 32;
				if (this._useStackAlloc)
				{
					this._arrayPtr[num] |= num2;
					return;
				}
				this._array[num] |= num2;
			}
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x0008912C File Offset: 0x0008732C
		internal unsafe bool IsMarked(int bitPosition)
		{
			int num = bitPosition / 32;
			if (num >= this._length || num < 0)
			{
				return false;
			}
			int num2 = 1 << bitPosition % 32;
			if (this._useStackAlloc)
			{
				return (this._arrayPtr[num] & num2) != 0;
			}
			return (this._array[num] & num2) != 0;
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x0008917E File Offset: 0x0008737E
		internal static int ToIntArrayLength(int n)
		{
			if (n <= 0)
			{
				return 0;
			}
			return (n - 1) / 32 + 1;
		}

		// Token: 0x04001574 RID: 5492
		private const byte MarkedBitFlag = 1;

		// Token: 0x04001575 RID: 5493
		private const byte IntSize = 32;

		// Token: 0x04001576 RID: 5494
		private readonly int _length;

		// Token: 0x04001577 RID: 5495
		private unsafe readonly int* _arrayPtr;

		// Token: 0x04001578 RID: 5496
		private readonly int[] _array;

		// Token: 0x04001579 RID: 5497
		private readonly bool _useStackAlloc;
	}
}
