using System;
using Qud.API;
using XRL.Core;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Encounters;

namespace XRL.World.Capabilities
{
	// Token: 0x020009F3 RID: 2547
	public static class PsychicGlimmer
	{
		// Token: 0x06006CB2 RID: 27826 RVA: 0x00235A78 File Offset: 0x00233C78
		public static bool Perceptible(int amount)
		{
			return amount >= DimensionManager.GLIMMER_FLOOR;
		}

		// Token: 0x06006CB3 RID: 27827 RVA: 0x00235A85 File Offset: 0x00233C85
		public static bool Perceptible(GameObject who)
		{
			return PsychicGlimmer.Perceptible(who.GetPsychicGlimmer(null));
		}

		// Token: 0x06006CB4 RID: 27828 RVA: 0x00235A94 File Offset: 0x00233C94
		public static void Update(GameObject who)
		{
			if (!who.IsPlayer())
			{
				return;
			}
			if (who.HasEffect<Dominated>())
			{
				return;
			}
			int psychicGlimmer = who.GetPsychicGlimmer(null);
			if (psychicGlimmer >= DimensionManager.GLIMMER_FLOOR && who.GetIntProperty("LastGlimmer", 0) < DimensionManager.GLIMMER_FLOOR)
			{
				Popup.Show("{{K|You are being watched.\n\nIt's a familiar feeling. When someone has watched you in the past, when it's light that's betrayed your presence, you made a friend of the darkness. You pulled your hat brim low over your eyes. You stepped behind the cover of a thatched wall. But those who watch you now watch in spite of such simple obstructions. Their sight isn't mediated by the rays of a gleaming star or torch but by something much older. If there are ways to conceal " + who.itself + " from these seeing eyes, if there are new kinds of darknesses to befriend, you know nothing of them.}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				if (!XRLCore.Core.Game.HasIntGameState("ExceededGlimmerFloor") || XRLCore.Core.Game.GetIntGameState("ExceededGlimmerFloor", 0) != 1)
				{
					JournalAPI.AddAccomplishment("You had the feeling of being watched, and learned that there's a sight older than sight.", string.Concat(new string[]
					{
						"In the month of ",
						Calendar.GetMonth(),
						" of ",
						Calendar.GetYear().ToString(),
						", =name= was gifted with a divine sight older than sight."
					}), string.Concat(new string[]
					{
						"Deep in the wilds of ",
						The.Player.CurrentZone.GetTerrainDisplayName(true),
						", =name= discovered the psychic sea. There ",
						The.Player.GetPronounProvider(false).Subjective,
						" befriended the Seekers of the Sightless Way and learned there's a sight older than sight."
					}), null, "general", MuralCategory.LearnsSecret, MuralWeight.High, null, -1L, true);
					Achievement.GLIMMER_20.Unlock();
				}
				if (who.IsTrueKin())
				{
					Achievement.TRUE_GLIMMER.Unlock();
				}
				XRLCore.Core.Game.SetIntGameState("ExceededGlimmerFloor", 1);
			}
			if (psychicGlimmer < DimensionManager.GLIMMER_FLOOR && who.GetIntProperty("LastGlimmer", 0) >= DimensionManager.GLIMMER_FLOOR)
			{
				Popup.Show("{{K|You've discovered a way to conceal " + who.itself + ". For now.}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			if (psychicGlimmer >= DimensionManager.GLIMMER_EXTRADIMENSIONAL_FLOOR && who.GetIntProperty("LastGlimmer", 0) < DimensionManager.GLIMMER_EXTRADIMENSIONAL_FLOOR)
			{
				Popup.Show("{{K|What you understood to be the psychic sea was only a pond. There are other watchers now, countless in number, beyond the gulf of materiality. Points of light glimmer in all directions, but what are directions on a space that cannot be ordered? All you know now is of an aether vaster than the very mathematics that describe it. And you are not nor will you ever be again alone.}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				if (!XRLCore.Core.Game.HasIntGameState("ExceededGlimmerExtraFloor") || XRLCore.Core.Game.GetIntGameState("ExceededGlimmerExtraFloor", 0) != 1)
				{
					Achievement.GLIMMER_40.Unlock();
					JournalAPI.AddAccomplishment("You learned a cosmic truth, early among truths, about the locality of space and time as you knew them and an aether vaster than both.", string.Concat(new string[]
					{
						"In the month of ",
						Calendar.GetMonth(),
						" of ",
						Calendar.GetYear().ToString(),
						", =name= saw the psychic aether for what it was and became an extradimensional being of note."
					}), string.Concat(new string[]
					{
						"Deep in the wilds of ",
						The.Player.CurrentZone.GetTerrainDisplayName(true),
						", =name= discovered the the directionless space that cannot be ordered. There she befriended extradimensional beings and learned that what ",
						The.Player.GetPronounProvider(false).PossessiveAdjective,
						" thought was the psychic sea was only a pond."
					}), null, "general", MuralCategory.LearnsSecret, MuralWeight.High, null, -1L, true);
				}
				XRLCore.Core.Game.SetIntGameState("ExceededGlimmerExtraFloor", 1);
			}
			if (psychicGlimmer < DimensionManager.GLIMMER_EXTRADIMENSIONAL_FLOOR && who.GetIntProperty("LastGlimmer", 0) >= DimensionManager.GLIMMER_EXTRADIMENSIONAL_FLOOR)
			{
				Popup.Show("{{K|You've discovered a way to conceal " + who.itself + " from extradimensional watchers. For now.}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			if (psychicGlimmer >= 100)
			{
				Achievement.GLIMMER_100.Unlock();
			}
			if (psychicGlimmer >= 200)
			{
				Achievement.GLIMMER_200.Unlock();
			}
			who.SetIntProperty("LastGlimmer", psychicGlimmer, false);
		}
	}
}
