using System;

namespace XRL.World.Parts
{
	// Token: 0x02001133 RID: 4403
	[Serializable]
	public class ModDrumLoaded : IModification
	{
		// Token: 0x0600AE6A RID: 44650 RVA: 0x00406E33 File Offset: 0x00405033
		public ModDrumLoaded()
		{
		}

		// Token: 0x0600AE6B RID: 44651 RVA: 0x00406E3B File Offset: 0x0040503B
		public ModDrumLoaded(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AE6C RID: 44652 RVA: 0x00406E44 File Offset: 0x00405044
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AE6D RID: 44653 RVA: 0x00406E4D File Offset: 0x0040504D
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<MagazineAmmoLoader>();
		}

		// Token: 0x0600AE6E RID: 44654 RVA: 0x00406E5C File Offset: 0x0040505C
		public override void ApplyModification(GameObject Object)
		{
			MagazineAmmoLoader part = Object.GetPart<MagazineAmmoLoader>();
			if (part != null)
			{
				part.MaxAmmo = (int)Math.Round((double)((float)part.MaxAmmo * 1.2f));
				if (part.MaxAmmo == 1)
				{
					part.MaxAmmo = 2;
				}
				else
				{
					MissileWeapon part2 = Object.GetPart<MissileWeapon>();
					if (part2 != null)
					{
						int num = part.MaxAmmo % part2.AmmoPerAction;
						if (num != 0)
						{
							if ((double)num < (double)part2.AmmoPerAction * 0.5)
							{
								part.MaxAmmo -= num;
							}
							else
							{
								part.MaxAmmo += part2.AmmoPerAction - num;
							}
						}
					}
				}
			}
			base.IncreaseDifficultyAndComplexity(1, 1, null);
		}

		// Token: 0x0600AE6F RID: 44655 RVA: 0x00406F00 File Offset: 0x00405100
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AE70 RID: 44656 RVA: 0x00406F1E File Offset: 0x0040511E
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("drum-loaded", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AE71 RID: 44657 RVA: 0x00406F48 File Offset: 0x00405148
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Drum-loaded: This weapon may hold 20% additional ammo.");
			return base.HandleEvent(E);
		}
	}
}
