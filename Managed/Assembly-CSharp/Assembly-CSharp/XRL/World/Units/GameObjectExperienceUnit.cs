using System;
using XRL.World.Parts;

namespace XRL.World.Units
{
	// Token: 0x020009AB RID: 2475
	[Serializable]
	public class GameObjectExperienceUnit : GameObjectUnit
	{
		// Token: 0x06006AD0 RID: 27344 RVA: 0x00226720 File Offset: 0x00224920
		public override void Apply(GameObject Object)
		{
			int level = Object.Level;
			int num = this.Experience;
			if (this.Levels > 0)
			{
				num += Leveler.GetXPForLevel(level + this.Levels);
				num -= Leveler.GetXPForLevel(level);
			}
			Object.AwardXP(num, -1, 0, int.MaxValue, null, null, null, Object, null, null);
		}

		// Token: 0x06006AD1 RID: 27345 RVA: 0x00226772 File Offset: 0x00224972
		public override void Reset()
		{
			base.Reset();
			this.Experience = 0;
			this.Levels = 0;
		}

		// Token: 0x06006AD2 RID: 27346 RVA: 0x00226788 File Offset: 0x00224988
		public override string GetDescription(bool Inscription = false)
		{
			if (this.Levels > 0)
			{
				return "+" + this.Levels + " levels";
			}
			return "+" + this.Experience + " experience";
		}

		// Token: 0x040030E9 RID: 12521
		public int Experience;

		// Token: 0x040030EA RID: 12522
		public int Levels;
	}
}
