using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010DA RID: 4314
	[Serializable]
	public class DiseaseOnHit : IPart
	{
		// Token: 0x0600AB55 RID: 43861 RVA: 0x003EB27D File Offset: 0x003E947D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AB56 RID: 43862 RVA: 0x003EB280 File Offset: 0x003E9480
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

		// Token: 0x0600AB57 RID: 43863 RVA: 0x003EB2C0 File Offset: 0x003E94C0
		public override bool FireEvent(Event E)
		{
			if (E.ID.EndsWith("Hit"))
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject parentObject = this.ParentObject;
				string text = "Part DiseaseOnHit Activation";
				GameObject gameObject = gameObjectParameter2;
				if (GetSpecialEffectChanceEvent.GetFor(gameObjectParameter, parentObject, text, this.Chance, gameObject, null, true, false).in100() && gameObjectParameter2 != null && !gameObjectParameter2.MakeSave("Toughness", this.Strength, null, null, "Disease", false, false, false, false, gameObjectParameter2))
				{
					Cell currentCell = this.ParentObject.GetCurrentCell();
					if (((currentCell != null && currentCell.ParentZone.Z != 0) ? 1 : 0) % 2 == 0)
					{
						gameObjectParameter2.ApplyEffect(new IronshankOnset(), null);
					}
					else
					{
						gameObjectParameter2.ApplyEffect(new GlotrotOnset(), null);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040046AA RID: 18090
		public int Chance;

		// Token: 0x040046AB RID: 18091
		public int Strength = 25;
	}
}
