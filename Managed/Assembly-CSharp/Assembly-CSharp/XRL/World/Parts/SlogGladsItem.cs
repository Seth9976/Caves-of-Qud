using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02001199 RID: 4505
	[Serializable]
	public class SlogGladsItem : IPart
	{
		// Token: 0x0600B317 RID: 45847 RVA: 0x004155D4 File Offset: 0x004137D4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Unequipped");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B318 RID: 45848 RVA: 0x004155EC File Offset: 0x004137EC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Unequipped")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("UnequippingObject");
				if (gameObjectParameter != null)
				{
					SlogGlands part = gameObjectParameter.GetPart<SlogGlands>();
					if (part != null)
					{
						part.Unmutate(gameObjectParameter);
					}
				}
			}
			return base.FireEvent(E);
		}
	}
}
