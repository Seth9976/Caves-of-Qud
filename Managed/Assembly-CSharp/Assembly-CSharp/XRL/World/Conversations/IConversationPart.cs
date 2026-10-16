using System;
using System.Collections.Generic;
using System.Reflection;
using XRL.UI;

namespace XRL.World.Conversations
{
	// Token: 0x02000BC7 RID: 3015
	public abstract class IConversationPart : IComparable<IConversationPart>, IDisposable
	{
		// Token: 0x06007913 RID: 30995 RVA: 0x002DBD8A File Offset: 0x002D9F8A
		public virtual bool HandleEvent(ColorTextEvent E)
		{
			return true;
		}

		// Token: 0x06007914 RID: 30996 RVA: 0x002DBD8D File Offset: 0x002D9F8D
		public virtual bool HandleEvent(DisplayTextEvent E)
		{
			return true;
		}

		// Token: 0x06007915 RID: 30997 RVA: 0x002DBD90 File Offset: 0x002D9F90
		public virtual bool HandleEvent(EnterElementEvent E)
		{
			return true;
		}

		// Token: 0x06007916 RID: 30998 RVA: 0x002DBD93 File Offset: 0x002D9F93
		public virtual bool HandleEvent(EnteredElementEvent E)
		{
			return true;
		}

		// Token: 0x06007917 RID: 30999 RVA: 0x002DBD96 File Offset: 0x002D9F96
		public virtual bool HandleEvent(GetChoiceTagEvent E)
		{
			return true;
		}

		// Token: 0x06007918 RID: 31000 RVA: 0x002DBD99 File Offset: 0x002D9F99
		public virtual bool HandleEvent(GetTargetElementEvent E)
		{
			return true;
		}

		// Token: 0x06007919 RID: 31001 RVA: 0x002DBD9C File Offset: 0x002D9F9C
		public virtual bool HandleEvent(GetTextElementEvent E)
		{
			return true;
		}

		// Token: 0x0600791A RID: 31002 RVA: 0x002DBD9F File Offset: 0x002D9F9F
		public virtual bool HandleEvent(HideElementEvent E)
		{
			return true;
		}

		// Token: 0x0600791B RID: 31003 RVA: 0x002DBDA2 File Offset: 0x002D9FA2
		public virtual bool HandleEvent(IsElementVisibleEvent E)
		{
			return true;
		}

		// Token: 0x0600791C RID: 31004 RVA: 0x002DBDA5 File Offset: 0x002D9FA5
		public virtual bool HandleEvent(LeaveElementEvent E)
		{
			return true;
		}

		// Token: 0x0600791D RID: 31005 RVA: 0x002DBDA8 File Offset: 0x002D9FA8
		public virtual bool HandleEvent(LeftElementEvent E)
		{
			return true;
		}

		// Token: 0x0600791E RID: 31006 RVA: 0x002DBDAB File Offset: 0x002D9FAB
		public virtual bool HandleEvent(PredicateEvent E)
		{
			return true;
		}

		// Token: 0x0600791F RID: 31007 RVA: 0x002DBDAE File Offset: 0x002D9FAE
		public virtual bool HandleEvent(PrepareTextEvent E)
		{
			return true;
		}

		// Token: 0x06007920 RID: 31008 RVA: 0x002DBDB1 File Offset: 0x002D9FB1
		public virtual bool HandleEvent(PrepareTextLateEvent E)
		{
			return true;
		}

		// Token: 0x06007921 RID: 31009 RVA: 0x002DBDB4 File Offset: 0x002D9FB4
		public virtual bool HandleEvent(RenderNodeEvent E)
		{
			return true;
		}

