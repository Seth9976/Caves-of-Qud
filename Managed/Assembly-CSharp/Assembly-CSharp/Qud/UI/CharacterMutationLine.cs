using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x0200045E RID: 1118
	public class CharacterMutationLine : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06002A52 RID: 10834 RVA: 0x000DE35E File Offset: 0x000DC55E
		public bool selected
		{
			get
			{
				CharacterMutationLine.Context context = this.context;
				return context != null && context.IsActive(true);
			}
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x000DE372 File Offset: 0x000DC572
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000DE37C File Offset: 0x000DC57C
		public void SetupContexts(ScrollChildContext scrollContext)
		{
			scrollContext.proxyTo = this.context;
			CharacterMutationLine.Context context = this.context;
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

		// Token: 0x06002A55 RID: 10837 RVA: 0x000DE3CC File Offset: 0x000DC5CC
		public void setData(FrameworkDataElement data)
		{
			CharacterMutationLineData characterMutationLineData = data as CharacterMutationLineData;
			if (characterMutationLineData != null)
			{
				this.context.data = characterMutationLineData;
				if (!characterMutationLineData.mutation.ShouldShowLevel())
				{
					this.text.SetText("{{y|" + characterMutationLineData.mutation.GetDisplayName(true) + "}}");
					return;
				}
				string text = "C";
				if (characterMutationLineData.mutation.Level > characterMutationLineData.mutation.BaseLevel)
				{
					if (characterMutationLineData.mutation.Level > characterMutationLineData.mutation.GetMutationCap())
					{
						text = "M";
					}
					else
					{
						text = "G";
					}
				}
				else if (characterMutationLineData.mutation.Level < characterMutationLineData.mutation.BaseLevel)
				{
					text = "R";
				}
				this.text.SetText(string.Format("{{{{y|{0} ({{{{{1}|{2}}}}})}}}}", characterMutationLineData.mutation.GetDisplayName(true), text, characterMutationLineData.mutation.GetUIDisplayLevel()));
			}
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x000DE4BE File Offset: 0x000DC6BE
		public void OnPointerClick(PointerEventData eventData)
		{
			this.context.IsActive(true);
		}

		// Token: 0x04001630 RID: 5680
		private CharacterMutationLine.Context context = new CharacterMutationLine.Context();

		// Token: 0x04001631 RID: 5681
		public UITextSkin text;

		// Token: 0x04001632 RID: 5682
		public StringBuilder SB = new StringBuilder();

		// Token: 0x04001633 RID: 5683
		public static List<MenuOption> categoryExpandOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Expand"
			}
		};

		// Token: 0x04001634 RID: 5684
		public static List<MenuOption> categoryCollapseOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Collapse"
			}
		};

		// Token: 0x02001872 RID: 6258
		public class Context : NavigationContext
		{
			// Token: 0x04006272 RID: 25202
			public CharacterMutationLineData data;
		}
	}
}
