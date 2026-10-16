using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B6 RID: 438
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public sealed class ToggleButtonGroupStatePropertiesAttribute : PropertyAttribute
	{
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x0003D6A3 File Offset: 0x0003B8A3
		public bool allowMultipleSelection { get; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0003D6AB File Offset: 0x0003B8AB
		public bool allowEmptySelection { get; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0003D6B3 File Offset: 0x0003B8B3
		public int length { get; }

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0003D6BB File Offset: 0x0003B8BB
		public ToggleButtonGroupStatePropertiesAttribute(bool allowMultipleSelection = true, bool allowEmptySelection = true, int length = -1)
		{
			this.allowMultipleSelection = allowMultipleSelection;
			this.allowEmptySelection = allowEmptySelection;
			this.length = length;
		}
	}
}
