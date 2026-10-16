using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010E8 RID: 4328
	[Serializable]
	public class MutateOnHit : IPart
	{
		// Token: 0x0600ABAC RID: 43948 RVA: 0x003EE5E5 File Offset: 0x003EC7E5
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			Registrar.Register("ProjectileHit");
			Registrar.Register("WeaponThrowHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600ABAD RID: 43949 RVA: 0x003EE610 File Offset: 0x003EC810
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" || E.ID == "WeaponThrowHit" || E.ID == "ProjectileHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Projectile");
				if (GameObject.Validate(ref gameObjectParameter2))
				{
					GameObject gameObject = gameObjectParameter;
					GameObject parentObject = this.ParentObject;
					string text = "Part MutateOnHit Activation";
					GameObject gameObject2 = gameObjectParameter2;
					GameObject gameObject3 = gameObjectParameter3;
					if (GetSpecialEffectChanceEvent.GetFor(gameObject, parentObject, text, this.Chance, gameObject2, gameObject3, true, false).in100() && !gameObjectParameter2.MakeSave("Toughness", this.SaveTarget.RollCached(), null, null, "Mutation", false, false, false, false, this.ParentObject))
					{
						gameObjectParameter2.ApplyEffect(new Mutating(this.IncubationTime, this.ResultTable), null);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040046EB RID: 18155
		public int Chance = 100;

		// Token: 0x040046EC RID: 18156
		public string ResultTable = "MutatingResults";

		// Token: 0x040046ED RID: 18157
		public int IncubationTime = 100;

		// Token: 0x040046EE RID: 18158
		public string SaveTarget = "20";
	}
}
