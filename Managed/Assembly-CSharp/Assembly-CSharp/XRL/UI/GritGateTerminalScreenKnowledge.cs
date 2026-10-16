using System;
using Qud.API;
using XRL.Core;
using XRL.Language;

namespace XRL.UI
{
	// Token: 0x02001498 RID: 5272
	public class GritGateTerminalScreenKnowledge : GritGateTerminalScreen
	{
		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x0600CF22 RID: 53026 RVA: 0x004B6836 File Offset: 0x004B4A36
		// (set) Token: 0x0600CF23 RID: 53027 RVA: 0x004B684C File Offset: 0x004B4A4C
		private bool bSecretRevealed
		{
			get
			{
				return XRLCore.Core.Game.HasIntGameState("EreshkigalSecret");
			}
			set
			{
				if (value)
				{
					XRLCore.Core.Game.SetIntGameState("EreshkigalSecret", 1);
					return;
				}
				XRLCore.Core.Game.IntGameState.Remove("EreshkigalSecret");
			}
		}

		// Token: 0x0600CF24 RID: 53028 RVA: 0x004B6884 File Offset: 0x004B4A84
		public GritGateTerminalScreenKnowledge()
		{
			if (this.bSecretRevealed)
			{
				this.MainText = "I've done as you asked. Your repetition is ungainly.";
				this.Options.Add("Are you sapient?");
				this.Options.Add("What is the Thin World?");
				this.Options.Add("Exit.");
				return;
			}
			this.MainText = "It is done.";
			this.Options.Add("Thank you, Ereshkigal.");
			this.optionActions.Add(delegate
			{
				this.bSecretRevealed = true;
				IBaseJournalEntry randomUnrevealedNote = JournalAPI.GetRandomUnrevealedNote(null);
				string text;
				if (randomUnrevealedNote is JournalMapNote)
				{
					text = "The location of " + Grammar.InitLowerIfArticle(randomUnrevealedNote.Text);
				}
				else
				{
					text = randomUnrevealedNote.Text;
				}
				Popup.Show("Ereshkigal delivers insight from the Thin World:\n\n" + text, null, "Sounds/UI/ui_notification", true, true, true, true, null);
				randomUnrevealedNote.Reveal("Ereshkigal", false);
				base.Terminal.CurrentScreen = null;
			});
		}

		// Token: 0x0600CF25 RID: 53029 RVA: 0x004B690D File Offset: 0x004B4B0D
		public override void Back()
		{
			base.Terminal.CurrentScreen = null;
		}

		// Token: 0x0600CF26 RID: 53030 RVA: 0x004B691C File Offset: 0x004B4B1C
		public override void Activate()
		{
			if (this.bSecretRevealed)
			{
				if (base.Terminal.Selected == 0)
				{
					base.Terminal.CurrentScreen = new GritGateTerminalScreenWhoAreYou();
				}
				if (base.Terminal.Selected == 1)
				{
					base.Terminal.CurrentScreen = new GritGateTerminalScreenThinWorld();
				}
				if (base.Terminal.Selected == 2)
				{
					base.Terminal.CurrentScreen = null;
					return;
				}
			}
			else
			{
				this.optionActions[base.Terminal.Selected]();
			}
		}
	}
}
