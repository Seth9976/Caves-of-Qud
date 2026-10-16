using System;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001380 RID: 4992
	[Serializable]
	public class Discipline_MindOverBody : BaseSkill
	{
		// Token: 0x0600C1B7 RID: 49591 RVA: 0x0046CC5E File Offset: 0x0046AE5E
		public override bool AddSkill(GameObject GO)
		{
			GO.RemoveEffect<Famished>();
			return true;
		}
	}
}
