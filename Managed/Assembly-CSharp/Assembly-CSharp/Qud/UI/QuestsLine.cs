using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x02000472 RID: 1138
	public class QuestsLine : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06002B59 RID: 11097 RVA: 0x000E6C50 File Offset: 0x000E4E50
		public bool selected
		{
			get
			{
				QuestsLine.Context context = this.context;
				return context != null && context.IsActive(true);
			}
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x000E6C64 File Offset: 0x000E4E64
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x000E6C6C File Offset: 0x000E4E6C
		public void SetupContexts(ScrollChildContext scrollContext)
		{
			scrollContext.proxyTo = this.context;
			QuestsLine.Context context = this.context;
			if (context.axisHandlers == null)
			{
				context.axisHandlers = new Dictionary<InputAxisTypes, Action> { 
				{
					InputAxisTypes.NavigationXAxis,
					new Action(this.XAxis)
				} };
			}
			context = this.context;
			if (context.commandHandlers == null)
			{
				context.commandHandlers = new Dictionary<string, Action>();
			}
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x000E6CCC File Offset: 0x000E4ECC
		public void LateUpdate()
		{
			bool selected = this.selected;
			bool? flag = this.wasSelected;
			if (!((selected == flag.GetValueOrDefault()) & (flag != null)))
			{
				this.background.color = (this.selected ? ConsoleLib.Console.ColorUtility.FromWebColor("1A3843") : new Color(0f, 0f, 0f, 0f));
				this.titleText.color = (this.selected ? ConsoleLib.Console.ColorUtility.FromWebColor("cfc041") : ConsoleLib.Console.ColorUtility.FromWebColor("829EA8"));
			}
			this.wasSelected = new bool?(this.selected);
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x000E6D6C File Offset: 0x000E4F6C
		public void XAxis()
		{
			XRL.UI.Framework.Event currentEvent = NavigationController.currentEvent;
			int? num = currentEvent.axisValue;
			int num2 = 0;
			if ((num.GetValueOrDefault() < num2) & (num != null))
			{
				QuestsLineData data = this.context.data;
				bool flag;
				if (data == null)
				{
					flag = null != null;
				}
				else
				{
					Quest quest = data.quest;
					flag = ((quest != null) ? quest.ID : null) != null;
				}
				if (flag)
				{
					QuestsStatusScreen questsStatusScreen = SingletonWindowBase<QuestsStatusScreen>.instance;
					if (questsStatusScreen.CollapsedEntries == null)
					{
						questsStatusScreen.CollapsedEntries = new HashSet<string>();
					}
					HashSet<string> collapsedEntries = SingletonWindowBase<QuestsStatusScreen>.instance.CollapsedEntries;
					QuestsLineData data2 = this.context.data;
					string text;
					if (data2 == null)
					{
						text = null;
					}
					else
					{
						Quest quest2 = data2.quest;
						text = ((quest2 != null) ? quest2.ID : null);
					}
					collapsedEntries.Add(text);
					SingletonWindowBase<QuestsStatusScreen>.instance.UpdateViewFromData();
					return;
				}
			}
			else
			{
				num = currentEvent.axisValue;
				num2 = 0;
				if ((num.GetValueOrDefault() > num2) & (num != null))
				{
					QuestsLineData data3 = this.context.data;
					bool flag2;
					if (data3 == null)
					{
						flag2 = null != null;
					}
					else
					{
						Quest quest3 = data3.quest;
						flag2 = ((quest3 != null) ? quest3.ID : null) != null;
					}
					if (flag2)
					{
						QuestsStatusScreen questsStatusScreen = SingletonWindowBase<QuestsStatusScreen>.instance;
						if (questsStatusScreen.CollapsedEntries == null)
						{
							questsStatusScreen.CollapsedEntries = new HashSet<string>();
						}
						HashSet<string> collapsedEntries2 = SingletonWindowBase<QuestsStatusScreen>.instance.CollapsedEntries;
						QuestsLineData data4 = this.context.data;
						string text2;
						if (data4 == null)
						{
							text2 = null;
						}
						else
						{
							Quest quest4 = data4.quest;
							text2 = ((quest4 != null) ? quest4.ID : null);
						}
						collapsedEntries2.Remove(text2);
						SingletonWindowBase<QuestsStatusScreen>.instance.UpdateViewFromData();
					}
				}
			}
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x000E6EB4 File Offset: 0x000E50B4
		public void setData(FrameworkDataElement data)
		{
			QuestsLineData questsLineData = data as QuestsLineData;
			if (questsLineData != null)
			{
				if (questsLineData.quest == null)
				{
					this.giverLabel.SetActive(false);
					this.titleText.SetText("You have no active quests.");
					this.giverText.SetText("");
					this.bodyText.SetText("");
					return;
				}
				this.giverLabel.SetActive(true);
				this.context.data = questsLineData;
				questsLineData.line = this;
				int num = 70;
				if (Media.sizeClass <= Media.SizeClass.Small)
				{
					num = 45;
				}
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in QuestLog.GetLinesForQuest(questsLineData.quest, false, true, num))
				{
					stringBuilder.AppendLine(text);
				}
				global::UnityEngine.GameObject[] array = this.detailLayouts;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(questsLineData.expanded);
				}
				this.titleText.SetText((questsLineData.expanded ? "[-]" : "[+]") + " " + ConsoleLib.Console.ColorUtility.StripFormatting(questsLineData.quest.DisplayName));
				this.giverText.SetText((questsLineData.quest.QuestGiverName ?? "<unknown>") + " / " + (questsLineData.quest.QuestGiverLocationName ?? "<unknown>"));
				this.bodyText.SetText(stringBuilder.ToString());
			}
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x000E7050 File Offset: 0x000E5250
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

		// Token: 0x06002B60 RID: 11104 RVA: 0x000E7174 File Offset: 0x000E5374
		public void OnPointerClick(PointerEventData eventData)
		{
			this.context.IsActive(true);
		}

		// Token: 0x0400177E RID: 6014
		private QuestsLine.Context context = new QuestsLine.Context();

		// Token: 0x0400177F RID: 6015
		public TextMeshProUGUI titleTextText;

		// Token: 0x04001780 RID: 6016
		public UITextSkin titleText;

		// Token: 0x04001781 RID: 6017
		public UITextSkin bodyText;

		// Token: 0x04001782 RID: 6018
		public UITextSkin giverText;

		// Token: 0x04001783 RID: 6019
		public StringBuilder SB = new StringBuilder();

		// Token: 0x04001784 RID: 6020
		public static List<MenuOption> categoryExpandOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Expand"
			}
		};

		// Token: 0x04001785 RID: 6021
		public static List<MenuOption> categoryCollapseOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Collapse"
			}
		};

		// Token: 0x04001786 RID: 6022
		public global::UnityEngine.GameObject giverLabel;

		// Token: 0x04001787 RID: 6023
		public Image background;

		// Token: 0x04001788 RID: 6024
		public bool? wasSelected;

		// Token: 0x04001789 RID: 6025
		public global::UnityEngine.GameObject[] detailLayouts;

		// Token: 0x0400178A RID: 6026
		private List<AbilityManagerSpacer> spacers = new List<AbilityManagerSpacer>();

		// Token: 0x020018B0 RID: 6320
		public class Context : NavigationContext
		{
			// Token: 0x04006340 RID: 25408
			public QuestsLineData data;
		}
	}
}
