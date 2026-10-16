using System;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x0200044E RID: 1102
	public class OptionsComboBoxRow : OptionsDataRow<string>
	{
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060029B5 RID: 10677 RVA: 0x000DA62E File Offset: 0x000D882E
		public string[] Options
		{
			get
			{
				return this.Option.Values;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060029B6 RID: 10678 RVA: 0x000DA63B File Offset: 0x000D883B
		public string[] DisplayOptions
		{
			get
			{
				return this.Option.DisplayValues;
			}
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x000DA648 File Offset: 0x000D8848
		public OptionsComboBoxRow(GameOption Option)
			: base(Option)
		{
			base.Initialize(XRL.UI.Options.GetOption(Option.ID, ""));
		}
	}
}
