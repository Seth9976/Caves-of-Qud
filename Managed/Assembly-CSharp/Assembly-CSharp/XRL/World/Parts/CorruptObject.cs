using System;
using System.Linq;
using XRL.Collections;
using XRL.Wish;

namespace XRL.World.Parts
{
	// Token: 0x02000E45 RID: 3653
	[HasWishCommand]
	[Serializable]
	public class CorruptObject : IPart
	{
		// Token: 0x06009419 RID: 37913 RVA: 0x003551EE File Offset: 0x003533EE
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600941A RID: 37914 RVA: 0x003551F1 File Offset: 0x003533F1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600941B RID: 37915 RVA: 0x00355210 File Offset: 0x00353410
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Destroy", "{{R|destroy}}", "Destroy", null, 'd', false, 1000, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600941C RID: 37916 RVA: 0x0035524B File Offset: 0x0035344B
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Destroy")
			{
				this.ParentObject.Obliterate(null, true, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600941D RID: 37917 RVA: 0x00355278 File Offset: 0x00353478
		[WishCommand("clearcorrupt", null)]
		public static void ClearAll()
		{
			foreach (Zone zone in The.ZoneManager.CachedZones.Values)
			{
				for (int i = 0; i < zone.Height; i++)
				{
					for (int j = 0; j < zone.Width; j++)
					{
						Cell cell = zone.Map[j][i];
						cell.ClearObjectsWithPart("CorruptObject", true, true);
						int k = 0;
						int count = cell.Objects.Count;
						while (k < count)
						{
							using (ScopeDisposedList<GameObject> fromPool = ScopeDisposedList<GameObject>.GetFromPool())
							{
								cell.Objects[k].GetContents(fromPool);
								int l = 0;
								int count2 = fromPool.Count;
								while (l < count2)
								{
									if (fromPool[l].HasPart("CorruptObject"))
									{
										fromPool[l].RemoveFromContext(null);
									}
									l++;
								}
							}
							k++;
						}
					}
				}
			}
			string[] array = The.ZoneManager.CachedObjects.Keys.ToArray<string>();
			for (int m = 0; m < array.Length; m++)
			{
				GameObject gameObject;
				if (The.ZoneManager.CachedObjects.TryGetValue(array[m], out gameObject) && gameObject.HasPart("CorruptObject"))
				{
					The.ZoneManager.CachedObjects.Remove(array[m]);
				}
			}
		}
	}
}
