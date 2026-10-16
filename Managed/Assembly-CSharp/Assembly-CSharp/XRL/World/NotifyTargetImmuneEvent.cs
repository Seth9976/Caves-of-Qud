using System;

namespace XRL.World
{
	// Token: 0x02000810 RID: 2064
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class NotifyTargetImmuneEvent : PooledEvent<NotifyTargetImmuneEvent>
	{
		// Token: 0x06005260 RID: 21088 RVA: 0x0018CA07 File Offset: 0x0018AC07
		public override int GetCascadeLevel()
		{
			return NotifyTargetImmuneEvent.CascadeLevel;
		}

		// Token: 0x06005261 RID: 21089 RVA: 0x0018CA0E File Offset: 0x0018AC0E
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005262 RID: 21090 RVA: 0x0018CA17 File Offset: 0x0018AC17
		public override void Reset()
		{
			base.Reset();
			this.Weapon = null;
			this.Target = null;
			this.Actor = null;
			this.Damage = null;
			this.ImmunityFrom = null;
			this.Checking = null;
		}

		// Token: 0x06005263 RID: 21091 RVA: 0x0018CA4C File Offset: 0x0018AC4C
		public static void Send(GameObject Weapon, GameObject Target, GameObject Actor = null, Damage Damage = null, IComponent<GameObject> ImmunityFrom = null)
		{
			bool flag = true;
			if (flag)
			{
				bool flag2 = GameObject.Validate(ref Weapon) && Weapon.HasRegisteredEvent("NotifyTargetImmune");
				bool flag3 = GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("NotifyTargetImmune");
				if (flag2 || flag3)
				{
					Event @event = Event.New("NotifyTargetImmune", 0, 0, 0);
					@event.SetParameter("Weapon", Weapon);
					@event.SetParameter("Target", Target);
					@event.SetParameter("Actor", Actor);
					@event.SetParameter("Damage", Damage);
					@event.SetParameter("ImmunityFrom", ImmunityFrom);
					if (flag && flag2)
					{
						@event.SetParameter("Checking", "Weapon");
						flag = Weapon.FireEvent(@event);
					}
					if (flag && flag3)
					{
						@event.SetParameter("Checking", "Actor");
						flag = Actor.FireEvent(@event);
					}
				}
			}
			if (flag)
			{
				bool flag4 = GameObject.Validate(ref Weapon) && Weapon.WantEvent(PooledEvent<NotifyTargetImmuneEvent>.ID, NotifyTargetImmuneEvent.CascadeLevel);
				bool flag5 = GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<NotifyTargetImmuneEvent>.ID, NotifyTargetImmuneEvent.CascadeLevel);
				if (flag4 || flag5)
				{
					NotifyTargetImmuneEvent notifyTargetImmuneEvent = PooledEvent<NotifyTargetImmuneEvent>.FromPool();
					notifyTargetImmuneEvent.Weapon = Weapon;
					notifyTargetImmuneEvent.Target = Target;
					notifyTargetImmuneEvent.Actor = Actor;
					notifyTargetImmuneEvent.Damage = Damage;
					notifyTargetImmuneEvent.ImmunityFrom = ImmunityFrom;
					if (flag && flag4)
					{
						notifyTargetImmuneEvent.Checking = "Weapon";
						flag = Weapon.HandleEvent(notifyTargetImmuneEvent);
					}
					if (flag && flag5)
					{
						notifyTargetImmuneEvent.Checking = "Actor";
						flag = Actor.HandleEvent(notifyTargetImmuneEvent);
					}
				}
			}
		}

		// Token: 0x04002968 RID: 10600
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002969 RID: 10601
		public GameObject Weapon;

		// Token: 0x0400296A RID: 10602
		public GameObject Target;

		// Token: 0x0400296B RID: 10603
		public GameObject Actor;

		// Token: 0x0400296C RID: 10604
		public Damage Damage;

		// Token: 0x0400296D RID: 10605
		public IComponent<GameObject> ImmunityFrom;

		// Token: 0x0400296E RID: 10606
		public string Checking;
	}
}
