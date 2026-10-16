using System;
using System.Collections.Generic;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World
{
	// Token: 0x020006D5 RID: 1749
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ChargeUsedEvent : IChargeConsumptionEvent
	{
		// Token: 0x060049CF RID: 18895 RVA: 0x00172A53 File Offset: 0x00170C53
		public ChargeUsedEvent()
		{
			this.ID = ChargeUsedEvent.ID;
		}

		// Token: 0x060049D0 RID: 18896 RVA: 0x00172A66 File Offset: 0x00170C66
		public static int CountPool()
		{
			if (ChargeUsedEvent.Pool != null)
			{
				return ChargeUsedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060049D1 RID: 18897 RVA: 0x00172A7B File Offset: 0x00170C7B
		public static void ResetPool()
		{
			while (ChargeUsedEvent.PoolCounter > 0)
			{
				ChargeUsedEvent.Pool[--ChargeUsedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060049D2 RID: 18898 RVA: 0x00172AA3 File Offset: 0x00170CA3
		public static void ResetTo(ref ChargeUsedEvent E)
		{
			MinEvent.ResetTo<ChargeUsedEvent>(E, ChargeUsedEvent.Pool, ref ChargeUsedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060049D3 RID: 18899 RVA: 0x00172AB9 File Offset: 0x00170CB9
		public static ChargeUsedEvent FromPool()
		{
			return MinEvent.FromPool<ChargeUsedEvent>(ref ChargeUsedEvent.Pool, ref ChargeUsedEvent.PoolCounter, 8192);
		}

		// Token: 0x060049D4 RID: 18900 RVA: 0x00172ACF File Offset: 0x00170CCF
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060049D5 RID: 18901 RVA: 0x00172AE3 File Offset: 0x00170CE3
		public override void Reset()
		{
			base.Reset();
			this.DesiredAmount = 0;
		}

		// Token: 0x060049D6 RID: 18902 RVA: 0x00172AF4 File Offset: 0x00170CF4
		public static void Send(GameObject Object, GameObject Source, int Amount, int DesiredAmount, int Multiple = 1, long GridMask = 0L, bool Forced = false, bool LiveOnly = false, bool IncludeTransient = true, bool IncludeBiological = true, int PowerLoadLevel = 100)
		{
			if (ChargeUsedEvent.Wanted(Object))
			{
				ChargeUsedEvent chargeUsedEvent = ChargeUsedEvent.FromPool();
				chargeUsedEvent.Source = Source;
				chargeUsedEvent.Amount = Amount;
				chargeUsedEvent.DesiredAmount = DesiredAmount;
				chargeUsedEvent.Multiple = Multiple;
				chargeUsedEvent.GridMask = GridMask;
				chargeUsedEvent.Forced = Forced;
				chargeUsedEvent.LiveOnly = LiveOnly;
				chargeUsedEvent.IncludeTransient = IncludeTransient;
				chargeUsedEvent.IncludeBiological = IncludeBiological;
				chargeUsedEvent.PowerLoadLevel = new int?(PowerLoadLevel);
				ChargeUsedEvent.Process(Object, chargeUsedEvent);
			}
			if (PowerLoadLevel > 100)
			{
				int @for = GetOverloadChargeEvent.GetFor(Object, Amount);
				if (@for > 0)
				{
					GameObject holder = Object.Holder;
					Object.TemperatureChange(1 + @for / 100, holder, false, false, false, false, 0, null, null);
					if (holder != null)
					{
						holder.TemperatureChange(1 + @for / 100, holder, false, false, false, false, 0, null, null);
					}
					if ((1 + @for / 10).in10000() && Object.ApplyEffect(new Broken(false, false, true, false), null))
					{
						Messaging.XDidY(Object, "overheat", null, "!", null, null, null, null, false, false, holder, null, false, false, false, false, false, holder);
					}
				}
			}
		}

		// Token: 0x060049D7 RID: 18903 RVA: 0x00172C13 File Offset: 0x00170E13
		public static bool Wanted(GameObject Object)
		{
			return Object.WantEvent(ChargeUsedEvent.ID, IChargeEvent.CascadeLevel) || Object.HasRegisteredEvent("ChargeUsed");
		}

		// Token: 0x060049D8 RID: 18904 RVA: 0x00172C34 File Offset: 0x00170E34
		public static bool Process(GameObject Object, ChargeUsedEvent E)
		{
			return E.CheckRegisteredEvent(Object, "ChargeUsed") && Object.HandleEvent(E);
		}

		// Token: 0x04002377 RID: 9079
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ChargeUsedEvent), null, new MinEvent.EventPoolCount(ChargeUsedEvent.CountPool), new MinEvent.EventPoolReset(ChargeUsedEvent.ResetPool));

		// Token: 0x04002378 RID: 9080
		private static List<ChargeUsedEvent> Pool;

		// Token: 0x04002379 RID: 9081
		private static int PoolCounter;

		// Token: 0x0400237A RID: 9082
		public int DesiredAmount;
	}
}
