using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200003B RID: 59
	[UsedByNativeCode]
	public readonly struct ContactPairPoint
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x00008D55 File Offset: 0x00006F55
		public Vector3 position
		{
			get
			{
				return this.m_Position;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00008D5D File Offset: 0x00006F5D
		public float separation
		{
			get
			{
				return this.m_Separation;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00008D65 File Offset: 0x00006F65
		public Vector3 normal
		{
			get
			{
				return this.m_Normal;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00008D6D File Offset: 0x00006F6D
		public Vector3 impulse
		{
			get
			{
				return this.m_Impulse;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00008D75 File Offset: 0x00006F75
		[Obsolete("Please use ContactPairPoint.position instead. (UnityUpgradable) -> position", false)]
		public Vector3 Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x00008D7D File Offset: 0x00006F7D
		[Obsolete("Please use ContactPairPoint.separation instead. (UnityUpgradable) -> separation", false)]
		public float Separation
		{
			get
			{
				return this.separation;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00008D85 File Offset: 0x00006F85
		[Obsolete("Please use ContactPairPoint.normal instead. (UnityUpgradable) -> normal", false)]
		public Vector3 Normal
		{
			get
			{
				return this.normal;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x00008D8D File Offset: 0x00006F8D
		[Obsolete("Please use ContactPairPoint.impulse instead. (UnityUpgradable) -> impulse", false)]
		public Vector3 Impulse
		{
			get
			{
				return this.impulse;
			}
		}

		// Token: 0x040000E4 RID: 228
		internal readonly Vector3 m_Position;

		// Token: 0x040000E5 RID: 229
		internal readonly float m_Separation;

		// Token: 0x040000E6 RID: 230
		internal readonly Vector3 m_Normal;

		// Token: 0x040000E7 RID: 231
		internal readonly uint m_InternalFaceIndex0;

		// Token: 0x040000E8 RID: 232
		internal readonly Vector3 m_Impulse;

		// Token: 0x040000E9 RID: 233
		internal readonly uint m_InternalFaceIndex1;
	}
}
