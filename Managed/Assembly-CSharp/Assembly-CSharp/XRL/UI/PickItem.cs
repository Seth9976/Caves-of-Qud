using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using Qud.API;
using Qud.UI;
using XRL.Core;
using XRL.World;
using XRL.World.Capabilities;

namespace XRL.UI
{
	// Token: 0x020014A3 RID: 5283
	public class PickItem
	{
		// Token: 0x0600CF75 RID: 53109 RVA: 0x004BDE84 File Offset: 0x004BC084
		public void ClearLists()
		{
			if (this.CategoryMap == null)
			{
				return;
			}
			this.ExpandState = new Dictionary<string, bool>(this.CategoryMap.Count);
			foreach (string text in this.CategoryMap.Keys)
			{
				this.ExpandState.Add(this.CategoryList[text].Name, true);
			}
			this.CategoryMap.Clear();
			this.SelectionList.Clear();
			this.CategorySelectionList.Clear();
			this.CategoryList.Clear();
			this.SortList.Clear();
		}

		// Token: 0x0600CF76 RID: 53110 RVA: 0x004BDF48 File Offset: 0x004BC148
		public string GetObjectCategory(GameObject go)
		{
			string inventoryCategory;
			if (this.ObjectCategories.TryGetValue(go, out inventoryCategory))
			{
				return inventoryCategory;
			}
			inventoryCategory = go.GetInventoryCategory(false);
			this.ObjectCategories.Add(go, inventoryCategory);
			return inventoryCategory;
		}

		// Token: 0x0600CF77 RID: 53111 RVA: 0x004BDF80 File Offset: 0x004BC180
		public int RebuildLists(IList<GameObject> Items, string CategoryPrioritySpec, bool PreserveOrder = false)
		{
			PickItem.SortGOCategory sortGOCategory = new PickItem.SortGOCategory(this, new List<GameObject>(Items), PreserveOrder);
			int num = 0;
			int num2 = 18;
			List<string> list = null;
			if (CategoryPrioritySpec != null)
			{
				list = new List<string>(CategoryPrioritySpec.CachedCommaExpansion());
			}
			this.CategoryMap.Clear();
			this.SelectionList.Clear();
			if (!this.Categories.Contains("Category"))
			{
				this.Categories.Add("Category");
			}
			int i = 0;
			int count = Items.Count;
			while (i < count)
			{
				GameObject gameObject = Items[i];
				gameObject.Seen();
				string objectCategory = this.GetObjectCategory(gameObject);
				if (!this.CategoryList.ContainsKey(objectCategory))
				{
					InventoryCategory inventoryCategory = new InventoryCategory(objectCategory, false);
					if (CategoryPrioritySpec != null)
					{
						inventoryCategory.Expanded = list.Contains(objectCategory);
					}
					this.CategoryList.Add(objectCategory, inventoryCategory);
					this.Categories.Add(objectCategory);
				}
				if (!this.CategoryMap.ContainsKey(objectCategory))
				{
					this.CategoryMap.Add(objectCategory, new List<GameObject>());
				}
				this.CategoryMap[objectCategory].Add(gameObject);
				i++;
			}
			if (this.Categories[0] == "Category")
			{
				this.SortList.Clear();
				this.SortList.AddRange(Items);
				this.SortList.Sort(sortGOCategory);
			}
			else if (this.CategoryMap.ContainsKey(this.Categories[0]))
			{
				this.SortList.Clear();
				this.SortList.AddRange(this.CategoryMap[this.Categories[0]]);
			}
			int num3 = 0;
			this.bMore = false;
			if (this.Categories[0] == "Category")
			{
				this.CategorySelectionList.Clear();
				int num4 = 0;
				char c = 'a';
				this.CatNames.Clear();
				foreach (string text in this.CategoryList.Keys)
				{
					this.CatNames.Add(text);
				}
				this.CatNames.Sort();
				using (List<string>.Enumerator enumerator2 = this.CatNames.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						string text2 = enumerator2.Current;
						InventoryCategory inventoryCategory2 = this.CategoryList[text2];
						if (num4 >= this.StartObject && num4 <= num2 + this.StartObject)
						{
							this.CategorySelectionList.Add(c, new CategorySelectionListEntry(inventoryCategory2));
							c += '\u0001';
							num3++;
						}
						if (inventoryCategory2.Expanded && this.CategoryMap.ContainsKey(inventoryCategory2.Name))
						{
							foreach (GameObject gameObject2 in this.CategoryMap[inventoryCategory2.Name])
							{
								num4++;
								num3++;
								if (num4 >= this.StartObject && num4 <= num2 + this.StartObject)
								{
									if (num == 0)
									{
										num = num3 - 1;
									}
									this.CategorySelectionList.Add(c, new CategorySelectionListEntry(gameObject2));
									c += '\u0001';
								}
								else if (num4 > num2 + this.StartObject)
								{
									this.bMore = true;
									goto IL_04C2;
								}
							}
						}
						if (this.CategoryList.ContainsKey(text2))
						{
							this.CategoryList[text2].Weight = 0;
						}
						this.CategoryList[text2].Items = 0;
						if (this.CategoryMap.ContainsKey(inventoryCategory2.Name))
						{
							foreach (GameObject gameObject3 in this.CategoryMap[inventoryCategory2.Name])
							{
								this.CategoryList[text2].Weight += gameObject3.Weight;
								this.CategoryList[text2].Items++;
							}
						}
						if (num4 > num2 + this.StartObject)
						{
							this.bMore = true;
							break;
						}
						num4++;
					}
					goto IL_04C2;
				}
			}
			int num5 = 0;
			char c2 = 'a';
			foreach (GameObject gameObject4 in this.SortList)
			{
				if (num5 >= this.StartObject && num5 <= num2 + this.StartObject)
				{
					this.SelectionList.Add(c2, gameObject4);
					c2 += '\u0001';
				}
				num5++;
				if (num5 > num2 + this.StartObject)
				{
					this.bMore = true;
					break;
				}
			}
			IL_04C2:
			List<string> list2 = new List<string>();
			foreach (string text3 in this.CategoryList.Keys)
			{
				if (!this.CategoryMap.ContainsKey(text3))
				{
					list2.Add(text3);
				}
				else if (this.CategoryMap[text3].Count == 0)
				{
					list2.Add(text3);
				}
			}
			foreach (string text4 in list2)
			{
				if (this.CategoryList.ContainsKey(text4))
				{
					this.CategoryList.Remove(text4);
				}
				if (this.CategoryMap.ContainsKey(text4))
				{
					this.CategoryMap.Remove(text4);
				}
			}
			return num;
		}

