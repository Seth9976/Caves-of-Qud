using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x0200136C RID: 4972
	[Serializable]
	public class Pistol_SlingAndRun : BaseSkill
	{
		// Token: 0x0600C160 RID: 49504 RVA: 0x0046B9AC File Offset: 0x00469BAC
		public override void Initialize()
		{
			base.Initialize();
			Run.SyncAbility(this.ParentObject, false);
		}
	}
}
