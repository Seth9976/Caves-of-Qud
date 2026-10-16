using System;

namespace UnityEngine
{
	// Token: 0x02000232 RID: 562
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	public class TooltipAttribute : PropertyAttribute
	{
		// Token: 0x06001E60 RID: 7776 RVA: 0x0003A826 File Offset: 0x00038A26
		public TooltipAttribute(string tooltip)
		{
			this.tooltip = tooltip;
		}

		// Token: 0x04000901 RID: 2305
		public readonly string tooltip;
	}
}
