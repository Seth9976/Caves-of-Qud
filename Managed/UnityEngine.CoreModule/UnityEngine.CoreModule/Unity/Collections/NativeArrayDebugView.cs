using System;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000AA RID: 170
	internal sealed class NativeArrayDebugView<T> where T : struct
	{
		// Token: 0x06000391 RID: 913 RVA: 0x000074F9 File Offset: 0x000056F9
		public NativeArrayDebugView(NativeArray<T> array)
		{
			this.m_Array = array;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000392 RID: 914 RVA: 0x0000750C File Offset: 0x0000570C
		public unsafe T[] Items
		{
			get
			{
				bool flag = !this.m_Array.IsCreated;
				T[] array;
				if (flag)
				{
					array = null;
				}
				else
				{
					int length = this.m_Array.m_Length;
					T[] array2 = new T[length];
					GCHandle gchandle = GCHandle.Alloc(array2, GCHandleType.Pinned);
					IntPtr intPtr = gchandle.AddrOfPinnedObject();
					UnsafeUtility.MemCpy((void*)intPtr, this.m_Array.m_Buffer, (long)(length * UnsafeUtility.SizeOf<T>()));
					gchandle.Free();
					array = array2;
				}
				return array;
			}
		}

		// Token: 0x04000282 RID: 642
		private NativeArray<T> m_Array;
	}
}
