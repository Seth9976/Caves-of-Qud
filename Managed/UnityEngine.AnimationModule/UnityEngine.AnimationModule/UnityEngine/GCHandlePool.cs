using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000015 RID: 21
	internal class GCHandlePool
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00003C3C File Offset: 0x00001E3C
		public GCHandlePool()
		{
			this.m_handles = new GCHandle[128];
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003C58 File Offset: 0x00001E58
		public GCHandle Alloc()
		{
			bool flag = this.m_current > 0;
			GCHandle gchandle;
			if (flag)
			{
				GCHandle[] handles = this.m_handles;
				int num = this.m_current - 1;
				this.m_current = num;
				gchandle = handles[num];
			}
			else
			{
				gchandle = GCHandle.Alloc(null);
			}
			return gchandle;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00003CA0 File Offset: 0x00001EA0
		public GCHandle Alloc(object o)
		{
			bool flag = this.m_current > 0;
			GCHandle gchandle2;
			if (flag)
			{
				GCHandle[] handles = this.m_handles;
				int num = this.m_current - 1;
				this.m_current = num;
				GCHandle gchandle = handles[num];
				gchandle.Target = o;
				gchandle2 = gchandle;
			}
			else
			{
				gchandle2 = GCHandle.Alloc(o);
			}
			return gchandle2;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003CF0 File Offset: 0x00001EF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IntPtr AllocHandleIfNotNull(object o)
		{
			bool flag = o == null;
			IntPtr intPtr;
			if (flag)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				intPtr = (IntPtr)this.Alloc(o);
			}
			return intPtr;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003D20 File Offset: 0x00001F20
		public void Free(GCHandle h)
		{
			bool flag = this.m_current == this.m_handles.Length;
			if (flag)
			{
				int num = this.m_handles.Length * 2;
				GCHandle[] array = new GCHandle[num];
				Array.Copy(this.m_handles, array, this.m_handles.Length);
				this.m_handles = array;
			}
			h.Target = null;
			GCHandle[] handles = this.m_handles;
			int current = this.m_current;
			this.m_current = current + 1;
			handles[current] = h;
		}

		// Token: 0x04000033 RID: 51
		private GCHandle[] m_handles;

		// Token: 0x04000034 RID: 52
		private int m_current;
	}
}
