using System;
using System.Collections.Generic;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x020012D9 RID: 4825
	[Serializable]
	public class HologramMaterial : IPart
	{
		// Token: 0x0600BDF8 RID: 48632 RVA: 0x0045A2CE File Offset: 0x004584CE
		public override void AddedAfterCreation()
		{
			base.AddedAfterCreation();
			this.ParentObject.MakeNonflammable();
		}

		// Token: 0x0600BDF9 RID: 48633 RVA: 0x0045A2E4 File Offset: 0x004584E4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanBeDismemberedEvent>.ID || ID == PooledEvent<CanBeInvoluntarilyMovedEvent>.ID || ID == PooledEvent<GetElectricalConductivityEvent>.ID || ID == PooledEvent<GetMatterPhaseEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == PooledEvent<GetScanTypeEvent>.ID || ID == ObjectCreatedEvent.ID || ID == PooledEvent<RespiresEvent>.ID;
		}

		// Token: 0x0600BDFA RID: 48634 RVA: 0x0045A33D File Offset: 0x0045853D
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.ParentObject.MakeImperviousToHeat();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BDFB RID: 48635 RVA: 0x0045A352 File Offset: 0x00458552
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 1 && E.Object == this.ParentObject)
			{
				E.Value = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BDFC RID: 48636 RVA: 0x0045A37B File Offset: 0x0045857B
		public override bool HandleEvent(CanBeDismemberedEvent E)
		{
			return E.Object != this.ParentObject && base.HandleEvent(E);
		}

		// Token: 0x0600BDFD RID: 48637 RVA: 0x0045A394 File Offset: 0x00458594
		public override bool HandleEvent(CanBeInvoluntarilyMovedEvent E)
		{
			return E.Object != this.ParentObject && base.HandleEvent(E);
		}

		// Token: 0x0600BDFE RID: 48638 RVA: 0x0045A3AD File Offset: 0x004585AD
		public override bool HandleEvent(GetMatterPhaseEvent E)
		{
			E.MinMatterPhase(4);
			return false;
		}

		// Token: 0x0600BDFF RID: 48639 RVA: 0x0045A3B7 File Offset: 0x004585B7
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageReduction = 100;
			return false;
		}

		// Token: 0x0600BE00 RID: 48640 RVA: 0x0045A3C2 File Offset: 0x004585C2
		public override bool HandleEvent(GetScanTypeEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.ScanType = Scanning.Scan.Tech;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BE01 RID: 48641 RVA: 0x0045A3E2 File Offset: 0x004585E2
		public override bool HandleEvent(RespiresEvent E)
		{
			return false;
		}

		// Token: 0x0600BE02 RID: 48642 RVA: 0x0045A3E5 File Offset: 0x004585E5
		public void StartFlicker()
		{
			this.FlickerFrame = 4;
		}

		// Token: 0x0600BE03 RID: 48643 RVA: 0x0045A3EE File Offset: 0x004585EE
		public void reset()
		{
			this.ColorStringParts = null;
			this.DetailColorParts = null;
		}

		// Token: 0x0600BE04 RID: 48644 RVA: 0x0045A400 File Offset: 0x00458600
		public override bool Render(RenderEvent E)
		{
			string text = null;
			string text2 = null;
			int num = (XRLCore.CurrentFrame + this.FrameOffset) % 200;
			if (!this.ColorStrings.IsNullOrEmpty())
			{
				if (this.ColorStringParts == null)
				{
					this.ColorStringParts = this.ColorStrings.CachedCommaExpansion();
				}
				int count = this.ColorStringParts.Count;
				int num2 = num / count;
				text = ((num2 < count) ? this.ColorStringParts[num2] : this.ColorStringParts[count - 1]);
			}
			if (!this.DetailColors.IsNullOrEmpty())
			{
				if (this.DetailColorParts == null)
				{
					this.DetailColorParts = this.DetailColors.CachedCommaExpansion();
				}
				int count2 = this.DetailColorParts.Count;
				int num3 = num / count2;
				text2 = ((num3 < count2) ? this.DetailColorParts[num3] : this.DetailColorParts[count2 - 1]);
			}
			if (this.FlickerFrame > 0 || Stat.RandomCosmetic(1, 200) == 1)
			{
				E.Tile = null;
				if (this.FlickerFrame == 0)
				{
					E.RenderString = "_";
				}
				if (this.FlickerFrame == 1)
				{
					E.RenderString = "-";
				}
				if (this.FlickerFrame == 2)
				{
					E.RenderString = "|";
				}
				if (this.FlickerFrame == 0)
				{
					this.FlickerFrame = 3;
				}
				this.FlickerFrame--;
				text = "&Y";
			}
			else if (Stat.RandomCosmetic(1, 400) == 1)
			{
				text = "&Y";
			}
			if (!text.IsNullOrEmpty() || !text2.IsNullOrEmpty())
			{
				E.ApplyColors(text, text2, HologramMaterial.ICON_COLOR_PRIORITY, HologramMaterial.ICON_COLOR_PRIORITY);
			}
			if (!Options.DisableTextAnimationEffects)
			{
				this.FrameOffset += Stat.Random(0, 20);
			}
			return true;
		}

		// Token: 0x04004C7F RID: 19583
		public static readonly int ICON_COLOR_PRIORITY = 30;

		// Token: 0x04004C80 RID: 19584
		public string ColorStrings = "&C,&b,&c,&B";

		// Token: 0x04004C81 RID: 19585
		public string DetailColors = "c,C,b,b";

		// Token: 0x04004C82 RID: 19586
		[NonSerialized]
		public int FrameOffset;

		// Token: 0x04004C83 RID: 19587
		[NonSerialized]
		public int FlickerFrame;

		// Token: 0x04004C84 RID: 19588
		[NonSerialized]
		private List<string> ColorStringParts;

		// Token: 0x04004C85 RID: 19589
		[NonSerialized]
		private List<string> DetailColorParts;
	}
}
