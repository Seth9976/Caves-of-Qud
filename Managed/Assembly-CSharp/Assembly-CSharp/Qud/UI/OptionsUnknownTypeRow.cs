using System;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x02000450 RID: 1104
	public class OptionsUnknownTypeRow : OptionsDataRow<string>
	{
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060029B9 RID: 10681 RVA: 0x000DA67C File Offset: 0x000D887C
		public string Type
		{
			get
			{
				return this.Option.Type;
			}
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x000DA689 File Offset: 0x000D8889
		public OptionsUnknownTypeRow(GameOption Option)
			: base(Option)
		{
			base.Initialize(Options.GetOption(Option.ID, ""));
		}
	}
}
