using System;
using XRL.Rules;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.World.PartBuilders
{
	// Token: 0x020009C9 RID: 2505
	public class ChestBuilders
	{
		// Token: 0x06006B89 RID: 27529 RVA: 0x0022B8A8 File Offset: 0x00229AA8
		public static void BuildCommonChestInventory(Inventory pInventory, int tier, string Context = null)
		{
			if (5.in100())
			{
				ChestBuilders.BuildSpecialChestInventory(pInventory, tier, Context);
				return;
			}
			int num = Stat.Random(2, 4);
			bool flag = false;
			for (int i = 0; i < num; i++)
			{
				try
				{
					GameObject gameObject = PopulationManager.CreateOneFrom("Junk " + Tier.Fuzz(tier, 5).ToString(), null, 0, 0, null, Context, null, null, null);
					if (gameObject != null)
					{
						pInventory.AddObject(gameObject, null, false, false, true, null);
					}
				}
				catch (Exception ex)
				{
					if (!flag)
					{
						MetricsManager.LogError("exception in BuildCommonChestInventory()", ex);
						flag = true;
					}
				}
			}
		}

		// Token: 0x06006B8A RID: 27530 RVA: 0x0022B940 File Offset: 0x00229B40
		public static void BuildTableChestInventory(Inventory pInventory, string Table, string Amount, int tier, string Context = null)
		{
			int num = Stat.Roll(Amount, null);
			bool flag = false;
			for (int i = 0; i < num; i++)
			{
				try
				{
					pInventory.AddObject(PopulationManager.CreateOneFrom(Table + Tier.Fuzz(tier, 5).ToString(), null, 0, 0, null, Context, null, null, null), null, false, false, true, null);
				}
				catch (Exception ex)
				{
					if (!flag)
					{
						MetricsManager.LogError("exception in BuildTableChestInventory(" + Table + ")", ex);
						flag = true;
					}
				}
			}
		}

		// Token: 0x06006B8B RID: 27531 RVA: 0x0022B9C8 File Offset: 0x00229BC8
		public static void BuildRareChestInventory(Inventory pInventory, int tier, string Context = null)
		{
			if (5.in100())
			{
				ChestBuilders.BuildSpecialChestInventory(pInventory, tier, Context);
				return;
			}
			int num = Stat.Random(1, 3);
			bool flag = false;
			for (int i = 0; i < num; i++)
			{
				try
				{
					Tier.Fuzz(tier, 5);
					pInventory.AddObject(PopulationManager.CreateOneFrom("Junk " + tier.ToString() + "R", null, 17, 0, null, Context, null, null, null), null, false, false, true, null);
				}
				catch (Exception ex)
				{
					if (!flag)
					{
						MetricsManager.LogError("exception in BuildRareChestInventory(" + tier.ToString() + ")", ex);
						flag = true;
					}
				}
			}
		}

		// Token: 0x06006B8C RID: 27532 RVA: 0x0022BA6C File Offset: 0x00229C6C
		public static void BuildSpecialChestInventory(Inventory pInventory, int Tier, string Context = null)
		{
			string text = "Junk 1";
			string text2 = "1d4+2";
			int num = Stat.Random(1, 12);
			if (num == 1)
			{
				text = "SpecialChestShoes";
			}
			else if (num == 2)
			{
				text = "Melee Weapons " + Tier.ToString();
			}
			else if (num == 3)
			{
				text = "Armor " + Tier.ToString();
			}
			else if (num == 4)
			{
				text = "Missile " + Tier.ToString();
			}
			else if (num == 5)
			{
				text = "Ammo " + Tier.ToString();
			}
			else if (num == 6)
			{
				text = "Cash " + Tier.ToString();
			}
			else if (num == 7)
			{
				text = "Utility " + Tier.ToString();
			}
			else if (num == 8)
			{
				text = "Food " + Tier.ToString();
			}
			else if (num == 9)
			{
				text = "Meds " + Tier.ToString();
			}
			else if (num == 10)
			{
				text = "Scrap " + Tier.ToString();
			}
			else if (num == 11)
			{
				text = "Trinkets " + Tier.ToString();
			}
			else if (num == 12)
			{
				text = "Artifact " + Tier.ToString();
				text2 = "1d2+1";
			}
			bool flag = false;
			int i = 0;
			int num2 = text2.RollCached();
			while (i < num2)
			{
				try
				{
					pInventory.AddObject(PopulationManager.CreateOneFrom(text, null, 0, 0, null, Context, null, null, null), null, false, false, true, null);
				}
				catch (Exception ex)
				{
					if (!flag)
					{
						MetricsManager.LogError("exception in BuildSpecialChestInventory(" + Tier.ToString() + ")", ex);
						flag = true;
					}
				}
				i++;
			}
		}
	}
}
