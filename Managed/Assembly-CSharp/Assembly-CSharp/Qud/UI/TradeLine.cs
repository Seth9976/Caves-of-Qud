using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x02000480 RID: 1152
	public class TradeLine : BaseLineWithTooltip, IFrameworkControl, IFrameworkControlSubcontexts, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IScrollHandler
	{
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06002C0A RID: 11274 RVA: 0x000EC72F File Offset: 0x000EA92F
		public bool selected
		{
			get
			{
				TradeLine.Context context = this.context;
				return context != null && context.IsActive(true);
			}
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x000EC743 File Offset: 0x000EA943
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x000EC74C File Offset: 0x000EA94C
		public void SetupContexts(ScrollChildContext scrollContext)
		{
			scrollContext.proxyTo = this.context;
			TradeLine.Context context = this.context;
			if (context.axisHandlers == null)
			{
				context.axisHandlers = new Dictionary<InputAxisTypes, Action> { 
				{
					InputAxisTypes.NavigationVAxis,
					new Action(this.HandleNavigationVAxis)
				} };
			}
			context = this.context;
			if (context.commandHandlers == null)
			{
				context.commandHandlers = new Dictionary<string, Action>
				{
					{
						"V Positive",
						new Action(this.HandleVPositive)
					},
					{
						"V Negative",
						new Action(this.HandleVNegative)
					},
					{
						"CmdTradeAdd",
						new Action(this.HandleTradeAdd)
					},
					{
						"CmdTradeAllItems",
						new Action(this.HandleTradeAddAll)
					},
					{
						"CmdTradeRemove",
						new Action(this.HandleTradeRemove)
					},
					{
						"CmdVendorActions",
						new Action(this.HandleVendorActions)
					},
					{
						"CmdVendorLook",
						new Action(this.HandleVendorLook)
					},
					{
						"CmdVendorRepair",
						new Action(this.HandleVendorRepair)
					},
					{
						"CmdVendorRead",
						new Action(this.HandleVendorRead)
					},
					{
						"CmdVendorRecharge",
						new Action(this.HandleVendorRecharge)
					},
					{
						"CmdVendorExamine",
						new Action(this.HandleVendorExamine)
					}
				};
			}
			if (this.context.data.type == TradeLineDataType.Category)
			{
				this.context.menuOptionDescriptions = (this.context.data.collapsed ? TradeLine.categoryExpandOptions : TradeLine.categoryCollapseOptions);
			}
			if (this.context.data.type == TradeLineDataType.Item)
			{
				this.context.menuOptionDescriptions = TradeLine.itemOptions;
			}
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x000EC90A File Offset: 0x000EAB0A
		public void HandleTradeSome()
		{
			if (this.context.data.go != null)
			{
				this.screen.HandleTradeSome(this);
			}
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x000EC92A File Offset: 0x000EAB2A
		public void HandleVPositive()
		{
			if (this.context.data.go == null)
			{
				this.screen.HandleVPositive();
			}
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x000EC949 File Offset: 0x000EAB49
		public void HandleVNegative()
		{
			if (this.context.data.go == null)
			{
				this.screen.HandleVNegative();
			}
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x000EC968 File Offset: 0x000EAB68
		public async void HandleTradeAddAll()
		{
			if (this.context.data.go != null)
			{
				if (this.context.data.numberSelected > 0)
				{
					TradeLineData tradeLineData = this.context.data;
					int num = await this.screen.setHowManySelected(this.context.data.go, 0);
					tradeLineData.numberSelected = num;
					tradeLineData = null;
					this.setData(this.context.data);
					this.screen.UpdateTotals();
				}
				else
				{
					TradeLineData tradeLineData = this.context.data;
					int num = await this.screen.setHowManySelected(this.context.data.go, this.context.data.go.Count);
					tradeLineData.numberSelected = num;
					tradeLineData = null;
					this.setData(this.context.data);
					this.screen.UpdateTotals();
				}
			}
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x000EC9A0 File Offset: 0x000EABA0
		public async void HandleVendorActions()
		{
			if (this.context.data.go != null)
			{
				string selected = null;
				await APIDispatch.RunAndWaitAsync(delegate
				{
					selected = TradeUI.ShowVendorActions(this.context.data.go, TradeScreen.Trader, true);
				});
				if (selected == "Recharge")
				{
					this.screen.ClearAndSetupTradeUI();
				}
				if (selected == "Identify")
				{
					this.screen.ClearAndSetupTradeUI();
				}
				this.screen.UpdateViewFromData();
				if (selected == "Add to trade")
				{
					this.HandleTradeSome();
				}
			}
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x000EC9D8 File Offset: 0x000EABD8
		public async void HandleVendorLook()
		{
			if (this.context.data.go != null)
			{
				await APIDispatch.RunAndWaitAsync(delegate
				{
					TradeUI.DoVendorLook(this.context.data.go, TradeScreen.Trader);
				});
				this.screen.UpdateViewFromData();
			}
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x000ECA10 File Offset: 0x000EAC10
		public async void HandleVendorExamine()
		{
			if (this.context.data.go != null)
			{
				await APIDispatch.RunAndWaitAsync(delegate
				{
					TradeUI.DoVendorExamine(this.context.data.go, TradeScreen.Trader);
				});
				this.screen.UpdateViewFromData();
			}
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x000ECA48 File Offset: 0x000EAC48
		public async void HandleVendorRecharge()
		{
			if (this.context.data.go != null)
			{
				await APIDispatch.RunAndWaitAsync<bool>(() => TradeUI.DoVendorRecharge(this.context.data.go, TradeScreen.Trader));
				this.screen.ClearAndSetupTradeUI();
				this.screen.UpdateViewFromData();
			}
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x000ECA80 File Offset: 0x000EAC80
		public async void HandleVendorRead()
		{
			if (this.context.data.go != null && (TradeScreen.Trader != null && TradeScreen.Trader.GetIntProperty("Librarian", 0) != 0))
			{
				await APIDispatch.RunAndWaitAsync(delegate
				{
					TradeUI.DoVendorRead(this.context.data.go, TradeScreen.Trader);
				});
				this.screen.UpdateViewFromData();
			}
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x000ECAB8 File Offset: 0x000EACB8
		public async void HandleVendorRepair()
		{
			if (this.context.data.go != null)
			{
				await APIDispatch.RunAndWaitAsync(delegate
				{
					TradeUI.DoVendorRepair(this.context.data.go, TradeScreen.Trader);
				});
				this.screen.UpdateViewFromData();
			}
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x000ECAF0 File Offset: 0x000EACF0
		public async void HandleTradeAdd()
		{
			if (this.context.data.go != null)
			{
				TradeLineData tradeLineData = this.context.data;
				int num = await this.screen.incrementHowManySelected(this.context.data.go, 1);
				tradeLineData.numberSelected = num;
				tradeLineData = null;
				this.setData(this.context.data);
				this.screen.UpdateTotals();
			}
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x000ECB28 File Offset: 0x000EAD28
		public async void HandleTradeRemove()
		{
			if (this.context.data.go != null)
			{
				TradeLineData tradeLineData = this.context.data;
				int num = await this.screen.incrementHowManySelected(this.context.data.go, -1);
				tradeLineData.numberSelected = num;
				tradeLineData = null;
				this.setData(this.context.data);
				this.screen.UpdateTotals();
			}
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x000ECB5F File Offset: 0x000EAD5F
		public void HandleNavigationVAxis()
		{
			this.screen.HandleVAxis(NavigationController.currentEvent.axisValue);
			NavigationController.currentEvent.Handle();
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x000ECB80 File Offset: 0x000EAD80
		public void HandleNavigationXAxis()
		{
			this.screen.HandleXAxis(NavigationController.currentEvent.axisValue);
			NavigationController.currentEvent.Handle();
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x000ECBA4 File Offset: 0x000EADA4
		public override void Update()
		{
			TradeLine.Context context = this.context;
			this.tooltipContextActive = context != null && context.IsActive(true);
			if (this.context.IsActive(true) && !TradeLine.dragging && this.context.data.go != null)
			{
				bool flag = false;
				foreach (KeyValuePair<KeyCode, int> keyValuePair in this.intKeys.Where<KeyValuePair<KeyCode, int>>((KeyValuePair<KeyCode, int> kv) => Input.GetKeyDown(kv.Key) && !ControlManager.isKeyMapped(kv.Key, null)))
				{
					this.TradeTypeAmount += keyValuePair.Value.ToString();
					flag = true;
				}
				if (Input.GetKeyDown(KeyCode.Backspace) && !ControlManager.isKeyMapped(KeyCode.Backspace, null) && !string.IsNullOrEmpty(this.TradeTypeAmount))
				{
					this.TradeTypeAmount = this.TradeTypeAmount.Substring(0, this.TradeTypeAmount.Length - 1);
					flag = true;
				}
				if (flag)
				{
					int num = Convert.ToInt32(this.TradeTypeAmount.IsNullOrEmpty() ? 0 : this.TradeTypeAmount);
					if (num > this.context.data.go.Count)
					{
						if (this.TradeTypeAmount.Length > 0 && this.TradeTypeAmount.Last<char>() == '0')
						{
							num = 0;
						}
						else
						{
							num = this.context.data.go.Count;
						}
					}
					if (num < 0)
					{
						num = 0;
					}
					SingletonWindowBase<TradeScreen>.instance.dragIndicatorText.SetText(string.Format("{{{{W|{0}}}}}", num));
					this.screen.setHowManySelected(this.context.data.go, num).ContinueWith(delegate(Task<int> i)
					{
						this.context.data.numberSelected = i.Result;
						this.setData(this.context.data);
						this.screen.UpdateTotals();
					});
				}
			}
			base.Update();
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x000ECD94 File Offset: 0x000EAF94
		public void LateUpdate()
		{
			if (this.context.IsActive(true) && !TradeLine.dragging && !this.wasActive)
			{
				this.wasActive = true;
				if (!this.wasHighlighted && this.isHighlighted)
				{
					this.TradeTypeAmount = "";
				}
				if (!this.wasSelected && this.selected)
				{
					this.TradeTypeAmount = "";
				}
			}
			else if (this.wasActive && !this.context.IsActive(true))
			{
				this.wasActive = false;
			}
			if ((this.selected != this.wasSelected || this.isHighlighted != this.wasHighlighted) && this.background != null)
			{
				this.background.enabled = this.selected || this.isHighlighted;
				if (this.isHighlighted)
				{
					this.background.color = new Color(this.background.color.r, this.background.color.g, this.background.color.b, 0.5f);
				}
				if (this.selected)
				{
					this.background.color = new Color(this.background.color.r, this.background.color.g, this.background.color.b, 1f);
				}
			}
			this.wasSelected = this.selected;
			this.wasHighlighted = this.isHighlighted;
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x000ECF18 File Offset: 0x000EB118
		public void setData(FrameworkDataElement data)
		{
			TradeLineData tradeLineData = data as TradeLineData;
			if (tradeLineData != null)
			{
				this.tooltipGo = tradeLineData.go;
				this.tooltipCompareGo = null;
				this.itemMode.SetActive(tradeLineData.type == TradeLineDataType.Item);
				this.categoryMode.SetActive(tradeLineData.type == TradeLineDataType.Category);
				this.context.data = tradeLineData;
				if (tradeLineData.type == TradeLineDataType.Category)
				{
					string text = ((!tradeLineData.collapsed) ? "-" : "+");
					this.categoryText.SetText("[" + text + "] " + tradeLineData.category);
					this.check.SetText("");
					this.isHighlighted = false;
					if (TutorialManager.currentStep != null)
					{
						ControlId.Assign(base.gameObject, "TradeLine:category:" + tradeLineData.category, false);
						return;
					}
				}
				else
				{
					if (TutorialManager.currentStep != null)
					{
						global::UnityEngine.GameObject gameObject = base.gameObject;
						string text2 = "TradeLine:item:";
						XRL.World.GameObject go = tradeLineData.go;
						ControlId.Assign(gameObject, text2 + ((go != null) ? go.ID : null), false);
					}
					this.icon.FromRenderable(tradeLineData.go.RenderForUI(null, false), true);
					this.check.SetText("");
					if (tradeLineData.numberSelected > 0)
					{
						this.check.SetText(string.Format("{{{{W|{0}}}}}", tradeLineData.numberSelected));
					}
					this.isHighlighted = tradeLineData.numberSelected > 0;
					this.SB.Clear();
					XRL.World.GameObject go2 = tradeLineData.go;
					this.SB.Append(go2.DisplayName);
					this.text.SetText(this.SB.ToString());
					bool isCurrency = tradeLineData.go.IsCurrency;
					string text3 = string.Format("{0:0.00}", TradeUI.GetValue(tradeLineData.go, new bool?(tradeLineData.traderInventory)));
					this.rightFloatText.gameObject.SetActive(true);
					this.rightFloatText.color = new Color(0.2674735f, 0.6836081f, 0.9245283f);
					if (tradeLineData.go.IsCurrency)
					{
						this.rightFloatText.SetText("[{{W|$" + text3 + "}}]");
						return;
					}
					this.rightFloatText.SetText("[$" + text3 + "]");
				}
			}
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x000ED170 File Offset: 0x000EB370
		public int HighlightClosestSpacer(Vector2 screenPosition)
		{
			AbilityManagerSpacer abilityManagerSpacer = ((this.spacers.Count > 0) ? this.spacers[0] : null);
			float num = float.MaxValue;
			int num2 = -1;
			Vector3[] array = new Vector3[4];
			int num3 = 0;
			foreach (AbilityManagerSpacer abilityManagerSpacer2 in this.spacers)
			{
				(abilityManagerSpacer2.transform as RectTransform).GetWorldCorners(array);
				float magnitude = (new Vector2((array[0].x + array[2].x) / 2f, (array[0].y + array[2].y) / 2f) - screenPosition).magnitude;
				if (magnitude < num)
				{
					abilityManagerSpacer = abilityManagerSpacer2;
					num = magnitude;
					num2 = num3;
				}
				abilityManagerSpacer2.image.enabled = false;
				num3++;
			}
			if (abilityManagerSpacer != null)
			{
				abilityManagerSpacer.image.enabled = true;
			}
			return num2;
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x000ED294 File Offset: 0x000EB494
		public static global::UnityEngine.GameObject dragObject
		{
			get
			{
				return SingletonWindowBase<TradeScreen>.instance.dragIndicator;
			}
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x000ED2A0 File Offset: 0x000EB4A0
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (this.context.data.go != null)
			{
				CursorManager.instance.cursorHidden = true;
				CursorManager.instance.forceCursorHidden = true;
				TradeLine.itemDragContext = new NavigationController.DragContext(delegate
				{
					this.OnEndDrag(null);
				});
				TradeLine.dragObject.SetActive(true);
				TradeLine.dragObject.transform.SetParent(UIManager.mainCanvas.transform, true);
				TradeLine.dragObject.GetComponent<CanvasGroup>().interactable = false;
				TradeLine.dragObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
				SingletonWindowBase<TradeScreen>.instance.dragIndicatorText.SetText(string.Format("{{{{W|{0}}}}}", this.screen.howManySelected(this.context.data.go)));
				RectTransform component = TradeLine.dragObject.GetComponent<RectTransform>();
				component.pivot = new Vector2(0f, 0f);
				component.anchoredPosition = Input.mousePosition / UIManager.mainCanvas.scaleFactor;
				TradeLine.dragStartPosition = Input.mousePosition;
				TradeLine.itemBeingDragged = this.context.data;
				TradeLine.startAlpha = base.GetComponent<CanvasGroup>().alpha;
				TradeLine.dragging = true;
				NavigationController.BeginDragWithContext(TradeLine.itemDragContext, delegate
				{
					this.OnEndDrag(null);
				});
			}
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x000ED3F8 File Offset: 0x000EB5F8
		public async void OnDrag(PointerEventData eventData)
		{
			if (TradeLine.dragging)
			{
				RectTransform component = TradeLine.dragObject.GetComponent<RectTransform>();
				component.anchorMin = new Vector2(0.5f, 0.5f);
				component.anchorMax = new Vector2(0.5f, 0.5f);
				component.anchoredPosition = base.ScreenToRectPos(eventData.position, UIManager.mainCanvas, UIManager.mainCanvas.transform as RectTransform);
				float num = Input.mousePosition.x - TradeLine.dragStartPosition.x;
				int num2 = (int)Math.Max(0f, Math.Min((float)TradeLine.dragStartQuantity + num / 200f * (float)this.context.data.entry.GO.Count, (float)this.context.data.entry.GO.Count));
				SingletonWindowBase<TradeScreen>.instance.dragIndicatorText.SetText(string.Format("{{{{W|{0}}}}}", num2));
				if (this.screen.IsGoingToWarn(this.context.data.go))
				{
					this.OnEndDrag(null);
				}
				TradeLineData tradeLineData = this.context.data;
				int num3 = await this.screen.setHowManySelected(this.context.data.go, num2);
				tradeLineData.numberSelected = num3;
				tradeLineData = null;
				TradeLine.dragStartQuantity = this.context.data.numberSelected;
				this.setData(this.context.data);
				this.screen.UpdateTotals();
			}
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x000ED438 File Offset: 0x000EB638
		public void OnEndDrag(PointerEventData eventData)
		{
			if (TradeLine.dragging)
			{
				CursorManager.instance.cursorHidden = false;
				CursorManager.instance.forceCursorHidden = false;
				TradeLine.dragObject.SetActive(false);
				TradeLine.dragging = false;
				TradeLine.itemBeingDragged = null;
				NavigationController.EndDragWithContext(TradeLine.itemDragContext);
				TradeLine.itemDragContext = null;
			}
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x000ED48C File Offset: 0x000EB68C
		public override void OnPointerClick(PointerEventData eventData)
		{
			if (this.context.IsActive(true) && !TradeLine.dragging)
			{
				if (eventData.button == PointerEventData.InputButton.Left)
				{
					if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
					{
						this.HandleTradeSome();
					}
					else
					{
						this.HandleTradeAddAll();
					}
				}
				else if (eventData.button == PointerEventData.InputButton.Right)
				{
					this.HandleVendorActions();
				}
			}
			base.OnPointerClick(eventData);
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x000ED4F4 File Offset: 0x000EB6F4
		public async void OnScroll(PointerEventData eventData)
		{
			if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
			{
				eventData.Use();
				int num;
				if (Input.mouseScrollDelta.y > 0f)
				{
					num = 1;
				}
				else
				{
					num = -1;
				}
				int num2 = Math.Max(0, Math.Min(this.context.data.numberSelected + num, this.context.data.entry.GO.Count));
				SingletonWindowBase<TradeScreen>.instance.dragIndicatorText.SetText(string.Format("{{{{W|{0}}}}}", num2));
				TradeLineData tradeLineData = this.context.data;
				int num3 = await this.screen.setHowManySelected(this.context.data.go, num2);
				tradeLineData.numberSelected = num3;
				tradeLineData = null;
				this.setData(this.context.data);
				this.screen.UpdateTotals();
			}
			else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				eventData.Use();
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(base.gameObject.transform.parent.gameObject, eventData, ExecuteEvents.scrollHandler);
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(base.gameObject.transform.parent.gameObject, eventData, ExecuteEvents.scrollHandler);
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(base.gameObject.transform.parent.gameObject, eventData, ExecuteEvents.scrollHandler);
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(base.gameObject.transform.parent.gameObject, eventData, ExecuteEvents.scrollHandler);
			}
			else
			{
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(base.gameObject.transform.parent.gameObject, eventData, ExecuteEvents.scrollHandler);
			}
		}

		// Token: 0x04001834 RID: 6196
		public TradeLine.Context context = new TradeLine.Context();

		// Token: 0x04001835 RID: 6197
		public global::UnityEngine.GameObject categoryMode;

		// Token: 0x04001836 RID: 6198
		public global::UnityEngine.GameObject itemMode;

		// Token: 0x04001837 RID: 6199
		public UITextSkin categoryText;

		// Token: 0x04001838 RID: 6200
		public UITextSkin categoryExpanderText;

		// Token: 0x04001839 RID: 6201
		public UITextSkin text;

		// Token: 0x0400183A RID: 6202
		public UITextSkin check;

		// Token: 0x0400183B RID: 6203
		public UITextSkin hotkey;

		// Token: 0x0400183C RID: 6204
		public global::UnityEngine.GameObject rightFloatSpacer;

		// Token: 0x0400183D RID: 6205
		public UITextSkin rightFloatText;

		// Token: 0x0400183E RID: 6206
		public UIThreeColorProperties icon;

		// Token: 0x0400183F RID: 6207
		public global::UnityEngine.GameObject iconSpacer;

		// Token: 0x04001840 RID: 6208
		public TradeScreen screen;

		// Token: 0x04001841 RID: 6209
		public StringBuilder SB = new StringBuilder();

		// Token: 0x04001842 RID: 6210
		public static List<MenuOption> categoryExpandOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Expand"
			}
		};

		// Token: 0x04001843 RID: 6211
		public static List<MenuOption> categoryCollapseOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Select"
			}
		};

		// Token: 0x04001844 RID: 6212
		public static List<MenuOption> itemOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Select"
			}
		};

		// Token: 0x04001845 RID: 6213
		public Image background;

		// Token: 0x04001846 RID: 6214
		public bool wasSelected;

		// Token: 0x04001847 RID: 6215
		public bool wasHighlighted;

		// Token: 0x04001848 RID: 6216
		public bool isHighlighted;

		// Token: 0x04001849 RID: 6217
		public Dictionary<KeyCode, int> intKeys = new Dictionary<KeyCode, int>
		{
			{
				KeyCode.Alpha0,
				0
			},
			{
				KeyCode.Alpha1,
				1
			},
			{
				KeyCode.Alpha2,
				2
			},
			{
				KeyCode.Alpha3,
				3
			},
			{
				KeyCode.Alpha4,
				4
			},
			{
				KeyCode.Alpha5,
				5
			},
			{
				KeyCode.Alpha6,
				6
			},
			{
				KeyCode.Alpha7,
				7
			},
			{
				KeyCode.Alpha8,
				8
			},
			{
				KeyCode.Alpha9,
				9
			}
		};

		// Token: 0x0400184A RID: 6218
		private bool wasActive;

		// Token: 0x0400184B RID: 6219
		private string TradeTypeAmount = "";

		// Token: 0x0400184C RID: 6220
		private List<AbilityManagerSpacer> spacers = new List<AbilityManagerSpacer>();

		// Token: 0x0400184D RID: 6221
		public static bool dragging = false;

		// Token: 0x0400184E RID: 6222
		public static TradeLineData itemBeingDragged;

		// Token: 0x0400184F RID: 6223
		public static float startAlpha = 1f;

		// Token: 0x04001850 RID: 6224
		public static Vector2 dragStartPosition;

		// Token: 0x04001851 RID: 6225
		public static int dragStartQuantity;

		// Token: 0x04001852 RID: 6226
		public static NavigationController.DragContext itemDragContext;

		// Token: 0x020018CA RID: 6346
		public class Context : NavigationContext
		{
			// Token: 0x04006389 RID: 25481
			public TradeLineData data;
		}
	}
}
