using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x0200111B RID: 4379
	public class MissilePath
	{
		// Token: 0x0600AD51 RID: 44369 RVA: 0x003FA794 File Offset: 0x003F8994
		public void Reset()
		{
			this.Path.Clear();
			List<float> cover = this.Cover;
			if (cover != null)
			{
				cover.Clear();
			}
			this.Angle = 0f;
			this.X0 = 0f;
			this.Y0 = 0f;
			this.X1 = 0f;
			this.Y1 = 0f;
		}

		// Token: 0x04004798 RID: 18328
		public List<Cell> Path = new List<Cell>();

		// Token: 0x04004799 RID: 18329
		public List<float> Cover;

		// Token: 0x0400479A RID: 18330
		public float Angle;

		// Token: 0x0400479B RID: 18331
		public float X0;

		// Token: 0x0400479C RID: 18332
		public float Y0;

		// Token: 0x0400479D RID: 18333
		public float X1;

		// Token: 0x0400479E RID: 18334
		public float Y1;
	}
}
