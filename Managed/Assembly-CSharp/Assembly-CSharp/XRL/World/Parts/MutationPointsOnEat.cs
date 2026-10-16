using System;
using XRL.Language;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F87 RID: 3975
	[Serializable]
	public class MutationPointsOnEat : IPart
	{
		// Token: 0x06009DEE RID: 40430 RVA: 0x00392F7B File Offset: 0x0039117B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009DEF RID: 40431 RVA: 0x00392F90 File Offset: 0x00391190
		public override bool FireEvent(Event E)
		{
			if (E.ID == "OnEat")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Eater");
				if (gameObjectParameter.IsTrueKin())
				{
					gameObjectParameter.ApplyEffect(new Poisoned(this.TrueKinPoisonDuration, this.TrueKinPoisonDamage, this.TrueKinPoisonStrength, null, false, false), null);
				}
				else if (gameObjectParameter.CanGainMP())
				{
					gameObjectParameter.GainMP(this.Points);
					if (gameObjectParameter.IsPlayer())
					{
						Popup.Show(string.Concat(new string[]
						{
							"Your genome destabilizes and you gain ",
							Grammar.Cardinal(this.Points),
							" mutation ",
							(this.Points == 1) ? "point" : "points",
							"."
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400414F RID: 16719
		public int Points = 1;

		// Token: 0x04004150 RID: 16720
		public int TrueKinPoisonDuration = 40;

		// Token: 0x04004151 RID: 16721
		public string TrueKinPoisonDamage = "2d6";

		// Token: 0x04004152 RID: 16722
		public int TrueKinPoisonStrength = 30;
	}
}
