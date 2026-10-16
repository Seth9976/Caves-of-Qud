using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C8B RID: 3211
	public class CookingDomainCold_OnSlowedByCold : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x06008170 RID: 33136 RVA: 0x002F63FA File Offset: 0x002F45FA
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature @is slowed by cold,";
		}

		// Token: 0x06008171 RID: 33137 RVA: 0x002F6401 File Offset: 0x002F4601
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008172 RID: 33138 RVA: 0x002F6416 File Offset: 0x002F4616
		public override void Init(GameObject target)
		{
			base.Init(target);
			this.CurrentlySlowed = false;
		}

		// Token: 0x06008173 RID: 33139 RVA: 0x002F6428 File Offset: 0x002F4628
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				if (this.CurrentlySlowed)
				{
					if (base.Object.Physics.Temperature > base.Object.Physics.FreezeTemperature)
					{
						this.CurrentlySlowed = false;
					}
				}
				else if (base.Object.Physics.Temperature <= base.Object.Physics.FreezeTemperature)
				{
					this.CurrentlySlowed = true;
					base.Trigger();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400391C RID: 14620
		public bool CurrentlySlowed;
	}
}
