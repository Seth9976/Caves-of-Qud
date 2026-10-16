using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013A6 RID: 5030
	[Serializable]
	public class Tactics_Hurdle : BaseSkill
	{
		// Token: 0x0600C271 RID: 49777 RVA: 0x00470892 File Offset: 0x0046EA92
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			if (!Object.HasSkill("Tactics_Run"))
			{
				Object.AddSkill("Tactics_Run", null, null);
			}
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C272 RID: 49778 RVA: 0x004708B7 File Offset: 0x0046EAB7
		public override void Initialize()
		{
			base.Initialize();
			Run.SyncAbility(this.ParentObject, false);
		}
	}
}
