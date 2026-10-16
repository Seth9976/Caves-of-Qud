using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World.Parts.Mutation;
using XRL.World.Tinkering;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013C2 RID: 5058
	[Serializable]
	public class Tinkering : BaseSkill
	{
		// Token: 0x0600C32B RID: 49963 RVA: 0x00474234 File Offset: 0x00472434
		public static void LearnNewRecipe(GameObject Actor, int MinTier, int MaxTier)
		{
			int num = (Actor.IsPlayer() ? 3 : 1);
			List<GameObject> list = new List<GameObject>(num);
			List<TinkerData> list2 = new List<TinkerData>(num);
			while (list.Count < num)
			{
				GameObject gameObject = GameObject.Create("DataDisk", 0, 0, null, null, null, Tinkering.LEARN_NEW_RECIPE_CONTEXT, null);
				DataDisk part = gameObject.GetPart<DataDisk>();
				if (part != null && part.Data.Tier >= MinTier && part.Data.Tier <= MaxTier && !list2.CleanContains(part.Data))
				{
					list.Add(gameObject);
					list2.Add(part.Data);
				}
				else
				{
					gameObject.Obliterate(null, false, null);
				}
			}
			GameObject gameObject2;
			if ((Tinkering.IsPlayerOverride || Actor.IsPlayer()) && !Popup.Suppress)
			{
				string[] array = new string[num];
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					array[i] = list[i].DisplayName;
					i++;
				}
				int j;
				for (j = -1; j < 0; j = Popup.PickOption("", "Choose a schematic.", "", "Sounds/UI/ui_notification", array, null, null, null, null, null, null, 0, 60, 0, -1, false, false, false, true, false, null, null))
				{
				}
				gameObject2 = list[j];
			}
			else
			{
				gameObject2 = list[0];
			}
			TinkeringHelpers.CheckMakersMark(gameObject2, Actor, null, null);
			Actor.ReceiveObject(gameObject2, false, Tinkering.LEARN_NEW_RECIPE_CONTEXT);
			if (Actor.IsPlayer())
			{
				Popup.Show("You have a flash of insight and scribe " + gameObject2.an(int.MaxValue, null, null, false, false, false, false, false, false, false, false, false, true, false, null, false, null, false) + ".", null, "sfx_characterMod_tinkerSchematic_learn", true, true, true, true, null);
			}
		}

		// Token: 0x0600C32C RID: 49964 RVA: 0x004743D4 File Offset: 0x004725D4
		public static int GetIdentifyLevel(GameObject who)
		{
			if (who == null)
			{
				return 0;
			}
			int num = 0;
			if (who.HasSkill("Tinkering_GadgetInspector") && !who.HasPart<Dystechnia>())
			{
				num += 3;
				if (who.HasSkill("Tinkering_Tinker2"))
				{
					num += 2;
				}
				if (who.HasSkill("Tinkering_Tinker3"))
				{
					num++;
				}
				int num2 = num * (who.StatMod("Intelligence", 0) * 3) / 100;
				num += GetTinkeringBonusEvent.GetFor(who, null, "Inspect", num, num2, true, false);
			}
			return num;
		}

		// Token: 0x04004DAF RID: 19887
		public static readonly string LEARN_NEW_RECIPE_CONTEXT = "LearnNewRecipe";

		// Token: 0x04004DB0 RID: 19888
		[NonSerialized]
		public static bool IsPlayerOverride = false;
	}
}
