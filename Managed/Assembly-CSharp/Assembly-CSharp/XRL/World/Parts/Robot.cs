using System;
using XRL.World.AI.GoalHandlers;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02001200 RID: 4608
	[Serializable]
	public class Robot : IPart
	{
		// Token: 0x0600B743 RID: 46915 RVA: 0x0042B86D File Offset: 0x00429A6D
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600B744 RID: 46916 RVA: 0x0042B870 File Offset: 0x00429A70
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ApplyEffectEvent.ID || ID == BeforeDeathRemovalEvent.ID || ID == BeforeApplyDamageEvent.ID || ID == CanApplyEffectEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == PooledEvent<GetMutationTermEvent>.ID || ID == PooledEvent<GetScanTypeEvent>.ID || ID == PooledEvent<MutationsSubjectToEMPEvent>.ID || ID == PooledEvent<IsMutantEvent>.ID || ID == PooledEvent<IsSensableAsPsychicEvent>.ID || ID == PooledEvent<RespiresEvent>.ID || ID == PooledEvent<TransparentToEMPEvent>.ID;
		}

		// Token: 0x0600B745 RID: 46917 RVA: 0x0042B8F1 File Offset: 0x00429AF1
		public override bool HandleEvent(GetScanTypeEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.ScanType = Scanning.Scan.Tech;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B746 RID: 46918 RVA: 0x0042B90F File Offset: 0x00429B0F
		public override bool HandleEvent(IsMutantEvent E)
		{
			E.IsMutant = false;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B747 RID: 46919 RVA: 0x0042B91F File Offset: 0x00429B1F
		public override bool HandleEvent(IsSensableAsPsychicEvent E)
		{
			E.Sensable = false;
			return false;
		}

		// Token: 0x0600B748 RID: 46920 RVA: 0x0042B929 File Offset: 0x00429B29
		public override bool HandleEvent(GetMutationTermEvent E)
		{
			E.Term = "module";
			E.Color = "C";
			return false;
		}

		// Token: 0x0600B749 RID: 46921 RVA: 0x0042B942 File Offset: 0x00429B42
		public override bool HandleEvent(MutationsSubjectToEMPEvent E)
		{
			return !this.EMPable && base.HandleEvent(E);
		}

		// Token: 0x0600B74A RID: 46922 RVA: 0x0042B955 File Offset: 0x00429B55
		public override bool HandleEvent(RespiresEvent E)
		{
			return false;
		}

		// Token: 0x0600B74B RID: 46923 RVA: 0x0042B958 File Offset: 0x00429B58
		public override bool HandleEvent(TransparentToEMPEvent E)
		{
			return !this.EMPable && base.HandleEvent(E);
		}

		// Token: 0x0600B74C RID: 46924 RVA: 0x0042B96B File Offset: 0x00429B6B
		public override bool HandleEvent(CanApplyEffectEvent E)
		{
			return this.Check(E) && base.HandleEvent(E);
		}

		// Token: 0x0600B74D RID: 46925 RVA: 0x0042B97F File Offset: 0x00429B7F
		public override bool HandleEvent(ApplyEffectEvent E)
		{
			return this.Check(E) && base.HandleEvent(E);
		}

		// Token: 0x0600B74E RID: 46926 RVA: 0x0042B993 File Offset: 0x00429B93
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageReduction += 25;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B74F RID: 46927 RVA: 0x0042B9AC File Offset: 0x00429BAC
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Object == this.ParentObject && (E.Damage.HasAttribute("Poison") || E.Damage.HasAttribute("Asphyxiation")))
			{
				NotifyTargetImmuneEvent.Send(E.Weapon, E.Object, E.Actor, E.Damage, this);
				E.Damage.Amount = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B750 RID: 46928 RVA: 0x0042BA20 File Offset: 0x00429C20
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			GameObject gameObject = null;
			int num = 5;
			if (!this.ParentObject.HasPart<ExtradimensionalLoot>())
			{
				if (IComponent<GameObject>.ThePlayer.HasSkill("Tinkering_Scavenger"))
				{
					num = 35;
				}
				if (num.in100())
				{
					gameObject = GameObjectFactory.create(PopulationManager.RollOneFrom("Scrap " + Tier.Constrain(this.ParentObject.Stat("Level", 0) / 5).ToString(), null, null).Blueprint);
				}
			}
			if (gameObject != null && gameObject.IsReal)
			{
				IInventory dropInventory = this.ParentObject.GetDropInventory();
				if (dropInventory != null)
				{
					dropInventory.AddObjectToInventory(gameObject, null, false, false, true, null, E);
					DroppedEvent.Send(this.ParentObject, gameObject, false);
				}
				else
				{
					gameObject.Obliterate(null, false, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B751 RID: 46929 RVA: 0x0042BADE File Offset: 0x00429CDE
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("circuitry", 2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B752 RID: 46930 RVA: 0x0042BB01 File Offset: 0x00429D01
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B753 RID: 46931 RVA: 0x0042BB04 File Offset: 0x00429D04
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AIMovingTowardsTarget");
			Registrar.Register("ApplyEMP");
			Registrar.Register("ApplyingTonic");
			Registrar.Register("CanApplyAshPoison");
			Registrar.Register("CanApplySpores");
			Registrar.Register("CanApplyTonic");
			Registrar.Register("HasPowerConnectors");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B754 RID: 46932 RVA: 0x0042BB68 File Offset: 0x00429D68
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplyingTonic" || E.ID == "CanApplyTonic")
			{
				if (E.HasFlag("ShowMessage"))
				{
					GameObject gameObjectParameter = E.GetGameObjectParameter("Actor");
					GameObject gameObjectParameter2 = E.GetGameObjectParameter("Subject");
					GameObject gameObjectParameter3 = E.GetGameObjectParameter("Tonic");
					gameObjectParameter.Fail(gameObjectParameter2.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " robotic and cannot be affected by " + gameObjectParameter3.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
				}
				return false;
			}
			if (E.ID == "CanApplyTonic" || E.ID == "CanApplyAshPoison" || E.ID == "CanApplySpores")
			{
				return false;
			}
			if (E.ID == "HasPowerConnectors")
			{
				return false;
			}
			if (E.ID == "AIMovingTowardsTarget")
			{
				GameObject gameObjectParameter4 = E.GetGameObjectParameter("Target");
				if (gameObjectParameter4 != null && gameObjectParameter4.HasProperty("RobotStop") && !this.ParentObject.MakeSave("Willpower", 18, null, null, "RobotStop Restraint", false, false, false, false, gameObjectParameter4))
				{
					base.DidX("come", "to a complete stop", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
					this.ParentObject.ParticleBlip("&rX", 10, 0L, false);
					return false;
				}
			}
			else if (E.ID == "ApplyEMP" && this.EMPable)
			{
				Brain brain = this.ParentObject.Brain;
				brain.Goals.Clear();
				brain.PushGoal(new Dormant(E.GetIntParameter("Duration", 0)));
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B755 RID: 46933 RVA: 0x0042BD5C File Offset: 0x00429F5C
		private bool Check(IEffectCheckEvent E)
		{
			return !(E.Name == "AshPoison") && !(E.Name == "CardiacArrest") && !(E.Name == "Confusion") && !(E.Name == "CyberneticRejectionSyndrome") && !(E.Name == "Disease") && !(E.Name == "DiseaseOnset") && !(E.Name == "Poison") && !(E.Name == "PoisonGasPoison") && !(E.Name == "ShatterMentalArmor") && !(E.Name == "ToxicConfusion");
		}

		// Token: 0x040049C4 RID: 18884
		public bool EMPable = true;
	}
}
