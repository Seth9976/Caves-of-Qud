using System;
using System.Collections.Generic;
using HistoryKit;
using XRL.Collections;
using XRL.Language;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x02000EBF RID: 3775
	[HasModSensitiveStaticCache]
	public class GenerateFriendOrFoe
	{
		// Token: 0x06009776 RID: 38774 RVA: 0x0036FCF0 File Offset: 0x0036DEF0
		[ModSensitiveCacheInit]
		public static void Init()
		{
			if (GenerateFriendOrFoe.hateReasons == null)
			{
				GenerateFriendOrFoe.hateReasons = new List<string>(30);
			}
			GenerateFriendOrFoe.hateReasons.Add("insulting their $noun");
			GenerateFriendOrFoe.hateReasons.Add("stealing a cherished heirloom");
			GenerateFriendOrFoe.hateReasons.Add("slaying one of their leaders");
			GenerateFriendOrFoe.hateReasons.Add("eating one of their young");
			GenerateFriendOrFoe.hateReasons.Add("casting doubt on their beliefs");
			GenerateFriendOrFoe.hateReasons.Add("tricking them into sharing their freshwater");
			GenerateFriendOrFoe.hateReasons.Add("some reason no one remembers");
			GenerateFriendOrFoe.hateReasons.Add("sowing their fields with salt");
			GenerateFriendOrFoe.hateReasons.Add("disproving a famous theorem");
			GenerateFriendOrFoe.hateReasons.Add("digging up the remains of their ancestors");
			GenerateFriendOrFoe.hateReasons.Add("torching one of their villages");
			GenerateFriendOrFoe.hateReasons.Add("repeatedly beating them at dice");
			GenerateFriendOrFoe.hateReasons.Add("releasing snakes into one of their camps");
			GenerateFriendOrFoe.hateReasons.Add("disparaging a famous poet");
			GenerateFriendOrFoe.hateReasons.Add("poisoning their freshwater");
			GenerateFriendOrFoe.hateReasons.Add("impersonating one of their leaders");
			GenerateFriendOrFoe.hateReasons.Add("leading a raiding party on one of their camps");
			GenerateFriendOrFoe.hateReasons.Add("cooking them a rancid meal");
			GenerateFriendOrFoe.hateReasons.Add("telling bawdy jokes");
			GenerateFriendOrFoe.hateReasons.Add("lighting a beacon fire to warn their enemies");
			GenerateFriendOrFoe.hateReasons.Add("refusing them entrance to a local library");
			GenerateFriendOrFoe.hateReasons.Add("selling a map of their vaults to adventurers");
			GenerateFriendOrFoe.hateReasons.Add("eating all their fruit");
			GenerateFriendOrFoe.hateReasons.Add("eavesdropping on their secret ceremonies");
			GenerateFriendOrFoe.hateReasons.Add("reprogramming their favorite robot");
			GenerateFriendOrFoe.hateReasons.Add("ruining the festival of Ut yara Ux");
			GenerateFriendOrFoe.hateReasons.Add("burning one of their leaders in effigy");
			GenerateFriendOrFoe.hateReasons.Add("giving one of their kind an unfavorable horoscope reading");
			GenerateFriendOrFoe.hateReasons.Add("questioning the origins of the moon");
			GenerateFriendOrFoe.hateReasons.Add("worshipping a highly entropic being");
			if (GenerateFriendOrFoe.likeReasons == null)
			{
				GenerateFriendOrFoe.likeReasons = new List<string>(30);
			}
			GenerateFriendOrFoe.likeReasons.Add("praising their $noun");
			GenerateFriendOrFoe.likeReasons.Add("sharing freshwater with them");
			GenerateFriendOrFoe.likeReasons.Add("making them feel welcomed at a supper feast");
			GenerateFriendOrFoe.likeReasons.Add("saving one of their young from drowning");
			GenerateFriendOrFoe.likeReasons.Add("resembling one of their idols");
			GenerateFriendOrFoe.likeReasons.Add("respecting the sanctity of a burial site");
			GenerateFriendOrFoe.likeReasons.Add("worshipping the same deities");
			GenerateFriendOrFoe.likeReasons.Add("attending a funeral for their leader");
			GenerateFriendOrFoe.likeReasons.Add("uncovering a plot against them");
			GenerateFriendOrFoe.likeReasons.Add("penning a moving poem");
			GenerateFriendOrFoe.likeReasons.Add("telling bawdy jokes");
			GenerateFriendOrFoe.likeReasons.Add("cooking them a splendid meal");
			GenerateFriendOrFoe.likeReasons.Add("giving one of their kind a favorable horoscope reading");
			GenerateFriendOrFoe.likeReasons.Add("faithfully adapting one of their plays");
			GenerateFriendOrFoe.likeReasons.Add("pouring asphalt on one of their enemies");
			GenerateFriendOrFoe.likeReasons.Add("explaining the meaning of the Canticles Chromaic");
			GenerateFriendOrFoe.likeReasons.Add("reprogramming their least favorite robot");
			GenerateFriendOrFoe.likeReasons.Add("fervently celebrating the solstice");
			GenerateFriendOrFoe.likeReasons.Add("providing shelter during a glass storm");
			if (GenerateFriendOrFoe.reasonReplacers == null)
			{
				GenerateFriendOrFoe.reasonReplacers = new List<Func<string, string>>();
			}
			GenerateFriendOrFoe.reasonReplacers.Add(new Func<string, string>(GenerateFriendOrFoe.replacePlaceholders));
		}

		// Token: 0x06009777 RID: 38775 RVA: 0x00370029 File Offset: 0x0036E229
		public static string replacePlaceholders(string reason)
		{
			if (reason == null)
			{
				return null;
			}
			return reason.Replace("$noun", Grammar.Pluralize(HistoricStringExpander.ExpandString("<spice.nouns.!random>", null)));
		}

		// Token: 0x06009778 RID: 38776 RVA: 0x0037004C File Offset: 0x0036E24C
		public static string getHateReason()
		{
			string text = GenerateFriendOrFoe.hateReasons.GetRandomElement<string>(null);
			foreach (Func<string, string> func in GenerateFriendOrFoe.reasonReplacers)
			{
				text = func(text);
			}
			return text;
		}

		// Token: 0x06009779 RID: 38777 RVA: 0x003700AC File Offset: 0x0036E2AC
		public static string getLikeReason()
		{
			string text = GenerateFriendOrFoe.likeReasons.GetRandomElement<string>(null);
			foreach (Func<string, string> func in GenerateFriendOrFoe.reasonReplacers)
			{
				text = func(text);
			}
			return text;
		}

		// Token: 0x0600977A RID: 38778 RVA: 0x0037010C File Offset: 0x0036E30C
		public static string getRandomFaction(GameObject parent)
		{
			string randomElement;
			using (ScopeDisposedList<string> fromPool = ScopeDisposedList<string>.GetFromPool())
			{
				AllegianceSet baseAllegiance = parent.Brain.GetBaseAllegiance();
				AllegianceSet allegiance = parent.Brain.Allegiance;
				string text = parent.GetxTag("Reputation", "LovedBy", null);
				foreach (Faction faction in Factions.Loop())
				{
					if (faction.Visible && !baseAllegiance.ContainsKey(faction.Name) && !allegiance.ContainsKey(faction.Name) && (text == null || !text.HasDelimitedSubstring(',', faction.Name, StringComparison.Ordinal)))
					{
						fromPool.Add(faction.Name);
					}
				}
				randomElement = fromPool.GetRandomElement<string>(null);
			}
			return randomElement;
		}

		// Token: 0x04003EF3 RID: 16115
		[ModSensitiveStaticCache(false)]
		public static List<string> hateReasons;

		// Token: 0x04003EF4 RID: 16116
		[ModSensitiveStaticCache(false)]
		public static List<string> likeReasons;

		// Token: 0x04003EF5 RID: 16117
		[ModSensitiveStaticCache(false)]
		public static List<Func<string, string>> reasonReplacers;
	}
}
