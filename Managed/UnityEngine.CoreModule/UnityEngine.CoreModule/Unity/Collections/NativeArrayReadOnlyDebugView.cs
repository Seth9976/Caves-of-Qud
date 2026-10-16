using System;

namespace Unity.Collections
{
	// Token: 0x020000AB RID: 171
	internal sealed class NativeArrayReadOnlyDebugView<T> where T : struct
	{
		// Token: 0x06000393 RID: 915 RVA: 0x00007585 File Offset: 0x00005785
		public NativeArrayReadOnlyDebugView(NativeArray<T>.ReadOnly array)
		{
			this.m_Array = array;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00007598 File Offset: 0x00005798
		public T[] Items
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
					array = this.m_Array.ToArray();
				}
				return array;
			}
		}

		// Token: 0x04000283 RID: 643
		private NativeArray<T>.ReadOnly m_Array;
	}
}
