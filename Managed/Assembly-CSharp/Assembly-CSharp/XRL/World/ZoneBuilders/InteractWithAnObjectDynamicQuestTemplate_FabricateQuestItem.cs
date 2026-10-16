using System;
using System.Collections.Generic;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A36 RID: 2614
	public class InteractWithAnObjectDynamicQuestTemplate_FabricateQuestItem : ZoneBuilderSandbox
	{
		// Token: 0x06006DAF RID: 28079 RVA: 0x0024EEA4 File Offset: 0x0024D0A4
		public InteractWithAnObjectDynamicQuestTemplate_FabricateQuestItem()
		{
		}

		// Token: 0x06006DB0 RID: 28080 RVA: 0x0024EEAC File Offset: 0x0024D0AC
		public InteractWithAnObjectDynamicQuestTemplate_FabricateQuestItem(string deliveryItemID)
		{
			this.deliveryItemID = deliveryItemID;
		}

		// Token: 0x06006DB1 RID: 28081 RVA: 0x0024EEBC File Offset: 0x0024D0BC
		public bool BuildZone(Zone zone)
		{
			GameObject cachedObjects = The.ZoneManager.GetCachedObjects(this.deliveryItemID);
			cachedObjects.SetIntProperty("norestock", 1, false);
			List<GameObject> objectsWithTagOrProperty = zone.GetObjectsWithTagOrProperty("LairOwner", false);
			if (objectsWithTagOrProperty.Count > 0 && cachedObjects.Physics.Owner == null)
			{
				GameObject randomElement = objectsWithTagOrProperty.GetRandomElement<GameObject>(null);
				if (randomElement.Brain != null)
				{
					cachedObjects.Physics.Owner = randomElement.Brain.GetPrimaryFaction(false);
				}
			}
			if (cachedObjects.Physics.Owner == null)
			{
				List<GameObject> objectsWithTagOrProperty2 = zone.GetObjectsWithTagOrProperty("Villager", false);
				if (objectsWithTagOrProperty2.Count > 0)
				{
					GameObject randomElement2 = objectsWithTagOrProperty2.GetRandomElement<GameObject>(null);
					if (randomElement2.Brain != null)
					{
						cachedObjects.Physics.Owner = randomElement2.Brain.GetPrimaryFaction(false);
					}
				}
			}
			if (cachedObjects.Physics.Owner == null)
			{
				List<GameObject> objectsWithPart = zone.GetObjectsWithPart("Brain");
				if (objectsWithPart.Count > 0)
				{
					GameObject randomElement3 = objectsWithPart.GetRandomElement<GameObject>(null);
					if (randomElement3.Brain != null)
					{
						cachedObjects.Physics.Owner = randomElement3.Brain.GetPrimaryFaction(false);
					}
				}
			}
			List<Cell> list = zone.GetEmptyCellsWithNoFurniture();
			if (list.Count == 0)
			{
				list = zone.GetEmptyCells(null, null);
			}
			if (list.Count == 0)
			{
				list = zone.GetEmptyReachableCells();
			}
			if (list.Count == 0)
			{
				list = zone.GetReachableCells();
			}
			if (list.Count == 0)
			{
				list = zone.GetCells();
			}
			if (list.Count > 0)
			{
				Cell cell = list.ShuffleInPlace<Cell>(null)[0];
				cell.AddObject(cachedObjects, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				base.EnsureCellReachable(zone, cell);
			}
			return true;
		}

		// Token: 0x04003228 RID: 12840
		public string deliveryItemID;
	}
}
