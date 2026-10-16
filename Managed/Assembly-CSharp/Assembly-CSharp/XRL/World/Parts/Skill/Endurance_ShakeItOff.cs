using System;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001340 RID: 4928
	[Serializable]
	public class Endurance_ShakeItOff : BaseSkill
	{
		// Token: 0x0600C05B RID: 49243 RVA: 0x00467746 File Offset: 0x00465946
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyStun");
			Registrar.Register("ApplyDazed");
			Registrar.Register("BeforeApplyDamage");
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C05C RID: 49244 RVA: 0x0046777C File Offset: 0x0046597C
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600C05D RID: 49245 RVA: 0x0046777F File Offset: 0x0046597F
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.TryToShakeItOff();
		}

		// Token: 0x0600C05E RID: 49246 RVA: 0x00467788 File Offset: 0x00465988
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeApplyDamage")
			{
				Damage damage = E.GetParameter("Damage") as Damage;
				if (damage != null && damage.HasAttribute("Poison"))
				{
					damage.Amount = damage.Amount * 3 / 4;
					if (damage.Amount <= 0)
					{
						return false;
					}
				}
			}
			else if ((E.ID == "ApplyStun" || E.ID == "ApplyDazed") && this.GetShakeItOffChance().in100())
			{
				if (this.ParentObject.IsPlayer())
				{
					if (E.ID == "ApplyStun")
					{
						this.ParentObject.ParticleText("Shook off stun!", 'G', false, 1.5f, -8f);
						IComponent<GameObject>.AddPlayerMessage("You shook off the stun.", 'g', true);
					}
					else if (E.ID == "ApplyDazed")
					{
						this.ParentObject.ParticleText("Shook off daze!", 'G', false, 1.5f, -8f);
						IComponent<GameObject>.AddPlayerMessage("You shook off the dazing.", 'g', true);
					}
				}
				else if (E.ID == "ApplyStun")
				{
					this.ParentObject.ParticleText("Shook off stun!", 'R', false, 1.5f, -8f);
					IComponent<GameObject>.AddPlayerMessage(this.ParentObject.The + this.ParentObject.ShortDisplayName + " shook off the stun.", null, true);
				}
				else if (E.ID == "ApplyDazed")
				{
					this.ParentObject.ParticleText("Shook off daze!", 'R', false, 1.5f, -8f);
					IComponent<GameObject>.AddPlayerMessage(this.ParentObject.The + this.ParentObject.ShortDisplayName + " shook off the dazing.", null, true);
				}
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C05F RID: 49247 RVA: 0x0046796F File Offset: 0x00465B6F
		public override bool AddSkill(GameObject GO)
		{
			return true;
		}

		// Token: 0x0600C060 RID: 49248 RVA: 0x00467972 File Offset: 0x00465B72
		public override bool RemoveSkill(GameObject GO)
		{
			return true;
		}

		// Token: 0x0600C061 RID: 49249 RVA: 0x00467978 File Offset: 0x00465B78
		public void TryToShakeItOff()
		{
			if (this.ParentObject.HasEffect<Dazed>() && this.GetShakeItOffChance().in100())
			{
				this.ParentObject.RemoveEffect<Dazed>();
				char c = ColorCoding.ConsequentialColorChar(this.ParentObject, null);
				this.ParentObject.ParticleText("Shook off daze!", c, false, 1.5f, -8f);
				base.DidX("shake", "off " + this.ParentObject.its + " daze", null, null, c.ToString() ?? "", null, null, false, false, null, null, false, false, false, false, false, null);
			}
			if (this.ParentObject.HasEffect<Stun>() && this.GetShakeItOffChance().in100())
			{
				this.ParentObject.RemoveEffect<Stun>();
				char c2 = ColorCoding.ConsequentialColorChar(this.ParentObject, null);
				this.ParentObject.ParticleText("Shook off stun!", c2, false, 1.5f, -8f);
				base.DidX("shake", "off the stun", null, null, c2.ToString() ?? "", null, null, false, false, null, null, false, false, false, false, false, null);
			}
		}

		// Token: 0x0600C062 RID: 49250 RVA: 0x00467A98 File Offset: 0x00465C98
		public int GetShakeItOffChance()
		{
			int num = this.ParentObject.Stat("Toughness", 0) - 10;
			Endurance_ShakeItOff.eGetShakeItOffChance.SetParameter("Chance", num);
			this.ParentObject.FireEvent(Endurance_ShakeItOff.eGetShakeItOffChance);
			this.ParentObject.FireEventOnBodyparts(Endurance_ShakeItOff.eGetShakeItOffChance);
			return Endurance_ShakeItOff.eGetShakeItOffChance.GetIntParameter("Chance", 0);
		}

		// Token: 0x04004D47 RID: 19783
		[NonSerialized]
		public static Event eGetShakeItOffChance = new Event("GetShakeItOffChance", "Chance", 0);
	}
}
