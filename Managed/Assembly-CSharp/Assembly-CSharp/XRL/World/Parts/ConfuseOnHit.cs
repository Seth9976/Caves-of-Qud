using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010D4 RID: 4308
	[Serializable]
	public class ConfuseOnHit : IPart
	{
		// Token: 0x0600AB3B RID: 43835 RVA: 0x003EAA23 File Offset: 0x003E8C23
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			if (Object.IsCreature)
			{
				Registrar.Register("AttackerHit");
				return;
			}
			if (Object.IsProjectile)
			{
				Registrar.Register("ProjectileHit");
				return;
			}
			Registrar.Register("WeaponHit");
			Registrar.Register("WeaponThrowHit");
		}

		// Token: 0x0600AB3C RID: 43836 RVA: 0x003EAA64 File Offset: 0x003E8C64
		public override bool FireEvent(Event E)
		{
			if (E.ID.EndsWith("Hit"))
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject parentObject = this.ParentObject;
				string text = "Part ConfuseOnHit Activation";
				GameObject gameObject = gameObjectParameter2;
				if (GetSpecialEffectChanceEvent.GetFor(gameObjectParameter, parentObject, text, this.Chance, gameObject, null, true, false).in100() && gameObjectParameter2 != null && !gameObjectParameter2.MakeSave("Willpower", this.Strength, null, null, this.NameForChecks, false, false, false, false, this.ParentObject))
				{
					gameObjectParameter2.ApplyEffect(new Confused(this.Duration.RollCached(), this.Level, this.Level + 2, this.NameForChecks), null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400469A RID: 18074
		public int Chance = 100;

		// Token: 0x0400469B RID: 18075
		public int Strength = 25;

		// Token: 0x0400469C RID: 18076
		public string Duration = "10-15";

		// Token: 0x0400469D RID: 18077
		public int Level = 10;

		// Token: 0x0400469E RID: 18078
		public string NameForChecks = "Confusion";
	}
}
