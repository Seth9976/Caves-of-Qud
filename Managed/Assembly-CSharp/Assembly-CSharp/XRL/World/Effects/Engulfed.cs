using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D33 RID: 3379
	[Serializable]
	public class Engulfed : Effect
	{
		// Token: 0x06008548 RID: 34120 RVA: 0x00301CDA File Offset: 0x002FFEDA
		public Engulfed()
		{
			this.Duration = 1;
		}

		// Token: 0x06008549 RID: 34121 RVA: 0x00301CEC File Offset: 0x002FFEEC
		public Engulfed(GameObject EngulfedBy)
			: this()
		{
			this.EngulfedBy = EngulfedBy;
			this.DisplayName = "{{B|engulfed by " + EngulfedBy.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + "}}";
		}

		// Token: 0x0600854A RID: 34122 RVA: 0x00301D3F File Offset: 0x002FFF3F
		public override int GetEffectType()
		{
			return 33554464;
		}

		// Token: 0x0600854B RID: 34123 RVA: 0x00301D46 File Offset: 0x002FFF46
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600854C RID: 34124 RVA: 0x00301D4C File Offset: 0x002FFF4C
		public override bool Render(RenderEvent E)
		{
			if (GameObject.Validate(ref this.EngulfedBy))
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num >= 0 && num <= 30)
				{
					E.ColorString = this.EngulfedBy.Render.ColorString;
					E.DetailColor = this.EngulfedBy.Render.DetailColor;
					E.Tile = this.EngulfedBy.Render.Tile;
					E.RenderString = this.EngulfedBy.Render.RenderString;
				}
			}
			return base.Render(E);
		}

		// Token: 0x0600854D RID: 34125 RVA: 0x00301DD8 File Offset: 0x002FFFD8
		public override string GetDetails()
		{
			if (!this.CheckEngulfedBy(true))
			{
				return null;
			}
			Engulfing part = this.EngulfedBy.GetPart<Engulfing>();
			if (part.MustBeUnderstood && base.Object.IsPlayer() && !this.EngulfedBy.Understood())
			{
				return null;
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (!part.EffectDescriptionPrefix.IsNullOrEmpty())
			{
				stringBuilder.Append(part.EffectDescriptionPrefix);
			}
			if (part.AVBonus != 0)
			{
				stringBuilder.Append(part.AVBonus.Signed(false)).Append(" AV.\n");
			}
			if (part.DVPenalty != 0)
			{
				stringBuilder.Append((-part.DVPenalty).Signed(false)).Append(" DV.\n");
			}
			if (!part.Damage.IsNullOrEmpty() && part.DamageChance > 0)
			{
				if (part.DamageChance >= 100)
				{
					stringBuilder.Append("Inflicts periodic damage.\n");
				}
				else
				{
					stringBuilder.Append("May inflict periodic damage.\n");
				}
			}
			stringBuilder.Append("Cannot move until @they break free.\n");
			if (part.ExitSaveTarget > 0)
			{
				if (!part.ExitSaveStat.IsNullOrEmpty())
				{
					stringBuilder.Append("Exiting may not succeed, and is a task dependent on " + part.ExitSaveStat + ".\n");
				}
				else
				{
					stringBuilder.Append("Exiting may not succeed.\n");
				}
			}
			if (!part.Damage.IsNullOrEmpty() && part.ExitDamageChance > 0)
			{
				if (part.ExitDamageChance >= 100)
				{
					if (part.ExitSaveTarget > 0)
					{
						if (part.ExitDamageFailOnly)
						{
							stringBuilder.Append("Attempting to exit and failing inflicts damage.\n");
						}
						else
						{
							stringBuilder.Append("Attempting to exit inflicts damage.\n");
						}
					}
					else
					{
						stringBuilder.Append("Exiting inflicts damage.\n");
					}
				}
				else if (part.ExitSaveTarget > 0)
				{
					if (part.ExitDamageFailOnly)
					{
						stringBuilder.Append("Attempting to exit and failing may inflict damage.\n");
					}
					else
					{
						stringBuilder.Append("Attempting to exit may inflict damage.\n");
					}
				}
				else
				{
					stringBuilder.Append("Exiting may inflict damage.\n");
				}
			}
			if (!part.EffectDescriptionPostfix.IsNullOrEmpty())
			{
				stringBuilder.Append(part.EffectDescriptionPostfix);
			}
			return stringBuilder.ToString().TrimEnd('\n');
		}

		// Token: 0x0600854E RID: 34126 RVA: 0x00301FD0 File Offset: 0x003001D0
		public override bool Apply(GameObject Object)
		{
			Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_organicEngulfment", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			if (!Object.FireEvent(Event.New("ApplyEngulfed", 0, 0, 0)))
			{
				return false;
			}
			if (Object.CurrentCell != null)
			{
				Object.CurrentCell.FireEvent(Event.New("ObjectBecomingEngulfed", "Object", Object));
			}
			GameObject engulfedBy = this.EngulfedBy;
			if (engulfedBy != null)
			{
				engulfedBy.MovementModeChanged("Engulfing", true);
			}
			GameObject engulfedBy2 = this.EngulfedBy;
			if (engulfedBy2 != null)
			{
				engulfedBy2.BodyPositionChanged("Engulfing", true);
			}
			Object.MovementModeChanged("Engulfed", true);
			Object.BodyPositionChanged("Engulfed", true);
			this.ApplyChanges();
			this.ApplyStats();
			return base.Apply(Object);
		}

		// Token: 0x0600854F RID: 34127 RVA: 0x0030209B File Offset: 0x0030029B
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
			this.UnapplyChanges();
		}

		// Token: 0x06008550 RID: 34128 RVA: 0x003020AC File Offset: 0x003002AC
		public bool IsEngulfedByValid()
		{
			return GameObject.Validate(ref this.EngulfedBy) && this.EngulfedBy.CurrentCell != null && base.Object != null && this.EngulfedBy.CurrentCell == base.Object.CurrentCell && base.Object.PhaseMatches(this.EngulfedBy);
		}

		// Token: 0x06008551 RID: 34129 RVA: 0x00302114 File Offset: 0x00300314
		public bool CheckEngulfedBy(bool AllowDisplayNameUpdate = true)
		{
			if (!this.IsEngulfedByValid())
			{
				this.Duration = 0;
				GameObject @object = base.Object;
				if (@object != null)
				{
					@object.RemoveEffect(this, true);
				}
				this.EngulfedBy = null;
				return false;
			}
			if (this.EngulfedBy != null && AllowDisplayNameUpdate)
			{
				this.DisplayName = "{{B|engulfed by " + this.EngulfedBy.an(int.MaxValue, null, null, false, true, false, false, true, false, true, false, false, true, false, null, false, null, false) + "}}";
			}
			return true;
		}

		// Token: 0x06008552 RID: 34130 RVA: 0x0030219C File Offset: 0x0030039C
		private void ApplyChangesCore()
		{
			if (this.ChangesWereApplied)
			{
				this.UnapplyChangesCore();
			}
			if (this.EngulfedBy != null)
			{
				Engulfing part = this.EngulfedBy.GetPart<Engulfing>();
				if (part != null && !part.AffectedProperties.IsNullOrEmpty())
				{
					Dictionary<string, int> propertyMap = part.PropertyMap;
					foreach (string text in propertyMap.Keys)
					{
						base.Object.ModIntProperty(text, propertyMap[text], true);
					}
				}
			}
			this.ChangesWereApplied = true;
		}

		// Token: 0x06008553 RID: 34131 RVA: 0x00302240 File Offset: 0x00300440
		public void ApplyChanges()
		{
			if (base.Object == null)
			{
				return;
			}
			if (this.ChangesWereApplied)
			{
				this.UnapplyChanges();
			}
			if (this.EngulfedBy != null)
			{
				Engulfing part = this.EngulfedBy.GetPart<Engulfing>();
				if (part != null)
				{
					if (part.MustBeUnderstood && base.Object.IsPlayer() && !this.EngulfedBy.Understood())
					{
						return;
					}
					if (part.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
					{
						return;
					}
					this.ApplyChangesCore();
					if (!part.ApplyChangesEventSelf.IsNullOrEmpty())
					{
						this.EngulfedBy.FireEvent(part.ApplyChangesEventSelf);
					}
					if (!part.ApplyChangesEventUser.IsNullOrEmpty())
					{
						base.Object.FireEvent(part.ApplyChangesEventUser);
					}
				}
			}
		}

		// Token: 0x06008554 RID: 34132 RVA: 0x00302314 File Offset: 0x00300514
		private void UnapplyChangesCore()
		{
			if (!this.ChangesWereApplied)
			{
				return;
			}
			if (this.EngulfedBy == null)
			{
				return;
			}
			Engulfing part = this.EngulfedBy.GetPart<Engulfing>();
			if (part == null)
			{
				return;
			}
			if (!part.AffectedProperties.IsNullOrEmpty())
			{
				Dictionary<string, int> propertyMap = part.PropertyMap;
				foreach (string text in propertyMap.Keys)
				{
					base.Object.ModIntProperty(text, -propertyMap[text], true);
				}
			}
			this.ChangesWereApplied = false;
		}

		// Token: 0x06008555 RID: 34133 RVA: 0x003023B4 File Offset: 0x003005B4
		public void UnapplyChanges()
		{
			if (base.Object == null)
			{
				return;
			}
			if (!this.ChangesWereApplied)
			{
				return;
			}
			if (this.EngulfedBy == null)
			{
				return;
			}
			Engulfing part = this.EngulfedBy.GetPart<Engulfing>();
			if (part == null)
			{
				return;
			}
			this.UnapplyChangesCore();
			if (!part.UnapplyChangesEventSelf.IsNullOrEmpty())
			{
				this.EngulfedBy.FireEvent(part.UnapplyChangesEventSelf);
			}
			if (!part.UnapplyChangesEventUser.IsNullOrEmpty())
			{
				base.Object.FireEvent(part.UnapplyChangesEventUser);
			}
		}

		// Token: 0x06008556 RID: 34134 RVA: 0x00302430 File Offset: 0x00300630
		private void ApplyStats()
		{
			if (this.EngulfedBy == null)
			{
				return;
			}
			Engulfing part = this.EngulfedBy.GetPart<Engulfing>();
			if (part == null)
			{
				return;
			}
			base.StatShifter.SetStatShift(base.Object, "AV", part.AVBonus, false);
			base.StatShifter.SetStatShift(base.Object, "DV", -part.DVPenalty, false);
		}

		// Token: 0x06008557 RID: 34135 RVA: 0x00302493 File Offset: 0x00300693
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x06008558 RID: 34136 RVA: 0x003024A6 File Offset: 0x003006A6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<CanChangeMovementModeEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<GetCompanionStatusEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == BeforeDeathRemovalEvent.ID;
		}

		// Token: 0x06008559 RID: 34137 RVA: 0x003024E4 File Offset: 0x003006E4
		public override bool HandleEvent(CanChangeMovementModeEvent E)
		{
			if (E.Object == base.Object && this.CheckEngulfedBy(true))
			{
				if (E.ShowMessage)
				{
					E.Object.Fail("You cannot do that while engulfed by " + this.EngulfedBy.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600855A RID: 34138 RVA: 0x0030255E File Offset: 0x0030075E
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0)
			{
				this.CheckEngulfedBy(true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600855B RID: 34139 RVA: 0x00302578 File Offset: 0x00300778
		public override bool HandleEvent(GetCompanionStatusEvent E)
		{
			if (E.Object == base.Object)
			{
				E.AddStatus("engulfed", 100);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600855C RID: 34140 RVA: 0x0030259C File Offset: 0x0030079C
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Reference && this.CheckEngulfedBy(false))
			{
				E.AddTag("[{{B|engulfed by " + this.EngulfedBy.an(int.MaxValue, null, null, false, false, false, false, false, false, true, true, false, true, false, null, false, null, false) + "}}]", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600855D RID: 34141 RVA: 0x00302600 File Offset: 0x00300800
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.CheckEngulfedBy(true))
			{
				Engulfing part = this.EngulfedBy.GetPart<Engulfing>();
				Engulfing engulfing = part;
				GameObject @object = base.Object;
				int num = this.TurnsEngulfed + 1;
				this.TurnsEngulfed = num;
				engulfing.ProcessTurnEngulfed(@object, num);
				if (this.ChangesWereApplied)
				{
					if (part.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
					{
						this.UnapplyChanges();
					}
				}
				else if (!part.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					this.ApplyChanges();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600855E RID: 34142 RVA: 0x003026AF File Offset: 0x003008AF
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			base.Object.RemoveEffect(this, true);
			return base.HandleEvent(E);
		}

		// Token: 0x0600855F RID: 34143 RVA: 0x003026C8 File Offset: 0x003008C8
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("BeginMove");
			Registrar.Register("BodyPositionChanged");
			Registrar.Register("CanChangeBodyPosition");
			Registrar.Register("LeaveCell");
			Registrar.Register("MovementModeChanged");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008560 RID: 34144 RVA: 0x0030272C File Offset: 0x0030092C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginMove")
			{
				if (this.Duration > 0 && !E.HasFlag("Teleporting") && this.CheckEngulfedBy(true))
				{
					base.Object.PerformMeleeAttack(this.EngulfedBy);
					if (!GameObject.Validate(ref this.EngulfedBy))
					{
						GameObject @object = base.Object;
						if (@object != null)
						{
							@object.RemoveEffect(this, true);
						}
					}
					return false;
				}
			}
			else if (E.ID == "LeaveCell" || E.ID == "MovementModeChanged" || E.ID == "BodyPositionChanged")
			{
				base.Object.RemoveEffect(this, true);
			}
			else if (E.ID == "CanChangeBodyPosition")
			{
				if (this.CheckEngulfedBy(true))
				{
					if (E.HasFlag("ShowMessage") && base.Object.IsPlayer())
					{
						Popup.Show("You cannot do that while engulfed by " + this.EngulfedBy.the + this.EngulfedBy.ShortDisplayName + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					return false;
				}
			}
			else if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyStats();
				this.UnapplyChangesCore();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyChangesCore();
				this.ApplyStats();
			}
			return base.FireEvent(E);
		}

		// Token: 0x040039B4 RID: 14772
		public GameObject EngulfedBy;

		// Token: 0x040039B5 RID: 14773
		public bool ChangesWereApplied;

		// Token: 0x040039B6 RID: 14774
		public int TurnsEngulfed;
	}
}
