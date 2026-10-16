using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013A9 RID: 5033
	[Serializable]
	public class Tactics_Run : BaseSkill
	{
		// Token: 0x0600C286 RID: 49798 RVA: 0x004716AC File Offset: 0x0046F8AC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetRunningBehaviorEvent>.ID || ID == PooledEvent<PartSupportEvent>.ID;
		}

		// Token: 0x0600C287 RID: 49799 RVA: 0x004716CA File Offset: 0x0046F8CA
		public override bool HandleEvent(PartSupportEvent E)
		{
			return (E.Skip == this || !(E.Type == "Run")) && base.HandleEvent(E);
		}

		// Token: 0x0600C288 RID: 49800 RVA: 0x004716F0 File Offset: 0x0046F8F0
		public override bool HandleEvent(GetRunningBehaviorEvent E)
		{
			if (E.Priority < 10)
			{
				int @for = GetSprintDurationEvent.GetFor(E.Actor, 10, 0, 0, 0, 0, E.Stats);
				if (@for > 0)
				{
					E.AbilityName = "Sprint";
					E.Verb = "sprint";
					E.EffectDisplayName = "sprinting";
					E.EffectMessageName = "sprinting";
					E.EffectDuration = @for;
					E.SpringingEffective = true;
					E.Priority = 10;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C289 RID: 49801 RVA: 0x0047176C File Offset: 0x0046F96C
		public override bool AddSkill(GameObject GO)
		{
			GO.RequirePart<Run>(false);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C28A RID: 49802 RVA: 0x0047177D File Offset: 0x0046F97D
		public override bool RemoveSkill(GameObject GO)
		{
			NeedPartSupportEvent.Send(GO, "Run", this);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C28B RID: 49803 RVA: 0x00471792 File Offset: 0x0046F992
		public override void Initialize()
		{
			base.Initialize();
			Run.SyncAbility(this.ParentObject, false);
		}
	}
}
