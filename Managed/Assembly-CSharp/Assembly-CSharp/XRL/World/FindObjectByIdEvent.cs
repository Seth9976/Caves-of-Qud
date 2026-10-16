using System;

namespace XRL.World
{
	// Token: 0x02000714 RID: 1812
	[GameEvent(Cascade = 271, Cache = MinEvent.Cache.Singleton)]
	public class FindObjectByIdEvent : SingletonEvent<FindObjectByIdEvent>
	{
		// Token: 0x06004B83 RID: 19331 RVA: 0x001771CF File Offset: 0x001753CF
		public override int GetCascadeLevel()
		{
			return FindObjectByIdEvent.CascadeLevel;
		}

		// Token: 0x06004B84 RID: 19332 RVA: 0x001771D6 File Offset: 0x001753D6
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004B85 RID: 19333 RVA: 0x001771DF File Offset: 0x001753DF
		public override void Reset()
		{
			base.Reset();
			this.FindID = null;
			this.FindBaseID = 0;
			this.Object = null;
		}

		// Token: 0x06004B86 RID: 19334 RVA: 0x001771FC File Offset: 0x001753FC
		public static GameObject Find(GameObject From, string ID)
		{
			SingletonEvent<FindObjectByIdEvent>.Instance.FindID = ID;
			SingletonEvent<FindObjectByIdEvent>.Instance.Object = null;
			From.HandleEvent(SingletonEvent<FindObjectByIdEvent>.Instance);
			GameObject @object = SingletonEvent<FindObjectByIdEvent>.Instance.Object;
			SingletonEvent<FindObjectByIdEvent>.Instance.Reset();
			return @object;
		}

		// Token: 0x06004B87 RID: 19335 RVA: 0x00177234 File Offset: 0x00175434
		public static GameObject Find(GameObject From, int ID)
		{
			SingletonEvent<FindObjectByIdEvent>.Instance.FindBaseID = ID;
			SingletonEvent<FindObjectByIdEvent>.Instance.Object = null;
			From.HandleEvent(SingletonEvent<FindObjectByIdEvent>.Instance);
			GameObject @object = SingletonEvent<FindObjectByIdEvent>.Instance.Object;
			SingletonEvent<FindObjectByIdEvent>.Instance.Reset();
			return @object;
		}

		// Token: 0x04002473 RID: 9331
		public new static readonly int CascadeLevel = 271;

		// Token: 0x04002474 RID: 9332
		public string FindID;

		// Token: 0x04002475 RID: 9333
		public int FindBaseID;

		// Token: 0x04002476 RID: 9334
		public GameObject Object;
	}
}
