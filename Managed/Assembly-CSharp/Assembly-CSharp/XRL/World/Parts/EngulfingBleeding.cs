using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000EAA RID: 3754
	[Serializable]
	public class EngulfingBleeding : IPart
	{
		// Token: 0x060096C9 RID: 38601 RVA: 0x0036B3C6 File Offset: 0x003695C6
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060096CA RID: 38602 RVA: 0x0036B3C9 File Offset: 0x003695C9
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurnEngulfing");
			base.Register(Object, Registrar);
		}

		// Token: 0x060096CB RID: 38603 RVA: 0x0036B3E0 File Offset: 0x003695E0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurnEngulfing")
			{
				this.Countdown--;
				if (this.Countdown <= 0)
				{
					this.Countdown = this.BleedFrequency;
					GameObject parameter = E.GetParameter<GameObject>("Object");
					if (parameter != null)
					{
						parameter.ApplyEffect(new Bleeding(this.Damage, this.BaseSave, this.ParentObject, true, false, false, false), null);
					}
				}
			}
			return true;
		}

		// Token: 0x04003E93 RID: 16019
		public int BleedFrequency = 3;

		// Token: 0x04003E94 RID: 16020
		public int Countdown;

		// Token: 0x04003E95 RID: 16021
		public string Damage = "1-2";

		// Token: 0x04003E96 RID: 16022
		public int BaseSave = 20;
	}
}
