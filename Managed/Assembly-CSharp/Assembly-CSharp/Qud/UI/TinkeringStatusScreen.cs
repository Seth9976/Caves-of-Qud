using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using FuzzySharp;
using FuzzySharp.Extractor;
using UnityEngine.Events;
using XRL;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;
using XRL.World.Parts;
using XRL.World.Parts.Skill;
using XRL.World.Tinkering;

namespace Qud.UI
{
	// Token: 0x0200047F RID: 1151
	[UIView("TinkeringStatusScreen", false, false, false, null, null, false, 0, false, NavCategory = "Menus", UICanvas = "StatusScreens", UICanvasHost = 1)]
	public class TinkeringStatusScreen : BaseStatusScreen<TinkeringStatusScreen>
	{
		// Token: 0x06002BEC RID: 11244 RVA: 0x000EB2BA File Offset: 0x000E94BA
		public static void StartupModItemWithTinkering(GameObject go)
		{
			TinkeringStatusScreen.startupModItem = go;
			SingletonWindowBase<StatusScreensScreen>.instance.SetPage(3);
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x000EB2CD File Offset: 0x000E94CD
		public override IRenderable GetTabIcon()
		{
			if (this.icon == null)
			{
				this.icon = new Renderable("Items/sw_toolbox.bmp", " ", "&c", null, 'C');
			}
			return this.icon;
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x000EB2FA File Offset: 0x000E94FA
		public override string GetTabString()
		{
			return "Tinkering";
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x000EB301 File Offset: 0x000E9501
		public override bool WantsCategoryBar()
		{
			return true;
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x000EB304 File Offset: 0x000E9504
		public void UpdateTinkeringData()
		{
			TinkeringStatusScreen.BuildRecipes.Clear();
			TinkeringStatusScreen.ModRecipes.Clear();
			TinkerData.KnownRecipes.Where<TinkerData>((TinkerData d) => d.Blueprint == "HandENuke").ToList<TinkerData>();
			foreach (TinkerData tinkerData in TinkerData.KnownRecipes)
			{
				if (tinkerData.Type == "Build")
				{
					TinkeringStatusScreen.BuildRecipes.Add(tinkerData);
				}
				else if (tinkerData.Type == "Mod")
				{
					TinkeringStatusScreen.ModRecipes.Add(tinkerData);
				}
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06002BF1 RID: 11249 RVA: 0x000EB3D0 File Offset: 0x000E95D0
		public Dictionary<string, bool> categoryCollapsed
		{
			get
			{
				return this._categoryCollapsed[this.CurrentCategory];
			}
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x000EB3E4 File Offset: 0x000E95E4
		public bool isCollapsed(string category)
		{
			bool flag;
			return !this._categoryCollapsed[this.CurrentCategory].TryGetValue(category, out flag) || flag;
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x000EB40F File Offset: 0x000E960F
		public void SetCategoryExpanded(string category, bool state)
		{
			state = !state;
			if (this.isCollapsed(category) == state)
			{
				return;
			}
			this.categoryCollapsed[category] = state;
			this.UpdateViewFromData();
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x000EB435 File Offset: 0x000E9635
		public override void FilterUpdated(string filterText)
		{
			this.OnSearchTextChange(filterText);
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x000EB440 File Offset: 0x000E9640
		public override void UpdateViewFromData()
		{
			this.UpdateTinkeringData();
			this.UpdateBitlocker();
			this.usedCategories.Clear();
			this.listItems.Clear();
			this.dataToFree.ForEach(delegate(TinkeringLineData l)
			{
				l.free();
			});
			this.dataToFree.Clear();
			this.modeToggleText.SetText((this.CurrentCategory == 0) ? "{{hotkey|[~Toggle]}} switch to modifications" : "{{hotkey|[~Toggle]}} switch to build");
			this.filterBarCategories.Clear();
			this.filterBarCategories.Add("*All");
			if (this.CurrentCategory == 0)
			{
				this.dataList.Clear();
				this.dataList.AddRange(TinkeringStatusScreen.BuildRecipes.Select<TinkerData, TinkeringLineData>((TinkerData n) => PooledFrameworkDataElement<TinkeringLineData>.next().set(this, this.CurrentCategory, false, null, 0, true, 0, n, null)));
				this.dataToFree.AddRange(this.dataList);
				foreach (TinkeringLineData tinkeringLineData in this.dataList)
				{
					string uiCategory = tinkeringLineData.uiCategory;
					if (!this.filterBarCategories.Contains(uiCategory))
					{
						this.filterBarCategories.Add(uiCategory);
					}
				}
				IEnumerable<TinkeringLineData> enumerable;
				if (this.searcher.sortString.IsNullOrEmpty() && this.filterBar.enabledCategories.Count == 1 && this.filterBar.enabledCategories[0] == "*All")
				{
					enumerable = this.dataList;
				}
				else if (this.searcher.sortString.IsNullOrEmpty())
				{
					enumerable = this.dataList.Where<TinkeringLineData>((TinkeringLineData i) => this.filterBar.enabledCategories.Contains("*All") || this.filterBar.enabledCategories.Contains(j.uiCategory) || this.filterBar.enabledCategories.Contains(j.uiCategory)).ToList<TinkeringLineData>();
				}
				else
				{
					enumerable = this.dataList.Where<TinkeringLineData>((TinkeringLineData i) => this.filterBar.enabledCategories.Contains("*All") || this.filterBar.enabledCategories.Contains(j.uiCategory) || this.filterBar.enabledCategories.Contains(j.uiCategory)).ToList<TinkeringLineData>();
					IEnumerable<TinkeringLineData> enumerable2;
					if (!this.searcher.sortString.IsNullOrEmpty())
					{
						enumerable2 = from m in Process.ExtractTop<TinkeringLineData>(this.searcher, enumerable, (TinkeringLineData i) => j.sortString, null, enumerable.Count<TinkeringLineData>(), 50)
							select m.Value;
					}
					else
					{
						IEnumerable<TinkeringLineData> enumerable3 = this.dataList;
						enumerable2 = enumerable3;
					}
					enumerable = enumerable2;
				}
				this.filterBarCategories.Sort();
				this.filterBar.SetCategories(this.filterBarCategories);
				foreach (KeyValuePair<string, List<TinkeringLineData>> keyValuePair in this.objectCategories)
				{
					keyValuePair.Value.Clear();
				}
				foreach (TinkeringLineData tinkeringLineData2 in enumerable)
				{
					if (tinkeringLineData2.data != null)
					{
						string uiCategory2 = tinkeringLineData2.uiCategory;
						if (!this.objectCategories.ContainsKey(uiCategory2))
						{
							this.objectCategories.Add(uiCategory2, new List<TinkeringLineData>());
						}
						this.objectCategories[uiCategory2].Add(tinkeringLineData2);
						if (!this.usedCategories.Contains(uiCategory2))
						{
							this.usedCategories.Add(uiCategory2);
						}
					}
				}
				this.usedCategories.Sort();
				int num = 0;
				using (IEnumerator<string> enumerator4 = this.usedCategories.Where<string>((string k) => this.objectCategories[k].Count > 0).GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						string text = enumerator4.Current;
						this.listItems.Add(PooledFrameworkDataElement<TinkeringLineData>.next().set(this, this.CurrentCategory, true, text, this.objectCategories[text].Count, !this.isCollapsed(text), 0, null, null));
						num++;
						if (!this.isCollapsed(text))
						{
							this.objectCategories[text].Sort((TinkeringLineData a, TinkeringLineData b) => a.sortString.CompareTo(b.sortString));
							int num2 = 0;
							foreach (TinkeringLineData tinkeringLineData3 in this.objectCategories[text])
							{
								tinkeringLineData3.categoryOffset = num2 + 1;
								tinkeringLineData3.categoryName = text;
								this.listItems.Add(tinkeringLineData3);
								num++;
								num2++;
							}
						}
					}
					goto IL_08D7;
				}
			}
			if (this.CurrentCategory == 1)
			{
				this.dataList.Clear();
				this.dataList.AddRange(TinkeringStatusScreen.ModRecipes.Select<TinkerData, TinkeringLineData>((TinkerData n) => PooledFrameworkDataElement<TinkeringLineData>.next().set(this, this.CurrentCategory, false, null, 0, true, 0, n, null)));
				this.dataToFree.AddRange(this.dataList);
				Action<GameObject> action = delegate(GameObject obj)
				{
					string text2 = ItemModding.ModKey(obj);
					if (text2 != null && obj.Understood())
					{
						foreach (TinkeringLineData tinkeringLineData10 in this.dataList)
						{
							if (tinkeringLineData10.data.CanMod(text2) && ItemModding.ModificationApplicable(tinkeringLineData10.data.PartName, obj, The.Player, null))
							{
								foreach (string text3 in text2.Split(',', StringSplitOptions.None))
								{
									if (!this.filterBarCategories.Contains(text3))
									{
										this.filterBarCategories.Add(text3);
									}
								}
								if (!tinkeringLineData10.applicableKeys.Contains(text2))
								{
									tinkeringLineData10.applicableKeys.Add(text2);
								}
								tinkeringLineData10.applicableObjects.Add(obj);
							}
						}
					}
				};
				this.GO.Inventory.ForeachObject(action);
				this.GO.Body.ForeachEquippedObject(action);
				List<TinkeringLineData> list = this.dataList;
				if (TinkeringStatusScreen.startupModItem != null)
				{
					this.dataList = this.dataList.Where<TinkeringLineData>((TinkeringLineData m) => m.applicableObjects.Count > 0).ToList<TinkeringLineData>();
				}
				IEnumerable<TinkeringLineData> enumerable4;
				if (this.searcher.sortString.IsNullOrEmpty() && this.filterBar.enabledCategories.Count == 1 && this.filterBar.enabledCategories[0] == "*All")
				{
					enumerable4 = this.dataList;
				}
				else if (this.searcher.sortString.IsNullOrEmpty())
				{
					enumerable4 = this.dataList.Where<TinkeringLineData>((TinkeringLineData i) => this.filterBar.enabledCategories.Contains("*All") || this.filterBar.enabledCategories.Any<string>((string e) => i.applicableKeys.Any<string>((string k) => k.Contains(e)))).ToList<TinkeringLineData>();
				}
				else
				{
					enumerable4 = this.dataList.Where<TinkeringLineData>((TinkeringLineData i) => this.filterBar.enabledCategories.Contains("*All") || this.filterBar.enabledCategories.Any<string>((string e) => i.applicableKeys.Any<string>((string k) => k.Contains(e)))).ToList<TinkeringLineData>();
					IEnumerable<TinkeringLineData> enumerable5;
					if (!this.searcher.sortString.IsNullOrEmpty())
					{
						enumerable5 = from m in Process.ExtractTop<TinkeringLineData>(this.searcher, enumerable4, (TinkeringLineData i) => i.sortString, null, enumerable4.Count<TinkeringLineData>(), 50)
							select m.Value;
					}
					else
					{
						IEnumerable<TinkeringLineData> enumerable3 = this.dataList;
						enumerable5 = enumerable3;
					}
					enumerable4 = enumerable5;
				}
				foreach (TinkeringLineData tinkeringLineData4 in enumerable4)
				{
					if (!this.usedCategories.Contains(tinkeringLineData4.data.DisplayName))
					{
						this.usedCategories.Add(tinkeringLineData4.data.DisplayName);
					}
				}
				this.filterBarCategories.Sort();
				this.filterBar.SetCategories(this.filterBarCategories);
				this.usedCategories.Sort();
				int num3 = 0;
				using (List<string>.Enumerator enumerator5 = this.usedCategories.GetEnumerator())
				{
					while (enumerator5.MoveNext())
					{
						string category = enumerator5.Current;
						TinkeringLineData tinkeringLineData5 = this.dataList.FirstOrDefault<TinkeringLineData>((TinkeringLineData d) => d.data.DisplayName == category);
						List<GameObject> list2 = tinkeringLineData5.applicableObjects;
						if (TinkeringStatusScreen.startupModItem != null)
						{
							list2 = list2.Where<GameObject>((GameObject o) => o == TinkeringStatusScreen.startupModItem).ToList<GameObject>();
						}
						if (list2.Count<GameObject>() != 0 || TinkeringStatusScreen.startupModItem == null)
						{
							TinkeringLineData tinkeringLineData6 = PooledFrameworkDataElement<TinkeringLineData>.next().set(this, this.CurrentCategory, true, category, list2.Count, !this.isCollapsed(category), 0, tinkeringLineData5.data, null);
							this.dataToFree.Add(tinkeringLineData6);
							this.listItems.Add(tinkeringLineData6);
							num3++;
							if (!this.isCollapsed(category))
							{
								if (list2.Count<GameObject>() == 0)
								{
									TinkeringLineData tinkeringLineData7 = PooledFrameworkDataElement<TinkeringLineData>.next().set(this, this.CurrentCategory, false, category, 0, false, 1, tinkeringLineData5.data, null);
									this.dataToFree.Add(tinkeringLineData7);
									this.listItems.Add(tinkeringLineData7);
									num3++;
								}
								else
								{
									list2.Sort((GameObject a, GameObject b) => a.DisplayName.CompareTo(b.DisplayName));
									int num4 = 0;
									foreach (GameObject gameObject in list2)
									{
										TinkeringLineData tinkeringLineData8 = PooledFrameworkDataElement<TinkeringLineData>.next();
										int currentCategory = this.CurrentCategory;
										bool flag = false;
										GameObject gameObject2 = gameObject;
										TinkeringLineData tinkeringLineData9 = tinkeringLineData8.set(this, currentCategory, flag, category, 0, false, num4 + 1, tinkeringLineData5.data, gameObject2);
										this.dataToFree.Add(tinkeringLineData9);
										this.listItems.Add(tinkeringLineData9);
										num3++;
										num4++;
									}
								}
							}
						}
					}
				}
			}
			IL_08D7:
			if (this.listItems.Count == 0)
			{
				this.controller.BeforeShow(new List<TinkeringLineData>
				{
					new TinkeringLineData
					{
						category = true,
						categoryExpanded = false,
						categoryName = "~<none>"
					}
				});
			}
			else
			{
				this.controller.BeforeShow(this.listItems);
			}
			int selectedPosition = this.controller.selectedPosition;
			List<FrameworkDataElement> choices = this.controller.choices;
			if (selectedPosition < ((choices != null) ? choices.Count : 0))
			{
				this.HandleHighlightObject(this.controller.choices[this.controller.selectedPosition]);
			}
			base.UpdateViewFromData();
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x000EBE88 File Offset: 0x000EA088
		public void UpdateBitlocker()
		{
			if (this.GO == null)
			{
				return;
			}
			BitLocker bitLocker = ((StatusScreensScreen.GO.GetPart<Tinkering>() == null) ? StatusScreensScreen.GO.GetPart<BitLocker>() : StatusScreensScreen.GO.RequirePart<BitLocker>(false));
			this.bitlockerData.ForEach(delegate(TinkeringBitsLineData l)
			{
				l.free();
			});
			this.bitlockerData.Clear();
			if (bitLocker != null)
			{
				for (int i = 0; i < BitType.BitOrder.Count; i++)
				{
					char c = BitType.BitOrder[i];
					BitType bitType = BitType.BitMap[c];
					string text;
					if (!this.bitDescriptions.TryGetValue(i, out text))
					{
						text = string.Format("{{{{{0}|{1} {2}}}}}", bitType.Color, Options.AlphanumericBits ? BitType.CharTranslateBit(bitType.Color) : '\a', bitType.Description);
						this.bitDescriptions.Add(i, text);
					}
					this.bitlockerData.Add(PooledFrameworkDataElement<TinkeringBitsLineData>.next().set(c, text, bitLocker.GetBitCount(c), this.ActiveCost));
				}
			}
			this.bitsController.BeforeShow(this.bitlockerData);
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x000EBFBC File Offset: 0x000EA1BC
		public void categoryBarClicked(FrameworkDataElement el)
		{
			ButtonBar.ButtonBarButtonData buttonBarButtonData = el as ButtonBar.ButtonBarButtonData;
			if (buttonBarButtonData != null && buttonBarButtonData != null)
			{
				buttonBarButtonData.onSelect();
			}
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x000EBFE4 File Offset: 0x000EA1E4
		public async void HandleSelectItem(FrameworkDataElement data)
		{
			TinkeringLineData d = data as TinkeringLineData;
			if (d != null)
			{
				if (d.category)
				{
					if (!this.categoryCollapsed.ContainsKey(d.categoryName))
					{
						this.categoryCollapsed.Add(d.categoryName, true);
					}
					this.categoryCollapsed[d.categoryName] = !this.categoryCollapsed[d.categoryName];
					this.UpdateViewFromData();
				}
				else
				{
					global::XRL.World.Event FromEvent = new global::XRL.World.Event("InterfaceTest");
					bool exit = false;
					await APIDispatch.RunAndWaitAsync(delegate
					{
						if (d.mode == 0)
						{
							if (!TinkeringScreen.PerformUITinkerBuild(this.GO, d.data, FromEvent) || FromEvent.InterfaceExitRequested())
							{
								exit = true;
								return;
							}
						}
						else if (d.mode == 1)
						{
							bool flag = false;
							if (!TinkeringScreen.PerformUITinkerMod(this.GO, d.modObject, d.data, d.cost, FromEvent, ref flag, null) || FromEvent.InterfaceExitRequested())
							{
								exit = true;
							}
						}
					});
					if (exit)
					{
						this.parent.Exit();
					}
					else
					{
						this.UpdateViewFromData();
					}
				}
			}
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x000EC024 File Offset: 0x000EA224
		public void HandleHighlightObject(FrameworkDataElement data)
		{
			this.ActiveCost.Clear();
			TinkeringLineData tinkeringLineData = data as TinkeringLineData;
			if (tinkeringLineData != null)
			{
				if (!tinkeringLineData.category)
				{
					tinkeringLineData.cost.CopyTo(this.ActiveCost);
					this.UpdateBitlocker();
				}
				this.detailsLine.setData(data);
			}
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x000EC071 File Offset: 0x000EA271
		public void OnSearchTextChange(string text)
		{
			TinkeringStatusScreen.startupModItem = null;
			if (string.IsNullOrWhiteSpace(text))
			{
				this.searcher._sortString = text;
			}
			else
			{
				this.searcher._sortString = text.ToLower();
			}
			this.UpdateViewFromData();
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000EC0A6 File Offset: 0x000EA2A6
		public void HandleModeToggle()
		{
			TinkeringStatusScreen.startupModItem = null;
			this.CurrentCategory = 1 - this.CurrentCategory;
			this.UpdateViewFromData();
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x000EC0C4 File Offset: 0x000EA2C4
		public void HandleVPositive()
		{
			this.categoryCollapsed.Clear();
			foreach (string text in this.usedCategories)
			{
				this.categoryCollapsed[text] = false;
			}
			this.UpdateViewFromData();
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x000EC130 File Offset: 0x000EA330
		public void HandleVNegative()
		{
			this.categoryCollapsed.Clear();
			this.UpdateViewFromData();
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x000EC144 File Offset: 0x000EA344
		public override NavigationContext ShowScreen(GameObject GO, StatusScreensScreen parent)
		{
			this.detailsLine.gameObject.SetActive(false);
			this.filterBar = parent.filterBar;
			this.parent = parent;
			this.GO = GO;
			this.vertNav.SetAxis(InputAxisTypes.NavigationYAxis, false);
			this.vertNav.contexts.Add(this.controller.scrollContext);
			this.vertNav.wraps = false;
			this.vertNav.Setup();
			ScrollContext<NavigationContext> scrollContext = this.vertNav;
			if (scrollContext.commandHandlers == null)
			{
				scrollContext.commandHandlers = new Dictionary<string, Action>();
			}
			this.vertNav.commandHandlers["Toggle"] = new Action(this.HandleModeToggle);
			this.controller.onSelected.RemoveAllListeners();
			this.controller.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleSelectItem));
			this.controller.onHighlight.RemoveAllListeners();
			this.controller.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HandleHighlightObject));
			this.controller.scrollContext.wraps = false;
			ScrollContext<FrameworkDataElement, NavigationContext> scrollContext2 = this.controller.scrollContext;
			if (scrollContext2.commandHandlers == null)
			{
				scrollContext2.commandHandlers = new Dictionary<string, Action>();
			}
			this.controller.scrollContext.commandHandlers["V Positive"] = new Action(this.HandleVPositive);
			this.controller.scrollContext.commandHandlers["V Negative"] = new Action(this.HandleVNegative);
			if (TinkeringStatusScreen.startupModItem == null)
			{
				this.OnSearchTextChange(null);
			}
			else
			{
				this.searcher._sortString = null;
				this.CurrentCategory = 1;
				this.filterBar.searchInput.InputText.SetText(TinkeringStatusScreen.startupModItem.DisplayNameOnlyStripped.ToLower());
				this.UpdateViewFromData();
			}
			this.controller.selectedPosition = 0;
			this.controller.scrollContext.ActivateAndEnable();
			base.ShowScreen(GO, parent);
			return this.vertNav;
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x000EC344 File Offset: 0x000EA544
		public override bool Exit()
		{
			TinkeringStatusScreen.startupModItem = null;
			return true;
		}

		// Token: 0x04001817 RID: 6167
		public ScrollContext<NavigationContext> vertNav = new ScrollContext<NavigationContext>();

		// Token: 0x04001818 RID: 6168
		public ScrollContext<NavigationContext> horizNav = new ScrollContext<NavigationContext>();

		// Token: 0x04001819 RID: 6169
		public FrameworkScroller controller;

		// Token: 0x0400181A RID: 6170
		public FrameworkScroller bitsController;

		// Token: 0x0400181B RID: 6171
		private Renderable icon;

		// Token: 0x0400181C RID: 6172
		private static GameObject startupModItem;

		// Token: 0x0400181D RID: 6173
		private static List<TinkerData> BuildRecipes = new List<TinkerData>(64);

		// Token: 0x0400181E RID: 6174
		private static List<TinkerData> ModRecipes = new List<TinkerData>(64);

		// Token: 0x0400181F RID: 6175
		public int CurrentCategory;

		// Token: 0x04001820 RID: 6176
		public int MaxCategory = 1;

		// Token: 0x04001821 RID: 6177
		private List<TinkeringLineData> dataList = new List<TinkeringLineData>();

		// Token: 0x04001822 RID: 6178
		private TinkeringLineData searcher = new TinkeringLineData
		{
			_sortString = ""
		};

		// Token: 0x04001823 RID: 6179
		private List<TinkeringLineData> dataToFree = new List<TinkeringLineData>();

		// Token: 0x04001824 RID: 6180
		private List<TinkeringLineData> listItems = new List<TinkeringLineData>();

		// Token: 0x04001825 RID: 6181
		private List<string> usedCategories = new List<string>();

		// Token: 0x04001826 RID: 6182
		private Dictionary<string, List<TinkeringLineData>> objectCategories = new Dictionary<string, List<TinkeringLineData>>();

		// Token: 0x04001827 RID: 6183
		public Dictionary<int, Dictionary<string, bool>> _categoryCollapsed = new Dictionary<int, Dictionary<string, bool>>
		{
			{
				0,
				new Dictionary<string, bool>()
			},
			{
				1,
				new Dictionary<string, bool>()
			}
		};

		// Token: 0x04001828 RID: 6184
		private List<string> filterBarCategories = new List<string>();

		// Token: 0x04001829 RID: 6185
		private Dictionary<TinkerData, List<GameObject>> applicableObjects = new Dictionary<TinkerData, List<GameObject>>();

		// Token: 0x0400182A RID: 6186
		public FilterBar filterBar;

		// Token: 0x0400182B RID: 6187
		private Dictionary<int, string> bitDescriptions = new Dictionary<int, string>();

		// Token: 0x0400182C RID: 6188
		private List<TinkeringBitsLineData> bitlockerData = new List<TinkeringBitsLineData>();

		// Token: 0x0400182D RID: 6189
		public List<TinkeringStatusScreen.CategoryInfo> categoryInfos = new List<TinkeringStatusScreen.CategoryInfo>
		{
			new TinkeringStatusScreen.CategoryInfo
			{
				N = 0,
				Name = "Build"
			},
			new TinkeringStatusScreen.CategoryInfo
			{
				N = 1,
				Name = "Mod"
			}
		};

		// Token: 0x0400182E RID: 6190
		private BitCost ActiveCost = new BitCost();

		// Token: 0x0400182F RID: 6191
		public UIHotkeySkin modeToggleText;

		// Token: 0x04001830 RID: 6192
		public StatusScreensScreen parent;

		// Token: 0x04001831 RID: 6193
		public GameObject GO;

		// Token: 0x04001832 RID: 6194
		public string SelectedSubCategory;

		// Token: 0x04001833 RID: 6195
		public TinkeringDetailsLine detailsLine;

		// Token: 0x020018C1 RID: 6337
		public class CategoryInfo
		{
			// Token: 0x0400636B RID: 25451
			public int N;

			// Token: 0x0400636C RID: 25452
			public bool Selected;

			// Token: 0x0400636D RID: 25453
			public string Name;
		}
	}
}
