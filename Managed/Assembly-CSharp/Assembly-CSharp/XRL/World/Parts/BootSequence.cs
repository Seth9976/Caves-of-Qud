using System;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x02001000 RID: 4096
	[Serializable]
	public class BootSequence : IPoweredPart
	{
		// Token: 0x0600A338 RID: 41784 RVA: 0x003BA830 File Offset: 0x003B8A30
		public BootSequence()
		{
			this.IsBootSensitive = false;
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A339 RID: 41785 RVA: 0x003BA8C8 File Offset: 0x003B8AC8
		public override bool SameAs(IPart p)
		{
			BootSequence bootSequence = p as BootSequence;
			return bootSequence.BootTime == this.BootTime && !(bootSequence.VariableBootTime != this.VariableBootTime) && bootSequence.ReadoutInName == this.ReadoutInName && bootSequence.ReadoutInDescription == this.ReadoutInDescription && bootSequence.AlwaysObvious == this.AlwaysObvious && bootSequence.ObviousIfUnderstood == this.ObviousIfUnderstood && !(bootSequence.TextInDescription != this.TextInDescription) && !(bootSequence.VerbOnBootInitialized != this.VerbOnBootInitialized) && !(bootSequence.VerbOnBootDone != this.VerbOnBootDone) && !(bootSequence.VerbOnBootAborted != this.VerbOnBootAborted) && !(bootSequence.SoundOnBootInitialized != this.SoundOnBootInitialized) && !(bootSequence.SoundOnBootDone != this.SoundOnBootDone) && !(bootSequence.SoundOnBootAborted != this.SoundOnBootAborted) && bootSequence.ComputePowerFactor == this.ComputePowerFactor && bootSequence.Sensitive == this.Sensitive && base.SameAs(p);
		}

		// Token: 0x0600A33A RID: 41786 RVA: 0x003BA9FB File Offset: 0x003B8BFB
		public bool IsObvious()
		{
			return this.AlwaysObvious || ((this.ObviousIfUnderstood || this.ReadoutInName || this.ReadoutInDescription) && this.ParentObject.Understood());
		}

		// Token: 0x0600A33B RID: 41787 RVA: 0x003BAA30 File Offset: 0x003B8C30
		private void BootUI(GameObject Context, string Verb, string Sound)
		{
			if (!string.IsNullOrEmpty(Verb))
			{
				base.DidX(Verb, null, null, null, null, null, null, false, false, null, null, false, false, false, false, false, Context);
			}
			if (!string.IsNullOrEmpty(Sound))
			{
				base.PlayWorldSound(Sound, 0.5f, 0f, false, (Context != null) ? Context.GetCurrentCell() : null, 0f, 1f, 1f, int.MaxValue);
			}
		}

		// Token: 0x0600A33C RID: 41788 RVA: 0x003BAA96 File Offset: 0x003B8C96
		private void ResetBootTime()
		{
			this.BootTimeLeft = GetAvailableComputePowerEvent.AdjustDown(this, (this.VariableBootTime != null) ? Math.Max(this.VariableBootTime.RollCached(), this.BootTime) : this.BootTime, this.ComputePowerFactor, 2);
		}

		// Token: 0x0600A33D RID: 41789 RVA: 0x003BAAD4 File Offset: 0x003B8CD4
		private void InitBoot(GameObject Context = null)
		{
			this.ResetBootTime();
			BootSequenceInitializedEvent.Send(this.ParentObject);
			SyncRenderEvent.Send(this.ParentObject);
			this.BootUI(Context, this.VerbOnBootInitialized, this.SoundOnBootInitialized);
			base.ConsumeCharge(null, null);
		}

		// Token: 0x0600A33E RID: 41790 RVA: 0x003BAB29 File Offset: 0x003B8D29
		private void AbortBoot(GameObject Context = null)
		{
			BootSequenceAbortedEvent.Send(this.ParentObject);
			SyncRenderEvent.Send(this.ParentObject);
			this.BootUI(Context, this.VerbOnBootAborted, this.SoundOnBootAborted);
		}

		// Token: 0x0600A33F RID: 41791 RVA: 0x003BAB54 File Offset: 0x003B8D54
		private void SyncBoot(GameObject Context = null)
		{
			if (this.PartWasReady && this.Sensitive)
			{
				this.AbortBoot(Context);
			}
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (this.PartWasReady)
				{
					if (!this.Sensitive)
					{
						this.AbortBoot(Context);
					}
					this.PartWasReady = false;
					return;
				}
			}
			else
			{
				if (!this.PartWasReady || this.Sensitive)
				{
					this.InitBoot(Context);
				}
				this.PartWasReady = true;
			}
		}

		// Token: 0x0600A340 RID: 41792 RVA: 0x003BABE0 File Offset: 0x003B8DE0
		public void Reboot()
		{
			if (this.PartWasReady)
			{
				this.AbortBoot(null);
			}
			this.PartWasReady = base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
			if (this.PartWasReady)
			{
				this.InitBoot(null);
			}
		}

		// Token: 0x0600A341 RID: 41793 RVA: 0x003BAC38 File Offset: 0x003B8E38
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CellChangedEvent.ID || ID == EffectAppliedEvent.ID || ID == EnteredCellEvent.ID || ID == EquippedEvent.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == ImplantedEvent.ID || ID == LeftCellEvent.ID || ID == PooledEvent<QueryDrawEvent>.ID || ID == TakenEvent.ID || ID == UnequippedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x0600A342 RID: 41794 RVA: 0x003BACB9 File Offset: 0x003B8EB9
		public override bool HandleEvent(QueryDrawEvent E)
		{
			return this.BootTimeLeft <= 0 || !base.WasReadyIfKnown() || base.HandleEvent(E);
		}

		// Token: 0x0600A343 RID: 41795 RVA: 0x003BACD8 File Offset: 0x003B8ED8
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "BootTime", this.BootTime);
			E.AddEntry(this, "VariableBootTime", this.VariableBootTime);
			E.AddEntry(this, "BootTimeLeft", this.BootTimeLeft);
			E.AddEntry(this, "Sensitive", this.Sensitive);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A344 RID: 41796 RVA: 0x003BAD34 File Offset: 0x003B8F34
		public override bool HandleEvent(CellChangedEvent E)
		{
			this.Reboot();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A345 RID: 41797 RVA: 0x003BAD44 File Offset: 0x003B8F44
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (this.PartWasReady && !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, new int?((this.BootTimeLeft > 0) ? this.ChargeUse : 0), false, 0L, null))
			{
				this.AbortBoot(null);
				this.PartWasReady = false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A346 RID: 41798 RVA: 0x003BADA4 File Offset: 0x003B8FA4
		public override bool HandleEvent(EquippedEvent E)
		{
			this.SyncBoot(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A347 RID: 41799 RVA: 0x003BADB9 File Offset: 0x003B8FB9
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.SyncBoot(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A348 RID: 41800 RVA: 0x003BADCE File Offset: 0x003B8FCE
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.SyncBoot(E.Implantee);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A349 RID: 41801 RVA: 0x003BADE3 File Offset: 0x003B8FE3
		public override bool HandleEvent(UnimplantedEvent E)
		{
			this.SyncBoot(E.Implantee);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A34A RID: 41802 RVA: 0x003BADF8 File Offset: 0x003B8FF8
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.SyncBoot(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A34B RID: 41803 RVA: 0x003BAE08 File Offset: 0x003B9008
		public override bool HandleEvent(LeftCellEvent E)
		{
			this.SyncBoot(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A34C RID: 41804 RVA: 0x003BAE18 File Offset: 0x003B9018
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (this.BootTimeLeft > 0 && this.ReadoutInName && this.PartWasReady && E.Understood())
			{
				E.AddTag("[{{K|" + this.BootTimeLeft.ToString() + " sec}}]", 40);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A34D RID: 41805 RVA: 0x003BAE70 File Offset: 0x003B9070
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.BootTimeLeft > 0 && this.PartWasReady)
			{
				if (!string.IsNullOrEmpty(this.TextInDescription))
				{
					E.Postfix.Append('\n').Append(this.TextInDescription);
				}
				if (this.ReadoutInDescription)
				{
					E.Postfix.Append('\n').Append(this.ParentObject.Its).Append(" readout indicates that ")
						.Append(this.ParentObject.its)
						.Append(" startup sequence will take an estimated ")
						.Append(Grammar.Cardinal(this.BootTimeLeft))
						.Append(" more ")
						.Append((this.BootTimeLeft == 1) ? "round" : "rounds")
						.Append(".");
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A34E RID: 41806 RVA: 0x003BAF4C File Offset: 0x003B914C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("GameStart");
			Registrar.Register("LiquidFueledPowerPlantFueled");
			Registrar.Register("ObjectEntered");
			Registrar.Register("ObjectExited");
			Registrar.Register("Reboot");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A34F RID: 41807 RVA: 0x003BAF98 File Offset: 0x003B9198
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Reboot")
			{
				this.Reboot();
			}
			else if (E.ID == "GameStart")
			{
				if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, new int?(0), false, 0L, null))
				{
					this.BootTimeLeft = 0;
					BootSequenceDoneEvent.Send(this.ParentObject);
					this.PartWasReady = true;
				}
				else
				{
					this.PartWasReady = false;
				}
			}
			else if (E.ID == "ObjectEntered" || E.ID == "ObjectExited")
			{
				this.SyncBoot(null);
			}
			else if (E.ID == "LiquidFueledPowerPlantFueled" && !this.PartWasReady && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.InitBoot(null);
				this.PartWasReady = true;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A350 RID: 41808 RVA: 0x003BB0A4 File Offset: 0x003B92A4
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A351 RID: 41809 RVA: 0x003BB0A8 File Offset: 0x003B92A8
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (!this.PartWasReady)
				{
					this.InitBoot(null);
				}
				else if (this.BootTimeLeft > 0)
				{
					this.BootTimeLeft = Math.Max(this.BootTimeLeft - Amount, 0);
					if (this.BootTimeLeft <= 0)
					{
						BootSequenceDoneEvent.Send(this.ParentObject);
						this.BootUI(null, this.VerbOnBootDone, this.SoundOnBootDone);
					}
					base.ConsumeCharge(null, null);
				}
				this.PartWasReady = true;
				return;
			}
			if (this.PartWasReady && !base.IsWorldMapActive)
			{
				this.AbortBoot(null);
				this.PartWasReady = false;
			}
		}

		// Token: 0x0600A352 RID: 41810 RVA: 0x003BB171 File Offset: 0x003B9371
		public void ForceBoot()
		{
			this.BootTimeLeft = 0;
			this.PartWasReady = true;
		}

		// Token: 0x0400439C RID: 17308
		public int BootTime = 100;

		// Token: 0x0400439D RID: 17309
		public string VariableBootTime;

		// Token: 0x0400439E RID: 17310
		public int BootTimeLeft;

		// Token: 0x0400439F RID: 17311
		public bool ReadoutInName;

		// Token: 0x040043A0 RID: 17312
		public bool ReadoutInDescription;

		// Token: 0x040043A1 RID: 17313
		public bool AlwaysObvious;

		// Token: 0x040043A2 RID: 17314
		public bool ObviousIfUnderstood;

		// Token: 0x040043A3 RID: 17315
		public string TextInDescription;

		// Token: 0x040043A4 RID: 17316
		public string VerbOnBootInitialized = "beep";

		// Token: 0x040043A5 RID: 17317
		public string VerbOnBootDone = "click";

		// Token: 0x040043A6 RID: 17318
		public string VerbOnBootAborted = "bloop";

		// Token: 0x040043A7 RID: 17319
		public string SoundOnBootInitialized = "Sounds/Interact/sfx_interact_artifact_windup";

		// Token: 0x040043A8 RID: 17320
		public string SoundOnBootDone = "Sounds/Interact/sfx_interact_artifact_ready";

		// Token: 0x040043A9 RID: 17321
		public string SoundOnBootAborted = "Sounds/Interact/sfx_interact_artifact_abort_bloop";

		// Token: 0x040043AA RID: 17322
		public float SoundVolumeOnBootInitialized = 0.6f;

		// Token: 0x040043AB RID: 17323
		public float SoundVolumeOnBootDone = 0.5f;

		// Token: 0x040043AC RID: 17324
		public float SoundVolumeOnBootAborted = 1f;

		// Token: 0x040043AD RID: 17325
		public float ComputePowerFactor = 2.5f;

		// Token: 0x040043AE RID: 17326
		public bool Sensitive;

		// Token: 0x040043AF RID: 17327
		public bool PartWasReady;
	}
}
