using System;
using System.Collections.Generic;
using XRL.Messages;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000CF2 RID: 3314
	[Serializable]
	public class CookingDomainReflect_UnitReflectDamage : ProceduralCookingEffectUnit
	{
		// Token: 0x06008350 RID: 33616 RVA: 0x002FA7A3 File Offset: 0x002F89A3
		public override void Init(GameObject target)
		{
			this.Tier = Stat.Random(3, 4);
			base.Init(target);
		}

		// Token: 0x06008351 RID: 33617 RVA: 0x002FA7B9 File Offset: 0x002F89B9
		public override string GetDescription()
		{
			return "Reflect " + this.Tier.ToString() + "% damage back at @their attackers, rounded up.";
		}

		// Token: 0x06008352 RID: 33618 RVA: 0x002FA7D5 File Offset: 0x002F89D5
		public override string GetTemplatedDescription()
		{
			return "Reflect 3-4% damage back at @their attackers, rounded up.";
		}

		// Token: 0x06008353 RID: 33619 RVA: 0x002FA7DC File Offset: 0x002F89DC
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.RegisterEffectEvent(parent, "BeforeApplyDamage");
		}

		// Token: 0x06008354 RID: 33620 RVA: 0x002FA7EA File Offset: 0x002F89EA
		public override void Remove(GameObject Object, Effect parent)
		{
			Object.UnregisterEffectEvent(parent, "BeforeApplyDamage");
		}

		// Token: 0x06008355 RID: 33621 RVA: 0x002FA7F8 File Offset: 0x002F89F8
		public override void FireEvent(Event E)
		{
			if (E.ID == "BeforeApplyDamage")
			{
				if (this.parent == null || this.parent.Object == null)
				{
					return;
				}
				Damage damage = E.GetParameter("Damage") as Damage;
				GameObject gameObjectParameter = E.GetGameObjectParameter("Owner");
				if (damage.Amount > 0 && !damage.HasAttribute("reflected"))
				{
					int num = (int)Math.Ceiling((double)((float)damage.Amount * (float)this.Tier / 100f));
					if (num > 0 && gameObjectParameter != null && gameObjectParameter != this.parent.Object)
					{
						Event @event = new Event("TakeDamage");
						Damage damage2 = new Damage(num);
						damage2.Attributes = new List<string>(damage.Attributes);
						if (!damage2.HasAttribute("reflected"))
						{
							damage2.Attributes.Add("reflected");
						}
						@event.AddParameter("Damage", damage2);
						@event.AddParameter("Owner", this.parent.Object);
						@event.AddParameter("Attacker", this.parent.Object);
						@event.AddParameter("Message", "from %t tiny spines!");
						if (this.parent.Object.IsPlayer())
						{
							MessageQueue.AddPlayerMessage(string.Concat(new string[]
							{
								"You reflect ",
								num.ToString(),
								" damage back at ",
								gameObjectParameter.the,
								gameObjectParameter.ShortDisplayName,
								"&y."
							}), null, true);
						}
						else if (gameObjectParameter.IsPlayer())
						{
							MessageQueue.AddPlayerMessage(string.Concat(new string[]
							{
								this.parent.Object.The,
								this.parent.Object.ShortDisplayName,
								"&y",
								this.parent.Object.GetVerb("reflect", true, false, false, true, false),
								" ",
								num.ToString(),
								" damage back at you."
							}), null, true);
						}
						else if (this.parent.Object.IsVisible())
						{
							MessageQueue.AddPlayerMessage(string.Concat(new string[]
							{
								this.parent.Object.The,
								this.parent.Object.ShortDisplayName,
								"&y",
								this.parent.Object.GetVerb("reflect", true, false, false, true, false),
								" ",
								num.ToString(),
								" damage back at ",
								gameObjectParameter.the,
								gameObjectParameter.ShortDisplayName,
								"&y."
							}), null, true);
						}
						gameObjectParameter.FireEvent(@event);
						this.parent.Object.FireEvent("ReflectedDamage");
					}
				}
			}
		}

		// Token: 0x04003964 RID: 14692
		public int Tier = 1;
	}
}
