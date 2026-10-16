using System;
using System.Collections.Generic;

namespace Qud.API
{
	// Token: 0x02000403 RID: 1027
	public static class MuralCategoryHelpers
	{
		// Token: 0x06002688 RID: 9864 RVA: 0x000C5F58 File Offset: 0x000C4158
		public static MuralCategory parseCategory(string value)
		{
			if (value.IsNullOrEmpty())
			{
				return MuralCategory.DoesSomethingRad;
			}
			MuralCategory muralCategory;
			try
			{
				muralCategory = (MuralCategory)Enum.Parse(typeof(MuralCategory), value);
			}
			catch
			{
				MetricsManager.LogError("Unknown hagiographic category: " + value);
				muralCategory = MuralCategory.DoesSomethingRad;
			}
			return muralCategory;
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x000C5FB0 File Offset: 0x000C41B0
		public static MuralWeight parseWeight(string value)
		{
			if (value.IsNullOrEmpty())
			{
				return MuralWeight.Medium;
			}
			MuralWeight muralWeight;
			try
			{
				muralWeight = (MuralWeight)Enum.Parse(typeof(MuralWeight), value);
			}
			catch
			{
				MetricsManager.LogError("Unknown hagiographic category: " + value);
				muralWeight = MuralWeight.Medium;
			}
			return muralWeight;
		}

		// Token: 0x04001353 RID: 4947
		public static Dictionary<MuralCategory, string> muralCategoryToChronologyCategory = new Dictionary<MuralCategory, string>
		{
			{
				MuralCategory.Generic,
				"Notes"
			},
			{
				MuralCategory.IsBorn,
				"Milestones"
			},
			{
				MuralCategory.Dies,
				"Milestones"
			},
			{
				MuralCategory.CrownedSultan,
				"Milestones"
			},
			{
				MuralCategory.CreatesSomething,
				"Discoveries"
			},
			{
				MuralCategory.LearnsSecret,
				"Discoveries"
			},
			{
				MuralCategory.FindsObject,
				"Discoveries"
			},
			{
				MuralCategory.WieldsItemInBattle,
				"Discoveries"
			},
			{
				MuralCategory.VisitsLocation,
				"Travels"
			},
			{
				MuralCategory.HasInspiringExperience,
				"Travels"
			},
			{
				MuralCategory.DoesBureaucracy,
				"Travels"
			},
			{
				MuralCategory.WeirdThingHappens,
				"Happenings"
			},
			{
				MuralCategory.CommitsFolly,
				"Happenings"
			},
			{
				MuralCategory.EnduresHardship,
				"Happenings"
			},
			{
				MuralCategory.BodyExperienceBad,
				"Happenings"
			},
			{
				MuralCategory.BodyExperienceGood,
				"Happenings"
			},
			{
				MuralCategory.BodyExperienceNeutral,
				"Happenings"
			},
			{
				MuralCategory.DoesSomethingRad,
				"Happenings"
			},
			{
				MuralCategory.DoesSomethingHumble,
				"Happenings"
			},
			{
				MuralCategory.DoesSomethingDestructive,
				"Happenings"
			},
			{
				MuralCategory.BecomesLoved,
				"Meetings"
			},
			{
				MuralCategory.Slays,
				"Meetings"
			},
			{
				MuralCategory.Treats,
				"Meetings"
			},
			{
				MuralCategory.MeetsWithCounselors,
				"Meetings"
			},
			{
				MuralCategory.Resists,
				"Meetings"
			},
			{
				MuralCategory.AppeasesBaetyl,
				"Meetings"
			}
		};
	}
}
