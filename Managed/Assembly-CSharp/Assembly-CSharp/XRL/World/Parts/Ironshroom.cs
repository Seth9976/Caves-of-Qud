using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E7B RID: 3707
	[Serializable]
	public class Ironshroom : IPart
	{
		// Token: 0x060095A3 RID: 38307 RVA: 0x0036231B File Offset: 0x0036051B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ObjectEnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x060095A4 RID: 38308 RVA: 0x00362330 File Offset: 0x00360530
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ObjectEnteredCell")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Object");
				if (gameObjectParameter != null && gameObjectParameter.IsCombat)
				{
					IComponent<GameObject>.XDidYToZ(gameObjectParameter, "are", "impaled by", this.ParentObject, null, null, null, null, null, gameObjectParameter, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					if (gameObjectParameter.TakeDamage(Stat.Random(1, 10), "from %t impalement.", null, null, null, null, this.ParentObject, null, null, null, false, false, false, false, false, false, false, false, false, 5, null))
					{
						gameObjectParameter.Bloodsplatter();
						gameObjectParameter.ApplyEffect(new Bleeding("1d3", 30, this.ParentObject, false, false, false, false), null);
					}
				}
			}
			return base.FireEvent(E);
		}
	}
}
