using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200023B RID: 571
	public enum PropagationPhase
	{
		// Token: 0x04000A1A RID: 2586
		None,
		// Token: 0x04000A1B RID: 2587
		TrickleDown,
		// Token: 0x04000A1C RID: 2588
		BubbleUp = 3,
		// Token: 0x04000A1D RID: 2589
		[Obsolete("PropagationPhase.AtTarget has been removed as part of an event propagation simplification. Events now propagate through the TrickleDown phase followed immediately by the BubbleUp phase. Please use TrickleDown or BubbleUp. You can check if the event target is the current element by testing event.target == this in your local callback.", false)]
		AtTarget = 2,
		// Token: 0x04000A1E RID: 2590
		[Obsolete("PropagationPhase.DefaultAction has been removed as part of an event propagation simplification. ExecuteDefaultAction now occurs as part of the BubbleUp phase. Please use BubbleUp.", false)]
		DefaultAction = 4,
		// Token: 0x04000A1F RID: 2591
		[Obsolete("PropagationPhase.DefaultActionAtTarget has been removed as part of an event propagation simplification. ExecuteDefaultActionAtTarget now occurs as part of the BubbleUp phase. Please use BubbleUp", false)]
		DefaultActionAtTarget
	}
}
