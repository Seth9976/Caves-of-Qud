using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x020012E8 RID: 4840
	[Serializable]
	public class RandomTile : IPart
	{
		// Token: 0x0600BE45 RID: 48709 RVA: 0x0045B72B File Offset: 0x0045992B
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600BE46 RID: 48710 RVA: 0x0045B72E File Offset: 0x0045992E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID || ID == SingletonEvent<RefreshTileEvent>.ID;
		}

		// Token: 0x0600BE47 RID: 48711 RVA: 0x0045B74C File Offset: 0x0045994C
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.RandomizeTile();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BE48 RID: 48712 RVA: 0x0045B75B File Offset: 0x0045995B
		public override bool HandleEvent(RefreshTileEvent E)
		{
			this.RandomizeTile();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BE49 RID: 48713 RVA: 0x0045B76C File Offset: 0x0045996C
		public void RandomizeTile()
		{
			List<string> list = this.Tiles.CachedCommaExpansion();
			if (list.Count > 0)
			{
				this.ParentObject.Render.Tile = list.GetRandomElement<string>(null);
			}
		}

		// Token: 0x0600BE4A RID: 48714 RVA: 0x0045B7A5 File Offset: 0x004599A5
		public bool IsTileInRandomSet(string Tile)
		{
			return this.Tiles.CachedCommaExpansion().Contains(Tile);
		}

		// Token: 0x04004CA2 RID: 19618
		public string Tiles = "";
	}
}
