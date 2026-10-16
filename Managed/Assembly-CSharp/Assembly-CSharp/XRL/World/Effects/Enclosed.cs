using System;
using System.Collections.Generic;
using System.Text;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D32 RID: 3378
	[Serializable]
	public class Enclosed : Effect
	{
		// Token: 0x06008531 RID: 34097 RVA: 0x00301293 File Offset: 0x002FF493
		public Enclosed()
		{
			this.Duration = 1;
		}

		// Token: 0x06008532 RID: 34098 RVA: 0x003012A4 File Offset: 0x002FF4A4
		public Enclosed(GameObject EnclosedBy)
			: this()
		{
			this.EnclosedBy = EnclosedBy;
			this.DisplayName = "{{C|enclosed in " + EnclosedBy.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + "}}";
		}

		// Token: 0x06008533 RID: 34099 RVA: 0x003012F7 File Offset: 0x002FF4F7
		public override int GetEffectType()
		{
			return 32;
		}

		// Token: 0x06008534 RID: 34100 RVA: 0x003012FB File Offset: 0x002FF4FB
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008535 RID: 34101 RVA: 0x00301300 File Offset: 0x002FF500
		public override string GetDetails()
		{
			if (!this.CheckEnclosedBy())
			{
				return null;
			}
			Enclosing part = this.EnclosedBy.GetPart<Enclosing>();
			if (part.MustBeUnderstood && base.Object.IsPlayer() && !this.EnclosedBy.Understood())
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
			stringBuilder.Append("Must spend a turn exiting before moving.\n");
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

		// Token: 0x06008536 RID: 34102 RVA: 0x003014F8 File Offset: 0x002FF6F8
		public override bool Apply(GameObject Object)
		{
			Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_mechanicalEngulfment", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			if (Object.FireEvent("ApplyEnclosed"))
			{
				if (Object.CurrentCell != null)
				{
					Object.CurrentCell.FireEvent(Event.New("ObjectBecomingEnclosed", "Object", Object));
				}
				this.ApplyChanges();
				this.ApplyStats();
				this.ApplyRenderLayer();
				return true;
			}
			return false;
		}

		// Token: 0x06008537 RID: 34103 RVA: 0x00301575 File Offset: 0x002FF775
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
			this.UnapplyChanges();
			this.UnapplyRenderLayer();
		}

		// Token: 0x06008538 RID: 34104 RVA: 0x0030158C File Offset: 0x002FF78C
		public bool IsEnclosedByValid()
		{
			return this.EnclosedBy != null && !this.EnclosedBy.IsInvalid() && this.EnclosedBy.CurrentCell != null && base.Object != null && base.Object.CurrentCell != null && this.EnclosedBy.CurrentCell == base.Object.CurrentCell;
		}

		// Token: 0x06008539 RID: 34105 RVA: 0x003015F8 File Offset: 0x002FF7F8
		public bool CheckEnclosedBy()
		{
			if (!this.IsEnclosedByValid())
			{
				this.EnclosedBy = null;
				base.Object.RemoveEffect(this, true);
				return false;
			}
			this.DisplayName = "{{C|enclosed in " + this.EnclosedBy.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + "}}";
			return true;
		}

		// Token: 0x0600853A RID: 34106 RVA: 0x00301664 File Offset: 0x002FF864
		private void ApplyChangesCore()
		{
			if (this.ChangesWereApplied)
			{
				this.UnapplyChangesCore();
			}
			if (this.EnclosedBy == null)
			{
				return;
			}
			Enclosing part = this.EnclosedBy.GetPart<Enclosing>();
			if (part == null)
			{
				return;
			}
			if (!part.AffectedProperties.IsNullOrEmpty())
			{
				Dictionary<string, int> propertyMap = part.PropertyMap;
				foreach (string text in propertyMap.Keys)
				{
					base.Object.ModIntProperty(text, propertyMap[text], true);
				}
			}
			this.ChangesWereApplied = true;
		}

		// Token: 0x0600853B RID: 34107 RVA: 0x00301708 File Offset: 0x002FF908
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
			Enclosing part = this.EnclosedBy.GetPart<Enclosing>();
			if (part.MustBeUnderstood && base.Object.IsPlayer() && !this.EnclosedBy.Understood())
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
				this.EnclosedBy.FireEvent(part.ApplyChangesEventSelf);
			}
			if (!part.ApplyChangesEventUser.IsNullOrEmpty())
			{
				base.Object.FireEvent(part.ApplyChangesEventUser);
			}
		}

		// Token: 0x0600853C RID: 34108 RVA: 0x003017C8 File Offset: 0x002FF9C8
		private void UnapplyChangesCore()
		{
			if (!this.ChangesWereApplied)
			{
				return;
			}
			if (this.EnclosedBy == null)
			{
				return;
			}
			Enclosing part = this.EnclosedBy.GetPart<Enclosing>();
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

		// Token: 0x0600853D RID: 34109 RVA: 0x00301868 File Offset: 0x002FFA68
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
			if (this.EnclosedBy == null)
			{
				return;
			}
			Enclosing part = this.EnclosedBy.GetPart<Enclosing>();
			if (part == null)
			{
				return;
			}
			this.UnapplyChangesCore();
			if (!part.UnapplyChangesEventSelf.IsNullOrEmpty())
			{
				this.EnclosedBy.FireEvent(part.UnapplyChangesEventSelf);
			}
			if (!part.UnapplyChangesEventUser.IsNullOrEmpty())
			{
				base.Object.FireEvent(part.UnapplyChangesEventUser);
			}
			this.ChangesWereApplied = false;
		}

		// Token: 0x0600853E RID: 34110 RVA: 0x003018EC File Offset: 0x002FFAEC
		private void ApplyStats()
		{
			if (this.EnclosedBy != null)
			{
				Enclosing part = this.EnclosedBy.GetPart<Enclosing>();
				base.StatShifter.SetStatShift(base.Object, "AV", part.AVBonus, false);
				base.StatShifter.SetStatShift(base.Object, "DV", -part.DVPenalty, false);
			}
		}

		// Token: 0x0600853F RID: 34111 RVA: 0x0030194A File Offset: 0x002FFB4A
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x06008540 RID: 34112 RVA: 0x00301960 File Offset: 0x002FFB60
		private void ApplyRenderLayer()
		{
			if (!this.EnclosedBy.IsValid())
			{
				return;
			}
			int num = base.Object.Render.RenderLayer - this.EnclosedBy.Render.RenderLayer + 1;
			if (num > 0)
			{
				this.AppliedRenderLayer += num;
				base.Object.Render.RenderLayer -= num;
			}
		}

		// Token: 0x06008541 RID: 34113 RVA: 0x003019C9 File Offset: 0x002FFBC9
		private void UnapplyRenderLayer()
		{
			if (this.AppliedRenderLayer <= 0)
			{
				return;
			}
			base.Object.Render.RenderLayer += this.AppliedRenderLayer;
			this.AppliedRenderLayer = 0;
		}

		// Token: 0x06008542 RID: 34114 RVA: 0x003019F9 File Offset: 0x002FFBF9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanChangeMovementModeEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID;
		}

		// Token: 0x06008543 RID: 34115 RVA: 0x00301A1F File Offset: 0x002FFC1F
		public override bool HandleEvent(CanChangeMovementModeEvent E)
		{
			return (E.Object != base.Object || !this.CheckEnclosedBy() || !this.EnclosedBy.GetPart<Enclosing>().EnclosureExitImpeded(base.Object, E.ShowMessage, this)) && base.HandleEvent(E);
		}

		// Token: 0x06008544 RID: 34116 RVA: 0x00301A60 File Offset: 0x002FFC60
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Reference && this.CheckEnclosedBy())
			{
				E.AddTag("[{{B|enclosed in " + this.EnclosedBy.an(int.MaxValue, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false) + "}}]", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008545 RID: 34117 RVA: 0x00301AC4 File Offset: 0x002FFCC4
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.CheckEnclosedBy())
			{
				Enclosing part = this.EnclosedBy.GetPart<Enclosing>();
				Enclosing enclosing = part;
				GameObject @object = base.Object;
				int num = this.TurnsEnclosed + 1;
				this.TurnsEnclosed = num;
				enclosing.ProcessTurnEnclosed(@object, num);
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

		// Token: 0x06008546 RID: 34118 RVA: 0x00301B74 File Offset: 0x002FFD74
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("BodyPositionChanged");
			Registrar.Register("CanChangeBodyPosition");
			Registrar.Register("CanMoveExtremities");
			Registrar.Register("LeaveCell");
			Registrar.Register("MovementModeChanged");
		}

		// Token: 0x06008547 RID: 34119 RVA: 0x00301BD0 File Offset: 0x002FFDD0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "LeaveCell" || E.ID == "MovementModeChanged" || E.ID == "BodyPositionChanged")
			{
				if (this.CheckEnclosedBy() && !this.EnclosedBy.GetPart<Enclosing>().ExitEnclosure(base.Object, E, this))
				{
					return false;
				}
			}
			else if (E.ID == "CanChangeBodyPosition" || E.ID == "CanMoveExtremities")
			{
				if (this.CheckEnclosedBy() && this.EnclosedBy.GetPart<Enclosing>().EnclosureExitImpeded(base.Object, E != null && E.HasFlag("ShowMessage"), this))
				{
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

		// Token: 0x040039B0 RID: 14768
		public GameObject EnclosedBy;

		// Token: 0x040039B1 RID: 14769
		public bool ChangesWereApplied;

		// Token: 0x040039B2 RID: 14770
		public int TurnsEnclosed;

		// Token: 0x040039B3 RID: 14771
		[FieldSaveVersion(397)]
		public int AppliedRenderLayer;
	}
}
