using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E9D RID: 3741
	[Serializable]
	public class DiseaseMelee : IPart
	{
		// Token: 0x0600966A RID: 38506 RVA: 0x00368172 File Offset: 0x00366372
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponDealDamage");
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600966B RID: 38507 RVA: 0x00368194 File Offset: 0x00366394
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponDealDamage" && this.Chance.in100())
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Defender");
				if (gameObjectParameter != null && !gameObjectParameter.MakeSave("Toughness", 13, null, null, "Disease", false, false, false, false, this.ParentObject))
				{
					Cell currentCell = this.ParentObject.GetCurrentCell();
					if (((currentCell != null && currentCell.ParentZone.Z != 0) ? 1 : 0) % 2 == 0)
					{
						gameObjectParameter.ApplyEffect(new IronshankOnset(), null);
					}
					else
					{
						gameObjectParameter.ApplyEffect(new GlotrotOnset(), null);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003E57 RID: 15959
		public int Chance = 10;
	}
}
