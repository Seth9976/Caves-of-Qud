using System;

namespace XRL.World.Parts
{
	// Token: 0x020011AF RID: 4527
	[Serializable]
	public class RootKnotStarter : IPart
	{
		// Token: 0x0600B3D3 RID: 46035 RVA: 0x0041963F File Offset: 0x0041783F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600B3D4 RID: 46036 RVA: 0x00419655 File Offset: 0x00417855
		public override bool HandleEvent(EnteredCellEvent E)
		{
			The.Game.RequireSystem<RootKnotSystem>().PlaceSummoner(this.ParentObject.CurrentZone);
			this.ParentObject.Destroy(null, false, false, null);
			return base.HandleEvent(E);
		}
	}
}
