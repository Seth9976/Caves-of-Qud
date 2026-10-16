using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020007F9 RID: 2041
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class JumpedEvent : PooledEvent<JumpedEvent>
	{
		// Token: 0x060051BF RID: 20927 RVA: 0x0018A875 File Offset: 0x00188A75
		public override int GetCascadeLevel()
		{
			return JumpedEvent.CascadeLevel;
		}

		// Token: 0x060051C0 RID: 20928 RVA: 0x0018A87C File Offset: 0x00188A7C
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060051C1 RID: 20929 RVA: 0x0018A888 File Offset: 0x00188A88
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.OriginCell = null;
			this.TargetCell = null;
			this.Path = null;
			this.Range = 0;
			this.Pass = 0;
			this.AbilityName = null;
			this.ProviderKey = null;
			this.SourceKey = null;
		}

		// Token: 0x060051C2 RID: 20930 RVA: 0x0018A8DC File Offset: 0x00188ADC
		public static void Send(GameObject Actor, Cell OriginCell, Cell TargetCell, List<Point> Path, int Range, string AbilityName = null, string ProviderKey = null, string SourceKey = null)
		{
			bool flag = GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("Jumped");
			bool flag2 = GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<JumpedEvent>.ID, JumpedEvent.CascadeLevel);
			if (flag || flag2)
			{
				bool flag3 = true;
				bool flag4 = true;
				int num = 1;
				while (flag4 && flag3 && num <= JumpedEvent.PASSES)
				{
					flag4 = false;
					if (flag3 && flag && GameObject.Validate(ref Actor))
					{
						Event @event = Event.New("Jumped", 0, 0, 0);
						@event.SetParameter("Actor", Actor);
						@event.SetParameter("OriginCell", OriginCell);
						@event.SetParameter("TargetCell", TargetCell);
						@event.SetParameter("Path", Path);
						@event.SetParameter("Range", Range);
						@event.SetParameter("Pass", num);
						@event.SetParameter("AbilityName", AbilityName);
						@event.SetParameter("ProviderKey", ProviderKey);
						@event.SetParameter("SourceKey", SourceKey);
						flag3 = Actor.FireEvent(@event);
						flag4 = true;
					}
					if (flag3 && flag2 && GameObject.Validate(ref Actor))
					{
						JumpedEvent jumpedEvent = PooledEvent<JumpedEvent>.FromPool();
						jumpedEvent.Actor = Actor;
						jumpedEvent.OriginCell = OriginCell;
						jumpedEvent.TargetCell = TargetCell;
						jumpedEvent.Path = Path;
						jumpedEvent.Range = Range;
						jumpedEvent.Pass = num;
						jumpedEvent.AbilityName = AbilityName;
						jumpedEvent.ProviderKey = ProviderKey;
						jumpedEvent.SourceKey = SourceKey;
						flag3 = Actor.HandleEvent(jumpedEvent);
						flag4 = true;
					}
					num++;
				}
			}
		}

		// Token: 0x04002905 RID: 10501
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002906 RID: 10502
		public static readonly int PASSES = 3;

		// Token: 0x04002907 RID: 10503
		public GameObject Actor;

		// Token: 0x04002908 RID: 10504
		public Cell OriginCell;

		// Token: 0x04002909 RID: 10505
		public Cell TargetCell;

		// Token: 0x0400290A RID: 10506
		public List<Point> Path;

		// Token: 0x0400290B RID: 10507
		public int Range;

		// Token: 0x0400290C RID: 10508
		public int Pass;

		// Token: 0x0400290D RID: 10509
		public string AbilityName;

		// Token: 0x0400290E RID: 10510
		public string ProviderKey;

		// Token: 0x0400290F RID: 10511
		public string SourceKey;
	}
}
