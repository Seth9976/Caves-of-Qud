using System;

namespace XRL.World
{
	// Token: 0x020007CB RID: 1995
	[GameEvent(Base = true, Cascade = 1)]
	public abstract class IHitDiceEvent : MinEvent
	{
		// Token: 0x06005085 RID: 20613 RVA: 0x00186EAD File Offset: 0x001850AD
		public override int GetCascadeLevel()
		{
			return IHitDiceEvent.CascadeLevel;
		}

		// Token: 0x06005086 RID: 20614 RVA: 0x00186EB4 File Offset: 0x001850B4
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005087 RID: 20615 RVA: 0x00186EBD File Offset: 0x001850BD
		public override void Reset()
		{
			base.Reset();
			this.Attacker = null;
			this.Defender = null;
			this.Weapon = null;
			this.PenetrationBonus = 0;
			this.AV = 0;
			this.ShieldBlocked = false;
		}

		// Token: 0x06005088 RID: 20616 RVA: 0x00186EF0 File Offset: 0x001850F0
		public static bool Process(ref int PenetrationBonus, ref int AV, ref bool ShieldBlocked, GameObject Attacker, GameObject Defender, GameObject Weapon, string RegisteredEvent, GameObject Target, int ID, int CascadeLevel, Func<GameObject, GameObject, GameObject, int, int, bool, IHitDiceEvent> Generator)
		{
			if (GameObject.Validate(ref Target))
			{
				if (Target.HasRegisteredEvent(RegisteredEvent))
				{
					Event @event = Event.New(RegisteredEvent, 0, 0, 0);
					@event.SetParameter("PenetrationBonus", PenetrationBonus);
					@event.SetParameter("Attacker", Attacker);
					@event.SetParameter("Defender", Defender);
					@event.SetParameter("Weapon", Weapon);
					@event.SetParameter("AV", AV);
					@event.SetFlag("ShieldBlocked", ShieldBlocked);
					try
					{
						if (!Target.FireEvent(@event))
						{
							return false;
						}
					}
					finally
					{
						PenetrationBonus = @event.GetIntParameter("PenetrationBonus", 0);
						AV = @event.GetIntParameter("AV", 0);
						ShieldBlocked = @event.HasFlag("ShieldBlocked");
					}
				}
				if (Target.WantEvent(ID, CascadeLevel))
				{
					IHitDiceEvent hitDiceEvent = Generator(Attacker, Defender, Weapon, PenetrationBonus, AV, ShieldBlocked);
					try
					{
						if (!Target.HandleEvent(hitDiceEvent))
						{
							return false;
						}
					}
					finally
					{
						PenetrationBonus = hitDiceEvent.PenetrationBonus;
						AV = hitDiceEvent.AV;
						ShieldBlocked = hitDiceEvent.ShieldBlocked;
					}
					return true;
				}
				return true;
			}
			return true;
		}

		// Token: 0x0400280A RID: 10250
		public new static readonly int CascadeLevel = 1;

		// Token: 0x0400280B RID: 10251
		public GameObject Attacker;

		// Token: 0x0400280C RID: 10252
		public GameObject Defender;

		// Token: 0x0400280D RID: 10253
		public GameObject Weapon;

		// Token: 0x0400280E RID: 10254
		public int PenetrationBonus;

		// Token: 0x0400280F RID: 10255
		public int AV;

		// Token: 0x04002810 RID: 10256
		public bool ShieldBlocked;
	}
}
