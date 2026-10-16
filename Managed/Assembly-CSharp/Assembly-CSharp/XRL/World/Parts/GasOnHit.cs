using System;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: gas density produced is increased by a percentage
	/// equal to ((power load - 100) / 10), i.e. 30% for the standard overload
	/// power load of 400.
	/// </remarks>
	// Token: 0x020010E0 RID: 4320
	[Serializable]
	public class GasOnHit : IPart
	{
		// Token: 0x0600AB79 RID: 43897 RVA: 0x003EC31C File Offset: 0x003EA51C
		public GasOnHit()
		{
		}

		// Token: 0x0600AB7A RID: 43898 RVA: 0x003EC33A File Offset: 0x003EA53A
		public GasOnHit(string Blueprint, string Density)
			: this()
		{
			this.Blueprint = Blueprint;
			this.Density = Density;
		}

		// Token: 0x0600AB7B RID: 43899 RVA: 0x003EC350 File Offset: 0x003EA550
		public override bool SameAs(IPart p)
		{
			GasOnHit gasOnHit = p as GasOnHit;
			return !(gasOnHit.Blueprint != this.Blueprint) && !(gasOnHit.Density != this.Density) && base.SameAs(p);
		}

		// Token: 0x0600AB7C RID: 43900 RVA: 0x003EC395 File Offset: 0x003EA595
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			if (!this.OnWielderHit)
			{
				Registrar.Register("WeaponDealDamage");
			}
			Registrar.Register("ProjectileHit");
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB7D RID: 43901 RVA: 0x003EC3C8 File Offset: 0x003EA5C8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" || E.ID == "WeaponDealDamage" || E.ID == "ProjectileHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Defender");
				if (((gameObjectParameter != null) ? gameObjectParameter.CurrentCell : null) != null)
				{
					GameObject gameObject = GameObject.Create(this.Blueprint);
					Gas part = gameObject.GetPart<Gas>();
					part.Density = this.Density.RollCached() * (100 + this.MyPowerLoadBonus(int.MinValue, 100, 10)) / 100;
					part.Creator = E.GetGameObjectParameter("Attacker");
					gameObjectParameter.CurrentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040046BC RID: 18108
		public string Blueprint = "AcidGas";

		// Token: 0x040046BD RID: 18109
		public string Density = "3d10";

		// Token: 0x040046BE RID: 18110
		public bool OnWielderHit;
	}
}
