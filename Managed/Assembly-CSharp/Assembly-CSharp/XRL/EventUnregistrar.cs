using System;
using XRL.World;

namespace XRL
{
	// Token: 0x020004E2 RID: 1250
	public class EventUnregistrar : IEventRegistrar
	{
		// Token: 0x060035F9 RID: 13817 RVA: 0x00104615 File Offset: 0x00102815
		public static EventUnregistrar Get(IEventSource Source = null, IEventHandler Handler = null)
		{
			EventUnregistrar.Instance.Source = Source;
			EventUnregistrar.Instance.Handler = Handler;
			return EventUnregistrar.Instance;
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060035FA RID: 13818 RVA: 0x00104632 File Offset: 0x00102832
		public bool IsUnregister
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x00104635 File Offset: 0x00102835
		public void Register(IEventSource Source, IEventHandler Handler, int EventID, int Order = 0, bool Serialize = false)
		{
			Source.UnregisterEvent(Handler, EventID);
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x0010463F File Offset: 0x0010283F
		public void Register(IEventSource Source, int EventID, int Order = 0, bool Serialize = false)
		{
			Source.UnregisterEvent(this.Handler, EventID);
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x0010464E File Offset: 0x0010284E
		public void Register(int EventID, int Order = 0, bool Serialize = false)
		{
			this.Source.UnregisterEvent(this.Handler, EventID);
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x00104664 File Offset: 0x00102864
		public void Register(string EventID)
		{
			GameObject gameObject = this.Source as GameObject;
			if (gameObject != null)
			{
				IPart part = this.Handler as IPart;
				if (part != null)
				{
					gameObject.UnregisterPartEvent(part, EventID);
					return;
				}
				Effect effect = this.Handler as Effect;
				if (effect != null)
				{
					gameObject.UnregisterEffectEvent(effect, EventID);
				}
			}
		}

		// Token: 0x04001B29 RID: 6953
		public static EventUnregistrar Instance = new EventUnregistrar();

		// Token: 0x04001B2A RID: 6954
		public IEventSource Source;

		// Token: 0x04001B2B RID: 6955
		public IEventHandler Handler;
	}
}
