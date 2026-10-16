using System;
using System.Collections.Generic;
using System.Text;
using XRL.CharacterCreation;
using XRL.Language;
using XRL.World;
using XRL.World.Capabilities;
using XRL.World.Parts.Mutation;
using XRL.World.Skills;

namespace XRL
{
	// Token: 0x0200050B RID: 1291
	[Serializable]
	public class SubtypeEntry
	{
		// Token: 0x06003754 RID: 14164 RVA: 0x0010C520 File Offset: 0x0010A720
		public SubtypeClass GetClass()
		{
			SubtypeClass subtypeClass = null;
			SubtypeFactory.ClassesByID.TryGetValue(this.Class, out subtypeClass);
			return subtypeClass;
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06003755 RID: 14165 RVA: 0x0010C543 File Offset: 0x0010A743
		public bool supportsCybernetics
		{
			get
			{
				return this.CyberneticsLicensePoints > 0;
			}
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x0010C54E File Offset: 0x0010A74E
		public int GetStatBonus(string ID)
		{
			if (this.Stats.ContainsKey(ID))
			{
				return this.Stats[ID].Bonus;
			}
			return 0;
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x0010C574 File Offset: 0x0010A774
		public void MergeWith(SubtypeEntry newEntry)
		{
			if (!string.IsNullOrEmpty(newEntry.Name))
			{
				this.Name = newEntry.Name;
			}
			if (!string.IsNullOrEmpty(newEntry.DisplayName))
			{
				this.DisplayName = newEntry.DisplayName;
			}
			if (!string.IsNullOrEmpty(newEntry.Gear))
			{
				this.Gear = newEntry.Gear;
			}
			if (!string.IsNullOrEmpty(newEntry.Class))
			{
				this.Class = newEntry.Class;
			}
			if (!string.IsNullOrEmpty(newEntry.Tile))
			{
				this.Tile = newEntry.Tile;
			}
			if (!string.IsNullOrEmpty(newEntry.BodyObject))
			{
				this.BodyObject = newEntry.BodyObject;
			}
			if (!string.IsNullOrEmpty(newEntry.DetailColor))
			{
				this.DetailColor = newEntry.DetailColor;
			}
			if (!string.IsNullOrEmpty(newEntry.StartingLocation))
			{
				this.StartingLocation = newEntry.StartingLocation;
			}
			if (!string.IsNullOrEmpty(newEntry.Constructor))
			{
				this.Constructor = newEntry.Constructor;
			}
			if (!string.IsNullOrEmpty(newEntry.BaseMPGain))
			{
				this.BaseMPGain = newEntry.BaseMPGain;
			}
			if (!string.IsNullOrEmpty(newEntry.BaseHPGain))
			{
				this.BaseHPGain = newEntry.BaseHPGain;
			}
			if (!string.IsNullOrEmpty(newEntry.BaseSPGain))
			{
				this.BaseSPGain = newEntry.BaseSPGain;
			}
			if (!string.IsNullOrEmpty(newEntry.Species))
			{
				this.Species = newEntry.Species;
			}
			if (newEntry.CyberneticsLicensePoints != -999)
			{
				this.CyberneticsLicensePoints = newEntry.CyberneticsLicensePoints;
			}
			foreach (string text in newEntry.Skills)
			{
				if (!this.Skills.Contains(text))
				{
					this.Skills.Add(text);
				}
			}
			foreach (string text2 in newEntry.RemoveSkills)
			{
				if (!this.RemoveSkills.Contains(text2))
				{
					this.RemoveSkills.Add(text2);
				}
			}
			foreach (SubtypeStat subtypeStat in newEntry.Stats.Values)
			{
				SubtypeStat subtypeStat2;
				if (this.Stats.TryGetValue(subtypeStat.Name, out subtypeStat2))
				{
					subtypeStat2.MergeWith(subtypeStat);
				}
				else
				{
					this.Stats[subtypeStat.Name] = subtypeStat;
				}
			}
			foreach (string text3 in newEntry.ExtraInfo)
			{
				if (!this.ExtraInfo.Contains(text3))
				{
					this.ExtraInfo.Add(text3);
				}
			}
			foreach (string text4 in newEntry.RemoveExtraInfo)
			{
				if (!this.RemoveExtraInfo.Contains(text4))
				{
					this.RemoveExtraInfo.Add(text4);
				}
			}
			foreach (SubtypeReputation subtypeReputation in newEntry.Reputations)
			{
				bool flag = false;
				foreach (SubtypeReputation subtypeReputation2 in this.Reputations)
				{
					if (subtypeReputation2.With == subtypeReputation.With)
					{
						subtypeReputation2.Value += subtypeReputation.Value;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.Reputations.Add(subtypeReputation);
				}
			}
			foreach (SubtypeSaveModifier subtypeSaveModifier in newEntry.SaveModifiers)
			{
				bool flag2 = false;
				foreach (SubtypeSaveModifier subtypeSaveModifier2 in this.SaveModifiers)
				{
					if (subtypeSaveModifier2.Vs == subtypeSaveModifier.Vs)
					{
						subtypeSaveModifier2.Amount += subtypeSaveModifier.Amount;
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					this.SaveModifiers.Add(subtypeSaveModifier);
				}
			}
		}

		// Token: 0x06003758 RID: 14168 RVA: 0x0010CA34 File Offset: 0x0010AC34
		public BaseMutation CreateInstance()
		{
			if (string.IsNullOrEmpty(this.Class))
			{
				return null;
			}
			Type type = ModManager.ResolveType("XRL.CharacterGeneration." + this.Class, false, false, true);
			if (string.IsNullOrEmpty(this.Constructor))
			{
				return (BaseMutation)Activator.CreateInstance(type);
			}
			Type type2 = type;
			object[] array = this.Constructor.Split(',', StringSplitOptions.None);
			return (BaseMutation)Activator.CreateInstance(type2, array);
		}

		// Token: 0x06003759 RID: 14169 RVA: 0x0010CAA0 File Offset: 0x0010ACA0
		public string GetFlatChargenInfo()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in this.GetChargenInfo(null))
			{
				stringBuilder.Append(text);
				stringBuilder.Append('\n');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600375A RID: 14170 RVA: 0x0010CB0C File Offset: 0x0010AD0C
		public List<string> GetChargenInfo(GenotypeEntry genotype = null)
		{
			List<string> list = new List<string>(16);
			if (this.Stats.Count > 0)
			{
				if (this.Stats.Count > 1)
				{
					List<string> list2 = new List<string>(this.Stats.Keys);
					Statistic.SortStatistics(list2);
					using (List<string>.Enumerator enumerator = list2.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							this.ProcessChargenInfoStat(list, text, this.Stats[text]);
						}
						goto IL_00C4;
					}
				}
				foreach (KeyValuePair<string, SubtypeStat> keyValuePair in this.Stats)
				{
					this.ProcessChargenInfoStat(list, keyValuePair.Key, keyValuePair.Value);
				}
			}
			IL_00C4:
			foreach (SubtypeSaveModifier subtypeSaveModifier in this.SaveModifiers)
			{
				if (subtypeSaveModifier.Amount != 0 && subtypeSaveModifier.Amount != -999)
				{
					string saveBonusDescription = SavingThrows.GetSaveBonusDescription(subtypeSaveModifier.Amount, subtypeSaveModifier.Vs, false);
					if (saveBonusDescription != null)
					{
						list.Add("{{c|ù}} " + saveBonusDescription);
					}
				}
			}
			List<string> list3 = new List<string>(this.Skills.Count);
			foreach (string text2 in this.Skills)
			{
				if (!this.RemoveSkills.Contains(text2) && (genotype == null || !genotype.RemoveSkills.Contains(text2)))
				{
					list3.Add(text2);
				}
			}
			List<string> list4 = new List<string>(list3.Count);
			List<string> list5 = new List<string>(list3.Count);
			List<string> list6 = new List<string>(list3.Count);
			List<string> list7 = new List<string>(list3.Count);
			List<string> list8 = new List<string>();
			Dictionary<string, SkillEntry> dictionary = new Dictionary<string, SkillEntry>(list3.Count);
			foreach (string text3 in list3)
			{
				SkillEntry skillEntry;
				PowerEntry powerEntry;
				if (SkillFactory.Factory.SkillByClass.TryGetValue(text3, out skillEntry))
				{
					dictionary[text3] = skillEntry;
					list4.Add(text3);
				}
				else if (SkillFactory.Factory.PowersByClass.TryGetValue(text3, out powerEntry))
				{
					list5.Add(text3);
				}
			}
			foreach (string text4 in list5)
			{
				PowerEntry powerEntry;
				if (SkillFactory.Factory.PowersByClass.TryGetValue(text4, out powerEntry))
				{
					SkillEntry skillEntry;
					if (dictionary.TryGetValue(powerEntry.ParentSkill.Class, out skillEntry))
					{
						if (powerEntry.Cost != 0)
						{
							list6.Add(text4);
						}
					}
					else
					{
						list7.Add(text4);
					}
				}
			}
			foreach (string text5 in list4)
			{
				SkillEntry skillEntry = dictionary[text5];
				list.Add("{{c|ù}} " + skillEntry.Name);
				foreach (string text6 in list6)
				{
					PowerEntry powerEntry;
					if (SkillFactory.Factory.PowersByClass.TryGetValue(text6, out powerEntry) && powerEntry.ParentSkill == skillEntry)
					{
						list.Add("  {{C|ù}} " + powerEntry.Name);
					}
				}
			}
			foreach (string text7 in list7)
			{
				PowerEntry powerEntry;
				if (SkillFactory.Factory.PowersByClass.TryGetValue(text7, out powerEntry))
				{
					string text8 = this.DisambiguateSkill(list8, powerEntry);
					list.Add("{{c|ù}} " + text8);
				}
			}
			if (!string.IsNullOrEmpty(this.Class))
			{
				ICustomChargenClass customChargenClass = Activator.CreateInstance(ModManager.ResolveType("XRL.CharacterCreation." + this.Class, false, false, true)) as ICustomChargenClass;
				if (customChargenClass != null)
				{
					foreach (string text9 in customChargenClass.GetChargenInfo())
					{
						if (!string.IsNullOrEmpty(text9))
						{
							list.Add("{{c|ù}} " + text9);
						}
					}
				}
			}
			foreach (SubtypeReputation subtypeReputation in this.Reputations)
			{
				if (subtypeReputation.Value != 0 && subtypeReputation.Value != -999)
				{
					Faction ifExists = Factions.GetIfExists(subtypeReputation.With);
					if (ifExists != null)
					{
						list.Add("{{c|ù}} " + subtypeReputation.Value.Signed(false) + " reputation with " + ifExists.GetFormattedName());
					}
				}
			}
			foreach (string text10 in this.ExtraInfo)
			{
				if (!this.RemoveExtraInfo.Contains(text10))
				{
					list.Add("{{c|ù}} " + text10);
				}
			}
			return list;
		}

		// Token: 0x0600375B RID: 14171 RVA: 0x0010D0EC File Offset: 0x0010B2EC
		private string DisambiguateSkill(List<string> SearchSkills, PowerEntry powerEntry)
		{
			string text = powerEntry.Name;
			SearchSkills.Clear();
			SearchSkills.Add(text);
			if (text.EndsWith("s"))
			{
				SearchSkills.Add(text.Substring(0, text.Length - 1));
			}
			else
			{
				SearchSkills.Add(Grammar.Pluralize(text));
			}
			foreach (PowerEntry powerEntry2 in SkillFactory.Factory.PowersByClass.Values)
			{
				if (powerEntry2 != powerEntry && SearchSkills.Contains(powerEntry2.Name))
				{
					text = text + " (" + powerEntry.ParentSkill.Name + ")";
					break;
				}
			}
			return text;
		}

		// Token: 0x0600375C RID: 14172 RVA: 0x0010D1B8 File Offset: 0x0010B3B8
		private void ProcessChargenInfoStat(List<string> info, string StatName, SubtypeStat Stat)
		{
			string statDisplayName = Statistic.GetStatDisplayName(StatName);
			if (Stat.Minimum != -999)
			{
				info.Add("{{c|ù}} minimum " + Stat.Minimum.ToString() + " " + statDisplayName);
			}
			if (Stat.Maximum != -999)
			{
				info.Add("{{c|ù}} maximum " + Stat.Minimum.ToString() + " " + statDisplayName);
			}
			if (Stat.Bonus != -999 && Stat.Bonus != 0)
			{
				info.Add("{{c|ù}} " + Stat.Bonus.Signed(false) + " " + statDisplayName);
			}
		}

		// Token: 0x0600375D RID: 14173 RVA: 0x0010D260 File Offset: 0x0010B460
		public void AddSkills(GameObject Object, ICollection<string> Exclude = null)
		{
			foreach (string text in this.Skills)
			{
				if (!this.RemoveSkills.Contains(text) && (Exclude == null || !Exclude.Contains(text)))
				{
					Object.AddSkill(text, null, null);
				}
			}
		}

		// Token: 0x04001BAA RID: 7082
		public SubtypeCategory Category;

		// Token: 0x04001BAB RID: 7083
		public SubtypeClass SubtypeClass;

		// Token: 0x04001BAC RID: 7084
		public string Name;

		// Token: 0x04001BAD RID: 7085
		public string DisplayName;

		// Token: 0x04001BAE RID: 7086
		public int RandomWeight = 1;

		// Token: 0x04001BAF RID: 7087
		public int CyberneticsLicensePoints;

		// Token: 0x04001BB0 RID: 7088
		public string Gear;

		// Token: 0x04001BB1 RID: 7089
		public string Class;

		// Token: 0x04001BB2 RID: 7090
		public string Tile;

		// Token: 0x04001BB3 RID: 7091
		public string DetailColor;

		// Token: 0x04001BB4 RID: 7092
		public string StartingLocation;

		// Token: 0x04001BB5 RID: 7093
		public string BodyObject;

		// Token: 0x04001BB6 RID: 7094
		public string BaseMPGain;

		// Token: 0x04001BB7 RID: 7095
		public string BaseHPGain;

		// Token: 0x04001BB8 RID: 7096
		public string BaseSPGain;

		// Token: 0x04001BB9 RID: 7097
		public string Species;

		// Token: 0x04001BBA RID: 7098
		public string Constructor;

		// Token: 0x04001BBB RID: 7099
		public Dictionary<string, SubtypeStat> Stats = new Dictionary<string, SubtypeStat>();

		// Token: 0x04001BBC RID: 7100
		public List<string> Skills = new List<string>(8);

		// Token: 0x04001BBD RID: 7101
		public List<string> RemoveSkills = new List<string>();

		// Token: 0x04001BBE RID: 7102
		public List<SubtypeReputation> Reputations = new List<SubtypeReputation>();

		// Token: 0x04001BBF RID: 7103
		public List<SubtypeSaveModifier> SaveModifiers = new List<SubtypeSaveModifier>();

		// Token: 0x04001BC0 RID: 7104
		public List<string> ExtraInfo = new List<string>();

		// Token: 0x04001BC1 RID: 7105
		public List<string> RemoveExtraInfo = new List<string>();
	}
}
