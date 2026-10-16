using System;
using XRL.Liquids;

namespace XRL.World.Parts
{
	// Token: 0x020010F0 RID: 4336
	[Serializable]
	public class RandomStatusOnHit : IPart
	{
		// Token: 0x0600ABC8 RID: 43976 RVA: 0x003EEE7E File Offset: 0x003ED07E
		public RandomStatusOnHit()
		{
		}

		// Token: 0x0600ABC9 RID: 43977 RVA: 0x003EEE8E File Offset: 0x003ED08E
		public RandomStatusOnHit(int Chance)
			: this()
		{
			this.Chance = Chance;
		}

		// Token: 0x0600ABCA RID: 43978 RVA: 0x003EEE9D File Offset: 0x003ED09D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600ABCB RID: 43979 RVA: 0x003EEEA0 File Offset: 0x003ED0A0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerHit");
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600ABCC RID: 43980 RVA: 0x003EEEC0 File Offset: 0x003ED0C0
		public override bool FireEvent(Event E)
		{
			if (E.ID.EndsWith("Hit"))
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObject = gameObjectParameter;
				GameObject parentObject = this.ParentObject;
				string text = "Part RandomStatusOnHit Activation";
				GameObject gameObject2 = gameObjectParameter2;
				if (GetSpecialEffectChanceEvent.GetFor(gameObject, parentObject, text, this.Chance, gameObject2, null, true, false).in100())
				{
					LiquidWarmStatic.ApplyRandomEffectTo(gameObjectParameter2, gameObjectParameter.GetTier(), false, false);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040046FD RID: 18173
		public int Chance = 100;
	}
}
