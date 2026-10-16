using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200023F RID: 575
	public interface IFocusEvent
	{
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600112D RID: 4397
		Focusable relatedTarget { get; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600112E RID: 4398
		FocusChangeDirection direction { get; }
	}
}
