using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F6A RID: 3946
	[Serializable]
	public class CaverCorpseLoot2 : IPart
	{
		// Token: 0x06009D1B RID: 40219 RVA: 0x0038E3B1 File Offset: 0x0038C5B1
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009D1C RID: 40220 RVA: 0x0038E3B4 File Offset: 0x0038C5B4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009D1D RID: 40221 RVA: 0x0038E3CC File Offset: 0x0038C5CC
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
			if (physics.CurrentCell.ParentZone.X == 1 && physics.currentCell.ParentZone.Y == 1)
			{
				physics.CurrentCell.AddObject(GameObject.Create("Laser Pistol"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				physics.CurrentCell.AddObject(GameObject.Create("Solar Cell"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				physics.CurrentCell.AddObject(GameObject.Create("DataDisk"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				physics.CurrentCell.AddObject(GameObject.Create("Floating Glowsphere"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				physics.CurrentCell.AddObject(GameObject.Create("Basic Toolkit"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				physics.CurrentCell.AddObject(GameObject.Create("Canned Have-It-All"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				physics.CurrentCell.AddObject(GameObject.Create("Canned Have-It-All"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				physics.CurrentCell.AddObject(GameObject.Create("Canned Have-It-All"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				physics.CurrentCell.AddObject(GameObject.Create("Canned Have-It-All"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				physics.CurrentCell.AddObject(GameObject.Create("Canned Have-It-All"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			return true;
		}

		// Token: 0x040040EC RID: 16620
		public bool bCreated;
	}
}
