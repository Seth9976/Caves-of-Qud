using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World.Tinkering;

namespace Qud.UI
{
	// Token: 0x0200047A RID: 1146
	public class TinkeringBitsLine : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
	{
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06002BBD RID: 11197 RVA: 0x000E9D59 File Offset: 0x000E7F59
		public bool selected
		{
			get
			{
				TinkeringBitsLine.Context context = this.context;
				return context != null && context.IsActive(true);
			}
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000E9D6D File Offset: 0x000E7F6D
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000E9D78 File Offset: 0x000E7F78
		public void SetupContexts(ScrollChildContext scrollContext)
		{
			scrollContext.proxyTo = this.context;
			TinkeringBitsLine.Context context = this.context;
			if (context.axisHandlers == null)
			{
				context.axisHandlers = new Dictionary<InputAxisTypes, Action>();
			}
			context = this.context;
			if (context.commandHandlers == null)
			{
				context.commandHandlers = new Dictionary<string, Action>();
			}
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x000E9DC5 File Offset: 0x000E7FC5
		public void LateUpdate()
		{
			this.wasSelected = this.selected;
			this.wasHighlighted = this.isHighlighted;
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x000E9DE0 File Offset: 0x000E7FE0
		public void setData(FrameworkDataElement data)
		{
			TinkeringBitsLineData tinkeringBitsLineData = data as TinkeringBitsLineData;
			if (tinkeringBitsLineData != null)
			{
				this.context.data = tinkeringBitsLineData;
				this.text.SetText("{{" + tinkeringBitsLineData.bit + "}}");
				BitType bitType = BitType.BitMap[tinkeringBitsLineData.cBit];
				if (tinkeringBitsLineData.activeCost == null || !tinkeringBitsLineData.activeCost.ContainsKey(tinkeringBitsLineData.cBit))
				{
					this.amountText.SetText(string.Format("{{{{K|{0}}}}}", tinkeringBitsLineData.amount));
					this.usedIndicator.color = new Color(0f, 0f, 0f, 0f);
					return;
				}
				this.usedIndicator.color = ConsoleLib.Console.ColorUtility.ColorMap[bitType.Color];
				int num = tinkeringBitsLineData.activeCost[tinkeringBitsLineData.cBit];
				if (num == 0)
				{
					this.amountText.SetText(string.Format("{{{{K|{0}}}}}", tinkeringBitsLineData.amount));
					return;
				}
				if (tinkeringBitsLineData.amount >= num)
				{
					this.amountText.SetText(string.Format("{{{{G|{0}}}}}", tinkeringBitsLineData.amount));
					return;
				}
				this.amountText.SetText(string.Format("{{{{R|{0}}}}}", tinkeringBitsLineData.amount));
			}
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x000E9F37 File Offset: 0x000E8137
		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x000E9F3C File Offset: 0x000E813C
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

		// Token: 0x06002BC4 RID: 11204 RVA: 0x000EA060 File Offset: 0x000E8260
		public void OnDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x000EA062 File Offset: 0x000E8262
		public void OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x000EA064 File Offset: 0x000E8264
		public void OnPointerClick(PointerEventData eventData)
		{
			this.context.IsActive(true);
		}

		// Token: 0x040017D9 RID: 6105
		private TinkeringBitsLine.Context context = new TinkeringBitsLine.Context();

		// Token: 0x040017DA RID: 6106
		public Image usedIndicator;

		// Token: 0x040017DB RID: 6107
		public UITextSkin text;

		// Token: 0x040017DC RID: 6108
		public UITextSkin amountText;

		// Token: 0x040017DD RID: 6109
		public StringBuilder SB = new StringBuilder();

		// Token: 0x040017DE RID: 6110
		public static List<MenuOption> categoryExpandOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Expand"
			}
		};

		// Token: 0x040017DF RID: 6111
		public static List<MenuOption> categoryCollapseOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Collapse"
			}
		};

		// Token: 0x040017E0 RID: 6112
		public Image background;

		// Token: 0x040017E1 RID: 6113
		public bool wasSelected;

		// Token: 0x040017E2 RID: 6114
		public bool wasHighlighted;

		// Token: 0x040017E3 RID: 6115
		public bool isHighlighted;

		// Token: 0x040017E4 RID: 6116
		private List<AbilityManagerSpacer> spacers = new List<AbilityManagerSpacer>();

		// Token: 0x020018BE RID: 6334
		public class Context : NavigationContext
		{
			// Token: 0x04006368 RID: 25448
			public TinkeringBitsLineData data;
		}
	}
}
