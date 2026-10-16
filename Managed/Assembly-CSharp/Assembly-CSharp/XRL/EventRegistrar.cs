using System;
using System.Runtime.CompilerServices;
using XRL.World;

namespace XRL
{
	// Token: 0x020004E1 RID: 1249
	public class EventRegistrar : IEventRegistrar
	{
		// Token: 0x060035F1 RID: 13809 RVA: 0x001044FE File Offset: 0x001026FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static EventRegistrar Get(IEventSource Source = null, IEventHandler Handler = null)
		{
			EventRegistrar.Instance.Source = Source;
			EventRegistrar.Instance.Handler = Handler;
			return EventRegistrar.Instance;
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060035F2 RID: 13810 RVA: 0x0010451B File Offset: 0x0010271B
		public bool IsUnregister
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x0010451E File Offset: 0x0010271E
		public void Register(IEventSource Source, IEventHandler Handler, int EventID, int Order = 0, bool Serialize = false)
		{
			Source.RegisterEvent(Handler, EventID, Order, Serialize);
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x0010452C File Offset: 0x0010272C
		public void Register(IEventSource Source, int EventID, int Order = 0, bool Serialize = false)
		{
			Source.RegisterEvent(this.Handler, EventID, Order, Serialize);
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x0010453E File Offset: 0x0010273E
		public void Register(int EventID, int Order = 0, bool Serialize = false)
		{
			this.Source.RegisterEvent(this.Handler, EventID, Order, Serialize);
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x00104554 File Offset: 0x00102754
		public void Register(string EventID)
		{
			GameObject gameObject = this.Source as GameObject;
			if (gameObject == null)
			{
				MetricsManager.LogAssemblyWarning(this.Handler.GetType(), "Cannot register for string events on '" + this.Source.GetType().GetName(true) + "', only GameObject sources are supported.");
				return;
			}
			IPart part = this.Handler as IPart;
			if (part != null)
			{
				gameObject.RegisterPartEvent(part, EventID);
				return;
			}
			Effect effect = this.Handler as Effect;
			if (effect != null)
			{
				gameObject.RegisterEffectEvent(effect, EventID);
				return;
			}
			MetricsManager.LogAssemblyWarning(this.Handler.GetType(), "Cannot register for string events with '" + this.Handler.GetType().GetName(true) + "', only IPart and Effect handlers are supported.");
		}

		// Token: 0x04001B26 RID: 6950
		public static EventRegistrar Instance = new EventRegistrar();

		// Token: 0x04001B27 RID: 6951
		public IEventSource Source;

		// Token: 0x04001B28 RID: 6952
		public IEventHandler Handler;
	}
}
