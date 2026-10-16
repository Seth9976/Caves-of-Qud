using System;
using XRL.Language;

namespace XRL.World.Effects
{
	// Token: 0x02000C54 RID: 3156
	public class CookingDomainArmor_OnPenetration : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x060080A8 RID: 32936 RVA: 0x002F4E66 File Offset: 0x002F3066
		public override void Init(GameObject target)
		{
			this.Tier = 2;
			base.Init(target);
		}

		// Token: 0x060080A9 RID: 32937 RVA: 0x002F4E76 File Offset: 0x002F3076
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature suffer@s " + Grammar.A(this.Tier, false) + "X or greater physical penetration,";
		}

		// Token: 0x060080AA RID: 32938 RVA: 0x002F4E93 File Offset: 0x002F3093
		public override string GetTemplatedTriggerDescription()
		{
			return "whenever @thisCreature suffer@s " + Grammar.A(this.Tier, false) + "X or greater physical penetration,";
		}

		// Token: 0x060080AB RID: 32939 RVA: 0x002F4EB0 File Offset: 0x002F30B0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DefenderHit");
			Registrar.Register("DefenderMissileWeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x060080AC RID: 32940 RVA: 0x002F4ED0 File Offset: 0x002F30D0
		public override bool FireEvent(Event E)
		{
			if ((E.ID == "DefenderHit" || E.ID == "DefenderMissileWeaponHit") && E.GetIntParameter("Penetrations", 0) >= this.Tier)
			{
				base.Trigger();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003902 RID: 14594
		public int Tier = 2;
	}
}
