using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010CB RID: 4299
	[Serializable]
	public class Empty_Tonic_Applicator : IPart
	{
		// Token: 0x0600AB04 RID: 43780 RVA: 0x003E8CF8 File Offset: 0x003E6EF8
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyTonic");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB05 RID: 43781 RVA: 0x003E8D10 File Offset: 0x003E6F10
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplyTonic")
			{
				return E.GetGameObjectParameter("Target").ApplyEffect(new Bleeding("1d2+2", 30, this.ParentObject, false, false, false, false), null);
			}
			return base.FireEvent(E);
		}
	}
}
