using System;
using UnityEngine;

namespace XRL.World.ZoneFactories
{
	// Token: 0x020009A1 RID: 2465
	public class InteriorWorldZoneFactory : IZoneFactory
	{
		// Token: 0x06006A9A RID: 27290 RVA: 0x002247A9 File Offset: 0x002229A9
		public override bool CanBuildZone(ZoneRequest Request)
		{
			return Request.IsWorldZone;
		}

		// Token: 0x06006A9B RID: 27291 RVA: 0x002247B1 File Offset: 0x002229B1
		public override Zone GenerateZone(ZoneRequest Request, int Width, int Height)
		{
			if (!Request.ZoneID.IsNullOrEmpty())
			{
				The.ZoneManager.SetZoneProperty(Request.ZoneID, "Inside", "1");
			}
			if (!Request.IsWorldZone)
			{
				return new InteriorZone(Width, Height);
			}
			return new Zone(Width, Height);
		}

		// Token: 0x06006A9C RID: 27292 RVA: 0x002247F4 File Offset: 0x002229F4
		public override Zone BuildZone(ZoneRequest Request)
		{
			if (Request.IsWorldZone)
			{
				Zone zone = new Zone(80, 25);
				zone.ForeachCell(delegate(Cell c)
				{
					c.AddObject("TerrainTzimtzlum", null, null, null, null);
				});
				return zone;
			}
			return new InteriorZone(80, 25);
		}

		// Token: 0x06006A9D RID: 27293 RVA: 0x00224844 File Offset: 0x00222A44
		public override void AddBlueprintsFor(ZoneRequest Request)
		{
			if (Request.Schema.IsNullOrEmpty())
			{
				int num = Request.WorldID.IndexOf('@');
				if (num == -1)
				{
					return;
				}
				int num2 = Request.WorldID.IndexOf('@', ++num);
				if (num2 == -1)
				{
					num2 = Request.WorldID.Length - 1;
				}
				Request.Schema = Request.WorldID.Substring(num, num2 - num);
			}
			CellBlueprint cellBlueprint;
			if (this.Blueprint.CellBlueprintsByName.TryGetValue(Request.Schema, out cellBlueprint))
			{
				Request.Blueprints.Add(cellBlueprint.LevelBlueprint[Request.X, Request.Y, Mathf.Clamp(Request.Z, 0, 49)]);
			}
		}

		// Token: 0x06006A9E RID: 27294 RVA: 0x002248F6 File Offset: 0x00222AF6
		public override void AfterBuildZone(Zone Zone, ZoneManager ZoneManager)
		{
			Zone.SetInside(new bool?(true));
			ZoneManager.PaintWalls(Zone, 0, 0, -1, -1);
			ZoneManager.PaintWater(Zone, 0, 0, -1, -1);
		}
	}
}
