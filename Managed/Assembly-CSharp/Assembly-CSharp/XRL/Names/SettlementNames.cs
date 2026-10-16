using System;
using System.Collections.Generic;
using HistoryKit;
using XRL.Annals;
using XRL.Language;
using XRL.Rules;

namespace XRL.Names
{
	// Token: 0x02000585 RID: 1413
	public static class SettlementNames
	{
		// Token: 0x06003CA5 RID: 15525 RVA: 0x0012A214 File Offset: 0x00128414
		public static string GenerateGoatfolkVillageName(History history)
		{
			return NameMaker.MakeName(null, null, null, null, "Goatfolk", null, null, null, null, null, null, "Site", new Dictionary<string, string> { 
			{
				"*Sultan*",
				QudHistoryHelpers.GetRandomSultan(history).GetCurrentSnapshot().GetProperty("nameRoot", "unknown") + " "
			} }, false, false, null, null);
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x0012A284 File Offset: 0x00128484
		public static string GenerateGoatfolkQlippothVillageName(History history)
		{
			return NameMaker.MakeName(null, null, null, null, "Goatfolk", null, null, null, null, null, "Qlippoth", "Site", new Dictionary<string, string> { 
			{
				"*Sultan*",
				QudHistoryHelpers.GetRandomSultan(history).GetCurrentSnapshot().GetProperty("nameRoot", "unknown") + " "
			} }, false, true, null, null);
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x0012A2F6 File Offset: 0x001284F6
		public static string GenerateStarappleFarmName(History history)
		{
			return SettlementNames.GenerateFarmName(history, "starapple");
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x0012A303 File Offset: 0x00128503
		public static string GeneratePigFarmName(History history)
		{
			return SettlementNames.GenerateFarmName(history, "pig");
		}

		// Token: 0x06003CA9 RID: 15529 RVA: 0x0012A310 File Offset: 0x00128510
		private static string GenerateFarmNameInner(History history, string type)
		{
			HistoricEntityList sultans = QudHistoryHelpers.GetSultans(history);
			string text = MutantNameMaker.MakeMutantName();
			if (text.IsNullOrEmpty())
			{
				MetricsManager.LogError("got " + ((text == null) ? "null" : "empty") + " name from MakeMutantName()");
				text = "Urist";
			}
			string text2 = "";
			int num = Stat.Random(0, 100);
			int num2 = Stat.Random(0, 100);
			if (15.in100())
			{
				if (type == "pig")
				{
					return Grammar.A(HistoricStringExpander.ExpandString("<spice.commonPhrases.secluded.!random>", null, history, null, null) + " pig farm", false);
				}
				return Grammar.A(HistoricStringExpander.ExpandString("<spice.commonPhrases.secluded.!random>", null, history, null, null) + " starapple farm", false);
			}
			else
			{
				string text3;
				if (num < 80)
				{
					text3 = text;
					if (30.in100())
					{
						text2 = "the ";
					}
					else if (50.in100())
					{
						text3 = Grammar.MakePossessive(text);
					}
				}
				else if (num < 90)
				{
					text3 = sultans.GetRandomElement(null).GetCurrentSnapshot().GetProperty("nameRoot", "unknown");
					if (text3.IsNullOrEmpty())
					{
						MetricsManager.LogError("got " + ((text == null) ? "null" : "empty") + " name root from sultan");
						text3 = "Urist";
					}
				}
				else
				{
					text3 = QudHistoryHelpers.GetRandomCognomen(history);
				}
				string text4;
				if (num2 < 50)
				{
					text4 = HistoricStringExpander.ExpandString("<spice.commonPhrases." + type + "farm.!random>", null, history, null, null);
				}
				else
				{
					text4 = HistoricStringExpander.ExpandString("<spice.commonPhrases.shire.!random>", null, history, null, null);
					if (20.in100())
					{
						text3 = HistoricStringExpander.ExpandString("<spice.instancesOf." + type + "farmyPrefixes.!random>", null, history, null, null);
						return Grammar.MakeTitleCase(text3 + text4);
					}
				}
				if (num < 90)
				{
					return text2 + Grammar.MakeTitleCase(text3 + " " + text4);
				}
				return "the " + Grammar.MakeTitleCase(text4 + " of " + text2 + text3);
			}
		}

		// Token: 0x06003CAA RID: 15530 RVA: 0x0012A4F4 File Offset: 0x001286F4
		public static string GenerateFarmName(History History, string Type)
		{
			string text = SettlementNames.GenerateFarmNameInner(History, Type);
			if (text.IsNullOrEmpty() || text.EndsWith("of") || text.EndsWith("the") || text.EndsWith(" "))
			{
				MetricsManager.LogError(string.Concat(new string[]
				{
					"generated ",
					Type,
					" farm name \"",
					text ?? "NULL",
					"\""
				}));
			}
			return text;
		}
	}
}
