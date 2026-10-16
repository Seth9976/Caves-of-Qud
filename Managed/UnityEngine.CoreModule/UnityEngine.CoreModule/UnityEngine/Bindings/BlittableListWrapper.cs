using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x0200036B RID: 875
	[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
	[VisibleToOtherModules]
	internal ref struct BlittableListWrapper
	{
		// Token: 0x060028C7 RID: 10439 RVA: 0x0004EB0E File Offset: 0x0004CD0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public BlittableListWrapper(BlittableArrayWrapper arrayWrapper, int listSize)
		{
			this.arrayWrapper = arrayWrapper;
			this.listSize = listSize;
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x0004EB20 File Offset: 0x0004CD20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void Unmarshal<[IsUnmanaged] T>(List<T> list) where T : struct, ValueType
		{
			bool flag = list == null;
			if (!flag)
			{
				switch (this.arrayWrapper.updateFlags)
				{
				case BlittableArrayWrapper.UpdateFlags.SizeChanged:
				case BlittableArrayWrapper.UpdateFlags.DataIsEmpty:
				case BlittableArrayWrapper.UpdateFlags.DataIsNull:
					NoAllocHelpers.ResetListSize<T>(list, this.listSize);
					break;
				case BlittableArrayWrapper.UpdateFlags.DataIsNativePointer:
					NoAllocHelpers.ResetListContents<T>(list, new ReadOnlySpan<T>(this.arrayWrapper.data, this.arrayWrapper.size));
					break;
				case BlittableArrayWrapper.UpdateFlags.DataIsNativeOwnedMemory:
					NoAllocHelpers.ResetListContents<T>(list, new ReadOnlySpan<T>(BindingsAllocator.GetNativeOwnedDataPointer(this.arrayWrapper.data), this.arrayWrapper.size));
					BindingsAllocator.FreeNativeOwnedMemory(this.arrayWrapper.data);
					break;
				}
			}
		}

		// Token: 0x04000B8C RID: 2956
		private BlittableArrayWrapper arrayWrapper;

		// Token: 0x04000B8D RID: 2957
		private int listSize;
	}
}
