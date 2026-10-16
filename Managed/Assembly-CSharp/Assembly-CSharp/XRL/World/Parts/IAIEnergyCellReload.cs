using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000E39 RID: 3641
	[Serializable]
	public abstract class IAIEnergyCellReload : IPart
	{
		// Token: 0x060093C8 RID: 37832 RVA: 0x00353500 File Offset: 0x00351700
		public void CheckEnergyCellReload(IAICommandListEvent E, int ToleranceFactor = 2, int RequirePercent = 110)
		{
			if (this.ParentObject.Equipped != E.Actor)
			{
				return;
			}
			Inventory inventory = E.Actor.Inventory;
			if (inventory == null)
			{
				return;
			}
			if (E.Actor.Stat("Intelligence", 0) < 7)
			{
				return;
			}
			EnergyCellSocket part = this.ParentObject.GetPart<EnergyCellSocket>();
			if (part == null)
			{
				return;
			}
			int @for = QueryDrawEvent.GetFor(this.ParentObject);
			if (@for <= 0)
			{
				return;
			}
			int num = this.ParentObject.QueryCharge(false, 0L, true, true);
			if (num >= @for * ToleranceFactor)
			{
				return;
			}
			List<GameObject> list = Event.NewGameObjectList();
			inventory.GetObjects(list, new Predicate<GameObject>(part.CompatibleCell));
			if (list.Count == 0)
			{
				return;
			}
			int num2 = num * RequirePercent / 100;
			if (RequirePercent > 100)
			{
				if (num2 <= num)
				{
					num2 = num + 1;
				}
			}
			else if (RequirePercent < 100 && num2 >= num)
			{
				num2 = num - 1;
			}
			GameObject gameObject = null;
			int num3 = 0;
			foreach (GameObject gameObject2 in list)
			{
				int num4 = gameObject2.QueryCharge(false, 0L, true, true);
				if (num4 >= num2 && (gameObject == null || num4 > num3))
				{
					gameObject = gameObject2;
					num3 = num4;
				}
			}
			if (gameObject == null)
			{
				return;
			}
			E.Add(EnergyCellSocket.REPLACE_CELL_INTERACTION, 1, this.ParentObject, true, false, gameObject, null);
		}
	}
}
