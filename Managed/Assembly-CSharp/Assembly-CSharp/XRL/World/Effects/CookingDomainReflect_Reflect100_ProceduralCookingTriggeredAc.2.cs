using System;
using System.Collections.Generic;

namespace XRL.World.Effects
{
	// Token: 0x02000CEB RID: 3307
	[Serializable]
	public class CookingDomainReflect_Reflect100_ProceduralCookingTriggeredAction_Effect : BasicTriggeredCookingEffect
	{
		// Token: 0x06008332 RID: 33586 RVA: 0x002FA2B0 File Offset: 0x002F84B0
		public override string GetDetails()
		{
			if (this.Times > 1)
			{
				return "Reflect 100% damage the next " + this.Times.ToString() + " times @they take damage.";
			}
			return "Reflect 100% damage the next time @they take damage.";
		}

		// Token: 0x06008333 RID: 33587 RVA: 0x002FA2DB File Offset: 0x002F84DB
		public CookingDomainReflect_Reflect100_ProceduralCookingTriggeredAction_Effect()
		{
			this.Duration = 50;
			this.DisplayName = null;
		}

		// Token: 0x06008334 RID: 33588 RVA: 0x002FA2F9 File Offset: 0x002F84F9
		public override void ApplyEffect(GameObject Object)
		{
			Object.RegisterEffectEvent(this, "BeforeApplyDamage");
			base.ApplyEffect(Object);
		}

		// Token: 0x06008335 RID: 33589 RVA: 0x002FA30E File Offset: 0x002F850E
		public override void RemoveEffect(GameObject Object)
		{
			Object.UnregisterEffectEvent(this, "BeforeApplyDamage");
			base.RemoveEffect(Object);
		}

		// Token: 0x06008336 RID: 33590 RVA: 0x002FA324 File Offset: 0x002F8524
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeApplyDamage")
			{
				if (this.Duration <= 0)
				{
					return true;
				}
				if (this.Times <= 0)
				{
					return true;
				}
				Damage damage = E.GetParameter("Damage") as Damage;
				GameObject gameObject = E.GetParameter("Owner") as GameObject;
				if (damage.Amount > 0 && !damage.HasAttribute("reflected"))
				{
					int amount = damage.Amount;
					if (amount > 0 && gameObject != null && gameObject != base.Object)
					{
						Event @event = new Event("TakeDamage");
						Damage damage2 = new Damage(amount);
						damage2.Attributes = new List<string>(damage.Attributes);
						if (!damage2.HasAttribute("reflected"))
						{
							damage2.Attributes.Add("reflected");
						}
						@event.AddParameter("Damage", damage2);
						@event.AddParameter("Owner", base.Object);
						@event.AddParameter("Attacker", base.Object);
						@event.AddParameter("Message", "from %t tiny spines!");
						if (base.Object.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								"&GYou reflect ",
								amount.ToString(),
								" damage back at ",
								gameObject.the,
								gameObject.ShortDisplayName,
								"&G."
							}), null, true);
						}
						else if (gameObject.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								"&R",
								base.Object.The,
								base.Object.ShortDisplayName,
								"&R",
								base.Object.GetVerb("reflect", true, false, false, true, false),
								" ",
								amount.ToString(),
								" damage back at you."
							}), null, true);
						}
						else if (base.Object.IsVisible())
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								base.Object.The,
								base.Object.ShortDisplayName,
								"&y",
								base.Object.GetVerb("reflect", true, false, false, true, false),
								" ",
								amount.ToString(),
								" damage back at ",
								gameObject.the,
								gameObject.ShortDisplayName,
								"&y."
							}), null, true);
						}
						gameObject.FireEvent(@event);
						base.Object.FireEvent("ReflectedDamage");
						this.Times--;
						if (this.Times <= 0)
						{
							this.Duration = 0;
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003960 RID: 14688
		public int Times = 1;
	}
}
