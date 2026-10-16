using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using XRL.Language;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	/// <summary>The base class for mutation parts to inherit from.</summary>
	// Token: 0x020013C4 RID: 5060
	[Serializable]
	public class BaseMutation : IPart
	{
		/// <summary>The name shown in the mutations menu and character creation.</summary>
		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x0600C33E RID: 49982 RVA: 0x0047469F File Offset: 0x0047289F
		// (set) Token: 0x0600C33F RID: 49983 RVA: 0x004746A8 File Offset: 0x004728A8
		public string DisplayName
		{
			[Obsolete("Use GetDisplayName()")]
			get
			{
				return this.GetDisplayName(true);
			}
			[Obsolete("All mutations should now have MutationEntries in XML. Set DisplayName on XML node.")]
			set
			{
				this._DisplayName = value;
			}
		}

		// Token: 0x0600C340 RID: 49984 RVA: 0x004746B1 File Offset: 0x004728B1
		protected virtual string GetBaseDisplayName()
		{
			return this.GetMutationEntry().GetDisplayName(false);
		}

		// Token: 0x0600C341 RID: 49985 RVA: 0x004746C0 File Offset: 0x004728C0
		public string GetDisplayName(bool WithAnnotations = true)
		{
			if (!string.IsNullOrEmpty(this._DisplayName))
			{
				return this._DisplayName;
			}
			if (this.UseVariantName)
			{
				this._DisplayName = this.GetVariantName(this.Variant);
			}
			if (string.IsNullOrEmpty(this._DisplayName))
			{
				this._DisplayName = this.GetBaseDisplayName() ?? "";
			}
			if (WithAnnotations && this.IsDefect())
			{
				return this._DisplayName + " ({{r|D}})";
			}
			return this._DisplayName;
		}

		// Token: 0x0600C342 RID: 49986 RVA: 0x0047473F File Offset: 0x0047293F
		public void SetDisplayName(string DisplayName)
		{
			this._DisplayName = DisplayName;
		}

		// Token: 0x0600C343 RID: 49987 RVA: 0x00474748 File Offset: 0x00472948
		public void ResetDisplayName()
		{
			this._DisplayName = null;
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x0600C344 RID: 49988 RVA: 0x00474751 File Offset: 0x00472951
		public virtual int BaseElementWeight
		{
			get
			{
				if (this.BaseLevel > 0)
				{
					return 3 + this.BaseLevel;
				}
				return 0;
			}
		}

		/// <summary>Check whether mutation has more than one available variant.</summary>
		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x0600C345 RID: 49989 RVA: 0x00474768 File Offset: 0x00472968
		public virtual bool HasVariants
		{
			get
			{
				List<string> variants = this.GetVariants();
				return variants != null && variants.Count > 1;
			}
		}

		/// <summary>Allow free selection of variant when gaining mutation.</summary>
		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x0600C346 RID: 49990 RVA: 0x0047478A File Offset: 0x0047298A
		public virtual bool CanSelectVariant
		{
			get
			{
				return true;
			}
		}

		/// <summary>Set the mutation's display name to the picked variant.</summary>
		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x0600C347 RID: 49991 RVA: 0x0047478D File Offset: 0x0047298D
		public virtual bool UseVariantName
		{
			get
			{
				return true;
			}
		}

		/// <summary>Calculate and return the current level with bonuses.</summary>
		// Token: 0x0600C348 RID: 49992 RVA: 0x00474790 File Offset: 0x00472990
		public int CalcLevel()
		{
			return this.CalcLevel(false);
		}

		// Token: 0x0600C349 RID: 49993 RVA: 0x00474799 File Offset: 0x00472999
		public List<BaseMutation.LevelCalculation> GetLevelCalculations()
		{
			this.CalcLevel(true);
			return this._levelCalcWorkspace;
		}

		// Token: 0x0600C34A RID: 49994 RVA: 0x004747A9 File Offset: 0x004729A9
		public virtual int GetUIDisplayLevel()
		{
			return this.Level;
		}

		// Token: 0x0600C34B RID: 49995 RVA: 0x004747B4 File Offset: 0x004729B4
		private int CalcLevel(bool storeWork = false)
		{
			if (storeWork)
			{
				this._levelCalcWorkspace.Clear();
			}
			if (!this.CanLevel() || this.ParentObject == null)
			{
				return this.BaseLevel;
			}
			string text = "mutation";
			int num = this.BaseLevel;
			if (storeWork)
			{
				text = GetMutationTermEvent.GetFor(this.ParentObject, this);
				if (num <= 0)
				{
					List<BaseMutation.LevelCalculation> levelCalcWorkspace = this._levelCalcWorkspace;
					BaseMutation.LevelCalculation levelCalculation = new BaseMutation.LevelCalculation
					{
						bonus = num,
						sigil = '\0',
						temporary = false,
						reason = "* You do not possess this " + text + " inherently, and so you cannot advance its rank."
					};
					levelCalcWorkspace.Add(levelCalculation);
				}
				else
				{
					List<BaseMutation.LevelCalculation> levelCalcWorkspace2 = this._levelCalcWorkspace;
					BaseMutation.LevelCalculation levelCalculation = new BaseMutation.LevelCalculation
					{
						bonus = num,
						sigil = '\0',
						temporary = false,
						reason = string.Concat(new string[]
						{
							"* This ",
							Grammar.MakePossessive(text),
							" base rank is ",
							num.ToString(),
							"."
						})
					};
					levelCalcWorkspace2.Add(levelCalculation);
				}
			}
			MutationEntry mutationEntry = this.GetMutationEntry();
			Statistic statistic = null;
			Dictionary<string, Statistic> statistics = this.ParentObject.Statistics;
			if (statistics != null && statistics.TryGetValue(((mutationEntry != null) ? mutationEntry.GetStat() : null) ?? "", out statistic))
			{
				num += statistic.Modifier;
				if (statistic.Modifier > 0 && storeWork)
				{
					List<BaseMutation.LevelCalculation> levelCalcWorkspace3 = this._levelCalcWorkspace;
					BaseMutation.LevelCalculation levelCalculation = new BaseMutation.LevelCalculation
					{
						bonus = statistic.Modifier,
						sigil = '+',
						temporary = false,
						reason = string.Concat(new string[]
						{
							"+ This ",
							Grammar.MakePossessive(text),
							" rank is increased by ",
							statistic.Modifier.ToString(),
							" due to your high ",
							statistic.Name,
							"."
						})
					};
					levelCalcWorkspace3.Add(levelCalculation);
				}
				if (statistic.Modifier < 0 && storeWork)
				{
					List<BaseMutation.LevelCalculation> levelCalcWorkspace4 = this._levelCalcWorkspace;
					BaseMutation.LevelCalculation levelCalculation = new BaseMutation.LevelCalculation
					{
						bonus = statistic.Modifier,
						sigil = '-',
						temporary = false,
						reason = string.Concat(new string[]
						{
							"- This ",
							Grammar.MakePossessive(text),
							" rank is decreased by ",
							(-statistic.Modifier).ToString(),
							" due to your low ",
							statistic.Name,
							"."
						})
					};
					levelCalcWorkspace4.Add(levelCalculation);
				}
			}
			int num2 = this.ParentObject.GetIntProperty((mutationEntry != null) ? mutationEntry.Class : null, 0);
			num += num2;
			if (num2 != 0)
			{
				MetricsManager.LogWarning("Using old ClassName based IntProperty to adjust mutation level, use RequirePart<Mutations>().AddMutationMod(...):" + this.DebugName);
			}
			num2 = this.ParentObject.GetIntProperty((mutationEntry != null) ? mutationEntry.GetProperty() : null, 0);
			num += num2;
			if (num2 != 0)
			{
				MetricsManager.LogWarning("Using old MutationEntry.Property based IntProperty to adjust mutation level, use RequirePart<Mutations>().AddMutationMod(...):" + this.DebugName);
			}
			num2 = this.ParentObject.GetIntProperty("AllMutationLevelModifier", 0);
			num += num2;
			if (num2 > 0 && storeWork)
			{
				List<BaseMutation.LevelCalculation> levelCalcWorkspace5 = this._levelCalcWorkspace;
				BaseMutation.LevelCalculation levelCalculation = new BaseMutation.LevelCalculation
				{
					temporary = false,
					bonus = num2,
					sigil = '+',
					reason = string.Concat(new string[]
					{
						"+ All your ",
						Grammar.Pluralize(text),
						"' ranks are increased by ",
						num2.ToString(),
						"."
					})
				};
				levelCalcWorkspace5.Add(levelCalculation);
			}
			if (num2 < 0 && storeWork)
			{
				List<BaseMutation.LevelCalculation> levelCalcWorkspace6 = this._levelCalcWorkspace;
				BaseMutation.LevelCalculation levelCalculation = new BaseMutation.LevelCalculation
				{
					temporary = false,
					bonus = num2,
					sigil = '-',
					reason = string.Concat(new string[]
					{
						"- All your ",
						Grammar.Pluralize(text),
						"' ranks are decreased by ",
						(-num2).ToString(),
						"."
					})
				};
				levelCalcWorkspace6.Add(levelCalculation);
			}
			GameObject parentObject = this.ParentObject;
			string text2;
			if (mutationEntry == null)
			{
				text2 = null;
			}
			else
			{
				MutationCategory category = mutationEntry.Category;
				text2 = ((category != null) ? category.CategoryModifierName : null);
			}
			num2 = parentObject.GetIntProperty(text2 ?? "UnknownMutationLevelModifier", 0);
			num += num2;
			if (num2 > 0 && storeWork)
			{
				List<BaseMutation.LevelCalculation> levelCalcWorkspace7 = this._levelCalcWorkspace;
				BaseMutation.LevelCalculation levelCalculation = default(BaseMutation.LevelCalculation);
				levelCalculation.temporary = false;
				levelCalculation.bonus = num2;
				levelCalculation.sigil = '+';
				string[] array = new string[7];
				array[0] = "+ All your ";
				int num3 = 1;
				string text3;
				if (mutationEntry == null)
				{
					text3 = null;
				}
				else
				{
					MutationCategory category2 = mutationEntry.Category;
					text3 = ((category2 != null) ? category2.DisplayName : null);
				}
				array[num3] = text3;
				array[2] = " ";
				array[3] = Grammar.MakePossessive(Grammar.Pluralize(text));
				array[4] = " ranks are increased by ";
				array[5] = num2.ToString();
				array[6] = ".";
				levelCalculation.reason = string.Concat(array);
				levelCalcWorkspace7.Add(levelCalculation);
			}
			if (num2 < 0 && storeWork)
			{
				List<BaseMutation.LevelCalculation> levelCalcWorkspace8 = this._levelCalcWorkspace;
				BaseMutation.LevelCalculation levelCalculation = default(BaseMutation.LevelCalculation);
				levelCalculation.temporary = false;
				levelCalculation.bonus = num2;
				levelCalculation.sigil = '-';
				string[] array2 = new string[7];
				array2[0] = "- All your ";
				int num4 = 1;
				string text4;
				if (mutationEntry == null)
				{
					text4 = null;
				}
				else
				{
					MutationCategory category3 = mutationEntry.Category;
					text4 = ((category3 != null) ? category3.DisplayName : null);
				}
				array2[num4] = text4;
				array2[2] = " ";
				array2[3] = Grammar.MakePossessive(Grammar.Pluralize(text));
				array2[4] = " ranks are decreased by ";
				array2[5] = (-num2).ToString();
				array2[6] = ".";
				levelCalculation.reason = string.Concat(array2);
				levelCalcWorkspace8.Add(levelCalculation);
			}
			if (base.GetType() != typeof(AdrenalControl2) && this.IsPhysical())
			{
				num2 = this.ParentObject.GetIntProperty("AdrenalLevelModifier", 0);
				num += num2;
				if (num2 > 0 && storeWork)
				{
					List<BaseMutation.LevelCalculation> levelCalcWorkspace9 = this._levelCalcWorkspace;
					BaseMutation.LevelCalculation levelCalculation = new BaseMutation.LevelCalculation
					{
						temporary = true,
						bonus = num2,
						sigil = '+',
						reason = string.Concat(new string[]
						{
							"+ This ",
							Grammar.MakePossessive(text),
							" rank is increased by ",
							num2.ToString(),
							" due to your high adrenaline."
						})
					};
					levelCalcWorkspace9.Add(levelCalculation);
				}
			}
			num2 = this.GetRapidLevelAmount();
			num += num2;
			if (num2 > 0 && storeWork)
			{
				List<BaseMutation.LevelCalculation> levelCalcWorkspace10 = this._levelCalcWorkspace;
				BaseMutation.LevelCalculation levelCalculation = new BaseMutation.LevelCalculation
				{
					temporary = false,
					bonus = num2,
					sigil = '+',
					reason = string.Concat(new string[]
					{
						"+ This ",
						Grammar.MakePossessive(text),
						" rank is increased by ",
						num2.ToString(),
						" due to being rapidly advanced ",
						(num2 / 3).ToString(),
						" time",
						(num2 > 3) ? "s" : "",
						"."
					})
				};
				levelCalcWorkspace10.Add(levelCalculation);
			}
			Mutations part = this.ParentObject.GetPart<Mutations>();
			if (part != null)
			{
				for (int i = 0; i < part.MutationMods.Count; i++)
				{
					Mutations.MutationModifierTracker mutationModifierTracker = part.MutationMods[i];
					if (!(mutationModifierTracker.mutationName != base.Name))
					{
						num += mutationModifierTracker.bonus;
						if (storeWork)
						{
							BaseMutation.LevelCalculation levelCalculation;
							switch (mutationModifierTracker.sourceType)
							{
							case Mutations.MutationModifierTracker.SourceType.External:
							{
								List<BaseMutation.LevelCalculation> levelCalcWorkspace11 = this._levelCalcWorkspace;
								levelCalculation = new BaseMutation.LevelCalculation
								{
									temporary = true,
									bonus = mutationModifierTracker.bonus,
									sigil = '+',
									reason = string.Concat(new string[]
									{
										"+ This ",
										Grammar.MakePossessive(text),
										" rank is increased by ",
										mutationModifierTracker.bonus.ToString(),
										" due to ",
										mutationModifierTracker.sourceName,
										"."
									})
								};
								levelCalcWorkspace11.Add(levelCalculation);
								goto IL_09FA;
							}
							case Mutations.MutationModifierTracker.SourceType.Equipment:
							{
								List<BaseMutation.LevelCalculation> levelCalcWorkspace12 = this._levelCalcWorkspace;
								levelCalculation = new BaseMutation.LevelCalculation
								{
									temporary = true,
									bonus = mutationModifierTracker.bonus,
									sigil = '+',
									reason = string.Concat(new string[]
									{
										"+ This ",
										Grammar.MakePossessive(text),
										" rank is increased by ",
										mutationModifierTracker.bonus.ToString(),
										" due to your equipped item, ",
										mutationModifierTracker.sourceName,
										"."
									})
								};
								levelCalcWorkspace12.Add(levelCalculation);
								goto IL_09FA;
							}
							case Mutations.MutationModifierTracker.SourceType.Cooking:
							{
								List<BaseMutation.LevelCalculation> levelCalcWorkspace13 = this._levelCalcWorkspace;
								levelCalculation = new BaseMutation.LevelCalculation
								{
									temporary = true,
									bonus = mutationModifierTracker.bonus,
									sigil = '+',
									reason = string.Concat(new string[]
									{
										"+ This ",
										Grammar.MakePossessive(text),
										" rank is increased by ",
										mutationModifierTracker.bonus.ToString(),
										" due to a metabolizing effect."
									})
								};
								levelCalcWorkspace13.Add(levelCalculation);
								goto IL_09FA;
							}
							case Mutations.MutationModifierTracker.SourceType.Tonic:
							{
								List<BaseMutation.LevelCalculation> levelCalcWorkspace14 = this._levelCalcWorkspace;
								levelCalculation = new BaseMutation.LevelCalculation
								{
									temporary = true,
									bonus = mutationModifierTracker.bonus,
									sigil = '+',
									reason = string.Concat(new string[]
									{
										"+ This ",
										Grammar.MakePossessive(text),
										" rank is increased by ",
										mutationModifierTracker.bonus.ToString(),
										" due to a tonic effect."
									})
								};
								levelCalcWorkspace14.Add(levelCalculation);
								goto IL_09FA;
							}
							}
							List<BaseMutation.LevelCalculation> levelCalcWorkspace15 = this._levelCalcWorkspace;
							levelCalculation = new BaseMutation.LevelCalculation
							{
								temporary = true,
								bonus = mutationModifierTracker.bonus,
								sigil = ((mutationModifierTracker.bonus > 0) ? '+' : '-'),
								reason = string.Concat(new string[]
								{
									"+ This ",
									Grammar.MakePossessive(text),
									" rank is increased by ",
									Math.Abs(mutationModifierTracker.bonus).ToString(),
									" due to your ",
									mutationModifierTracker.sourceName,
									"."
								})
							};
							levelCalcWorkspace15.Add(levelCalculation);
						}
					}
					IL_09FA:;
				}
			}
			if (num < 1)
			{
				if (storeWork)
				{
					List<BaseMutation.LevelCalculation> levelCalcWorkspace16 = this._levelCalcWorkspace;
					BaseMutation.LevelCalculation levelCalculation = new BaseMutation.LevelCalculation
					{
						bonus = 1 - num,
						temporary = false,
						sigil = '+',
						reason = "+ " + ColorUtility.CapitalizeExceptFormatting(text) + " ranks cannot be reduced below 1."
					};
					levelCalcWorkspace16.Add(levelCalculation);
				}
				num = 1;
			}
			int mutationCap = this.GetMutationCap();
			if (mutationCap != -1 && mutationCap < num)
			{
				if (storeWork)
				{
					bool flag = false;
					int num5 = num - mutationCap;
					int num6 = this._levelCalcWorkspace.Where<BaseMutation.LevelCalculation>((BaseMutation.LevelCalculation c) => c.temporary).Aggregate(0, (int a, BaseMutation.LevelCalculation b) => a + b.bonus);
					if (num6 > 0)
					{
						int num7 = Math.Min(num5, num6);
						List<BaseMutation.LevelCalculation> levelCalcWorkspace17 = this._levelCalcWorkspace;
						BaseMutation.LevelCalculation levelCalculation = new BaseMutation.LevelCalculation
						{
							bonus = -num7,
							temporary = true,
							sigil = '-',
							reason = string.Concat(new string[]
							{
								"- This ",
								Grammar.MakePossessive(text),
								" rank is capped at ",
								mutationCap.ToString(),
								" due to your level."
							})
						};
						levelCalcWorkspace17.Add(levelCalculation);
						num5 -= num7;
						flag = true;
					}
					if (num5 > 0)
					{
						List<BaseMutation.LevelCalculation> levelCalcWorkspace18 = this._levelCalcWorkspace;
						BaseMutation.LevelCalculation levelCalculation = new BaseMutation.LevelCalculation
						{
							bonus = -num5,
							temporary = false,
							sigil = (flag ? '\0' : '-'),
							reason = (flag ? null : string.Concat(new string[]
							{
								"- This ",
								Grammar.MakePossessive(text),
								" rank is capped at ",
								mutationCap.ToString(),
								" due to your level."
							}))
						};
						levelCalcWorkspace18.Add(levelCalculation);
					}
				}
				num = mutationCap;
			}
			num2 = this.ParentObject.GetIntProperty((mutationEntry != null) ? mutationEntry.GetCategoryForceProperty() : null, 0);
			num2 += this.ParentObject.GetIntProperty((mutationEntry != null) ? mutationEntry.GetForceProperty() : null, 0);
			return num + num2;
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x0600C34C RID: 49996 RVA: 0x004753EB File Offset: 0x004735EB
		// (set) Token: 0x0600C34D RID: 49997 RVA: 0x004753F3 File Offset: 0x004735F3
		public int Level
		{
			get
			{
				return this.CalcLevel();
			}
			set
			{
				this.BaseLevel = value;
				if (this.ParentObject != null)
				{
					SyncMutationLevelsEvent.Send(this.ParentObject);
				}
			}
		}

		// Token: 0x0600C34E RID: 49998 RVA: 0x00475410 File Offset: 0x00473610
		public override bool WantEvent(int ID, int cascade)
		{
			if (base.WantEvent(ID, cascade) || (ID == PooledEvent<GetPsychicGlimmerEvent>.ID && this.Type == "Mental") || (ID == PooledEvent<IsSensableAsPsychicEvent>.ID && this.Type == "Mental"))
			{
				return true;
			}
			if (ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID && this.ActivatedAbilityID != Guid.Empty)
			{
				Guid activatedAbilityID = this.ActivatedAbilityID;
				return true;
			}
			return false;
		}

		// Token: 0x0600C34F RID: 49999 RVA: 0x00475481 File Offset: 0x00473681
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600C350 RID: 50000 RVA: 0x004754A8 File Offset: 0x004736A8
		public override bool HandleEvent(GetPsychicGlimmerEvent E)
		{
			if (this.Type == "Mental" && E.Subject == this.ParentObject && !this.IsDefect())
			{
				E.Level += this.Level;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C351 RID: 50001 RVA: 0x004754F7 File Offset: 0x004736F7
		public override bool HandleEvent(IsSensableAsPsychicEvent E)
		{
			if (this.Type == "Mental")
			{
				E.Sensable = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C352 RID: 50002 RVA: 0x00475519 File Offset: 0x00473719
		public void SyncLevel()
		{
			Mutations.SyncMutation(this, true);
		}

		// Token: 0x0600C353 RID: 50003 RVA: 0x00475524 File Offset: 0x00473724
		public virtual bool CompatibleWith(GameObject go)
		{
			List<MutationEntry> mutationEntries = MutationFactory.GetMutationEntries(this);
			if (mutationEntries == null)
			{
				return true;
			}
			foreach (MutationEntry mutationEntry in mutationEntries)
			{
				foreach (string text in mutationEntry.GetExclusions())
				{
					if (MutationFactory.HasMutation(text))
					{
						string @class = MutationFactory.GetMutationEntryByName(text).Class;
						if (go.HasPart(@class))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		/// <summary>
		/// For determining how many points of the mutation are "permanent" this will calculate the number of bonus levels
		/// added by Adrenal Control, temporary boosts from food and gear, and bonuses to physical and mental mutations.
		/// </summary>
		/// <returns>Number of levels attributable to a temporary boost.</returns>
		// Token: 0x0600C354 RID: 50004 RVA: 0x004755BC File Offset: 0x004737BC
		public int GetTemporaryLevels()
		{
			return (from v in this.GetLevelCalculations()
				where v.temporary
				select v).Aggregate(0, (int memo, BaseMutation.LevelCalculation value) => memo + value.bonus);
		}

		/// <summary>Is the mutation in the physical category.</summary>
		// Token: 0x0600C355 RID: 50005 RVA: 0x00475618 File Offset: 0x00473818
		public bool IsPhysical()
		{
			MutationEntry mutationEntry = this.GetMutationEntry();
			return mutationEntry != null && mutationEntry.IsPhysical();
		}

		/// <summary>Is the mutation in the mental category.</summary>
		// Token: 0x0600C356 RID: 50006 RVA: 0x0047562B File Offset: 0x0047382B
		public bool IsMental()
		{
			MutationEntry mutationEntry = this.GetMutationEntry();
			return mutationEntry != null && mutationEntry.IsMental();
		}

		/// <summary>Is the mutation in either defect category.</summary>
		// Token: 0x0600C357 RID: 50007 RVA: 0x0047563E File Offset: 0x0047383E
		public bool IsDefect()
		{
			MutationEntry mutationEntry = this.GetMutationEntry();
			return mutationEntry != null && mutationEntry.IsDefect();
		}

		/// <summary>Calculates the maximum level.  Defaults to 10 but will read MaxLevel from the mutations xml also.</summary>
		// Token: 0x0600C358 RID: 50008 RVA: 0x00475654 File Offset: 0x00473854
		public virtual int GetMaxLevel()
		{
			if (this.MaxLevel == -1)
			{
				this.MaxLevel = 10;
				string name = base.GetType().Name;
				foreach (MutationEntry mutationEntry in MutationFactory.AllMutationEntries())
				{
					if (mutationEntry.Class.EqualsNoCase(name) && mutationEntry.MaxLevel > -1)
					{
						this.MaxLevel = mutationEntry.MaxLevel;
						break;
					}
				}
			}
			return this.MaxLevel;
		}

		/// <summary>Checks the mutation is levelable, base level is under max level, and total level is less than the current mutation cap</summary>
		// Token: 0x0600C359 RID: 50009 RVA: 0x004756E4 File Offset: 0x004738E4
		public bool CanIncreaseLevel()
		{
			return this.CanLevel() && this.BaseLevel < this.GetMaxLevel() && this.Level < this.GetMutationCap();
		}

		/// <summary>Example "Physical" or "MentalDefects" defaults to Mutation.xml category name</summary>
		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x0600C35A RID: 50010 RVA: 0x0047570C File Offset: 0x0047390C
		// (set) Token: 0x0600C35B RID: 50011 RVA: 0x00475714 File Offset: 0x00473914
		public string Type
		{
			get
			{
				return this.GetMutationType();
			}
			[Obsolete("Should not need to set Type. Defaults to mutation Type in Mutations.xml, or the mutation category's Name.")]
			set
			{
				this._Type = value;
			}
		}

		/// <summary>The <see cref="F:XRL.MutationEntry.Type"><c>Type</c></see> of the mutation (from mutation entry or setting of <c><see cref="P:XRL.World.Parts.Mutation.BaseMutation.Type">Type</see></c>)</summary>
		// Token: 0x0600C35C RID: 50012 RVA: 0x00475720 File Offset: 0x00473920
		public virtual string GetMutationType()
		{
			string text;
			if ((text = this._Type) == null)
			{
				MutationEntry mutationEntry = this.GetMutationEntry();
				text = (this._Type = ((mutationEntry != null) ? mutationEntry.Type : null) ?? "Physical");
			}
			return text;
		}

		/// <summary>Is this mutation in the named <paramref name="category" /> in the Mutations.xml</summary>
		// Token: 0x0600C35D RID: 50013 RVA: 0x0047575B File Offset: 0x0047395B
		public bool isCategory(string category)
		{
			MutationEntry mutationEntry = this.GetMutationEntry();
			string text;
			if (mutationEntry == null)
			{
				text = null;
			}
			else
			{
				MutationCategory category2 = mutationEntry.Category;
				text = ((category2 != null) ? category2.Name : null);
			}
			return text == category;
		}

		/// <summary>The <see cref="T:XRL.MutationEntry">MutationEntry</see> in <see cref="T:XRL.MutationFactory">MutationFactory</see></summary>
		// Token: 0x0600C35E RID: 50014 RVA: 0x00475784 File Offset: 0x00473984
		public MutationEntry GetMutationEntry()
		{
			if (this._Entry != null)
			{
				return this._Entry;
			}
			List<MutationEntry> mutationEntries = MutationFactory.GetMutationEntries(this);
			if (mutationEntries.IsNullOrEmpty<MutationEntry>())
			{
				string text = (string.IsNullOrEmpty(this._DisplayName) ? "..." : this._DisplayName);
				MetricsManager.LogError(string.Concat(new string[]
				{
					"Mutation entry not found for '",
					base.GetType().Name,
					"'. Please add `<mutation Name=\"",
					text,
					"\" Class=\"",
					base.GetType().Name,
					"\" />` node to a mutations xml file."
				}));
				MutationEntry mutationEntry = (this._Entry = MutationFactory.CreateMutationEntryForMutation(this));
				return mutationEntry;
			}
			if (mutationEntries.Count == 1)
			{
				MutationEntry mutationEntry = (this._Entry = mutationEntries[0]);
				return mutationEntry;
			}
			string variant = this.Variant;
			if (variant == null)
			{
				using (List<MutationEntry>.Enumerator enumerator = mutationEntries.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MutationEntry mutationEntry2 = enumerator.Current;
						if (mutationEntry2.Variant == null && !mutationEntry2.HasVariants)
						{
							MutationEntry mutationEntry = (this._Entry = mutationEntry2);
							return mutationEntry;
						}
					}
					goto IL_016F;
				}
			}
			foreach (MutationEntry mutationEntry3 in mutationEntries)
			{
				if (mutationEntry3.Variant == variant || (mutationEntry3.HasVariants && mutationEntry3.GetVariants().Contains(variant)))
				{
					return this._Entry = mutationEntry3;
				}
			}
			IL_016F:
			return this._Entry = mutationEntries[0];
		}

		/// <summary>The <see cref="!:MutationEntry.Class">Class</see> from the mutation entry.</summary>
		// Token: 0x0600C35F RID: 50015 RVA: 0x00475930 File Offset: 0x00473B30
		public string GetMutationClass()
		{
			MutationEntry mutationEntry = this.GetMutationEntry();
			if (mutationEntry == null)
			{
				return null;
			}
			return mutationEntry.Class;
		}

		/// <summary>The <see cref="!:MutationEntry.Snippet">Snippet</see> from the mutation entry.</summary>
		// Token: 0x0600C360 RID: 50016 RVA: 0x00475943 File Offset: 0x00473B43
		public string GetBearerDescription()
		{
			MutationEntry mutationEntry = this.GetMutationEntry();
			return ((mutationEntry != null) ? mutationEntry.Snippet : null) ?? "";
		}

		/// <summary>Shortcut helper for calling <see cref="M:XRL.World.GameObject.UseEnergy(System.Int32,System.String,System.String,System.Nullable{System.Int32},System.Boolean)">ParentObject.UseEnergy</see>.</summary>
		// Token: 0x0600C361 RID: 50017 RVA: 0x00475960 File Offset: 0x00473B60
		public void UseEnergy(int Amount)
		{
			if (this.EnergyUseType == null)
			{
				this.EnergyUseType = this.Type + " Mutation";
			}
			this.ParentObject.UseEnergy(Amount, this.EnergyUseType, null, null, false);
		}

		/// <summary>Shortcut helper for calling <see cref="M:XRL.World.GameObject.UseEnergy(System.Int32,System.String,System.String,System.Nullable{System.Int32},System.Boolean)">ParentObject.UseEnergy</see>.</summary>
		// Token: 0x0600C362 RID: 50018 RVA: 0x004759AC File Offset: 0x00473BAC
		public void UseEnergy(int Amount, string Type)
		{
			this.ParentObject.UseEnergy(Amount, Type, null, null, false);
		}

		// Token: 0x0600C363 RID: 50019 RVA: 0x004759D2 File Offset: 0x00473BD2
		public static int GetMutationCapForLevel(int level)
		{
			return level / 2 + 1;
		}

		/// <summary>Get the current cap for this mutation on this object.  Based on Level.  -1 means uncapped.</summary>
		// Token: 0x0600C364 RID: 50020 RVA: 0x004759DC File Offset: 0x00473BDC
		public virtual int GetMutationCap()
		{
			GameObject parentObject = this.ParentObject;
			if (parentObject != null && parentObject.HasStat("Level"))
			{
				return Math.Max(this.CapOverride, BaseMutation.GetMutationCapForLevel(this.ParentObject.Stat("Level", 0)));
			}
			return this.CapOverride;
		}

		/// <summary>The <see cref="F:XRL.MutationEntry.Stat">Stat</see> from the mutation entry, or its category.</summary>
		// Token: 0x0600C365 RID: 50021 RVA: 0x00475A2C File Offset: 0x00473C2C
		public string GetStat()
		{
			MutationEntry mutationEntry = this.GetMutationEntry();
			if (mutationEntry != null)
			{
				if (string.IsNullOrEmpty(mutationEntry.Stat) && mutationEntry.Category != null && !string.IsNullOrEmpty(mutationEntry.Category.Stat))
				{
					mutationEntry.Stat = mutationEntry.Category.Stat;
				}
				return mutationEntry.Stat;
			}
			return null;
		}

		/// <summary>Should the level of the mutation be shown in the character sheet. Defaults to <see cref="M:XRL.World.Parts.Mutation.BaseMutation.CanLevel" /></summary>
		// Token: 0x0600C366 RID: 50022 RVA: 0x00475A83 File Offset: 0x00473C83
		public virtual bool ShouldShowLevel()
		{
			return this.CanLevel();
		}

		/// <summary>Can the player spend points in the mutation.  Defaults to true, override to disable.</summary>
		// Token: 0x0600C367 RID: 50023 RVA: 0x00475A8B File Offset: 0x00473C8B
		public virtual bool CanLevel()
		{
			return true;
		}

		/// <summary>Should we remutate the body when it changes (basically only to slog 💋)</summary>
		// Token: 0x0600C368 RID: 50024 RVA: 0x00475A8E File Offset: 0x00473C8E
		public virtual bool AffectsBodyParts()
		{
			return false;
		}

		/// <summary>Should we remutate the body when it changes (basically only to slog 💋)</summary>
		// Token: 0x0600C369 RID: 50025 RVA: 0x00475A91 File Offset: 0x00473C91
		public virtual bool GeneratesEquipment()
		{
			return false;
		}

		// Token: 0x0600C36A RID: 50026 RVA: 0x00475A94 File Offset: 0x00473C94
		public virtual string GetCreateCharacterDisplayName()
		{
			return this.GetDisplayName(true);
		}

		/// <summary>
		///     CollectStats using the current <see cref="P:XRL.World.Parts.Mutation.BaseMutation.Level" /> 
		/// </summary>
		/// <remarks>
		///     If overriding on a Mutation, you should override the version that is passed a Level
		/// </remarks>
		// Token: 0x0600C36B RID: 50027 RVA: 0x00475A9D File Offset: 0x00473C9D
		public virtual void CollectStats(Templates.StatCollector stats)
		{
			this.CollectStats(stats, this.Level);
		}

		/// <summary>
		///     CollectStats using <see cref="P:XRL.World.Parts.Mutation.BaseMutation.Level" /> 
		/// </summary>
		// Token: 0x0600C36C RID: 50028 RVA: 0x00475AAC File Offset: 0x00473CAC
		public virtual void CollectStats(Templates.StatCollector stats, int Level)
		{
		}

		/// <summary>The long description of the mutations powers.  Effects that change based on level should not be in this description method.</summary>
		// Token: 0x0600C36D RID: 50029 RVA: 0x00475AB0 File Offset: 0x00473CB0
		public virtual string GetDescription()
		{
			Templates.Template template;
			if (Templates.TemplateByID.TryGetValue("Mutation." + this.GetMutationClass() + ".Description", out template))
			{
				return template.Build(new Action<Templates.StatCollector>(this.CollectStats), "mutation description");
			}
			return "<description>";
		}

		// Token: 0x0600C36E RID: 50030 RVA: 0x00475B00 File Offset: 0x00473D00
		public virtual string GetLevelText(int Level)
		{
			Templates.Template template;
			if (Templates.TemplateByID.TryGetValue("Mutation." + this.GetMutationClass() + ".LevelText", out template))
			{
				return template.Build(delegate(Templates.StatCollector stats)
				{
					this.CollectStats(stats, Level);
				}, "mutation leveltext").Trim('\n');
			}
			return "<Does level " + Level.ToString() + " stuff>";
		}

		/// <summary>
		///   Add the mutation to the <paramref name="GO">game object</paramref>.
		///   Base method calls <see cref="M:XRL.World.Parts.Mutation.BaseMutation.ChangeLevel(System.Int32)">ChangeLevel</see>.
		///   Return false to cancel adding mutation.
		/// </summary>
		// Token: 0x0600C36F RID: 50031 RVA: 0x00475B7D File Offset: 0x00473D7D
		public virtual bool Mutate(GameObject GO, int Level = 1)
		{
			this.BaseLevel = Level;
			this.ChangeLevel(this.Level);
			return true;
		}

		/// <summary>Additional hook for after mutation is added.  <see cref="!:ParentObject" /> will be the new mutant.</summary>
		// Token: 0x0600C370 RID: 50032 RVA: 0x00475B94 File Offset: 0x00473D94
		public virtual void AfterMutate()
		{
		}

		/// <summary>Removing the mutation from <paramref name="GO" />.</summary>
		// Token: 0x0600C371 RID: 50033 RVA: 0x00475B96 File Offset: 0x00473D96
		public virtual bool Unmutate(GameObject GO)
		{
			this.LastLevel = 0;
			return true;
		}

		/// <summary>Hook for after mutation has been removed.  Note <see cref="!:ParentObject">ParentObject</see> will probably be null, so the old GameObject is passed in.</summary>
		// Token: 0x0600C372 RID: 50034 RVA: 0x00475BA0 File Offset: 0x00473DA0
		public virtual void AfterUnmutate(GameObject GO)
		{
		}

		/// <summary>Number of levels from rapid mutation.</summary>
		// Token: 0x0600C373 RID: 50035 RVA: 0x00475BA2 File Offset: 0x00473DA2
		public int GetRapidLevelAmount()
		{
			if (this._RapidKey == null)
			{
				this._RapidKey = "RapidLevel_" + base.Name;
			}
			return this.ParentObject.GetIntProperty(this._RapidKey, 0);
		}

		/// <summary>Set levels from rapid mutation and if enabled, Sync glimmer.</summary>
		// Token: 0x0600C374 RID: 50036 RVA: 0x00475BD4 File Offset: 0x00473DD4
		public void SetRapidLevelAmount(int Amount, bool Sync = true)
		{
			if (this._RapidKey == null)
			{
				this._RapidKey = "RapidLevel_" + base.Name;
			}
			this.ParentObject.SetIntProperty(this._RapidKey, Amount, true);
			this.ChangeLevel(this.Level);
			if (Sync)
			{
				this.ParentObject.SyncMutationLevelAndGlimmer();
			}
		}

		/// <summary>Increase levels from rapid mutation and if enabled, Sync glimmer.</summary>
		// Token: 0x0600C375 RID: 50037 RVA: 0x00475C30 File Offset: 0x00473E30
		public virtual void RapidLevel(int Amount, bool Sync = true)
		{
			if (this._RapidKey == null)
			{
				this._RapidKey = "RapidLevel_" + base.Name;
			}
			this.ParentObject.ModIntProperty(this._RapidKey, Amount, true);
			this.ChangeLevel(this.Level);
			if (Sync)
			{
				this.ParentObject.SyncMutationLevelAndGlimmer();
			}
		}

		/// <summary>This does not set the level, but is a hook to add logic for changing levels.  "LevelChanged" so to speak.  Base method sets <see cref="F:XRL.World.Parts.Mutation.BaseMutation.LastLevel" />=<paramref name="NewLevel" />.</summary>
		// Token: 0x0600C376 RID: 50038 RVA: 0x00475C8C File Offset: 0x00473E8C
		public virtual bool ChangeLevel(int NewLevel)
		{
			if (NewLevel >= 15 && this.LastLevel < 15 && this.ParentObject != null && this.ParentObject.IsPlayer() && !this.ParentObject.HasEffect<Dominated>())
			{
				Achievement.GET_MUTATION_LEVEL_15.Unlock();
			}
			this.LastLevel = NewLevel;
			return true;
		}

		/// <summary>Use to remove a equipment created by the mutation from the game.</summary>
		/// <param name="who">The mutant (ParentObject or what it used to be).</param>
		/// <param name="obj">The <c>ref</c> to the GameObject you are cleaning up - we will set it to null for you.</param>
		// Token: 0x0600C377 RID: 50039 RVA: 0x00475CDC File Offset: 0x00473EDC
		protected void CleanUpMutationEquipment(GameObject who, ref GameObject obj)
		{
			if (obj != null)
			{
				if (who != null)
				{
					BodyPart bodyPart = obj.EquippedOn();
					if (bodyPart != null)
					{
						who.FireEvent(Event.New("CommandForceUnequipObject", "BodyPart", bodyPart).SetSilent(true));
					}
					else
					{
						Body body = who.Body;
						bodyPart = ((body != null) ? body.FindDefaultOrEquippedItem(obj) : null);
						if (bodyPart != null && bodyPart.DefaultBehavior == obj)
						{
							bodyPart.DefaultBehavior = null;
						}
					}
				}
				obj.Obliterate(null, false, null);
				obj = null;
			}
		}

		/// <summary>Fetches a static list of all possible variants.</summary>
		// Token: 0x0600C378 RID: 50040 RVA: 0x00475D51 File Offset: 0x00473F51
		public virtual List<string> GetVariants()
		{
			return Mutations.GetVariants(base.Name);
		}

		/// <summary>Create a list of all possible variants for <see cref="M:XRL.World.Parts.Mutations.GetVariants(System.String)" /> using the "MutationEquipment" tag by default.</summary>
		/// <remarks>Invoked on stateless instance, result cached.</remarks>
		// Token: 0x0600C379 RID: 50041 RVA: 0x00475D60 File Offset: 0x00473F60
		public virtual List<string> CreateVariants()
		{
			List<string> list = new List<string>();
			string name = base.Name;
			foreach (GameObjectBlueprint gameObjectBlueprint in Mutations.GetAllMutationEquipment())
			{
				string text;
				if (gameObjectBlueprint.Tags.TryGetValue("MutationEquipment", out text) && text == name && !gameObjectBlueprint.IsBaseBlueprint())
				{
					list.Add(gameObjectBlueprint.Name);
				}
			}
			return list;
		}

		/// <summary>Get the display name of the current variant.</summary>
		// Token: 0x0600C37A RID: 50042 RVA: 0x00475DEC File Offset: 0x00473FEC
		public virtual string GetVariantName()
		{
			return this.GetVariantName(this.Variant);
		}

		/// <summary>Get the display name of a variant's blueprint.</summary>
		// Token: 0x0600C37B RID: 50043 RVA: 0x00475DFA File Offset: 0x00473FFA
		public virtual string GetVariantName(string Blueprint)
		{
			return this.GetVariantName(GameObjectFactory.Factory.GetBlueprintIfExists(Blueprint));
		}

		/// <summary>Get the display name of a variant's blueprint.</summary>
		// Token: 0x0600C37C RID: 50044 RVA: 0x00475E10 File Offset: 0x00474010
		public virtual string GetVariantName(GameObjectBlueprint Blueprint)
		{
			if (Blueprint == null)
			{
				return null;
			}
			string text;
			if (!Blueprint.Tags.TryGetValue("VariantName", out text))
			{
				return Blueprint.CachedDisplayNameStrippedTitleCase;
			}
			return text;
		}

		/// <summary>Choose variant #<paramref name="Index" /> from the <see cref="M:XRL.World.Parts.Mutation.BaseMutation.GetVariants" /> list.</summary>
		// Token: 0x0600C37D RID: 50045 RVA: 0x00475E3E File Offset: 0x0047403E
		public virtual void SetVariant(string Variant)
		{
			this.Variant = Variant;
			if (this.UseVariantName)
			{
				this.ResetDisplayName();
			}
		}

		/// <summary>Get a renderable icon for a specific variant.</summary>
		// Token: 0x0600C37E RID: 50046 RVA: 0x00475E58 File Offset: 0x00474058
		public virtual IRenderable GetIcon(string Variant)
		{
			GameObjectBlueprint blueprintIfExists = GameObjectFactory.Factory.GetBlueprintIfExists(Variant);
			if (blueprintIfExists != null)
			{
				return new Renderable(blueprintIfExists);
			}
			return null;
		}

		/// <summary>Get a renderable icon for this mutation.</summary>
		// Token: 0x0600C37F RID: 50047 RVA: 0x00475E7C File Offset: 0x0047407C
		public virtual IRenderable GetIcon()
		{
			if (this.Variant != null)
			{
				return this.GetIcon(this.Variant);
			}
			MutationEntry mutationEntry;
			if (MutationFactory.TryGetMutationEntry(this, out mutationEntry))
			{
				return mutationEntry.GetRenderable();
			}
			return null;
		}

		// Token: 0x0600C380 RID: 50048 RVA: 0x00475EB0 File Offset: 0x004740B0
		public virtual bool TryGetVariantValidity(GameObject Object, string Variant, out string Message)
		{
			Message = null;
			return true;
		}

		/// <summary>Display an option list of variants and set the selected variant to this instance.</summary>
		// Token: 0x0600C381 RID: 50049 RVA: 0x00475EB8 File Offset: 0x004740B8
		public bool SelectVariant(GameObject Object, bool AllowEscape = true)
		{
			List<string> variants = this.GetVariants();
			if (variants.IsNullOrEmpty<string>())
			{
				return false;
			}
			string[] array = new string[variants.Count];
			IRenderable[] array2 = new IRenderable[variants.Count];
			bool[] array3 = new bool[variants.Count];
			for (int i = 0; i < variants.Count; i++)
			{
				string text = variants[i];
				array[i] = this.GetVariantName(text);
				array2[i] = this.GetIcon(text);
				string text2;
				array3[i] = this.TryGetVariantValidity(Object, text, out text2);
				if (!array3[i])
				{
					array[i] = array[i].WithColor("K") + "\n" + text2;
				}
			}
			int num = 0;
			do
			{
				num = Popup.PickOption("Choose variant", null, "", "Sounds/UI/ui_notification", array, null, array2, null, null, null, null, 0, 60, num, -1, AllowEscape, false, false, true, false, null, null);
				if (num < 0)
				{
					return false;
				}
			}
			while (!array3[num]);
			this.SetVariant(variants[num]);
			return true;
		}

		/// <summary>
		///     Returns the term for this "mutation" on the object.
		/// </summary>
		/// <param name="who">Default to ParentObject</param>
		/// <returns />
		// Token: 0x0600C382 RID: 50050 RVA: 0x00475FAE File Offset: 0x004741AE
		public string GetMutationTerm(GameObject who = null)
		{
			return GetMutationTermEvent.GetFor(who ?? this.ParentObject, this);
		}

		/// <summary>Use to remove a equipment created by the mutation from the game.</summary>
		/// <param name="who">The mutant (ParentObject or what it used to be).</param>
		/// <param name="obj">The GameObject you are cleaning up.</param>
		// Token: 0x0600C383 RID: 50051 RVA: 0x00475FC1 File Offset: 0x004741C1
		protected void CleanUpMutationEquipment(GameObject who, GameObject obj)
		{
			this.CleanUpMutationEquipment(who, ref obj);
		}

		/// <summary>The <see cref="P:XRL.World.IPart.StatShifter" /> with the mutation's <see cref="P:XRL.World.Parts.Mutation.BaseMutation.DisplayName" />.</summary>
		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x0600C384 RID: 50052 RVA: 0x00475FCC File Offset: 0x004741CC
		public new StatShifter StatShifter
		{
			get
			{
				StatShifter statShifter = base.StatShifter;
				if (string.IsNullOrEmpty(statShifter.DefaultDisplayName))
				{
					statShifter.DefaultDisplayName = this.GetDisplayName(false);
				}
				return statShifter;
			}
		}

		// Token: 0x0600C385 RID: 50053 RVA: 0x00475FFC File Offset: 0x004741FC
		public static BaseMutation Create(string Mutation, string Variant = null)
		{
			BaseMutation genericMutation = Mutations.GetGenericMutation(Mutation, Variant, null);
			if (genericMutation != null)
			{
				return BaseMutation.Create(genericMutation.GetType(), Variant);
			}
			return null;
		}

		// Token: 0x0600C386 RID: 50054 RVA: 0x00476024 File Offset: 0x00474224
		public static BaseMutation Create(Type Mutation, string Variant = null)
		{
			BaseMutation baseMutation = (BaseMutation)GamePartBlueprint.PartReflectionCache.Get(Mutation).GetNewInstance();
			if (!Variant.IsNullOrEmpty())
			{
				baseMutation.SetVariant(Variant);
			}
			return baseMutation;
		}

		// Token: 0x0600C387 RID: 50055 RVA: 0x00476057 File Offset: 0x00474257
		public static BaseMutation Create(MutationEntry Mutation)
		{
			return BaseMutation.Create(Mutation.Class, Mutation.Variant);
		}

		// Token: 0x0600C388 RID: 50056 RVA: 0x0047606A File Offset: 0x0047426A
		public static T Create<T>(string Variant = null) where T : BaseMutation
		{
			return (T)((object)BaseMutation.Create(typeof(T), Variant));
		}

		/// <summary>The variant chosen from the <see cref="M:XRL.World.Parts.Mutation.BaseMutation.GetVariants">GetVariants()</see>.</summary>
		// Token: 0x04004DB2 RID: 19890
		public string Variant;

		// Token: 0x04004DB3 RID: 19891
		[Obsolete("Preserves SaveCompat, use GetDisplayName() or Set DisplayName on XML Node.")]
		public string _DisplayName = "";

		/// <summary>The base level (without bonuses) of the mutation.</summary>
		// Token: 0x04004DB4 RID: 19892
		public int BaseLevel = 1;

		/// <summary>The last value of BaseLevel, only differs during a call to <c><see cref="!:!">ChangeLevel</see></c>.</summary>
		// Token: 0x04004DB5 RID: 19893
		public int LastLevel;

		/// <summary>Most mutations will have an activated ability, so the base defines one to use by default with its ability helper methods.</summary>
		// Token: 0x04004DB6 RID: 19894
		public Guid ActivatedAbilityID;

		/// cache space saver - private
		// Token: 0x04004DB7 RID: 19895
		[NonSerialized]
		private List<BaseMutation.LevelCalculation> _levelCalcWorkspace = new List<BaseMutation.LevelCalculation>();

		// Token: 0x04004DB8 RID: 19896
		[Obsolete("Use GetMaxLevel()")]
		[NonSerialized]
		protected int MaxLevel = -1;

		// Token: 0x04004DB9 RID: 19897
		[Obsolete("Cache Value Only, use GetMutationType()")]
		[NonSerialized]
		public string _Type;

		// Token: 0x04004DBA RID: 19898
		[NonSerialized]
		private MutationEntry _Entry;

		/// <summary>Additional parameters passed by default to <see cref="M:XRL.World.GameObject.UseEnergy(System.Int32,System.String,System.String,System.Nullable{System.Int32},System.Boolean)">UseEnergy</see> when calling the version without a <c>string Type</c> parameter.</summary>
		// Token: 0x04004DBB RID: 19899
		private string EnergyUseType;

		/// <summary>
		///   Overrides the level based mutation cap.  Is set by <see cref="M:XRL.World.GameObjectFactory.CreateObject(System.String)">the game object factory</see>
		///   when creating an object with a mutation set to a specific level.  Allows mosquitos to have Wings 10 at Level 1 for instance.
		/// </summary>
		// Token: 0x04004DBC RID: 19900
		public int CapOverride = -1;

		// Token: 0x04004DBD RID: 19901
		[NonSerialized]
		private string _RapidKey;

		/// <summary>A struct used while calculating the level of the mutation with all it's various bonues, and relaying the information to the UI.</summary>
		// Token: 0x02001D43 RID: 7491
		public struct LevelCalculation
		{
			// Token: 0x0400724E RID: 29262
			public int bonus;

			// Token: 0x0400724F RID: 29263
			public bool temporary;

			// Token: 0x04007250 RID: 29264
			public string reason;

			// Token: 0x04007251 RID: 29265
			public char sigil;
		}
	}
}
