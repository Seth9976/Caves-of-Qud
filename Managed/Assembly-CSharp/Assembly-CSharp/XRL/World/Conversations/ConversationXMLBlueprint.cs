using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using Wintellect.PowerCollections;
using XRL.Language;

namespace XRL.World.Conversations
{
	// Token: 0x02000BE7 RID: 3047
	[Serializable]
	public class ConversationXMLBlueprint : IEquatable<ConversationXMLBlueprint>
	{
		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06007A6E RID: 31342 RVA: 0x002DFC46 File Offset: 0x002DDE46
		public string CardinalID
		{
			get
			{
				if (this.Cardinal <= 1)
				{
					return this.ID;
				}
				return this.ID + this.Cardinal.ToString();
			}
		}

		// Token: 0x06007A6F RID: 31343 RVA: 0x002DFC6E File Offset: 0x002DDE6E
		public ConversationXMLBlueprint()
		{
		}

		/// <summary>Shallow copy of original blueprint.</summary>
		// Token: 0x06007A70 RID: 31344 RVA: 0x002DFC84 File Offset: 0x002DDE84
		public ConversationXMLBlueprint(ConversationXMLBlueprint Original)
			: this()
		{
			this.ID = Original.ID;
			this.Name = Original.Name;
			this.Text = Original.Text;
			this.Inherits = Original.Inherits;
			this.Distribute = Original.Distribute;
			this.Load = Original.Load;
			if (!Original.Attributes.IsNullOrEmpty<KeyValuePair<string, string>>())
			{
				this.Attributes = new Dictionary<string, string>(Original.Attributes);
			}
			if (!Original.Children.IsNullOrEmpty<ConversationXMLBlueprint>())
			{
				this.Children = new List<ConversationXMLBlueprint>(Original.Children);
			}
		}

