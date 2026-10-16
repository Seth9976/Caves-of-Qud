using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020006AC RID: 1708
	internal struct FixedBuffer2<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x17000C11 RID: 3089
		public unsafe ref T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				bool flag = index < 0 || index >= 2;
				if (flag)
				{
					throw new IndexOutOfRangeException("index");
				}
				fixed (FixedBuffer2<T>* ptr = (FixedBuffer2<T>*)(&this))
				{
					void* ptr2 = (void*)ptr;
					T* ptr3 = (T*)ptr2;
					return ref ptr3[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
			}
		}

		// Token: 0x04001882 RID: 6274
		private T __0;

		// Token: 0x04001883 RID: 6275
		private T __1;

		// Token: 0x04001884 RID: 6276
		public const int Length = 2;
	}
}
