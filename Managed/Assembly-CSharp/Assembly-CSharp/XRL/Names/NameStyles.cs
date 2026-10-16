using System;
using System.Collections.Generic;
using System.Xml;
using XRL.Rules;
using XRL.UI;
using XRL.World;

namespace XRL.Names
{
	// Token: 0x02000582 RID: 1410
	[HasModSensitiveStaticCache]
	public static class NameStyles
	{
		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06003C81 RID: 15489 RVA: 0x0012884D File Offset: 0x00126A4D
		public static Dictionary<string, NameStyle> NameStyleTable
		{
			get
			{
				NameStyles.CheckInit();
				return NameStyles._NameStyleTable;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06003C82 RID: 15490 RVA: 0x00128859 File Offset: 0x00126A59
		public static List<NameStyle> NameStyleList
		{
			get
			{
				NameStyles.CheckInit();
				return NameStyles._NameStyleList;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06003C83 RID: 15491 RVA: 0x00128865 File Offset: 0x00126A65
		public static Dictionary<string, List<NameValue>> DefaultTemplateVars
		{
			get
			{
				NameStyles.CheckInit();
				return NameStyles._DefaultTemplateVars;
			}
		}

		// Token: 0x06003C84 RID: 15492 RVA: 0x00128871 File Offset: 0x00126A71
		public static void CheckInit()
		{
			if (NameStyles._NameStyleTable == null)
			{
				Loading.LoadTask("Loading Naming.xml", new Action(NameStyles.Init), true);
			}
		}

		// Token: 0x06003C85 RID: 15493 RVA: 0x00128894 File Offset: 0x00126A94
		private static void Init()
		{
			NameStyles._NameStyleTable = new Dictionary<string, NameStyle>(16);
			NameStyles._NameStyleList = new List<NameStyle>(16);
			NameStyles._DefaultTemplateVars = new Dictionary<string, List<NameValue>>();
			foreach (DataFile dataFile in DataManager.GetXMLFilesWithRoot("Naming"))
			{
				try
				{
					NameStyles.ProcessNamingXmlFile(dataFile, dataFile.IsMod);
				}
				catch (Exception ex)
				{
					MetricsManager.LogPotentialModError(dataFile.Mod, ex);
				}
			}
		}

		// Token: 0x06003C86 RID: 15494 RVA: 0x00128934 File Offset: 0x00126B34
		private static void ProcessNamingXmlFile(string file, bool Mod)
		{
			using (XmlTextReader streamingAssetsXMLStream = DataManager.GetStreamingAssetsXMLStream(file))
			{
				streamingAssetsXMLStream.WhitespaceHandling = WhitespaceHandling.None;
				while (streamingAssetsXMLStream.Read())
				{
					if (streamingAssetsXMLStream.NodeType == XmlNodeType.Element)
					{
						if (!(streamingAssetsXMLStream.Name == "naming"))
						{
							throw new XmlUnsupportedElementException(streamingAssetsXMLStream);
						}
						NameStyles.LoadNamingNode(streamingAssetsXMLStream, Mod);
					}
				}
				streamingAssetsXMLStream.Close();
			}
		}

		// Token: 0x06003C87 RID: 15495 RVA: 0x001289A8 File Offset: 0x00126BA8
		public static void LoadNamingNode(XmlTextReader Reader, bool Mod = false)
		{
			string text = (Mod ? Reader.GetAttribute("Load") : null);
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (Reader.Name == "namestyles")
					{
						NameStyles.LoadNameStylesNode(Reader, Mod, text);
					}
					else
					{
						if (!(Reader.Name == "defaulttemplatevars"))
						{
							throw new XmlUnsupportedElementException(Reader);
						}
						NameStyles.LoadDefaultTemplateVarsNode(Reader, Mod, text);
					}
				}
				else if (Reader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		// Token: 0x06003C88 RID: 15496 RVA: 0x00128A24 File Offset: 0x00126C24
		public static void LoadNameStylesNode(XmlTextReader Reader, bool Mod = false, string LoadMode = null)
		{
			if (Mod)
			{
				LoadMode = Reader.GetAttribute("Load") ?? LoadMode;
			}
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (!(Reader.Name == "namestyle"))
					{
						throw new XmlUnsupportedElementException(Reader);
					}
					NameStyles.LoadNameStyleNode(Reader, Mod, LoadMode);
				}
				else if (Reader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		// Token: 0x06003C89 RID: 15497 RVA: 0x00128A88 File Offset: 0x00126C88
		public static void LoadNameStyleNode(XmlTextReader Reader, bool Mod = false, string LoadMode = null)
		{
			string attribute = Reader.GetAttribute("Name");
			if (attribute == null)
			{
				throw new XmlException(Reader.Name + " tag had no Name attribute", Reader);
			}
			if (Mod)
			{
				LoadMode = Reader.GetAttribute("Load") ?? LoadMode;
			}
			NameStyle nameStyle;
			if (NameStyles._NameStyleTable.TryGetValue(attribute, out nameStyle))
			{
				if (!Mod)
				{
					MetricsManager.LogError("duplicate name style " + attribute);
					return;
				}
				if (LoadMode != "Merge")
				{
					NameStyles._NameStyleList.Remove(nameStyle);
					nameStyle = new NameStyle();
					nameStyle.Name = attribute;
					NameStyles._NameStyleTable[attribute] = nameStyle;
				}
			}
			else
			{
				nameStyle = new NameStyle();
				nameStyle.Name = attribute;
				NameStyles._NameStyleTable.Add(attribute, nameStyle);
				NameStyles._NameStyleList.Add(nameStyle);
			}
			string text = Reader.GetAttribute("HyphenationChance");
			if (!text.IsNullOrEmpty())
			{
				if (!int.TryParse(text, out nameStyle.HyphenationChance))
				{
					throw new XmlException("invalid HyphenationChance: " + text, Reader);
				}
				nameStyle.HyphenationChanceSet = true;
			}
			text = Reader.GetAttribute("TwoNameChance");
			if (!text.IsNullOrEmpty())
			{
				if (!int.TryParse(text, out nameStyle.TwoNameChance))
				{
					throw new XmlException("invalid TwoNameChance: " + text, Reader);
				}
				nameStyle.TwoNameChanceSet = true;
			}
			text = Reader.GetAttribute("Base");
			if (!text.IsNullOrEmpty())
			{
				nameStyle.Base = text;
			}
			text = Reader.GetAttribute("Format");
			if (!text.IsNullOrEmpty())
			{
				nameStyle.Format = text;
			}
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (Reader.Name == "prefixes")
					{
						NameStyles.LoadNameStylePrefixesNode(nameStyle, Reader, Mod, LoadMode);
					}
					else if (Reader.Name == "infixes")
					{
						NameStyles.LoadNameStyleInfixesNode(nameStyle, Reader, Mod, LoadMode);
					}
					else if (Reader.Name == "postfixes")
					{
						NameStyles.LoadNameStylePostfixesNode(nameStyle, Reader, Mod, LoadMode);
					}
					else if (Reader.Name == "templates" || Reader.Name == "titletemplates")
					{
						NameStyles.LoadNameStyleTemplatesNode(nameStyle, Reader, Mod, LoadMode);
					}
					else if (Reader.Name == "templatevars")
					{
						NameStyles.LoadNameStyleTemplateVarsNode(nameStyle, Reader, Mod, LoadMode);
					}
					else
					{
						if (!(Reader.Name == "scopes"))
						{
							throw new XmlUnsupportedElementException(Reader);
						}
						NameStyles.LoadNameStyleScopesNode(nameStyle, Reader, Mod, LoadMode);
					}
				}
				else if (Reader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		// Token: 0x06003C8A RID: 15498 RVA: 0x00128CF0 File Offset: 0x00126EF0
		public static void LoadNameStylePrefixesNode(NameStyle style, XmlTextReader Reader, bool Mod = false, string LoadMode = null)
		{
			if (Mod)
			{
				LoadMode = Reader.GetAttribute("Load") ?? LoadMode;
				if (LoadMode != "Merge")
				{
					style.Prefixes.Clear();
				}
			}
			string attribute = Reader.GetAttribute("Amount");
			if (!attribute.IsNullOrEmpty())
			{
				style.PrefixAmount = attribute;
			}
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (!(Reader.Name == "prefix"))
					{
						throw new XmlUnsupportedElementException(Reader);
					}
					NameStyles.LoadNameStylePrefixNode(style, Reader, Mod, LoadMode);
				}
				else if (Reader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		// Token: 0x06003C8B RID: 15499 RVA: 0x00128D88 File Offset: 0x00126F88
		public static void LoadNameStylePrefixNode(NameStyle style, XmlTextReader Reader, bool Mod = false, string LoadMode = null)
		{
			string attribute = Reader.GetAttribute("Name");
			if (attribute == null)
			{
				throw new XmlException(Reader.Name + " tag had no Name attribute", Reader);
			}
			NamePrefix namePrefix = null;
			bool flag = false;
			if (Mod)
			{
				LoadMode = Reader.GetAttribute("Load") ?? LoadMode;
				namePrefix = style.Prefixes.Find<NamePrefix>(attribute);
				if (namePrefix != null)
				{
					if (LoadMode != "Merge")
					{
						style.Prefixes.Remove(namePrefix);
						namePrefix = null;
					}
					else
					{
						flag = true;
					}
				}
			}
			else if (style.Prefixes.Has<NamePrefix>(attribute))
			{
				throw new XmlException("duplicate element", Reader);
			}
			if (namePrefix == null)
			{
				namePrefix = new NamePrefix();
				namePrefix.Name = attribute;
			}
			string attribute2 = Reader.GetAttribute("Weight");
			if (!attribute2.IsNullOrEmpty() && !int.TryParse(attribute2, out namePrefix.Weight))
			{
				throw new XmlException("invalid Weight: " + attribute2, Reader);
			}
			if (!flag)
			{
				style.Prefixes.Add(namePrefix);
			}
		}

		// Token: 0x06003C8C RID: 15500 RVA: 0x00128E74 File Offset: 0x00127074
		public static void LoadNameStyleInfixesNode(NameStyle style, XmlTextReader Reader, bool Mod = false, string LoadMode = null)
		{
			if (Mod)
			{
				LoadMode = Reader.GetAttribute("Load") ?? LoadMode;
				if (LoadMode != "Merge")
				{
					style.Infixes.Clear();
				}
			}
			string attribute = Reader.GetAttribute("Amount");
			if (!attribute.IsNullOrEmpty())
			{
				style.InfixAmount = attribute;
			}
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (!(Reader.Name == "infix"))
					{
						throw new XmlUnsupportedElementException(Reader);
					}
					NameStyles.LoadNameStyleInfixNode(style, Reader, Mod, LoadMode);
				}
				else if (Reader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		// Token: 0x06003C8D RID: 15501 RVA: 0x00128F0C File Offset: 0x0012710C
		public static void LoadNameStyleInfixNode(NameStyle style, XmlTextReader Reader, bool Mod = false, string LoadMode = null)
		{
			string attribute = Reader.GetAttribute("Name");
			if (attribute == null)
			{
				throw new XmlException(Reader.Name + " tag had no Name attribute", Reader);
			}
			NameInfix nameInfix = null;
			bool flag = false;
			if (Mod)
			{
				LoadMode = Reader.GetAttribute("Load") ?? LoadMode;
				nameInfix = style.Infixes.Find<NameInfix>(attribute);
				if (nameInfix != null)
				{
					if (LoadMode != "Merge")
					{
						style.Infixes.Remove(nameInfix);
						nameInfix = null;
					}
					else
					{
						flag = true;
					}
				}
			}
			else if (style.Infixes.Has<NameInfix>(attribute))
			{
				throw new XmlException("duplicate element", Reader);
			}
			if (nameInfix == null)
			{
				nameInfix = new NameInfix();
				nameInfix.Name = attribute;
			}
			string attribute2 = Reader.GetAttribute("Weight");
			if (!attribute2.IsNullOrEmpty() && !int.TryParse(attribute2, out nameInfix.Weight))
			{
				throw new XmlException("invalid Weight: " + attribute2, Reader);
			}
			if (!flag)
			{
				style.Infixes.Add(nameInfix);
			}
		}

		// Token: 0x06003C8E RID: 15502 RVA: 0x00128FF8 File Offset: 0x001271F8
		public static void LoadNameStylePostfixesNode(NameStyle style, XmlTextReader Reader, bool Mod = false, string LoadMode = null)
		{
			if (Mod)
			{
				LoadMode = Reader.GetAttribute("Load") ?? LoadMode;
				if (LoadMode != "Merge")
				{
					style.Postfixes.Clear();
				}
			}
			string attribute = Reader.GetAttribute("Amount");
			if (!attribute.IsNullOrEmpty())
			{
				style.PostfixAmount = attribute;
			}
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (!(Reader.Name == "postfix"))
					{
						throw new XmlUnsupportedElementException(Reader);
					}
					NameStyles.LoadNameStylePostfixNode(style, Reader, Mod, LoadMode);
				}
				else if (Reader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		// Token: 0x06003C8F RID: 15503 RVA: 0x00129090 File Offset: 0x00127290
		public static void LoadNameStylePostfixNode(NameStyle style, XmlTextReader Reader, bool Mod = false, string LoadMode = null)
		{
			string attribute = Reader.GetAttribute("Name");
			if (attribute == null)
			{
				throw new XmlException(Reader.Name + " tag had no Name attribute", Reader);
			}
			NamePostfix namePostfix = null;
			bool flag = false;
			if (Mod)
			{
				LoadMode = Reader.GetAttribute("Load") ?? LoadMode;
				namePostfix = style.Postfixes.Find<NamePostfix>(attribute);
				if (namePostfix != null)
				{
					if (LoadMode != "Merge")
					{
						style.Postfixes.Remove(namePostfix);
						namePostfix = null;
					}
					else
					{
						flag = true;
					}
				}
			}
			else if (style.Postfixes.Has<NamePostfix>(attribute))
			{
				throw new XmlException("duplicate element", Reader);
			}
			if (namePostfix == null)
			{
				namePostfix = new NamePostfix();
				namePostfix.Name = attribute;
			}
			string attribute2 = Reader.GetAttribute("Weight");
			if (!attribute2.IsNullOrEmpty() && !int.TryParse(attribute2, out namePostfix.Weight))
			{
				throw new XmlException("invalid Weight: " + attribute2, Reader);
			}
			if (!flag)
			{
				style.Postfixes.Add(namePostfix);
			}
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x0012917C File Offset: 0x0012737C
		public static void LoadNameStyleTemplatesNode(NameStyle style, XmlTextReader Reader, bool Mod = false, string LoadMode = null)
		{
			if (Mod)
			{
				LoadMode = Reader.GetAttribute("Load") ?? LoadMode;
				if (LoadMode != "Merge")
				{
					style.Templates.Clear();
				}
			}
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (!(Reader.Name == "template") && !(Reader.Name == "titletemplate"))
					{
						throw new XmlUnsupportedElementException(Reader);
					}
					NameStyles.LoadNameStyleTemplateNode(style, Reader, Mod, LoadMode);
				}
				else if (Reader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		// Token: 0x06003C91 RID: 15505 RVA: 0x0012920C File Offset: 0x0012740C
		public static void LoadNameStyleTemplateNode(NameStyle style, XmlTextReader Reader, bool Mod = false, string LoadMode = null)
		{
			string attribute = Reader.GetAttribute("Name");
			if (attribute == null)
			{
				throw new XmlException(Reader.Name + " tag had no Name attribute", Reader);
			}
			NameTemplate nameTemplate = null;
			bool flag = false;
			if (Mod)
			{
				LoadMode = Reader.GetAttribute("Load") ?? LoadMode;
				nameTemplate = style.Templates.Find<NameTemplate>(attribute);
				if (nameTemplate != null)
				{
					if (LoadMode != "Merge")
					{
						style.Templates.Remove(nameTemplate);
						nameTemplate = null;
					}
					else
					{
						flag = true;
					}
				}
			}
			else if (style.Templates.Has<NameTemplate>(attribute))
			{
				throw new XmlException("duplicate element", Reader);
			}
			if (nameTemplate == null)
			{
				nameTemplate = new NameTemplate();
				nameTemplate.Name = attribute;
			}
			string attribute2 = Reader.GetAttribute("Weight");
			if (!attribute2.IsNullOrEmpty() && !int.TryParse(attribute2, out nameTemplate.Weight))
			{
				throw new XmlException("invalid Weight: " + attribute2, Reader);
			}
			if (!flag)
			{
				style.Templates.Add(nameTemplate);
			}
		}

		// Token: 0x06003C92 RID: 15506 RVA: 0x001292F8 File Offset: 0x001274F8
		public static void LoadNameStyleTemplateVarsNode(NameStyle style, XmlTextReader Reader, bool Mod = false, string LoadMode = null)
		{
			if (Mod)
			{
				LoadMode = Reader.GetAttribute("Load") ?? LoadMode;
				if (LoadMode != "Merge")
				{
					style.TemplateVars.Clear();
				}
			}
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (!(Reader.Name == "templatevar"))
					{
						throw new XmlUnsupportedElementException(Reader);
					}
					NameStyles.LoadNameStyleTemplateVarNode(style, Reader, Mod, LoadMode);
				}
				else if (Reader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		// Token: 0x06003C93 RID: 15507 RVA: 0x00129374 File Offset: 0x00127574
		public static void LoadNameStyleTemplateVarNode(NameStyle Style, XmlTextReader Reader, bool Mod = false, string LoadMode = null)
		{
			string attribute = Reader.GetAttribute("Name");
			if (attribute == null)
			{
				throw new XmlException(Reader.Name + " tag had no Name attribute", Reader);
			}
			List<NameValue> list = null;
			bool flag = false;
			if (Mod)
			{
				LoadMode = Reader.GetAttribute("Load") ?? LoadMode;
				if (Style.TemplateVars != null && Style.TemplateVars.TryGetValue(attribute, out list))
				{
					if (LoadMode != "Merge")
					{
						list.Clear();
					}
					flag = true;
				}
			}
			else if (Style.TemplateVars.ContainsKey(attribute))
			{
				throw new XmlException("duplicate element", Reader);
			}
			if (list == null)
			{
				list = new List<NameValue>();
			}
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (!(Reader.Name == "value"))
					{
						throw new XmlUnsupportedElementException(Reader);
					}
					NameStyles.LoadNameStyleTemplateValueNode(Style, list, Reader, Mod, LoadMode);
				}
				else if (Reader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
			}
			if (!flag)
			{
				if (Style.TemplateVars == null)
				{
					Style.TemplateVars = new Dictionary<string, List<NameValue>>();
				}
				Style.TemplateVars[attribute] = list;
			}
		}

		// Token: 0x06003C94 RID: 15508 RVA: 0x0012947C File Offset: 0x0012767C
		public static void LoadNameStyleTemplateValueNode(NameStyle Style, List<NameValue> List, XmlTextReader Reader, bool Mod = false, string LoadMode = null)
		{
			string attribute = Reader.GetAttribute("Name");
			if (attribute == null)
			{
				throw new XmlException(Reader.Name + " tag had no Name attribute", Reader);
			}
			NameValue nameValue = null;
			bool flag = false;
			if (Mod)
			{
				LoadMode = Reader.GetAttribute("Load") ?? LoadMode;
				nameValue = List.Find<NameValue>(attribute);
				if (nameValue != null)
				{
					if (LoadMode != "Merge")
					{
						Style.TemplateVars.Remove(attribute);
						nameValue = null;
					}
					else
					{
						flag = true;
					}
				}
			}
			else if (List.Has<NameValue>(attribute))
			{
				throw new XmlException("duplicate element", Reader);
			}
			if (nameValue == null)
			{
				nameValue = new NameValue();
				nameValue.Name = attribute;
			}
			if (!flag)
			{
				List.Add(nameValue);
			}
		}

		// Token: 0x06003C95 RID: 15509 RVA: 0x00129528 File Offset: 0x00127728
		public static void LoadNameStyleScopesNode(NameStyle Style, XmlTextReader Reader, bool Mod = false, string LoadMode = null)
		{
			if (Mod)
			{
				LoadMode = Reader.GetAttribute("Load") ?? LoadMode;
				if (LoadMode != "Merge")
				{
					Style.Scopes.Clear();
				}
			}
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (!(Reader.Name == "scope"))
					{
						throw new XmlUnsupportedElementException(Reader);
					}
					NameStyles.LoadNameStyleScopeNode(Style, Reader, Mod, LoadMode);
				}
				else if (Reader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		// Token: 0x06003C96 RID: 15510 RVA: 0x001295A4 File Offset: 0x001277A4
		public static void LoadNameStyleScopeNode(NameStyle Style, XmlTextReader Reader, bool Mod = false, string LoadMode = null)
		{
			string attribute = Reader.GetAttribute("Name");
			if (attribute == null)
			{
				throw new XmlException(Reader.Name + " tag had no Name attribute", Reader);
			}
			NameScope nameScope = null;
			bool flag = false;
			if (Mod)
			{
				LoadMode = Reader.GetAttribute("Load") ?? LoadMode;
				nameScope = Style.Scopes.Find<NameScope>(attribute);
				if (nameScope != null)
				{
					if (LoadMode != "Merge")
					{
						Style.Scopes.Remove(nameScope);
						nameScope = null;
					}
					else
					{
						flag = true;
					}
				}
			}
			else if (Style.Scopes.Has<NameScope>(attribute))
			{
				throw new XmlException("duplicate element", Reader);
			}
			if (nameScope == null)
			{
				nameScope = new NameScope();
				nameScope.Name = attribute;
			}
			string text = Reader.GetAttribute("Weight");
			if (!text.IsNullOrEmpty() && !int.TryParse(text, out nameScope.Weight))
			{
				throw new XmlException("invalid Weight: " + text, Reader);
			}
			text = Reader.GetAttribute("Genotype");
			if (!text.IsNullOrEmpty())
			{
				nameScope.Genotype = text;
			}
			text = Reader.GetAttribute("Subtype");
			if (!text.IsNullOrEmpty())
			{
				nameScope.Subtype = text;
			}
			text = Reader.GetAttribute("Species");
			if (!text.IsNullOrEmpty())
			{
				nameScope.Species = text;
			}
			text = Reader.GetAttribute("Culture");
			if (!text.IsNullOrEmpty())
			{
				nameScope.Culture = text;
			}
			text = Reader.GetAttribute("Faction");
			if (!text.IsNullOrEmpty())
			{
				nameScope.Faction = text;
			}
			text = Reader.GetAttribute("Region");
			if (!text.IsNullOrEmpty())
			{
				nameScope.Region = text;
			}
			text = Reader.GetAttribute("Gender");
			if (!text.IsNullOrEmpty())
			{
				nameScope.Gender = text;
			}
			text = Reader.GetAttribute("Mutation");
			if (!text.IsNullOrEmpty())
			{
				nameScope.Mutation = text;
			}
			text = Reader.GetAttribute("Tag");
			if (!text.IsNullOrEmpty())
			{
				nameScope.Tag = text;
			}
			text = Reader.GetAttribute("Special");
			if (!text.IsNullOrEmpty())
			{
				nameScope.Special = text;
			}
			text = Reader.GetAttribute("Type");
			if (!text.IsNullOrEmpty())
			{
				nameScope.Type = text;
			}
			text = Reader.GetAttribute("Priority");
			if (!text.IsNullOrEmpty() && !int.TryParse(text, out nameScope.Priority))
			{
				throw new XmlException("invalid Priority: " + text, Reader);
			}
			text = Reader.GetAttribute("Chance");
			if (!text.IsNullOrEmpty() && !int.TryParse(text, out nameScope.Chance))
			{
				throw new XmlException("invalid Chance: " + text, Reader);
			}
			text = Reader.GetAttribute("Combine");
			if (!text.IsNullOrEmpty() && !bool.TryParse(text, out nameScope.Combine))
			{
				throw new XmlException("invalid Combine: " + text, Reader);
			}
			text = Reader.GetAttribute("SkipIfHasHonorific");
			if (!text.IsNullOrEmpty() && !bool.TryParse(text, out nameScope.SkipIfHasHonorific))
			{
				throw new XmlException("invalid SkipIfHasHonorific: " + text, Reader);
			}
			text = Reader.GetAttribute("SkipIfHasEpithet");
			if (!text.IsNullOrEmpty() && !bool.TryParse(text, out nameScope.SkipIfHasEpithet))
			{
				throw new XmlException("invalid SkipIfHasEpithet: " + text, Reader);
			}
			if (!flag)
			{
				Style.Scopes.Add(nameScope);
			}
		}

		// Token: 0x06003C97 RID: 15511 RVA: 0x001298BC File Offset: 0x00127ABC
		public static void LoadDefaultTemplateVarsNode(XmlTextReader Reader, bool Mod = false, string LoadMode = null)
		{
			if (Mod)
			{
				LoadMode = Reader.GetAttribute("Load") ?? LoadMode;
				if (LoadMode != "Merge")
				{
					NameStyles._DefaultTemplateVars.Clear();
				}
			}
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (!(Reader.Name == "templatevar"))
					{
						throw new XmlUnsupportedElementException(Reader);
					}
					NameStyles.LoadDefaultTemplateVarNode(Reader, Mod, LoadMode);
				}
				else if (Reader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		// Token: 0x06003C98 RID: 15512 RVA: 0x00129938 File Offset: 0x00127B38
		public static void LoadDefaultTemplateVarNode(XmlTextReader Reader, bool Mod = false, string LoadMode = null)
		{
			string attribute = Reader.GetAttribute("Name");
			if (attribute == null)
			{
				throw new XmlException(Reader.Name + " tag had no Name attribute", Reader);
			}
			List<NameValue> list = null;
			bool flag = false;
			if (Mod)
			{
				LoadMode = Reader.GetAttribute("Load") ?? LoadMode;
				if (NameStyles._DefaultTemplateVars != null && NameStyles._DefaultTemplateVars.TryGetValue(attribute, out list))
				{
					if (LoadMode != "Merge")
					{
						list.Clear();
					}
					flag = true;
				}
			}
			else if (NameStyles._DefaultTemplateVars.ContainsKey(attribute))
			{
				throw new XmlException("duplicate element", Reader);
			}
			if (list == null)
			{
				list = new List<NameValue>();
			}
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (!(Reader.Name == "value"))
					{
						throw new XmlUnsupportedElementException(Reader);
					}
					NameStyles.LoadDefaultTemplateValueNode(list, Reader, Mod, LoadMode);
				}
				else if (Reader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
			}
			if (!flag)
			{
				if (NameStyles._DefaultTemplateVars == null)
				{
					NameStyles._DefaultTemplateVars = new Dictionary<string, List<NameValue>>();
				}
				NameStyles._DefaultTemplateVars[attribute] = list;
			}
		}

		// Token: 0x06003C99 RID: 15513 RVA: 0x00129A34 File Offset: 0x00127C34
		public static void LoadDefaultTemplateValueNode(List<NameValue> List, XmlTextReader Reader, bool Mod = false, string LoadMode = null)
		{
			string attribute = Reader.GetAttribute("Name");
			if (attribute == null)
			{
				throw new XmlException(Reader.Name + " tag had no Name attribute", Reader);
			}
			NameValue nameValue = null;
			bool flag = false;
			if (Mod)
			{
				LoadMode = Reader.GetAttribute("Load") ?? LoadMode;
				nameValue = List.Find<NameValue>(attribute);
				if (nameValue != null)
				{
					if (LoadMode != "Merge")
					{
						NameStyles._DefaultTemplateVars.Remove(attribute);
						nameValue = null;
					}
					else
					{
						flag = true;
					}
				}
			}
			else if (List.Has<NameValue>(attribute))
			{
				throw new XmlException("duplicate element", Reader);
			}
			if (nameValue == null)
			{
				nameValue = new NameValue();
				nameValue.Name = attribute;
			}
			if (!flag)
			{
				List.Add(nameValue);
			}
		}

		// Token: 0x06003C9A RID: 15514 RVA: 0x00129ADC File Offset: 0x00127CDC
		public static string Generate(GameObject For = null, string Genotype = null, string Subtype = null, string Species = null, string Culture = null, string Faction = null, string Region = null, string Gender = null, List<string> Mutations = null, string Tag = null, string Special = null, string Type = null, Dictionary<string, string> NamingContext = null, bool FailureOkay = false, bool SpecialFaildown = false, NameStyle Skip = null, List<NameStyle> SkipList = null, int? HyphenationChance = null, int? TwoNameChance = null, bool? HasHonorific = null, bool? HasEpithet = null, bool ForProcessed = false)
		{
			if (!ForProcessed && GameObject.Validate(ref For))
			{
				Genotype = For.GetGenotype();
				Subtype = For.GetSubtype();
				Species = For.GetSpecies();
				Culture = For.GetCulture();
				Faction = For.GetPrimaryFaction(false);
				Region = For.GetNativeRegion();
				Gender = For.GetGender(false).Name;
				Mutations = For.GetMutationNames();
				Tag = For.GetPropertyOrTag("NamingTag", null);
				Region = For.GetPropertyOrTag("NamingRegion", null);
				bool flag = HasHonorific.GetValueOrDefault();
				if (HasHonorific == null)
				{
					flag = For.HasHonorific;
					HasHonorific = new bool?(flag);
				}
				flag = HasEpithet.GetValueOrDefault();
				if (HasEpithet == null)
				{
					flag = For.HasEpithet;
					HasEpithet = new bool?(flag);
				}
			}
			int num;
			string text;
			for (;;)
			{
				List<ValueTuple<NameStyle, NameScope>> list = new List<ValueTuple<NameStyle, NameScope>>();
				bool flag2 = true;
				foreach (NameStyle nameStyle in NameStyles.NameStyleList)
				{
					if (nameStyle != Skip && (SkipList == null || !SkipList.Contains(nameStyle)))
					{
						NameScope nameScope = nameStyle.CheckApply(Genotype, Subtype, Species, Culture, Faction, Region, Gender, Mutations, Tag, Special, Type, HasHonorific.GetValueOrDefault(), HasEpithet.GetValueOrDefault());
						if (nameScope != null)
						{
							if (nameScope.Combine && flag2)
							{
								list.Add(new ValueTuple<NameStyle, NameScope>(nameStyle, nameScope));
							}
							else
							{
								bool flag3 = true;
								foreach (ValueTuple<NameStyle, NameScope> valueTuple in list)
								{
									if ((!nameScope.Combine || !valueTuple.Item2.Combine) && valueTuple.Item2.Priority > nameScope.Priority)
									{
										flag3 = false;
										break;
									}
								}
								if (flag3)
								{
									list.Clear();
									list.Add(new ValueTuple<NameStyle, NameScope>(nameStyle, nameScope));
									flag2 = nameScope.Combine;
								}
							}
						}
					}
				}
				num = list.Count;
				if (num != 0)
				{
					if (num == 1)
					{
						text = list[0].Item1.Generate(For, Genotype, Subtype, Species, Culture, Faction, Region, Gender, Mutations, Tag, Special, Type, NamingContext, FailureOkay, SpecialFaildown, Skip, SkipList, HyphenationChance, TwoNameChance, HasHonorific, HasEpithet);
						if (!text.IsNullOrEmpty())
						{
							break;
						}
					}
					else
					{
						int num2 = 0;
						foreach (ValueTuple<NameStyle, NameScope> valueTuple2 in list)
						{
							if (valueTuple2.Item2.Priority > 0)
							{
								num2 += valueTuple2.Item2.Priority;
							}
						}
						if (num2 > 0)
						{
							int num3 = Stat.Random(0, num2);
							int num4 = 0;
							foreach (ValueTuple<NameStyle, NameScope> valueTuple3 in list)
							{
								if (valueTuple3.Item2.Priority > 0)
								{
									num4 += valueTuple3.Item2.Priority;
									if (num3 < num4)
									{
										string text2 = valueTuple3.Item1.Generate(For, Genotype, Subtype, Species, Culture, Faction, Region, Gender, Mutations, Tag, Special, Type, NamingContext, FailureOkay, SpecialFaildown, Skip, SkipList, HyphenationChance, TwoNameChance, HasHonorific, HasEpithet);
										if (!text2.IsNullOrEmpty())
										{
											return text2;
										}
									}
								}
							}
							foreach (ValueTuple<NameStyle, NameScope> valueTuple4 in list)
							{
								if (valueTuple4.Item2.Priority > 0)
								{
									string text3 = valueTuple4.Item1.Generate(For, Genotype, Subtype, Species, Culture, Faction, Region, Gender, Mutations, Tag, Special, Type, NamingContext, FailureOkay, SpecialFaildown, Skip, SkipList, HyphenationChance, TwoNameChance, HasHonorific, HasEpithet);
									if (!text3.IsNullOrEmpty())
									{
										return text3;
									}
								}
							}
						}
					}
				}
				if (!SpecialFaildown || Special.IsNullOrEmpty())
				{
					goto IL_03CA;
				}
				Special = null;
			}
			return text;
			IL_03CA:
			if (FailureOkay)
			{
				return null;
			}
			string text4 = "NameGenFail";
			num = ++NameStyles.NameGenerationFailures;
			return text4 + num.ToString();
		}

		// Token: 0x04001E4B RID: 7755
		[ModSensitiveStaticCache(false)]
		private static Dictionary<string, NameStyle> _NameStyleTable;

		// Token: 0x04001E4C RID: 7756
		[ModSensitiveStaticCache(false)]
		private static List<NameStyle> _NameStyleList;

		// Token: 0x04001E4D RID: 7757
		[ModSensitiveStaticCache(false)]
		private static Dictionary<string, List<NameValue>> _DefaultTemplateVars;

		// Token: 0x04001E4E RID: 7758
		public static int NameGenerationFailures;
	}
}
