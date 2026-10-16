using System;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x02000070 RID: 112
	public struct Eyes
	{
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x000389E9 File Offset: 0x00036BE9
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x000389F1 File Offset: 0x00036BF1
		public Vector3 leftEyePosition
		{
			get
			{
				return this.m_LeftEyePosition;
			}
			set
			{
				this.m_LeftEyePosition = value;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x000389FA File Offset: 0x00036BFA
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x00038A02 File Offset: 0x00036C02
		public Quaternion leftEyeRotation
		{
			get
			{
				return this.m_LeftEyeRotation;
			}
			set
			{
				this.m_LeftEyeRotation = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00038A0B File Offset: 0x00036C0B
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x00038A13 File Offset: 0x00036C13
		public Vector3 rightEyePosition
		{
			get
			{
				return this.m_RightEyePosition;
			}
			set
			{
				this.m_RightEyePosition = value;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x00038A1C File Offset: 0x00036C1C
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x00038A24 File Offset: 0x00036C24
		public Quaternion rightEyeRotation
		{
			get
			{
				return this.m_RightEyeRotation;
			}
			set
			{
				this.m_RightEyeRotation = value;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00038A2D File Offset: 0x00036C2D
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x00038A35 File Offset: 0x00036C35
		public Vector3 fixationPoint
		{
			get
			{
				return this.m_FixationPoint;
			}
			set
			{
				this.m_FixationPoint = value;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x00038A3E File Offset: 0x00036C3E
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x00038A46 File Offset: 0x00036C46
		public float leftEyeOpenAmount
		{
			get
			{
				return this.m_LeftEyeOpenAmount;
			}
			set
			{
				this.m_LeftEyeOpenAmount = value;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00038A4F File Offset: 0x00036C4F
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x00038A57 File Offset: 0x00036C57
		public float rightEyeOpenAmount
		{
			get
			{
				return this.m_RightEyeOpenAmount;
			}
			set
			{
				this.m_RightEyeOpenAmount = value;
			}
		}

		// Token: 0x04000384 RID: 900
		public Vector3 m_LeftEyePosition;

		// Token: 0x04000385 RID: 901
		public Quaternion m_LeftEyeRotation;

		// Token: 0x04000386 RID: 902
		public Vector3 m_RightEyePosition;

		// Token: 0x04000387 RID: 903
		public Quaternion m_RightEyeRotation;

		// Token: 0x04000388 RID: 904
		public Vector3 m_FixationPoint;

		// Token: 0x04000389 RID: 905
		public float m_LeftEyeOpenAmount;

		// Token: 0x0400038A RID: 906
		public float m_RightEyeOpenAmount;
	}
}
