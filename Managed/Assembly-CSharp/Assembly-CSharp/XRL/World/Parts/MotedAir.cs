using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000F76 RID: 3958
	[Serializable]
	public class MotedAir : IPart
	{
		// Token: 0x06009D92 RID: 40338 RVA: 0x0039144C File Offset: 0x0038F64C
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009D93 RID: 40339 RVA: 0x0039144F File Offset: 0x0038F64F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID;
		}

		// Token: 0x06009D94 RID: 40340 RVA: 0x00391468 File Offset: 0x0038F668
		public override bool Render(RenderEvent E)
		{
			if (!Options.UseTiles && !string.IsNullOrEmpty(this.render))
			{
				if (Stat.RandomCosmetic(1, 1000) == 1)
				{
					E.ColorString = "&" + Crayons.GetRandomColorAll(null) + "^k";
				}
				if (Stat.RandomCosmetic(1, 1000) == 1)
				{
					int num = Stat.RandomCosmetic(1, 100);
					if (num < 2)
					{
						E.RenderString = "ø";
					}
					else if (num < 5)
					{
						E.RenderString = "ù";
					}
					else if (num < 8)
					{
						E.RenderString = "\a";
					}
					else if (num < 40)
					{
						E.RenderString = "ú";
					}
					else if (num < 45)
					{
						E.RenderString = ".";
					}
					else
					{
						E.RenderString = " ";
					}
				}
			}
			return true;
		}

		// Token: 0x06009D95 RID: 40341 RVA: 0x00391534 File Offset: 0x0038F734
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (!Options.UseTiles && this.render == null)
			{
				int num = Stat.RandomCosmetic(1, 100);
				this.color = Crayons.GetRandomColorAll(null);
				if (num < 2)
				{
					this.render = "ø";
				}
				else if (num < 5)
				{
					this.render = "ù";
				}
				else if (num < 8)
				{
					this.render = "\a";
				}
				else if (num < 40)
				{
					this.render = "ú";
				}
				else if (num < 45)
				{
					this.render = ".";
				}
				else
				{
					this.render = " ";
				}
				this.ParentObject.Render.RenderString = this.render;
				this.ParentObject.Render.ColorString = "&" + this.color + "^k";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004125 RID: 16677
		private string render;

		// Token: 0x04004126 RID: 16678
		private string color;
	}
}
