using System;

namespace XRL.World.Parts
{
	// Token: 0x02001064 RID: 4196
	[Serializable]
	public class MaxPenetration : IPart
	{
		// Token: 0x0600A6F4 RID: 42740 RVA: 0x003D0AA8 File Offset: 0x003CECA8
		public override bool SameAs(IPart p)
		{
			return (p as MaxPenetration).Max == this.Max && base.SameAs(p);
		}

		// Token: 0x0600A6F5 RID: 42741 RVA: 0x003D0AC6 File Offset: 0x003CECC6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNamePenetrationColorEvent>.ID || ID == PooledEvent<MissilePenetrateEvent>.ID;
		}

		// Token: 0x0600A6F6 RID: 42742 RVA: 0x003D0AE4 File Offset: 0x003CECE4
		public override bool HandleEvent(GetDisplayNamePenetrationColorEvent E)
		{
			if (this.Max <= 1)
			{
				E.Color = "K";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A6F7 RID: 42743 RVA: 0x003D0B01 File Offset: 0x003CED01
		public override bool HandleEvent(MissilePenetrateEvent E)
		{
			if (E.Penetrations > this.Max)
			{
				E.Penetrations = this.Max;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A6F8 RID: 42744 RVA: 0x003D0B24 File Offset: 0x003CED24
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A6F9 RID: 42745 RVA: 0x003D0B27 File Offset: 0x003CED27
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A6FA RID: 42746 RVA: 0x003D0B3C File Offset: 0x003CED3C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" && E.GetIntParameter("Penetrations", 0) > this.Max)
			{
				E.SetParameter("Penetrations", this.Max);
			}
			return base.FireEvent(E);
		}

		// Token: 0x040044F9 RID: 17657
		public int Max = 1;
	}
}
