using System;
using Genkit;

namespace XRL.World.Parts
{
	// Token: 0x02001280 RID: 4736
	[Serializable]
	public class RandomFungal : IPart
	{
		// Token: 0x0600BB82 RID: 48002 RVA: 0x00447D12 File Offset: 0x00445F12
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BB83 RID: 48003 RVA: 0x00447D15 File Offset: 0x00445F15
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x0600BB84 RID: 48004 RVA: 0x00447D2B File Offset: 0x00445F2B
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.SetupFungal();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB85 RID: 48005 RVA: 0x00447D3A File Offset: 0x00445F3A
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ZoneLoaded")
			{
				this.SetupFungal();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BB86 RID: 48006 RVA: 0x00447D5C File Offset: 0x00445F5C
		private void SetupFungal()
		{
			Render render = this.ParentObject.Render;
			if (this.ParentObject.Physics.CurrentCell.X > 0 && this.ParentObject.Physics.CurrentCell.X < 79 && this.ParentObject.Physics.CurrentCell.Y > 0 && this.ParentObject.Physics.CurrentCell.Y < 24)
			{
				bool flag = this.ParentObject.Physics.CurrentCell.X % 2 == 0;
				bool flag2 = RandomFungal.GetSeededRange((this.ParentObject.Physics.CurrentCell.X / 2).ToString() + "," + this.ParentObject.Physics.CurrentCell.Y.ToString(), 1, 4) == 1;
				string text = ((RandomFungal.GetSeededRange(this.ParentObject.Physics.CurrentCell.Y.ToString() + "," + (this.ParentObject.Physics.CurrentCell.X / 2).ToString(), 1, 3) == 1) ? "a" : "b");
				if (flag2)
				{
					Cell cellFromDirection = this.ParentObject.Physics.CurrentCell.GetCellFromDirection("E", true);
					Cell cellFromDirection2 = this.ParentObject.Physics.CurrentCell.GetCellFromDirection("W", true);
					if (cellFromDirection2 != null && cellFromDirection != null && ((flag && cellFromDirection.HasObjectWithPropertyOrTagEqualToValue("Terrain", "Fungal")) || (!flag && cellFromDirection2.HasObjectWithPropertyOrTagEqualToValue("Terrain", "Fungal"))))
					{
						if (flag)
						{
							render.Tile = "Terrain/sw_worldmap_rainbowwood_left_" + text + ".bmp";
						}
						else
						{
							render.Tile = "Terrain/sw_worldmap_rainbowwood_right_" + text + ".bmp";
						}
					}
				}
			}
			this.ParentObject.RemovePart(this);
		}

		// Token: 0x0600BB87 RID: 48007 RVA: 0x00447F55 File Offset: 0x00446155
		public static int GetSeededRange(string Seed, int Low, int High)
		{
			return new Random(Hash.String(Seed)).Next(Low, High);
		}
	}
}
