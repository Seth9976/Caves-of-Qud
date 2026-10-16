using System;
using System.Linq;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D55 RID: 3413
	[Serializable]
	public class Interdicted : Effect
	{
		// Token: 0x060086DD RID: 34525 RVA: 0x00307081 File Offset: 0x00305281
		public Interdicted()
		{
			this.DisplayName = "{{C|interdicted}}";
			this.Duration = 1;
			this.PenaltyToApply = Stat.Random(1, 9) * 10;
		}

		// Token: 0x060086DE RID: 34526 RVA: 0x003070B4 File Offset: 0x003052B4
		public Interdicted(string interdictorId, int penalty)
			: this()
		{
			this.PenaltyToApply = penalty;
			this.interdictorId = interdictorId;
		}

		// Token: 0x060086DF RID: 34527 RVA: 0x003070CA File Offset: 0x003052CA
		public override int GetEffectType()
		{
			return 33554560;
		}

		// Token: 0x060086E0 RID: 34528 RVA: 0x003070D1 File Offset: 0x003052D1
		public override string GetDetails()
		{
			return "-10 Move Speed";
		}

		// Token: 0x060086E1 RID: 34529 RVA: 0x003070D8 File Offset: 0x003052D8
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x060086E2 RID: 34530 RVA: 0x003070DC File Offset: 0x003052DC
		public override bool Apply(GameObject Object)
		{
			foreach (Effect effect in Object.Effects)
			{
				Interdicted interdicted = effect as Interdicted;
				if (interdicted != null && interdicted.interdictorId == this.interdictorId)
				{
					return false;
				}
			}
			if (Object.FireEvent(Event.New("ApplyInterdiction", "Duration", this.Duration)))
			{
				this.CurrentSpeedPenalty = this.PenaltyToApply;
				this.ApplyStats();
				return true;
			}
			return false;
		}

		// Token: 0x060086E3 RID: 34531 RVA: 0x0030717C File Offset: 0x0030537C
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
			this.CurrentSpeedPenalty = 0;
		}

		// Token: 0x060086E4 RID: 34532 RVA: 0x0030718B File Offset: 0x0030538B
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift(base.Object, "MoveSpeed", this.CurrentSpeedPenalty, false);
		}

		// Token: 0x060086E5 RID: 34533 RVA: 0x003071AB File Offset: 0x003053AB
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x060086E6 RID: 34534 RVA: 0x003071BE File Offset: 0x003053BE
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("BeginTakeAction");
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x060086E7 RID: 34535 RVA: 0x003071F4 File Offset: 0x003053F4
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 10)
				{
					E.RenderString = "?";
					E.ColorString = "&c^b";
					return false;
				}
			}
			return true;
		}

		// Token: 0x060086E8 RID: 34536 RVA: 0x00307238 File Offset: 0x00305438
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandTakeAction" || E.ID == "EndTurn")
			{
				GameObject gameObject = base.Object.GetVisibleCombatObjects().FirstOrDefault<GameObject>((GameObject o) => o.ID == this.interdictorId);
				if (gameObject == null || !base.Object.HasLOSTo(gameObject, true, false, false, true, null))
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

		// Token: 0x040039FC RID: 14844
		private int PenaltyToApply = 10;

		// Token: 0x040039FD RID: 14845
		private int CurrentSpeedPenalty;

		// Token: 0x040039FE RID: 14846
		public string interdictorId;
	}
}
