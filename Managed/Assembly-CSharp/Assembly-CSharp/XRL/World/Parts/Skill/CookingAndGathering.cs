using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001326 RID: 4902
	[Serializable]
	public class CookingAndGathering : BaseSkill
	{
		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x0600BFBB RID: 49083 RVA: 0x004637AB File Offset: 0x004619AB
		public override int Priority
		{
			get
			{
				return int.MinValue;
			}
		}
	}
}
