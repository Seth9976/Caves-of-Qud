using System;
using XRL.Rules;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Effects;
using XRL.World.Parts;

namespace XRL.World.Capabilities
{
	// Token: 0x020009E8 RID: 2536
	public static class Firefighting
	{
		// Token: 0x06006C2D RID: 27693 RVA: 0x0022FE70 File Offset: 0x0022E070
		public static bool CanAttemptFirefighting(GameObject Actor, GameObject Subject = null)
		{
			if (Subject == null)
			{
				Subject = Actor;
			}
			BodyPart bodyPart;
			if (Actor == null)
			{
				bodyPart = null;
			}
			else
			{
				Body body = Actor.Body;
				bodyPart = ((body != null) ? body.GetFirstPart("Hands") : null);
			}
			BodyPart bodyPart2 = bodyPart;
			if (Actor != Subject)
			{
				if (bodyPart2 == null)
				{
					return false;
				}
				if (Subject.CurrentCell != null && Actor.CurrentCell != null && Subject.IsFlying && !Actor.IsFlying)
				{
					return false;
				}
			}
			else if (bodyPart2 == null)
			{
				if (Actor.IsFlying)
				{
					return false;
				}
				if (!Actor.PhaseMatches(1))
				{
					return false;
				}
			}
			return Actor.isDamaged(50, true) && Actor.Stat("Intelligence", 0) >= 7 && Actor.Stat("Willpower", 0) >= 7;
		}

		// Token: 0x06006C2E RID: 27694 RVA: 0x0022FF10 File Offset: 0x0022E110
		public static bool NeedsToLandToAttemptFirefighting(GameObject Actor, GameObject Subject = null)
		{
			if (Subject == null)
			{
				Subject = Actor;
			}
			if (Actor != Subject)
			{
				return false;
			}
			bool flag;
			if (Actor == null)
			{
				flag = null != null;
			}
			else
			{
				Body body = Actor.Body;
				flag = ((body != null) ? body.GetFirstPart("Hands") : null) != null;
			}
			return !flag && Actor.IsFlying && Actor.PhaseMatches(1);
		}

		// Token: 0x06006C2F RID: 27695 RVA: 0x0022FF5C File Offset: 0x0022E15C
		public static bool AttemptFirefighting(GameObject Actor, GameObject Subject, int EnergyCost = 1000, bool Automatic = false, bool Dialog = false)
		{
			bool flag = Firefighting.AttemptFirefightingCore(Actor, Subject, EnergyCost, Automatic, Dialog);
			if (flag && EnergyCost > 0)
			{
				Actor.UseEnergy(1000, Automatic ? "Pass Firefighting" : "Firefighting", null, null, false);
			}
			return flag;
		}

