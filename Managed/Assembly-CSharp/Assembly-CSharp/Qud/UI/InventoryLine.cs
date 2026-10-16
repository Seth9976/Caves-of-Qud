using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;
using XRL.World.Anatomy;

namespace Qud.UI
{
	// Token: 0x02000469 RID: 1129
	public class InventoryLine : BaseLineWithTooltip, IFrameworkControl, IFrameworkControlSubcontexts, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IScrollHandler
	{
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06002AE2 RID: 10978 RVA: 0x000E3313 File Offset: 0x000E1513
		public bool selected
		{
			get
			{
				InventoryLine.Context context = this.context;
				return context != null && context.IsActive(true);
			}
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x000E3327 File Offset: 0x000E1527
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000E3330 File Offset: 0x000E1530
		public void SetupContexts(ScrollChildContext scrollContext)
		{
			scrollContext.proxyTo = this.context;
			InventoryLine.Context context = this.context;
			if (context.commandHandlers == null)
			{
				context.commandHandlers = new Dictionary<string, Action>
				{
					{
						"InventoryQuickDrop",
						new Action(this.HandleQuickDrop)
					},
					{
						"InventoryQuickEat",
						new Action(this.HandleQuickEat)
					},
					{
						"InventoryQuickDrink",
						new Action(this.HandleQuickDrink)
					},
					{
						"InventoryQuickApply",
						new Action(this.HandleQuickApply)
					}
				};
			}
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x000E33C0 File Offset: 0x000E15C0
		public void LateUpdate()
		{
			if ((this.selected != this.wasSelected || this.isHighlighted != this.wasHighlighted) && this.highlight != null)
			{
				if (!this.highlight.enabled)
				{
					this.highlight.enabled = true;
				}
				this.highlight.color = new Color(this.highlight.color.r, this.highlight.color.g, this.highlight.color.b, (this.selected || this.isHighlighted) ? 0.2509804f : 0f);
			}
			this.wasSelected = this.selected;
			this.wasHighlighted = this.isHighlighted;
			if (this.selected || this.isHighlighted)
			{
				InventoryAndEquipmentStatusScreen.equipmentPaneHighlighted = false;
			}
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x000E349D File Offset: 0x000E169D
		public void HandleQuickDrop()
		{
			this.context.data.screen.HandleQuickDrop(this.context.data);
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x000E34BF File Offset: 0x000E16BF
		public void HandleQuickEat()
		{
			this.context.data.screen.HandleQuickEat(this.context.data);
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x000E34E1 File Offset: 0x000E16E1
		public void HandleQuickDrink()
		{
			this.context.data.screen.HandleQuickDrink(this.context.data);
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x000E3503 File Offset: 0x000E1703
		public void HandleQuickApply()
		{
			this.context.data.screen.HandleQuickApply(this.context.data);
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x000E3528 File Offset: 0x000E1728
		public override void Update()
		{
			InventoryLine.Context context = this.context;
			this.tooltipContextActive = context != null && context.IsActive(true);
			InventoryLineData data = this.context.data;
			if (data == null || !data.screen.equipmentListController.scrollContext.IsActive(true))
			{
				InventoryLineData data2 = this.context.data;
				if (data2 == null || !data2.screen.equipmentPaperdollController.scrollContext.IsActive(true))
				{
					InventoryLineData data3 = this.context.data;
					bool? flag;
					if (data3 == null)
					{
						flag = null;
					}
					else
					{
						ScrollContext<FrameworkDataElement, NavigationContext> scrollContext = data3.screen.inventoryController.scrollContext;
						flag = ((scrollContext != null) ? new bool?(scrollContext.IsActive(true)) : null);
					}
					if ((flag ?? true) && ControlManager.isHotkeyDown(this.hotkey))
					{
						FrameworkUnityScrollChild frameworkUnityScrollChild = this.scrollChild;
						bool? flag2;
						if (frameworkUnityScrollChild == null)
						{
							flag2 = null;
						}
						else
						{
							ScrollChildContext scrollContext2 = frameworkUnityScrollChild.scrollContext;
							flag2 = ((scrollContext2 != null) ? new bool?(scrollContext2.IsActive(true)) : null);
						}
						bool? flag3 = flag2;
						if (flag3.GetValueOrDefault())
						{
							this.context.data.screen.HandleSelectItem(this.context.data);
						}
						else
						{
							this.context.Activate(false);
						}
					}
				}
			}
			if (this.lastDeviceType != ControlManager.activeControllerType)
			{
				this.setData(this.context.data);
				this.lastDeviceType = ControlManager.activeControllerType;
			}
			base.Update();
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x000E36A8 File Offset: 0x000E18A8
		public void setData(FrameworkDataElement data)
		{
			if (this.scrollChild == null)
			{
				this.scrollChild = base.GetComponent<FrameworkUnityScrollChild>();
			}
			InventoryLineData inventoryLineData = data as InventoryLineData;
			if (inventoryLineData != null)
			{
				if (data != this.context.data)
				{
					this.hotkeyText.SetText("");
				}
				this.context.data = inventoryLineData;
				this.hotkey = KeyCode.None;
				if (this.dotImage.enabled != inventoryLineData.category)
				{
					this.dotImage.enabled = inventoryLineData.category;
				}
				ControlId.Assign(base.gameObject, null, false);
				if (inventoryLineData.category)
				{
					if (this.hotkeySpacer.activeSelf)
					{
						this.hotkeySpacer.SetActive(false);
					}
					if (!this.categoryMode.activeSelf)
					{
						this.categoryMode.SetActive(true);
						this.itemMode.SetActive(false);
					}
					this.categoryLabel.SetText(inventoryLineData.categoryName);
					this.categoryExpandLabel.SetText(inventoryLineData.categoryExpanded ? "[-]" : "[+]");
					this.tooltipGo = null;
					this.tooltipCompareGo = null;
					if (Options.ShowNumberOfItems)
					{
						this.categoryWeightText.SetText(string.Format("|{0} items|{1} lbs.|", inventoryLineData.categoryAmount, inventoryLineData.categoryWeight));
					}
					else
					{
						this.categoryWeightText.SetText(string.Format("|{0} lbs.|", inventoryLineData.categoryWeight));
					}
					this.itemWeightText.SetText("");
					ControlId.Assign(base.gameObject, "InventoryLine:Category:" + inventoryLineData.categoryName, false);
				}
				else
				{
					if (!this.hotkeySpacer.activeSelf)
					{
						this.hotkeySpacer.SetActive(true);
					}
					if (this.categoryMode.activeSelf)
					{
						this.categoryMode.SetActive(false);
						this.itemMode.SetActive(true);
					}
					this.tooltipGo = inventoryLineData.go;
					BodyPart comparisonBodypart = The.Player.getComparisonBodypart(inventoryLineData.go);
					global::XRL.World.GameObject gameObject;
					if ((gameObject = ((comparisonBodypart != null) ? comparisonBodypart.Equipped : null)) == null)
					{
						BodyPart comparisonBodypart2 = The.Player.getComparisonBodypart(inventoryLineData.go);
						gameObject = ((comparisonBodypart2 != null) ? comparisonBodypart2.DefaultBehavior : null);
					}
					this.tooltipCompareGo = gameObject;
					this.dotImage.enabled = false;
					this.categoryWeightText.SetText("");
					this.itemWeightText.SetText(string.Format("[{0} lbs.]", inventoryLineData.go.Weight));
					this.text.SetText(inventoryLineData.displayName);
					this.icon.FromRenderable(inventoryLineData.go.RenderForUI("Inventory", false), true);
					if (inventoryLineData != null)
					{
						global::XRL.World.GameObject go = inventoryLineData.go;
						if (((go != null) ? go.IDIfAssigned : null) != null)
						{
							global::UnityEngine.GameObject gameObject2 = base.gameObject;
							string text = "InventoryLine:Item:";
							string text2;
							if (inventoryLineData == null)
							{
								text2 = null;
							}
							else
							{
								global::XRL.World.GameObject go2 = inventoryLineData.go;
								text2 = ((go2 != null) ? go2.IDIfAssigned : null);
							}
							ControlId.Assign(gameObject2, text + text2, false);
						}
					}
				}
				InventoryLine.Context context = this.context;
				if (context.axisHandlers == null)
				{
					context.axisHandlers = new Dictionary<InputAxisTypes, Action>();
				}
				this.context.axisHandlers[InputAxisTypes.NavigationXAxis] = (InventoryAndEquipmentStatusScreen.paginationMode ? new Action(this.XAxis) : null);
				if (this.context.axisHandlers[InputAxisTypes.NavigationXAxis] == null)
				{
					this.context.axisHandlers.Remove(InputAxisTypes.NavigationXAxis);
				}
				this.UpdateHotkey();
			}
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x000E3A00 File Offset: 0x000E1C00
		public void XAxis()
		{
			global::XRL.UI.Framework.Event currentEvent = NavigationController.currentEvent;
			if (this.context.data.category)
			{
				int? num = currentEvent.axisValue;
				int num2 = 0;
				if ((num.GetValueOrDefault() < num2) & (num != null))
				{
					this.context.data.screen.inventoryController.scrollContext.SelectIndex(this.context.data.screen.inventoryController.selectedPosition - this.context.data.categoryOffset);
					this.context.data.screen.SetCategoryExpanded(this.context.data.categoryName, false);
					return;
				}
				num = currentEvent.axisValue;
				num2 = 0;
				if ((num.GetValueOrDefault() > num2) & (num != null))
				{
					this.context.data.screen.SetCategoryExpanded(this.context.data.categoryName, true);
					return;
				}
			}
			else
			{
				int? num = currentEvent.axisValue;
				int num2 = 0;
				if ((num.GetValueOrDefault() < num2) & (num != null))
				{
					this.context.data.screen.inventoryController.scrollContext.SelectIndex(this.context.data.screen.inventoryController.selectedPosition - this.context.data.categoryOffset);
					this.context.data.screen.SetCategoryExpanded(this.context.data.categoryName, false);
					return;
				}
				num = currentEvent.axisValue;
				num2 = 0;
				if ((num.GetValueOrDefault() > num2) & (num != null))
				{
					if (Options.GetOption("OptionPressingRightInInventoryEquips", "") == "Yes")
					{
						this.context.data.screen.HandleDragDropAutoequip(this.context.data);
						return;
					}
					this.context.data.screen.SetCategoryExpanded(this.context.data.categoryName, true);
				}
			}
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x000E3C0C File Offset: 0x000E1E0C
		public void OnBeginDragObject()
		{
			this.hotkeyText.gameObject.SetActive(false);
			base.transform.localScale = new Vector3(1.5f, 1.5f);
			base.GetComponent<HorizontalLayoutGroup>().padding = new RectOffset(-52, 0, 8, 0);
			this.itemWeightText.SetText("");
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x000E3C6C File Offset: 0x000E1E6C
		public void OnBeginDrag(PointerEventData eventData)
		{
			InventoryLine.itemDragContext = new NavigationController.DragContext(delegate
			{
				this.OnEndDrag(null);
			});
			if (this.context.data.category)
			{
				return;
			}
			if (this.context.data.go == null)
			{
				return;
			}
			DraggableInventoryArea.inside = null;
			InventoryLine.dragObject = global::UnityEngine.Object.Instantiate<global::UnityEngine.GameObject>(base.gameObject);
			InventoryLine.dragObject.transform.SetParent(UIManager.mainCanvas.transform, true);
			InventoryLine.dragObject.GetComponent<CanvasGroup>().interactable = false;
			InventoryLine.dragObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
			InventoryLine.dragObject.SendMessage("OnBeginDragObject", SendMessageOptions.DontRequireReceiver);
			RectTransform component = InventoryLine.dragObject.GetComponent<RectTransform>();
			component.pivot = new Vector2(0f, 0f);
			component.anchoredPosition = Input.mousePosition / UIManager.mainCanvas.scaleFactor;
			InventoryLine.itemBeingDragged = this.context.data;
			InventoryLine.startAlpha = base.GetComponent<CanvasGroup>().alpha;
			base.GetComponent<CanvasGroup>().alpha = 0.3f;
			InventoryLine.dragging = true;
			NavigationController.BeginDragWithContext(InventoryLine.itemDragContext, delegate
			{
				this.OnEndDrag(null);
			});
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x000E3DA0 File Offset: 0x000E1FA0
		public void OnDrag(PointerEventData eventData)
		{
			if (InventoryLine.dragging)
			{
				RectTransform component = InventoryLine.dragObject.GetComponent<RectTransform>();
				component.anchorMin = new Vector2(0.5f, 0.5f);
				component.anchorMax = new Vector2(0.5f, 0.5f);
				component.anchoredPosition = base.ScreenToRectPos(eventData.position, UIManager.mainCanvas, UIManager.mainCanvas.transform as RectTransform);
			}
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x000E3E10 File Offset: 0x000E2010
		public void OnEndDrag(PointerEventData eventData)
		{
			global::UnityEngine.Object.Destroy(InventoryLine.dragObject);
			base.GetComponent<CanvasGroup>().alpha = InventoryLine.startAlpha;
			InventoryLine.dragging = false;
			InventoryLine.dragObject = null;
			InventoryLine.itemBeingDragged = null;
			if (InventoryLine.draggingOverEquipment != null)
			{
				this.context.data.screen.HandleDragDrop(this.context.data, InventoryLine.draggingOverEquipment);
			}
			else
			{
				DraggableInventoryArea inside = DraggableInventoryArea.inside;
				if (((inside != null) ? inside.ID : null) == "Equipment")
				{
					this.context.data.screen.HandleDragDropAutoequip(this.context.data);
				}
			}
			NavigationController.EndDragWithContext(InventoryLine.itemDragContext);
			InventoryLine.itemDragContext = null;
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x000E3EC8 File Offset: 0x000E20C8
		public override void OnPointerClick(PointerEventData eventData)
		{
			base.OnPointerClick(eventData);
			if (this.context != null && this.context.Activate(false) && !InventoryLine.dragging)
			{
				NavigationController.instance.FireInputButtonEvent(InputButtonTypes.AcceptButton, new Dictionary<string, object> { { "PointerEventData", eventData } });
			}
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x000E3F18 File Offset: 0x000E2118
		public void UpdateHotkey()
		{
			InventoryLine.Context context = this.context;
			bool flag;
			if (context == null)
			{
				flag = null != null;
			}
			else
			{
				InventoryLineData data = context.data;
				flag = ((data != null) ? data.spread : null) != null;
			}
			if (!flag || ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
			{
				this.hotkey = KeyCode.None;
				this.hotkeyText.SetText("");
				return;
			}
			this.hotkey = this.context.data.spread.codeAt(this.scrollIndex);
			if (this.hotkey == KeyCode.None)
			{
				this.hotkeyText.SetText("");
				return;
			}
			this.hotkeyText.SetText(string.Format("{0})", this.context.data.spread.charAt(this.scrollIndex)));
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x000E3FD7 File Offset: 0x000E21D7
		public void ScrollIndexChanged(int index)
		{
			this.scrollIndex = index;
			this.UpdateHotkey();
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x000E3FE8 File Offset: 0x000E21E8
		public async void OnScroll(PointerEventData eventData)
		{
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				eventData.Use();
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(base.gameObject.transform.parent.gameObject, eventData, ExecuteEvents.scrollHandler);
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(base.gameObject.transform.parent.gameObject, eventData, ExecuteEvents.scrollHandler);
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

		// Token: 0x040016EC RID: 5868
		public global::UnityEngine.GameObject categoryMode;

		// Token: 0x040016ED RID: 5869
		public global::UnityEngine.GameObject itemMode;

		// Token: 0x040016EE RID: 5870
		public UITextSkin categoryLabel;

		// Token: 0x040016EF RID: 5871
		public UITextSkin categoryExpandLabel;

		// Token: 0x040016F0 RID: 5872
		public HotkeySpread spread;

		// Token: 0x040016F1 RID: 5873
		private InventoryLine.Context context = new InventoryLine.Context();

		// Token: 0x040016F2 RID: 5874
		public Image dotImage;

		// Token: 0x040016F3 RID: 5875
		public UITextSkin categoryWeightText;

		// Token: 0x040016F4 RID: 5876
		public UITextSkin itemWeightText;

		// Token: 0x040016F5 RID: 5877
		public UITextSkin text;

		// Token: 0x040016F6 RID: 5878
		public UIThreeColorProperties icon;

		// Token: 0x040016F7 RID: 5879
		public StringBuilder SB = new StringBuilder();

		// Token: 0x040016F8 RID: 5880
		public UITextSkin hotkeyText;

		// Token: 0x040016F9 RID: 5881
		public global::UnityEngine.GameObject hotkeySpacer;

		// Token: 0x040016FA RID: 5882
		public static List<MenuOption> categoryExpandOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Expand"
			}
		};

		// Token: 0x040016FB RID: 5883
		public static List<MenuOption> categoryCollapseOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Collapse"
			}
		};

		// Token: 0x040016FC RID: 5884
		public FrameworkUnityScrollChild scrollChild;

		// Token: 0x040016FD RID: 5885
		public Image background;

		// Token: 0x040016FE RID: 5886
		public Image highlight;

		// Token: 0x040016FF RID: 5887
		public bool wasSelected;

		// Token: 0x04001700 RID: 5888
		public bool wasHighlighted;

		// Token: 0x04001701 RID: 5889
		public bool isHighlighted;

		// Token: 0x04001702 RID: 5890
		private ControlManager.InputDeviceType lastDeviceType = ControlManager.InputDeviceType.Unknown;

		// Token: 0x04001703 RID: 5891
		private List<AbilityManagerSpacer> spacers = new List<AbilityManagerSpacer>();

		// Token: 0x04001704 RID: 5892
		public static bool dragging = false;

		// Token: 0x04001705 RID: 5893
		public static global::UnityEngine.GameObject dragObject = null;

		// Token: 0x04001706 RID: 5894
		public static InventoryLineData itemBeingDragged;

		// Token: 0x04001707 RID: 5895
		public static float startAlpha = 1f;

		// Token: 0x04001708 RID: 5896
		public static EquipmentLineData draggingOverEquipment;

		// Token: 0x04001709 RID: 5897
		public static NavigationController.DragContext itemDragContext;

		// Token: 0x0400170A RID: 5898
		private int scrollIndex = -1;

		// Token: 0x0400170B RID: 5899
		private KeyCode hotkey;

		// Token: 0x020018A2 RID: 6306
		public class Context : NavigationContext
		{
			// Token: 0x04006310 RID: 25360
			public InventoryLineData data;
		}
	}
}
