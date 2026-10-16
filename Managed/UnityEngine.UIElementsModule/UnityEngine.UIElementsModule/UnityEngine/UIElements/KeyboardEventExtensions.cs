using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000251 RID: 593
	internal static class KeyboardEventExtensions
	{
		// Token: 0x0600118D RID: 4493 RVA: 0x0004ACE0 File Offset: 0x00048EE0
		internal static bool ShouldSendNavigationMoveEvent(this KeyDownEvent e)
		{
			return e.keyCode == KeyCode.Tab && !e.ctrlKey && !e.altKey && !e.commandKey && !e.functionKey;
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0004AD20 File Offset: 0x00048F20
		internal static bool ShouldSendNavigationMoveEventRuntime(this Event e)
		{
			return e.type == EventType.KeyDown && e.keyCode == KeyCode.Tab;
		}
	}
}
