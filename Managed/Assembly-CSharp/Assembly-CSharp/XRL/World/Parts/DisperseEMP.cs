using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x020011A1 RID: 4513
	[Serializable]
	internal class DisperseEMP : IPart
	{
		// Token: 0x0600B37B RID: 45947 RVA: 0x00417A80 File Offset: 0x00415C80
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanBeDismemberedEvent>.ID || ID == PooledEvent<CanBeInvoluntarilyMovedEvent>.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<GetMatterPhaseEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == PooledEvent<GetScanTypeEvent>.ID || ID == PooledEvent<RespiresEvent>.ID;
		}

		// Token: 0x0600B37C RID: 45948 RVA: 0x00417AD4 File Offset: 0x00415CD4
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Active)
			{
				this.Countdown--;
				if (this.Countdown <= 0 && this.ParentObject.CurrentCell != null)
				{
					this.Countdown = 20;
					this.Active = false;
					ElectromagneticPulse.EMP(this.ParentObject.CurrentCell, 10, Stat.Roll("24-33", null), true, 1);
					this.ParentObject.Destroy(null, false, false, null);
				}
				return false;
			}
			if (Stat.Random(1, this.Chance) <= 1)
			{
				this.Active = true;
				this.ParentObject.Effects.Add(new Crackling());
				base.DidX("start", "crackling", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B37D RID: 45949 RVA: 0x00417B9E File Offset: 0x00415D9E
		public override bool HandleEvent(CanBeDismemberedEvent E)
		{
			return E.Object != this.ParentObject && base.HandleEvent(E);
		}

		// Token: 0x0600B37E RID: 45950 RVA: 0x00417BB7 File Offset: 0x00415DB7
		public override bool HandleEvent(CanBeInvoluntarilyMovedEvent E)
		{
			return E.Object != this.ParentObject && base.HandleEvent(E);
		}

		// Token: 0x0600B37F RID: 45951 RVA: 0x00417BD0 File Offset: 0x00415DD0
		public override bool HandleEvent(GetScanTypeEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.ScanType = Scanning.Scan.Tech;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B380 RID: 45952 RVA: 0x00417BF0 File Offset: 0x00415DF0
		public override bool HandleEvent(RespiresEvent E)
		{
			return false;
		}

		// Token: 0x0600B381 RID: 45953 RVA: 0x00417BF3 File Offset: 0x00415DF3
		public override bool HandleEvent(GetMatterPhaseEvent E)
		{
			E.MinMatterPhase(4);
			return false;
		}

		// Token: 0x0600B382 RID: 45954 RVA: 0x00417BFD File Offset: 0x00415DFD
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageReduction = 100;
			return false;
		}

		// Token: 0x0600B383 RID: 45955 RVA: 0x00417C08 File Offset: 0x00415E08
		public override bool Render(RenderEvent E)
		{
			if (!this.Active)
			{
				return true;
			}
			if (this.Tile == null)
			{
				this.Tile = this.ParentObject.Render.Tile;
				this.RenderString = this.ParentObject.Render.RenderString;
			}
			Render render = this.ParentObject.Render;
			render.Tile = null;
			int num = (XRLCore.CurrentFrame + this.FrameOffset) % 90;
			if (Stat.Random(1, 80) == 1 || this.FlickerFrame > 0)
			{
				render.Tile = null;
				if (this.FlickerFrame == 0)
				{
					render.RenderString = "_";
				}
				if (this.FlickerFrame == 1)
				{
					render.RenderString = "-";
				}
				if (this.FlickerFrame == 2)
				{
					render.RenderString = "|";
				}
				E.ColorString = "&W";
				if (this.FlickerFrame == 0)
				{
					this.FlickerFrame = 3;
				}
				this.FlickerFrame--;
				this.ParentObject.Sparksplatter();
			}
			else
			{
				render.RenderString = this.RenderString;
				render.Tile = this.Tile;
			}
			if (num < 4)
			{
				render.ColorString = "&W";
				render.TileColor = "&W";
				render.DetailColor = "c";
			}
			else if (num < 8)
			{
				render.ColorString = "&b";
				render.TileColor = "&b";
				render.DetailColor = "W";
			}
			else if (num < 12)
			{
				render.ColorString = "&c";
				render.TileColor = "&c";
				render.DetailColor = "b";
			}
			else
			{
				render.ColorString = "&C";
				render.TileColor = "&C";
				render.DetailColor = "W";
			}
			if (!Options.DisableTextAnimationEffects)
			{
				this.FrameOffset += Stat.Random(0, 20);
			}
			if (Stat.Random(1, 400) == 1 || this.FlickerFrame > 0)
			{
				render.ColorString = "&b";
				render.TileColor = "&b";
			}
			return false;
		}

		// Token: 0x0400485A RID: 18522
		public bool Active;

		// Token: 0x0400485B RID: 18523
		public string Tile;

		// Token: 0x0400485C RID: 18524
		public string RenderString = "@";

		// Token: 0x0400485D RID: 18525
		public int FlickerFrame;

		// Token: 0x0400485E RID: 18526
		public int FrameOffset;

		// Token: 0x0400485F RID: 18527
		public int Chance = 1500;

		// Token: 0x04004860 RID: 18528
		public int Countdown = 20;
	}
}
