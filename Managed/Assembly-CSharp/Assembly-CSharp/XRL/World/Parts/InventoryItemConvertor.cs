using System;
using System.Collections.Generic;
using XRL.Language;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x02001055 RID: 4181
	[Serializable]
	public class InventoryItemConvertor : IPoweredPart
	{
		// Token: 0x0600A681 RID: 42625 RVA: 0x003CE6FA File Offset: 0x003CC8FA
		public InventoryItemConvertor()
		{
			this.ChargeUse = 500;
			this.WorksOnEquipper = true;
			this.NameForStatus = "ConversionSystems";
		}

		// Token: 0x0600A682 RID: 42626 RVA: 0x003CE73C File Offset: 0x003CC93C
		public override bool SameAs(IPart Part)
		{
			InventoryItemConvertor inventoryItemConvertor = Part as InventoryItemConvertor;
			return !(inventoryItemConvertor.ConversionTag != this.ConversionTag) && !(inventoryItemConvertor.Verb != this.Verb) && !(inventoryItemConvertor.Preposition != this.Preposition) && inventoryItemConvertor.Chance == this.Chance && inventoryItemConvertor.AllowRandomMods == this.AllowRandomMods && inventoryItemConvertor.GiganticFactor == this.GiganticFactor && inventoryItemConvertor.UseChargeEveryTurn == this.UseChargeEveryTurn && inventoryItemConvertor.UseChargeOnProcessing == this.UseChargeOnProcessing && base.SameAs(Part);
		}

		// Token: 0x0600A683 RID: 42627 RVA: 0x003CE7E6 File Offset: 0x003CC9E6
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A684 RID: 42628 RVA: 0x003CE7E9 File Offset: 0x003CC9E9
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A685 RID: 42629 RVA: 0x003CE7EC File Offset: 0x003CC9EC
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ProcessItems(Amount);
		}

		// Token: 0x0600A686 RID: 42630 RVA: 0x003CE7F8 File Offset: 0x003CC9F8
		private bool ProcessItem(GameObject Object)
		{
			string text = ((Object != null) ? Object.GetPropertyOrTag(this.ConversionTag, null) : null);
			if (text.IsNullOrEmpty())
			{
				return true;
			}
			if (!this.Chance.in100())
			{
				return true;
			}
			int num = 1;
			if (text.Contains(":"))
			{
				string[] array = text.Split(new string[] { ":" }, 2, StringSplitOptions.None);
				text = array[0];
				num = array[1].RollCached();
				if (num <= 0)
				{
					return false;
				}
			}
			if (this.GiganticFactor != 1f && Object.HasPart<ModGigantic>())
			{
				num = (int)((float)num * this.GiganticFactor);
			}
			bool flag = Object.GetIntProperty("StoredByPlayer", 0) > 0;
			GameObject gameObject = null;
			try
			{
				if (this.AllowRandomMods)
				{
					gameObject = GameObject.Create(text);
				}
				else
				{
					gameObject = GameObject.CreateUnmodified(text);
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("ItemConvertor", ex, "game_exception");
			}
			if (gameObject == null)
			{
				return true;
			}
			List<GameObject> list = null;
			List<GameObject> list2 = null;
			if (num > 1)
			{
				if (gameObject.CanGenerateStacked())
				{
					gameObject.Count = num;
				}
				else
				{
					list = Event.NewGameObjectList();
					list.Add(gameObject);
					list2 = Event.NewGameObjectList();
					for (int i = 1; i < num; i++)
					{
						try
						{
							GameObject gameObject2;
							if (this.AllowRandomMods)
							{
								gameObject2 = GameObject.Create(text);
							}
							else
							{
								gameObject2 = GameObject.CreateUnmodified(text);
							}
							list.Add(gameObject2);
							list2.Add(gameObject2);
						}
						catch (Exception ex2)
						{
							MetricsManager.LogException("ItemConvertor", ex2, "game_exception");
						}
					}
				}
			}
			if (flag)
			{
				Object.SetIntProperty("FromStoredByPlayer", 1, false);
			}
			Object.SplitFromStack();
			try
			{
				WasDerivedFromEvent.Send(Object, this.ParentObject, gameObject, list2, list, InventoryItemConvertor.DERIVATION_CONTEXT);
			}
			catch (Exception ex3)
			{
				MetricsManager.LogError(ex3);
			}
			try
			{
				DerivationCreatedEvent.Send(gameObject, this.ParentObject, Object, InventoryItemConvertor.DERIVATION_CONTEXT);
			}
			catch (Exception ex4)
			{
				MetricsManager.LogError(ex4);
			}
			if (!this.Verb.IsNullOrEmpty())
			{
				if (list2 != null && list != null)
				{
					base.DidXToY(this.Verb, Object, this.Preposition, Grammar.MakeAndList(list, false, true, false, true, false, null), null, null, null, null, false, false, true, false, false, null, null, null, false, false, false, false, false, null);
				}
				else
				{
					base.DidXToYWithZ(this.Verb, Object, this.Preposition, gameObject, null, null, null, null, null, null, false, false, true, true, false, false, false, false, null, null, null, null, false, false, false, false, false, null);
				}
			}
			Object.ReplaceWith(gameObject);
			if (list2 != null)
			{
				GameObject gameObject3;
				Cell cell;
				Object.GetContext(out gameObject3, out cell);
				if (gameObject3 != null)
				{
					using (List<GameObject>.Enumerator enumerator = list2.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							GameObject gameObject4 = enumerator.Current;
							gameObject3.ReceiveObject(gameObject4, false, null);
						}
						goto IL_02D1;
					}
				}
				if (cell != null)
				{
					using (List<GameObject>.Enumerator enumerator = list2.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							GameObject gameObject5 = enumerator.Current;
							cell.AddObject(gameObject5, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						}
						goto IL_02D1;
					}
				}
				MetricsManager.LogError("no context to deliver additional objects from " + this.ParentObject.DebugName);
			}
			IL_02D1:
			Object.CheckStack();
			return false;
		}

		// Token: 0x0600A687 RID: 42631 RVA: 0x003CEB2C File Offset: 0x003CCD2C
		private bool ProcessInventory(GameObject Object)
		{
			bool flag = true;
			Inventory inventory = Object.Inventory;
			if (inventory != null)
			{
				List<GameObject> list = Event.NewGameObjectList();
				inventory.GetObjects(list);
				foreach (GameObject gameObject in list)
				{
					if (gameObject.InInventory == Object && !this.ProcessItem(gameObject))
					{
						flag = false;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600A688 RID: 42632 RVA: 0x003CEBA8 File Offset: 0x003CCDA8
		public bool ProcessItems(int Turns = 1)
		{
			bool flag = false;
			if (base.IsReady(this.UseChargeEveryTurn, false, false, false, false, false, false, false, false, false, Turns, null, false, 0L, null))
			{
				int num = 0;
				while (num < Turns && !this.ForeachActivePartSubjectWhile(new Predicate<GameObject>(this.ProcessInventory), true))
				{
					flag = true;
					if (this.UseChargeOnProcessing)
					{
						base.ConsumeCharge(null, null);
					}
					num++;
				}
			}
			return flag;
		}

		// Token: 0x040044C2 RID: 17602
		public static readonly string DERIVATION_CONTEXT = "InventoryItemConvertor";

		// Token: 0x040044C3 RID: 17603
		public string ConversionTag;

		// Token: 0x040044C4 RID: 17604
		public string Verb;

		// Token: 0x040044C5 RID: 17605
		public string Preposition;

		// Token: 0x040044C6 RID: 17606
		public int Chance = 100;

		// Token: 0x040044C7 RID: 17607
		public bool AllowRandomMods;

		// Token: 0x040044C8 RID: 17608
		public float GiganticFactor = 1f;

		// Token: 0x040044C9 RID: 17609
		public bool UseChargeEveryTurn = true;

		// Token: 0x040044CA RID: 17610
		public bool UseChargeOnProcessing;
	}
}
