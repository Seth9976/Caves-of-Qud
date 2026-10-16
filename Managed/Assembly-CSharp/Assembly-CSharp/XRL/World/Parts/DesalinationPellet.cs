using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using XRL.Liquids;
using XRL.UI;
using XRL.World.Text;

namespace XRL.World.Parts
{
	// Token: 0x02000E04 RID: 3588
	[Serializable]
	public class DesalinationPellet : IPart
	{
		// Token: 0x06009100 RID: 37120 RVA: 0x00343FC8 File Offset: 0x003421C8
		public override bool SameAs(IPart p)
		{
			DesalinationPellet desalinationPellet = p as DesalinationPellet;
			return !(desalinationPellet.RemoveLiquid != this.RemoveLiquid) && !(desalinationPellet.RemoveLiquidAmount != this.RemoveLiquidAmount) && !(desalinationPellet.Message != this.Message) && !(desalinationPellet.DestroyMessage != this.DestroyMessage) && !(desalinationPellet.ConvertMessage != this.ConvertMessage) && desalinationPellet.Convert == this.Convert && base.SameAs(p);
		}

		// Token: 0x06009101 RID: 37121 RVA: 0x0034405C File Offset: 0x0034225C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID || ID == InventoryActionEvent.ID || ID == ObjectEnteringCellEvent.ID;
		}

