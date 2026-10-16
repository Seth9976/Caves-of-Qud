using System;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AF6 RID: 2806
	public class TeleportGateBuilder
	{
		// Token: 0x0600706D RID: 28781 RVA: 0x002805BC File Offset: 0x0027E7BC
		public bool BuildZone(Zone Z)
		{
			if (!this.Blueprint.IsNullOrEmpty())
			{
				Cell cell;
				if (this.InboundTarget == null || this.InboundTarget.ZoneID != Z.ZoneID || this.InboundTarget.CellX == -1 || this.InboundTarget.CellY == -1)
				{
					cell = Z.GetEmptyCells(null, null).GetRandomElement<Cell>(null);
					GameObject gameObject = GameObject.FindByID(this.TwinID);
					TeleportGate teleportGate;
					if (gameObject != null && gameObject.TryGetPart<TeleportGate>(out teleportGate))
					{
						if (teleportGate.Target == null)
						{
							teleportGate.Target = new GlobalLocation(cell);
						}
						else
						{
							teleportGate.Target.CellX = cell.X;
							teleportGate.Target.CellY = cell.Y;
						}
					}
				}
				else
				{
					cell = Z.GetCell(this.InboundTarget.CellX, this.InboundTarget.CellY);
				}
				Cell cell2 = ((cell != null) ? cell.GetEmptyConnectedAdjacentCells(1, true).GetRandomElement<Cell>(null) : null) ?? ((cell != null) ? cell.GetEmptyConnectedAdjacentCells(2, true).GetRandomElement<Cell>(null) : null);
				if (cell2 == null)
				{
					cell2 = Z.GetEmptyCells(null, null).GetRandomElement<Cell>(null);
				}
				if (cell2 != null)
				{
					GameObject gameObject2 = GameObject.Create(this.Blueprint);
					if (gameObject2 != null)
					{
						if (this.ReturnTarget != null)
						{
							TeleportGate part = gameObject2.GetPart<TeleportGate>();
							if (part != null)
							{
								part.Target = this.ReturnTarget;
							}
						}
						cell2.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
			return true;
		}

		// Token: 0x040033DF RID: 13279
		public string Blueprint;

		// Token: 0x040033E0 RID: 13280
		public GlobalLocation InboundTarget;

		// Token: 0x040033E1 RID: 13281
		public GlobalLocation ReturnTarget;

		// Token: 0x040033E2 RID: 13282
		public string TwinID;
	}
}
