using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000038 RID: 56
	[NativeHeader("Modules/Animation/ScriptBindings/AvatarBuilder.bindings.h")]
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	public struct HumanDescription
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00006F98 File Offset: 0x00005198
		// (set) Token: 0x06000324 RID: 804 RVA: 0x00006FB0 File Offset: 0x000051B0
		public float upperArmTwist
		{
			get
			{
				return this.m_ArmTwist;
			}
			set
			{
				this.m_ArmTwist = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00006FBC File Offset: 0x000051BC
		// (set) Token: 0x06000326 RID: 806 RVA: 0x00006FD4 File Offset: 0x000051D4
		public float lowerArmTwist
		{
			get
			{
				return this.m_ForeArmTwist;
			}
			set
			{
				this.m_ForeArmTwist = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00006FE0 File Offset: 0x000051E0
		// (set) Token: 0x06000328 RID: 808 RVA: 0x00006FF8 File Offset: 0x000051F8
		public float upperLegTwist
		{
			get
			{
				return this.m_UpperLegTwist;
			}
			set
			{
				this.m_UpperLegTwist = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00007004 File Offset: 0x00005204
		// (set) Token: 0x0600032A RID: 810 RVA: 0x0000701C File Offset: 0x0000521C
		public float lowerLegTwist
		{
			get
			{
				return this.m_LegTwist;
			}
			set
			{
				this.m_LegTwist = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00007028 File Offset: 0x00005228
		// (set) Token: 0x0600032C RID: 812 RVA: 0x00007040 File Offset: 0x00005240
		public float armStretch
		{
			get
			{
				return this.m_ArmStretch;
			}
			set
			{
				this.m_ArmStretch = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000704C File Offset: 0x0000524C
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00007064 File Offset: 0x00005264
		public float legStretch
		{
			get
			{
				return this.m_LegStretch;
			}
			set
			{
				this.m_LegStretch = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00007070 File Offset: 0x00005270
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00007088 File Offset: 0x00005288
		public float feetSpacing
		{
			get
			{
				return this.m_FeetSpacing;
			}
			set
			{
				this.m_FeetSpacing = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00007094 File Offset: 0x00005294
		// (set) Token: 0x06000332 RID: 818 RVA: 0x000070AC File Offset: 0x000052AC
		public bool hasTranslationDoF
		{
			get
			{
				return this.m_HasTranslationDoF;
			}
			set
			{
				this.m_HasTranslationDoF = value;
			}
		}

		// Token: 0x04000127 RID: 295
		[NativeName("m_Human")]
		public HumanBone[] human;

		// Token: 0x04000128 RID: 296
		[NativeName("m_Skeleton")]
		public SkeletonBone[] skeleton;

		// Token: 0x04000129 RID: 297
		internal float m_ArmTwist;

		// Token: 0x0400012A RID: 298
		internal float m_ForeArmTwist;

		// Token: 0x0400012B RID: 299
		internal float m_UpperLegTwist;

		// Token: 0x0400012C RID: 300
		internal float m_LegTwist;

		// Token: 0x0400012D RID: 301
		internal float m_ArmStretch;

		// Token: 0x0400012E RID: 302
		internal float m_LegStretch;

		// Token: 0x0400012F RID: 303
		internal float m_FeetSpacing;

		// Token: 0x04000130 RID: 304
		internal float m_GlobalScale;

		// Token: 0x04000131 RID: 305
		internal string m_RootMotionBoneName;

		// Token: 0x04000132 RID: 306
		internal bool m_HasTranslationDoF;

		// Token: 0x04000133 RID: 307
		internal bool m_HasExtraRoot;

		// Token: 0x04000134 RID: 308
		internal bool m_SkeletonHasParents;
	}
}
