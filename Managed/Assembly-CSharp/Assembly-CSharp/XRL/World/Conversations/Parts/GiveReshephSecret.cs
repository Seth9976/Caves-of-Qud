using System;
using System.Collections.Generic;
using System.Linq;
using Qud.API;
using XRL.Annals;
using XRL.UI;

namespace XRL.World.Conversations.Parts
{
	/// <summary>Share a secret from Resheph's life to gain some XP.</summary>
	// Token: 0x02000BFB RID: 3067
	public class GiveReshephSecret : IConversationPart
	{
		// Token: 0x06007B49 RID: 31561 RVA: 0x002E52E3 File Offset: 0x002E34E3
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnterElementEvent.ID || ID == HideElementEvent.ID;
		}

		// Token: 0x06007B4A RID: 31562 RVA: 0x002E5309 File Offset: 0x002E3509
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = "{{g|[Share secrets from Resheph's life]}}";
			return false;
		}

		// Token: 0x06007B4B RID: 31563 RVA: 0x002E5317 File Offset: 0x002E3517
		public static bool IsShared(JournalSultanNote N)
		{
			return The.Game.StringGameState.ContainsKey("soldcultist_" + N.EventID.ToString());
		}

		// Token: 0x06007B4C RID: 31564 RVA: 0x002E5340 File Offset: 0x002E3540
		public override bool HandleEvent(EnterElementEvent E)
		{
			List<JournalSultanNote> knownNotesForResheph = JournalAPI.GetKnownNotesForResheph(new Predicate<JournalSultanNote>(GiveReshephSecret.IsShared));
			List<JournalSultanNote> knownNotesForResheph2 = JournalAPI.GetKnownNotesForResheph((JournalSultanNote x) => !GiveReshephSecret.IsShared(x));
			if (knownNotesForResheph2.Count == 0)
			{
				Popup.Show("You do not have any unshared secrets about the life of Resheph.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			string[] array = knownNotesForResheph2.Select<JournalSultanNote, string>((JournalSultanNote g) => g.Text).ToArray<string>();
			List<ValueTuple<int, int>> list = Popup.PickSeveral("Choose secrets about the life of Resheph to share.", null, "", "Sounds/UI/ui_notification", array, null, null, null, The.Speaker, null, null, -1, 1, 60, 0, -1, false, true, false, true, false);
			if (list.IsNullOrEmpty<ValueTuple<int, int>>())
			{
				return false;
			}
			int num = 0;
			int num2 = knownNotesForResheph.Count + 1;
			foreach (ValueTuple<int, int> valueTuple in list)
			{
				num += QudHistoryHelpers.GetReshephGospelXP(num2++);
				The.Game.SetStringGameState("soldcultist_" + knownNotesForResheph2[valueTuple.Item1].EventID.ToString(), "1");
			}
			Popup.Show(string.Concat(new string[]
			{
				"You muse over the secret",
				(list.Count > 1) ? "s" : "",
				" with ",
				The.Speaker.ShortDisplayNameWithoutTitlesStripped,
				" and gain some insight."
			}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
			Popup.Show("You gain {{C|" + num.ToString() + "}} XP.", null, "Sounds/UI/ui_notification", true, true, true, false, null);
			The.Player.AwardXP(num, -1, 0, int.MaxValue, null, The.Speaker, null, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06007B4D RID: 31565 RVA: 0x002E5534 File Offset: 0x002E3734
		public override bool HandleEvent(HideElementEvent E)
		{
			return false;
		}
	}
}
