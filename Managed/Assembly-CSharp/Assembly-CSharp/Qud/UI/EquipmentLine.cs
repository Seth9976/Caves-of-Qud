using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;
using XRL.World.Anatomy;

namespace Qud.UI
{
	// Token: 0x02000466 RID: 1126
	public class EquipmentLine : BaseLineWithTooltip, IFrameworkControl, IFrameworkControlSubcontexts, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06002AA5 RID: 10917 RVA: 0x000E0DE8 File Offset: 0x000DEFE8
		public bool selected
		{
			get
			{
				EquipmentLine.Context context = this.context;
				return context != null && context.IsActive(true);
			}
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000E0DFC File Offset: 0x000DEFFC
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000E0E04 File Offset: 0x000DF004
		public void SetupContexts(ScrollChildContext scrollContext)
		{
			scrollContext.proxyTo = this.context;
			EquipmentLine.Context context = this.context;
			if (context.axisHandlers == null)
			{
				context.axisHandlers = new Dictionary<InputAxisTypes, Action>();
			}
			context = this.context;
			if (context.commandHandlers == null)
			{
				context.commandHandlers = new Dictionary<string, Action>();
			}
			if (scrollContext.commandHandlers == null)
			{
				scrollContext.commandHandlers = new Dictionary<string, Action>();
			}
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x000E0E68 File Offset: 0x000DF068
		public override void Update()
		{
			EquipmentLine.Context context = this.context;
			this.tooltipContextActive = context != null && context.IsActive(true);
			EquipmentLineData data = this.context.data;
			if (data == null || !data.screen.equipmentListController.scrollContext.IsActive(true))
			{
				EquipmentLineData data2 = this.context.data;
				if (data2 == null || !data2.screen.equipmentPaperdollController.scrollContext.IsActive(true))
				{
					goto IL_0141;
				}
			}
			EquipmentLineData data3 = this.context.data;
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
			if (!(flag ?? true) && ControlManager.isHotkeyDown(this.hotkey))
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
			IL_0141:
			bool flag4 = false;
			if ((InventoryLine.dragging || EquipmentLine.dragging) && InventoryLine.draggingOverEquipment == this.context.data && this.dragOverIndicator != null)
			{
				flag4 = true;
			}
			if (this.dragOverIndicator != null)
			{
				if (flag4 && !this.dragOverIndicator.enabled)
				{
					this.dragOverIndicator.enabled = true;
				}
				if (!flag4 && this.dragOverIndicator.enabled)
				{
					this.dragOverIndicator.enabled = false;
				}
			}
			if (this.selected || this.isHighlighted)
			{
				InventoryAndEquipmentStatusScreen.equipmentPaneHighlighted = true;
			}
			base.Update();
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000E104C File Offset: 0x000DF24C
		public void LateUpdate()
		{
			if ((this.selected != this.wasSelected || this.isHighlighted != this.wasHighlighted) && this.highlightImage != null)
			{
				this.highlightImage.enabled = this.selected || this.isHighlighted;
			}
			this.wasSelected = this.selected;
			this.wasHighlighted = this.isHighlighted;
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x000E10B8 File Offset: 0x000DF2B8
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (this.context.data.bodyPart.Equipped == null)
			{
				return;
			}
			DraggableInventoryArea.inside = null;
			EquipmentLine.dragObject = global::UnityEngine.Object.Instantiate<global::UnityEngine.GameObject>(base.gameObject);
			EquipmentLine.dragObject.transform.position = base.gameObject.transform.position;
			EquipmentLine.dragObject.transform.SetParent(UIManager.mainCanvas.transform, true);
			EquipmentLine.dragObject.GetComponent<CanvasGroup>().interactable = false;
			EquipmentLine.dragObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
			RectTransform component = EquipmentLine.dragObject.GetComponent<RectTransform>();
			component.pivot = new Vector2(0f, 0f);
			component.anchoredPosition = Input.mousePosition / UIManager.mainCanvas.scaleFactor;
			EquipmentLine.itemBeingDragged = this.context.data;
			EquipmentLine.startAlpha = base.GetComponent<CanvasGroup>().alpha;
			base.GetComponent<CanvasGroup>().alpha = 0.3f;
			EquipmentLine.dragging = true;
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x000E11C0 File Offset: 0x000DF3C0
		public void OnDrag(PointerEventData eventData)
		{
			if (EquipmentLine.dragging)
			{
				RectTransform component = EquipmentLine.dragObject.GetComponent<RectTransform>();
				component.anchorMin = new Vector2(0.5f, 0.5f);
				component.anchorMax = new Vector2(0.5f, 0.5f);
				component.anchoredPosition = base.ScreenToRectPos(eventData.position, UIManager.mainCanvas, UIManager.mainCanvas.transform as RectTransform);
			}
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x000E1230 File Offset: 0x000DF430
		public void OnEndDrag(PointerEventData eventData)
		{
			global::UnityEngine.Object.Destroy(EquipmentLine.dragObject);
			base.GetComponent<CanvasGroup>().alpha = EquipmentLine.startAlpha;
			EquipmentLine.dragging = false;
			EquipmentLine.dragObject = null;
			EquipmentLine.itemBeingDragged = null;
			if (InventoryLine.draggingOverEquipment != this.context.data)
			{
				this.context.data.screen.HandleDragDrop(this.context.data, InventoryLine.draggingOverEquipment);
			}
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x000E12A0 File Offset: 0x000DF4A0
		public override void OnPointerClick(PointerEventData eventData)
		{
			base.OnPointerClick(eventData);
			if (this.context.IsActive(true) && this.context.Activate(false) && !EquipmentLine.dragging)
			{
				if (this.isDefaultBehavior && eventData.button == PointerEventData.InputButton.Right)
				{
					this.context.data.screen.HandleLookAtDefaultEquipmnent(this.context.data);
					return;
				}
				NavigationController.instance.FireInputButtonEvent(InputButtonTypes.AcceptButton, new Dictionary<string, object> { { "PointerEventData", eventData } });
			}
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x000E1326 File Offset: 0x000DF526
		public override void OnPointerEnter(PointerEventData eventData)
		{
			base.OnPointerEnter(eventData);
			InventoryLine.draggingOverEquipment = this.context.data;
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000E133F File Offset: 0x000DF53F
		public override void OnPointerExit(PointerEventData eventData)
		{
			base.OnPointerExit(eventData);
			InventoryLine.draggingOverEquipment = null;
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x000E1350 File Offset: 0x000DF550
		private async void SetPrimaryLimb()
		{
			BodyPart bodyPart = this.context.data.bodyPart;
			await APIDispatch.RunAndWaitAsync(delegate
			{
				this.context.data.bodyPart.SetAsPreferredDefault(false);
			});
			this.context.data.screen.UpdateViewFromData();
			if (this.context.data.screen.IsPaperdollMode)
			{
				this.context.data.screen.equipmentPaperdollController.scrollContext.ActivateAndEnable();
			}
			else
			{
				this.context.data.screen.equipmentListController.scrollContext.ActivateAndEnable();
			}
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x000E1388 File Offset: 0x000DF588
		public void setData(FrameworkDataElement data)
		{
			if (this.scrollChild == null)
			{
				this.scrollChild = base.GetComponent<FrameworkUnityScrollChild>();
			}
			this.tooltipCompareGo = null;
			EquipmentLineData equipmentLineData = data as EquipmentLineData;
			if (equipmentLineData != null)
			{
				equipmentLineData.line = this;
				if (equipmentLineData.showCybernetics)
				{
					this.tooltipGo = equipmentLineData.bodyPart.Cybernetics ?? null;
				}
				else
				{
					XRL.World.GameObject gameObject;
					if ((gameObject = equipmentLineData.bodyPart.Equipped) == null)
					{
						gameObject = equipmentLineData.bodyPart.DefaultBehavior ?? null;
					}
					this.tooltipGo = gameObject;
				}
				global::UnityEngine.GameObject gameObject2 = base.gameObject;
				string text = "EquipmentLine:Slot:";
				string text2;
				if (equipmentLineData == null)
				{
					text2 = null;
				}
				else
				{
					BodyPart bodyPart = equipmentLineData.bodyPart;
					text2 = ((bodyPart != null) ? bodyPart.Name : null);
				}
				ControlId.Assign(gameObject2, text + text2, true);
				this.context.data = equipmentLineData;
				EquipmentLine.Context context = this.context;
				if (context.commandHandlers == null)
				{
					context.commandHandlers = new Dictionary<string, Action>();
				}
				this.context.commandHandlers["CmdInsert"] = new Action(this.SetPrimaryLimb);
				int num = 0;
				if (Options.IndentBodyParts)
				{
					int? num2;
					if (equipmentLineData == null)
					{
						num2 = null;
					}
					else
					{
						BodyPart bodyPart2 = equipmentLineData.bodyPart;
						num2 = ((bodyPart2 != null) ? new int?(bodyPart2.ParentBody.GetPartDepth((equipmentLineData != null) ? equipmentLineData.bodyPart : null)) : null);
					}
					int? num3 = num2;
					num = num3.GetValueOrDefault();
				}
				string cardinalDescription = equipmentLineData.bodyPart.GetCardinalDescription();
				this.text.SetText((equipmentLineData.bodyPart.Primary ? "{{G|*}}" : "") + ((num == 0) ? cardinalDescription : cardinalDescription.PadLeft(num + cardinalDescription.Length, ' ')));
				object obj2;
				if (!equipmentLineData.showCybernetics)
				{
					object obj;
					if (equipmentLineData == null)
					{
						obj = null;
					}
					else
					{
						BodyPart bodyPart3 = equipmentLineData.bodyPart;
						obj = ((bodyPart3 != null) ? bodyPart3.Equipped : null);
					}
					if ((obj2 = obj) == null)
					{
						object obj3;
						if (equipmentLineData == null)
						{
							obj3 = null;
						}
						else
						{
							BodyPart bodyPart4 = equipmentLineData.bodyPart;
							obj3 = ((bodyPart4 != null) ? bodyPart4.DefaultBehavior : null);
						}
						obj2 = obj3 ?? null;
					}
				}
				else if (equipmentLineData == null)
				{
					obj2 = null;
				}
				else
				{
					BodyPart bodyPart5 = equipmentLineData.bodyPart;
					obj2 = ((bodyPart5 != null) ? bodyPart5.Cybernetics : null);
				}
				XRL.World.GameObject gameObject3 = obj2;
				this.isDefaultBehavior = false;
				if (equipmentLineData.showCybernetics)
				{
					UIThreeColorProperties uithreeColorProperties = this.icon;
					BodyPart bodyPart6 = equipmentLineData.bodyPart;
					IRenderable renderable;
					if (bodyPart6 == null)
					{
						renderable = null;
					}
					else
					{
						XRL.World.GameObject cybernetics = bodyPart6.Cybernetics;
						renderable = ((cybernetics != null) ? cybernetics.RenderForUI("Equipment", false) : null);
					}
					uithreeColorProperties.FromRenderable(renderable ?? null, true);
				}
				else
				{
					BodyPart bodyPart7 = equipmentLineData.bodyPart;
					XRL.World.GameObject gameObject4;
					if ((gameObject4 = ((bodyPart7 != null) ? bodyPart7.Equipped : null)) == null)
					{
						BodyPart bodyPart8 = equipmentLineData.bodyPart;
						gameObject4 = ((bodyPart8 != null) ? bodyPart8.DefaultBehavior : null) ?? null;
					}
					XRL.World.GameObject gameObject5 = gameObject4;
					if (gameObject5 != null)
					{
						XRL.World.GameObject gameObject6 = gameObject5;
						BodyPart bodyPart9 = equipmentLineData.bodyPart;
						if (gameObject6 == ((bodyPart9 != null) ? bodyPart9.DefaultBehavior : null))
						{
							UIThreeColorProperties uithreeColorProperties2 = this.icon;
							RenderEvent renderEvent = gameObject5.RenderForUI("Equipment", false);
							uithreeColorProperties2.FromRenderable((renderEvent != null) ? renderEvent.GreyOutForUI() : null, true);
							this.isDefaultBehavior = true;
						}
						else
						{
							EquipmentLine.partsScrap.Clear();
							equipmentLineData.bodyPart.ParentBody.GetPartsEquippedOn(gameObject5, EquipmentLine.partsScrap);
							bool flag = false;
							if (EquipmentLine.partsScrap.Count == 0 || equipmentLineData.bodyPart != EquipmentLine.partsScrap[0])
							{
								flag = true;
							}
							UIThreeColorProperties uithreeColorProperties3 = this.icon;
							IRenderable renderable2;
							if (!flag)
							{
								renderable2 = ((gameObject5 != null) ? gameObject5.RenderForUI("Equipment", false) : null);
							}
							else if (gameObject5 == null)
							{
								renderable2 = null;
							}
							else
							{
								RenderEvent renderEvent2 = gameObject5.RenderForUI("Equipment", false);
								renderable2 = ((renderEvent2 != null) ? renderEvent2.GreyOutForUI() : null);
							}
							uithreeColorProperties3.FromRenderable(renderable2, true);
						}
					}
					else
					{
						this.icon.FromRenderable((gameObject5 != null) ? gameObject5.RenderForUI("Equipment", false) : null, true);
					}
				}
				if (this.itemText != null)
				{
					this.itemText.SetText(((gameObject3 != null) ? gameObject3.DisplayName : null) ?? "{{K|-}}");
				}
				if (this.frames != null && this.frames.Length != 0)
				{
					string text3 = ((gameObject3 != null) ? gameObject3.GetTagOrStringProperty("EquipmentFrameColors", "----") : null) ?? "----";
					for (int i = 0; i < this.frames.Length; i++)
					{
						if (text3[i] == '-')
						{
							this.frames[i].color = EquipmentLine.DEFAULT_FRAME_COLOR;
						}
						else
						{
							this.frames[i].color = ConsoleLib.Console.ColorUtility.colorFromChar(text3[i]);
						}
					}
				}
				context = this.context;
				if (context.axisHandlers == null)
				{
					context.axisHandlers = new Dictionary<InputAxisTypes, Action>();
				}
				this.context.axisHandlers[InputAxisTypes.NavigationXAxis] = (InventoryAndEquipmentStatusScreen.paginationMode ? new Action(this.XAxis) : null);
				if (this.context.axisHandlers[InputAxisTypes.NavigationXAxis] == null)
				{
					this.context.axisHandlers.Remove(InputAxisTypes.NavigationXAxis);
				}
			}
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x000E1818 File Offset: 0x000DFA18
		public void UpdateHotkey()
		{
			if (!(this.hotkeyText != null))
			{
				this.hotkey = global::UnityEngine.KeyCode.None;
				return;
			}
			EquipmentLine.Context context = this.context;
			bool flag;
			if (context == null)
			{
				flag = null != null;
			}
			else
			{
				EquipmentLineData data = context.data;
				flag = ((data != null) ? data.spread : null) != null;
			}
			if (!flag || ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
			{
				this.hotkey = global::UnityEngine.KeyCode.None;
				this.hotkeyText.SetText("");
				return;
			}
			this.hotkey = this.context.data.spread.codeAt(this.scrollIndex);
			if (this.hotkey == global::UnityEngine.KeyCode.None)
			{
				this.hotkeyText.SetText("");
				return;
			}
			this.hotkeyText.SetText(string.Format("{0})", this.context.data.spread.charAt(this.scrollIndex)));
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x000E18F0 File Offset: 0x000DFAF0
		public void ScrollIndexChanged(int index)
		{
			this.scrollIndex = index;
			this.UpdateHotkey();
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x000E1900 File Offset: 0x000DFB00
		public void XAxis()
		{
			int? axisValue = NavigationController.currentEvent.axisValue;
			int num = 0;
			if (((axisValue.GetValueOrDefault() < num) & (axisValue != null)) && Options.GetOption("OptionPressingRightInInventoryEquips", "") == "Yes")
			{
				this.context.data.screen.HandleDragDrop(this.context.data, null);
			}
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x000E196C File Offset: 0x000DFB6C
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

		// Token: 0x0400169E RID: 5790
		private EquipmentLine.Context context = new EquipmentLine.Context("EquipmentLineContext");

		// Token: 0x0400169F RID: 5791
		public UITextSkin text;

		// Token: 0x040016A0 RID: 5792
		public UITextSkin itemText;

		// Token: 0x040016A1 RID: 5793
		public UIThreeColorProperties icon;

		// Token: 0x040016A2 RID: 5794
		public bool isDefaultBehavior;

		// Token: 0x040016A3 RID: 5795
		public StringBuilder SB = new StringBuilder();

		// Token: 0x040016A4 RID: 5796
		public Image dragOverIndicator;

		// Token: 0x040016A5 RID: 5797
		public static List<MenuOption> categoryExpandOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Expand"
			}
		};

		// Token: 0x040016A6 RID: 5798
		public static List<MenuOption> categoryCollapseOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Collapse"
			}
		};

		// Token: 0x040016A7 RID: 5799
		public FrameworkUnityScrollChild scrollChild;

		// Token: 0x040016A8 RID: 5800
		public Image[] frames;

		// Token: 0x040016A9 RID: 5801
		public bool wasSelected;

		// Token: 0x040016AA RID: 5802
		public bool wasHighlighted;

		// Token: 0x040016AB RID: 5803
		public bool isHighlighted;

		// Token: 0x040016AC RID: 5804
		public Image highlightImage;

		// Token: 0x040016AD RID: 5805
		public static bool dragging = false;

		// Token: 0x040016AE RID: 5806
		public static global::UnityEngine.GameObject dragObject = null;

		// Token: 0x040016AF RID: 5807
		public static EquipmentLineData itemBeingDragged;

		// Token: 0x040016B0 RID: 5808
		public static float startAlpha = 1f;

		// Token: 0x040016B1 RID: 5809
		private static List<BodyPart> partsScrap = new List<BodyPart>();

		// Token: 0x040016B2 RID: 5810
		private static Color DEFAULT_FRAME_COLOR = ConsoleLib.Console.ColorUtility.FromWebColor("3A5A66");

		// Token: 0x040016B3 RID: 5811
		public UITextSkin hotkeyText;

		// Token: 0x040016B4 RID: 5812
		private int scrollIndex = -1;

		// Token: 0x040016B5 RID: 5813
		private global::UnityEngine.KeyCode hotkey;

		// Token: 0x040016B6 RID: 5814
		private List<AbilityManagerSpacer> spacers = new List<AbilityManagerSpacer>();

		// Token: 0x02001886 RID: 6278
		public class Context : NavigationContext
		{
			// Token: 0x0600E0D8 RID: 57560 RVA: 0x0051B042 File Offset: 0x00519242
			public Context()
			{
			}

			// Token: 0x0600E0D9 RID: 57561 RVA: 0x0051B04A File Offset: 0x0051924A
			public Context(string debugname)
				: base(debugname)
			{
			}

			// Token: 0x040062A8 RID: 25256
			public EquipmentLineData data;
		}
	}
}
