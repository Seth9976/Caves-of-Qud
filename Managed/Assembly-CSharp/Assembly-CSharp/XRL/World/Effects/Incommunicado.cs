using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D54 RID: 3412
	[Serializable]
	public class Incommunicado : Effect
	{
		// Token: 0x060086D4 RID: 34516 RVA: 0x00306F90 File Offset: 0x00305190
		public Incommunicado()
		{
			this.DisplayName = "incommunicado";
			this.Duration = 1;
		}

		// Token: 0x060086D5 RID: 34517 RVA: 0x00306FAA File Offset: 0x003051AA
		public override string GetDetails()
		{
			return "Doesn't know how to rejoin leader.";
		}

		// Token: 0x060086D6 RID: 34518 RVA: 0x00306FB1 File Offset: 0x003051B1
		public override int GetEffectType()
		{
			return 1;
		}

		// Token: 0x060086D7 RID: 34519 RVA: 0x00306FB4 File Offset: 0x003051B4
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x060086D8 RID: 34520 RVA: 0x00306FB7 File Offset: 0x003051B7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<JoinPartyLeaderPossibleEvent>.ID;
		}

		// Token: 0x060086D9 RID: 34521 RVA: 0x00306FD8 File Offset: 0x003051D8
		public override bool HandleEvent(EndTurnEvent E)
		{
			GameObject partyLeader = base.Object.PartyLeader;
			if (partyLeader == null || base.Object.InSameZone(partyLeader))
			{
				base.Object.RemoveEffect(this, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060086DA RID: 34522 RVA: 0x00307018 File Offset: 0x00305218
		public override bool HandleEvent(JoinPartyLeaderPossibleEvent E)
		{
			return E.Result = false;
		}

		// Token: 0x060086DB RID: 34523 RVA: 0x00307030 File Offset: 0x00305230
		public override bool Apply(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				return false;
			}
			if (Object.HasEffect<Incommunicado>())
			{
				return false;
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_bewilderment", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return true;
		}

		// Token: 0x060086DC RID: 34524 RVA: 0x0030707F File Offset: 0x0030527F
		public override void Remove(GameObject Object)
		{
		}
	}
}
