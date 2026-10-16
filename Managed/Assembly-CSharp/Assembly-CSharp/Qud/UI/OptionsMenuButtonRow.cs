using System;
using System.Reflection;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x0200044F RID: 1103
	public class OptionsMenuButtonRow : OptionsDataRow<bool>
	{
		// Token: 0x060029B8 RID: 10680 RVA: 0x000DA667 File Offset: 0x000D8867
		public OptionsMenuButtonRow(GameOption Option)
			: base(Option)
		{
			this.OnClick = Option.OnClick;
		}

		// Token: 0x0400159B RID: 5531
		public MethodInfo OnClick;
	}
}
