using System;

namespace XRL.World.Parts
{
	// Token: 0x020012FA RID: 4858
	[Serializable]
	public class ConnectWidget : IPart
	{
		// Token: 0x0600BE82 RID: 48770 RVA: 0x0045C29C File Offset: 0x0045A49C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BE83 RID: 48771 RVA: 0x0045C2B4 File Offset: 0x0045A4B4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell" && this.ParentObject.CurrentZone != null)
			{
				this.ParentObject.CurrentZone.AddZoneConnection("-", this.ParentObject.CurrentCell.X, this.ParentObject.CurrentCell.Y, "Connection Widget", null);
			}
			return base.FireEvent(E);
		}
	}
}
