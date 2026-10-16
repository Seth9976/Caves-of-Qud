using System;

namespace XRL.World
{
	// Token: 0x020007EA RID: 2026
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class InterruptAutowalkEvent : PooledEvent<InterruptAutowalkEvent>
	{
		// Token: 0x06005167 RID: 20839 RVA: 0x001896C3 File Offset: 0x001878C3
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005168 RID: 20840 RVA: 0x001896CC File Offset: 0x001878CC
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Cell = null;
			this.Because = null;
			this.IndicateObject = null;
			this.IndicateCell = null;
			this.AsThreat = false;
		}

		// Token: 0x06005169 RID: 20841 RVA: 0x00189700 File Offset: 0x00187900
		public static bool Check(GameObject Actor, Cell Cell, out string Because, out GameObject IndicateObject, out Cell IndicateCell, out bool AsThreat)
		{
			Because = null;
			IndicateObject = null;
			IndicateCell = null;
			AsThreat = false;
			if (Cell == null)
			{
				return true;
			}
			bool flag = true;
			if (flag && Cell.WantEvent(PooledEvent<InterruptAutowalkEvent>.ID, MinEvent.CascadeLevel))
			{
				InterruptAutowalkEvent interruptAutowalkEvent = PooledEvent<InterruptAutowalkEvent>.FromPool();
				interruptAutowalkEvent.Actor = Actor;
				interruptAutowalkEvent.Cell = Cell;
				flag = Cell.HandleEvent(interruptAutowalkEvent);
				Because = interruptAutowalkEvent.Because;
				IndicateObject = interruptAutowalkEvent.IndicateObject;
				IndicateCell = interruptAutowalkEvent.IndicateCell;
				AsThreat = interruptAutowalkEvent.AsThreat;
			}
			return !flag;
		}

		// Token: 0x0600516A RID: 20842 RVA: 0x0018977C File Offset: 0x0018797C
		public static bool Check(GameObject Actor, Cell Cell)
		{
			string text;
			GameObject gameObject;
			Cell cell;
			bool flag;
			return InterruptAutowalkEvent.Check(Actor, Cell, out text, out gameObject, out cell, out flag);
		}

		// Token: 0x040028C1 RID: 10433
		public GameObject Actor;

		// Token: 0x040028C2 RID: 10434
		public Cell Cell;

		// Token: 0x040028C3 RID: 10435
		public string Because;

		// Token: 0x040028C4 RID: 10436
		public GameObject IndicateObject;

		// Token: 0x040028C5 RID: 10437
		public Cell IndicateCell;

		// Token: 0x040028C6 RID: 10438
		public bool AsThreat;
	}
}
