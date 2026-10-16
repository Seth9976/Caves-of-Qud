using System;
using System.Collections.Generic;
using XRL.Collections;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001370 RID: 4976
	[Serializable]
	public class Rifle_DisorientingFire : BaseSkill
	{
		// Token: 0x0600C169 RID: 49513 RVA: 0x0046BB1C File Offset: 0x00469D1C
		public static bool MeetsCriteria(GameObject GO)
		{
			if (!GameObject.Validate(ref GO))
			{
				return false;
			}
			if (!GO.IsCombat)
			{
				return false;
			}
			if (GO.Brain != null)
			{
				Cell currentCell = GO.CurrentCell;
				List<GameObject> list = currentCell.ParentZone.FastFloodVisibility(currentCell.X, currentCell.Y, 80, "Brain", GO, null, null);
				for (int i = 0; i < list.Count; i++)
				{
					GameObject gameObject = list[i];
					if (gameObject != GO)
					{
						Brain brain = gameObject.Brain;
						foreach (KeyValuePair<string, int> keyValuePair in GO.Brain.Allegiance)
						{
							string text;
							int num;
							keyValuePair.Deconstruct(out text, out num);
							string text2 = text;
							int num2 = num;
							using (StringMap<int>.Enumerator enumerator2 = brain.Allegiance.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									keyValuePair = enumerator2.Current;
									keyValuePair.Deconstruct(out text, out num);
									string text3 = text;
									int num3 = num;
									if (text2 == text3 && num2 > 0 && num3 > 0)
									{
										return false;
									}
								}
							}
						}
					}
				}
			}
			return true;
		}
	}
}
