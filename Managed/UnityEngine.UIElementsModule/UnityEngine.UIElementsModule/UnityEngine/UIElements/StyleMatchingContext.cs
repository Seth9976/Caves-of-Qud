using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020005D4 RID: 1492
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class StyleMatchingContext
	{
		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06002C00 RID: 11264 RVA: 0x0009EDD5 File Offset: 0x0009CFD5
		public int styleSheetCount
		{
			get
			{
				return this.m_StyleSheetStack.Count;
			}
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x0009EDE2 File Offset: 0x0009CFE2
		public StyleMatchingContext(Action<VisualElement, MatchResultInfo> processResult)
		{
			this.m_StyleSheetStack = new List<StyleSheet>();
			this.variableContext = StyleVariableContext.none;
			this.currentElement = null;
			this.processResult = processResult;
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x0009EE1C File Offset: 0x0009D01C
		public void AddStyleSheet(StyleSheet sheet)
		{
			bool flag = sheet == null;
			if (!flag)
			{
				this.m_StyleSheetStack.Add(sheet);
			}
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x0009EE44 File Offset: 0x0009D044
		public void RemoveStyleSheetRange(int index, int count)
		{
			this.m_StyleSheetStack.RemoveRange(index, count);
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x0009EE58 File Offset: 0x0009D058
		public StyleSheet GetStyleSheetAt(int index)
		{
			return this.m_StyleSheetStack[index];
		}

		// Token: 0x0400141F RID: 5151
		private List<StyleSheet> m_StyleSheetStack;

		// Token: 0x04001420 RID: 5152
		public StyleVariableContext variableContext;

		// Token: 0x04001421 RID: 5153
		public VisualElement currentElement;

		// Token: 0x04001422 RID: 5154
		public Action<VisualElement, MatchResultInfo> processResult;

		// Token: 0x04001423 RID: 5155
		public AncestorFilter ancestorFilter = new AncestorFilter();
	}
}
