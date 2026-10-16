using System;
using System.Linq;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A40 RID: 2624
	public class BananaSky : ZoneBuilderSandbox
	{
		// Token: 0x06006DC6 RID: 28102 RVA: 0x00250768 File Offset: 0x0024E968
		public bool BuildZone(Zone Z)
		{
			if (Z.Z > 0)
			{
				if (Z.Z < 10)
				{
					ZoneBuilderSandbox.PlaceObject("ConcreteFloor", Z);
				}
				Z.GetTerrainNameFromDirection(".") == "TerrainTheSpindle";
				if (Z.Y == 2 && Z.GetTerrainNameFromDirection("S") == "TerrainTheSpindle")
				{
					if (Z.X == 0)
					{
						ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_NNW.rpm", ZoneBuilderSandbox.PlacePrefabAlign.S, null, delegate(Cell c)
						{
							c.Clear(null, false, false, null);
						}, null, null, null, null, null);
					}
					if (Z.X == 1)
					{
						ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_N.rpm", ZoneBuilderSandbox.PlacePrefabAlign.S, null, delegate(Cell c)
						{
							c.Clear(null, false, false, null);
						}, null, null, null, null, null);
					}
					if (Z.X == 2)
					{
						ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_NNE.rpm", ZoneBuilderSandbox.PlacePrefabAlign.S, null, delegate(Cell c)
						{
							c.Clear(null, false, false, null);
						}, null, null, null, null, null);
					}
				}
				if (Z.Y == 0 && Z.GetTerrainNameFromDirection("N") == "TerrainTheSpindle")
				{
					if (Z.X == 0)
					{
						ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_SSW.rpm", ZoneBuilderSandbox.PlacePrefabAlign.N, null, delegate(Cell c)
						{
							c.Clear(null, false, false, null);
						}, null, null, null, null, null);
					}
					if (Z.X == 1)
					{
						ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_S.rpm", ZoneBuilderSandbox.PlacePrefabAlign.N, null, delegate(Cell c)
						{
							c.Clear(null, false, false, null);
						}, null, null, null, null, null);
					}
					if (Z.X == 2)
					{
						ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_SSE.rpm", ZoneBuilderSandbox.PlacePrefabAlign.N, null, delegate(Cell c)
						{
							c.Clear(null, false, false, null);
						}, null, null, null, null, null);
					}
				}
				if (Z.X == 0 && Z.GetTerrainNameFromDirection("W") == "TerrainTheSpindle")
				{
					if (Z.Y == 0)
					{
						ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_ENE.rpm", ZoneBuilderSandbox.PlacePrefabAlign.W, null, delegate(Cell c)
						{
							c.Clear(null, false, false, null);
						}, null, null, null, null, null);
					}
					if (Z.Y == 1)
					{
						ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_E.rpm", ZoneBuilderSandbox.PlacePrefabAlign.W, null, delegate(Cell c)
						{
							c.Clear(null, false, false, null);
						}, null, null, null, null, null);
					}
					if (Z.Y == 2)
					{
						ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_ESE.rpm", ZoneBuilderSandbox.PlacePrefabAlign.W, null, delegate(Cell c)
						{
							c.Clear(null, false, false, null);
						}, null, null, null, null, null);
					}
				}
				if (Z.X == 2 && Z.GetTerrainNameFromDirection("E") == "TerrainTheSpindle")
				{
					if (Z.Y == 0)
					{
						ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_WNW.rpm", ZoneBuilderSandbox.PlacePrefabAlign.E, null, delegate(Cell c)
						{
							c.Clear(null, false, false, null);
						}, null, null, null, null, null);
					}
					if (Z.Y == 1)
					{
						ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_W.rpm", ZoneBuilderSandbox.PlacePrefabAlign.E, null, delegate(Cell c)
						{
							c.Clear(null, false, false, null);
						}, null, null, null, null, null);
					}
					if (Z.Y == 2)
					{
						ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_WSW.rpm", ZoneBuilderSandbox.PlacePrefabAlign.E, null, delegate(Cell c)
						{
							c.Clear(null, false, false, null);
						}, null, null, null, null, null);
					}
				}
				if (Z.X == 0 && Z.Y == 0 && Z.GetTerrainNameFromDirection("NW") == "TerrainTheSpindle")
				{
					ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_SE.rpm", ZoneBuilderSandbox.PlacePrefabAlign.NW, null, delegate(Cell c)
					{
						c.Clear(null, false, false, null);
					}, null, null, null, null, null);
				}
				if (Z.X == 2 && Z.Y == 0 && Z.GetTerrainNameFromDirection("NE") == "TerrainTheSpindle")
				{
					ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_SW.rpm", ZoneBuilderSandbox.PlacePrefabAlign.NE, null, delegate(Cell c)
					{
						c.Clear(null, false, false, null);
					}, null, null, null, null, null);
				}
				if (Z.X == 0 && Z.Y == 2 && Z.GetTerrainNameFromDirection("SW") == "TerrainTheSpindle")
				{
					ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_NE.rpm", ZoneBuilderSandbox.PlacePrefabAlign.SW, null, delegate(Cell c)
					{
						c.Clear(null, false, false, null);
					}, null, null, null, null, null);
				}
				if (Z.X == 2 && Z.Y == 2 && Z.GetTerrainNameFromDirection("SE") == "TerrainTheSpindle")
				{
					ZoneBuilderSandbox.PlacePrefab(Z, "preset_tile_chunks/TombExteriorWall_NW.rpm", ZoneBuilderSandbox.PlacePrefabAlign.SE, null, delegate(Cell c)
					{
						c.Clear(null, false, false, null);
					}, null, null, null, null, null);
				}
			}
			foreach (Cell cell in from c in Z.GetCells()
				where !c.HasObjectWithPart("Physics")
				select c)
			{
				cell.AddObject("Air", null, null, null, null);
			}
			Z.GetCell(0, 0).AddObject(GameObjectFactory.Factory.CreateObject("DaylightWidget"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			Z.ClearReachableMap();
			Z.BuildReachableMap(Z.Width / 2, Z.Height / 2);
			return true;
		}
	}
}
