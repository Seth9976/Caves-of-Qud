using System;
using System.Collections.Generic;
using System.Text;
using Qud.API;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x0200046B RID: 1131
	public class JournalLine : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
	{
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06002B00 RID: 11008 RVA: 0x000E4226 File Offset: 0x000E2426
		public bool selected
		{
			get
			{
				JournalLine.Context context = this.context;
				return context != null && context.IsActive(true);
			}
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x000E423A File Offset: 0x000E243A
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x000E4244 File Offset: 0x000E2444
		public void XAxis()
		{
			XRL.UI.Framework.Event currentEvent = NavigationController.currentEvent;
			if (this.context.data.category)
			{
				Dictionary<string, bool> categoryCollapsed = this.context.data.screen.categoryCollapsed;
				string categoryName = this.context.data.categoryName;
				int? num = currentEvent.axisValue;
				int num2 = 0;
				categoryCollapsed[categoryName] = !((num.GetValueOrDefault() > num2) & (num != null));
				this.context.data.screen.UpdateViewFromData();
				return;
			}
			if (this.context.data.categoryName != null)
			{
				int? num = currentEvent.axisValue;
				int num2 = 0;
				if ((num.GetValueOrDefault() < num2) & (num != null))
				{
					this.context.data.screen.controller.scrollContext.SelectIndex(this.context.data.screen.controller.scrollContext.selectedPosition - this.context.data.categoryOffset);
					this.context.data.screen.categoryCollapsed[this.context.data.categoryName] = true;
					this.context.data.screen.UpdateViewFromData();
				}
			}
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000E4390 File Offset: 0x000E2590
		public void SetupContexts(ScrollChildContext scrollContext)
		{
			scrollContext.proxyTo = this.context;
			JournalLine.Context context = this.context;
			if (context.axisHandlers == null)
			{
				context.axisHandlers = new Dictionary<InputAxisTypes, Action>();
			}
			this.context.axisHandlers[InputAxisTypes.NavigationXAxis] = new Action(this.XAxis);
			context = this.context;
			if (context.commandHandlers == null)
			{
				context.commandHandlers = new Dictionary<string, Action> { 
				{
					"CmdDelete",
					new Action(this.HandleDelete)
				} };
			}
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x000E4411 File Offset: 0x000E2611
		public void HandleDelete()
		{
			this.screen.HandleDelete(this.context.data);
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000E4429 File Offset: 0x000E2629
		public void LateUpdate()
		{
			this.wasSelected = this.selected;
			this.wasHighlighted = this.isHighlighted;
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x000E4444 File Offset: 0x000E2644
		public void setData(FrameworkDataElement data)
		{
			JournalLineData journalLineData = data as JournalLineData;
			if (journalLineData != null)
			{
				this.imageContainer.SetActive(journalLineData.renderable != null);
				if (journalLineData.renderable != null)
				{
					this.image.FromRenderable(journalLineData.renderable, true);
				}
				this.screen = journalLineData.screen;
				this.context.data = journalLineData;
				JournalLine.sb.Clear();
				if (journalLineData.category)
				{
					this.layoutGroup.padding.left = 16;
					if (journalLineData.categoryName != JournalStatusScreen.NO_ENTRIES_TEXT)
					{
						if (journalLineData.categoryExpanded)
						{
							JournalLine.sb.Append("[-] ");
						}
						else
						{
							JournalLine.sb.Append("[+] ");
						}
					}
					JournalLine.sb.Append(journalLineData.categoryName);
					this.headerContainer.SetActive(true);
					this.headerText.SetText(JournalLine.sb.ToString());
					this.text.SetText("");
					return;
				}
				JournalRecipeNote journalRecipeNote = journalLineData.entry as JournalRecipeNote;
				if (journalRecipeNote != null && journalRecipeNote.Recipe != null)
				{
					this.layoutGroup.padding.left = 16;
					this.headerContainer.SetActive(true);
					this.headerText.SetText(journalRecipeNote.Recipe.GetDisplayName());
					JournalLine.sb.Append("{{K|Ingredients:}} ");
					JournalLine.sb.AppendLine(journalRecipeNote.Recipe.GetIngredients());
					string[] array = journalRecipeNote.Recipe.GetDescription().Split("\n", StringSplitOptions.None);
					for (int i = 0; i < array.Length; i++)
					{
						if (i != 0)
						{
							JournalLine.sb.Append("\n");
						}
						if (i == 0)
						{
							JournalLine.sb.Append("{{K|Effects:}}     {{K|/}} {{y|");
							JournalLine.sb.Append(array[i]);
							JournalLine.sb.Append("}}");
						}
						else
						{
							JournalLine.sb.Append("             {{K|/}} {{y|");
							JournalLine.sb.Append(array[i]);
							JournalLine.sb.Append("}}");
						}
					}
					JournalLine.sb.Append(" \n \n");
					this.text.SetText(JournalLine.sb.ToString());
					return;
				}
				this.headerContainer.SetActive(false);
				this.layoutGroup.padding.left = 48;
				JournalMapNote journalMapNote = journalLineData.entry as JournalMapNote;
				if (journalMapNote != null)
				{
					JournalLine.sb.Append(journalMapNote.Tracked ? "[X] " : "[ ] ");
				}
				if (journalLineData.entry != null)
				{
					if (journalLineData.entry.Tradable)
					{
						JournalLine.sb.Append("{{G|$}} ");
					}
					else
					{
						JournalLine.sb.Append("{{K|$}} ");
					}
				}
				IBaseJournalEntry entry = journalLineData.entry;
				bool flag = entry != null && entry.Has("sultanTombPropaganda");
				if (flag)
				{
					JournalLine.sb.Append("{{w|[tomb engraving] ");
				}
				StringBuilder stringBuilder = JournalLine.sb;
				IBaseJournalEntry entry2 = journalLineData.entry;
				stringBuilder.Append(((entry2 != null) ? entry2.GetDisplayText() : null) ?? "");
				if (flag)
				{
					JournalLine.sb.Append("}}");
				}
				if (Media.sizeClass > Media.SizeClass.Small || journalLineData.screen.CurrentCategory == 2)
				{
					this.text.SetText(JournalLine.sb.ToString());
					return;
				}
				this.text.SetText(StringFormat.ClipText(JournalLine.sb.ToString(), 45, false, true, false));
			}
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x000E47BA File Offset: 0x000E29BA
		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000E47BC File Offset: 0x000E29BC
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

		// Token: 0x06002B09 RID: 11017 RVA: 0x000E48E0 File Offset: 0x000E2AE0
		public void OnDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x000E48E2 File Offset: 0x000E2AE2
		public void OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x000E48E4 File Offset: 0x000E2AE4
		public void OnPointerClick(PointerEventData eventData)
		{
			this.context.IsActive(true);
		}

		// Token: 0x04001717 RID: 5911
		public LayoutGroup layoutGroup;

		// Token: 0x04001718 RID: 5912
		private JournalLine.Context context = new JournalLine.Context();

		// Token: 0x04001719 RID: 5913
		public UITextSkin text;

		// Token: 0x0400171A RID: 5914
		public GameObject imageContainer;

		// Token: 0x0400171B RID: 5915
		public UIThreeColorProperties image;

		// Token: 0x0400171C RID: 5916
		public GameObject headerContainer;

		// Token: 0x0400171D RID: 5917
		public UITextSkin headerText;

		// Token: 0x0400171E RID: 5918
		public GameObject textContainer;

		// Token: 0x0400171F RID: 5919
		public JournalStatusScreen screen;

		// Token: 0x04001720 RID: 5920
		public Image background;

		// Token: 0x04001721 RID: 5921
		public bool wasSelected;

		// Token: 0x04001722 RID: 5922
		public bool wasHighlighted;

		// Token: 0x04001723 RID: 5923
		public bool isHighlighted;

		// Token: 0x04001724 RID: 5924
		private static StringBuilder sb = new StringBuilder();

		// Token: 0x04001725 RID: 5925
		private List<AbilityManagerSpacer> spacers = new List<AbilityManagerSpacer>();

		// Token: 0x020018A4 RID: 6308
		public class Context : NavigationContext
		{
			// Token: 0x04006315 RID: 25365
			public JournalLineData data;
		}
	}
}
