using System;
using System.Collections.Generic;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A1A RID: 2586
	[Serializable]
	public class RandomTile : IObjectBuilder
	{
		// Token: 0x06006D3A RID: 27962 RVA: 0x002496BD File Offset: 0x002478BD
		public override void Initialize()
		{
			this.Tiles = "";
		}

		// Token: 0x06006D3B RID: 27963 RVA: 0x002496CC File Offset: 0x002478CC
		public override void Apply(GameObject Object, string Context)
		{
			List<string> list = this.Tiles.CachedCommaExpansion();
			if (list.Count > 0)
			{
				Object.Render.Tile = list.GetRandomElement<string>(null);
			}
		}

		// Token: 0x040031E7 RID: 12775
		public string Tiles;
	}
}
