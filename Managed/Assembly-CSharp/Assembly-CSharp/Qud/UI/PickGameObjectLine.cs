using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using XRL;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x02000453 RID: 1107
	public class PickGameObjectLine : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
	{
		// Token: 0x060029E9 RID: 10729 RVA: 0x000DBCE5 File Offset: 0x000D9EE5
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x000DBCF0 File Offset: 0x000D9EF0
		public void SetupContexts(ScrollChildContext scrollContext)
		{
			scrollContext.proxyTo = this.context;
			PickGameObjectLine.Context context = this.context;
			if (context.axisHandlers == null)
			{
				context.axisHandlers = new Dictionary<InputAxisTypes, Action>
				{
					{
						InputAxisTypes.NavigationVAxis,
						new Action(this.HandleNavigationVAxis)
					},
					{
						InputAxisTypes.NavigationXAxis,
						new Action(this.HandleNavigationXAxis)
					}
				};
			}
			if (this.context.data.type == PickGameObjectLineDataType.Category)
			{
				this.context.menuOptionDescriptions = (this.context.data.collapsed ? PickGameObjectLine.categoryExpandOptions : PickGameObjectLine.categoryCollapseOptions);
			}
			if (this.context.data.type == PickGameObjectLineDataType.Item)
			{
				this.context.menuOptionDescriptions = PickGameObjectLine.itemOptions;
			}
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000DBDA7 File Offset: 0x000D9FA7
		public void HandleNavigationVAxis()
		{
			this.screen.HandleVAxis(NavigationController.currentEvent.axisValue);
			NavigationController.currentEvent.Handle();
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x000DBDC8 File Offset: 0x000D9FC8
		public void HandleNavigationXAxis()
		{
			this.screen.HandleXAxis(NavigationController.currentEvent.axisValue);
			NavigationController.currentEvent.Handle();
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x000DBDEC File Offset: 0x000D9FEC
		public void setData(FrameworkDataElement data)
		{
			PickGameObjectLineData pickGameObjectLineData = data as PickGameObjectLineData;
			if (pickGameObjectLineData != null)
			{
				if (pickGameObjectLineData.style == PickGameObjectLineDataStyle.Interact)
				{
					this.check.SetText("");
				}
				this.context.data = pickGameObjectLineData;
				if (pickGameObjectLineData.go == null)
				{
					string text = ((!pickGameObjectLineData.collapsed) ? "-" : "+");
					this.text.SetText(string.Concat(new string[] { "[", text, "] {{K|", pickGameObjectLineData.category, "}}" }));
					this.icon.gameObject.SetActive(false);
					this.iconSpacer.SetActive(false);
					this.rightFloatText.SetText("");
					ControlId.Assign(base.gameObject, "PickGameObject:Category:" + (((pickGameObjectLineData != null) ? pickGameObjectLineData.category : null) ?? "(null)"), false);
				}
				else
				{
					this.icon.gameObject.SetActive(true);
					this.iconSpacer.SetActive(true);
					this.icon.FromRenderable(pickGameObjectLineData.go.RenderForUI(null, false), true);
					this.SB.Clear();
					global::XRL.World.GameObject go = pickGameObjectLineData.go;
					this.SB.Append(go.DisplayName);
					if (PickGameObjectScreen.NotePlayerOwned && go.OwnedByPlayer)
					{
						this.SB.Append(" {{G|[owned by you]}}");
					}
					if (PickGameObjectScreen.ShowContext)
					{
						this.SB.Append(" [" + go.GetListDisplayContext(The.Player) + "]");
					}
					this.text.SetText(this.SB.ToString());
					this.rightFloatText.SetText(string.Format("{{{{K|{0}#}}}}", pickGameObjectLineData.go.GetWeight()));
					global::UnityEngine.GameObject gameObject = base.gameObject;
					string text2 = "PickGameObject:Item:";
					string text3;
					if (pickGameObjectLineData == null)
					{
						text3 = null;
					}
					else
					{
						global::XRL.World.GameObject go2 = pickGameObjectLineData.go;
						text3 = ((go2 != null) ? go2.IDIfAssigned : null);
					}
					ControlId.Assign(gameObject, text2 + (text3 ?? "(null)"), false);
				}
				string text4 = (pickGameObjectLineData.indent ? "   " : "");
				if (!string.IsNullOrEmpty(pickGameObjectLineData.hotkeyDescription))
				{
					this.hotkey.SetText(text4 + "{{Y|{{w|" + pickGameObjectLineData.hotkeyDescription + "}})}} ");
					return;
				}
				this.hotkey.SetText(text4 + " ");
			}
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000DC060 File Offset: 0x000DA260
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (SingletonWindowBase<AbilityManagerScreen>.instance.sortMode != AbilityManagerScreen.SortMode.Custom)
			{
				eventData.pointerDrag = null;
				return;
			}
			CursorManager.instance.currentStyle = CursorManager.Style.ResizeNorthSouth;
			this.spacers.Clear();
			this.spacers.AddRange(base.transform.parent.GetComponentsInChildren<AbilityManagerSpacer>());
			this.HighlightClosestSpacer(eventData.position);
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x000DC0C0 File Offset: 0x000DA2C0
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

		// Token: 0x060029F0 RID: 10736 RVA: 0x000DC1E4 File Offset: 0x000DA3E4
		public void OnDrag(PointerEventData eventData)
		{
			this.HighlightClosestSpacer(eventData.position);
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x000DC1F4 File Offset: 0x000DA3F4
		public void OnEndDrag(PointerEventData eventData)
		{
			CursorManager.instance.currentStyle = CursorManager.Style.Pointer;
			int num = this.HighlightClosestSpacer(eventData.position);
			this.spacers[num].image.enabled = false;
			int num2 = base.transform.GetSiblingIndex() / 2;
			if (num == num2 || num == num2 + 1)
			{
				return;
			}
			if (num < num2)
			{
				this.screen.MoveItem(this.context.data, num - num2);
				return;
			}
			this.screen.MoveItem(this.context.data, num - num2 - 1);
		}

		// Token: 0x040015C8 RID: 5576
		private PickGameObjectLine.Context context = new PickGameObjectLine.Context();

		// Token: 0x040015C9 RID: 5577
		public UITextSkin text;

		// Token: 0x040015CA RID: 5578
		public UITextSkin check;

		// Token: 0x040015CB RID: 5579
		public UITextSkin hotkey;

		// Token: 0x040015CC RID: 5580
		public UITextSkin rightFloatText;

		// Token: 0x040015CD RID: 5581
		public UIThreeColorProperties icon;

		// Token: 0x040015CE RID: 5582
		public global::UnityEngine.GameObject iconSpacer;

		// Token: 0x040015CF RID: 5583
		public PickGameObjectScreen screen;

		// Token: 0x040015D0 RID: 5584
		public StringBuilder SB = new StringBuilder();

		// Token: 0x040015D1 RID: 5585
		public static List<MenuOption> categoryExpandOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Expand"
			}
		};

		// Token: 0x040015D2 RID: 5586
		public static List<MenuOption> categoryCollapseOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Collapse"
			}
		};

		// Token: 0x040015D3 RID: 5587
		public static List<MenuOption> itemOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Select"
			}
		};

		// Token: 0x040015D4 RID: 5588
		private List<AbilityManagerSpacer> spacers = new List<AbilityManagerSpacer>();

		// Token: 0x02001861 RID: 6241
		public class Context : NavigationContext
		{
			// Token: 0x04006227 RID: 25127
			public PickGameObjectLineData data;
		}
	}
}
