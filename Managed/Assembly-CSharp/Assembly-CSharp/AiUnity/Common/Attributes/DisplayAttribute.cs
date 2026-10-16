using System;
using System.ComponentModel;

namespace AiUnity.Common.Attributes
{
	// Token: 0x020002E6 RID: 742
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public sealed class DisplayAttribute : DisplayNameAttribute
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x000815C3 File Offset: 0x0007F7C3
		// (set) Token: 0x06001974 RID: 6516 RVA: 0x000815CB File Offset: 0x0007F7CB
		public bool Advanced { get; private set; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x000815D4 File Offset: 0x0007F7D4
		// (set) Token: 0x06001976 RID: 6518 RVA: 0x000815DC File Offset: 0x0007F7DC
		public int Order { get; private set; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001977 RID: 6519 RVA: 0x000815E5 File Offset: 0x0007F7E5
		// (set) Token: 0x06001978 RID: 6520 RVA: 0x000815ED File Offset: 0x0007F7ED
		public string Tooltip { get; private set; }

		// Token: 0x06001979 RID: 6521 RVA: 0x000815F6 File Offset: 0x0007F7F6
		public DisplayAttribute(string name, string toolTip, bool advanced = false, int order = 0)
			: base(name)
		{
			this.Advanced = advanced;
			this.Tooltip = toolTip;
			this.Order = order;
		}
	}
}
