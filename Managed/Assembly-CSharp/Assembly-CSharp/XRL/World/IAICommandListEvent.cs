using System;
using System.Collections.Generic;
using XRL.World.AI.GoalHandlers;

namespace XRL.World
{
	// Token: 0x020007B6 RID: 1974
	[GameEvent(Base = true, Cascade = 17)]
	public abstract class IAICommandListEvent : MinEvent
	{
		// Token: 0x06005029 RID: 20521 RVA: 0x00186102 File Offset: 0x00184302
		public override int GetCascadeLevel()
		{
			return IAICommandListEvent.CascadeLevel;
		}

		// Token: 0x0600502A RID: 20522 RVA: 0x00186109 File Offset: 0x00184309
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600502B RID: 20523 RVA: 0x00186112 File Offset: 0x00184312
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Target = null;
			this.Distance = 0;
			this.List.Clear();
		}

		// Token: 0x0600502C RID: 20524 RVA: 0x0018613C File Offset: 0x0018433C
		public void Add(string Command, int Priority = 1, GameObject Object = null, bool Inv = false, bool Self = false, GameObject TargetOverride = null, Cell TargetCellOverride = null)
		{
			AICommandList aicommandList = new AICommandList(Command, Priority, Object, Inv, Self, TargetOverride, TargetCellOverride);
			if (Priority > this.List.Capacity)
			{
				this.List.Capacity += Priority;
			}
			for (int i = 0; i < Priority; i++)
			{
				this.List.Add(aicommandList);
			}
		}

		// Token: 0x040027AC RID: 10156
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040027AD RID: 10157
		public GameObject Actor;

		// Token: 0x040027AE RID: 10158
		public GameObject Target;

		// Token: 0x040027AF RID: 10159
		public int Distance;

		// Token: 0x040027B0 RID: 10160
		public List<AICommandList> List = new List<AICommandList>();
	}
}
