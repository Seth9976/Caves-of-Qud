using System;

namespace XRL.World.Parts
{
	// Token: 0x020011AC RID: 4524
	[Serializable]
	public class PreachOnPartyCombat : IPart
	{
		// Token: 0x0600B3C0 RID: 46016 RVA: 0x004192DD File Offset: 0x004174DD
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600B3C1 RID: 46017 RVA: 0x004192E0 File Offset: 0x004174E0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			Registrar.Register("CanPreach");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B3C2 RID: 46018 RVA: 0x00419300 File Offset: 0x00417500
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanPreach")
			{
				return this.ParentObject.PartyLeader != null && this.ParentObject.PartyLeader.Brain != null && this.ParentObject.PartyLeader.Brain.Target != null;
			}
			if (E.ID == "BeginTakeAction" && this.ParentObject.PartyLeader != null && this.ParentObject.PartyLeader.Brain != null && this.leaderInCombat != (this.ParentObject.PartyLeader.Brain.Target != null))
			{
				if (!this.leaderInCombat && this.ParentObject.HasPart<Preacher>())
				{
					this.ParentObject.GetPart<Preacher>().PreacherHomily(false);
				}
				this.leaderInCombat = !this.leaderInCombat;
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004885 RID: 18565
		public bool leaderInCombat;
	}
}
