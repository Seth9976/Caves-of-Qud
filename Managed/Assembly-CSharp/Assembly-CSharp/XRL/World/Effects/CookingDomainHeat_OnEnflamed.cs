using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CAE RID: 3246
	public class CookingDomainHeat_OnEnflamed : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x060081F4 RID: 33268 RVA: 0x002F6FC6 File Offset: 0x002F51C6
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature @is set on fire,";
		}

		// Token: 0x060081F5 RID: 33269 RVA: 0x002F6FCD File Offset: 0x002F51CD
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x060081F6 RID: 33270 RVA: 0x002F6FE2 File Offset: 0x002F51E2
		public override void Init(GameObject target)
		{
			base.Init(target);
			this.CurrentlyEnflamed = false;
		}

		// Token: 0x060081F7 RID: 33271 RVA: 0x002F6FF4 File Offset: 0x002F51F4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				if (this.CurrentlyEnflamed)
				{
					if (base.Object.Physics.Temperature < base.Object.Physics.FlameTemperature)
					{
						this.CurrentlyEnflamed = false;
					}
				}
				else if (base.Object.Physics.Temperature >= base.Object.Physics.FlameTemperature)
				{
					this.CurrentlyEnflamed = true;
					base.Trigger();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003929 RID: 14633
		public bool CurrentlyEnflamed;
	}
}
