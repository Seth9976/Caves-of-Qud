using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x020010D8 RID: 4312
	[Serializable]
	public class DisarmOnHit : IPart
	{
		// Token: 0x0600AB4B RID: 43851 RVA: 0x003EAF1C File Offset: 0x003E911C
		public DisarmOnHit()
		{
		}

		// Token: 0x0600AB4C RID: 43852 RVA: 0x003EAF2C File Offset: 0x003E912C
		public DisarmOnHit(int Chance)
			: this()
		{
			this.Chance = Chance;
		}

		// Token: 0x0600AB4D RID: 43853 RVA: 0x003EAF3B File Offset: 0x003E913B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB4E RID: 43854 RVA: 0x003EAF50 File Offset: 0x003E9150
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AttackerHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject parentObject = this.ParentObject;
				string text = "Part DisarmOnHit Activation";
				GameObject gameObject = gameObjectParameter2;
				if (GetSpecialEffectChanceEvent.GetFor(gameObjectParameter, parentObject, text, this.Chance, gameObject, null, true, false).in100())
				{
					Disarming.Disarm(gameObjectParameter2, this.ParentObject, 40, "Strength", "Agility", null, this.ParentObject);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040046A7 RID: 18087
		public int Chance = 100;
	}
}
