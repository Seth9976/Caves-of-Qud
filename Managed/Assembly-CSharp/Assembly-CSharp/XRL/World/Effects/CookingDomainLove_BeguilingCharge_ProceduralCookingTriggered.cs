using System;
using XRL.Rules;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000CC3 RID: 3267
	[Serializable]
	public class CookingDomainLove_BeguilingCharge_ProceduralCookingTriggeredAction : ProceduralCookingTriggeredAction
	{
		// Token: 0x0600824B RID: 33355 RVA: 0x002F781E File Offset: 0x002F5A1E
		public override void Init(GameObject target)
		{
			this.Tier = Stat.Random(7, 8);
			this.TargetID = ((target != null) ? target.ID : null);
			this.AppliedBonus = 0;
			base.Init(target);
		}

		// Token: 0x0600824C RID: 33356 RVA: 0x002F784D File Offset: 0x002F5A4D
		public override string GetDescription()
		{
			return "@they beguile a creature as per Beguiling at rank 7-8 for the duration of this effect.";
		}

		// Token: 0x0600824D RID: 33357 RVA: 0x002F7854 File Offset: 0x002F5A54
		public override string GetNotification()
		{
			return "@they feel the swell of love inside.";
		}

		// Token: 0x0600824E RID: 33358 RVA: 0x002F785B File Offset: 0x002F5A5B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCompanionLimitEvent>.ID;
		}

		// Token: 0x0600824F RID: 33359 RVA: 0x002F7874 File Offset: 0x002F5A74
		public override bool HandleEvent(GetCompanionLimitEvent E)
		{
			if (E.Means == "Beguiling")
			{
				GameObject actor = E.Actor;
				if (actor != null && actor.IDMatch(this.TargetID))
				{
					E.Limit += this.AppliedBonus;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008250 RID: 33360 RVA: 0x002F78C7 File Offset: 0x002F5AC7
		public override void Apply(GameObject go)
		{
			if (go.IsPlayer())
			{
				this.AppliedBonus++;
				if (!Beguiling.Cast(go, null, null, this.Tier))
				{
					this.AppliedBonus--;
				}
			}
		}

		// Token: 0x06008251 RID: 33361 RVA: 0x002F78FD File Offset: 0x002F5AFD
		public override void Remove(GameObject go)
		{
			this.AppliedBonus = 0;
			Beguiling.SyncTarget(go, null, false);
			base.Remove(go);
		}

		// Token: 0x04003936 RID: 14646
		public int Tier;

		// Token: 0x04003937 RID: 14647
		public int AppliedBonus;

		// Token: 0x04003938 RID: 14648
		public string TargetID;
	}
}
