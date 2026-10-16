using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020006E1 RID: 1761
	internal class StyleSheetBuilder
	{
		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x060032F9 RID: 13049 RVA: 0x000C5CA9 File Offset: 0x000C3EA9
		public StyleProperty currentProperty
		{
			get
			{
				return this.m_CurrentProperty;
			}
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x000C5CB4 File Offset: 0x000C3EB4
		public StyleRule BeginRule(int ruleLine)
		{
			StyleSheetBuilder.Log("Beginning rule");
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Init);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Rule;
			this.m_CurrentRule = new StyleRule
			{
				line = ruleLine
			};
			return this.m_CurrentRule;
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x000C5D00 File Offset: 0x000C3F00
		public StyleSheetBuilder.ComplexSelectorScope BeginComplexSelector(int specificity)
		{
			StyleSheetBuilder.Log("Begin complex selector with specificity " + specificity.ToString());
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Rule);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.ComplexSelector;
			this.m_CurrentComplexSelector = new StyleComplexSelector();
			this.m_CurrentComplexSelector.specificity = specificity;
			this.m_CurrentComplexSelector.ruleIndex = this.m_Rules.Count;
			return new StyleSheetBuilder.ComplexSelectorScope(this);
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x000C5D74 File Offset: 0x000C3F74
		public void AddSimpleSelector(StyleSelectorPart[] parts, StyleSelectorRelationship previousRelationsip)
		{
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.ComplexSelector);
			StyleSelector styleSelector = new StyleSelector();
			styleSelector.parts = parts;
			styleSelector.previousRelationship = previousRelationsip;
			string text = "Add simple selector ";
			StyleSelector styleSelector2 = styleSelector;
			StyleSheetBuilder.Log(text + ((styleSelector2 != null) ? styleSelector2.ToString() : null));
			this.m_CurrentSelectors.Add(styleSelector);
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x000C5DD4 File Offset: 0x000C3FD4
		public void EndComplexSelector()
		{
			StyleSheetBuilder.Log("Ending complex selector");
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.ComplexSelector);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Rule;
			bool flag = this.m_CurrentSelectors.Count > 0;
			if (flag)
			{
				this.m_CurrentComplexSelector.selectors = this.m_CurrentSelectors.ToArray();
				this.m_ComplexSelectors.Add(this.m_CurrentComplexSelector);
				this.m_CurrentSelectors.Clear();
			}
			this.m_CurrentComplexSelector = null;
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x000C5E54 File Offset: 0x000C4054
		public StyleProperty BeginProperty(string name, int line = -1)
		{
			StyleSheetBuilder.Log("Begin property named " + name);
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Rule);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Property;
			this.m_CurrentProperty = new StyleProperty
			{
				name = name,
				line = line
			};
			this.m_CurrentProperties.Add(this.m_CurrentProperty);
			return this.m_CurrentProperty;
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x000C5EC0 File Offset: 0x000C40C0
		public void AddImport(StyleSheet.ImportStruct importStruct)
		{
			this.m_Imports.Add(importStruct);
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x000C5ED0 File Offset: 0x000C40D0
		public void AddValue(float value)
		{
			this.RegisterValue<float>(this.m_Floats, StyleValueType.Float, value);
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x000C5EE2 File Offset: 0x000C40E2
		public void AddValue(Dimension value)
		{
			this.RegisterValue<Dimension>(this.m_Dimensions, StyleValueType.Dimension, value);
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x000C5EF4 File Offset: 0x000C40F4
		public void AddValue(StyleValueKeyword keyword)
		{
			this.m_CurrentValues.Add(new StyleValueHandle((int)keyword, StyleValueType.Keyword));
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x000C5F0A File Offset: 0x000C410A
		public void AddValue(StyleValueFunction function)
		{
			this.m_CurrentValues.Add(new StyleValueHandle((int)function, StyleValueType.Function));
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x000C5F21 File Offset: 0x000C4121
		public void AddCommaSeparator()
		{
			this.m_CurrentValues.Add(new StyleValueHandle(0, StyleValueType.CommaSeparator));
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x000C5F38 File Offset: 0x000C4138
		public void AddValue(string value, StyleValueType type)
		{
			bool flag = type == StyleValueType.Variable;
			if (flag)
			{
				this.RegisterVariable(value);
			}
			else
			{
				this.RegisterValue<string>(this.m_Strings, type, value);
			}
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x000C5F67 File Offset: 0x000C4167
		public void AddValue(Color value)
		{
			this.RegisterValue<Color>(this.m_Colors, StyleValueType.Color, value);
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x000C5F79 File Offset: 0x000C4179
		public void AddValue(Object value)
		{
			this.RegisterValue<Object>(this.m_Assets, StyleValueType.AssetReference, value);
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x000C5F8B File Offset: 0x000C418B
		public void AddValue(ScalableImage value)
		{
			this.RegisterValue<ScalableImage>(this.m_ScalableImages, StyleValueType.ScalableImage, value);
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x000C5FA0 File Offset: 0x000C41A0
		public void EndProperty()
		{
			StyleSheetBuilder.Log("Ending property");
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Property);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Rule;
			this.m_CurrentProperty.values = this.m_CurrentValues.ToArray();
			this.m_CurrentProperty = null;
			this.m_CurrentValues.Clear();
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x000C5FFC File Offset: 0x000C41FC
		public int EndRule()
		{
			StyleSheetBuilder.Log("Ending rule");
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Rule);
			this.m_BuilderState = StyleSheetBuilder.BuilderState.Init;
			this.m_CurrentRule.properties = this.m_CurrentProperties.ToArray();
			this.m_Rules.Add(this.m_CurrentRule);
			this.m_CurrentRule = null;
			this.m_CurrentProperties.Clear();
			return this.m_Rules.Count - 1;
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x000C6078 File Offset: 0x000C4278
		public void BuildTo(StyleSheet writeTo)
		{
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Init);
			writeTo.floats = this.m_Floats.ToArray();
			writeTo.dimensions = this.m_Dimensions.ToArray();
			writeTo.colors = this.m_Colors.ToArray();
			writeTo.strings = this.m_Strings.ToArray();
			writeTo.rules = this.m_Rules.ToArray();
			writeTo.assets = this.m_Assets.ToArray();
			writeTo.scalableImages = this.m_ScalableImages.ToArray();
			writeTo.complexSelectors = this.m_ComplexSelectors.ToArray();
			writeTo.imports = this.m_Imports.ToArray();
			bool flag = writeTo.imports.Length != 0;
			if (flag)
			{
				writeTo.FlattenImportedStyleSheetsRecursive();
			}
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x000C6148 File Offset: 0x000C4348
		private void RegisterVariable(string value)
		{
			StyleSheetBuilder.Log("Add variable : " + value);
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Property);
			int num = this.m_Strings.IndexOf(value);
			bool flag = num < 0;
			if (flag)
			{
				this.m_Strings.Add(value);
				num = this.m_Strings.Count - 1;
			}
			this.m_CurrentValues.Add(new StyleValueHandle(num, StyleValueType.Variable));
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x000C61BC File Offset: 0x000C43BC
		private void RegisterValue<T>(List<T> list, StyleValueType type, T value)
		{
			string text = "Add value of type ";
			string text2 = type.ToString();
			string text3 = " : ";
			T t = value;
			StyleSheetBuilder.Log(text + text2 + text3 + ((t != null) ? t.ToString() : null));
			Debug.Assert(this.m_BuilderState == StyleSheetBuilder.BuilderState.Property);
			list.Add(value);
			this.m_CurrentValues.Add(new StyleValueHandle(list.Count - 1, type));
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x00002152 File Offset: 0x00000352
		private static void Log(string msg)
		{
		}

		// Token: 0x040019F1 RID: 6641
		private StyleSheetBuilder.BuilderState m_BuilderState;

		// Token: 0x040019F2 RID: 6642
		private List<float> m_Floats = new List<float>();

		// Token: 0x040019F3 RID: 6643
		private List<Dimension> m_Dimensions = new List<Dimension>();

		// Token: 0x040019F4 RID: 6644
		private List<Color> m_Colors = new List<Color>();

		// Token: 0x040019F5 RID: 6645
		private List<string> m_Strings = new List<string>();

		// Token: 0x040019F6 RID: 6646
		private List<StyleRule> m_Rules = new List<StyleRule>();

		// Token: 0x040019F7 RID: 6647
		private List<Object> m_Assets = new List<Object>();

		// Token: 0x040019F8 RID: 6648
		private List<ScalableImage> m_ScalableImages = new List<ScalableImage>();

		// Token: 0x040019F9 RID: 6649
		private List<StyleComplexSelector> m_ComplexSelectors = new List<StyleComplexSelector>();

		// Token: 0x040019FA RID: 6650
		private List<StyleProperty> m_CurrentProperties = new List<StyleProperty>();

		// Token: 0x040019FB RID: 6651
		private List<StyleValueHandle> m_CurrentValues = new List<StyleValueHandle>();

		// Token: 0x040019FC RID: 6652
		private StyleComplexSelector m_CurrentComplexSelector;

		// Token: 0x040019FD RID: 6653
		private List<StyleSelector> m_CurrentSelectors = new List<StyleSelector>();

		// Token: 0x040019FE RID: 6654
		private StyleProperty m_CurrentProperty;

		// Token: 0x040019FF RID: 6655
		private StyleRule m_CurrentRule;

		// Token: 0x04001A00 RID: 6656
		private List<StyleSheet.ImportStruct> m_Imports = new List<StyleSheet.ImportStruct>();

		// Token: 0x020006E2 RID: 1762
		public struct ComplexSelectorScope : IDisposable
		{
			// Token: 0x06003310 RID: 13072 RVA: 0x000C62D0 File Offset: 0x000C44D0
			public ComplexSelectorScope(StyleSheetBuilder builder)
			{
				this.m_Builder = builder;
			}

			// Token: 0x06003311 RID: 13073 RVA: 0x000C62DA File Offset: 0x000C44DA
			public void Dispose()
			{
				this.m_Builder.EndComplexSelector();
			}

			// Token: 0x04001A01 RID: 6657
			private StyleSheetBuilder m_Builder;
		}

		// Token: 0x020006E3 RID: 1763
		private enum BuilderState
		{
			// Token: 0x04001A03 RID: 6659
			Init,
			// Token: 0x04001A04 RID: 6660
			Rule,
			// Token: 0x04001A05 RID: 6661
			ComplexSelector,
			// Token: 0x04001A06 RID: 6662
			Property
		}
	}
}
