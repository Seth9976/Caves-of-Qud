using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x0200045C RID: 1116
	public class CharacterEffectLine : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
	{
		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06002A43 RID: 10819 RVA: 0x000DE096 File Offset: 0x000DC296
		public bool selected
		{
			get
			{
				CharacterEffectLine.Context context = this.context;
				return context != null && context.IsActive(true);
			}
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000DE0AA File Offset: 0x000DC2AA
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x000DE0B4 File Offset: 0x000DC2B4
		public void SetupContexts(ScrollChildContext scrollContext)
		{
			scrollContext.proxyTo = this.context;
			CharacterEffectLine.Context context = this.context;
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

		// Token: 0x06002A46 RID: 10822 RVA: 0x000DE101 File Offset: 0x000DC301
		public void LateUpdate()
		{
			this.wasSelected = this.selected;
			this.wasHighlighted = this.isHighlighted;
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x000DE11C File Offset: 0x000DC31C
		public void setData(FrameworkDataElement data)
		{
			CharacterEffectLineData characterEffectLineData = data as CharacterEffectLineData;
			if (characterEffectLineData != null)
			{
				this.context.data = characterEffectLineData;
				this.text.SetText(characterEffectLineData.effect.DisplayName ?? "");
			}
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x000DE15F File Offset: 0x000DC35F
		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x000DE164 File Offset: 0x000DC364
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

		// Token: 0x06002A4A RID: 10826 RVA: 0x000DE288 File Offset: 0x000DC488
		public void OnDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x000DE28A File Offset: 0x000DC48A
		public void OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x000DE28C File Offset: 0x000DC48C
		public void OnPointerClick(PointerEventData eventData)
		{
			this.context.IsActive(true);
		}

		// Token: 0x04001625 RID: 5669
		private CharacterEffectLine.Context context = new CharacterEffectLine.Context();

		// Token: 0x04001626 RID: 5670
		public UITextSkin text;

		// Token: 0x04001627 RID: 5671
		public StringBuilder SB = new StringBuilder();

		// Token: 0x04001628 RID: 5672
		public static List<MenuOption> categoryExpandOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Expand"
			}
		};

		// Token: 0x04001629 RID: 5673
		public static List<MenuOption> categoryCollapseOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Collapse"
			}
		};

		// Token: 0x0400162A RID: 5674
		public Image background;

		// Token: 0x0400162B RID: 5675
		public bool wasSelected;

		// Token: 0x0400162C RID: 5676
		public bool wasHighlighted;

		// Token: 0x0400162D RID: 5677
		public bool isHighlighted;

		// Token: 0x0400162E RID: 5678
		private List<AbilityManagerSpacer> spacers = new List<AbilityManagerSpacer>();

		// Token: 0x02001871 RID: 6257
		public class Context : NavigationContext
		{
			// Token: 0x04006271 RID: 25201
			public CharacterEffectLineData data;
		}
	}
}
