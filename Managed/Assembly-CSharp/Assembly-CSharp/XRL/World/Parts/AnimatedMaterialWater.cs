using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020012CD RID: 4813
	[Serializable]
	public class AnimatedMaterialWater : IPart
	{
		// Token: 0x0600BDC6 RID: 48582 RVA: 0x00458CD1 File Offset: 0x00456ED1
		public AnimatedMaterialWater()
		{
			this.nFrameOffset = Stat.RandomCosmetic(0, 60);
			this.Rushing = true;
		}

		// Token: 0x0600BDC7 RID: 48583 RVA: 0x00458CEE File Offset: 0x00456EEE
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BDC8 RID: 48584 RVA: 0x00458CF1 File Offset: 0x00456EF1
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BDC9 RID: 48585 RVA: 0x00458CFC File Offset: 0x00456EFC
		public override bool Render(RenderEvent E)
		{
			if (this.ParentObject.Physics.IsFreezing())
			{
				E.RenderString = "~";
				if (E.ColorsVisible)
				{
					E.ColorString = "&c";
					E.DetailColor = "C";
				}
			}
			else if (this.Acid)
			{
				Render render = this.ParentObject.Render;
				int num = (XRLCore.CurrentFrame + this.nFrameOffset) % 60;
				if (this.ParentObject.Physics.CurrentCell.ParentZone.Z == 10 && Stat.RandomCosmetic(1, 600) == 1)
				{
					E.RenderString = "÷";
					E.TileVariantColors("&Y^g", "&Y", "g");
				}
				if (Stat.RandomCosmetic(1, 60) == 1)
				{
					if (num < 15)
					{
						render.RenderString = "÷";
						if (E.ColorsVisible)
						{
							render.ColorString = "&g^G";
							render.TileColor = "&g";
							render.DetailColor = "G";
						}
					}
					else if (num < 30)
					{
						render.RenderString = " ";
						if (E.ColorsVisible)
						{
							render.ColorString = "&Y^g";
							render.TileColor = "&Y";
							render.DetailColor = "g";
						}
					}
					else if (num < 45)
					{
						render.RenderString = "÷";
						if (E.ColorsVisible)
						{
							render.ColorString = "&g^G";
							render.TileColor = "&g";
							render.DetailColor = "G";
						}
					}
					else
					{
						render.RenderString = "~";
						if (E.ColorsVisible)
						{
							render.ColorString = "&y^G";
							render.TileColor = "&y";
							render.DetailColor = "G";
						}
					}
				}
			}
			else if (this.Bloody)
			{
				Render render2 = this.ParentObject.Render;
				int num2 = (XRLCore.CurrentFrame + this.nFrameOffset) % 60;
				if (this.ParentObject.Physics.CurrentCell.ParentZone.Z == 10 && Stat.RandomCosmetic(1, 600) == 1)
				{
					E.RenderString = "÷";
					E.TileVariantColors("&Y^r", "&Y", "r");
				}
				if (Stat.RandomCosmetic(1, 60) == 1)
				{
					if (num2 < 15)
					{
						render2.RenderString = "÷";
						render2.ColorString = "&b^R";
						render2.TileColor = "&b";
						render2.DetailColor = "R";
					}
					else if (num2 < 30)
					{
						render2.RenderString = " ";
						render2.ColorString = "&Y^R";
						render2.TileColor = "&Y";
						render2.DetailColor = "R";
					}
					else if (num2 < 45)
					{
						render2.RenderString = "÷";
						render2.ColorString = "&b^R";
						render2.TileColor = "&b";
						render2.DetailColor = "R";
					}
					else
					{
						render2.RenderString = "~";
						render2.ColorString = "&y^r";
						render2.TileColor = "&y";
						render2.DetailColor = "r";
					}
				}
			}
			else if (this.Fresh)
			{
				Render render3 = this.ParentObject.Render;
				int num3 = (XRLCore.CurrentFrame + this.nFrameOffset) % 60;
				if (this.ParentObject.Physics.CurrentCell.ParentZone.Z == 10 && Stat.RandomCosmetic(1, 600) == 1)
				{
					E.RenderString = "÷";
					E.TileVariantColors("&Y^B", "&Y", "B");
				}
				if (Stat.RandomCosmetic(1, 60) == 1)
				{
					if (num3 < 15)
					{
						render3.RenderString = "÷";
						render3.ColorString = "&b^B";
						render3.TileColor = "&b";
						render3.DetailColor = "B";
					}
					else if (num3 < 30)
					{
						render3.RenderString = " ";
						render3.ColorString = "&Y^B";
						render3.TileColor = "&Y";
						render3.DetailColor = "B";
					}
					else if (num3 < 45)
					{
						render3.RenderString = "÷";
						render3.ColorString = "&b^B";
						render3.TileColor = "&b";
						render3.DetailColor = "B";
					}
					else
					{
						render3.RenderString = "~";
						render3.ColorString = "&y^B";
						render3.TileColor = "&y";
						render3.DetailColor = "B";
					}
				}
			}
			else if (this.Rushing)
			{
				Render render4 = this.ParentObject.Render;
				int num4 = (XRLCore.CurrentFrame + this.nFrameOffset) % 60;
				if (num4 < 15)
				{
					E.RenderString = "~";
					E.TileVariantColors("&B^b", "&B", "b");
				}
				else if (num4 < 30)
				{
					E.RenderString = render4.RenderString;
					E.TileVariantColors("&Y^b", "&Y", "b");
				}
				else if (num4 < 45)
				{
					E.RenderString = "~";
					E.TileVariantColors("&B^b", "&B", "b");
				}
				else
				{
					E.RenderString = render4.RenderString;
					E.TileVariantColors("&B^b", "&B", "b");
				}
			}
			else
			{
				Render render5 = this.ParentObject.Render;
				int num5 = (XRLCore.CurrentFrame + this.nFrameOffset) % 60;
				if (this.ParentObject.Physics.CurrentCell.ParentZone.Z == 10 && Stat.RandomCosmetic(1, 600) == 1)
				{
					E.RenderString = "~";
					E.TileVariantColors("&Y^b", "&Y", "b");
				}
				if (Stat.RandomCosmetic(1, 60) == 1)
				{
					if (num5 < 15)
					{
						render5.RenderString = "÷";
						render5.ColorString = "&B^b";
						render5.TileColor = "&B";
						render5.DetailColor = "b";
					}
					else if (num5 < 30)
					{
						render5.RenderString = "~";
						render5.ColorString = "&B^b";
						render5.TileColor = "&B";
						render5.DetailColor = "b";
					}
					else if (num5 < 45)
					{
						render5.RenderString = " ";
						render5.ColorString = "&B^b";
						render5.TileColor = "&B";
						render5.DetailColor = "b";
					}
					else
					{
						render5.RenderString = "~";
						render5.ColorString = "&B^b";
						render5.TileColor = "&B";
						render5.DetailColor = "b";
					}
				}
			}
			return base.Render(E);
		}

		// Token: 0x04004C6B RID: 19563
		public int nFrameOffset;

		// Token: 0x04004C6C RID: 19564
		public bool Rushing;

		// Token: 0x04004C6D RID: 19565
		public bool Fresh;

		// Token: 0x04004C6E RID: 19566
		public bool Acid;

		// Token: 0x04004C6F RID: 19567
		public bool Bloody;
	}
}
