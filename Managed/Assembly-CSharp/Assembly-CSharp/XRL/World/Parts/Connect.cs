using System;
using XRL.Core;

namespace XRL.World.Parts
{
	// Token: 0x0200124F RID: 4687
	[Serializable]
	public class Connect : IPart
	{
		// Token: 0x0600BA36 RID: 47670 RVA: 0x0043FE92 File Offset: 0x0043E092
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x0600BA37 RID: 47671 RVA: 0x0043FEA8 File Offset: 0x0043E0A8
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			XRLCore.Core.Game.ZoneManager.AddZoneConnection(currentCell.ParentZone.ZoneID, this.Direction, currentCell.X, currentCell.Y, this.Type, this.Object);
			return base.HandleEvent(E);
		}

		// Token: 0x04004AD9 RID: 19161
		public string Direction = "";

		// Token: 0x04004ADA RID: 19162
		public string Type = "";

		// Token: 0x04004ADB RID: 19163
		public string Object = "";
	}
}
