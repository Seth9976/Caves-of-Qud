using System;

namespace System.Web.UI
{
	// Token: 0x02000227 RID: 551
	internal class StringPropertyBuilder : ControlBuilder
	{
		// Token: 0x060016AB RID: 5803 RVA: 0x0003AE22 File Offset: 0x00039022
		public StringPropertyBuilder(string prop_name)
		{
			this.prop_name = prop_name;
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x0003AE31 File Offset: 0x00039031
		public string PropertyName
		{
			get
			{
				return this.prop_name;
			}
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0003AE39 File Offset: 0x00039039
		public override void AppendSubBuilder(ControlBuilder subBuilder)
		{
			throw new HttpException("StringPropertyBuilder should never be called");
		}

		// Token: 0x0400157C RID: 5500
		private string prop_name;
	}
}
