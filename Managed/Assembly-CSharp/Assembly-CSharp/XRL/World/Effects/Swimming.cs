using System;
using XRL.Core;
using XRL.Liquids;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D9A RID: 3482
	[Serializable]
	public class Swimming : Effect
	{
		// Token: 0x06008AD9 RID: 35545 RVA: 0x00317AFB File Offset: 0x00315CFB
		public Swimming()
		{
			this.DisplayName = "{{B|swimming}}";
			this.Duration = 1;
		}

		// Token: 0x06008ADA RID: 35546 RVA: 0x00317B18 File Offset: 0x00315D18
		public override int GetEffectType()
		{
			int num = 16777344;
			if (base.Object == null || Swimming.GetTargetMoveSpeedPenalty(base.Object) > 0)
			{
				num |= 33554432;
			}
			return num;
		}

		// Token: 0x06008ADB RID: 35547 RVA: 0x00317B4A File Offset: 0x00315D4A
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008ADC RID: 35548 RVA: 0x00317B4D File Offset: 0x00315D4D
		public override string GetDescription()
		{
			return "{{B|swimming}}";
		}

		// Token: 0x06008ADD RID: 35549 RVA: 0x00317B54 File Offset: 0x00315D54
		public override bool SuppressInLookDisplay()
		{
			return true;
		}

		// Token: 0x06008ADE RID: 35550 RVA: 0x00317B58 File Offset: 0x00315D58
		public override string GetDetails()
		{
			this.CheckMoveSpeedPenalty();
			int num = -(this.MoveSpeedShiftApplied ?? 50);
			if (num == 0)
			{
				return "Moving at full speed.";
			}
			return num.Signed(false) + " move speed.";
		}

		// Token: 0x06008ADF RID: 35551 RVA: 0x00317BA4 File Offset: 0x00315DA4
		public static int GetTargetMoveSpeedPenalty(GameObject obj)
		{
			int num = 50;
			GetSwimmingPerformanceEvent.GetFor(obj, ref num);
			return num;
		}

		// Token: 0x06008AE0 RID: 35552 RVA: 0x00317BC0 File Offset: 0x00315DC0
		public void CheckMoveSpeedPenalty()
		{
			if (base.Object != null)
			{
				int targetMoveSpeedPenalty = Swimming.GetTargetMoveSpeedPenalty(base.Object);
				int num = targetMoveSpeedPenalty;
				int? moveSpeedShiftApplied = this.MoveSpeedShiftApplied;
				if (!((num == moveSpeedShiftApplied.GetValueOrDefault()) & (moveSpeedShiftApplied != null)))
				{
					base.StatShifter.SetStatShift("MoveSpeed", targetMoveSpeedPenalty, false);
					this.MoveSpeedShiftApplied = new int?(targetMoveSpeedPenalty);
					return;
				}
			}
			else
			{
				this.MoveSpeedShiftApplied = null;
			}
		}

		// Token: 0x06008AE1 RID: 35553 RVA: 0x00317C28 File Offset: 0x00315E28
		public void RemoveMoveSpeedPenalty()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x06008AE2 RID: 35554 RVA: 0x00317C35 File Offset: 0x00315E35
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<Swimming>())
			{
				return false;
			}
			if (!Object.CanChangeMovementMode("Swimming", false, true, false, false))
			{
				return false;
			}
			this.CheckMoveSpeedPenalty();
			Object.FireEvent("StartSwimming");
			Object.MovementModeChanged("Swimming", true);
			return true;
		}

		// Token: 0x06008AE3 RID: 35555 RVA: 0x00317C73 File Offset: 0x00315E73
		public override void Remove(GameObject Object)
		{
			this.RemoveMoveSpeedPenalty();
			Object.UnregisterEffectEvent(this, "MovementModeChanged");
			Object.MovementModeChanged("NotSwimming", true);
			Object.FireEvent("StopSwimming");
			base.Remove(Object);
		}

		// Token: 0x06008AE4 RID: 35556 RVA: 0x00317CA6 File Offset: 0x00315EA6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanChangeMovementModeEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == EnteredCellEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID;
		}

		// Token: 0x06008AE5 RID: 35557 RVA: 0x00317CD4 File Offset: 0x00315ED4
		public override bool HandleEvent(CanChangeMovementModeEvent E)
		{
			if (this.Duration > 0 && E.Object == base.Object && E.To != "Flying" && E.To != "Wading" && E.To != "Stuck" && E.To != "Engulfed")
			{
				if (E.ShowMessage)
				{
					E.Object.Fail("You cannot do that while swimming.");
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008AE6 RID: 35558 RVA: 0x00317D62 File Offset: 0x00315F62
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.Validate(null);
			return base.HandleEvent(E);
		}

		// Token: 0x06008AE7 RID: 35559 RVA: 0x00317D72 File Offset: 0x00315F72
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.Validate(E.Cell);
			return base.HandleEvent(E);
		}

		// Token: 0x06008AE8 RID: 35560 RVA: 0x00317D87 File Offset: 0x00315F87
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Reference)
			{
				this.Validate(null);
				if (this.Duration > -1)
				{
					E.AddTag("{{y|[{{B|swimming}}]}}", 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008AE9 RID: 35561 RVA: 0x00317DB4 File Offset: 0x00315FB4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("MovementModeChanged");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008AEA RID: 35562 RVA: 0x00317DE0 File Offset: 0x00315FE0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "MovementModeChanged")
			{
				base.Object.RemoveEffect(this, true);
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

		// Token: 0x06008AEB RID: 35563 RVA: 0x00317E48 File Offset: 0x00316048
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				E.RenderEffectIndicator("à", "Tiles2/status_swimming.bmp", "&B", "B", 45, 10);
			}
			Brain brain = base.Object.Brain;
			if (brain != null && brain.LimitToAquatic())
			{
				Cell currentCell = base.Object.CurrentCell;
				LiquidVolume liquidVolume;
				if (currentCell == null)
				{
					liquidVolume = null;
				}
				else
				{
					GameObject aquaticSupportFor = currentCell.GetAquaticSupportFor(base.Object);
					liquidVolume = ((aquaticSupportFor != null) ? aquaticSupportFor.LiquidVolume : null);
				}
				LiquidVolume liquidVolume2 = liquidVolume;
				if (liquidVolume2 != null)
				{
					BaseLiquid primaryLiquid = liquidVolume2.GetPrimaryLiquid();
					if (primaryLiquid != null)
					{
						primaryLiquid.RenderBackgroundPrimary(liquidVolume2, E);
					}
					BaseLiquid secondaryLiquid = liquidVolume2.GetSecondaryLiquid();
					if (secondaryLiquid != null)
					{
						secondaryLiquid.RenderBackgroundSecondary(liquidVolume2, E);
					}
				}
			}
			return true;
		}

		// Token: 0x06008AEC RID: 35564 RVA: 0x00317EE7 File Offset: 0x003160E7
		private void ApplyStats()
		{
			this.CheckMoveSpeedPenalty();
		}

		// Token: 0x06008AED RID: 35565 RVA: 0x00317EEF File Offset: 0x003160EF
		private void UnapplyStats()
		{
			this.RemoveMoveSpeedPenalty();
		}

		// Token: 0x06008AEE RID: 35566 RVA: 0x00317EF8 File Offset: 0x003160F8
		public void Validate(Cell C = null)
		{
			if (base.Object != null && (C != null || XRLCore.Core.Game.Segments > this.validatedOn))
			{
				this.validatedOn = XRLCore.Core.Game.Segments;
				if (C == null)
				{
					C = base.Object.CurrentCell;
				}
				if (C == null || !C.HasAquaticSupportFor(base.Object) || C.HasBridge() || base.Object.IsFlying)
				{
					this.Duration = 0;
					base.Object.RemoveEffect(this, true);
				}
			}
		}

		// Token: 0x04003ABD RID: 15037
		public const int BASE_MOVE_SPEED_PENALTY = 50;

		// Token: 0x04003ABE RID: 15038
		public int? MoveSpeedShiftApplied;

		// Token: 0x04003ABF RID: 15039
		[NonSerialized]
		private long validatedOn;
	}
}
