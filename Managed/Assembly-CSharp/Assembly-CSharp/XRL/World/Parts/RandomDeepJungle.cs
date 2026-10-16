using System;
using Genkit;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x0200127E RID: 4734
	[Serializable]
	public class RandomDeepJungle : IPart
	{
		// Token: 0x0600BB77 RID: 47991 RVA: 0x004478AB File Offset: 0x00445AAB
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BB78 RID: 47992 RVA: 0x004478AE File Offset: 0x00445AAE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x0600BB79 RID: 47993 RVA: 0x004478C4 File Offset: 0x00445AC4
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.SetupDeepJungle();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB7A RID: 47994 RVA: 0x004478D3 File Offset: 0x00445AD3
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ZoneLoaded")
			{
				this.SetupDeepJungle();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BB7B RID: 47995 RVA: 0x004478F4 File Offset: 0x00445AF4
		private void SetupDeepJungle()
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
			if (num < 3)
			{
				render.RenderString = "á";
			}
			else if (num < 6)
			{
				render.RenderString = "\u009d";
			}
			else if (num < 9)
			{
				render.RenderString = "ç";
			}
			else if (num < 11)
			{
				render.RenderString = "æ";
			}
			render.Tile = "Terrain/sw_deepjungle_" + Stat.Random(1, 4).ToString() + ".bmp";
			render.DetailColor = (50.in100() ? "G" : "g");
			if (this.ParentObject.Physics.CurrentCell.X > 0 && this.ParentObject.Physics.CurrentCell.X < 79 && this.ParentObject.Physics.CurrentCell.Y > 0 && this.ParentObject.Physics.CurrentCell.Y < 24)
			{
				bool flag = this.ParentObject.Physics.CurrentCell.X % 2 == 0;
				bool flag2 = RandomDeepJungle.GetSeededRange((this.ParentObject.Physics.CurrentCell.X / 2).ToString() + "," + this.ParentObject.Physics.CurrentCell.Y.ToString(), 1, 4) == 1;
				string text = ((RandomDeepJungle.GetSeededRange(this.ParentObject.Physics.CurrentCell.Y.ToString() + "," + (this.ParentObject.Physics.CurrentCell.X / 2).ToString(), 1, 3) == 1) ? "a" : "b");
				if (flag2)
				{
					Cell cellFromDirection = this.ParentObject.Physics.CurrentCell.GetCellFromDirection("E", true);
					Cell cellFromDirection2 = this.ParentObject.Physics.CurrentCell.GetCellFromDirection("W", true);
					if (cellFromDirection2 != null && cellFromDirection != null && ((flag && cellFromDirection.HasObjectWithBlueprint("TerrainDeepJungle")) || (!flag && cellFromDirection2.HasObjectWithBlueprint("TerrainDeepJungle"))))
					{
						if (flag)
						{
							render.Tile = "Terrain/sw_deepjungle_" + text + "_left.bmp";
						}
						else
						{
							render.Tile = "Terrain/sw_deepjungle_" + text + "_right.bmp";
						}
					}
				}
			}
			this.ParentObject.RemovePart(this);
		}

		// Token: 0x0600BB7C RID: 47996 RVA: 0x00447B97 File Offset: 0x00445D97
		public static int GetSeededRange(string Seed, int Low, int High)
		{
			return new Random(Hash.String(Seed)).Next(Low, High);
		}
	}
}
