using System;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02000F8C RID: 3980
	[Serializable]
	public class SecretsOnEat : IPart
	{
		// Token: 0x06009E02 RID: 40450 RVA: 0x003933F0 File Offset: 0x003915F0
		public override bool SameAs(IPart p)
		{
			return !((p as SecretsOnEat).Number != this.Number) && base.SameAs(p);
		}

		// Token: 0x06009E03 RID: 40451 RVA: 0x00393413 File Offset: 0x00391613
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Eaten");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009E04 RID: 40452 RVA: 0x00393428 File Offset: 0x00391628
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Eaten" && E.GetGameObjectParameter("Eater").IsPlayer())
			{
				int i = 0;
				int num = this.Number.RollCached();
				while (i < num)
				{
					JournalAPI.RevealRandomSecret();
					i++;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004158 RID: 16728
		public string Number = "1-2";
	}
}
