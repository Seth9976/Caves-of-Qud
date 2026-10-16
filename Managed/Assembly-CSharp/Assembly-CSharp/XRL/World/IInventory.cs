using System;

namespace XRL.World
{
	// Token: 0x02000972 RID: 2418
	public interface IInventory
	{
		// Token: 0x0600640D RID: 25613
		GameObject AddObjectToInventory(GameObject Object, GameObject Actor = null, bool Silent = false, bool NoStack = false, bool FlushTransient = true, string Context = null, IEvent ParentEvent = null);

		// Token: 0x0600640E RID: 25614
		bool RemoveObjectFromInventory(GameObject Object, GameObject Actor = null, bool Silent = false, bool NoStack = false, bool FlushTransient = true, string Context = null, IEvent ParentEvent = null);

		// Token: 0x0600640F RID: 25615
		bool InventoryContains(GameObject Object);

		// Token: 0x06006410 RID: 25616
		Cell GetInventoryCell();

		// Token: 0x06006411 RID: 25617
		Zone GetInventoryZone();
	}
}
