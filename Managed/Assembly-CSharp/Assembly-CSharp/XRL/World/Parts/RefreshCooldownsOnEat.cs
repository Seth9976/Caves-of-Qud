using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F8B RID: 3979
	[Serializable]
	public class RefreshCooldownsOnEat : IPart
	{
		// Token: 0x06009DFE RID: 40446 RVA: 0x003932E0 File Offset: 0x003914E0
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009DFF RID: 40447 RVA: 0x003932E3 File Offset: 0x003914E3
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
			Registrar.Register("AfterEat");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009E00 RID: 40448 RVA: 0x00393304 File Offset: 0x00391504
		public override bool FireEvent(Event E)
		{
			if (E.ID == "OnEat")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Eater");
				ActivatedAbilities activatedAbilities = ((gameObjectParameter != null) ? gameObjectParameter.ActivatedAbilities : null);
				if (activatedAbilities != null)
				{
					foreach (ActivatedAbilityEntry activatedAbilityEntry in activatedAbilities.AbilityByGuid.Values)
					{
						if (activatedAbilityEntry.Class.Contains("Mental") && activatedAbilityEntry.Cooldown > 0)
						{
							activatedAbilityEntry.Refresh(this);
						}
					}
				}
			}
			if (E.ID == "AfterEat" && E.GetGameObjectParameter("Eater").IsPlayer())
			{
				IComponent<GameObject>.EmitMessage(IComponent<GameObject>.ThePlayer, "{{rules|All your mental cooldowns are refreshed.}}", ' ', false, true, false, null, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004157 RID: 16727
		public bool Controlled;
	}
}
