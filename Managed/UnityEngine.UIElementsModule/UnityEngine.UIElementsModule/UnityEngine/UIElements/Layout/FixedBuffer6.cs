using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020006AE RID: 1710
	internal struct FixedBuffer6<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x17000C13 RID: 3091
		public unsafe ref T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				bool flag = index < 0 || index >= 6;
				if (flag)
				{
					throw new IndexOutOfRangeException("index");
				}
				fixed (FixedBuffer6<T>* ptr = (FixedBuffer6<T>*)(&this))
				{
					void* ptr2 = (void*)ptr;
					T* ptr3 = (T*)ptr2;
					return ref ptr3[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
			}
		}

		// Token: 0x0400188A RID: 6282
		private T __0;

		// Token: 0x0400188B RID: 6283
		private T __1;

		// Token: 0x0400188C RID: 6284
		private T __2;

		// Token: 0x0400188D RID: 6285
		private T __3;

		// Token: 0x0400188E RID: 6286
		private T __4;

		// Token: 0x0400188F RID: 6287
		private T __5;

		// Token: 0x04001890 RID: 6288
		public const int Length = 6;
	}
}
