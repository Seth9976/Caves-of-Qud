using System;

namespace XRL.World.Parts
{
	// Token: 0x02001100 RID: 4352
	[Serializable]
	public class AIStopShootingImmuneTargets : AIBehaviorPart
	{
		// Token: 0x0600AC27 RID: 44071 RVA: 0x003F177A File Offset: 0x003EF97A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIWantUseWeaponEvent>.ID || ID == PooledEvent<NotifyTargetImmuneEvent>.ID;
		}

		// Token: 0x0600AC28 RID: 44072 RVA: 0x003F1798 File Offset: 0x003EF998
		public override bool HandleEvent(AIWantUseWeaponEvent E)
		{
			return (E.Object != this.ParentObject || !GameObject.Validate(E.Target) || !E.Target.HasID || this.ParentObject.GetIntProperty(this.ImmunityKey(E.Target), 0) <= 0) && base.HandleEvent(E);
		}

		// Token: 0x0600AC29 RID: 44073 RVA: 0x003F17F1 File Offset: 0x003EF9F1
		public override bool HandleEvent(NotifyTargetImmuneEvent E)
		{
			if (E.Weapon == this.ParentObject && GameObject.Validate(E.Target))
			{
				this.ParentObject.SetIntProperty(this.ImmunityKey(E.Target), 1, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC2A RID: 44074 RVA: 0x003F182F File Offset: 0x003EFA2F
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AC2B RID: 44075 RVA: 0x003F1832 File Offset: 0x003EFA32
		private string ImmunityKey(GameObject Target)
		{
			return "TargetImmune" + Target.ID;
		}
	}
}
