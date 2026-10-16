using System;
using XRL.Collections;

namespace XRL.World
{
	// Token: 0x02000672 RID: 1650
	[GameEvent(Cascade = 17)]
	public class AfterTravelEvent : PooledEvent<AfterTravelEvent>
	{
		// Token: 0x060046F6 RID: 18166 RVA: 0x0016815A File Offset: 0x0016635A
		public override int GetCascadeLevel()
		{
			return AfterTravelEvent.CascadeLevel;
		}

		// Token: 0x060046F7 RID: 18167 RVA: 0x00168161 File Offset: 0x00166361
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060046F8 RID: 18168 RVA: 0x0016816A File Offset: 0x0016636A
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Terrain = null;
			this.Segments = 0;
			this.Handlers.Clear();
		}

		// Token: 0x060046F9 RID: 18169 RVA: 0x00168194 File Offset: 0x00166394
		public static void Send(GameObject Actor, GameObject Terrain, int Segments)
		{
			using (AfterTravelEvent afterTravelEvent = PooledEvent<AfterTravelEvent>.FromPool())
			{
				afterTravelEvent.Actor = Actor;
				afterTravelEvent.Terrain = Terrain;
				afterTravelEvent.Segments = Segments;
				afterTravelEvent.Handlers.Add(The.Game);
				Zone currentZone = Terrain.CurrentZone;
				currentZone.GetContentWantEventHandlers(PooledEvent<AfterTravelEvent>.ID, AfterTravelEvent.CascadeLevel, afterTravelEvent.Handlers);
				if (currentZone != Actor.CurrentZone)
				{
					afterTravelEvent.Handlers.Add(Actor);
				}
				IEventHandler[] array = afterTravelEvent.Handlers.GetArray();
				int i = 0;
				int count = afterTravelEvent.Handlers.Count;
				while (i < count)
				{
					array[i].HandleEvent(afterTravelEvent);
					i++;
				}
			}
		}

		// Token: 0x040021CF RID: 8655
		public new static readonly int CascadeLevel = 17;

		// Token: 0x040021D0 RID: 8656
		public GameObject Actor;

		// Token: 0x040021D1 RID: 8657
		public GameObject Terrain;

		// Token: 0x040021D2 RID: 8658
		public int Segments;

		// Token: 0x040021D3 RID: 8659
		private Rack<IEventHandler> Handlers = new Rack<IEventHandler>();
	}
}
