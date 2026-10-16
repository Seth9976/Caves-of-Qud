using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using HistoryKit;
using Qud.API;
using UnityEngine;
using XRL.Collections;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000EBE RID: 3774
	[Serializable]
	public class GivesRep : IPart
	{
		// Token: 0x0600975B RID: 38747 RVA: 0x0036E788 File Offset: 0x0036C988
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			GivesRep givesRep = (GivesRep)base.DeepCopy(Parent, MapInv);
			givesRep.relatedFactions = new List<FriendorFoe>(this.relatedFactions.Count);
			foreach (FriendorFoe friendorFoe in this.relatedFactions)
			{
				givesRep.relatedFactions.Add(new FriendorFoe(friendorFoe));
			}
			return givesRep;
		}

		// Token: 0x0600975C RID: 38748 RVA: 0x0036E80C File Offset: 0x0036CA0C
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.Write(this.relatedFactions.Count);
			foreach (FriendorFoe friendorFoe in this.relatedFactions)
			{
				Writer.Write(friendorFoe.faction);
				Writer.Write(friendorFoe.status);
				Writer.Write(friendorFoe.reason);
			}
			base.Write(Basis, Writer);
		}

		// Token: 0x0600975D RID: 38749 RVA: 0x0036E898 File Offset: 0x0036CA98
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			int num = Reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = Reader.ReadString();
				string text2 = Reader.ReadString();
				string text3 = Reader.ReadString();
				this.relatedFactions.Add(new FriendorFoe(text, text2, text3));
			}
			base.Read(Basis, Reader);
		}

		// Token: 0x0600975E RID: 38750 RVA: 0x0036E8EA File Offset: 0x0036CAEA
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600975F RID: 38751 RVA: 0x0036E8ED File Offset: 0x0036CAED
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDeathRemovalEvent.ID || ID == CanGiveDirectionsEvent.ID || ID == PooledEvent<GetPointsOfInterestEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == ReplicaCreatedEvent.ID;
		}

		// Token: 0x06009760 RID: 38752 RVA: 0x0036E924 File Offset: 0x0036CB24
		public override bool HandleEvent(GetPointsOfInterestEvent E)
		{
			if (E.StandardChecks(this, E.Actor, null))
			{
				E.Add(this.ParentObject, this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false), null, null, null, null, null, -1, null, 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009761 RID: 38753 RVA: 0x0036E97C File Offset: 0x0036CB7C
		public override bool HandleEvent(CanGiveDirectionsEvent E)
		{
			if (E.SpeakingWith == this.ParentObject && !E.PlayerCompanion && !this.ParentObject.HasEffect<Lost>() && !this.ParentObject.HasEffect<Confused>() && !this.ParentObject.HasEffect<FuriouslyConfused>())
			{
				E.CanGiveDirections = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009762 RID: 38754 RVA: 0x0036E9D4 File Offset: 0x0036CBD4
		public override bool HandleEvent(ReplicaCreatedEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.WantToRemove(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009763 RID: 38755 RVA: 0x0036E9F4 File Offset: 0x0036CBF4
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			this.AppendReputationDescription(E.Postfix);
			if (this.wasParleyed)
			{
				E.Postfix.Compound("You are water-bonded with " + this.ParentObject.GetPronounProvider(false).Objective + ".", "\n");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009764 RID: 38756 RVA: 0x0036EA50 File Offset: 0x0036CC50
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (E.Killer != null && E.Killer.IsPlayerControlled() && !this.KillRepDone)
			{
				this.KillRepDone = true;
				if (this.wasParleyed && E.Killer != this.ParentObject && (!E.Accidental || this.ParentObject.IsHostileTowards(E.Killer)))
				{
					Achievement.VIOLATE_WATER_RITUAL.Unlock();
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					stringBuilder.Append("You violated the covenant of the water ritual and killed your bonded kith. You are cursed.\n\n");
					foreach (Faction faction in Factions.Loop())
					{
						if (faction.Visible && !faction.HatesPlayer)
						{
							The.Game.PlayerReputation.Modify(faction, GivesRep.VaryRep(-100), null, stringBuilder, "WaterRitualCurse", false, false, true, true);
						}
					}
					The.Game.PlayerReputation.FinishModify();
					Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				foreach (Faction faction2 in this.GetLovedFactions(25, true, true))
				{
					The.Game.PlayerReputation.Modify(faction2, GivesRep.VaryRep(-this.repValue * 2), null, null, "LegendaryFactionAward", false, false, false, false);
				}
				foreach (FriendorFoe friendorFoe in this.relatedFactions)
				{
					if (Factions.Get(friendorFoe.faction).Visible)
					{
						if (friendorFoe.status == "love")
						{
							The.Game.PlayerReputation.Modify(friendorFoe.faction, GivesRep.VaryRep(-this.repValue * 2), "LegendaryFactionAward", null, null, false, false);
						}
						else if (friendorFoe.status == "friend")
						{
							The.Game.PlayerReputation.Modify(friendorFoe.faction, GivesRep.VaryRep(-this.repValue * 2), "LegendaryFriendAward", null, null, false, false);
						}
						else if (friendorFoe.status == "dislike")
						{
							The.Game.PlayerReputation.Modify(friendorFoe.faction, GivesRep.VaryRep(this.repValue), "LegendaryDislikeAward", null, null, false, false);
						}
						else if (friendorFoe.status == "hate")
						{
							The.Game.PlayerReputation.Modify(friendorFoe.faction, GivesRep.VaryRep(this.repValue * 2), "LegendaryHateAward", null, null, false, false);
						}
						else
						{
							Debug.LogError("Unknown status " + friendorFoe.status + " for " + friendorFoe.faction);
						}
					}
				}
				string referenceDisplayName = this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, null, false);
				if (this.wasParleyed)
				{
					JournalAPI.AddAccomplishment("You slew your bonded kith " + referenceDisplayName + ", violating the covenant of the water ritual and earning the emnity of all.", string.Concat(new string[]
					{
						"Blasphemously, the traitor ",
						referenceDisplayName,
						" attacked =name=, ",
						this.ParentObject.its,
						" water-sib, and =name= was forced to slay ",
						this.ParentObject.them,
						". Deep in grief, =name= wept for one year."
					}), string.Format("In the month of {0} of {1}, =name= was challenged by <spice.commonPhrases.pretender.!random.article> to a duel over the rights of {2}. =name= won and murdered the pretender before tragically realizing <spice.pronouns.subject.!random> was {3} water-sib.", new object[]
					{
						Calendar.GetMonth(),
						Calendar.GetYear(),
						Factions.GetMostLikedFormattedName(true),
						The.Player.GetPronounProvider(false).PossessiveAdjective
					}), null, "general", MuralCategory.Slays, MuralWeight.High, null, -1L, true);
				}
				else
				{
					JournalAPI.AddAccomplishment("You slew " + referenceDisplayName + ".", HistoricStringExpander.ExpandString(string.Concat(new string[]
					{
						"In the month of ",
						Calendar.GetMonth(),
						" of ",
						Calendar.GetYear().ToString(),
						", brave =name= slew <spice.commonPhrases.odious.!random> ",
						referenceDisplayName,
						" in single combat."
					}), null), string.Format("In the month of {0} of {1}, =name= was challenged by <spice.commonPhrases.pretender.!random.article> to a duel over the rights of {2}. =name= won and murdered the pretender <spice.elements.{3}.murdermethods.!random>.", new object[]
					{
						Calendar.GetMonth(),
						Calendar.GetYear(),
						Factions.GetMostLikedFormattedName(true),
						The.Player.GetMythicDomain(5, null)
					}), null, "general", MuralCategory.Slays, MuralWeight.Low, null, -1L, true);
					ItemNaming.Opportunity(E.Killer, this.ParentObject, null, null, "Kill", 6, 0, 0, 1, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009765 RID: 38757 RVA: 0x0036EF38 File Offset: 0x0036D138
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009766 RID: 38758 RVA: 0x0036EF3B File Offset: 0x0036D13B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("FactionsAdded");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009767 RID: 38759 RVA: 0x0036EF50 File Offset: 0x0036D150
		public override bool FireEvent(Event E)
		{
			if (E.ID == "FactionsAdded")
			{
				this.FillInRelatedFactions(true);
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009768 RID: 38760 RVA: 0x0036EF74 File Offset: 0x0036D174
		public List<Faction> GetLovedFactions(int MinimumMembership = 25, bool VisibleOnly = false, bool WithoutReason = false)
		{
			List<Faction> lovedCache = GivesRep.LovedCache;
			lovedCache.Clear();
			foreach (KeyValuePair<string, int> keyValuePair in this.ParentObject.Brain.GetBaseAllegiance())
			{
				Faction ifExists = Factions.GetIfExists(keyValuePair.Key);
				if (ifExists != null && keyValuePair.Value >= MinimumMembership && (!VisibleOnly || ifExists.Visible))
				{
					lovedCache.Add(ifExists);
				}
			}
			string text = this.ParentObject.GetxTag("Reputation", "LovedBy", null);
			if (text.IsNullOrEmpty())
			{
				return lovedCache;
			}
			foreach (ReadOnlySpan<char> readOnlySpan in text.DelimitedBy(','))
			{
				ReadOnlySpan<char> readOnlySpan2;
				ReadOnlySpan<char> readOnlySpan3;
				readOnlySpan.Split(':', out readOnlySpan2, out readOnlySpan3);
				if (!WithoutReason || readOnlySpan3.IsEmpty)
				{
					Faction ifExists2 = Factions.GetIfExists(new string(readOnlySpan2));
					if (ifExists2 != null && (!VisibleOnly || ifExists2.Visible))
					{
						lovedCache.Add(ifExists2);
					}
				}
			}
			return lovedCache;
		}

		// Token: 0x06009769 RID: 38761 RVA: 0x0036F08C File Offset: 0x0036D28C
		public void DumpRelatedFactions(string When)
		{
		}

		// Token: 0x0600976A RID: 38762 RVA: 0x0036F090 File Offset: 0x0036D290
		public void ResetRelatedFactions()
		{
			Brain brain = this.ParentObject.Brain;
			foreach (FriendorFoe friendorFoe in this.relatedFactions)
			{
				int num = 0;
				if (friendorFoe.status == "friend" || friendorFoe.status == "love")
				{
					num = 100;
				}
				else if (friendorFoe.status == "dislike")
				{
					num = -50;
				}
				else if (friendorFoe.status == "hate")
				{
					num = -100;
				}
				if (brain.FactionFeelings.ContainsKey(friendorFoe.faction))
				{
					if (brain.FactionFeelings[friendorFoe.faction] == num)
					{
						brain.FactionFeelings.Remove(friendorFoe.faction);
					}
					else
					{
						StringMap<int> factionFeelings = brain.FactionFeelings;
						string faction = friendorFoe.faction;
						factionFeelings[faction] -= num;
					}
				}
			}
			this.relatedFactions.Clear();
		}

		// Token: 0x0600976B RID: 38763 RVA: 0x0036F1B0 File Offset: 0x0036D3B0
		public int FillInRelatedFactions(bool Initial = false)
		{
			string text = this.ParentObject.GetxTag("Reputation", "RelatedAmount", "1d3");
			return this.FillInRelatedFactions(text.RollCached(), Initial);
		}

		// Token: 0x0600976C RID: 38764 RVA: 0x0036F1E8 File Offset: 0x0036D3E8
		public int FillInRelatedFactions(int FactionCount, bool Initial = false)
		{
			int num = 0;
			List<string[]> list = new List<string[]>(3);
			if (base.HasPropertyOrTag("staticFaction1"))
			{
				list.Add(base.GetPropertyOrTag("staticFaction1", null).Split(',', StringSplitOptions.None));
				num++;
			}
			if (base.HasPropertyOrTag("staticFaction2"))
			{
				list.Add(base.GetPropertyOrTag("staticFaction2", null).Split(',', StringSplitOptions.None));
				num++;
			}
			if (base.HasPropertyOrTag("staticFaction3"))
			{
				list.Add(base.GetPropertyOrTag("staticFaction3", null).Split(',', StringSplitOptions.None));
				num++;
			}
			num += this.AddStaticXTagFactions(list, "LovedBy", "love");
			num += this.AddStaticXTagFactions(list, "LikedBy", "friend");
			num += this.AddStaticXTagFactions(list, "DislikedBy", "dislike");
			num += this.AddStaticXTagFactions(list, "HatedBy", "hate");
			while (list.Count < 3)
			{
				list.Add(new string[3]);
			}
			string text = this.ParentObject.GetPropertyOrTag("NoHateFactions", null) ?? this.ParentObject.GetxTag("Reputation", "NotDislikedBy", null);
			string text2 = this.ParentObject.GetPropertyOrTag("NoFriendFactions", null) ?? this.ParentObject.GetxTag("Reputation", "NotLikedBy", null);
			if (FactionCount < num)
			{
				FactionCount = num;
			}
			Brain brain = this.ParentObject.Brain;
			for (int i = 1; i <= FactionCount; i++)
			{
				FriendorFoe friendorFoe = ((this.relatedFactions.Count >= i) ? this.relatedFactions[i - 1] : new FriendorFoe());
				string[] array = list[i - 1];
				string text3;
				if (!friendorFoe.faction.IsNullOrEmpty())
				{
					text3 = friendorFoe.faction;
				}
				else if (!array[0].IsNullOrEmpty())
				{
					text3 = array[0];
					friendorFoe.faction = text3;
				}
				else
				{
					text3 = GenerateFriendOrFoe.getRandomFaction(this.ParentObject);
					friendorFoe.faction = text3;
				}
				string text4;
				if (!friendorFoe.status.IsNullOrEmpty())
				{
					text4 = friendorFoe.status;
				}
				else if (!array[1].IsNullOrEmpty())
				{
					text4 = array[1];
					friendorFoe.status = text4;
				}
				else
				{
					int num2 = Stat.Random(1, 100);
					if (num2 <= 10)
					{
						text4 = "friend";
					}
					else if (num2 <= 55)
					{
						text4 = "dislike";
					}
					else
					{
						text4 = "hate";
					}
					friendorFoe.status = text4;
				}
				if (text4 != "love" && text4 != "friend" && text4 != "dislike" && text4 != "hate")
				{
					Debug.LogWarning("had unknown status '" + text4 + "', using dislike");
					text4 = "dislike";
					friendorFoe.status = text4;
				}
				if (text != null && (text4 == "dislike" || text4 == "hate") && text.Contains(text3))
				{
					text4 = "friend";
					friendorFoe.status = text4;
				}
				if (text2 != null && (text4 == "friend" || text4 == "love") && text2.Contains(text3))
				{
					text4 = "dislike";
					friendorFoe.status = text4;
				}
				string text5 = null;
				if (!friendorFoe.reason.IsNullOrEmpty())
				{
					text5 = friendorFoe.reason;
				}
				else if (!array[2].IsNullOrEmpty())
				{
					text5 = array[2];
					friendorFoe.reason = text5;
				}
				if (text4 == "friend")
				{
					if (brain.FactionFeelings.ContainsKey(text3))
					{
						if (Initial)
						{
							StringMap<int> stringMap = brain.FactionFeelings;
							string text6 = text3;
							stringMap[text6] += 100;
						}
					}
					else
					{
						brain.FactionFeelings.Add(text3, 100);
					}
					if (text5.IsNullOrEmpty())
					{
						if (this.ParentObject.HasTag("StaticLikeReason"))
						{
							text5 = this.ParentObject.GetTag("StaticLikeReason", null);
						}
						else if (this.ParentObject.BelongsToFaction("Entropic"))
						{
							text5 = GenerateFriendOrFoe_HEB.getLikeReason();
						}
						else
						{
							text5 = GenerateFriendOrFoe.getLikeReason();
						}
						friendorFoe.reason = text5;
					}
				}
				else if (text4 == "love")
				{
					if (brain.FactionFeelings.ContainsKey(text3))
					{
						if (Initial)
						{
							StringMap<int> stringMap = brain.FactionFeelings;
							string text6 = text3;
							stringMap[text6] += 100;
						}
					}
					else
					{
						brain.FactionFeelings.Add(text3, 100);
					}
				}
				else if (text4 == "dislike")
				{
					if (brain.FactionFeelings.ContainsKey(text3))
					{
						if (Initial)
						{
							StringMap<int> stringMap = brain.FactionFeelings;
							string text6 = text3;
							stringMap[text6] -= 50;
						}
					}
					else
					{
						brain.FactionFeelings.Add(text3, -50);
					}
					if (text5.IsNullOrEmpty())
					{
						if (this.ParentObject.HasTag("StaticHateReason"))
						{
							text5 = this.ParentObject.GetTag("StaticHateReason", null);
						}
						else if (this.ParentObject.BelongsToFaction("Entropic"))
						{
							text5 = GenerateFriendOrFoe_HEB.getHateReason();
						}
						else
						{
							text5 = GenerateFriendOrFoe.getHateReason();
						}
						friendorFoe.reason = text5;
					}
				}
				else
				{
					if (!(text4 == "hate"))
					{
						throw new Exception("internal inconsistency");
					}
					if (brain.FactionFeelings.ContainsKey(text3))
					{
						if (Initial)
						{
							StringMap<int> stringMap = brain.FactionFeelings;
							string text6 = text3;
							stringMap[text6] -= 100;
						}
					}
					else
					{
						brain.FactionFeelings.Add(text3, -100);
					}
					if (text5.IsNullOrEmpty())
					{
						if (this.ParentObject.HasTag("StaticHateReason"))
						{
							text5 = this.ParentObject.GetTag("StaticHateReason", null);
						}
						else if (this.ParentObject.BelongsToFaction("Entropic"))
						{
							text5 = GenerateFriendOrFoe_HEB.getHateReason();
						}
						else
						{
							text5 = GenerateFriendOrFoe.getHateReason();
						}
						friendorFoe.reason = text5;
					}
				}
				if (this.relatedFactions.Count < i)
				{
					this.relatedFactions.Add(friendorFoe);
				}
			}
			return FactionCount;
		}

		// Token: 0x0600976D RID: 38765 RVA: 0x0036F80C File Offset: 0x0036DA0C
		private int AddStaticXTagFactions(List<string[]> Factions, string Tag, string Status)
		{
			int num = 0;
			string text = this.ParentObject.GetxTag("Reputation", Tag, null);
			if (!text.IsNullOrEmpty())
			{
				foreach (ReadOnlySpan<char> readOnlySpan in text.DelimitedBy(','))
				{
					string[] array = new string[3];
					array[1] = Status;
					string[] array2 = array;
					ReadOnlySpan<char> readOnlySpan2;
					ReadOnlySpan<char> readOnlySpan3;
					readOnlySpan.Split(':', out readOnlySpan2, out readOnlySpan3);
					array2[0] = ((readOnlySpan2.Length == 0) ? null : new string(readOnlySpan2));
					array2[2] = ((readOnlySpan3.Length == 0) ? null : new string(readOnlySpan3));
					Factions.Add(array2);
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600976E RID: 38766 RVA: 0x0036F8B0 File Offset: 0x0036DAB0
		public static int VaryRep(int Rep)
		{
			float num = (float)((Math.Abs(Rep) >= 100) ? Stat.Random(-5, 5) : Stat.Random(-25, 25));
			float num2 = (float)Rep;
			num2 += 0.1f * num2 * num / 5f;
			num2 = (float)(5 * (int)Math.Round((double)(num2 / 5f)));
			return (int)num2;
		}

		// Token: 0x0600976F RID: 38767 RVA: 0x0036F905 File Offset: 0x0036DB05
		[Obsolete("use VaryRep(), will be removed after Q2 2024")]
		public static int varyRep(int Rep)
		{
			return GivesRep.VaryRep(Rep);
		}

		// Token: 0x06009770 RID: 38768 RVA: 0x0036F910 File Offset: 0x0036DB10
		private int AddStaticXTagFactions(List<string[]> Factions, string Tag, string Status, bool RequireReason = false)
		{
			int num = 0;
			string text = this.ParentObject.GetxTag("Reputation", Tag, null);
			if (!text.IsNullOrEmpty())
			{
				foreach (ReadOnlySpan<char> readOnlySpan in text.DelimitedBy(','))
				{
					ReadOnlySpan<char> readOnlySpan2;
					ReadOnlySpan<char> readOnlySpan3;
					readOnlySpan.Split(':', out readOnlySpan2, out readOnlySpan3);
					if (!RequireReason || !readOnlySpan3.IsEmpty)
					{
						string[] array = new string[3];
						array[1] = Status;
						string[] array2 = array;
						array2[0] = ((readOnlySpan2.Length == 0) ? null : new string(readOnlySpan2));
						array2[2] = ((readOnlySpan3.Length == 0) ? null : new string(readOnlySpan3));
						Factions.Add(array2);
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06009771 RID: 38769 RVA: 0x0036F9C0 File Offset: 0x0036DBC0
		public static int VaryRepUp(int Rep)
		{
			float num = (float)Stat.Random(0, 10);
			float num2 = (float)Rep;
			num2 += 0.1f * num2 * num / 10f;
			num2 = (float)(10 * (int)Math.Round((double)(num2 / 10f)));
			return (int)num2;
		}

		// Token: 0x06009772 RID: 38770 RVA: 0x0036FA01 File Offset: 0x0036DC01
		[Obsolete("use VaryRepUp(), will be removed after Q2 2024")]
		public static int varyRepUp(int Rep)
		{
			return GivesRep.VaryRepUp(Rep);
		}

		// Token: 0x06009773 RID: 38771 RVA: 0x0036FA0C File Offset: 0x0036DC0C
		public void AppendReputationDescription(StringBuilder SB)
		{
			if (SB.Length > 0 && SB[SB.Length - 1] != '\n')
			{
				SB.Append('\n');
			}
			List<string> list = new List<string>(2);
			foreach (Faction faction in this.GetLovedFactions(25, true, true))
			{
				string text = Markup.Color("C", faction.GetFormattedName());
				if (Options.GivesRepShowsCurrentRep)
				{
					string text2 = Markup.Color("K", string.Format("({0})", Faction.PlayerReputation.Get(faction.Name)));
					list.Add(text + text2);
				}
				else
				{
					list.Add(text);
				}
			}
			bool flag = false;
			if (list.Count > 0)
			{
				SB.Append("{{C|-----}}");
				flag = true;
				SB.Append("\nLoved by ").Append(Grammar.MakeAndList(list, true)).Append(".\n");
			}
			foreach (FriendorFoe friendorFoe in this.relatedFactions)
			{
				Faction ifExists = Factions.GetIfExists(friendorFoe.faction);
				if (ifExists != null && ifExists.Visible && !friendorFoe.reason.IsNullOrEmpty())
				{
					if (!flag)
					{
						SB.Append("{{C|-----}}");
						flag = true;
					}
					SB.Append('\n');
					if (friendorFoe.status == "love")
					{
						SB.Append("Loved");
					}
					if (friendorFoe.status == "friend")
					{
						SB.Append("Admired");
					}
					else if (friendorFoe.status == "dislike")
					{
						SB.Append("Disliked");
					}
					else if (friendorFoe.status == "hate")
					{
						SB.Append("Hated");
					}
					SB.Append(" by {{C|").Append(Faction.GetFormattedName(friendorFoe.faction)).Append("}}");
					if (Options.GivesRepShowsCurrentRep)
					{
						SB.Append(Markup.Color("K", string.Format("({0})", Faction.PlayerReputation.Get(friendorFoe.faction))));
					}
					SB.Append(" for ").Append(friendorFoe.reason).Append(".\n");
				}
			}
		}

		// Token: 0x04003EEE RID: 16110
		public bool wasParleyed;

		// Token: 0x04003EEF RID: 16111
		public int repValue = 100;

		// Token: 0x04003EF0 RID: 16112
		[NonSerialized]
		public List<FriendorFoe> relatedFactions = new List<FriendorFoe>();

		// Token: 0x04003EF1 RID: 16113
		private bool KillRepDone;

		// Token: 0x04003EF2 RID: 16114
		private static List<Faction> LovedCache = new List<Faction>();
	}
}