		// Token: 0x06007A71 RID: 31345 RVA: 0x002DFD1C File Offset: 0x002DDF1C
		public void Merge(ConversationXMLBlueprint Blueprint)
		{
			if (!string.IsNullOrWhiteSpace(Blueprint.Text))
			{
				this.Text = Blueprint.Text;
			}
			if (!Blueprint.Distribute.IsNullOrEmpty())
			{
				this.Distribute = Blueprint.Distribute;
			}
			if (Blueprint.Attributes != null)
			{
				if (this.Attributes == null)
				{
					this.Attributes = new Dictionary<string, string>();
				}
				foreach (KeyValuePair<string, string> keyValuePair in Blueprint.Attributes)
				{
					this.Attributes[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			if (Blueprint.Children != null)
			{
				if (this.Children == null)
				{
					this.Children = new List<ConversationXMLBlueprint>();
				}
				foreach (ConversationXMLBlueprint conversationXMLBlueprint in Blueprint.Children)
				{
					if (conversationXMLBlueprint.Load == 2)
					{
						this.AddChild(conversationXMLBlueprint);
					}
					else
					{
						int num = this.Children.IndexOf(conversationXMLBlueprint);
						if (num != -1)
						{
							if (conversationXMLBlueprint.Load == 1)
							{
								this.Children[num] = conversationXMLBlueprint;
							}
							else if (conversationXMLBlueprint.Load == 3)
							{
								this.Children.RemoveAt(num);
							}
							else
							{
								this.Children[num].Merge(conversationXMLBlueprint);
							}
						}
						else
						{
							this.Children.Add(conversationXMLBlueprint);
						}
					}
				}
			}
		}

		// Token: 0x06007A72 RID: 31346 RVA: 0x002DFEA4 File Offset: 0x002DE0A4
		public void Recardinate(ConversationXMLBlueprint Blueprint)
		{
			Blueprint.Cardinal = 1;
			if (this.Children != null)
			{
				while (this.Children.Count<ConversationXMLBlueprint>(new Func<ConversationXMLBlueprint, bool>(this.Equals)) > 1)
				{
					Blueprint.Cardinal++;
				}
			}
		}

		// Token: 0x06007A73 RID: 31347 RVA: 0x002DFEE0 File Offset: 0x002DE0E0
		public ConversationXMLBlueprint Reference(int Mod = 1)
		{
			this.References += Mod;
			if (this.Children.IsNullOrEmpty<ConversationXMLBlueprint>())
			{
				return this;
			}
			for (int i = 0; i < this.Children.Count; i++)
			{
				this.Children[i].Reference(Mod);
			}
			return this;
		}

		// Token: 0x06007A74 RID: 31348 RVA: 0x002DFF34 File Offset: 0x002DE134
		public bool IsEmpty()
		{
			return this.Attributes.IsNullOrEmpty<KeyValuePair<string, string>>() && this.Children.IsNullOrEmpty<ConversationXMLBlueprint>() && this.Text.IsNullOrEmpty();
		}

		// Token: 0x06007A75 RID: 31349 RVA: 0x002DFF60 File Offset: 0x002DE160
		public void Inherit(ConversationXMLBlueprint Blueprint, BuildContext Context)
		{
			if (this.References > 0 && !Blueprint.IsEmpty())
			{
				ConversationXMLBlueprint conversationXMLBlueprint = new ConversationXMLBlueprint(this);
				if (Context.Peek() == this)
				{
					Context.Pop();
					Context.Push(conversationXMLBlueprint);
				}
				conversationXMLBlueprint.Inherit(Blueprint, Context);
				this.References--;
				return;
			}
			if (this.ID != Blueprint.ID && (string.IsNullOrWhiteSpace(this.ID) || this.ID == this.Name))
			{
				ConversationXMLBlueprint conversationXMLBlueprint2 = Context.Parent(this);
				if (conversationXMLBlueprint2 != null && this.IsEmpty())
				{
					int num = conversationXMLBlueprint2.Children.IndexOf(this);
					if (num == -1)
					{
						throw new Exception("Orphaned child by ID: " + Context.AssemblePathID(this));
					}
					conversationXMLBlueprint2.Children[num] = Blueprint.Reference(1);
					conversationXMLBlueprint2.Recardinate(Blueprint);
					if (Context.Peek() == this)
					{
						Context.Pop();
						Context.Push(Blueprint);
					}
					return;
				}
				else
				{
					this.ID = Blueprint.ID;
					if (conversationXMLBlueprint2 != null)
					{
						conversationXMLBlueprint2.Recardinate(this);
					}
				}
			}
			if (string.IsNullOrWhiteSpace(this.Text))
			{
				this.Text = Blueprint.Text;
			}
			if (this.Distribute == null)
			{
				this.Distribute = Blueprint.Distribute;
			}
			if (Blueprint.Attributes != null)
			{
				if (this.Attributes == null)
				{
					this.Attributes = new Dictionary<string, string>();
				}
				foreach (KeyValuePair<string, string> keyValuePair in Blueprint.Attributes)
				{
					if (!this.Attributes.ContainsKey(keyValuePair.Key))
					{
						this.Attributes[keyValuePair.Key] = keyValuePair.Value;
					}
				}
			}
			if (Blueprint.Children != null)
			{
				if (this.Children == null)
				{
					this.Children = new List<ConversationXMLBlueprint>();
				}
				for (int i = Blueprint.Children.Count - 1; i >= 0; i--)
				{
					this.InheritChild(Blueprint.Children[i], Context, true);
				}
			}
		}

		// Token: 0x06007A76 RID: 31350 RVA: 0x002E0170 File Offset: 0x002DE370
		public void InheritChild(ConversationXMLBlueprint Child, BuildContext Context, bool Insert = true)
		{
			if (this.Children == null)
			{
				this.Children = new List<ConversationXMLBlueprint>();
			}
			int num = this.Children.IndexOf(Child);
			if (num != -1)
			{
				ConversationXMLBlueprint conversationXMLBlueprint = this.Children[num];
				if (conversationXMLBlueprint.Load == 2)
				{
					this.Children.Insert(Insert ? 0 : this.Children.Count, Child.Reference(1));
					this.Recardinate(conversationXMLBlueprint);
					return;
				}
				if (conversationXMLBlueprint.Load == 3)
				{
					this.Children.RemoveAt(num);
					return;
				}
				if (conversationXMLBlueprint.Load != 1)
				{
					this.Children[num].Inherit(Child, Context);
					return;
				}
			}
			else
			{
				this.Children.Insert(Insert ? 0 : this.Children.Count, Child.Reference(1));
			}
		}

		// Token: 0x06007A77 RID: 31351 RVA: 0x002E0238 File Offset: 0x002DE438
		public bool ResolveBlueprint(string ID, BuildContext Context)
		{
			ConversationXMLBlueprint conversationXMLBlueprint;
			if (ID.Split('.', StringSplitOptions.None).Length == 1)
			{
				if (!Context.Finalized.TryGetValue(Context.AssembleNamedID(this, ID), out conversationXMLBlueprint) && !Context.Finalized.TryGetValue(Context.AssemblePathID(ID, 1), out conversationXMLBlueprint))
				{
					return Context.Missing(this, ID);
				}
			}
			else if (!Context.Finalized.TryGetValue(ID, out conversationXMLBlueprint))
			{
				ConversationXMLBlueprint conversationXMLBlueprint2 = Context.Lineage.LastOrDefault<ConversationXMLBlueprint>();
				if (conversationXMLBlueprint2 == null || conversationXMLBlueprint2 == this)
				{
					return Context.Missing(this, ID);
				}
				Context.Text.Clear().Append(conversationXMLBlueprint2.ID).Append('.')
					.Append(ID);
				if (!Context.Finalized.TryGetValue(Context.Text.ToString(), out conversationXMLBlueprint))
				{
					return Context.Missing(this, ID);
				}
			}
			this.Inherit(conversationXMLBlueprint, Context);
			return true;
		}

		// Token: 0x06007A78 RID: 31352 RVA: 0x002E030C File Offset: 0x002DE50C
		public void DistributeChildren(BuildContext Context)
		{
			Context.Push(this);
			if (!this.Distribute.IsNullOrEmpty())
			{
				string[] array = this.Distribute.Split(',', StringSplitOptions.None);
				bool flag = this.Qualifier == 2;
				bool flag2 = !flag && this.Distribute == "Start";
				ConversationXMLBlueprint conversationXMLBlueprint = Context.Parent(null);
				if (((conversationXMLBlueprint != null) ? conversationXMLBlueprint.Children : null) == null)
				{
					return;
				}
				foreach (ConversationXMLBlueprint conversationXMLBlueprint2 in conversationXMLBlueprint.Children)
				{
					if (conversationXMLBlueprint2 != this && conversationXMLBlueprint2.Distribute.IsNullOrEmpty())
					{
						if (flag)
						{
							if (array.Contains(conversationXMLBlueprint2.ID))
							{
								conversationXMLBlueprint2.InheritChild(this, Context, false);
							}
						}
						else if (array.Contains(conversationXMLBlueprint2.Name) || (flag2 && conversationXMLBlueprint2.Name == "Node" && conversationXMLBlueprint2.ID == "Start"))
						{
							conversationXMLBlueprint2.InheritChild(this, Context, false);
						}
					}
				}
				conversationXMLBlueprint.Children.Remove(this);
			}
			else if (!this.Children.IsNullOrEmpty<ConversationXMLBlueprint>())
			{
				Algorithms.StableSortInPlace<ConversationXMLBlueprint>(this.Children, new Comparison<ConversationXMLBlueprint>(this.CompareDistributable));
				for (int i = this.Children.Count - 1; i >= 0; i--)
				{
					this.Children[i].DistributeChildren(Context);
				}
			}
			Context.Pop();
		}

		// Token: 0x06007A79 RID: 31353 RVA: 0x002E0498 File Offset: 0x002DE698
		private int CompareDistributable(ConversationXMLBlueprint A, ConversationXMLBlueprint B)
		{
			if (A.Distribute.IsNullOrEmpty())
			{
				if (!B.Distribute.IsNullOrEmpty())
				{
					return 1;
				}
				return 0;
			}
			else
			{
				if (!B.Distribute.IsNullOrEmpty())
				{
					return 0;
				}
				return -1;
			}
		}

		// Token: 0x06007A7A RID: 31354 RVA: 0x002E04C8 File Offset: 0x002DE6C8
		public bool Bake(BuildContext Context)
		{
			if (!this.ID.IsNullOrEmpty() && Context.Finalized.ContainsKey(Context.AssemblePathID(this)))
			{
				return true;
			}
			Context.Push(this);
			bool flag = true;
			if (!this.Children.IsNullOrEmpty<ConversationXMLBlueprint>())
			{
				for (int i = 0; i < this.Children.Count; i++)
				{
					flag &= this.Children[i].Bake(Context);
				}
			}
			if (!this.Inherits.IsNullOrEmpty())
			{
				if (this.Inherits.Contains(','))
				{
					foreach (string text in this.Inherits.Split(',', StringSplitOptions.None))
					{
						flag &= this.ResolveBlueprint(text, Context);
					}
				}
				else
				{
					flag &= this.ResolveBlueprint(this.Inherits, Context);
				}
			}
			ConversationXMLBlueprint conversationXMLBlueprint = Context.Pop();
			if (flag)
			{
				Context.Finalized[Context.AssemblePathID(conversationXMLBlueprint)] = this;
				Context.Finalized.TryAdd(Context.AssembleNamedID(conversationXMLBlueprint, null), this);
				if (!this.Children.IsNullOrEmpty<ConversationXMLBlueprint>())
				{
					for (int k = this.Children.Count - 1; k >= 0; k--)
					{
						if (this.TrySortAt(k) < k)
						{
							k++;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x170009AC RID: 2476
		public string this[string Key]
		{
			get
			{
				return this.GetAttribute(Key);
			}
			set
			{
				this.SetAttribute(Key, value);
			}
		}

		// Token: 0x06007A7D RID: 31357 RVA: 0x002E061B File Offset: 0x002DE81B
		public string GetAttribute(string Key)
		{
			Dictionary<string, string> attributes = this.Attributes;
			if (attributes == null)
			{
				return null;
			}
			return attributes.GetValue(Key, null);
		}

		// Token: 0x06007A7E RID: 31358 RVA: 0x002E0630 File Offset: 0x002DE830
		public void SetAttribute(string Key, string Value)
		{
			if (this.Attributes == null)
			{
				this.Attributes = new Dictionary<string, string>();
			}
			this.Attributes[Key] = Value;
		}

		// Token: 0x06007A7F RID: 31359 RVA: 0x002E0652 File Offset: 0x002DE852
		public void RemoveAttribute(string Key)
		{
			Dictionary<string, string> attributes = this.Attributes;
			if (attributes == null)
			{
				return;
			}
			attributes.Remove(Key);
		}

		// Token: 0x06007A80 RID: 31360 RVA: 0x002E0666 File Offset: 0x002DE866
		public void AddChild(ConversationXMLBlueprint Child)
		{
			if (this.Children == null)
			{
				this.Children = new List<ConversationXMLBlueprint>();
			}
			while (this.Children.IndexOf(Child) >= 0)
			{
				Child.Cardinal++;
			}
			this.Children.Add(Child);
		}

		// Token: 0x06007A81 RID: 31361 RVA: 0x002E06A8 File Offset: 0x002DE8A8
		public ConversationXMLBlueprint GetChild(params string[] Path)
		{
			ConversationXMLBlueprint conversationXMLBlueprint = this;
			int num = 0;
			while (num < Path.Length && conversationXMLBlueprint != null)
			{
				conversationXMLBlueprint = conversationXMLBlueprint.GetChild(Path[num]);
				num++;
			}
			return conversationXMLBlueprint;
		}

		// Token: 0x06007A82 RID: 31362 RVA: 0x002E06D3 File Offset: 0x002DE8D3
		public ConversationXMLBlueprint GetChild(string First, string Second)
		{
			ConversationXMLBlueprint child = this.GetChild(First);
			if (child == null)
			{
				return null;
			}
			return child.GetChild(Second);
		}

		// Token: 0x06007A83 RID: 31363 RVA: 0x002E06E8 File Offset: 0x002DE8E8
		public ConversationXMLBlueprint GetChild(string First, string Second, string Third)
		{
			ConversationXMLBlueprint child = this.GetChild(First);
			if (child == null)
			{
				return null;
			}
			ConversationXMLBlueprint child2 = child.GetChild(Second);
			if (child2 == null)
			{
				return null;
			}
			return child2.GetChild(Third);
		}

		// Token: 0x06007A84 RID: 31364 RVA: 0x002E070C File Offset: 0x002DE90C
		public ConversationXMLBlueprint GetChild(string CardinalID)
		{
			int childIndex = this.GetChildIndex(CardinalID);
			if (childIndex == -1)
			{
				return null;
			}
			return this.Children[childIndex];
		}

		// Token: 0x06007A85 RID: 31365 RVA: 0x002E0738 File Offset: 0x002DE938
		public ConversationXMLBlueprint GetChild(ReadOnlySpan<char> CardinalID)
		{
			int childIndex = this.GetChildIndex(CardinalID);
			if (childIndex == -1)
			{
				return null;
			}
			return this.Children[childIndex];
		}

		// Token: 0x06007A86 RID: 31366 RVA: 0x002E0760 File Offset: 0x002DE960
		public ConversationXMLBlueprint GetChild(ReadOnlySpan<char> ID, int Cardinal)
		{
			int childIndex = this.GetChildIndex(ID, Cardinal);
			if (childIndex == -1)
			{
				return null;
			}
			return this.Children[childIndex];
		}

		// Token: 0x06007A87 RID: 31367 RVA: 0x002E0788 File Offset: 0x002DE988
		public ConversationXMLBlueprint GetChild(string ID, int Cardinal)
		{
			int childIndex = this.GetChildIndex(ID, Cardinal);
			if (childIndex == -1)
			{
				return null;
			}
			return this.Children[childIndex];
		}

		// Token: 0x06007A88 RID: 31368 RVA: 0x002E07B0 File Offset: 0x002DE9B0
		public unsafe int GetChildIndex(ReadOnlySpan<char> CardinalID)
		{
			int num = -1;
			int num2 = CardinalID.Length - 1;
			while (num2 >= 0 && char.IsNumber((char)(*CardinalID[num2])))
			{
				num = num2;
				num2--;
			}
			if (num < 0)
			{
				return this.GetChildIndex(CardinalID, 1);
			}
			int childIndex = this.GetChildIndex(CardinalID.Slice(0, num), int.Parse(CardinalID.Slice(num), NumberStyles.Integer, null));
			if (childIndex == -1)
			{
				return this.GetChildIndex(CardinalID, 1);
			}
			return childIndex;
		}

		// Token: 0x06007A89 RID: 31369 RVA: 0x002E0820 File Offset: 0x002DEA20
		public int GetChildIndex(ReadOnlySpan<char> ID, int Cardinal)
		{
			if (this.Children == null)
			{
				return -1;
			}
			int i = 0;
			int count = this.Children.Count;
			while (i < count)
			{
				ConversationXMLBlueprint conversationXMLBlueprint = this.Children[i];
				if (conversationXMLBlueprint.Cardinal == Cardinal && ID.SequenceEqual<char>(conversationXMLBlueprint.ID))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06007A8A RID: 31370 RVA: 0x002E087C File Offset: 0x002DEA7C
		public int GetChildIndex(string ID, int Cardinal)
		{
			if (this.Children == null)
			{
				return -1;
			}
			int i = 0;
			int count = this.Children.Count;
			while (i < count)
			{
				ConversationXMLBlueprint conversationXMLBlueprint = this.Children[i];
				if (conversationXMLBlueprint.Cardinal == Cardinal && !(conversationXMLBlueprint.ID != ID))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06007A8B RID: 31371 RVA: 0x002E08D4 File Offset: 0x002DEAD4
		private int TrySortAt(int Index)
		{
			ConversationXMLBlueprint conversationXMLBlueprint = this.Children[Index];
			if (conversationXMLBlueprint.Attributes.IsNullOrEmpty<KeyValuePair<string, string>>())
			{
				return Index;
			}
			int num = 0;
			string text;
			if (!conversationXMLBlueprint.Attributes.TryGetValue("Before", out text))
			{
				if (!conversationXMLBlueprint.Attributes.TryGetValue("After", out text))
				{
					return Index;
				}
				num = 1;
			}
			int num2 = -1;
			foreach (ReadOnlySpan<char> readOnlySpan in text.DelimitedBy(','))
			{
				num2 = this.GetChildIndex(readOnlySpan);
				if (num2 != -1)
				{
					break;
				}
			}
			if (num2 != -1 && num2 != Index)
			{
				this.Children.RemoveAt(Index);
				if (num2 > Index)
				{
					num2--;
				}
				num2 = this.TrySortAt(num2) + num;
				this.Children.Insert(num2, conversationXMLBlueprint);
				return num2;
			}
			return Index;
		}

		// Token: 0x06007A8C RID: 31372 RVA: 0x002E099C File Offset: 0x002DEB9C
		public void ReadAttributes(XmlTextReader Reader, BuildContext Context)
		{
			if (!Reader.HasAttributes)
			{
				this.ID = this.Name;
				if (this.Name == "Choice")
				{
					ConversationXMLBlueprint conversationXMLBlueprint = Context.Parent(null);
					if (((conversationXMLBlueprint != null) ? conversationXMLBlueprint.Name : null) == "Conversation")
					{
						this.Distribute = "Start";
					}
				}
				return;
			}
			this.Attributes = new Dictionary<string, string>();
			while (Reader.MoveToNextAttribute())
			{
				string value = Reader.Value;
				string text = this.ReadAttribute(Reader, ref value, Context);
				if (!text.IsNullOrEmpty())
				{
					this.Attributes[string.Intern(text)] = string.Intern(value);
				}
			}
			if (this.Distribute == null && this.Name == "Choice")
			{
				ConversationXMLBlueprint conversationXMLBlueprint2 = Context.Parent(null);
				if (((conversationXMLBlueprint2 != null) ? conversationXMLBlueprint2.Name : null) == "Conversation")
				{
					this.Distribute = "Start";
				}
			}
		}

		// Token: 0x06007A8D RID: 31373 RVA: 0x002E0A84 File Offset: 0x002DEC84
		public string ReplaceText(string Text)
		{
			string text;
			if (!this.Children.IsNullOrEmpty<ConversationXMLBlueprint>())
			{
				foreach (ConversationXMLBlueprint conversationXMLBlueprint in this.Children)
				{
					if (conversationXMLBlueprint.Name == "Text")
					{
						text = conversationXMLBlueprint.Text;
						conversationXMLBlueprint.Text = Text;
						return text;
					}
				}
			}
			text = this.Text;
			this.Text = Text;
			return text;
		}

		// Token: 0x06007A8E RID: 31374 RVA: 0x002E0B18 File Offset: 0x002DED18
		public string ReadAttribute(XmlTextReader Reader, ref string Value, BuildContext Context)
		{
			string name = Reader.Name;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 1458105184U)
			{
				if (num <= 496827801U)
				{
					if (num != 266367750U)
					{
						if (num != 423970612U)
						{
							if (num != 496827801U)
							{
								return name;
							}
							if (!(name == "UseID"))
							{
								return name;
							}
							if (this.Inherits == null)
							{
								this.Inherits = string.Intern(Value);
							}
							return null;
						}
						else if (!(name == "IfNotHavePart"))
						{
							return name;
						}
					}
					else
					{
						if (!(name == "Name"))
						{
							return name;
						}
						if (this.ID == null)
						{
							this.ID = string.Intern(Value);
						}
						if (this.Name == "Part")
						{
							if (!Context.Namespace.IsNullOrEmpty())
							{
								string text = Context.Namespace + "." + Value;
								if (IConversationPart.CanResolve(text))
								{
									Value = string.Intern(text);
									return name;
								}
							}
							if (Value.IndexOf('.') < 0)
							{
								string text2 = "XRL.World.Conversations.Parts." + Value;
								if (IConversationPart.CanResolve(text2))
								{
									Value = string.Intern(text2);
									return name;
								}
							}
							if (!IConversationPart.CanResolve(Value))
							{
								MetricsManager.LogFileError(Context.File, "No part of name '" + Value + "' could be resolved.", Reader);
							}
						}
						return name;
					}
				}
				else if (num <= 920600990U)
				{
					if (num != 597910197U)
					{
						if (num != 920600990U)
						{
							return name;
						}
						if (!(name == "Distribute"))
						{
							return name;
						}
						this.Distribute = string.Intern(Value);
						return null;
					}
					else if (!(name == "IfHavePart"))
					{
						return name;
					}
				}
				else if (num != 1386747739U)
				{
					if (num != 1458105184U)
					{
						return name;
					}
					if (!(name == "ID"))
					{
						return name;
					}
					this.ID = string.Intern(Value);
					return null;
				}
				else
				{
					if (!(name == "GotoID"))
					{
						return name;
					}
					if (this.ID == null)
					{
						this.ID = string.Intern(Value + this.Name);
					}
					return "Target";
				}
			}
			else if (num <= 2523519369U)
			{
				if (num <= 2284143367U)
				{
					if (num != 1756340745U)
					{
						if (num != 2284143367U)
						{
							return name;
						}
						if (!(name == "IfSpeakerNotHavePart"))
						{
							return name;
						}
					}
					else
					{
						if (!(name == "Load"))
						{
							return name;
						}
						string text3 = Value.ToLowerInvariant();
						byte b;
						if (!(text3 == "replace"))
						{
							if (!(text3 == "add"))
							{
								if (!(text3 == "remove"))
								{
									b = 0;
								}
								else
								{
									b = 3;
								}
							}
							else
							{
								b = 2;
							}
						}
						else
						{
							b = 1;
						}
						this.Load = b;
						return null;
					}
				}
				else if (num != 2338845032U)
				{
					if (num != 2523519369U)
					{
						return name;
					}
					if (!(name == "Cardinal"))
					{
						return name;
					}
					int num2;
					if (int.TryParse(Value, out num2))
					{
						this.Cardinal = num2;
					}
					else
					{
						MetricsManager.LogFileError(Context.File, "Invalid merge cardinal: " + Value, Reader);
					}
					return null;
				}
				else
				{
					if (!(name == "Target"))
					{
						return name;
					}
					if (this.ID == null)
					{
						this.ID = string.Intern(Value + this.Name);
					}
					return name;
				}
			}
			else if (num <= 3211025119U)
			{
				if (num != 3201769609U)
				{
					if (num != 3211025119U)
					{
						return name;
					}
					if (!(name == "Qualifier"))
					{
						return name;
					}
					string text3 = Value.ToLowerInvariant();
					byte b;
					if (!(text3 == "id"))
					{
						if (!(text3 == "name"))
						{
							if (!(text3 == "type"))
							{
								b = 0;
							}
							else
							{
								b = 1;
							}
						}
						else
						{
							b = 1;
						}
					}
					else
					{
						b = 2;
					}
					this.Qualifier = b;
					return null;
				}
				else
				{
					if (!(name == "Inherits"))
					{
						return name;
					}
					this.Inherits = string.Intern(Value);
					return null;
				}
			}
			else if (num != 3576578624U)
			{
				if (num != 4021092656U)
				{
					return name;
				}
				if (!(name == "IfSpeakerHavePart"))
				{
					return name;
				}
			}
			else
			{
				if (!(name == "Namespace"))
				{
					return name;
				}
				return null;
			}
			string text4;
			string text5;
			if (CompatManager.TryGetPart(Value, out text4, out text5))
			{
				MetricsManager.LogFileWarning(Context.File, string.Concat(new string[]
				{
					Context.AssemblePathID(),
					".",
					name,
					": Referencing deprecated ",
					text5,
					" '",
					Value,
					"' which has been replaced by '",
					text4,
					"'."
				}), Reader);
				Value = text4;
			}
			return name;
		}

		// Token: 0x06007A8F RID: 31375 RVA: 0x002E0FF8 File Offset: 0x002DF1F8
		public void Read(XmlTextReader Reader, BuildContext Context)
		{
			Context.Push(this);
			Context.Record(this, Reader);
			this.Name = string.Intern(Grammar.InitCap(Reader.Name));
			if (Reader.IsEmptyElement || Reader.NodeType == XmlNodeType.EndElement)
			{
				this.ReadAttributes(Reader, Context);
				Context.Pop();
				return;
			}
			this.ReadAttributes(Reader, Context);
			while (Reader.Read())
			{
				XmlNodeType nodeType = Reader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType - XmlNodeType.Text > 1)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							Context.Pop();
							return;
						}
					}
					else
					{
						Context.Text.Clear().Append(Reader.Value).Unindent();
						if (this.Name == "Text" || this.Name == "Part")
						{
							this.Text = Context.Text.ToString();
						}
						else
						{
							this.AddChild(new ConversationXMLBlueprint
							{
								Name = "Text",
								ID = "Text",
								Text = Context.Text.ToString()
							});
						}
					}
				}
				else
				{
					ConversationXMLBlueprint conversationXMLBlueprint = new ConversationXMLBlueprint();
					conversationXMLBlueprint.Read(Reader, Context);
					this.AddChild(conversationXMLBlueprint);
				}
			}
			Context.Pop();
		}

		// Token: 0x06007A90 RID: 31376 RVA: 0x002E1130 File Offset: 0x002DF330
		public bool Equals(ConversationXMLBlueprint Other)
		{
			return Other != null && (this == Other || (this.Cardinal == Other.Cardinal && this.Name == Other.Name && this.ID == Other.ID));
		}

		// Token: 0x040037D5 RID: 14293
		public const byte LOAD_MERGE = 0;

		// Token: 0x040037D6 RID: 14294
		public const byte LOAD_REPLACE = 1;

		// Token: 0x040037D7 RID: 14295
		public const byte LOAD_ADD = 2;

		// Token: 0x040037D8 RID: 14296
		public const byte LOAD_REMOVE = 3;

		// Token: 0x040037D9 RID: 14297
		public const byte DIST_NONE = 0;

		// Token: 0x040037DA RID: 14298
		public const byte DIST_TYPE = 1;

		// Token: 0x040037DB RID: 14299
		public const byte DIST_ID = 2;

		// Token: 0x040037DC RID: 14300
		public const byte DIST_ESCAPE = 4;

		// Token: 0x040037DD RID: 14301
		public const byte DIST_LOCAL = 8;

		/// <summary>
		/// An ID for this element, used for navigation, merging, and inheritance.
		/// Not required to be unique, though still recommended.
		/// </summary>
		/// <see cref="F:XRL.World.Conversations.ConversationXMLBlueprint.Cardinal" />
		// Token: 0x040037DE RID: 14302
		public string ID;

		/// <summary>The element's type, 'Node', 'Choice', 'Text', etc.</summary>
		// Token: 0x040037DF RID: 14303
		public string Name;

		/// <summary>The plain text content of this element.</summary>
		// Token: 0x040037E0 RID: 14304
		public string Text;

		/// <summary>The ID of one or more base blueprints to inherit.</summary>
		/// <example>Inherit a choice from a node in another conversation: 'ConversationID.NodeID.ChoiceID' -&gt; 'Barathrum.TombExplain3.QuestionsChoice'.</example>
		/// <example>Inherit a choice from a node in this conversation: 'NodeID.ChoiceID' -&gt; 'TombExplain3.QuestionsChoice'.</example>
		// Token: 0x040037E1 RID: 14305
		public string Inherits;

		/// <summary>An incrementing suffix to distinguish elements with non-unique IDs.</summary>
		/// <remarks>The default value of '1' is omitted from the final ID.</remarks>
		// Token: 0x040037E2 RID: 14306
		public int Cardinal = 1;

		/// <summary>A count of references to this element from inheritors.</summary>
		// Token: 0x040037E3 RID: 14307
		public int References;

		/// <summary>Distribute this element to be inherited by its sibling elements of specified type.</summary>
		// Token: 0x040037E4 RID: 14308
		public string Distribute;

		/// <summary>Controls distribution behavior.</summary>
		// Token: 0x040037E5 RID: 14309
		public byte Qualifier = 1;

		/// <summary>Controls the behavior on a merge/inherit conflict.</summary>
		// Token: 0x040037E6 RID: 14310
		public byte Load;

		/// <summary>
		/// A dictionary of attributes defined on this element.
		/// These are typically consumed by delegates or parsed and assigned to writeable members.
		/// </summary>
		// Token: 0x040037E7 RID: 14311
		public Dictionary<string, string> Attributes;

		/// <summary>A list of child elements.</summary>
		// Token: 0x040037E8 RID: 14312
		public List<ConversationXMLBlueprint> Children;
	}
}
