using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000027 RID: 39
	[UsedByNativeCode]
	public struct PatchExtents
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00003FA8 File Offset: 0x000021A8
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00003FC0 File Offset: 0x000021C0
		public float min
		{
			get
			{
				return this.m_min;
			}
			set
			{
				this.m_min = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00003FCC File Offset: 0x000021CC
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00003FE4 File Offset: 0x000021E4
		public float max
		{
			get
			{
				return this.m_max;
			}
			set
			{
				this.m_max = value;
			}
		}

		// Token: 0x0400008F RID: 143
		internal float m_min;

		// Token: 0x04000090 RID: 144
		internal float m_max;
	}
}
