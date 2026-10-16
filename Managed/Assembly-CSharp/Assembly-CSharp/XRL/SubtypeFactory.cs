using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace XRL
{
	// Token: 0x0200050E RID: 1294
	[HasModSensitiveStaticCache]
	public static class SubtypeFactory
	{
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06003762 RID: 14178 RVA: 0x0010D3C3 File Offset: 0x0010B5C3
		public static List<SubtypeClass> Classes
		{
			get
			{
				if (SubtypeFactory._Classes == null)
				{
					SubtypeFactory.Init();
				}
				return SubtypeFactory._Classes;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06003763 RID: 14179 RVA: 0x0010D3D6 File Offset: 0x0010B5D6
		public static Dictionary<string, SubtypeClass> ClassesByID
		{
			get
			{
				if (SubtypeFactory._ClassesByID == null)
				{
					SubtypeFactory.Init();
				}
				return SubtypeFactory._ClassesByID;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06003764 RID: 14180 RVA: 0x0010D3E9 File Offset: 0x0010B5E9
		public static List<SubtypeEntry> Subtypes
		{
			get
			{
				if (SubtypeFactory._Subtypes == null)
				{
					SubtypeFactory.Init();
				}
				return SubtypeFactory._Subtypes;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06003765 RID: 14181 RVA: 0x0010D3FC File Offset: 0x0010B5FC
		public static Dictionary<string, SubtypeEntry> SubtypesByName
		{
			get
			{
				if (SubtypeFactory._SubtypesByName == null)
				{
					SubtypeFactory.Init();
				}
				return SubtypeFactory._SubtypesByName;
			}
		}

		// Token: 0x06003766 RID: 14182 RVA: 0x0010D410 File Offset: 0x0010B610
		public static SubtypeEntry GetSubtypeEntry(string Name)
		{
			SubtypeEntry subtypeEntry;
			SubtypeFactory.TryGetSubtypeEntry(Name, out subtypeEntry);
			return subtypeEntry;
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x0010D427 File Offset: 0x0010B627
		public static bool TryGetSubtypeEntry(string Name, out SubtypeEntry Entry)
		{
			if (Name != null && SubtypeFactory.SubtypesByName.TryGetValue(Name, out Entry))
			{
				return true;
			}
			Entry = null;
			return false;
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x0010D440 File Offset: 0x0010B640
		public static SubtypeClass GetSubtypeClass(string ID)
		{
			SubtypeClass subtypeClass;
			SubtypeFactory.TryGetSubtypeClass(ID, out subtypeClass);
			return subtypeClass;
		}

		// Token: 0x06003769 RID: 14185 RVA: 0x0010D458 File Offset: 0x0010B658
		public static SubtypeClass GetSubtypeClass(GenotypeEntry Genotype)
		{
			SubtypeClass subtypeClass;
			SubtypeFactory.TryGetSubtypeClass(Genotype, out subtypeClass);
			return subtypeClass;
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x0010D46F File Offset: 0x0010B66F
		public static bool TryGetSubtypeClass(string ID, out SubtypeClass Class)
		{
			if (ID != null && SubtypeFactory.ClassesByID.TryGetValue(ID, out Class))
			{
				return true;
			}
			Class = null;
			return false;
		}

		// Token: 0x0600376B RID: 14187 RVA: 0x0010D488 File Offset: 0x0010B688
		public static bool TryGetSubtypeClass(GenotypeEntry Genotype, out SubtypeClass Class)
		{
			return SubtypeFactory.TryGetSubtypeClass((Genotype != null) ? Genotype.Subtypes : null, out Class);
		}

		// Token: 0x0600376C RID: 14188 RVA: 0x0010D49C File Offset: 0x0010B69C
		[ModSensitiveCacheInit]
		public static void Init()
		{
			SubtypeFactory._Classes = new List<SubtypeClass>();
			SubtypeFactory._ClassesByID = new Dictionary<string, SubtypeClass>();
			SubtypeFactory._Subtypes = new List<SubtypeEntry>();
			SubtypeFactory._SubtypesByName = new Dictionary<string, SubtypeEntry>();
			foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("Subtypes", true, true))
			{
				try
				{
					xmlDataHelper.WhitespaceHandling = WhitespaceHandling.None;
					while (xmlDataHelper.Read())
					{
						if (xmlDataHelper.Name == "subtypes")
						{
							SubtypeFactory.LoadSubtypesNode(xmlDataHelper, true);
						}
						if (xmlDataHelper.NodeType == XmlNodeType.EndElement && xmlDataHelper.Name == "subtypes")
						{
							break;
						}
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogPotentialModError(xmlDataHelper.modInfo, ex);
				}
			}
		}

		// Token: 0x0600376D RID: 14189 RVA: 0x0010D574 File Offset: 0x0010B774
		public static void LoadSubtypesNode(XmlTextReader Reader, bool bMod = false)
		{
			SubtypeClass subtypeClass = null;
			SubtypeCategory subtypeCategory = null;
			while (Reader.Read())
			{
				if (Reader.Name == "class")
				{
					if (Reader.NodeType == XmlNodeType.Element)
					{
						string attribute = Reader.GetAttribute("ID");
						subtypeClass = null;
						for (int i = 0; i < SubtypeFactory.Classes.Count; i++)
						{
							if (SubtypeFactory.Classes[i].ID == attribute)
							{
								subtypeClass = SubtypeFactory.Classes[i];
								subtypeCategory = SubtypeFactory.Classes[i].Categories[0];
								break;
							}
						}
						if (subtypeClass == null)
						{
							subtypeClass = new SubtypeClass();
							subtypeClass.ID = Reader.GetAttribute("ID");
							SubtypeFactory.Classes.Add(subtypeClass);
							SubtypeFactory.ClassesByID.Add(subtypeClass.ID, subtypeClass);
							subtypeClass.Categories.Add(new SubtypeCategory());
							subtypeCategory = subtypeClass.Categories[0];
						}
						subtypeClass.ChargenTitle = Reader.GetAttribute("ChargenTitle") ?? subtypeClass.ChargenTitle;
						SubtypeClass subtypeClass2 = subtypeClass;
						string text;
						if ((text = Reader.GetAttribute("SingularTitle")) == null)
						{
							text = subtypeClass.SingluarTitle ?? subtypeClass.ChargenTitle;
						}
						subtypeClass2.SingluarTitle = text;
						subtypeClass.StatBoxDisplay = Reader.GetAttribute("StatBoxDisplay") ?? subtypeClass.StatBoxDisplay;
					}
					if (Reader.NodeType == XmlNodeType.EndElement)
					{
						subtypeClass = null;
					}
				}
				else if (Reader.Name == "category")
				{
					if (Reader.NodeType == XmlNodeType.Element)
					{
						string attribute2 = Reader.GetAttribute("Name");
						subtypeCategory = null;
						for (int j = 0; j < subtypeClass.Categories.Count; j++)
						{
							if (subtypeClass.Categories[j].Name == attribute2)
							{
								subtypeCategory = subtypeClass.Categories[j];
								break;
							}
						}
						if (subtypeCategory == null)
						{
							subtypeCategory = new SubtypeCategory();
							subtypeCategory.Name = Reader.GetAttribute("Name") ?? "";
							subtypeCategory.DisplayName = Reader.GetAttribute("DisplayName");
							subtypeClass.Categories.Add(subtypeCategory);
						}
					}
					if (Reader.NodeType == XmlNodeType.EndElement)
					{
						subtypeCategory = subtypeClass.Categories[0];
					}
				}
				else if (Reader.Name == "subtype")
				{
					SubtypeFactory.LoadSubtypeNode(subtypeClass, subtypeCategory, Reader, bMod);
				}
				if (Reader.NodeType == XmlNodeType.EndElement && Reader.Name == "subtypes")
				{
					return;
				}
			}
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x0010D7E0 File Offset: 0x0010B9E0
		public static void LoadSubtypeNode(SubtypeClass currentClass, SubtypeCategory currentCategory, XmlTextReader Reader, bool bMod)
		{
			string text = Reader.GetAttribute("Name");
			if (text[0] == '-')
			{
				text = text.Substring(1);
				if (SubtypeFactory.SubtypesByName.ContainsKey(text))
				{
					SubtypeEntry subtypeEntry = SubtypeFactory.SubtypesByName[text];
					SubtypeFactory.SubtypesByName.Remove(text);
					SubtypeFactory.Subtypes.Remove(subtypeEntry);
					for (int i = 0; i < SubtypeFactory.Classes.Count; i++)
					{
						for (int j = 0; j < SubtypeFactory.Classes[i].Categories.Count; j++)
						{
							if (SubtypeFactory.Classes[i].Categories[j].Subtypes.Contains(subtypeEntry))
							{
								SubtypeFactory.Classes[i].Categories[j].Subtypes.Remove(subtypeEntry);
							}
						}
					}
				}
				return;
			}
			SubtypeEntry subtypeEntry2 = new SubtypeEntry();
			subtypeEntry2.Name = text;
			subtypeEntry2.DisplayName = Reader.GetAttribute("DisplayName");
			if (string.IsNullOrEmpty(subtypeEntry2.DisplayName))
			{
				subtypeEntry2.DisplayName = subtypeEntry2.Name;
			}
			subtypeEntry2.Class = Reader.GetAttribute("Class");
			subtypeEntry2.Gear = Reader.GetAttribute("Gear");
			subtypeEntry2.Tile = Reader.GetAttribute("Tile");
			subtypeEntry2.BodyObject = Reader.GetAttribute("BodyObject");
			subtypeEntry2.DetailColor = Reader.GetAttribute("DetailColor");
			subtypeEntry2.StartingLocation = Reader.GetAttribute("StartingLocation");
			subtypeEntry2.Constructor = Reader.GetAttribute("Constructor");
			subtypeEntry2.BaseMPGain = Reader.GetAttribute("BaseMPGain");
			subtypeEntry2.BaseSPGain = Reader.GetAttribute("BaseSPGain");
			subtypeEntry2.BaseHPGain = Reader.GetAttribute("BaseHPGain");
			subtypeEntry2.Species = Reader.GetAttribute("Species");
			subtypeEntry2.CyberneticsLicensePoints = SubtypeFactory.GetAttributeInt(Reader.GetAttribute("CyberneticsLicensePoints"), -999);
			subtypeEntry2.Constructor = Reader.GetAttribute("Constructor");
			string attribute = Reader.GetAttribute("Skills");
			int k;
			if (!string.IsNullOrEmpty(attribute))
			{
				string baseURI = Reader.BaseURI;
				string text2 = " uses Skills attribute at line ";
				k = Reader.LineNumber;
				Debug.LogWarning(baseURI + text2 + k.ToString() + ", should be ported to skills element");
				string[] array = attribute.Split(',', StringSplitOptions.None);
				for (k = 0; k < array.Length; k++)
				{
					string text3 = CompatManager.ProcessSkill(array[k]);
					if (!subtypeEntry2.Skills.Contains(text3))
					{
						subtypeEntry2.Skills.Add(text3);
					}
				}
			}
			string attribute2 = Reader.GetAttribute("Reputation");
			if (!string.IsNullOrEmpty(attribute2))
			{
				string baseURI2 = Reader.BaseURI;
				string text4 = " uses Reputation attribute at line ";
				k = Reader.LineNumber;
				Debug.LogWarning(baseURI2 + text4 + k.ToString() + ", should be ported to reputations element");
				string[] array = attribute2.Split(',', StringSplitOptions.None);
				for (k = 0; k < array.Length; k++)
				{
					string[] array2 = array[k].Split(':', StringSplitOptions.None);
					if (array2.Length == 2)
					{
						try
						{
							int num = Convert.ToInt32(array2[1]);
							SubtypeReputation subtypeReputation = new SubtypeReputation();
							subtypeReputation.With = array2[0];
							subtypeReputation.Value = num;
							subtypeEntry2.Reputations.Add(subtypeReputation);
						}
						catch
						{
						}
					}
				}
			}
			string attribute3 = Reader.GetAttribute("SaveModifierVs");
			if (string.IsNullOrEmpty(attribute3))
			{
				goto IL_04C4;
			}
			string baseURI3 = Reader.BaseURI;
			string text5 = " uses deprecated SaveModifierVs attribute at line ";
			k = Reader.LineNumber;
			MetricsManager.LogError(baseURI3 + text5 + k.ToString() + ", should be ported to savemodifiers element");
			try
			{
				int attributeInt = SubtypeFactory.GetAttributeInt(Reader.GetAttribute("SaveModifierAmount"), -999);
				SubtypeSaveModifier subtypeSaveModifier = new SubtypeSaveModifier();
				subtypeSaveModifier.Vs = attribute3;
				subtypeSaveModifier.Amount = attributeInt;
				subtypeEntry2.SaveModifiers.Add(subtypeSaveModifier);
				goto IL_04C4;
			}
			catch
			{
				goto IL_04C4;
			}
			IL_03BC:
			if (Reader.Name == "stat")
			{
				SubtypeFactory.LoadStatNode(subtypeEntry2, Reader, bMod);
			}
			else if (Reader.Name == "skills")
			{
				SubtypeFactory.LoadSkillsNode(subtypeEntry2, Reader, bMod);
			}
			else if (Reader.Name == "reputations")
			{
				SubtypeFactory.LoadReputationsNode(subtypeEntry2, Reader, bMod);
			}
			else if (Reader.Name == "savemodifiers")
			{
				SubtypeFactory.LoadSaveModifiersNode(subtypeEntry2, Reader, bMod);
			}
			else if (Reader.Name == "extrainfo")
			{
				SubtypeFactory.LoadExtraInfoNode(subtypeEntry2, Reader, bMod);
			}
			else if (Reader.Name == "removeextrainfo")
			{
				SubtypeFactory.LoadRemoveExtraInfoNode(subtypeEntry2, Reader, bMod);
			}
			else if (Reader.Name == "chargeninfo")
			{
				string baseURI4 = Reader.BaseURI;
				string text6 = " uses chargeninfo element at line ";
				k = Reader.LineNumber;
				Debug.LogWarning(baseURI4 + text6 + k.ToString() + ", should be ported to extrainfo elements");
			}
			if (Reader.NodeType == XmlNodeType.EndElement && Reader.Name == "subtype")
			{
				goto IL_04CF;
			}
			IL_04C4:
			if (Reader.Read())
			{
				goto IL_03BC;
			}
			IL_04CF:
			if (SubtypeFactory.SubtypesByName.ContainsKey(subtypeEntry2.Name))
			{
				SubtypeFactory.SubtypesByName[subtypeEntry2.Name].MergeWith(subtypeEntry2);
				if (currentCategory != null && currentCategory != SubtypeFactory.SubtypesByName[subtypeEntry2.Name].Category)
				{
					SubtypeFactory.SubtypesByName[subtypeEntry2.Name].Category.Subtypes.Remove(SubtypeFactory.SubtypesByName[subtypeEntry2.Name]);
					currentCategory.Subtypes.Add(SubtypeFactory.SubtypesByName[subtypeEntry2.Name]);
					SubtypeFactory.SubtypesByName[subtypeEntry2.Name].Category = currentCategory;
					return;
				}
			}
			else
			{
				SubtypeFactory.SubtypesByName[subtypeEntry2.Name] = subtypeEntry2;
				SubtypeFactory.Subtypes.Add(subtypeEntry2);
				subtypeEntry2.Category = currentCategory;
				subtypeEntry2.SubtypeClass = currentClass;
				currentCategory.Subtypes.Add(subtypeEntry2);
			}
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x0010DDBC File Offset: 0x0010BFBC
		public static int GetAttributeInt(string Attribute, int Default = -999)
		{
			if (string.IsNullOrEmpty(Attribute))
			{
				return Default;
			}
			return int.Parse(Attribute);
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x0010DDD0 File Offset: 0x0010BFD0
		public static void LoadStatNode(SubtypeEntry NewSubtype, XmlTextReader Reader, bool bMod)
		{
			SubtypeStat subtypeStat = new SubtypeStat();
			subtypeStat.Name = Reader.GetAttribute("Name");
			subtypeStat.Minimum = SubtypeFactory.GetAttributeInt(Reader.GetAttribute("Minimum"), -999);
			subtypeStat.Maximum = SubtypeFactory.GetAttributeInt(Reader.GetAttribute("Maximum"), -999);
			subtypeStat.Bonus = SubtypeFactory.GetAttributeInt(Reader.GetAttribute("Bonus"), -999);
			NewSubtype.Stats[subtypeStat.Name] = subtypeStat;
			if (Reader.NodeType != XmlNodeType.EndElement && !Reader.IsEmptyElement)
			{
				while (Reader.Read() && (Reader.NodeType != XmlNodeType.EndElement || (!(Reader.Name == "") && !(Reader.Name == "stat"))))
				{
				}
			}
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x0010DEA4 File Offset: 0x0010C0A4
		public static void LoadSkillsNode(SubtypeEntry NewSubtype, XmlTextReader Reader, bool bMod)
		{
			while (Reader.Read())
			{
				if (Reader.Name == "skill")
				{
					SubtypeFactory.LoadSkillNode(NewSubtype, Reader, bMod);
				}
				else if (Reader.Name == "removeskill")
				{
					SubtypeFactory.LoadRemoveSkillNode(NewSubtype, Reader, bMod);
				}
				if (Reader.NodeType == XmlNodeType.EndElement && Reader.Name == "skills")
				{
					break;
				}
			}
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x0010DF10 File Offset: 0x0010C110
		public static void LoadSkillNode(SubtypeEntry NewSubtype, XmlTextReader Reader, bool bMod)
		{
			string attribute = Reader.GetAttribute("Name");
			CompatManager.ProcessSkill(ref attribute);
			if (!NewSubtype.Skills.Contains(attribute))
			{
				NewSubtype.Skills.Add(attribute);
			}
			if (Reader.NodeType != XmlNodeType.EndElement && !Reader.IsEmptyElement)
			{
				while (Reader.Read() && (Reader.NodeType != XmlNodeType.EndElement || (!(Reader.Name == "") && !(Reader.Name == "skill"))))
				{
				}
			}
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x0010DF94 File Offset: 0x0010C194
		public static void LoadRemoveSkillNode(SubtypeEntry NewSubtype, XmlTextReader Reader, bool bMod)
		{
			string attribute = Reader.GetAttribute("Name");
			CompatManager.ProcessSkill(ref attribute);
			if (!NewSubtype.RemoveSkills.Contains(attribute))
			{
				NewSubtype.RemoveSkills.Add(attribute);
			}
			if (Reader.NodeType != XmlNodeType.EndElement && !Reader.IsEmptyElement)
			{
				while (Reader.Read() && (Reader.NodeType != XmlNodeType.EndElement || (!(Reader.Name == "") && !(Reader.Name == "removeskill"))))
				{
				}
			}
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x0010E018 File Offset: 0x0010C218
		public static void LoadExtraInfoNode(SubtypeEntry NewSubtype, XmlTextReader Reader, bool bMod)
		{
			Reader.Read();
			string value = Reader.Value;
			if (!NewSubtype.ExtraInfo.Contains(value))
			{
				NewSubtype.ExtraInfo.Add(value);
			}
			if (Reader.NodeType != XmlNodeType.EndElement && !Reader.IsEmptyElement)
			{
				while (Reader.Read() && (Reader.NodeType != XmlNodeType.EndElement || !(Reader.Name == "extrainfo")))
				{
				}
			}
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x0010E088 File Offset: 0x0010C288
		public static void LoadRemoveExtraInfoNode(SubtypeEntry NewSubtype, XmlTextReader Reader, bool bMod)
		{
			Reader.Read();
			string value = Reader.Value;
			if (!NewSubtype.RemoveExtraInfo.Contains(value))
			{
				NewSubtype.RemoveExtraInfo.Add(value);
			}
			if (Reader.NodeType != XmlNodeType.EndElement && !Reader.IsEmptyElement)
			{
				while (Reader.Read() && (Reader.NodeType != XmlNodeType.EndElement || !(Reader.Name == "removeextrainfo")))
				{
				}
			}
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x0010E0F8 File Offset: 0x0010C2F8
		public static void LoadReputationsNode(SubtypeEntry NewSubtype, XmlTextReader Reader, bool bMod)
		{
			while (Reader.Read())
			{
				if (Reader.Name == "reputation")
				{
					SubtypeFactory.LoadReputationNode(NewSubtype, Reader, bMod);
				}
				if (Reader.NodeType == XmlNodeType.EndElement && Reader.Name == "reputations")
				{
					break;
				}
			}
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x0010E148 File Offset: 0x0010C348
		public static void LoadReputationNode(SubtypeEntry NewSubtype, XmlTextReader Reader, bool bMod)
		{
			SubtypeReputation subtypeReputation = new SubtypeReputation();
			subtypeReputation.With = Reader.GetAttribute("With");
			subtypeReputation.Value = SubtypeFactory.GetAttributeInt(Reader.GetAttribute("Value"), -999);
			NewSubtype.Reputations.Add(subtypeReputation);
			if (Reader.NodeType != XmlNodeType.EndElement && !Reader.IsEmptyElement)
			{
				while (Reader.Read() && (Reader.NodeType != XmlNodeType.EndElement || (!(Reader.Name == "") && !(Reader.Name == "reputation"))))
				{
				}
			}
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x0010E1E0 File Offset: 0x0010C3E0
		public static void LoadSaveModifiersNode(SubtypeEntry NewSubtype, XmlTextReader Reader, bool bMod)
		{
			while (Reader.Read())
			{
				if (Reader.Name == "savemodifier")
				{
					SubtypeFactory.LoadSaveModifierNode(NewSubtype, Reader, bMod);
				}
				if (Reader.NodeType == XmlNodeType.EndElement && Reader.Name == "savemodifiers")
				{
					break;
				}
			}
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x0010E230 File Offset: 0x0010C430
		public static void LoadSaveModifierNode(SubtypeEntry NewSubtype, XmlTextReader Reader, bool bMod)
		{
			SubtypeSaveModifier subtypeSaveModifier = new SubtypeSaveModifier();
			subtypeSaveModifier.Vs = Reader.GetAttribute("Vs");
			subtypeSaveModifier.Amount = SubtypeFactory.GetAttributeInt(Reader.GetAttribute("Amount"), -999);
			NewSubtype.SaveModifiers.Add(subtypeSaveModifier);
			if (Reader.NodeType != XmlNodeType.EndElement && !Reader.IsEmptyElement)
			{
				while (Reader.Read() && (Reader.NodeType != XmlNodeType.EndElement || (!(Reader.Name == "") && !(Reader.Name == "savemodifier"))))
				{
				}
			}
		}

		// Token: 0x04001BCA RID: 7114
		[ModSensitiveStaticCache(false)]
		private static List<SubtypeClass> _Classes;

		// Token: 0x04001BCB RID: 7115
		[ModSensitiveStaticCache(false)]
		private static Dictionary<string, SubtypeClass> _ClassesByID;

		// Token: 0x04001BCC RID: 7116
		[ModSensitiveStaticCache(false)]
		private static List<SubtypeEntry> _Subtypes;

		// Token: 0x04001BCD RID: 7117
		[ModSensitiveStaticCache(false)]
		private static Dictionary<string, SubtypeEntry> _SubtypesByName;
	}
}
