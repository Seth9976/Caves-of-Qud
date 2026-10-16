using System;

namespace XRL.World
{
	// Token: 0x020006AF RID: 1711
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class BlocksRadarEvent : PooledEvent<BlocksRadarEvent>
	{
		// Token: 0x060048C5 RID: 18629 RVA: 0x0016D530 File Offset: 0x0016B730
		public override int GetCascadeLevel()
		{
			return BlocksRadarEvent.CascadeLevel;
		}

		// Token: 0x060048C6 RID: 18630 RVA: 0x0016D537 File Offset: 0x0016B737
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060048C7 RID: 18631 RVA: 0x0016D540 File Offset: 0x0016B740
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x060048C8 RID: 18632 RVA: 0x0016D54F File Offset: 0x0016B74F
		public static BlocksRadarEvent FromPool(GameObject Object)
		{
			BlocksRadarEvent blocksRadarEvent = PooledEvent<BlocksRadarEvent>.FromPool();
			blocksRadarEvent.Object = Object;
			return blocksRadarEvent;
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x0016D560 File Offset: 0x0016B760
		public static bool Check(GameObject Object)
		{
			if (GameObject.Validate(ref Object) && Object.HasRegisteredEvent("BlocksRadar"))
			{
				Event @event = Event.New("BlocksRadar", 0, 0, 0);
				@event.SetParameter("Object", Object);
				if (!Object.FireEvent(@event))
				{
					return true;
				}
			}
			return GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<BlocksRadarEvent>.ID, BlocksRadarEvent.CascadeLevel) && !Object.HandleEvent(BlocksRadarEvent.FromPool(Object));
		}

		// Token: 0x060048CA RID: 18634 RVA: 0x0016D5D8 File Offset: 0x0016B7D8
		public static bool Check(Cell C)
		{
			for (int i = C.Objects.Count - 1; i >= 0; i--)
			{
				if (BlocksRadarEvent.Check(C.Objects[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040022E3 RID: 8931
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040022E4 RID: 8932
		public GameObject Object;
	}
}