		// Token: 0x170009A4 RID: 2468
		// (set) Token: 0x06007922 RID: 31010 RVA: 0x002DBDB8 File Offset: 0x002D9FB8
		public string Register
		{
			set
			{
				string text = value.ToLowerInvariant();
				int num;
				if (!(text == "listener"))
				{
					if (!(text == "player"))
					{
						if (!(text == "speaker"))
						{
							if (!(text == "all"))
							{
								num = 0;
							}
							else
							{
								num = 3;
							}
						}
						else
						{
							num = 2;
						}
					}
					else
					{
						num = 1;
					}
				}
				else
				{
					num = 1;
				}
				this.Propagation = num;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06007923 RID: 31011 RVA: 0x002DBE1B File Offset: 0x002DA01B
		protected static string LastChoiceID
		{
			get
			{
				Choice lastChoice = ConversationUI.LastChoice;
				if (lastChoice == null)
				{
					return null;
				}
				return lastChoice.ID;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06007924 RID: 31012 RVA: 0x002DBE2D File Offset: 0x002DA02D
		protected static Choice LastChoice
		{
			get
			{
				return ConversationUI.LastChoice;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06007925 RID: 31013 RVA: 0x002DBE34 File Offset: 0x002DA034
		protected static Dictionary<string, object> State
		{
			get
			{
				return The.Conversation.State;
			}
		}

		/// <summary>Called once when the part is constructed and added to its parent element.</summary>
		// Token: 0x06007926 RID: 31014 RVA: 0x002DBE40 File Offset: 0x002DA040
		public virtual void Initialize()
		{
			if (this.Propagation == -1)
			{
				if (this.ParentElement == null)
				{
					this.Propagation = 0;
					return;
				}
				this.Propagation = this.ParentElement.Propagation;
			}
		}

		/// <summary>Called once before the parent element is first queried for any activity.</summary>
		// Token: 0x06007927 RID: 31015 RVA: 0x002DBE6C File Offset: 0x002DA06C
		public virtual void Awake()
		{
		}

		// Token: 0x06007928 RID: 31016 RVA: 0x002DBE6E File Offset: 0x002DA06E
		public virtual bool PropagateTo(int Propagation)
		{
			return this.Propagation.HasBit(Propagation);
		}

		// Token: 0x06007929 RID: 31017 RVA: 0x002DBE7C File Offset: 0x002DA07C
		public virtual bool WantEvent(int ID, int Propagation)
		{
			return false;
		}

		// Token: 0x0600792A RID: 31018 RVA: 0x002DBE7F File Offset: 0x002DA07F
		public virtual bool HandleEvent(ConversationEvent E)
		{
			return true;
		}

		// Token: 0x0600792B RID: 31019 RVA: 0x002DBE82 File Offset: 0x002DA082
		public virtual void Enter()
		{
		}

		// Token: 0x0600792C RID: 31020 RVA: 0x002DBE84 File Offset: 0x002DA084
		public virtual bool Load(ConversationXMLBlueprint Blueprint)
		{
			if (!Blueprint.Text.IsNullOrEmpty())
			{
				this.LoadText(Blueprint.Text);
			}
			if (!Blueprint.Attributes.IsNullOrEmpty<KeyValuePair<string, string>>())
			{
				this.LoadAttributes(Blueprint.Attributes);
			}
			if (Blueprint.Children == null)
			{
				return true;
			}
			foreach (ConversationXMLBlueprint conversationXMLBlueprint in Blueprint.Children)
			{
				try
				{
					this.LoadChild(conversationXMLBlueprint);
				}
				catch (Exception ex)
				{
					MetricsManager.LogError("Error adding " + conversationXMLBlueprint.Name + " by ID " + conversationXMLBlueprint.ID, ex);
				}
			}
			return true;
		}

		// Token: 0x0600792D RID: 31021 RVA: 0x002DBF48 File Offset: 0x002DA148
		public virtual void LoadText(string Text)
		{
		}

		// Token: 0x0600792E RID: 31022 RVA: 0x002DBF4C File Offset: 0x002DA14C
		public virtual void LoadAttributes(Dictionary<string, string> Attributes)
		{
			Type type = base.GetType();
			foreach (KeyValuePair<string, string> keyValuePair in Attributes)
			{
				try
				{
					FieldInfo field = type.GetField(keyValuePair.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					if (field != null && !field.IsInitOnly)
					{
						field.SetValue(this, Convert.ChangeType(keyValuePair.Value, field.FieldType));
					}
					else
					{
						PropertyInfo property = type.GetProperty(keyValuePair.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
						if (property != null && property.CanWrite)
						{
							property.SetValue(this, Convert.ChangeType(keyValuePair.Value, property.PropertyType));
						}
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogAssemblyError(type.Assembly, string.Format("Error setting field '{0}.{1}' to '{2}': {3}", new object[] { type.FullName, keyValuePair.Key, keyValuePair.Value, ex }));
				}
			}
		}

		// Token: 0x0600792F RID: 31023 RVA: 0x002DC060 File Offset: 0x002DA260
		public virtual bool LoadChild(ConversationXMLBlueprint Blueprint)
		{
			return true;
		}

		// Token: 0x06007930 RID: 31024 RVA: 0x002DC063 File Offset: 0x002DA263
		public virtual bool FireEvent(Event E)
		{
			return true;
		}

		// Token: 0x06007931 RID: 31025 RVA: 0x002DC066 File Offset: 0x002DA266
		public static bool CanResolve(string Name)
		{
			return ModManager.ResolveType(Name, false, false, false) != null;
		}

		// Token: 0x06007932 RID: 31026 RVA: 0x002DC074 File Offset: 0x002DA274
		public static bool TryResolve(string Name, out Type Type)
		{
			if (Name.IndexOf('.') == -1)
			{
				Name = "XRL.World.Conversations.Parts." + Name;
			}
			Type = ModManager.ResolveType(Name, false, false, true);
			return Type != null;
		}

		// Token: 0x06007933 RID: 31027 RVA: 0x002DC0A0 File Offset: 0x002DA2A0
		public static bool TryCreate(string Name, ConversationXMLBlueprint Blueprint, out IConversationPart Part)
		{
			Type type;
			if (!IConversationPart.TryResolve(Name, out type))
			{
				Part = null;
				MetricsManager.LogError("No part of name '" + Name + "' could be resolved.");
				return false;
			}
			try
			{
				Part = Activator.CreateInstance(type) as IConversationPart;
				if (Part == null)
				{
					MetricsManager.LogAssemblyError(type, "Type of name '" + Name + "' is not a valid instance of IConversationPart.");
					return false;
				}
			}
			catch (Exception ex)
			{
				Type type2 = type;
				string text = "Exception when instantiating part '";
				string text2 = "': ";
				Exception ex2 = ex;
				MetricsManager.LogAssemblyError(type2, text + Name + text2 + ((ex2 != null) ? ex2.ToString() : null));
				Part = null;
				return false;
			}
			return Part.Load(Blueprint);
		}

		/// <summary>Retrieve and cast state associated with specified key.</summary>
		// Token: 0x06007934 RID: 31028 RVA: 0x002DC144 File Offset: 0x002DA344
		protected static bool TryGetState<T>(string Key, out T Value)
		{
			object obj;
			if (Key != null && IConversationPart.State.TryGetValue(Key, out obj))
			{
				Value = (T)((object)obj);
				return true;
			}
			Value = default(T);
			return false;
		}

		// Token: 0x06007935 RID: 31029 RVA: 0x002DC179 File Offset: 0x002DA379
		public int CompareTo(IConversationPart other)
		{
			return this.Priority.CompareTo(other.Priority);
		}

		// Token: 0x06007936 RID: 31030 RVA: 0x002DC18C File Offset: 0x002DA38C
		public virtual void Dispose()
		{
		}

		// Token: 0x04003765 RID: 14181
		public const string DEF_NAMESPACE = "XRL.World.Conversations.Parts";

		// Token: 0x04003766 RID: 14182
		public const int PROPAGATE_UNINITIALIZED = -1;

		// Token: 0x04003767 RID: 14183
		public const int PROPAGATE_NONE = 0;

		// Token: 0x04003768 RID: 14184
		public const int PROPAGATE_LISTENER = 1;

		// Token: 0x04003769 RID: 14185
		public const int PROPAGATE_SPEAKER = 2;

		// Token: 0x0400376A RID: 14186
		public IConversationElement ParentElement;

		/// <summary>Controls the part's order of execution for events, higher values preceding lower ones.</summary>
		// Token: 0x0400376B RID: 14187
		public int Priority;

		// Token: 0x0400376C RID: 14188
		public int Propagation = -1;
	}
}
