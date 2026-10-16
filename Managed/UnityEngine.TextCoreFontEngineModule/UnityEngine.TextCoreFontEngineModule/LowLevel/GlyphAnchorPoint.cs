using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000023 RID: 35
	[UsedByNativeCode]
	[VisibleToOtherModules(new string[] { "UnityEngine.TextCoreTextEngineModule" })]
	[Serializable]
	internal struct GlyphAnchorPoint
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00005EB8 File Offset: 0x000040B8
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00005ED0 File Offset: 0x000040D0
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

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00005EDC File Offset: 0x000040DC
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00005EF4 File Offset: 0x000040F4
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

		// Token: 0x040000D7 RID: 215
		[NativeName("xPositionAdjustment")]
		[SerializeField]
		private float m_XCoordinate;

		// Token: 0x040000D8 RID: 216
		[NativeName("yPositionAdjustment")]
		[SerializeField]
		private float m_YCoordinate;
	}
}
