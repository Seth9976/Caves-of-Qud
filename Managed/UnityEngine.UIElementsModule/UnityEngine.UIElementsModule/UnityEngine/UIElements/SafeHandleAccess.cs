using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000319 RID: 793
	internal struct SafeHandleAccess
	{
		// Token: 0x060016C2 RID: 5826 RVA: 0x00058102 File Offset: 0x00056302
		public SafeHandleAccess(IntPtr ptr)
		{
			this.m_Handle = ptr;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x0005810C File Offset: 0x0005630C
		public bool IsNull()
		{
			return this.m_Handle == IntPtr.Zero;
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00058130 File Offset: 0x00056330
		public static implicit operator IntPtr(SafeHandleAccess a)
		{
			bool flag = a.m_Handle == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentNullException();
			}
			return a.m_Handle;
		}

		// Token: 0x04000C6D RID: 3181
		private IntPtr m_Handle;
	}
}
