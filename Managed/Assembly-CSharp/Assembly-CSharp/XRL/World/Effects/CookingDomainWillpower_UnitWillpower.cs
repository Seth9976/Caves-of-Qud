using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D19 RID: 3353
	[Serializable]
	public class CookingDomainWillpower_UnitWillpower : ProceduralCookingEffectUnit
	{
		// Token: 0x06008400 RID: 33792 RVA: 0x002FBD8F File Offset: 0x002F9F8F
		public override string GetDescription()
		{
			return this.Bonus.Signed(false) + " Willpower";
		}

		// Token: 0x06008401 RID: 33793 RVA: 0x002FBDA7 File Offset: 0x002F9FA7
		public override string GetTemplatedDescription()
		{
			return "+4 Willpower";
		}

		// Token: 0x06008402 RID: 33794 RVA: 0x002FBDAE File Offset: 0x002F9FAE
		public override void Init(GameObject target)
		{
			this.Bonus = 4;
		}

		// Token: 0x06008403 RID: 33795 RVA: 0x002FBDB7 File Offset: 0x002F9FB7
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.Statistics["Willpower"].Bonus += this.Bonus;
		}

		// Token: 0x06008404 RID: 33796 RVA: 0x002FBDDB File Offset: 0x002F9FDB
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.Statistics["Willpower"].Bonus -= this.Bonus;
		}

		// Token: 0x04003970 RID: 14704
		public int Bonus;
	}
}
