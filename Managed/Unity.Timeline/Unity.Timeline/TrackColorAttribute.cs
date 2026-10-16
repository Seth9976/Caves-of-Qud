using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000013 RID: 19
	[AttributeUsage(AttributeTargets.Class)]
	public class TrackColorAttribute : Attribute
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00006712 File Offset: 0x00004912
		public Color color
		{
			get
			{
				return this.m_Color;
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000671A File Offset: 0x0000491A
		public TrackColorAttribute(float r, float g, float b)
		{
			this.m_Color = new Color(r, g, b);
		}

		// Token: 0x04000086 RID: 134
		private Color m_Color;
	}
}
