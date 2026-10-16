using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F86 RID: 3974
	[Serializable]
	public class MutationInfectionOnEat : IPart
	{
		// Token: 0x06009DEA RID: 40426 RVA: 0x00392F08 File Offset: 0x00391108
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009DEB RID: 40427 RVA: 0x00392F0B File Offset: 0x0039110B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
		}

		// Token: 0x06009DEC RID: 40428 RVA: 0x00392F18 File Offset: 0x00391118
		public override bool FireEvent(Event E)
		{
			if (E.ID == "OnEat")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Eater");
				if (gameObjectParameter != null)
				{
					gameObjectParameter.ApplyEffect(new MutationInfection(Stat.Roll(this.Duration, null)), null);
				}
				return true;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400414E RID: 16718
		public string Duration = "2400-3600";
	}
}
