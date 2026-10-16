using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200136F RID: 4975
	[Serializable]
	public class Rifle_BeaconFire : BaseSkill
	{
		// Token: 0x0600C167 RID: 49511 RVA: 0x0046BA2C File Offset: 0x00469C2C
		public static bool MeetsCriteria(GameObject GO)
		{
			if (!GameObject.Validate(ref GO))
			{
				return false;
			}
			if (!GO.IsVisible())
			{
				return false;
			}
			if (GO.IsAflame())
			{
				return true;
			}
			if (GO.HasEffect<Luminous>())
			{
				return true;
			}
			if (GO.HasPart<Raycat>())
			{
				return true;
			}
			if (GO.HasEffect<LiquidCovered>())
			{
				foreach (LiquidCovered liquidCovered in GO.YieldEffects<LiquidCovered>())
				{
					if (liquidCovered.Liquid != null)
					{
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator2 = liquidCovered.Liquid.ComponentLiquids.Keys.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								if (LiquidVolume.GetLiquid(enumerator2.Current).Glows)
								{
									return true;
								}
							}
						}
					}
				}
				return false;
			}
			return false;
		}
	}
}
