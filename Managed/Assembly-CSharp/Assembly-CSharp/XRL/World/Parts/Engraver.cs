using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.UI;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02001024 RID: 4132
	[Serializable]
	public class Engraver : IPoweredPart
	{
		// Token: 0x0600A489 RID: 42121 RVA: 0x003C23E9 File Offset: 0x003C05E9
		public Engraver()
		{
			this.ChargeUse = 500;
			this.WorksOnEquipper = true;
			this.NameForStatus = "PrecisionEngraver";
		}

		// Token: 0x0600A48A RID: 42122 RVA: 0x003C240E File Offset: 0x003C060E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600A48B RID: 42123 RVA: 0x003C242C File Offset: 0x003C062C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.IsObjectActivePartSubject(E.Actor))
			{
				E.AddAction("Engrave", "engrave", "Engrave", null, 'e', false, 0, 0, false, false, true, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A48C RID: 42124 RVA: 0x003C2474 File Offset: 0x003C0674
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Engrave" && this.AttemptEngrave(E.Actor))
			{
				E.Actor.UseEnergy(10000, "Item Engraver", null, null, false);
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A48D RID: 42125 RVA: 0x003C24D0 File Offset: 0x003C06D0
		public bool AttemptEngrave(GameObject Actor)
		{
			if (!Actor.CanMoveExtremities(null, true, false, true))
			{
				return false;
			}
			if (Actor.AreHostilesNearby())
			{
				return Actor.Fail("You can't engrave with hostiles nearby.");
			}
			ActivePartStatus activePartStatus = base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			if (activePartStatus != ActivePartStatus.Operational)
			{
				if (activePartStatus == ActivePartStatus.Unpowered)
				{
					return Actor.Fail(this.ParentObject.Does("click", int.MaxValue, null, null, "merely", false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + ".");
				}
				return Actor.Fail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + this.GetStatusPhrase(activePartStatus) + ".");
			}
			else
			{
				if (!Actor.IsPlayer())
				{
					return false;
				}
				if (Actor.CurrentCell == null)
				{
					return false;
				}
				Cell cell = base.PickDirection("Engrave", null);
				if (cell == null)
				{
					return false;
				}
				GameObject firstObjectWithPart = cell.GetFirstObjectWithPart("Body", Actor.GetPhase(), Actor, null, null, null, true, false, false);
				if (firstObjectWithPart == null)
				{
					return Actor.Fail("There is nobody there you can engrave.");
				}
				if (firstObjectWithPart != Actor && !firstObjectWithPart.IsPlayerLed())
				{
					return Actor.Fail("You can only engrave " + Actor.itself + " and your companions.");
				}
				Body body = firstObjectWithPart.Body;
				if (body == null)
				{
					return Actor.Fail("You cannot engrave " + ((firstObjectWithPart == Actor) ? Actor.itself : firstObjectWithPart.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)) + ".");
				}
				List<BodyPart> list = new List<BodyPart>();
				foreach (BodyPart bodyPart in body.GetParts())
				{
					if (Tattoos.GetBodyPartGeneralTattooability(bodyPart) == Tattoos.ApplyResult.Engravable)
					{
						list.Add(bodyPart);
					}
				}
				if (list.Count == 0)
				{
					return Actor.Fail("You cannot engrave " + ((firstObjectWithPart == Actor) ? Actor.itself : firstObjectWithPart.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)) + ".");
				}
				List<string> list2 = new List<string>(list.Count);
				List<BodyPart> list3 = new List<BodyPart>(list.Count);
				List<char> list4 = new List<char>(list.Count);
				char c = 'a';
				foreach (BodyPart bodyPart2 in list)
				{
					list2.Add(bodyPart2.GetCardinalName());
					list3.Add(bodyPart2);
					list4.Add(c);
					c += '\u0001';
				}
				int num = Popup.PickOption("Choose a body part to engrave.", null, "", "Sounds/UI/ui_notification", list2.ToArray(), list4.ToArray(), null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
				if (num < 0)
				{
					return false;
				}
				BodyPart bodyPart3 = list3[num];
				if (bodyPart3 == null)
				{
					return false;
				}
				Tattoos.ApplyResult applyResult = Tattoos.CanApplyTattoo(Actor, bodyPart3, false, true);
				if (applyResult != Tattoos.ApplyResult.Engravable)
				{
					if (applyResult == Tattoos.ApplyResult.TooManyTattoos)
					{
						return Actor.Fail(string.Concat(new string[]
						{
							"There are too many engravings on ",
							(firstObjectWithPart == Actor) ? "your" : Grammar.MakePossessive(firstObjectWithPart.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)),
							" ",
							bodyPart3.GetOrdinalName(),
							" to add more."
						}));
					}
					if (applyResult == Tattoos.ApplyResult.AbstractBodyPart || applyResult == Tattoos.ApplyResult.NonContactBodyPart)
					{
						return Actor.Fail(string.Concat(new string[]
						{
							"You can't engrave ",
							(firstObjectWithPart == Actor) ? "your" : Grammar.MakePossessive(firstObjectWithPart.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)),
							" ",
							bodyPart3.GetOrdinalName(),
							"."
						}));
					}
					if (applyResult == Tattoos.ApplyResult.InappropriateBodyPart)
					{
						return Actor.Fail(string.Concat(new string[]
						{
							(firstObjectWithPart == Actor) ? "Your" : Grammar.MakePossessive(firstObjectWithPart.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)),
							" ",
							bodyPart3.GetOrdinalName(),
							" can't be engraved because ",
							bodyPart3.Plural ? "they don't" : "it doesn't",
							" have a hard enough body part."
						}));
					}
					return Actor.Fail(string.Concat(new string[]
					{
						"You can't engrave ",
						(firstObjectWithPart == Actor) ? "your" : Grammar.MakePossessive(firstObjectWithPart.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)),
						" ",
						bodyPart3.GetOrdinalName(),
						"."
					}));
				}
				else
				{
					string text = Popup.AskString("Describe your engraving. For example: \"a tiny snail\".", "", "Sounds/UI/ui_notification", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -+/#()!@$%*<>", null, 30, 0, false, true, null);
					if (text.IsNullOrEmpty())
					{
						return false;
					}
					bool flag = Actor.HasMarkOfDeath(null);
					if (!Tattoos.IsSuccess(Tattoos.ApplyTattoo(firstObjectWithPart, bodyPart3, false, true, text, null, null)))
					{
						return Actor.Fail("Something went wrong and the engraving fails.");
					}
					base.PlayWorldSound("Sounds/Interact/sfx_interact_engraver", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
					base.ConsumeCharge(null, null);
					if (Actor.IsPlayer())
					{
						if (!flag && Actor.HasMarkOfDeath(null))
						{
							Popup.Show("You engrave the mark of death on " + firstObjectWithPart.poss(bodyPart3.GetOrdinalName(), true) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							The.Game.FinishQuestStep("Tomb of the Eaters", "Inscribe the Mark", -1, true, null);
						}
						else
						{
							Popup.Show(string.Concat(new string[]
							{
								"You engrave ",
								text,
								" on ",
								firstObjectWithPart.poss(bodyPart3.GetOrdinalName(), true),
								"."
							}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
					}
					return true;
				}
			}
		}
	}
}
