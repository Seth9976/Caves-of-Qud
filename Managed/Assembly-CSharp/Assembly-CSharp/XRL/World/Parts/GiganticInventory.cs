using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FCE RID: 4046
	[Serializable]
	public class GiganticInventory : IPart
	{
		// Token: 0x0600A0DF RID: 41183 RVA: 0x003A8708 File Offset: 0x003A6908
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<StockedEvent>.ID || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600A0E0 RID: 41184 RVA: 0x003A8726 File Offset: 0x003A6926
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!this.Applied)
			{
				this.Apply();
				if (!this.ParentObject.HasPart(typeof(GenericInventoryRestocker)))
				{
					this.ParentObject.RemovePart(this);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0E1 RID: 41185 RVA: 0x003A8760 File Offset: 0x003A6960
		public override bool HandleEvent(StockedEvent E)
		{
			this.Apply();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0E2 RID: 41186 RVA: 0x003A8770 File Offset: 0x003A6970
		public void Apply()
		{
			this.Applied = true;
			foreach (GameObject gameObject in this.ParentObject.GetInventory())
			{
				if (!gameObject.HasPart<ModGigantic>() && this.Chance.in100() && !(gameObject.GetTag("Mods", null) == "None"))
				{
					gameObject.IsGiganticEquipment = true;
					if (this.WaresChance.in100())
					{
						gameObject.SetIntProperty("_stock", 1, false);
					}
				}
			}
		}

		// Token: 0x040042C1 RID: 17089
		public int Chance = 100;

		// Token: 0x040042C2 RID: 17090
		public int WaresChance = 100;

		// Token: 0x040042C3 RID: 17091
		public bool Applied;
	}
}
