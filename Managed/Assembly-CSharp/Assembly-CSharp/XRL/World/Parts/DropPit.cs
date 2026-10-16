using System;
using XRL.Names;

namespace XRL.World.Parts
{
	// Token: 0x02000EA5 RID: 3749
	[Serializable]
	public class DropPit : IPart
	{
		// Token: 0x06009690 RID: 38544 RVA: 0x00368F93 File Offset: 0x00367193
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009691 RID: 38545 RVA: 0x00368F96 File Offset: 0x00367196
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDeathRemovalEvent.ID;
		}

		// Token: 0x06009692 RID: 38546 RVA: 0x00368FAC File Offset: 0x003671AC
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (this.ParentObject.GetIntProperty("SuppressCorpseDrops", 0) <= 0)
			{
				this.ProcessPitDrop(E.ThirdPersonReason);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009693 RID: 38547 RVA: 0x00368FD5 File Offset: 0x003671D5
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009694 RID: 38548 RVA: 0x00368FD8 File Offset: 0x003671D8
		private void ProcessPitDrop(string DeathReason = null)
		{
			IInventory dropInventory = this.ParentObject.GetDropInventory();
			if (dropInventory == null)
			{
				return;
			}
			Zone inventoryZone = dropInventory.GetInventoryZone();
			if (inventoryZone == null || !inventoryZone.Built)
			{
				return;
			}
			Cell inventoryCell = dropInventory.GetInventoryCell();
			GameObject gameObject = GameObject.Create("Pit");
			if (gameObject == null)
			{
				return;
			}
			if (this.ParentObject.HasProperName)
			{
				gameObject.SetStringProperty("CreatureName", this.ParentObject.BaseDisplayName, false);
			}
			else
			{
				string text = NameMaker.MakeName(this.ParentObject, null, null, null, null, null, null, null, null, null, null, null, null, true, false, null, null);
				if (text != null)
				{
					gameObject.SetStringProperty("CreatureName", text, false);
				}
			}
			if (!DeathReason.IsNullOrEmpty())
			{
				gameObject.SetStringProperty("DeathReason", DeathReason, false);
			}
			if (this.ParentObject.HasProperty("StoredByPlayer") || this.ParentObject.HasProperty("FromStoredByPlayer"))
			{
				gameObject.SetIntProperty("FromStoredByPlayer", 1, false);
			}
			inventoryCell.RemoveObject(this.ParentObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
			inventoryCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			inventoryCell.PaintWallsAround();
			inventoryZone.FireEvent("FirmPitEdges");
			inventoryZone.FireEvent("PaintPitEdges");
			ZoneManager.PaintWalls(inventoryZone, 0, 0, -1, -1);
		}
	}
}
