using System;

namespace XRL.World.Parts
{
	// Token: 0x020010D6 RID: 4310
	[Serializable]
	public class CrumblesOnHit : IPart
	{
		// Token: 0x0600AB41 RID: 43841 RVA: 0x003EAC95 File Offset: 0x003E8E95
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AB42 RID: 43842 RVA: 0x003EAC98 File Offset: 0x003E8E98
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			Registrar.Register("WeaponThrowHit");
			Registrar.Register("WeaponAfterAttack");
			Registrar.Register("ThrownProjectileHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB43 RID: 43843 RVA: 0x003EACD0 File Offset: 0x003E8ED0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" || E.ID == "WeaponThrowHit")
			{
				if (this.Chance.in100())
				{
					this.Crumbling = true;
				}
			}
			else if (this.Crumbling && (E.ID == "WeaponAfterAttack" || E.ID == "ThrownProjectileHit"))
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				if (gameObjectParameter != null && gameObjectParameter.IsPlayer())
				{
					base.DidX("crumble", "to dust", "!", null, null, null, gameObjectParameter, false, false, null, null, false, false, false, false, false, null);
					SoundManager.PlaySound("Sounds/Damage/sfx_destroy_stone", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
				}
				this.ParentObject.Destroy(null, true, false, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x040046A2 RID: 18082
		public int Chance = 100;

		// Token: 0x040046A3 RID: 18083
		[NonSerialized]
		private bool Crumbling;
	}
}
