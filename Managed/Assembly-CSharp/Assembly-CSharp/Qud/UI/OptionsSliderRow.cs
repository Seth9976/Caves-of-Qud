using System;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x0200044D RID: 1101
	public class OptionsSliderRow : OptionsDataRow<int>
	{
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060029B1 RID: 10673 RVA: 0x000DA5BA File Offset: 0x000D87BA
		public int Min
		{
			get
			{
				return this.Option.Min;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060029B2 RID: 10674 RVA: 0x000DA5C7 File Offset: 0x000D87C7
		public int Max
		{
			get
			{
				return this.Option.Max;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060029B3 RID: 10675 RVA: 0x000DA5D4 File Offset: 0x000D87D4
		public int Increment
		{
			get
			{
				return this.Option.Increment;
			}
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x000DA5E4 File Offset: 0x000D87E4
		public OptionsSliderRow(GameOption Option)
			: base(Option)
		{
			int min;
			if (!int.TryParse(Options.GetOption(Option.ID, ""), out min) && !int.TryParse(Option.Default, out min))
			{
				min = Option.Min;
			}
			base.Initialize(min);
		}
	}
}
