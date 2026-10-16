using System;
using System.Collections.Generic;
using HistoryKit;
using Newtonsoft.Json.Linq;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A18 RID: 2584
	public class CreatureRegionSpice : IObjectBuilder
	{
		// Token: 0x06006D34 RID: 27956 RVA: 0x0024938F File Offset: 0x0024758F
		public override void Initialize()
		{
			this.TileColor = null;
			this.DetailColor = null;
		}

		// Token: 0x06006D35 RID: 27957 RVA: 0x002493A0 File Offset: 0x002475A0
		public override void Apply(GameObject Object, string Context)
		{
			if (Object.Render == null)
			{
				return;
			}
			Zone zoneGenerationContext = ZoneManager.ZoneGenerationContext;
			string text = ((zoneGenerationContext != null) ? zoneGenerationContext.GetTerrainRegion() : null);
			if (text.IsNullOrEmpty())
			{
				return;
			}
			Render render = Object.Render;
			JToken jtoken = HistoricSpice.root["history"]["regions"]["terrain"][text];
			JArray jarray = (JArray)jtoken["creatureRegionAdjective"];
			JArray jarray2 = (JArray)jtoken["creatureRegionNoun"];
			JArray jarray3 = (JArray)jtoken["creatureAlteredLocale"];
			JArray jarray4 = (JArray)jtoken["creatureAlteredCast"];
			Random seededRandomGenerator = Stat.GetSeededRandomGenerator(Object.Blueprint);
			string text2 = (string)jarray[seededRandomGenerator.Next(jarray.Count)];
			string text3 = (string)jarray2[seededRandomGenerator.Next(jarray2.Count)];
			render.DisplayName = render.DisplayName.StartReplace().AddObject(Object, null).AddReplacer("creatureRegionAdjective", text2)
				.AddReplacer("creatureRegionNoun", text3)
				.ToString();
			Description description;
			if (Object.TryGetPart<Description>(out description))
			{
				string text4 = (string)jarray3[seededRandomGenerator.Next(jarray3.Count)];
				string text5 = (string)jarray4[seededRandomGenerator.Next(jarray4.Count)];
				string[] array = Object.GetxTag("TextFragments", "PoeticFeatures", null).Split(',', StringSplitOptions.None);
				description._Short = description._Short.Replace("=creatureRegionAdjective=", text2).Replace("=creatureRegionNoun=", text3);
				Description description2 = description;
				description2._Short = string.Concat(new string[]
				{
					description2._Short,
					" Time in ",
					text4,
					" has altered =pronouns.possessive= features -- ",
					string.Join(", ", array),
					" -- and given them ",
					text5,
					"."
				});
			}
			if (!this.TileColor.IsNullOrEmpty())
			{
				string text6 = this.TileColor.CachedCommaExpansion().GetRandomElement<string>(seededRandomGenerator);
				if (text6.Length == 1)
				{
					text6 = "&" + text6;
				}
				render.ColorString = text6;
				if (!render.TileColor.IsNullOrEmpty())
				{
					render.TileColor = render.ColorString;
				}
			}
			else
			{
				JArray jarray5 = (JArray)jtoken["baseColor"];
				Render render2 = render;
				string text7 = "&";
				JToken jtoken2 = jarray5[seededRandomGenerator.Next(jarray5.Count)];
				render2.ColorString = text7 + ((jtoken2 != null) ? jtoken2.ToString() : null);
				if (!render.TileColor.IsNullOrEmpty())
				{
					render.TileColor = render.ColorString;
				}
			}
			if (!this.DetailColor.IsNullOrEmpty())
			{
				List<string> list = this.DetailColor.CachedCommaExpansion();
				char tileForegroundColorChar = Object.Render.GetTileForegroundColorChar();
				int num = 0;
				do
				{
					render.DetailColor = list.GetRandomElement<string>(seededRandomGenerator);
				}
				while (tileForegroundColorChar == render.getDetailColor() && num++ < list.Count);
			}
		}

		// Token: 0x040031E5 RID: 12773
		public string TileColor;

		// Token: 0x040031E6 RID: 12774
		public string DetailColor;
	}
}
