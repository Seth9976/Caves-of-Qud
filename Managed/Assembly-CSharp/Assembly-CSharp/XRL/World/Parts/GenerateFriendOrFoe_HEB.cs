using System;
using System.Collections.Generic;
using HistoryKit;
using XRL.Language;
using XRL.World.Encounters;

namespace XRL.World.Parts
{
	// Token: 0x02000EC0 RID: 3776
	[HasModSensitiveStaticCache]
	public class GenerateFriendOrFoe_HEB
	{
		// Token: 0x0600977C RID: 38780 RVA: 0x003701FC File Offset: 0x0036E3FC
		[ModSensitiveCacheInit]
		public static void Init()
		{
			if (GenerateFriendOrFoe_HEB.hateReasons == null)
			{
				GenerateFriendOrFoe_HEB.hateReasons = new List<string>();
			}
			GenerateFriendOrFoe_HEB.hateReasons.Add("inventing the irrational numbers");
			GenerateFriendOrFoe_HEB.hateReasons.Add("destroying the $adjective numbers");
			GenerateFriendOrFoe_HEB.hateReasons.Add("dreaming $dimension into being");
			GenerateFriendOrFoe_HEB.hateReasons.Add("inventing the concept of $nouns");
			GenerateFriendOrFoe_HEB.hateReasons.Add("swapping how $objnouns and $objnoun2s are perceived");
			GenerateFriendOrFoe_HEB.hateReasons.Add("warping a pocket of spacetime into a $weirdobj");
			if (GenerateFriendOrFoe_HEB.likeReasons == null)
			{
				GenerateFriendOrFoe_HEB.likeReasons = new List<string>();
			}
			GenerateFriendOrFoe_HEB.likeReasons.Add("inventing the irrational numbers");
			GenerateFriendOrFoe_HEB.likeReasons.Add("destroying the $adjective numbers");
			GenerateFriendOrFoe_HEB.likeReasons.Add("dreaming $dimension into being");
			GenerateFriendOrFoe_HEB.likeReasons.Add("inventing the concept of $nouns");
			GenerateFriendOrFoe_HEB.likeReasons.Add("swapping how $objnouns and $objnoun2s are perceived");
			GenerateFriendOrFoe_HEB.likeReasons.Add("warping a pocket of spacetime into a $weirdobj");
			if (GenerateFriendOrFoe_HEB.reasonReplacers == null)
			{
				GenerateFriendOrFoe_HEB.reasonReplacers = new List<Func<string, string>>();
			}
			GenerateFriendOrFoe_HEB.reasonReplacers.Add(new Func<string, string>(GenerateFriendOrFoe_HEB.replacePlaceholders));
		}

		// Token: 0x0600977D RID: 38781 RVA: 0x00370308 File Offset: 0x0036E508
		public static string replacePlaceholders(string reason)
		{
			reason = HistoricStringExpander.ExpandString(reason, null, null, new Dictionary<string, string>
			{
				{
					"$nouns",
					Grammar.Pluralize(HistoricStringExpander.ExpandString("<spice.nouns.!random>", null))
				},
				{
					"$noun2s",
					Grammar.Pluralize(HistoricStringExpander.ExpandString("<spice.nouns.!random>", null))
				},
				{
					"$objnouns",
					Grammar.Pluralize(HistoricStringExpander.ExpandString("<spice.objectNouns.!random>", null))
				},
				{
					"$objnoun2s",
					Grammar.Pluralize(HistoricStringExpander.ExpandString("<spice.objectNouns.!random>", null))
				},
				{
					"$weirdobj",
					HistoricStringExpander.ExpandString("<spice.history.gospels.EarlySultanate.location.!random>", null)
				},
				{
					"$adjective",
					HistoricStringExpander.ExpandString("<spice.adjectives.!random>", null)
				}
			}, null);
			if (reason.Contains("$dimension"))
			{
				DimensionManager dimensionManager = The.Game.GetObjectGameState("DimensionManager") as DimensionManager;
				List<ExtraDimension> list = ((dimensionManager != null) ? dimensionManager.ExtraDimensions : null);
				if (list.IsNullOrEmpty<ExtraDimension>())
				{
					reason = reason.Replace("$dimension", "an uncharted dimension");
				}
				else
				{
					ExtraDimension randomElement = list.GetRandomElement<ExtraDimension>(null);
					string text = randomElement.Name.Replace("*DimensionSymbol*", ((char)randomElement.Symbol).ToString());
					reason = reason.Replace("$dimension", text);
				}
			}
			return reason;
		}

		// Token: 0x0600977E RID: 38782 RVA: 0x00370440 File Offset: 0x0036E640
		public static string getHateReason()
		{
			string text = GenerateFriendOrFoe_HEB.hateReasons.GetRandomElement<string>(null);
			foreach (Func<string, string> func in GenerateFriendOrFoe_HEB.reasonReplacers)
			{
				text = func(text);
			}
			return text;
		}

		// Token: 0x0600977F RID: 38783 RVA: 0x003704A0 File Offset: 0x0036E6A0
		public static string getLikeReason()
		{
			string text = GenerateFriendOrFoe_HEB.likeReasons.GetRandomElement<string>(null);
			foreach (Func<string, string> func in GenerateFriendOrFoe_HEB.reasonReplacers)
			{
				text = func(text);
			}
			return text;
		}

		// Token: 0x04003EF6 RID: 16118
		[ModSensitiveStaticCache(false)]
		public static List<string> hateReasons;

		// Token: 0x04003EF7 RID: 16119
		[ModSensitiveStaticCache(false)]
		public static List<string> likeReasons;

		// Token: 0x04003EF8 RID: 16120
		[ModSensitiveStaticCache(false)]
		public static List<Func<string, string>> reasonReplacers;
	}
}
