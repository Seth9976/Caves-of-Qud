using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02001092 RID: 4242
	[Serializable]
	public class RocketSkates : IActivePart
	{
		// Token: 0x0600A8C5 RID: 43205 RVA: 0x003DBE87 File Offset: 0x003DA087
		public RocketSkates()
		{
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A8C6 RID: 43206 RVA: 0x003DBE9D File Offset: 0x003DA09D
		public override bool SameAs(IPart p)
		{
			return (p as RocketSkates).PlumeLevel == this.PlumeLevel && base.SameAs(p);
		}

		// Token: 0x0600A8C7 RID: 43207 RVA: 0x003DBEBC File Offset: 0x003DA0BC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CellChangedEvent.ID || ID == EquippedEvent.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == ExamineCriticalFailureEvent.ID || ID == ExamineFailureEvent.ID || ID == PooledEvent<GetJumpingBehaviorEvent>.ID || ID == PooledEvent<GetRunningBehaviorEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<JumpedEvent>.ID || ID == PooledEvent<PartSupportEvent>.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A8C8 RID: 43208 RVA: 0x003DBF38 File Offset: 0x003DA138
		public override bool HandleEvent(PartSupportEvent E)
		{
			return (E.Skip == this || !(E.Type == "Run") || !this.IsObjectActivePartSubject(E.Actor) || !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && base.HandleEvent(E);
		}

		// Token: 0x0600A8C9 RID: 43209 RVA: 0x003DBFA0 File Offset: 0x003DA1A0
		public override bool HandleEvent(GetRunningBehaviorEvent E)
		{
			if (E.Priority < 20 && this.IsObjectActivePartSubject(E.Actor) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.AbilityName = "Power Skate";
				E.Verb = "power skate";
				E.EffectDisplayName = "{{K-y-Y-M-m-K-m-M-Y-y-K-y-Y sequence|power skating}}";
				E.EffectMessageName = "power skating";
				E.EffectDuration = 9999;
				E.SpringingEffective = true;
				E.Priority = 20;
				Templates.StatCollector stats = E.Stats;
				if (stats != null)
				{
					stats.Set("PowerSkate", "true", false, 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8CA RID: 43210 RVA: 0x003DC060 File Offset: 0x003DA260
		public override bool HandleEvent(GetJumpingBehaviorEvent E)
		{
			if (E.Priority < 20 && this.IsObjectActivePartSubject(E.Actor) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.AbilityName = "Rocket Jump";
				E.Verb = "rocket jump";
				E.ProviderKey = base.GetType().Name;
				E.RangeModifier = 4;
				E.MinimumRange = 3;
				E.CanJumpOverCreatures = true;
				E.Priority = 20;
				Templates.StatCollector stats = E.Stats;
				if (stats != null)
				{
					stats.Set("CanJumpOverCreatures", "true", false, 0);
				}
				Templates.StatCollector stats2 = E.Stats;
				if (stats2 != null)
				{
					stats2.Set("Rocket", "true", false, 0);
				}
				Templates.StatCollector stats3 = E.Stats;
				if (stats3 != null)
				{
					stats3.AddLinearBonusModifier("Range", 4, this.ParentObject.BaseDisplayName);
				}
				Templates.StatCollector stats4 = E.Stats;
				if (stats4 != null)
				{
					stats4.AddLinearBonusModifier("MinimumRange", 1, this.ParentObject.BaseDisplayName);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8CB RID: 43211 RVA: 0x003DC180 File Offset: 0x003DA380
		public override bool HandleEvent(JumpedEvent E)
		{
			if (E.Pass == 2 && E.ProviderKey == base.GetType().Name)
			{
				List<Point> path = E.Path;
				int num;
				if (path == null)
				{
					Cell originCell = E.OriginCell;
					num = ((originCell != null) ? originCell.DistanceTo(E.TargetCell) : 5);
				}
				else
				{
					num = path.Count;
				}
				base.ConsumeCharge(num * 2, null, null);
				if (E.Path != null)
				{
					bool flag = false;
					Zone parentZone = E.OriginCell.ParentZone;
					int i = 0;
					int num2 = E.Path.Count - 1;
					while (i < num2)
					{
						Cell cell = ((parentZone != null) ? parentZone.GetCell(E.Path[i].X, E.Path[i].Y) : null);
						if (cell != null && this.EmitFlamePlume(cell, null, E.Actor, false, false))
						{
							flag = true;
						}
						i++;
					}
					if (flag)
					{
						IComponent<GameObject>.EmitMessage(E.Actor, "A {{fiery|jet of flame}} shoots out of " + this.ParentObject.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, false, false, null, false, false, true, false, null, true, null, false, true) + "!", ' ', true, false, false, null, null);
					}
				}
				if (!E.Actor.MakeSave("Agility", 16, null, null, "RocketSkates RocketJump Knockdown", false, false, false, false, this.ParentObject) && E.Actor.ApplyEffect(new Prone(false, E.Actor.IsPlayer(), E.Actor.IsPlayer(), null), null))
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8CC RID: 43212 RVA: 0x003DC328 File Offset: 0x003DA528
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Replaces Sprint with Power Skate (unlimited duration).");
			E.Postfix.AppendRules("Emits plumes of fire when the wearer moves while power skating.", base.GetEventSensitiveAddStatusSummary(E));
			E.Postfix.AppendRules("Replaces Jump with Rocket Jump.");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8CD RID: 43213 RVA: 0x003DC376 File Offset: 0x003DA576
		public override bool HandleEvent(CellChangedEvent E)
		{
			this.SyncAbility(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8CE RID: 43214 RVA: 0x003DC386 File Offset: 0x003DA586
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.SyncAbility(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8CF RID: 43215 RVA: 0x003DC396 File Offset: 0x003DA596
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.SyncAbility(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8D0 RID: 43216 RVA: 0x003DC3A6 File Offset: 0x003DA5A6
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterPartEvent(this, "AfterMoved");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8D1 RID: 43217 RVA: 0x003DC3C0 File Offset: 0x003DA5C0
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterPartEvent(this, "AfterMoved");
			NeedPartSupportEvent.Send(E.Actor, "Run", this);
			Run.SyncAbility(E.Actor, false);
			Acrobatics_Jump.SyncAbility(E.Actor, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8D2 RID: 43218 RVA: 0x003DC40E File Offset: 0x003DA60E
		public override bool HandleEvent(ExamineFailureEvent E)
		{
			return !this.ExamineFailure(E, 50) && base.HandleEvent(E);
		}

		// Token: 0x0600A8D3 RID: 43219 RVA: 0x003DC424 File Offset: 0x003DA624
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return !this.ExamineFailure(E, 75) && base.HandleEvent(E);
		}

		// Token: 0x0600A8D4 RID: 43220 RVA: 0x003DC43A File Offset: 0x003DA63A
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A8D5 RID: 43221 RVA: 0x003DC440 File Offset: 0x003DA640
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.IsSkating() && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.ParentObject.Smoke();
			}
			this.SyncAbility(null);
		}

		// Token: 0x0600A8D6 RID: 43222 RVA: 0x003DC48F File Offset: 0x003DA68F
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A8D7 RID: 43223 RVA: 0x003DC494 File Offset: 0x003DA694
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AfterMoved")
			{
				Cell cell = E.GetParameter("FromCell") as Cell;
				Cell currentCell = this.ParentObject.GetCurrentCell();
				if (cell != null && cell.ParentZone == currentCell.ParentZone && this.PlumeLevel > 0 && !currentCell.OnWorldMap() && this.IsSkating() && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					string directionFromCell = currentCell.GetDirectionFromCell(cell, false);
					Cell cell2 = cell.GetCellFromDirection(directionFromCell, true);
					if (cell2 == null || cell2.ParentZone != currentCell.ParentZone)
					{
						cell2 = cell;
					}
					this.EmitFlamePlume(cell2, cell, null, false, false);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A8D8 RID: 43224 RVA: 0x003DC568 File Offset: 0x003DA768
		private bool EmitFlamePlume(Cell FlameCell, Cell FromCell = null, GameObject Actor = null, bool ShowMessage = false, bool UsePopups = false)
		{
			if (FlameCell == null)
			{
				return false;
			}
			if (ShowMessage)
			{
				base.DidX("emit", "a {{fiery|plume of flame}}", "!", null, null, null, null, false, false, null, null, false, false, false, false, UsePopups, null);
			}
			if (this._flamingRay == null)
			{
				this._flamingRay = new FlamingRay();
			}
			this._flamingRay.ParentObject = Actor ?? this.ParentObject.Equipped ?? this.ParentObject;
			this._flamingRay.Level = this.PlumeLevel;
			if (FlameCell != null)
			{
				FlameCell.ParticleBlip("&r^W" + ((char)(219 + Stat.Random(0, 4))).ToString(), 6, 0L, false);
			}
			if (FromCell != FlameCell && FromCell != null)
			{
				FromCell.ParticleBlip("&R^W" + ((char)(219 + Stat.Random(0, 4))).ToString(), 3, 0L, false);
			}
			this._flamingRay.Flame(FlameCell, null, false, UsePopups);
			return true;
		}

		// Token: 0x0600A8D9 RID: 43225 RVA: 0x003DC65C File Offset: 0x003DA85C
		public bool IsSkating()
		{
			GameObject equipped = this.ParentObject.Equipped;
			Running running = ((equipped != null) ? equipped.GetEffect<Running>() : null);
			return running != null && running.MessageName == "power skating";
		}

		// Token: 0x0600A8DA RID: 43226 RVA: 0x003DC696 File Offset: 0x003DA896
		public override bool IsActivePartEngaged()
		{
			return this.IsSkating() && base.IsActivePartEngaged();
		}

		// Token: 0x0600A8DB RID: 43227 RVA: 0x003DC6A8 File Offset: 0x003DA8A8
		private void SyncAbility(GameObject who = null)
		{
			if (who == null)
			{
				who = this.ParentObject.Equipped;
				if (who == null)
				{
					return;
				}
			}
			if (this.IsObjectActivePartSubject(who) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				who.RequirePart<Run>(false);
			}
			Run.SyncAbility(who, false);
			Acrobatics_Jump.SyncAbility(who, false);
		}

		// Token: 0x0600A8DC RID: 43228 RVA: 0x003DC710 File Offset: 0x003DA910
		private bool ExamineFailure(IExamineEvent E, int Chance)
		{
			if (E.Pass == 1 && GlobalConfig.GetBoolSetting("ContextualExamineFailures", false) && Chance.in100() && base.IsReady(true, false, false, false, false, false, false, false, true, false, 1, null, false, 0L, null) && this.EmitFlamePlume(E.Actor.CurrentCell, this.ParentObject.CurrentCell, this.ParentObject, true, E.Actor.IsPlayer()))
			{
				E.Identify = true;
				return true;
			}
			return false;
		}

		// Token: 0x040045C3 RID: 17859
		public int PlumeLevel = 3;

		// Token: 0x040045C4 RID: 17860
		private FlamingRay _flamingRay;
	}
}
