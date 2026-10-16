using System;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D92 RID: 3474
	[Serializable]
	public class Stasis : Effect
	{
		// Token: 0x06008A56 RID: 35414 RVA: 0x0031561F File Offset: 0x0031381F
		public Stasis()
		{
			this.DisplayName = "{{M|stasis}}";
			this.Duration = 1;
		}

		// Token: 0x06008A57 RID: 35415 RVA: 0x00315639 File Offset: 0x00313839
		public override string GetDescription()
		{
			return "{{M|in stasis}}";
		}

		// Token: 0x06008A58 RID: 35416 RVA: 0x00315640 File Offset: 0x00313840
		public override string GetDetails()
		{
			return "Suspended in spacetime. Cannot act or be interacted with.";
		}

		// Token: 0x06008A59 RID: 35417 RVA: 0x00315647 File Offset: 0x00313847
		public override int GetEffectType()
		{
			return 33558528;
		}

		// Token: 0x06008A5A RID: 35418 RVA: 0x00315650 File Offset: 0x00313850
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeAITakingActionEvent>.ID || ID == BeforeApplyDamageEvent.ID || ID == CanApplyEffectEvent.ID || ID == PooledEvent<CanBeDismemberedEvent>.ID || ID == PooledEvent<CanBeInvoluntarilyMovedEvent>.ID || ID == CanBeReplicatedEvent.ID || ID == PooledEvent<CanTemperatureReturnToAmbientEvent>.ID || ID == SingletonEvent<EarlyBeforeBeginTakeActionEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<GetCompanionStatusEvent>.ID || ID == PooledEvent<GetKineticResistanceEvent>.ID || ID == PooledEvent<IsConversationallyResponsiveEvent>.ID;
		}

		// Token: 0x06008A5B RID: 35419 RVA: 0x003156C9 File Offset: 0x003138C9
		public override bool HandleEvent(BeforeAITakingActionEvent E)
		{
			return E.Actor != base.Object && base.HandleEvent(E);
		}

		// Token: 0x06008A5C RID: 35420 RVA: 0x003156E2 File Offset: 0x003138E2
		public override bool HandleEvent(CanBeDismemberedEvent E)
		{
			return false;
		}

		// Token: 0x06008A5D RID: 35421 RVA: 0x003156E5 File Offset: 0x003138E5
		public override bool HandleEvent(GetCompanionStatusEvent E)
		{
			if (E.Object == base.Object)
			{
				E.AddStatus("in stasis", 200);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008A5E RID: 35422 RVA: 0x0031570C File Offset: 0x0031390C
		public override bool HandleEvent(CanTemperatureReturnToAmbientEvent E)
		{
			return E.Object != base.Object && base.HandleEvent(E);
		}

		// Token: 0x06008A5F RID: 35423 RVA: 0x00315725 File Offset: 0x00313925
		public override bool HandleEvent(CanBeInvoluntarilyMovedEvent E)
		{
			return E.Object != base.Object && base.HandleEvent(E);
		}

		// Token: 0x06008A60 RID: 35424 RVA: 0x0031573E File Offset: 0x0031393E
		public override bool HandleEvent(EarlyBeforeBeginTakeActionEvent E)
		{
			if (this.CheckMaintenance(null))
			{
				E.PreventAction = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008A61 RID: 35425 RVA: 0x00315758 File Offset: 0x00313958
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Actor != null)
			{
				if (E.Actor.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("Your attack bounces harmlessly off of " + base.Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", IComponent<GameObject>.ConsequentialColor(base.Object, null), true);
				}
				else if (IComponent<GameObject>.Visible(E.Actor) || IComponent<GameObject>.Visible(base.Object))
				{
					IComponent<GameObject>.AddPlayerMessage(E.Actor.Poss("attack", true) + " bounces harmlessly off of " + base.Object.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", IComponent<GameObject>.ConsequentialColor(base.Object, null), true);
				}
			}
			return false;
		}

		// Token: 0x06008A62 RID: 35426 RVA: 0x00315844 File Offset: 0x00313A44
		public override bool HandleEvent(IsConversationallyResponsiveEvent E)
		{
			if (E.Speaker == base.Object)
			{
				if (E.Mental && !E.Physical)
				{
					E.Message = "You can dimly sense " + base.Object.poss("mind", true) + ", but it is unresponsive.";
				}
				else
				{
					E.Message = base.Object.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + base.Object.Is + " utterly unresponsive.";
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008A63 RID: 35427 RVA: 0x003158E5 File Offset: 0x00313AE5
		public override bool HandleEvent(CanApplyEffectEvent E)
		{
			return false;
		}

		// Token: 0x06008A64 RID: 35428 RVA: 0x003158E8 File Offset: 0x00313AE8
		public override bool HandleEvent(CanBeReplicatedEvent E)
		{
			return false;
		}

		// Token: 0x06008A65 RID: 35429 RVA: 0x003158EB File Offset: 0x00313AEB
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.CheckMaintenance(null);
			return base.HandleEvent(E);
		}

		// Token: 0x06008A66 RID: 35430 RVA: 0x003158FC File Offset: 0x00313AFC
		public override bool HandleEvent(GetKineticResistanceEvent E)
		{
			E.LinearIncrease = 99999999;
			E.PercentageIncrease = 0;
			E.LinearReduction = 0;
			E.PercentageReduction = 0;
			return false;
		}

		// Token: 0x06008A67 RID: 35431 RVA: 0x0031591F File Offset: 0x00313B1F
		public override bool Apply(GameObject Object)
		{
			if (!this.MaintenanceSustained(Object))
			{
				return false;
			}
			if (Object.IsInStasis())
			{
				return false;
			}
			Object.ForfeitTurn(false);
			return true;
		}

		// Token: 0x06008A68 RID: 35432 RVA: 0x0031593E File Offset: 0x00313B3E
		public override void Remove(GameObject Object)
		{
			base.Remove(Object);
		}

		// Token: 0x06008A69 RID: 35433 RVA: 0x00315948 File Offset: 0x00313B48
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift(base.Object, "AcidResistance", 1000, false);
			base.StatShifter.SetStatShift(base.Object, "ColdResistance", 1000, false);
			base.StatShifter.SetStatShift(base.Object, "ElectricResistance", 1000, false);
			base.StatShifter.SetStatShift(base.Object, "HeatResistance", 1000, false);
		}

		// Token: 0x06008A6A RID: 35434 RVA: 0x003159C9 File Offset: 0x00313BC9
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x06008A6B RID: 35435 RVA: 0x003159DC File Offset: 0x00313BDC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
		}

		// Token: 0x06008A6C RID: 35436 RVA: 0x003159F4 File Offset: 0x00313BF4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyStats();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyStats();
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008A6D RID: 35437 RVA: 0x00315A2F File Offset: 0x00313C2F
		public bool CheckMaintenance(GameObject Skip = null)
		{
			if (!this.MaintenanceSustained(Skip))
			{
				GameObject @object = base.Object;
				if (@object != null)
				{
					@object.RemoveEffect(this, true);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06008A6E RID: 35438 RVA: 0x00315A54 File Offset: 0x00313C54
		public bool MaintenanceSustained(GameObject Skip = null)
		{
			if (!Stasis.EligibleForStasis(base.Object))
			{
				return false;
			}
			Cell currentCell = base.Object.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			int i = 0;
			int count = currentCell.Objects.Count;
			while (i < count)
			{
				if (currentCell.Objects[i] != Skip && currentCell.Objects[i].GetPart<Stasisfield>() != null && currentCell.Objects[i].PhaseMatches(base.Object))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06008A6F RID: 35439 RVA: 0x00315AD7 File Offset: 0x00313CD7
		public static bool EligibleForStasis(GameObject obj)
		{
			return GameObject.Validate(ref obj) && obj.IsReal && !obj.HasTagOrProperty("ForcefieldNullifier");
		}
	}
}
