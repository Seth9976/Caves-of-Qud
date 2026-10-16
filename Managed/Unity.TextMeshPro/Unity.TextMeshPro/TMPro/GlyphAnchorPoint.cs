using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	public struct GlyphAnchorPoint
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002389 File Offset: 0x00000589
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002391 File Offset: 0x00000591
		public float xCoordinate
		{
			get
			{
				return this.m_XCoordinate;
			}
			set
			{
				this.m_XCoordinate = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000239A File Offset: 0x0000059A
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000023A2 File Offset: 0x000005A2
		public float yCoordinate
		{
			get
			{
				return this.m_YCoordinate;
			}
			set
			{
				this.m_YCoordinate = value;
			}
		}

		// Token: 0x0400000E RID: 14
		[SerializeField]
		private float m_XCoordinate;

		// Token: 0x0400000F RID: 15
		[SerializeField]
		private float m_YCoordinate;
	}
}
