using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World;
using XRL.World.Conversations;
using XRL.World.Parts;

namespace Qud.API
{
	// Token: 0x02000405 RID: 1029
	public static class ConversationsAPI
	{
		// Token: 0x0600268D RID: 9869 RVA: 0x000C6220 File Offset: 0x000C4420
		public static ConversationXMLBlueprint AddConversation(GameObject Object, string Filter = null, string FilterExtras = null, string Append = null, bool ClearLost = false, bool ClearOriginal = true)
		{
			ConversationScript conversationScript = Object.GetPart<ConversationScript>();
			if (((conversationScript != null) ? conversationScript.Blueprint : null) != null && !ClearOriginal)
			{
				return conversationScript.Blueprint;
			}
			Object.RemovePart<ConversationScript>();
			conversationScript = Object.AddPart<ConversationScript>(new ConversationScript(), true, false);
			conversationScript.ClearLost = ClearLost;
			conversationScript.Filter = Filter;
			conversationScript.FilterExtras = FilterExtras;
			conversationScript.Append = Append;
			conversationScript.Blueprint = new ConversationXMLBlueprint
			{
				ID = "CustomConversation::" + Object.ID,
				Name = "Conversation"
			};
			if (Object != null)
			{
				Object.SetStringProperty("SuppressPowerSwitchTwiddle", "1", false);
			}
			return conversationScript.Blueprint;
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x000C62C8 File Offset: 0x000C44C8
		public static void AddDynamicShim(ConversationXMLBlueprint Conversation)
		{
			ConversationXMLBlueprint conversationXMLBlueprint = Conversation.Blueprints["BaseDynamicShim"];
			ConversationXMLBlueprint child = conversationXMLBlueprint.GetChild("DynamicStart");
			if (!child.Children.IsNullOrEmpty<ConversationXMLBlueprint>() && !Conversation.Children.IsNullOrEmpty<ConversationXMLBlueprint>())
			{
				foreach (ConversationXMLBlueprint conversationXMLBlueprint2 in Conversation.Children)
				{
					if (!(conversationXMLBlueprint2.Name != child.Name))
					{
						ConversationXMLBlueprint conversationXMLBlueprint3 = conversationXMLBlueprint2;
						if (conversationXMLBlueprint3.Children == null)
						{
							conversationXMLBlueprint3.Children = new List<ConversationXMLBlueprint>();
						}
						foreach (ConversationXMLBlueprint conversationXMLBlueprint4 in child.Children)
						{
							conversationXMLBlueprint2.Children.Add(conversationXMLBlueprint4);
						}
					}
				}
			}
			ConversationXMLBlueprint child2 = conversationXMLBlueprint.GetChild("DynamicNode");
			if (!child2.Children.IsNullOrEmpty<ConversationXMLBlueprint>() && !Conversation.Children.IsNullOrEmpty<ConversationXMLBlueprint>())
			{
				foreach (ConversationXMLBlueprint conversationXMLBlueprint5 in Conversation.Children)
				{
					if (!(conversationXMLBlueprint5.Name != child2.Name))
					{
						ConversationXMLBlueprint conversationXMLBlueprint3 = conversationXMLBlueprint5;
						if (conversationXMLBlueprint3.Children == null)
						{
							conversationXMLBlueprint3.Children = new List<ConversationXMLBlueprint>();
						}
						foreach (ConversationXMLBlueprint conversationXMLBlueprint6 in child2.Children)
						{
							conversationXMLBlueprint5.Children.Add(conversationXMLBlueprint6);
						}
					}
				}
			}
			ConversationXMLBlueprint conversationXMLBlueprint7 = Conversation.Blueprints["BaseConversation"];
			if (Conversation.Children == null)
			{
				Conversation.Children = new List<ConversationXMLBlueprint>();
			}
			Conversation.Children.AddRange(conversationXMLBlueprint7.Children);
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x000C64E4 File Offset: 0x000C46E4
		public static void RemoveDynamicShim(ConversationXMLBlueprint Conversation)
		{
			ConversationXMLBlueprint conversationXMLBlueprint = Conversation.Blueprints["BaseDynamicShim"];
			ConversationXMLBlueprint child = conversationXMLBlueprint.GetChild("DynamicStart");
			if (!child.Children.IsNullOrEmpty<ConversationXMLBlueprint>() && !Conversation.Children.IsNullOrEmpty<ConversationXMLBlueprint>())
			{
				foreach (ConversationXMLBlueprint conversationXMLBlueprint2 in Conversation.Children)
				{
					if (!(conversationXMLBlueprint2.Name != child.Name) && !conversationXMLBlueprint2.Children.IsNullOrEmpty<ConversationXMLBlueprint>())
					{
						foreach (ConversationXMLBlueprint conversationXMLBlueprint3 in child.Children)
						{
							conversationXMLBlueprint2.Children.Remove(conversationXMLBlueprint3);
						}
					}
				}
			}
			ConversationXMLBlueprint child2 = conversationXMLBlueprint.GetChild("DynamicNode");
			if (!child2.Children.IsNullOrEmpty<ConversationXMLBlueprint>() && !Conversation.Children.IsNullOrEmpty<ConversationXMLBlueprint>())
			{
				foreach (ConversationXMLBlueprint conversationXMLBlueprint4 in Conversation.Children)
				{
					if (!(conversationXMLBlueprint4.Name != child2.Name) && !conversationXMLBlueprint4.Children.IsNullOrEmpty<ConversationXMLBlueprint>())
					{
						foreach (ConversationXMLBlueprint conversationXMLBlueprint5 in child2.Children)
						{
							conversationXMLBlueprint4.Children.Remove(conversationXMLBlueprint5);
						}
					}
				}
			}
			ConversationXMLBlueprint inherit = Conversation.Blueprints["BaseConversation"];
			if (Conversation.Children == null)
			{
				Conversation.Children = new List<ConversationXMLBlueprint>();
			}
			Conversation.Children.RemoveAll((ConversationXMLBlueprint x) => inherit.Children.Contains(x));
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x000C6700 File Offset: 0x000C4900
		public static ConversationXMLBlueprint AddText(ConversationXMLBlueprint Blueprint, string ID = null, string Text = null)
		{
			ConversationXMLBlueprint conversationXMLBlueprint = new ConversationXMLBlueprint
			{
				ID = (ID ?? "Text"),
				Name = "Text",
				Text = Text
			};
			Blueprint.AddChild(conversationXMLBlueprint);
			return conversationXMLBlueprint;
		}

		// Token: 0x06002691 RID: 9873 RVA: 0x000C6740 File Offset: 0x000C4940
		public static ConversationXMLBlueprint AddElement(ConversationXMLBlueprint Blueprint, string Name, string ID = null, string Text = null)
		{
			ConversationXMLBlueprint conversationXMLBlueprint = new ConversationXMLBlueprint
			{
				ID = (ID ?? Name),
				Name = Name
			};
			if (!Text.IsNullOrEmpty())
			{
				ConversationsAPI.AddText(conversationXMLBlueprint, null, Text);
			}
			Blueprint.AddChild(conversationXMLBlueprint);
			return conversationXMLBlueprint;
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x000C677F File Offset: 0x000C497F
		public static ConversationXMLBlueprint AddNode(ConversationXMLBlueprint Blueprint, string ID = null, string Text = null)
		{
			return ConversationsAPI.AddElement(Blueprint, "Node", ID, Text);
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x000C678E File Offset: 0x000C498E
		public static ConversationXMLBlueprint AddStart(ConversationXMLBlueprint Blueprint, string Text = null)
		{
			return ConversationsAPI.AddElement(Blueprint, "Start", null, Text);
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x000C67A0 File Offset: 0x000C49A0
		public static ConversationXMLBlueprint AddChoice(ConversationXMLBlueprint Blueprint, string ID = null, string Target = null, string Text = null)
		{
			ConversationXMLBlueprint conversationXMLBlueprint = ConversationsAPI.AddElement(Blueprint, "Choice", ID, Text);
			if (!Target.IsNullOrEmpty())
			{
				conversationXMLBlueprint["Target"] = Target;
			}
			return conversationXMLBlueprint;
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x000C67D0 File Offset: 0x000C49D0
		public static ConversationXMLBlueprint AddChoice(ConversationXMLBlueprint Blueprint, string ID = null, ConversationXMLBlueprint Target = null, string Text = null)
		{
			return ConversationsAPI.AddChoice(Blueprint, ID, Target.CardinalID, Text);
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x000C67E0 File Offset: 0x000C49E0
		public static ConversationXMLBlueprint DistributeChoice(ConversationXMLBlueprint Blueprint, string ParentName, ConversationXMLBlueprint Choice)
		{
			if (Blueprint.Children.IsNullOrEmpty<ConversationXMLBlueprint>())
			{
				return null;
			}
			foreach (ConversationXMLBlueprint conversationXMLBlueprint in Blueprint.Children)
			{
				if (conversationXMLBlueprint.Name == ParentName)
				{
					conversationXMLBlueprint.AddChild(Choice);
				}
			}
			return Choice;
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x000C6854 File Offset: 0x000C4A54
		public static ConversationXMLBlueprint DistributeChoice(ConversationXMLBlueprint Blueprint, string ParentName, string ID = null, string Target = null, string Text = null)
		{
			if (Blueprint.Children.IsNullOrEmpty<ConversationXMLBlueprint>())
			{
				return null;
			}
			ConversationXMLBlueprint conversationXMLBlueprint = null;
			foreach (ConversationXMLBlueprint conversationXMLBlueprint2 in Blueprint.Children)
			{
				if (conversationXMLBlueprint2.Name == ParentName)
				{
					if (conversationXMLBlueprint == null)
					{
						conversationXMLBlueprint = ConversationsAPI.AddChoice(conversationXMLBlueprint2, ID, Target, Text);
					}
					else
					{
						conversationXMLBlueprint2.AddChild(conversationXMLBlueprint);
					}
				}
			}
			return conversationXMLBlueprint;
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x000C68DC File Offset: 0x000C4ADC
		public static ConversationXMLBlueprint DistributeChoice(ConversationXMLBlueprint Blueprint, string ParentName, string ID = null, ConversationXMLBlueprint Target = null, string Text = null)
		{
			return ConversationsAPI.DistributeChoice(Blueprint, ParentName, ID, (Target != null) ? Target.CardinalID : null, Text);
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x000C68F4 File Offset: 0x000C4AF4
		public static ConversationXMLBlueprint AddPart(ConversationXMLBlueprint Blueprint, string Name)
		{
			ConversationXMLBlueprint conversationXMLBlueprint = ConversationsAPI.AddElement(Blueprint, "Part", null, null);
			conversationXMLBlueprint["Name"] = Name;
			return conversationXMLBlueprint;
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x000C690F File Offset: 0x000C4B0F
		public static ConversationXMLBlueprint GetConversation(GameObject Object)
		{
			ConversationScript part = Object.GetPart<ConversationScript>();
			if (part == null)
			{
				return null;
			}
			return part.Blueprint;
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x000C6924 File Offset: 0x000C4B24
		public static ConversationXMLBlueprint addSimpleRootInformationOption(GameObject go, string optionText, string choiceText)
		{
			ConversationXMLBlueprint conversation = ConversationsAPI.GetConversation(go);
			ConversationXMLBlueprint conversationXMLBlueprint = ConversationsAPI.AddNode(conversation, null, choiceText);
			ConversationsAPI.AddChoice(conversation.GetChild("Start"), null, conversationXMLBlueprint, optionText);
			ConversationsAPI.AddChoice(conversationXMLBlueprint, null, "Start", "I have more to ask.");
			return conversation;
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x000C6968 File Offset: 0x000C4B68
		public static GameObject chooseOneItem(List<GameObject> objects, string title, bool allowEscape)
		{
			List<GameObject> list = new List<GameObject>();
			List<string> list2 = new List<string>();
			List<char> list3 = new List<char>();
			char c = 'a';
			foreach (GameObject gameObject in objects)
			{
				list.Add(gameObject);
				list2.Add(gameObject.DisplayName);
				list3.Add(c);
				c += '\u0001';
			}
			int num = Popup.PickOption("Choose a reward", null, "", "Sounds/UI/ui_notification", list2.ToArray(), list3.ToArray(), null, null, null, null, null, 1, 60, 0, -1, allowEscape, false, false, true, false, null, null);
			if (num < 0)
			{
				return null;
			}
			return list[num];
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x000C6A2C File Offset: 0x000C4C2C
		public static ConversationXMLBlueprint addSimpleConversationToObject(GameObject Object, string Text, string Goodbye, string Filter = null, string FilterExtras = null, string Append = null, bool ClearLost = false, bool ClearOriginal = true)
		{
			ConversationXMLBlueprint conversationXMLBlueprint = ConversationsAPI.AddConversation(Object, Filter, FilterExtras, Append, ClearLost, ClearOriginal);
			ConversationsAPI.AddChoice(ConversationsAPI.AddStart(conversationXMLBlueprint, Text), null, "End", Goodbye);
			return conversationXMLBlueprint;
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x000C6A51 File Offset: 0x000C4C51
		public static ConversationXMLBlueprint appendSimpleConversationToObject(GameObject Object, string Text, string Goodbye, string Filter = null, string FilterExtras = null, string Append = null, bool ClearLost = false)
		{
			return ConversationsAPI.addSimpleConversationToObject(Object, Text, Goodbye, Filter, FilterExtras, Append, ClearLost, false);
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x000C6A64 File Offset: 0x000C4C64
		public static ConversationXMLBlueprint addSimpleConversationToObject(GameObject Object, string Text, string Goodbye, string Question, string Answer, string Filter = null, string FilterExtras = null, string Append = null, bool ClearLost = false, bool ClearOriginal = true)
		{
			ConversationXMLBlueprint conversationXMLBlueprint = ConversationsAPI.addSimpleConversationToObject(Object, Text, Goodbye, Filter, FilterExtras, Append, ClearLost, ClearOriginal);
			ConversationXMLBlueprint conversationXMLBlueprint2 = ConversationsAPI.AddNode(conversationXMLBlueprint, null, Answer);
			ConversationsAPI.AddChoice(conversationXMLBlueprint2, null, "End", Goodbye);
			ConversationsAPI.AddChoice(conversationXMLBlueprint.GetChild("Start"), null, conversationXMLBlueprint2, Question);
			return conversationXMLBlueprint;
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x000C6AB0 File Offset: 0x000C4CB0
		public static ConversationXMLBlueprint appendSimpleConversationToObject(GameObject Object, string Text, string Goodbye, string Question, string Answer, string Filter = null, string FilterExtras = null, string Append = null, bool ClearLost = false)
		{
			return ConversationsAPI.addSimpleConversationToObject(Object, Text, Goodbye, Question, Answer, Filter, FilterExtras, Append, ClearLost, false);
		}
	}
}
