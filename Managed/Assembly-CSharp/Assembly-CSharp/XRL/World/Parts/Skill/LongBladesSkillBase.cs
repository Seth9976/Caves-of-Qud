using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001350 RID: 4944
	public abstract class LongBladesSkillBase : BaseSkill
	{
		// Token: 0x0600C0AE RID: 49326 RVA: 0x004684CA File Offset: 0x004666CA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<PartSupportEvent>.ID;
		}

		// Token: 0x0600C0AF RID: 49327 RVA: 0x004684E0 File Offset: 0x004666E0
		public override bool HandleEvent(PartSupportEvent E)
		{
			return (E.Skip == this || !(E.Type == "LongBladesCore")) && base.HandleEvent(E);
		}

		// Token: 0x0600C0B0 RID: 49328 RVA: 0x00468506 File Offset: 0x00466706
		public override bool AddSkill(GameObject Object)
		{
			Object.RequirePart<LongBladesCore>(false);
			return base.AddSkill(Object);
		}

		// Token: 0x0600C0B1 RID: 49329 RVA: 0x00468517 File Offset: 0x00466717
		public override bool RemoveSkill(GameObject Object)
		{
			NeedPartSupportEvent.Send(Object, "LongBladesCore", this);
			return base.RemoveSkill(Object);
		}
	}
}
