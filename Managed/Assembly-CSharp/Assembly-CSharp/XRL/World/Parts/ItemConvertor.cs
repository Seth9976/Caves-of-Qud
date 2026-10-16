using System;
using System.Collections.Generic;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x02000FA7 RID: 4007
	[Serializable]
	public class ItemConvertor : IPoweredPart
	{
		// Token: 0x06009F4D RID: 40781 RVA: 0x0039E358 File Offset: 0x0039C558
		public ItemConvertor()
		{
			this.ChargeUse = 500;
			this.WorksOnInventory = true;
			this.NameForStatus = "ConversionSystems";
		}

		// Token: 0x06009F4E RID: 40782 RVA: 0x0039E390 File Offset: 0x0039C590
		public override bool SameAs(IPart Part)
		{
			ItemConvertor itemConvertor = Part as ItemConvertor;
			return !(itemConvertor.ConversionTag != this.ConversionTag) && !(itemConvertor.Verb != this.Verb) && !(itemConvertor.Preposition != this.Preposition) && itemConvertor.Chance == this.Chance && itemConvertor.AllowRandomMods == this.AllowRandomMods && itemConvertor.GiganticFactor == this.GiganticFactor && itemConvertor.UseChargeEveryTurn == this.UseChargeEveryTurn && itemConvertor.UseChargeOnProcessing == this.UseChargeOnProcessing && base.SameAs(Part);
		}

		// Token: 0x06009F4F RID: 40783 RVA: 0x0039E43A File Offset: 0x0039C63A
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009F50 RID: 40784 RVA: 0x0039E43D File Offset: 0x0039C63D
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06009F51 RID: 40785 RVA: 0x0039E440 File Offset: 0x0039C640
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ProcessItems(Amount);
		}

		// Token: 0x06009F52 RID: 40786 RVA: 0x0039E44C File Offset: 0x0039C64C
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
				WasDerivedFromEvent.Send(Object, this.ParentObject, gameObject, list2, list, ItemConvertor.DERIVATION_CONTEXT);
			}
			catch (Exception ex3)
			{
				MetricsManager.LogError(ex3);
			}
			try
			{
				DerivationCreatedEvent.Send(gameObject, this.ParentObject, Object, ItemConvertor.DERIVATION_CONTEXT);
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

		// Token: 0x06009F53 RID: 40787 RVA: 0x0039E780 File Offset: 0x0039C980
		public bool ProcessItems(int Turns = 1)
		{
			bool flag = false;
			if (base.IsReady(this.UseChargeEveryTurn, false, false, false, false, false, false, false, false, false, Turns, null, false, 0L, null))
			{
				int num = 0;
				while (num < Turns && !this.ForeachActivePartSubjectWhile(new Predicate<GameObject>(this.ProcessItem), true))
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

		// Token: 0x0400421B RID: 16923
		public static readonly string DERIVATION_CONTEXT = "ItemConvertor";

		// Token: 0x0400421C RID: 16924
		public string ConversionTag;

		// Token: 0x0400421D RID: 16925
		public string Verb;

		// Token: 0x0400421E RID: 16926
		public string Preposition;

		// Token: 0x0400421F RID: 16927
		public int Chance = 100;

		// Token: 0x04004220 RID: 16928
		public bool AllowRandomMods;

		// Token: 0x04004221 RID: 16929
		public float GiganticFactor = 1f;

		// Token: 0x04004222 RID: 16930
		public bool UseChargeEveryTurn;

		// Token: 0x04004223 RID: 16931
		public bool UseChargeOnProcessing;
	}
}
