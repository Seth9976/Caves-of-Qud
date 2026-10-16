using System;

namespace XRL.World.Parts
{
	// Token: 0x02000EC9 RID: 3785
	[Serializable]
	public class HeatSelfOnFreeze : IPart
	{
		// Token: 0x060097B2 RID: 38834 RVA: 0x00371536 File Offset: 0x0036F736
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x060097B3 RID: 38835 RVA: 0x0037154C File Offset: 0x0036F74C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				if (this.HeatCooldown > 0)
				{
					this.HeatCooldown--;
				}
				if (this.ParentObject.IsFrozen() && this.HeatCooldown <= 0)
				{
					this.HeatCooldown = this.HeatFrequency.RollCached();
					if (this.ParentObject.IsVisible())
					{
						base.DidXToY(this.HeatVerb, "to warm", this.ParentObject, null, "!", null, null, this.ParentObject, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					}
					int num;
					if (this.AmountIsPercentage)
					{
						num = this.HeatAmount.RollCached() * (this.ParentObject.Physics.BrittleTemperature - this.ParentObject.Physics.Temperature) / 100;
					}
					else
					{
						num = this.HeatAmount.RollCached();
					}
					this.ParentObject.TemperatureChange(num, null, false, false, false, false, 0, null, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003F16 RID: 16150
		public int HeatCooldown;

		// Token: 0x04003F17 RID: 16151
		public string HeatFrequency = "1";

		// Token: 0x04003F18 RID: 16152
		public string HeatAmount = "60";

		// Token: 0x04003F19 RID: 16153
		public bool AmountIsPercentage = true;

		// Token: 0x04003F1A RID: 16154
		public string HeatVerb = "vibrate";
	}
}
