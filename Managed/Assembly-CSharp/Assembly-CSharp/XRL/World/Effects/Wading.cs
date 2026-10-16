using System;
using XRL.Core;

namespace XRL.World.Effects
{
	// Token: 0x02000DA8 RID: 3496
	[Serializable]
	public class Wading : Effect
	{
		// Token: 0x06008B95 RID: 35733 RVA: 0x003199A0 File Offset: 0x00317BA0
		public Wading()
		{
			this.DisplayName = "{{B|wading}}";
			this.Duration = 1;
		}

		// Token: 0x06008B96 RID: 35734 RVA: 0x003199BC File Offset: 0x00317BBC
		public override int GetEffectType()
		{
			int num = 16777344;
			if (base.Object == null || Wading.GetTargetMoveSpeedPenalty(base.Object) > 0)
			{
				num |= 33554432;
			}
			return num;
		}

		// Token: 0x06008B97 RID: 35735 RVA: 0x003199EE File Offset: 0x00317BEE
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008B98 RID: 35736 RVA: 0x003199F1 File Offset: 0x00317BF1
		public override string GetDescription()
		{
			return "{{B|wading}}";
		}

		// Token: 0x06008B99 RID: 35737 RVA: 0x003199F8 File Offset: 0x00317BF8
		public override bool SuppressInLookDisplay()
		{
			return true;
		}

		// Token: 0x06008B9A RID: 35738 RVA: 0x003199FC File Offset: 0x00317BFC
		public override string GetDetails()
		{
			this.CheckMoveSpeedPenalty();
			int num = -(this.MoveSpeedShiftApplied ?? 20);
			if (num == 0)
			{
				return "Moving at full speed.";
			}
			return num.Signed(false) + " move speed.";
		}

		// Token: 0x06008B9B RID: 35739 RVA: 0x00319A48 File Offset: 0x00317C48
		public static int GetTargetMoveSpeedPenalty(GameObject obj)
		{
			int num = 20;
			GetWadingPerformanceEvent.GetFor(obj, ref num);
			return num;
		}

		// Token: 0x06008B9C RID: 35740 RVA: 0x00319A62 File Offset: 0x00317C62
		public void CheckMoveSpeedPenalty()
		{
			if (base.Object != null)
			{
				this.MoveSpeedShiftApplied = new int?(Wading.GetTargetMoveSpeedPenalty(base.Object));
				base.StatShifter.SetStatShift("MoveSpeed", this.MoveSpeedShiftApplied.Value, false);
			}
		}

		// Token: 0x06008B9D RID: 35741 RVA: 0x00319A9F File Offset: 0x00317C9F
		public void RemoveMoveSpeedPenalty()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x06008B9E RID: 35742 RVA: 0x00319AAC File Offset: 0x00317CAC
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<Wading>())
			{
				return false;
			}
			if (!Object.CanChangeMovementMode("Wading", false, true, false, false))
			{
				return false;
			}
			this.CheckMoveSpeedPenalty();
			Object.FireEvent("StartWading");
			Object.MovementModeChanged("Wading", true);
			return true;
		}

		// Token: 0x06008B9F RID: 35743 RVA: 0x00319AEA File Offset: 0x00317CEA
		public override void Remove(GameObject Object)
		{
			this.RemoveMoveSpeedPenalty();
			Object.MovementModeChanged("NotWading", true);
			Object.FireEvent("StopWading");
			base.Remove(Object);
		}

		// Token: 0x06008BA0 RID: 35744 RVA: 0x00319B11 File Offset: 0x00317D11
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanChangeMovementModeEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == EnteredCellEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID;
		}

		// Token: 0x06008BA1 RID: 35745 RVA: 0x00319B40 File Offset: 0x00317D40
		public override bool HandleEvent(CanChangeMovementModeEvent E)
		{
			if (this.Duration > 0 && E.Object == base.Object && E.To != "Flying" && E.To != "Swimming" && E.To != "Dodging" && E.To != "Juking" && E.To != "Jumping" && E.To != "Charging" && E.To != "Stuck" && E.To != "Engulfed")
			{
				if (E.ShowMessage)
				{
					E.Object.Fail("You cannot do that while wading.");
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008BA2 RID: 35746 RVA: 0x00319C22 File Offset: 0x00317E22
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.Validate(null);
			return base.HandleEvent(E);
		}

		// Token: 0x06008BA3 RID: 35747 RVA: 0x00319C32 File Offset: 0x00317E32
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.Validate(E.Cell);
			return base.HandleEvent(E);
		}

		// Token: 0x06008BA4 RID: 35748 RVA: 0x00319C47 File Offset: 0x00317E47
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Reference)
			{
				this.Validate(null);
				if (this.Duration > 0)
				{
					E.AddTag("{{y|[{{B|wading}}]}}", 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008BA5 RID: 35749 RVA: 0x00319C74 File Offset: 0x00317E74
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("MovementModeChanged");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008BA6 RID: 35750 RVA: 0x00319CA0 File Offset: 0x00317EA0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "MovementModeChanged")
			{
				string stringParameter = E.GetStringParameter("To", null);
				if (stringParameter == "Flying" || stringParameter == "Swimming")
				{
					base.Object.RemoveEffect(this, true);
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

		// Token: 0x06008BA7 RID: 35751 RVA: 0x00319D2F File Offset: 0x00317F2F
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				E.RenderEffectIndicator("à", "Tiles2/status_swimming.bmp", "&b", "b", 45, 10);
			}
			return true;
		}

		// Token: 0x06008BA8 RID: 35752 RVA: 0x00319D59 File Offset: 0x00317F59
		private void ApplyStats()
		{
			this.CheckMoveSpeedPenalty();
		}

		// Token: 0x06008BA9 RID: 35753 RVA: 0x00319D61 File Offset: 0x00317F61
		private void UnapplyStats()
		{
			this.RemoveMoveSpeedPenalty();
		}

		// Token: 0x06008BAA RID: 35754 RVA: 0x00319D6C File Offset: 0x00317F6C
		public void Validate(Cell C = null)
		{
			if (base.Object != null && (C != null || XRLCore.Core.Game.Segments > this.validatedOn))
			{
				this.validatedOn = XRLCore.Core.Game.Segments;
				if (C == null)
				{
					C = base.Object.CurrentCell;
				}
				if (C == null || !C.HasWadingDepthLiquid() || C.HasBridge() || base.Object.HasEffect<Swimming>() || base.Object.IsFlying)
				{
					this.Duration = 0;
					base.Object.RemoveEffect(this, false);
				}
			}
		}

		// Token: 0x04003AD2 RID: 15058
		private const int BASE_MOVE_SPEED_PENALTY = 20;

		// Token: 0x04003AD3 RID: 15059
		public int? MoveSpeedShiftApplied;

		// Token: 0x04003AD4 RID: 15060
		[NonSerialized]
		private long validatedOn;
	}
}
