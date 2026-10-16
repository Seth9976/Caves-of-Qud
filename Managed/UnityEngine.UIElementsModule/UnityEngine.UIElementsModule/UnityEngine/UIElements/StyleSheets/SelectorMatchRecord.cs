using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006DE RID: 1758
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal struct SelectorMatchRecord
	{
		// Token: 0x060032F0 RID: 13040 RVA: 0x000C55B6 File Offset: 0x000C37B6
		public SelectorMatchRecord(StyleSheet sheet, int styleSheetIndexInStack)
		{
			this = default(SelectorMatchRecord);
			this.sheet = sheet;
			this.styleSheetIndexInStack = styleSheetIndexInStack;
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x000C55D0 File Offset: 0x000C37D0
		public static int Compare(SelectorMatchRecord a, SelectorMatchRecord b)
		{
			bool flag = a.sheet.isDefaultStyleSheet != b.sheet.isDefaultStyleSheet;
			int num;
			if (flag)
			{
				num = (a.sheet.isDefaultStyleSheet ? (-1) : 1);
			}
			else
			{
				int num2 = a.complexSelector.specificity.CompareTo(b.complexSelector.specificity);
				bool flag2 = num2 == 0;
				if (flag2)
				{
					num2 = a.styleSheetIndexInStack.CompareTo(b.styleSheetIndexInStack);
				}
				bool flag3 = num2 == 0;
				if (flag3)
				{
					num2 = a.complexSelector.orderInStyleSheet.CompareTo(b.complexSelector.orderInStyleSheet);
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x040019EC RID: 6636
		public StyleSheet sheet;

		// Token: 0x040019ED RID: 6637
		public int styleSheetIndexInStack;

		// Token: 0x040019EE RID: 6638
		public StyleComplexSelector complexSelector;
	}
}
