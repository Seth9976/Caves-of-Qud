using System;

namespace Unity.Collections
{
	// Token: 0x020000AF RID: 175
	internal sealed class NativeSliceDebugView<T> where T : struct
	{
		// Token: 0x060003BD RID: 957 RVA: 0x00007B11 File Offset: 0x00005D11
		public NativeSliceDebugView(NativeSlice<T> array)
		{
			this.m_Array = array;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00007B24 File Offset: 0x00005D24
		public T[] Items
		{
			get
			{
				return this.m_Array.ToArray();
			}
		}

		// Token: 0x04000289 RID: 649
		private NativeSlice<T> m_Array;
	}
}
