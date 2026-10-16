using System;
using System.Collections.Generic;
using System.Linq;
using Genkit;

namespace XRL.World.Conversations
{
	// Token: 0x02000BE8 RID: 3048
	[HasGameBasedStaticCache]
	public class Choice : IConversationElement
	{
		// Token: 0x06007A91 RID: 31377 RVA: 0x002E117C File Offset: 0x002DF37C
		[GameBasedCacheInit]
		public static void ResetHashes()
		{
			Choice.Hashes.Clear();
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06007A92 RID: 31378 RVA: 0x002E1188 File Offset: 0x002DF388
		public override int Propagation
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06007A93 RID: 31379 RVA: 0x002E118C File Offset: 0x002DF38C
		public ulong Hash
		{
			get
			{
				if (this._Hash > 0UL)
				{
					return this._Hash;
				}
				IConversationElement conversationElement = this.Parent;
				while (conversationElement.Parent != null)
				{
					conversationElement = conversationElement.Parent;
				}
				this._Hash = global::Genkit.Hash.FNV1A64(conversationElement.ID ?? "[Unknown]", 14695981039346656037UL);
				this._Hash = global::Genkit.Hash.FNV1A64(this.ID, this._Hash);
				List<ConversationText> texts = this.Texts;
				string text;
				if (texts == null)
				{
					text = null;
				}
				else
				{
					ConversationText conversationText = texts.FirstOrDefault<ConversationText>();
					text = ((conversationText != null) ? conversationText.Text : null);
				}
				string text2;
				if ((text2 = text) == null)
				{
					text2 = this.Text ?? "[Empty]";
				}
				this._Hash = global::Genkit.Hash.FNV1A64(text2, this._Hash);
				return this._Hash;
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06007A94 RID: 31380 RVA: 0x002E1244 File Offset: 0x002DF444
		public bool Visited
		{
			get
			{
				return Choice.Hashes.Contains(this.Hash);
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06007A95 RID: 31381 RVA: 0x002E1256 File Offset: 0x002DF456
		// (set) Token: 0x06007A96 RID: 31382 RVA: 0x002E125E File Offset: 0x002DF45E
		public string Target
		{
			get
			{
				return this._Target;
			}
			set
			{
				this._Target = value;
				if (value == "End")
				{
					this.Priority -= 999999;
				}
			}
		}

		// Token: 0x06007A97 RID: 31383 RVA: 0x002E1286 File Offset: 0x002DF486
		public override void Entered()
		{
			base.Entered();
			if (!this.Transient)
			{
				Choice.Hashes.Add(this.Hash);
			}
		}

		// Token: 0x06007A98 RID: 31384 RVA: 0x002E12A8 File Offset: 0x002DF4A8
		public override string GetTextColor()
		{
			string text = "G";
			if (this.Visited)
			{
				text = "g";
			}
			ColorTextEvent.Send(this, ref text);
			return text;
		}

		// Token: 0x06007A99 RID: 31385 RVA: 0x002E12D4 File Offset: 0x002DF4D4
		public override string GetDisplayText(bool WithColor = false)
		{
			string text = base.GetDisplayText(WithColor);
			string tag = this.GetTag();
			if (!tag.IsNullOrEmpty())
			{
				text = text + " " + tag;
			}
			return text;
		}

		// Token: 0x06007A9A RID: 31386 RVA: 0x002E1308 File Offset: 0x002DF508
		public string GetTag()
		{
			string text = GetChoiceTagEvent.For(this, null);
			if (!text.IsNullOrEmpty())
			{
				return text;
			}
			if (this.Target == "End")
			{
				return "{{K|[End]}}";
			}
			return null;
		}

		/// <summary>Will not store its hash among visited choices.</summary>
		// Token: 0x040037E9 RID: 14313
		public bool Transient;

		// Token: 0x040037EA RID: 14314
		public static FixedHashSet Hashes = new FixedHashSet();

		// Token: 0x040037EB RID: 14315
		private ulong _Hash;

		// Token: 0x040037EC RID: 14316
		private string _Target;
	}
}
