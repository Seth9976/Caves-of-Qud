using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000683 RID: 1667
	internal class GradientRemap : LinkedPoolItem<GradientRemap>
	{
		// Token: 0x0600307B RID: 12411 RVA: 0x000B7A60 File Offset: 0x000B5C60
		public void Reset()
		{
			this.origIndex = 0;
			this.destIndex = 0;
			this.location = default(RectInt);
			this.atlas = TextureId.invalid;
		}

		// Token: 0x040017D2 RID: 6098
		public int origIndex;

		// Token: 0x040017D3 RID: 6099
		public int destIndex;

		// Token: 0x040017D4 RID: 6100
		public RectInt location;

		// Token: 0x040017D5 RID: 6101
		public GradientRemap next;

		// Token: 0x040017D6 RID: 6102
		public TextureId atlas;
	}
}
