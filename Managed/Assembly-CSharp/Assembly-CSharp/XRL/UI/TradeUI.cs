using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using Qud.UI;
using XRL.Language;
using XRL.World;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;

namespace XRL.UI
{
	// Token: 0x0200149C RID: 5276
	[UIView("ConsoleTrade", false, true, false, "Trade", null, false, 0, false)]
	public class TradeUI : IWantsTextConsoleInit
	{
		// Token: 0x0600CF37 RID: 53047 RVA: 0x004B6F23 File Offset: 0x004B5123
		void IWantsTextConsoleInit.Init(TextConsole console, ScreenBuffer buffer)
		{
			TradeUI._TextConsole = console;
			TradeUI._ScreenBuffer = buffer;
		}

		// Token: 0x0600CF38 RID: 53048 RVA: 0x004B6F31 File Offset: 0x004B5131
		public static double GetMultiplier(GameObject GO)
		{
			if (GO == null || !GO.IsCurrency)
			{
				return TradeUI.Performance;
			}
			return 1.0;
		}

		// Token: 0x0600CF39 RID: 53049 RVA: 0x004B6F50 File Offset: 0x004B5150
		public static bool ValidForTrade(GameObject Object, GameObject Trader, GameObject Other = null, float CostMultiple = 1f, bool AcceptRelevant = true)
		{
			if (Other != null && Object.MovingIntoWouldCreateContainmentLoop(Other))
			{
				return false;
			}
			if (AcceptRelevant && !CanAcceptObjectEvent.Check(Object, Trader, Other))
			{
				return false;
			}
			if (TradeUI.ScreenMode == TradeUI.TradeScreenMode.Container)
			{
				return true;
			}
			if (Object.IsNatural())
			{
				return false;
			}
			if (CostMultiple > 0f && Object.HasPropertyOrTag("WaterContainer") && !Object.HasTagOrProperty("QuestItem"))
			{
				LiquidVolume liquidVolume = Object.LiquidVolume;
				if (liquidVolume != null && liquidVolume.IsFreshWater(false) && !Object.HasPart<TinkerItem>())
				{
					return false;
				}
			}
			if (Trader.IsPlayer())
			{
				if (Object.HasPropertyOrTag("PlayerWontSell"))
				{
					return false;
				}
				if (CostMultiple > 0f && !CanBeTradedEvent.Check(Object, Trader, Other, CostMultiple))
				{
					return false;
				}
			}
			else
			{
				if (Object.HasPropertyOrTag("WontSell") && !Trader.IsPlayerLed())
				{
					return false;
				}
				if (Trader.HasPropertyOrTag("WontSell") && Trader.GetPropertyOrTag("WontSell", null).Contains(Object.Blueprint) && !Trader.IsPlayerLed())
				{
					return false;
				}
				if (Trader.HasPropertyOrTag("WontSellTag") && Object.HasTagOrProperty(Trader.GetPropertyOrTag("WontSellTag", null)) && !Trader.IsPlayerLed())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600CF3A RID: 53050 RVA: 0x004B7070 File Offset: 0x004B5270
		public static void GetObjects(GameObject Trader, List<TradeEntry> ReturnObjects, GameObject Other, float CostMultiple = 1f)
		{
			List<GameObject> list = new List<GameObject>(64);
			bool flag = CanAcceptObjectEvent.Relevant(Other);
			foreach (GameObject gameObject in Trader.Inventory.GetObjects())
			{
				if (TradeUI.ValidForTrade(gameObject, Trader, Other, CostMultiple, flag))
				{
					list.Add(gameObject);
				}
			}
			list.Sort((GameObject a, GameObject b) => a.SortVs(b, null, true, true, true, false, null));
			string text = "";
			foreach (GameObject gameObject2 in list)
			{
				gameObject2.Seen();
				string inventoryCategory = gameObject2.GetInventoryCategory(false);
				if (inventoryCategory != text)
				{
					text = inventoryCategory;
					ReturnObjects.Add(new TradeEntry(text));
				}
				ReturnObjects.Add(new TradeEntry(gameObject2));
			}
		}

		// Token: 0x0600CF3B RID: 53051 RVA: 0x004B7180 File Offset: 0x004B5380
		public static string FormatPrice(double Price, float Multiplier)
		{
			return string.Format("{0:0.00}", Price * (double)Multiplier);
		}

		// Token: 0x0600CF3C RID: 53052 RVA: 0x004B7198 File Offset: 0x004B5398
		public static void Reset()
		{
			TradeUI.ScrollPosition = new int[2];
			TradeUI.Totals = new double[2];
			TradeUI.Weight = new int[2];
			if (TradeUI.Objects == null)
			{
				TradeUI.Objects = new List<TradeEntry>[2];
				TradeUI.Objects[0] = new List<TradeEntry>();
				TradeUI.Objects[1] = new List<TradeEntry>();
			}
			TradeUI.Objects[0].Clear();
			TradeUI.Objects[1].Clear();
			TradeUI.NumberSelected = new int[2][];
		}

		// Token: 0x0600CF3D RID: 53053 RVA: 0x004B7213 File Offset: 0x004B5413
		public static int GetSideOfObject(GameObject obj)
		{
			if (TradeUI.FindInTradeList(TradeUI.Objects[0], obj) > -1)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x0600CF3E RID: 53054 RVA: 0x004B7228 File Offset: 0x004B5428
		public static int FindInTradeList(List<TradeEntry> list, GameObject obj)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].GO == obj)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600CF3F RID: 53055 RVA: 0x004B7258 File Offset: 0x004B5458
		public static double ItemValueEach(GameObject obj, bool? TraderInventory = null)
		{
			double num = obj.ValueEach;
			if (TradeUI._Trader != null)
			{
				bool? flag = TraderInventory;
				bool flag2 = true;
				if (((flag.GetValueOrDefault() == flag2) & (flag != null)) || (TraderInventory == null && TradeUI.FindInTradeList(TradeUI.Objects[0], obj) != -1))
				{
					int intProperty = TradeUI._Trader.GetIntProperty("MinimumSellValue", 0);
					if (intProperty > 0 && num < (double)intProperty)
					{
						num = (double)intProperty;
					}
				}
			}
			return num;
		}

		// Token: 0x0600CF40 RID: 53056 RVA: 0x004B72C4 File Offset: 0x004B54C4
		public static double GetValue(GameObject obj, bool? TraderInventory = null)
		{
			bool? flag = TraderInventory;
			bool flag2 = true;
			if (((flag.GetValueOrDefault() == flag2) & (flag != null)) || (TraderInventory == null && TradeUI.FindInTradeList(TradeUI.Objects[0], obj) != -1))
			{
				return TradeUI.ItemValueEach(obj, new bool?(true)) / TradeUI.GetMultiplier(obj);
			}
			flag = TraderInventory;
			flag2 = false;
			if (((flag.GetValueOrDefault() == flag2) & (flag != null)) || (TraderInventory == null && TradeUI.FindInTradeList(TradeUI.Objects[1], obj) != -1))
			{
				return TradeUI.ItemValueEach(obj, new bool?(false)) * TradeUI.GetMultiplier(obj);
			}
			return 0.0;
		}

		// Token: 0x0600CF41 RID: 53057 RVA: 0x004B7364 File Offset: 0x004B5564
		public static int GetNumberSelected(GameObject obj)
		{
			int num = TradeUI.FindInTradeList(TradeUI.Objects[0], obj);
			if (num != -1)
			{
				return TradeUI.NumberSelected[0][num];
			}
			num = TradeUI.FindInTradeList(TradeUI.Objects[1], obj);
			if (num != -1)
			{
				return TradeUI.NumberSelected[1][num];
			}
			return -999;
		}

		// Token: 0x0600CF42 RID: 53058 RVA: 0x004B73B0 File Offset: 0x004B55B0
		public static void SetSelectedObject(GameObject obj)
		{
			int num = TradeUI.FindInTradeList(TradeUI.Objects[0], obj);
			if (num != -1)
			{
				TradeUI.SideSelected = 0;
				TradeUI.RowSelect = num;
			}
			num = TradeUI.FindInTradeList(TradeUI.Objects[1], obj);
			if (num != -1)
			{
				TradeUI.SideSelected = 1;
				TradeUI.RowSelect = num;
			}
		}

		// Token: 0x0600CF43 RID: 53059 RVA: 0x004B73FC File Offset: 0x004B55FC
		public static void SetNumberSelected(GameObject obj, int amount)
		{
			int num = TradeUI.FindInTradeList(TradeUI.Objects[0], obj);
			if (num != -1)
			{
				TradeUI.NumberSelected[0][num] = amount;
			}
			num = TradeUI.FindInTradeList(TradeUI.Objects[1], obj);
			if (num != -1)
			{
				TradeUI.NumberSelected[1][num] = amount;
			}
			TradeUI.UpdateTotals(TradeUI.Totals, TradeUI.Weight, TradeUI.Objects, TradeUI.NumberSelected);
		}

		// Token: 0x0600CF44 RID: 53060 RVA: 0x004B745C File Offset: 0x004B565C
		public static void PerformObjectDropped(GameObject Object, int DroppedOnSide)
		{
			if (TradeUI.FindInTradeList(TradeUI.Objects[DroppedOnSide], Object) != -1)
			{
				int num = TradeUI.FindInTradeList(TradeUI.Objects[DroppedOnSide], Object);
				TradeUI.NumberSelected[DroppedOnSide][num] = 0;
				TradeUI.UpdateTotals(TradeUI.Totals, TradeUI.Weight, TradeUI.Objects, TradeUI.NumberSelected);
				return;
			}
			int num2 = TradeUI.FindInTradeList(TradeUI.Objects[1 - DroppedOnSide], Object);
			if (num2 != -1)
			{
				TradeUI.NumberSelected[1 - DroppedOnSide][num2] = TradeUI.Objects[1 - DroppedOnSide][num2].GO.Count;
				TradeUI.UpdateTotals(TradeUI.Totals, TradeUI.Weight, TradeUI.Objects, TradeUI.NumberSelected);
			}
		}

		// Token: 0x0600CF45 RID: 53061 RVA: 0x004B74FC File Offset: 0x004B56FC
		public static void UpdateTotals(double[] Totals, int[] Weight, List<TradeEntry>[] Objects, int[][] NumberSelected)
		{
			for (int i = 0; i <= 1; i++)
			{
				double num = 1.0;
				if (i == 0)
				{
					num = 1.0 / TradeUI.Performance;
				}
				else if (i == 1)
				{
					num = TradeUI.Performance;
				}
				Totals[i] = 0.0;
				Weight[i] = 0;
				for (int j = 0; j < Objects[i].Count; j++)
				{
					if (Objects[i][j].GO != null && NumberSelected[i][j] > 0)
					{
						Weight[i] += Objects[i][j].GO.WeightEach * NumberSelected[i][j];
						if (Objects[i][j].GO.IsCurrency)
						{
							Totals[i] += TradeUI.ItemValueEach(Objects[i][j].GO, null) * (double)NumberSelected[i][j];
						}
						else
						{
							Totals[i] += TradeUI.ItemValueEach(Objects[i][j].GO, null) * num * (double)NumberSelected[i][j];
						}
					}
				}
				Totals[i] *= (double)TradeUI.costMultiple;
			}
			TradeUI.sReadout = string.Concat(new string[]
			{
				" {{C|",
				string.Format("{0:0.###}", Totals[0]),
				"}} drams <-> {{C|",
				string.Format("{0:0.###}", Totals[1]),
				"}} drams ÄÄ {{W|$",
				The.Player.GetFreeDrams("water", null, null, null, false).ToString(),
				"}} "
			});
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x0600CF46 RID: 53062 RVA: 0x004B76AE File Offset: 0x004B58AE
		public static string TradeScreenVerb
		{
			get
			{
				if (TradeUI.ScreenMode != TradeUI.TradeScreenMode.Trade || TradeUI.costMultiple <= 0f)
				{
					return "transfer";
				}
				return "trade";
			}
		}

		// Token: 0x0600CF47 RID: 53063 RVA: 0x004B76D0 File Offset: 0x004B58D0
		public static void ShowTradeScreen(GameObject Trader, float _costMultiple = 1f, TradeUI.TradeScreenMode screenMode = TradeUI.TradeScreenMode.Trade)
		{
			bool flag = Trader.IsPlayerLed();
			if (flag)
			{
				_costMultiple = 0f;
			}
			TradeUI.costMultiple = _costMultiple;
			TradeUI.ScreenMode = screenMode;
			TextConsole.LoadScrapBuffers();
			TradeUI.Reset();
			IL_0027:
			while (Trader != null)
			{
				if (!Trader.HasPart<Inventory>())
				{
					Popup.ShowFail(Trader.T(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false) + " cannot carry things.", true, true, true);
					TradeUI.Reset();
					return;
				}
				if (Trader.IsEngagedInMelee() && Trader.Brain != null)
				{
					Popup.ShowFail(Trader.Does("are", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false) + " engaged in melee combat and" + Trader.Is + " too busy to trade with you.", true, true, true);
					TradeUI.Reset();
					return;
				}
				if (Trader.IsAflame() && Trader.Brain != null)
				{
					Popup.ShowFail(Trader.Does("are", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false) + " on fire and" + Trader.Is + " too busy to trade with you.", true, true, true);
					TradeUI.Reset();
					return;
				}
				TradeUI._Trader = Trader;
				int num = TradeUI._Trader.GetIntProperty("TraderCreditExtended", 0);
				if (num > 0)
				{
					int freeDrams = The.Player.GetFreeDrams("water", null, null, null, false);
					if (freeDrams <= 0)
					{
						Popup.Show(string.Concat(new string[]
						{
							TradeUI._Trader.T(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false),
							" will not trade with you until you pay ",
							TradeUI._Trader.them,
							" the {{C|",
							num.ToString(),
							"}} ",
							(num == 1) ? "dram" : "drams",
							" of {{B|fresh water}} you owe ",
							TradeUI._Trader.them,
							"."
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
						TradeUI.Reset();
						return;
					}
					if (freeDrams < num)
					{
						if (Popup.ShowYesNo(string.Concat(new string[]
						{
							TradeUI._Trader.T(2147483647, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false),
							" will not trade with you until you pay ",
							TradeUI._Trader.them,
							" the {{C|",
							num.ToString(),
							"}} ",
							(num == 1) ? "dram" : "drams",
							" of {{B|fresh water}} you owe ",
							TradeUI._Trader.them,
							". Do you want to give ",
							TradeUI._Trader.them,
							" your {{C|",
							freeDrams.ToString(),
							"}} ",
							(freeDrams == 1) ? "dram" : "drams",
							" now?"
						}), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
						{
							num -= freeDrams;
							The.Player.UseDrams(freeDrams, "water", null, null, null, null, false);
							TradeUI._Trader.SetIntProperty("TraderCreditExtended", num, false);
						}
						TradeUI.Reset();
						return;
					}
					if (Popup.ShowYesNo(string.Concat(new string[]
					{
						TradeUI._Trader.T(2147483647, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false),
						" will not trade with you until you pay ",
						TradeUI._Trader.them,
						" the {{C|",
						num.ToString(),
						"}} ",
						(num == 1) ? "dram" : "drams",
						" of {{B|fresh water}} you owe ",
						TradeUI._Trader.them,
						". Do you want to give it to ",
						TradeUI._Trader.them,
						" now?"
					}), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
					{
						TradeUI.Reset();
						return;
					}
					The.Player.UseDrams(num, "water", null, null, null, null, false);
					TradeUI._Trader.RemoveIntProperty("TraderCreditExtended");
				}
				TradeUI._Trader.ModIntProperty("TradeCount", 1, false);
				TradeUI.Performance = GetTradePerformanceEvent.GetFor(The.Player, TradeUI._Trader);
				object part = Trader.GetPart<Tinkering_Repair>();
				int identifyLevel = Tinkering.GetIdentifyLevel(Trader);
				bool flag2 = identifyLevel > 0;
				bool flag3 = part != null;
				bool flag4 = Trader.HasSkill("Tinkering_Tinker1");
				bool flag5 = Trader.GetIntProperty("Librarian", 0) != 0;
				GameObject player = The.Player;
				bool flag6 = flag;
				StartTradeEvent.Send(player, Trader, identifyLevel, flag6, flag2, flag3, flag4, flag5);
				TradeUI.SideSelected = 0;
				TradeUI.RowSelect = 0;
				int num2 = 21;
				int num3 = 1;
				bool flag7;
				for (;;)
				{
					IL_04BF:
					TradeUI.Objects[0].Clear();
					TradeUI.Objects[1].Clear();
					TradeUI.ScrollPosition[0] = 0;
					TradeUI.ScrollPosition[1] = 0;
					TradeUI.Totals[0] = 0.0;
					TradeUI.Totals[1] = 0.0;
					TradeUI.GetObjects(Trader, TradeUI.Objects[0], The.Player, TradeUI.costMultiple);
					TradeUI.GetObjects(The.Player, TradeUI.Objects[1], Trader, TradeUI.costMultiple);
					TradeUI.NumberSelected[0] = new int[TradeUI.Objects[0].Count];
					TradeUI.NumberSelected[1] = new int[TradeUI.Objects[1].Count];
					if (TradeUI.Objects[0].Count <= 0 && TradeUI.costMultiple > 0f && !AllowTradeWithNoInventoryEvent.Check(The.Player, Trader))
					{
						goto Block_19;
					}
					TradeUI.nTotalWeight = The.Player.GetCarriedWeight();
					TradeUI.nMaxWeight = The.Player.GetMaxCarriedWeight();
					TradeUI.UpdateTotals(TradeUI.Totals, TradeUI.Weight, TradeUI.Objects, TradeUI.NumberSelected);
					flag7 = false;
					int num4 = 0;
					string text = string.Concat(new string[]
					{
						"[{{W|",
						ControlManager.getCommandInputDescription("CmdTradeAdd", false, false, ControlManager.InputDeviceType.Unknown, false),
						"}}/{{W|",
						ControlManager.getCommandInputDescription("CmdTradeRemove", false, false, ControlManager.InputDeviceType.Unknown, false),
						"}} Add/Remove]"
					});
					int length = ColorUtility.StripFormatting(text).Length;
					string text2 = "[{{W|" + ControlManager.getCommandInputDescription("CmdTradeOffer", false, false, ControlManager.InputDeviceType.Unknown, false) + "}} Offer]";
					int length2 = ColorUtility.StripFormatting(text2).Length;
					if (Options.ModernUI)
					{
						break;
					}
					GameManager.Instance.PushGameView("ConsoleTrade", true);
					string text3 = "[ {{W|" + StringFormat.ClipLine(Trader.IsCreature ? Trader.poss("inventory", true) : Trader.ShortDisplayName, 34, true, null) + "}} ]";
					bool flag8 = false;
					while (!flag8)
					{
						if (TradeUI.Objects[TradeUI.SideSelected].Count == 0)
						{
							TradeUI.SideSelected = 1 - TradeUI.SideSelected;
						}
						Event.ResetPool(false);
						TradeUI._ScreenBuffer.Clear();
						string text4 = "";
						string text5 = "";
						IRenderable renderable = null;
						int num5 = 0;
						int num6 = TradeUI.ScrollPosition[0];
						while (num5 < num2 && num6 < TradeUI.Objects[0].Count)
						{
							TradeUI._ScreenBuffer.Goto(2, num5 + num3);
							GameObject go = TradeUI.Objects[0][num6].GO;
							if (go != null)
							{
								if (TradeUI.NumberSelected[0][num6] > 0)
								{
									TradeUI._ScreenBuffer.Write("{{&Y^g|" + TradeUI.NumberSelected[0][num6].ToString() + "}} ", true, false, false, null, -1);
								}
								TradeUI._ScreenBuffer.Write(go.RenderForUI("Trade", false));
								TradeUI._ScreenBuffer.Write(" ", true, false, false, null, -1);
								TradeUI._ScreenBuffer.Write(go.DisplayName, true, false, false, null, 35);
								if (Trader.IsOwned() && go.OwnedByPlayer)
								{
									TradeUI._ScreenBuffer.Write(" {{G|[owned by you]}}", true, false, false, null, -1);
								}
								string text6 = "";
								if (TradeUI.SideSelected == 0 && TradeUI.RowSelect == num5)
								{
									text4 = go.DisplayNameSingle;
									if (Trader.IsOwned() && go.OwnedByPlayer)
									{
										text4 += " {{G|[owned by you]}}";
									}
									renderable = go.RenderForUI("Trade", false);
									text5 = " {{K|" + go.WeightEach.ToString() + "#}}";
									if (screenMode == TradeUI.TradeScreenMode.Trade)
									{
										string text7 = (go.IsCurrency ? "Y" : "B");
										text6 = string.Concat(new string[]
										{
											"{{",
											text7,
											"|$}}{{C|",
											TradeUI.FormatPrice(TradeUI.GetValue(go, new bool?(true)), TradeUI.costMultiple),
											"}}"
										});
										text5 = text5 + " " + text6;
									}
								}
								else if (screenMode == TradeUI.TradeScreenMode.Trade)
								{
									string text8 = (go.IsCurrency ? "W" : "b");
									text6 = string.Concat(new string[]
									{
										"{{",
										text8,
										"|$}}{{c|",
										TradeUI.FormatPrice(TradeUI.GetValue(go, new bool?(true)), TradeUI.costMultiple),
										"}}"
									});
								}
								int num7 = 40 - ColorUtility.LengthExceptFormatting(text6);
								TradeUI._ScreenBuffer.Goto(num7, num5 + num3);
								TradeUI._ScreenBuffer.Write(text6, true, false, false, null, -1);
							}
							else
							{
								string text9 = "{{K|[{{y|" + TradeUI.Objects[0][num6].CategoryName + "}}]}}";
								TradeUI._ScreenBuffer.Goto(40 - ColorUtility.LengthExceptFormatting(text9), num5 + num3);
								TradeUI._ScreenBuffer.Write(text9, true, false, false, null, -1);
							}
							num5++;
							num6++;
						}
						TradeUI._ScreenBuffer.Fill(41, num3, 77, num3 + num2, 32, 0, false, false);
						int num8 = 0;
						int num9 = TradeUI.ScrollPosition[1];
						while (num8 < num2 && num9 < TradeUI.Objects[1].Count)
						{
							TradeUI._ScreenBuffer.Goto(42, num8 + num3);
							GameObject go2 = TradeUI.Objects[1][num9].GO;
							if (go2 != null)
							{
								if (TradeUI.NumberSelected[1][num9] > 0)
								{
									TradeUI._ScreenBuffer.Write("{{&Y^g|" + TradeUI.NumberSelected[1][num9].ToString() + "}} ", true, false, false, null, -1);
								}
								TradeUI._ScreenBuffer.Write(go2.RenderForUI("Trade", false));
								TradeUI._ScreenBuffer.Write(" ", true, false, false, null, -1);
								TradeUI._ScreenBuffer.Write(go2.DisplayName, true, false, false, null, -1);
								string text6 = "";
								if (TradeUI.SideSelected == 1 && TradeUI.RowSelect == num8)
								{
									text4 = go2.DisplayNameSingle;
									renderable = go2.RenderForUI("Trade", false);
									text5 = " {{K|" + go2.WeightEach.ToString() + "#}}";
									if (screenMode == TradeUI.TradeScreenMode.Trade)
									{
										string text10 = (go2.IsCurrency ? "Y" : "B");
										text6 = string.Concat(new string[]
										{
											"{{",
											text10,
											"|$}}{{C|",
											TradeUI.FormatPrice(TradeUI.GetValue(go2, new bool?(false)), TradeUI.costMultiple),
											"}}"
										});
										text5 = text5 + " " + text6;
									}
								}
								else if (screenMode == TradeUI.TradeScreenMode.Trade)
								{
									string text11 = (go2.IsCurrency ? "W" : "b");
									text6 = string.Concat(new string[]
									{
										"{{",
										text11,
										"|$}}{{c|",
										TradeUI.FormatPrice(TradeUI.GetValue(go2, new bool?(false)), TradeUI.costMultiple),
										"}}"
									});
								}
								int num10 = 79 - ColorUtility.LengthExceptFormatting(text6);
								TradeUI._ScreenBuffer.Goto(num10, num8 + num3);
								TradeUI._ScreenBuffer.Write(text6, true, false, false, null, -1);
							}
							else
							{
								string text12 = "{{K|[{{y|" + TradeUI.Objects[1][num9].CategoryName + "}}]}}";
								TradeUI._ScreenBuffer.Goto(79 - ColorUtility.LengthExceptFormatting(text12), num8 + num3);
								TradeUI._ScreenBuffer.Write(text12, true, false, false, null, -1);
							}
							num8++;
							num9++;
						}
						TradeUI._ScreenBuffer.SingleBox(0, 0, 79, 24, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
						TradeUI._ScreenBuffer.Goto(2, 0);
						TradeUI._ScreenBuffer.Write(text3, true, false, false, null, -1);
						TradeUI._ScreenBuffer.Goto(42, 0);
						TradeUI._ScreenBuffer.Write("[ {{W|Your inventory}} ]", true, false, false, null, -1);
						TradeUI._ScreenBuffer.Goto(40, 0);
						TradeUI._ScreenBuffer.Write(194);
						for (int i = 1; i < 22; i++)
						{
							TradeUI._ScreenBuffer.Goto(40, i);
							TradeUI._ScreenBuffer.Write(179);
						}
						for (int j = 1; j < 79; j++)
						{
							TradeUI._ScreenBuffer.Goto(j, 22);
							TradeUI._ScreenBuffer.Write(196);
						}
						if (!Trader.IsCreature && Trader.IsOwned())
						{
							TradeUI._ScreenBuffer.WriteAt(2, 22, "{{R|[ owned by someone else ]}}", true);
						}
						if (TradeUI.SideSelected == 0)
						{
							TradeUI._ScreenBuffer.Goto(1, TradeUI.RowSelect + num3);
						}
						else
						{
							TradeUI._ScreenBuffer.Goto(41, TradeUI.RowSelect + num3);
						}
						TradeUI._ScreenBuffer.Write("{{&k^Y|>}}", true, false, false, null, -1);
						TradeUI._ScreenBuffer.Goto(40, 22);
						TradeUI._ScreenBuffer.Write(193);
						TradeUI._ScreenBuffer.Goto(0, 22);
						TradeUI._ScreenBuffer.Write(195);
						TradeUI._ScreenBuffer.Goto(79, 22);
						TradeUI._ScreenBuffer.Write(180);
						if (TradeUI.Objects[TradeUI.SideSelected].Count > 0 && TradeUI.Objects[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]] != null)
						{
							TradeUI._ScreenBuffer.Goto(2, 23);
							if (renderable != null)
							{
								TradeUI._ScreenBuffer.Write(renderable);
								TradeUI._ScreenBuffer.Goto(4, 23);
							}
							TradeUI._ScreenBuffer.Write(text4, true, false, false, null, -1);
							if (!string.IsNullOrEmpty(text5))
							{
								TradeUI._ScreenBuffer.Goto(79 - ColorUtility.LengthExceptFormatting(text5), 23);
								TradeUI._ScreenBuffer.Write(text5, true, false, false, null, -1);
							}
						}
						int num11 = 2;
						TradeUI._ScreenBuffer.Goto(num11, 24);
						if (ControlManager.activeControllerType != ControlManager.InputDeviceType.Gamepad)
						{
							TradeUI._ScreenBuffer.Write("[{{W|" + ControlManager.getCommandInputDescription("Cancel", true, false, ControlManager.InputDeviceType.Unknown, false) + "}} Exit]", true, false, false, null, -1);
							num11 += 11;
						}
						TradeUI._ScreenBuffer.Goto(num11, 24);
						TradeUI._ScreenBuffer.Write(text, true, false, false, null, -1);
						num11 += length + 1;
						if (ControlManager.activeControllerType != ControlManager.InputDeviceType.Gamepad)
						{
							TradeUI._ScreenBuffer.Goto(num11, 24);
							TradeUI._ScreenBuffer.Write("[{{W|0-9}} Pick]", true, false, false, null, -1);
							num11 += 11;
						}
						if (screenMode == TradeUI.TradeScreenMode.Trade)
						{
							TradeUI._ScreenBuffer.Goto(num11, 24);
							TradeUI._ScreenBuffer.Write(text2, true, false, false, null, -1);
							num11 += length2 + 1;
							if (Options.SifrahHaggling)
							{
								TradeUI._ScreenBuffer.Goto(num11, 24);
								TradeUI._ScreenBuffer.Write("[" + ControlManager.getCommandInputFormatted("SifraHaggle", false, false) + " Haggle]", true, false, false, null, -1);
								num11 += 11;
							}
						}
						else
						{
							TradeUI._ScreenBuffer.Goto(num11, 24);
							TradeUI._ScreenBuffer.Write("[{{W|" + ControlManager.getCommandInputDescription("CmdTradeOffer", false, false, ControlManager.InputDeviceType.Unknown, false) + "}} Transfer]", true, false, false, null, -1);
							num11 += length2 + 4;
						}
						TradeUI._ScreenBuffer.Goto(num11, 24);
						TradeUI._ScreenBuffer.Write("[{{W|" + ControlManager.getCommandInputDescription("CmdVendorActions", false, false, ControlManager.InputDeviceType.Unknown, false) + "}} Actions]", true, false, false, null, -1);
						num11 += 16;
						if (screenMode == TradeUI.TradeScreenMode.Trade)
						{
							string text13 = " {{C|" + string.Format("{0:0.###}", TradeUI.Totals[0]) + "}} drams ";
							TradeUI._ScreenBuffer.Goto(39 - ColorUtility.LengthExceptFormatting(text13), 22);
							TradeUI._ScreenBuffer.Write(text13, true, false, false, null, -1);
							TradeUI._ScreenBuffer.Goto(42, 22);
							TradeUI._ScreenBuffer.Write(string.Concat(new string[]
							{
								" {{C|",
								string.Format("{0:0.###}", TradeUI.Totals[1]),
								"}} drams ÄÄ {{W|$",
								The.Player.GetFreeDrams("water", null, null, null, false).ToString(),
								"}} "
							}), true, false, false, null, -1);
						}
						for (int k = 0; k <= 1; k++)
						{
							if (TradeUI.Objects[k].Count > num2)
							{
								for (int l = 1; l < 22; l++)
								{
									if (k == 0)
									{
										TradeUI._ScreenBuffer.Goto(0, l);
									}
									else
									{
										TradeUI._ScreenBuffer.Goto(79, l);
									}
									TradeUI._ScreenBuffer.Write(177, ColorUtility.Bright(0), 0);
								}
								int num12 = (int)Math.Ceiling((double)TradeUI.Objects[k].Count / (double)num2);
								int num13 = (int)Math.Ceiling((double)(TradeUI.Objects[k].Count + num2) / (double)num2);
								if (num13 <= 0)
								{
									num13 = 1;
								}
								int num14 = 21 / num13;
								if (num14 <= 0)
								{
									num14 = 1;
								}
								int num15 = (int)((double)(21 - num14) * ((double)TradeUI.ScrollPosition[k] / (double)(TradeUI.Objects[k].Count - num2)));
								num15++;
								for (int m = num15; m < num15 + num14; m++)
								{
									if (k == 0)
									{
										TradeUI._ScreenBuffer.Goto(0, m);
									}
									else
									{
										TradeUI._ScreenBuffer.Goto(79, m);
									}
									TradeUI._ScreenBuffer.Write(219, ColorUtility.Bright(7), 0);
								}
							}
						}
						int num16 = (int)(LiquidVolume.GetLiquid("water").Weight * (double)TradeUI.CalculateTrade(TradeUI.Totals[0], TradeUI.Totals[1]));
						int num17 = Math.Max(0, TradeUI.nTotalWeight + TradeUI.Weight[0] - TradeUI.Weight[1] - num16);
						string text14 = "K";
						if (num17 > TradeUI.nMaxWeight)
						{
							text14 = "R";
						}
						string text15 = string.Concat(new string[]
						{
							" {{",
							text14,
							"|",
							num17.ToString(),
							"/",
							TradeUI.nMaxWeight.ToString(),
							" lbs.}} "
						});
						TradeUI._ScreenBuffer.Goto(77 - ColorUtility.LengthExceptFormatting(text15), 22);
						TradeUI._ScreenBuffer.Write(text15, true, false, false, null, -1);
						TradeUI._TextConsole.DrawBuffer(TradeUI._ScreenBuffer, null, false);
						Keys keys = Keyboard.getvk(Options.MapDirectionsToKeypad, true, true);
						if (keys == Keys.Escape)
						{
							flag8 = true;
						}
						if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "RightClick")
						{
							flag8 = true;
						}
						if (keys >= Keys.D0 && keys <= Keys.D9)
						{
							if (TradeUI.Objects[TradeUI.SideSelected].Count > 0)
							{
								GameObject go3 = TradeUI.Objects[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]].GO;
								if (go3 != null)
								{
									int num18 = keys - Keys.D0;
									if (num4 >= go3.Count)
									{
										num4 = num18;
									}
									else
									{
										num4 = num4 * 10 + num18;
									}
									if (TradeUI.SideSelected == 1 && TradeUI.NumberSelected[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]] == 0 && (TradeUI.ScreenMode != TradeUI.TradeScreenMode.Container || Trader.IsOwned()) && !go3.ConfirmUseImportant(null, TradeUI.TradeScreenVerb, null, num4))
									{
										continue;
									}
									if (num4 > go3.Count)
									{
										TradeUI.NumberSelected[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]] = go3.Count;
									}
									else
									{
										TradeUI.NumberSelected[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]] = num4;
									}
								}
								TradeUI.UpdateTotals(TradeUI.Totals, TradeUI.Weight, TradeUI.Objects, TradeUI.NumberSelected);
								continue;
							}
						}
						else
						{
							num4 = 0;
						}
						if (keys == Keys.Oemtilde && TradeUI.Objects[TradeUI.SideSelected].Count > 0)
						{
							TradeUI.NumberSelected[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]] = 0;
							TradeUI.UpdateTotals(TradeUI.Totals, TradeUI.Weight, TradeUI.Objects, TradeUI.NumberSelected);
						}
						else
						{
							if (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdVendorActions" && TradeUI.Objects[TradeUI.SideSelected].Count > 0)
							{
								GameObject go4 = TradeUI.Objects[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]].GO;
								if (go4 == null)
								{
									continue;
								}
								TradeUI.ShowVendorActions(go4, Trader, false);
							}
							if (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdVendorRepair")
							{
								if (flag3)
								{
									if (TradeUI.Objects[TradeUI.SideSelected].Count > 0)
									{
										GameObject go5 = TradeUI.Objects[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]].GO;
										if (go5 != null)
										{
											TradeUI.DoVendorRepair(go5, Trader);
										}
									}
								}
								else
								{
									Popup.Show(Trader.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false) + Trader.GetVerb("do", true, false, false, true, false) + " not have the skill to repair items.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
								}
							}
							else if (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdTradeToggleAll")
							{
								bool flag9 = false;
								int n = 0;
								int count = TradeUI.Objects[TradeUI.SideSelected].Count;
								while (n < count)
								{
									GameObject go6 = TradeUI.Objects[TradeUI.SideSelected][n].GO;
									if (go6 != null)
									{
										int count2 = go6.Count;
										if (TradeUI.NumberSelected[TradeUI.SideSelected][n] != count2 && (TradeUI.SideSelected != 1 || !go6.IsImportant()))
										{
											TradeUI.NumberSelected[TradeUI.SideSelected][n] = count2;
											flag9 = true;
										}
									}
									n++;
								}
								if (!flag9)
								{
									int num19 = 0;
									int num20 = TradeUI.NumberSelected[TradeUI.SideSelected].Length;
									while (num19 < num20)
									{
										TradeUI.NumberSelected[TradeUI.SideSelected][num19] = 0;
										num19++;
									}
								}
								TradeUI.UpdateTotals(TradeUI.Totals, TradeUI.Weight, TradeUI.Objects, TradeUI.NumberSelected);
							}
							else
							{
								if (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdTradeAllItems" && TradeUI.Objects[TradeUI.SideSelected].Count > 0)
								{
									GameObject go7 = TradeUI.Objects[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]].GO;
									if (go7 != null)
									{
										if (TradeUI.NumberSelected[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]] == go7.Count)
										{
											TradeUI.NumberSelected[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]] = 0;
										}
										else
										{
											if (TradeUI.SideSelected == 1 && (TradeUI.ScreenMode != TradeUI.TradeScreenMode.Container || Trader.IsOwned()) && !go7.ConfirmUseImportant(null, TradeUI.TradeScreenVerb, null, -1))
											{
												goto IL_18DC;
											}
											TradeUI.NumberSelected[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]] = go7.Count;
										}
										TradeUI.UpdateTotals(TradeUI.Totals, TradeUI.Weight, TradeUI.Objects, TradeUI.NumberSelected);
									}
								}
								IL_18DC:
								if (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdVendorLook")
								{
									if (TradeUI.Objects[TradeUI.SideSelected].Count > 0)
									{
										GameObject go8 = TradeUI.Objects[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]].GO;
										InventoryActionEvent.Check(go8, The.Player, go8, "Look", false, false, false, false, false, 0, 0, 0, null, null, null, null);
									}
								}
								else if (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdVendorRead" && flag5)
								{
									if (TradeUI.Objects[TradeUI.SideSelected].Count > 0)
									{
										GameObject go9 = TradeUI.Objects[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]].GO;
										InventoryActionEvent.Check(go9, The.Player, go9, "Read", false, false, false, false, false, 0, 0, 0, null, null, null, null);
									}
								}
								else if (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdVendorRecharge")
								{
									if (flag4)
									{
										if (TradeUI.Objects[TradeUI.SideSelected].Count > 0)
										{
											GameObject go10 = TradeUI.Objects[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]].GO;
											if (go10 != null && (go10.Understood() || identifyLevel >= go10.GetComplexity()) && TradeUI.DoVendorRecharge(go10, Trader))
											{
												goto IL_04BF;
											}
										}
										else
										{
											Popup.Show(Trader.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false) + Trader.GetVerb("do", true, false, false, true, false) + " not have the skill to recharge items.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
										}
									}
								}
								else if (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdVendorRecharge" && flag5)
								{
									if (TradeUI.Objects[TradeUI.SideSelected].Count > 0)
									{
										GameObject go11 = TradeUI.Objects[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]].GO;
										InventoryActionEvent.Check(go11, The.Player, go11, "Read", false, false, false, false, false, 0, 0, 0, null, null, null, null);
									}
								}
								else if (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdVendorExamine")
								{
									if (TradeUI.Objects[TradeUI.SideSelected].Count > 0)
									{
										GameObject go12 = TradeUI.Objects[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]].GO;
										if (go12 != null)
										{
											TradeUI.DoVendorExamine(go12, Trader);
										}
									}
								}
								else
								{
									if (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdTradeAdd" && TradeUI.Objects[TradeUI.SideSelected].Count > 0)
									{
										GameObject go13 = TradeUI.Objects[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]].GO;
										if (go13 != null && (TradeUI.SideSelected != 1 || TradeUI.NumberSelected[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]] != 0 || (TradeUI.ScreenMode == TradeUI.TradeScreenMode.Container && !Trader.IsOwned()) || go13.ConfirmUseImportant(null, TradeUI.TradeScreenVerb, null, 1)))
										{
											if (TradeUI.NumberSelected[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]] < TradeUI.Objects[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]].GO.Count)
											{
												TradeUI.NumberSelected[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]]++;
											}
											TradeUI.UpdateTotals(TradeUI.Totals, TradeUI.Weight, TradeUI.Objects, TradeUI.NumberSelected);
										}
									}
									if (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdTradeRemove" && TradeUI.Objects[TradeUI.SideSelected].Count > 0 && TradeUI.Objects[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]].GO != null)
									{
										if (TradeUI.NumberSelected[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]] > 0)
										{
											TradeUI.NumberSelected[TradeUI.SideSelected][TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected]]--;
										}
										TradeUI.UpdateTotals(TradeUI.Totals, TradeUI.Weight, TradeUI.Objects, TradeUI.NumberSelected);
									}
									if (keys == Keys.NumPad4 && TradeUI.SideSelected == 1 && TradeUI.Objects[0].Count > 0)
									{
										TradeUI.SideSelected = 0;
									}
									if (keys == Keys.NumPad6 && TradeUI.SideSelected == 0 && TradeUI.Objects[1].Count > 0)
									{
										TradeUI.SideSelected = 1;
									}
									if (keys == Keys.NumPad8)
									{
										if (TradeUI.RowSelect == 0)
										{
											if (TradeUI.ScrollPosition[TradeUI.SideSelected] > 0)
											{
												TradeUI.ScrollPosition[TradeUI.SideSelected]--;
											}
										}
										else
										{
											TradeUI.RowSelect--;
										}
									}
									if (keys == Keys.NumPad2)
									{
										if (TradeUI.RowSelect < num2 - 1 && TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected] < TradeUI.Objects[TradeUI.SideSelected].Count - 1)
										{
											TradeUI.RowSelect++;
										}
										else if (TradeUI.ScrollPosition[TradeUI.SideSelected] + num2 < TradeUI.Objects[TradeUI.SideSelected].Count)
										{
											TradeUI.ScrollPosition[TradeUI.SideSelected]++;
										}
									}
									if (keys == Keys.Prior || Keyboard.RawCode == Keys.Prior || Keyboard.RawCode == Keys.Back)
									{
										if (TradeUI.RowSelect > 0)
										{
											TradeUI.RowSelect = 0;
										}
										else
										{
											TradeUI.ScrollPosition[TradeUI.SideSelected] -= num2 - 1;
											if (TradeUI.ScrollPosition[TradeUI.SideSelected] < 0)
											{
												TradeUI.ScrollPosition[TradeUI.SideSelected] = 0;
											}
										}
									}
									if (keys == Keys.Next || keys == Keys.Next || Keyboard.RawCode == Keys.Next || Keyboard.RawCode == Keys.Next)
									{
										if (TradeUI.RowSelect < num2 - 1)
										{
											TradeUI.RowSelect = num2 - 1;
											if (TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected] >= TradeUI.Objects[TradeUI.SideSelected].Count - 1)
											{
												TradeUI.RowSelect = TradeUI.Objects[TradeUI.SideSelected].Count - 1 - TradeUI.ScrollPosition[TradeUI.SideSelected];
											}
										}
										else if (TradeUI.RowSelect == num2 - 1)
										{
											TradeUI.ScrollPosition[TradeUI.SideSelected] += num2 - 1;
											if (TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected] >= TradeUI.Objects[TradeUI.SideSelected].Count - 1)
											{
												TradeUI.ScrollPosition[TradeUI.SideSelected] = TradeUI.Objects[TradeUI.SideSelected].Count - 1 - TradeUI.RowSelect;
											}
										}
									}
									if (TradeUI.RowSelect + TradeUI.ScrollPosition[TradeUI.SideSelected] >= TradeUI.Objects[TradeUI.SideSelected].Count - 1)
									{
										TradeUI.RowSelect = TradeUI.Objects[TradeUI.SideSelected].Count - 1 - TradeUI.ScrollPosition[TradeUI.SideSelected];
									}
									bool flag10 = false;
									bool flag11 = false;
									if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:SifrahHaggle" && Options.SifrahHaggling && screenMode == TradeUI.TradeScreenMode.Trade)
									{
										if (TradeUI.Totals[0] == 0.0 && TradeUI.Totals[1] == 0.0)
										{
											Popup.ShowFail("Set up a trade, then haggle for it.", true, true, true);
											continue;
										}
										int num21 = TradeUI.CalculateTrade(TradeUI.Totals[0], TradeUI.Totals[1]);
										if (num21 > 0 && num21 > The.Player.GetFreeDrams("water", null, null, null, false) * 2)
										{
											Popup.ShowFail("You must have at least half the fresh water you would need to pay for this trade available in order to attempt to haggle for it.", true, true, true);
											continue;
										}
										HagglingSifrah hagglingSifrah = new HagglingSifrah(Trader);
										hagglingSifrah.Play(Trader);
										if (hagglingSifrah.Abort)
										{
											continue;
										}
										if (hagglingSifrah.Performance > 0)
										{
											TradeUI.Performance += (1.0 - TradeUI.Performance) * (double)hagglingSifrah.Performance / 100.0;
										}
										else
										{
											TradeUI.Performance -= TradeUI.Performance * (double)(-(double)hagglingSifrah.Performance) / 100.0;
										}
										TradeUI.UpdateTotals(TradeUI.Totals, TradeUI.Weight, TradeUI.Objects, TradeUI.NumberSelected);
										int num22 = TradeUI.CalculateTrade(TradeUI.Totals[0], TradeUI.Totals[1]);
										string text16 = null;
										if (num21 == num22)
										{
											text16 = "In the end, though, it makes no difference.";
										}
										else if (num21 >= 0 && num22 >= 0)
										{
											text16 = string.Concat(new string[]
											{
												"As a result, the trade costs you ",
												num22.ToString(),
												" ",
												(num22 == 1) ? "dram" : "drams",
												" rather than ",
												num21.ToString(),
												"."
											});
										}
										else if (num21 < 0 && num22 < 0)
										{
											text16 = string.Concat(new string[]
											{
												"As a result, the trade is worth ",
												(-num22).ToString(),
												" ",
												(-num22 == 1) ? "dram" : "drams",
												" rather than ",
												(-num21).ToString(),
												"."
											});
										}
										else if (num21 >= 0 && num22 < 0)
										{
											text16 = string.Concat(new string[]
											{
												"As a result, the trade goes from costing you ",
												num21.ToString(),
												" ",
												(num21 == 1) ? "dram" : "drams",
												" to being worth ",
												(-num22).ToString(),
												"."
											});
										}
										else if (num21 < 0 && num22 >= 0)
										{
											text16 = string.Concat(new string[]
											{
												"As a result, the trade goes from being worth ",
												(-num21).ToString(),
												" ",
												(-num21 == 1) ? "dram" : "drams",
												" to being worth ",
												num22.ToString(),
												"."
											});
										}
										StringBuilder stringBuilder = Event.NewStringBuilder(null);
										if (!string.IsNullOrEmpty(hagglingSifrah.Description))
										{
											stringBuilder.Compound(hagglingSifrah.Description);
										}
										if (!string.IsNullOrEmpty(text16))
										{
											stringBuilder.Compound(text16);
										}
										if (stringBuilder.Length > 0)
										{
											Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
										}
										flag10 = true;
										flag11 = true;
									}
									if (Keyboard.vkCode == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:CmdTradeOffer")
									{
										flag10 = true;
									}
									int num23 = TradeUI.CalculateTrade(TradeUI.Totals[0], TradeUI.Totals[1]);
									if (flag10)
									{
										TradeUI.OfferStatus offerStatus = TradeUI.PerformOffer(num23, flag11, Trader, screenMode, TradeUI.Objects, TradeUI.NumberSelected);
										if (offerStatus == TradeUI.OfferStatus.REFRESH)
										{
											goto IL_04BF;
										}
										if (offerStatus == TradeUI.OfferStatus.TOP)
										{
											goto IL_0027;
										}
									}
								}
							}
						}
					}
					goto Block_195;
				}
				TradeUI.OfferStatus result = TradeScreen.show(Trader, TradeUI.costMultiple, screenMode).Result;
				if (result != TradeUI.OfferStatus.REFRESH && result != TradeUI.OfferStatus.NEXT && result != TradeUI.OfferStatus.TOP)
				{
					return;
				}
				continue;
				Block_19:
				Popup.Show(Trader.Does("have", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false) + " nothing to trade.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				TradeUI._Trader = null;
				TradeUI.Reset();
				return;
				Block_195:
				ScreenBuffer.ClearImposterSuppression();
				if (flag7)
				{
					The.Player.UseEnergy(1000, "Trading", null, null, false);
					TradeUI._Trader.UseEnergy(1000, "Trading", null, null, false);
				}
				GameManager.Instance.PopGameView(false);
				TradeUI._TextConsole.DrawBuffer(TextConsole.ScrapBuffer2, null, false);
				TradeUI._Trader = null;
				TradeUI.Reset();
				if (flag)
				{
					Trader.Brain.PerformReequip(false, true, false, null);
				}
				return;
			}
			TradeUI.Reset();
		}

		// Token: 0x0600CF48 RID: 53064 RVA: 0x004B9BA4 File Offset: 0x004B7DA4
		private static bool TryRemove(GameObject Object, GameObject Receiver, List<GameObject> TradeToPlayer, List<GameObject> TradeToTrader, bool Force = false)
		{
			Event @event = Event.New("CommandRemoveObject", "Object", Object).SetSilent(true);
			if (Receiver.FireEvent(@event) || Force)
			{
				return true;
			}
			string text = (Receiver.IsPlayer() ? "you" : Receiver.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false));
			Popup.ShowBlock("Trade could not be completed, " + text + " couldn't drop object: " + Object.DisplayName, null, "Sounds/UI/ui_notification", true, true, true, true, null);
			TradeUI.ReturnItems(TradeToPlayer, TradeToTrader);
			if (GameObject.Validate(ref Object))
			{
				Object.CheckStack();
			}
			return false;
		}

		// Token: 0x0600CF49 RID: 53065 RVA: 0x004B9C4C File Offset: 0x004B7E4C
		public static string ShowVendorActions(GameObject GO, GameObject Trader, bool IncludeModernTradeOptions = false)
		{
			if (GO == null || Trader == null)
			{
				return null;
			}
			Tinkering_Repair part = Trader.GetPart<Tinkering_Repair>();
			int identifyLevel = Tinkering.GetIdentifyLevel(Trader);
			bool flag = identifyLevel > 0;
			bool flag2 = part != null;
			bool flag3 = Trader.HasSkill("Tinkering_Tinker1");
			bool flag4 = Trader.GetIntProperty("Librarian", 0) != 0;
			List<string> list = new List<string> { "Look" };
			List<char> list2 = new List<char> { 'l' };
			if (IncludeModernTradeOptions)
			{
				list.Add("Add to trade");
				list2.Add('t');
			}
			if (flag && !GO.Understood())
			{
				list.Add("Identify");
				list2.Add('i');
			}
			if (flag2 && IsRepairableEvent.Check(Trader, GO, null, part, null))
			{
				list.Add("Repair");
				list2.Add('r');
			}
			if (flag3 && (GO.Understood() || identifyLevel >= GO.GetComplexity()) && GO.NeedsRecharge())
			{
				list.Add("Recharge");
				list2.Add('c');
			}
			if (flag4 && GO.HasInventoryActionWithCommand("Read", null))
			{
				list.Add("Read");
				list2.Add('b');
			}
			int num = Popup.PickOption("select an action", null, "", "Sounds/UI/ui_notification", list.ToArray(), list2.ToArray(), null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
			if (num >= 0)
			{
				if (list[num] == "Identify")
				{
					TradeUI.DoVendorExamine(GO, Trader);
				}
				if (list[num] == "Read")
				{
					TradeUI.DoVendorRead(GO, Trader);
				}
				if (list[num] == "Repair")
				{
					TradeUI.DoVendorRepair(GO, Trader);
				}
				if (list[num] == "Look")
				{
					TradeUI.DoVendorLook(GO, Trader);
				}
				if (list[num] == "Recharge")
				{
					TradeUI.DoVendorRecharge(GO, Trader);
				}
				return list[num];
			}
			return null;
		}

		// Token: 0x0600CF4A RID: 53066 RVA: 0x004B9E48 File Offset: 0x004B8048
		public static void DoVendorExamine(GameObject GO, GameObject Trader)
		{
			int identifyLevel = Tinkering.GetIdentifyLevel(Trader);
			if (identifyLevel > 0)
			{
				if (GO.Understood())
				{
					Popup.ShowFail("You already understand this item.", true, true, true);
					return;
				}
				int complexity = GO.GetComplexity();
				int examineDifficulty = GO.GetExamineDifficulty();
				if (The.Player.HasPart<Dystechnia>())
				{
					Popup.ShowFail("You can't understand " + Grammar.MakePossessive(Trader.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false)) + " explanation.", true, true, true);
					return;
				}
				if (identifyLevel < complexity)
				{
					Popup.ShowFail("This item is too complex for " + Trader.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false) + " to identify.", true, true, true);
					return;
				}
				float num = (float)(complexity + examineDifficulty);
				int num2 = (int)Math.Max(2.0, -0.0667 + 1.24 * (double)num + 0.0967 * Math.Pow((double)num, 2.0) + 0.0979 * Math.Pow((double)num, 3.0));
				if (The.Player.GetFreeDrams("water", null, null, null, false) < num2)
				{
					Popup.ShowFail(string.Concat(new string[]
					{
						"You do not have the required {{C|",
						num2.ToString(),
						"}} ",
						(num2 == 1) ? "dram" : "drams",
						" to identify this item."
					}), true, true, true);
					return;
				}
				if (Popup.ShowYesNo(string.Concat(new string[]
				{
					"You may identify this for ",
					num2.ToString(),
					" ",
					(num2 == 1) ? "dram" : "drams",
					" of fresh water."
				}), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes && The.Player.UseDrams(num2, "water", null, null, null, null, false))
				{
					Trader.GiveDrams(num2, "water", false, null, null, null, null, true, null);
					Popup.Show(string.Concat(new string[]
					{
						Trader.does("identify", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
						" ",
						GO.the,
						" ",
						GO.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, false, false, false, false, null, false, false, false, false, null, false, null, false, true),
						" as ",
						GO.an(int.MaxValue, null, null, true, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
						"."
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					GO.MakeUnderstood(false);
					return;
				}
			}
			else
			{
				Popup.Show(Trader.Does("don't", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false) + " have the skill to identify artifacts.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x0600CF4B RID: 53067 RVA: 0x004BA180 File Offset: 0x004B8380
		public static void DoVendorRead(GameObject GO, GameObject Trader)
		{
			InventoryActionEvent.Check(GO, The.Player, GO, "Read", false, false, false, false, false, 0, 0, 0, null, null, null, null);
		}

		// Token: 0x0600CF4C RID: 53068 RVA: 0x004BA1AC File Offset: 0x004B83AC
		public static void DoVendorRepair(GameObject GO, GameObject Trader)
		{
			if (GO == null || Trader == null)
			{
				return;
			}
			Tinkering_Repair part = Trader.GetPart<Tinkering_Repair>();
			if (part == null)
			{
				return;
			}
			bool flag = GO.IsPlural || GO.Count > 1;
			if (IsRepairableEvent.Check(Trader, GO, null, part, null))
			{
				if (!Tinkering_Repair.IsRepairableBy(GO, Trader, null, part, null))
				{
					Popup.Show((flag ? "These items are" : "This item is") + " too complex for " + Trader.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " to repair.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					return;
				}
				int num = Math.Max(5 + (int)(TradeUI.GetValue(GO, new bool?(false)) / 25.0), 5) * GO.Count;
				if (The.Player.GetFreeDrams("water", null, null, null, false) < num)
				{
					Popup.Show(string.Concat(new string[]
					{
						"You need {{C|",
						num.ToString(),
						"}} ",
						(num == 1) ? "dram" : "drams",
						" of fresh water to repair ",
						flag ? "those" : "that",
						"."
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					return;
				}
				if (Popup.ShowYesNo(string.Concat(new string[]
				{
					"You may repair ",
					flag ? "those" : "this",
					" for {{C|",
					num.ToString(),
					"}} ",
					(num == 1) ? "dram" : "drams",
					" of fresh water."
				}), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes && The.Player.UseDrams(num, "water", null, null, null, null, false))
				{
					Trader.GiveDrams(num, "water", false, null, null, null, null, true, null);
					The.Player.PlayWorldOrUISound("Sounds/Misc/sfx_interact_artifact_repair", null);
					RepairedEvent.Send(Trader, GO, null, part, 0);
					return;
				}
			}
			else
			{
				Popup.Show((flag ? "Those items aren't" : "That item isn't") + " broken!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x0600CF4D RID: 53069 RVA: 0x004BA3F0 File Offset: 0x004B85F0
		public static void DoVendorLook(GameObject GO, GameObject Trader)
		{
			InventoryActionEvent.Check(GO, The.Player, GO, "Look", false, false, false, false, false, 0, 0, 0, null, null, null, null);
		}

		// Token: 0x0600CF4E RID: 53070 RVA: 0x004BA41C File Offset: 0x004B861C
		public static TradeUI.OfferStatus PerformOffer(int Difference, bool forceComplete, GameObject Trader, TradeUI.TradeScreenMode screenMode, List<TradeEntry>[] Objects, int[][] NumberSelected)
		{
			if (Difference > 0)
			{
				int freeDrams = The.Player.GetFreeDrams("water", null, null, null, false);
				if (freeDrams >= Difference)
				{
					if (forceComplete)
					{
						Popup.Show(string.Concat(new string[]
						{
							"You pony up ",
							Difference.ToString(),
							" ",
							(Difference == 1) ? "dram" : "drams",
							" of fresh water to even up the trade."
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else if (Popup.ShowYesNo(string.Concat(new string[]
					{
						"You'll have to pony up ",
						Difference.ToString(),
						" ",
						(Difference == 1) ? "dram" : "drams",
						" of fresh water to even up the trade. Agreed?"
					}), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.No)
					{
						return TradeUI.OfferStatus.NEXT;
					}
				}
				else
				{
					if (!forceComplete)
					{
						Popup.Show(string.Concat(new string[]
						{
							"You don't have ",
							Difference.ToString(),
							" ",
							(Difference == 1) ? "dram" : "drams",
							" of fresh water to even up the trade!"
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
						return TradeUI.OfferStatus.NEXT;
					}
					int num = Difference - freeDrams;
					if (freeDrams > 0)
					{
						Popup.Show(string.Concat(new string[]
						{
							"You pony up ",
							freeDrams.ToString(),
							" ",
							(freeDrams == 1) ? "dram" : "drams",
							" of fresh water, and now owe ",
							Trader.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false),
							" ",
							num.ToString(),
							" ",
							(num == 1) ? "dram" : "drams",
							"."
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else
					{
						Popup.Show(string.Concat(new string[]
						{
							"You now owe ",
							Trader.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false),
							" ",
							num.ToString(),
							" ",
							(num == 1) ? "dram" : "drams",
							" of fresh water."
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					Trader.ModIntProperty("TraderCreditExtended", num, false);
				}
			}
			if (Difference < 0)
			{
				int num2 = -Difference;
				if (TradeUI.AssumeTradersHaveWater || Trader.GetFreeDrams("water", null, null, null, false) >= num2)
				{
					if (forceComplete)
					{
						Popup.Show(string.Concat(new string[]
						{
							Trader.Does("pony", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false),
							" up ",
							num2.ToString(),
							" ",
							(num2 == 1) ? "dram" : "drams",
							" of fresh water to even up the trade."
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else if (Popup.ShowYesNo(string.Concat(new string[]
					{
						Trader.T(2147483647, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false),
						" will have to pony up ",
						num2.ToString(),
						" ",
						(num2 == 1) ? "dram" : "drams",
						" of fresh water to even up the trade. Agreed?"
					}), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.No)
					{
						return TradeUI.OfferStatus.NEXT;
					}
				}
				else if (forceComplete)
				{
					Popup.Show(string.Concat(new string[]
					{
						Trader.Does("don't", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false),
						" have ",
						num2.ToString(),
						" ",
						(num2 == 1) ? "dram" : "drams",
						" of fresh water to even up the trade!"
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else if (Popup.ShowYesNo(string.Concat(new string[]
				{
					Trader.Does("don't", 2147483647, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false),
					" have ",
					num2.ToString(),
					" ",
					(num2 == 1) ? "dram" : "drams",
					" of fresh water to even up the trade! Do you want to complete the trade anyway?"
				}), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
				{
					return TradeUI.OfferStatus.NEXT;
				}
			}
			int num3 = -Difference;
			List<GameObject> list = null;
			for (int i = 0; i < Objects[1].Count; i++)
			{
				if (NumberSelected[1][i] > 0)
				{
					if (list == null)
					{
						list = new List<GameObject>();
					}
					list.Add(Objects[1][i].GO);
				}
			}
			int num4 = The.Player.GetStorableDrams("water", null, list, null, true, null);
			List<GameObject> countForStore = null;
			for (int j = 0; j < Objects[0].Count; j++)
			{
				if (NumberSelected[0][j] > 0 && NumberSelected[0][j] >= Objects[0][j].GO.Count)
				{
					if (countForStore == null)
					{
						countForStore = new List<GameObject>();
					}
					countForStore.Add(Objects[0][j].GO);
				}
			}
			if (countForStore != null)
			{
				num4 += Trader.GetStorableDrams("water", null, null, (GameObject o) => countForStore.Contains(o), true, null);
			}
			if (Difference < 0 && num4 < num3)
			{
				if (forceComplete)
				{
					Popup.Show(string.Concat(new string[]
					{
						"You don't have enough water containers to carry that many drams! You can store ",
						num4.ToString(),
						" ",
						(num4 == 1) ? "dram" : "drams",
						"."
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else if (Popup.ShowYesNo(string.Concat(new string[]
				{
					"You don't have enough water containers to carry that many drams! Do you want to complete the trade for the ",
					num4.ToString(),
					" ",
					(num4 == 1) ? "dram" : "drams",
					" you can store?"
				}), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.No)
				{
					return TradeUI.OfferStatus.NEXT;
				}
			}
			if (screenMode == TradeUI.TradeScreenMode.Container)
			{
				if (NumberSelected[0].Any<int>((int x) => x > 0) && !Trader.FireEvent(Event.New("BeforeContentsTaken", "Taker", The.Player)))
				{
					return TradeUI.OfferStatus.REFRESH;
				}
			}
			if (Difference > 0)
			{
				The.Player.UseDrams(Difference, "water", null, null, null, null, false);
			}
			List<GameObject> list2 = new List<GameObject>(16);
			List<GameObject> list3 = new List<GameObject>(16);
			for (int k = 0; k < Objects[0].Count; k++)
			{
				if (NumberSelected[0][k] > 0)
				{
					GameObject go = Objects[0][k].GO;
					go.SplitStack(NumberSelected[0][k], Trader, false);
					if (!TradeUI.TryRemove(go, Trader, list3, list2, forceComplete))
					{
						return TradeUI.OfferStatus.REFRESH;
					}
					go.RemoveIntProperty("_stock");
					list3.Add(go);
				}
			}
			for (int l = 0; l < Objects[1].Count; l++)
			{
				if (NumberSelected[1][l] > 0)
				{
					GameObject go2 = Objects[1][l].GO;
					go2.SplitStack(NumberSelected[1][l], The.Player, false);
					if (!TradeUI.TryRemove(go2, The.Player, list3, list2, forceComplete))
					{
						return TradeUI.OfferStatus.REFRESH;
					}
					if (Trader.IsMerchant() && TradeUI.costMultiple > 0f)
					{
						go2.SetIntProperty("_stock", 1, false);
						go2.RemoveIntProperty("StoredByPlayer");
						go2.RemoveIntProperty("FromStoredByPlayer");
					}
					else
					{
						go2.RemoveIntProperty("_stock");
						if (Trader.HasPart<Container>())
						{
							go2.SetIntProperty("StoredByPlayer", 1, false);
						}
					}
					list2.Add(go2);
				}
			}
			if (screenMode == TradeUI.TradeScreenMode.Container)
			{
				if (list3.Count > 0 && !Trader.FireEvent(Event.New("AfterContentsTaken", "Taker", The.Player)))
				{
					TradeUI.ReturnItems(list3, list2);
					return TradeUI.OfferStatus.REFRESH;
				}
				using (List<GameObject>.Enumerator enumerator = list2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameObject gameObject = enumerator.Current;
						try
						{
							if (!Trader.FireEvent(Event.New("CommandTakeObject", "Object", gameObject, "PutBy", The.Player, "EnergyCost", 0)))
							{
								The.Player.ReceiveObject(gameObject, false, null);
							}
						}
						catch (Exception ex)
						{
							MetricsManager.LogException("trade move to container", ex, "game_exception");
						}
					}
					goto IL_097F;
				}
			}
			foreach (GameObject gameObject2 in list2)
			{
				try
				{
					if (((gameObject2 != null) ? gameObject2.Physics : null) != null)
					{
						gameObject2.Physics.Owner = null;
					}
					Trader.TakeObject(gameObject2, false, false, new int?(0), null, null);
				}
				catch (Exception ex2)
				{
					MetricsManager.LogException("trade move to trader", ex2, "game_exception");
				}
			}
			if (list2.Count > 0 && Trader.HasPart<Container>())
			{
				The.Player.FireEvent(Event.New("PutSomethingIn", "Object", Trader));
			}
			IL_097F:
			foreach (GameObject gameObject3 in list3)
			{
				try
				{
					if (!The.Player.TakeObject(gameObject3, false, false, new int?(0), null, null))
					{
						Trader.ReceiveObject(gameObject3, false, null);
					}
				}
				catch (Exception ex3)
				{
					MetricsManager.LogException("trade move to player", ex3, "game_exception");
				}
			}
			try
			{
				if (Difference < 0)
				{
					The.Player.GiveDrams(num3, "water", false, null, null, null, null, true, null);
					Trader.UseDrams(num3, "water", null, null, null, null, false);
				}
				else if (Difference > 0)
				{
					Trader.GiveDrams(Difference, "water", false, null, null, null, null, true, null);
				}
			}
			catch (Exception ex4)
			{
				MetricsManager.LogException("trade water exchange", ex4, "game_exception");
			}
			if (screenMode == TradeUI.TradeScreenMode.Trade)
			{
				if (list2.Count > 0 || list3.Count > 0)
				{
					Popup.Show("Trade complete!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else
				{
					Popup.Show("Nothing to trade.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			if (list2.Count > 0 && Trader.IsPlayerLed())
			{
				Trader.Brain.PerformReequip(false, true, false, null);
			}
			return TradeUI.OfferStatus.TOP;
		}

		// Token: 0x0600CF4F RID: 53071 RVA: 0x004BAF2C File Offset: 0x004B912C
		private static void ReturnItems(List<GameObject> TradeToPlayer, List<GameObject> TradeToTrader)
		{
			foreach (GameObject gameObject in TradeToPlayer)
			{
				TradeUI._Trader.ReceiveObject(gameObject, false, null);
			}
			foreach (GameObject gameObject2 in TradeToTrader)
			{
				The.Player.ReceiveObject(gameObject2, false, null);
			}
		}

		// Token: 0x0600CF50 RID: 53072 RVA: 0x004BAFC8 File Offset: 0x004B91C8
		public static bool DoVendorRecharge(GameObject GO, GameObject Trader)
		{
			bool AnyRelevant = false;
			bool AnyRechargeable = false;
			bool AnyNotFullyCharged = false;
			bool AnyRecharged = false;
			Predicate<IRechargeable> predicate = delegate(IRechargeable P)
			{
				AnyRelevant = true;
				if (!P.CanBeRecharged())
				{
					return true;
				}
				AnyRechargeable = true;
				int rechargeAmount = P.GetRechargeAmount();
				if (rechargeAmount <= 0)
				{
					return true;
				}
				AnyNotFullyCharged = true;
				int num = Math.Max(rechargeAmount / 500, 1);
				string text = ((P.ParentObject.Count > 1) ? "one of those" : P.ParentObject.indicativeDistal);
				if (The.Player.GetFreeDrams("water", null, null, null, false) < num)
				{
					Popup.Show(string.Concat(new string[]
					{
						"You need {{C|",
						Grammar.Cardinal(num),
						"}} ",
						(num == 1) ? "dram" : "drams",
						" of fresh water to charge ",
						text,
						"."
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					return false;
				}
				if (Popup.ShowYesNo(string.Concat(new string[]
				{
					"You may recharge ",
					text,
					" for {{C|",
					Grammar.Cardinal(num),
					"}} ",
					(num == 1) ? "dram" : "drams",
					" of fresh water."
				}), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes && The.Player.UseDrams(num, "water", null, null, null, null, false))
				{
					P.ParentObject.SplitFromStack();
					P.AddCharge(rechargeAmount);
					P.ParentObject.CheckStack();
					Trader.GiveDrams(num, "water", false, null, null, null, null, true, null);
					AnyRecharged = true;
				}
				return true;
			};
			GO.ForeachPartDescendedFrom<IRechargeable>(predicate);
			EnergyCellSocket part = GO.GetPart<EnergyCellSocket>();
			if (part != null && part.Cell != null)
			{
				part.Cell.ForeachPartDescendedFrom<IRechargeable>(predicate);
			}
			if (!AnyRelevant)
			{
				Popup.Show("That item has no cell or rechargeable capacitor in it.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			else if (!AnyRechargeable)
			{
				Popup.Show("That item cannot be recharged this way.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			else if (!AnyNotFullyCharged)
			{
				Popup.Show(GO.Does("are", int.MaxValue, null, null, null, false, false, false, false, true, true, true, false, false, null, false, false, true, null, true, null, false) + " fully charged!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			if (AnyRecharged && Options.Sound)
			{
				SoundManager.PlaySound("Sounds/Abilities/sfx_ability_energyCell_recharge", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
			}
			return AnyRecharged;
		}

		// Token: 0x0600CF51 RID: 53073 RVA: 0x004BB0F4 File Offset: 0x004B92F4
		public static int CalculateTrade(double Bought, double Sold)
		{
			double num = Bought - Sold;
			if (num > 0.0)
			{
				num += 0.0001;
			}
			else if (num < 0.0)
			{
				num -= 0.0001;
			}
			if (Math.Abs(num) < 10.0)
			{
				return (int)Math.Ceiling(num);
			}
			return (int)Math.Round(num, MidpointRounding.AwayFromZero);
		}

		// Token: 0x04005071 RID: 20593
		public const double TRADING_FLOATING_POINT_FUDGE = 0.0001;

		// Token: 0x04005072 RID: 20594
		public static TextConsole _TextConsole;

		// Token: 0x04005073 RID: 20595
		public static ScreenBuffer _ScreenBuffer;

		// Token: 0x04005074 RID: 20596
		public static GameObject _Trader = null;

		// Token: 0x04005075 RID: 20597
		public static double Performance = 1.0;

		// Token: 0x04005076 RID: 20598
		public static bool AssumeTradersHaveWater = true;

		// Token: 0x04005077 RID: 20599
		public static int[] ScrollPosition = new int[2];

		// Token: 0x04005078 RID: 20600
		public static double[] Totals = new double[2];

		// Token: 0x04005079 RID: 20601
		public static int[] Weight = new int[2];

		// Token: 0x0400507A RID: 20602
		public static List<TradeEntry>[] Objects = null;

		// Token: 0x0400507B RID: 20603
		public static int[][] NumberSelected = new int[2][];

		// Token: 0x0400507C RID: 20604
		public static int nTotalWeight = 0;

		// Token: 0x0400507D RID: 20605
		public static int nMaxWeight = 0;

		// Token: 0x0400507E RID: 20606
		private static int SideSelected = 0;

		// Token: 0x0400507F RID: 20607
		private static int RowSelect = 0;

		// Token: 0x04005080 RID: 20608
		public static string sReadout = "";

		// Token: 0x04005081 RID: 20609
		public static float costMultiple = 1f;

		// Token: 0x04005082 RID: 20610
		public static TradeUI.TradeScreenMode ScreenMode;

		// Token: 0x02001DA4 RID: 7588
		public enum TradeScreenMode
		{
			// Token: 0x04007384 RID: 29572
			Trade,
			// Token: 0x04007385 RID: 29573
			Container
		}

		// Token: 0x02001DA5 RID: 7589
		public enum OfferStatus
		{
			// Token: 0x04007387 RID: 29575
			NEXT,
			// Token: 0x04007388 RID: 29576
			REFRESH,
			// Token: 0x04007389 RID: 29577
			TOP,
			// Token: 0x0400738A RID: 29578
			CLOSE
		}
	}
}
