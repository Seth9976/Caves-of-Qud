using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D9E RID: 3486
	public abstract class ITimeDilated : Effect, ITierInitialized
	{
		// Token: 0x06008B1B RID: 35611 RVA: 0x003185D0 File Offset: 0x003167D0
		public ITimeDilated()
		{
			this.DisplayName = "time-dilated";
			this.Duration = 1;
		}

		// Token: 0x06008B1C RID: 35612 RVA: 0x003185EA File Offset: 0x003167EA
		public ITimeDilated(int SpeedPenalty)
			: this()
		{
			this.SpeedPenalty = SpeedPenalty;
		}

		// Token: 0x06008B1D RID: 35613 RVA: 0x003185F9 File Offset: 0x003167F9
		public virtual void Initialize(int Tier)
		{
			this.Duration = Stat.Random(20, 200);
			this.SpeedPenalty = Stat.Random(1, 9) * 10;
		}

		// Token: 0x06008B1E RID: 35614
		public abstract bool DoTimeDilationVisualEffects();

		// Token: 0x06008B1F RID: 35615 RVA: 0x0031861E File Offset: 0x0031681E
		public override int GetEffectType()
		{
			return 117444608;
		}

		// Token: 0x06008B20 RID: 35616 RVA: 0x00318625 File Offset: 0x00316825
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID || ID == PooledEvent<RealityStabilizeEvent>.ID;
		}

		// Token: 0x06008B21 RID: 35617 RVA: 0x00318643 File Offset: 0x00316843
		public override bool HandleEvent(RealityStabilizeEvent E)
		{
			if (E.Check(false))
			{
				base.Object.RemoveEffect(this, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008B22 RID: 35618 RVA: 0x00318663 File Offset: 0x00316863
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(base.Object))
			{
				E.Add("time", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008B23 RID: 35619 RVA: 0x00318688 File Offset: 0x00316888
		public override string GetDescription()
		{
			return "time-dilated ({{C|" + (-this.SpeedPenalty).ToString() + "}} Quickness)";
		}

		// Token: 0x06008B24 RID: 35620 RVA: 0x003186B4 File Offset: 0x003168B4
		public override string GetDetails()
		{
			return (-this.SpeedPenalty).ToString() + " Quickness";
		}

		// Token: 0x06008B25 RID: 35621 RVA: 0x003186DA File Offset: 0x003168DA
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008B26 RID: 35622 RVA: 0x003186DD File Offset: 0x003168DD
		public override bool Apply(GameObject Object)
		{
			return Object.FireEvent("ApplyTimeDilated");
		}

		// Token: 0x06008B27 RID: 35623 RVA: 0x003186EF File Offset: 0x003168EF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("ApplyTimeDilated");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("CanApplyTimeDilated");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008B28 RID: 35624 RVA: 0x00318725 File Offset: 0x00316925
		public override void Remove(GameObject Object)
		{
			this.UnapplyChanges();
			this.SpeedPenalty = 0;
			base.Remove(Object);
		}

		// Token: 0x06008B29 RID: 35625 RVA: 0x0031873B File Offset: 0x0031693B
		public virtual void ApplyChanges()
		{
			base.StatShifter.SetStatShift("Speed", -this.SpeedPenalty, false);
		}

		// Token: 0x06008B2A RID: 35626 RVA: 0x00318756 File Offset: 0x00316956
		public virtual void UnapplyChanges()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x06008B2B RID: 35627 RVA: 0x00318763 File Offset: 0x00316963
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0 && this.DoTimeDilationVisualEffects())
			{
				E.ColorString += "^b";
			}
			return true;
		}

		// Token: 0x06008B2C RID: 35628 RVA: 0x00318790 File Offset: 0x00316990
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanApplyTimeDilated" || E.ID == "ApplyTimeDilated")
			{
				return false;
			}
			if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyChanges();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyChanges();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003AC7 RID: 15047
		public int SpeedPenalty;
	}
}
