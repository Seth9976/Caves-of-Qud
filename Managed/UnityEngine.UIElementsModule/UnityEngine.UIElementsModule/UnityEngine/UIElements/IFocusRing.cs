using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B9 RID: 697
	public interface IFocusRing
	{
		// Token: 0x060013E5 RID: 5093
		FocusChangeDirection GetFocusChangeDirection(Focusable currentFocusable, EventBase e);

		// Token: 0x060013E6 RID: 5094
		Focusable GetNextFocusable(Focusable currentFocusable, FocusChangeDirection direction);
	}
}
