using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000EB9 RID: 3769
	[Serializable]
	public class FugueOnStep : IActivePart
	{
		// Token: 0x06009726 RID: 38694 RVA: 0x0036D6F0 File Offset: 0x0036B8F0
		public FugueOnStep()
		{
			this.WorksOnCellContents = true;
		}

		// Token: 0x06009727 RID: 38695 RVA: 0x0036D78C File Offset: 0x0036B98C
		public override bool SameAs(IPart p)
		{
			FugueOnStep fugueOnStep = p as FugueOnStep;
			return fugueOnStep.Chance == this.Chance && !(fugueOnStep.SaveStat != this.SaveStat) && !(fugueOnStep.SaveDifficultyStat != this.SaveDifficultyStat) && fugueOnStep.SaveTarget == this.SaveTarget && !(fugueOnStep.SaveVs != this.SaveVs) && fugueOnStep.TriggerOnSaveSuccess == this.TriggerOnSaveSuccess && !(fugueOnStep.Duration != this.Duration) && !(fugueOnStep.Copies != this.Copies) && !(fugueOnStep.Cooldown != this.Cooldown) && fugueOnStep.HostileCopyChance == this.HostileCopyChance && !(fugueOnStep.HostileCopyColorString != this.HostileCopyColorString) && !(fugueOnStep.HostileCopyPrefix != this.HostileCopyPrefix) && !(fugueOnStep.FriendlyCopyColorString != this.FriendlyCopyColorString) && !(fugueOnStep.FriendlyCopyPrefix != this.FriendlyCopyPrefix) && !(fugueOnStep.ReadyColorString != this.ReadyColorString) && !(fugueOnStep.ReadyTileColor != this.ReadyTileColor) && !(fugueOnStep.ReadyDetailColor != this.ReadyDetailColor) && !(fugueOnStep.CooldownColorString != this.CooldownColorString) && !(fugueOnStep.CooldownTileColor != this.CooldownTileColor) && !(fugueOnStep.CooldownDetailColor != this.CooldownDetailColor) && fugueOnStep.CooldownLeft == this.CooldownLeft && fugueOnStep.TreatAsPsychic == this.TreatAsPsychic && base.SameAs(p);
		}

		// Token: 0x06009728 RID: 38696 RVA: 0x0036D957 File Offset: 0x0036BB57
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06009729 RID: 38697 RVA: 0x0036D95A File Offset: 0x0036BB5A
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.CooldownLeft > 0)
			{
				this.CooldownLeft--;
				if (this.CooldownLeft <= 0)
				{
					this.SyncColor();
				}
			}
			this.CheckActivate();
		}

		// Token: 0x0600972A RID: 38698 RVA: 0x0036D988 File Offset: 0x0036BB88
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<InterruptAutowalkEvent>.ID || (ID == PooledEvent<IsSensableAsPsychicEvent>.ID && this.TreatAsPsychic) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600972B RID: 38699 RVA: 0x0036D9B6 File Offset: 0x0036BBB6
		public override bool HandleEvent(InterruptAutowalkEvent E)
		{
			E.IndicateObject = this.ParentObject;
			return false;
		}

		// Token: 0x0600972C RID: 38700 RVA: 0x0036D9C5 File Offset: 0x0036BBC5
		public override bool HandleEvent(IsSensableAsPsychicEvent E)
		{
			if (this.TreatAsPsychic)
			{
				E.Sensable = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600972D RID: 38701 RVA: 0x0036D9DD File Offset: 0x0036BBDD
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.SyncColor();
			return base.HandleEvent(E);
		}

		// Token: 0x0600972E RID: 38702 RVA: 0x0036D9EC File Offset: 0x0036BBEC
		public void CheckActivate()
		{
			if (this.CooldownLeft <= 0 && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && this.Chance.in100())
			{
				List<GameObject> activePartSubjects = this.GetActivePartSubjects();
				bool flag = false;
				for (int i = 0; i < activePartSubjects.Count; i++)
				{
					GameObject gameObject = activePartSubjects[i];
					if (this.ValidStepTarget(gameObject) && (string.IsNullOrEmpty(this.SaveStat) || gameObject.MakeSave(this.SaveStat, this.SaveTarget, this.ParentObject, this.SaveDifficultyStat, null, false, false, false, false, null) == this.TriggerOnSaveSuccess) && this.Activate(gameObject))
					{
						flag = true;
					}
				}
				if (flag && !string.IsNullOrEmpty(this.Cooldown))
				{
					base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, 1, null, false, 0, false, null);
					this.CooldownLeft = Stat.Roll(this.Cooldown, null);
					this.SyncColor();
				}
			}
		}

		// Token: 0x0600972F RID: 38703 RVA: 0x0036DB04 File Offset: 0x0036BD04
		public bool Activate(GameObject Subject)
		{
			GameObject parentObject = this.ParentObject;
			GameObject gameObject = null;
			TemporalFugue temporalFugue = null;
			IEvent @event = null;
			bool flag = true;
			bool flag2 = true;
			int? num = new int?(Stat.Roll(this.Duration, null));
			int? num2 = new int?(Stat.Roll(this.Copies, null));
			int hostileCopyChance = this.HostileCopyChance;
			string text = "Temporal Fugue";
			string hostileCopyColorString = this.HostileCopyColorString;
			string hostileCopyPrefix = this.HostileCopyPrefix;
			if (!TemporalFugue.PerformTemporalFugue(parentObject, Subject, gameObject, temporalFugue, @event, flag, flag2, num, num2, hostileCopyChance, text, this.FriendlyCopyColorString, hostileCopyColorString, this.FriendlyCopyPrefix, hostileCopyPrefix))
			{
				return false;
			}
			if (base.Visible())
			{
				if (Subject.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("You step on " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " and vibrate through spacetime.", null, true);
				}
				else
				{
					IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
					{
						Subject.Does("step", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
						" on ",
						this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						" and",
						Subject.GetVerb("vibrate", true, false, false, true, false),
						" through spacetime."
					}), null, true);
				}
			}
			return true;
		}

		// Token: 0x06009730 RID: 38704 RVA: 0x0036DC64 File Offset: 0x0036BE64
		public void SyncColor()
		{
			if (this.CooldownLeft > 0)
			{
				if (!string.IsNullOrEmpty(this.CooldownColorString))
				{
					this.ParentObject.Render.ColorString = this.CooldownColorString;
				}
				if (!string.IsNullOrEmpty(this.CooldownTileColor))
				{
					this.ParentObject.Render.TileColor = this.CooldownTileColor;
				}
				if (!string.IsNullOrEmpty(this.CooldownDetailColor))
				{
					this.ParentObject.Render.DetailColor = this.CooldownDetailColor;
					return;
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(this.ReadyColorString))
				{
					this.ParentObject.Render.ColorString = this.ReadyColorString;
				}
				if (!string.IsNullOrEmpty(this.ReadyTileColor))
				{
					this.ParentObject.Render.TileColor = this.ReadyTileColor;
				}
				if (!string.IsNullOrEmpty(this.ReadyDetailColor))
				{
					this.ParentObject.Render.DetailColor = this.ReadyDetailColor;
				}
			}
		}

		// Token: 0x06009731 RID: 38705 RVA: 0x0036DD50 File Offset: 0x0036BF50
		public bool ValidStepTarget(GameObject obj)
		{
			return obj.IsCombatObject() && obj.FlightMatches(this.ParentObject) && !obj.HasPart(typeof(FugueOnStep));
		}

		// Token: 0x04003ECE RID: 16078
		public int Chance = 100;

		// Token: 0x04003ECF RID: 16079
		public string SaveStat;

		// Token: 0x04003ED0 RID: 16080
		public string SaveDifficultyStat;

		// Token: 0x04003ED1 RID: 16081
		public int SaveTarget = 15;

		// Token: 0x04003ED2 RID: 16082
		public string SaveVs;

		// Token: 0x04003ED3 RID: 16083
		public bool TriggerOnSaveSuccess = true;

		// Token: 0x04003ED4 RID: 16084
		public string Duration = "2d6+20";

		// Token: 0x04003ED5 RID: 16085
		public string Copies = "1d4+1";

		// Token: 0x04003ED6 RID: 16086
		public string Cooldown = "100-200";

		// Token: 0x04003ED7 RID: 16087
		public int HostileCopyChance;

		// Token: 0x04003ED8 RID: 16088
		public string HostileCopyColorString;

		// Token: 0x04003ED9 RID: 16089
		public string HostileCopyPrefix;

		// Token: 0x04003EDA RID: 16090
		public string FriendlyCopyColorString;

		// Token: 0x04003EDB RID: 16091
		public string FriendlyCopyPrefix;

		// Token: 0x04003EDC RID: 16092
		public new string ReadyColorString = "&G";

		// Token: 0x04003EDD RID: 16093
		public string ReadyTileColor = "&G";

		// Token: 0x04003EDE RID: 16094
		public new string ReadyDetailColor = "M";

		// Token: 0x04003EDF RID: 16095
		public string CooldownColorString = "&g";

		// Token: 0x04003EE0 RID: 16096
		public string CooldownTileColor = "&g";

		// Token: 0x04003EE1 RID: 16097
		public string CooldownDetailColor = "m";

		// Token: 0x04003EE2 RID: 16098
		public bool TreatAsPsychic = true;

		// Token: 0x04003EE3 RID: 16099
		public int CooldownLeft;
	}
}
