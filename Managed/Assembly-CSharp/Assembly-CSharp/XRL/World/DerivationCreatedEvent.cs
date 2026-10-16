using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x020006F8 RID: 1784
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class DerivationCreatedEvent : IDerivationEvent
	{
		// Token: 0x06004AA8 RID: 19112 RVA: 0x00174DFA File Offset: 0x00172FFA
		public DerivationCreatedEvent()
		{
			this.ID = DerivationCreatedEvent.ID;
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x00174E0D File Offset: 0x0017300D
		public override int GetCascadeLevel()
		{
			return DerivationCreatedEvent.CascadeLevel;
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x00174E14 File Offset: 0x00173014
		public static int CountPool()
		{
			if (DerivationCreatedEvent.Pool != null)
			{
				return DerivationCreatedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x00174E29 File Offset: 0x00173029
		public static void ResetPool()
		{
			while (DerivationCreatedEvent.PoolCounter > 0)
			{
				DerivationCreatedEvent.Pool[--DerivationCreatedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x00174E51 File Offset: 0x00173051
		public static void ResetTo(ref DerivationCreatedEvent E)
		{
			MinEvent.ResetTo<DerivationCreatedEvent>(E, DerivationCreatedEvent.Pool, ref DerivationCreatedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x00174E67 File Offset: 0x00173067
		public static DerivationCreatedEvent FromPool()
		{
			return MinEvent.FromPool<DerivationCreatedEvent>(ref DerivationCreatedEvent.Pool, ref DerivationCreatedEvent.PoolCounter, 8192);
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x00174E7D File Offset: 0x0017307D
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x00174E91 File Offset: 0x00173091
		public override void Reset()
		{
			base.Reset();
			this.Original = null;
			this.PartsToRemove = null;
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x00174EA7 File Offset: 0x001730A7
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

		// Token: 0x06004AB1 RID: 19121 RVA: 0x00174ED8 File Offset: 0x001730D8
		public static void Send(GameObject Object, GameObject Actor, GameObject Original, string Context = null)
		{
			List<IPart> list = null;
			try
			{
				if (GameObject.Validate(ref Object) && Object.HasRegisteredEvent("DerivationCreated"))
				{
					if (list == null)
					{
						list = new List<IPart>();
					}
					Event @event = Event.New("DerivationCreated", 0, 0, 0);
					@event.SetParameter("Object", Object);
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("Context", Context);
					@event.SetParameter("Original", Original);
					@event.SetParameter("PartsToRemove", list);
					Object.FireEvent(@event);
				}
				if (GameObject.Validate(ref Object) && Object.WantEvent(DerivationCreatedEvent.ID, DerivationCreatedEvent.CascadeLevel))
				{
					if (list == null)
					{
						list = new List<IPart>();
					}
					DerivationCreatedEvent derivationCreatedEvent = DerivationCreatedEvent.FromPool();
					derivationCreatedEvent.Object = Object;
					derivationCreatedEvent.Actor = Actor;
					derivationCreatedEvent.Original = Original;
					derivationCreatedEvent.Context = Context;
					derivationCreatedEvent.PartsToRemove = list;
					Object.HandleEvent(derivationCreatedEvent);
				}
			}
			finally
			{
				if (list != null)
				{
					foreach (IPart part in list)
					{
						try
						{
							Object.RemovePart(part);
						}
						catch (Exception ex)
						{
							MetricsManager.LogError(ex);
						}
					}
				}
			}
		}

		// Token: 0x04002412 RID: 9234
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(DerivationCreatedEvent), null, new MinEvent.EventPoolCount(DerivationCreatedEvent.CountPool), new MinEvent.EventPoolReset(DerivationCreatedEvent.ResetPool));

		// Token: 0x04002413 RID: 9235
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002414 RID: 9236
		private static List<DerivationCreatedEvent> Pool;

		// Token: 0x04002415 RID: 9237
		private static int PoolCounter;

		// Token: 0x04002416 RID: 9238
		public GameObject Original;

		// Token: 0x04002417 RID: 9239
		public List<IPart> PartsToRemove;
	}
}
