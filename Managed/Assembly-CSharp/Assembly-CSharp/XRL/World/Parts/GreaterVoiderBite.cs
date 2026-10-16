using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E78 RID: 3704
	[Serializable]
	public class GreaterVoiderBite : IPart
	{
		// Token: 0x06009587 RID: 38279 RVA: 0x003617BC File Offset: 0x0035F9BC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009588 RID: 38280 RVA: 0x003617D4 File Offset: 0x0035F9D4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Defender");
				if (gameObjectParameter != null && this.ParentObject.Equipped != null)
				{
					this.ParentObject.Equipped.FireEvent(Event.New("GreaterVoiderBiteHit", "Target", gameObjectParameter));
				}
			}
			return true;
		}
	}
}
