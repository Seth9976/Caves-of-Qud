using System;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x0200044C RID: 1100
	public class OptionsCheckboxRow : OptionsDataRow<bool>
	{
		// Token: 0x060029B0 RID: 10672 RVA: 0x000DA591 File Offset: 0x000D8791
		public OptionsCheckboxRow(GameOption Option)
			: base(Option)
		{
			base.Initialize(Options.GetOption(Option.ID, "") == "Yes");
		}
	}
}
