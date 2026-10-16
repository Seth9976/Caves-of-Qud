using System;
using Qud.API;
using XRL.Rules;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D25 RID: 3365
	[Serializable]
	public class Ironshank : Effect, ITierInitialized
	{
		// Token: 0x060084A1 RID: 33953 RVA: 0x002FED43 File Offset: 0x002FCF43
		public Ironshank()
		{
			this.DisplayName = "ironshank";
		}

		// Token: 0x060084A2 RID: 33954 RVA: 0x002FED58 File Offset: 0x002FCF58
		public override string GetDetails()
		{
			return string.Concat(new string[]
			{
				"Leg bones are fusing at the joints.\n{{C|-",
				this.Penalty.ToString(),
				"}} Move Speed\n{{C|+",
				this.AVBonus.ToString(),
				"}} AV\nWill continue to lose Move Speed and gain AV until Move Speed penalty reaches -80."
			});
		}

		// Token: 0x060084A3 RID: 33955 RVA: 0x002FEDA4 File Offset: 0x002FCFA4
		public override int GetEffectType()
		{
			return 100679700;
		}

		// Token: 0x060084A4 RID: 33956 RVA: 0x002FEDAB File Offset: 0x002FCFAB
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x060084A5 RID: 33957 RVA: 0x002FEDB0 File Offset: 0x002FCFB0
		public void SetPenalty(int Amount)
		{
			if (Amount < 0)
			{
				Amount = 0;
			}
			base.StatShifter.SetStatShift("MoveSpeed", Amount, false);
			this.AVBonus = Amount / 15;
			base.StatShifter.SetStatShift("AV", this.AVBonus, false);
			this.Penalty = Amount;
		}

		// Token: 0x060084A6 RID: 33958 RVA: 0x002FEE00 File Offset: 0x002FD000
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<Ironshank>())
			{
				return false;
			}
			if (!Ironshank.IsInfectable(Object))
			{
				return false;
			}
			if (!Object.FireEvent("ApplyDisease"))
			{
				return false;
			}
			if (!ApplyEffectEvent.Check(Object, "Disease", this, null))
			{
				return false;
			}
			if (!Object.FireEvent("ApplyIronshank"))
			{
				return false;
			}
			if (!ApplyEffectEvent.Check(Object, "Ironshank", this, null))
			{
				return false;
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_diseased", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			this.Duration = 1;
			this.SetPenalty(this.Penalty + Stat.Random(6, 10));
			if (Object.IsPlayer())
			{
				int num = Stat.Random(2, 6);
				Achievement.GET_IRONSHANK.Unlock();
				Popup.Show("You have contracted Ironshank! You feel the cartilage stretch as your leg bones grind together at the joints.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				JournalAPI.AddAccomplishment("You contracted ironshank.", "Woe to the scroundrels and dastards who conspired to have =name= contract Stiff Leg!", string.Format("Near the location of Golgotha, =name= was captured by bandits. {0} languished in captivity for {1} years, eventually contracting ironshank before escaping to {2}.", The.Player.GetPronounProvider(false).CapitalizedSubjective, num, The.Player.CurrentZone.GetTerrainDisplayName(true)), null, "general", MuralCategory.BodyExperienceBad, MuralWeight.Medium, null, -1L, true);
			}
			return true;
		}

		// Token: 0x060084A7 RID: 33959 RVA: 0x002FEF26 File Offset: 0x002FD126
		public override void Remove(GameObject Object)
		{
			base.StatShifter.RemoveStatShifts();
			base.Remove(Object);
		}

		// Token: 0x060084A8 RID: 33960 RVA: 0x002FEF3A File Offset: 0x002FD13A
		public override string GetDescription()
		{
			return "{{ironshank|ironshank}}";
		}

		// Token: 0x060084A9 RID: 33961 RVA: 0x002FEF41 File Offset: 0x002FD141
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DrinkingFrom");
			Registrar.Register("Eating");
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x060084AA RID: 33962 RVA: 0x002FEF6C File Offset: 0x002FD16C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				if (this.Duration > 0 && !Ironshank.IsInfectable(base.Object))
				{
					this.Duration = 0;
				}
				if (this.Duration > 0)
				{
					this.Count++;
					if (this.Count >= 1200 && this.DrankCure)
					{
						this.Count = 0;
						this.DrankCure = false;
						if (base.Object.IsPlayer())
						{
							Popup.Show("The pain in your joints subsides.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						this.SetPenalty(this.Penalty - Stat.Random(6, 10));
						if (this.Penalty <= 0)
						{
							this.Duration = 0;
							if (base.Object.IsPlayer())
							{
								Achievement.CURE_IRONSHANK.Unlock();
								Popup.Show("You are cured of ironshank.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
								JournalAPI.AddAccomplishment("You were cured of ironshank and your leg bones softened.", string.Concat(new string[]
								{
									"Blessed was the ",
									Calendar.GetDay(),
									" of ",
									Calendar.GetMonth(),
									", in the year of ",
									Calendar.GetYear().ToString(),
									" AR, when =name= was cured of ironing leg!"
								}), string.Concat(new string[]
								{
									"Some time in =year=, =name= came upon the bandits who had captured ",
									The.Player.GetPronounProvider(false).Objective,
									" and let ",
									The.Player.GetPronounProvider(false).Objective,
									" languish in captivity until ",
									The.Player.GetPronounProvider(false).Subjective,
									" contracted ironshank. =name= murdered their leader <spice.elements.",
									The.Player.GetMythicDomain(5, null),
									".murdermethods.!random> and cured ",
									The.Player.GetPronounProvider(false).Reflexive,
									" of the disease."
								}), null, "general", MuralCategory.BodyExperienceGood, MuralWeight.Medium, null, -1L, true);
							}
						}
					}
					else if (this.Count >= 4800)
					{
						this.Count = 0;
						if (this.Penalty < 80)
						{
							int num = Stat.Random(6, 10);
							if (this.Penalty + num >= 80)
							{
								this.SetPenalty(80);
								if (base.Object.IsPlayer())
								{
									Popup.Show("Your legs bones are nearly fused at the joints.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
								}
							}
							else
							{
								if (base.Object.IsPlayer())
								{
									IComponent<GameObject>.AddPlayerMessage("You feel the cartilage stretch as your leg bones grind together at the joints.", 'R', true);
								}
								this.SetPenalty(this.Penalty + num);
							}
						}
					}
				}
			}
			else if (E.ID == "DrinkingFrom")
			{
				LiquidVolume liquidVolume = E.GetGameObjectParameter("Container").LiquidVolume;
				if (liquidVolume.ContainsLiquid(The.Game.GetStringGameState("IronshankCure", "")) && liquidVolume.ContainsLiquid("gel"))
				{
					this.DrankCure = true;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x060084AB RID: 33963 RVA: 0x002FF254 File Offset: 0x002FD454
		public static bool IsInfectable(GameObject who)
		{
			return who.HasBodyPart(new Predicate<BodyPart>(Ironshank.IsInfectableLimb));
		}

		// Token: 0x060084AC RID: 33964 RVA: 0x002FF268 File Offset: 0x002FD468
		public static bool IsInfectableLimb(BodyPart Part)
		{
			return !(Part.Type != "Feet") && Part.IsCategoryLive() && Part.Mobility > 0;
		}

		// Token: 0x0400398E RID: 14734
		public int Count;

		// Token: 0x0400398F RID: 14735
		public int Penalty;

		// Token: 0x04003990 RID: 14736
		public int AVBonus;

		// Token: 0x04003991 RID: 14737
		public bool DrankCure;
	}
}
