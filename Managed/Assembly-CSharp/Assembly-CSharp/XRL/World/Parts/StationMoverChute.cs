using System;
using XRL.Collections;

namespace XRL.World.Parts
{
	// Token: 0x020012B0 RID: 4784
	[Serializable]
	public class StationMoverChute : IPart
	{
		// Token: 0x0600BD0A RID: 48394 RVA: 0x00453518 File Offset: 0x00451718
		public override bool WantEvent(int ID, int Cascade)
		{
			return ID == PooledEvent<GetTransitiveLocationEvent>.ID || ID == ZoneBuiltEvent.ID || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x0600BD0B RID: 48395 RVA: 0x00453534 File Offset: 0x00451734
		public override bool HandleEvent(GetTransitiveLocationEvent E)
		{
			if (E.IsPullDown && E.IsWorldMap && E.Actor.HasPart<StationMover>())
			{
				Cell cell = this.ParentObject.CurrentCell;
				int num = 1000;
				for (int i = 0; i < 100; i++)
				{
					GameObject gameObject = ((cell != null) ? cell.GetFirstObjectWithPropertyOrTag(this.Tag) : null);
					if (gameObject == null)
					{
						break;
					}
					Cell localCellFromDirection = cell.GetLocalCellFromDirection(this.Mount, true);
					if (localCellFromDirection != null && localCellFromDirection.IsEmpty())
					{
						E.AddLocation(localCellFromDirection, gameObject, num);
					}
					cell = cell.GetLocalCellFromDirection(this.Direction, true);
					num -= 25;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD0C RID: 48396 RVA: 0x004535D6 File Offset: 0x004517D6
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.RequireChuteBlocker();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD0D RID: 48397 RVA: 0x004535E5 File Offset: 0x004517E5
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.RequireChuteBlocker();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD0E RID: 48398 RVA: 0x004535F4 File Offset: 0x004517F4
		public void RequireChuteBlocker()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			Cell cell = ((currentCell != null) ? currentCell.GetLocalCellFromDirection(this.Mount, true) : null);
			if (cell != null)
			{
				GameObject gameObject = cell.RequireObject("Widget", null, null, null, null);
				gameObject.RegisterEvent(this, ObjectEnteringCellEvent.ID, 0, false);
				gameObject.RegisterEvent(this, GetNavigationWeightEvent.ID, 0, false);
			}
		}

		// Token: 0x0600BD0F RID: 48399 RVA: 0x00453650 File Offset: 0x00451850
		public override bool HandleEvent(ObjectEnteringCellEvent E)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			Cell cell = ((currentCell != null) ? currentCell.GetLocalCellFromDirection(this.Mount, true) : null);
			return (E.System || E.Forced || cell != E.Cell || E.Object == null || E.Object.HasPart<StationMover>()) && base.HandleEvent(E);
		}

		// Token: 0x0600BD10 RID: 48400 RVA: 0x004536B3 File Offset: 0x004518B3
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			E.MinWeight(100);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD11 RID: 48401 RVA: 0x004536C4 File Offset: 0x004518C4
		public static string GetAdjacentDirection(Cell Cell)
		{
			foreach (Cell cell in Cell.IterateAdjacent(1, false, true, true))
			{
				using (Container<GameObject>.Enumerator enumerator2 = cell.Objects.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						StationMoverChute stationMoverChute;
						if (enumerator2.Current.TryGetPart<StationMoverChute>(out stationMoverChute))
						{
							return stationMoverChute.Direction;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x04004BBD RID: 19389
		public string Tag = "MoverChute";

		// Token: 0x04004BBE RID: 19390
		public string Direction = "N";

		// Token: 0x04004BBF RID: 19391
		public string Mount = "E";
	}
}
