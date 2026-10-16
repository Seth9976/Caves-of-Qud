using System;
using Genkit;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001281 RID: 4737
	[Serializable]
	public class RandomJungle : IPart
	{
		// Token: 0x0600BB89 RID: 48009 RVA: 0x00447F71 File Offset: 0x00446171
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BB8A RID: 48010 RVA: 0x00447F74 File Offset: 0x00446174
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x0600BB8B RID: 48011 RVA: 0x00447F8A File Offset: 0x0044618A
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.SetupJungle();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB8C RID: 48012 RVA: 0x00447F99 File Offset: 0x00446199
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ZoneLoaded")
			{
				this.SetupJungle();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BB8D RID: 48013 RVA: 0x00447FBC File Offset: 0x004461BC
		private void SetupJungle()
		{
			Render render = this.ParentObject.Render;
			if (Stat.Random(1, 10) == 1)
			{
				render.ColorString = "&G";
			}
			else
			{
				render.ColorString = "&g";
			}
			int num = Stat.Random(1, 10);
			if (num == 1)
			{
				render.RenderString = "\u0005";
			}
			else if (num < 5)
			{
				render.RenderString = "\u009d";
			}
			else if (num < 8)
			{
				render.RenderString = "ç";
			}
			else if (num < 11)
			{
				render.RenderString = "æ";
			}
			string text = "";
			int num2 = Stat.Random(1, 3);
			if (num2 == 1)
			{
				text = "a";
			}
			if (num2 == 2)
			{
				text = "b";
			}
			if (num2 == 3)
			{
				text = "c";
			}
			render.Tile = "terrain/tile_jungle" + Stat.Random(1, 3).ToString() + text + ".png";
			render.DetailColor = "G";
			if (this.ParentObject.Physics.CurrentCell.X > 0 && this.ParentObject.Physics.CurrentCell.X < 79 && this.ParentObject.Physics.CurrentCell.Y > 0 && this.ParentObject.Physics.CurrentCell.Y < 24)
			{
				bool flag = this.ParentObject.Physics.CurrentCell.X % 2 == 0;
				bool flag2 = RandomJungle.GetSeededRange((this.ParentObject.Physics.CurrentCell.X / 2).ToString() + "," + this.ParentObject.Physics.CurrentCell.Y.ToString(), 1, 4) == 1;
				int seededRange = RandomJungle.GetSeededRange(this.ParentObject.Physics.CurrentCell.Y.ToString() + "," + (this.ParentObject.Physics.CurrentCell.X / 2).ToString(), 1, 3);
				if (flag2)
				{
					Cell cellFromDirection = this.ParentObject.Physics.CurrentCell.GetCellFromDirection("E", true);
					Cell cellFromDirection2 = this.ParentObject.Physics.CurrentCell.GetCellFromDirection("W", true);
					if (cellFromDirection2 != null && cellFromDirection != null && ((flag && cellFromDirection.HasObjectWithBlueprint("TerrainJungle")) || (!flag && cellFromDirection2.HasObjectWithBlueprint("TerrainJungle"))))
					{
						if (flag)
						{
							render.Tile = "terrain/tile_jungleleft" + seededRange.ToString() + text + ".png";
						}
						else
						{
							render.Tile = "terrain/tile_jungleright" + seededRange.ToString() + text + ".png";
						}
					}
				}
			}
			this.ParentObject.RemovePart(this);
		}

		// Token: 0x0600BB8E RID: 48014 RVA: 0x00448278 File Offset: 0x00446478
		public static int GetSeededRange(string Seed, int Low, int High)
		{
			return new Random(Hash.String(Seed)).Next(Low, High);
		}
	}
}
