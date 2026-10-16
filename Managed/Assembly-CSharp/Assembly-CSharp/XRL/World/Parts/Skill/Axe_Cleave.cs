using System;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200131D RID: 4893
	[Serializable]
	public class Axe_Cleave : BaseSkill
	{
		// Token: 0x0600BF53 RID: 48979 RVA: 0x00461221 File Offset: 0x0045F421
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerAfterAttack");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BF54 RID: 48980 RVA: 0x00461238 File Offset: 0x0045F438
		private static void ProcessPenalty(Effect FX)
		{
			IShatterEffect shatterEffect = FX as IShatterEffect;
			if (shatterEffect != null)
			{
				Axe_Cleave.Penalty += shatterEffect.GetPenalty();
			}
		}

		// Token: 0x0600BF55 RID: 48981 RVA: 0x00461260 File Offset: 0x0045F460
		private static void ProcessPenalties(GameObject obj)
		{
			foreach (Effect effect in obj.Effects)
			{
				if (effect is IShatterEffect && effect.ClassName == Axe_Cleave.PenaltyEffect)
				{
					Axe_Cleave.ProcessPenalty(effect);
				}
			}
		}

		// Token: 0x0600BF56 RID: 48982 RVA: 0x004612CC File Offset: 0x0045F4CC
		public static int GetCurrentPenalty(GameObject who, string EffectName, string EquipmentEffectName)
		{
			Axe_Cleave.Penalty = 0;
			if (EffectName != null)
			{
				Axe_Cleave.PenaltyEffect = EffectName;
				Axe_Cleave.ProcessPenalties(who);
			}
			if (EquipmentEffectName != null)
			{
				Axe_Cleave.PenaltyEffect = EquipmentEffectName;
				who.ForeachEquippedObject(new Action<GameObject>(Axe_Cleave.ProcessPenalties));
			}
			return Axe_Cleave.Penalty;
		}

		// Token: 0x0600BF57 RID: 48983 RVA: 0x00461304 File Offset: 0x0045F504
		public static void PerformCleave(GameObject Attacker, GameObject Defender, GameObject Weapon, string Skill = null, string Properties = null, int Chance = 75, int AdjustAVPenalty = 0, int? MaxAVPenalty = null)
		{
			if (Attacker == null || Defender == null || Weapon == null)
			{
				return;
			}
			if (Defender.HasPart<Gas>() || Defender.HasPart<NoDamage>())
			{
				return;
			}
			MeleeWeapon part = Weapon.GetPart<MeleeWeapon>();
			if (Skill != null && (part == null || part.Skill != Skill))
			{
				return;
			}
			string stat = part.Stat;
			string text;
			string text2;
			string text3;
			string text4;
			string text5;
			if (Statistic.IsMental(stat))
			{
				text = "MA";
				text2 = "mental armor";
				text3 = "ShatterMentalArmor";
				text4 = null;
				text5 = "CanApplyShatterMentalArmor";
			}
			else
			{
				text = "AV";
				text2 = "armor";
				text3 = "ShatterArmor";
				text4 = "ShatteredArmor";
				text5 = "CanApplyShatterArmor";
			}
			if (Defender != null && Defender.HasStat(text) && Defender.FireEvent(text5) && CanApplyEffectEvent.Check(Defender, text3, 0))
			{
				int num = Attacker.StatMod(stat, 0);
				int num2;
				if (MaxAVPenalty == null)
				{
					num2 = num / 2 + AdjustAVPenalty;
					if (num % 2 == 1)
					{
						num2++;
					}
				}
				else
				{
					num2 = MaxAVPenalty.Value + AdjustAVPenalty;
				}
				if (num2 < 1)
				{
					num2 = 1;
				}
				int num3 = 1;
				bool flag = Properties != null && Properties.HasDelimitedSubstring(',', "Charging", StringComparison.Ordinal) && Attacker.HasSkill("Cudgel_ChargingStrike");
				if (flag)
				{
					num3++;
				}
				int @for = GetCleaveAmountEvent.GetFor(Weapon, Attacker, Defender, num3);
				if (@for <= 0)
				{
					return;
				}
				if (num2 < @for)
				{
					num2 = @for;
				}
				num2 = GetCleaveMaxPenaltyEvent.GetFor(Weapon, Attacker, Defender, num2, flag);
				if (num2 <= 0)
				{
					return;
				}
				Statistic stat2 = Defender.GetStat(text);
				int num4 = Axe_Cleave.GetCurrentPenalty(Defender, text3, text4);
				if (stat2.Value > stat2.Min && num4 < num2)
				{
					GetSpecialEffectChanceEvent.GetFor(Attacker, Weapon, "Skill Cleave", Chance, Defender, null, true, false);
					if (Chance.in100())
					{
						bool flag2 = false;
						IShatterEffect shatterEffect = Activator.CreateInstance(ModManager.ResolveType("XRL.World.Effects." + text3, false, false, true)) as IShatterEffect;
						if (shatterEffect != null)
						{
							shatterEffect.Duration = 300;
							shatterEffect.SetOwner(Attacker);
							int num5 = shatterEffect.GetPenalty();
							while (num5 < @for && num4 < num2 - 1)
							{
								shatterEffect.IncrementPenalty();
								num4++;
								num5++;
							}
							flag2 = Defender.ApplyEffect(shatterEffect, null);
						}
						if (flag2)
						{
							if (flag)
							{
								if (Attacker.IsPlayer())
								{
									IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
									{
										"The momentum from your charge causes your ",
										Weapon.ShortDisplayNameSingle,
										" to cleave deeper through ",
										Defender.poss(text2, true),
										"."
									}), 'g', true);
								}
								Defender.DustPuff("&c", 15);
							}
							if (Attacker.IsPlayer())
							{
								IComponent<GameObject>.AddPlayerMessage("You cleave through " + Defender.poss(text2, true) + ".", 'G', true);
								return;
							}
							if (Defender.IsPlayer())
							{
								IComponent<GameObject>.AddPlayerMessage(Attacker.Does("cleave", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " through your " + text2 + ".", 'R', true);
								return;
							}
							if (IComponent<GameObject>.Visible(Defender))
							{
								if (Defender.IsPlayerLed())
								{
									IComponent<GameObject>.AddPlayerMessage(Attacker.Does("cleave", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " through " + Defender.poss(text2, true) + ".", 'r', true);
									return;
								}
								IComponent<GameObject>.AddPlayerMessage(Attacker.Does("cleave", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " through " + Defender.poss(text2, true) + ".", 'g', true);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600BF58 RID: 48984 RVA: 0x004616AC File Offset: 0x0045F8AC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AttackerAfterAttack" && !E.HasFlag("Critical"))
			{
				Axe_Cleave.PerformCleave(E.GetGameObjectParameter("Attacker"), E.GetGameObjectParameter("Defender"), E.GetGameObjectParameter("Weapon"), "Axe", E.GetStringParameter("Properties", null), 75, 0, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BF59 RID: 48985 RVA: 0x00461722 File Offset: 0x0045F922
		public override bool AddSkill(GameObject GO)
		{
			return base.AddSkill(GO);
		}

		// Token: 0x0600BF5A RID: 48986 RVA: 0x0046172B File Offset: 0x0045F92B
		public override bool RemoveSkill(GameObject GO)
		{
			return base.RemoveSkill(GO);
		}

		// Token: 0x04004D15 RID: 19733
		[NonSerialized]
		private static int Penalty;

		// Token: 0x04004D16 RID: 19734
		[NonSerialized]
		private static string PenaltyEffect;
	}
}
