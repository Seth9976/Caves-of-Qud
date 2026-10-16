using System;

namespace XRL.World.Parts
{
	// Token: 0x020010A7 RID: 4263
	[Serializable]
	public class SvenSword : IPart
	{
		// Token: 0x0600A9AB RID: 43435 RVA: 0x003E0467 File Offset: 0x003DE667
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A9AC RID: 43436 RVA: 0x003E046A File Offset: 0x003DE66A
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Equipped");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A9AD RID: 43437 RVA: 0x003E0480 File Offset: 0x003DE680
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Equipped")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("EquippingObject");
				if (gameObjectParameter != null && gameObjectParameter.IsPlayer())
				{
					foreach (GameObject gameObject in gameObjectParameter.Body.GetEquippedObjects((GameObject o) => o.HasPart<SvenSword>()))
					{
						if (gameObject != this.ParentObject && gameObject.Blueprint != this.ParentObject.Blueprint)
						{
							Achievement.WIELD_CAS_POL.Unlock();
						}
					}
				}
			}
			return base.FireEvent(E);
		}
	}
}
