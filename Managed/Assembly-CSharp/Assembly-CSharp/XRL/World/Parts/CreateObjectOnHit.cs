using System;

namespace XRL.World.Parts
{
	// Token: 0x020010D5 RID: 4309
	[Serializable]
	public class CreateObjectOnHit : IPart
	{
		// Token: 0x0600AB3E RID: 43838 RVA: 0x003EAB51 File Offset: 0x003E8D51
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerHit");
			Registrar.Register("ProjectileHit");
			Registrar.Register("WeaponHit");
			Registrar.Register("WeaponThrowHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB3F RID: 43839 RVA: 0x003EAB88 File Offset: 0x003E8D88
		public override bool FireEvent(Event E)
		{
			if ((E.ID == "AttackerHit" || E.ID == "ProjectileHit" || E.ID == "WeaponHit" || E.ID == "WeaponThrowHit") && !this.Blueprint.IsNullOrEmpty() && this.Charges > 0)
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				if (gameObjectParameter2 == null || gameObjectParameter2.CurrentCell == null)
				{
					return true;
				}
				GameObject gameObject = gameObjectParameter;
				GameObject parentObject = this.ParentObject;
				string text = "Part CreateObjectOnHit Activation";
				GameObject gameObject2 = gameObjectParameter2;
				if (GetSpecialEffectChanceEvent.GetFor(gameObject, parentObject, text, this.Chance, gameObject2, null, true, false).in100())
				{
					if (gameObjectParameter2 != null)
					{
						Cell currentCell = gameObjectParameter2.CurrentCell;
						if (currentCell != null)
						{
							currentCell.AddObject(this.Blueprint, null, null, null, null);
						}
					}
					this.Charges--;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400469F RID: 18079
		public string Blueprint = "";

		// Token: 0x040046A0 RID: 18080
		public int Chance = 100;

		// Token: 0x040046A1 RID: 18081
		public int Charges = 1;
	}
}
