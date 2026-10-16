using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000EAC RID: 3756
	[Serializable]
	public class EngulfingDamage : IPart
	{
		// Token: 0x060096D1 RID: 38609 RVA: 0x0036B704 File Offset: 0x00369904
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060096D2 RID: 38610 RVA: 0x0036B707 File Offset: 0x00369907
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurnEngulfing");
			base.Register(Object, Registrar);
		}

		// Token: 0x060096D3 RID: 38611 RVA: 0x0036B71C File Offset: 0x0036991C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurnEngulfing")
			{
				GameObject parameter = E.GetParameter<GameObject>("Object");
				if (parameter != null)
				{
					Damage damage = new Damage(Stat.Roll(this.Amount, null));
					Event @event = Event.New("TakeDamage", 0, 0, 0);
					@event.AddParameter("Damage", damage);
					@event.AddParameter("Owner", this.ParentObject);
					@event.AddParameter("Attacker", this.ParentObject);
					@event.AddParameter("Message", this.DamageMessage);
					parameter.FireEvent(@event);
				}
			}
			return true;
		}

		// Token: 0x04003EA0 RID: 16032
		public string Amount = "1-6";

		// Token: 0x04003EA1 RID: 16033
		public string DamageMessage = "from %t digestive enzymes!";
	}
}
