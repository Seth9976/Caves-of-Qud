using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000DE8 RID: 3560
	[Serializable]
	public class Skulk_Tonic_Applicator : IPart
	{
		// Token: 0x0600907C RID: 36988 RVA: 0x00341FD2 File Offset: 0x003401D2
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600907D RID: 36989 RVA: 0x00341FD5 File Offset: 0x003401D5
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyTonic");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600907E RID: 36990 RVA: 0x00341FEC File Offset: 0x003401EC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplyTonic")
			{
				int intParameter = E.GetIntParameter("Dosage", 0);
				if (intParameter <= 0)
				{
					return false;
				}
				GameObject gameObjectParameter = E.GetGameObjectParameter("Actor");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Subject");
				int @for = GetTonicDurationEvent.GetFor(this.ParentObject, gameObjectParameter, gameObjectParameter2, "Skulk", 1000 * intParameter + Stat.Random(intParameter, 200 * intParameter), intParameter, false);
				gameObjectParameter2.ApplyEffect(new Skulk_Tonic(@for), null);
			}
			return base.FireEvent(E);
		}
	}
}
