using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using ConsoleLib.Console;
using FuzzySharp;
using FuzzySharp.Extractor;
using ModelShark;
using Qud.API;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XRL;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;
using XRL.World.Anatomy;

namespace Qud.UI
{
	// Token: 0x02000468 RID: 1128
	[UIView("InventoryAndEquipmentStatusScreen", false, false, false, null, null, false, 0, false, NavCategory = "Menus", UICanvas = "StatusScreens", UICanvasHost = 1)]
	public class InventoryAndEquipmentStatusScreen : BaseStatusScreen<InventoryAndEquipmentStatusScreen>
	{
		// Token: 0x06002ABC RID: 10940 RVA: 0x000E1BDB File Offset: 0x000DFDDB
		public override IRenderable GetTabIcon()
		{
			if (this.icon == null)
			{
				this.icon = new Renderable("Tiles/sw_chest.bmp", " ", "&w", null, 'W');
			}
			return this.icon;
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x000E1C08 File Offset: 0x000DFE08
		public override string GetTabString()
		{
			if (Media.sizeClass < Media.SizeClass.Medium)
			{
				return "Eq";
			}
			return "Equipment";
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x000E1C1E File Offset: 0x000DFE1E
		public override bool WantsCategoryBar()
		{
			return true;
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x000E1C24 File Offset: 0x000DFE24
		public async void HandleQuickDrop(FrameworkDataElement element)
		{
			bool bDone = false;
			InventoryLineData inventoryLine = element as InventoryLineData;
			if (inventoryLine != null && !inventoryLine.category)
			{
				await APIDispatch.RunAndWaitAsync(delegate
				{
					Zone currentZone = this.GO.CurrentZone;
					if (currentZone == null || !currentZone.IsWorldMap())
					{
						InventoryActionEvent.Check(ref bDone, this.GO, this.GO, inventoryLine.go, "CommandDropObject", false, false, false, false, false, 0, 0, 0, null, null, null, null);
					}
				});
				if (bDone)
				{
					this.statusScreensScreen.Exit();
				}
				else
				{
					this.UpdateViewFromData();
				}
			}
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x000E1C64 File Offset: 0x000DFE64
		public async void HandleQuickApply(FrameworkDataElement element)
		{
			bool bDone = false;
			InventoryLineData inventoryLine = element as InventoryLineData;
			if (inventoryLine != null && !inventoryLine.category)
			{
				IEvent sentEvent = null;
				await APIDispatch.RunAndWaitAsync(delegate
				{
					InventoryActionEvent.Check(out sentEvent, inventoryLine.go, this.GO, inventoryLine.go, "Apply", false, false, false, false, false, 0, 0, 0, null, null, null, null);
				});
				if (bDone)
				{
					this.statusScreensScreen.Exit();
				}
				else
				{
					this.UpdateViewFromData();
				}
			}
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x000E1CA4 File Offset: 0x000DFEA4
		public async void HandleQuickDrink(FrameworkDataElement element)
		{
			bool bDone = false;
			InventoryLineData inventoryLine = element as InventoryLineData;
			if (inventoryLine != null && !inventoryLine.category)
			{
				IEvent sentEvent = null;
				await APIDispatch.RunAndWaitAsync(delegate
				{
					InventoryActionEvent.Check(out sentEvent, inventoryLine.go, this.GO, inventoryLine.go, "Drink", false, false, false, false, false, 0, 0, 0, null, null, null, null);
				});
				if (bDone)
				{
					this.statusScreensScreen.Exit();
				}
				else
				{
					this.UpdateViewFromData();
				}
			}
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x000E1CE4 File Offset: 0x000DFEE4
		public async void HandleQuickEat(FrameworkDataElement element)
		{
			bool bDone = false;
			InventoryLineData inventoryLine = element as InventoryLineData;
			if (inventoryLine != null && !inventoryLine.category)
			{
				IEvent sentEvent = null;
				await APIDispatch.RunAndWaitAsync(delegate
				{
					InventoryActionEvent.Check(out sentEvent, inventoryLine.go, this.GO, inventoryLine.go, "Eat", false, false, false, false, false, 0, 0, 0, null, null, null, null);
				});
				if (bDone)
				{
					this.statusScreensScreen.Exit();
				}
				else
				{
					this.UpdateViewFromData();
				}
			}
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x000E1D24 File Offset: 0x000DFF24
		public async void HandleDragDropAutoequip(InventoryLineData item)
		{
			InventoryLineData item2 = item;
			if (((item2 != null) ? item2.go : null) != null)
			{
				await APIDispatch.RunAndWaitAsync(delegate
				{
					The.Player.AutoEquip(item.go, false, false, false);
				});
				The.Player.CheckStacks();
				this.UpdateViewFromData();
			}
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x000E1D64 File Offset: 0x000DFF64
		public async void HandleLookAtDefaultEquipmnent(EquipmentLineData from)
		{
			EquipmentLineData from2 = from;
			bool flag;
			if (from2 == null)
			{
				flag = null != null;
			}
			else
			{
				BodyPart bodyPart = from2.bodyPart;
				flag = ((bodyPart != null) ? bodyPart.DefaultBehavior : null) != null;
			}
			if (flag)
			{
				await APIDispatch.RunAndWaitAsync(delegate
				{
					EquipmentLineData from3 = from;
					global::XRL.World.GameObject gameObject;
					if (from3 == null)
					{
						gameObject = null;
					}
					else
					{
						BodyPart bodyPart2 = from3.bodyPart;
						gameObject = ((bodyPart2 != null) ? bodyPart2.DefaultBehavior : null);
					}
					global::XRL.World.GameObject go = this.GO;
					EquipmentLineData from4 = from;
					global::XRL.World.GameObject gameObject2;
					if (from4 == null)
					{
						gameObject2 = null;
					}
					else
					{
						BodyPart bodyPart3 = from4.bodyPart;
						gameObject2 = ((bodyPart3 != null) ? bodyPart3.DefaultBehavior : null);
					}
					InventoryActionEvent.Check(gameObject, go, gameObject2, "Look", false, false, false, false, false, 0, 0, 0, null, null, null, null);
				});
				The.Player.CheckStacks();
				this.UpdateViewFromData();
			}
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x000E1DA4 File Offset: 0x000DFFA4
		public async void HandleDragDrop(EquipmentLineData from, EquipmentLineData to)
		{
			if (to == null)
			{
				await APIDispatch.RunAndWaitAsync(delegate
				{
					EquipmentAPI.UnequipObject(from.bodyPart.Equipped);
				});
				EquipmentLine line = from.line;
				if (line != null)
				{
					line.GetNavigationContext().Activate(false);
				}
				The.Player.CheckStacks();
			}
			else
			{
				await APIDispatch.RunAndWaitAsync(delegate
				{
					EquipmentAPI.EquipObjectToPlayer(from.bodyPart.Equipped, to.bodyPart);
				});
				EquipmentLine line2 = to.line;
				if (line2 != null)
				{
					line2.GetNavigationContext().Activate(false);
				}
				The.Player.CheckStacks();
			}
			this.UpdateViewFromData();
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x000E1DEC File Offset: 0x000DFFEC
		public async void HandleDragDrop(InventoryLineData from, EquipmentLineData to)
		{
			await APIDispatch.RunAndWaitAsync(delegate
			{
				EquipmentAPI.EquipObjectToPlayer(from.go, to.bodyPart);
				The.Player.CheckStacks();
			});
			this.UpdateViewFromData();
			EquipmentLine line = to.line;
			if (line != null)
			{
				line.GetNavigationContext().Activate(false);
			}
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x000E1E34 File Offset: 0x000E0034
		public void SetCategoryExpanded(string categoryName, bool state)
		{
			state = !state;
			if (!this.categoryCollapsed.ContainsKey(categoryName))
			{
				this.categoryCollapsed.Add(categoryName, false);
			}
			if (this.categoryCollapsed[categoryName] != state)
			{
				this.categoryCollapsed[categoryName] = state;
				this.UpdateViewFromData();
			}
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x000E1E84 File Offset: 0x000E0084
		public async void HandleSelectItem(FrameworkDataElement element)
		{
			InventoryAndEquipmentStatusScreen.<>c__DisplayClass24_0 CS$<>8__locals1 = new InventoryAndEquipmentStatusScreen.<>c__DisplayClass24_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.inventoryLine = element as InventoryLineData;
			if (CS$<>8__locals1.inventoryLine != null)
			{
				if (CS$<>8__locals1.inventoryLine.category)
				{
					this.categoryCollapsed[CS$<>8__locals1.inventoryLine.categoryName] = !this.isCollapsed(CS$<>8__locals1.inventoryLine.categoryName);
					this.UpdateViewFromData();
				}
				else
				{
					if (TutorialManager.AllowInventoryInteract(CS$<>8__locals1.inventoryLine.go))
					{
						bool bDone = false;
						InventoryAction resultingAction = null;
						await APIDispatch.RunAndWaitAsync(delegate
						{
							EquipmentAPI.TwiddleObject(The.Player, CS$<>8__locals1.inventoryLine.go, ref bDone, out resultingAction, false, false, false);
						});
						if (resultingAction != null && resultingAction.Command == "Mod")
						{
							TinkeringStatusScreen.StartupModItemWithTinkering(CS$<>8__locals1.inventoryLine.go);
						}
						if (bDone)
						{
							this.statusScreensScreen.Exit();
						}
						else
						{
							this.UpdateViewFromData();
						}
					}
				}
			}
			else
			{
				CS$<>8__locals1.equipmentLine = element as EquipmentLineData;
				if (CS$<>8__locals1.equipmentLine != null)
				{
					InventoryAndEquipmentStatusScreen.<>c__DisplayClass24_2 CS$<>8__locals3 = new InventoryAndEquipmentStatusScreen.<>c__DisplayClass24_2();
					CS$<>8__locals3.CS$<>8__locals2 = CS$<>8__locals1;
					global::XRL.UI.Framework.Event currentEvent = NavigationController.currentEvent;
					bool flag = currentEvent != null && currentEvent.IsRightClick();
					CS$<>8__locals3.bDone = false;
					if (CS$<>8__locals3.CS$<>8__locals2.equipmentLine.showCybernetics)
					{
						if (CS$<>8__locals3.CS$<>8__locals2.equipmentLine.bodyPart.Cybernetics == null)
						{
							return;
						}
						await APIDispatch.RunAndWaitAsync(delegate
						{
							EquipmentAPI.TwiddleObject(CS$<>8__locals3.CS$<>8__locals2.<>4__this.GO, CS$<>8__locals3.CS$<>8__locals2.equipmentLine.bodyPart.Cybernetics, ref CS$<>8__locals3.bDone, false, false, false);
						});
					}
					else if (CS$<>8__locals3.CS$<>8__locals2.equipmentLine.bodyPart.Equipped != null)
					{
						InventoryAndEquipmentStatusScreen.<>c__DisplayClass24_3 CS$<>8__locals4 = new InventoryAndEquipmentStatusScreen.<>c__DisplayClass24_3();
						CS$<>8__locals4.CS$<>8__locals3 = CS$<>8__locals3;
						CS$<>8__locals4.resultingAction = null;
						await APIDispatch.RunAndWaitAsync(delegate
						{
							EquipmentAPI.TwiddleObject(CS$<>8__locals4.CS$<>8__locals3.CS$<>8__locals2.<>4__this.GO, CS$<>8__locals4.CS$<>8__locals3.CS$<>8__locals2.equipmentLine.bodyPart.Equipped, ref CS$<>8__locals4.CS$<>8__locals3.bDone, out CS$<>8__locals4.resultingAction, false, false, false);
						});
						if (CS$<>8__locals4.resultingAction != null && CS$<>8__locals4.resultingAction.Command == "Mod")
						{
							TinkeringStatusScreen.StartupModItemWithTinkering(CS$<>8__locals4.CS$<>8__locals3.CS$<>8__locals2.equipmentLine.bodyPart.Equipped);
						}
						if (CS$<>8__locals4.CS$<>8__locals3.bDone)
						{
							this.statusScreensScreen.Exit();
						}
						else
						{
							this.UpdateViewFromData();
						}
						CS$<>8__locals4 = null;
					}
					else
					{
						if (flag)
						{
							EquipmentLineData equipmentLine = CS$<>8__locals3.CS$<>8__locals2.equipmentLine;
							bool flag2;
							if (equipmentLine == null)
							{
								flag2 = null != null;
							}
							else
							{
								BodyPart bodyPart = equipmentLine.bodyPart;
								flag2 = ((bodyPart != null) ? bodyPart.DefaultBehavior : null) != null;
							}
							if (flag2)
							{
								await APIDispatch.RunAndWaitAsync(delegate
								{
									EquipmentLineData equipmentLine2 = CS$<>8__locals3.CS$<>8__locals2.equipmentLine;
									global::XRL.World.GameObject gameObject;
									if (equipmentLine2 == null)
									{
										gameObject = null;
									}
									else
									{
										BodyPart bodyPart2 = equipmentLine2.bodyPart;
										gameObject = ((bodyPart2 != null) ? bodyPart2.DefaultBehavior : null);
									}
									global::XRL.World.GameObject go = CS$<>8__locals3.CS$<>8__locals2.<>4__this.GO;
									EquipmentLineData equipmentLine3 = CS$<>8__locals3.CS$<>8__locals2.equipmentLine;
									global::XRL.World.GameObject gameObject2;
									if (equipmentLine3 == null)
									{
										gameObject2 = null;
									}
									else
									{
										BodyPart bodyPart3 = equipmentLine3.bodyPart;
										gameObject2 = ((bodyPart3 != null) ? bodyPart3.DefaultBehavior : null);
									}
									InventoryActionEvent.Check(gameObject, go, gameObject2, "Look", false, false, false, false, false, 0, 0, 0, null, null, null, null);
								});
								goto IL_04F8;
							}
						}
						await APIDispatch.RunAndWaitAsync(delegate
						{
							EquipmentScreen.ShowBodypartEquipUI(CS$<>8__locals3.CS$<>8__locals2.<>4__this.GO, CS$<>8__locals3.CS$<>8__locals2.equipmentLine.bodyPart);
						});
					}
					IL_04F8:
					if (CS$<>8__locals3.bDone)
					{
						this.statusScreensScreen.Exit();
					}
					else
					{
						this.UpdateViewFromData();
					}
					CS$<>8__locals3 = null;
				}
			}
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x000E1EC4 File Offset: 0x000E00C4
		public bool isCollapsed(string category)
		{
			bool flag;
			return this.categoryCollapsed.TryGetValue(category, out flag) && flag;
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x000E1EE4 File Offset: 0x000E00E4
		public override void UpdateViewFromData()
		{
			this.usedCategories.Clear();
			this.listItems.ForEach(delegate(InventoryLineData l)
			{
				l.free();
			});
			this.listItems.Clear();
			foreach (KeyValuePair<string, List<InventoryLineData>> keyValuePair in this.objectCategories)
			{
				keyValuePair.Value.Clear();
			}
			IEnumerable<InventoryLineData> enumerable;
			if (this.GO != null)
			{
				enumerable = from o in this.GO.Inventory.Objects
					where !o.HasTag("HiddenInInventory")
					select o into go
					select PooledFrameworkDataElement<InventoryLineData>.next().set(false, go.GetInventoryCategory(false), false, 0, 0, 0, go, this.inventorySpread, this);
			}
			else
			{
				IEnumerable<InventoryLineData> enumerable2 = new List<InventoryLineData>();
				enumerable = enumerable2;
			}
			IEnumerable<InventoryLineData> enumerable3 = enumerable;
			this.filterBarCategories.Clear();
			this.filterBarCategories.Add("*All");
			foreach (InventoryLineData inventoryLineData in enumerable3)
			{
				if (inventoryLineData.go != null)
				{
					string inventoryCategory = inventoryLineData.go.GetInventoryCategory(false);
					if (!this.filterBarCategories.Contains(inventoryCategory))
					{
						this.filterBarCategories.Add(inventoryCategory);
					}
				}
			}
			IEnumerable<InventoryLineData> enumerable4;
			if (this.filterText.IsNullOrEmpty() && this.filterBar.enabledCategories.Count == 1 && this.filterBar.enabledCategories[0] == "*All")
			{
				enumerable4 = enumerable3;
			}
			else if (this.filterText.IsNullOrEmpty())
			{
				enumerable4 = enumerable3.Where<InventoryLineData>((InventoryLineData i) => this.filterBar.enabledCategories.Contains("*All") || this.filterBar.enabledCategories.Contains(i.categoryName) || (i.go != null && this.filterBar.enabledCategories.Contains(i.categoryName))).ToList<InventoryLineData>();
			}
			else
			{
				enumerable4 = enumerable3.Where<InventoryLineData>((InventoryLineData i) => this.filterBar.enabledCategories.Contains("*All") || this.filterBar.enabledCategories.Contains(i.categoryName) || (i.go != null && this.filterBar.enabledCategories.Contains(i.categoryName))).ToList<InventoryLineData>();
				if (this.SearchMode == InventoryAndEquipmentStatusScreen.SEARCH_MODE_FUZZY)
				{
					IEnumerable<InventoryLineData> enumerable5;
					if (!this.filterText.IsNullOrEmpty())
					{
						enumerable5 = from m in Process.ExtractTop<InventoryLineData>(this.searcher, enumerable4, (InventoryLineData i) => i.sortString, null, enumerable4.Count<InventoryLineData>(), 50)
							select m.Value;
					}
					else
					{
						enumerable5 = enumerable3;
					}
					enumerable4 = enumerable5;
				}
				else
				{
					enumerable4 = enumerable4.Where<InventoryLineData>((InventoryLineData item) => item.sortString.Contains(this.searcher.sortString, CompareOptions.IgnoreCase));
				}
			}
			this.filterBar.SetCategories(this.filterBarCategories);
			foreach (string text in this.categoryWeight.Keys.ToList<string>())
			{
				this.categoryWeight[text] = 0;
			}
			foreach (string text2 in this.categoryAmount.Keys.ToList<string>())
			{
				this.categoryAmount[text2] = 0;
			}
			foreach (InventoryLineData inventoryLineData2 in enumerable4)
			{
				if (inventoryLineData2.go != null)
				{
					string inventoryCategory2 = inventoryLineData2.go.GetInventoryCategory(false);
					if (!this.objectCategories.ContainsKey(inventoryCategory2))
					{
						this.objectCategories.Add(inventoryCategory2, new List<InventoryLineData>());
					}
					if (!this.categoryWeight.ContainsKey(inventoryCategory2))
					{
						this.categoryWeight.Add(inventoryCategory2, 0);
					}
					if (!this.categoryAmount.ContainsKey(inventoryCategory2))
					{
						this.categoryAmount.Add(inventoryCategory2, 0);
					}
					Dictionary<string, int> dictionary = this.categoryAmount;
					string text3 = inventoryCategory2;
					int num = dictionary[text3];
					dictionary[text3] = num + 1;
					Dictionary<string, int> dictionary2 = this.categoryWeight;
					text3 = inventoryCategory2;
					dictionary2[text3] += inventoryLineData2.go.Weight;
					this.objectCategories[inventoryCategory2].Add(inventoryLineData2);
					if (!this.usedCategories.Contains(inventoryCategory2))
					{
						this.usedCategories.Add(inventoryCategory2);
					}
				}
			}
			this.usedCategories.Sort();
			if (this.sortMode == InventoryAndEquipmentStatusScreen.SortMode.Category)
			{
				int num2 = 0;
				using (List<string>.Enumerator enumerator3 = this.usedCategories.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						string text4 = enumerator3.Current;
						List<InventoryLineData> list = this.listItems;
						InventoryLineData inventoryLineData3 = PooledFrameworkDataElement<InventoryLineData>.next();
						bool flag = true;
						string text5 = text4;
						bool flag2 = !this.isCollapsed(text4);
						int num = (this.categoryWeight.ContainsKey(text4) ? this.categoryWeight[text4] : 0);
						list.Add(inventoryLineData3.set(flag, text5, flag2, this.categoryAmount.ContainsKey(text4) ? this.categoryAmount[text4] : 0, num, 0, null, this.inventorySpread, this));
						num2++;
						if (!this.isCollapsed(text4))
						{
							this.objectCategories[text4].Sort((InventoryLineData a, InventoryLineData b) => a.displayName.CompareTo(b.displayName));
							int num3 = 1;
							foreach (InventoryLineData inventoryLineData4 in this.objectCategories[text4])
							{
								inventoryLineData4.categoryOffset = num3;
								num3++;
								this.listItems.Add(inventoryLineData4);
								num2++;
							}
						}
					}
					goto IL_05C4;
				}
			}
			if (this.sortMode == InventoryAndEquipmentStatusScreen.SortMode.AZ)
			{
				this.listItems.AddRange(enumerable4);
				this.listItems.Sort((InventoryLineData a, InventoryLineData b) => a.displayName.CompareTo(b.displayName));
			}
			IL_05C4:
			if (this.GO != null)
			{
				this.priceText.SetText(string.Format("{{{{B|${0}}}}}", this.GO.GetFreeDrams("water", null, null, null, false)));
				this.weightText.SetText(string.Format("{{{{C|{0}{{{{K|/{1}}}}} lbs. }}}}", this.GO.GetCarriedWeight(), this.GO.GetMaxCarriedWeight()));
			}
			this.horizNav.contexts.Clear();
			this.horizNav.contexts.Add(this.IsPaperdollMode ? this.equipmentPaperdollController.scrollContext : this.equipmentListController.scrollContext);
			this.horizNav.contexts.Add(this.inventoryController.scrollContext);
			this.inventoryController.BeforeShow(this.listItems);
			foreach (EquipmentLineData equipmentLineData in this.lines)
			{
				equipmentLineData.free();
			}
			this.lines.Clear();
			if (this.IsPaperdollMode)
			{
				if (this.GO != null)
				{
					this.lines.AddRange(from bodyPart in this.GO.Body.GetParts()
						select PooledFrameworkDataElement<EquipmentLineData>.next().set(this.showCybernetics, bodyPart, this, this.inventorySpread));
				}
				this.equipmentPaperdollController.BeforeShow(this.lines);
				this.cyberneticsHotkeySkin.text = (this.showCybernetics ? "{{hotkey|[~Toggle]}} show equipment" : "{{hotkey|[~Toggle]}} show cybernetics");
			}
			else
			{
				if (this.GO != null)
				{
					this.lines.AddRange(from bodyPart in this.GO.Body.LoopParts()
						select PooledFrameworkDataElement<EquipmentLineData>.next().set(this.showCybernetics, bodyPart, this, this.inventorySpread));
				}
				this.equipmentListController.BeforeShow(this.lines);
				this.cyberneticsHotkeySkinForList.text = (this.showCybernetics ? "{{hotkey|[~Toggle]}} show equipment" : "{{hotkey|[~Toggle]}} show cybernetics");
			}
			TooltipManager.Instance.CloseAll();
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x000E2770 File Offset: 0x000E0970
		public void HandleShowCybernetics()
		{
			this.showCybernetics = !this.showCybernetics;
			this.UpdateViewFromData();
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x000E2788 File Offset: 0x000E0988
		public async void HandleShowOptions()
		{
			int num = await Popup.PickOptionAsync("Options", null, "", new string[]
			{
				(this.EquipmentMode == InventoryAndEquipmentStatusScreen.EQUIPMENT_MODE_PAPERDOLL) ? "Equipment View: {{W|Paperdoll}}/List" : "Equipment View: Paperdoll/{{W|List}}",
				(this.sortMode == InventoryAndEquipmentStatusScreen.SortMode.Category) ? "Sort Mode: {{W|Category}}/A-Z" : "Sort Mode: Category/{{W|A-Z}}",
				(this.SearchMode == InventoryAndEquipmentStatusScreen.SEARCH_MODE_STRICT) ? "Search Mode: {{W|Strict}}/Fuzzy" : "Search Mode: Strict/{{W|Fuzzy}}"
			}, null, null, null, null, 0, 60, 0, -1, false, true, false, true);
			if (num == 0)
			{
				this.HandleToggleEquipmentMode();
			}
			if (num == 1)
			{
				this.HandleToggleSortMode();
			}
			if (num == 2)
			{
				this.HandleToggleSearchMode();
			}
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x000E27C0 File Offset: 0x000E09C0
		public void HandleToggleSearchMode()
		{
			if (this.SearchMode == InventoryAndEquipmentStatusScreen.SEARCH_MODE_FUZZY)
			{
				this.SearchMode = InventoryAndEquipmentStatusScreen.SEARCH_MODE_STRICT;
			}
			else
			{
				this.SearchMode = InventoryAndEquipmentStatusScreen.SEARCH_MODE_FUZZY;
			}
			PlayerPrefs.SetString("InventoryScreenSearchMode", this.SearchMode);
			this.UpdateViewFromData();
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x000E280D File Offset: 0x000E0A0D
		public void HandleToggleSortMode()
		{
			if (this.sortMode == InventoryAndEquipmentStatusScreen.SortMode.Category)
			{
				this.sortMode = InventoryAndEquipmentStatusScreen.SortMode.AZ;
			}
			else
			{
				this.sortMode = InventoryAndEquipmentStatusScreen.SortMode.Category;
			}
			this.UpdateViewFromData();
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x000E2830 File Offset: 0x000E0A30
		public void HandleToggleEquipmentMode()
		{
			if (this.EquipmentMode == InventoryAndEquipmentStatusScreen.EQUIPMENT_MODE_LIST)
			{
				this.EquipmentMode = InventoryAndEquipmentStatusScreen.EQUIPMENT_MODE_PAPERDOLL;
			}
			else
			{
				this.EquipmentMode = InventoryAndEquipmentStatusScreen.EQUIPMENT_MODE_LIST;
			}
			PlayerPrefs.SetString("EquipmentScreenListMode", this.EquipmentMode);
			this.ShowScreen(this.GO, this.statusScreensScreen);
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x000E288A File Offset: 0x000E0A8A
		public void OnSearchTextChange(string text)
		{
			this.filterText = text;
			if (string.IsNullOrEmpty(text))
			{
				this.searcher.sortString = text;
			}
			else
			{
				this.searcher.sortString = text.ToLower();
			}
			this.UpdateViewFromData();
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x000E28C0 File Offset: 0x000E0AC0
		public void HandleVPositive()
		{
			this.categoryCollapsed.Clear();
			this.UpdateViewFromData();
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x000E28D4 File Offset: 0x000E0AD4
		public void HandleVNegative()
		{
			foreach (string text in this.usedCategories)
			{
				this.categoryCollapsed[text] = true;
			}
			this.UpdateViewFromData();
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06002AD3 RID: 10963 RVA: 0x000E2934 File Offset: 0x000E0B34
		public bool IsPaperdollMode
		{
			get
			{
				return this.EquipmentMode == InventoryAndEquipmentStatusScreen.EQUIPMENT_MODE_PAPERDOLL;
			}
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x000E2946 File Offset: 0x000E0B46
		public override void FilterUpdated(string filterText)
		{
			this.OnSearchTextChange(filterText);
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x000E2950 File Offset: 0x000E0B50
		public void UpdatePaneSize()
		{
			if (!InventoryAndEquipmentStatusScreen.expandHighlightedPaneMode)
			{
				if (this.equipmentPaperdollLayoutElement.flexibleWidth != 1f)
				{
					this.equipmentPaperdollLayoutElement.flexibleWidth = 1f;
				}
				if (this.equipmentListLayoutElement.flexibleWidth != 1f)
				{
					this.equipmentListLayoutElement.flexibleWidth = 1f;
				}
				if (this.inventoryLayoutElement.flexibleWidth != 1.5f)
				{
					this.inventoryLayoutElement.flexibleWidth = 1.5f;
					return;
				}
			}
			else if (InventoryAndEquipmentStatusScreen.equipmentPaneHighlighted)
			{
				if (this.equipmentPaperdollLayoutElement.flexibleWidth != 2f)
				{
					this.equipmentPaperdollLayoutElement.flexibleWidth = 2f;
				}
				if (this.equipmentListLayoutElement.flexibleWidth != 2f)
				{
					this.equipmentListLayoutElement.flexibleWidth = 2f;
				}
				if (this.inventoryLayoutElement.flexibleWidth != 1f)
				{
					this.inventoryLayoutElement.flexibleWidth = 1f;
					return;
				}
			}
			else
			{
				if (this.equipmentPaperdollLayoutElement.flexibleWidth != 1f)
				{
					this.equipmentPaperdollLayoutElement.flexibleWidth = 1f;
				}
				if (this.equipmentListLayoutElement.flexibleWidth != 1f)
				{
					this.equipmentListLayoutElement.flexibleWidth = 1f;
				}
				if (this.inventoryLayoutElement.flexibleWidth != 2f)
				{
					this.inventoryLayoutElement.flexibleWidth = 2f;
				}
			}
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x000E2AA2 File Offset: 0x000E0CA2
		public void Update()
		{
			this.UpdatePaneSize();
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06002AD7 RID: 10967 RVA: 0x000E2AAA File Offset: 0x000E0CAA
		// (set) Token: 0x06002AD8 RID: 10968 RVA: 0x000E2AB1 File Offset: 0x000E0CB1
		public static bool EnteringFromEquipmentSide
		{
			get
			{
				return InventoryAndEquipmentStatusScreen._EnteringFromEquipmentSide;
			}
			set
			{
				InventoryAndEquipmentStatusScreen._EnteringFromEquipmentSide = value;
				InventoryAndEquipmentStatusScreen.equipmentPaneHighlighted = value;
			}
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x000E2AC0 File Offset: 0x000E0CC0
		public override NavigationContext ShowScreen(global::XRL.World.GameObject GO, StatusScreensScreen parent)
		{
			InventoryAndEquipmentStatusScreen.expandHighlightedPaneMode = Options.GetOption("OptionExpandHighlightedInventoryAndEquipmentPane", "") == "Yes";
			InventoryAndEquipmentStatusScreen.paginationMode = Options.GetOption("OptionNavigateInventoryAndEquipmentWithPagination", "") == "Yes";
			this.UpdatePaneSize();
			this.ResetLayoutFrame(3);
			this.EquipmentMode = PlayerPrefs.GetString("EquipmentScreenListMode", InventoryAndEquipmentStatusScreen.EQUIPMENT_MODE_PAPERDOLL);
			this.equipmentPaperdollScroller.SetActive(this.EquipmentMode == InventoryAndEquipmentStatusScreen.EQUIPMENT_MODE_PAPERDOLL);
			this.equipmentListScroller.SetActive(this.EquipmentMode == InventoryAndEquipmentStatusScreen.EQUIPMENT_MODE_LIST);
			this.SearchMode = PlayerPrefs.GetString("InventoryScreenSearchMode", InventoryAndEquipmentStatusScreen.SEARCH_MODE_STRICT);
			this.statusScreensScreen = parent;
			this.filterBar = parent.filterBar;
			this.inventorySpread = HotkeySpread.get("Menu");
			this.statusScreensScreen = parent;
			this.GO = GO;
			this.horizNav.SetAxis(InventoryAndEquipmentStatusScreen.paginationMode ? InputAxisTypes.NavigationPageXAxis : InputAxisTypes.NavigationXAxis, false);
			this.horizNav.wraps = !InventoryAndEquipmentStatusScreen.paginationMode;
			this.horizNav.contexts.Clear();
			this.horizNav.contexts.Add(this.IsPaperdollMode ? this.equipmentPaperdollController.scrollContext : this.equipmentListController.scrollContext);
			this.horizNav.contexts.Add(this.inventoryController.scrollContext);
			this.vertNav.SetAxis(InputAxisTypes.NavigationYAxis, false);
			this.vertNav.contexts.Clear();
			this.vertNav.contexts.Add(this.horizNav);
			this.vertNav.wraps = false;
			this.OnSearchTextChange(null);
			if (this.IsPaperdollMode)
			{
				this.equipmentPaperdollController.onSelected.RemoveAllListeners();
				this.equipmentPaperdollController.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleSelectItem));
				this.equipmentPaperdollController.onHighlight.RemoveAllListeners();
			}
			else
			{
				this.equipmentListController.onSelected.RemoveAllListeners();
				this.equipmentListController.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleSelectItem));
				this.equipmentListController.onHighlight.RemoveAllListeners();
			}
			this.inventoryController.onSelected.RemoveAllListeners();
			this.inventoryController.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleSelectItem));
			this.inventoryController.onHighlight.RemoveAllListeners();
			this.inventoryController.scrollContext.wraps = false;
			if (this.IsPaperdollMode)
			{
				this.equipmentPaperdollController.selectedPosition = 0;
			}
			else
			{
				this.equipmentListController.selectedPosition = 0;
			}
			this.inventoryController.selectedPosition = 0;
			if (InventoryAndEquipmentStatusScreen.EnteringFromEquipmentSide)
			{
				if (this.IsPaperdollMode)
				{
					this.equipmentPaperdollController.scrollContext.ActivateAndEnable();
				}
				else
				{
					this.equipmentListController.scrollContext.ActivateAndEnable();
				}
			}
			else
			{
				this.inventoryController.scrollContext.ActivateAndEnable();
			}
			this.horizNav.Setup();
			this.vertNav.Setup();
			NavigationContext navigationContext = parent.screenGlobalContext;
			if (navigationContext.menuOptionDescriptions == null)
			{
				navigationContext.menuOptionDescriptions = new List<MenuOption>();
			}
			parent.screenGlobalContext.menuOptionDescriptions.Add(this.SET_PRIMARY_LIMB);
			parent.screenGlobalContext.menuOptionDescriptions.Add(this.SHOW_TOOLTIP);
			parent.screenGlobalContext.menuOptionDescriptions.Add(this.CMD_OPTIONS);
			navigationContext = parent.screenGlobalContext;
			if (navigationContext.commandHandlers == null)
			{
				navigationContext.commandHandlers = new Dictionary<string, Action>();
			}
			parent.screenGlobalContext.commandHandlers["Toggle"] = new Action(this.HandleShowCybernetics);
			parent.screenGlobalContext.commandHandlers["CmdOptions"] = new Action(this.HandleShowOptions);
			foreach (MethodInfo methodInfo in ModManager.GetMethodsWithAttribute(typeof(InventoryAndEquipmentStatusScreen.EquipmentScreenCommandsCallbackAttribute), typeof(InventoryAndEquipmentStatusScreen.HasEquipmentScreenCommandsCallbackAttribute), true))
			{
				try
				{
					object[] array = new object[] { parent.screenGlobalContext, this };
					methodInfo.Invoke(null, array);
				}
				catch (Exception ex)
				{
					MetricsManager.LogAssemblyError(methodInfo, string.Format("Error invoking {0}.{1}: {2}", methodInfo.DeclaringType.FullName, methodInfo.Name, ex));
				}
			}
			base.ShowScreen(GO, parent);
			ScrollContext<NavigationContext> scrollContext = this.vertNav;
			if (scrollContext.commandHandlers == null)
			{
				scrollContext.commandHandlers = new Dictionary<string, Action>();
			}
			this.vertNav.commandHandlers["V Positive"] = new Action(this.HandleVPositive);
			this.vertNav.commandHandlers["V Negative"] = new Action(this.HandleVNegative);
			this.UpdatePaneSize();
			return this.vertNav;
		}

		// Token: 0x040016BC RID: 5820
		public InventoryAndEquipmentStatusScreen.SortMode sortMode = InventoryAndEquipmentStatusScreen.SortMode.Category;

		// Token: 0x040016BD RID: 5821
		public ScrollContext<NavigationContext> vertNav = new ScrollContext<NavigationContext>();

		// Token: 0x040016BE RID: 5822
		public ScrollContext<NavigationContext> horizNav = new ScrollContext<NavigationContext>();

		// Token: 0x040016BF RID: 5823
		public global::UnityEngine.GameObject equipmentPaperdollScroller;

		// Token: 0x040016C0 RID: 5824
		public global::UnityEngine.GameObject equipmentListScroller;

		// Token: 0x040016C1 RID: 5825
		public PaperdollScroller equipmentPaperdollController;

		// Token: 0x040016C2 RID: 5826
		public FrameworkScroller equipmentListController;

		// Token: 0x040016C3 RID: 5827
		public FrameworkScroller inventoryController;

		// Token: 0x040016C4 RID: 5828
		private Renderable icon;

		// Token: 0x040016C5 RID: 5829
		public UITextSkin priceText;

		// Token: 0x040016C6 RID: 5830
		public UITextSkin weightText;

		// Token: 0x040016C7 RID: 5831
		private InventoryLineData searcher = new InventoryLineData();

		// Token: 0x040016C8 RID: 5832
		private List<string> usedCategories = new List<string>();

		// Token: 0x040016C9 RID: 5833
		private List<InventoryLineData> listItems = new List<InventoryLineData>();

		// Token: 0x040016CA RID: 5834
		private Dictionary<string, List<InventoryLineData>> objectCategories = new Dictionary<string, List<InventoryLineData>>();

		// Token: 0x040016CB RID: 5835
		private Dictionary<string, bool> categoryCollapsed = new Dictionary<string, bool>();

		// Token: 0x040016CC RID: 5836
		private List<string> filterBarCategories = new List<string>();

		// Token: 0x040016CD RID: 5837
		private Dictionary<string, int> categoryWeight = new Dictionary<string, int>();

		// Token: 0x040016CE RID: 5838
		private Dictionary<string, int> categoryAmount = new Dictionary<string, int>();

		// Token: 0x040016CF RID: 5839
		private FilterBar filterBar;

		// Token: 0x040016D0 RID: 5840
		public List<EquipmentLineData> lines = new List<EquipmentLineData>(64);

		// Token: 0x040016D1 RID: 5841
		public UIHotkeySkin cyberneticsHotkeySkin;

		// Token: 0x040016D2 RID: 5842
		public UIHotkeySkin cyberneticsHotkeySkinForList;

		// Token: 0x040016D3 RID: 5843
		public bool showCybernetics;

		// Token: 0x040016D4 RID: 5844
		public static string EQUIPMENT_MODE_PAPERDOLL = "Paperdoll";

		// Token: 0x040016D5 RID: 5845
		public static string EQUIPMENT_MODE_LIST = "List";

		// Token: 0x040016D6 RID: 5846
		public string EquipmentMode = InventoryAndEquipmentStatusScreen.EQUIPMENT_MODE_PAPERDOLL;

		// Token: 0x040016D7 RID: 5847
		public static string SEARCH_MODE_FUZZY = "Fuzzy";

		// Token: 0x040016D8 RID: 5848
		public static string SEARCH_MODE_STRICT = "Strict";

		// Token: 0x040016D9 RID: 5849
		public string SearchMode = InventoryAndEquipmentStatusScreen.SEARCH_MODE_STRICT;

		// Token: 0x040016DA RID: 5850
		private StatusScreensScreen statusScreensScreen;

		// Token: 0x040016DB RID: 5851
		public global::XRL.World.GameObject GO;

		// Token: 0x040016DC RID: 5852
		public static bool equipmentPaneHighlighted;

		// Token: 0x040016DD RID: 5853
		public LayoutElement equipmentListLayoutElement;

		// Token: 0x040016DE RID: 5854
		public LayoutElement equipmentPaperdollLayoutElement;

		// Token: 0x040016DF RID: 5855
		public LayoutElement inventoryLayoutElement;

		// Token: 0x040016E0 RID: 5856
		public static bool _EnteringFromEquipmentSide = false;

		// Token: 0x040016E1 RID: 5857
		public static bool expandHighlightedPaneMode;

		// Token: 0x040016E2 RID: 5858
		public static bool paginationMode;

		// Token: 0x040016E3 RID: 5859
		private HotkeySpread inventorySpread;

		// Token: 0x040016E4 RID: 5860
		public MenuOption CMD_SHOWCYBERNETICS = new MenuOption
		{
			Id = "CmdShowCybernetics",
			InputCommand = "Toggle",
			Description = "Toggle Cybernetics"
		};

		// Token: 0x040016E5 RID: 5861
		public MenuOption CMD_OPTIONS = new MenuOption
		{
			Id = "CmdShowOptions",
			InputCommand = "CmdOptions",
			Description = "Display Options"
		};

		// Token: 0x040016E6 RID: 5862
		public MenuOption SET_PRIMARY_LIMB = new MenuOption
		{
			Id = "CmdInsert",
			InputCommand = "CmdInsert",
			Description = "Set Primary Limb"
		};

		// Token: 0x040016E7 RID: 5863
		public MenuOption SHOW_TOOLTIP = new MenuOption
		{
			Id = "CmdShowTooltip",
			InputCommand = "Highlight",
			Description = "Show Tooltip"
		};

		// Token: 0x040016E8 RID: 5864
		public MenuOption QUICK_DROP = new MenuOption
		{
			Id = "InventoryQuickDrop",
			InputCommand = "InventoryQuickDrop",
			Description = "Quick Drop"
		};

		// Token: 0x040016E9 RID: 5865
		public MenuOption QUICK_EAT = new MenuOption
		{
			Id = "InventoryQuickEat",
			InputCommand = "InventoryQuickEat",
			Description = "Quick Eat"
		};

		// Token: 0x040016EA RID: 5866
		public MenuOption QUICK_DRINK = new MenuOption
		{
			Id = "InventoryQuickDrink",
			InputCommand = "InventoryQuickDrink",
			Description = "Quick Drink"
		};

		// Token: 0x040016EB RID: 5867
		public MenuOption QUICK_APPLY = new MenuOption
		{
			Id = "InventoryQuickApply",
			InputCommand = "InventoryQuickApply",
			Description = "Quick Apply"
		};

		// Token: 0x02001888 RID: 6280
		public enum SortMode
		{
			// Token: 0x040062AE RID: 25262
			AZ,
			// Token: 0x040062AF RID: 25263
			Category
		}

		// Token: 0x02001889 RID: 6281
		[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
		public class EquipmentScreenCommandsCallbackAttribute : Attribute
		{
		}

		// Token: 0x0200188A RID: 6282
		[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
		public class HasEquipmentScreenCommandsCallbackAttribute : Attribute
		{
		}
	}
}