		// Token: 0x0600CF78 RID: 53112 RVA: 0x004BE5D8 File Offset: 0x004BC7D8
		public static GameObject ShowPicker(IList<GameObject> Items, ref bool RequestInterfaceExit, string CategoryPriority = null, PickItem.PickItemDialogStyle Style = PickItem.PickItemDialogStyle.SelectItemDialog, GameObject Actor = null, GameObject Container = null, Cell Cell = null, string Title = null, bool PreserveOrder = false, Func<List<GameObject>> Regenerate = null, bool ShowContext = false, bool ShowIcons = true, bool NotePlayerOwned = false)
		{
			return new PickItem().ShowPickerInternal(Items, ref RequestInterfaceExit, CategoryPriority, Style, Actor, Container, Cell, false, Title, PreserveOrder, 42, 78, Regenerate, ShowContext, ShowIcons, NotePlayerOwned);
		}

		// Token: 0x0600CF79 RID: 53113 RVA: 0x004BE60C File Offset: 0x004BC80C
		public static GameObject ShowPicker(IList<GameObject> Items, string CategoryPriority = null, PickItem.PickItemDialogStyle Style = PickItem.PickItemDialogStyle.SelectItemDialog, GameObject Actor = null, GameObject Container = null, Cell Cell = null, string Title = null, bool PreserveOrder = false, Func<List<GameObject>> Regenerate = null, bool ShowContext = false, bool ShowIcons = true, bool NotePlayerOwned = false)
		{
			bool flag = false;
			return new PickItem().ShowPickerInternal(Items, ref flag, CategoryPriority, Style, Actor, Container, Cell, false, Title, PreserveOrder, 42, 78, Regenerate, ShowContext, ShowIcons, NotePlayerOwned);
		}

