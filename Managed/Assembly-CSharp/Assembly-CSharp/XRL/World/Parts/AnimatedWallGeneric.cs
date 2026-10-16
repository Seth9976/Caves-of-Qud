using System;
using Cysharp.Text;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020012CC RID: 4812
	[Serializable]
	public class AnimatedWallGeneric : IPart
	{
		// Token: 0x0600BDBF RID: 48575 RVA: 0x00458900 File Offset: 0x00456B00
		public AnimatedWallGeneric()
		{
			this.nFrameOffset = Stat.RandomCosmetic(0, 60);
			this.RushingBack = 0;
			this.RushingForward = 0;
		}

		// Token: 0x0600BDC0 RID: 48576 RVA: 0x00458966 File Offset: 0x00456B66
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BDC1 RID: 48577 RVA: 0x00458969 File Offset: 0x00456B69
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<RepaintedEvent>.ID;
		}

		// Token: 0x0600BDC2 RID: 48578 RVA: 0x0045897F File Offset: 0x00456B7F
		public override bool HandleEvent(RepaintedEvent E)
		{
			this.postfix = null;
			return true;
		}

		// Token: 0x0600BDC3 RID: 48579 RVA: 0x00458989 File Offset: 0x00456B89
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BDC4 RID: 48580 RVA: 0x00458994 File Offset: 0x00456B94
		public override bool Render(RenderEvent E)
		{
			if (E.Tile != null)
			{
				if (this.postfix == null)
				{
					this.atlas = base.GetPropertyOrTag("PaintedWallAtlas", null);
					if (E.Tile.Contains('-'))
					{
						this.postfix = E.Tile.Substring(E.Tile.LastIndexOf('-') + 1);
					}
					else
					{
						this.postfix = "";
					}
				}
				if (string.IsNullOrEmpty(this.postfix))
				{
					return true;
				}
				using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
				{
					if (this.RushingForward > 0)
					{
						this.RushingForward--;
						string text = E.Tile.Substring(E.Tile.LastIndexOf('-') + 1);
						this.lastN++;
						if (this.lastN >= this.Frames)
						{
							this.lastN = 0;
						}
						utf16ValueStringBuilder.Clear();
						utf16ValueStringBuilder.Append(this.atlas);
						utf16ValueStringBuilder.Append(this.Tile);
						utf16ValueStringBuilder.Append(this.lastN);
						utf16ValueStringBuilder.Append('-');
						utf16ValueStringBuilder.Append(text);
						this.ParentObject.Render.Tile = utf16ValueStringBuilder.ToString();
					}
					else if (this.RushingBack > 0)
					{
						this.RushingBack--;
						string text2 = E.Tile.Substring(E.Tile.LastIndexOf('-') + 1);
						this.lastN--;
						if (this.lastN < 0)
						{
							this.lastN = this.Frames - 1;
						}
						utf16ValueStringBuilder.Clear();
						utf16ValueStringBuilder.Append(this.atlas);
						utf16ValueStringBuilder.Append(this.Tile);
						utf16ValueStringBuilder.Append(this.lastN);
						utf16ValueStringBuilder.Append('-');
						utf16ValueStringBuilder.Append(text2);
						this.ParentObject.Render.Tile = utf16ValueStringBuilder.ToString();
					}
					else if (XRLCore.FrameTimer.ElapsedMilliseconds - this.accumulator >= (long)this.FrameMS)
					{
						this.accumulator = XRLCore.FrameTimer.ElapsedMilliseconds;
						string text3 = E.Tile.Substring(E.Tile.LastIndexOf('-') + 1);
						int num = this.lastN + 1;
						this.lastN = num;
						if (num >= this.Frames)
						{
							this.lastN = 0;
						}
						utf16ValueStringBuilder.Clear();
						utf16ValueStringBuilder.Append(this.atlas);
						utf16ValueStringBuilder.Append(this.Tile);
						utf16ValueStringBuilder.Append(this.lastN);
						utf16ValueStringBuilder.Append('-');
						utf16ValueStringBuilder.Append(text3);
						this.ParentObject.Render.Tile = utf16ValueStringBuilder.ToString();
						if (this.AllowRushing)
						{
							if (Stat.RandomCosmetic(1, this.RushingChance) <= 1)
							{
								this.RushingBack = Stat.Random(15, 120);
							}
							else if (Stat.RandomCosmetic(1, this.RushingChance) <= 1)
							{
								this.RushingForward = Stat.Random(15, 120);
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600BDC5 RID: 48581 RVA: 0x00458CC8 File Offset: 0x00456EC8
		public override bool FireEvent(Event E)
		{
			return base.FireEvent(E);
		}

		// Token: 0x04004C5D RID: 19549
		public int Frames;

		// Token: 0x04004C5E RID: 19550
		public int nFrameOffset;

		// Token: 0x04004C5F RID: 19551
		public int RushingBack;

		// Token: 0x04004C60 RID: 19552
		public int RushingForward;

		// Token: 0x04004C61 RID: 19553
		public string Color = "Normal";

		// Token: 0x04004C62 RID: 19554
		public string Tile = "";

		// Token: 0x04004C63 RID: 19555
		public bool AllowRushing = true;

		// Token: 0x04004C64 RID: 19556
		public int RushingChance = 64;

		// Token: 0x04004C65 RID: 19557
		public int FrameMS = 500;

		// Token: 0x04004C66 RID: 19558
		[NonSerialized]
		private int lastN = 1;

		// Token: 0x04004C67 RID: 19559
		[NonSerialized]
		private long accumulator;

		// Token: 0x04004C68 RID: 19560
		[NonSerialized]
		private string postfix;

		// Token: 0x04004C69 RID: 19561
		[NonSerialized]
		private string lastwall;

		// Token: 0x04004C6A RID: 19562
		[NonSerialized]
		private string atlas;
	}
}
