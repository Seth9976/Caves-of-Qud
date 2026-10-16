using System;
using System.ComponentModel;

namespace System.Web
{
	// Token: 0x0200005F RID: 95
	[AttributeUsage(AttributeTargets.All)]
	internal class WebSysDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x000074E0 File Offset: 0x000056E0
		internal WebSysDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x000074E9 File Offset: 0x000056E9
		public override string Description
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DescriptionValue = global::SR.GetString(base.Description);
				}
				return base.Description;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00007511 File Offset: 0x00005711
		public override object TypeId
		{
			get
			{
				return typeof(DescriptionAttribute);
			}
		}

		// Token: 0x04000E37 RID: 3639
		private bool replaced;
	}
}
