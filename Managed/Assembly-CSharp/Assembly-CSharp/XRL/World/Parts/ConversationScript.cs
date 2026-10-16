using System;
using ConsoleLib.Console;
using Qud.API;
using XRL.Core;
using XRL.UI;
using XRL.World.Conversations;
using XRL.World.Conversations.Parts;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000E43 RID: 3651
	[Serializable]
	public class ConversationScript : IPart
	{
		// Token: 0x060093F8 RID: 37880 RVA: 0x00354161 File Offset: 0x00352361
		public ConversationScript()
		{
		}

		// Token: 0x060093F9 RID: 37881 RVA: 0x00354169 File Offset: 0x00352369
		public ConversationScript(string ConversationID)
			: this()
		{
			this.ConversationID = ConversationID;
		}

		// Token: 0x060093FA RID: 37882 RVA: 0x00354178 File Offset: 0x00352378
		public ConversationScript(string ConversationID, bool ClearLost)
			: this(ConversationID)
		{
			this.ClearLost = ClearLost;
		}

		// Token: 0x060093FB RID: 37883 RVA: 0x00354188 File Offset: 0x00352388
		public override bool SameAs(IPart p)
		{
			ConversationScript conversationScript = p as ConversationScript;
			return !(conversationScript.ConversationID != this.ConversationID) && !(conversationScript.Quest != this.Quest) && !(conversationScript.PreQuestConversationID != this.PreQuestConversationID) && !(conversationScript.PostQuestConversationID != this.PostQuestConversationID) && conversationScript.ClearLost == this.ClearLost && conversationScript.ChargeUse == this.ChargeUse && !(conversationScript.Filter != this.Filter) && !(conversationScript.FilterExtras != this.FilterExtras) && !(conversationScript.Color != this.Color) && !(conversationScript.Append != this.Append) && base.SameAs(p);
		}

		// Token: 0x060093FC RID: 37884 RVA: 0x0035426C File Offset: 0x0035246C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeginConversationEvent.ID || (ID == CanGiveDirectionsEvent.ID && this.ClearLost) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x060093FD RID: 37885 RVA: 0x003542C8 File Offset: 0x003524C8
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "RecordConversationAsProperty", this.RecordConversationAsProperty);
			E.AddEntry(this, "ConversationID", this.ConversationID);
			E.AddEntry(this, "Quest", this.Quest);
			E.AddEntry(this, "PreQuestConversationID", this.PreQuestConversationID);
			E.AddEntry(this, "InQuestConversationID", this.InQuestConversationID);
			E.AddEntry(this, "PostQuestConversationID", this.PostQuestConversationID);
			E.AddEntry(this, "ClearLost", this.ClearLost);
			E.AddEntry(this, "ChargeUse", this.ChargeUse);
			E.AddEntry(this, "Filter", this.Filter);
			E.AddEntry(this, "FilterExtras", this.FilterExtras);
			E.AddEntry(this, "Color", this.Color);
			E.AddEntry(this, "Append", this.Append);
			return base.HandleEvent(E);
		}

		// Token: 0x060093FE RID: 37886 RVA: 0x003543B4 File Offset: 0x003525B4
		public override bool HandleEvent(BeginConversationEvent E)
		{
			if (E.SpeakingWith == this.ParentObject)
			{
				if (this.ChargeUse > 0)
				{
					this.ParentObject.UseCharge(this.ChargeUse, false, 0L, true, true, null);
				}
				if (this.RecordConversationAsProperty)
				{
					E.Actor.ModIntProperty(this.ParentObject.Blueprint + "Chat", 1, false);
				}
				if (!this.Filter.IsNullOrEmpty())
				{
					Conversation conversation = The.Conversation;
					if (conversation != null)
					{
						conversation.AddPart(new TextFilter(this.Filter, this.FilterExtras), true);
					}
				}
				if (!this.Append.IsNullOrEmpty())
				{
					Conversation conversation2 = The.Conversation;
					if (conversation2 != null)
					{
						conversation2.AddPart(new TextInsert
						{
							Text = this.Append
						}, true);
					}
				}
				int intProperty = E.Actor.GetIntProperty("PronounSetTick", 0);
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				this.ParentObject.ModIntProperty("ConversationCount", 1, false);
				if (PronounSet.EnableConversationalExchange && this.ParentObject.Brain != null && !this.ParentObject.HasProperty("FugueCopy"))
				{
					if (E.Actor.GetPronounSet() != null)
					{
						if (this.ParentObject.GetIntProperty("ConversationCount", 0) == 1)
						{
							flag2 = true;
						}
						else if (this.ParentObject.GetIntProperty("KnowsPlayerPronounsAsOfTick", 0) < intProperty)
						{
							flag2 = true;
							flag3 = true;
						}
						this.ParentObject.SetIntProperty("KnowsPlayerPronounsAsOfTick", intProperty, false);
					}
					if (this.ParentObject.GetPronounSet() != null && !this.ParentObject.IsPronounSetKnown())
					{
						this.ParentObject.SetPronounSetKnown(true);
						flag = true;
					}
				}
				string text = ConversationScript.PronounExchangeDescription(E.Actor, this.ParentObject, flag, flag2, flag3);
				if (text != null)
				{
					string text2 = "[" + ColorUtility.CapitalizeExceptFormatting(text) + ".]\n\n";
					Node startNode = ConversationUI.StartNode;
					if (startNode != null)
					{
						startNode.AddPart(new TextInsert
						{
							Text = text2,
							Prepend = true,
							Spoken = false
						}, true);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060093FF RID: 37887 RVA: 0x003545B9 File Offset: 0x003527B9
		public override bool HandleEvent(CanGiveDirectionsEvent E)
		{
			if (E.SpeakingWith == this.ParentObject && this.ClearLost && !E.PlayerCompanion && !E.SpeakingWith.HasEffect<Lost>())
			{
				E.CanGiveDirections = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009400 RID: 37888 RVA: 0x003545F4 File Offset: 0x003527F4
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.ParentObject != E.Actor)
			{
				E.AddAction("Chat", "chat", "Chat", null, 'h', false, 10, 0, false, false, false, true, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009401 RID: 37889 RVA: 0x0035463C File Offset: 0x0035283C
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Chat" && ConversationScript.AttemptConversation(E.Actor, this.ParentObject, null, null, this.GetActiveConversationBlueprint(), 0, false, this.Blueprint != null, null, E))
			{
				E.RequestInterfaceExit();
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009402 RID: 37890 RVA: 0x0035469C File Offset: 0x0035289C
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return (!E.Actor.IsPlayer() || (this.ParentObject.IsPlayerLed() && this.ParentObject.IsMobile()) || !this.ParentObject.FireEvent(ConversationScript.eCanHaveSmartUseConversation)) && base.HandleEvent(E);
		}

		// Token: 0x06009403 RID: 37891 RVA: 0x003546EC File Offset: 0x003528EC
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			return (!E.Actor.IsPlayer() || (this.ParentObject.IsPlayerLed() && this.ParentObject.IsMobile()) || !this.ParentObject.FireEvent(ConversationScript.eCanHaveSmartUseConversation) || !this.AttemptConversation(false, null, null)) && base.HandleEvent(E);
		}

		// Token: 0x06009404 RID: 37892 RVA: 0x0035474E File Offset: 0x0035294E
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009405 RID: 37893 RVA: 0x00354754 File Offset: 0x00352954
		public static bool IsPhysicalConversationPossible(GameObject Who, GameObject With, bool ShowPopup = true, bool AllowCombat = false, bool AllowFrozen = false, int ChargeUse = 0)
		{
			if (Who.HasEffect<Stun>() || Who.HasEffect<Exhausted>() || Who.HasEffect<Paralyzed>() || Who.HasEffect<Asleep>())
			{
				if (ShowPopup && Who.IsPlayer())
				{
					Popup.ShowFail("You are in no shape to start a conversation.", true, true, true);
				}
				return false;
			}
			if (Who.HasEffect<Confused>())
			{
				if (ShowPopup && Who.IsPlayer())
				{
					Popup.ShowFail("You can't seem to make out what " + With.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + With.Is + " saying.", true, true, true);
				}
				return false;
			}
			string text;
			if (!IsConversationallyResponsiveEvent.Check(With, Who, out text, true, false))
			{
				if (!string.IsNullOrEmpty(text) && ShowPopup && Who.IsPlayer())
				{
					Popup.ShowFail(text, true, true, true);
				}
				return false;
			}
			if (ChargeUse > 0 && !With.TestCharge(ChargeUse, false, 0L, true, true))
			{
				if (ShowPopup && Who.IsPlayer())
				{
					Popup.ShowFail(With.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + With.Is + " utterly unresponsive.", true, true, true);
				}
				return false;
			}
			if (!AllowFrozen && With.IsFrozen())
			{
				if (ShowPopup && Who.IsPlayer())
				{
					Popup.ShowFail("You hear a muffled grunting coming from inside the block of ice.", true, true, true);
				}
				return false;
			}
			if (!AllowCombat && With.IsHostileTowards(Who))
			{
				if (ShowPopup && Who.IsPlayer())
				{
					Popup.ShowFail(With.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + With.GetVerb("refuse", true, false, false, true, false) + " to speak to you.", true, true, true);
				}
				return false;
			}
			if (!AllowCombat && With.IsEngagedInMelee())
			{
				if (ShowPopup && Who.IsPlayer())
				{
					Popup.ShowFail(With.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " engaged in hand-to-hand combat and" + With.Is + " too busy to have a conversation with you.", true, true, true);
				}
				return false;
			}
			if (!AllowCombat && With.IsAflame())
			{
				if (ShowPopup && Who.IsPlayer())
				{
					Popup.ShowFail(With.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " on fire and" + With.Is + " too busy to have a conversation with you.", true, true, true);
				}
				return false;
			}
			string text2;
			if (!CanStartConversationEvent.Check(Who, With, out text2, true, false))
			{
				if (ShowPopup && Who.IsPlayer())
				{
					if (!string.IsNullOrEmpty(text2))
					{
						Popup.ShowFail(text2, true, true, true);
					}
					else
					{
						Popup.ShowFail("You cannot seem to engage " + With.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " in conversation.", true, true, true);
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06009406 RID: 37894 RVA: 0x00354A2F File Offset: 0x00352C2F
		public static bool IsPhysicalConversationPossible(GameObject With, bool ShowPopup = false, bool AllowCombat = false, bool AllowFrozen = false, int ChargeUse = 0)
		{
			return ConversationScript.IsPhysicalConversationPossible(IComponent<GameObject>.ThePlayer, With, ShowPopup, AllowCombat, AllowFrozen, ChargeUse);
		}

		// Token: 0x06009407 RID: 37895 RVA: 0x00354A41 File Offset: 0x00352C41
		public bool IsPhysicalConversationPossible(bool ShowPopup = false, bool AllowCombat = false, bool AllowFrozen = false)
		{
			return ConversationScript.IsPhysicalConversationPossible(this.ParentObject, ShowPopup, AllowCombat, AllowFrozen, this.ChargeUse);
		}

		// Token: 0x06009408 RID: 37896 RVA: 0x00354A58 File Offset: 0x00352C58
		public static bool IsMentalConversationPossible(GameObject Who, GameObject With, bool ShowPopup = true, bool AllowCombat = false, int ChargeUse = 0)
		{
			if (!Who.HasPart<Telepathy>())
			{
				return false;
			}
			if (!Who.CanMakeTelepathicContactWith(With) || (ChargeUse > 0 && !With.TestCharge(ChargeUse, false, 0L, true, true)))
			{
				if (ShowPopup && Who.IsPlayer())
				{
					Popup.ShowFail("You can sense nothing from " + With.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", true, true, true);
				}
				return false;
			}
			string text;
			if (!IsConversationallyResponsiveEvent.Check(With, Who, out text, false, true))
			{
				if (!string.IsNullOrEmpty(text) && ShowPopup && Who.IsPlayer())
				{
					Popup.ShowFail(text, true, true, true);
				}
				return false;
			}
			if (!AllowCombat && With.IsHostileTowards(Who))
			{
				if (ShowPopup && Who.IsPlayer())
				{
					Popup.ShowFail("You sense only hostility from " + With.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", true, true, true);
				}
				return false;
			}
			string text2;
			if (!CanStartConversationEvent.Check(Who, With, out text2, false, true))
			{
				if (ShowPopup && Who.IsPlayer())
				{
					if (!string.IsNullOrEmpty(text2))
					{
						Popup.ShowFail(text2, true, true, true);
					}
					else
					{
						Popup.ShowFail("You cannot seem to make contact with " + With.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", true, true, true);
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06009409 RID: 37897 RVA: 0x00354BC1 File Offset: 0x00352DC1
		public static bool IsMentalConversationPossible(GameObject With, bool ShowPopup = false, bool AllowCombat = false, int ChargeUse = 0)
		{
			return ConversationScript.IsMentalConversationPossible(IComponent<GameObject>.ThePlayer, With, ShowPopup, AllowCombat, ChargeUse);
		}

		// Token: 0x0600940A RID: 37898 RVA: 0x00354BD1 File Offset: 0x00352DD1
		public bool IsMentalConversationPossible(bool ShowPopup = false, bool AllowCombat = false)
		{
			return ConversationScript.IsMentalConversationPossible(this.ParentObject, ShowPopup, AllowCombat, this.ChargeUse);
		}

		// Token: 0x0600940B RID: 37899 RVA: 0x00354BE8 File Offset: 0x00352DE8
		public string GetActiveConversationID()
		{
			if (!string.IsNullOrEmpty(this.Quest))
			{
				if (!string.IsNullOrEmpty(this.PostQuestConversationID) && XRLCore.Core.Game.FinishedQuest(this.Quest))
				{
					return this.PostQuestConversationID;
				}
				if (!string.IsNullOrEmpty(this.PreQuestConversationID) && !XRLCore.Core.Game.HasQuest(this.Quest))
				{
					return this.PreQuestConversationID;
				}
				if (!string.IsNullOrEmpty(this.InQuestConversationID) && XRLCore.Core.Game.HasQuest(this.Quest) && !XRLCore.Core.Game.FinishedQuest(this.Quest))
				{
					return this.InQuestConversationID;
				}
			}
			return this.ConversationID;
		}

		// Token: 0x0600940C RID: 37900 RVA: 0x00354CA4 File Offset: 0x00352EA4
		public ConversationXMLBlueprint GetActiveConversationBlueprint()
		{
			if (this.Blueprint != null)
			{
				return this.Blueprint;
			}
			string activeConversationID = this.GetActiveConversationID();
			ConversationXMLBlueprint conversationXMLBlueprint;
			if (Conversation.Blueprints.TryGetValue(activeConversationID, out conversationXMLBlueprint))
			{
				return conversationXMLBlueprint;
			}
			return null;
		}

		// Token: 0x0600940D RID: 37901 RVA: 0x00354CD9 File Offset: 0x00352ED9
		public static bool IsDistant(GameObject Listener, GameObject Speaker, GameObject Transmitter = null, GameObject Receiver = null)
		{
			return !Listener.InSameOrAdjacentCellTo(Speaker) && !Listener.InSameOrAdjacentCellTo(Transmitter) && !Speaker.InSameOrAdjacentCellTo(Receiver) && (Receiver == null || !Receiver.InSameOrAdjacentCellTo(Transmitter));
		}

		// Token: 0x0600940E RID: 37902 RVA: 0x00354D08 File Offset: 0x00352F08
		public static bool AttemptConversation(GameObject Listener, GameObject Speaker, GameObject Transmitter = null, GameObject Receiver = null, ConversationXMLBlueprint Blueprint = null, int ChargeUse = 0, bool Silent = false, bool Dynamic = false, bool? Mental = null, IEvent ParentEvent = null)
		{
			if (!GameObject.Validate(Listener) || !GameObject.Validate(Speaker) || Listener == Speaker)
			{
				return false;
			}
			if (!AttemptConversationEvent.Check(ref Speaker, ref Listener, ref Transmitter, ref Receiver, ref Blueprint, Silent))
			{
				return false;
			}
			if (Blueprint == null)
			{
				return !Silent && Listener.ShowFailure(Speaker.Does("have", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " nothing to say.");
			}
			if (Dynamic)
			{
				ConversationsAPI.AddDynamicShim(Blueprint);
			}
			try
			{
				bool flag = ConversationScript.IsDistant(Listener, Speaker, Transmitter, Receiver);
				if (Mental == null && flag)
				{
					Mental = new bool?(true);
				}
				bool? flag2 = Mental;
				bool flag3 = true;
				if (((flag2.GetValueOrDefault() == flag3) & (flag2 != null)) && !ConversationScript.IsMentalConversationPossible(Listener, Speaker, !Silent, false, ChargeUse))
				{
					return false;
				}
				bool flag4;
				if (flag || !ConversationScript.IsPhysicalConversationPossible(Listener, Speaker, !Silent, false, false, ChargeUse))
				{
					if (Mental == null)
					{
						if (!ConversationScript.IsMentalConversationPossible(Listener, Speaker, !Silent, false, ChargeUse))
						{
							return false;
						}
					}
					else
					{
						flag2 = Mental;
						flag4 = false;
						if ((flag2.GetValueOrDefault() == flag4) & (flag2 != null))
						{
							return false;
						}
					}
				}
				if (Speaker != null)
				{
					GameObject gameObject = Speaker;
					string text = "ChatSound";
					flag2 = Mental;
					flag4 = true;
					gameObject.PlayWorldSoundTag(text, ((flag2.GetValueOrDefault() == flag4) & (flag2 != null)) ? "sfx_interact_chat_telepathic" : "sfx_interact_chat", (Listener != null) ? Listener.CurrentCell : null, 0.5f);
				}
				ConversationXMLBlueprint conversationXMLBlueprint = Blueprint;
				GameObject gameObject2 = Speaker;
				GameObject gameObject3 = Listener;
				GameObject gameObject4 = Transmitter;
				GameObject gameObject5 = Receiver;
				bool flag5 = true;
				flag2 = Mental;
				flag4 = true;
				bool flag6 = !((flag2.GetValueOrDefault() == flag4) & (flag2 != null));
				flag2 = Mental;
				flag4 = true;
				ConversationUI.HaveConversation(conversationXMLBlueprint, gameObject2, gameObject3, gameObject4, gameObject5, flag5, flag6, (flag2.GetValueOrDefault() == flag4) & (flag2 != null));
			}
			finally
			{
				if (Dynamic)
				{
					ConversationsAPI.RemoveDynamicShim(Blueprint);
				}
			}
			return true;
		}

		// Token: 0x0600940F RID: 37903 RVA: 0x00354EEC File Offset: 0x003530EC
		public bool AttemptConversation(bool Silent = false, bool? Mental = null, IEvent ParentEvent = null)
		{
			return ConversationScript.AttemptConversation(The.Player, this.ParentObject, null, null, this.GetActiveConversationBlueprint(), 0, Silent, this.Blueprint != null, Mental, ParentEvent);
		}

		// Token: 0x06009410 RID: 37904 RVA: 0x00354F20 File Offset: 0x00353120
		public static string PronounExchangeDescription(GameObject Player, GameObject Speaker, bool SpeakerGivePronouns, bool SpeakerGetPronouns, bool SpeakerGetNewPronouns)
		{
			if (Speaker.Brain == null)
			{
				return null;
			}
			if (Speaker.HasProperty("FugueCopy"))
			{
				return null;
			}
			if (SpeakerGivePronouns && SpeakerGetPronouns)
			{
				return string.Concat(new string[]
				{
					"you and ",
					Speaker.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
					" exchange pronouns; ",
					Speaker.its,
					" are ",
					Speaker.GetPronounSet().GetShortName()
				});
			}
			if (SpeakerGivePronouns)
			{
				return string.Concat(new string[]
				{
					Speaker.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
					Speaker.GetVerb("give", true, false, false, true, false),
					" you ",
					Speaker.its,
					" pronouns, which are ",
					Speaker.GetPronounSet().GetShortName()
				});
			}
			if (SpeakerGetNewPronouns)
			{
				return "you give " + Speaker.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " your new pronouns";
			}
			if (SpeakerGetPronouns)
			{
				return "you give " + Speaker.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " your pronouns";
			}
			return null;
		}

		// Token: 0x04003D34 RID: 15668
		public ConversationXMLBlueprint Blueprint;

		// Token: 0x04003D35 RID: 15669
		public bool RecordConversationAsProperty;

		// Token: 0x04003D36 RID: 15670
		public string ConversationID;

		// Token: 0x04003D37 RID: 15671
		public string Quest;

		// Token: 0x04003D38 RID: 15672
		public string PreQuestConversationID;

		// Token: 0x04003D39 RID: 15673
		public string InQuestConversationID;

		// Token: 0x04003D3A RID: 15674
		public string PostQuestConversationID;

		// Token: 0x04003D3B RID: 15675
		public bool ClearLost;

		// Token: 0x04003D3C RID: 15676
		public int ChargeUse;

		// Token: 0x04003D3D RID: 15677
		public string Filter;

		// Token: 0x04003D3E RID: 15678
		public string FilterExtras;

		// Token: 0x04003D3F RID: 15679
		public string Color;

		// Token: 0x04003D40 RID: 15680
		public string Append;

		// Token: 0x04003D41 RID: 15681
		[NonSerialized]
		private static Event eCanHaveSmartUseConversation = new Event("CanHaveSmartUseConversation");
	}
}
