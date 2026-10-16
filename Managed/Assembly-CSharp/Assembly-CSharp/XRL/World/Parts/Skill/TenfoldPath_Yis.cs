using System;
using XRL.UI;

namespace XRL.World.Parts.Skill
{
	/// This part is not used in the base game.
	// Token: 0x020013B7 RID: 5047
	[Serializable]
	public class TenfoldPath_Yis : BaseInitiatorySkill
	{
		// Token: 0x0600C2CA RID: 49866 RVA: 0x0047229B File Offset: 0x0047049B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetLevelUpSkillPointsEvent>.ID;
		}

		// Token: 0x0600C2CB RID: 49867 RVA: 0x004722B1 File Offset: 0x004704B1
		public override bool HandleEvent(GetLevelUpSkillPointsEvent E)
		{
			if (E.Actor == this.ParentObject)
			{
				E.Amount += TenfoldPath_Yis.AMOUNT_PER_LEVEL;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2CC RID: 49868 RVA: 0x004722DC File Offset: 0x004704DC
		public override bool AddSkill(GameObject Object)
		{
			int num = TenfoldPath_Yis.AMOUNT_PER_LEVEL * (Object.Stat("Level", 0) - 1);
			if (num > 0)
			{
				Object.GetStat("SP").BaseValue += num;
				if (Object.IsPlayer())
				{
					Popup.Show(string.Concat(new string[]
					{
						"You gain ",
						num.ToString(),
						" skill ",
						(num == 1) ? "point" : "points",
						"."
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			return base.AddSkill(Object);
		}

		// Token: 0x04004DA8 RID: 19880
		public static readonly int AMOUNT_PER_LEVEL = 30;
	}
}
