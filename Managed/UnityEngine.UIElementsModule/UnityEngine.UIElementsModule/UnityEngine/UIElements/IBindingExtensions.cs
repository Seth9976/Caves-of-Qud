using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000AA RID: 170
	public static class IBindingExtensions
	{
		// Token: 0x060006CE RID: 1742 RVA: 0x0001F3C0 File Offset: 0x0001D5C0
		public static bool IsBound(this IBindable control)
		{
			return ((control != null) ? control.binding : null) != null;
		}
	}
}
