using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020006B0 RID: 1712
	internal struct FixedBuffer16<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x17000C15 RID: 3093
		public unsafe ref T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				bool flag = index < 0 || index >= 16;
				if (flag)
				{
					throw new IndexOutOfRangeException("index");
				}
				fixed (FixedBuffer16<T>* ptr = (FixedBuffer16<T>*)(&this))
				{
					void* ptr2 = (void*)ptr;
					T* ptr3 = (T*)ptr2;
					return ref ptr3[(IntPtr)index * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				}
			}
		}

		// Token: 0x0400189B RID: 6299
		private T __0;

		// Token: 0x0400189C RID: 6300
		private T __1;

		// Token: 0x0400189D RID: 6301
		private T __2;

		// Token: 0x0400189E RID: 6302
		private T __3;

		// Token: 0x0400189F RID: 6303
		private T __4;

		// Token: 0x040018A0 RID: 6304
		private T __5;

		// Token: 0x040018A1 RID: 6305
		private T __6;

		// Token: 0x040018A2 RID: 6306
		private T __7;

		// Token: 0x040018A3 RID: 6307
		private T __8;

		// Token: 0x040018A4 RID: 6308
		private T __9;

		// Token: 0x040018A5 RID: 6309
		private T _10;

		// Token: 0x040018A6 RID: 6310
		private T _11;

		// Token: 0x040018A7 RID: 6311
		private T _12;

		// Token: 0x040018A8 RID: 6312
		private T _13;

		// Token: 0x040018A9 RID: 6313
		private T _14;

		// Token: 0x040018AA RID: 6314
		private T _15;

		// Token: 0x040018AB RID: 6315
		public const int Length = 16;
	}
}
