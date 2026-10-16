using System;
using XRL.Core;
using XRL.Rules;
using XRL.World.Capabilities;

namespace XRL.World.Effects
{
	// Token: 0x02000D96 RID: 3478
	[Serializable]
	public class Stun : Effect, ITierInitialized
	{
		// Token: 0x06008A9B RID: 35483 RVA: 0x003169E3 File Offset: 0x00314BE3
		public Stun()
		{
			this.DisplayName = "{{C|stunned}}";
		}

		// Token: 0x06008A9C RID: 35484 RVA: 0x003169FE File Offset: 0x00314BFE
		public Stun(int Duration, int SaveTarget, bool DontStunIfPlayer = false)
			: this()
		{
			this.SaveTarget = SaveTarget;
			this.Duration = Duration;
			this.DontStunIfPlayer = DontStunIfPlayer;
		}

		// Token: 0x06008A9D RID: 35485 RVA: 0x00316A1B File Offset: 0x00314C1B
		public void Initialize(int Tier)
		{
			Tier = Tier.Constrain(Stat.Random(Tier - 2, Tier + 2));
			this.Duration = Stat.Random(1, 4);
			this.SaveTarget = 11 + Tier * 4;
		}

		// Token: 0x06008A9E RID: 35486 RVA: 0x00316A48 File Offset: 0x00314C48
		public override int GetEffectType()
		{
			return 117440514;
		}

		// Token: 0x06008A9F RID: 35487 RVA: 0x00316A4F File Offset: 0x00314C4F
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008AA0 RID: 35488 RVA: 0x00316A52 File Offset: 0x00314C52
		public override string GetDescription()
		{
			return "{{C|stunned}}";
		}

		// Token: 0x06008AA1 RID: 35489 RVA: 0x00316A59 File Offset: 0x00314C59
		public override string GetDetails()
		{
			if (this.DVPenalty < 0)
			{
				return "Can't take actions.\n" + this.DVPenalty.ToString() + " DV";
			}
			return "Can't take actions.\nDV set to 0.";
		}

		// Token: 0x06008AA2 RID: 35490 RVA: 0x00316A84 File Offset: 0x00314C84
		public override bool Apply(GameObject Object)
		{
			if (Object.Brain == null)
			{
				return false;
			}
			Stun effect = Object.GetEffect<Stun>();
			if (effect != null)
			{
				if (Object.IsPlayer() && this.DontStunIfPlayer)
				{
					this.Duration = 0;
					return false;
				}
				effect.Duration += this.Duration;
				if (effect.SaveTarget < this.SaveTarget)
				{
					effect.SaveTarget = this.SaveTarget;
				}
				return false;
			}
			else
			{
				if (!ApplyEffectEvent.Check(Object, "Stun", this, null))
				{
					return false;
				}
				if (!Object.FireEvent("ApplyStun"))
				{
					return false;
				}
				base.DidX("are", "stunned", "!", null, null, null, Object, false, false, null, null, false, false, false, false, false, null);
				Object.ParticleText("*stunned*", IComponent<GameObject>.ConsequentialColorChar(null, Object), false, 1.5f, -8f);
				Object.ForfeitTurn(false);
				this.ApplyStats();
				return true;
			}
		}

		// Token: 0x06008AA3 RID: 35491 RVA: 0x00316B5B File Offset: 0x00314D5B
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
			base.Remove(Object);
		}

		// Token: 0x06008AA4 RID: 35492 RVA: 0x00316B6C File Offset: 0x00314D6C
		private void ApplyStats()
		{
			int combatDV = Stats.GetCombatDV(base.Object);
			if (combatDV > 0)
			{
				this.DVPenalty = combatDV;
				base.StatShifter.SetStatShift("DV", -this.DVPenalty, false);
				return;
			}
			this.DVPenalty = 0;
		}

		// Token: 0x06008AA5 RID: 35493 RVA: 0x00316BB1 File Offset: 0x00314DB1
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts();
			this.DVPenalty = 0;
		}

		// Token: 0x06008AA6 RID: 35494 RVA: 0x00316BC5 File Offset: 0x00314DC5
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<IsConversationallyResponsiveEvent>.ID;
		}

		// Token: 0x06008AA7 RID: 35495 RVA: 0x00316BE4 File Offset: 0x00314DE4
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (E.Object == base.Object && this.Duration > 0)
			{
				if (this.SaveTarget < 0 || !base.Object.MakeSave("Toughness", this.SaveTarget, null, null, "Stun", false, false, false, false, null))
				{
					base.DidX("remain", "stunned", "!", null, null, null, base.Object, false, false, null, null, false, false, false, false, false, null);
					if (base.Object.IsPlayer())
					{
						XRLCore.Core.RenderDelay(500, true, true, false);
					}
					else
					{
						base.Object.ParticleText("*remains stunned*", IComponent<GameObject>.ConsequentialColorChar(null, base.Object), false, 1.5f, -8f);
					}
					base.Object.ForfeitTurn(false);
					if (this.Duration != 9999)
					{
						this.Duration--;
					}
					return false;
				}
				base.Object.ParticleText("*made save vs. stun*", IComponent<GameObject>.ConsequentialColorChar(base.Object, null), false, 1.5f, -8f);
				this.Duration = 0;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008AA8 RID: 35496 RVA: 0x00316D10 File Offset: 0x00314F10
		public override bool HandleEvent(IsConversationallyResponsiveEvent E)
		{
			if (E.Speaker == base.Object)
			{
				if (E.Mental && !E.Physical)
				{
					E.Message = base.Object.Poss("mind", true) + " is in disarray.";
				}
				else
				{
					E.Message = base.Object.Does("don't", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " seem to understand you.";
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008AA9 RID: 35497 RVA: 0x00316DA8 File Offset: 0x00314FA8
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("IsMobile");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008AAA RID: 35498 RVA: 0x00316DD4 File Offset: 0x00314FD4
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 10 && num < 25)
				{
					E.Tile = null;
					E.RenderString = "!";
					E.ColorString = "&C^c";
				}
			}
			return true;
		}

		// Token: 0x06008AAB RID: 35499 RVA: 0x00316E1C File Offset: 0x0031501C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "IsMobile")
			{
				if (this.Duration > 0)
				{
					return false;
				}
			}
			else if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyStats();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyStats();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003AB5 RID: 15029
		public int DVPenalty;

		// Token: 0x04003AB6 RID: 15030
		public int SaveTarget = 15;

		// Token: 0x04003AB7 RID: 15031
		public bool DontStunIfPlayer;
	}
}
