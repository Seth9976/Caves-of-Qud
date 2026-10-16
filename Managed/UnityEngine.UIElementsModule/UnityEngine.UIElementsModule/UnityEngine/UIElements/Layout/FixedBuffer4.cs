using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020006AD RID: 1709
	internal struct FixedBuffer4<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x060031F3 RID: 12787 RVA: 0x000BC99C File Offset: 0x000BAB9C
		public FixedBuffer4(T x, T y, T z, T w)
		{
			this.__0 = x;
			this.__1 = y;
			this.__2 = z;
			this.__3 = w;
		}

		// Token: 0x17000C12 RID: 3090
		public unsafe ref T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				bool flag = index < 0 || index >= 4;
				if (flag)
				{
					throw new IndexOutOfRangeException("index");
				}
				fixed (FixedBuffer4<T>* ptr = (FixedBuffer4<T>*)(&this))
				{
					void* ptr2 = (void*)ptr;
					T* ptr3 = (T*)ptr2;
					return ref ptr3[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
			}
		}

		// Token: 0x04001885 RID: 6277
		private T __0;

		// Token: 0x04001886 RID: 6278
		private T __1;

		// Token: 0x04001887 RID: 6279
		private T __2;

		// Token: 0x04001888 RID: 6280
		private T __3;

		// Token: 0x04001889 RID: 6281
		public const int Length = 4;
	}
}
