using System;
using Genkit;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x0200127C RID: 4732
	[Serializable]
	public class RandomCraters : IPart
	{
		// Token: 0x0600BB69 RID: 47977 RVA: 0x004473E0 File Offset: 0x004455E0
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BB6A RID: 47978 RVA: 0x004473E3 File Offset: 0x004455E3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x0600BB6B RID: 47979 RVA: 0x004473F9 File Offset: 0x004455F9
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.SetupCraters();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB6C RID: 47980 RVA: 0x00447408 File Offset: 0x00445608
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ZoneLoaded")
			{
				this.SetupCraters();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600BB6D RID: 47981 RVA: 0x00447429 File Offset: 0x00445629
		public static int GetSeededRange(string Seed, int Low, int High)
		{
			return new Random(Hash.String(Seed)).Next(Low, High);
		}

		// Token: 0x0600BB6E RID: 47982 RVA: 0x00447440 File Offset: 0x00445640
		private void SetupCraters()
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
			num3 += Stat.Random(-1, 1);
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
