using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;
using XRL.World.Capabilities;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D59 RID: 3417
	[Serializable]
	public class LifeDrain : Effect, ITierInitialized
	{
		// Token: 0x0600870D RID: 34573 RVA: 0x00307D83 File Offset: 0x00305F83
		public LifeDrain()
		{
			this.DisplayName = "syphoned";
		}

		// Token: 0x0600870E RID: 34574 RVA: 0x00307DA9 File Offset: 0x00305FA9
		public LifeDrain(int Duration, int Level, string DamagePerRound, GameObject Drainer)
			: this()
		{
			this.Damage = DamagePerRound;
			this.Duration = Duration;
			this.Drainer = Drainer;
			this.Level = Level;
		}

		// Token: 0x0600870F RID: 34575 RVA: 0x00307DCE File Offset: 0x00305FCE
		public LifeDrain(int Duration, int Level, string DamagePerRound, GameObject Drainer, bool RealityDistortionBased)
			: this(Duration, Level, DamagePerRound, Drainer)
		{
			this.RealityDistortionBased = RealityDistortionBased;
		}

		// Token: 0x06008710 RID: 34576 RVA: 0x00307DE4 File Offset: 0x00305FE4
		public void Initialize(int Tier)
		{
			Tier = Stat.Random(Tier - 1, Tier + 1);
			if (Tier < 1)
			{
				Tier = 1;
			}
			if (Tier > 8)
			{
				Tier = 8;
			}
			this.Level = Stat.Random(-1, 1) + Tier * 2;
			if (Tier < 1)
			{
				Tier = 1;
			}
			if (Tier > 8)
			{
				Tier = 8;
			}
			this.Damage = this.Level.ToString();
			this.Duration = 20;
			this.RealityDistortionBased = true;
		}

		// Token: 0x06008711 RID: 34577 RVA: 0x00307E4B File Offset: 0x0030604B
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008712 RID: 34578 RVA: 0x00307E4E File Offset: 0x0030604E
		public override int GetEffectType()
		{
			return 33587204;
		}

		// Token: 0x06008713 RID: 34579 RVA: 0x00307E55 File Offset: 0x00306055
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008714 RID: 34580 RVA: 0x00307E58 File Offset: 0x00306058
		public override string GetDetails()
		{
			return this.Damage + " life drained per turn.";
		}

		// Token: 0x06008715 RID: 34581 RVA: 0x00307E6C File Offset: 0x0030606C
		public override bool Apply(GameObject Object)
		{
			if (!LifeDrain.IsValidTarget(Object))
			{
				return false;
			}
			if (!Object.FireEvent("ApplyLifeDrain"))
			{
				return false;
			}
			if (this.Drainer == null)
			{
				if (Object.Target != null)
				{
					this.Drainer = Object.Target;
				}
				else
				{
					Cell currentCell = Object.CurrentCell;
					List<GameObject> list = currentCell.ParentZone.FastSquareVisibility(currentCell.X, currentCell.Y, 6, "Physics", Object, false, false, null);
					if (list.Count > 0)
					{
						this.Drainer = list.GetRandomElement<GameObject>(null);
					}
					else
					{
						this.Drainer = Object;
					}
				}
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_negativeVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			GameObject drainer = this.Drainer;
			if (drainer != null)
			{
				drainer.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_positiveVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			IComponent<GameObject>.XDidYToZ(this.Drainer, "bond", "with", Object, null, ".", null, null, null, Object, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			Object.ParticleText("*bonded*", IComponent<GameObject>.ConsequentialColorChar(null, Object), false, 1.5f, -8f);
			IComponent<GameObject>.XDidYToZ(this.Drainer, "begin", "to drain life essence from", Object, null, "!", null, null, null, Object, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			if (this.Drainer.IsPlayer() && this.Drainer.Target == null)
			{
				this.Drainer.Target = Object;
			}
			if (Object.IsPlayer())
			{
				AutoAct.Interrupt(null, null, null, false);
			}
			else if (Object.IsPlayerLed() && !Object.IsTrifling)
			{
				if (Object.IsVisible())
				{
					AutoAct.Interrupt(null, null, Object, true);
				}
				else if (Object.IsAudible(IComponent<GameObject>.ThePlayer, 20))
				{
					AutoAct.Interrupt("you hear a cry of distress from " + Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false), null, Object, true);
				}
			}
			return true;
		}

		// Token: 0x06008716 RID: 34582 RVA: 0x00308076 File Offset: 0x00306276
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x06008717 RID: 34583 RVA: 0x0030809C File Offset: 0x0030629C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (E.Actor == this.Drainer && GameObject.Validate(ref this.Drainer))
			{
				E.AddAction("CancelLifeDrain", "cancel life drain", "CancelLifeDrain", null, 'c', false, 0, 0, false, true, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008718 RID: 34584 RVA: 0x003080F0 File Offset: 0x003062F0
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "CancelLifeDrain" && E.Actor == this.Drainer)
			{
				IComponent<GameObject>.XDidYToZ(E.Actor, "release", base.Object, "from " + E.Actor.its + " life drain", null, null, null, E.Actor, null, false, false, false, false, false, null, null, null, false, false, false, false, true, null);
				base.Object.RemoveEffect(this, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008719 RID: 34585 RVA: 0x0030817C File Offset: 0x0030637C
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (!GameObject.Validate(ref this.Drainer) || !this.Drainer.HasHitpoints())
			{
				this.Duration = 0;
				this.Drainer = null;
				return true;
			}
			if (!LifeDrain.IsValidTarget(base.Object))
			{
				this.Duration = 0;
				return true;
			}
			if (this.RealityDistortionBased && (!IComponent<GameObject>.CheckRealityDistortionUsability(this.Drainer, null, this.Drainer, null, this.Drainer.GetPart<LifeDrain>(), null, 0) || !IComponent<GameObject>.CheckRealityDistortionAccessibility(base.Object, null, this.Drainer, null, this.Drainer.GetPart<LifeDrain>(), 0)))
			{
				this.Duration = 0;
				return true;
			}
			if (this.Duration > 0)
			{
				if (Stat.Random(1, 8) + Math.Max(this.Drainer.StatMod("Ego", 0), this.Level) > Stats.GetCombatMA(base.Object) + 4)
				{
					base.Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_life_drain", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					int num = this.Damage.RollCached();
					if (num > 0 && base.Object.TakeDamage(ref num, "Drain Unavoidable", null, null, this.Drainer, null, null, null, null, "from %t life drain!", false, false, true, false, false, false, false, false, false, 5, null) && num > 0)
					{
						this.Drainer.Heal(num, true, true, true);
					}
				}
				else if (this.Drainer.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage(base.Object.Does("resist", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " your life drain!", 'r', true);
				}
				else if (base.Object.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("You resist " + this.Drainer.poss("life drain", true) + "!", 'g', true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600871A RID: 34586 RVA: 0x00308388 File Offset: 0x00306588
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (num > 25 && num < 35)
			{
				E.Tile = null;
				E.RenderString = "\u0003";
				E.ColorString = "&K^k";
			}
			return true;
		}

		// Token: 0x04003A0D RID: 14861
		public string Damage = "1";

		// Token: 0x04003A0E RID: 14862
		public int SaveTarget = 20;

		// Token: 0x04003A0F RID: 14863
		public GameObject Drainer;

		// Token: 0x04003A10 RID: 14864
		public int Level;

		// Token: 0x04003A11 RID: 14865
		public bool RealityDistortionBased;
	}
}
