using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000512 RID: 1298
	internal struct RuleMatcher
	{
		// Token: 0x0600284E RID: 10318 RVA: 0x000936A4 File Offset: 0x000918A4
		public RuleMatcher(StyleSheet sheet, StyleComplexSelector complexSelector, int styleSheetIndexInStack)
		{
			this.sheet = sheet;
			this.complexSelector = complexSelector;
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x000936B8 File Offset: 0x000918B8
		public override string ToString()
		{
			return this.complexSelector.ToString();
		}

		// Token: 0x04001234 RID: 4660
		public StyleSheet sheet;

		// Token: 0x04001235 RID: 4661
		public StyleComplexSelector complexSelector;
	}
}
