using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000036 RID: 54
	[NativeHeader("Modules/Animation/ScriptBindings/AvatarBuilder.bindings.h")]
	[NativeType(CodegenOptions.Custom, "MonoHumanLimit")]
	[NativeHeader("Modules/Animation/HumanDescription.h")]
	public struct HumanLimit
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00006E94 File Offset: 0x00005094
		// (set) Token: 0x06000316 RID: 790 RVA: 0x00006EAF File Offset: 0x000050AF
		public bool useDefaultValues
		{
			get
			{
				return this.m_UseDefaultValues != 0;
			}
			set
			{
				this.m_UseDefaultValues = (value ? 1 : 0);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00006EC0 File Offset: 0x000050C0
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00006ED8 File Offset: 0x000050D8
		public Vector3 min
		{
			get
			{
				return this.m_Min;
			}
			set
			{
				this.m_Min = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00006EE4 File Offset: 0x000050E4
		// (set) Token: 0x0600031A RID: 794 RVA: 0x00006EFC File Offset: 0x000050FC
		public Vector3 max
		{
			get
			{
				return this.m_Max;
			}
			set
			{
				this.m_Max = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00006F08 File Offset: 0x00005108
		// (set) Token: 0x0600031C RID: 796 RVA: 0x00006F20 File Offset: 0x00005120
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00006F2C File Offset: 0x0000512C
		// (set) Token: 0x0600031E RID: 798 RVA: 0x00006F44 File Offset: 0x00005144
		public float axisLength
		{
			get
			{
				return this.m_AxisLength;
			}
			set
			{
				this.m_AxisLength = value;
			}
		}

		// Token: 0x0400011F RID: 287
		private Vector3 m_Min;

		// Token: 0x04000120 RID: 288
		private Vector3 m_Max;

		// Token: 0x04000121 RID: 289
		private Vector3 m_Center;

		// Token: 0x04000122 RID: 290
		private float m_AxisLength;

		// Token: 0x04000123 RID: 291
		private int m_UseDefaultValues;
	}
}
