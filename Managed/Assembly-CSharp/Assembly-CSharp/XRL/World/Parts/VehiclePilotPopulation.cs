using System;

namespace XRL.World.Parts
{
	// Token: 0x020012B7 RID: 4791
	[Serializable]
	public class VehiclePilotPopulation : IPart
	{
		// Token: 0x0600BD58 RID: 48472 RVA: 0x00454E5F File Offset: 0x0045305F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDieEvent.ID || ID == ObjectCreatedEvent.ID || ID == PooledEvent<InteriorZoneBuiltEvent>.ID || ID == SingletonEvent<FindObjectByIdEvent>.ID;
		}

		// Token: 0x0600BD59 RID: 48473 RVA: 0x00454E90 File Offset: 0x00453090
		public override bool HandleEvent(BeforeDieEvent E)
		{
			Interior interior;
			Vehicle vehicle;
			if (this.Object != null && this.ParentObject.TryGetPart<Interior>(out interior) && interior.Zone == null && this.ParentObject.TryGetPart<Vehicle>(out vehicle) && vehicle.Pilot == null)
			{
				this.ParentObject.CurrentCell.AddObject(this.Object, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				this.Object = null;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD5A RID: 48474 RVA: 0x00454F08 File Offset: 0x00453108
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			Vehicle vehicle;
			if (this.ParentObject.TryGetPart<Vehicle>(out vehicle))
			{
				if (!this.Blueprint.IsNullOrEmpty())
				{
					this.Object = GameObject.Create(this.Blueprint.GetRandomSubstring(',', false, null));
				}
				else if (!this.Table.IsNullOrEmpty())
				{
					this.Object = PopulationManager.CreateOneFrom(this.Table, null, 0, 0, null, null, null, null, null);
				}
				if (GameObject.Validate(ref this.Object))
				{
					if (!vehicle.CanBePilotedBy(this.Object))
					{
						vehicle.OwnerID = this.Object.ID;
					}
					vehicle.Pilot = this.Object;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD5B RID: 48475 RVA: 0x00454FB8 File Offset: 0x004531B8
		public override bool HandleEvent(InteriorZoneBuiltEvent E)
		{
			GameObject firstObjectWithPart = E.Zone.GetFirstObjectWithPart("VehicleSeat");
			if (firstObjectWithPart != null && GameObject.Validate(ref this.Object))
			{
				firstObjectWithPart.CurrentCell.AddObject(this.Object, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				Chair part = firstObjectWithPart.GetPart<Chair>();
				if (part != null)
				{
					part.SitDown(this.Object, E);
				}
				this.Object = null;
			}
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD5C RID: 48476 RVA: 0x0045503A File Offset: 0x0045323A
		public override bool HandleEvent(FindObjectByIdEvent E)
		{
			if (GameObject.Validate(ref this.Object) && this.Object.IDMatch(E.FindID))
			{
				E.Object = this.Object;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004BE3 RID: 19427
		public string Blueprint;

		// Token: 0x04004BE4 RID: 19428
		public string Table;

		// Token: 0x04004BE5 RID: 19429
		public GameObject Object;
	}
}
