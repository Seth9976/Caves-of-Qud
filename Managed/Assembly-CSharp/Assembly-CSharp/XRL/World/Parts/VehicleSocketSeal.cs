using System;

namespace XRL.World.Parts
{
	// Token: 0x020011DE RID: 4574
	[Serializable]
	public class VehicleSocketSeal : IPart
	{
		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x0600B657 RID: 46679 RVA: 0x00428CE0 File Offset: 0x00426EE0
		public Vehicle Vehicle
		{
			get
			{
				Vehicle vehicle;
				if ((vehicle = this._Vehicle) == null)
				{
					vehicle = (this._Vehicle = this.ParentObject.GetPart<Vehicle>());
				}
				return vehicle;
			}
		}

		// Token: 0x0600B658 RID: 46680 RVA: 0x00428D0B File Offset: 0x00426F0B
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B659 RID: 46681 RVA: 0x00428D0E File Offset: 0x00426F0E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeReplaceCell");
		}

		// Token: 0x0600B65A RID: 46682 RVA: 0x00428D1C File Offset: 0x00426F1C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeReplaceCell")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Actor");
				if (!this.Vehicle.IsOwnedBy(gameObjectParameter) && (!this.Vehicle.PilotID.IsNullOrEmpty() || this.Vehicle.Autonomous) && !this.ParentObject.InSamePartyAs(gameObjectParameter))
				{
					return gameObjectParameter.IsPlayer() && gameObjectParameter.ShowFailure(this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + "'s socket is sealed.");
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004992 RID: 18834
		[NonSerialized]
		private Vehicle _Vehicle;
	}
}
