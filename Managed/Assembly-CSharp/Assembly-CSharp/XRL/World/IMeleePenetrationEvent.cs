using System;

namespace XRL.World
{
	// Token: 0x020007D1 RID: 2001
	[GameEvent(Cascade = 17, Base = true)]
	public abstract class IMeleePenetrationEvent : MinEvent
	{
		// Token: 0x060050A3 RID: 20643 RVA: 0x0018749F File Offset: 0x0018569F
		public override int GetCascadeLevel()
		{
			return IMeleePenetrationEvent.CascadeLevel;
		}

		// Token: 0x060050A4 RID: 20644 RVA: 0x001874A6 File Offset: 0x001856A6
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060050A5 RID: 20645 RVA: 0x001874B0 File Offset: 0x001856B0
		public override void Reset()
		{
			base.Reset();
			this.Attacker = null;
			this.Defender = null;
			this.Weapon = null;
			this.Properties = null;
			this.Hand = null;
			this.Penetrations = 0;
			this.StatBonus = 0;
			this.MaxStatBonus = 0;
			this.PenetrationBonus = 0;
			this.MaxPenetrationBonus = 0;
			this.AV = 0;
			this.Critical = false;
		}

		// Token: 0x060050A6 RID: 20646 RVA: 0x00187518 File Offset: 0x00185718
		public static bool Process(ref int Penetrations, ref int StatBonus, ref int MaxStatBonus, ref int PenetrationBonus, ref int MaxPenetrationBonus, int AV, bool Critical, string Properties, string Hand, GameObject Attacker, GameObject Defender, GameObject Weapon, string RegisteredEvent, GameObject Target, int ID, int CascadeLevel, Func<IMeleePenetrationEvent> Generator)
		{
			bool flag = GameObject.Validate(ref Target);
			if (flag && Target.HasRegisteredEvent(RegisteredEvent))
			{
				Event @event = Event.New(RegisteredEvent, 0, 0, 0);
				@event.SetParameter("Penetrations", StatBonus);
				@event.SetParameter("MaxStrengthBonus", MaxStatBonus);
				@event.SetParameter("Attacker", Attacker);
				@event.SetParameter("Defender", Defender);
				@event.SetParameter("Weapon", Weapon);
				@event.SetParameter("PenBonus", PenetrationBonus);
				@event.SetParameter("CapBonus", MaxPenetrationBonus);
				@event.SetParameter("AV", AV);
				@event.SetParameter("Hand", Hand);
				@event.SetParameter("Properties", Properties);
				@event.SetFlag("Critical", Critical);
				flag = Target.FireEvent(@event);
				StatBonus = @event.GetIntParameter("Penetrations", 0);
				MaxStatBonus = @event.GetIntParameter("MaxStrengthBonus", 0);
				PenetrationBonus = @event.GetIntParameter("PenBonus", 0);
				MaxPenetrationBonus = @event.GetIntParameter("CapBonus", 0);
			}
			flag = flag && GameObject.Validate(ref Target);
			if (flag && Target.WantEvent(ID, CascadeLevel))
			{
				IMeleePenetrationEvent meleePenetrationEvent = Generator();
				meleePenetrationEvent.Penetrations = Penetrations;
				meleePenetrationEvent.StatBonus = StatBonus;
				meleePenetrationEvent.MaxStatBonus = MaxStatBonus;
				meleePenetrationEvent.Attacker = Attacker;
				meleePenetrationEvent.Defender = Defender;
				meleePenetrationEvent.Weapon = Weapon;
				meleePenetrationEvent.PenetrationBonus = PenetrationBonus;
				meleePenetrationEvent.MaxPenetrationBonus = MaxPenetrationBonus;
				meleePenetrationEvent.AV = AV;
				meleePenetrationEvent.Hand = Hand;
				meleePenetrationEvent.Properties = Properties;
				meleePenetrationEvent.Critical = Critical;
				flag = Target.HandleEvent(meleePenetrationEvent);
				Penetrations = meleePenetrationEvent.Penetrations;
				StatBonus = meleePenetrationEvent.StatBonus;
				MaxStatBonus = meleePenetrationEvent.MaxStatBonus;
				PenetrationBonus = meleePenetrationEvent.PenetrationBonus;
				MaxPenetrationBonus = meleePenetrationEvent.MaxPenetrationBonus;
			}
			return flag;
		}

		// Token: 0x0400282E RID: 10286
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400282F RID: 10287
		public GameObject Attacker;

		// Token: 0x04002830 RID: 10288
		public GameObject Defender;

		// Token: 0x04002831 RID: 10289
		public GameObject Weapon;

		// Token: 0x04002832 RID: 10290
		public string Properties;

		// Token: 0x04002833 RID: 10291
		public string Hand;

		// Token: 0x04002834 RID: 10292
		public int Penetrations;

		// Token: 0x04002835 RID: 10293
		public int StatBonus;

		// Token: 0x04002836 RID: 10294
		public int MaxStatBonus;

		// Token: 0x04002837 RID: 10295
		public int PenetrationBonus;

		// Token: 0x04002838 RID: 10296
		public int MaxPenetrationBonus;

		// Token: 0x04002839 RID: 10297
		public int AV;

		// Token: 0x0400283A RID: 10298
		public bool Critical;
	}
}
