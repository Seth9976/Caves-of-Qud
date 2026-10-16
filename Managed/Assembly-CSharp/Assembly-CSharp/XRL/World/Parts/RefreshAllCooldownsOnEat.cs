using System;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x02000F8A RID: 3978
	[Serializable]
	public class RefreshAllCooldownsOnEat : IPart
	{
		// Token: 0x06009DF7 RID: 40439 RVA: 0x00393175 File Offset: 0x00391375
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009DF8 RID: 40440 RVA: 0x00393178 File Offset: 0x00391378
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x06009DF9 RID: 40441 RVA: 0x0039318E File Offset: 0x0039138E
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009DFA RID: 40442 RVA: 0x00393194 File Offset: 0x00391394
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ChancePerAbility >= 100)
			{
				E.Postfix.AppendRules("When eaten, each activated ability's cooldown is refreshed.");
			}
			else
			{
				E.Postfix.AppendRules("When eaten, there's " + Grammar.A(this.ChancePerAbility, false) + "% chance that each activated ability's cooldown is refreshed.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009DFB RID: 40443 RVA: 0x003931EC File Offset: 0x003913EC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009DFC RID: 40444 RVA: 0x00393204 File Offset: 0x00391404
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
						if (activatedAbilityEntry.Cooldown > 0 && this.ChancePerAbility.in100())
						{
							activatedAbilityEntry.Refresh(this);
							if (gameObjectParameter.IsPlayer())
							{
								IComponent<GameObject>.AddPlayerMessage("You suddenly feel ready to use " + activatedAbilityEntry.DisplayName + " again.", null, true);
							}
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004156 RID: 16726
		public int ChancePerAbility = 25;
	}
}
