using System;
using System.Collections.Generic;
using XRL.World;

namespace XRL.CharacterCreation
{
	// Token: 0x02000578 RID: 1400
	public class ScholarSkills : CustomChargenSandbox, ICustomChargenClass
	{
		// Token: 0x06003C6A RID: 15466 RVA: 0x001275FC File Offset: 0x001257FC
		public void BuildCharacterBody(GameObject body)
		{
			List<string> list = new List<string>(2);
			for (int i = 0; i < 2; i++)
			{
				bool flag = false;
				string text = "";
				while (!flag)
				{
					text = new List<string>(7) { "Survival_PlainsSurvival", "Survival_SaltmarshSurvival", "Survival_MountainsSurvival", "Survival_DesertCanyonSurvival", "Survival_JungleSurvival", "Survival_SaltDesertSurvival", "Survival_RuinsSurvival" }.GetRandomElement<string>(null);
					if (!list.CleanContains(text))
					{
						flag = true;
					}
				}
				list.Add(text);
				base.AddSkill(body, text);
			}
		}

		// Token: 0x06003C6B RID: 15467 RVA: 0x001276A3 File Offset: 0x001258A3
		public IEnumerable<string> GetChargenInfo()
		{
			yield return "Wilderness Lore: Random";
			yield return "Wilderness Lore: Random";
			yield break;
		}
	}
}
