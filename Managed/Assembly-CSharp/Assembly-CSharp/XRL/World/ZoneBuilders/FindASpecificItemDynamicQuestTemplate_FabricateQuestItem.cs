using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A33 RID: 2611
	public class FindASpecificItemDynamicQuestTemplate_FabricateQuestItem : ZoneBuilderSandbox
	{
		// Token: 0x06006DA2 RID: 28066 RVA: 0x0024E364 File Offset: 0x0024C564
		public FindASpecificItemDynamicQuestTemplate_FabricateQuestItem()
		{
		}

		// Token: 0x06006DA3 RID: 28067 RVA: 0x0024E36C File Offset: 0x0024C56C
		public FindASpecificItemDynamicQuestTemplate_FabricateQuestItem(string deliveryItemID)
			: this()
		{
			this.deliveryItemID = deliveryItemID;
		}

		// Token: 0x06006DA4 RID: 28068 RVA: 0x0024E37C File Offset: 0x0024C57C
		public bool BuildZone(Zone zone)
		{
			GameObject cachedObjects = The.ZoneManager.GetCachedObjects(this.deliveryItemID);
			cachedObjects.SetIntProperty("norestock", 1, false);
			List<GameObject> objectsWithTagOrProperty = zone.GetObjectsWithTagOrProperty("LairOwner", false);
			if (objectsWithTagOrProperty.Count > 0)
			{
				GameObject randomElement = objectsWithTagOrProperty.GetRandomElement<GameObject>(null);
				randomElement.RequirePart<Inventory>(false).AddObject(cachedObjects.DeepCopy(false, true, null), null, false, false, true, null);
				randomElement.Brain.PerformEquip(false, true, null);
				return true;
			}
			List<GameObject> objectsWithTagOrProperty2 = zone.GetObjectsWithTagOrProperty("NamedVillager", false);
			if (objectsWithTagOrProperty2.Count > 0)
			{
				GameObject randomElement2 = objectsWithTagOrProperty2.GetRandomElement<GameObject>(null);
				randomElement2.RequirePart<Inventory>(false).AddObject(cachedObjects.DeepCopy(false, true, null), null, false, false, true, null);
				randomElement2.Brain.PerformEquip(false, true, null);
				return true;
			}
			GameObject gameObject = zone.GetObjectWithTag("RelicContainer") ?? GameObject.Create("RelicChest");
			gameObject.Inventory.AddObject(cachedObjects.DeepCopy(false, true, null), null, false, false, true, null);
			gameObject.SetImportant(true, false, false);
			if (gameObject.CurrentCell == null || gameObject.CurrentCell.IsSolid())
			{
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
				Cell randomElement3 = list.GetRandomElement<Cell>(null);
				if (randomElement3 != null)
				{
					randomElement3.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					base.EnsureCellReachable(zone, randomElement3);
				}
			}
			return true;
		}

		// Token: 0x0400321D RID: 12829
		public string deliveryItemID;
	}
}
