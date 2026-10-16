using System;

namespace System.Web.UI
{
	// Token: 0x020001D1 RID: 465
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class HtmlControlPersistableAttribute : Attribute
	{
		// Token: 0x060012D2 RID: 4818 RVA: 0x00030CB3 File Offset: 0x0002EEB3
		public HtmlControlPersistableAttribute(bool persist)
		{
			this.persist = persist;
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x00030CC2 File Offset: 0x0002EEC2
		public bool Persist
		{
			get
			{
				return this.persist;
			}
		}

		// Token: 0x04001438 RID: 5176
		private bool persist;
	}
}
