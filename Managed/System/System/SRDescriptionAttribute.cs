using System;
using System.ComponentModel;

namespace System
{
	// Token: 0x02000178 RID: 376
	[AttributeUsage(AttributeTargets.All)]
	internal class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x060009EB RID: 2539 RVA: 0x0002BAB8 File Offset: 0x00029CB8
		public SRDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0002BAC1 File Offset: 0x00029CC1
		public override string Description
		{
			get
			{
				if (!this.isReplaced)
				{
					this.isReplaced = true;
					base.DescriptionValue = global::Locale.GetText(base.DescriptionValue);
				}
				return base.DescriptionValue;
			}
		}

		// Token: 0x040006D8 RID: 1752
		private bool isReplaced;
	}
}
