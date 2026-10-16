using System;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02001215 RID: 4629
	[Serializable]
	public class RevealObservationOnLook : IPart
	{
		// Token: 0x0600B7FB RID: 47099 RVA: 0x0042EBBF File Offset: 0x0042CDBF
		public RevealObservationOnLook()
		{
		}

		// Token: 0x0600B7FC RID: 47100 RVA: 0x0042EBC7 File Offset: 0x0042CDC7
		public RevealObservationOnLook(string _ObservationId)
		{
			this.ObservationId = _ObservationId;
		}

		// Token: 0x0600B7FD RID: 47101 RVA: 0x0042EBD6 File Offset: 0x0042CDD6
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600B7FE RID: 47102 RVA: 0x0042EBD9 File Offset: 0x0042CDD9
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterLookedAt");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B7FF RID: 47103 RVA: 0x0042EBF0 File Offset: 0x0042CDF0
		public override bool FireEvent(Event E)
		{
			if (!(E.ID == "AfterLookedAt"))
			{
				return base.FireEvent(E);
			}
			if (this.bLookedAt)
			{
				return true;
			}
			this.bLookedAt = true;
			if (this.ObservationId == null)
			{
				if (!this.ParentObject.HasPart<AddObservation>())
				{
					return true;
				}
				AddObservation part = this.ParentObject.GetPart<AddObservation>();
				this.ObservationId = part.ID;
			}
			JournalAPI.RevealObservation(this.ObservationId, true);
			return true;
		}

		// Token: 0x04004A07 RID: 18951
		public string ObservationId;

		// Token: 0x04004A08 RID: 18952
		public bool bLookedAt;
	}
}
