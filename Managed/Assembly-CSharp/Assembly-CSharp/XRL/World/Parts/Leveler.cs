using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Qud.API;
using XRL.Core;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000ED3 RID: 3795
	[Serializable]
	public class Leveler : IPart
	{
		// Token: 0x060097EF RID: 38895 RVA: 0x00372735 File Offset: 0x00370935
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AwardedXPEvent.ID || ID == PooledEvent<StatChangeEvent>.ID;
		}

		// Token: 0x060097F0 RID: 38896 RVA: 0x00372754 File Offset: 0x00370954
		public override bool HandleEvent(AwardedXPEvent E)
		{
			int num = E.Actor.Stat("XP", 0);
			while (this.valuePassed(E.AmountBefore, num, Leveler.GetXPForLevel(this.ParentObject.Stat("Level", 0) + 1)) && this.LevelUp(E.Kill, E.InfluencedBy, E.ZoneID, null))
			{
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060097F1 RID: 38897 RVA: 0x003727C0 File Offset: 0x003709C0
		public override bool HandleEvent(StatChangeEvent E)
		{
			if (E.Name == "Toughness")
			{
				int scoreModifier = Stat.GetScoreModifier(E.OldValue);
				int scoreModifier2 = Stat.GetScoreModifier(E.NewValue);
				int num = E.NewValue - E.OldValue + (scoreModifier2 - scoreModifier) * (this.ParentObject.Stat("Level", 0) - 1);
				if (num != 0)
				{
					Statistic stat = this.ParentObject.GetStat("Hitpoints");
					if (stat != null)
					{
						int baseValue = stat.BaseValue;
						if (num > 0)
						{
							stat.BaseValue += num;
						}
						if (stat.Penalty != 0)
						{
							int num2 = baseValue + num;
							double num3 = (double)stat.Penalty / (double)baseValue;
							int num4 = (int)Math.Min(Math.Round((double)num2 * num3, MidpointRounding.AwayFromZero), (double)(num2 - 1));
							stat.Penalty = num4;
						}
						if (num < 0)
						{
							stat.BaseValue += num;
						}
					}
				}
			}
			else if (E.Name == "Intelligence" && E.Type == "BaseValue")
			{
				int intProperty = this.ParentObject.GetIntProperty("PeakIntelligence", E.OldValue);
				if (E.NewValue > intProperty && this.ParentObject.HasStat("SP"))
				{
					int num5 = (E.NewValue - intProperty) * 4 * (this.ParentObject.Stat("Level", 0) - 1);
					if (num5 > 0)
					{
						this.ParentObject.GetStat("SP").BaseValue += num5;
					}
					if (E.OldValue != 0 && E.NewValue > intProperty)
					{
						this.ParentObject.SetIntProperty("PeakIntelligence", E.NewValue, false);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060097F2 RID: 38898 RVA: 0x00372983 File Offset: 0x00370B83
		private bool valuePassed(int oldValue, int newValue, int threshold)
		{
			return oldValue < threshold && newValue >= threshold;
		}

		// Token: 0x060097F3 RID: 38899 RVA: 0x00372992 File Offset: 0x00370B92
		public static int GetXPForLevel(int L)
		{
			if (L <= 1)
			{
				return 0;
			}
			return (int)(Math.Floor(Math.Pow((double)L, 3.0) * 15.0) + 100.0);
		}

		// Token: 0x060097F4 RID: 38900 RVA: 0x003729C4 File Offset: 0x00370BC4
		public void GetEntryDice(out string BaseHPGain, out string BaseSPGain, out string BaseMPGain)
		{
			GenotypeEntry genotypeEntry = GenotypeFactory.RequireGenotypeEntry(this.ParentObject.GetGenotype());
			SubtypeEntry subtypeEntry = SubtypeFactory.GetSubtypeEntry(this.ParentObject.GetSubtype());
			if (!string.IsNullOrEmpty((subtypeEntry != null) ? subtypeEntry.BaseHPGain : null))
			{
				BaseHPGain = subtypeEntry.BaseHPGain;
			}
			else if (!string.IsNullOrEmpty(genotypeEntry.BaseHPGain))
			{
				BaseHPGain = genotypeEntry.BaseHPGain;
			}
			else
			{
				BaseHPGain = "1-4";
			}
			if (!string.IsNullOrEmpty((subtypeEntry != null) ? subtypeEntry.BaseSPGain : null))
			{
				BaseSPGain = subtypeEntry.BaseSPGain;
			}
			else if (!string.IsNullOrEmpty(genotypeEntry.BaseSPGain))
			{
				BaseSPGain = genotypeEntry.BaseSPGain;
			}
			else
			{
				BaseSPGain = "50";
			}
			if (!string.IsNullOrEmpty((subtypeEntry != null) ? subtypeEntry.BaseMPGain : null))
			{
				BaseMPGain = subtypeEntry.BaseMPGain;
				return;
			}
			if (!string.IsNullOrEmpty(genotypeEntry.BaseMPGain))
			{
				BaseMPGain = genotypeEntry.BaseMPGain;
				return;
			}
			BaseMPGain = "1";
		}

		// Token: 0x060097F5 RID: 38901 RVA: 0x00372AA2 File Offset: 0x00370CA2
		public int RollHP(string BaseHPGain)
		{
			return Math.Max(Stat.RollLevelupChoice(BaseHPGain) + this.ParentObject.StatMod("Toughness", 0), 1);
		}

		// Token: 0x060097F6 RID: 38902 RVA: 0x00372AC4 File Offset: 0x00370CC4
		public int RollSP(string BaseSPGain)
		{
			int num = Stat.RollLevelupChoice(BaseSPGain);
			num += (this.ParentObject.BaseStat("Intelligence", 0) - 10) * 4;
			return GetLevelUpSkillPointsEvent.GetFor(this.ParentObject, num);
		}

		// Token: 0x060097F7 RID: 38903 RVA: 0x00372AFF File Offset: 0x00370CFF
		public int RollMP(string BaseMPGain)
		{
			return Stat.RollLevelupChoice(BaseMPGain);
		}

		// Token: 0x060097F8 RID: 38904 RVA: 0x00372B08 File Offset: 0x00370D08
		public void AppendStatMessage(StringBuilder sb, string Type, int Amount, bool Pluralize = true)
		{
			if (sb == null)
			{
				return;
			}
			int num = Math.Abs(Amount);
			sb.CompoundItVerb(this.ParentObject, (Amount > 0) ? "gain" : "lose", '\n', true).Append(" {{rules|").Append(num)
				.Append("}} ")
				.Append(Type);
			if (Pluralize && num != 1)
			{
				sb.Append('s');
			}
		}

		// Token: 0x060097F9 RID: 38905 RVA: 0x00372B71 File Offset: 0x00370D71
		public void AddHitpoints(StringBuilder sb, int HPGain)
		{
			if (HPGain == 0)
			{
				return;
			}
			this.ParentObject.GetStat("Hitpoints").BaseValue += HPGain;
			this.AppendStatMessage(sb, "hitpoint", HPGain, true);
		}

		// Token: 0x060097FA RID: 38906 RVA: 0x00372BA2 File Offset: 0x00370DA2
		public void AddSkillPoints(StringBuilder sb, int SPGain)
		{
			if (SPGain == 0)
			{
				return;
			}
			this.ParentObject.GetStat("SP").BaseValue += SPGain;
			this.AppendStatMessage(sb, "Skill Point", SPGain, true);
		}

		// Token: 0x060097FB RID: 38907 RVA: 0x00372BD3 File Offset: 0x00370DD3
		public void AddMutationPoints(StringBuilder sb, int MPGain)
		{
			if (MPGain == 0)
			{
				return;
			}
			this.ParentObject.GainMP(MPGain);
			this.AppendStatMessage(sb, "Mutation Point", MPGain, true);
		}

		// Token: 0x060097FC RID: 38908 RVA: 0x00372BF4 File Offset: 0x00370DF4
		public void AddAttributePoints(StringBuilder sb, int APGain)
		{
			if (APGain == 0)
			{
				return;
			}
			this.ParentObject.GetStat("AP").BaseValue += APGain;
			this.AppendStatMessage(sb, "Attribute Point", APGain, true);
		}

		// Token: 0x060097FD RID: 38909 RVA: 0x00372C28 File Offset: 0x00370E28
		public void AddAttributeBonus(StringBuilder sb, int ABGain)
		{
			if (ABGain == 0)
			{
				return;
			}
			this.ParentObject.GetStat("Strength").BaseValue += ABGain;
			this.ParentObject.GetStat("Intelligence").BaseValue += ABGain;
			this.ParentObject.GetStat("Willpower").BaseValue += ABGain;
			this.ParentObject.GetStat("Agility").BaseValue += ABGain;
			this.ParentObject.GetStat("Toughness").BaseValue += ABGain;
			this.ParentObject.GetStat("Ego").BaseValue += ABGain;
			this.AppendStatMessage(sb, "to each attribute", ABGain, false);
		}

		// Token: 0x060097FE RID: 38910 RVA: 0x00372CF5 File Offset: 0x00370EF5
		public bool UseDetailedPrompt()
		{
			return this.ParentObject.IsPlayer() || (Leveler.PlayerLedPrompt && this.ParentObject.IsPlayerLed());
		}

		// Token: 0x060097FF RID: 38911 RVA: 0x00372D1C File Offset: 0x00370F1C
		public bool LevelUp(GameObject Kill = null, GameObject InfluencedBy = null, string ZoneID = null, IEvent ParentEvent = null)
		{
			int num = this.ParentObject.Stat("Level", 0) + 1;
			bool flag = this.ParentObject.IsPlayer();
			bool flag2;
			using (BeforeLevelGainedEvent beforeLevelGainedEvent = BeforeLevelGainedEvent.FromPool(this.ParentObject, Kill, InfluencedBy, num, this.UseDetailedPrompt()))
			{
				if (beforeLevelGainedEvent.Detail)
				{
					beforeLevelGainedEvent.Message.Append(flag ? "You" : this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false)).Append(this.ParentObject.Has).Append(" gained a level!")
						.CompoundItVerb(this.ParentObject, "are", ' ', true)
						.Append(" now level {{C|")
						.Append(num)
						.Append("}}!");
				}
				if (!beforeLevelGainedEvent.Check())
				{
					flag2 = false;
				}
				else
				{
					Statistic stat = this.ParentObject.GetStat("Level");
					int baseValue = stat.BaseValue;
					stat.BaseValue = baseValue + 1;
					string text;
					string text2;
					string text3;
					this.GetEntryDice(out text, out text2, out text3);
					GetLevelUpDiceEvent.GetFor(this.ParentObject, num, ref text, ref text2, ref text3);
					bool flag3 = this.ParentObject.IsMutant();
					int num2 = this.RollHP(text);
					int num3 = this.RollSP(text2);
					int num4 = (flag3 ? this.RollMP(text3) : 0);
					int num5 = ((num % 3 == 0 && num % 6 != 0) ? 1 : 0);
					int num6 = ((num % 3 == 0 && num % 6 == 0) ? 1 : 0);
					int num7 = ((flag3 && (num + 5) % 10 == 0 && !this.ParentObject.IsEsper()) ? 3 : 0);
					GetLevelUpPointsEvent.GetFor(this.ParentObject, num, ref num2, ref num3, ref num4, ref num5, ref num6, ref num7);
					StringBuilder stringBuilder = (beforeLevelGainedEvent.Detail ? beforeLevelGainedEvent.Message : null);
					this.AddHitpoints(stringBuilder, num2);
					this.AddSkillPoints(stringBuilder, num3);
					this.AddMutationPoints(stringBuilder, num4);
					this.AddAttributePoints(stringBuilder, num5);
					this.AddAttributeBonus(stringBuilder, num6);
					if (flag)
					{
						IComponent<GameObject>.PlayUISound("sfx_characterMod_level_gain", 1f, false, SoundRequest.SoundEffectType.None);
					}
					else
					{
						base.PlayWorldSound("sfx_npc_level gain", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
					}
					this.RenderParticlesAt(this.ParentObject.CurrentCell);
					if (beforeLevelGainedEvent.Detail && !beforeLevelGainedEvent.Message.IsNullOrEmpty())
					{
						Popup.Show(beforeLevelGainedEvent.Message.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else
					{
						base.DidX("gain", "a level", "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
					}
					Leveler.RapidAdvancement(num7, this.ParentObject);
					this.SifrahInsights();
					ItemNaming.Opportunity(this.ParentObject, Kill, InfluencedBy, ZoneID, "Level", 6, 0, 0, 3, false);
					AfterLevelGainedEvent.Send(beforeLevelGainedEvent);
					if (ParentEvent != null)
					{
						ParentEvent.ProcessChildEvent(beforeLevelGainedEvent);
					}
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06009800 RID: 38912 RVA: 0x00373024 File Offset: 0x00371224
		public void RenderParticlesAt(Cell C)
		{
			if (C == null || !C.IsVisible())
			{
				return;
			}
			ParticleManager particleManager = XRLCore.ParticleManager;
			particleManager.AddSinusoidal("&W*", (float)C.X, (float)C.Y, 1.5f * (float)Stat.Random(1, 6), 0.1f * (float)Stat.Random(1, 60), 0.1f + 0.025f * (float)Stat.Random(0, 4), 1f, 0f, 0f, -0.1f - 0.05f * (float)Stat.Random(1, 6), 999, 0L);
			particleManager.AddSinusoidal("&Y*", (float)C.X, (float)C.Y, 1.5f * (float)Stat.Random(1, 6), 0.1f * (float)Stat.Random(1, 60), 0.1f + 0.025f * (float)Stat.Random(0, 4), 1f, 0f, 0f, -0.1f - 0.05f * (float)Stat.Random(1, 6), 999, 0L);
			particleManager.AddSinusoidal("&W\r", (float)C.X, (float)C.Y, 1.5f * (float)Stat.Random(1, 6), 0.1f * (float)Stat.Random(1, 60), 0.1f + 0.025f * (float)Stat.Random(0, 4), 1f, 0f, 0f, -0.1f - 0.05f * (float)Stat.Random(1, 6), 999, 0L);
			particleManager.AddSinusoidal("&Y\u000e", (float)C.X, (float)C.Y, 1.5f * (float)Stat.Random(1, 6), 0.1f * (float)Stat.Random(1, 60), 0.1f + 0.025f * (float)Stat.Random(0, 4), 1f, 0f, 0f, -0.1f - 0.05f * (float)Stat.Random(1, 6), 999, 0L);
		}

		// Token: 0x06009801 RID: 38913 RVA: 0x00373214 File Offset: 0x00371414
		public static void RapidAdvancement(int Amount, GameObject ParentObject)
		{
			if (Amount <= 0)
			{
				return;
			}
			SyncMutationLevelsEvent.Send(ParentObject);
			string @for = GetMutationTermEvent.GetFor(ParentObject, null);
			bool flag = ParentObject.IsPlayer() && ParentObject.Stat("MP", 0) >= 4;
			bool flag2 = false;
			if (flag && Popup.ShowYesNo("Your genome enters an excited state! Would you like to spend {{rules|4}} mutation points to buy " + Grammar.A(@for, false) + " before rapidly mutating?", "Sounds/UI/ui_notification_question", false, DialogResult.Yes, null) == DialogResult.Yes)
			{
				flag2 = MutationsAPI.BuyRandomMutation(ParentObject, 4, false, @for);
			}
			List<BaseMutation> list = (from m in ParentObject.GetPhysicalMutations()
				where m.CanLevel()
				select m).ToList<BaseMutation>();
			if (list.Count <= 0)
			{
				if (flag2)
				{
					Popup.Show("You have no physical " + Grammar.Pluralize(@for) + " to rapidly advance!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				return;
			}
			if (!flag && ParentObject.IsPlayer())
			{
				Popup.Show("Your genome enters an excited state!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			if (ParentObject.IsPlayer())
			{
				string[] array = list.Select<BaseMutation, string>((BaseMutation m) => m.GetDisplayName(true) + " ({{C|" + m.Level.ToString() + "}})").ToArray<string>();
				int num = Popup.PickOption("Choose a physical " + @for + " to rapidly advance.", null, "", "Sounds/Misc/sfx_characterMod_mutation_windowPopup", array, null, null, null, null, null, null, 0, 60, 0, -1, false, false, false, true, false, null, null);
				Popup.Show(string.Concat(new string[]
				{
					"You have rapidly advanced ",
					list[num].GetDisplayName(true),
					" by ",
					Grammar.Cardinal(Amount),
					" ranks to rank {{C|",
					(list[num].Level + Amount).ToString(),
					"}}!"
				}), null, "Sounds/Misc/sfx_characterMod_mutation_rankUp_quickSuccession", true, true, true, true, null);
				list[num].RapidLevel(Amount, true);
				return;
			}
			list.GetRandomElement<BaseMutation>(null).RapidLevel(Amount, true);
		}

		// Token: 0x06009802 RID: 38914 RVA: 0x00373408 File Offset: 0x00371608
		public void SifrahInsights()
		{
			if (!SifrahGame.Installed)
			{
				return;
			}
			if (this.ParentObject.HasSkill("Tinkering_Tinker1") && 10.in100())
			{
				TinkeringSifrah.AwardInsight();
			}
			if (this.ParentObject.HasSkill("Persuasion_Proselytize") && 10.in100())
			{
				SocialSifrah.AwardInsight();
			}
			if (this.ParentObject.HasSkill("Customs_Sharer") && 10.in100())
			{
				RitualSifrah.AwardInsight();
			}
			if (this.ParentObject.HasSkill("Discipline_Meditate") && this.ParentObject.GetPsychicGlimmer(null) > 0 && 10.in100())
			{
				PsionicSifrah.AwardInsight();
			}
		}

		// Token: 0x04003F41 RID: 16193
		[NonSerialized]
		public static bool PlayerLedPrompt;
	}
}
