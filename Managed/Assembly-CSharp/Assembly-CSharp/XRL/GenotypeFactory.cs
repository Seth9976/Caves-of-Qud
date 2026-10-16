using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XRL
{
	// Token: 0x02000506 RID: 1286
	[HasModSensitiveStaticCache]
	public static class GenotypeFactory
	{
		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600373B RID: 14139 RVA: 0x0010B9DB File Offset: 0x00109BDB
		public static List<GenotypeEntry> Genotypes
		{
			get
			{
				if (GenotypeFactory._Genotypes == null)
				{
					GenotypeFactory.Init();
				}
				return GenotypeFactory._Genotypes;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600373C RID: 14140 RVA: 0x0010B9EE File Offset: 0x00109BEE
		public static Dictionary<string, GenotypeEntry> GenotypesByName
		{
			get
			{
				if (GenotypeFactory._GenotypesByName == null)
				{
					GenotypeFactory.Init();
				}
				return GenotypeFactory._GenotypesByName;
			}
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x0010BA04 File Offset: 0x00109C04
		public static GenotypeEntry GetGenotypeEntry(string Name)
		{
			GenotypeEntry genotypeEntry;
			GenotypeFactory.TryGetGenotypeEntry(Name, out genotypeEntry);
			return genotypeEntry;
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x0010BA1C File Offset: 0x00109C1C
		public static GenotypeEntry RequireGenotypeEntry(string Name)
		{
			GenotypeEntry genotypeEntry;
			if (!GenotypeFactory.TryGetGenotypeEntry(Name, out genotypeEntry))
			{
				return GenotypeFactory.DefaultEntry;
			}
			return genotypeEntry;
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x0010BA3A File Offset: 0x00109C3A
		public static bool TryGetGenotypeEntry(string Name, out GenotypeEntry Entry)
		{
			if (Name != null && GenotypeFactory.GenotypesByName.TryGetValue(Name, out Entry))
			{
				return true;
			}
			Entry = null;
			return false;
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x0010BA53 File Offset: 0x00109C53
		private static void HandleNodes(XmlDataHelper xml)
		{
			xml.HandleNodes(GenotypeFactory.Nodes, null);
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x0010BA64 File Offset: 0x00109C64
		[ModSensitiveCacheInit]
		public static void Init()
		{
			GenotypeFactory._Genotypes = new List<GenotypeEntry>();
			GenotypeFactory._GenotypesByName = new Dictionary<string, GenotypeEntry>();
			foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("Genotypes", true, true))
			{
				GenotypeFactory.HandleNodes(xmlDataHelper);
			}
			if (!GenotypeFactory._GenotypesByName.TryGetValue("Mutated Human", out GenotypeFactory.DefaultEntry))
			{
				GenotypeFactory.DefaultEntry = GenotypeFactory._Genotypes.FirstOrDefault<GenotypeEntry>();
			}
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x0010BAF0 File Offset: 0x00109CF0
		private static void HandleGenotypeNode(XmlDataHelper Reader)
		{
			GenotypeEntry genotypeEntry = GenotypeFactory.LoadGenotypeNode(Reader, Reader.modInfo != null);
			if (genotypeEntry.Name[0] == '-')
			{
				if (GenotypeFactory._GenotypesByName.ContainsKey(genotypeEntry.Name.Substring(1)))
				{
					GenotypeEntry genotypeEntry2 = GenotypeFactory._GenotypesByName[genotypeEntry.Name.Substring(1)];
					GenotypeFactory._GenotypesByName.Remove(genotypeEntry.Name.Substring(1));
					GenotypeFactory._Genotypes.Remove(genotypeEntry2);
					return;
				}
			}
			else
			{
				if (GenotypeFactory._GenotypesByName.ContainsKey(genotypeEntry.Name))
				{
					GenotypeFactory._GenotypesByName[genotypeEntry.Name].MergeWith(genotypeEntry);
					return;
				}
				GenotypeFactory._GenotypesByName.Add(genotypeEntry.Name, genotypeEntry);
				GenotypeFactory._Genotypes.Add(genotypeEntry);
			}
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x0010BBB8 File Offset: 0x00109DB8
		public static GenotypeEntry LoadGenotypeNode(XmlDataHelper Reader, bool bMod)
		{
			GenotypeEntry NewGenotype = new GenotypeEntry();
			NewGenotype.Name = Reader.GetAttribute("Name");
			NewGenotype.DisplayName = Reader.GetAttribute("DisplayName");
			NewGenotype.AllowedMutationCategories = Reader.GetAttribute("AllowedMutationCategories");
			NewGenotype.CharacterBuilderModules = Reader.GetAttribute("CharacterBuilderModules");
			NewGenotype.BodyTypes = Reader.GetAttribute("BodyTypes");
			NewGenotype.RestrictedGender = Reader.GetAttribute("RestrictedGender");
			NewGenotype.Subtypes = Reader.GetAttribute("Subtypes");
			NewGenotype.Class = Reader.GetAttribute("Class");
			NewGenotype.Tile = Reader.GetAttribute("Tile");
			NewGenotype.Gear = Reader.GetAttribute("Gear");
			NewGenotype.BaseHPGain = Reader.GetAttribute("BaseHPGain");
			NewGenotype.BaseMPGain = Reader.GetAttribute("BaseMPGain");
			NewGenotype.BaseSPGain = Reader.GetAttribute("BaseSPGain");
			NewGenotype.DetailColor = Reader.GetAttribute("DetailColor");
			NewGenotype.BodyObject = Reader.GetAttribute("BodyObject");
			NewGenotype.StartingLocation = Reader.GetAttribute("StartingLocation");
			NewGenotype.Species = Reader.GetAttribute("Species");
			NewGenotype.Constructor = Reader.GetAttribute("Constructor");
			NewGenotype.IsMutant = Reader.GetAttributeBool("IsMutant", false, false);
			NewGenotype.IsTrueKin = Reader.GetAttributeBool("IsTrueKin", false, false);
			NewGenotype.MutationPoints = Reader.GetAttributeInt("MutationPoints", -999, false);
			NewGenotype.CyberneticsLicensePoints = Reader.GetAttributeInt("CyberneticsLicensePoints", -999, false);
			NewGenotype.StatPoints = Reader.GetAttributeInt("StatPoints", -999, false);
			NewGenotype.RandomWeight = Reader.GetAttributeInt("RandomWeight", -999, false);
			string attribute = Reader.GetAttribute("Skills");
			if (!string.IsNullOrEmpty(attribute))
			{
				string baseURI = Reader.BaseURI;
				string text = " uses Skills attribute at line ";
				int i = Reader.LineNumber;
				Debug.LogWarning(baseURI + text + i.ToString() + ", should be ported to skills element");
				string[] array = attribute.Split(',', StringSplitOptions.None);
				for (i = 0; i < array.Length; i++)
				{
					string text2 = CompatManager.ProcessSkill(array[i]);
					if (!NewGenotype.Skills.Contains(text2))
					{
						NewGenotype.Skills.Add(text2);
					}
				}
			}
			string attribute2 = Reader.GetAttribute("Reputation");
			if (!string.IsNullOrEmpty(attribute2))
			{
				string baseURI2 = Reader.BaseURI;
				string text3 = " uses Reputation attribute at line ";
				int i = Reader.LineNumber;
				Debug.LogWarning(baseURI2 + text3 + i.ToString() + ", should be ported to reputations element");
				string[] array = attribute2.Split(',', StringSplitOptions.None);
				for (i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(':', StringSplitOptions.None);
					if (array2.Length == 2)
					{
						try
						{
							int num = Convert.ToInt32(array2[1]);
							GenotypeReputation genotypeReputation = new GenotypeReputation();
							genotypeReputation.With = array2[0];
							genotypeReputation.Value = num;
							NewGenotype.Reputations.Add(genotypeReputation);
						}
						catch
						{
						}
					}
				}
			}
			Dictionary<string, Action<XmlDataHelper>> subNodes = null;
			subNodes = new Dictionary<string, Action<XmlDataHelper>>
			{
				{
					"stat",
					delegate(XmlDataHelper _)
					{
						GenotypeFactory.LoadStatNode(NewGenotype, Reader, bMod);
					}
				},
				{
					"skills",
					delegate(XmlDataHelper _)
					{
						Reader.HandleNodes(subNodes, null);
					}
				},
				{
					"skill",
					delegate(XmlDataHelper _)
					{
						GenotypeFactory.LoadSkillNode(NewGenotype, Reader, bMod);
					}
				},
				{
					"removeskill",
					delegate(XmlDataHelper _)
					{
						GenotypeFactory.LoadRemoveSkillNode(NewGenotype, Reader, bMod);
					}
				},
				{
					"reputations",
					delegate(XmlDataHelper _)
					{
						Reader.HandleNodes(subNodes, null);
					}
				},
				{
					"reputation",
					delegate(XmlDataHelper _)
					{
						GenotypeFactory.LoadReputationNode(NewGenotype, Reader, bMod);
					}
				},
				{
					"savemodifiers",
					delegate(XmlDataHelper _)
					{
						Reader.HandleNodes(subNodes, null);
					}
				},
				{
					"savemodifier",
					delegate(XmlDataHelper _)
					{
						GenotypeFactory.LoadSaveModifierNode(NewGenotype, Reader, bMod);
					}
				},
				{
					"chargeninfo",
					delegate(XmlDataHelper _)
					{
						GenotypeFactory.LoadChargenInfoNode(NewGenotype, Reader, bMod);
					}
				},
				{
					"extrainfo",
					delegate(XmlDataHelper _)
					{
						GenotypeFactory.LoadExtraInfoNode(NewGenotype, Reader, bMod);
					}
				},
				{
					"removeextrainfo",
					delegate(XmlDataHelper _)
					{
						GenotypeFactory.LoadRemoveExtraInfoNode(NewGenotype, Reader, bMod);
					}
				}
			};
			Reader.HandleNodes(subNodes, null);
			return NewGenotype;
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x0010C110 File Offset: 0x0010A310
		public static void LoadStatNode(GenotypeEntry NewGenotype, XmlDataHelper Reader, bool bMod)
		{
			GenotypeStat genotypeStat = new GenotypeStat();
			genotypeStat.Name = Reader.GetAttribute("Name");
			genotypeStat.ChargenDescription = Reader.GetAttribute("ChargenDescription");
			genotypeStat.Minimum = Reader.GetAttributeInt("Minimum", -999, false);
			genotypeStat.Maximum = Reader.GetAttributeInt("Maximum", -999, false);
			genotypeStat.Bonus = Reader.GetAttributeInt("Bonus", -999, false);
			NewGenotype.Stats[genotypeStat.Name] = genotypeStat;
			Reader.DoneWithElement();
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x0010C1A4 File Offset: 0x0010A3A4
		public static void LoadSkillNode(GenotypeEntry NewGenotype, XmlDataHelper Reader, bool bMod)
		{
			string text = Reader.ParseAttribute<string>("Name", null, true, null);
			string newSkill = CompatManager.GetNewSkill(text);
			if (newSkill != null)
			{
				Reader.ParseWarning(string.Concat(new string[] { "skill \"", text, "\" was renamed \"", newSkill, "\"" }));
				text = newSkill;
			}
			if (!NewGenotype.Skills.Contains(text))
			{
				NewGenotype.Skills.Add(text);
			}
			Reader.DoneWithElement();
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x0010C220 File Offset: 0x0010A420
		public static void LoadRemoveSkillNode(GenotypeEntry NewGenotype, XmlDataHelper Reader, bool bMod)
		{
			string attribute = Reader.GetAttribute("Name");
			CompatManager.ProcessSkill(ref attribute);
			if (!NewGenotype.RemoveSkills.Contains(attribute))
			{
				NewGenotype.RemoveSkills.Add(attribute);
			}
			Reader.DoneWithElement();
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x0010C260 File Offset: 0x0010A460
		public static void LoadReputationNode(GenotypeEntry NewGenotype, XmlDataHelper Reader, bool bMod)
		{
			GenotypeReputation genotypeReputation = new GenotypeReputation();
			genotypeReputation.With = Reader.GetAttribute("With");
			genotypeReputation.Value = Reader.GetAttributeInt("Value", -999, false);
			NewGenotype.Reputations.Add(genotypeReputation);
			Reader.DoneWithElement();
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x0010C2B0 File Offset: 0x0010A4B0
		public static void LoadSaveModifierNode(GenotypeEntry NewGenotype, XmlDataHelper Reader, bool bMod)
		{
			GenotypeSaveModifier genotypeSaveModifier = new GenotypeSaveModifier();
			genotypeSaveModifier.Vs = Reader.GetAttribute("Vs");
			genotypeSaveModifier.Amount = Reader.GetAttributeInt("Amount", -999, false);
			NewGenotype.SaveModifiers.Add(genotypeSaveModifier);
			Reader.DoneWithElement();
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x0010C300 File Offset: 0x0010A500
		public static void LoadChargenInfoNode(GenotypeEntry NewGenotype, XmlDataHelper Reader, bool bMod)
		{
			Debug.LogWarning(DataManager.SanitizePathForDisplay(Reader.BaseURI) + " uses chargeninfo element at line " + Reader.LineNumber.ToString() + ", should be ported to extrainfo elements");
			Reader.Read();
			foreach (string text in Reader.ReadString().Split('\n', StringSplitOptions.None))
			{
				NewGenotype.ExtraInfo.AddIfNot(text.Replace("ù", "").Trim(), new Func<string, bool>(string.IsNullOrEmpty));
			}
			Reader.DoneWithElement();
		}

		// Token: 0x0600374A RID: 14154 RVA: 0x0010C394 File Offset: 0x0010A594
		public static void LoadExtraInfoNode(GenotypeEntry NewGenotype, XmlDataHelper Reader, bool bMod)
		{
			Reader.Read();
			NewGenotype.ExtraInfo.AddIfNot(Reader.ReadString(), new Func<string, bool>(NewGenotype.ExtraInfo.Contains));
			Reader.DoneWithElement();
		}

		// Token: 0x0600374B RID: 14155 RVA: 0x0010C3C6 File Offset: 0x0010A5C6
		public static void LoadRemoveExtraInfoNode(GenotypeEntry NewGenotype, XmlDataHelper Reader, bool bMod)
		{
			Reader.Read();
			NewGenotype.RemoveExtraInfo.AddIfNot(Reader.ReadString(), new Func<string, bool>(NewGenotype.RemoveExtraInfo.Contains));
			Reader.DoneWithElement();
		}

		// Token: 0x04001B9D RID: 7069
		[ModSensitiveStaticCache(false)]
		private static List<GenotypeEntry> _Genotypes = null;

		// Token: 0x04001B9E RID: 7070
		[ModSensitiveStaticCache(false)]
		private static Dictionary<string, GenotypeEntry> _GenotypesByName = null;

		// Token: 0x04001B9F RID: 7071
		[ModSensitiveStaticCache(false)]
		private static GenotypeEntry DefaultEntry = null;

		// Token: 0x04001BA0 RID: 7072
		private static Dictionary<string, Action<XmlDataHelper>> Nodes = new Dictionary<string, Action<XmlDataHelper>>
		{
			{
				"genotypes",
				new Action<XmlDataHelper>(GenotypeFactory.HandleNodes)
			},
			{
				"genotype",
				new Action<XmlDataHelper>(GenotypeFactory.HandleGenotypeNode)
			}
		};
	}
}
