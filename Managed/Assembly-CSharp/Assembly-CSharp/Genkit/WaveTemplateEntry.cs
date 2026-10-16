using System;
using UnityEngine;

namespace Genkit
{
	// Token: 0x02000395 RID: 917
	public class WaveTemplateEntry
	{
		// Token: 0x06002297 RID: 8855 RVA: 0x000AED84 File Offset: 0x000ACF84
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.name,
				" ",
				this.width.ToString(),
				"x",
				this.height.ToString()
			});
		}

		// Token: 0x0400115C RID: 4444
		public string name;

		// Token: 0x0400115D RID: 4445
		public Color32[] pixels;

		// Token: 0x0400115E RID: 4446
		public int width;

		// Token: 0x0400115F RID: 4447
		public int height;
	}
}