		// Token: 0x06009102 RID: 37122 RVA: 0x00344084 File Offset: 0x00342284
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Apply")
			{
				if (!E.Actor.CheckFrozen(false, true, false, null))
				{
					return false;
				}
				Inventory inventory = E.Actor.Inventory;
				List<GameObject> list = Event.NewGameObjectList();
				inventory.GetObjects(list);
				GameObject gameObject = PickItem.ShowPicker(list, null, PickItem.PickItemDialogStyle.SelectItemDialog, E.Actor, null, null, null, false, null, false, true, false);
				if (gameObject == null)
				{
					return false;
				}
				if (gameObject.LiquidVolume == null)
				{
					E.Actor.ShowFailure("It doesn't seem to do anything.");
					return false;
				}
				IComponent<GameObject>.EmitMessage(E.Actor, string.Concat(new string[]
				{
					"You drop ",
					this.ParentObject.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false),
					" into ",
					gameObject.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
					".\n\n",
					this.PurifyLiquid(gameObject, E.Actor, false, true, E.Actor.IsPlayer())
				}), ' ', true, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009103 RID: 37123 RVA: 0x003441B8 File Offset: 0x003423B8
		public override bool HandleEvent(EnteredCellEvent E)
		{
			GameObject firstObjectWithPart = E.Cell.GetFirstObjectWithPart("LiquidVolume");
			if (firstObjectWithPart != null && firstObjectWithPart.IsOpenLiquidVolume())
			{
				this.PurifyLiquid(firstObjectWithPart, null, true, false, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009104 RID: 37124 RVA: 0x003441F4 File Offset: 0x003423F4
		public override bool HandleEvent(ObjectEnteringCellEvent E)
		{
			if (E.Object.IsOpenLiquidVolume())
			{
				this.PurifyLiquid(E.Object, null, true, false, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009105 RID: 37125 RVA: 0x0034421C File Offset: 0x0034241C
		public string PurifyLiquid(GameObject Item, GameObject Actor = null, bool ShowMessage = false, bool Single = false, bool MakeUnderstood = false)
		{
			DesalinationPellet.<>c__DisplayClass11_0 CS$<>8__locals1;
			CS$<>8__locals1.Item = Item;
			CS$<>8__locals1.LV = CS$<>8__locals1.Item.LiquidVolume;
			if (CS$<>8__locals1.LV == null)
			{
				return "";
			}
			CS$<>8__locals1.oldLiquidName = CS$<>8__locals1.LV.GetLiquidName();
			CS$<>8__locals1.purifier = (Single ? this.ParentObject.RemoveOne() : this.ParentObject);
			bool flag = false;
			bool flag2 = false;
			if (!this.RemoveLiquid.IsNullOrEmpty() && LiquidVolume.GetLiquid(this.RemoveLiquid) != null && CS$<>8__locals1.LV.ContainsLiquid(this.RemoveLiquid))
			{
				flag = true;
				int num = 0;
				for (int i = CS$<>8__locals1.purifier.Count; i > 0; i--)
				{
					num += this.RemoveLiquidAmount.RollCached();
				}
				if (num > 0)
				{
					if (CS$<>8__locals1.LV.IsPure())
					{
						if (CS$<>8__locals1.LV.Volume > num)
						{
							CS$<>8__locals1.LV.Volume -= num;
						}
						else
						{
							CS$<>8__locals1.LV.Empty(false, false);
						}
					}
					else
					{
						int num2 = Math.Min(Math.Max(CS$<>8__locals1.LV.UpperAmount(this.RemoveLiquid), 1), num);
						CS$<>8__locals1.LV.MixWith(new LiquidVolume(this.RemoveLiquid, -num2, num2), null, null, null, false);
					}
				}
			}
			if (this.Convert && CS$<>8__locals1.LV.Volume > 0)
			{
				foreach (BaseLiquid baseLiquid in LiquidVolume.getAllLiquids())
				{
					if (!baseLiquid.ConversionProduct.IsNullOrEmpty() && CS$<>8__locals1.LV.ContainsLiquid(baseLiquid.ID))
					{
						flag = true;
						int maxVolume = CS$<>8__locals1.LV.MaxVolume;
						string text;
						if (LiquidVolume.GetLiquid(baseLiquid.ConversionProduct) != null)
						{
							text = baseLiquid.ConversionProduct;
						}
						else
						{
							text = baseLiquid.ConversionProduct.CachedNumericDictionaryExpansion().GetRandomElement<string>(null);
						}
						try
						{
							CS$<>8__locals1.LV.MaxVolume = CS$<>8__locals1.LV.Volume;
							int num3;
							if (CS$<>8__locals1.LV.IsPure())
							{
								num3 = CS$<>8__locals1.LV.Volume;
								CS$<>8__locals1.LV.Empty(false, false);
							}
							else
							{
								num3 = Math.Max(CS$<>8__locals1.LV.UpperAmount(baseLiquid.ID), 1);
								CS$<>8__locals1.LV.MixWith(new LiquidVolume(baseLiquid.ID, -num3, num3), null, null, null, false);
							}
							if (num3 > 0)
							{
								CS$<>8__locals1.LV.MixWith(new LiquidVolume(text, num3, num3), null, null, null, false);
								flag2 = true;
							}
						}
						finally
						{
							CS$<>8__locals1.LV.MaxVolume = maxVolume;
						}
					}
				}
			}
			string liquidName = CS$<>8__locals1.LV.GetLiquidName();
			string text2;
			if (CS$<>8__locals1.LV.Volume <= 0)
			{
				text2 = DesalinationPellet.<PurifyLiquid>g__SetupBuilder|11_0(this.DestroyMessage, ref CS$<>8__locals1).ToString();
			}
			else if (flag2 && liquidName != CS$<>8__locals1.oldLiquidName)
			{
				string liquidDescription = CS$<>8__locals1.LV.GetLiquidDescription(true, true, true, true, false, null);
				text2 = DesalinationPellet.<PurifyLiquid>g__SetupBuilder|11_0(this.ConvertMessage, ref CS$<>8__locals1).AddReplacer("newLiquid", liquidDescription).ToString();
			}
			else
			{
				text2 = DesalinationPellet.<PurifyLiquid>g__SetupBuilder|11_0(this.Message, ref CS$<>8__locals1).ToString();
			}
			string text3 = null;
			if (flag)
			{
				CS$<>8__locals1.LV.Update();
				if (MakeUnderstood)
				{
					CS$<>8__locals1.purifier.MakeUnderstood(out text3);
				}
			}
			if (ShowMessage)
			{
				if (!text2.IsNullOrEmpty())
				{
					IComponent<GameObject>.EmitMessage(Actor ?? CS$<>8__locals1.Item, text2, ' ', true, false, false, null, null);
				}
				if (!text3.IsNullOrEmpty())
				{
					IComponent<GameObject>.EmitMessage(Actor ?? CS$<>8__locals1.Item, text3, ' ', true, false, false, null, null);
				}
			}
			else if (!text3.IsNullOrEmpty())
			{
				if (text2.IsNullOrEmpty())
				{
					text2 = text3;
				}
				else
				{
					text2 = text2 + "\n\n" + text3;
				}
			}
			CS$<>8__locals1.purifier.Obliterate(null, false, null);
			return text2;
		}

		// Token: 0x06009107 RID: 37127 RVA: 0x003446B4 File Offset: 0x003428B4
		[CompilerGenerated]
		internal static ReplaceBuilder <PurifyLiquid>g__SetupBuilder|11_0(string message, ref DesalinationPellet.<>c__DisplayClass11_0 A_1)
		{
			ReplaceBuilder replaceBuilder = message.StartReplace().StripColors();
			if (A_1.LV.IsOpenVolume())
			{
				replaceBuilder.AddObject(A_1.Item, null);
			}
			else
			{
				replaceBuilder.AddExplicit(A_1.oldLiquidName, false);
			}
			replaceBuilder.AddObject(A_1.purifier, null);
			return replaceBuilder;
		}

		// Token: 0x04003C79 RID: 15481
		public string RemoveLiquid = "salt";

		// Token: 0x04003C7A RID: 15482
		public string RemoveLiquidAmount = "200";

		// Token: 0x04003C7B RID: 15483
		public string Message = "=subject.T= =verb:fizzle= for several seconds.";

		// Token: 0x04003C7C RID: 15484
		public string DestroyMessage = "=subject.T= =verb:fizzle= for several seconds, then =verb:evaporate=.";

		// Token: 0x04003C7D RID: 15485
		public string ConvertMessage = "=object.T= =object.verb:react= strangely with =subject.t= and =object.verb:convert= =pronouns.objective= to =newLiquid=.";

		// Token: 0x04003C7E RID: 15486
		public bool Convert = true;
	}
}
