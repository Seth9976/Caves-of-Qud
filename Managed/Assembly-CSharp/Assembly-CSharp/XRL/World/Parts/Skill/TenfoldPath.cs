using System;
using System.Linq;
using XRL.Wish;
using XRL.World.Skills;

namespace XRL.World.Parts.Skill
{
	/// This part is not used in the base game.
	// Token: 0x020013B4 RID: 5044
	[HasWishCommand]
	[Serializable]
	public class TenfoldPath : BaseInitiatorySkill
	{
		// Token: 0x0600C2C1 RID: 49857 RVA: 0x004720A0 File Offset: 0x004702A0
		[WishCommand(null, null, Command = "tenfold")]
		public static bool HandleTenfoldPathInitiationWish()
		{
			if (The.Player.HasSkill("TenfoldPath"))
			{
				PowerEntry powerEntry = SkillFactory.Factory.SkillByClass["TenfoldPath"].Powers.Values.FirstOrDefault<PowerEntry>((PowerEntry p) => !The.Player.HasSkill(p.Class) && p.MeetsRequirements(The.Player, false));
				if (powerEntry != null)
				{
					The.Player.AddSkill(powerEntry.Class, null, null);
				}
			}
			else
			{
				The.Player.AddSkill("TenfoldPath", null, null);
			}
			return true;
		}
	}
}
