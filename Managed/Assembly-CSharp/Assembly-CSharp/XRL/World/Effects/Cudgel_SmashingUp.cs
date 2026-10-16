using System;
using XRL.Core;
using XRL.Rules;
using XRL.World.Parts.Skill;

namespace XRL.World.Effects
{
	// Token: 0x02000DB9 RID: 3513
	[Serializable]
	public class Cudgel_SmashingUp : Effect, ITierInitialized
	{
		// Token: 0x06008C25 RID: 35877 RVA: 0x0031B85D File Offset: 0x00319A5D
		public Cudgel_SmashingUp()
		{
			this.DisplayName = "demolishing";
		}

		// Token: 0x06008C26 RID: 35878 RVA: 0x0031B870 File Offset: 0x00319A70
		public Cudgel_SmashingUp(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008C27 RID: 35879 RVA: 0x0031B87F File Offset: 0x00319A7F
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(5, 15);
		}

		// Token: 0x06008C28 RID: 35880 RVA: 0x0031B88F File Offset: 0x00319A8F
		public override int GetEffectType()
		{
			return 67108992;
		}

		// Token: 0x06008C29 RID: 35881 RVA: 0x0031B896 File Offset: 0x00319A96
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008C2A RID: 35882 RVA: 0x0031B899 File Offset: 0x00319A99
		public override string GetDetails()
		{
			return "Slam has no cooldown.\n100% chance to daze with cudgels.";
		}

		// Token: 0x06008C2B RID: 35883 RVA: 0x0031B8A0 File Offset: 0x00319AA0
		public override bool Apply(GameObject Object)
		{
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_enraged", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return true;
		}

		// Token: 0x06008C2C RID: 35884 RVA: 0x0031B8DC File Offset: 0x00319ADC
		public override void Remove(GameObject Object)
		{
			Cudgel_Slam part = Object.GetPart<Cudgel_Slam>();
			if (part != null)
			{
				part.CooldownMyActivatedAbility(part.ActivatedAbilityID, 50, null, null, false);
			}
			base.Remove(Object);
		}

		// Token: 0x06008C2D RID: 35885 RVA: 0x0031B90C File Offset: 0x00319B0C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008C2E RID: 35886 RVA: 0x0031B924 File Offset: 0x00319B24
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 45 && num < 55)
				{
					E.Tile = null;
					E.RenderString = "!";
					E.ColorString = "&R";
				}
			}
			return true;
		}

		// Token: 0x06008C2F RID: 35887 RVA: 0x0031B96C File Offset: 0x00319B6C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				if (this.Duration > 0)
				{
					this.Duration--;
				}
				if (this.Duration > 0 && base.Object.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage(this.Duration.Things("turn remains", "turns remain") + " until you stop demolishing.", null, true);
				}
				return true;
			}
			return base.FireEvent(E);
		}
	}
}
