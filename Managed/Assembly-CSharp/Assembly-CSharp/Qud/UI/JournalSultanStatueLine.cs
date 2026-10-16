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
	// Token: 0x0200046E RID: 1134
	public class JournalSultanStatueLine : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
	{
		// Token: 0x06002B32 RID: 11058 RVA: 0x000E600C File Offset: 0x000E420C
		public void SetHighlight(bool highlightState)
		{
			this.fadedImage.enabled = !highlightState;
			this.highlightedBorder.enabled = highlightState;
			if (this.hasBase)
			{
				this.fadedBase.enabled = !highlightState;
			}
			ColorUtility.TryParseHtmlString(highlightState ? "#EFD03E" : "#235157", out this.text.color);
			this.text.Apply();
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x000E6078 File Offset: 0x000E4278
		public void SetBase(bool State)
		{
			this.hasBase = State;
			this.fullBase.enabled = State;
			this.fadedBase.enabled = this.fadedImage.enabled && State;
			if (State)
			{
				this.fadedImage.rectTransform.localPosition = new Vector3(0f, 6f, 0f);
				this.fullImage.rectTransform.localPosition = new Vector3(0f, 6f, 0f);
				return;
			}
			this.fadedImage.rectTransform.localPosition = new Vector3(0f, 0f, 0f);
			this.fullImage.rectTransform.localPosition = new Vector3(0f, 0f, 0f);
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06002B34 RID: 11060 RVA: 0x000E6144 File Offset: 0x000E4344
		public bool selected
		{
			get
			{
				JournalSultanStatueLine.Context context = this.context;
				return context != null && context.IsActive(true);
			}
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x000E6158 File Offset: 0x000E4358
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x000E6160 File Offset: 0x000E4360
		public void SetupContexts(ScrollChildContext scrollContext)
		{
			scrollContext.proxyTo = this.context;
			JournalSultanStatueLine.Context context = this.context;
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

		// Token: 0x06002B37 RID: 11063 RVA: 0x000E61AD File Offset: 0x000E43AD
		public void LateUpdate()
		{
			this.wasSelected = this.selected;
			this.wasHighlighted = this.isHighlighted;
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x000E61C8 File Offset: 0x000E43C8
		public void setData(FrameworkDataElement data)
		{
			JournalLineData journalLineData = data as JournalLineData;
			if (journalLineData != null)
			{
				this.context.data = journalLineData;
				JournalSultanStatueLine.sb.Clear();
				if (journalLineData.category)
				{
					this.layoutGroup.padding.left = 16;
					if (journalLineData.categoryExpanded)
					{
						JournalSultanStatueLine.sb.Append("[-] ");
					}
					else
					{
						JournalSultanStatueLine.sb.Append("[+] ");
					}
					JournalSultanStatueLine.sb.Append(journalLineData.categoryName);
				}
				else
				{
					this.layoutGroup.padding.left = 48;
					if (journalLineData.entry.Tradable)
					{
						JournalSultanStatueLine.sb.Append("{{G|$}} ");
					}
					else
					{
						JournalSultanStatueLine.sb.Append("{{K|$}} ");
					}
					JournalSultanStatueLine.sb.Append(journalLineData.entry.GetDisplayText());
				}
				this.text.SetText(JournalSultanStatueLine.sb.ToString());
			}
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x000E62BD File Offset: 0x000E44BD
		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x000E62C0 File Offset: 0x000E44C0
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

		// Token: 0x06002B3B RID: 11067 RVA: 0x000E63E4 File Offset: 0x000E45E4
		public void OnDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x000E63E6 File Offset: 0x000E45E6
		public void OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x000E63E8 File Offset: 0x000E45E8
		public void OnPointerClick(PointerEventData eventData)
		{
			this.context.IsActive(true);
		}

		// Token: 0x04001755 RID: 5973
		public Image fadedImage;

		// Token: 0x04001756 RID: 5974
		public Image fullImage;

		// Token: 0x04001757 RID: 5975
		public Image highlightedBorder;

		// Token: 0x04001758 RID: 5976
		public Image fadedBase;

		// Token: 0x04001759 RID: 5977
		public Image fullBase;

		// Token: 0x0400175A RID: 5978
		public LayoutGroup layoutGroup;

		// Token: 0x0400175B RID: 5979
		private JournalSultanStatueLine.Context context = new JournalSultanStatueLine.Context();

		// Token: 0x0400175C RID: 5980
		public UITextSkin text;

		// Token: 0x0400175D RID: 5981
		public StringBuilder SB = new StringBuilder();

		// Token: 0x0400175E RID: 5982
		public static List<MenuOption> categoryExpandOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Expand"
			}
		};

		// Token: 0x0400175F RID: 5983
		public static List<MenuOption> categoryCollapseOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Collapse"
			}
		};

		// Token: 0x04001760 RID: 5984
		public Image background;

		// Token: 0x04001761 RID: 5985
		public bool wasSelected;

		// Token: 0x04001762 RID: 5986
		public bool wasHighlighted;

		// Token: 0x04001763 RID: 5987
		public bool isHighlighted;

		// Token: 0x04001764 RID: 5988
		public bool hasBase;

		// Token: 0x04001765 RID: 5989
		private static StringBuilder sb = new StringBuilder();

		// Token: 0x04001766 RID: 5990
		private List<AbilityManagerSpacer> spacers = new List<AbilityManagerSpacer>();

		// Token: 0x020018AC RID: 6316
		public class Context : NavigationContext
		{
			// Token: 0x04006335 RID: 25397
			public JournalLineData data;
		}
	}
}
