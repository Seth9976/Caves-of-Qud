using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using UnityEngine;
using XRL.CharacterBuilds.Qud.UI;
using XRL.CharacterCreation;
using XRL.UI.Framework;
using XRL.World;
using XRL.World.Parts;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x020005AF RID: 1455
	public class QudSubtypeModule : QudEmbarkBuilderModule<QudSubtypeModuleData>
	{
		// Token: 0x06003DF2 RID: 15858 RVA: 0x001303FC File Offset: 0x0012E5FC
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
				QudGenotypeModule module = builder.GetModule<QudGenotypeModule>();
				if (module == null)
				{
					obj = null;
				}
				else
				{
					QudGenotypeModuleData data = module.data;
					obj = ((data != null) ? data.Genotype : null);
				}
			}
			return obj != null;
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x0013042B File Offset: 0x0012E62B
		public override bool shouldBeEditable()
		{
			return this.builder.IsEditableGameMode();
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06003DF4 RID: 15860 RVA: 0x00130438 File Offset: 0x0012E638
		public Dictionary<string, SubtypeEntry> genotypesByName
		{
			get
			{
				return SubtypeFactory.SubtypesByName;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06003DF5 RID: 15861 RVA: 0x0013043F File Offset: 0x0012E63F
		public List<SubtypeEntry> subtypes
		{
			get
			{
				return SubtypeFactory.Subtypes;
			}
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x00130446 File Offset: 0x0012E646
		public void SelectSubtype(string id)
		{
			this.setData(new QudSubtypeModuleData(id));
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x00130454 File Offset: 0x0012E654
		public override void handleModuleDataChange(AbstractEmbarkBuilderModule module, AbstractEmbarkBuilderModuleData oldValues, AbstractEmbarkBuilderModuleData newValues)
		{
			if (module is QudGenotypeModule && base.data != null)
			{
				QudGenotypeModuleData data = this.builder.GetModule<QudGenotypeModule>().data;
				if (((data != null) ? data.Genotype : null) == null)
				{
					this.setData(null);
				}
				else if (!this.GetSelections().Any<ChoiceWithColorIcon>((ChoiceWithColorIcon s) => s.Id == base.data.Subtype))
				{
					this.setData(null);
				}
			}
			base.handleModuleDataChange(module, oldValues, newValues);
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x001304C4 File Offset: 0x0012E6C4
		public override void InitFromSeed(string seed)
		{
			BallBag<SubtypeEntry> ballBag = new BallBag<SubtypeEntry>(base.getModuleRngFromSeed(seed), 0);
			foreach (SubtypeEntry subtypeEntry in SubtypeFactory.Subtypes)
			{
				ballBag.Add(subtypeEntry, subtypeEntry.RandomWeight);
			}
			base.data = new QudSubtypeModuleData
			{
				Subtype = ballBag.PeekOne().Name
			};
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x00130548 File Offset: 0x0012E748
		public override object handleBootEvent(string id, XRLGame game, EmbarkInfo info, object element = null)
		{
			QudSubtypeModuleData data = base.data;
			SubtypeEntry subtypeEntry = ((data != null) ? data.Entry : null);
			if (subtypeEntry == null)
			{
				return base.handleBootEvent(id, game, info, element);
			}
			if (id == QudGameBootModule.BOOTEVENT_BOOTPLAYERTILE)
			{
				return element ?? subtypeEntry.Tile;
			}
			if (id == QudGameBootModule.BOOTEVENT_BOOTPLAYERTILEDETAIL)
			{
				return element ?? subtypeEntry.DetailColor;
			}
			if (id == QudGameBootModule.BOOTEVENT_BOOTPLAYEROBJECT)
			{
				XRL.World.GameObject gameObject = (XRL.World.GameObject)element;
				gameObject.SetStringProperty("Subtype", base.data.Subtype, false);
				if (!subtypeEntry.Gear.IsNullOrEmpty())
				{
					string[] array = subtypeEntry.Gear.Split(',', StringSplitOptions.None);
					int i = 0;
					while (i < array.Length)
					{
						string text = array[i];
						if (PopulationManager.Populations.ContainsKey(text))
						{
							using (List<PopulationResult>.Enumerator enumerator = PopulationManager.Populations[text].Generate(new Dictionary<string, string>(), "").GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									PopulationResult populationResult = enumerator.Current;
									AbstractQudEmbarkBuilderModule.AddItem(populationResult.Blueprint, populationResult.Number, gameObject);
								}
								goto IL_012B;
							}
							goto IL_011A;
						}
						goto IL_011A;
						IL_012B:
						i++;
						continue;
						IL_011A:
						Debug.LogError("Unknown gear population table: " + text);
						goto IL_012B;
					}
					gameObject.CheckStacks();
				}
				QudGenotypeModuleData data2 = info.getData<QudGenotypeModuleData>();
				List<string> list;
				if (data2 == null)
				{
					list = null;
				}
				else
				{
					GenotypeEntry entry = data2.Entry;
					list = ((entry != null) ? entry.RemoveSkills : null);
				}
				List<string> list2 = list;
				subtypeEntry.AddSkills(gameObject, list2);
				if (subtypeEntry.CyberneticsLicensePoints != -999)
				{
					gameObject.ModIntProperty("CyberneticsLicenses", subtypeEntry.CyberneticsLicensePoints, false);
				}
				if (!subtypeEntry.SaveModifiers.Any<SubtypeSaveModifier>((SubtypeSaveModifier x) => x.Amount != 0 && x.Amount != -999))
				{
					goto IL_0334;
				}
				SaveModifiers saveModifiers = gameObject.RequirePart<SaveModifiers>(false);
				saveModifiers.WorksOnSelf = true;
				saveModifiers.WorksOnEquipper = false;
				using (List<SubtypeSaveModifier>.Enumerator enumerator2 = subtypeEntry.SaveModifiers.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						SubtypeSaveModifier subtypeSaveModifier = enumerator2.Current;
						if (subtypeSaveModifier.Amount != 0 && subtypeSaveModifier.Amount != -999)
						{
							saveModifiers.AddModifier(subtypeSaveModifier.Vs, subtypeSaveModifier.Amount);
						}
					}
					goto IL_0334;
				}
			}
			if (id == QudGameBootModule.BOOTEVENT_AFTERBOOTPLAYEROBJECT && !subtypeEntry.Class.IsNullOrEmpty())
			{
				XRL.World.GameObject gameObject2 = (XRL.World.GameObject)element;
				foreach (string text2 in subtypeEntry.Class.Split(',', StringSplitOptions.None))
				{
					if (!text2.IsNullOrEmpty())
					{
						try
						{
							(ModManager.CreateInstance("XRL.CharacterCreation." + text2) as ICustomChargenClass).BuildCharacterBody(gameObject2);
						}
						catch (Exception ex)
						{
							string text3 = "Exception executing builder: ";
							string text4 = text2;
							string text5 = " -> ";
							Exception ex2 = ex;
							Debug.LogError(text3 + text4 + text5 + ((ex2 != null) ? ex2.ToString() : null));
						}
					}
				}
			}
			else if (id == QudGameBootModule.BOOTEVENT_BOOTPLAYERREPUTATION)
			{
				foreach (SubtypeReputation subtypeReputation in subtypeEntry.Reputations)
				{
					Faction.PlayerReputation.Modify(subtypeReputation.With, subtypeReputation.Value, "Subtype", null, null, true, false);
				}
			}
			IL_0334:
			return base.handleBootEvent(id, game, info, element);
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x001308C8 File Offset: 0x0012EAC8
		public override object handleUIEvent(string ID, object Element)
		{
			QudSubtypeModuleData data = base.data;
			SubtypeEntry subtypeEntry = ((data != null) ? data.Entry : null);
			if (subtypeEntry == null)
			{
				return base.handleUIEvent(ID, Element);
			}
			if (ID == QudAttributesModuleWindow.EID_BEFORE_GET_BASE_ATTRIBUTES)
			{
				List<AttributeDataElement> list = Element as List<AttributeDataElement>;
				using (Dictionary<string, SubtypeStat>.Enumerator enumerator = subtypeEntry.Stats.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, SubtypeStat> stat2 = enumerator.Current;
						AttributeDataElement attributeDataElement = ((list != null) ? list.FirstOrDefault<AttributeDataElement>((AttributeDataElement a) => a.Attribute == stat2.Value.Name) : null);
						if (attributeDataElement != null)
						{
							if (stat2.Value.Minimum != -999)
							{
								attributeDataElement.BaseValue = stat2.Value.Minimum;
							}
							if (stat2.Value.Minimum != -999)
							{
								attributeDataElement.Minimum = stat2.Value.Minimum;
							}
							if (stat2.Value.Maximum != -999)
							{
								attributeDataElement.Maximum = stat2.Value.Maximum;
							}
						}
					}
					goto IL_01ED;
				}
			}
			if (ID == QudAttributesModuleWindow.EID_GET_BASE_ATTRIBUTES)
			{
				List<AttributeDataElement> list2 = Element as List<AttributeDataElement>;
				using (Dictionary<string, SubtypeStat>.Enumerator enumerator = subtypeEntry.Stats.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, SubtypeStat> stat = enumerator.Current;
						AttributeDataElement attributeDataElement2 = list2.FirstOrDefault<AttributeDataElement>((AttributeDataElement a) => a.Attribute == stat.Value.Name);
						if (attributeDataElement2 != null && stat.Value.Bonus != -999)
						{
							attributeDataElement2.AddBonus(stat.Value.Bonus, "{{important|" + base.data.Entry.DisplayName + "}} " + this.SubtypeSingularName());
						}
					}
				}
			}
			IL_01ED:
			return base.handleUIEvent(ID, Element);
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x00130AE8 File Offset: 0x0012ECE8
		public string getSelected()
		{
			QudSubtypeModuleData data = base.data;
			if (data == null)
			{
				return null;
			}
			return data.Subtype;
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x00130AFC File Offset: 0x0012ECFC
		public bool hasCategories()
		{
			SubtypeClass subtypeClass;
			return this.TryGetSubtypeClass(out subtypeClass) && subtypeClass.Categories.Count > 1;
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x00130B24 File Offset: 0x0012ED24
		public IEnumerable<SubtypeEntry> getAvailableSelections()
		{
			SubtypeClass subtypeClass;
			if (this.TryGetSubtypeClass(out subtypeClass))
			{
				return subtypeClass.Categories.SelectMany<SubtypeCategory, SubtypeEntry>((SubtypeCategory c) => c.Subtypes);
			}
			return new SubtypeEntry[0];
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x00130B6C File Offset: 0x0012ED6C
		public ChoiceWithColorIcon choiceFromSubtype(SubtypeEntry subtype)
		{
			return new ChoiceWithColorIcon
			{
				Id = subtype.Name,
				Title = subtype.DisplayName,
				IconPath = subtype.Tile,
				HFlip = true,
				IconDetailColor = ConsoleLib.Console.ColorUtility.ColorFromString(subtype.DetailColor),
				IconForegroundColor = The.Color.Gray,
				Description = subtype.GetFlatChargenInfo(),
				Chosen = new Predicate<ChoiceWithColorIcon>(this.IsChoiceSelected)
			};
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x00130BE8 File Offset: 0x0012EDE8
		public string SubtypeSingularName()
		{
			SubtypeClass subtypeClass;
			if (!this.TryGetSubtypeClass(out subtypeClass))
			{
				return "subtype";
			}
			return subtypeClass.SingluarTitle;
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x00130C0B File Offset: 0x0012EE0B
		public bool TryGetSubtypeClass(out SubtypeClass Subtypes)
		{
			QudGenotypeModule module = this.builder.GetModule<QudGenotypeModule>();
			GenotypeEntry genotypeEntry;
			if (module == null)
			{
				genotypeEntry = null;
			}
			else
			{
				QudGenotypeModuleData data = module.data;
				genotypeEntry = ((data != null) ? data.Entry : null);
			}
			return SubtypeFactory.TryGetSubtypeClass(genotypeEntry, out Subtypes);
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x00130C36 File Offset: 0x0012EE36
		public IEnumerable<ChoiceWithColorIcon> GetSelections()
		{
			foreach (SubtypeEntry subtypeEntry in this.getAvailableSelections())
			{
				yield return this.choiceFromSubtype(subtypeEntry);
			}
			IEnumerator<SubtypeEntry> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x00130C46 File Offset: 0x0012EE46
		public IEnumerable<CategoryIcons> GetSelectionCategories()
		{
			SubtypeClass subtypeClass;
			if (!this.TryGetSubtypeClass(out subtypeClass))
			{
				yield break;
			}
			foreach (SubtypeCategory subtypeCategory in subtypeClass.Categories)
			{
				if (subtypeCategory.Subtypes.Count > 0)
				{
					yield return new CategoryIcons
					{
						Title = subtypeCategory.DisplayName,
						Choices = subtypeCategory.Subtypes.Select<SubtypeEntry, ChoiceWithColorIcon>(new Func<SubtypeEntry, ChoiceWithColorIcon>(this.choiceFromSubtype)).ToList<ChoiceWithColorIcon>()
					};
				}
			}
			List<SubtypeCategory>.Enumerator enumerator = default(List<SubtypeCategory>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x00130C56 File Offset: 0x0012EE56
		public bool IsChoiceSelected(ChoiceWithColorIcon choice)
		{
			return this.getSelected() == ((choice != null) ? choice.Id : null);
		}

		// Token: 0x04001EB7 RID: 7863
		[Obsolete]
		public const string BOOTEVENT_BOOTPLAYERTILE = "BootPlayerTile";

		// Token: 0x04001EB8 RID: 7864
		[Obsolete]
		public const string BOOTEVENT_BOOTPLAYERTILEFOREGROUND = "BootPlayerTileForeground";

		// Token: 0x04001EB9 RID: 7865
		[Obsolete]
		public const string BOOTEVENT_BOOTPLAYERTILEBACKGROUND = "BootPlayerTileBackground";

		// Token: 0x04001EBA RID: 7866
		[Obsolete]
		public const string BOOTEVENT_BOOTPLAYERTILEDETAIL = "BootPlayerTileDetail";
	}
}
