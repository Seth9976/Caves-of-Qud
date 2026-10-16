using System;

namespace XRL.UI
{
	// Token: 0x020014C5 RID: 5317
	public class SinusoidalTextParticle
	{
		// Token: 0x0400520B RID: 21003
		public float x;

		// Token: 0x0400520C RID: 21004
		public float y;

		// Token: 0x0400520D RID: 21005
		public float xdel;

		// Token: 0x0400520E RID: 21006
		public float ydel;

		// Token: 0x0400520F RID: 21007
		public float sinstart;

		// Token: 0x04005210 RID: 21008
		public float sinincrement;

		// Token: 0x04005211 RID: 21009
		public float amp;

		// Token: 0x04005212 RID: 21010
		public float xamp;

		// Token: 0x04005213 RID: 21011
		public float yamp;

		// Token: 0x04005214 RID: 21012
		public string Text;

		// Token: 0x04005215 RID: 21013
		public TextBlock TextBlock;

		// Token: 0x04005216 RID: 21014
		public int TextWidth = 12;

		// Token: 0x04005217 RID: 21015
		public int Life = 999;

		// Token: 0x04005218 RID: 21016
		public bool bBorder;

		// Token: 0x04005219 RID: 21017
		public long DelayMS;
	}
}
