using System;
using System.Collections.Generic;
using XRL.World.ZoneParts;

namespace XRL.World.Parts
{
	// Token: 0x020011B9 RID: 4537
	[Serializable]
	public class BroadcastPowerField : IZonePart
	{
		// Token: 0x0600B41E RID: 46110 RVA: 0x0041B496 File Offset: 0x00419696
		public void ValidateTransmitters()
		{
			if (this.transmitters == null)
			{
				return;
			}
			this.transmitters.RemoveAll((GameObject o) => o == null || !o.IsValid() || o.CurrentZone != this.ParentZone);
		}

		// Token: 0x0600B41F RID: 46111 RVA: 0x0041B4B9 File Offset: 0x004196B9
		public void RegisterTransmitter(GameObject transmitter)
		{
			if (this.transmitters == null)
			{
				this.transmitters = new List<GameObject>();
			}
			if (!this.transmitters.Contains(transmitter))
			{
				this.transmitters.Add(transmitter);
			}
		}

		// Token: 0x0600B420 RID: 46112 RVA: 0x0041B4E8 File Offset: 0x004196E8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CollectBroadcastChargeEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x0600B421 RID: 46113 RVA: 0x0041B506 File Offset: 0x00419706
		public override bool HandleEvent(EndTurnEvent ev)
		{
			this.ValidateTransmitters();
			return true;
		}

		// Token: 0x0600B422 RID: 46114 RVA: 0x0041B510 File Offset: 0x00419710
		public override bool HandleEvent(CollectBroadcastChargeEvent ev)
		{
			if (this.transmitters == null)
			{
				return true;
			}
			foreach (GameObject gameObject in this.transmitters)
			{
				BroadcastPowerTransmitter part = gameObject.GetPart<BroadcastPowerTransmitter>();
				if (part != null && !part.HandleEvent(ev))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400489B RID: 18587
		[NonSerialized]
		private List<GameObject> transmitters;
	}
}
