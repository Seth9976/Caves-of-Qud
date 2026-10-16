using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XRL.World;
using XRL.World.Parts.Mutation;

namespace XRL
{
	// Token: 0x02000505 RID: 1285
	[Serializable]
	public class GenotypeEntry
	{
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x0600372F RID: 14127 RVA: 0x0010B192 File Offset: 0x00109392
		public bool supportsMutations
		{
			get
			{
				return !this.AllowedMutationCategoriesList.IsNullOrEmpty<string>() && this.MutationPoints > 0;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06003730 RID: 14128 RVA: 0x0010B1AC File Offset: 0x001093AC
		public bool supportsCybernetics
		{
			get
			{
				return this.CyberneticsLicensePoints > 0;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06003731 RID: 14129 RVA: 0x0010B1B7 File Offset: 0x001093B7
		// (set) Token: 0x06003732 RID: 14130 RVA: 0x0010B1BF File Offset: 0x001093BF
		public string AllowedMutationCategories
		{
			get
			{
				return this._AllowedMutationCategories;
			}
			set
			{
				this._AllowedMutationCategories = value;
				this._AllowedMutationCategoriesList = null;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06003733 RID: 14131 RVA: 0x0010B1D0 File Offset: 0x001093D0
		public List<string> AllowedMutationCategoriesList
		{
			get
			{
				if (this._AllowedMutationCategoriesList == null)
				{
					this._AllowedMutationCategoriesList = new List<string>();
					if (!this.AllowedMutationCategories.IsNullOrEmpty())
					{
						foreach (string text in this.AllowedMutationCategories.Split(',', StringSplitOptions.None))
						{
							if (text == "*")
							{
								this._AllowedMutationCategoriesList.AddRange(from x in MutationFactory.GetCategories()
									select x.Name);
							}
							else if (text.Length > 1 && text.StartsWith("-"))
							{
								this._AllowedMutationCategoriesList.Remove(text.Substring(1));
							}
							else
							{
								this._AllowedMutationCategoriesList.Add(text);
							}
						}
					}
				}
				return this._AllowedMutationCategoriesList;
			}
		}

		// Token: 0x06003734 RID: 14132 RVA: 0x0010B2AA File Offset: 0x001094AA
		public string GetBodyDisplayName()
		{
			GameObjectBlueprint blueprintIfExists = GameObjectFactory.Factory.GetBlueprintIfExists(this.BodyObject);
			return ((blueprintIfExists != null) ? blueprintIfExists.GetTag("BodyDisplayName", this.BodyObject) : null) ?? this.BodyObject;
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x0010B2E0 File Offset: 0x001094E0
		public void MergeWith(GenotypeEntry newEntry)
		{
			if (!string.IsNullOrEmpty(newEntry.Name))
			{
				this.Name = newEntry.Name;
			}
			if (!string.IsNullOrEmpty(newEntry.DisplayName))
			{
				this.DisplayName = newEntry.DisplayName;
			}
			if (!string.IsNullOrEmpty(newEntry.AllowedMutationCategories))
			{
				this.AllowedMutationCategories = newEntry.AllowedMutationCategories;
			}
			if (!string.IsNullOrEmpty(newEntry.CharacterBuilderModules))
			{
				this.CharacterBuilderModules = newEntry.CharacterBuilderModules;
			}
			if (!string.IsNullOrEmpty(newEntry.BodyTypes))
			{
				this.BodyTypes = newEntry.BodyTypes;
			}
			if (!string.IsNullOrEmpty(newEntry.RestrictedGender))
			{
				this.RestrictedGender = newEntry.RestrictedGender;
			}
			if (!string.IsNullOrEmpty(newEntry.Subtypes))
			{
				this.Subtypes = newEntry.Subtypes;
			}
			if (!string.IsNullOrEmpty(newEntry.Class))
			{
				this.Class = newEntry.Class;
			}
			if (!string.IsNullOrEmpty(newEntry.BodyObject))
			{
				this.BodyObject = newEntry.BodyObject;
			}
			if (!string.IsNullOrEmpty(newEntry.Tile))
			{
				this.Tile = newEntry.Tile;
			}
			if (!string.IsNullOrEmpty(newEntry.BaseHPGain))
			{
				this.BaseHPGain = newEntry.BaseHPGain;
			}
			if (!string.IsNullOrEmpty(newEntry.BaseSPGain))
			{
				this.BaseSPGain = newEntry.BaseSPGain;
			}
			if (!string.IsNullOrEmpty(newEntry.BaseMPGain))
			{
				this.BaseMPGain = newEntry.BaseMPGain;
			}
			if (!string.IsNullOrEmpty(newEntry.Gear))
			{
				this.Gear = newEntry.Gear;
			}
			if (!string.IsNullOrEmpty(newEntry.DetailColor))
			{
				this.DetailColor = newEntry.DetailColor;
			}
			if (!string.IsNullOrEmpty(newEntry.StartingLocation))
			{
				this.StartingLocation = newEntry.StartingLocation;
			}
			if (!string.IsNullOrEmpty(newEntry.Species))
			{
				this.Species = newEntry.Species;
			}
			if (!string.IsNullOrEmpty(newEntry.Constructor))
			{
				this.Constructor = newEntry.Constructor;
			}
			if (newEntry.IsMutant)
			{
				this.IsMutant = true;
			}
			if (newEntry.IsTrueKin)
			{
				this.IsTrueKin = true;
			}
			if (newEntry.MutationPoints != -999)
			{
				this.MutationPoints = newEntry.MutationPoints;
			}
			if (newEntry.CyberneticsLicensePoints != -999)
			{
				this.CyberneticsLicensePoints = newEntry.CyberneticsLicensePoints;
			}
			if (newEntry.StatPoints != -999)
			{
				this.StatPoints = newEntry.StatPoints;
			}
			if (newEntry.RandomWeight != -999)
			{
				this.RandomWeight = newEntry.RandomWeight;
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
			foreach (GenotypeStat genotypeStat in newEntry.Stats.Values)
			{
				GenotypeStat genotypeStat2;
				if (this.Stats.TryGetValue(genotypeStat.Name, out genotypeStat2))
				{
					genotypeStat2.MergeWith(genotypeStat);
				}
				else
				{
					this.Stats[genotypeStat.Name] = genotypeStat;
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
			foreach (GenotypeReputation genotypeReputation in newEntry.Reputations)
			{
				this.Reputations.Add(genotypeReputation);
			}
			foreach (GenotypeSaveModifier genotypeSaveModifier in newEntry.SaveModifiers)
			{
				this.SaveModifiers.Add(genotypeSaveModifier);
			}
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x0010B79C File Offset: 0x0010999C
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

		// Token: 0x06003737 RID: 14135 RVA: 0x0010B808 File Offset: 0x00109A08
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

		// Token: 0x06003738 RID: 14136 RVA: 0x0010B878 File Offset: 0x00109A78
		public List<string> GetChargenInfo()
		{
			List<string> list = new List<string>(this.ExtraInfo.Count - this.RemoveExtraInfo.Count);
			foreach (string text in this.ExtraInfo)
			{
				if (!this.RemoveExtraInfo.Contains(text))
				{
					list.Add("{{c|ù}} " + text);
				}
			}
			return list;
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x0010B904 File Offset: 0x00109B04
		public string GetFlatChargenInfo()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in this.GetChargenInfo())
			{
				stringBuilder.Append(text).Append("\n");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001B7D RID: 7037
		public string Name;

		// Token: 0x04001B7E RID: 7038
		public string DisplayName;

		// Token: 0x04001B7F RID: 7039
		public int MutationPoints;

		// Token: 0x04001B80 RID: 7040
		public int CyberneticsLicensePoints;

		// Token: 0x04001B81 RID: 7041
		public int StatPoints;

		// Token: 0x04001B82 RID: 7042
		public int RandomWeight;

		// Token: 0x04001B83 RID: 7043
		public string CharacterBuilderModules;

		// Token: 0x04001B84 RID: 7044
		public string BodyTypes;

		// Token: 0x04001B85 RID: 7045
		public string RestrictedGender;

		// Token: 0x04001B86 RID: 7046
		public string Subtypes;

		// Token: 0x04001B87 RID: 7047
		public string Class;

		// Token: 0x04001B88 RID: 7048
		public string Tile;

		// Token: 0x04001B89 RID: 7049
		public string Gear;

		// Token: 0x04001B8A RID: 7050
		public string DetailColor;

		// Token: 0x04001B8B RID: 7051
		public string BodyObject;

		// Token: 0x04001B8C RID: 7052
		public string BaseHPGain;

		// Token: 0x04001B8D RID: 7053
		public string BaseSPGain;

		// Token: 0x04001B8E RID: 7054
		public string BaseMPGain;

		// Token: 0x04001B8F RID: 7055
		public string StartingLocation;

		// Token: 0x04001B90 RID: 7056
		public string Species = "human";

		// Token: 0x04001B91 RID: 7057
		public bool IsMutant;

		// Token: 0x04001B92 RID: 7058
		public bool IsTrueKin;

		// Token: 0x04001B93 RID: 7059
		public string _AllowedMutationCategories;

		// Token: 0x04001B94 RID: 7060
		public List<string> _AllowedMutationCategoriesList;

		// Token: 0x04001B95 RID: 7061
		public string Constructor;

		// Token: 0x04001B96 RID: 7062
		public Dictionary<string, GenotypeStat> Stats = new Dictionary<string, GenotypeStat>();

		// Token: 0x04001B97 RID: 7063
		public List<string> Skills = new List<string>();

		// Token: 0x04001B98 RID: 7064
		public List<string> RemoveSkills = new List<string>();

		// Token: 0x04001B99 RID: 7065
		public List<GenotypeReputation> Reputations = new List<GenotypeReputation>();

		// Token: 0x04001B9A RID: 7066
		public List<GenotypeSaveModifier> SaveModifiers = new List<GenotypeSaveModifier>();

		// Token: 0x04001B9B RID: 7067
		public List<string> ExtraInfo = new List<string>();

		// Token: 0x04001B9C RID: 7068
		public List<string> RemoveExtraInfo = new List<string>();
	}
}
