using System;
using System.Collections.Generic;

namespace XRL.World.Parts.Skill
{
	/// This part is not used in the base game.
	// Token: 0x020013B2 RID: 5042
	[Serializable]
	public class TenfoldPath_Ret : BaseInitiatorySkill
	{
		// Token: 0x0600C2B1 RID: 49841 RVA: 0x00471D69 File Offset: 0x0046FF69
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ApplyEffectEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == SingletonEvent<GetEnergyCostEvent>.ID || ID == PooledEvent<GetPsionicSifrahSetupEvent>.ID;
		}

		// Token: 0x0600C2B2 RID: 49842 RVA: 0x00471D97 File Offset: 0x0046FF97
		public override bool HandleEvent(GetPsionicSifrahSetupEvent E)
		{
			E.Rating++;
			E.Turns++;
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2B3 RID: 49843 RVA: 0x00471DBC File Offset: 0x0046FFBC
		public override bool HandleEvent(GetEnergyCostEvent E)
		{
			if (E.Type != null && E.Type.Contains("Mental"))
			{
				E.PercentageReduction += 50;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2B4 RID: 49844 RVA: 0x00471DF0 File Offset: 0x0046FFF0
		public override bool HandleEvent(ApplyEffectEvent E)
		{
			if (this.AffectEffect(E.Effect) && 25.in100() && this.ParentObject.IsPlayer())
			{
				string stateDescription = E.Effect.GetStateDescription();
				if (stateDescription.IsNullOrEmpty())
				{
					IComponent<GameObject>.AddPlayerMessage("A supernal force helps you shake off the effect!", 'g', true);
				}
				else
				{
					IComponent<GameObject>.AddPlayerMessage("A supernal force helps you shake off being " + stateDescription + "!", 'g', true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2B5 RID: 49845 RVA: 0x00471E64 File Offset: 0x00470064
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.ParentObject.Effects != null && 25.in100())
			{
				TenfoldPath_Ret.targetEffects.Clear();
				int i = 0;
				int count = this.ParentObject.Effects.Count;
				while (i < count)
				{
					if (this.AffectEffect(this.ParentObject.Effects[i]))
					{
						TenfoldPath_Ret.targetEffects.Add(this.ParentObject.Effects[i]);
					}
					i++;
				}
				if (TenfoldPath_Ret.targetEffects.Count > 0)
				{
					Effect randomElement = TenfoldPath_Ret.targetEffects.GetRandomElement<Effect>(null);
					if (randomElement != null)
					{
						if (this.ParentObject.IsPlayer())
						{
							string stateDescription = randomElement.GetStateDescription();
							if (stateDescription.IsNullOrEmpty())
							{
								IComponent<GameObject>.AddPlayerMessage("A supernal force helps you shake off a mental state!", 'g', true);
							}
							else
							{
								IComponent<GameObject>.AddPlayerMessage("A supernal force helps you shake off being " + stateDescription + "!", 'g', true);
							}
						}
						this.ParentObject.RemoveEffect(randomElement, true);
					}
				}
				TenfoldPath_Ret.targetEffects.Clear();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2B6 RID: 49846 RVA: 0x00471F66 File Offset: 0x00470166
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C2B7 RID: 49847 RVA: 0x00471F69 File Offset: 0x00470169
		private bool AffectEffect(Effect FX)
		{
			return FX.IsOfTypes(100663298) && !FX.IsOfType(134217728);
		}

		// Token: 0x04004DA5 RID: 19877
		public const int SHAKE_OFF = 25;

		// Token: 0x04004DA6 RID: 19878
		public const int MENTAL_ACTION_REDUCTION = 50;

		// Token: 0x04004DA7 RID: 19879
		private static List<Effect> targetEffects = new List<Effect>();
	}
}
