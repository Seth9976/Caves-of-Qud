using System;
using System.Linq;

namespace XRL.World.Parts
{
	// Token: 0x02000FA2 RID: 4002
	[Serializable]
	public class DoubleEnclosing : IPart
	{
		// Token: 0x06009EF5 RID: 40693 RVA: 0x0039A4DD File Offset: 0x003986DD
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009EF6 RID: 40694 RVA: 0x0039A4E0 File Offset: 0x003986E0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("SyncOpened");
			Registrar.Register("SyncClosed");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009EF7 RID: 40695 RVA: 0x0039A500 File Offset: 0x00398700
		public override bool FireEvent(Event E)
		{
			if (E.ID == "SyncOpened")
			{
				GameObject gameObject = this.ParentObject.CurrentCell.GetCellFromDirection(this.Direction, true).GetObjectsWithPart("Enclosing").First<GameObject>();
				Enclosing part = gameObject.GetPart<Enclosing>();
				if (part.OpenColor != null)
				{
					gameObject.Render.ColorString = part.OpenColor;
				}
				if (part.OpenTileColor != null)
				{
					gameObject.Render.TileColor = part.OpenTileColor;
				}
				if (part.OpenRenderString != null)
				{
					gameObject.Render.RenderString = part.OpenRenderString;
				}
				if (part.OpenTile != null)
				{
					gameObject.Render.Tile = part.OpenTile;
				}
				if (part.OpenLayer != -2147483648)
				{
					gameObject.Render.RenderLayer = part.OpenLayer;
				}
				return true;
			}
			if (E.ID == "SyncClosed")
			{
				GameObject gameObject2 = this.ParentObject.CurrentCell.GetCellFromDirection(this.Direction, true).GetObjectsWithPart("Enclosing").First<GameObject>();
				Enclosing part2 = gameObject2.GetPart<Enclosing>();
				if (part2.ClosedColor != null)
				{
					gameObject2.Render.ColorString = part2.ClosedColor;
				}
				if (part2.ClosedTile != null)
				{
					gameObject2.Render.TileColor = part2.ClosedTile;
				}
				if (part2.ClosedRenderString != null)
				{
					gameObject2.Render.RenderString = part2.ClosedRenderString;
				}
				if (part2.ClosedTile != null)
				{
					gameObject2.Render.Tile = part2.ClosedTile;
				}
				if (part2.ClosedLayer != -2147483648)
				{
					gameObject2.Render.RenderLayer = part2.ClosedLayer;
				}
				return true;
			}
			return base.FireEvent(E);
		}

		// Token: 0x040041C3 RID: 16835
		public string Direction;
	}
}
