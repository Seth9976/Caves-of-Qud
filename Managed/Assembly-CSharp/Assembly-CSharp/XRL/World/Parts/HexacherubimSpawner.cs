using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000ECA RID: 3786
	[Serializable]
	public class HexacherubimSpawner : IPart
	{
		// Token: 0x060097B5 RID: 38837 RVA: 0x00371696 File Offset: 0x0036F896
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeObjectCreatedEvent.ID;
		}

		// Token: 0x060097B6 RID: 38838 RVA: 0x003716AC File Offset: 0x0036F8AC
		public override bool HandleEvent(BeforeObjectCreatedEvent E)
		{
			Random seededRandomGenerator = Stat.GetSeededRandomGenerator("SheshCherubim");
			List<string> list = new List<string>(CherubimSpawner.Elements);
			GameObject gameObject = GameObject.Create(CherubimSpawner.Factions.GetRandomElement<string>(seededRandomGenerator) + " Cherub");
			string text = "the " + string.Join(", the ", gameObject.GetxTag("TextFragments", "PoeticFeatures", null).Split(',', StringSplitOptions.None));
			gameObject.Render.DisplayName = gameObject.Render.DisplayName.Replace("cherub", "hexacherub");
			gameObject.GetStat("Level").BaseValue = 50;
			gameObject.GetStat("Hitpoints").BaseValue = 2000;
			gameObject.RequirePart<HeatSelfOnFreeze>(false);
			gameObject.RequirePart<ReflectProjectiles>(false);
			gameObject.SetIntProperty("CherubimLock", 1, false);
			gameObject.Brain.Wanders = false;
			gameObject.Brain.WandersRandomly = false;
			gameObject.Brain.Allegiance.Clear();
			gameObject.Brain.Allegiance.Add("Cherubim", 100);
			CherubimSpawner.ReplaceDescription(gameObject, CherubimSpawner.BaseDescription, text);
			int i = 0;
			int num = Math.Min(list.Count, 6);
			while (i < num)
			{
				string text2 = list.RemoveRandomElement<string>(seededRandomGenerator);
				CherubimSpawner.BestowElement(gameObject, text2, false);
				i++;
			}
			gameObject.Render.ColorString = (gameObject.Render.TileColor = "&W");
			gameObject.Render.DetailColor = "m";
			E.ReplacementObject = gameObject;
			return base.HandleEvent(E);
		}
	}
}
