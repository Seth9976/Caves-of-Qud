using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000D6C RID: 3436
	[Serializable]
	public class Paralyzed : Effect, ITierInitialized
	{
		// Token: 0x060087FF RID: 34815 RVA: 0x0030BCEF File Offset: 0x00309EEF
		public Paralyzed()
		{
			this.DisplayName = "{{C|paralyzed}}";
		}

		// Token: 0x06008800 RID: 34816 RVA: 0x0030BD02 File Offset: 0x00309F02
		public Paralyzed(int Duration, int SaveTarget)
			: this()
		{
			this.SaveTarget = SaveTarget;
			this.Duration = Duration;
		}

		// Token: 0x06008801 RID: 34817 RVA: 0x0030BD18 File Offset: 0x00309F18
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
			if (Tier < 2)
			{
				this.Duration = Stat.Roll("1d3+1", null);
			}
			else
			{
				this.Duration = Stat.Roll("1d3+" + Math.Min(7, Tier * 2 / 3 + 1).ToString(), null);
			}
			this.SaveTarget = 14 + Tier * 2 * 2;
		}

		// Token: 0x06008802 RID: 34818 RVA: 0x0030BD90 File Offset: 0x00309F90
		public override int GetEffectType()
		{
			return 117440516;
		}

		// Token: 0x06008803 RID: 34819 RVA: 0x0030BD98 File Offset: 0x00309F98
		public override bool SameAs(Effect e)
		{
			Paralyzed paralyzed = e as Paralyzed;
			return paralyzed.DVPenalty == this.DVPenalty && paralyzed.SaveTarget == this.SaveTarget && base.SameAs(e);
		}

		// Token: 0x06008804 RID: 34820 RVA: 0x0030BDD3 File Offset: 0x00309FD3
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008805 RID: 34821 RVA: 0x0030BDD6 File Offset: 0x00309FD6
		public override string GetDetails()
		{
			if (this.DVPenalty < 0)
			{
				return "Can't move or attack.\n" + this.DVPenalty.ToString() + " DV";
			}
			return "Can't move or attack.\nDV set to 0.";
		}

		// Token: 0x06008806 RID: 34822 RVA: 0x0030BE04 File Offset: 0x0030A004
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<Paralyzed>())
			{
				Paralyzed effect = Object.GetEffect<Paralyzed>();
				if (this.Duration > effect.Duration)
				{
					effect.Duration = this.Duration;
				}
				return true;
			}
			if (!Object.FireEvent("ApplyParalyze"))
			{
				return false;
			}
			this.ApplyStats();
			base.DidX("are", "paralyzed", "!", null, null, null, Object, false, false, null, null, false, false, false, false, false, null);
			Object.ParticleText("*paralyzed*", IComponent<GameObject>.ConsequentialColorChar(null, Object), false, 1.5f, -8f);
			return true;
		}

		// Token: 0x06008807 RID: 34823 RVA: 0x0030BE92 File Offset: 0x0030A092
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
			base.Remove(Object);
		}

		// Token: 0x06008808 RID: 34824 RVA: 0x0030BEA4 File Offset: 0x0030A0A4
		private void ApplyStats()
		{
			int combatDV = Stats.GetCombatDV(base.Object);
			if (combatDV > 0)
			{
				this.DVPenalty += combatDV;
				base.StatShifter.SetStatShift(base.Object, "DV", -this.DVPenalty, false);
				return;
			}
			this.DVPenalty = 0;
		}

		// Token: 0x06008809 RID: 34825 RVA: 0x0030BEF6 File Offset: 0x0030A0F6
		private void UnapplyStats()
		{
			this.DVPenalty = 0;
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x0600880A RID: 34826 RVA: 0x0030BF10 File Offset: 0x0030A110
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<CanChangeMovementModeEvent>.ID || ID == PooledEvent<GetCompanionStatusEvent>.ID || ID == PooledEvent<IsConversationallyResponsiveEvent>.ID;
		}

		// Token: 0x0600880B RID: 34827 RVA: 0x0030BF40 File Offset: 0x0030A140
		public override bool HandleEvent(CanChangeMovementModeEvent E)
		{
			if (this.Duration > 0 && !E.Involuntary && E.Object == base.Object)
			{
				if (E.ShowMessage)
				{
					E.Object.Fail("You are paralyzed!");
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600880C RID: 34828 RVA: 0x0030BF8E File Offset: 0x0030A18E
		public override bool HandleEvent(GetCompanionStatusEvent E)
		{
			if (E.Object == base.Object)
			{
				E.AddStatus("paralyzed", 90);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600880D RID: 34829 RVA: 0x0030BFB2 File Offset: 0x0030A1B2
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0)
			{
				if (base.Object.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("You are {{C|paralyzed}}.", null, true);
				}
				E.PreventAction = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600880E RID: 34830 RVA: 0x0030BFE4 File Offset: 0x0030A1E4
		public override bool HandleEvent(IsConversationallyResponsiveEvent E)
		{
			if (E.Speaker == base.Object && !E.Mental)
			{
				E.Message = base.Object.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + base.Object.Is + " utterly unresponsive.";
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600880F RID: 34831 RVA: 0x0030C058 File Offset: 0x0030A258
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("CanChangeBodyPosition");
			Registrar.Register("CanMoveExtremities");
			Registrar.Register("IsMobile");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008810 RID: 34832 RVA: 0x0030C0A4 File Offset: 0x0030A2A4
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (num > 15 && num < 30)
			{
				E.Tile = null;
				E.RenderString = "X";
				E.ColorString = "&C^c";
			}
			return true;
		}

		// Token: 0x06008811 RID: 34833 RVA: 0x0030C0E4 File Offset: 0x0030A2E4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "IsMobile")
			{
				if (this.Duration > 0)
				{
					return false;
				}
			}
			else if (E.ID == "CanChangeBodyPosition" || E.ID == "CanMoveExtremities")
			{
				if (this.Duration > 0 && !E.HasFlag("Involuntary"))
				{
					if (E.HasFlag("ShowMessage") && base.Object.IsPlayer())
					{
						Popup.ShowFail("You are {{C|paralyzed}}!", true, true, true);
					}
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

		// Token: 0x04003A39 RID: 14905
		public int DVPenalty;

		// Token: 0x04003A3A RID: 14906
		public int SaveTarget;
	}
}
