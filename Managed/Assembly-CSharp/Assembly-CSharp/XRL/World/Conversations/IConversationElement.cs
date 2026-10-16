using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using XRL.UI;

namespace XRL.World.Conversations
{
	// Token: 0x02000BEF RID: 3055
	public abstract class IConversationElement : IComparable<IConversationElement>, IDisposable
	{
		// Token: 0x170009BC RID: 2492
		// (set) Token: 0x06007AC5 RID: 31429 RVA: 0x002E1CDD File Offset: 0x002DFEDD
		public int Ordinal
		{
			set
			{
				this.Priority = -value;
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06007AC6 RID: 31430 RVA: 0x002E1CE7 File Offset: 0x002DFEE7
		public string PathID
		{
			get
			{
				if (this.Parent != null)
				{
					return this.Parent.PathID + "." + this.ID;
				}
				return this.ID;
			}
		}

		// Token: 0x06007AC7 RID: 31431 RVA: 0x002E1D14 File Offset: 0x002DFF14
		public IConversationElement GetText()
		{
			if (this.Texts.IsNullOrEmpty<ConversationText>())
			{
				return null;
			}
			GetTextElementEvent getTextElementEvent = GetTextElementEvent.FromPool();
			int num = int.MinValue;
			foreach (ConversationText conversationText in this.Texts)
			{
				getTextElementEvent.Texts.Add(conversationText);
				if (conversationText.IsVisible())
				{
					getTextElementEvent.Visible.Add(conversationText);
					if (conversationText.Priority > num)
					{
						num = conversationText.Priority;
					}
				}
			}
			if (getTextElementEvent.Visible.Count == 0)
			{
				return null;
			}
			foreach (ConversationText conversationText2 in getTextElementEvent.Visible)
			{
				if (conversationText2.Priority == num)
				{
					getTextElementEvent.Group.Add(conversationText2);
				}
			}
			getTextElementEvent.Selected = getTextElementEvent.Group.GetRandomElement<ConversationText>(null);
			getTextElementEvent.Element = this;
			this.HandleEvent(getTextElementEvent);
			ConversationText selected = getTextElementEvent.Selected;
			GetTextElementEvent.ResetTo(ref getTextElementEvent);
			if (!selected.Text.IsNullOrEmpty())
			{
				return selected;
			}
			return selected.GetText();
		}

		// Token: 0x06007AC8 RID: 31432 RVA: 0x002E1E58 File Offset: 0x002E0058
		public IConversationElement GetAncestor(Predicate<IConversationElement> Predicate, bool IncludeSelf = true)
		{
			IConversationElement conversationElement = (IncludeSelf ? this : this.Parent);
			int num = 0;
			while (num < 100 && conversationElement != null)
			{
				if (Predicate(conversationElement))
				{
					return conversationElement;
				}
				conversationElement = conversationElement.Parent;
				num++;
			}
			return null;
		}

		// Token: 0x06007AC9 RID: 31433 RVA: 0x002E1E98 File Offset: 0x002E0098
		public virtual void Awake()
		{
			if (!this.Awoken)
			{
				if (this.Parts != null)
				{
					foreach (IConversationPart conversationPart in this.Parts)
					{
						conversationPart.Awake();
					}
				}
				this.Awoken = true;
			}
		}

		// Token: 0x06007ACA RID: 31434 RVA: 0x002E1F00 File Offset: 0x002E0100
		public virtual void Prepare()
		{
			if (this.Text != null)
			{
				return;
			}
			IConversationElement text = this.GetText();
			if (text == null)
			{
				return;
			}
			this.Text = text.Text.GetRandomSubstring('~', true, null);
			StringBuilder stringBuilder = Event.NewStringBuilder(this.Text.Trim());
			GameObject speaker = The.Speaker;
			GameObject gameObject = null;
			string text2;
			bool flag;
			string text3;
			bool flag2;
			PrepareTextEvent.Send(text, stringBuilder, ref speaker, ref gameObject, out text2, out flag, out text3, out flag2);
			StringBuilder stringBuilder2 = stringBuilder;
			GameObject gameObject2 = speaker;
			GameObject gameObject3 = gameObject;
			this.Text = GameText.VariableReplace(stringBuilder2, gameObject2, text2, flag, gameObject3, text3, flag2, false);
			IConversationElement conversationElement = text;
			ref string ptr = ref this.Text;
			PrepareTextLateEvent.Send(conversationElement, speaker, gameObject, ref ptr);
		}

		// Token: 0x06007ACB RID: 31435 RVA: 0x002E1F90 File Offset: 0x002E0190
		public virtual string GetDisplayText(bool WithColor = false)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(this.Text);
			GameObject speaker = The.Speaker;
			GameObject gameObject = null;
			string text;
			bool flag;
			string text2;
			bool flag2;
			bool flag3;
			DisplayTextEvent.Send(this, stringBuilder, ref speaker, ref gameObject, out text, out flag, out text2, out flag2, out flag3);
			if (flag3)
			{
				StringBuilder stringBuilder2 = stringBuilder;
				GameObject gameObject2 = speaker;
				GameObject gameObject3 = gameObject;
				GameText.VariableReplace(stringBuilder2, gameObject2, text, flag, gameObject3, text2, flag2, false);
			}
			if (WithColor)
			{
				stringBuilder.Insert(0, '|');
				stringBuilder.Insert(0, this.GetTextColor());
				stringBuilder.Insert(0, "{{");
				stringBuilder.Append("}}");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007ACC RID: 31436 RVA: 0x002E201C File Offset: 0x002E021C
		public virtual string GetTextColor()
		{
			string text = "y";
			ColorTextEvent.Send(this, ref text);
			return text;
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06007ACD RID: 31437 RVA: 0x002E2038 File Offset: 0x002E0238
		public virtual int Propagation
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06007ACE RID: 31438 RVA: 0x002E203B File Offset: 0x002E023B
		public bool WantEvent(int ID)
		{
			return this.WantEvent(ID, this.Propagation);
		}

		// Token: 0x06007ACF RID: 31439 RVA: 0x002E204C File Offset: 0x002E024C
		private bool WantEvent(int ID, int Propagation)
		{
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].PropagateTo(Propagation) && this.Parts[i].WantEvent(ID, Propagation))
					{
						return true;
					}
					i++;
				}
			}
			return this.Parent != null && this.Parent.WantEvent(ID, Propagation);
		}

