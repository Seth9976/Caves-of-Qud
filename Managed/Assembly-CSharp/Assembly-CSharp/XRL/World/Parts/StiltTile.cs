using System;

namespace XRL.World.Parts
{
	// Token: 0x0200128E RID: 4750
	[Serializable]
	public class StiltTile : IPart
	{
		// Token: 0x0600BBE7 RID: 48103 RVA: 0x00449B78 File Offset: 0x00447D78
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BBE8 RID: 48104 RVA: 0x00449B7B File Offset: 0x00447D7B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BBE9 RID: 48105 RVA: 0x00449B90 File Offset: 0x00447D90
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				if (this.ParentObject.CurrentCell == null)
				{
					return true;
				}
				Render render = this.ParentObject.Render;
				if ((this.ParentObject.CurrentCell.X + this.ParentObject.CurrentCell.Y) % 2 == 0)
				{
					render.ColorString = "&K";
					render.RenderString = "ù";
					render.Tile = "terrain/sw_cathedral1.bmp";
					this.ParentObject.Physics.CurrentCell.PaintTile = "terrain/sw_cathedral1.bmp";
					this.ParentObject.Physics.CurrentCell.PaintTileColor = "&y";
				}
				else
				{
					render.ColorString = "&K";
					render.RenderString = "ú";
					render.Tile = "terrain/sw_cathedral2.bmp";
					this.ParentObject.Physics.CurrentCell.PaintTile = "terrain/sw_cathedral2.bmp";
					this.ParentObject.Physics.CurrentCell.PaintTileColor = "&y";
				}
				this.ParentObject.RemovePart(this);
			}
			return base.FireEvent(E);
		}
	}
}
