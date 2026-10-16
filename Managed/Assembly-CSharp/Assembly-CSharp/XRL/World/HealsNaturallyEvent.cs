using System;

namespace XRL.World
{
	// Token: 0x020007B5 RID: 1973
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class HealsNaturallyEvent : PooledEvent<HealsNaturallyEvent>
	{
		// Token: 0x06005025 RID: 20517 RVA: 0x0018609C File Offset: 0x0018429C
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005026 RID: 20518 RVA: 0x001860A5 File Offset: 0x001842A5
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
		}

		// Token: 0x06005027 RID: 20519 RVA: 0x001860B4 File Offset: 0x001842B4
		public static bool Check(GameObject Object)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<HealsNaturallyEvent>.ID, MinEvent.CascadeLevel))
			{
				HealsNaturallyEvent healsNaturallyEvent = PooledEvent<HealsNaturallyEvent>.FromPool();
				healsNaturallyEvent.Object = Object;
				flag = Object.HandleEvent(healsNaturallyEvent);
			}
			return !flag;
		}

		// Token: 0x040027AB RID: 10155
		public GameObject Object;
	}
}
