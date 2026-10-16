using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000E74 RID: 3700
	[Serializable]
	public class GelatenousPalmProperties : IPart
	{
		// Token: 0x0600956C RID: 38252 RVA: 0x00360D2D File Offset: 0x0035EF2D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600956D RID: 38253 RVA: 0x00360D30 File Offset: 0x0035EF30
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DefendMeleeHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600956E RID: 38254 RVA: 0x00360D48 File Offset: 0x0035EF48
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DefendMeleeHit")
			{
				GameObject gameObject = E.GetGameObjectParameter("Weapon");
				GameObject parentObject = this.ParentObject;
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				gameObject = Disarming.Disarm(parentObject, gameObjectParameter, 100, "Strength", "Agility", null, null);
				if (gameObject != null)
				{
					this.ParentObject.ReceiveObject(gameObject, false, null);
					if (gameObjectParameter.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage(gameObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " lost in the goop!", null, true);
					}
				}
			}
			return base.FireEvent(E);
		}
	}
}
