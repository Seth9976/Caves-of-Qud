using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XRL.CharacterBuilds.Qud.UI;
using XRL.CharacterCreation;
using XRL.World;
using XRL.World.Parts;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x020005A7 RID: 1447
	public class QudGenotypeModule : QudEmbarkBuilderModule<QudGenotypeModuleData>
	{
		// Token: 0x06003DC1 RID: 15809 RVA: 0x0012F1F5 File Offset: 0x0012D3F5
		public override bool shouldBeEnabled()
		{
			EmbarkBuilder builder = this.builder;
			object obj;
			if (builder == null)
			{
				obj = null;
			}
			else
			{
				QudChartypeModule module = builder.GetModule<QudChartypeModule>();
				obj = ((module != null) ? module.data : null);
			}
			return obj != null;
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x0012F218 File Offset: 0x0012D418
		public override bool shouldBeEditable()
		{
			return this.builder.IsEditableGameMode();
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06003DC3 RID: 15811 RVA: 0x0012F225 File Offset: 0x0012D425
		public Dictionary<string, GenotypeEntry> genotypesByName
		{
			get
			{
				return GenotypeFactory.GenotypesByName;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06003DC4 RID: 15812 RVA: 0x0012F22C File Offset: 0x0012D42C
		public List<GenotypeEntry> genotypes
		{
			get
			{
				QudChartypeModule module = this.builder.GetModule<QudChartypeModule>();
				string text;
				if (module == null)
				{
					text = null;
				}
				else
				{
					QudChartypeModuleData data = module.data;
					text = ((data != null) ? data.type : null);
				}
				if (text == "Pregen")
				{
					if (this.PregenGenotypes == null)
					{
						QudGenotypeModule.<>c__DisplayClass6_0 CS$<>8__locals1 = new QudGenotypeModule.<>c__DisplayClass6_0();
						QudGenotypeModule.<>c__DisplayClass6_0 CS$<>8__locals2 = CS$<>8__locals1;
						QudPregenModule module2 = this.builder.GetModule<QudPregenModule>();
						Dictionary<string, QudPregenModule.QudPregenData>.ValueCollection valueCollection;
						if (module2 == null)
						{
							valueCollection = null;
						}
						else
						{
							Dictionary<string, QudPregenModule.QudPregenData> pregens = module2.pregens;
							valueCollection = ((pregens != null) ? pregens.Values : null);
						}
						CS$<>8__locals2.pregens = valueCollection;
						this.PregenGenotypes = ((CS$<>8__locals1.pregens != null) ? GenotypeFactory.Genotypes.Where<GenotypeEntry>((GenotypeEntry g) => CS$<>8__locals1.pregens.Any<QudPregenModule.QudPregenData>((QudPregenModule.QudPregenData p) => p.Genotype == g.Name)).ToList<GenotypeEntry>() : GenotypeFactory.Genotypes);
					}
					return this.PregenGenotypes;
				}
				return GenotypeFactory.Genotypes;
			}
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x0012F2DB File Offset: 0x0012D4DB
		public override void RandomSelectionNoUI()
		{
			this.setData(new QudGenotypeModuleData(this.genotypes.GetRandomElement<GenotypeEntry>(null).Name));
		}

		// Token: 0x06003DC6 RID: 15814 RVA: 0x0012F2F9 File Offset: 0x0012D4F9
		public void SelectGenotype(string id)
		{
			this.setData(new QudGenotypeModuleData(id));
		}

		// Token: 0x06003DC7 RID: 15815 RVA: 0x0012F307 File Offset: 0x0012D507
		public string getSelected()
		{
			QudGenotypeModuleData data = base.data;
			if (data == null)
			{
				return null;
			}
			return data.Genotype;
		}

		// Token: 0x06003DC8 RID: 15816 RVA: 0x0012F31C File Offset: 0x0012D51C
		public override void InitFromSeed(string seed)
		{
			BallBag<GenotypeEntry> ballBag = new BallBag<GenotypeEntry>(base.getModuleRngFromSeed(seed), 0);
			foreach (GenotypeEntry genotypeEntry in this.genotypes)
			{
				ballBag.Add(genotypeEntry, genotypeEntry.RandomWeight);
			}
			QudGenotypeModuleData qudGenotypeModuleData = new QudGenotypeModuleData(ballBag.PeekOne().Name);
			this.setData(qudGenotypeModuleData);
		}

		// Token: 0x06003DC9 RID: 15817 RVA: 0x0012F39C File Offset: 0x0012D59C
		public override object handleUIEvent(string ID, object Element = null)
		{
			QudGenotypeModuleData data = base.data;
			GenotypeEntry genotypeEntry = ((data != null) ? data.Entry : null);
			if (genotypeEntry == null)
			{
				return base.handleUIEvent(ID, Element);
			}
			if (ID == QudAttributesModuleWindow.EID_GET_BASE_AP && genotypeEntry.StatPoints != -999)
			{
				return genotypeEntry.StatPoints;
			}
			if (ID == QudMutationsModuleWindow.EID_GET_BASE_MP && genotypeEntry.MutationPoints != -999)
			{
				return genotypeEntry.MutationPoints;
			}
			if (ID == QudMutationsModuleWindow.EID_GET_CATEGORIES && genotypeEntry.AllowedMutationCategories != null)
			{
				return genotypeEntry.AllowedMutationCategoriesList;
			}
			if (ID == QudAttributesModuleWindow.EID_BEFORE_GET_BASE_ATTRIBUTES)
			{
				List<AttributeDataElement> list = Element as List<AttributeDataElement>;
				using (Dictionary<string, GenotypeStat>.Enumerator enumerator = genotypeEntry.Stats.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, GenotypeStat> keyValuePair = enumerator.Current;
						if (list != null)
						{
							list.Add(new AttributeDataElement
							{
								Attribute = keyValuePair.Key,
								BaseValue = keyValuePair.Value.Minimum,
								Minimum = keyValuePair.Value.Minimum,
								Maximum = keyValuePair.Value.Maximum,
								Description = keyValuePair.Value.ChargenDescription,
								Purchased = 0
							});
						}
					}
					goto IL_0231;
				}
			}
			if (ID == QudAttributesModuleWindow.EID_GET_BASE_ATTRIBUTES)
			{
				List<AttributeDataElement> list2 = Element as List<AttributeDataElement>;
				using (Dictionary<string, GenotypeStat>.Enumerator enumerator = genotypeEntry.Stats.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, GenotypeStat> stat = enumerator.Current;
						AttributeDataElement attributeDataElement = ((list2 != null) ? list2.FirstOrDefault<AttributeDataElement>((AttributeDataElement a) => a.Attribute == stat.Value.Name) : null);
						if (attributeDataElement == null)
						{
							MetricsManager.LogError("Attribute " + stat.Value.Name + " did not exist in base attributes of genotype " + genotypeEntry.Name);
						}
						else if (stat.Value.Bonus != -999)
						{
							attributeDataElement.AddBonus(stat.Value.Bonus, "{{important|" + base.data.Entry.DisplayName + "}} genotype");
						}
					}
				}
			}
			IL_0231:
			return base.handleUIEvent(ID, Element);
		}

		// Token: 0x06003DCA RID: 15818 RVA: 0x0012F600 File Offset: 0x0012D800
		public override object handleBootEvent(string id, XRLGame game, EmbarkInfo info, object element = null)
		{
			GenotypeEntry entry = base.data.Entry;
			if (entry == null)
			{
				return base.handleBootEvent(id, game, info, element);
			}
			if (id == QudGameBootModule.BOOTEVENT_BOOTPLAYEROBJECT)
			{
				XRL.World.GameObject gameObject = (XRL.World.GameObject)element;
				gameObject.SetStringProperty("Genotype", entry.Name, false);
				QudSubtypeModuleData data = info.getData<QudSubtypeModuleData>();
				List<string> list;
				if (data == null)
				{
					list = null;
				}
				else
				{
					SubtypeEntry entry2 = data.Entry;
					list = ((entry2 != null) ? entry2.RemoveSkills : null);
				}
				List<string> list2 = list;
				entry.AddSkills(gameObject, list2);
				if (entry.CyberneticsLicensePoints != -999)
				{
					gameObject.ModIntProperty("CyberneticsLicenses", entry.CyberneticsLicensePoints, false);
				}
				if (entry.SaveModifiers.Any<GenotypeSaveModifier>((GenotypeSaveModifier x) => x.Amount != 0 && x.Amount != -999))
				{
					SaveModifiers saveModifiers = gameObject.RequirePart<SaveModifiers>(false);
					saveModifiers.WorksOnSelf = true;
					saveModifiers.WorksOnEquipper = false;
					foreach (GenotypeSaveModifier genotypeSaveModifier in entry.SaveModifiers)
					{
						if (genotypeSaveModifier.Amount != 0 && genotypeSaveModifier.Amount != -999)
						{
							saveModifiers.AddModifier(genotypeSaveModifier.Vs, genotypeSaveModifier.Amount);
						}
					}
				}
				using (List<GenotypeReputation>.Enumerator enumerator2 = entry.Reputations.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						GenotypeReputation genotypeReputation = enumerator2.Current;
						Faction.PlayerReputation.Modify(genotypeReputation.With, genotypeReputation.Value, "Genotype", null, null, true, false);
					}
					goto IL_0224;
				}
			}
			if (id == QudGameBootModule.BOOTEVENT_AFTERBOOTPLAYEROBJECT && !entry.Class.IsNullOrEmpty())
			{
				XRL.World.GameObject gameObject2 = (XRL.World.GameObject)element;
				foreach (string text in entry.Class.Split(',', StringSplitOptions.None))
				{
					if (!text.IsNullOrEmpty())
					{
						try
						{
							(ModManager.CreateInstance("XRL.CharacterCreation." + text) as ICustomChargenClass).BuildCharacterBody(gameObject2);
						}
						catch (Exception ex)
						{
							string text2 = "Exception executing builder: ";
							string text3 = text;
							string text4 = " -> ";
							Exception ex2 = ex;
							Debug.LogError(text2 + text3 + text4 + ((ex2 != null) ? ex2.ToString() : null));
						}
					}
				}
			}
			IL_0224:
			return base.handleBootEvent(id, game, info, element);
		}

		// Token: 0x04001EAC RID: 7852
		private List<GenotypeEntry> PregenGenotypes;
	}
}
