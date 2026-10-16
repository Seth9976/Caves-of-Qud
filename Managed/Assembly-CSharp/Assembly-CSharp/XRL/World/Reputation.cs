using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using HistoryKit;
using Occult.Engine.CodeGeneration;
using Qud.API;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000853 RID: 2131
	[GenerateSerializationPartial]
	[Serializable]
	public class Reputation : IComposite
	{
		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060054E9 RID: 21737 RVA: 0x0019618C File Offset: 0x0019438C
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060054EA RID: 21738 RVA: 0x0019618F File Offset: 0x0019438F
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Write(SerializationWriter Writer)
		{
			Writer.Write<string, float>(this.ReputationValues);
			Writer.Write<string, string>(this.FactionRanks);
			Writer.WriteComposite<WorshipTracking>(this.WorshipTracking);
			Writer.WriteComposite<WorshipTracking>(this.BlasphemyTracking);
		}

		// Token: 0x060054EB RID: 21739 RVA: 0x001961C1 File Offset: 0x001943C1
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual void Read(SerializationReader Reader)
		{
			this.ReputationValues = Reader.ReadDictionary<string, float>();
			this.FactionRanks = Reader.ReadDictionary<string, string>();
			this.WorshipTracking = Reader.ReadCompositeList<WorshipTracking>();
			this.BlasphemyTracking = Reader.ReadCompositeList<WorshipTracking>();
		}

		// Token: 0x060054EC RID: 21740 RVA: 0x001961F4 File Offset: 0x001943F4
		public string GetFactionRank(string Faction)
		{
			string text;
			this.FactionRanks.TryGetValue(Faction, out text);
			return GetFactionRankEvent.GetFor(The.Player, Faction, text);
		}

		// Token: 0x060054ED RID: 21741 RVA: 0x0019621C File Offset: 0x0019441C
		public void SetFactionRank(string FactionName, string Rank, bool Message = false, bool Capitalize = true)
		{
			this.FactionRanks[FactionName] = Rank;
			if (Message)
			{
				Popup.Show(string.Concat(new string[]
				{
					"You are promoted to the ",
					Faction.GetRankTerm(FactionName),
					" of ",
					Capitalize ? Rank.Capitalize() : Rank,
					"."
				}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x060054EE RID: 21742 RVA: 0x00196285 File Offset: 0x00194485
		public bool PromoteIfBelow(string Faction, string Rank, bool Message = true, bool Capitalize = true)
		{
			if (this.IsBelowRank(Faction, Rank))
			{
				this.SetFactionRank(Faction, Rank, Message, Capitalize);
				return true;
			}
			return false;
		}

		// Token: 0x060054EF RID: 21743 RVA: 0x001962A0 File Offset: 0x001944A0
		public int GetFactionStanding(string Faction)
		{
			string text;
			if (this.FactionRanks.TryGetValue(Faction, out text))
			{
				return Factions.Get(Faction).GetRankStanding(text);
			}
			return 0;
		}

		// Token: 0x060054F0 RID: 21744 RVA: 0x001962CC File Offset: 0x001944CC
		public bool IsBelowRank(string Faction, string Rank)
		{
			string factionRank = this.GetFactionRank(Faction);
			if (factionRank == null)
			{
				return true;
			}
			if (factionRank == Rank)
			{
				return false;
			}
			Faction faction = Factions.Get(Faction);
			return faction.GetRankStanding(factionRank) < faction.GetRankStanding(Rank);
		}

		// Token: 0x060054F1 RID: 21745 RVA: 0x00196308 File Offset: 0x00194508
		public bool IsAtLeastRank(string Faction, string Rank)
		{
			return !this.IsBelowRank(Faction, Rank);
		}

		// Token: 0x060054F2 RID: 21746 RVA: 0x00196315 File Offset: 0x00194515
		public bool IsBelowStanding(string Faction, int Standing)
		{
			return this.GetFactionStanding(Faction) < Standing;
		}

		// Token: 0x060054F3 RID: 21747 RVA: 0x00196321 File Offset: 0x00194521
		public bool IsAtLeastStanding(string Faction, int Standing)
		{
			return !this.IsBelowStanding(Faction, Standing);
		}

		// Token: 0x060054F4 RID: 21748 RVA: 0x0019632E File Offset: 0x0019452E
		public bool Any(string Faction)
		{
			return this.ReputationValues.ContainsKey(Faction);
		}

		// Token: 0x060054F5 RID: 21749 RVA: 0x0019633C File Offset: 0x0019453C
		public int Get(Faction Faction)
		{
			if (Faction == null)
			{
				return 0;
			}
			float num;
			int num2;
			if (this.ReputationValues.TryGetValue(Faction.Name, out num))
			{
				num2 = (int)num;
			}
			else
			{
				num2 = this.GetDefaultRep(Faction.Name);
			}
			GameObject player = The.Player;
			if (GameObject.Validate(player))
			{
				if (Faction.PartReputation != null)
				{
					foreach (KeyValuePair<string, int> keyValuePair in Faction.PartReputation)
					{
						if (player.HasPart(keyValuePair.Key))
						{
							num2 += keyValuePair.Value;
						}
					}
				}
				if (Faction.Visible)
				{
					num2 += player.GetIntProperty("AllVisibleRepModifier", 0);
				}
			}
			return num2;
		}

		// Token: 0x060054F6 RID: 21750 RVA: 0x001963FC File Offset: 0x001945FC
		public int GetLevel(string Faction)
		{
			int num = this.Get(Faction);
			if (num >= RuleSettings.REPUTATION_LOVED)
			{
				return 2;
			}
			if (num >= RuleSettings.REPUTATION_LIKED)
			{
				return 1;
			}
			if (num > RuleSettings.REPUTATION_DISLIKED)
			{
				return 0;
			}
			if (num > RuleSettings.REPUTATION_HATED)
			{
				return -1;
			}
			return -2;
		}

		// Token: 0x060054F7 RID: 21751 RVA: 0x0019643B File Offset: 0x0019463B
		public int Get(string Faction)
		{
			return this.Get(Factions.Get(Faction));
		}

		// Token: 0x060054F8 RID: 21752 RVA: 0x00196449 File Offset: 0x00194649
		public int Set(Faction Faction, int Rep)
		{
			this.ReputationValues[Faction.Name] = (float)Rep;
			Faction.FactionFeeling["Player"] = this.GetFeeling(Faction.Name);
			return Rep;
		}

		// Token: 0x060054F9 RID: 21753 RVA: 0x0019647B File Offset: 0x0019467B
		public int Set(string Faction, int Rep)
		{
			return this.Set(Factions.Get(Faction), Rep);
		}

		// Token: 0x060054FA RID: 21754 RVA: 0x0019648C File Offset: 0x0019468C
		public void FinishModify()
		{
			int num = 0;
			if (!Reputation.PositiveSounds.IsNullOrEmpty<string>())
			{
				if (Reputation.PositiveSounds.Count > 2)
				{
					SoundManager.PlaySound("Sounds/Reputation/sfx_reputation_cacophonic_positive", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, (float)num * 1.3f);
					num++;
				}
				else
				{
					int i = 0;
					int count = Reputation.PositiveSounds.Count;
					while (i < count)
					{
						SoundManager.PlaySound(Reputation.PositiveSounds[i], 0f, 1f, 1f, SoundRequest.SoundEffectType.None, (float)num * 1.3f);
						num++;
						i++;
					}
				}
			}
			if (!Reputation.NegativeSounds.IsNullOrEmpty<string>())
			{
				if (Reputation.NegativeSounds.Count > 2)
				{
					SoundManager.PlaySound("Sounds/Reputation/sfx_reputation_cacophonic_negative", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, (float)num * 1.3f);
					num++;
				}
				else
				{
					int j = 0;
					int count2 = Reputation.NegativeSounds.Count;
					while (j < count2)
					{
						SoundManager.PlaySound(Reputation.NegativeSounds[j], 0f, 1f, 1f, SoundRequest.SoundEffectType.None, (float)num * 1.3f);
						num++;
						j++;
					}
				}
			}
			Reputation.PositiveSounds.Clear();
			Reputation.NegativeSounds.Clear();
		}

		// Token: 0x060054FB RID: 21755 RVA: 0x001965BC File Offset: 0x001947BC
		public float Modify(Faction Faction, int Amount, string Because = null, StringBuilder PutMessage = null, string Type = null, bool Silent = false, bool Transient = false, bool SingleLine = false, bool Multiple = false)
		{
			string name = Faction.Name;
			if (!this.ReputationValues.ContainsKey(name))
			{
				this.ReputationValues.Add(name, (float)this.GetDefaultRep(name));
			}
			int @for = ReputationChangeEvent.GetFor(Faction, Amount, Type, Silent, Transient, false);
			if (@for == 0)
			{
				return (float)this.Get(name);
			}
			int num = this.Get(name);
			Dictionary<string, float> reputationValues = this.ReputationValues;
			string text = name;
			reputationValues[text] += (float)@for;
			Faction.FactionFeeling["Player"] = this.GetFeeling(Faction.Name);
			int num2 = this.Get(name);
			char color = Reputation.GetColor(num2);
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			string text2 = null;
			bool flag6 = false;
			if (@for > 0)
			{
				if (num < RuleSettings.REPUTATION_LOVED && num2 >= RuleSettings.REPUTATION_LOVED)
				{
					flag = true;
					text2 = "loved";
				}
				else if (num < RuleSettings.REPUTATION_LIKED && num2 >= RuleSettings.REPUTATION_LIKED)
				{
					flag4 = true;
					text2 = "favored";
				}
				else if (num <= RuleSettings.REPUTATION_DISLIKED && num2 > RuleSettings.REPUTATION_DISLIKED)
				{
					flag5 = true;
					text2 = "indifferent";
					flag6 = true;
				}
				else if (num <= RuleSettings.REPUTATION_HATED && num2 > RuleSettings.REPUTATION_HATED)
				{
					flag3 = true;
					text2 = "disliked";
				}
			}
			else if (num > RuleSettings.REPUTATION_HATED && num2 <= RuleSettings.REPUTATION_HATED)
			{
				flag2 = true;
				text2 = "despised";
			}
			else if (num > RuleSettings.REPUTATION_DISLIKED && num2 <= RuleSettings.REPUTATION_DISLIKED)
			{
				flag3 = true;
				text2 = "disliked";
			}
			else if (num >= RuleSettings.REPUTATION_LIKED && num2 < RuleSettings.REPUTATION_LIKED)
			{
				flag5 = true;
				text2 = "indifferent";
				flag6 = true;
			}
			else if (num >= RuleSettings.REPUTATION_LOVED && num2 < RuleSettings.REPUTATION_LOVED)
			{
				flag4 = true;
				text2 = "favored";
			}
			if (Faction.Visible)
			{
				if (!Silent)
				{
					if (num2 > num && !Faction.PositiveSound.IsNullOrEmpty())
					{
						if (Reputation.PositiveSounds == null)
						{
							Reputation.PositiveSounds = new List<string>();
						}
						Reputation.PositiveSounds.Add(Faction.PositiveSound);
					}
					if (num2 < num && !Faction.NegativeSound.IsNullOrEmpty())
					{
						if (Reputation.NegativeSounds == null)
						{
							Reputation.NegativeSounds = new List<string>();
						}
						Reputation.NegativeSounds.Add(Faction.NegativeSound);
					}
					StringBuilder stringBuilder = PutMessage ?? Event.NewStringBuilder(null);
					if (!Because.IsNullOrEmpty())
					{
						stringBuilder.Append(Because.Capitalize()).Append(", your ");
					}
					else
					{
						stringBuilder.Append("Your ");
					}
					stringBuilder.Append("reputation with {{C|").Append(Faction.GetFormattedName()).Append("}} ")
						.Append((@for >= 0) ? "increased" : "decreased")
						.Append(" by {{")
						.Append((@for >= 0) ? 'G' : 'R')
						.Append('|')
						.Append(Math.Abs(@for))
						.Append("}} to {{")
						.Append(color)
						.Append('|')
						.Append(num2)
						.Append("}}.");
					if (text2 != null)
					{
						if (flag6)
						{
							stringBuilder.Append(SingleLine ? ' ' : "\n\n").Append(Faction.GetFormattedName().Capitalize()).Append(' ')
								.Append(Faction.Plural ? "are" : "is")
								.Append(" now {{")
								.Append(color)
								.Append("|")
								.Append(text2)
								.Append("}} to you.");
						}
						else
						{
							stringBuilder.Append(SingleLine ? ' ' : "\n\n").Append("You are now {{").Append(color)
								.Append("|")
								.Append(text2)
								.Append("}} by {{C|")
								.Append(Faction.GetFormattedName())
								.Append("}}.");
						}
					}
					if (PutMessage == null)
					{
						this.FinishModify();
						Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else
					{
						PutMessage.Append('\n');
					}
				}
				if (flag && The.Player != null)
				{
					if (!Faction.HasProperty("BecameLoved"))
					{
						JournalAPI.AddAccomplishment("You became loved among " + Faction.GetFormattedName() + " and were treated as one of their own.", string.Concat(new string[]
						{
							"While wandering around ",
							Grammar.InitLowerIfArticle(Grammar.GetProsaicZoneName(The.Player.CurrentZone)),
							", =name= stumbled upon a clan of ",
							Faction.GetFormattedName(),
							" performing a secret ritual. Because of ",
							The.Player.GetPronounProvider(false).PossessiveAdjective,
							" ",
							HistoricStringExpander.ExpandString("<spice.elements." + IComponent<GameObject>.ThePlayerMythDomain + ".quality.!random>", null),
							", they accepted ",
							The.Player.GetPronounProvider(false).Objective,
							" into their fold and taught ",
							The.Player.GetPronounProvider(false).Objective,
							" their secrets."
						}), string.Concat(new string[]
						{
							"Deep in the wilds of ",
							Grammar.InitLowerIfArticle(Grammar.GetProsaicZoneName(The.Player.CurrentZone)),
							", =name= stumbled upon a clan of ",
							Faction.GetFormattedName(),
							" performing a secret ritual. Because of ",
							Grammar.MakePossessive(The.Player.BaseDisplayNameStripped),
							" <spice.elements.",
							The.Player.GetMythicDomain(5, null),
							".quality.!random>, they accepted ",
							The.Player.GetPronounProvider(false).Objective,
							" into their fold and taught ",
							The.Player.GetPronounProvider(false).Objective,
							" their secrets."
						}), null, "general", MuralCategory.BecomesLoved, MuralWeight.Medium, null, -1L, true);
						Faction.SetProperty("BecameLoved", 1);
					}
					Achievement.LOVED_BY_FACTION.Unlock();
					if (name == "Newly Sentient Beings")
					{
						Achievement.LOVED_BY_NEW_BEINGS.Unlock();
					}
				}
				if (flag2 && name == "Joppa")
				{
					Achievement.HATED_BY_JOPPA.Unlock();
				}
			}
			if (The.Player != null && The.Player.HasRegisteredEvent("ReputationChanged"))
			{
				StringBuilder stringBuilder2 = PutMessage ?? Event.NewStringBuilder(null);
				Event @event = Event.New("ReputationChanged", 0, 0, 0);
				@event.SetParameter("Actor", The.Player);
				@event.SetParameter("Faction", name);
				@event.SetParameter("OldReputation", num);
				@event.SetParameter("NewReputation", num2);
				@event.SetParameter("Type", Type);
				@event.SetParameter("Because", Because);
				@event.SetParameter("Message", stringBuilder2);
				@event.SetFlag("BecameLoved", flag);
				@event.SetFlag("BecameLiked", flag4);
				@event.SetFlag("BecameIndifferent", flag5);
				@event.SetFlag("BecameDisliked", flag3);
				@event.SetFlag("BecameHated", flag2);
				The.Player.FireEvent(@event);
				if (PutMessage == null && stringBuilder2.Length > 0)
				{
					this.FinishModify();
					Popup.Show(stringBuilder2.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			if (!Multiple)
			{
				this.FinishModify();
			}
			AfterReputationChangeEvent.Send(Faction, num, num2, Type, Silent, Transient);
			return (float)this.Get(Faction);
		}

		// Token: 0x060054FC RID: 21756 RVA: 0x00196CF8 File Offset: 0x00194EF8
		public float Modify(string Faction, int Amount, string Type = null, string Because = null, StringBuilder PutMessage = null, bool Silent = false, bool Transient = false)
		{
			return this.Modify(Factions.Get(Faction), Amount, Because, PutMessage, Type, Silent, Transient, false, false);
		}

		// Token: 0x060054FD RID: 21757 RVA: 0x00196D20 File Offset: 0x00194F20
		public float Modify(string Faction, int Amount, bool DisplayMessage)
		{
			return this.Modify(Factions.Get(Faction), Amount, null, null, null, !DisplayMessage, false, false, false);
		}

		// Token: 0x060054FE RID: 21758 RVA: 0x00196D44 File Offset: 0x00194F44
		public bool Use(string Faction, int Amount)
		{
			if (this.Get(Faction) < Amount)
			{
				return false;
			}
			this.Modify(Faction, -Amount, null, null, null, false, false);
			return true;
		}

		// Token: 0x060054FF RID: 21759 RVA: 0x00196D62 File Offset: 0x00194F62
		public static int GetAttitude(int Rep)
		{
			if (Rep <= RuleSettings.REPUTATION_HATED)
			{
				return -2;
			}
			if (Rep <= RuleSettings.REPUTATION_DISLIKED)
			{
				return -1;
			}
			if (Rep < RuleSettings.REPUTATION_LIKED)
			{
				return 0;
			}
			if (Rep < RuleSettings.REPUTATION_LOVED)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x06005500 RID: 21760 RVA: 0x00196D8E File Offset: 0x00194F8E
		public int GetAttitude(Faction Faction)
		{
			return Reputation.GetAttitude(this.Get(Faction));
		}

		// Token: 0x06005501 RID: 21761 RVA: 0x00196D9C File Offset: 0x00194F9C
		public int GetAttitude(string Faction)
		{
			return Reputation.GetAttitude(this.Get(Factions.Get(Faction)));
		}

		// Token: 0x06005502 RID: 21762 RVA: 0x00196DAF File Offset: 0x00194FAF
		public static int GetTradePerformance(int Rep)
		{
			if (Rep <= RuleSettings.REPUTATION_HATED)
			{
				return -3;
			}
			if (Rep <= RuleSettings.REPUTATION_DISLIKED)
			{
				return -1;
			}
			if (Rep < RuleSettings.REPUTATION_LIKED)
			{
				return 0;
			}
			if (Rep < RuleSettings.REPUTATION_LOVED)
			{
				return 1;
			}
			return 3;
		}

		// Token: 0x06005503 RID: 21763 RVA: 0x00196DDB File Offset: 0x00194FDB
		public int GetTradePerformance(Faction Faction)
		{
			return Reputation.GetTradePerformance(this.Get(Faction));
		}

		// Token: 0x06005504 RID: 21764 RVA: 0x00196DE9 File Offset: 0x00194FE9
		public int GetTradePerformance(string Faction)
		{
			return this.GetTradePerformance(Factions.Get(Faction));
		}

		// Token: 0x06005505 RID: 21765 RVA: 0x00196DF7 File Offset: 0x00194FF7
		public static char GetColor(int Rep)
		{
			if (Rep <= RuleSettings.REPUTATION_HATED)
			{
				return 'R';
			}
			if (Rep <= RuleSettings.REPUTATION_DISLIKED)
			{
				return 'r';
			}
			if (Rep < RuleSettings.REPUTATION_LIKED)
			{
				return 'C';
			}
			if (Rep < RuleSettings.REPUTATION_LOVED)
			{
				return 'g';
			}
			return 'G';
		}

		// Token: 0x06005506 RID: 21766 RVA: 0x00196E27 File Offset: 0x00195027
		public char GetColor(Faction Faction)
		{
			return Reputation.GetColor(this.Get(Faction));
		}

		// Token: 0x06005507 RID: 21767 RVA: 0x00196E35 File Offset: 0x00195035
		public char GetColor(string Faction)
		{
			return Reputation.GetColor(this.Get(Factions.Get(Faction)));
		}

		// Token: 0x06005508 RID: 21768 RVA: 0x00196E48 File Offset: 0x00195048
		public int GetDefaultRep(string Faction)
		{
			return 0;
		}

		// Token: 0x06005509 RID: 21769 RVA: 0x00196E4C File Offset: 0x0019504C
		public int GetFeeling(Faction Faction)
		{
			int feeling = Reputation.GetFeeling((float)this.Get(Faction));
			if (feeling > -50 && feeling < 50 && Faction == HolyPlaceSystem.LastHolyFaction)
			{
				return -50;
			}
			return feeling;
		}

		// Token: 0x0600550A RID: 21770 RVA: 0x00196E7D File Offset: 0x0019507D
		public int GetFeeling(string Faction)
		{
			return this.GetFeeling(Factions.Get(Faction));
		}

		// Token: 0x0600550B RID: 21771 RVA: 0x00196E8B File Offset: 0x0019508B
		public static int GetFeeling(float Reputation)
		{
			if (Reputation <= (float)RuleSettings.REPUTATION_HATED)
			{
				return -100;
			}
			if (Reputation <= (float)RuleSettings.REPUTATION_DISLIKED)
			{
				return -50;
			}
			if (Reputation < (float)RuleSettings.REPUTATION_LIKED)
			{
				return 0;
			}
			if (Reputation < (float)RuleSettings.REPUTATION_LOVED)
			{
				return 50;
			}
			return 100;
		}

		// Token: 0x0600550C RID: 21772 RVA: 0x00196EC0 File Offset: 0x001950C0
		public void Init()
		{
			foreach (Faction faction in Factions.Loop())
			{
				int initialPlayerReputation = faction.InitialPlayerReputation;
				if (initialPlayerReputation != -2147483648)
				{
					this.Set(faction.Name, initialPlayerReputation);
				}
			}
		}

		// Token: 0x0600550D RID: 21773 RVA: 0x00196F24 File Offset: 0x00195124
		public void InitFeeling()
		{
			IReadOnlyList<Faction> list = Factions.GetList();
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				Faction faction = list[i];
				faction.FactionFeeling["Player"] = this.GetFeeling(faction);
				i++;
			}
		}

		// Token: 0x0600550E RID: 21774 RVA: 0x00196F69 File Offset: 0x00195169
		public void AfterGameLoaded()
		{
			this.InitFeeling();
		}

		// Token: 0x0600550F RID: 21775 RVA: 0x00196F74 File Offset: 0x00195174
		public void WorshipPerformed(Worshippable Worship)
		{
			if (Worship == null)
			{
				return;
			}
			WorshipTracking worshipTracking = null;
			bool flag = false;
			foreach (WorshipTracking worshipTracking2 in this.WorshipTracking)
			{
				if (worshipTracking2.Faction == Worship.Faction)
				{
					flag = true;
					if (worshipTracking == null && worshipTracking2.Name == Worship.Name)
					{
						worshipTracking = worshipTracking2;
					}
				}
			}
			if (worshipTracking == null)
			{
				worshipTracking = new WorshipTracking();
				worshipTracking.Name = Worship.Name;
				worshipTracking.Faction = Worship.Faction;
				worshipTracking.First = The.CurrentTurn;
				this.WorshipTracking.Add(worshipTracking);
			}
			worshipTracking.Times++;
			worshipTracking.Last = The.CurrentTurn;
			if (!flag)
			{
				this.ApplyFactionWorshipAttitudes(worshipTracking.Faction, worshipTracking.Name);
			}
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x0019705C File Offset: 0x0019525C
		public bool HasWorshipped(string Faction, int WithinTurns = 0)
		{
			foreach (WorshipTracking worshipTracking in this.WorshipTracking)
			{
				if (worshipTracking.Faction == Faction && (WithinTurns == 0 || worshipTracking.Last >= The.CurrentTurn - (long)WithinTurns))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005511 RID: 21777 RVA: 0x001970D0 File Offset: 0x001952D0
		public bool HasWorshipped(Worshippable Being, int WithinTurns = 0)
		{
			foreach (WorshipTracking worshipTracking in this.WorshipTracking)
			{
				if (worshipTracking.Name == Being.Name && (WithinTurns == 0 || worshipTracking.Last >= The.CurrentTurn - (long)WithinTurns))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005512 RID: 21778 RVA: 0x0019714C File Offset: 0x0019534C
		public bool HasWorshippedInName(string Name, string Faction = null, int WithinTurns = 0)
		{
			foreach (WorshipTracking worshipTracking in this.WorshipTracking)
			{
				if (worshipTracking.Name == Name && (Faction.IsNullOrEmpty() || worshipTracking.Faction == Faction) && (WithinTurns == 0 || worshipTracking.Last >= The.CurrentTurn - (long)WithinTurns))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005513 RID: 21779 RVA: 0x001971D8 File Offset: 0x001953D8
		public bool HasWorshippedBySpec(string Spec, string ContextFaction = null)
		{
			if (Spec.IsNullOrEmpty())
			{
				return this.HasWorshipped(ContextFaction, 0);
			}
			string text = null;
			string text2 = null;
			int num = 0;
			if (Spec.Contains("=") && Spec.Contains(";"))
			{
				Dictionary<string, string> dictionary = Spec.CachedDictionaryExpansion();
				dictionary.TryGetValue("Name", out text);
				dictionary.TryGetValue("Factions", out text2);
				if (text2 == "Context")
				{
					text2 = ContextFaction;
				}
				string text3;
				if (dictionary.TryGetValue("WithinTurns", out text3))
				{
					int.TryParse(text3, out num);
				}
			}
			else
			{
				text = Spec;
			}
			if (!text.IsNullOrEmpty())
			{
				return this.HasWorshippedInName(text, text2, num);
			}
			return this.HasWorshipped(text2, num);
		}

		// Token: 0x06005514 RID: 21780 RVA: 0x0019727C File Offset: 0x0019547C
		public List<WorshipTracking> GetWorshipTracking()
		{
			return this.WorshipTracking;
		}

		// Token: 0x06005515 RID: 21781 RVA: 0x00197284 File Offset: 0x00195484
		public int GetWorshipValence(string Faction)
		{
			int num = 0;
			foreach (Faction faction in Factions.Loop())
			{
				num += faction.GetFactionWorshipAttitude(Faction);
			}
			return num;
		}

		// Token: 0x06005516 RID: 21782 RVA: 0x001972D8 File Offset: 0x001954D8
		private void ApplyFactionWorshipAttitudes(string Faction, string Name)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			foreach (Faction faction in Factions.Loop())
			{
				int factionWorshipAttitude = faction.GetFactionWorshipAttitude(Faction);
				if (factionWorshipAttitude != 0)
				{
					num4++;
					num += factionWorshipAttitude;
					if (factionWorshipAttitude > 0)
					{
						num2 += factionWorshipAttitude;
					}
					else
					{
						num3 += factionWorshipAttitude;
					}
				}
			}
			if (num4 == 0)
			{
				return;
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			StringBuilder stringBuilder2 = Event.NewStringBuilder(null);
			stringBuilder.Append("Your worship of ").Append(Name);
			if (num <= -1000 || num3 <= -1000)
			{
				stringBuilder.Append(" has made you infamous to many across Qud");
				if (num2 > 0)
				{
					stringBuilder.Append(" while meeting with the approval of others");
				}
			}
			else if (num >= 1000 || num2 >= 1000)
			{
				stringBuilder.Append(" has ennobled you in the eyes of many across Qud");
				if (num3 < 0)
				{
					stringBuilder.Append(" while making you infamous to others");
				}
			}
			else if (num <= -200)
			{
				stringBuilder.Append(" has made you infamous to some across Qud");
				if (num2 > 0)
				{
					stringBuilder.Append(" while meeting with the approval of others");
				}
			}
			else if (num >= 200)
			{
				stringBuilder.Append(" has ennobled you in the eyes of some across Qud");
				if (num3 < 0)
				{
					stringBuilder.Append(" while making you infamous to others");
				}
			}
			else
			{
				stringBuilder.Append(" has been noted by some across Qud");
			}
			stringBuilder.Append(".\n\n");
			foreach (Faction faction2 in Factions.Loop())
			{
				int factionWorshipAttitude2 = faction2.GetFactionWorshipAttitude(Faction);
				if (factionWorshipAttitude2 != 0)
				{
					int num5 = GivesRep.VaryRep(factionWorshipAttitude2);
					if (num5 != 0)
					{
						The.Game.PlayerReputation.Modify(faction2, num5, null, faction2.Visible ? stringBuilder : stringBuilder2, "Worship", !faction2.Visible, false, false, true);
					}
				}
			}
			The.Game.PlayerReputation.FinishModify();
			Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x06005517 RID: 21783 RVA: 0x001974F8 File Offset: 0x001956F8
		public void BlasphemyPerformed(Worshippable Blasphemed)
		{
			if (Blasphemed == null)
			{
				return;
			}
			WorshipTracking worshipTracking = null;
			bool flag = false;
			foreach (WorshipTracking worshipTracking2 in this.BlasphemyTracking)
			{
				if (worshipTracking2.Faction == Blasphemed.Faction)
				{
					flag = true;
					if (worshipTracking == null && worshipTracking2.Name == Blasphemed.Name)
					{
						worshipTracking = worshipTracking2;
					}
				}
			}
			if (worshipTracking == null)
			{
				worshipTracking = new WorshipTracking();
				worshipTracking.Name = Blasphemed.Name;
				worshipTracking.Faction = Blasphemed.Faction;
				worshipTracking.First = The.CurrentTurn;
				this.BlasphemyTracking.Add(worshipTracking);
			}
			worshipTracking.Times++;
			worshipTracking.Last = The.CurrentTurn;
			if (!flag)
			{
				this.ApplyFactionBlasphemyAttitudes(worshipTracking.Faction, worshipTracking.Name);
			}
		}

		// Token: 0x06005518 RID: 21784 RVA: 0x001975E0 File Offset: 0x001957E0
		public bool HasBlasphemed(string Faction, int WithinTurns = 0)
		{
			foreach (WorshipTracking worshipTracking in this.BlasphemyTracking)
			{
				if (worshipTracking.Faction == Faction && (WithinTurns == 0 || worshipTracking.Last >= The.CurrentTurn - (long)WithinTurns))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06005519 RID: 21785 RVA: 0x00197654 File Offset: 0x00195854
		public bool HasBlasphemed(Worshippable Being, int WithinTurns = 0)
		{
			foreach (WorshipTracking worshipTracking in this.BlasphemyTracking)
			{
				if (worshipTracking.Name == Being.Name && (WithinTurns == 0 || worshipTracking.Last >= The.CurrentTurn - (long)WithinTurns))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600551A RID: 21786 RVA: 0x001976D0 File Offset: 0x001958D0
		public bool HasBlasphemedAgainstName(string Name, string Faction = null, int WithinTurns = 0)
		{
			foreach (WorshipTracking worshipTracking in this.BlasphemyTracking)
			{
				if (worshipTracking.Name == Name && (Faction.IsNullOrEmpty() || worshipTracking.Faction == Faction) && (WithinTurns == 0 || worshipTracking.Last >= The.CurrentTurn - (long)WithinTurns))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600551B RID: 21787 RVA: 0x0019775C File Offset: 0x0019595C
		public bool HasBlasphemedBySpec(string Spec, string ContextFaction = null)
		{
			if (Spec.IsNullOrEmpty())
			{
				return this.HasBlasphemed(ContextFaction, 0);
			}
			string text = null;
			string text2 = null;
			int num = 0;
			if (Spec.Contains("=") && Spec.Contains(";"))
			{
				Dictionary<string, string> dictionary = Spec.CachedDictionaryExpansion();
				dictionary.TryGetValue("Name", out text);
				dictionary.TryGetValue("Factions", out text2);
				if (text2 == "Context")
				{
					text2 = ContextFaction;
				}
				string text3;
				if (dictionary.TryGetValue("WithinTurns", out text3))
				{
					int.TryParse(text3, out num);
				}
			}
			else
			{
				text = Spec;
			}
			if (!text.IsNullOrEmpty())
			{
				return this.HasBlasphemedAgainstName(text, text2, num);
			}
			return this.HasBlasphemed(text2, num);
		}

		// Token: 0x0600551C RID: 21788 RVA: 0x00197800 File Offset: 0x00195A00
		public List<WorshipTracking> GetBlasphemyTracking()
		{
			return this.BlasphemyTracking;
		}

		// Token: 0x0600551D RID: 21789 RVA: 0x00197808 File Offset: 0x00195A08
		public int GetBlasphemyValence(string Faction)
		{
			int num = 0;
			foreach (Faction faction in Factions.Loop())
			{
				num -= faction.GetFactionBlasphemyAttitude(Faction);
			}
			return num;
		}

		// Token: 0x0600551E RID: 21790 RVA: 0x0019785C File Offset: 0x00195A5C
		private void ApplyFactionBlasphemyAttitudes(string Faction, string Name)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			foreach (Faction faction in Factions.Loop())
			{
				int factionBlasphemyAttitude = faction.GetFactionBlasphemyAttitude(Faction);
				if (factionBlasphemyAttitude != 0)
				{
					num4++;
					num += factionBlasphemyAttitude;
					if (factionBlasphemyAttitude > 0)
					{
						num2 += factionBlasphemyAttitude;
					}
					else
					{
						num3 += factionBlasphemyAttitude;
					}
				}
			}
			if (num4 == 0)
			{
				return;
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			StringBuilder stringBuilder2 = Event.NewStringBuilder(null);
			stringBuilder.Append("Your blasphemy against ").Append(Name);
			if (num >= 1000 || num2 >= 1000)
			{
				stringBuilder.Append(" has ennobled you in the eyes of many across Qud");
				if (num3 < 0)
				{
					stringBuilder.Append(" while making you infamous to others");
				}
			}
			else if (num <= -1000 || num3 <= -1000)
			{
				stringBuilder.Append(" has made you infamous to many across Qud");
				if (num2 > 0)
				{
					stringBuilder.Append(" while meeting with the approval of others");
				}
			}
			else if (num >= 200)
			{
				stringBuilder.Append(" has ennobled you in the eyes of some across Qud");
				if (num3 < 0)
				{
					stringBuilder.Append(" while making you infamous to others");
				}
			}
			else if (num <= -200)
			{
				stringBuilder.Append(" has made you infamous to some across Qud");
				if (num2 > 0)
				{
					stringBuilder.Append(" while meeting with the approval of others");
				}
			}
			else if (num2 - num3 >= 1000)
			{
				stringBuilder.Append(" has been noted by many across Qud");
			}
			else
			{
				stringBuilder.Append(" has been noted by some across Qud");
			}
			stringBuilder.Append(".\n\n");
			foreach (Faction faction2 in Factions.Loop())
			{
				int factionBlasphemyAttitude2 = faction2.GetFactionBlasphemyAttitude(Faction);
				if (factionBlasphemyAttitude2 != 0)
				{
					int num5 = GivesRep.VaryRep(factionBlasphemyAttitude2);
					if (num5 != 0)
					{
						The.Game.PlayerReputation.Modify(faction2, num5, null, faction2.Visible ? stringBuilder : stringBuilder2, "Blasphemy", !faction2.Visible, false, false, true);
					}
				}
			}
			The.Game.PlayerReputation.FinishModify();
			Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}

		// Token: 0x0600551F RID: 21791 RVA: 0x00197A98 File Offset: 0x00195C98
		[Obsolete("Use Get(), will be removed after Q2 2024")]
		public int get(Faction Faction)
		{
			return this.Get(Faction);
		}

		// Token: 0x06005520 RID: 21792 RVA: 0x00197AA1 File Offset: 0x00195CA1
		[Obsolete("Use Get(), will be removed after Q2 2024")]
		public int get(string faction)
		{
			return this.Get(faction);
		}

		// Token: 0x06005521 RID: 21793 RVA: 0x00197AAA File Offset: 0x00195CAA
		[Obsolete("Use Set(), will be removed after Q2 2024")]
		public int set(Faction faction, int rep)
		{
			return this.Set(faction, rep);
		}

		// Token: 0x06005522 RID: 21794 RVA: 0x00197AB4 File Offset: 0x00195CB4
		[Obsolete("Use Set(), will be removed after Q2 2024")]
		public int set(string faction, int rep)
		{
			return this.Set(faction, rep);
		}

		// Token: 0x06005523 RID: 21795 RVA: 0x00197AC0 File Offset: 0x00195CC0
		[Obsolete("Use Modify(), will be removed after Q2 2024")]
		public float modify(Faction Faction, int Amount, string Because = null, StringBuilder PutMessage = null, string Type = null, bool Silent = false, bool Transient = false, bool SingleLine = false, bool Multiple = false)
		{
			return this.Modify(Faction, Amount, Because, PutMessage, Type, Silent, Transient, SingleLine, Multiple);
		}

		// Token: 0x06005524 RID: 21796 RVA: 0x00197AE2 File Offset: 0x00195CE2
		[Obsolete("Use Modify(), will be removed after Q2 2024")]
		public float modify(string Faction, int Amount, string Type = null, string Because = null, StringBuilder PutMessage = null, bool Silent = false, bool Transient = false)
		{
			return this.Modify(Faction, Amount, Type, Because, PutMessage, Silent, Transient);
		}

		// Token: 0x06005525 RID: 21797 RVA: 0x00197AF5 File Offset: 0x00195CF5
		[Obsolete("Use Modify(), will be removed after Q2 2024")]
		public float modify(string Faction, int Amount, bool DisplayMessage)
		{
			return this.Modify(Faction, Amount, DisplayMessage);
		}

		// Token: 0x04002AC5 RID: 10949
		public const int LOVED = 2;

		// Token: 0x04002AC6 RID: 10950
		public const int LIKED = 1;

		// Token: 0x04002AC7 RID: 10951
		public const int INDIFFERENT = 0;

		// Token: 0x04002AC8 RID: 10952
		public const int DISLIKED = -1;

		// Token: 0x04002AC9 RID: 10953
		public const int HATED = -2;

		// Token: 0x04002ACA RID: 10954
		public Dictionary<string, float> ReputationValues = new Dictionary<string, float>(64);

		// Token: 0x04002ACB RID: 10955
		public Dictionary<string, string> FactionRanks = new Dictionary<string, string>();

		// Token: 0x04002ACC RID: 10956
		public List<WorshipTracking> WorshipTracking = new List<WorshipTracking>();

		// Token: 0x04002ACD RID: 10957
		public List<WorshipTracking> BlasphemyTracking = new List<WorshipTracking>();

		// Token: 0x04002ACE RID: 10958
		private static List<string> PositiveSounds = new List<string>();

		// Token: 0x04002ACF RID: 10959
		private static List<string> NegativeSounds = new List<string>();
	}
}