		// Token: 0x0600CF7A RID: 53114 RVA: 0x004BE640 File Offset: 0x004BC840
		public GameObject ShowPickerInternal(IList<GameObject> Items, ref bool RequestInterfaceExit, string CategoryPriority = null, PickItem.PickItemDialogStyle Style = PickItem.PickItemDialogStyle.SelectItemDialog, GameObject Actor = null, GameObject Container = null, Cell Cell = null, bool KeepSel = false, string Title = null, bool PreserveOrder = false, int MinWidth = 42, int MaxWidth = 78, Func<List<GameObject>> Regenerate = null, bool ShowContext = false, bool ShowIcons = true, bool NotePlayerOwned = false)
		{
			string text = Title ?? ((Style == PickItem.PickItemDialogStyle.StoreItemDialog) ? "{{W|Select an item to store}}" : "{{W|Select an item}}");
			if (Options.ModernUI)
			{
				bool flag = false;
				while (Items != null && Items.Count != 0)
				{
					if (Style == PickItem.PickItemDialogStyle.GetItemDialog)
					{
						PickGameObjectScreen.Result result = PickGameObjectScreen.show(Items, text, Style, Actor, Container, Cell, PreserveOrder, CategoryPriority, ShowContext, NotePlayerOwned, flag).Result;
						flag = true;
						GameObject item = result.item;
						RequestInterfaceExit = result.requestInterfaceExit;
						if (result.wantToStore && Actor.Inventory != null)
						{
							TradeUI.ShowTradeScreen(Container, 0f, TradeUI.TradeScreenMode.Container);
							return null;
						}
						if (item == null)
						{
							return null;
						}
						EquipmentAPI.TwiddleObject(Actor, item, ref RequestInterfaceExit, false, false, false);
						if (RequestInterfaceExit)
						{
							return null;
						}
						Items = Regenerate();
					}
					else if (Style == PickItem.PickItemDialogStyle.StoreItemDialog)
					{
						PickGameObjectScreen.Result result2 = PickGameObjectScreen.show(Items, text, Style, Actor, Container, Cell, PreserveOrder, CategoryPriority, ShowContext, NotePlayerOwned, flag).Result;
						GameObject item2 = result2.item;
						RequestInterfaceExit = result2.requestInterfaceExit;
						if (item2 == null)
						{
							return null;
						}
						return item2;
					}
					else
					{
						if (Style != PickItem.PickItemDialogStyle.SelectItemDialog)
						{
							goto IL_0136;
						}
						PickGameObjectScreen.Result result3 = PickGameObjectScreen.show(Items, text, Style, Actor, Container, Cell, PreserveOrder, CategoryPriority, ShowContext, NotePlayerOwned, flag).Result;
						GameObject item3 = result3.item;
						RequestInterfaceExit = result3.requestInterfaceExit;
						if (item3 == null)
						{
							return null;
						}
						return item3;
					}
				}
				return null;
			}
			IL_0136:
			StringBuilder stringBuilder = new StringBuilder();
			GameManager.Instance.PushGameView("Popup:Item", true);
			TextConsole.LoadScrapBuffers();
			ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
			ScreenBuffer scrapBuffer2 = TextConsole.ScrapBuffer2;
			StringBuilder stringBuilder2 = new StringBuilder(256);
			bool flag2 = false;
			GameObject gameObject = null;
			this.StartObject = 0;
			int num = -1;
			if (KeepSel)
			{
				num = this.LastSelected;
			}
			Dictionary<char, int> dictionary = new Dictionary<char, int>();
			Event.PinCurrentPool();
			this.RebuildLists(Items, CategoryPriority, PreserveOrder);
			while (!flag2)
			{
				int num8;
				InventoryCategory inventoryCategory;
				GameObject gameObject3;
				for (;;)
				{
					Event.ResetToPin();
					if (Items.Count == 0)
					{
						goto IL_0FD3;
					}
					int num2 = this.RebuildLists(Items, CategoryPriority, PreserveOrder);
					if (num == -1)
					{
						if (KeepSel && this.LastSelected != -1)
						{
							num = this.LastSelected - 1;
						}
						else
						{
							num = num2;
						}
					}
					this.LastSelected = num;
					CategoryPriority = null;
					scrapBuffer.Copy(scrapBuffer2);
					int num3 = MinWidth;
					int num4 = ColorUtility.LengthExceptFormatting("[ " + text + " ]") + 6;
					if (num4 > num3)
					{
						num3 = Math.Min(num4, MaxWidth);
					}
					if (num3 < MaxWidth)
					{
						foreach (List<GameObject> list in this.CategoryMap.Values)
						{
							foreach (GameObject gameObject2 in list)
							{
								int num5 = ColorUtility.LengthExceptFormatting(gameObject2.DisplayName) + 11;
								if (NotePlayerOwned && gameObject2.OwnedByPlayer)
								{
									num5 += 15;
								}
								if (ShowIcons)
								{
									num5 += 2;
								}
								if (ShowContext)
								{
									string listDisplayContext = gameObject2.GetListDisplayContext(Actor);
									if (!string.IsNullOrEmpty(listDisplayContext))
									{
										num5 += ColorUtility.LengthExceptFormatting(listDisplayContext) + 3;
									}
								}
								if (num5 >= MaxWidth)
								{
									num3 = MaxWidth;
									break;
								}
								if (gameObject2.IsTakeable())
								{
									num5 += gameObject2.Weight.ToString().Length + 2;
									if (num5 >= MaxWidth)
									{
										num3 = MaxWidth;
										break;
									}
								}
								if (num5 > num3)
								{
									num3 = num5;
								}
							}
							if (num3 >= MaxWidth)
							{
								break;
							}
						}
					}
					if (num3 % 2 == 1)
					{
						num3++;
						if (num3 > MaxWidth)
						{
							num3 = MaxWidth - 2;
						}
					}
					int num6 = (80 - num3) / 2;
					int num7 = num3 - 4;
					scrapBuffer.Fill(num6, 2, 79 - num6, 22, 32, ColorUtility.MakeColor(TextColor.Black, TextColor.Black), false, false);
					scrapBuffer.SingleBox(num6, 2, 79 - num6, 22, ColorUtility.MakeColor(TextColor.Grey, TextColor.Black));
					scrapBuffer.Goto(num6 + 5, 2);
					scrapBuffer.Write("[ " + text + " ]", true, false, false, null, -1);
					if (Style == PickItem.PickItemDialogStyle.GetItemDialog)
					{
						scrapBuffer.Goto(20, 22);
						scrapBuffer.Write("[{{W|" + ControlManager.getCommandInputDescription("Take All", false, false, ControlManager.InputDeviceType.Unknown, false) + "}}] {{Y|Take all}}", true, false, false, null, -1);
						if (Actor != Container && Actor != null && Container != null)
						{
							scrapBuffer.Goto(37, 22);
							scrapBuffer.Write("[" + ControlManager.getCommandInputFormatted("Store Items", false, false) + "] {{Y|Store an item}}", true, false, false, null, -1);
						}
					}
					if (this.StartObject > 0)
					{
						scrapBuffer.Goto(42, 2);
						scrapBuffer.Write("<{{W|8}} to scroll up>", true, false, false, null, -1);
					}
					num8 = 0;
					dictionary.Clear();
					inventoryCategory = null;
					gameObject3 = null;
					stringBuilder.Length = 0;
					int num9 = 3;
					int num10 = num6 + 2;
					foreach (char c in this.CategorySelectionList.Keys)
					{
						if (this.CategorySelectionList[c].Category != null)
						{
							scrapBuffer.Goto(num10, num9 + num8);
							string text2;
							if (num8 == num)
							{
								text2 = "{{Y|>}}";
								inventoryCategory = this.CategorySelectionList[c].Category;
							}
							else
							{
								text2 = " ";
							}
							if (this.CategorySelectionList[c].Category.Expanded)
							{
								if (num8 == num)
								{
									scrapBuffer.Write(string.Concat(new string[]
									{
										text2,
										c.ToString(),
										") [-] {{K|[{{Y|",
										this.CategorySelectionList[c].Category.Name,
										"}}]}}"
									}), true, false, false, null, -1);
								}
								else
								{
									scrapBuffer.Write(string.Concat(new string[]
									{
										text2,
										c.ToString(),
										") [-] {{K|[{{|",
										this.CategorySelectionList[c].Category.Name,
										"}}]}}"
									}), true, false, false, null, -1);
								}
							}
							else if (num8 == num)
							{
								scrapBuffer.Write(string.Concat(new string[]
								{
									text2,
									c.ToString(),
									") [+] {{K|[{{Y|",
									this.CategorySelectionList[c].Category.Name,
									"}}]}}"
								}), true, false, false, null, -1);
							}
							else
							{
								scrapBuffer.Write(string.Concat(new string[]
								{
									text2,
									c.ToString(),
									") [+] {{K|[{{|",
									this.CategorySelectionList[c].Category.Name,
									"}}]}}"
								}), true, false, false, null, -1);
							}
							dictionary.Add(c, num8);
							num8++;
						}
						else
						{
							int num11 = 6;
							string text3;
							char c2;
							if (num8 == num)
							{
								text3 = "{{Y|>}}  ";
								c2 = 'y';
								gameObject3 = this.CategorySelectionList[c].Object;
							}
							else
							{
								text3 = "   ";
								c2 = 'K';
							}
							scrapBuffer.Goto(num10, num9 + num8);
							scrapBuffer.Write(text3 + c.ToString() + ") ", true, false, false, null, -1);
							GameObject @object = this.CategorySelectionList[c].Object;
							if (ShowIcons)
							{
								IRenderable renderable = @object.RenderForUI(null, false);
								if (renderable != null)
								{
									scrapBuffer.Write(renderable);
									num11 += 2;
								}
							}
							stringBuilder.Clear().Append(@object.DisplayName);
							if (NotePlayerOwned && @object.OwnedByPlayer)
							{
								stringBuilder.Append(" {{G|[owned by you]}}");
							}
							if (ShowContext)
							{
								string listDisplayContext2 = @object.GetListDisplayContext(Actor);
								if (!string.IsNullOrEmpty(listDisplayContext2))
								{
									stringBuilder.Append(" [").Append(listDisplayContext2).Append(']');
								}
							}
							StringBuilder stringBuilder3 = Event.NewStringBuilder(null);
							if (@object.IsTakeable())
							{
								stringBuilder3.Length = 0;
								stringBuilder3.Append(" {{").Append(c2).Append('|')
									.Append(@object.Weight)
									.Append("#}}");
								Markup.Transform(stringBuilder3, false);
								int num12 = ColorUtility.LengthExceptFormatting(stringBuilder3);
								StringFormat.ClipLine(stringBuilder, num7 - num12 - num11, true, stringBuilder2);
								scrapBuffer.Goto(num10 + num11, num9 + num8);
								scrapBuffer.Write(stringBuilder2, -1);
								scrapBuffer.Goto(num10 + num7 - num12, num9 + num8);
								scrapBuffer.Write(stringBuilder3, -1);
							}
							else
							{
								scrapBuffer.Goto(num10 + num11, num9 + num8);
								StringFormat.ClipLine(stringBuilder, num7, true, stringBuilder2);
								scrapBuffer.Write(stringBuilder2, -1);
							}
							dictionary.Add(c, num8);
							num8++;
						}
					}
					if (num8 == 0 && this.StartObject != 0)
					{
						this.StartObject = 0;
					}
					else
					{
						if (num < num8)
						{
							break;
						}
						num = num8 - 1;
					}
				}
				if (this.bMore)
				{
					scrapBuffer.Goto(42, 22);
					scrapBuffer.Write("<{{W|2}} to scroll down>", true, false, false, null, -1);
				}
				if (Style == PickItem.PickItemDialogStyle.GetItemDialog)
				{
					stringBuilder.Length = 0;
					stringBuilder.Append("Total weight: {{Y|").Append(EquipmentAPI.GetPlayerCurrentCarryWeight()).Append(" {{y|/}}  ")
						.Append(EquipmentAPI.GetPlayerMaxCarryWeight())
						.Append(" lbs.}}");
					scrapBuffer.Goto(79 - ColorUtility.LengthExceptFormatting(stringBuilder), 24);
					scrapBuffer.Write(stringBuilder.ToString(), true, false, false, null, -1);
				}
				Popup._TextConsole.DrawBuffer(scrapBuffer, null, false);
				if (!XRLCore.Core.Game.Running)
				{
					return null;
				}
				Keys keys = Keyboard.getvk(Options.MapDirectionsToKeypad, false, true);
				if (ShowIcons)
				{
					ScreenBuffer.ClearImposterSuppression();
				}
				char c3 = ("" + ((char)Keyboard.Char).ToString() + " ").ToLower()[0];
				if (keys == Keys.Enter)
				{
					keys = Keys.Space;
				}
				if (keys == Keys.Escape || keys == Keys.NumPad5 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "RightClick"))
				{
					break;
				}
				if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:Store Items" && Actor != Container && Actor != null && Container != null && Actor.Inventory != null)
				{
					TradeUI.ShowTradeScreen(Container, 0f, TradeUI.TradeScreenMode.Container);
					break;
				}
				if (keys == Keys.NumPad8)
				{
					if (num > 0)
					{
						num--;
					}
					else if (this.StartObject > 0)
					{
						this.StartObject--;
					}
				}
				else if (keys == Keys.NumPad2)
				{
					if (num < num8 - 1)
					{
						num++;
					}
					else if (this.bMore)
					{
						this.StartObject++;
					}
				}
				else if (keys == Keys.Next || keys == Keys.Next || keys == Keys.NumPad3 || Keyboard.RawCode == Keys.Next || Keyboard.RawCode == Keys.Next || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:Page Down"))
				{
					if (num < num8 - 1)
					{
						num = num8 - 1;
					}
					else if (this.bMore)
					{
						this.StartObject += 18;
					}
				}
				else if (keys == Keys.Prior || keys == Keys.Back || keys == Keys.NumPad9 || Keyboard.RawCode == Keys.Prior || Keyboard.RawCode == Keys.Back || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:Page Up"))
				{
					if (num > 0)
					{
						num = 0;
					}
					else
					{
						this.StartObject -= 18;
						if (this.StartObject < 0)
						{
							this.StartObject = 0;
						}
					}
				}
				else
				{
					if (keys == Keys.Subtract || keys == Keys.OemMinus || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:V Negative"))
					{
						using (Dictionary<string, InventoryCategory>.ValueCollection.Enumerator enumerator4 = this.CategoryList.Values.GetEnumerator())
						{
							while (enumerator4.MoveNext())
							{
								InventoryCategory inventoryCategory2 = enumerator4.Current;
								inventoryCategory2.Expanded = false;
							}
							goto IL_0FC1;
						}
					}
					if (keys == Keys.Add || keys == Keys.Oemplus || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:V Positive"))
					{
						using (Dictionary<string, InventoryCategory>.ValueCollection.Enumerator enumerator4 = this.CategoryList.Values.GetEnumerator())
						{
							while (enumerator4.MoveNext())
							{
								InventoryCategory inventoryCategory3 = enumerator4.Current;
								inventoryCategory3.Expanded = true;
							}
							goto IL_0FC1;
						}
					}
					if (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "ItemSelected")
					{
						if (Style == PickItem.PickItemDialogStyle.SelectItemDialog || Style == PickItem.PickItemDialogStyle.StoreItemDialog)
						{
							gameObject = Keyboard.CurrentMouseEvent.Data as GameObject;
							break;
						}
						gameObject3 = Keyboard.CurrentMouseEvent.Data as GameObject;
						keys = Keys.Space;
					}
					if (Style == PickItem.PickItemDialogStyle.SelectItemDialog || Style == PickItem.PickItemDialogStyle.StoreItemDialog)
					{
						if (keys >= Keys.A && keys <= Keys.Z)
						{
							if (this.CategorySelectionList.ContainsKey(c3) && this.CategorySelectionList[c3].Object != null)
							{
								gameObject = this.CategorySelectionList[c3].Object;
								break;
							}
						}
						else if (gameObject3 != null && (keys == Keys.Space || keys == Keys.Enter))
						{
							gameObject = gameObject3;
							break;
						}
					}
					else if (Style == PickItem.PickItemDialogStyle.GetItemDialog)
					{
						if (gameObject3 != null && (keys == Keys.Space || keys == Keys.Enter))
						{
							Cell currentCell = gameObject3.CurrentCell;
							GameObject equipped = gameObject3.Equipped;
							GameObject inInventory = gameObject3.InInventory;
							EquipmentAPI.TwiddleObject(Actor, gameObject3, ref RequestInterfaceExit, false, false, false);
							if (RequestInterfaceExit)
							{
								break;
							}
							if (Regenerate != null)
							{
								Items = Regenerate();
							}
							else if (currentCell != gameObject3.CurrentCell || equipped != gameObject3.Equipped || inInventory != gameObject3.InInventory)
							{
								Items.Remove(gameObject3);
							}
							if (Items.Count == 0)
							{
								break;
							}
						}
						else if (keys >= Keys.A && keys <= Keys.Z)
						{
							if (this.CategorySelectionList.ContainsKey(c3) && this.CategorySelectionList[c3].Object != null)
							{
								Cell currentCell2 = this.CategorySelectionList[c3].Object.CurrentCell;
								GameObject equipped2 = this.CategorySelectionList[c3].Object.Equipped;
								GameObject inInventory2 = this.CategorySelectionList[c3].Object.InInventory;
								EquipmentAPI.TwiddleObject(Actor, this.CategorySelectionList[c3].Object, ref RequestInterfaceExit, false, false, false);
								if (RequestInterfaceExit)
								{
									break;
								}
								if (Regenerate != null)
								{
									Items = Regenerate();
								}
								else if (currentCell2 != this.CategorySelectionList[c3].Object.CurrentCell || equipped2 != this.CategorySelectionList[c3].Object.Equipped || inInventory2 != this.CategorySelectionList[c3].Object.InInventory)
								{
									Items.Remove(this.CategorySelectionList[c3].Object);
								}
								if (Items.Count == 0)
								{
									break;
								}
							}
						}
						else if ((keys == Keys.Tab || keys == Keys.F1 || (keys == Keys.MouseEvent && Keyboard.CurrentMouseEvent.Event == "Command:Take All")) && PickItem.TakeAll(Actor, Container, Cell, Items, ref RequestInterfaceExit))
						{
							GameManager.Instance.PopGameView(true);
							return null;
						}
					}
					if (inventoryCategory != null)
					{
						if (keys == Keys.NumPad4)
						{
							inventoryCategory.Expanded = false;
						}
						else if (keys == Keys.NumPad6)
						{
							inventoryCategory.Expanded = true;
						}
						else if (keys == Keys.Space || keys == Keys.Enter)
						{
							inventoryCategory.Expanded = !inventoryCategory.Expanded;
						}
					}
					if (keys >= Keys.A && keys <= Keys.Z && this.CategorySelectionList.ContainsKey(c3))
					{
						num = dictionary[c3];
						if (this.CategorySelectionList.ContainsKey(c3) && this.CategorySelectionList[c3].Category != null)
						{
							this.CategorySelectionList[c3].Category.Expanded = !this.CategorySelectionList[c3].Category.Expanded;
						}
					}
				}
				IL_0FC1:
				this.RebuildLists(Items, CategoryPriority, PreserveOrder);
			}
			IL_0FD3:
			this.ClearLists();
			GameManager.Instance.PopGameView(true);
			return gameObject;
		}

		// Token: 0x0600CF7B RID: 53115 RVA: 0x004BF6B0 File Offset: 0x004BD8B0
		public static bool TakeAll(GameObject Actor, GameObject Container, Cell Cell, IList<GameObject> Items, ref bool RequestInterfaceExit)
		{
			if (Actor != null && Actor.IsPlayer() && (Container == null || Container.GetObjectContext() != Actor))
			{
				int num = 0;
				int num2 = 0;
				foreach (GameObject gameObject in Items)
				{
					if (gameObject.ShouldTakeAll())
					{
						num += gameObject.Weight;
						num2 += gameObject.Count;
					}
				}
				int maxCarriedWeight = Actor.GetMaxCarriedWeight();
				int carriedWeight = Actor.GetCarriedWeight();
				if (carriedWeight <= maxCarriedWeight && carriedWeight + num > maxCarriedWeight && Popup.ShowYesNo("Taking " + ((num2 > 2) ? "all these objects" : ((num2 == 2) ? "these objects" : "this object")) + " will put you over your weight limit. Are you sure you want to do it?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
				{
					return false;
				}
			}
			if (Container != null && Actor.IsPlayer() && Container.InSameOrAdjacentCellTo(Actor))
			{
				if (AutoAct.ShouldHostilesInterrupt("g", null, false, true, true))
				{
					return false;
				}
				AutoAct.ResumeSetting = AutoAct.Setting;
				AutoAct.Setting = "go" + Container.ID;
				The.ActionManager.SkipPlayerTurn = true;
				RequestInterfaceExit = true;
			}
			else if (Cell != null && Actor.IsPlayer() && Cell.IsSameOrAdjacent(Actor.CurrentCell, true))
			{
				if (AutoAct.ShouldHostilesInterrupt("g", null, false, true, true))
				{
					return false;
				}
				AutoAct.ResumeSetting = AutoAct.Setting;
				AutoAct.Setting = "gd" + Actor.CurrentCell.GetDirectionFromCell(Cell, false);
				The.ActionManager.SkipPlayerTurn = true;
				RequestInterfaceExit = true;
			}
			else
			{
				foreach (GameObject gameObject2 in Items)
				{
					if (gameObject2.ShouldTakeAll())
					{
						Actor.TakeObject(gameObject2, false, false, null, "TakeAll", null);
					}
				}
			}
			return true;
		}

		// Token: 0x040050A8 RID: 20648
		private Dictionary<char, GameObject> SelectionList = new Dictionary<char, GameObject>();

		// Token: 0x040050A9 RID: 20649
		private Dictionary<char, CategorySelectionListEntry> CategorySelectionList = new Dictionary<char, CategorySelectionListEntry>();

		// Token: 0x040050AA RID: 20650
		private Dictionary<string, List<GameObject>> CategoryMap = new Dictionary<string, List<GameObject>>();

		// Token: 0x040050AB RID: 20651
		private Dictionary<string, bool> ExpandState = new Dictionary<string, bool>();

		// Token: 0x040050AC RID: 20652
		private Dictionary<string, InventoryCategory> CategoryList = new Dictionary<string, InventoryCategory>();

		// Token: 0x040050AD RID: 20653
		private List<GameObject> SortList = new List<GameObject>();

		// Token: 0x040050AE RID: 20654
		private List<string> Categories = new List<string>();

		// Token: 0x040050AF RID: 20655
		private List<string> CatNames = new List<string>();

		// Token: 0x040050B0 RID: 20656
		private int StartObject;

		// Token: 0x040050B1 RID: 20657
		private bool bMore;

		// Token: 0x040050B2 RID: 20658
		private Dictionary<GameObject, string> ObjectCategories = new Dictionary<GameObject, string>();

		// Token: 0x040050B3 RID: 20659
		private int LastSelected = -1;

		// Token: 0x02001DAB RID: 7595
		public class SortGOCategory : Comparer<GameObject>
		{
			// Token: 0x0600F4F0 RID: 62704 RVA: 0x00554448 File Offset: 0x00552648
			public SortGOCategory(PickItem Picker, List<GameObject> Items, bool PreserveOrder)
			{
				this.Picker = Picker;
				this.Items = Items;
				this.PreserveOrder = PreserveOrder;
			}

			// Token: 0x0600F4F1 RID: 62705 RVA: 0x00554465 File Offset: 0x00552665
			public override int Compare(GameObject x, GameObject y)
			{
				return x.SortVs(y, this.Picker.ObjectCategories, true, !this.PreserveOrder, !this.PreserveOrder, false, this.Items);
			}

			// Token: 0x0400739C RID: 29596
			private PickItem Picker;

			// Token: 0x0400739D RID: 29597
			private List<GameObject> Items;

			// Token: 0x0400739E RID: 29598
			private bool PreserveOrder;
		}

		// Token: 0x02001DAC RID: 7596
		public enum PickItemDialogStyle
		{
			// Token: 0x040073A0 RID: 29600
			SelectItemDialog,
			// Token: 0x040073A1 RID: 29601
			StoreItemDialog,
			// Token: 0x040073A2 RID: 29602
			GetItemDialog
		}
	}
}
