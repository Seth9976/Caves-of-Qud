using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000738 RID: 1848
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class GetContentsEvent : PooledEvent<GetContentsEvent>
	{
		// Token: 0x06004C81 RID: 19585 RVA: 0x00179FF8 File Offset: 0x001781F8
		public override int GetCascadeLevel()
		{
			return GetContentsEvent.CascadeLevel;
		}

		// Token: 0x06004C82 RID: 19586 RVA: 0x00179FFF File Offset: 0x001781FF
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004C83 RID: 19587 RVA: 0x0017A008 File Offset: 0x00178208
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Objects.Clear();
		}

		// Token: 0x06004C84 RID: 19588 RVA: 0x0017A022 File Offset: 0x00178222
		public static GetContentsEvent FromPool(GameObject Object)
		{
			GetContentsEvent getContentsEvent = PooledEvent<GetContentsEvent>.FromPool();
			getContentsEvent.Object = Object;
			getContentsEvent.Objects.Clear();
			return getContentsEvent;
		}

		// Token: 0x06004C85 RID: 19589 RVA: 0x0017A03C File Offset: 0x0017823C
		public static IList<GameObject> GetFor(GameObject Object, IList<GameObject> ListToFill = null)
		{
			if (ListToFill == null)
			{
				ListToFill = Event.NewGameObjectList();
			}
			if (Object.WantEvent(PooledEvent<GetContentsEvent>.ID, GetContentsEvent.CascadeLevel))
			{
				GetContentsEvent getContentsEvent = GetContentsEvent.FromPool(Object);
				Object.HandleEvent(getContentsEvent);
				ListToFill.AddRange<GameObject>(getContentsEvent.Objects);
			}
			return ListToFill;
		}

		// Token: 0x06004C86 RID: 19590 RVA: 0x0017A084 File Offset: 0x00178284
		public static IList<GameObject> GetFor(Cell C, IList<GameObject> ListToFill = null)
		{
			if (ListToFill == null)
			{
				ListToFill = Event.NewGameObjectList();
			}
			if (C.WantEvent(PooledEvent<GetContentsEvent>.ID, GetContentsEvent.CascadeLevel))
			{
				GetContentsEvent getContentsEvent = GetContentsEvent.FromPool(null);
				C.HandleEvent(getContentsEvent);
				ListToFill.AddRange<GameObject>(getContentsEvent.Objects);
			}
			return ListToFill;
		}

		// Token: 0x06004C87 RID: 19591 RVA: 0x0017A0CC File Offset: 0x001782CC
		public static IList<GameObject> GetFor(Zone Z, IList<GameObject> ListToFill = null)
		{
			if (ListToFill == null)
			{
				ListToFill = Event.NewGameObjectList();
			}
			if (Z.WantEvent(PooledEvent<GetContentsEvent>.ID, GetContentsEvent.CascadeLevel))
			{
				GetContentsEvent getContentsEvent = GetContentsEvent.FromPool(null);
				Z.HandleEvent(getContentsEvent);
				ListToFill.AddRange<GameObject>(getContentsEvent.Objects);
			}
			return ListToFill;
		}

		// Token: 0x0400250A RID: 9482
		public new static readonly int CascadeLevel = 15;

		// Token: 0x0400250B RID: 9483
		public GameObject Object;

		// Token: 0x0400250C RID: 9484
		public List<GameObject> Objects = new List<GameObject>();
	}
}
