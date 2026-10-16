using System;

namespace XRL.World.Parts
{
	// Token: 0x02001048 RID: 4168
	[Serializable]
	public class Hangable : IPart
	{
		// Token: 0x0600A5E2 RID: 42466 RVA: 0x003CB1F8 File Offset: 0x003C93F8
		public override bool SameAs(IPart p)
		{
			Hangable hangable = p as Hangable;
			return hangable.SameCellSupportSufficient == this.SameCellSupportSufficient && hangable.SupportPointsRequired == this.SupportPointsRequired && !(hangable.HangingTile != this.HangingTile);
		}

		// Token: 0x0600A5E3 RID: 42467 RVA: 0x003CB242 File Offset: 0x003C9442
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanFallEvent>.ID || ID == EnteredCellEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == LeftCellEvent.ID || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x0600A5E4 RID: 42468 RVA: 0x003CB278 File Offset: 0x003C9478
		public override bool HandleEvent(CanFallEvent E)
		{
			if (this.Hanging)
			{
				E.CanFall = false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5E5 RID: 42469 RVA: 0x003CB290 File Offset: 0x003C9490
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			this.CheckHanging();
			if (this.Hanging)
			{
				E.AddTag("{{y|[hanging]}}", 0);
			}
			else if (this.ParentObject.CurrentCell != null)
			{
				E.AddTag("{{y|[on ground]}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5E6 RID: 42470 RVA: 0x003CB2CE File Offset: 0x003C94CE
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.CheckHanging();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5E7 RID: 42471 RVA: 0x003CB2DD File Offset: 0x003C94DD
		public override bool HandleEvent(LeftCellEvent E)
		{
			this.Hanging = false;
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5E8 RID: 42472 RVA: 0x003CB2ED File Offset: 0x003C94ED
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.CheckHanging();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5E9 RID: 42473 RVA: 0x003CB2FC File Offset: 0x003C94FC
		public bool HangingSupportAvailable()
		{
			if (this.ParentObject.Physics == null)
			{
				return false;
			}
			Cell currentCell = this.ParentObject.Physics.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			int num = 0;
			if (currentCell.HasObjectWithPropertyOrTag("HangingSupport"))
			{
				if (this.SameCellSupportSufficient || this.SupportPointsRequired <= 1)
				{
					return true;
				}
				int num3 = num;
				num = num3 + 1;
			}
			currentCell.ForeachCardinalAdjacentLocalCell(delegate(Cell OC)
			{
				if (OC.HasWall() || OC.HasObjectWithPropertyOrTag("HangingSupport"))
				{
					int num2 = num;
					num = num2 + 1;
					if (num >= this.SupportPointsRequired)
					{
						return false;
					}
				}
				return true;
			});
			return num >= this.SupportPointsRequired;
		}

		// Token: 0x0600A5EA RID: 42474 RVA: 0x003CB398 File Offset: 0x003C9598
		public void CheckHanging()
		{
			bool hanging = this.Hanging;
			this.Hanging = this.HangingSupportAvailable();
			if (this.Hanging && !hanging)
			{
				this.ParentObject.FireEvent(Hangable.eStartHanging);
				return;
			}
			if (hanging && !this.Hanging)
			{
				this.ParentObject.FireEvent(Hangable.eStopHanging);
			}
		}

		// Token: 0x0600A5EB RID: 42475 RVA: 0x003CB3F1 File Offset: 0x003C95F1
		public override bool Render(RenderEvent E)
		{
			if (this.Hanging && !string.IsNullOrEmpty(this.HangingTile))
			{
				E.Tile = this.HangingTile;
			}
			return base.Render(E);
		}

		// Token: 0x0400447D RID: 17533
		public bool SameCellSupportSufficient = true;

		// Token: 0x0400447E RID: 17534
		public int SupportPointsRequired = 2;

		// Token: 0x0400447F RID: 17535
		public string HangingTile;

		// Token: 0x04004480 RID: 17536
		public bool Hanging;

		// Token: 0x04004481 RID: 17537
		[NonSerialized]
		private static Event eStartHanging = new ImmutableEvent("StartHanging");

		// Token: 0x04004482 RID: 17538
		[NonSerialized]
		private static Event eStopHanging = new ImmutableEvent("StopHanging");
	}
}
