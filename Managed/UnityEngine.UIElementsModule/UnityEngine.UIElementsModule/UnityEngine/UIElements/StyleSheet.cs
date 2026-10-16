using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020004D1 RID: 1233
	[HelpURL("UIE-USS")]
	[Serializable]
	public class StyleSheet : ScriptableObject
	{
		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060025BE RID: 9662 RVA: 0x00088C6C File Offset: 0x00086E6C
		// (set) Token: 0x060025BF RID: 9663 RVA: 0x00088C84 File Offset: 0x00086E84
		public bool importedWithErrors
		{
			get
			{
				return this.m_ImportedWithErrors;
			}
			internal set
			{
				this.m_ImportedWithErrors = value;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060025C0 RID: 9664 RVA: 0x00088C90 File Offset: 0x00086E90
		// (set) Token: 0x060025C1 RID: 9665 RVA: 0x00088CA8 File Offset: 0x00086EA8
		public bool importedWithWarnings
		{
			get
			{
				return this.m_ImportedWithWarnings;
			}
			internal set
			{
				this.m_ImportedWithWarnings = value;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060025C2 RID: 9666 RVA: 0x00088CB4 File Offset: 0x00086EB4
		// (set) Token: 0x060025C3 RID: 9667 RVA: 0x00088CCC File Offset: 0x00086ECC
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal StyleRule[] rules
		{
			get
			{
				return this.m_Rules;
			}
			set
			{
				this.m_Rules = value;
				this.SetupReferences();
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x00088CE0 File Offset: 0x00086EE0
		// (set) Token: 0x060025C5 RID: 9669 RVA: 0x00088CF8 File Offset: 0x00086EF8
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal StyleComplexSelector[] complexSelectors
		{
			get
			{
				return this.m_ComplexSelectors;
			}
			set
			{
				this.m_ComplexSelectors = value;
				this.SetupReferences();
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060025C6 RID: 9670 RVA: 0x00088D0C File Offset: 0x00086F0C
		internal List<StyleSheet> flattenedRecursiveImports
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_FlattenedImportedStyleSheets;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x00088D24 File Offset: 0x00086F24
		// (set) Token: 0x060025C8 RID: 9672 RVA: 0x00088D3C File Offset: 0x00086F3C
		public int contentHash
		{
			get
			{
				return this.m_ContentHash;
			}
			set
			{
				this.m_ContentHash = value;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060025C9 RID: 9673 RVA: 0x00088D48 File Offset: 0x00086F48
		// (set) Token: 0x060025CA RID: 9674 RVA: 0x00088D60 File Offset: 0x00086F60
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool isDefaultStyleSheet
		{
			get
			{
				return this.m_IsDefaultStyleSheet;
			}
			set
			{
				this.m_IsDefaultStyleSheet = value;
				bool flag = this.flattenedRecursiveImports != null;
				if (flag)
				{
					foreach (StyleSheet styleSheet in this.flattenedRecursiveImports)
					{
						styleSheet.isDefaultStyleSheet = value;
					}
				}
			}
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x00088DD0 File Offset: 0x00086FD0
		private bool TryCheckAccess<T>(T[] list, StyleValueType type, StyleValueHandle handle, out T value)
		{
			bool flag = false;
			value = default(T);
			bool flag2 = handle.valueType == type && handle.valueIndex >= 0 && handle.valueIndex < list.Length;
			if (flag2)
			{
				value = list[handle.valueIndex];
				flag = true;
			}
			else
			{
				Debug.LogErrorFormat(this, "Trying to read value of type {0} while reading a value of type {1}", new object[] { type, handle.valueType });
			}
			return flag;
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x00088E58 File Offset: 0x00087058
		private T CheckAccess<T>(T[] list, StyleValueType type, StyleValueHandle handle)
		{
			T t = default(T);
			bool flag = handle.valueType != type;
			if (flag)
			{
				Debug.LogErrorFormat(this, "Trying to read value of type {0} while reading a value of type {1}", new object[] { type, handle.valueType });
			}
			else
			{
				bool flag2 = list == null || handle.valueIndex < 0 || handle.valueIndex >= list.Length;
				if (flag2)
				{
					Debug.LogError("Accessing invalid property", this);
				}
				else
				{
					t = list[handle.valueIndex];
				}
			}
			return t;
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x00088EF2 File Offset: 0x000870F2
		internal virtual void OnEnable()
		{
			this.SetupReferences();
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x00088EFC File Offset: 0x000870FC
		internal void FlattenImportedStyleSheetsRecursive()
		{
			this.m_FlattenedImportedStyleSheets = new List<StyleSheet>();
			this.FlattenImportedStyleSheetsRecursive(this);
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x00088F14 File Offset: 0x00087114
		private void FlattenImportedStyleSheetsRecursive(StyleSheet sheet)
		{
			bool flag = sheet.imports == null;
			if (!flag)
			{
				for (int i = 0; i < sheet.imports.Length; i++)
				{
					StyleSheet styleSheet = sheet.imports[i].styleSheet;
					bool flag2 = styleSheet == null;
					if (!flag2)
					{
						styleSheet.isDefaultStyleSheet = this.isDefaultStyleSheet;
						this.FlattenImportedStyleSheetsRecursive(styleSheet);
						this.m_FlattenedImportedStyleSheets.Add(styleSheet);
					}
				}
			}
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x00088F90 File Offset: 0x00087190
		private void SetupReferences()
		{
			bool flag = this.complexSelectors == null || this.rules == null;
			if (!flag)
			{
				foreach (StyleRule styleRule in this.rules)
				{
					foreach (StyleProperty styleProperty in styleRule.properties)
					{
						bool flag2 = StyleSheet.CustomStartsWith(styleProperty.name, StyleSheet.kCustomPropertyMarker);
						if (flag2)
						{
							styleRule.customPropertiesCount++;
							styleProperty.isCustomProperty = true;
						}
						foreach (StyleValueHandle styleValueHandle in styleProperty.values)
						{
							bool flag3 = styleValueHandle.IsVarFunction();
							if (flag3)
							{
								styleProperty.requireVariableResolve = true;
								break;
							}
						}
					}
				}
				int l = 0;
				int num = this.complexSelectors.Length;
				while (l < num)
				{
					this.complexSelectors[l].CachePseudoStateMasks();
					l++;
				}
				this.tables = new Dictionary<string, StyleComplexSelector>[3];
				this.tables[0] = new Dictionary<string, StyleComplexSelector>(StringComparer.Ordinal);
				this.tables[1] = new Dictionary<string, StyleComplexSelector>(StringComparer.Ordinal);
				this.tables[2] = new Dictionary<string, StyleComplexSelector>(StringComparer.Ordinal);
				this.nonEmptyTablesMask = 0;
				this.firstRootSelector = null;
				this.firstWildCardSelector = null;
				int m = 0;
				while (m < this.complexSelectors.Length)
				{
					StyleComplexSelector styleComplexSelector = this.complexSelectors[m];
					bool flag4 = styleComplexSelector.ruleIndex < this.rules.Length;
					if (flag4)
					{
						styleComplexSelector.rule = this.rules[styleComplexSelector.ruleIndex];
					}
					styleComplexSelector.CalculateHashes();
					styleComplexSelector.orderInStyleSheet = m;
					StyleSelector styleSelector = styleComplexSelector.selectors[styleComplexSelector.selectors.Length - 1];
					StyleSelectorPart styleSelectorPart = styleSelector.parts[0];
					string text = styleSelectorPart.value;
					StyleSheet.OrderedSelectorType orderedSelectorType = StyleSheet.OrderedSelectorType.None;
					switch (styleSelectorPart.type)
					{
					case StyleSelectorType.Wildcard:
					{
						bool flag5 = this.firstWildCardSelector != null;
						if (flag5)
						{
							styleComplexSelector.nextInTable = this.firstWildCardSelector;
						}
						this.firstWildCardSelector = styleComplexSelector;
						break;
					}
					case StyleSelectorType.Type:
						text = styleSelectorPart.value;
						orderedSelectorType = StyleSheet.OrderedSelectorType.Type;
						break;
					case StyleSelectorType.Class:
						orderedSelectorType = StyleSheet.OrderedSelectorType.Class;
						break;
					case StyleSelectorType.PseudoClass:
					{
						bool flag6 = (styleSelector.pseudoStateMask & 128) != 0;
						if (flag6)
						{
							bool flag7 = this.firstRootSelector != null;
							if (flag7)
							{
								styleComplexSelector.nextInTable = this.firstRootSelector;
							}
							this.firstRootSelector = styleComplexSelector;
						}
						else
						{
							bool flag8 = this.firstWildCardSelector != null;
							if (flag8)
							{
								styleComplexSelector.nextInTable = this.firstWildCardSelector;
							}
							this.firstWildCardSelector = styleComplexSelector;
						}
						break;
					}
					case StyleSelectorType.RecursivePseudoClass:
						goto IL_02CC;
					case StyleSelectorType.ID:
						orderedSelectorType = StyleSheet.OrderedSelectorType.Name;
						break;
					default:
						goto IL_02CC;
					}
					IL_02EB:
					bool flag9 = orderedSelectorType != StyleSheet.OrderedSelectorType.None;
					if (flag9)
					{
						Dictionary<string, StyleComplexSelector> dictionary = this.tables[(int)orderedSelectorType];
						StyleComplexSelector styleComplexSelector2;
						bool flag10 = dictionary.TryGetValue(text, out styleComplexSelector2);
						if (flag10)
						{
							styleComplexSelector.nextInTable = styleComplexSelector2;
						}
						this.nonEmptyTablesMask |= 1 << (int)orderedSelectorType;
						dictionary[text] = styleComplexSelector;
					}
					m++;
					continue;
					IL_02CC:
					Debug.LogError(string.Format("Invalid first part type {0}", styleSelectorPart.type), this);
					goto IL_02EB;
				}
			}
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x000892FC File Offset: 0x000874FC
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal StyleValueKeyword ReadKeyword(StyleValueHandle handle)
		{
			return (StyleValueKeyword)handle.valueIndex;
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x00089314 File Offset: 0x00087514
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal float ReadFloat(StyleValueHandle handle)
		{
			bool flag = handle.valueType == StyleValueType.Dimension;
			float num;
			if (flag)
			{
				Dimension dimension = this.CheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle);
				num = dimension.value;
			}
			else
			{
				num = this.CheckAccess<float>(this.floats, StyleValueType.Float, handle);
			}
			return num;
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x0008935C File Offset: 0x0008755C
		internal bool TryReadFloat(StyleValueHandle handle, out float value)
		{
			bool flag = this.TryCheckAccess<float>(this.floats, StyleValueType.Float, handle, out value);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				Dimension dimension;
				bool flag3 = this.TryCheckAccess<Dimension>(this.dimensions, StyleValueType.Float, handle, out dimension);
				value = dimension.value;
				flag2 = flag3;
			}
			return flag2;
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x000893A0 File Offset: 0x000875A0
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal Dimension ReadDimension(StyleValueHandle handle)
		{
			bool flag = handle.valueType == StyleValueType.Float;
			Dimension dimension;
			if (flag)
			{
				float num = this.CheckAccess<float>(this.floats, StyleValueType.Float, handle);
				dimension = new Dimension(num, Dimension.Unit.Unitless);
			}
			else
			{
				dimension = this.CheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle);
			}
			return dimension;
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000893E8 File Offset: 0x000875E8
		internal bool TryReadDimension(StyleValueHandle handle, out Dimension value)
		{
			bool flag = this.TryCheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle, out value);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				float num = 0f;
				bool flag3 = this.TryCheckAccess<float>(this.floats, StyleValueType.Float, handle, out num);
				value = new Dimension(num, Dimension.Unit.Unitless);
				flag2 = flag3;
			}
			return flag2;
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x00089438 File Offset: 0x00087638
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal Color ReadColor(StyleValueHandle handle)
		{
			return this.CheckAccess<Color>(this.colors, StyleValueType.Color, handle);
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x00089458 File Offset: 0x00087658
		internal bool TryReadColor(StyleValueHandle handle, out Color value)
		{
			return this.TryCheckAccess<Color>(this.colors, StyleValueType.Color, handle, out value);
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x0008947C File Offset: 0x0008767C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal string ReadString(StyleValueHandle handle)
		{
			return this.CheckAccess<string>(this.strings, StyleValueType.String, handle);
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x000894A0 File Offset: 0x000876A0
		internal bool TryReadString(StyleValueHandle handle, out string value)
		{
			return this.TryCheckAccess<string>(this.strings, StyleValueType.String, handle, out value);
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x000894C4 File Offset: 0x000876C4
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal string ReadEnum(StyleValueHandle handle)
		{
			return this.CheckAccess<string>(this.strings, StyleValueType.Enum, handle);
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x000894E4 File Offset: 0x000876E4
		internal bool TryReadEnum(StyleValueHandle handle, out string value)
		{
			return this.TryCheckAccess<string>(this.strings, StyleValueType.Enum, handle, out value);
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x00089508 File Offset: 0x00087708
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal string ReadVariable(StyleValueHandle handle)
		{
			return this.CheckAccess<string>(this.strings, StyleValueType.Variable, handle);
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x00089528 File Offset: 0x00087728
		internal bool TryReadVariable(StyleValueHandle handle, out string value)
		{
			return this.TryCheckAccess<string>(this.strings, StyleValueType.Variable, handle, out value);
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x0008954C File Offset: 0x0008774C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal string ReadResourcePath(StyleValueHandle handle)
		{
			return this.CheckAccess<string>(this.strings, StyleValueType.ResourcePath, handle);
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x0008956C File Offset: 0x0008776C
		internal bool TryReadResourcePath(StyleValueHandle handle, out string value)
		{
			return this.TryCheckAccess<string>(this.strings, StyleValueType.ResourcePath, handle, out value);
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x00089590 File Offset: 0x00087790
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal Object ReadAssetReference(StyleValueHandle handle)
		{
			return this.CheckAccess<Object>(this.assets, StyleValueType.AssetReference, handle);
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000895B0 File Offset: 0x000877B0
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal string ReadMissingAssetReferenceUrl(StyleValueHandle handle)
		{
			return this.CheckAccess<string>(this.strings, StyleValueType.MissingAssetReference, handle);
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x000895D4 File Offset: 0x000877D4
		internal bool TryReadAssetReference(StyleValueHandle handle, out Object value)
		{
			return this.TryCheckAccess<Object>(this.assets, StyleValueType.AssetReference, handle, out value);
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x000895F8 File Offset: 0x000877F8
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal StyleValueFunction ReadFunction(StyleValueHandle handle)
		{
			return (StyleValueFunction)handle.valueIndex;
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x00089610 File Offset: 0x00087810
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal string ReadFunctionName(StyleValueHandle handle)
		{
			bool flag = handle.valueType != StyleValueType.Function;
			string text;
			if (flag)
			{
				Debug.LogErrorFormat(this, string.Format("Trying to read value of type {0} while reading a value of type {1}", StyleValueType.Function, handle.valueType), Array.Empty<object>());
				text = string.Empty;
			}
			else
			{
				StyleValueFunction valueIndex = (StyleValueFunction)handle.valueIndex;
				text = valueIndex.ToUssString();
			}
			return text;
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x00089674 File Offset: 0x00087874
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal ScalableImage ReadScalableImage(StyleValueHandle handle)
		{
			return this.CheckAccess<ScalableImage>(this.scalableImages, StyleValueType.ScalableImage, handle);
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x00089698 File Offset: 0x00087898
		private static bool CustomStartsWith(string originalString, string pattern)
		{
			int length = originalString.Length;
			int length2 = pattern.Length;
			int num = 0;
			int num2 = 0;
			while (num < length && num2 < length2 && originalString[num] == pattern[num2])
			{
				num++;
				num2++;
			}
			return (num2 == length2 && length >= length2) || (num == length && length2 >= length);
		}

		// Token: 0x040010D8 RID: 4312
		[SerializeField]
		private bool m_ImportedWithErrors;

		// Token: 0x040010D9 RID: 4313
		[SerializeField]
		private bool m_ImportedWithWarnings;

		// Token: 0x040010DA RID: 4314
		[SerializeField]
		private StyleRule[] m_Rules;

		// Token: 0x040010DB RID: 4315
		[SerializeField]
		private StyleComplexSelector[] m_ComplexSelectors;

		// Token: 0x040010DC RID: 4316
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[SerializeField]
		internal float[] floats;

		// Token: 0x040010DD RID: 4317
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[SerializeField]
		internal Dimension[] dimensions;

		// Token: 0x040010DE RID: 4318
		[SerializeField]
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal Color[] colors;

		// Token: 0x040010DF RID: 4319
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[SerializeField]
		internal string[] strings;

		// Token: 0x040010E0 RID: 4320
		[SerializeField]
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal Object[] assets;

		// Token: 0x040010E1 RID: 4321
		[SerializeField]
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal StyleSheet.ImportStruct[] imports;

		// Token: 0x040010E2 RID: 4322
		[SerializeField]
		private List<StyleSheet> m_FlattenedImportedStyleSheets;

		// Token: 0x040010E3 RID: 4323
		[SerializeField]
		private int m_ContentHash;

		// Token: 0x040010E4 RID: 4324
		[SerializeField]
		internal ScalableImage[] scalableImages;

		// Token: 0x040010E5 RID: 4325
		[NonSerialized]
		internal Dictionary<string, StyleComplexSelector>[] tables;

		// Token: 0x040010E6 RID: 4326
		[NonSerialized]
		internal int nonEmptyTablesMask;

		// Token: 0x040010E7 RID: 4327
		[NonSerialized]
		internal StyleComplexSelector firstRootSelector;

		// Token: 0x040010E8 RID: 4328
		[NonSerialized]
		internal StyleComplexSelector firstWildCardSelector;

		// Token: 0x040010E9 RID: 4329
		[NonSerialized]
		private bool m_IsDefaultStyleSheet;

		// Token: 0x040010EA RID: 4330
		private static string kCustomPropertyMarker = "--";

		// Token: 0x020004D2 RID: 1234
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[Serializable]
		internal struct ImportStruct
		{
			// Token: 0x040010EB RID: 4331
			public StyleSheet styleSheet;

			// Token: 0x040010EC RID: 4332
			public string[] mediaQueries;
		}

		// Token: 0x020004D3 RID: 1235
		internal enum OrderedSelectorType
		{
			// Token: 0x040010EE RID: 4334
			None = -1,
			// Token: 0x040010EF RID: 4335
			Name,
			// Token: 0x040010F0 RID: 4336
			Type,
			// Token: 0x040010F1 RID: 4337
			Class,
			// Token: 0x040010F2 RID: 4338
			Length
		}
	}
}
