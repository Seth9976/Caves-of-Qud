using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02001116 RID: 4374
	[Serializable]
	public class DestroyContiguous : IPart
	{
		// Token: 0x0600AD38 RID: 44344 RVA: 0x003F8E34 File Offset: 0x003F7034
		public override bool SameAs(IPart p)
		{
			DestroyContiguous destroyContiguous = p as DestroyContiguous;
			return !(destroyContiguous.TargetPart != this.TargetPart) && !(destroyContiguous.TargetTag != this.TargetTag) && !(destroyContiguous.TargetTagValue != this.TargetTagValue) && destroyContiguous.Chance == this.Chance && destroyContiguous.ChanceDegradation == this.ChanceDegradation;
		}

		// Token: 0x0600AD39 RID: 44345 RVA: 0x003F8EA8 File Offset: 0x003F70A8
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			if (Object.HasTagOrProperty("MeleeWeapon"))
			{
				Registrar.Register("WeaponHit");
				return;
			}
			Registrar.Register("ProjectileEntering");
		}

		// Token: 0x0600AD3A RID: 44346 RVA: 0x003F8ED0 File Offset: 0x003F70D0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ProjectileEntering")
			{
				Cell cell = E.GetParameter("Cell") as Cell;
				if (cell != null)
				{
					Cell cell2 = cell;
					int chance = this.Chance;
					Cell cell3 = cell;
					GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
					Projectile part = this.ParentObject.GetPart<Projectile>();
					this.ChainDestroy(cell2, chance, cell3, gameObjectParameter, (part != null) ? part.Launcher : null, this.ParentObject);
				}
			}
			else if (E.ID == "WeaponHit")
			{
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				Cell cell4 = ((gameObjectParameter2 != null) ? gameObjectParameter2.CurrentCell : null);
				if (cell4 != null)
				{
					this.ChainDestroy(cell4, this.Chance, cell4, E.GetGameObjectParameter("Attacker"), E.GetGameObjectParameter("Weapon"), null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AD3B RID: 44347 RVA: 0x003F8F98 File Offset: 0x003F7198
		public void ChainDestroy(Cell C, int UseChance, Cell OC, GameObject Actor, GameObject Weapon, GameObject Projectile = null)
		{
			List<GameObject> list = Event.NewGameObjectList();
			list.AddRange(C.Objects);
			bool flag = false;
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				GameObject gameObject = list[i];
				if (this.Match(gameObject))
				{
					string text = "Part DestroyContiguous Activation";
					GameObject gameObject2 = gameObject;
					int @for = GetSpecialEffectChanceEvent.GetFor(Actor, Weapon, text, UseChance, gameObject2, Projectile, true, false);
					int j = 0;
					int count2 = gameObject.Count;
					while (j < count2)
					{
						if (@for.in100())
						{
							if (gameObject.IsPlayer() || gameObject.HasTag("Creature"))
							{
								gameObject.Die(null, "annihilation", "You were cancelled from existence.", gameObject.It + gameObject.GetVerb("were", true, false, false, true, false) + " @@cancelled from existence.", C != OC, null, null, false, false, null, null, null);
							}
							else
							{
								gameObject.Destroy(null, false, false, null);
							}
							flag = true;
						}
						j++;
					}
				}
				i++;
			}
			if (flag)
			{
				List<Cell> adjacentCells = C.GetAdjacentCells(true);
				int k = 0;
				int count3 = adjacentCells.Count;
				while (k < count3)
				{
					this.ChainDestroy(adjacentCells[k], UseChance - this.ChanceDegradation, OC, Actor, Weapon, Projectile);
					k++;
				}
			}
		}

		// Token: 0x0600AD3C RID: 44348 RVA: 0x003F90D8 File Offset: 0x003F72D8
		public bool Match(GameObject GO)
		{
			return !GO.IsInvalid() && ((this.TargetPart != null && GO.HasPart(this.TargetPart)) || (this.TargetTag != null && GO.HasTag(this.TargetTag) && (this.TargetTagValue == null || GO.GetTag(this.TargetTag, null) == this.TargetTagValue)));
		}

		// Token: 0x0400477E RID: 18302
		public string TargetPart;

		// Token: 0x0400477F RID: 18303
		public string TargetTag;

		// Token: 0x04004780 RID: 18304
		public string TargetTagValue;

		// Token: 0x04004781 RID: 18305
		public int Chance = 100;

		// Token: 0x04004782 RID: 18306
		public int ChanceDegradation;
	}
}
