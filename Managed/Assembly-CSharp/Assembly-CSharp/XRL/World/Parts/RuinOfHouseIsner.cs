using System;

namespace XRL.World.Parts
{
	// Token: 0x0200109F RID: 4255
	[Serializable]
	public class RuinOfHouseIsner : IPart
	{
		// Token: 0x0600A93D RID: 43325 RVA: 0x003DDD5F File Offset: 0x003DBF5F
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A93E RID: 43326 RVA: 0x003DDD62 File Offset: 0x003DBF62
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A93F RID: 43327 RVA: 0x003DDD80 File Offset: 0x003DBF80
		public override bool HandleEvent(EquippedEvent E)
		{
			if (this.EgoBonus == 0 && E.Actor.HasStat("Ego") && this.ParentObject.IsEquippedProperly(null))
			{
				Statistic stat = E.Actor.GetStat("Ego");
				int bonus = stat.Bonus;
				stat.Bonus = bonus + 1;
				this.EgoBonus = 1;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A940 RID: 43328 RVA: 0x003DDDE4 File Offset: 0x003DBFE4
		public override bool HandleEvent(UnequippedEvent E)
		{
			if (this.EgoBonus != 0 && E.Actor.HasStat("Ego"))
			{
				E.Actor.GetStat("Ego").Bonus -= this.EgoBonus;
				this.EgoBonus = 0;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A941 RID: 43329 RVA: 0x003DDE3C File Offset: 0x003DC03C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("QueryMissileFireSound");
			Registrar.Register("MagazineAmmoLoaderReloaded");
			Registrar.Register("WeaponMissileWeaponHit");
			Registrar.Register("WeaponMissileWeaponShot");
			Registrar.Register("WeaponMissleWeaponFiring");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A942 RID: 43330 RVA: 0x003DDE88 File Offset: 0x003DC088
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponMissileWeaponShot")
			{
				if (this.ShotCount == 7)
				{
					E.SetParameter("AimVariance", 0);
					E.SetParameter("FlatVariance", 0);
					E.SetParameter("WeaponAccuracy", 0);
				}
			}
			else if (E.ID == "MagazineAmmoLoaderReloaded")
			{
				this.ShotCount = 0;
			}
			else if (E.ID == "QueryMissileFireSound")
			{
				if (this.ShotCount == 7)
				{
					E.SetParameter("Sound", "Sounds/Missile/Fires/Pistols/sfx_missile_ruinOfHouseIsner_finalShot");
				}
			}
			else if (E.ID == "WeaponMissleWeaponFiring")
			{
				this.ShotCount++;
			}
			else if (E.ID == "WeaponMissileWeaponHit")
			{
				if (this.ShotCount == 7)
				{
					E.SetFlag("Critical", true);
				}
				if (E.HasFlag("Critical"))
				{
					E.SetParameter("Penetrations", E.GetIntParameter("Penetrations", 0) + 4);
					E.SetParameter("PenetrationCap", E.GetIntParameter("PenetrationCap", 0) + 4);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A943 RID: 43331 RVA: 0x003DDFC3 File Offset: 0x003DC1C3
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x040045D9 RID: 17881
		public int EgoBonus;

		// Token: 0x040045DA RID: 17882
		public int ShotCount;
	}
}
