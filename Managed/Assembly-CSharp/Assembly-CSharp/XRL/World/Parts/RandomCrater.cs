using System;
using Genkit;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x0200127D RID: 4733
	[Serializable]
	public class RandomCrater : IPart
	{
		// Token: 0x0600BB70 RID: 47984 RVA: 0x0044764A File Offset: 0x0044584A
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BB71 RID: 47985 RVA: 0x0044764D File Offset: 0x0044584D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x0600BB72 RID: 47986 RVA: 0x00447663 File Offset: 0x00445863
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.SetupCrater();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB73 RID: 47987 RVA: 0x00447672 File Offset: 0x00445872
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ZoneLoaded")
			{
				this.SetupCrater();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BB74 RID: 47988 RVA: 0x00447693 File Offset: 0x00445893
		public static int GetSeededRange(string Seed, int Low, int High)
		{
			return new Random(Hash.String(Seed)).Next(Low, High);
		}

		// Token: 0x0600BB75 RID: 47989 RVA: 0x004476A8 File Offset: 0x004458A8
		private void SetupCrater()
		{
			Render render = this.ParentObject.Render;
			int num = Stat.Random(1, 2);
			int num2 = Stat.Random(1, 15);
			if (this.ParentObject.HasProperty("SmallCrater"))
			{
				num2 = 2;
			}
			if (this.ParentObject.HasProperty("BigCrater"))
			{
				num2 = 4;
			}
			int num3 = this.ParentObject.Physics.CurrentCell.X;
			num3 = 50;
			if (num3 < 50)
			{
				if (num == 1)
				{
					render.ColorString = "&K";
				}
				if (num == 2)
				{
					render.ColorString = "&K";
				}
				Render render2 = render;
				render2.ColorString += "^Y";
			}
			else if (num3 < 55)
			{
				if (num == 1)
				{
					render.ColorString = "&K";
				}
				if (num == 2)
				{
					render.ColorString = "&K";
				}
				Render render3 = render;
				render3.ColorString += "^y";
			}
			else if (num3 < 60)
			{
				if (num == 1)
				{
					render.ColorString = "&y";
				}
				if (num == 2)
				{
					render.ColorString = "&Y";
				}
				Render render4 = render;
				render4.ColorString += "^K";
			}
			else
			{
				if (num == 1)
				{
					render.ColorString = "&y";
				}
				if (num == 2)
				{
					render.ColorString = "&K";
				}
				Render render5 = render;
				render5.ColorString += "^k";
			}
			if (num2 == 1)
			{
				render.RenderString = ":";
			}
			if (num2 == 2)
			{
				render.RenderString = ".";
			}
			if (num2 == 3)
			{
				render.RenderString = "o";
			}
			if (num2 == 4)
			{
				render.RenderString = "O";
			}
			if (num2 == 5)
			{
				render.RenderString = "0";
			}
			if (num2 > 5)
			{
				render.RenderString = " ";
			}
			int num4 = Stat.Random(1, 4);
			if (num4 == 1)
			{
				render.DetailColor = "r";
			}
			if (num4 == 2)
			{
				render.DetailColor = "b";
			}
			if (num4 == 3)
			{
				render.DetailColor = "m";
			}
			if (num4 == 4)
			{
				render.DetailColor = "K";
			}
			this.ParentObject.RemovePart(this);
		}
	}
}
