using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x020012DA RID: 4826
	[Serializable]
	public class HologramMaterialPrimary : IPoweredPart
	{
		// Token: 0x0600BE07 RID: 48647 RVA: 0x0045A5D4 File Offset: 0x004587D4
		public HologramMaterialPrimary()
		{
			this.ChargeUse = 1;
			this.IsBootSensitive = false;
			this.IsEMPSensitive = true;
			this.MustBeUnderstood = false;
			this.WorksOnWearer = true;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600BE08 RID: 48648 RVA: 0x0045A611 File Offset: 0x00458811
		public override void AddedAfterCreation()
		{
			base.AddedAfterCreation();
			this.ParentObject.MakeNonflammable();
		}

		// Token: 0x0600BE09 RID: 48649 RVA: 0x0045A628 File Offset: 0x00458828
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanBeDismemberedEvent>.ID || ID == PooledEvent<CanBeInvoluntarilyMovedEvent>.ID || ID == PooledEvent<GetElectricalConductivityEvent>.ID || ID == PooledEvent<GetMatterPhaseEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == PooledEvent<GetScanTypeEvent>.ID || ID == ObjectCreatedEvent.ID || ID == PooledEvent<RespiresEvent>.ID;
		}

		// Token: 0x0600BE0A RID: 48650 RVA: 0x0045A681 File Offset: 0x00458881
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.ParentObject.MakeImperviousToHeat();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BE0B RID: 48651 RVA: 0x0045A696 File Offset: 0x00458896
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 1 && E.Object == this.ParentObject)
			{
				E.Value = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BE0C RID: 48652 RVA: 0x0045A6BF File Offset: 0x004588BF
		public override bool HandleEvent(CanBeDismemberedEvent E)
		{
			return E.Object != this.ParentObject && base.HandleEvent(E);
		}

		// Token: 0x0600BE0D RID: 48653 RVA: 0x0045A6D8 File Offset: 0x004588D8
		public override bool HandleEvent(CanBeInvoluntarilyMovedEvent E)
		{
			return E.Object != this.ParentObject && base.HandleEvent(E);
		}

		// Token: 0x0600BE0E RID: 48654 RVA: 0x0045A6F1 File Offset: 0x004588F1
		public override bool HandleEvent(GetMatterPhaseEvent E)
		{
			E.MinMatterPhase(4);
			return false;
		}

		// Token: 0x0600BE0F RID: 48655 RVA: 0x0045A6FB File Offset: 0x004588FB
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageReduction = 100;
			return false;
		}

		// Token: 0x0600BE10 RID: 48656 RVA: 0x0045A706 File Offset: 0x00458906
		public override bool HandleEvent(GetScanTypeEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.ScanType = Scanning.Scan.Tech;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BE11 RID: 48657 RVA: 0x0045A726 File Offset: 0x00458926
		public override bool HandleEvent(RespiresEvent E)
		{
			return false;
		}

		// Token: 0x0600BE12 RID: 48658 RVA: 0x0045A729 File Offset: 0x00458929
		public override void Initialize()
		{
			base.Initialize();
			this.Tile = this.ParentObject.Render.Tile;
			this.RenderString = this.ParentObject.Render.RenderString;
		}

		// Token: 0x0600BE13 RID: 48659 RVA: 0x0045A760 File Offset: 0x00458960
		public override bool Render(RenderEvent E)
		{
			string text;
			if (base.WasReady())
			{
				int num = (XRLCore.CurrentFrame + this.FrameOffset) % 200;
				if (this.FlickerFrame > 0 || Stat.RandomCosmetic(1, 200) == 1)
				{
					E.Tile = null;
					if (this.FlickerFrame == 0)
					{
						E.RenderString = "_";
					}
					else if (this.FlickerFrame == 1)
					{
						E.RenderString = "-";
					}
					else if (this.FlickerFrame == 2)
					{
						E.RenderString = "|";
					}
					if (num < 8)
					{
					}
					if (this.FlickerFrame == 0)
					{
						this.FlickerFrame = 3;
					}
					this.FlickerFrame--;
				}
				if (num < 4)
				{
					text = "&C";
				}
				else if (num < 8)
				{
					text = "&b";
				}
				else if (num < 12)
				{
					text = "&c";
				}
				else
				{
					text = "&B";
				}
				if (!Options.DisableTextAnimationEffects)
				{
					this.FrameOffset += Stat.Random(0, 20);
				}
				if (this.FlickerFrame == 0 && Stat.RandomCosmetic(1, 400) == 1)
				{
					text = "&Y";
				}
			}
			else
			{
				text = "&K";
			}
			if (!text.IsNullOrEmpty())
			{
				E.ApplyColors(text, HologramMaterialPrimary.ICON_COLOR_PRIORITY);
			}
			return base.Render(E);
		}

		// Token: 0x04004C86 RID: 19590
		public static readonly int ICON_COLOR_PRIORITY;

		// Token: 0x04004C87 RID: 19591
		public string Tile;

		// Token: 0x04004C88 RID: 19592
		public string RenderString = "@";

		// Token: 0x04004C89 RID: 19593
		public int FlickerFrame;

		// Token: 0x04004C8A RID: 19594
		public int FrameOffset;
	}
}
