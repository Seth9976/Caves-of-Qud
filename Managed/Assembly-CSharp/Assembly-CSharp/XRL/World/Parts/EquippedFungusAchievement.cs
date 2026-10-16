using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000DF3 RID: 3571
	[Serializable]
	public class EquippedFungusAchievement : IPart
	{
		// Token: 0x060090B1 RID: 37041 RVA: 0x003429DE File Offset: 0x00340BDE
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x060090B2 RID: 37042 RVA: 0x003429E1 File Offset: 0x00340BE1
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Equipped");
			base.Register(Object, Registrar);
		}

		// Token: 0x060090B3 RID: 37043 RVA: 0x003429F8 File Offset: 0x00340BF8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Equipped" && !this.Triggered)
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("EquippingObject");
				if (gameObjectParameter != null && gameObjectParameter.IsPlayer())
				{
					Body body = gameObjectParameter.Body;
					List<string> list = new List<string>();
					foreach (GameObject gameObject in body.GetEquippedObjects())
					{
						if (gameObject.HasPart<EquippedFungusAchievement>() && !list.Contains(gameObject.Blueprint))
						{
							list.Add(gameObject.Blueprint);
						}
					}
					if (list.Count >= 3)
					{
						Achievement.GET_FUNGAL_INFECTIONS.Unlock();
					}
					this.Triggered = true;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003C42 RID: 15426
		public bool Triggered;
	}
}
