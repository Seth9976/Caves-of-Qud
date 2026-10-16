using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000833 RID: 2099
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class QueryDrawEvent : PooledEvent<QueryDrawEvent>
	{
		// Token: 0x06005372 RID: 21362 RVA: 0x0018F75C File Offset: 0x0018D95C
		public override int GetCascadeLevel()
		{
			return QueryDrawEvent.CascadeLevel;
		}

		// Token: 0x06005373 RID: 21363 RVA: 0x0018F763 File Offset: 0x0018D963
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005374 RID: 21364 RVA: 0x0018F76C File Offset: 0x0018D96C
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Draw = 0;
			this.BroadcastDrawDone = false;
			this.BroadcastDraw = 0;
			this.HighTransmitRate = 0;
		}

		// Token: 0x06005375 RID: 21365 RVA: 0x0018F797 File Offset: 0x0018D997
		public static QueryDrawEvent FromPool(GameObject Object)
		{
			QueryDrawEvent queryDrawEvent = PooledEvent<QueryDrawEvent>.FromPool();
			queryDrawEvent.Object = Object;
			queryDrawEvent.Draw = 0;
			queryDrawEvent.BroadcastDrawDone = false;
			queryDrawEvent.BroadcastDraw = 0;
			queryDrawEvent.HighTransmitRate = 0;
			return queryDrawEvent;
		}

		// Token: 0x06005376 RID: 21366 RVA: 0x0018F7C4 File Offset: 0x0018D9C4
		public static int GetFor(GameObject Object)
		{
			if (Object != null && Object.WantEvent(PooledEvent<QueryDrawEvent>.ID, QueryDrawEvent.CascadeLevel))
			{
				QueryDrawEvent queryDrawEvent = QueryDrawEvent.FromPool(Object);
				Object.HandleEvent(queryDrawEvent);
				return queryDrawEvent.Draw;
			}
			return 0;
		}

		// Token: 0x06005377 RID: 21367 RVA: 0x0018F800 File Offset: 0x0018DA00
		public static int GetFor(List<GameObject> Objects)
		{
			QueryDrawEvent queryDrawEvent = null;
			if (Objects != null)
			{
				int i = 0;
				int count = Objects.Count;
				while (i < count)
				{
					GameObject gameObject = Objects[i];
					if (gameObject.WantEvent(PooledEvent<QueryDrawEvent>.ID, QueryDrawEvent.CascadeLevel))
					{
						if (queryDrawEvent == null)
						{
							queryDrawEvent = QueryDrawEvent.FromPool(gameObject);
						}
						else
						{
							queryDrawEvent.Object = gameObject;
						}
						gameObject.HandleEvent(queryDrawEvent);
					}
					i++;
				}
			}
			if (queryDrawEvent == null)
			{
				return 0;
			}
			return queryDrawEvent.Draw;
		}

		// Token: 0x040029FD RID: 10749
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040029FE RID: 10750
		public GameObject Object;

		// Token: 0x040029FF RID: 10751
		public int Draw;

		// Token: 0x04002A00 RID: 10752
		public bool BroadcastDrawDone;

		// Token: 0x04002A01 RID: 10753
		public int BroadcastDraw;

		// Token: 0x04002A02 RID: 10754
		public int HighTransmitRate;
	}
}
