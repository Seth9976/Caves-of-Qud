using System;

namespace XRL.World
{
	// Token: 0x02000860 RID: 2144
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class SubjectToGravityEvent : PooledEvent<SubjectToGravityEvent>
	{
		// Token: 0x0600556D RID: 21869 RVA: 0x0019889D File Offset: 0x00196A9D
		public override int GetCascadeLevel()
		{
			return SubjectToGravityEvent.CascadeLevel;
		}

		// Token: 0x0600556E RID: 21870 RVA: 0x001988A4 File Offset: 0x00196AA4
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600556F RID: 21871 RVA: 0x001988AD File Offset: 0x00196AAD
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.SubjectToGravity = false;
		}

		// Token: 0x06005570 RID: 21872 RVA: 0x001988C4 File Offset: 0x00196AC4
		public static bool Check(GameObject Object)
		{
			bool flag = true;
			if (GameObject.Validate(ref Object))
			{
				if (!Object.IsReal)
				{
					flag = false;
				}
				else if (Object.HasPropertyOrTag("IgnoresGravity"))
				{
					flag = false;
				}
			}
			bool flag2 = true;
			if (flag2 && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("SubjectToGravity"))
			{
				Event @event = Event.New("SubjectToGravity", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetFlag("SubjectToGravity", flag);
				flag2 = Object.FireEvent(@event);
				flag = @event.HasFlag("SubjectToGravity");
			}
			if (flag2 && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<SubjectToGravityEvent>.ID, SubjectToGravityEvent.CascadeLevel))
			{
				SubjectToGravityEvent subjectToGravityEvent = PooledEvent<SubjectToGravityEvent>.FromPool();
				subjectToGravityEvent.Object = Object;
				subjectToGravityEvent.SubjectToGravity = flag;
				flag2 = Object.HandleEvent(subjectToGravityEvent);
				flag = subjectToGravityEvent.SubjectToGravity;
			}
			return flag;
		}

		// Token: 0x04002B10 RID: 11024
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002B11 RID: 11025
		public GameObject Object;

		// Token: 0x04002B12 RID: 11026
		public bool SubjectToGravity;
	}
}
