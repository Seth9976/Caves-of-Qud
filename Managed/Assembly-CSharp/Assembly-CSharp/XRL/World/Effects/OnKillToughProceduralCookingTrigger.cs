using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C5F RID: 3167
	public class OnKillToughProceduralCookingTrigger : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x060080D5 RID: 32981 RVA: 0x002F53E8 File Offset: 0x002F35E8
		public override string GetTriggerDescription()
		{
			return "when you kill a tough mob";
		}

		// Token: 0x060080D6 RID: 32982 RVA: 0x002F53EF File Offset: 0x002F35EF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Killed");
			base.Register(Object, Registrar);
		}

		// Token: 0x060080D7 RID: 32983 RVA: 0x002F5404 File Offset: 0x002F3604
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Killed")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Object");
				if (gameObjectParameter != null)
				{
					int? num = gameObjectParameter.Con(null, true);
					int num2 = 5;
					if ((num.GetValueOrDefault() >= num2) & (num != null))
					{
						base.Trigger();
					}
				}
				this.Remove(base.Object);
			}
			return base.FireEvent(E);
		}
	}
}
