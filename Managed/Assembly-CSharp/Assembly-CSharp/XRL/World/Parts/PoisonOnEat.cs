using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F89 RID: 3977
	[Serializable]
	public class PoisonOnEat : IPart
	{
		// Token: 0x06009DF4 RID: 40436 RVA: 0x003930EB File Offset: 0x003912EB
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009DF5 RID: 40437 RVA: 0x00393100 File Offset: 0x00391300
		public override bool FireEvent(Event E)
		{
			if (E.ID == "OnEat")
			{
				E.GetGameObjectParameter("Eater").ApplyEffect(new Poisoned(this.Duration, this.Damage, this.Strength, null, false, false), null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004153 RID: 16723
		public int Duration = 40;

		// Token: 0x04004154 RID: 16724
		public string Damage = "2d6";

		// Token: 0x04004155 RID: 16725
		public int Strength = 30;
	}
}
