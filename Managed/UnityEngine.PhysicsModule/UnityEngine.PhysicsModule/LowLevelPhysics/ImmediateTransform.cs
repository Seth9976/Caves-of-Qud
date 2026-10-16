using System;

namespace UnityEngine.LowLevelPhysics
{
	// Token: 0x02000055 RID: 85
	public struct ImmediateTransform
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0000BD8C File Offset: 0x00009F8C
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x0000BDA4 File Offset: 0x00009FA4
		public Quaternion Rotation
		{
			get
			{
				return this.m_Rotation;
			}
			set
			{
				this.m_Rotation = value;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0000BDB0 File Offset: 0x00009FB0
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x0000BDC8 File Offset: 0x00009FC8
		public Vector3 Position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x04000163 RID: 355
		private Quaternion m_Rotation;

		// Token: 0x04000164 RID: 356
		private Vector3 m_Position;
	}
}
