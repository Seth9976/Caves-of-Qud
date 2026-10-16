using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000678 RID: 1656
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class AnimateEvent : PooledEvent<AnimateEvent>
	{
		// Token: 0x06004726 RID: 18214 RVA: 0x001687FE File Offset: 0x001669FE
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004727 RID: 18215 RVA: 0x00168807 File Offset: 0x00166A07
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Object = null;
			this.Using = null;
			this.PartsToRemove = null;
		}

		// Token: 0x06004728 RID: 18216 RVA: 0x0016882B File Offset: 0x00166A2B
		public void WantToRemove(IPart Part)
		{
			if (this.PartsToRemove != null)
			{
				if (!this.PartsToRemove.Contains(Part))
				{
					this.PartsToRemove.Add(Part);
					return;
				}
			}
			else
			{
				this.Object.RemovePart(Part);
			}
		}

		// Token: 0x06004729 RID: 18217 RVA: 0x0016885C File Offset: 0x00166A5C
		public static void Send(GameObject Actor, GameObject Object, GameObject Using)
		{
			bool flag = true;
			List<IPart> list = null;
			try
			{
				if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("Animate"))
				{
					if (list == null)
					{
						list = new List<IPart>();
					}
					Event @event = Event.New("Animate", 0, 0, 0);
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("Object", Object);
					@event.SetParameter("Using", Using);
					@event.SetParameter("PartsToRemove", list);
					flag = Object.FireEvent(@event);
				}
				if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<AnimateEvent>.ID, MinEvent.CascadeLevel))
				{
					if (list == null)
					{
						list = new List<IPart>();
					}
					AnimateEvent animateEvent = PooledEvent<AnimateEvent>.FromPool();
					animateEvent.Actor = Actor;
					animateEvent.Object = Object;
					animateEvent.Using = Using;
					animateEvent.PartsToRemove = list;
					flag = Object.HandleEvent(animateEvent);
				}
			}
			finally
			{
				if (list != null)
				{
					foreach (IPart part in list)
					{
						Object.RemovePart(part);
					}
				}
			}
		}

		// Token: 0x040021E8 RID: 8680
		public GameObject Actor;

		// Token: 0x040021E9 RID: 8681
		public GameObject Object;

		// Token: 0x040021EA RID: 8682
		public GameObject Using;

		// Token: 0x040021EB RID: 8683
		public List<IPart> PartsToRemove;
	}
}
