using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x020010D0 RID: 4304
	[Serializable]
	public class BurgeonOnHit : IPart
	{
		// Token: 0x0600AB27 RID: 43815 RVA: 0x003EA37D File Offset: 0x003E857D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB28 RID: 43816 RVA: 0x003EA394 File Offset: 0x003E8594
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" && this.Chance.in100())
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				if (GameObject.Validate(ref gameObjectParameter) && GameObject.Validate(ref gameObjectParameter2) && !gameObjectParameter2.IsNowhere())
				{
					UnwelcomeGermination.Germinate(gameObjectParameter, this.Level.RollCached(), 1, true, gameObjectParameter2.CurrentCell);
					IComponent<GameObject>.XDidY(gameObjectParameter, "cause", "several plants to germinate with the force of " + gameObjectParameter.its_(this.ParentObject, false), "!", null, null, gameObjectParameter, null, false, false, null, null, false, false, false, false, false, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400468E RID: 18062
		public int Chance = 100;

		// Token: 0x0400468F RID: 18063
		public string Level = "10";
	}
}
