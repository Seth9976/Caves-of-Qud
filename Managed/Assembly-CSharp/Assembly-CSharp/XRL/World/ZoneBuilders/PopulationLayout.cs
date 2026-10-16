using System;
using System.Collections.Generic;
using Genkit;
using UnityEngine;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AC6 RID: 2758
	public class PopulationLayout
	{
		// Token: 0x06006F91 RID: 28561 RVA: 0x0027208C File Offset: 0x0027028C
		public PopulationLayout(Zone zone, InfluenceMapRegion region, Rect2D innerRect)
		{
			this.zone = zone;
			this.map = region.map;
			this.seed = region.Seed;
			this.innerRect = innerRect;
			this.position = this.innerRect.Center.location;
			this.originalRegion = region;
		}

		// Token: 0x06006F92 RID: 28562 RVA: 0x00272128 File Offset: 0x00270328
		public PopulationLayout(Zone zone, InfluenceMapRegion region, Rect2D innerRect, Location2D position)
		{
			this.zone = zone;
			this.map = region.map;
			this.seed = region.Seed;
			this.innerRect = innerRect;
			this.position = position;
			this.originalRegion = region;
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06006F93 RID: 28563 RVA: 0x002721B4 File Offset: 0x002703B4
		public InfluenceMapRegion region
		{
			get
			{
				if (this.map.Regions.Count <= this.seed)
				{
					return this.map.Regions[0];
				}
				return this.map.Regions[this.seed];
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06006F95 RID: 28565 RVA: 0x00272224 File Offset: 0x00270424
		// (set) Token: 0x06006F94 RID: 28564 RVA: 0x00272201 File Offset: 0x00270401
		public Location2D position
		{
			get
			{
				if (this.lastPosition != null)
				{
					return this.lastPosition;
				}
				if (this.map.Seeds.Count <= this.seed)
				{
					Debug.LogError("Couldn't get a seed:" + this.seed.ToString());
					return null;
				}
				return this.map.Seeds[this.seed];
			}
			set
			{
				this.lastPosition = value;
				this.map.Seeds[this.seed] = value;
			}
		}

		// Token: 0x04003328 RID: 13096
		public Rect2D innerRect;

		// Token: 0x04003329 RID: 13097
		public InfluenceMap map;

		// Token: 0x0400332A RID: 13098
		public int seed;

		// Token: 0x0400332B RID: 13099
		public List<Location2D> inside = new List<Location2D>();

		// Token: 0x0400332C RID: 13100
		public List<Location2D> insideWall = new List<Location2D>();

		// Token: 0x0400332D RID: 13101
		public List<Location2D> insideCorner = new List<Location2D>();

		// Token: 0x0400332E RID: 13102
		public List<Location2D> outside = new List<Location2D>();

		// Token: 0x0400332F RID: 13103
		public List<Location2D> outsideWall = new List<Location2D>();

		// Token: 0x04003330 RID: 13104
		public List<Location2D> outsideCorner = new List<Location2D>();

		// Token: 0x04003331 RID: 13105
		public bool hasStructure;

		// Token: 0x04003332 RID: 13106
		public Zone zone;

		// Token: 0x04003333 RID: 13107
		public InfluenceMapRegion originalRegion;

		// Token: 0x04003334 RID: 13108
		public Location2D lastPosition;
	}
}
