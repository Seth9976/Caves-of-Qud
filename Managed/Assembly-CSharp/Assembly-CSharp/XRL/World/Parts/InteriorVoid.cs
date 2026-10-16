using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F75 RID: 3957
	[Serializable]
	public class InteriorVoid : IPart
	{
		// Token: 0x06009D8C RID: 40332 RVA: 0x00391282 File Offset: 0x0038F482
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneActivatedEvent.ID || ID == ObjectEnteringCellEvent.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x06009D8D RID: 40333 RVA: 0x003912A8 File Offset: 0x0038F4A8
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			return true;
		}

		// Token: 0x06009D8E RID: 40334 RVA: 0x003912AB File Offset: 0x0038F4AB
		public override bool HandleEvent(ObjectEnteringCellEvent E)
		{
			return E.Forced && base.HandleEvent(E);
		}

		// Token: 0x06009D8F RID: 40335 RVA: 0x003912C0 File Offset: 0x0038F4C0
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (E.Object != this.ParentObject && E.Object.IsReal)
			{
				InteriorZone interiorZone = this.ParentObject.CurrentZone as InteriorZone;
				if (interiorZone != null)
				{
					Cell escapeCell = interiorZone.GetEscapeCell(E.Object);
					if (escapeCell != null)
					{
						E.Object.SystemMoveTo(escapeCell, null, false, true, false, false);
						int fallDistance = interiorZone.ParentObject.GetPart<Interior>().FallDistance;
						if (fallDistance > 1)
						{
							StairsDown.InflictFallDamage(E.Object, fallDistance, null, null);
						}
						if (escapeCell.OnWorldMap())
						{
							E.Object.PullDown(false);
							GameObject parentObject = interiorZone.ParentObject;
							if (parentObject != null)
							{
								parentObject.PullDown(false);
							}
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D90 RID: 40336 RVA: 0x0039137C File Offset: 0x0038F57C
		public bool IsRenderable(GameObject Object)
		{
			return Object.Brain == null;
		}

		// Token: 0x06009D91 RID: 40337 RVA: 0x0039138C File Offset: 0x0038F58C
		public bool Paint()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			InteriorZone interiorZone = currentCell.ParentZone as InteriorZone;
			Zone zone;
			if (interiorZone == null)
			{
				zone = null;
			}
			else
			{
				GameObject parentObject = interiorZone.ParentObject;
				zone = ((parentObject != null) ? parentObject.CurrentZone : null);
			}
			Zone zone2 = zone;
			if (zone2 == null)
			{
				return false;
			}
			Render render = this.ParentObject.Render;
			render.DetailColor = "k";
			render.ColorString = (render.TileColor = "&K");
			Cell cell = zone2.GetCell(currentCell.X, currentCell.Y);
			GameObject highestRenderLayerObject = cell.GetHighestRenderLayerObject(new Predicate<GameObject>(this.IsRenderable));
			if (highestRenderLayerObject != null)
			{
				render.Tile = highestRenderLayerObject.Render.Tile;
			}
			else
			{
				render.Tile = cell.PaintTile;
			}
			return true;
		}
	}
}
