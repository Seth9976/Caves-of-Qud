using System;
using System.Collections.Generic;
using System.Linq;
using HistoryKit;
using XRL.World.Conversations;

namespace XRL.World.Parts
{
	// Token: 0x02000DD7 RID: 3543
	[Serializable]
	public class DynamicQuestSignpostConversation : IPart
	{
		// Token: 0x0600902B RID: 36907 RVA: 0x00340028 File Offset: 0x0033E228
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600902C RID: 36908 RVA: 0x0034002B File Offset: 0x0033E22B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeConversationEvent.ID;
		}

		// Token: 0x0600902D RID: 36909 RVA: 0x00340044 File Offset: 0x0033E244
		public override bool HandleEvent(BeforeConversationEvent E)
		{
			if (this.ParentObject.GetCurrentCell() == null)
			{
				return false;
			}
			List<ValueTuple<GameObject, Quest>> list = new List<ValueTuple<GameObject, Quest>>();
			foreach (GameObject gameObject in this.ParentObject.GetCurrentCell().ParentZone.GetObjects((GameObject o) => o.HasProperty("GivesDynamicQuest")))
			{
				string stringProperty = gameObject.GetStringProperty("GivesDynamicQuest", null);
				if (!The.Game.HasQuest(stringProperty))
				{
					Quest quest;
					if (The.Game.Quests.TryGetValue(stringProperty, out quest))
					{
						list.Add(new ValueTuple<GameObject, Quest>(gameObject, quest));
					}
					else
					{
						list.Add(new ValueTuple<GameObject, Quest>(gameObject, null));
					}
				}
			}
			if (list.Count <= 0 || list.Any<ValueTuple<GameObject, Quest>>((ValueTuple<GameObject, Quest> t) => t.Item1 == this.ParentObject))
			{
				return base.HandleEvent(E);
			}
			foreach (Node node in E.Conversation.Starts)
			{
				if (!node.Elements.Any<IConversationElement>(delegate(IConversationElement n)
				{
					Choice choice = n as Choice;
					return choice != null && choice.Target == "*DynamicQuestSignpostConversationIntro";
				}))
				{
					node.AddChoice(null, HistoricStringExpander.ExpandString("<spice.quests.intro.!random>", null), "*DynamicQuestSignpostConversationIntro");
				}
			}
			string text = "";
			string text2 = null;
			bool flag = false;
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				GameObject item = list[i].Item1;
				string text3 = this.ParentObject.DescribeDirectionToward(item, true, false);
				if (i > 0)
				{
					if (i == list.Count - 1)
					{
						if (flag)
						{
							text += ", or ";
						}
						else
						{
							text += " or ";
						}
					}
					else
					{
						text += ", ";
					}
				}
				text = text + "{{Y|" + item.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, true, false, false, true, false, false, null, false, false, false, false, null, false, null, false, true) + "}}";
				if (!string.IsNullOrEmpty(text3))
				{
					text = text + ", " + ((text3 == text2) ? "also " : "") + text3;
					flag = true;
					text2 = text3;
				}
				i++;
			}
			Node node2 = E.Conversation.AddNode("*DynamicQuestSignpostConversationIntro", HistoricStringExpander.ExpandString("<spice.instancesOf.speakTo.!random.capitalize> ", null) + text + ".");
			node2.AddChoice(null, "Thank you. I have more to ask.", "Start");
			node2.AddChoice(null, "Live and drink.", "End");
			return base.HandleEvent(E);
		}
	}
}
