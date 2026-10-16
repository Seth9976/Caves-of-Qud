using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000995 RID: 2453
	[Serializable]
	public struct ZoneRequest
	{
		// Token: 0x060068FE RID: 26878 RVA: 0x0020F154 File Offset: 0x0020D354
		public ZoneRequest(string ZoneID)
		{
			this.ZoneID = ZoneID;
			this.IsWorldZone = ZoneID.IndexOf('.') == -1;
			this.Seed = null;
			this.Zone = null;
			this.World = null;
			this.Blueprints = null;
			if (this.IsWorldZone)
			{
				this.WorldID = ZoneID;
				this.Schema = (this.Instance = null);
				this.WorldX = (this.WorldY = (this.X = (this.Y = -1)));
				this.Z = 10;
				return;
			}
			XRL.World.ZoneID.Parse(ZoneID, out this.WorldID, out this.Schema, out this.Instance, out this.WorldX, out this.WorldY, out this.X, out this.Y, out this.Z);
		}

		// Token: 0x060068FF RID: 26879 RVA: 0x0020F218 File Offset: 0x0020D418
		public ZoneRequest(Zone Zone)
		{
			this.Zone = Zone;
			this.ZoneID = Zone.ZoneID;
			this.IsWorldZone = Zone.IsWorldMap();
			this.Seed = null;
			this.World = null;
			this.Blueprints = null;
			if (this.IsWorldZone)
			{
				this.WorldID = this.ZoneID;
				this.Schema = (this.Instance = null);
				this.WorldX = (this.WorldY = (this.X = (this.Y = -1)));
				this.Z = 10;
			}
			else
			{
				this.WorldID = Zone.ZoneWorld;
				this.WorldX = Zone.wX;
				this.WorldY = Zone.wY;
				this.X = Zone.X;
				this.Y = Zone.Y;
				this.Z = Zone.Z;
			}
			InteriorZone interiorZone = Zone as InteriorZone;
			if (interiorZone != null)
			{
				this.Schema = interiorZone.Schema;
				this.Instance = interiorZone.Instance;
				return;
			}
			this.Schema = (this.Instance = null);
		}

		// Token: 0x06006900 RID: 26880 RVA: 0x0020F324 File Offset: 0x0020D524
		public ZoneRequest(string WorldID, int WorldX, int WorldY, int X, int Y, int Z)
		{
			this.ZoneID = XRL.World.ZoneID.Assemble(WorldID, WorldX, WorldY, X, Y, Z);
			this.IsWorldZone = false;
			this.Seed = null;
			this.Zone = null;
			this.World = null;
			this.Schema = null;
			this.Instance = null;
			this.Blueprints = null;
			this.WorldID = WorldID;
			this.WorldX = WorldX;
			this.WorldY = WorldY;
			this.X = X;
			this.Y = Y;
			this.Z = Z;
		}

		// Token: 0x06006901 RID: 26881 RVA: 0x0020F3A4 File Offset: 0x0020D5A4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[ZoneID: ",
				this.ZoneID,
				", WorldID: ",
				this.WorldID,
				", Schema: ",
				this.Schema,
				", Instance: ",
				this.Instance,
				", Seed: ",
				this.Seed,
				", Position: (",
				this.WorldX.ToString(),
				", ",
				this.WorldY.ToString(),
				", ",
				this.X.ToString(),
				", ",
				this.Y.ToString(),
				", ",
				this.Z.ToString(),
				"), IsWorldZone: ",
				this.IsWorldZone.ToString(),
				"]"
			});
		}

		// Token: 0x04003052 RID: 12370
		public string ZoneID;

		// Token: 0x04003053 RID: 12371
		public string WorldID;

		// Token: 0x04003054 RID: 12372
		public string Schema;

		// Token: 0x04003055 RID: 12373
		public string Instance;

		// Token: 0x04003056 RID: 12374
		public string Seed;

		// Token: 0x04003057 RID: 12375
		public int WorldX;

		// Token: 0x04003058 RID: 12376
		public int WorldY;

		// Token: 0x04003059 RID: 12377
		public int X;

		// Token: 0x0400305A RID: 12378
		public int Y;

		// Token: 0x0400305B RID: 12379
		public int Z;

		// Token: 0x0400305C RID: 12380
		public bool IsWorldZone;

		// Token: 0x0400305D RID: 12381
		public Zone Zone;

		// Token: 0x0400305E RID: 12382
		public WorldBlueprint World;

		// Token: 0x0400305F RID: 12383
		public List<ZoneBlueprint> Blueprints;
	}
}
