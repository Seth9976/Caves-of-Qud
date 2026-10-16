using System;

namespace System.Web
{
	// Token: 0x02000072 RID: 114
	internal sealed class ExceptionPageTemplateFragmentValue
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x000092DC File Offset: 0x000074DC
		public string Value
		{
			get
			{
				if (this.valueProvider != null)
				{
					return this.valueProvider(this.name);
				}
				return this.value;
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000092FE File Offset: 0x000074FE
		public ExceptionPageTemplateFragmentValue(string name, Func<string, string> valueProvider)
		{
			this.valueProvider = valueProvider;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000930D File Offset: 0x0000750D
		public ExceptionPageTemplateFragmentValue(string name, string value)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x04000E85 RID: 3717
		private Func<string, string> valueProvider;

		// Token: 0x04000E86 RID: 3718
		private string value;

		// Token: 0x04000E87 RID: 3719
		private string name;
	}
}
