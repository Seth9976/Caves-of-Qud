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
	// Token: 0x0200046F RID: 1135
	public class MessageLogLine : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
	{
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x000E64A3 File Offset: 0x000E46A3
		public bool selected
		{
			get
			{
				MessageLogLine.Context context = this.context;
				return context != null && context.IsActive(true);
			}
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x000E64B7 File Offset: 0x000E46B7
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x000E64C0 File Offset: 0x000E46C0
		public void SetupContexts(ScrollChildContext scrollContext)
		{
			scrollContext.proxyTo = this.context;
			MessageLogLine.Context context = this.context;
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

		// Token: 0x06002B43 RID: 11075 RVA: 0x000E650D File Offset: 0x000E470D
		public void LateUpdate()
		{
			this.wasSelected = this.selected;
			this.wasHighlighted = this.isHighlighted;
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x000E6528 File Offset: 0x000E4728
		public void setData(FrameworkDataElement data)
		{
			MessageLogLineData messageLogLineData = data as MessageLogLineData;
			if (messageLogLineData != null)
			{
				this.context.data = messageLogLineData;
				this.text.SetText(messageLogLineData.text);
			}
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x000E655D File Offset: 0x000E475D
		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x000E6560 File Offset: 0x000E4760
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

		// Token: 0x06002B47 RID: 11079 RVA: 0x000E6684 File Offset: 0x000E4884
		public void OnDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x000E6686 File Offset: 0x000E4886
		public void OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x000E6688 File Offset: 0x000E4888
		public void OnPointerClick(PointerEventData eventData)
		{
			this.context.IsActive(true);
		}

		// Token: 0x04001767 RID: 5991
		private MessageLogLine.Context context = new MessageLogLine.Context();

		// Token: 0x04001768 RID: 5992
		public UITextSkin text;

		// Token: 0x04001769 RID: 5993
		public StringBuilder SB = new StringBuilder();

		// Token: 0x0400176A RID: 5994
		public static List<MenuOption> categoryExpandOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Expand"
			}
		};

		// Token: 0x0400176B RID: 5995
		public static List<MenuOption> categoryCollapseOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Collapse"
			}
		};

		// Token: 0x0400176C RID: 5996
		public Image background;

		// Token: 0x0400176D RID: 5997
		public bool wasSelected;

		// Token: 0x0400176E RID: 5998
		public bool wasHighlighted;

		// Token: 0x0400176F RID: 5999
		public bool isHighlighted;

		// Token: 0x04001770 RID: 6000
		private List<AbilityManagerSpacer> spacers = new List<AbilityManagerSpacer>();

		// Token: 0x020018AD RID: 6317
		public class Context : NavigationContext
		{
			// Token: 0x04006336 RID: 25398
			public MessageLogLineData data;
		}
	}
}
