using System;
using XRL.World.Capabilities;

namespace XRL.World
{
	// Token: 0x02000786 RID: 1926
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetScanTypeEvent : PooledEvent<GetScanTypeEvent>
	{
		// Token: 0x06004EE4 RID: 20196 RVA: 0x00181E09 File Offset: 0x00180009
		public override int GetCascadeLevel()
		{
			return GetScanTypeEvent.CascadeLevel;
		}

		// Token: 0x06004EE5 RID: 20197 RVA: 0x00181E10 File Offset: 0x00180010
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004EE6 RID: 20198 RVA: 0x00181E19 File Offset: 0x00180019
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.ScanType = Scanning.Scan.Bio;
		}

		// Token: 0x06004EE7 RID: 20199 RVA: 0x00181E30 File Offset: 0x00180030
		public static Scanning.Scan GetFor(GameObject Object)
		{
			Scanning.Scan scan = (Object.IsAlive ? Scanning.Scan.Bio : Scanning.Scan.Structure);
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetScanType"))
			{
				Event @event = Event.New("GetScanType", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("ScanType", scan);
				flag = Object.FireEvent(@event);
				scan = @event.GetParameter<Scanning.Scan>("ScanType");
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetScanTypeEvent>.ID, GetScanTypeEvent.CascadeLevel))
			{
				GetScanTypeEvent getScanTypeEvent = PooledEvent<GetScanTypeEvent>.FromPool();
				getScanTypeEvent.Object = Object;
				getScanTypeEvent.ScanType = scan;
				Object.HandleEvent(getScanTypeEvent);
				scan = getScanTypeEvent.ScanType;
			}
			return scan;
		}

		// Token: 0x040026A3 RID: 9891
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040026A4 RID: 9892
		public GameObject Object;

		// Token: 0x040026A5 RID: 9893
		public Scanning.Scan ScanType;
	}
}
