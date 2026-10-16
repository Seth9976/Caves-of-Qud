using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000023 RID: 35
	internal readonly struct BindingTarget
	{
		// Token: 0x060000AC RID: 172 RVA: 0x000036BB File Offset: 0x000018BB
		public BindingTarget(VisualElement element, in BindingId bindingId)
		{
			this.element = element;
			this.bindingId = bindingId;
		}

		// Token: 0x0400004B RID: 75
		public readonly VisualElement element;

		// Token: 0x0400004C RID: 76
		public readonly BindingId bindingId;
	}
}
