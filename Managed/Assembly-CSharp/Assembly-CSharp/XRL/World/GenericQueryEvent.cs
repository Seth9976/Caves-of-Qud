using System;

namespace XRL.World
{
	// Token: 0x02000721 RID: 1825
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GenericQueryEvent : PooledEvent<GenericQueryEvent>
	{
		// Token: 0x06004BDE RID: 19422 RVA: 0x0017801B File Offset: 0x0017621B
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x00178024 File Offset: 0x00176224
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Subject = null;
			this.Source = null;
			this.Query = null;
			this.Level = 0;
			this.Result = false;
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x00178058 File Offset: 0x00176258
		public static bool Check(GameObject Object, string Query, GameObject Subject = null, GameObject Source = null, int Level = 0, bool BaseResult = false)
		{
			GenericQueryEvent genericQueryEvent = PooledEvent<GenericQueryEvent>.FromPool();
			genericQueryEvent.Object = Object;
			genericQueryEvent.Subject = Subject;
			genericQueryEvent.Source = Source;
			genericQueryEvent.Query = Query;
			genericQueryEvent.Level = Level;
			genericQueryEvent.Result = BaseResult;
			Object.HandleEvent(genericQueryEvent);
			The.Game.HandleEvent(genericQueryEvent);
			return genericQueryEvent.Result;
		}

		// Token: 0x040024A9 RID: 9385
		public GameObject Object;

		// Token: 0x040024AA RID: 9386
		public GameObject Subject;

		// Token: 0x040024AB RID: 9387
		public GameObject Source;

		// Token: 0x040024AC RID: 9388
		public string Query;

		// Token: 0x040024AD RID: 9389
		public int Level;

		// Token: 0x040024AE RID: 9390
		public bool Result;
	}
}
