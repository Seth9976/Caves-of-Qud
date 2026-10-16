using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F69 RID: 3945
	[Serializable]
	public class CaverCorpseLoot : IPart
	{
		// Token: 0x06009D17 RID: 40215 RVA: 0x0038E1C8 File Offset: 0x0038C3C8
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009D18 RID: 40216 RVA: 0x0038E1CB File Offset: 0x0038C3CB
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009D19 RID: 40217 RVA: 0x0038E1E0 File Offset: 0x0038C3E0
		public override bool FireEvent(Event E)
		{
			if (!(E.ID == "EnteredCell"))
			{
				return base.FireEvent(E);
			}
			if (this.bCreated)
			{
				return true;
			}
			this.bCreated = true;
			Physics physics = this.ParentObject.Physics;
			physics.CurrentCell.AddObject(GameObject.Create("Miner's Helmet"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			physics.CurrentCell.AddObject(GameObject.Create("Pickaxe"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			physics.CurrentCell.AddObject(GameObject.Create("Sheaf1"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			physics.CurrentCell.AddObject(GameObject.Create("Canned Have-It-All"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			physics.CurrentCell.AddObject(GameObject.Create("Canned Have-It-All"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			physics.CurrentCell.AddObject(GameObject.Create("Canned Have-It-All"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			physics.CurrentCell.AddObject(GameObject.Create("Canned Have-It-All"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			physics.CurrentCell.AddObject(GameObject.Create("Canned Have-It-All"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			physics.CurrentCell.AddObject(GameObject.Create("Small Sphere of Negative Weight"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			physics.CurrentCell.AddObject(GameObject.Create("CyberneticsCreditWedge"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.ParentObject.UnregisterPartEvent(this, "EnteredCell");
			return true;
		}

		// Token: 0x040040EB RID: 16619
		public bool bCreated;
	}
}
