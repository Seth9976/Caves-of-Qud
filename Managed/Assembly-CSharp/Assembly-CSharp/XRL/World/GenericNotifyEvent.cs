using System;

namespace XRL.World
{
	// Token: 0x02000720 RID: 1824
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GenericNotifyEvent : PooledEvent<GenericNotifyEvent>
	{
		// Token: 0x06004BDA RID: 19418 RVA: 0x00177F91 File Offset: 0x00176191
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004BDB RID: 19419 RVA: 0x00177F9A File Offset: 0x0017619A
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Subject = null;
			this.Source = null;
			this.Notify = null;
			this.Level = 0;
		}

		// Token: 0x06004BDC RID: 19420 RVA: 0x00177FC8 File Offset: 0x001761C8
		public static void Send(GameObject Object, string Notify, GameObject Subject = null, GameObject Source = null, int Level = 0)
		{
			GenericNotifyEvent genericNotifyEvent = PooledEvent<GenericNotifyEvent>.FromPool();
			genericNotifyEvent.Object = Object;
			genericNotifyEvent.Subject = Subject;
			genericNotifyEvent.Source = Source;
			genericNotifyEvent.Notify = Notify;
			genericNotifyEvent.Level = Level;
			Object.HandleEvent(genericNotifyEvent);
			The.Game.HandleEvent(genericNotifyEvent);
		}

		// Token: 0x040024A4 RID: 9380
		public GameObject Object;

		// Token: 0x040024A5 RID: 9381
		public GameObject Subject;

		// Token: 0x040024A6 RID: 9382
		public GameObject Source;

		// Token: 0x040024A7 RID: 9383
		public string Notify;

		// Token: 0x040024A8 RID: 9384
		public int Level;
	}
}
