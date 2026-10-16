using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public struct MarkPositionAdjustment
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000023AB File Offset: 0x000005AB
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000023B3 File Offset: 0x000005B3
		public float xPositionAdjustment
		{
			get
			{
				return this.m_XPositionAdjustment;
			}
			set
			{
				this.m_XPositionAdjustment = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000023BC File Offset: 0x000005BC
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000023C4 File Offset: 0x000005C4
		public float yPositionAdjustment
		{
			get
			{
				return this.m_YPositionAdjustment;
			}
			set
			{
				this.m_YPositionAdjustment = value;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023CD File Offset: 0x000005CD
		public MarkPositionAdjustment(float x, float y)
		{
			this.m_XPositionAdjustment = x;
			this.m_YPositionAdjustment = y;
		}

		// Token: 0x04000010 RID: 16
		[SerializeField]
		private float m_XPositionAdjustment;

		// Token: 0x04000011 RID: 17
		[SerializeField]
		private float m_YPositionAdjustment;
	}
}
