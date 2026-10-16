using System;

namespace XRL.World
{
	// Token: 0x02000750 RID: 1872
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetGameObjectSortEvent : PooledEvent<GetGameObjectSortEvent>
	{
		// Token: 0x06004D64 RID: 19812 RVA: 0x0017CD78 File Offset: 0x0017AF78
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004D65 RID: 19813 RVA: 0x0017CD81 File Offset: 0x0017AF81
		public override void Reset()
		{
			base.Reset();
			this.Object1 = null;
			this.Object2 = null;
			this.Category1 = null;
			this.Category2 = null;
			this.Sort = 0;
		}

		// Token: 0x06004D66 RID: 19814 RVA: 0x0017CDAC File Offset: 0x0017AFAC
		public static int GetFor(GameObject Object1, GameObject Object2, string Category1, string Category2)
		{
			bool flag = true;
			int num = 0;
			if (flag && GameObject.Validate(ref Object1) && GameObject.Validate(ref Object2))
			{
				bool flag2 = Object1.WantEvent(PooledEvent<GetGameObjectSortEvent>.ID, MinEvent.CascadeLevel);
				bool flag3 = Object2.WantEvent(PooledEvent<GetGameObjectSortEvent>.ID, MinEvent.CascadeLevel);
				if (flag2 || flag3)
				{
					GetGameObjectSortEvent getGameObjectSortEvent = PooledEvent<GetGameObjectSortEvent>.FromPool();
					getGameObjectSortEvent.Object1 = Object1;
					getGameObjectSortEvent.Object2 = Object2;
					getGameObjectSortEvent.Category1 = Category1;
					getGameObjectSortEvent.Category2 = Category2;
					getGameObjectSortEvent.Sort = num;
					if (!flag2 || Object1.HandleEvent(getGameObjectSortEvent))
					{
						if (flag3)
						{
							Object2.HandleEvent(getGameObjectSortEvent);
						}
					}
					num = getGameObjectSortEvent.Sort;
					PooledEvent<GetGameObjectSortEvent>.ResetTo(ref getGameObjectSortEvent);
				}
			}
			return num;
		}

		// Token: 0x0400258F RID: 9615
		public GameObject Object1;

		// Token: 0x04002590 RID: 9616
		public GameObject Object2;

		// Token: 0x04002591 RID: 9617
		public string Category1;

		// Token: 0x04002592 RID: 9618
		public string Category2;

		// Token: 0x04002593 RID: 9619
		public int Sort;
	}
}
