using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020012EE RID: 4846
	[Serializable]
	public class ThinWorldMaterial : IPart
	{
		// Token: 0x0600BE58 RID: 48728 RVA: 0x0045B9C5 File Offset: 0x00459BC5
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanBeDismemberedEvent>.ID || ID == PooledEvent<CanBeInvoluntarilyMovedEvent>.ID || ID == PooledEvent<GetElectricalConductivityEvent>.ID || ID == PooledEvent<GetMatterPhaseEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID;
		}

		// Token: 0x0600BE59 RID: 48729 RVA: 0x0045B9FB File Offset: 0x00459BFB
		public override bool HandleEvent(CanBeDismemberedEvent E)
		{
			return E.Object != this.ParentObject && base.HandleEvent(E);
		}

		// Token: 0x0600BE5A RID: 48730 RVA: 0x0045BA14 File Offset: 0x00459C14
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 1 && E.Object == this.ParentObject)
			{
				E.Value = 100;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BE5B RID: 48731 RVA: 0x0045BA3E File Offset: 0x00459C3E
		public override bool HandleEvent(CanBeInvoluntarilyMovedEvent E)
		{
			return E.Object != this.ParentObject && base.HandleEvent(E);
		}

		// Token: 0x0600BE5C RID: 48732 RVA: 0x0045BA57 File Offset: 0x00459C57
		public override bool HandleEvent(GetMatterPhaseEvent E)
		{
			E.MinMatterPhase(4);
			return false;
		}

		// Token: 0x0600BE5D RID: 48733 RVA: 0x0045BA61 File Offset: 0x00459C61
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageReduction = 100;
			return false;
		}

		// Token: 0x0600BE5E RID: 48734 RVA: 0x0045BA6C File Offset: 0x00459C6C
		public override bool Render(RenderEvent E)
		{
			if (this.Tile == null)
			{
				this.Tile = this.ParentObject.Render.Tile;
				this.RenderString = this.ParentObject.Render.RenderString;
			}
			Render render = this.ParentObject.Render;
			render.Tile = null;
			int num = (XRLCore.CurrentFrame + this.FrameOffset) % 200;
			if (Stat.Random(1, 200) == 1 || this.FlickerFrame > 0)
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
				E.ColorString = "&C";
				if (this.FlickerFrame == 0)
				{
					this.FlickerFrame = 3;
				}
				this.FlickerFrame--;
			}
			else
			{
				render.RenderString = this.RenderString;
				render.Tile = this.Tile;
			}
			if (num < 4)
			{
				render.ColorString = "&C";
				render.TileColor = "&C";
				render.DetailColor = "c";
			}
			else if (num < 8)
			{
				render.ColorString = "&b";
				render.TileColor = "&b";
				render.DetailColor = "C";
			}
			else if (num < 12)
			{
				render.ColorString = "&c";
				render.TileColor = "&c";
				render.DetailColor = "b";
			}
			else
			{
				render.ColorString = "&B";
				render.TileColor = "&B";
				render.DetailColor = "b";
			}
			if (!Options.DisableTextAnimationEffects)
			{
				this.FrameOffset += Stat.Random(0, 20);
			}
			if (Stat.Random(1, 400) == 1 || this.FlickerFrame > 0)
			{
				render.ColorString = "&Y";
				render.TileColor = "&Y";
			}
			return base.Render(E);
		}

		// Token: 0x04004CA3 RID: 19619
		public string Tile;

		// Token: 0x04004CA4 RID: 19620
		public string RenderString = "@";

		// Token: 0x04004CA5 RID: 19621
		public int FlickerFrame;

		// Token: 0x04004CA6 RID: 19622
		public int FrameOffset;
	}
}
