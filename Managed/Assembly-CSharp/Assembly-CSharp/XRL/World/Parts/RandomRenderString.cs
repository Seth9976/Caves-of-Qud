using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x020012E6 RID: 4838
	[Serializable]
	public class RandomRenderString : IPart
	{
		// Token: 0x0600BE3B RID: 48699 RVA: 0x0045B626 File Offset: 0x00459826
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600BE3C RID: 48700 RVA: 0x0045B629 File Offset: 0x00459829
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600BE3D RID: 48701 RVA: 0x0045B63F File Offset: 0x0045983F
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.RandomizeTile();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BE3E RID: 48702 RVA: 0x0045B650 File Offset: 0x00459850
		public void RandomizeTile()
		{
			List<string> list = this.Strings.CachedCommaExpansion();
			if (list.Count > 0)
			{
				this.ParentObject.Render.RenderString = ((char)int.Parse(list.GetRandomElement<string>(null))).ToString();
			}
		}

		// Token: 0x0600BE3F RID: 48703 RVA: 0x0045B697 File Offset: 0x00459897
		public bool IsTileInRandomSet(string Tile)
		{
			return this.Strings.CachedCommaExpansion().Contains(Tile);
		}

		// Token: 0x04004CA1 RID: 19617
		public string Strings = "";
	}
}
