using System;

namespace XRL.World.Parts
{
	// Token: 0x02001267 RID: 4711
	[Serializable]
	public class DoubleContainer : IPart
	{
		// Token: 0x0600BAC2 RID: 47810 RVA: 0x00442C11 File Offset: 0x00440E11
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == LeftCellEvent.ID || ID == EnteredCellEvent.ID || ID == GetInventoryActionsEvent.ID || ID == BeforeDeathRemovalEvent.ID || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x0600BAC3 RID: 47811 RVA: 0x00442C47 File Offset: 0x00440E47
		public override bool HandleEvent(LeftCellEvent E)
		{
			this.Suspended = this.GetSibling(E.Cell);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BAC4 RID: 47812 RVA: 0x00442C64 File Offset: 0x00440E64
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (GameObject.Validate(ref this.Suspended))
			{
				GameObject suspended = this.Suspended;
				Cell currentCell = suspended.CurrentCell;
				Cell cellFromDirection = E.Cell.GetCellFromDirection(this.Direction, false);
				if (cellFromDirection.IsPassable(suspended, false))
				{
					if (currentCell != null)
					{
						currentCell.RemoveObject(suspended, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
					}
					cellFromDirection.AddObject(suspended, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				else
				{
					this.ParentObject.Die(E.Dragging, null, "You broke apart.", null, false, null, null, false, false, null, null, null);
					suspended.Destroy("You broke apart.", false, false, null);
				}
				this.Suspended = null;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BAC5 RID: 47813 RVA: 0x00442D1C File Offset: 0x00440F1C
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			GameObject sibling = this.GetSibling(null);
			if (sibling != null)
			{
				sibling.Destroy("You broke apart.", false, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BAC6 RID: 47814 RVA: 0x00442D40 File Offset: 0x00440F40
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Open", "open", "Open", null, 'o', false, 0, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BAC7 RID: 47815 RVA: 0x00442D77 File Offset: 0x00440F77
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			if (this.Master)
			{
				this.SyncContentsToSibling();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BAC8 RID: 47816 RVA: 0x00442D8E File Offset: 0x00440F8E
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BAC9 RID: 47817 RVA: 0x00442D91 File Offset: 0x00440F91
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EncumbranceChanged");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BACA RID: 47818 RVA: 0x00442DA6 File Offset: 0x00440FA6
		public GameObject GetSibling(Cell C = null)
		{
			Cell cell = C ?? this.ParentObject.CurrentCell;
			if (cell == null)
			{
				return null;
			}
			return cell.GetCellFromDirection(this.Direction, true).GetFirstObjectWithPart("DoubleContainer");
		}

		// Token: 0x0600BACB RID: 47819 RVA: 0x00442DD4 File Offset: 0x00440FD4
		public void SyncContentsToSibling()
		{
			GameObject sibling = this.GetSibling(null);
			if (sibling != null)
			{
				sibling.Inventory.Objects.Clear();
				sibling.Inventory.Objects.AddRange(this.ParentObject.Inventory.Objects);
			}
		}

		// Token: 0x0600BACC RID: 47820 RVA: 0x00442E1C File Offset: 0x0044101C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EncumbranceChanged")
			{
				this.SyncContentsToSibling();
				return true;
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004AEB RID: 19179
		public string Direction;

		// Token: 0x04004AEC RID: 19180
		public bool Master;

		// Token: 0x04004AED RID: 19181
		[NonSerialized]
		public GameObject Suspended;
	}
}
