using System;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Effects
{
	// Token: 0x02000D95 RID: 3477
	[Serializable]
	public class Stuck : Effect, ITierInitialized
	{
		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06008A7E RID: 35454 RVA: 0x00315DFB File Offset: 0x00313FFB
		// (set) Token: 0x06008A7F RID: 35455 RVA: 0x00315E03 File Offset: 0x00314003
		public int SaveTarget
		{
			get
			{
				return this._SaveTarget;
			}
			set
			{
				this._SaveTarget = value;
				this.BonusSetup();
			}
		}

		// Token: 0x06008A80 RID: 35456 RVA: 0x00315E14 File Offset: 0x00314014
		public Stuck()
		{
			this.DisplayName = this.Adjective;
			this.BonusSetup();
		}

		// Token: 0x06008A81 RID: 35457 RVA: 0x00315E90 File Offset: 0x00314090
		public Stuck(int Duration, int SaveTarget = 15, string SaveVs = "Stuck Restraint", GameObject DestroyOnBreak = null, string Adjective = "stuck", string Preposition = "in", string DependsOn = null, string DefendingSaveBonusVs = "Move", float DefendingSaveBonusFactor = 0.25f, float KineticResistanceLinearBonus = 1f, float KineticResistancePercentageBonus = 1f, bool DependsOnMustBeFrozen = false, bool DependsOnMustBeSolid = false)
			: this()
		{
			this.Duration = Duration;
			this.SaveTarget = SaveTarget;
			this.SaveVs = SaveVs;
			this.DestroyOnBreak = DestroyOnBreak;
			this.Adjective = Adjective;
			this.Preposition = Preposition;
			this.DependsOn = DependsOn;
			this.DefendingSaveBonusVs = DefendingSaveBonusVs;
			this.DefendingSaveBonusFactor = DefendingSaveBonusFactor;
			this.KineticResistanceLinearBonusFactor = KineticResistanceLinearBonus;
			this.KineticResistancePercentageBonusFactor = KineticResistancePercentageBonus;
			this.DependsOnMustBeFrozen = DependsOnMustBeFrozen;
			this.DependsOnMustBeSolid = DependsOnMustBeSolid;
			this.BonusSetup();
			this.DisplayName = this.GetStateDescription(null);
		}

		// Token: 0x06008A82 RID: 35458 RVA: 0x00315F1B File Offset: 0x0031411B
		public void Initialize(int Tier)
		{
			Tier = Stat.Random(Tier - 2, Tier + 2);
			if (Tier < 1)
			{
				Tier = 1;
			}
			if (Tier > 8)
			{
				Tier = 8;
			}
			this.Duration = Stat.Random(12, 15);
			this.SaveTarget = 11 + Tier * 4;
		}

		// Token: 0x06008A83 RID: 35459 RVA: 0x00315F54 File Offset: 0x00314154
		private void BonusSetup()
		{
			this.DefendingSaveBonus = (int)Math.Round((double)((float)this.SaveTarget * this.DefendingSaveBonusFactor));
			this.KineticResistanceLinearBonus = (int)Math.Round((double)((float)this.SaveTarget * this.KineticResistanceLinearBonusFactor));
			this.KineticResistancePercentageBonus = (int)Math.Round((double)((float)this.SaveTarget * this.KineticResistancePercentageBonusFactor));
		}

		// Token: 0x06008A84 RID: 35460 RVA: 0x00315FB2 File Offset: 0x003141B2
		public override int GetEffectType()
		{
			return 117440640;
		}

		// Token: 0x06008A85 RID: 35461 RVA: 0x00315FB9 File Offset: 0x003141B9
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008A86 RID: 35462 RVA: 0x00315FBC File Offset: 0x003141BC
		public override bool SameAs(Effect e)
		{
			Stuck stuck = e as Stuck;
			return stuck.DestroyOnBreak == this.DestroyOnBreak && !(stuck.Adjective != this.Adjective) && !(stuck.Preposition != this.Preposition) && stuck.SaveTarget == this.SaveTarget && !(stuck.SaveVs != this.SaveVs) && !(stuck.DependsOn != this.DependsOn) && !(stuck.DefendingSaveBonusVs != this.DefendingSaveBonusVs) && stuck.DefendingSaveBonusFactor == this.DefendingSaveBonusFactor && stuck.KineticResistanceLinearBonusFactor == this.KineticResistanceLinearBonusFactor && stuck.KineticResistancePercentageBonusFactor == this.KineticResistancePercentageBonusFactor && stuck.DefendingSaveBonusFactor == this.DefendingSaveBonusFactor && stuck.KineticResistanceLinearBonusFactor == this.KineticResistanceLinearBonusFactor && stuck.KineticResistancePercentageBonusFactor == this.KineticResistancePercentageBonusFactor && stuck.DefendingSaveBonus == this.DefendingSaveBonus && stuck.KineticResistanceLinearBonus == this.KineticResistanceLinearBonus && stuck.KineticResistancePercentageBonus == this.KineticResistancePercentageBonus && base.SameAs(e);
		}

		// Token: 0x06008A87 RID: 35463 RVA: 0x003160F0 File Offset: 0x003142F0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<CanChangeMovementModeEvent>.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<GetCompanionStatusEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetKineticResistanceEvent>.ID || ID == GetNavigationWeightEvent.ID || ID == ModifyDefendingSaveEvent.ID;
		}

		// Token: 0x06008A88 RID: 35464 RVA: 0x0031615C File Offset: 0x0031435C
		public override bool HandleEvent(CanChangeMovementModeEvent E)
		{
			if (this.Duration > 0 && E.Object == base.Object)
			{
				if (E.ShowMessage)
				{
					E.Object.Fail("You are " + this.Adjective + "!");
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008A89 RID: 35465 RVA: 0x003161B2 File Offset: 0x003143B2
		public override bool HandleEvent(GetCompanionStatusEvent E)
		{
			if (E.Object == base.Object)
			{
				E.AddStatus("stuck", 60);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008A8A RID: 35466 RVA: 0x003161D6 File Offset: 0x003143D6
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Reference)
			{
				E.AddTag("[{{B|" + this.DisplayName + "}}]", 20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008A8B RID: 35467 RVA: 0x00316204 File Offset: 0x00314404
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (!E.Flying)
			{
				E.Uncacheable = true;
				E.MinWeight(100);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008A8C RID: 35468 RVA: 0x00316224 File Offset: 0x00314424
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0)
			{
				this.CheckDependsOn(false, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008A8D RID: 35469 RVA: 0x0031623F File Offset: 0x0031443F
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.CheckDependsOn(true, true);
			return base.HandleEvent(E);
		}

		// Token: 0x06008A8E RID: 35470 RVA: 0x00316251 File Offset: 0x00314451
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.CheckDependsOn(true, true);
			return base.HandleEvent(E);
		}

		// Token: 0x06008A8F RID: 35471 RVA: 0x00316263 File Offset: 0x00314463
		public override bool HandleEvent(EndTurnEvent E)
		{
			GameObject.Validate(ref this.DestroyOnBreak);
			if (this.Duration > 0)
			{
				this.CheckDependsOn(false, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008A90 RID: 35472 RVA: 0x0031628A File Offset: 0x0031448A
		public override bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			if (this.DefendingSaveBonus != 0 && SavingThrows.Applicable(this.DefendingSaveBonusVs, E.Vs))
			{
				E.Roll += this.DefendingSaveBonus;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008A91 RID: 35473 RVA: 0x003162C4 File Offset: 0x003144C4
		public override bool HandleEvent(GetKineticResistanceEvent E)
		{
			if (this.KineticResistanceLinearBonus > 0)
			{
				E.LinearIncrease += this.KineticResistanceLinearBonus;
			}
			else if (this.KineticResistanceLinearBonus < 0)
			{
				E.LinearReduction += -this.KineticResistanceLinearBonus;
			}
			if (this.KineticResistancePercentageBonus > 0)
			{
				E.PercentageIncrease += this.KineticResistancePercentageBonus;
			}
			else if (this.KineticResistancePercentageBonus < 0)
			{
				E.PercentageReduction += -this.KineticResistancePercentageBonus;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008A92 RID: 35474 RVA: 0x00316350 File Offset: 0x00314550
		public GameObject CheckDependsOn(bool Immediate = false, bool AllowDisplayNameUpdate = true)
		{
			GameObject gameObject = null;
			if (!this.DependsOn.IsNullOrEmpty())
			{
				gameObject = GameObject.FindByID(this.DependsOn);
				if (!GameObject.Validate(ref gameObject) || !gameObject.InSameOrAdjacentCellTo(base.Object) || (!gameObject.PhaseMatches(base.Object) && !gameObject.HasTagOrProperty("IgnorePhaseMatchForStuck")) || (this.DependsOnMustBeFrozen && !gameObject.IsFrozen()) || (this.DependsOnMustBeSolid && !gameObject.ConsiderSolidFor(base.Object, false)))
				{
					this.DependsOn = null;
					if (Immediate)
					{
						base.Object.RemoveEffect(this, true);
					}
					else
					{
						this.Duration = 0;
					}
				}
				else if (AllowDisplayNameUpdate)
				{
					this.DisplayName = this.GetStateDescription(gameObject);
				}
			}
			return gameObject;
		}

		// Token: 0x06008A93 RID: 35475 RVA: 0x00316408 File Offset: 0x00314608
		public string GetStateDescription(GameObject From = null)
		{
			if (From == null && !this.DependsOn.IsNullOrEmpty())
			{
				From = GameObject.FindByID(this.DependsOn);
			}
			if (GameObject.Validate(ref From))
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				stringBuilder.Append(this.Adjective);
				if (!this.Preposition.IsNullOrEmpty())
				{
					stringBuilder.Append(' ').Append(this.Preposition);
				}
				stringBuilder.Append(' ').Append(From.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false));
				return stringBuilder.ToString();
			}
			return this.Adjective;
		}

		// Token: 0x06008A94 RID: 35476 RVA: 0x003164B0 File Offset: 0x003146B0
		public GameObject CheckDependsOn(string Invalidate, bool Immediate = false, bool AllowDisplayNameUpdate = true)
		{
			if (!this.DependsOn.IsNullOrEmpty() && Invalidate == this.DependsOn)
			{
				if (Immediate)
				{
					base.Object.RemoveEffect(this, true);
				}
				else
				{
					this.Duration = 0;
				}
				return null;
			}
			return this.CheckDependsOn(Immediate, AllowDisplayNameUpdate);
		}

		// Token: 0x06008A95 RID: 35477 RVA: 0x003164FC File Offset: 0x003146FC
		public override string GetDetails()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append(this.Adjective.Capitalize()).Append(' ');
			GameObject gameObject = this.CheckDependsOn(false, true);
			if (gameObject != null)
			{
				stringBuilder.Append("in ").Append(gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false));
			}
			else
			{
				stringBuilder.Append("where ").Append(base.Object.it).Append(base.Object.Is)
					.Append('.');
			}
			SavingThrows.AppendSaveBonusDescription(stringBuilder, this.DefendingSaveBonus, this.DefendingSaveBonusVs, false, false, true);
			return stringBuilder.ToString();
		}

		// Token: 0x06008A96 RID: 35478 RVA: 0x003165BC File Offset: 0x003147BC
		public override bool Apply(GameObject Object)
		{
			if (!Object.HasStat("Energy"))
			{
				return false;
			}
			if (Object.GetMatterPhase() > 1)
			{
				return false;
			}
			if (!Object.CanChangeMovementMode("Stuck", false, true, false, true))
			{
				return false;
			}
			if (!Object.FireEvent("ApplyStuck"))
			{
				return false;
			}
			this.CheckDependsOn(false, true);
			base.DidX("are", this.DisplayName, "!", null, null, null, Object, false, false, null, null, false, false, false, false, false, null);
			Object.ParticleText("*" + this.Adjective + "*", IComponent<GameObject>.ConsequentialColorChar(null, Object), false, 1.5f, -8f);
			Object.ForfeitTurn(false);
			Object.MovementModeChanged("Stuck", true);
			if (Object.IsFlying)
			{
				Flight.Fall(Object);
			}
			return true;
		}

		// Token: 0x06008A97 RID: 35479 RVA: 0x00316681 File Offset: 0x00314881
		public override void Remove(GameObject Object)
		{
			if (GameObject.Validate(ref this.DestroyOnBreak))
			{
				this.DestroyOnBreak.Destroy(null, false, false, null);
			}
			base.Remove(Object);
		}

		// Token: 0x06008A98 RID: 35480 RVA: 0x003166A8 File Offset: 0x003148A8
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginAttack");
			Registrar.Register("BodyPositionChanged");
			Registrar.Register("CanChangeBodyPosition");
			Registrar.Register("CheckStuck");
			Registrar.Register("IsMobile");
			Registrar.Register("LeaveCell");
			Registrar.Register("MovementModeChanged");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008A99 RID: 35481 RVA: 0x0031670C File Offset: 0x0031490C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "IsMobile")
			{
				if (this.Duration > 0 && !base.Object.IsTryingToJoinPartyLeader())
				{
					return false;
				}
			}
			else if (E.ID == "LeaveCell" || E.ID == "BeginAttack")
			{
				if (E.ID == "LeaveCell" && (E.HasFlag("Forced") || E.GetStringParameter("Type", null) == "Teleporting" || base.Object.IsTryingToJoinPartyLeader()))
				{
					base.Object.RemoveEffect(this, true);
				}
				else if (this.Duration > 0)
				{
					if (base.Object.MakeSave("Strength", this.SaveTarget - base.Object.GetIntProperty("Stable", 0), null, null, this.SaveVs, false, false, false, false, GameObject.FindByID(this.DependsOn)))
					{
						base.DidX("break", "free", "!", null, null, base.Object, null, false, false, null, null, false, false, false, false, false, null);
						base.Object.RemoveEffect(this, true);
					}
					else
					{
						if (base.Object.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage("You are " + this.Adjective + "!", null, true);
						}
						if (!E.HasParameter("Dragging") && !E.HasFlag("Forced"))
						{
							base.Object.UseEnergy(1000);
						}
						if (E.ID == "LeaveCell")
						{
							return false;
						}
					}
				}
			}
			else if (E.ID == "CanChangeBodyPosition")
			{
				if (this.Duration > 0)
				{
					if (E.HasFlag("ShowMessage") && base.Object.IsPlayer())
					{
						Popup.Show("You are " + this.Adjective + "!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					return false;
				}
			}
			else if (E.ID == "MovementModeChanged" || E.ID == "BodyPositionChanged")
			{
				if (this.Duration > 0)
				{
					base.Object.RemoveEffect(this, true);
				}
			}
			else if (E.ID == "CheckStuck")
			{
				string stringParameter = E.GetStringParameter("Invalidate", null);
				if (!string.IsNullOrEmpty(stringParameter))
				{
					this.CheckDependsOn(stringParameter, true, true);
				}
				else
				{
					this.CheckDependsOn(true, true);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008A9A RID: 35482 RVA: 0x00316998 File Offset: 0x00314B98
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 50 && num < 60)
				{
					E.Tile = "terrain/sw_web.bmp";
					E.RenderString = "\u000f";
					E.ColorString = "&Y^K";
				}
			}
			return true;
		}

		// Token: 0x04003A9E RID: 15006
		public const string DEFAULT_ADJECTIVE = "stuck";

		// Token: 0x04003A9F RID: 15007
		public const string DEFAULT_PREPOSITION = "in";

		// Token: 0x04003AA0 RID: 15008
		public const int DEFAULT_SAVE_TARGET = 15;

		// Token: 0x04003AA1 RID: 15009
		public const string DEFAULT_SAVE_VS = "Stuck Restraint";

		// Token: 0x04003AA2 RID: 15010
		public const string DEFAULT_DEFENDING_SAVE_BONUS_VS = "Move";

		// Token: 0x04003AA3 RID: 15011
		public const float DEFAULT_DEFENDING_SAVE_BONUS_FACTOR = 0.25f;

		// Token: 0x04003AA4 RID: 15012
		public const float DEFAULT_KINETIC_RESISTANCE_LINEAR_BONUS_FACTOR = 1f;

		// Token: 0x04003AA5 RID: 15013
		public const float DEFAULT_KINETIC_RESISTANCE_PERCENTAGE_BONUS_FACTOR = 1f;

		// Token: 0x04003AA6 RID: 15014
		public GameObject DestroyOnBreak;

		// Token: 0x04003AA7 RID: 15015
		public string Adjective = "stuck";

		// Token: 0x04003AA8 RID: 15016
		public string Preposition = "in";

		// Token: 0x04003AA9 RID: 15017
		public int _SaveTarget = 15;

		// Token: 0x04003AAA RID: 15018
		public string SaveVs = "Stuck Restraint";

		// Token: 0x04003AAB RID: 15019
		public string DependsOn;

		// Token: 0x04003AAC RID: 15020
		public string DefendingSaveBonusVs = "Move";

		// Token: 0x04003AAD RID: 15021
		public float DefendingSaveBonusFactor = 0.25f;

		// Token: 0x04003AAE RID: 15022
		public float KineticResistanceLinearBonusFactor = 1f;

		// Token: 0x04003AAF RID: 15023
		public float KineticResistancePercentageBonusFactor = 1f;

		// Token: 0x04003AB0 RID: 15024
		public int DefendingSaveBonus;

		// Token: 0x04003AB1 RID: 15025
		public int KineticResistanceLinearBonus;

		// Token: 0x04003AB2 RID: 15026
		public int KineticResistancePercentageBonus;

		// Token: 0x04003AB3 RID: 15027
		public bool DependsOnMustBeFrozen;

		// Token: 0x04003AB4 RID: 15028
		public bool DependsOnMustBeSolid;
	}
}
