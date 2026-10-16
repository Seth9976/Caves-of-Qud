using System;
using XRL.Language;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000EF2 RID: 3826
	[Serializable]
	public class Preacher : IPart
	{
		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060098DE RID: 39134 RVA: 0x00377CE9 File Offset: 0x00375EE9
		// (set) Token: 0x060098DF RID: 39135 RVA: 0x00377CF1 File Offset: 0x00375EF1
		public string[] Lines
		{
			get
			{
				return this._Lines;
			}
			set
			{
				this._Lines = value;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (set) Token: 0x060098E0 RID: 39136 RVA: 0x00377CFA File Offset: 0x00375EFA
		public string Quotes
		{
			set
			{
				this._Lines = ((value != null) ? value.Split(";;", StringSplitOptions.None) : null);
			}
		}

		// Token: 0x060098E1 RID: 39137 RVA: 0x00377D14 File Offset: 0x00375F14
		public int GetCount()
		{
			if (!this._Lines.IsNullOrEmpty<string>())
			{
				return this._Lines.Length;
			}
			BookInfo bookInfo;
			if (!this.Book.IsNullOrEmpty() && BookUI.Books.TryGetValue(this.Book, out bookInfo))
			{
				return bookInfo.Pages.Count;
			}
			return 0;
		}

		// Token: 0x060098E2 RID: 39138 RVA: 0x00377D68 File Offset: 0x00375F68
		public string GetLineText()
		{
			if (!this._Lines.IsNullOrEmpty<string>())
			{
				return this._Lines[this.Line];
			}
			BookInfo bookInfo;
			if (!this.Book.IsNullOrEmpty() && BookUI.Books.TryGetValue(this.Book, out bookInfo))
			{
				return bookInfo.Pages[this.Line].FullText.Replace("\n", " ").Replace("  ", " ").Trim();
			}
			return null;
		}

		// Token: 0x060098E3 RID: 39139 RVA: 0x00377DEC File Offset: 0x00375FEC
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060098E4 RID: 39140 RVA: 0x00377DEF File Offset: 0x00375FEF
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060098E5 RID: 39141 RVA: 0x00377DF4 File Offset: 0x00375FF4
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			base.Write(Basis, Writer);
			string[] lines = this._Lines;
			int num = ((lines != null) ? lines.Length : 0);
			Writer.WriteOptimized(num);
			for (int i = 0; i < num; i++)
			{
				Writer.WriteOptimized(this._Lines[i]);
			}
		}

		// Token: 0x060098E6 RID: 39142 RVA: 0x00377E3C File Offset: 0x0037603C
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			base.Read(Basis, Reader);
			int num = Reader.ReadOptimizedInt32();
			if (num > 0)
			{
				this._Lines = new string[num];
				for (int i = 0; i < num; i++)
				{
					this._Lines[i] = Reader.ReadOptimizedString();
				}
			}
		}

		// Token: 0x060098E7 RID: 39143 RVA: 0x00377E82 File Offset: 0x00376082
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanSmartUse");
			Registrar.Register("CommandSmartUseEarly");
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x060098E8 RID: 39144 RVA: 0x00377EB0 File Offset: 0x003760B0
		public void PreacherHomily(GameObject who, bool Dialog)
		{
			if (!Dialog && !this.ParentObject.IsAudible(IComponent<GameObject>.ThePlayer, this.Volume))
			{
				return;
			}
			if (!this.ParentObject.FireEvent("CanPreach"))
			{
				return;
			}
			if (this.ParentObject.IsFrozen())
			{
				base.EmitMessage(this.Frozen, ' ', Dialog, false, false, null, null);
				return;
			}
			int count = this.GetCount();
			if (!this.inOrder || this.Line == -1)
			{
				this.Line = Stat.Random(0, count - 1);
			}
			string text = this.GetLineText();
			if (!text.IsNullOrEmpty())
			{
				text = text.StartReplace().AddObject(this.ParentObject, null).ToString();
				if (this.Filter != null)
				{
					text = TextFilters.Filter(text, this.Filter, this.FilterExtras, true);
				}
				string text2 = this.Prefix.StartReplace().AddObject(this.ParentObject, null).ToString();
				string text3 = this.Postfix.StartReplace().AddObject(this.ParentObject, null).ToString();
				IComponent<GameObject>.EmitMessage(who ?? this.ParentObject, text2 + text + text3, ' ', Dialog, false, false, null, null);
				if (text.EndsWith(".") && !text.EndsWith("..."))
				{
					text = text.Substring(0, text.Length - 1);
				}
				if (!Dialog)
				{
					this.ParentObject.ParticleText("{{W|'" + text + "'}}", true);
				}
				if (this.inOrder)
				{
					if (this.Line < count - 1)
					{
						this.Line++;
						return;
					}
					this.Line = 0;
				}
			}
		}

		// Token: 0x060098E9 RID: 39145 RVA: 0x00378045 File Offset: 0x00376245
		public void PreacherHomily(bool Dialog)
		{
			this.PreacherHomily(null, Dialog);
		}

		// Token: 0x060098EA RID: 39146 RVA: 0x00378050 File Offset: 0x00376250
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanSmartUse")
			{
				if (this.SmartUse && !this.ParentObject.IsPlayerLed() && ConversationScript.IsPhysicalConversationPossible(this.ParentObject, false, false, false, 0))
				{
					return false;
				}
			}
			else if (E.ID == "CommandSmartUseEarly")
			{
				if (this.SmartUse && ConversationScript.IsPhysicalConversationPossible(this.ParentObject, false, false, false, 0))
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("User");
					if (gameObjectParameter.IsPlayer())
					{
						if (!this.ParentObject.IsPlayerLed())
						{
							this.PreacherHomily(gameObjectParameter, true);
						}
					}
					else
					{
						this.PreacherHomily(false);
					}
				}
			}
			else if (E.ID == "BeginTakeAction" && this.ParentObject.InActiveZone())
			{
				this.LastTalk--;
				if (this.LastTalk < 0 && this.Chance.in100())
				{
					this.LastTalk = this.ChatWait;
					this.PreacherHomily(false);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003FA5 RID: 16293
		public int Volume = 20;

		// Token: 0x04003FA6 RID: 16294
		public int Chance = 8;

		// Token: 0x04003FA7 RID: 16295
		public int ChatWait = 350;

		// Token: 0x04003FA8 RID: 16296
		public string Range = "10";

		// Token: 0x04003FA9 RID: 16297
		public string Duration = "350";

		// Token: 0x04003FAA RID: 16298
		public string Book = "Quotes";

		// Token: 0x04003FAB RID: 16299
		public string Filter;

		// Token: 0x04003FAC RID: 16300
		public string FilterExtras;

		// Token: 0x04003FAD RID: 16301
		public string Prefix = "=subject.T= =verb:yell= {{W|'";

		// Token: 0x04003FAE RID: 16302
		public string Frozen = "You hear inaudible mumbling.";

		// Token: 0x04003FAF RID: 16303
		public string Postfix = "'}}";

		// Token: 0x04003FB0 RID: 16304
		public bool SmartUse = true;

		// Token: 0x04003FB1 RID: 16305
		public bool inOrder;

		// Token: 0x04003FB2 RID: 16306
		[NonSerialized]
		private int Line = -1;

		// Token: 0x04003FB3 RID: 16307
		public int LastTalk;

		// Token: 0x04003FB4 RID: 16308
		[NonSerialized]
		private string[] _Lines;
	}
}
