using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.UI;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x020010AA RID: 4266
	[Serializable]
	public class TattooGun : IPoweredPart
	{
		// Token: 0x0600A9BC RID: 43452 RVA: 0x003E09E0 File Offset: 0x003DEBE0
		public TattooGun()
		{
			this.ChargeUse = 0;
			this.ConsumesLiquid = "ink";
			this.MustBeUnderstood = true;
			this.WorksOnEquipper = true;
			this.WorksOnCarrier = true;
			this.IsEMPSensitive = false;
			this.NameForStatus = "IntradermalInjector";
		}

		// Token: 0x0600A9BD RID: 43453 RVA: 0x003E0A2C File Offset: 0x003DEC2C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600A9BE RID: 43454 RVA: 0x003E0A4C File Offset: 0x003DEC4C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.IsObjectActivePartSubject(E.Actor))
			{
				E.AddAction("Tattoo", "tattoo", "Tattoo", null, 't', false, 0, 0, false, false, true, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9BF RID: 43455 RVA: 0x003E0A94 File Offset: 0x003DEC94
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Tattoo" && this.AttemptTattoo(E.Actor))
			{
				E.Actor.UseEnergy(10000, "Item TattooGun", null, null, false);
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9C0 RID: 43456 RVA: 0x003E0AF0 File Offset: 0x003DECF0
		public bool AttemptTattoo(GameObject Actor)
		{
			if (!Actor.CanMoveExtremities(null, true, false, true))
			{
				return false;
			}
			if (Actor.AreHostilesNearby())
			{
				Actor.Fail("You can't tattoo with hostiles nearby.");
				return false;
			}
			ActivePartStatus activePartStatus = base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			if (activePartStatus != ActivePartStatus.Operational)
			{
				if (activePartStatus == ActivePartStatus.ProcessInputMissing)
				{
					Actor.Fail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " out of {{|" + LiquidVolume.GetLiquid(this.ConsumesLiquid).GetName() + "}}.");
				}
				else if (activePartStatus == ActivePartStatus.ProcessInputInvalid)
				{
					Actor.Fail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " filled with contaminated {{|" + LiquidVolume.GetLiquid(this.ConsumesLiquid).GetName() + "}}!");
				}
				else if (activePartStatus == ActivePartStatus.Unpowered)
				{
					Actor.Fail(this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " merely" + this.ParentObject.GetVerb("click", true, false, false, true, false) + ".");
				}
				else
				{
					Actor.Fail(this.ParentObject.Does("aren't", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " working!");
				}
				return false;
			}
			if (!Actor.IsPlayer())
			{
				return false;
			}
			if (Actor.CurrentCell == null)
			{
				return false;
			}
			Cell cell = base.PickDirection("Tattoo", null);
			if (cell == null)
			{
				return false;
			}
			GameObject firstObjectWithPart = cell.GetFirstObjectWithPart("Body", Actor.GetPhase(), Actor, null, null, null, true, false, false);
			if (firstObjectWithPart == null)
			{
				Actor.Fail("There is nobody there you can tattoo.");
				return false;
			}
			if (firstObjectWithPart != Actor && !firstObjectWithPart.IsPlayerLed())
			{
				Actor.Fail("You can only tattoo " + Actor.itself + " and your companions.");
				return false;
			}
			Body body = firstObjectWithPart.Body;
			if (body == null)
			{
				Actor.Fail("You cannot tattoo " + ((firstObjectWithPart == Actor) ? Actor.itself : firstObjectWithPart.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)) + ".");
				return false;
			}
			List<BodyPart> list = new List<BodyPart>();
			foreach (BodyPart bodyPart in body.GetParts())
			{
				if (Tattoos.GetBodyPartGeneralTattooability(bodyPart) == Tattoos.ApplyResult.Tattooable)
				{
					list.Add(bodyPart);
				}
			}
			if (list.Count == 0)
			{
				Actor.Fail("You cannot tattoo " + ((firstObjectWithPart == Actor) ? Actor.itself : firstObjectWithPart.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)) + ".");
				return false;
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
			int num = Popup.PickOption("Choose a body part to tattoo.", null, "", "Sounds/UI/ui_notification", list2.ToArray(), list4.ToArray(), null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
			if (num < 0)
			{
				return false;
			}
			BodyPart bodyPart3 = list3[num];
			if (bodyPart3 == null)
			{
				return false;
			}
			Tattoos.ApplyResult applyResult = Tattoos.CanApplyTattoo(Actor, bodyPart3, true, false);
			if (applyResult != Tattoos.ApplyResult.Tattooable)
			{
				if (applyResult == Tattoos.ApplyResult.TooManyTattoos)
				{
					Actor.Fail(string.Concat(new string[]
					{
						"There are too many tattoos on ",
						(firstObjectWithPart == Actor) ? "your" : Grammar.MakePossessive(firstObjectWithPart.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)),
						" ",
						bodyPart3.GetOrdinalName(),
						" to add more."
					}));
				}
				else if (applyResult == Tattoos.ApplyResult.AbstractBodyPart || applyResult == Tattoos.ApplyResult.NonContactBodyPart)
				{
					Actor.Fail(string.Concat(new string[]
					{
						"You can't tattoo ",
						(firstObjectWithPart == Actor) ? "your" : Grammar.MakePossessive(firstObjectWithPart.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)),
						" ",
						bodyPart3.GetOrdinalName(),
						"."
					}));
				}
				else if (applyResult == Tattoos.ApplyResult.InappropriateBodyPart)
				{
					Actor.Fail(string.Concat(new string[]
					{
						(firstObjectWithPart == Actor) ? "Your" : Grammar.MakePossessive(firstObjectWithPart.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)),
						" ",
						bodyPart3.GetOrdinalName(),
						" can't be tattooed because ",
						bodyPart3.Plural ? "they don't" : "it doesn't",
						" have flesh."
					}));
				}
				else
				{
					Actor.Fail(string.Concat(new string[]
					{
						"You can't tattoo ",
						(firstObjectWithPart == Actor) ? "your" : Grammar.MakePossessive(firstObjectWithPart.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)),
						" ",
						bodyPart3.GetOrdinalName(),
						"."
					}));
				}
				return false;
			}
			string text = Popup.AskString("Describe your tattoo. For example: \"a tiny snail\".", "", "Sounds/UI/ui_notification", "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -+/#()!@$%*<>", null, 30, 0, false, true, null);
			if (text.IsNullOrEmpty())
			{
				return false;
			}
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				Actor.Fail(this.ParentObject.Does("aren't", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " working!");
				return false;
			}
			string text2 = Popup.ShowColorPicker("Choose a primary color.", 0, null, 60, false, false, null, "", true, false, true, null);
			if (text2 == null)
			{
				return false;
			}
			string text3 = null;
			if (!text2.IsNullOrEmpty())
			{
				text3 = Popup.ShowColorPicker("Choose a secondary color.", 0, null, 60, false, false, null, "", true, false, false, null);
				if (text3 == null)
				{
					return false;
				}
			}
			bool flag = Actor.HasMarkOfDeath(null);
			if (!Tattoos.IsSuccess(Tattoos.ApplyTattoo(firstObjectWithPart, bodyPart3, true, false, text, text2, text3)))
			{
				Actor.Fail("Something went wrong and the tattooing fails.");
				return false;
			}
			Actor.PlayWorldOrUISound("Sounds/Interact/sfx_interact_tattoo", null);
			base.ConsumeCharge(null, null);
			if (Actor.IsPlayer())
			{
				if (!flag && Actor.HasMarkOfDeath(null))
				{
					Popup.Show("You tattoo the mark of death on " + firstObjectWithPart.poss(bodyPart3.GetOrdinalName(), true) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					The.Game.FinishQuestStep("Tomb of the Eaters", "Inscribe the Mark", -1, true, null);
				}
				else
				{
					Popup.Show(string.Concat(new string[]
					{
						"You tattoo ",
						text,
						" on ",
						firstObjectWithPart.poss(bodyPart3.GetOrdinalName(), true),
						"."
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				if (firstObjectWithPart == Actor)
				{
					Achievement.TATTOO_SELF.Unlock();
				}
			}
			return true;
		}
	}
}
