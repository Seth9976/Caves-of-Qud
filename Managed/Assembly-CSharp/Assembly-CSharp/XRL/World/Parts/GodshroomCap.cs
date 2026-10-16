using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001042 RID: 4162
	[Serializable]
	public class GodshroomCap : IPart
	{
		// Token: 0x0600A5B5 RID: 42421 RVA: 0x003CA73A File Offset: 0x003C893A
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600A5B6 RID: 42422 RVA: 0x003CA73D File Offset: 0x003C893D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Eaten");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A5B7 RID: 42423 RVA: 0x003CA754 File Offset: 0x003C8954
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Eaten")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Eater");
				if (gameObjectParameter.IsPlayer())
				{
					if (gameObjectParameter != null)
					{
						gameObjectParameter.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_godshroom", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					}
					FungalVisionary fungalVisionary;
					if (gameObjectParameter.TryGetEffect<FungalVisionary>(out fungalVisionary))
					{
						fungalVisionary.Duration += 1000;
					}
					else
					{
						gameObjectParameter.ApplyEffect(new FungalVisionary(1000), null);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004472 RID: 17522
		public bool Controlled;
	}
}
