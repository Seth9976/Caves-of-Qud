using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000024 RID: 36
	[UsedByNativeCode]
	[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
	[Serializable]
	internal struct MarkPositionAdjustment
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00005F00 File Offset: 0x00004100
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00005F18 File Offset: 0x00004118
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

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00005F24 File Offset: 0x00004124
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00005F3C File Offset: 0x0000413C
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

		// Token: 0x06000180 RID: 384 RVA: 0x00005F46 File Offset: 0x00004146
		public MarkPositionAdjustment(float x, float y)
		{
			this.m_XPositionAdjustment = x;
			this.m_YPositionAdjustment = y;
		}

		// Token: 0x040000D9 RID: 217
		[NativeName("xCoordinate")]
		[SerializeField]
		private float m_XPositionAdjustment;

		// Token: 0x040000DA RID: 218
		[SerializeField]
		[NativeName("yCoordinate")]
		private float m_YPositionAdjustment;
	}
}
