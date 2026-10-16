using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000C37 RID: 3127
	[Serializable]
	public class BasiliskPoison : Effect, ITierInitialized
	{
		// Token: 0x06007F6B RID: 32619 RVA: 0x002F02DD File Offset: 0x002EE4DD
		public BasiliskPoison()
		{
			this.DisplayName = "{{K|stony}}";
		}

		// Token: 0x06007F6C RID: 32620 RVA: 0x002F02F0 File Offset: 0x002EE4F0
		public BasiliskPoison(int Duration, GameObject Owner)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06007F6D RID: 32621 RVA: 0x002F02FF File Offset: 0x002EE4FF
		public override int GetEffectType()
		{
			return 117440516;
		}

		// Token: 0x06007F6E RID: 32622 RVA: 0x002F0306 File Offset: 0x002EE506
		public override string GetDetails()
		{
			return "-1d6 Move Speed per turn for 14-18 turns.\n[total:-{{C|" + this.TotalDebuff.ToString() + "}} Move Speed]";
		}

		// Token: 0x06007F6F RID: 32623 RVA: 0x002F0324 File Offset: 0x002EE524
		public override bool Apply(GameObject Object)
		{
			if (!Object.FireEvent("ApplyPoison"))
			{
				return false;
			}
			if (!ApplyEffectEvent.Check(Object, "Poison", this, null))
			{
				return false;
			}
			if (!ApplyEffectEvent.Check(Object, "BasiliskPoison", this, null))
			{
				return false;
			}
			if (Object.HasEffect<BasiliskPoison>())
			{
				return false;
			}
			Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_poisoned", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			this.Duration = Stat.Roll("1d5+13", null);
			return true;
		}

		// Token: 0x06007F70 RID: 32624 RVA: 0x002F03A8 File Offset: 0x002EE5A8
		public override void Remove(GameObject Object)
		{
			if (Object.HasStat("MoveSpeed") && this.TotalDebuff != 0)
			{
				Object.GetStat("MoveSpeed").Bonus -= this.TotalDebuff;
				this.TotalDebuff = 0;
			}
			base.Remove(Object);
		}

		// Token: 0x06007F71 RID: 32625 RVA: 0x002F03F5 File Offset: 0x002EE5F5
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			Registrar.Register("Recuperating");
			base.Register(Object, Registrar);
		}

		// Token: 0x06007F72 RID: 32626 RVA: 0x002F0418 File Offset: 0x002EE618
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 160;
			if (num > 0 && num < 10)
			{
				E.RenderString = "R";
				E.ColorString = "&K";
				return false;
			}
			return true;
		}

		// Token: 0x06007F73 RID: 32627 RVA: 0x002F0454 File Offset: 0x002EE654
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				this.Duration--;
				if (this.Duration > 0 && base.Object.HasStat("MoveSpeed"))
				{
					int num = Stat.Random(1, 6);
					this.TotalDebuff += num;
					base.Object.GetStat("MoveSpeed").Bonus += num;
					if (base.Object.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("You feel stiff as a stone.", null, true);
					}
				}
			}
			else if (E.ID == "Recuperating")
			{
				this.Duration = 0;
				base.DidX("feel", "less stiff", null, null, null, base.Object, null, false, false, null, null, false, false, false, false, false, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x040038CE RID: 14542
		public int TotalDebuff;
	}
}
