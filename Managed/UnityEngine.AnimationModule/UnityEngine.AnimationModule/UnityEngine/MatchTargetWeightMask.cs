using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000024 RID: 36
	[NativeHeader("Modules/Animation/Animator.h")]
	public struct MatchTargetWeightMask
	{
		// Token: 0x06000122 RID: 290 RVA: 0x00004065 File Offset: 0x00002265
		public MatchTargetWeightMask(Vector3 positionXYZWeight, float rotationWeight)
		{
			this.m_PositionXYZWeight = positionXYZWeight;
			this.m_RotationWeight = rotationWeight;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00004078 File Offset: 0x00002278
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00004090 File Offset: 0x00002290
		public Vector3 positionXYZWeight
		{
			get
			{
				return this.m_PositionXYZWeight;
			}
			set
			{
				this.m_PositionXYZWeight = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000409C File Offset: 0x0000229C
		// (set) Token: 0x06000126 RID: 294 RVA: 0x000040B4 File Offset: 0x000022B4
		public float rotationWeight
		{
			get
			{
				return this.m_RotationWeight;
			}
			set
			{
				this.m_RotationWeight = value;
			}
		}

		// Token: 0x04000077 RID: 119
		private Vector3 m_PositionXYZWeight;

		// Token: 0x04000078 RID: 120
		private float m_RotationWeight;
	}
}
