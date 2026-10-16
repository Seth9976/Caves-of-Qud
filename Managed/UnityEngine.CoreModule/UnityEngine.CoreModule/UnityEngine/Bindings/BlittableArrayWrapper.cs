using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x02000369 RID: 873
	[VisibleToOtherModules]
	internal ref struct BlittableArrayWrapper
	{
		// Token: 0x060028C5 RID: 10437 RVA: 0x0004EA64 File Offset: 0x0004CC64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe BlittableArrayWrapper(void* data, int size)
		{
			this.data = data;
			this.size = size;
			this.updateFlags = BlittableArrayWrapper.UpdateFlags.NoUpdateNeeded;
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x0004EA7C File Offset: 0x0004CC7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void Unmarshal<[IsUnmanaged] T>(ref T[] array) where T : struct, ValueType
		{
			switch (this.updateFlags)
			{
			case BlittableArrayWrapper.UpdateFlags.SizeChanged:
			case BlittableArrayWrapper.UpdateFlags.DataIsNativePointer:
				array = new Span<T>(this.data, this.size).ToArray();
				break;
			case BlittableArrayWrapper.UpdateFlags.DataIsNativeOwnedMemory:
				array = new Span<T>(BindingsAllocator.GetNativeOwnedDataPointer(this.data), this.size).ToArray();
				BindingsAllocator.FreeNativeOwnedMemory(this.data);
				break;
			case BlittableArrayWrapper.UpdateFlags.DataIsEmpty:
				array = Array.Empty<T>();
				break;
			case BlittableArrayWrapper.UpdateFlags.DataIsNull:
				array = null;
				break;
			}
		}

		// Token: 0x04000B82 RID: 2946
		internal unsafe void* data;

		// Token: 0x04000B83 RID: 2947
		internal int size;

		// Token: 0x04000B84 RID: 2948
		internal BlittableArrayWrapper.UpdateFlags updateFlags;

		// Token: 0x0200036A RID: 874
		internal enum UpdateFlags
		{
			// Token: 0x04000B86 RID: 2950
			NoUpdateNeeded,
			// Token: 0x04000B87 RID: 2951
			SizeChanged,
			// Token: 0x04000B88 RID: 2952
			DataIsNativePointer,
			// Token: 0x04000B89 RID: 2953
			DataIsNativeOwnedMemory,
			// Token: 0x04000B8A RID: 2954
			DataIsEmpty,
			// Token: 0x04000B8B RID: 2955
			DataIsNull
		}
	}
}
