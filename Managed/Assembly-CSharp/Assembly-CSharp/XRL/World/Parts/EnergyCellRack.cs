using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x020011C3 RID: 4547
	[Serializable]
	public class EnergyCellRack : IPoweredPart
	{
		// Token: 0x0600B4AB RID: 46251 RVA: 0x0041E1D3 File Offset: 0x0041C3D3
		public EnergyCellRack()
		{
			this.ChargeUse = 0;
			this.IsBootSensitive = false;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B4AC RID: 46252 RVA: 0x0041E204 File Offset: 0x0041C404
		public override bool SameAs(IPart p)
		{
			EnergyCellRack energyCellRack = p as EnergyCellRack;
			return !(energyCellRack.SlotType != this.SlotType) && energyCellRack.PreventOther == this.PreventOther && base.SameAs(p);
		}

		// Token: 0x0600B4AD RID: 46253 RVA: 0x0041E244 File Offset: 0x0041C444
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == PooledEvent<CanAcceptObjectEvent>.ID && this.PreventOther) || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<GetContentsEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetExtrinsicValueEvent.ID || ID == GetExtrinsicWeightEvent.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == QueryChargeEvent.ID || ID == TestChargeEvent.ID || ID == UseChargeEvent.ID;
		}

		// Token: 0x0600B4AE RID: 46254 RVA: 0x0041E2C0 File Offset: 0x0041C4C0
		public override bool HandleEvent(QueryChargeEvent E)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			int num = 1;
			long gridMask = E.GridMask;
			if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num, null, false, gridMask, null))
			{
				Inventory inventory = this.ParentObject.Inventory;
				if (((inventory != null) ? inventory.Objects : null) != null)
				{
					int i = 0;
					int count = inventory.Objects.Count;
					while (i < count)
					{
						GameObject gameObject = inventory.Objects[i];
						int j = 0;
						int count2 = gameObject.PartsList.Count;
						while (j < count2)
						{
							IEnergyCell energyCell = gameObject.PartsList[j] as IEnergyCell;
							if (energyCell != null && energyCell.SlotType == this.SlotType)
							{
								GameObject gameObject2 = gameObject;
								bool flag11 = false;
								bool includeTransient = E.IncludeTransient;
								bool includeBiological = E.IncludeBiological;
								int num2 = gameObject2.QueryCharge(flag11, E.GridMask, includeTransient, includeBiological);
								if (num2 > 0)
								{
									E.Amount += num2;
									break;
								}
								break;
							}
							else
							{
								j++;
							}
						}
						i++;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4AF RID: 46255 RVA: 0x0041E3D8 File Offset: 0x0041C5D8
		public override bool HandleEvent(TestChargeEvent E)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			int num = 1;
			long gridMask = E.GridMask;
			if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num, null, false, gridMask, null))
			{
				Inventory inventory = this.ParentObject.Inventory;
				if (((inventory != null) ? inventory.Objects : null) != null)
				{
					int i = 0;
					int count = inventory.Objects.Count;
					while (i < count)
					{
						GameObject gameObject = inventory.Objects[i];
						int j = 0;
						int count2 = gameObject.PartsList.Count;
						while (j < count2)
						{
							IEnergyCell energyCell = gameObject.PartsList[j] as IEnergyCell;
							if (energyCell != null && energyCell.SlotType == this.SlotType)
							{
								int amount = E.Amount;
								GameObject gameObject2 = gameObject;
								bool flag11 = false;
								bool includeTransient = E.IncludeTransient;
								bool includeBiological = E.IncludeBiological;
								int num2 = Math.Min(amount, gameObject2.QueryCharge(flag11, E.GridMask, includeTransient, includeBiological) - this.ChargeUse * E.Multiple);
								if (num2 <= 0)
								{
									break;
								}
								E.Amount -= num2;
								if (E.Amount <= 0)
								{
									return false;
								}
								break;
							}
							else
							{
								j++;
							}
						}
						i++;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4B0 RID: 46256 RVA: 0x0041E518 File Offset: 0x0041C718
		public override bool HandleEvent(UseChargeEvent E)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			bool flag10 = false;
			int num = 1;
			long gridMask = E.GridMask;
			if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num, null, false, gridMask, null))
			{
				Inventory inventory = this.ParentObject.Inventory;
				if (((inventory != null) ? inventory.Objects : null) != null)
				{
					int i = 0;
					int count = inventory.Objects.Count;
					while (i < count)
					{
						GameObject gameObject = inventory.Objects[i];
						int j = 0;
						int count2 = gameObject.PartsList.Count;
						while (j < count2)
						{
							IEnergyCell energyCell = gameObject.PartsList[j] as IEnergyCell;
							if (energyCell != null && energyCell.SlotType == this.SlotType)
							{
								int num2 = this.ChargeUse * E.Multiple;
								int amount = E.Amount;
								GameObject gameObject2 = gameObject;
								bool flag11 = false;
								bool includeTransient = E.IncludeTransient;
								bool includeBiological = E.IncludeBiological;
								int num3 = Math.Min(amount, gameObject2.QueryCharge(flag11, E.GridMask, includeTransient, includeBiological) - num2);
								if (num3 <= 0)
								{
									break;
								}
								gameObject.UseCharge(num3 + num2, false, E.GridMask, true, true, null);
								E.Amount -= num3;
								if (E.Amount <= 0)
								{
									return false;
								}
								break;
							}
							else
							{
								j++;
							}
						}
						i++;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4B1 RID: 46257 RVA: 0x0041E67C File Offset: 0x0041C87C
		public override bool HandleEvent(CanAcceptObjectEvent E)
		{
			if (this.PreventOther)
			{
				bool flag = false;
				int i = 0;
				int count = E.Object.PartsList.Count;
				while (i < count)
				{
					IEnergyCell energyCell = E.Object.PartsList[i] as IEnergyCell;
					if (energyCell != null && energyCell.SlotType == this.SlotType)
					{
						flag = true;
						break;
					}
					i++;
				}
				if (!flag)
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4B2 RID: 46258 RVA: 0x0041E6F0 File Offset: 0x0041C8F0
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Context != "Tinkering" && E.Understood())
			{
				int cellCount = this.GetCellCount();
				if (cellCount == 0)
				{
					E.AddTag("{{y|[{{K|no cells}}]}}", -5);
				}
				else if (cellCount == 1)
				{
					E.AddTag("{{y|[1 cell]}}", -5);
				}
				else
				{
					E.AddTag("{{y|[" + cellCount.ToString() + " cells]}}", 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B4B3 RID: 46259 RVA: 0x0041E767 File Offset: 0x0041C967
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B4B4 RID: 46260 RVA: 0x0041E76C File Offset: 0x0041C96C
		public List<GameObject> GetCells()
		{
			List<GameObject> list = Event.NewGameObjectList();
			Inventory inventory = this.ParentObject.Inventory;
			if (((inventory != null) ? inventory.Objects : null) != null)
			{
				int i = 0;
				int count = inventory.Objects.Count;
				while (i < count)
				{
					GameObject gameObject = inventory.Objects[i];
					int j = 0;
					int count2 = gameObject.PartsList.Count;
					while (j < count2)
					{
						IEnergyCell energyCell = gameObject.PartsList[j] as IEnergyCell;
						if (energyCell != null && energyCell.SlotType == this.SlotType)
						{
							list.Add(gameObject);
						}
						j++;
					}
					i++;
				}
			}
			return list;
		}

		// Token: 0x0600B4B5 RID: 46261 RVA: 0x0041E814 File Offset: 0x0041CA14
		public int GetCellCount()
		{
			int num = 0;
			Inventory inventory = this.ParentObject.Inventory;
			if (((inventory != null) ? inventory.Objects : null) != null)
			{
				int i = 0;
				int count = inventory.Objects.Count;
				while (i < count)
				{
					GameObject gameObject = inventory.Objects[i];
					int j = 0;
					int count2 = gameObject.PartsList.Count;
					while (j < count2)
					{
						IEnergyCell energyCell = gameObject.PartsList[j] as IEnergyCell;
						if (energyCell != null && energyCell.SlotType == this.SlotType)
						{
							num += gameObject.Count;
						}
						j++;
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x040048D4 RID: 18644
		public string SlotType = "EnergyCell";

		// Token: 0x040048D5 RID: 18645
		public bool PreventOther = true;
	}
}
