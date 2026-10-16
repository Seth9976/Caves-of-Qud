using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x020012D4 RID: 4820
	[Serializable]
	public class ConcealedHologramMaterial : IPart
	{
		// Token: 0x0600BDE6 RID: 48614 RVA: 0x00459E9C File Offset: 0x0045809C
		public override void AddedAfterCreation()
		{
			base.AddedAfterCreation();
			this.ParentObject.MakeNonflammable();
		}

		// Token: 0x0600BDE7 RID: 48615 RVA: 0x00459EB0 File Offset: 0x004580B0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanBeDismemberedEvent>.ID || ID == PooledEvent<CanBeInvoluntarilyMovedEvent>.ID || ID == PooledEvent<GetElectricalConductivityEvent>.ID || ID == PooledEvent<GetMatterPhaseEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == PooledEvent<GetScanTypeEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == ObjectCreatedEvent.ID || ID == PooledEvent<RespiresEvent>.ID;
		}

		// Token: 0x0600BDE8 RID: 48616 RVA: 0x00459F11 File Offset: 0x00458111
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.ParentObject.MakeImperviousToHeat();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BDE9 RID: 48617 RVA: 0x00459F26 File Offset: 0x00458126
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 1 && E.Object == this.ParentObject)
			{
				E.Value = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BDEA RID: 48618 RVA: 0x00459F4F File Offset: 0x0045814F
		public override bool HandleEvent(CanBeDismemberedEvent E)
		{
			return E.Object != this.ParentObject && base.HandleEvent(E);
		}

		// Token: 0x0600BDEB RID: 48619 RVA: 0x00459F68 File Offset: 0x00458168
		public override bool HandleEvent(CanBeInvoluntarilyMovedEvent E)
		{
			return E.Object != this.ParentObject && base.HandleEvent(E);
		}

		// Token: 0x0600BDEC RID: 48620 RVA: 0x00459F81 File Offset: 0x00458181
		public override bool HandleEvent(GetMatterPhaseEvent E)
		{
			E.MinMatterPhase(4);
			return false;
		}

		// Token: 0x0600BDED RID: 48621 RVA: 0x00459F8B File Offset: 0x0045818B
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageReduction = 100;
			return false;
		}

		// Token: 0x0600BDEE RID: 48622 RVA: 0x00459F96 File Offset: 0x00458196
		public override bool HandleEvent(GetScanTypeEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.ScanType = Scanning.Scan.Tech;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BDEF RID: 48623 RVA: 0x00459FB8 File Offset: 0x004581B8
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.DescriptionPostfix)
			{
				E.Base.Compound(this.ParentObject.It + this.ParentObject.GetVerb("flicker", true, false, false, true, false) + " subtly.", ' ');
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BDF0 RID: 48624 RVA: 0x0045A00C File Offset: 0x0045820C
		public override bool HandleEvent(RespiresEvent E)
		{
			return false;
		}

		// Token: 0x0600BDF1 RID: 48625 RVA: 0x0045A010 File Offset: 0x00458210
		public override bool Render(RenderEvent E)
		{
			if (this.ParentObject.CurrentCell == null)
			{
				return true;
			}
			if (!this.ParentObject.CurrentCell.IsExplored())
			{
				return true;
			}
			if (!this.ParentObject.CurrentCell.IsVisible())
			{
				return true;
			}
			GameObject thePlayer = IComponent<GameObject>.ThePlayer;
			if (((thePlayer != null) ? thePlayer.CurrentCell : null) == null)
			{
				return true;
			}
			if (this.ParentObject.DistanceTo(IComponent<GameObject>.ThePlayer) > 1)
			{
				return true;
			}
			int num = (XRLCore.CurrentFrame + this.FrameOffset) % 200;
			string text = null;
			string text2 = null;
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
				if (num < 8)
				{
					text = "&C";
					text2 = "c";
				}
				else
				{
					text = "&Y";
					text2 = "y";
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
				text2 = "c";
			}
			else if (num < 8)
			{
				text = "&b";
				text2 = "C";
			}
			else if (num < 12)
			{
				text = "&c";
				text2 = "b";
			}
			if (this.FlickerFrame == 0 && Stat.RandomCosmetic(1, 400) == 1)
			{
				text = "&Y";
				text2 = "y";
			}
			if (!text.IsNullOrEmpty() || !text2.IsNullOrEmpty())
			{
				E.ApplyColors(text, text2, ConcealedHologramMaterial.ICON_COLOR_PRIORITY, ConcealedHologramMaterial.ICON_COLOR_PRIORITY);
			}
			if (!Options.DisableTextAnimationEffects)
			{
				this.FrameOffset += Stat.RandomCosmetic(0, 20);
			}
			return true;
		}

		// Token: 0x04004C7B RID: 19579
		public static readonly int ICON_COLOR_PRIORITY = 30;

		// Token: 0x04004C7C RID: 19580
		public int FlickerFrame;

		// Token: 0x04004C7D RID: 19581
		public int FrameOffset;

		// Token: 0x04004C7E RID: 19582
		public bool DescriptionPostfix = true;
	}
}
