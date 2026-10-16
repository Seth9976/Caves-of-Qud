using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020006AF RID: 1711
	internal struct FixedBuffer9<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x17000C14 RID: 3092
		public unsafe ref T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				bool flag = index < 0 || index >= 9;
				if (flag)
				{
					throw new IndexOutOfRangeException("index");
				}
				fixed (FixedBuffer9<T>* ptr = (FixedBuffer9<T>*)(&this))
				{
					void* ptr2 = (void*)ptr;
					T* ptr3 = (T*)ptr2;
					return ref ptr3[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
			}
		}

		// Token: 0x04001891 RID: 6289
		private T __0;

		// Token: 0x04001892 RID: 6290
		private T __1;

		// Token: 0x04001893 RID: 6291
		private T __2;

		// Token: 0x04001894 RID: 6292
		private T __3;

		// Token: 0x04001895 RID: 6293
		private T __4;

		// Token: 0x04001896 RID: 6294
		private T __5;

		// Token: 0x04001897 RID: 6295
		private T __6;

		// Token: 0x04001898 RID: 6296
		private T __7;

		// Token: 0x04001899 RID: 6297
		private T __8;

		// Token: 0x0400189A RID: 6298
		public const int Length = 9;
	}
}
