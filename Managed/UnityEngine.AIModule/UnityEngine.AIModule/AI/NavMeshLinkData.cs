using System;

namespace UnityEngine.AI
{
	// Token: 0x02000014 RID: 20
	public struct NavMeshLinkData
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00003D77 File Offset: 0x00001F77
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00003D7F File Offset: 0x00001F7F
		public Vector3 startPosition
		{
			get
			{
				return this.m_StartPosition;
			}
			set
			{
				this.m_StartPosition = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00003D88 File Offset: 0x00001F88
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00003D90 File Offset: 0x00001F90
		public Vector3 endPosition
		{
			get
			{
				return this.m_EndPosition;
			}
			set
			{
				this.m_EndPosition = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00003D99 File Offset: 0x00001F99
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00003DA1 File Offset: 0x00001FA1
		public float costModifier
		{
			get
			{
				return this.m_CostModifier;
			}
			set
			{
				this.m_CostModifier = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00003DAA File Offset: 0x00001FAA
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00003DB5 File Offset: 0x00001FB5
		public bool bidirectional
		{
			get
			{
				return this.m_Bidirectional != 0;
			}
			set
			{
				this.m_Bidirectional = (value ? 1 : 0);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00003DC4 File Offset: 0x00001FC4
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00003DCC File Offset: 0x00001FCC
		public float width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00003DD5 File Offset: 0x00001FD5
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00003DDD File Offset: 0x00001FDD
		public int area
		{
			get
			{
				return this.m_Area;
			}
			set
			{
				this.m_Area = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00003DE6 File Offset: 0x00001FE6
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00003DEE File Offset: 0x00001FEE
		public int agentTypeID
		{
			get
			{
				return this.m_AgentTypeID;
			}
			set
			{
				this.m_AgentTypeID = value;
			}
		}

		// Token: 0x04000031 RID: 49
		private Vector3 m_StartPosition;

		// Token: 0x04000032 RID: 50
		private Vector3 m_EndPosition;

		// Token: 0x04000033 RID: 51
		private float m_CostModifier;

		// Token: 0x04000034 RID: 52
		private int m_Bidirectional;

		// Token: 0x04000035 RID: 53
		private float m_Width;

		// Token: 0x04000036 RID: 54
		private int m_Area;

		// Token: 0x04000037 RID: 55
		private int m_AgentTypeID;
	}
}
