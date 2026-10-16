using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F88 RID: 3976
	[Serializable]
	public class PermuteMutationBuysOnEat : IPart
	{
		// Token: 0x06009DF1 RID: 40433 RVA: 0x00393091 File Offset: 0x00391291
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009DF2 RID: 40434 RVA: 0x003930A8 File Offset: 0x003912A8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "OnEat")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Eater");
				if (gameObjectParameter != null)
				{
					gameObjectParameter.PermuteRandomMutationBuys();
				}
			}
			return base.FireEvent(E);
		}
	}
}
