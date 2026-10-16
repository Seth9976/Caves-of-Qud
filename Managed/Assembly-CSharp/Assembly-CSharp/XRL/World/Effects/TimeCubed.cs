using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000D9D RID: 3485
	[Serializable]
	public class TimeCubed : Effect, ITierInitialized
	{
		// Token: 0x06008B12 RID: 35602 RVA: 0x0031847C File Offset: 0x0031667C
		public TimeCubed()
		{
			this.DisplayName = "time cubed";
			this.Duration = 1;
		}

		// Token: 0x06008B13 RID: 35603 RVA: 0x00318496 File Offset: 0x00316696
		public TimeCubed(int Amount)
			: this()
		{
			this.Amount = Amount;
		}

		// Token: 0x06008B14 RID: 35604 RVA: 0x003184A5 File Offset: 0x003166A5
		public override string GetDescription()
		{
			return null;
		}

		// Token: 0x06008B15 RID: 35605 RVA: 0x003184A8 File Offset: 0x003166A8
		public override int GetEffectType()
		{
			return 67112960;
		}

		// Token: 0x06008B16 RID: 35606 RVA: 0x003184AF File Offset: 0x003166AF
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008B17 RID: 35607 RVA: 0x003184B2 File Offset: 0x003166B2
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<RealityStabilizeEvent>.ID;
		}

		// Token: 0x06008B18 RID: 35608 RVA: 0x003184D0 File Offset: 0x003166D0
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (base.Object.Stat("Energy", 0) < 2000)
			{
				this.Duration = 0;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008B19 RID: 35609 RVA: 0x003184F8 File Offset: 0x003166F8
		public override bool HandleEvent(RealityStabilizeEvent E)
		{
			if (E.Check(false))
			{
				if (base.Object.Energy != null && base.Object.Energy.Value > 1000)
				{
					base.Object.Energy.BaseValue = 1000;
				}
				this.Duration = 0;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008B1A RID: 35610 RVA: 0x00318558 File Offset: 0x00316758
		public override bool Apply(GameObject Object)
		{
			if (Object.Energy != null)
			{
				if (this.Amount == 0)
				{
					this.Amount = 20000 + 3000 * Stat.Random(1, 10);
				}
				Object.Energy.BaseValue += this.Amount;
			}
			if (Object.IsPlayer())
			{
				Popup.ShowBlock("{{G|You are filled with the true vision! The {{B|Cubic Form}} is {{M|Infinite}}, {{W|Harmonic}} and transcends the {{R|1 Day rotation}}!}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			return !Object.HasEffect<TimeCubed>();
		}

		// Token: 0x04003AC6 RID: 15046
		public int Amount;
	}
}
