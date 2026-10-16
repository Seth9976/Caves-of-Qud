using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F83 RID: 3971
	[Serializable]
	public class HealOnEat : IPart
	{
		// Token: 0x06009DDD RID: 40413 RVA: 0x00392BE8 File Offset: 0x00390DE8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetDefensiveItemListEvent.ID;
		}

		// Token: 0x06009DDE RID: 40414 RVA: 0x00392C00 File Offset: 0x00390E00
		public override bool HandleEvent(AIGetDefensiveItemListEvent E)
		{
			if (!this.ParentObject.IsImportant() && E.Actor.HasPart<Stomach>())
			{
				int hitpoints = E.Actor.hitpoints;
				int baseHitpoints = E.Actor.baseHitpoints;
				if (hitpoints < baseHitpoints)
				{
					int num = 0;
					int num2 = baseHitpoints - hitpoints;
					if (num2 >= baseHitpoints * 8 / 10)
					{
						num++;
					}
					if (num2 >= baseHitpoints * 9 / 10)
					{
						num++;
					}
					if (num > 0)
					{
						E.Add("Eat", num, this.ParentObject, true, true, null, null);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009DDF RID: 40415 RVA: 0x00392C83 File Offset: 0x00390E83
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009DE0 RID: 40416 RVA: 0x00392C98 File Offset: 0x00390E98
		public override bool FireEvent(Event E)
		{
			if (E.ID == "OnEat")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Eater");
				if (gameObjectParameter != null && gameObjectParameter.HasStat("Hitpoints"))
				{
					if (gameObjectParameter.Statistics["Hitpoints"].Penalty > 0)
					{
						gameObjectParameter.Heal(gameObjectParameter.Statistics["Hitpoints"].Penalty, true, true, false);
					}
					gameObjectParameter.FireEvent("Recuperating");
				}
			}
			return base.FireEvent(E);
		}
	}
}
