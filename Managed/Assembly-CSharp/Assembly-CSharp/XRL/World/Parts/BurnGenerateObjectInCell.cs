using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x020011E7 RID: 4583
	[Serializable]
	public class BurnGenerateObjectInCell : IPart
	{
		// Token: 0x0600B68A RID: 46730 RVA: 0x004298F6 File Offset: 0x00427AF6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDeathRemovalEvent.ID;
		}

		// Token: 0x0600B68B RID: 46731 RVA: 0x0042990C File Offset: 0x00427B0C
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (!this.Table.IsNullOrEmpty() && this.ParentObject.Physics != null && (this.ParentObject.Physics.LastDamagedByType == "Fire" || this.ParentObject.Physics.LastDamagedByType == "Light") && this.ParentObject.GetIntProperty("SuppressCorpseDrops", 0) <= 0 && (this.EvenIfDroppedByPlayer || this.ParentObject.GetIntProperty("DroppedByPlayer", 0) == 0) && this.Chance.in100())
			{
				IInventory dropInventory = this.ParentObject.GetDropInventory();
				if (dropInventory != null)
				{
					string blueprint = this.GetBlueprint();
					if (blueprint != null)
					{
						GameObject gameObject = GameObject.Create(blueprint);
						this.DoCarryOvers(this.ParentObject, gameObject);
						dropInventory.AddObjectToInventory(gameObject, null, false, false, true, null, E);
						DroppedEvent.Send(this.ParentObject, gameObject, false);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B68C RID: 46732 RVA: 0x004299FF File Offset: 0x00427BFF
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B68D RID: 46733 RVA: 0x00429A02 File Offset: 0x00427C02
		private void DoCarryOvers(GameObject From, GameObject To)
		{
			if (From.HasProperty("StoredByPlayer") || From.HasProperty("FromStoredByPlayer"))
			{
				To.SetIntProperty("FromStoredByPlayer", 1, false);
			}
			Temporary.CarryOver(From, To, false);
			Phase.carryOver(From, To);
		}

		// Token: 0x0600B68E RID: 46734 RVA: 0x00429A3C File Offset: 0x00427C3C
		public string GetBlueprint()
		{
			Zone zone = (this.PerZone ? this.ParentObject.CurrentZone : null);
			string text = (this.PerZone ? this.PerZoneResultKey() : null);
			string text2 = ((zone != null) ? zone.GetZoneProperty(text, null) : null);
			if (text2 == null)
			{
				PopulationResult populationResult = PopulationManager.RollOneFrom(this.Table, null, null);
				if (populationResult != null)
				{
					text2 = populationResult.Blueprint;
					if (zone != null)
					{
						zone.SetZoneProperty(text, text2);
					}
				}
			}
			return text2;
		}

		// Token: 0x0600B68F RID: 46735 RVA: 0x00429AA8 File Offset: 0x00427CA8
		private string PerZoneResultKey()
		{
			return "BurnGenerateObjectInCell_" + this.Table;
		}

		// Token: 0x040049A2 RID: 18850
		public int Chance;

		// Token: 0x040049A3 RID: 18851
		public string Table;

		// Token: 0x040049A4 RID: 18852
		public bool EvenIfDroppedByPlayer;

		// Token: 0x040049A5 RID: 18853
		public bool PerZone;
	}
}
