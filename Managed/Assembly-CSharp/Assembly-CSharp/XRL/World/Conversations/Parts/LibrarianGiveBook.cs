using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using Qud.API;
using XRL.Language;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C0C RID: 3084
	public class LibrarianGiveBook : IConversationPart
	{
		// Token: 0x06007BBB RID: 31675 RVA: 0x002E7AD2 File Offset: 0x002E5CD2
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnterElementEvent.ID || ID == HideElementEvent.ID;
		}

		// Token: 0x06007BBC RID: 31676 RVA: 0x002E7AF8 File Offset: 0x002E5CF8
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = "{{g|[Give Books]}}";
			return false;
		}

		// Token: 0x06007BBD RID: 31677 RVA: 0x002E7B08 File Offset: 0x002E5D08
		public static bool IsAwardableBook(GameObject Object)
		{
			return !Object.HasIntProperty("LibrarianAwarded") && (Object.HasTagOrIntProperty("LibrarianAwardable") || Object.HasPart<Book>() || Object.HasPart<VillageHistoryBook>() || Object.HasPart<MarkovBook>() || Object.HasPart<Cookbook>());
		}

		// Token: 0x06007BBE RID: 31678 RVA: 0x002E7B5C File Offset: 0x002E5D5C
		public override bool HandleEvent(EnterElementEvent E)
		{
			Inventory inventory = The.Player.Inventory;
			List<string> list = new List<string>();
			List<GameObject> list2 = new List<GameObject>();
			List<int> list3 = new List<int>();
			List<int> list4 = new List<int>();
			List<IRenderable> list5 = new List<IRenderable>();
			bool flag = false;
			foreach (GameObject gameObject in inventory.GetObjects(new Predicate<GameObject>(LibrarianGiveBook.IsAwardableBook)))
			{
				if (gameObject.IsMarkedImportantByPlayer())
				{
					flag = true;
				}
				else
				{
					double valueEach = gameObject.ValueEach;
					int num = (int)(valueEach * valueEach / 25.0);
					if (num > 0)
					{
						list2.Add(gameObject);
						list3.Add(num);
						list4.Add(gameObject.Count);
						list5.Add(gameObject.Render);
						list.Add(gameObject.GetDisplayName(1120, null, null, false, false, false, false, false, false, true, false, false, false, false, false, false, null, false, false, false, false, null, false, null, false, true) + " [{{C|" + num.ToString() + "}} XP]");
					}
				}
			}
			if (list2.Count == 0)
			{
				return The.Player.ShowFailure(flag ? "You only have books you've marked important. Unmark any you wish to donate." : "You have no books to give.");
			}
			List<ValueTuple<int, int>> list6 = Popup.PickSeveral("Choose books to give.", null, "", "Sounds/UI/ui_notification", list, HotkeySpread.get(new string[] { "Menus", "UINav" }), list4, list5, The.Speaker, null, null, -1, 0, 60, 0, -1, false, true, false, true, false);
			if (list6.IsNullOrEmpty<ValueTuple<int, int>>())
			{
				return false;
			}
			int num2 = 0;
			List<GameObject> list7 = new List<GameObject>(list6.Count);
			string text = The.Speaker.Does("provide", int.MaxValue, null, null, null, false, false, true, false, true, true, true, true, false, null, false, false, true, null, true, null, false);
			string text2 = The.Speaker.an(int.MaxValue, null, null, false, false, true, false, true, true, true, true, false, true, false, null, false, null, false);
			foreach (ValueTuple<int, int> valueTuple in list6)
			{
				GameObject gameObject2 = list2[valueTuple.Item1];
				int item = valueTuple.Item2;
				if (gameObject2.ConfirmUseImportant(The.Player, "donate", null, item))
				{
					gameObject2.SplitStack(item, The.Player, false);
					GameObject speaker = The.Speaker;
					if (speaker != null && speaker.ReceiveObject(gameObject2, false, null))
					{
						num2 += list3[valueTuple.Item1] * item;
						list7.Add(gameObject2);
						JournalAPI.AddAccomplishment(text2 + " provided you with insightful commentary on " + gameObject2.ShortDisplayNameSingleStripped + ".", string.Concat(new string[]
						{
							"Remember the kindness of =name=, who patiently taught ",
							gameObject2.ShortDisplayNameSingleStripped,
							" to ",
							The.Player.GetPronounProvider(false).PossessiveAdjective,
							" simple pupil, ",
							text2,
							"."
						}), string.Concat(new string[]
						{
							"At a remote library near the Six Day Stilt, =name= met with the ",
							The.Speaker.GetCreatureType(false, false),
							" librarian  Sheba Hagadias and together they wrote an exegesis on the book ",
							gameObject2.ShortDisplayNameSingleStripped,
							"."
						}), "DonateBook", "general", MuralCategory.LearnsSecret, MuralWeight.Low, null, -1L, true);
						gameObject2.SetIntProperty("LibrarianAwarded", 1, false);
					}
				}
			}
			SoundManager.PlayUISound("Sounds/Interact/sfx_interact_book_turnIn", 1f, false, false, SoundRequest.SoundEffectType.None);
			string text3 = Grammar.MakeAndList(list7.Select<GameObject, string>((GameObject x) => "'" + x.ShortDisplayNameSingle + "'").ToList<string>(), true);
			Popup.Show(text + " some insightful commentary on " + text3 + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			Popup.Show("You gain {{C|" + num2.ToString() + "}} XP.", null, "Sounds/UI/ui_notification", true, true, true, false, null);
			The.Player.AwardXP(num2, -1, 0, int.MaxValue, null, The.Speaker, null, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06007BBF RID: 31679 RVA: 0x002E7FBC File Offset: 0x002E61BC
		public override bool HandleEvent(HideElementEvent E)
		{
			return false;
		}
	}
}
