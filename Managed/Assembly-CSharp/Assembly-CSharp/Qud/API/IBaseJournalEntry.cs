using System;
using System.Collections.Generic;
using System.Text;
using XRL.Messages;
using XRL.UI;
using XRL.World;

namespace Qud.API
{
	// Token: 0x020003FF RID: 1023
	[Serializable]
	public class IBaseJournalEntry : IComposite
	{
		// Token: 0x0600265A RID: 9818 RVA: 0x000C5972 File Offset: 0x000C3B72
		public bool Has(string att)
		{
			return this.Attributes.Contains(att);
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x000C5980 File Offset: 0x000C3B80
		public bool TryGetAttribute(string Prefix, out string Value)
		{
			foreach (string text in this.Attributes)
			{
				if (text.StartsWith(Prefix))
				{
					Value = text.Substring(Prefix.Length);
					return true;
				}
			}
			Value = null;
			return false;
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x000C59F0 File Offset: 0x000C3BF0
		public virtual string GetShortText()
		{
			return this.Text;
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x000C59F8 File Offset: 0x000C3BF8
		public virtual string GetDisplayText()
		{
			if (this.History.Length > 0)
			{
				return this.Text + "\n" + this.History;
			}
			return this.Text;
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x000C5A25 File Offset: 0x000C3C25
		public virtual string GetShareText()
		{
			return this.GetShortText();
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x000C5A2D File Offset: 0x000C3C2D
		public virtual void AppendHistory(string Line)
		{
			if (this.History.Length > 0)
			{
				this.History += "\n";
			}
			this.History += Line;
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x000C5A65 File Offset: 0x000C3C65
		public virtual void Updated()
		{
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x000C5A67 File Offset: 0x000C3C67
		public virtual void Forget(bool fast = false)
		{
			if (!this.Revealed)
			{
				return;
			}
			if (!this.Forgettable())
			{
				return;
			}
			this.Revealed = false;
			SecretVisibilityChangedEvent.Send(this);
			this.Updated();
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000C5A8E File Offset: 0x000C3C8E
		public virtual void Reveal(string LearnedFrom = null, bool Silent = false)
		{
			if (this.Revealed)
			{
				return;
			}
			this.LearnedFrom = LearnedFrom;
			this.Revealed = true;
			SecretVisibilityChangedEvent.Send(this);
			this.Updated();
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x000C5AB3 File Offset: 0x000C3CB3
		public virtual bool Forgettable()
		{
			return true;
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x000C5AB6 File Offset: 0x000C3CB6
		public virtual bool CanSell()
		{
			return this.Tradable && this.Revealed;
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x000C5AC8 File Offset: 0x000C3CC8
		public virtual bool CanBuy()
		{
			return this.Tradable && !this.Revealed;
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06002666 RID: 9830 RVA: 0x000C5ADD File Offset: 0x000C3CDD
		public virtual bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x000C5AE0 File Offset: 0x000C3CE0
		public virtual void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.ID);
			Writer.WriteOptimized(this.History);
			Writer.WriteOptimized(this.Text);
			Writer.WriteOptimized(this.LearnedFrom);
			Writer.WriteOptimized(this.Weight);
			Writer.Write(this.Revealed);
			Writer.Write(this.Tradable);
			Writer.Write(this.Attributes);
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x000C5B50 File Offset: 0x000C3D50
		public virtual void Read(SerializationReader Reader)
		{
			this.ID = Reader.ReadOptimizedString();
			this.History = Reader.ReadOptimizedString();
			this.Text = Reader.ReadOptimizedString();
			this.LearnedFrom = Reader.ReadOptimizedString();
			this.Weight = Reader.ReadOptimizedInt32();
			this.Revealed = Reader.ReadBoolean();
			this.Tradable = Reader.ReadBoolean();
			this.Attributes = Reader.ReadList<string>();
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x000C5BBD File Offset: 0x000C3DBD
		public static void DisplayMessage(string Message, string Sound = null)
		{
			if (IBaseJournalEntry.NotedPrompt)
			{
				Popup.Show(Message, null, Sound, true, true, true, true, null);
				return;
			}
			SoundManager.PlayUISound(Sound, 1f, false, false, SoundRequest.SoundEffectType.None);
			MessageQueue.AddPlayerMessage(Message, null, true);
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600266A RID: 9834 RVA: 0x000C5BEA File Offset: 0x000C3DEA
		// (set) Token: 0x0600266B RID: 9835 RVA: 0x000C5BF2 File Offset: 0x000C3DF2
		[Obsolete("Use Revealed")]
		public bool revealed
		{
			get
			{
				return this.Revealed;
			}
			set
			{
				this.Revealed = value;
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600266C RID: 9836 RVA: 0x000C5BFB File Offset: 0x000C3DFB
		// (set) Token: 0x0600266D RID: 9837 RVA: 0x000C5C06 File Offset: 0x000C3E06
		[Obsolete("Use !Tradable")]
		public bool secretSold
		{
			get
			{
				return !this.Tradable;
			}
			set
			{
				this.Tradable = !value;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600266E RID: 9838 RVA: 0x000C5C12 File Offset: 0x000C3E12
		// (set) Token: 0x0600266F RID: 9839 RVA: 0x000C5C1A File Offset: 0x000C3E1A
		[Obsolete("Use ID")]
		public string secretid
		{
			get
			{
				return this.ID;
			}
			set
			{
				this.ID = value;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06002670 RID: 9840 RVA: 0x000C5C23 File Offset: 0x000C3E23
		// (set) Token: 0x06002671 RID: 9841 RVA: 0x000C5C2B File Offset: 0x000C3E2B
		[Obsolete("Use History")]
		public string history
		{
			get
			{
				return this.History;
			}
			set
			{
				this.History = value;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06002672 RID: 9842 RVA: 0x000C5C34 File Offset: 0x000C3E34
		// (set) Token: 0x06002673 RID: 9843 RVA: 0x000C5C3C File Offset: 0x000C3E3C
		[Obsolete("Use Text")]
		public string text
		{
			get
			{
				return this.Text;
			}
			set
			{
				this.Text = value;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06002674 RID: 9844 RVA: 0x000C5C45 File Offset: 0x000C3E45
		// (set) Token: 0x06002675 RID: 9845 RVA: 0x000C5C4D File Offset: 0x000C3E4D
		[Obsolete("Use Attributes")]
		public List<string> attributes
		{
			get
			{
				return this.Attributes;
			}
			set
			{
				this.Attributes = value;
			}
		}

		// Token: 0x0400131B RID: 4891
		public const int BASE_WEIGHT = 100;

		// Token: 0x0400131C RID: 4892
		protected static StringBuilder SB = new StringBuilder();

		// Token: 0x0400131D RID: 4893
		public string ID;

		// Token: 0x0400131E RID: 4894
		public string History = "";

		// Token: 0x0400131F RID: 4895
		public string Text;

		// Token: 0x04001320 RID: 4896
		public string LearnedFrom;

		// Token: 0x04001321 RID: 4897
		public int Weight = 100;

		// Token: 0x04001322 RID: 4898
		public bool Revealed;

		// Token: 0x04001323 RID: 4899
		public bool Tradable = true;

		// Token: 0x04001324 RID: 4900
		public List<string> Attributes = new List<string>();

		// Token: 0x04001325 RID: 4901
		[NonSerialized]
		public static bool NotedPrompt = true;
	}
}
