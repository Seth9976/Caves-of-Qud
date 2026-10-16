using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000F85 RID: 3973
	[Serializable]
	public class MPOnEat : IPart
	{
		// Token: 0x06009DE6 RID: 40422 RVA: 0x00392E3A File Offset: 0x0039103A
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009DE7 RID: 40423 RVA: 0x00392E3D File Offset: 0x0039103D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009DE8 RID: 40424 RVA: 0x00392E54 File Offset: 0x00391054
		public override bool FireEvent(Event E)
		{
			if (E.ID == "OnEat")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Eater");
				int num = Stat.Roll(this.Value, null);
				if (gameObjectParameter != null && gameObjectParameter.CanGainMP())
				{
					gameObjectParameter.GainMP(num);
					if (gameObjectParameter.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
						{
							"You gain ",
							num.ToString(),
							" mutation ",
							(num == 1) ? "point" : "points",
							"!"
						}), null, true);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400414D RID: 16717
		public string Value = "1";
	}
}
