using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020002B5 RID: 693
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class TrackedReference
	{
		// Token: 0x060021D6 RID: 8662 RVA: 0x00030B93 File Offset: 0x0002ED93
		protected TrackedReference()
		{
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x00042020 File Offset: 0x00040220
		public static bool operator ==(TrackedReference x, TrackedReference y)
		{
			bool flag = y == null && x == null;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = y == null;
				if (flag3)
				{
					flag2 = x.m_Ptr == IntPtr.Zero;
				}
				else
				{
					bool flag4 = x == null;
					if (flag4)
					{
						flag2 = y.m_Ptr == IntPtr.Zero;
					}
					else
					{
						flag2 = x.m_Ptr == y.m_Ptr;
					}
				}
			}
			return flag2;
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x00042094 File Offset: 0x00040294
		public static bool operator !=(TrackedReference x, TrackedReference y)
		{
			return !(x == y);
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x000420B0 File Offset: 0x000402B0
		public override bool Equals(object o)
		{
			return o as TrackedReference == this;
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x000420D0 File Offset: 0x000402D0
		public override int GetHashCode()
		{
			return (int)this.m_Ptr;
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000420F0 File Offset: 0x000402F0
		public static implicit operator bool(TrackedReference exists)
		{
			return exists != null;
		}

		// Token: 0x040009E2 RID: 2530
		[VisibleToOtherModules(new string[] { "UnityEngine.AnimationModule" })]
		internal IntPtr m_Ptr;
	}
}
