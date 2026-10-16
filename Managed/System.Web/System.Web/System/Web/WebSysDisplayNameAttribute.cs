using System;
using System.ComponentModel;

namespace System.Web
{
	// Token: 0x02000060 RID: 96
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Event)]
	internal sealed class WebSysDisplayNameAttribute : DisplayNameAttribute
	{
		// Token: 0x0600040C RID: 1036 RVA: 0x0000751D File Offset: 0x0000571D
		internal WebSysDisplayNameAttribute(string DisplayName)
			: base(DisplayName)
		{
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00007526 File Offset: 0x00005726
		public override string DisplayName
		{
			get
			{
				if (!this.replaced)
				{
					this.replaced = true;
					base.DisplayNameValue = global::SR.GetString(base.DisplayName);
				}
				return base.DisplayName;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000754E File Offset: 0x0000574E
		public override object TypeId
		{
			get
			{
				return typeof(DisplayNameAttribute);
			}
		}

		// Token: 0x04000E38 RID: 3640
		private bool replaced;
	}
}
