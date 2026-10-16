using System;

namespace XRL.World.Parts
{
	// Token: 0x02001126 RID: 4390
	[Serializable]
	public abstract class IMeleeModification : IModification
	{
		// Token: 0x0600ADB7 RID: 44471 RVA: 0x00404C3D File Offset: 0x00402E3D
		public IMeleeModification()
		{
		}

		// Token: 0x0600ADB8 RID: 44472 RVA: 0x00404C45 File Offset: 0x00402E45
		public IMeleeModification(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600ADB9 RID: 44473 RVA: 0x00404C4E File Offset: 0x00402E4E
		public override bool ModificationApplicable(GameObject obj)
		{
			return !obj.HasPart<MissileWeapon>() && !obj.GetBlueprint(true).InheritsFrom("BaseMissileWeapon");
		}
	}
}
