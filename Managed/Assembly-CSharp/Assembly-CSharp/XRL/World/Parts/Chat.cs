using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x02000F9A RID: 3994
	[Serializable]
	public class Chat : IPart
	{
		// Token: 0x06009E8D RID: 40589 RVA: 0x00396EF3 File Offset: 0x003950F3
		public Chat()
		{
		}

		// Token: 0x06009E8E RID: 40590 RVA: 0x00396F06 File Offset: 0x00395106
		public Chat(string Says)
			: this()
		{
			this.Says = Says;
		}

		// Token: 0x06009E8F RID: 40591 RVA: 0x00396F15 File Offset: 0x00395115
		public Chat(string Says, bool ShowInShortDescription)
			: this(Says)
		{
			this.ShowInShortDescription = ShowInShortDescription;
		}

		// Token: 0x06009E90 RID: 40592 RVA: 0x00396F28 File Offset: 0x00395128
		public override bool SameAs(IPart p)
		{
			Chat chat = p as Chat;
			return !(chat.Says != this.Says) && chat.ShowInShortDescription == this.ShowInShortDescription;
		}

		// Token: 0x06009E91 RID: 40593 RVA: 0x00396F62 File Offset: 0x00395162
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || (ID == GetShortDescriptionEvent.ID && this.ShowInShortDescription);
		}

		// Token: 0x06009E92 RID: 40594 RVA: 0x00396F90 File Offset: 0x00395190
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return E.Actor.IsPlayerControlled() && base.HandleEvent(E);
		}

		// Token: 0x06009E93 RID: 40595 RVA: 0x00396FA8 File Offset: 0x003951A8
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			if (!E.Actor.IsPlayer())
			{
				this.PerformChat(E.Actor, false);
				return false;
			}
			if (!this.ParentObject.IsPlayerLed())
			{
				this.PerformChat(E.Actor, true);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E94 RID: 40596 RVA: 0x00396FF4 File Offset: 0x003951F4
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ShowInShortDescription)
			{
				string text = GameText.VariableReplace(this.Says, this.ParentObject, null, false);
				if (!text.IsNullOrEmpty())
				{
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					stringBuilder.Append(E.Infix);
					int length = stringBuilder.Length;
					if (text[0] == '*')
					{
						stringBuilder.Append('\n').Append(text.Substring(1));
					}
					else if (text[0] == '@')
					{
						foreach (string text2 in text.Substring(1).Split('~', StringSplitOptions.None))
						{
							if (text2[0] == '[')
							{
								stringBuilder.Append('\n').Append(this.ParentObject.It).Append(this.ParentObject.GetVerb("bear", true, false, false, true, false))
									.Append(' ')
									.Append(text2.Replace("[", "").Replace("]", ""));
							}
							else
							{
								stringBuilder.Append('\n').Append(this.ParentObject.It).Append(this.ParentObject.GetVerb("say", true, false, false, true, false))
									.Append(", '")
									.Append(text2)
									.Append('\'');
								if (!text2.EndsWith(".") && !text2.EndsWith("!") && !text2.EndsWith("?"))
								{
									stringBuilder.Append('.');
								}
							}
						}
					}
					else
					{
						string text3 = (text.Contains("~") ? text.Split('~', StringSplitOptions.None).GetRandomElement<string>(null) : text);
						if (!text3.IsNullOrEmpty())
						{
							if (text3[0] == '[')
							{
								stringBuilder.Append('\n').Append(this.ParentObject.It).Append(this.ParentObject.GetVerb("bear", true, false, false, true, false))
									.Append(' ')
									.Append(text3.Replace("[", "").Replace("]", ""));
							}
							else
							{
								stringBuilder.Append('\n').Append(this.ParentObject.It).Append(this.ParentObject.GetVerb("read", true, false, false, true, false))
									.Append(", '")
									.Append(text3)
									.Append('\'');
								if (!text3.EndsWith(".") && !text3.EndsWith("!") && !text3.EndsWith("?"))
								{
									stringBuilder.Append('.');
								}
							}
							stringBuilder.Append('\n');
						}
					}
					if (stringBuilder.Length != length)
					{
						E.Infix.Clear().Append(stringBuilder);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E95 RID: 40597 RVA: 0x003972DA File Offset: 0x003954DA
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009E96 RID: 40598 RVA: 0x003972DD File Offset: 0x003954DD
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ObjectTalking");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009E97 RID: 40599 RVA: 0x003972F2 File Offset: 0x003954F2
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ObjectTalking")
			{
				this.PerformChat(IComponent<GameObject>.ThePlayer, true);
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009E98 RID: 40600 RVA: 0x0039731C File Offset: 0x0039551C
		public void PerformChat(GameObject who, bool Dialog)
		{
			string text = GameText.VariableReplace(this.Says, this.ParentObject, null, false);
			if (!text.IsNullOrEmpty() && (this.ParentObject.Brain == null || ConversationScript.IsPhysicalConversationPossible(this.ParentObject, true, false, false, 0)))
			{
				GameObject gameObject = who ?? this.ParentObject;
				this.ParentObject.PlayWorldSoundTag("ChatSound", "sfx_interact_chat", gameObject.CurrentCell, 0.5f);
				if (text[0] == '*')
				{
					IComponent<GameObject>.EmitMessage(gameObject, text.Substring(1), ' ', Dialog, false, false, null, null);
				}
				else if (text[0] == '@')
				{
					foreach (string text2 in text.Substring(1).Split('~', StringSplitOptions.None))
					{
						if (text2[0] == '[')
						{
							IComponent<GameObject>.EmitMessage(gameObject, text2.Replace("[", "").Replace("]", ""), ' ', Dialog, false, false, null, null);
						}
						else
						{
							IComponent<GameObject>.EmitMessage(gameObject, this.ParentObject.Does("say", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + ", '{{|" + text2 + "}}'.", ' ', Dialog, false, false, null, null);
						}
					}
				}
				else
				{
					string text3 = (text.Contains("~") ? text.Split('~', StringSplitOptions.None).GetRandomElement<string>(null) : text);
					if (!text3.IsNullOrEmpty())
					{
						if (text3[0] == '[')
						{
							IComponent<GameObject>.EmitMessage(gameObject, text3.Replace("[", "").Replace("]", ""), ' ', Dialog, false, false, null, null);
						}
						else
						{
							IComponent<GameObject>.EmitMessage(gameObject, this.ParentObject.Does("say", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + ", '{{|" + text3 + "}}'.", ' ', Dialog, false, false, null, null);
						}
					}
				}
				if (who.IsPlayer())
				{
					this.ParentObject.FireEvent("ChattingWithPlayer");
				}
			}
		}

		// Token: 0x06009E99 RID: 40601 RVA: 0x00397551 File Offset: 0x00395751
		public void PerformChat(bool Dialog)
		{
			this.PerformChat(null, Dialog);
		}

		// Token: 0x04004193 RID: 16787
		public string Says = "Hi!";

		// Token: 0x04004194 RID: 16788
		public bool ShowInShortDescription;
	}
}
