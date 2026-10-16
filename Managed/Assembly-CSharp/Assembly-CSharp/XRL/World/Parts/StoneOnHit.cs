using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010F6 RID: 4342
	[Serializable]
	public class StoneOnHit : IPart
	{
		// Token: 0x0600ABEE RID: 44014 RVA: 0x003F04F3 File Offset: 0x003EE6F3
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600ABEF RID: 44015 RVA: 0x003F04F6 File Offset: 0x003EE6F6
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponDealDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600ABF0 RID: 44016 RVA: 0x003F050C File Offset: 0x003EE70C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponDealDamage")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				E.GetGameObjectParameter("Defender").ApplyEffect(new BasiliskPoison(1, gameObjectParameter), null);
			}
			return base.FireEvent(E);
		}
	}
}
