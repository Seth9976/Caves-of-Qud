using System;

namespace XRL.World
{
	// Token: 0x020006F2 RID: 1778
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class DamageConstantAdjustedEvent : PooledEvent<DamageConstantAdjustedEvent>
	{
		// Token: 0x06004A83 RID: 19075 RVA: 0x001745ED File Offset: 0x001727ED
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x001745F6 File Offset: 0x001727F6
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Part = null;
			this.Amount = 0;
		}

		// Token: 0x06004A85 RID: 19077 RVA: 0x00174614 File Offset: 0x00172814
		public static void Send(GameObject Object, IPart Part, int Amount)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("DamageConstantAdjusted"))
			{
				Event @event = Event.New("DamageConstantAdjusted", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Part", Part);
				@event.SetParameter("Amount", Amount);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<DamageConstantAdjustedEvent>.ID, MinEvent.CascadeLevel))
			{
				DamageConstantAdjustedEvent damageConstantAdjustedEvent = PooledEvent<DamageConstantAdjustedEvent>.FromPool();
				damageConstantAdjustedEvent.Object = Object;
				damageConstantAdjustedEvent.Part = Part;
				damageConstantAdjustedEvent.Amount = Amount;
				flag = Object.HandleEvent(damageConstantAdjustedEvent);
			}
		}

		// Token: 0x040023EC RID: 9196
		public GameObject Object;

		// Token: 0x040023ED RID: 9197
		public IPart Part;

		// Token: 0x040023EE RID: 9198
		public int Amount;
	}
}
