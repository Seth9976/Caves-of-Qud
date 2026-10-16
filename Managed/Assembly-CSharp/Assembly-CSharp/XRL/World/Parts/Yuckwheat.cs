using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F92 RID: 3986
	[Serializable]
	public class Yuckwheat : IPart
	{
		// Token: 0x06009E1C RID: 40476 RVA: 0x00393B0E File Offset: 0x00391D0E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009E1D RID: 40477 RVA: 0x00393B23 File Offset: 0x00391D23
		public override bool FireEvent(Event E)
		{
			if (E.ID == "OnEat")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Eater");
				gameObjectParameter.RemoveEffect<Confused>();
				gameObjectParameter.RemoveEffect<Poisoned>();
			}
			return base.FireEvent(E);
		}
	}
}
