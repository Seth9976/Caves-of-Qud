using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000546 RID: 1350
	public abstract class UxmlAttributeDescription
	{
		// Token: 0x0600294B RID: 10571 RVA: 0x00095A94 File Offset: 0x00093C94
		protected UxmlAttributeDescription()
		{
			this.use = UxmlAttributeDescription.Use.Optional;
			this.restriction = null;
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x00095AAE File Offset: 0x00093CAE
		// (set) Token: 0x0600294D RID: 10573 RVA: 0x00095AB6 File Offset: 0x00093CB6
		public string name { get; set; }

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x0600294E RID: 10574 RVA: 0x00095ABF File Offset: 0x00093CBF
		// (set) Token: 0x0600294F RID: 10575 RVA: 0x00095AC8 File Offset: 0x00093CC8
		public IEnumerable<string> obsoleteNames
		{
			get
			{
				return this.m_ObsoleteNames;
			}
			set
			{
				string[] array = value as string[];
				bool flag = array != null;
				if (flag)
				{
					this.m_ObsoleteNames = array;
				}
				else
				{
					this.m_ObsoleteNames = value.ToArray<string>();
				}
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06002950 RID: 10576 RVA: 0x00095AFA File Offset: 0x00093CFA
		// (set) Token: 0x06002951 RID: 10577 RVA: 0x00095B02 File Offset: 0x00093D02
		public string type { get; protected internal set; }

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06002952 RID: 10578 RVA: 0x00095B0B File Offset: 0x00093D0B
		// (set) Token: 0x06002953 RID: 10579 RVA: 0x00095B13 File Offset: 0x00093D13
		public string typeNamespace { get; protected set; }

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06002954 RID: 10580
		public abstract string defaultValueAsString { get; }

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06002955 RID: 10581 RVA: 0x00095B1C File Offset: 0x00093D1C
		// (set) Token: 0x06002956 RID: 10582 RVA: 0x00095B24 File Offset: 0x00093D24
		public UxmlAttributeDescription.Use use { get; set; }

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06002957 RID: 10583 RVA: 0x00095B2D File Offset: 0x00093D2D
		// (set) Token: 0x06002958 RID: 10584 RVA: 0x00095B35 File Offset: 0x00093D35
		public UxmlTypeRestriction restriction { get; set; }

		// Token: 0x06002959 RID: 10585 RVA: 0x00095B40 File Offset: 0x00093D40
		internal bool TryFindValueInAttributeOverrides(string elementName, CreationContext cc, List<TemplateAsset.AttributeOverride> attributeOverrides, out string value)
		{
			value = null;
			TemplateAsset.AttributeOverride attributeOverride = default(TemplateAsset.AttributeOverride);
			foreach (TemplateAsset.AttributeOverride attributeOverride2 in attributeOverrides)
			{
				bool flag = cc.namesPath == null;
				if (flag)
				{
					bool flag2 = attributeOverride2.m_ElementName != elementName;
					if (flag2)
					{
						continue;
					}
				}
				else
				{
					bool flag3 = !attributeOverride2.NamesPathMatchesElementNamesPath(cc.namesPath);
					if (flag3)
					{
						continue;
					}
				}
				bool flag4 = attributeOverride2.m_AttributeName != this.name;
				if (flag4)
				{
					bool flag5 = this.m_ObsoleteNames != null;
					if (!flag5)
					{
						continue;
					}
					bool flag6 = false;
					foreach (string text in this.m_ObsoleteNames)
					{
						bool flag7 = attributeOverride2.m_AttributeName != text;
						if (!flag7)
						{
							flag6 = true;
							break;
						}
					}
					bool flag8 = !flag6;
					if (flag8)
					{
						continue;
					}
				}
				bool flag9 = attributeOverride.m_AttributeName == null;
				if (flag9)
				{
					attributeOverride = attributeOverride2;
					bool flag10 = attributeOverride.m_NamesPath == null;
					if (flag10)
					{
						break;
					}
				}
				else
				{
					bool flag11 = attributeOverride.m_NamesPath.Length < attributeOverride2.m_NamesPath.Length;
					if (flag11)
					{
						attributeOverride = attributeOverride2;
					}
				}
			}
			bool flag12 = attributeOverride.m_AttributeName != null;
			bool flag13;
			if (flag12)
			{
				value = attributeOverride.m_Value;
				flag13 = true;
			}
			else
			{
				flag13 = false;
			}
			return flag13;
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x00095CD8 File Offset: 0x00093ED8
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool TryGetValueFromBagAsString(IUxmlAttributes bag, CreationContext cc, out string value)
		{
			VisualTreeAsset visualTreeAsset;
			return this.TryGetValueFromBagAsString(bag, cc, out value, out visualTreeAsset);
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x00095CF8 File Offset: 0x00093EF8
		internal bool TryGetAttributeOverrideValueFromBagAsString(IUxmlAttributes bag, CreationContext cc, out string value, out VisualTreeAsset sourceAsset)
		{
			string text;
			bag.TryGetAttributeValue("name", out text);
			bool flag = !string.IsNullOrEmpty(text) && cc.attributeOverrides != null;
			if (flag)
			{
				foreach (CreationContext.AttributeOverrideRange attributeOverrideRange in cc.attributeOverrides)
				{
					bool flag2 = this.TryFindValueInAttributeOverrides(text, cc, attributeOverrideRange.attributeOverrides, out value);
					if (flag2)
					{
						sourceAsset = attributeOverrideRange.sourceAsset;
						return true;
					}
				}
			}
			sourceAsset = null;
			value = null;
			return false;
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x00095DA8 File Offset: 0x00093FA8
		internal bool ValidateName()
		{
			bool flag = this.name == null && (this.m_ObsoleteNames == null || this.m_ObsoleteNames.Length == 0);
			bool flag2;
			if (flag)
			{
				Debug.LogError("Attribute description has no name.");
				flag2 = false;
			}
			else
			{
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x00095DF0 File Offset: 0x00093FF0
		internal bool TryGetValueFromBagAsString(IUxmlAttributes bag, CreationContext cc, out string value, out VisualTreeAsset sourceAsset)
		{
			value = null;
			sourceAsset = null;
			bool flag = !this.ValidateName();
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.TryGetAttributeOverrideValueFromBagAsString(bag, cc, out value, out sourceAsset);
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = this.name == null;
					if (flag4)
					{
						for (int i = 0; i < this.m_ObsoleteNames.Length; i++)
						{
							bool flag5 = bag.TryGetAttributeValue(this.m_ObsoleteNames[i], out value);
							if (flag5)
							{
								sourceAsset = cc.visualTreeAsset;
								return true;
							}
						}
						flag2 = false;
					}
					else
					{
						bool flag6 = !bag.TryGetAttributeValue(this.name, out value);
						if (flag6)
						{
							bool flag7 = this.m_ObsoleteNames != null;
							if (flag7)
							{
								for (int j = 0; j < this.m_ObsoleteNames.Length; j++)
								{
									bool flag8 = bag.TryGetAttributeValue(this.m_ObsoleteNames[j], out value);
									if (flag8)
									{
										sourceAsset = cc.visualTreeAsset;
										UxmlAsset uxmlAsset = bag as UxmlAsset;
										bool flag9 = uxmlAsset != null;
										if (flag9)
										{
											uxmlAsset.RemoveAttribute(this.m_ObsoleteNames[j]);
											uxmlAsset.SetAttribute(this.name, value);
										}
										return true;
									}
								}
							}
							flag2 = false;
						}
						else
						{
							sourceAsset = cc.visualTreeAsset;
							flag2 = true;
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x00095F44 File Offset: 0x00094144
		protected bool TryGetValueFromBag<T>(IUxmlAttributes bag, CreationContext cc, Func<string, T, T> converterFunc, T defaultValue, ref T value)
		{
			string text;
			bool flag = this.TryGetValueFromBagAsString(bag, cc, out text);
			bool flag3;
			if (flag)
			{
				bool flag2 = converterFunc != null;
				if (flag2)
				{
					value = converterFunc(text, defaultValue);
				}
				else
				{
					value = defaultValue;
				}
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x00095F94 File Offset: 0x00094194
		protected T GetValueFromBag<T>(IUxmlAttributes bag, CreationContext cc, Func<string, T, T> converterFunc, T defaultValue)
		{
			bool flag = converterFunc == null;
			if (flag)
			{
				throw new ArgumentNullException("converterFunc");
			}
			string text;
			bool flag2 = this.TryGetValueFromBagAsString(bag, cc, out text);
			T t;
			if (flag2)
			{
				t = converterFunc(text, defaultValue);
			}
			else
			{
				t = defaultValue;
			}
			return t;
		}

		// Token: 0x0400128C RID: 4748
		protected const string xmlSchemaNamespace = "http://www.w3.org/2001/XMLSchema";

		// Token: 0x0400128E RID: 4750
		private string[] m_ObsoleteNames;

		// Token: 0x02000547 RID: 1351
		public enum Use
		{
			// Token: 0x04001294 RID: 4756
			None,
			// Token: 0x04001295 RID: 4757
			Optional,
			// Token: 0x04001296 RID: 4758
			Prohibited,
			// Token: 0x04001297 RID: 4759
			Required
		}
	}
}
