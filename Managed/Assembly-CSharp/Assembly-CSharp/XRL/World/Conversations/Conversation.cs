using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;
using XRL.UI;

namespace XRL.World.Conversations
{
	// Token: 0x02000BE9 RID: 3049
	[HasModSensitiveStaticCache]
	public class Conversation : IConversationElement
	{
		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06007A9D RID: 31389 RVA: 0x002E1354 File Offset: 0x002DF554
		public static Dictionary<string, ConversationXMLBlueprint> Blueprints
		{
			get
			{
				if (Conversation._Blueprints == null)
				{
					ConversationLoader.CheckInit();
				}
				return Conversation._Blueprints;
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06007A9E RID: 31390 RVA: 0x002E1367 File Offset: 0x002DF567
		public static Conversation Current
		{
			get
			{
				return ConversationUI.CurrentConversation;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06007A9F RID: 31391 RVA: 0x002E136E File Offset: 0x002DF56E
		public static GameObject Speaker
		{
			get
			{
				return ConversationUI.Speaker;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06007AA0 RID: 31392 RVA: 0x002E1375 File Offset: 0x002DF575
		public static GameObject Listener
		{
			get
			{
				return ConversationUI.Listener;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06007AA1 RID: 31393 RVA: 0x002E137C File Offset: 0x002DF57C
		public static GameObject Transmitter
		{
			get
			{
				return ConversationUI.Transmitter;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06007AA2 RID: 31394 RVA: 0x002E1383 File Offset: 0x002DF583
		public static GameObject Receiver
		{
			get
			{
				return ConversationUI.Receiver;
			}
		}

		// Token: 0x06007AA3 RID: 31395 RVA: 0x002E138A File Offset: 0x002DF58A
		public Conversation()
		{
		}

		// Token: 0x06007AA4 RID: 31396 RVA: 0x002E139D File Offset: 0x002DF59D
		public Conversation(ConversationXMLBlueprint Blueprint)
		{
			this.Load(Blueprint);
		}

		/// <summary>Retrieve or set state associated with specified key.</summary>
		/// <param name="Key">A nullable string.</param>
		/// <returns><c>null</c> if Key not found.</returns>
		// Token: 0x170009B7 RID: 2487
		public object this[string Key]
		{
			get
			{
				object obj;
				if (Key == null || !this.State.TryGetValue(Key, out obj))
				{
					return null;
				}
				return obj;
			}
			set
			{
				this.State[Key] = value;
			}
		}

		/// <summary>Retrieve and cast state associated with specified key.</summary>
		// Token: 0x06007AA7 RID: 31399 RVA: 0x002E13EC File Offset: 0x002DF5EC
		public bool TryGetState<T>(string Key, out T Value)
		{
			object obj;
			if (Key != null && this.State.TryGetValue(Key, out obj))
			{
				Value = (T)((object)obj);
				return true;
			}
			Value = default(T);
			return false;
		}

		// Token: 0x06007AA8 RID: 31400 RVA: 0x002E1422 File Offset: 0x002DF622
		public bool HasState(string Key)
		{
			return Key != null && this.State.ContainsKey(Key);
		}

		// Token: 0x06007AA9 RID: 31401 RVA: 0x002E1438 File Offset: 0x002DF638
		public bool HasDelimitedState(string Key, char Separator, string Value)
		{
			object obj;
			if (this.State.TryGetValue(Key, out obj))
			{
				string text = obj as string;
				if (text != null)
				{
					return text.HasDelimitedSubstring(Separator, Value, StringComparison.Ordinal);
				}
			}
			return false;
		}

		// Token: 0x06007AAA RID: 31402 RVA: 0x002E146A File Offset: 0x002DF66A
		public void SetState(string Key, object Value)
		{
			this.State[Key] = Value;
		}

		// Token: 0x06007AAB RID: 31403 RVA: 0x002E1479 File Offset: 0x002DF679
		public bool RemoveState(string Key)
		{
			return Key != null && this.State.Remove(Key);
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06007AAC RID: 31404 RVA: 0x002E148C File Offset: 0x002DF68C
		public override int Propagation
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06007AAD RID: 31405 RVA: 0x002E1490 File Offset: 0x002DF690
		public override bool LoadChild(ConversationXMLBlueprint Blueprint)
		{
			if (Blueprint.Name == "Start" || (Blueprint.ID == "Start" && Blueprint.Name == "Node"))
			{
				if (this.Elements == null)
				{
					this.Elements = new List<IConversationElement>();
				}
				if (this.Starts == null)
				{
					this.Starts = new List<Node>();
				}
				Node node = base.Create<Node>(Blueprint);
				this.Elements.Add(node);
				this.Starts.Add(node);
				return true;
			}
			return base.LoadChild(Blueprint);
		}

		// Token: 0x06007AAE RID: 31406 RVA: 0x002E1524 File Offset: 0x002DF724
		public Node GetStart(string ID = null)
		{
			if (ID != null && !this.Elements.IsNullOrEmpty<IConversationElement>())
			{
				Node node = this.Elements.FirstOrDefault<IConversationElement>((IConversationElement x) => x.ID == ID) as Node;
				if (node != null)
				{
					return node;
				}
			}
			if (this.Starts.IsNullOrEmpty<Node>())
			{
				return null;
			}
			this.Starts.ForEach(delegate(Node x)
			{
				x.Awake();
			});
			Algorithms.StableSortInPlace<Node>(this.Starts);
			return this.Starts.FirstOrDefault<Node>((Node x) => x.IsVisible());
		}

		// Token: 0x040037ED RID: 14317
		[ModSensitiveStaticCache(false)]
		public static Dictionary<string, ConversationXMLBlueprint> _Blueprints;

		// Token: 0x040037EE RID: 14318
		public List<Node> Starts;

		// Token: 0x040037EF RID: 14319
		public Dictionary<string, object> State = new Dictionary<string, object>();
	}
}
