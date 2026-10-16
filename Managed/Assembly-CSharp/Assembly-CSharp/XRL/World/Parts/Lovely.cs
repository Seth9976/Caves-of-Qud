using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000ED8 RID: 3800
	[Serializable]
	public class Lovely : IPart
	{
		// Token: 0x0600981C RID: 38940 RVA: 0x003740B0 File Offset: 0x003722B0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterLookedAt");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600981D RID: 38941 RVA: 0x003740C8 File Offset: 0x003722C8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AfterLookedAt")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Looker");
				if (gameObjectParameter != null && !gameObjectParameter.HasEffect<Lovesick>())
				{
					gameObjectParameter.ApplyEffect(new Lovesick(Stat.Random(3000, 3600), this.ParentObject), null);
				}
			}
			return base.FireEvent(E);
		}
	}
}