		// Token: 0x06007AD0 RID: 31440 RVA: 0x002E20BB File Offset: 0x002E02BB
		public bool HandleEvent(ConversationEvent E)
		{
			return this.HandleEvent(E, this.Propagation);
		}

		// Token: 0x06007AD1 RID: 31441 RVA: 0x002E20CC File Offset: 0x002E02CC
		private bool HandleEvent(ConversationEvent E, int Propagation)
		{
			if (this.Parts != null)
			{
				for (int i = 0; i < this.Parts.Count; i++)
				{
					if (this.Parts[i].PropagateTo(Propagation) && this.Parts[i].WantEvent(E.ID, Propagation))
					{
						if (!E.HandlePartDispatch(this.Parts[i]))
						{
							return false;
						}
						this.Parts[i].HandleEvent(E);
					}
				}
			}
			return this.Parent == null || this.Parent.HandleEvent(E, Propagation);
		}

		// Token: 0x06007AD2 RID: 31442 RVA: 0x002E2165 File Offset: 0x002E0365
		public virtual bool Enter()
		{
			return EnterElementEvent.Check(this);
		}

		// Token: 0x06007AD3 RID: 31443 RVA: 0x002E2174 File Offset: 0x002E0374
		public virtual void Entered()
		{
			if (!this.Actions.IsNullOrEmpty<KeyValuePair<string, string>>())
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.Actions)
				{
					ActionReceiver actionReceiver;
					if (ConversationDelegates.Actions.TryGetValue(keyValuePair.Key, out actionReceiver))
					{
						try
						{
							string value = keyValuePair.Value;
							if (!value.StartsWith('(') || Expression.Evaluate(value, this, actionReceiver) == null)
							{
								actionReceiver(this, value);
							}
						}
						catch (Exception ex)
						{
							MetricsManager.LogException(string.Concat(new string[] { "Exception when executing action '", keyValuePair.Key, "' of '", this.PathID, "'" }), ex, "game_exception");
						}
					}
				}
			}
			EnteredElementEvent.Send(this);
		}

		// Token: 0x06007AD4 RID: 31444 RVA: 0x002E2274 File Offset: 0x002E0474
		public virtual bool Leave()
		{
			return LeaveElementEvent.Check(this);
		}

		// Token: 0x06007AD5 RID: 31445 RVA: 0x002E2281 File Offset: 0x002E0481
		public virtual void Left()
		{
			this.Reset();
			LeftElementEvent.Send(this);
		}

		// Token: 0x06007AD6 RID: 31446 RVA: 0x002E228F File Offset: 0x002E048F
		public virtual void Reset()
		{
			if (ConversationUI.StartNode != this && !this.Texts.IsNullOrEmpty<ConversationText>())
			{
				this.Text = null;
			}
		}

		// Token: 0x06007AD7 RID: 31447 RVA: 0x002E22AD File Offset: 0x002E04AD
		public bool FireEvent(Event E)
		{
			return this.FireEvent(E, this.Propagation);
		}

		// Token: 0x06007AD8 RID: 31448 RVA: 0x002E22BC File Offset: 0x002E04BC
		private bool FireEvent(Event E, int Propagation)
		{
			if (this.Parts != null)
			{
				for (int i = 0; i < this.Parts.Count; i++)
				{
					if (this.Parts[i].PropagateTo(Propagation) && !this.Parts[i].FireEvent(E))
					{
						return false;
					}
				}
			}
			return this.Parent == null || this.Parent.FireEvent(E, Propagation);
		}

		// Token: 0x06007AD9 RID: 31449 RVA: 0x002E2328 File Offset: 0x002E0528
		public bool CheckPredicates()
		{
			return this.CheckPredicates(this.Predicates);
		}

		// Token: 0x06007ADA RID: 31450 RVA: 0x002E2338 File Offset: 0x002E0538
		public bool CheckPredicates(Dictionary<string, string> Predicates)
		{
			if (Predicates.IsNullOrEmpty<KeyValuePair<string, string>>())
			{
				return true;
			}
			foreach (KeyValuePair<string, string> keyValuePair in Predicates)
			{
				if (!this.CheckPredicate(keyValuePair.Key, keyValuePair.Value, true))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06007ADB RID: 31451 RVA: 0x002E23A8 File Offset: 0x002E05A8
		public bool CheckPredicate(string Key, string Value, bool Default = true)
		{
			PredicateReceiver predicateReceiver;
			if (ConversationDelegates.Predicates.TryGetValue(Key, out predicateReceiver))
			{
				try
				{
					if (Value.StartsWith('(') || Value.StartsWith("NOT (", StringComparison.Ordinal))
					{
						bool? flag = Expression.Evaluate(Value, this, predicateReceiver);
						if (flag != null)
						{
							return flag.Value;
						}
					}
					return predicateReceiver(this, Value);
				}
				catch (Exception ex)
				{
					MetricsManager.LogException(string.Concat(new string[] { "Exception when executing predicate '", Key, "' of '", this.PathID, "'" }), ex, "game_exception");
				}
				return Default;
			}
			return Default;
		}

		// Token: 0x06007ADC RID: 31452 RVA: 0x002E2454 File Offset: 0x002E0654
		public virtual bool IsVisible()
		{
			return this.CheckPredicates() && IsElementVisibleEvent.Check(this);
		}

		// Token: 0x06007ADD RID: 31453 RVA: 0x002E2468 File Offset: 0x002E0668
		public IConversationElement GetElement(params string[] Path)
		{
			IConversationElement conversationElement = this;
			int num = 0;
			while (num < Path.Length && conversationElement != null)
			{
				conversationElement = conversationElement.GetElementByID(Path[num]);
				num++;
			}
			return conversationElement;
		}

		// Token: 0x06007ADE RID: 31454 RVA: 0x002E2494 File Offset: 0x002E0694
		public IConversationElement GetElementByID(string ID)
		{
			if (this.Elements != null)
			{
				int i = 0;
				int count = this.Elements.Count;
				while (i < count)
				{
					if (this.Elements[i].ID == ID)
					{
						return this.Elements[i];
					}
					i++;
				}
			}
			if (this.Texts != null)
			{
				int j = 0;
				int count2 = this.Texts.Count;
				while (j < count2)
				{
					if (this.Texts[j].ID == ID)
					{
						return this.Texts[j];
					}
					j++;
				}
			}
			return null;
		}

		// Token: 0x06007ADF RID: 31455 RVA: 0x002E252E File Offset: 0x002E072E
		public bool TryGetAttribute(string Key, out string Value)
		{
			if (this.Attributes == null)
			{
				Value = null;
				return false;
			}
			return this.Attributes.TryGetValue(Key, out Value);
		}

		// Token: 0x06007AE0 RID: 31456 RVA: 0x002E254C File Offset: 0x002E074C
		public virtual void LoadAttributes(Dictionary<string, string> Attributes)
		{
			if (Attributes == null || Attributes.Count == 0)
			{
				return;
			}
			Type type = base.GetType();
			foreach (KeyValuePair<string, string> keyValuePair in Attributes)
			{
				PartGeneratorReceiver partGeneratorReceiver;
				if (ConversationDelegates.Predicates.ContainsKey(keyValuePair.Key))
				{
					if (this.Predicates == null)
					{
						this.Predicates = new Dictionary<string, string>();
					}
					this.Predicates[keyValuePair.Key] = keyValuePair.Value;
				}
				else if (ConversationDelegates.Actions.ContainsKey(keyValuePair.Key))
				{
					if (this.Actions == null)
					{
						this.Actions = new Dictionary<string, string>();
					}
					this.Actions[keyValuePair.Key] = keyValuePair.Value;
				}
				else if (ConversationDelegates.PartGenerators.TryGetValue(keyValuePair.Key, out partGeneratorReceiver))
				{
					IConversationPart conversationPart = partGeneratorReceiver(this, keyValuePair.Value);
					if (conversationPart != null)
					{
						this.AddPart(conversationPart, false);
					}
				}
				else
				{
					FieldInfo field = type.GetField(keyValuePair.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					if (field != null && !field.IsInitOnly && IConversationElement.ConvertTypes.Contains(field.FieldType))
					{
						field.SetValue(this, Convert.ChangeType(keyValuePair.Value, field.FieldType));
					}
					else
					{
						PropertyInfo property = type.GetProperty(keyValuePair.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
						if (property != null && property.CanWrite && IConversationElement.ConvertTypes.Contains(property.PropertyType))
						{
							property.SetValue(this, Convert.ChangeType(keyValuePair.Value, property.PropertyType));
						}
						else
						{
							if (this.Attributes == null)
							{
								this.Attributes = new Dictionary<string, string>();
							}
							this.Attributes[keyValuePair.Key] = keyValuePair.Value;
						}
					}
				}
			}
		}

		// Token: 0x06007AE1 RID: 31457 RVA: 0x002E2744 File Offset: 0x002E0944
		public T Create<T>(ConversationXMLBlueprint Blueprint) where T : IConversationElement, new()
		{
			T t = new T();
			t.Parent = this;
			t.Load(Blueprint);
			return t;
		}

		// Token: 0x06007AE2 RID: 31458 RVA: 0x002E2764 File Offset: 0x002E0964
		public virtual bool LoadChild(ConversationXMLBlueprint Blueprint)
		{
			string name = Blueprint.Name;
			if (!(name == "Node"))
			{
				if (!(name == "Choice"))
				{
					if (!(name == "Text"))
					{
						if (!(name == "Part"))
						{
							return false;
						}
						IConversationPart conversationPart;
						if (IConversationPart.TryCreate(Blueprint.Attributes["Name"], Blueprint, out conversationPart))
						{
							this.AddPart(conversationPart, false);
						}
					}
					else
					{
						if (this.Texts == null)
						{
							this.Texts = new List<ConversationText>();
						}
						this.Texts.Add(this.Create<ConversationText>(Blueprint));
					}
				}
				else
				{
					if (this.Elements == null)
					{
						this.Elements = new List<IConversationElement>();
					}
					this.Elements.Add(this.Create<Choice>(Blueprint));
				}
			}
			else
			{
				if (this.Elements == null)
				{
					this.Elements = new List<IConversationElement>();
				}
				this.Elements.Add(this.Create<Node>(Blueprint));
			}
			return true;
		}

		// Token: 0x06007AE3 RID: 31459 RVA: 0x002E284D File Offset: 0x002E0A4D
		public bool HasPart(IConversationPart Part)
		{
			return this.Parts != null && this.Parts.Contains(Part);
		}

		// Token: 0x06007AE4 RID: 31460 RVA: 0x002E2868 File Offset: 0x002E0A68
		public bool HasPart<T>() where T : IConversationPart
		{
			if (this.Parts == null)
			{
				return false;
			}
			int i = 0;
			int count = this.Parts.Count;
			while (i < count)
			{
				if (this.Parts[i].GetType() == typeof(T))
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06007AE5 RID: 31461 RVA: 0x002E28B8 File Offset: 0x002E0AB8
		public void AddPart(IConversationPart Part, bool Sort = true)
		{
			if (this.Parts == null)
			{
				this.Parts = new List<IConversationPart>();
			}
			this.Parts.Add(Part);
			Part.ParentElement = this;
			Part.Initialize();
			if (Sort)
			{
				this.Parts.Sort();
			}
			if (this.Awoken)
			{
				Part.Awake();
			}
		}

		// Token: 0x06007AE6 RID: 31462 RVA: 0x002E2910 File Offset: 0x002E0B10
		public T GetPart<T>() where T : IConversationPart
		{
			if (this.Parts == null)
			{
				return default(T);
			}
			int i = 0;
			int count = this.Parts.Count;
			while (i < count)
			{
				if (this.Parts[i].GetType() == typeof(T))
				{
					return this.Parts[i] as T;
				}
				i++;
			}
			return default(T);
		}

		// Token: 0x06007AE7 RID: 31463 RVA: 0x002E2984 File Offset: 0x002E0B84
		public bool TryGetPart<T>(out T Part) where T : IConversationPart
		{
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].GetType() == typeof(T))
					{
						Part = this.Parts[i] as T;
						return true;
					}
					i++;
				}
			}
			Part = default(T);
			return false;
		}

		// Token: 0x06007AE8 RID: 31464 RVA: 0x002E29F4 File Offset: 0x002E0BF4
		public Choice AddChoice(string ID = null, string Text = null, string Target = null)
		{
			Choice choice = new Choice
			{
				ID = (ID ?? default(Guid).ToString()),
				Parent = this,
				Text = Text,
				Target = Target
			};
			if (this.Elements == null)
			{
				this.Elements = new List<IConversationElement>();
			}
			this.Elements.Add(choice);
			return choice;
		}

		// Token: 0x06007AE9 RID: 31465 RVA: 0x002E2A5C File Offset: 0x002E0C5C
		public Node AddNode(string ID = null, string Text = null)
		{
			Node node = new Node
			{
				ID = (ID ?? default(Guid).ToString()),
				Parent = this,
				Text = Text
			};
			if (this.Elements == null)
			{
				this.Elements = new List<IConversationElement>();
			}
			this.Elements.Add(node);
			return node;
		}

		// Token: 0x06007AEA RID: 31466 RVA: 0x002E2ABC File Offset: 0x002E0CBC
		public virtual void LoadText(ConversationXMLBlueprint Blueprint)
		{
			if (!string.IsNullOrWhiteSpace(Blueprint.Text))
			{
				this.Text = Blueprint.Text;
			}
		}

		// Token: 0x06007AEB RID: 31467 RVA: 0x002E2AD8 File Offset: 0x002E0CD8
		public virtual void Load(ConversationXMLBlueprint Blueprint)
		{
			this.ID = Blueprint.CardinalID;
			this.LoadAttributes(Blueprint.Attributes);
			this.LoadText(Blueprint);
			if (Blueprint.Children == null)
			{
				return;
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
			List<IConversationPart> parts = this.Parts;
			if (parts == null)
			{
				return;
			}
			parts.Sort();
		}

		// Token: 0x06007AEC RID: 31468 RVA: 0x002E2B98 File Offset: 0x002E0D98
		public int CompareTo(IConversationElement Other)
		{
			if (Other == null)
			{
				return -1;
			}
			return Other.Priority.CompareTo(this.Priority);
		}

		// Token: 0x06007AED RID: 31469 RVA: 0x002E2BB0 File Offset: 0x002E0DB0
		public virtual void Dispose()
		{
			if (this.Parts != null)
			{
				foreach (IConversationPart conversationPart in this.Parts)
				{
					conversationPart.Dispose();
				}
			}
			if (this.Elements != null)
			{
				foreach (IConversationElement conversationElement in this.Elements)
				{
					conversationElement.Dispose();
				}
			}
			if (this.Texts != null)
			{
				foreach (ConversationText conversationText in this.Texts)
				{
					conversationText.Dispose();
				}
			}
		}

		// Token: 0x040037FA RID: 14330
		public const int END_SORT_ORDINAL = 999999;

		// Token: 0x040037FB RID: 14331
		public static HashSet<Type> ConvertTypes = new HashSet<Type>
		{
			typeof(object),
			typeof(DBNull),
			typeof(bool),
			typeof(char),
			typeof(sbyte),
			typeof(byte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(DateTime),
			typeof(string)
		};

		// Token: 0x040037FC RID: 14332
		public string ID;

		// Token: 0x040037FD RID: 14333
		public string Text;

		// Token: 0x040037FE RID: 14334
		public int Priority;

		// Token: 0x040037FF RID: 14335
		public bool Awoken;

		// Token: 0x04003800 RID: 14336
		public IConversationElement Parent;

		// Token: 0x04003801 RID: 14337
		public List<IConversationPart> Parts;

		// Token: 0x04003802 RID: 14338
		public List<IConversationElement> Elements;

		// Token: 0x04003803 RID: 14339
		public List<ConversationText> Texts;

		// Token: 0x04003804 RID: 14340
		public Dictionary<string, string> Predicates;

		// Token: 0x04003805 RID: 14341
		public Dictionary<string, string> Actions;

		// Token: 0x04003806 RID: 14342
		public Dictionary<string, string> Attributes;
	}
}
