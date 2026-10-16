using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CA5 RID: 3237
	public class CookingDomainFear_OnEatLah : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x060081D5 RID: 33237 RVA: 0x002F6D07 File Offset: 0x002F4F07
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature eat@s a dreadroot tuber or lah petal,";
		}

		// Token: 0x060081D6 RID: 33238 RVA: 0x002F6D0E File Offset: 0x002F4F0E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Eating");
			base.Register(Object, Registrar);
		}

		// Token: 0x060081D7 RID: 33239 RVA: 0x002F6D24 File Offset: 0x002F4F24
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Eating")
			{
				GameObject gameObject = E.GetParameter("Food") as GameObject;
				if (gameObject == null)
				{
					return true;
				}
				if (gameObject.HasTag("Lah"))
				{
					base.Trigger();
				}
			}
			return base.FireEvent(E);
		}
	}
}
