using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	public struct JointDrive
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000026A0 File Offset: 0x000008A0
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000026B8 File Offset: 0x000008B8
		public float positionSpring
		{
			get
			{
				return this.m_PositionSpring;
			}
			set
			{
				this.m_PositionSpring = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000026C4 File Offset: 0x000008C4
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000026DC File Offset: 0x000008DC
		public float positionDamper
		{
			get
			{
				return this.m_PositionDamper;
			}
			set
			{
				this.m_PositionDamper = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000026E8 File Offset: 0x000008E8
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002700 File Offset: 0x00000900
		public float maximumForce
		{
			get
			{
				return this.m_MaximumForce;
			}
			set
			{
				this.m_MaximumForce = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000270C File Offset: 0x0000090C
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002727 File Offset: 0x00000927
		public bool useAcceleration
		{
			get
			{
				return this.m_UseAcceleration == 1;
			}
			set
			{
				this.m_UseAcceleration = (value ? 1 : 0);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002738 File Offset: 0x00000938
		// (set) Token: 0x06000031 RID: 49 RVA: 0x0000274B File Offset: 0x0000094B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("JointDriveMode is obsolete")]
		public JointDriveMode mode
		{
			get
			{
				return JointDriveMode.None;
			}
			set
			{
			}
		}

		// Token: 0x0400001A RID: 26
		private float m_PositionSpring;

		// Token: 0x0400001B RID: 27
		private float m_PositionDamper;

		// Token: 0x0400001C RID: 28
		private float m_MaximumForce;

		// Token: 0x0400001D RID: 29
		private int m_UseAcceleration;
	}
}
