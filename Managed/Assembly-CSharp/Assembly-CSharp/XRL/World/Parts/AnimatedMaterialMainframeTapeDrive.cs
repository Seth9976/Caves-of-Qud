using System;
using System.Text;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020012C7 RID: 4807
	[Serializable]
	public class AnimatedMaterialMainframeTapeDrive : IPart
	{
		// Token: 0x0600BDA9 RID: 48553 RVA: 0x00457E44 File Offset: 0x00456044
		public AnimatedMaterialMainframeTapeDrive()
		{
			this.nFrameOffset = Stat.RandomCosmetic(0, 60);
			this.RushingBack = 0;
			this.RushingForward = 0;
		}

		// Token: 0x0600BDAA RID: 48554 RVA: 0x00457E9B File Offset: 0x0045609B
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BDAB RID: 48555 RVA: 0x00457E9E File Offset: 0x0045609E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BDAC RID: 48556 RVA: 0x00457EA8 File Offset: 0x004560A8
		public override bool Render(RenderEvent E)
		{
			if (E.Tile != null)
			{
				if (this.RushingForward > 0)
				{
					this.RushingForward--;
					string text = E.Tile.Substring(E.Tile.LastIndexOf('-') + 1);
					this.lastN++;
					if (this.lastN > 4)
					{
						this.lastN = 1;
					}
					this.tileBuilder.Length = 0;
					this.tileBuilder.Append(this.Tile).Append(this.lastN).Append('-')
						.Append(text);
					this.ParentObject.Render.Tile = this.tileBuilder.ToString();
				}
				else if (this.RushingBack > 0)
				{
					this.RushingBack--;
					string text2 = E.Tile.Substring(E.Tile.LastIndexOf('-') + 1);
					this.lastN--;
					if (this.lastN < 1)
					{
						this.lastN = 4;
					}
					this.tileBuilder.Length = 0;
					this.tileBuilder.Append(this.Tile).Append(this.lastN).Append('-')
						.Append(text2);
					this.ParentObject.Render.Tile = this.tileBuilder.ToString();
				}
				else if (XRLCore.FrameTimer.ElapsedMilliseconds - this.accumulator > 500L)
				{
					this.accumulator = XRLCore.FrameTimer.ElapsedMilliseconds;
					string text3 = E.Tile.Substring(E.Tile.LastIndexOf('-') + 1);
					int num = this.lastN + 1;
					this.lastN = num;
					if (num > 4)
					{
						this.lastN = 1;
					}
					this.tileBuilder.Length = 0;
					this.tileBuilder.Append(this.Tile).Append(this.lastN).Append('-')
						.Append(text3);
					this.ParentObject.Render.Tile = this.tileBuilder.ToString();
					if (Stat.RandomCosmetic(1, 64) <= 1)
					{
						this.RushingBack = Stat.Random(15, 120);
					}
					else if (Stat.RandomCosmetic(1, 64) <= 1)
					{
						this.RushingForward = Stat.Random(15, 120);
					}
				}
			}
			return true;
		}

		// Token: 0x0600BDAD RID: 48557 RVA: 0x004580FC File Offset: 0x004562FC
		public override bool FireEvent(Event E)
		{
			return base.FireEvent(E);
		}

		// Token: 0x04004C4A RID: 19530
		public int nFrameOffset;

		// Token: 0x04004C4B RID: 19531
		public int RushingBack;

		// Token: 0x04004C4C RID: 19532
		public int RushingForward;

		// Token: 0x04004C4D RID: 19533
		public string Color = "Normal";

		// Token: 0x04004C4E RID: 19534
		[NonSerialized]
		private StringBuilder tileBuilder = new StringBuilder();

		// Token: 0x04004C4F RID: 19535
		[NonSerialized]
		private int lastN = 1;

		// Token: 0x04004C50 RID: 19536
		[NonSerialized]
		private long accumulator;

		// Token: 0x04004C51 RID: 19537
		public string Tile = "Assets_Content_Textures_Tiles2_mainframe_tape_drive_";
	}
}