		// Token: 0x06006C30 RID: 27696 RVA: 0x0022FFA4 File Offset: 0x0022E1A4
		private static bool AttemptFirefightingCore(GameObject Actor, GameObject Subject, int EnergyCost, bool Automatic, bool Dialog)
		{
			BodyPart bodyPart;
			if (Actor == null)
			{
				bodyPart = null;
			}
			else
			{
				Body body = Actor.Body;
				bodyPart = ((body != null) ? body.GetFirstPart("Hands") : null);
			}
			BodyPart bodyPart2 = bodyPart;
			if (Subject.CurrentCell != null && Actor.CurrentCell != null && Subject.IsFlying && !Actor.IsFlying)
			{
				if (Actor.IsPlayer())
				{
					Popup.ShowFail("You cannot reach " + Subject.the + Subject.ShortDisplayName + "!", true, true, true);
				}
				return false;
			}
			if (Actor == Subject && Actor.HasEffect<Prone>() && !Actor.IsFlying && Actor.PhaseMatches(1))
			{
				Messaging.XDidY(Actor, "roll", "on the ground", "!", null, null, Actor, null, false, false, null, null, false, false, false, Dialog, false, null);
				int @for = GetFirefightingPerformanceEvent.GetFor(Actor, null, false, true);
				if (@for != 0)
				{
					Actor.TemperatureChange(@for, null, false, true, false, false, 5, null, null);
				}
				return true;
			}
			if (bodyPart2 != null)
			{
				if (Actor != Subject)
				{
					if (Subject.IsHostileTowards(Actor) && Subject.IsMobile())
					{
						int combatDV = Stats.GetCombatDV(Subject);
						if (Stat.Random(1, 20) + Actor.StatMod("Agility", 0) < combatDV)
						{
							Messaging.XDidYToZ(Actor, "try", "to beat at the flames on", Subject, ", but " + Subject.it + Subject.GetVerb("dodge", true, true, false, true, false), "!", null, null, null, Actor, false, false, false, false, false, null, null, null, false, false, false, Dialog, false, null);
							return true;
						}
					}
					if (Subject.CurrentCell != null && !Actor.PhaseMatches(Subject))
					{
						Messaging.XDidYToZ(Actor, "try", "to beat at the flames on", Subject, string.Concat(new string[] { ", but ", Actor.its, " ", bodyPart2.Name, " pass through ", Subject.them }), "!", null, null, null, Actor, false, false, false, false, false, null, null, null, false, false, false, Dialog, false, null);
						return true;
					}
				}
				if (Actor == Subject)
				{
					Messaging.XDidY(Actor, "beat", "at the flames with " + Actor.its + " " + bodyPart2.Name, "!", null, null, Actor, null, false, false, null, null, false, false, false, Dialog, false, null);
				}
				else
				{
					Messaging.XDidYToZ(Actor, "beat", "at the flames on", Subject, "with " + Actor.its + " " + bodyPart2.Name, "!", null, null, Subject, null, false, false, false, false, false, null, null, null, false, false, false, Dialog, false, null);
				}
				if (!Actor.FireEvent(Event.New("PerformedFirefighting", "Subject", Subject)))
				{
					return true;
				}
				if (!Subject.FireEvent(Event.New("ReceivedFirefighting", "Subject", Subject)))
				{
					return true;
				}
				int for2 = GetFirefightingPerformanceEvent.GetFor(Actor, Subject, true, false);
				if (for2 != 0)
				{
					Subject.TemperatureChange(for2, null, false, true, false, false, 5, null, null);
				}
				return true;
			}
			else
			{
				if (Actor != Subject)
				{
					if (Actor.IsPlayer())
					{
						Popup.ShowFail("You have no hands to beat at the flames with!", true, true, true);
					}
					return false;
				}
				if (Actor.IsFlying)
				{
					if (Actor.IsPlayer())
					{
						Popup.ShowFail("You have no hands to beat at the flames with, and cannot roll on the ground because you are flying!", true, true, true);
					}
					return false;
				}
				if (!Actor.PhaseMatches(1))
				{
					if (Actor.IsPlayer())
					{
						Popup.ShowFail("You have no hands to beat at the flames with, and cannot roll on the ground because you are phased out!", true, true, true);
					}
					return false;
				}
				bool flag = false;
				if (Actor.IsPlayer())
				{
					if (Popup.ShowYesNo("You have no hands to beat at the flames with. Do you want to roll on the ground to try to put them out?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
					{
						flag = true;
					}
				}
				else if (Actor.isDamaged(50, true) && Actor.Stat("Intelligence", 0) >= 7 && Actor.Stat("Willpower", 0) >= 7)
				{
					flag = true;
				}
				if (flag)
				{
					Subject.ApplyEffect(new Prone(true, false, false, null), null);
					Messaging.XDidY(Actor, "roll", "on the ground", "!", null, null, Actor, null, false, false, null, null, false, false, false, Dialog, false, null);
					Subject.TemperatureChange(-200, null, false, true, false, false, 5, null, null);
					return true;
				}
				return false;
			}
		}
	}
}
