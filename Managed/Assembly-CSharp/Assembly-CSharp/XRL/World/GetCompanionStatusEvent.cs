using System;
using System.Collections.Generic;
using System.Linq;

namespace XRL.World
{
	// Token: 0x02000735 RID: 1845
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class GetCompanionStatusEvent : PooledEvent<GetCompanionStatusEvent>
	{
		// Token: 0x06004C68 RID: 19560 RVA: 0x00179C02 File Offset: 0x00177E02
		public override int GetCascadeLevel()
		{
			return GetCompanionStatusEvent.CascadeLevel;
		}

		// Token: 0x06004C69 RID: 19561 RVA: 0x00179C09 File Offset: 0x00177E09
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004C6A RID: 19562 RVA: 0x00179C12 File Offset: 0x00177E12
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.ForLeader = null;
			this.Status.Clear();
		}

		// Token: 0x06004C6B RID: 19563 RVA: 0x00179C33 File Offset: 0x00177E33
		public void AddStatus(string Description, int Priority = 0)
		{
			this.Status[Description] = Priority;
		}

		// Token: 0x06004C6C RID: 19564 RVA: 0x00179C44 File Offset: 0x00177E44
		public static string GetFor(GameObject Object, GameObject ForLeader)
		{
			if (GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetCompanionStatusEvent>.ID, GetCompanionStatusEvent.CascadeLevel))
			{
				GetCompanionStatusEvent E = PooledEvent<GetCompanionStatusEvent>.FromPool();
				E.Object = Object;
				E.ForLeader = ForLeader;
				E.Status.Clear();
				Object.HandleEvent(E);
				if (E.Status.Count == 1)
				{
					using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = E.Status.Keys.GetEnumerator())
					{
						if (!enumerator.MoveNext())
						{
							goto IL_0120;
						}
						return enumerator.Current;
					}
				}
				if (E.Status.Count > 0)
				{
					return string.Join(", ", (from d in E.Status.Keys
						orderby E.Status[d], d
						select d).ToArray<string>());
				}
			}
			IL_0120:
			return null;
		}

		// Token: 0x04002500 RID: 9472
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002501 RID: 9473
		public GameObject Object;

		// Token: 0x04002502 RID: 9474
		public GameObject ForLeader;

		// Token: 0x04002503 RID: 9475
		public Dictionary<string, int> Status = new Dictionary<string, int>();
	}
}
