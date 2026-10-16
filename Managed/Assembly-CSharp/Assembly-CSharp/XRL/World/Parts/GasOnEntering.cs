using System;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: gas density produced is increased by a percentage
	/// equal to ((power load - 100) / 10), i.e. 30% for the standard overload
	/// power load of 400.
	/// </remarks>
	// Token: 0x020010DF RID: 4319
	[Serializable]
	public class GasOnEntering : IPart
	{
		// Token: 0x0600AB75 RID: 43893 RVA: 0x003EC208 File Offset: 0x003EA408
		public override bool SameAs(IPart p)
		{
			GasOnEntering gasOnEntering = p as GasOnEntering;
			return !(gasOnEntering.Blueprint != this.Blueprint) && !(gasOnEntering.Density != this.Density) && base.SameAs(p);
		}

		// Token: 0x0600AB76 RID: 43894 RVA: 0x003EC24D File Offset: 0x003EA44D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ProjectileEntering");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB77 RID: 43895 RVA: 0x003EC264 File Offset: 0x003EA464
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ProjectileEntering")
			{
				Cell cell = E.GetParameter("Cell") as Cell;
				if (cell != null)
				{
					GameObject gameObject = GameObject.Create(this.Blueprint);
					Gas part = gameObject.GetPart<Gas>();
					part.Density = this.Density.RollCached() * (100 + this.MyPowerLoadBonus(int.MinValue, 100, 10)) / 100;
					part.Creator = E.GetGameObjectParameter("Attacker");
					cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040046BA RID: 18106
		public string Blueprint = "AcidGas";

		// Token: 0x040046BB RID: 18107
		public string Density = "3d10";
	}
}
