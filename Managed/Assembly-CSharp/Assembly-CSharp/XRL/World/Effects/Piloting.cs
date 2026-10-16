using System;
using System.Collections.Generic;
using XRL.World.AI;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D70 RID: 3440
	[Serializable]
	public class Piloting : Effect
	{
		// Token: 0x0600883C RID: 34876 RVA: 0x0030CF2D File Offset: 0x0030B12D
		public Piloting()
		{
			this.DisplayName = "{{C|piloting}}";
			this.Duration = 1;
		}

		// Token: 0x0600883D RID: 34877 RVA: 0x0030CF48 File Offset: 0x0030B148
		public Piloting(GameObject Vehicle)
		{
			this.Vehicle = Vehicle;
			this.DisplayName = "{{C|piloting " + Vehicle.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + "}}";
			this.Duration = 1;
		}

		// Token: 0x0600883E RID: 34878 RVA: 0x0030CFA4 File Offset: 0x0030B1A4
		public override bool Apply(GameObject Object)
		{
			if (GameObject.Validate(ref this.Vehicle) && this.Vehicle.Brain != null)
			{
				Brain brain = this.Vehicle.Brain;
				GameObject partyLeader = brain.PartyLeader;
				this.VehicleLeader = ((partyLeader != null) ? partyLeader.Reference() : null);
				brain.PartyLeader = Object.Brain.PartyLeader;
				brain.TakeAllegiance<AllyPilot>(Object);
				brain.Goals.Clear();
				brain.ClearHostileMemory();
				brain.Target = null;
			}
			return base.Apply(Object);
		}

		// Token: 0x0600883F RID: 34879 RVA: 0x0030D028 File Offset: 0x0030B228
		public override void Remove(GameObject Object)
		{
			if (GameObject.Validate(ref this.Vehicle) && this.Vehicle.Brain != null)
			{
				Brain brain = this.Vehicle.Brain;
				GameObjectReference vehicleLeader = this.VehicleLeader;
				brain.PartyLeader = ((vehicleLeader != null) ? vehicleLeader.Object : null);
				brain.RemoveAllegiance<AllyPilot>(Object);
				brain.Goals.Clear();
				brain.ClearHostileMemory();
				brain.Target = null;
			}
			base.Remove(Object);
		}

		// Token: 0x06008840 RID: 34880 RVA: 0x0030D097 File Offset: 0x0030B297
		public void Stop()
		{
			if (GameObject.Validate(ref this.Vehicle))
			{
				this.Vehicle.GetPart<Vehicle>().Pilot = null;
			}
		}

		// Token: 0x06008841 RID: 34881 RVA: 0x0030D0B7 File Offset: 0x0030B2B7
		public override int GetEffectType()
		{
			return 83886208;
		}

		// Token: 0x06008842 RID: 34882 RVA: 0x0030D0BE File Offset: 0x0030B2BE
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008843 RID: 34883 RVA: 0x0030D0C1 File Offset: 0x0030B2C1
		public override string GetDetails()
		{
			return "Can't take actions.";
		}

		// Token: 0x06008844 RID: 34884 RVA: 0x0030D0C8 File Offset: 0x0030B2C8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == LeaveCellEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == TookDamageEvent.ID || ID == BeforeDeathRemovalEvent.ID || ID == GetZoneSuspendabilityEvent.ID || ID == GetZoneFreezabilityEvent.ID;
		}

		// Token: 0x06008845 RID: 34885 RVA: 0x0030D106 File Offset: 0x0030B306
		public override bool HandleEvent(LeaveCellEvent E)
		{
			if (E.Object == base.Object)
			{
				this.Stop();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008846 RID: 34886 RVA: 0x0030D123 File Offset: 0x0030B323
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			return (!GameObject.Validate(ref this.Vehicle) || base.Object.IsPlayer()) && base.Object.RemoveEffect(this, true);
		}

		// Token: 0x06008847 RID: 34887 RVA: 0x0030D14E File Offset: 0x0030B34E
		public override bool HandleEvent(TookDamageEvent E)
		{
			if (E.Object == base.Object)
			{
				this.Stop();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008848 RID: 34888 RVA: 0x0030D16B File Offset: 0x0030B36B
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			this.Stop();
			return base.HandleEvent(E);
		}

		// Token: 0x06008849 RID: 34889 RVA: 0x0030D17A File Offset: 0x0030B37A
		public override bool HandleEvent(GetZoneSuspendabilityEvent E)
		{
			if (GameObject.Validate(ref this.Vehicle) && this.Vehicle.IsPlayer())
			{
				E.Suspendability = Suspendability.InsideInterior;
			}
			return false;
		}

		// Token: 0x0600884A RID: 34890 RVA: 0x0030D19E File Offset: 0x0030B39E
		public override bool HandleEvent(GetZoneFreezabilityEvent E)
		{
			if (GameObject.Validate(ref this.Vehicle) && this.Vehicle.IsPlayer())
			{
				E.Freezability = Freezability.FormerPlayerObject;
			}
			return false;
		}

		// Token: 0x04003A44 RID: 14916
		public GameObject Vehicle;

		// Token: 0x04003A45 RID: 14917
		public string VehicleFactions;

		// Token: 0x04003A46 RID: 14918
		public GameObjectReference VehicleLeader;

		// Token: 0x04003A47 RID: 14919
		public Dictionary<string, int> VehicleFeelings;

		// Token: 0x04003A48 RID: 14920
		public Dictionary<string, int> VehicleMembership;
	}
}
