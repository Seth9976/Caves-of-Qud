using System;

namespace XRL.World
{
	// Token: 0x02000787 RID: 1927
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetShieldBlockPreferenceEvent : PooledEvent<GetShieldBlockPreferenceEvent>
	{
		// Token: 0x06004EEA RID: 20202 RVA: 0x00181EF9 File Offset: 0x001800F9
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004EEB RID: 20203 RVA: 0x00181F02 File Offset: 0x00180102
		public override void Reset()
		{
			base.Reset();
			this.Shield = null;
			this.Attacker = null;
			this.Defender = null;
			this.Preference = 0;
		}

		// Token: 0x06004EEC RID: 20204 RVA: 0x00181F26 File Offset: 0x00180126
		public static GetShieldBlockPreferenceEvent FromPool(GameObject Shield, GameObject Attacker, GameObject Defender, int Preference)
		{
			GetShieldBlockPreferenceEvent getShieldBlockPreferenceEvent = PooledEvent<GetShieldBlockPreferenceEvent>.FromPool();
			getShieldBlockPreferenceEvent.Shield = Shield;
			getShieldBlockPreferenceEvent.Attacker = Attacker;
			getShieldBlockPreferenceEvent.Defender = Defender;
			getShieldBlockPreferenceEvent.Preference = Preference;
			return getShieldBlockPreferenceEvent;
		}

		// Token: 0x06004EED RID: 20205 RVA: 0x00181F4C File Offset: 0x0018014C
		public static int GetFor(GameObject Shield, GameObject Attacker = null, GameObject Defender = null, int Preference = 0)
		{
			if (GameObject.Validate(ref Shield))
			{
				if (Shield.HasRegisteredEvent("GetShieldBlockPreference"))
				{
					Event @event = Event.New("GetShieldBlockPreference", 0, 0, 0);
					@event.SetParameter("Shield", Shield);
					@event.SetParameter("Attacker", Attacker);
					@event.SetParameter("Defender", Defender);
					@event.SetParameter("Preference", Preference);
					bool flag = Shield.FireEvent(@event);
					Preference = @event.GetIntParameter("Preference", 0);
					if (!flag)
					{
						return Preference;
					}
				}
				if (Shield.WantEvent(PooledEvent<GetShieldBlockPreferenceEvent>.ID, MinEvent.CascadeLevel))
				{
					GetShieldBlockPreferenceEvent getShieldBlockPreferenceEvent = GetShieldBlockPreferenceEvent.FromPool(Shield, Attacker, Defender, Preference);
					Shield.HandleEvent(getShieldBlockPreferenceEvent);
					Preference = getShieldBlockPreferenceEvent.Preference;
					return Preference;
				}
			}
			return Preference;
		}

		// Token: 0x040026A6 RID: 9894
		public GameObject Shield;

		// Token: 0x040026A7 RID: 9895
		public GameObject Attacker;

		// Token: 0x040026A8 RID: 9896
		public GameObject Defender;

		// Token: 0x040026A9 RID: 9897
		public int Preference;
	}
}
