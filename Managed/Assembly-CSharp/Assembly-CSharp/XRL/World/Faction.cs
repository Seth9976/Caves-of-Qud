using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using HistoryKit;
using Occult.Engine.CodeGeneration;
using Qud.API;
using UnityEngine;
using XRL.Annals;
using XRL.Language;
using XRL.Names;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x0200084E RID: 2126
	[GenerateSerializationPartial]
	[Serializable]
	public class Faction : IComposite
	{
		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06005446 RID: 21574 RVA: 0x001919DE File Offset: 0x0018FBDE
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x001919E4 File Offset: 0x0018FBE4
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.ID);
			Writer.Write(this.Emblem);
			Writer.Write(this.Visible);
			Writer.Write(this.Old);
			Writer.Write(this.HatesPlayer);
			Writer.Write(this.Pettable);
			Writer.Write(this.Plural);
			Writer.Write(this.ExtradimensionalVersions);
			Writer.Write(this.FormatWithArticle);
			Writer.WriteOptimized(this.WaterRitualLiquid);
			Writer.WriteOptimized(this.WaterRitualSkill);
			Writer.WriteOptimized(this.WaterRitualSkillCost);
			Writer.Write(this.WaterRitualBuyMostValuableItem);
			Writer.WriteOptimized(this.WaterRitualFungusInfect);
			Writer.WriteOptimized(this.WaterRitualHermitOath);
			Writer.WriteOptimized(this.WaterRitualSkillPointAmount);
			Writer.WriteOptimized(this.WaterRitualSkillPointCost);
			Writer.WriteOptimized(this.WaterRitualMutation);
			Writer.WriteOptimized(this.WaterRitualMutationCost);
			Writer.WriteOptimized(this.WaterRitualGifts);
			Writer.WriteOptimized(this.WaterRitualItems);
			Writer.WriteOptimized(this.WaterRitualItemBlueprint);
			Writer.WriteOptimized(this.WaterRitualItemCost);
			Writer.WriteOptimized(this.WaterRitualBlueprints);
			Writer.WriteOptimized(this.WaterRitualRecipe);
			Writer.WriteOptimized(this.WaterRitualRecipeText);
			Writer.WriteOptimized(this.WaterRitualRecipeGenotype);
			Writer.Write(this.WaterRitualJoin);
			Writer.WriteOptimized(this.WaterRitualRandomMentalMutation);
			Writer.WriteOptimized(this.WaterRitualAltBehaviorPart);
			Writer.WriteOptimized(this.WaterRitualAltBehaviorTag);
			Writer.WriteOptimized(this.WaterRitualAltLiquid);
			Writer.WriteOptimized(this.WaterRitualAltSkill);
			Writer.WriteOptimized(this.WaterRitualAltSkillCost);
			Writer.WriteOptimized(this.WaterRitualAltGifts);
			Writer.WriteOptimized(this.WaterRitualAltItems);
			Writer.WriteOptimized(this.WaterRitualAltItemBlueprint);
			Writer.WriteOptimized(this.WaterRitualAltItemCost);
			Writer.WriteOptimized(this.WaterRitualAltBlueprints);
			Writer.WriteOptimized(this.Name);
			Writer.WriteOptimized(this._DisplayName);
			Writer.WriteOptimized(this.PositiveSound);
			Writer.WriteOptimized(this.NegativeSound);
			Writer.WriteOptimized(this.HistoricalSignificance);
			Writer.WriteOptimized(this.EntityID);
			Writer.WriteOptimized(this.EventID);
			Writer.WriteOptimized(this.DefaultAddress);
			Writer.WriteOptimized(this.RankTerm);
			Writer.Write(this.Ranks);
			Writer.Write<string, int>(this.RankStandings);
			Writer.WriteComposite<Worshippable>(this.Worshippables);
			Writer.WriteOptimized(this.DefaultFactionWorshipAttitude);
			Writer.Write(this.DefaultFactionWorshipAttitudeSet);
			Writer.Write(this.ApplyDefaultFactionWorshipAttitudeAfterSpecificFeelings);
			Writer.Write<string, int>(this.FactionWorshipAttitudes);
			Writer.WriteComposite<MemorialTracking>(this.QueuedMemorials);
			Writer.WriteComposite<MemorialTracking>(this.PerformedMemorials);
			Writer.Write<string, object>(this.Properties);
			Writer.Write<string, int>(this.IntProperties);
			Writer.WriteOptimized(this.Parent);
			Writer.Write<string, int>(this.FactionFeeling);
			Writer.WriteOptimized(this.InitialPlayerReputation);
			Writer.Write<string, int>(this.PartReputation);
			Writer.WriteComposite<FactionInterest>(this.Interests);
			Writer.Write(this.HolyPlaces);
			Writer.Write(this.BuyTargetedSecrets);
			Writer.Write(this.SellTargetedSecrets);
			Writer.WriteOptimized(this.BuyDescription);
			Writer.WriteOptimized(this.SellDescription);
			Writer.WriteOptimized(this.BothDescription);
			Writer.WriteOptimized(this.InterestsBlurb);
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x00191D48 File Offset: 0x0018FF48
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Read(SerializationReader Reader)
		{
			this.ID = Reader.ReadOptimizedInt32();
			this.Emblem = (FactionEmblem)Reader.ReadComposite();
			this.Visible = Reader.ReadBoolean();
			this.Old = Reader.ReadBoolean();
			this.HatesPlayer = Reader.ReadBoolean();
			this.Pettable = Reader.ReadBoolean();
			this.Plural = Reader.ReadBoolean();
			this.ExtradimensionalVersions = Reader.ReadBoolean();
			this.FormatWithArticle = Reader.ReadBoolean();
			this.WaterRitualLiquid = Reader.ReadOptimizedString();
			this.WaterRitualSkill = Reader.ReadOptimizedString();
			this.WaterRitualSkillCost = Reader.ReadOptimizedInt32();
			this.WaterRitualBuyMostValuableItem = Reader.ReadBoolean();
			this.WaterRitualFungusInfect = Reader.ReadOptimizedInt32();
			this.WaterRitualHermitOath = Reader.ReadOptimizedInt32();
			this.WaterRitualSkillPointAmount = Reader.ReadOptimizedInt32();
			this.WaterRitualSkillPointCost = Reader.ReadOptimizedInt32();
			this.WaterRitualMutation = Reader.ReadOptimizedString();
			this.WaterRitualMutationCost = Reader.ReadOptimizedInt32();
			this.WaterRitualGifts = Reader.ReadOptimizedString();
			this.WaterRitualItems = Reader.ReadOptimizedString();
			this.WaterRitualItemBlueprint = Reader.ReadOptimizedString();
			this.WaterRitualItemCost = Reader.ReadOptimizedInt32();
			this.WaterRitualBlueprints = Reader.ReadOptimizedString();
			this.WaterRitualRecipe = Reader.ReadOptimizedString();
			this.WaterRitualRecipeText = Reader.ReadOptimizedString();
			this.WaterRitualRecipeGenotype = Reader.ReadOptimizedString();
			this.WaterRitualJoin = Reader.ReadBoolean();
			this.WaterRitualRandomMentalMutation = Reader.ReadOptimizedInt32();
			this.WaterRitualAltBehaviorPart = Reader.ReadOptimizedString();
			this.WaterRitualAltBehaviorTag = Reader.ReadOptimizedString();
			this.WaterRitualAltLiquid = Reader.ReadOptimizedString();
			this.WaterRitualAltSkill = Reader.ReadOptimizedString();
			this.WaterRitualAltSkillCost = Reader.ReadOptimizedInt32();
			this.WaterRitualAltGifts = Reader.ReadOptimizedString();
			this.WaterRitualAltItems = Reader.ReadOptimizedString();
			this.WaterRitualAltItemBlueprint = Reader.ReadOptimizedString();
			this.WaterRitualAltItemCost = Reader.ReadOptimizedInt32();
			this.WaterRitualAltBlueprints = Reader.ReadOptimizedString();
			this.Name = Reader.ReadOptimizedString();
			this._DisplayName = Reader.ReadOptimizedString();
			this.PositiveSound = Reader.ReadOptimizedString();
			this.NegativeSound = Reader.ReadOptimizedString();
			this.HistoricalSignificance = Reader.ReadOptimizedInt32();
			this.EntityID = Reader.ReadOptimizedString();
			this.EventID = Reader.ReadOptimizedInt64();
			this.DefaultAddress = Reader.ReadOptimizedString();
			this.RankTerm = Reader.ReadOptimizedString();
			this.Ranks = Reader.ReadList<string>();
			this.RankStandings = Reader.ReadDictionary<string, int>();
			this.Worshippables = Reader.ReadCompositeList<Worshippable>();
			this.DefaultFactionWorshipAttitude = Reader.ReadOptimizedInt32();
			this.DefaultFactionWorshipAttitudeSet = Reader.ReadBoolean();
			this.ApplyDefaultFactionWorshipAttitudeAfterSpecificFeelings = Reader.ReadBoolean();
			this.FactionWorshipAttitudes = Reader.ReadDictionary<string, int>();
			this.QueuedMemorials = Reader.ReadCompositeList<MemorialTracking>();
			this.PerformedMemorials = Reader.ReadCompositeList<MemorialTracking>();
			this.Properties = Reader.ReadDictionary<string, object>();
			this.IntProperties = Reader.ReadDictionary<string, int>();
			this.Parent = Reader.ReadOptimizedString();
			this.FactionFeeling = Reader.ReadDictionary<string, int>();
			this.InitialPlayerReputation = Reader.ReadOptimizedInt32();
			this.PartReputation = Reader.ReadDictionary<string, int>();
			this.Interests = Reader.ReadCompositeList<FactionInterest>();
			this.HolyPlaces = Reader.ReadList<string>();
			this.BuyTargetedSecrets = Reader.ReadBoolean();
			this.SellTargetedSecrets = Reader.ReadBoolean();
			this.BuyDescription = Reader.ReadOptimizedString();
			this.SellDescription = Reader.ReadOptimizedString();
			this.BothDescription = Reader.ReadOptimizedString();
			this.InterestsBlurb = Reader.ReadOptimizedString();
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06005449 RID: 21577 RVA: 0x001920B0 File Offset: 0x001902B0
		// (set) Token: 0x0600544A RID: 21578 RVA: 0x0019211E File Offset: 0x0019031E
		public string DisplayName
		{
			get
			{
				if (this._DisplayName == null)
				{
					this._DisplayName = this.Name;
				}
				if (this.Name != null && this.Name.StartsWith("SultanCult") && The.Game != null)
				{
					this._DisplayName = The.Game.GetStringGameState("CultDisplayName_" + this.Name, this.Name);
				}
				return this._DisplayName;
			}
			set
			{
				this._DisplayName = value;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x0600544B RID: 21579 RVA: 0x00192128 File Offset: 0x00190328
		public string TargetedSecretString
		{
			get
			{
				string text;
				if ((text = this._TargetedSecretString) == null)
				{
					text = (this._TargetedSecretString = "include:" + this.Name);
				}
				return text;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x0600544C RID: 21580 RVA: 0x00192158 File Offset: 0x00190358
		public string GossipSecretString
		{
			get
			{
				string text;
				if ((text = this._GossipSecretString) == null)
				{
					text = (this._GossipSecretString = "gossip:" + this.Name);
				}
				return text;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x0600544D RID: 21581 RVA: 0x00192188 File Offset: 0x00190388
		public string NoBuySecretString
		{
			get
			{
				string text;
				if ((text = this._NoBuySecretString) == null)
				{
					text = (this._NoBuySecretString = "nobuy:" + this.Name);
				}
				return text;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x0600544E RID: 21582 RVA: 0x001921B8 File Offset: 0x001903B8
		public string NoSellSecretString
		{
			get
			{
				string text;
				if ((text = this._NoSellSecretString) == null)
				{
					text = (this._NoSellSecretString = "nosell:" + this.Name);
				}
				return text;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x0600544F RID: 21583 RVA: 0x001921E8 File Offset: 0x001903E8
		// (set) Token: 0x06005450 RID: 21584 RVA: 0x00192277 File Offset: 0x00190477
		public string Heirloom
		{
			get
			{
				if (!The.Game.HasStringGameState("Heirloom_" + this.Name))
				{
					Stat.ReseedFrom("Heirloom_" + this.Name, false);
					List<string> list = new List<string>(Items.ItemTableNames.Keys);
					The.Game.SetStringGameState("Heirloom_" + this.Name, list.GetRandomElement<string>(null));
				}
				return The.Game.GetStringGameState("Heirloom_" + this.Name, "");
			}
			set
			{
				The.Game.SetStringGameState("Heirloom_" + this.Name, value);
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06005451 RID: 21585 RVA: 0x00192294 File Offset: 0x00190494
		// (set) Token: 0x06005452 RID: 21586 RVA: 0x001922B5 File Offset: 0x001904B5
		public string HeirloomID
		{
			get
			{
				return The.Game.GetStringGameState("HeirloomID_" + this.Name, "");
			}
			set
			{
				if (value != null)
				{
					The.Game.SetStringGameState("HeirloomID_" + this.Name, value);
					return;
				}
				The.Game.RemoveStringGameState("HeirloomID_" + this.Name);
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06005453 RID: 21587 RVA: 0x001922F0 File Offset: 0x001904F0
		public static Reputation PlayerReputation
		{
			get
			{
				XRLGame game = The.Game;
				if (game == null)
				{
					return null;
				}
				return game.PlayerReputation;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06005454 RID: 21588 RVA: 0x00192302 File Offset: 0x00190502
		public int CurrentReputation
		{
			get
			{
				return The.Game.PlayerReputation.Get(this);
			}
		}

		// Token: 0x06005455 RID: 21589 RVA: 0x00192314 File Offset: 0x00190514
		public Faction()
		{
		}

		// Token: 0x06005456 RID: 21590 RVA: 0x0019242D File Offset: 0x0019062D
		public Faction(string Name)
			: this()
		{
			this.Name = Name;
		}

		// Token: 0x06005457 RID: 21591 RVA: 0x0019243C File Offset: 0x0019063C
		public Faction(string Name = null, bool Visibility = true, string DisplayName = null, bool Old = true, string WaterRitualLiquid = "water")
			: this(Name)
		{
			this.Visible = Visibility;
			this.Old = Old;
			this.WaterRitualLiquid = WaterRitualLiquid;
			this.DisplayName = DisplayName ?? Name;
		}

		// Token: 0x06005458 RID: 21592 RVA: 0x00192468 File Offset: 0x00190668
		public static string GetFormattedName(string FactionName)
		{
			if (FactionName == "*")
			{
				return "everyone";
			}
			string text;
			try
			{
				text = Factions.Get(FactionName).GetFormattedName();
			}
			catch
			{
				Debug.Log("Failed to get faction: " + FactionName);
				text = "";
			}
			return text;
		}

		// Token: 0x06005459 RID: 21593 RVA: 0x001924C4 File Offset: 0x001906C4
		[Obsolete("use GetFormattedName(), will be removed after Q2 2024")]
		public static string getFormattedName(string factionName)
		{
			return Faction.GetFormattedName(factionName);
		}

		// Token: 0x0600545A RID: 21594 RVA: 0x001924CC File Offset: 0x001906CC
		public static string GetFormattedGroupName(string FactionName)
		{
			string text2;
			try
			{
				string text = Faction.GetFormattedName(FactionName);
				if (text.StartsWith("the ") || text.StartsWith("The "))
				{
					text = text.Substring(4);
				}
				text2 = text;
			}
			catch
			{
				Debug.Log("Failed to get faction: " + FactionName);
				text2 = "";
			}
			return text2;
		}

		// Token: 0x0600545B RID: 21595 RVA: 0x00192530 File Offset: 0x00190730
		[Obsolete("use GetFormattedGroupName(), will be removed after Q2 2024")]
		public static string getFormattedGroupName(string FactionName)
		{
			return Faction.GetFormattedGroupName(FactionName);
		}

		// Token: 0x0600545C RID: 21596 RVA: 0x00192538 File Offset: 0x00190738
		public string GetFeelingText()
		{
			string text = Markup.Color("C", ConsoleLib.Console.ColorUtility.CapitalizeExceptFormatting(this.GetFormattedName())) + " ";
			switch ((The.Game == null) ? 0 : Faction.PlayerReputation.GetAttitude(this.Name))
			{
			case -2:
				return string.Concat(new string[]
				{
					text,
					this.Plural ? "despise" : "despises",
					" you. Even docile ",
					this.Plural ? "ones" : "members",
					" will attack you."
				});
			case -1:
				return string.Concat(new string[]
				{
					text,
					this.Plural ? "dislike" : "dislikes",
					" you, but docile ",
					this.Plural ? "ones" : "members",
					" won't attack you."
				});
			case 1:
				return string.Concat(new string[]
				{
					text,
					this.Plural ? "favor" : "favors",
					" you. Aggressive ",
					this.Plural ? "ones" : "members",
					" won't attack you."
				});
			case 2:
				return string.Concat(new string[]
				{
					text,
					this.Plural ? "revere" : "reveres",
					" you and ",
					this.Plural ? "consider" : "considers",
					" you one of their own."
				});
			}
			return string.Concat(new string[]
			{
				text,
				this.Plural ? "don't" : "doesn't",
				" care about you, but aggressive ",
				this.Plural ? "ones" : "members",
				" will attack you."
			});
		}

		// Token: 0x0600545D RID: 21597 RVA: 0x0019273C File Offset: 0x0019093C
		public string GetPetText()
		{
			if (!this.Pettable)
			{
				return string.Empty;
			}
			string text = ((Faction.PlayerReputation.GetAttitude(this.Name) >= 1) ? "will usually let you pet them" : "won't usually let you pet them");
			return Markup.Color("C", ConsoleLib.Console.ColorUtility.CapitalizeExceptFormatting(this.GetFormattedName())) + " " + text + ".";
		}

		// Token: 0x0600545E RID: 21598 RVA: 0x0019279C File Offset: 0x0019099C
		public string GetRankText()
		{
			if (GlobalConfig.GetBoolSetting("ShowFacitonRank", false))
			{
				XRLGame game = The.Game;
				string text;
				if (game == null)
				{
					text = null;
				}
				else
				{
					Reputation playerReputation = game.PlayerReputation;
					text = ((playerReputation != null) ? playerReputation.GetFactionRank(this.Name) : null);
				}
				string text2 = text;
				if (text2 != null && !string.IsNullOrEmpty(text2))
				{
					return string.Concat(new string[]
					{
						"You hold the ",
						this.GetRankTerm(),
						" of ",
						text2,
						" among them."
					});
				}
			}
			return string.Empty;
		}

		// Token: 0x0600545F RID: 21599 RVA: 0x0019281B File Offset: 0x00190A1B
		public string GetHolyPlaceText()
		{
			Reputation playerReputation = Faction.PlayerReputation;
			if (((playerReputation != null) ? playerReputation.GetAttitude(this.Name) : 0) < 1)
			{
				return "You aren't welcome in their holy places.";
			}
			return "You are welcome in their holy places.";
		}

		// Token: 0x06005460 RID: 21600 RVA: 0x00192844 File Offset: 0x00190A44
		public static string GetFeelingDescription(string FactionName)
		{
			Faction faction = Factions.Get(FactionName);
			string text = faction.GetFeelingText() + "\n\n";
			string text2 = "";
			string rankText = faction.GetRankText();
			if (rankText != null && !string.IsNullOrEmpty(rankText))
			{
				text = text + text2 + rankText;
				text2 = " ";
			}
			string petText = faction.GetPetText();
			if (petText != null && !string.IsNullOrEmpty(petText))
			{
				text = text + text2 + petText;
				text2 = " ";
			}
			return text + text2 + faction.GetHolyPlaceText();
		}

		// Token: 0x06005461 RID: 21601 RVA: 0x001928C4 File Offset: 0x00190AC4
		public static string GetPreferredSecretDescription(string FactionName)
		{
			Faction faction = Factions.Get(FactionName);
			string text = faction.GetFormattedName();
			text = char.ToUpper(text[0]).ToString() + text.Substring(1);
			text = "{{C|" + text + "}}";
			string text2 = " " + (faction.Plural ? "are" : "is");
			Faction.BuyOnly.Clear();
			Faction.SellOnly.Clear();
			Faction.Both.Clear();
			bool flag = false;
			foreach (FactionInterest factionInterest in faction.Interests)
			{
				if (factionInterest.WillBuy != factionInterest.WillSell && (factionInterest.WillBuy || factionInterest.WillSell))
				{
					flag = true;
					break;
				}
			}
			foreach (FactionInterest factionInterest2 in faction.Interests)
			{
				string description = factionInterest2.GetDescription(faction);
				if (!description.IsNullOrEmpty())
				{
					if (factionInterest2.WillBuy && factionInterest2.WillSell)
					{
						if (flag)
						{
							Faction.BuyOnly.Add(description);
							Faction.SellOnly.Add(description);
						}
						else
						{
							Faction.Both.Add(description);
						}
					}
					else if (factionInterest2.WillBuy)
					{
						Faction.BuyOnly.Add(description);
					}
					else if (factionInterest2.WillSell)
					{
						Faction.SellOnly.Add(description);
					}
				}
			}
			string sultanTerm = HistoryAPI.GetSultanTerm();
			string text3 = Grammar.Pluralize(sultanTerm);
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = faction.HasInterestIn("sultan", true, false);
			bool flag5 = faction.HasInterestIn("sultan", false, true);
			if (!flag4 || !flag5)
			{
				if (faction.Name.StartsWith("SultanCult") || faction.Name.StartsWith("PlayerCult"))
				{
					if (flag || flag4 || flag5)
					{
						if (!flag4)
						{
							Faction.BuyOnly.Add("the " + sultanTerm + " they worship");
						}
						if (!flag5)
						{
							Faction.SellOnly.Add("the " + sultanTerm + " they worship");
						}
					}
					else
					{
						Faction.Both.Add("the " + sultanTerm + " they worship");
					}
				}
				else
				{
					using (List<JournalSultanNote>.Enumerator enumerator2 = JournalAPI.GetSultanNotes(null).GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.Has(faction.TargetedSecretString))
							{
								if (faction.BuyTargetedSecrets && faction.SellTargetedSecrets && !flag && !flag4 && !flag5 && faction.BuyDescription.IsNullOrEmpty() && faction.SellDescription.IsNullOrEmpty())
								{
									Faction.Both.Add(text3 + " they admire or despise");
									break;
								}
								if (faction.BuyTargetedSecrets && !flag4)
								{
									if (faction.BuyDescription.IsNullOrEmpty())
									{
										Faction.BuyOnly.Add(text3 + " they admire or despise");
									}
									else
									{
										flag2 = true;
									}
								}
								if (!faction.SellTargetedSecrets || flag5)
								{
									break;
								}
								if (faction.SellDescription.IsNullOrEmpty())
								{
									Faction.SellOnly.Add(text3 + " they admire or despise");
									break;
								}
								flag3 = true;
								break;
							}
						}
					}
				}
			}
			bool flag6 = false;
			if (Faction.BuyOnly.Count > 0 && faction.BuyDescription.IsNullOrEmpty() && !faction.HasInterestIn("gossip", true, false) && !faction.HasInterestIn(faction.GossipSecretString, true, false))
			{
				Faction.BuyOnly.Add("gossip that's about them");
				flag6 = true;
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (!faction.InterestsBlurb.IsNullOrEmpty())
			{
				stringBuilder.Append(text).Append(faction.InterestsBlurb);
			}
			if (Faction.Both.Count > 0)
			{
				stringBuilder.Compound(text, ".\n\n").Append(text2).Append(faction.BothDescription.IsNullOrEmpty() ? " interested in trading secrets about " : faction.BothDescription)
					.Append(Grammar.MakeAndList(Faction.Both, true));
			}
			if (Faction.SellOnly.Count > 0)
			{
				stringBuilder.Compound(text, ".\n\n").Append(text2).Append(faction.SellDescription.IsNullOrEmpty() ? " interested in sharing secrets about " : faction.SellDescription)
					.Append(Grammar.MakeAndList(Faction.SellOnly, true));
			}
			if (Faction.BuyOnly.Count > 0)
			{
				stringBuilder.Compound(text, ".\n\n").Append(text2).Append(faction.BuyDescription.IsNullOrEmpty() ? " interested in learning about " : faction.BuyDescription)
					.Append(Grammar.MakeAndList(Faction.BuyOnly, true));
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append('.');
			}
			if (!flag6 && !faction.HasInterestIn("gossip", true, false) && !faction.HasInterestIn(faction.GossipSecretString, true, false))
			{
				if (stringBuilder.Length == 0)
				{
					stringBuilder.Append(text).Append(text2);
				}
				else
				{
					stringBuilder.Append(" They're also");
				}
				if (flag2 && flag3)
				{
					stringBuilder.Append(" interested in trading secrets about " + text3 + " they admire or despise and hearing gossip that's about them.");
					flag2 = false;
					flag3 = false;
				}
				else if (flag2)
				{
					stringBuilder.Append(" interested in learning about " + text3 + " they admire or despise and hearing gossip that's about them.");
					flag2 = false;
				}
				else if (flag3)
				{
					stringBuilder.Append(" interested in sharing secrets about " + text3 + " they admire or despise and hearing gossip that's about them.");
					flag3 = false;
				}
				else
				{
					stringBuilder.Append(" interested in hearing gossip that's about them.");
				}
			}
			if (flag2 || flag3)
			{
				if (stringBuilder.Length == 0)
				{
					stringBuilder.Append(text).Append(text2);
				}
				else
				{
					stringBuilder.Append(" They're also");
				}
				if (flag2 && flag3)
				{
					stringBuilder.Append(" interested in trading secrets about " + text3 + " they admire or despise.");
					flag2 = false;
					flag3 = false;
				}
				else if (flag2)
				{
					stringBuilder.Append(" interested in learning about " + text3 + " they admire or despise.");
					flag2 = false;
				}
				else if (flag3)
				{
					stringBuilder.Append(" interested in sharing secrets about " + text3 + " they admire or despise.");
					flag3 = false;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005462 RID: 21602 RVA: 0x00192F40 File Offset: 0x00191140
		public static string GetRepPageDescription(string Name)
		{
			return Faction.GetFeelingDescription(Name) + "\n\n" + Faction.GetPreferredSecretDescription(Name);
		}

		// Token: 0x06005463 RID: 21603 RVA: 0x00192F58 File Offset: 0x00191158
		public static string GetSultanFactionName(string Period)
		{
			if (Period == "6")
			{
				return "Resheph";
			}
			return "SultanCult" + Period;
		}

		// Token: 0x06005464 RID: 21604 RVA: 0x00192F78 File Offset: 0x00191178
		public static string GetSultanFactionName(int Period)
		{
			if (Period == 6)
			{
				return "Resheph";
			}
			return "SultanCult" + Period.ToString();
		}

		// Token: 0x06005465 RID: 21605 RVA: 0x00192F95 File Offset: 0x00191195
		public static string GetRankTerm(string Name)
		{
			Faction ifExists = Factions.GetIfExists(Name);
			return ((ifExists != null) ? ifExists.GetRankTerm() : null) ?? "rank";
		}

		// Token: 0x06005466 RID: 21606 RVA: 0x00192FB2 File Offset: 0x001911B2
		public static List<string> GetRanks(string Name)
		{
			Faction ifExists = Factions.GetIfExists(Name);
			if (ifExists == null)
			{
				return null;
			}
			return ifExists.GetRanks();
		}

		// Token: 0x06005467 RID: 21607 RVA: 0x00192FC5 File Offset: 0x001911C5
		public static Dictionary<string, int> GetRankStandings(string Name)
		{
			Faction ifExists = Factions.GetIfExists(Name);
			if (ifExists == null)
			{
				return null;
			}
			return ifExists.GetRankStandings();
		}

		// Token: 0x06005468 RID: 21608 RVA: 0x00192FD8 File Offset: 0x001911D8
		public static int GetRankStanding(string Name, string Rank)
		{
			Faction ifExists = Factions.GetIfExists(Name);
			if (ifExists == null)
			{
				return 0;
			}
			return ifExists.GetRankStanding(Rank);
		}

		// Token: 0x06005469 RID: 21609 RVA: 0x00192FEC File Offset: 0x001911EC
		public static string GetRankByStanding(string Name, int Standing)
		{
			Faction ifExists = Factions.GetIfExists(Name);
			if (ifExists == null)
			{
				return null;
			}
			return ifExists.GetRankByStanding(Standing);
		}

		// Token: 0x0600546A RID: 21610 RVA: 0x00193000 File Offset: 0x00191200
		public static string GetLowestRank(string Name)
		{
			Faction ifExists = Factions.GetIfExists(Name);
			if (ifExists == null)
			{
				return null;
			}
			return ifExists.GetLowestRank();
		}

		// Token: 0x0600546B RID: 21611 RVA: 0x00193013 File Offset: 0x00191213
		public static string GetHighestRank(string Name)
		{
			Faction ifExists = Factions.GetIfExists(Name);
			if (ifExists == null)
			{
				return null;
			}
			return ifExists.GetHighestRank();
		}

		// Token: 0x0600546C RID: 21612 RVA: 0x00193026 File Offset: 0x00191226
		public static int GetLowestStanding(string Name)
		{
			Faction ifExists = Factions.GetIfExists(Name);
			if (ifExists == null)
			{
				return 0;
			}
			return ifExists.GetLowestStanding();
		}

		// Token: 0x0600546D RID: 21613 RVA: 0x00193039 File Offset: 0x00191239
		public static int GetHighestStanding(string Name)
		{
			Faction ifExists = Factions.GetIfExists(Name);
			if (ifExists == null)
			{
				return 0;
			}
			return ifExists.GetHighestStanding();
		}

		// Token: 0x0600546E RID: 21614 RVA: 0x0019304C File Offset: 0x0019124C
		public static string GetDefaultAddress(string Name)
		{
			Faction ifExists = Factions.GetIfExists(Name);
			if (ifExists == null)
			{
				return null;
			}
			return ifExists.GetDefaultAddress();
		}

		// Token: 0x0600546F RID: 21615 RVA: 0x0019305F File Offset: 0x0019125F
		public string GetFormattedName()
		{
			if (!this.FormatWithArticle)
			{
				return this.DisplayName;
			}
			return "the " + this.DisplayName;
		}

		// Token: 0x06005470 RID: 21616 RVA: 0x00193080 File Offset: 0x00191280
		[Obsolete("Use GetFormattedName(), will be removed after Q2 2024")]
		public string getFormattedName()
		{
			return this.GetFormattedName();
		}

		// Token: 0x06005471 RID: 21617 RVA: 0x00193088 File Offset: 0x00191288
		public string GetWaterRitualLiquid(GameObject speaker)
		{
			if (this.WaterRitualAltLiquid.IsNullOrEmpty() || !this.UseAltBehavior(speaker))
			{
				return this.WaterRitualLiquid;
			}
			return this.WaterRitualAltLiquid;
		}

		// Token: 0x06005472 RID: 21618 RVA: 0x001930AD File Offset: 0x001912AD
		public string GetRankTerm()
		{
			return this.RankTerm ?? "rank";
		}

		// Token: 0x06005473 RID: 21619 RVA: 0x001930BE File Offset: 0x001912BE
		public List<string> GetRanks()
		{
			return this.Ranks;
		}

		// Token: 0x06005474 RID: 21620 RVA: 0x001930C6 File Offset: 0x001912C6
		public Dictionary<string, int> GetRankStandings()
		{
			return this.RankStandings;
		}

		// Token: 0x06005475 RID: 21621 RVA: 0x001930D0 File Offset: 0x001912D0
		public int GetRankStanding(string Rank)
		{
			int num;
			if (this.RankStandings.TryGetValue(Rank, out num))
			{
				return num;
			}
			return 0;
		}

		// Token: 0x06005476 RID: 21622 RVA: 0x001930F0 File Offset: 0x001912F0
		public string GetRankByStanding(int Standing)
		{
			foreach (KeyValuePair<string, int> keyValuePair in this.RankStandings)
			{
				if (keyValuePair.Value == Standing)
				{
					return keyValuePair.Key;
				}
			}
			return null;
		}

		// Token: 0x06005477 RID: 21623 RVA: 0x00193154 File Offset: 0x00191354
		public string GetLowestRank()
		{
			int num = int.MaxValue;
			string text = null;
			foreach (KeyValuePair<string, int> keyValuePair in this.RankStandings)
			{
				if (keyValuePair.Value < num)
				{
					text = keyValuePair.Key;
					num = keyValuePair.Value;
				}
			}
			return text;
		}

		// Token: 0x06005478 RID: 21624 RVA: 0x001931C4 File Offset: 0x001913C4
		public string GetHighestRank()
		{
			int num = int.MinValue;
			string text = null;
			foreach (KeyValuePair<string, int> keyValuePair in this.RankStandings)
			{
				if (keyValuePair.Value > num)
				{
					text = keyValuePair.Key;
					num = keyValuePair.Value;
				}
			}
			return text;
		}

		// Token: 0x06005479 RID: 21625 RVA: 0x00193234 File Offset: 0x00191434
		public int GetLowestStanding()
		{
			int num = int.MaxValue;
			foreach (KeyValuePair<string, int> keyValuePair in this.RankStandings)
			{
				if (keyValuePair.Value < num)
				{
					num = keyValuePair.Value;
				}
			}
			if (num != 2147483647)
			{
				return num;
			}
			return 0;
		}

		// Token: 0x0600547A RID: 21626 RVA: 0x001932A4 File Offset: 0x001914A4
		public int GetHighestStanding()
		{
			int num = int.MinValue;
			foreach (KeyValuePair<string, int> keyValuePair in this.RankStandings)
			{
				if (keyValuePair.Value < num)
				{
					num = keyValuePair.Value;
				}
			}
			if (num != -2147483648)
			{
				return num;
			}
			return 0;
		}

		// Token: 0x0600547B RID: 21627 RVA: 0x00193314 File Offset: 0x00191514
		public string GetDefaultAddress()
		{
			return this.DefaultAddress;
		}

		// Token: 0x0600547C RID: 21628 RVA: 0x0019331C File Offset: 0x0019151C
		public object GetProperty(string Key, object Default = null)
		{
			if (Key == null || this.Properties == null)
			{
				return Default;
			}
			object obj;
			if (!this.Properties.TryGetValue(Key, out obj))
			{
				return Default;
			}
			return obj;
		}

		// Token: 0x0600547D RID: 21629 RVA: 0x0019334C File Offset: 0x0019154C
		public T GetProperty<T>(string Key, T Default = default(T)) where T : class
		{
			if (Key == null || this.Properties == null)
			{
				return Default;
			}
			object obj;
			if (!this.Properties.TryGetValue(Key, out obj))
			{
				return Default;
			}
			return obj as T;
		}

		// Token: 0x0600547E RID: 21630 RVA: 0x00193384 File Offset: 0x00191584
		public string GetStringProperty(string Key, string Default = null)
		{
			if (Key == null || this.Properties == null)
			{
				return Default;
			}
			object obj;
			if (!this.Properties.TryGetValue(Key, out obj))
			{
				return Default;
			}
			return obj as string;
		}

		// Token: 0x0600547F RID: 21631 RVA: 0x001933B8 File Offset: 0x001915B8
		public int GetIntProperty(string Key, int Default = 0)
		{
			if (Key == null || this.IntProperties == null)
			{
				return Default;
			}
			int num;
			if (!this.IntProperties.TryGetValue(Key, out num))
			{
				return Default;
			}
			return num;
		}

		// Token: 0x06005480 RID: 21632 RVA: 0x001933E8 File Offset: 0x001915E8
		public float GetFloatProperty(string Key, float Default = 0f)
		{
			if (Key == null || this.IntProperties == null)
			{
				return Default;
			}
			int num;
			if (!this.IntProperties.TryGetValue(Key, out num))
			{
				return Default;
			}
			return BitConverter.Int32BitsToSingle(num);
		}

		// Token: 0x06005481 RID: 21633 RVA: 0x0019341A File Offset: 0x0019161A
		public void SetProperty(string Key, object Value)
		{
			if (this.Properties == null)
			{
				this.Properties = new Dictionary<string, object>(3);
			}
			this.Properties[Key] = Value;
		}

		// Token: 0x06005482 RID: 21634 RVA: 0x0019343D File Offset: 0x0019163D
		public void SetProperty(string Key, int Value)
		{
			if (this.IntProperties == null)
			{
				this.IntProperties = new Dictionary<string, int>(3);
			}
			this.IntProperties[Key] = Value;
		}

		// Token: 0x06005483 RID: 21635 RVA: 0x00193460 File Offset: 0x00191660
		public void SetProperty(string Key, float Value)
		{
			if (this.IntProperties == null)
			{
				this.IntProperties = new Dictionary<string, int>(3);
			}
			this.IntProperties[Key] = BitConverter.SingleToInt32Bits(Value);
		}

		// Token: 0x06005484 RID: 21636 RVA: 0x00193488 File Offset: 0x00191688
		public bool HasProperty(string Key)
		{
			return (this.Properties != null && this.Properties.ContainsKey(Key)) || (this.IntProperties != null && this.IntProperties.ContainsKey(Key));
		}

		// Token: 0x06005485 RID: 21637 RVA: 0x001934B8 File Offset: 0x001916B8
		public bool RemoveProperty(string Key)
		{
			bool flag = false;
			if (this.Properties != null)
			{
				flag |= this.Properties.Remove(Key);
			}
			if (this.IntProperties != null)
			{
				flag |= this.IntProperties.Remove(Key);
			}
			return flag;
		}

		// Token: 0x06005486 RID: 21638 RVA: 0x001934F8 File Offset: 0x001916F8
		public bool HasInterestIn(string topics, bool Buy = false, bool Sell = false)
		{
			using (List<FactionInterest>.Enumerator enumerator = this.Interests.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Includes(topics, Buy, Sell))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005487 RID: 21639 RVA: 0x00193554 File Offset: 0x00191754
		public bool TryAddFactionFeeling(string Faction, int Feeling)
		{
			return this.FactionFeeling.TryAdd(Faction, Feeling);
		}

		// Token: 0x06005488 RID: 21640 RVA: 0x00193563 File Offset: 0x00191763
		public int SetFactionFeeling(string faction, int faction_feeling)
		{
			this.FactionFeeling.Set(faction, faction_feeling);
			return faction_feeling;
		}

		// Token: 0x06005489 RID: 21641 RVA: 0x00193573 File Offset: 0x00191773
		public int SetPartReputation(string part, int feeling)
		{
			this.PartReputation.Set(part, feeling);
			return feeling;
		}

		// Token: 0x0600548A RID: 21642 RVA: 0x00193584 File Offset: 0x00191784
		public Faction SetParent(string Name)
		{
			Faction faction = Factions.Get(Name);
			this.Parent = faction.Name;
			this.InitialPlayerReputation = faction.InitialPlayerReputation;
			this.DefaultFactionWorshipAttitude = faction.DefaultFactionWorshipAttitude;
			this.DefaultFactionWorshipAttitudeSet = faction.DefaultFactionWorshipAttitudeSet;
			this.ApplyDefaultFactionWorshipAttitudeAfterSpecificFeelings = faction.ApplyDefaultFactionWorshipAttitudeAfterSpecificFeelings;
			return faction;
		}

		// Token: 0x0600548B RID: 21643 RVA: 0x001935D8 File Offset: 0x001917D8
		public void InheritParent()
		{
			if (this.Parent.IsNullOrEmpty())
			{
				return;
			}
			Faction faction = Factions.Get(this.Parent);
			foreach (KeyValuePair<string, int> keyValuePair in faction.FactionFeeling)
			{
				string text;
				int num;
				keyValuePair.Deconstruct(out text, out num);
				string text2 = text;
				int num2 = num;
				this.FactionFeeling.TryAdd(text2, num2);
			}
			foreach (KeyValuePair<string, int> keyValuePair in faction.FactionWorshipAttitudes)
			{
				string text;
				int num;
				keyValuePair.Deconstruct(out text, out num);
				string text3 = text;
				int num3 = num;
				this.FactionWorshipAttitudes.TryAdd(text3, num3);
			}
		}

		// Token: 0x0600548C RID: 21644 RVA: 0x001936C0 File Offset: 0x001918C0
		public virtual int GetFeelingTowardsObject(GameObject Object, IDictionary<string, int> Override = null)
		{
			Brain brain = Object.Brain;
			if (((brain != null) ? brain._ParentObject : null) == null)
			{
				return 0;
			}
			brain = brain.GetFinalLeaderBrain() ?? brain;
			int num;
			if (brain.FactionFeelings.TryGetValue(this.Name, out num))
			{
				return num;
			}
			int num2 = 0;
			if (brain.Allegiance.IsNullOrEmpty<KeyValuePair<string, int>>())
			{
				if (!this.FactionFeeling.TryGetValue("*", out num2))
				{
					return 0;
				}
			}
			else
			{
				num2 = brain.Allegiance.GetBaseFeeling(this, Override);
			}
			if (this == HolyPlaceSystem.LastHolyFaction && num2 > -50 && num2 < 50 && brain.ParentObject.IsPlayer())
			{
				num2 = -50;
			}
			return num2;
		}

		// Token: 0x0600548D RID: 21645 RVA: 0x00193760 File Offset: 0x00191960
		public virtual int GetFeelingTowardsFaction(string Faction, int? DefaultBeforeGeneral = null, int DefaultAfterGeneral = 0)
		{
			if (Faction == this.Name)
			{
				return 100;
			}
			int num;
			if (this.FactionFeeling.TryGetValue(Faction, out num))
			{
				return num;
			}
			if (DefaultBeforeGeneral != null)
			{
				return DefaultBeforeGeneral.Value;
			}
			int num2;
			if (this.FactionFeeling.TryGetValue("*", out num2))
			{
				return num2;
			}
			return DefaultAfterGeneral;
		}

		// Token: 0x0600548E RID: 21646 RVA: 0x001937B8 File Offset: 0x001919B8
		private bool SellRequiresSpecificInterest(IBaseJournalEntry note)
		{
			return note.Has("onlySellIfTargetedAndInterested");
		}

		// Token: 0x0600548F RID: 21647 RVA: 0x001937CA File Offset: 0x001919CA
		public bool InterestedIn(IBaseJournalEntry note, bool Buy = false, bool Sell = false)
		{
			return this.GetInterestIn(note, Buy, Sell) > 0;
		}

		// Token: 0x06005490 RID: 21648 RVA: 0x001937D8 File Offset: 0x001919D8
		public int GetInterestIn(IBaseJournalEntry note, bool Buy = false, bool Sell = false)
		{
			if (Buy && note.Has(this.NoBuySecretString))
			{
				return 0;
			}
			if (Sell && note.Has(this.NoSellSecretString))
			{
				return 0;
			}
			if (Buy)
			{
				if (note.Has(this.GossipSecretString))
				{
					return note.Weight;
				}
				if (this.BuyTargetedSecrets && note.Has(this.TargetedSecretString))
				{
					bool flag = true;
					int num = 0;
					foreach (FactionInterest factionInterest in this.Interests)
					{
						if (factionInterest.Excludes(note, Buy, Sell))
						{
							flag = false;
							break;
						}
						if (factionInterest.Weight > num)
						{
							num = factionInterest.Weight;
						}
					}
					if (flag)
					{
						return note.Weight + num;
					}
				}
			}
			if (Sell && this.SellTargetedSecrets && note.Has(this.TargetedSecretString))
			{
				if (this.SellRequiresSpecificInterest(note))
				{
					using (List<FactionInterest>.Enumerator enumerator = this.Interests.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							FactionInterest factionInterest2 = enumerator.Current;
							if (factionInterest2.Includes(note, Buy, Sell))
							{
								return note.Weight + factionInterest2.Weight;
							}
						}
						goto IL_019A;
					}
				}
				bool flag2 = true;
				int num2 = 0;
				foreach (FactionInterest factionInterest3 in this.Interests)
				{
					if (factionInterest3.Excludes(note, Buy, Sell))
					{
						flag2 = false;
						break;
					}
					if (factionInterest3.Weight > num2)
					{
						num2 = factionInterest3.Weight;
					}
				}
				if (flag2)
				{
					return note.Weight + num2;
				}
			}
			IL_019A:
			if (!Sell || !this.SellRequiresSpecificInterest(note))
			{
				foreach (FactionInterest factionInterest4 in this.Interests)
				{
					if (factionInterest4.Includes(note, Buy, Sell))
					{
						return note.Weight + factionInterest4.Weight;
					}
				}
			}
			return 0;
		}

		// Token: 0x06005491 RID: 21649 RVA: 0x00193A14 File Offset: 0x00191C14
		public bool InterestedIn(IBaseJournalEntry note, ref string becauseOf, bool Buy = false, bool Sell = false)
		{
			if (Buy && note.Has(this.NoBuySecretString))
			{
				becauseOf = "no buy secret string " + this.NoBuySecretString + " on note";
				return false;
			}
			if (Sell && note.Has(this.NoSellSecretString))
			{
				becauseOf = "no sell secret string " + this.NoSellSecretString + " on note";
				return false;
			}
			if (Buy && note.Has(this.GossipSecretString))
			{
				becauseOf = "gossip secret string " + this.GossipSecretString + " on note";
				return true;
			}
			if (Buy && this.BuyTargetedSecrets && note.Has(this.TargetedSecretString))
			{
				becauseOf = "targeted secret string " + this.TargetedSecretString + " on note for buy";
				return true;
			}
			if (Sell && this.SellTargetedSecrets && note.Has(this.TargetedSecretString))
			{
				becauseOf = "targeted secret string " + this.TargetedSecretString + " on note for sell";
				return true;
			}
			foreach (FactionInterest factionInterest in this.Interests)
			{
				if (factionInterest.Includes(note, Buy, Sell))
				{
					becauseOf = "interest " + factionInterest.DebugName;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005492 RID: 21650 RVA: 0x00193B6C File Offset: 0x00191D6C
		public bool UseAltBehavior(GameObject speaker)
		{
			return (!this.WaterRitualAltBehaviorPart.IsNullOrEmpty() && speaker.HasPart(this.WaterRitualAltBehaviorPart)) || (!this.WaterRitualAltBehaviorTag.IsNullOrEmpty() && speaker.HasTagOrProperty(this.WaterRitualAltBehaviorTag));
		}

		// Token: 0x06005493 RID: 21651 RVA: 0x00193BAC File Offset: 0x00191DAC
		public bool HasInterestSameAs(FactionInterest interest)
		{
			using (List<FactionInterest>.Enumerator enumerator = this.Interests.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.SameAs(interest))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005494 RID: 21652 RVA: 0x00193C08 File Offset: 0x00191E08
		public void AddInterestIfNew(FactionInterest interest)
		{
			if (!this.HasInterestSameAs(interest))
			{
				this.Interests.Add(interest);
			}
		}

		// Token: 0x06005495 RID: 21653 RVA: 0x00193C20 File Offset: 0x00191E20
		public static GameObject GenerateHeirloom(string Type)
		{
			int num = "5-6".Roll();
			GameObject gameObject = GameObject.Create(PopulationManager.RollOneFrom(Type + " " + num.ToString(), null, null).Blueprint, 0, 1, null, null, null, null, null);
			gameObject.SetStringProperty("Mods", "None", false);
			string type = RelicGenerator.GetType(gameObject);
			string subtype = RelicGenerator.GetSubtype(type);
			string text = RelicGenerator.SelectElement(gameObject, null, null, null) ?? "might";
			int num2 = 20;
			if (RelicGenerator.ApplyBasicBestowal(gameObject, type, num, subtype, false, false))
			{
				num2 += 20;
			}
			if (50.in100() && !text.IsNullOrEmpty())
			{
				if (RelicGenerator.ApplyElementBestowal(gameObject, text, type, num, subtype))
				{
					num2 += 40;
				}
			}
			else if (RelicGenerator.ApplyBasicBestowal(gameObject, type, num, subtype, false, false))
			{
				num2 += 20;
			}
			string text2 = null;
			string text3 = null;
			if (40.in100())
			{
				List<JournalMapNote> mapNotes = JournalAPI.GetMapNotes((JournalMapNote note) => note.Has("ruins") && !note.Has("historic") && note.Text != "some forgotten ruins");
				if (mapNotes.Count > 0)
				{
					text2 = HistoricStringExpander.ExpandString(string.Concat(new string[]
					{
						"<spice.elements.",
						text,
						".adjectives.!random> ",
						HistoricStringExpander.ExpandString("<spice.itemTypes." + type + ".!random>", null),
						" of ",
						mapNotes.GetRandomElement<JournalMapNote>(null).Text
					}), null);
					text3 = "the";
				}
			}
			if (text2 == null)
			{
				GameObject alegendaryEligibleCreature = EncountersAPI.GetALegendaryEligibleCreature(null);
				HeroMaker.MakeHero(alegendaryEligibleCreature, null, null, -1, "Hero");
				Dictionary<string, string> dictionary = new Dictionary<string, string>
				{
					{ "*element*", text },
					{ "*itemType*", type },
					{
						"*personNounPossessive*",
						Grammar.MakePossessive(HistoricStringExpander.ExpandString("<spice.personNouns.!random>", null))
					},
					{
						"*creatureNamePossessive*",
						Grammar.MakePossessive(alegendaryEligibleCreature.an(int.MaxValue, null, null, false, false, false, false, true, true, true, false, false, true, false, null, false, null, false))
					}
				};
				text2 = HistoricStringExpander.ExpandString("<spice.history.relics.names.!random>", null, null, dictionary, null);
				QudHistoryHelpers.ExtractArticle(ref text2, out text3);
			}
			gameObject.RequirePart<OriginalItemType>(false);
			text2 = QudHistoryHelpers.Ansify(Grammar.MakeTitleCase(text2), null);
			gameObject.GiveProperName(text2, true, null, false, null, null, null, null);
			if (!text3.IsNullOrEmpty())
			{
				gameObject.SetStringProperty("IndefiniteArticle", text3, false);
				gameObject.SetStringProperty("DefiniteArticle", text3, false);
			}
			gameObject.SetImportant(true, false, false);
			Commerce commerce;
			if (num2 != 0 && gameObject.TryGetPart<Commerce>(out commerce))
			{
				commerce.Value += commerce.Value * (double)num2 / 100.0;
			}
			AfterPseudoRelicGeneratedEvent.Send(gameObject, text, type, subtype, num);
			return gameObject;
		}

		// Token: 0x06005496 RID: 21654 RVA: 0x00193EDA File Offset: 0x001920DA
		public GameObject GenerateHeirloom()
		{
			return Faction.GenerateHeirloom(Items.ItemTableNames[this.Heirloom]);
		}

		// Token: 0x06005497 RID: 21655 RVA: 0x00193EF1 File Offset: 0x001920F1
		public void CacheHeirloom()
		{
			this.HeirloomID = ZoneManager.instance.CacheObject(this.GenerateHeirloom(), false, false);
		}

		// Token: 0x06005498 RID: 21656 RVA: 0x00193F0B File Offset: 0x0019210B
		public void RequireCachedHeirloom()
		{
			if (this.HeirloomID.IsNullOrEmpty())
			{
				this.HeirloomID = ZoneManager.instance.CacheObject(this.GenerateHeirloom(), false, false);
			}
		}

		// Token: 0x06005499 RID: 21657 RVA: 0x00193F34 File Offset: 0x00192134
		public GameObject GetHeirloom()
		{
			GameObject gameObject = null;
			string heirloomID = this.HeirloomID;
			if (!heirloomID.IsNullOrEmpty())
			{
				gameObject = ZoneManager.instance.PullCachedObject(heirloomID, true);
			}
			this.CacheHeirloom();
			return gameObject ?? this.GenerateHeirloom();
		}

		// Token: 0x0600549A RID: 21658 RVA: 0x00193F70 File Offset: 0x00192170
		public static bool WantsToBuySecret(string Faction, IBaseJournalEntry Note, GameObject Object = null)
		{
			return Faction.WantsToBuySecret(Factions.Get(Faction), Note, Object);
		}

		// Token: 0x0600549B RID: 21659 RVA: 0x00193F7F File Offset: 0x0019217F
		public bool WantsToBuySecret(IBaseJournalEntry Note, GameObject Object = null)
		{
			return Faction.WantsToBuySecret(this, Note, Object);
		}

		// Token: 0x0600549C RID: 21660 RVA: 0x00193F8C File Offset: 0x0019218C
		public static bool WantsToBuySecret(Faction Faction, IBaseJournalEntry Note, GameObject Object = null)
		{
			if (!Note.Revealed)
			{
				return false;
			}
			if (!Note.Tradable)
			{
				return false;
			}
			JournalMapNote journalMapNote = Note as JournalMapNote;
			return (journalMapNote == null || Object == null || !(journalMapNote.ZoneID == Object.CurrentZone.ZoneID)) && (Object == null || !(Object.GetStringProperty("nosecret", null) == Note.ID)) && ((Object != null && Object.HasPart<Chef>() && Note.Has("recipe")) || Faction.InterestedIn(Note, true, false));
		}

		// Token: 0x0600549D RID: 21661 RVA: 0x0019401C File Offset: 0x0019221C
		public static bool WantsToBuySecret(string faction, IBaseJournalEntry note, GameObject rep, ref string becauseOf)
		{
			if (!note.Revealed)
			{
				becauseOf = "note has not been revealed";
				return false;
			}
			if (!note.Tradable)
			{
				becauseOf = "note has been sold";
				return false;
			}
			JournalMapNote journalMapNote = note as JournalMapNote;
			if (journalMapNote != null && rep != null && journalMapNote.ZoneID == rep.CurrentZone.ZoneID)
			{
				becauseOf = "note is about current zone";
				return false;
			}
			if (rep != null && rep.GetStringProperty("nosecret", null) == note.ID)
			{
				becauseOf = "secret ID " + note.ID + " matching nosecret property on speaker";
				return false;
			}
			if (rep != null && rep.HasPart<Chef>() && note.Has("recipe"))
			{
				becauseOf = "note is a recipe and speaker is a chef";
				return true;
			}
			return Factions.Get(faction).InterestedIn(note, ref becauseOf, true, false);
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x001940E3 File Offset: 0x001922E3
		public int GetBuySecretWeight(IBaseJournalEntry Note, GameObject Object = null)
		{
			return Faction.GetBuySecretWeight(this, Note, Object);
		}

		// Token: 0x0600549F RID: 21663 RVA: 0x001940F0 File Offset: 0x001922F0
		public static int GetBuySecretWeight(Faction Faction, IBaseJournalEntry Note, GameObject Object = null)
		{
			if (!Note.CanSell())
			{
				return 0;
			}
			if (Object != null && Object.GetStringProperty("nosecret", null) == Note.ID)
			{
				return 0;
			}
			int num = Faction.GetInterestIn(Note, true, false);
			if (num < Note.Weight && Object != null && Note is JournalRecipeNote && Object.HasPart(typeof(Chef)))
			{
				num = Note.Weight;
			}
			return num;
		}

		// Token: 0x060054A0 RID: 21664 RVA: 0x0019415D File Offset: 0x0019235D
		public static bool WantsToSellSecret(string Faction, IBaseJournalEntry Note)
		{
			return Faction.WantsToSellSecret(Factions.Get(Faction), Note);
		}

		// Token: 0x060054A1 RID: 21665 RVA: 0x0019416B File Offset: 0x0019236B
		public bool WantsToSellSecret(IBaseJournalEntry Note)
		{
			return Faction.WantsToSellSecret(this, Note);
		}

		// Token: 0x060054A2 RID: 21666 RVA: 0x00194174 File Offset: 0x00192374
		public static bool WantsToSellSecret(Faction Faction, IBaseJournalEntry Note)
		{
			return !Note.Revealed && Faction.InterestedIn(Note, false, true);
		}

		// Token: 0x060054A3 RID: 21667 RVA: 0x0019418E File Offset: 0x0019238E
		public static bool WantsToSellSecret(string faction, IBaseJournalEntry note, ref string becauseOf)
		{
			if (note.Revealed)
			{
				becauseOf = "note has been revealed";
				return false;
			}
			return Factions.Get(faction).InterestedIn(note, ref becauseOf, false, true);
		}

		// Token: 0x060054A4 RID: 21668 RVA: 0x001941B5 File Offset: 0x001923B5
		public int GetSellSecretWeight(IBaseJournalEntry Note)
		{
			return Faction.GetSellSecretWeight(this, Note);
		}

		// Token: 0x060054A5 RID: 21669 RVA: 0x001941BE File Offset: 0x001923BE
		public static int GetSellSecretWeight(Faction Faction, IBaseJournalEntry Note)
		{
			if (!Note.CanBuy())
			{
				return 0;
			}
			return Faction.GetInterestIn(Note, false, true);
		}

		// Token: 0x060054A6 RID: 21670 RVA: 0x001941D4 File Offset: 0x001923D4
		public int GetFactionWorshipAttitude(string Faction)
		{
			int num;
			if (this.FactionWorshipAttitudes.TryGetValue(Faction, out num))
			{
				return num;
			}
			if (this.ApplyDefaultFactionWorshipAttitudeAfterSpecificFeelings)
			{
				if (this.DefaultFactionWorshipAttitudeSet || this.DefaultFactionWorshipAttitude != 0)
				{
					return this.GetFeelingTowardsFaction(Faction, new int?(this.DefaultFactionWorshipAttitude), 0);
				}
			}
			else if (this.DefaultFactionWorshipAttitudeSet || this.DefaultFactionWorshipAttitude != 0)
			{
				return this.DefaultFactionWorshipAttitude;
			}
			return this.GetFeelingTowardsFaction(Faction, null, 0);
		}

		// Token: 0x060054A7 RID: 21671 RVA: 0x00194247 File Offset: 0x00192447
		public int GetFactionBlasphemyAttitude(string Faction)
		{
			return -this.GetFactionWorshipAttitude(Faction);
		}

		// Token: 0x060054A8 RID: 21672 RVA: 0x00194251 File Offset: 0x00192451
		public void ApplyWorshipAttitude(string Faction, int Attitude)
		{
			this.FactionWorshipAttitudes[Faction] = this.GetFactionWorshipAttitude(Faction) + Attitude;
		}

		// Token: 0x060054A9 RID: 21673 RVA: 0x00194268 File Offset: 0x00192468
		public List<Worshippable> GetWorshippables()
		{
			return this.Worshippables;
		}

		// Token: 0x060054AA RID: 21674 RVA: 0x00194270 File Offset: 0x00192470
		public void RegisterWorshippable(GameObject Object)
		{
			if (!GameObject.Validate(ref Object))
			{
				return;
			}
			if (!Object.HasPropertyOrTag("Worshippable"))
			{
				return;
			}
			if (Object.HasIntProperty("WorshipRegistered"))
			{
				return;
			}
			Object.SetIntProperty("WorshipRegistered", 1, false);
			string text = Object.GetPropertyOrTag("WorshippedAs", null) ?? Object.GetReferenceDisplayName(int.MaxValue, null, "Worship", false, true, false, true, false, false, false, false, null, false);
			Worshippable worshippable = null;
			bool flag = false;
			foreach (Worshippable worshippable2 in this.Worshippables)
			{
				if (worshippable2.Name == text)
				{
					worshippable = worshippable2;
					flag = true;
					break;
				}
			}
			if (worshippable == null)
			{
				worshippable = new Worshippable();
				worshippable.Name = text;
				worshippable.Faction = this.Name;
			}
			if (Object.HasID || Object.HasPropertyOrTag("WorshipForceTrackID"))
			{
				string id = Object.ID;
				if (!worshippable.Sources.HasDelimitedSubstring(',', id, StringComparison.Ordinal))
				{
					if (worshippable.Sources == null)
					{
						worshippable.Sources = id;
					}
					else
					{
						Worshippable worshippable3 = worshippable;
						worshippable3.Sources = worshippable3.Sources + "," + id;
					}
				}
			}
			if (worshippable.Blueprints == null)
			{
				worshippable.Blueprints = Object.Blueprint;
			}
			else if (!worshippable.Blueprints.HasDelimitedSubstring(";;", Object.Blueprint, StringComparison.Ordinal))
			{
				Worshippable worshippable4 = worshippable;
				worshippable4.Blueprints = worshippable4.Blueprints + ";;" + Object.Blueprint;
			}
			int num = 0;
			if (Object.HasIntProperty("WorshipPower"))
			{
				num = Object.GetIntProperty("WorshipPower", 0);
			}
			else if (Object.HasTag("WorshipPower"))
			{
				int.TryParse(Object.GetTag("WorshipPower", null), out num);
			}
			if (num > worshippable.Power)
			{
				worshippable.Power = num;
			}
			if (!flag)
			{
				this.Worshippables.Add(worshippable);
			}
		}

		// Token: 0x060054AB RID: 21675 RVA: 0x00194458 File Offset: 0x00192658
		public Worshippable RegisterWorshippable(string Name, int Power = 0)
		{
			Worshippable worshippable = null;
			foreach (Worshippable worshippable2 in this.Worshippables)
			{
				if (worshippable2.Name == Name)
				{
					worshippable = worshippable2;
					break;
				}
			}
			if (worshippable == null)
			{
				worshippable = new Worshippable();
				worshippable.Name = Name;
				worshippable.Faction = this.Name;
				this.Worshippables.Add(worshippable);
			}
			if (Power > worshippable.Power)
			{
				worshippable.Power = Power;
			}
			return worshippable;
		}

		// Token: 0x060054AC RID: 21676 RVA: 0x001944F4 File Offset: 0x001926F4
		public Worshippable FindWorshippable(string Name)
		{
			foreach (Worshippable worshippable in this.Worshippables)
			{
				if (worshippable.Name == Name)
				{
					return worshippable;
				}
			}
			return null;
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x00194558 File Offset: 0x00192758
		public Worshippable FindWorshippable(GameObject Object)
		{
			if (!GameObject.Validate(ref Object))
			{
				return null;
			}
			if (!Object.HasPropertyOrTag("Worshippable"))
			{
				return null;
			}
			if (Object.HasID)
			{
				string id = Object.ID;
				foreach (Worshippable worshippable in this.Worshippables)
				{
					if (worshippable.Sources.HasDelimitedSubstring(',', id, StringComparison.Ordinal))
					{
						return worshippable;
					}
				}
			}
			string text = Object.GetPropertyOrTag("WorshippedAs", null) ?? Object.GetReferenceDisplayName(int.MaxValue, null, "Worship", false, true, false, true, false, false, false, false, null, false);
			foreach (Worshippable worshippable2 in this.Worshippables)
			{
				if (worshippable2.Name == text)
				{
					return worshippable2;
				}
			}
			return null;
		}

		// Token: 0x060054AE RID: 21678 RVA: 0x00194668 File Offset: 0x00192868
		public MemorialTracking QueueMemorial(GameObject Object, bool GenerateNameIfNotNamed = false, string Eulogy = null, string Reason = null, string ThirdPersonReason = null)
		{
			if (!GameObject.Validate(ref Object))
			{
				return null;
			}
			MemorialTracking memorialTracking = new MemorialTracking();
			if (GenerateNameIfNotNamed && !Object.HasProperName)
			{
				memorialTracking.Name = NameMaker.MakeName(Object, null, null, null, null, null, null, null, null, null, null, null, null, false, false, null, null);
			}
			MemorialTracking memorialTracking2 = memorialTracking;
			if (memorialTracking2.Name == null)
			{
				memorialTracking2.Name = Object.GetReferenceDisplayName(int.MaxValue, null, "Memorial", false, true, false, false, false, false, false, false, null, false);
			}
			memorialTracking.Eulogy = Eulogy;
			memorialTracking.Reason = Reason;
			memorialTracking.ThirdPersonReason = ThirdPersonReason;
			if (Object.HasID)
			{
				memorialTracking.ID = Object.ID;
			}
			memorialTracking.Blueprint = Object.Blueprint;
			memorialTracking.Queued = The.CurrentTurn;
			if (this.QueuedMemorials == null)
			{
				this.QueuedMemorials = new List<MemorialTracking>();
			}
			this.QueuedMemorials.Add(memorialTracking);
			return memorialTracking;
		}

		// Token: 0x060054AF RID: 21679 RVA: 0x0019474C File Offset: 0x0019294C
		public MemorialTracking MemorialPerformed(MemorialTracking Memorial, Cell Cell = null)
		{
			List<MemorialTracking> queuedMemorials = this.QueuedMemorials;
			if (queuedMemorials != null)
			{
				queuedMemorials.Remove(Memorial);
			}
			Memorial.Performed = The.CurrentTurn;
			if (Cell != null)
			{
				string[] array = new string[5];
				int num = 0;
				Zone parentZone = Cell.ParentZone;
				array[num] = ((parentZone != null) ? parentZone.ZoneID : null);
				array[1] = "@";
				array[2] = Cell.X.ToString();
				array[3] = ",";
				array[4] = Cell.Y.ToString();
				Memorial.Location = string.Concat(array);
			}
			if (this.PerformedMemorials == null)
			{
				this.PerformedMemorials = new List<MemorialTracking>();
			}
			this.PerformedMemorials.Add(Memorial);
			return Memorial;
		}

		// Token: 0x060054B0 RID: 21680 RVA: 0x001947EC File Offset: 0x001929EC
		public bool HasQueuedMemorialByName(string Name)
		{
			if (this.QueuedMemorials != null)
			{
				using (List<MemorialTracking>.Enumerator enumerator = this.QueuedMemorials.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Name == Name)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060054B1 RID: 21681 RVA: 0x00194854 File Offset: 0x00192A54
		public bool HasQueuedMemorialByID(string ID)
		{
			if (this.QueuedMemorials != null)
			{
				using (List<MemorialTracking>.Enumerator enumerator = this.QueuedMemorials.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.ID == ID)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x001948BC File Offset: 0x00192ABC
		public bool HasQueuedMemorialByBlueprint(string Blueprint)
		{
			if (this.QueuedMemorials != null)
			{
				using (List<MemorialTracking>.Enumerator enumerator = this.QueuedMemorials.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Blueprint == Blueprint)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060054B3 RID: 21683 RVA: 0x00194924 File Offset: 0x00192B24
		public bool HasPerformedMemorialByName(string Name)
		{
			if (this.PerformedMemorials != null)
			{
				using (List<MemorialTracking>.Enumerator enumerator = this.PerformedMemorials.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Name == Name)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060054B4 RID: 21684 RVA: 0x0019498C File Offset: 0x00192B8C
		public bool HasPerformedMemorialByID(string ID)
		{
			if (this.PerformedMemorials != null)
			{
				using (List<MemorialTracking>.Enumerator enumerator = this.PerformedMemorials.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.ID == ID)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060054B5 RID: 21685 RVA: 0x001949F4 File Offset: 0x00192BF4
		public bool HasPerformedMemorialByBlueprint(string Blueprint)
		{
			if (this.PerformedMemorials != null)
			{
				using (List<MemorialTracking>.Enumerator enumerator = this.PerformedMemorials.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Blueprint == Blueprint)
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060054B6 RID: 21686 RVA: 0x00194A5C File Offset: 0x00192C5C
		public MemorialTracking GetQueuedMemorialByName(string Name)
		{
			if (this.QueuedMemorials != null)
			{
				foreach (MemorialTracking memorialTracking in this.QueuedMemorials)
				{
					if (memorialTracking.Name == Name)
					{
						return memorialTracking;
					}
				}
			}
			return null;
		}

		// Token: 0x060054B7 RID: 21687 RVA: 0x00194AC8 File Offset: 0x00192CC8
		public MemorialTracking GetQueuedMemorialByID(string ID)
		{
			if (this.QueuedMemorials != null)
			{
				foreach (MemorialTracking memorialTracking in this.QueuedMemorials)
				{
					if (memorialTracking.ID == ID)
					{
						return memorialTracking;
					}
				}
			}
			return null;
		}

		// Token: 0x060054B8 RID: 21688 RVA: 0x00194B34 File Offset: 0x00192D34
		public MemorialTracking GetQueuedMemorialByBlueprint(string Blueprint)
		{
			if (this.QueuedMemorials == null)
			{
				foreach (MemorialTracking memorialTracking in this.QueuedMemorials)
				{
					if (memorialTracking.Blueprint == Blueprint)
					{
						return memorialTracking;
					}
				}
			}
			return null;
		}

		// Token: 0x060054B9 RID: 21689 RVA: 0x00194BA0 File Offset: 0x00192DA0
		public MemorialTracking GetPerformedMemorialByName(string Name)
		{
			if (this.PerformedMemorials != null)
			{
				foreach (MemorialTracking memorialTracking in this.PerformedMemorials)
				{
					if (memorialTracking.Name == Name)
					{
						return memorialTracking;
					}
				}
			}
			return null;
		}

		// Token: 0x060054BA RID: 21690 RVA: 0x00194C0C File Offset: 0x00192E0C
		public MemorialTracking GetPerformedMemorialByID(string ID)
		{
			if (this.PerformedMemorials != null)
			{
				foreach (MemorialTracking memorialTracking in this.PerformedMemorials)
				{
					if (memorialTracking.ID == ID)
					{
						return memorialTracking;
					}
				}
			}
			return null;
		}

		// Token: 0x060054BB RID: 21691 RVA: 0x00194C78 File Offset: 0x00192E78
		public MemorialTracking GetPerformedMemorialByBlueprint(string Blueprint)
		{
			if (this.PerformedMemorials == null)
			{
				foreach (MemorialTracking memorialTracking in this.PerformedMemorials)
				{
					if (memorialTracking.Blueprint == Blueprint)
					{
						return memorialTracking;
					}
				}
			}
			return null;
		}

		// Token: 0x060054BC RID: 21692 RVA: 0x00194CE4 File Offset: 0x00192EE4
		public List<GameObjectBlueprint> GetMembers(Predicate<GameObjectBlueprint> Predicate = null, bool Dynamic = true, bool ReadOnly = true)
		{
			return Faction.GetMembers(this.Name, Predicate, Dynamic, ReadOnly);
		}

		// Token: 0x060054BD RID: 21693 RVA: 0x00194CF4 File Offset: 0x00192EF4
		public static List<GameObjectBlueprint> GetMembers(string FactionName, Predicate<GameObjectBlueprint> Predicate = null, bool Dynamic = true, bool ReadOnly = true)
		{
			List<GameObjectBlueprint> list = (ReadOnly ? Faction.MemberCache : new List<GameObjectBlueprint>());
			list.Clear();
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (!gameObjectBlueprint.IsBaseBlueprint() && (!Dynamic || !gameObjectBlueprint.IsExcludedFromDynamicEncounters()) && Faction.IsMember(gameObjectBlueprint, FactionName) && (Predicate == null || Predicate(gameObjectBlueprint)))
				{
					list.Add(gameObjectBlueprint);
				}
			}
			return list;
		}

		// Token: 0x060054BE RID: 21694 RVA: 0x00194D8C File Offset: 0x00192F8C
		public bool AnyMembers(Predicate<GameObjectBlueprint> Predicate = null, bool Dynamic = true)
		{
			return Faction.AnyMembers(this.Name, Predicate, Dynamic);
		}

		// Token: 0x060054BF RID: 21695 RVA: 0x00194D9C File Offset: 0x00192F9C
		public static bool AnyMembers(string FactionName, Predicate<GameObjectBlueprint> Predicate = null, bool Dynamic = true)
		{
			foreach (GameObjectBlueprint gameObjectBlueprint in GameObjectFactory.Factory.BlueprintList)
			{
				if (!gameObjectBlueprint.IsBaseBlueprint() && (!Dynamic || !gameObjectBlueprint.IsExcludedFromDynamicEncounters()) && Faction.IsMember(gameObjectBlueprint, FactionName) && (Predicate == null || Predicate(gameObjectBlueprint)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x00194E1C File Offset: 0x0019301C
		public bool IsMember(GameObjectBlueprint Blueprint)
		{
			return Faction.IsMember(Blueprint, this.Name);
		}

		// Token: 0x060054C1 RID: 21697 RVA: 0x00194E2C File Offset: 0x0019302C
		public static bool IsMember(GameObjectBlueprint Blueprint, string FactionName)
		{
			string text;
			if (Blueprint.Tags.TryGetValue("AssociatedFactions", out text) && text.HasDelimitedSubstring(',', FactionName, StringComparison.Ordinal))
			{
				return true;
			}
			string text2;
			if (Blueprint.TryGetPartParameter<string>("Brain", "Factions", out text2) && !text2.IsNullOrEmpty())
			{
				int num = text2.Length - 1;
				int i = -1;
				while (i < num)
				{
					i = text2.IndexOf(FactionName, i + 1, StringComparison.Ordinal);
					if (i == -1)
					{
						break;
					}
					if (i <= 0 || text2[i - 1] == ',')
					{
						int num2 = i + FactionName.Length;
						if (num2 - 1 >= num || text2[num2] == '-')
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x04002A5A RID: 10842
		public const string HEIRLOOM_TIER = "5-6";

		// Token: 0x04002A5B RID: 10843
		public int ID;

		// Token: 0x04002A5C RID: 10844
		public FactionEmblem Emblem = new FactionEmblem();

		// Token: 0x04002A5D RID: 10845
		public bool Visible = true;

		// Token: 0x04002A5E RID: 10846
		public bool Old = true;

		// Token: 0x04002A5F RID: 10847
		public bool HatesPlayer;

		// Token: 0x04002A60 RID: 10848
		public bool Pettable;

		// Token: 0x04002A61 RID: 10849
		public bool Plural = true;

		// Token: 0x04002A62 RID: 10850
		public bool ExtradimensionalVersions = true;

		// Token: 0x04002A63 RID: 10851
		public bool FormatWithArticle;

		// Token: 0x04002A64 RID: 10852
		public string WaterRitualLiquid;

		// Token: 0x04002A65 RID: 10853
		public string WaterRitualSkill;

		// Token: 0x04002A66 RID: 10854
		public int WaterRitualSkillCost = -1;

		// Token: 0x04002A67 RID: 10855
		public bool WaterRitualBuyMostValuableItem;

		// Token: 0x04002A68 RID: 10856
		public int WaterRitualFungusInfect = -1;

		// Token: 0x04002A69 RID: 10857
		public int WaterRitualHermitOath = -1;

		// Token: 0x04002A6A RID: 10858
		public int WaterRitualSkillPointAmount = -1;

		// Token: 0x04002A6B RID: 10859
		public int WaterRitualSkillPointCost = -1;

		// Token: 0x04002A6C RID: 10860
		public string WaterRitualMutation;

		// Token: 0x04002A6D RID: 10861
		public int WaterRitualMutationCost = -1;

		// Token: 0x04002A6E RID: 10862
		public string WaterRitualGifts;

		// Token: 0x04002A6F RID: 10863
		public string WaterRitualItems;

		// Token: 0x04002A70 RID: 10864
		public string WaterRitualItemBlueprint;

		// Token: 0x04002A71 RID: 10865
		public int WaterRitualItemCost = -1;

		// Token: 0x04002A72 RID: 10866
		public string WaterRitualBlueprints;

		// Token: 0x04002A73 RID: 10867
		public string WaterRitualRecipe;

		// Token: 0x04002A74 RID: 10868
		public string WaterRitualRecipeText;

		// Token: 0x04002A75 RID: 10869
		public string WaterRitualRecipeGenotype;

		// Token: 0x04002A76 RID: 10870
		public bool WaterRitualJoin = true;

		// Token: 0x04002A77 RID: 10871
		public int WaterRitualRandomMentalMutation = -1;

		// Token: 0x04002A78 RID: 10872
		public string WaterRitualAltBehaviorPart;

		// Token: 0x04002A79 RID: 10873
		public string WaterRitualAltBehaviorTag;

		// Token: 0x04002A7A RID: 10874
		public string WaterRitualAltLiquid;

		// Token: 0x04002A7B RID: 10875
		public string WaterRitualAltSkill;

		// Token: 0x04002A7C RID: 10876
		public int WaterRitualAltSkillCost = -1;

		// Token: 0x04002A7D RID: 10877
		public string WaterRitualAltGifts;

		// Token: 0x04002A7E RID: 10878
		public string WaterRitualAltItems;

		// Token: 0x04002A7F RID: 10879
		public string WaterRitualAltItemBlueprint;

		// Token: 0x04002A80 RID: 10880
		public int WaterRitualAltItemCost = -1;

		// Token: 0x04002A81 RID: 10881
		public string WaterRitualAltBlueprints;

		// Token: 0x04002A82 RID: 10882
		public string Name = "?";

		// Token: 0x04002A83 RID: 10883
		public string _DisplayName;

		// Token: 0x04002A84 RID: 10884
		public string PositiveSound = "sfx_reputation_base_positive";

		// Token: 0x04002A85 RID: 10885
		public string NegativeSound = "sfx_reputation_base_negative";

		// Token: 0x04002A86 RID: 10886
		public int HistoricalSignificance;

		/// <summary>The associated historic entity.</summary>
		// Token: 0x04002A87 RID: 10887
		public string EntityID;

		/// <summary>The founding historic event.</summary>
		// Token: 0x04002A88 RID: 10888
		public long EventID;

		// Token: 0x04002A89 RID: 10889
		public string DefaultAddress;

		// Token: 0x04002A8A RID: 10890
		public string RankTerm = "rank";

		// Token: 0x04002A8B RID: 10891
		public List<string> Ranks = new List<string>();

		// Token: 0x04002A8C RID: 10892
		public Dictionary<string, int> RankStandings = new Dictionary<string, int>();

		// Token: 0x04002A8D RID: 10893
		public List<Worshippable> Worshippables = new List<Worshippable>();

		// Token: 0x04002A8E RID: 10894
		public int DefaultFactionWorshipAttitude;

		// Token: 0x04002A8F RID: 10895
		public bool DefaultFactionWorshipAttitudeSet;

		// Token: 0x04002A90 RID: 10896
		public bool ApplyDefaultFactionWorshipAttitudeAfterSpecificFeelings;

		// Token: 0x04002A91 RID: 10897
		public Dictionary<string, int> FactionWorshipAttitudes = new Dictionary<string, int>();

		// Token: 0x04002A92 RID: 10898
		public List<MemorialTracking> QueuedMemorials;

		// Token: 0x04002A93 RID: 10899
		public List<MemorialTracking> PerformedMemorials;

		// Token: 0x04002A94 RID: 10900
		public Dictionary<string, object> Properties;

		// Token: 0x04002A95 RID: 10901
		public Dictionary<string, int> IntProperties;

		// Token: 0x04002A96 RID: 10902
		public string Parent;

		// Token: 0x04002A97 RID: 10903
		public Dictionary<string, int> FactionFeeling = new Dictionary<string, int>();

		// Token: 0x04002A98 RID: 10904
		public int InitialPlayerReputation;

		// Token: 0x04002A99 RID: 10905
		public Dictionary<string, int> PartReputation = new Dictionary<string, int>();

		// Token: 0x04002A9A RID: 10906
		public List<FactionInterest> Interests = new List<FactionInterest>();

		// Token: 0x04002A9B RID: 10907
		public List<string> HolyPlaces = new List<string>();

		// Token: 0x04002A9C RID: 10908
		public bool BuyTargetedSecrets = true;

		// Token: 0x04002A9D RID: 10909
		public bool SellTargetedSecrets = true;

		// Token: 0x04002A9E RID: 10910
		public string BuyDescription;

		// Token: 0x04002A9F RID: 10911
		public string SellDescription;

		// Token: 0x04002AA0 RID: 10912
		public string BothDescription;

		// Token: 0x04002AA1 RID: 10913
		public string InterestsBlurb;

		// Token: 0x04002AA2 RID: 10914
		private string _TargetedSecretString;

		// Token: 0x04002AA3 RID: 10915
		private string _GossipSecretString;

		// Token: 0x04002AA4 RID: 10916
		private string _NoBuySecretString;

		// Token: 0x04002AA5 RID: 10917
		private string _NoSellSecretString;

		// Token: 0x04002AA6 RID: 10918
		[NonSerialized]
		private static List<string> BuyOnly = new List<string>(8);

		// Token: 0x04002AA7 RID: 10919
		[NonSerialized]
		private static List<string> SellOnly = new List<string>(8);

		// Token: 0x04002AA8 RID: 10920
		[NonSerialized]
		private static List<string> Both = new List<string>(8);

		// Token: 0x04002AA9 RID: 10921
		private static List<GameObjectBlueprint> MemberCache = new List<GameObjectBlueprint>();
	}
}
