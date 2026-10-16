using System;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02000FB3 RID: 4019
	[Serializable]
	public class RegenTank : IPoweredPart
	{
		// Token: 0x06009FC2 RID: 40898 RVA: 0x003A0FCC File Offset: 0x0039F1CC
		public RegenTank()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x06009FC3 RID: 40899 RVA: 0x003A109C File Offset: 0x0039F29C
		public override void Attach()
		{
			base.Attach();
			if (this.LimbRegenerationQuery == "AnyRegenerableLimbs" && this.LimbRegenerationNotify == "RegenerateAllLimbs")
			{
				this.LimbRegenerationQuery = null;
				this.LimbRegenerationNotify = null;
			}
		}

		// Token: 0x06009FC4 RID: 40900 RVA: 0x003A10D8 File Offset: 0x0039F2D8
		public override bool SameAs(IPart p)
		{
			RegenTank regenTank = p as RegenTank;
			return !(regenTank.RulesDescription != this.RulesDescription) && !(regenTank.RejuvenationStatusLine != this.RejuvenationStatusLine) && !(regenTank.LimbRegenerationStatusLine != this.LimbRegenerationStatusLine) && !(regenTank.RejuvenationTriggerNotify != this.RejuvenationTriggerNotify) && !(regenTank.LimbRegenerationTriggerNotify != this.LimbRegenerationTriggerNotify) && !(regenTank.BaseLiquid != this.BaseLiquid) && !(regenTank.RegenLiquid != this.RegenLiquid) && !(regenTank.RejuvenationHealing != this.RejuvenationHealing) && !(regenTank.RejuvenationEvent != this.RejuvenationEvent) && !(regenTank.RejuvenationQuery != this.RejuvenationQuery) && !(regenTank.RejuvenationNotify != this.RejuvenationNotify) && !(regenTank.LimbRegenerationHealing != this.LimbRegenerationHealing) && !(regenTank.LimbRegenerationEvent != this.LimbRegenerationEvent) && !(regenTank.LimbRegenerationQuery != this.LimbRegenerationQuery) && !(regenTank.LimbRegenerationNotify != this.LimbRegenerationNotify) && regenTank.MinTotalDrams == this.MinTotalDrams && regenTank.BaseLiquidPermillageNeeded == this.BaseLiquidPermillageNeeded && regenTank.RegenLiquidMilliDramsNeeded == this.RegenLiquidMilliDramsNeeded && regenTank.RegenLiquidDramsUsed == this.RegenLiquidDramsUsed && regenTank.RejuvenationRegeneraLevel == this.RejuvenationRegeneraLevel && regenTank.LimbRegenerationRegeneraLevel == this.LimbRegenerationRegeneraLevel && regenTank.RequiresAlive == this.RequiresAlive && regenTank.LimbRegenerationMinEvents == this.LimbRegenerationMinEvents && base.SameAs(p);
		}

		// Token: 0x06009FC5 RID: 40901 RVA: 0x003A12AE File Offset: 0x0039F4AE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GenericNotifyEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x06009FC6 RID: 40902 RVA: 0x003A12CC File Offset: 0x0039F4CC
		public override bool HandleEvent(GenericNotifyEvent E)
		{
			if (E.Notify == this.RejuvenationTriggerNotify)
			{
				if (E.Subject != null && this.GetRejuvenationStatus(E.Subject) == 1 && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && (this.RejuvenationQuery.IsNullOrEmpty() || GenericQueryEvent.Check(E.Subject, this.RejuvenationQuery, null, this.ParentObject, this.RejuvenationRegeneraLevel, false)))
				{
					if (!this.RejuvenationHealing.IsNullOrEmpty())
					{
						int num = this.RejuvenationHealing.RollCached();
						if (num > 0)
						{
							E.Subject.Heal(num, false, true, true);
						}
					}
					if (!this.RejuvenationEvent.IsNullOrEmpty())
					{
						E.Subject.FireEvent(this.RejuvenationEvent);
					}
					if (this.RejuvenationRegeneraLevel != 0)
					{
						E.Subject.FireEvent(Event.New("Regenera", "Source", this.ParentObject, "Level", this.RejuvenationRegeneraLevel));
					}
					if (!this.RejuvenationNotify.IsNullOrEmpty())
					{
						GenericNotifyEvent.Send(E.Subject, this.RejuvenationNotify, null, this.ParentObject, this.RejuvenationRegeneraLevel);
					}
				}
			}
			else if (E.Notify == this.LimbRegenerationTriggerNotify && E.Subject != null && this.GetLimbRegenerationStatus(E.Subject) == 1 && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && (this.LimbRegenerationQuery.IsNullOrEmpty() || GenericQueryEvent.Check(E.Subject, this.LimbRegenerationQuery, null, this.ParentObject, this.LimbRegenerationRegeneraLevel, false)))
			{
				if (!this.LimbRegenerationHealing.IsNullOrEmpty())
				{
					int num2 = this.LimbRegenerationHealing.RollCached();
					if (num2 > 0)
					{
						E.Subject.Heal(num2, false, true, true);
					}
				}
				if (this.LimbRegenerationMinEvents)
				{
					int? num3;
					int[] array;
					BodyPartCategory.ProcessList(this.LimbRegenerationMinEventsCategories, out num3, out array, true);
					int? num4;
					int[] array2;
					BodyPartCategory.ProcessList(this.LimbRegenerationMinEventsExceptCategories, out num4, out array2, true);
					GameObject subject = E.Subject;
					GameObject gameObject = null;
					GameObject parentObject = this.ParentObject;
					bool limbRegenerationMinEventsWhole = this.LimbRegenerationMinEventsWhole;
					bool limbRegenerationMinEventsWhole2 = this.LimbRegenerationMinEventsWhole;
					bool flag = limbRegenerationMinEventsWhole;
					bool flag2 = true;
					bool flag3 = true;
					int? num5 = num3;
					int[] array3 = array;
					int? num6 = num4;
					int[] array4 = array2;
					RegenerateLimbEvent.Send(subject, gameObject, parentObject, limbRegenerationMinEventsWhole2, flag, flag2, flag3, null, num5, array3, num6, array4);
				}
				if (!this.LimbRegenerationEvent.IsNullOrEmpty())
				{
					E.Subject.FireEvent(this.LimbRegenerationEvent);
				}
				if (this.LimbRegenerationRegeneraLevel != 0)
				{
					E.Subject.FireEvent(Event.New("Regenera", "Source", this.ParentObject, "Level", this.LimbRegenerationRegeneraLevel));
				}
				if (!this.LimbRegenerationNotify.IsNullOrEmpty())
				{
					GenericNotifyEvent.Send(E.Subject, this.LimbRegenerationNotify, null, this.ParentObject, this.LimbRegenerationRegeneraLevel);
				}
				if (this.RegenLiquidDramsUsed > 0)
				{
					LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
					if (liquidVolume != null)
					{
						liquidVolume.UseDrams(this.RegenLiquid, this.RegenLiquidDramsUsed);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009FC7 RID: 40903 RVA: 0x003A15F8 File Offset: 0x0039F7F8
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.RejuvenationStatusLine.IsNullOrEmpty() || !this.LimbRegenerationStatusLine.IsNullOrEmpty())
			{
				E.Base.Append('\n');
				if (!this.RejuvenationStatusLine.IsNullOrEmpty())
				{
					E.Base.Append('\n').Append(this.RejuvenationStatusLine).Append(this.GetRejuvenationStatusString());
				}
				if (!this.LimbRegenerationStatusLine.IsNullOrEmpty())
				{
					E.Base.Append('\n').Append(this.LimbRegenerationStatusLine).Append(this.GetLimbRegenerationStatusString());
				}
			}
			if (!this.RulesDescription.IsNullOrEmpty())
			{
				E.Postfix.AppendRules(this.RulesDescription);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009FC8 RID: 40904 RVA: 0x003A16B5 File Offset: 0x0039F8B5
		public override bool GetActivePartLocallyDefinedFailure()
		{
			return this.GetRejuvenationStatus(null) != 1;
		}

		// Token: 0x06009FC9 RID: 40905 RVA: 0x003A16C4 File Offset: 0x0039F8C4
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			switch (this.GetRejuvenationStatus(null))
			{
			case 2:
				return "TankNotFull";
			case 3:
				return "ImproperMixture";
			case 5:
				return "ImproperSubject";
			}
			return null;
		}

		// Token: 0x06009FCA RID: 40906 RVA: 0x003A1708 File Offset: 0x0039F908
		public int GetRejuvenationStatus(GameObject who = null)
		{
			if (this.RequiresAlive && who != null && !who.IsAlive)
			{
				return 5;
			}
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			if (liquidVolume == null || liquidVolume.Volume < this.MinTotalDrams)
			{
				return 2;
			}
			if (liquidVolume.Proportion(this.BaseLiquid) < this.BaseLiquidPermillageNeeded)
			{
				return 3;
			}
			return 1;
		}

		// Token: 0x06009FCB RID: 40907 RVA: 0x003A1760 File Offset: 0x0039F960
		public int GetLimbRegenerationStatus(GameObject who = null)
		{
			if (this.RequiresAlive && who != null && !who.IsAlive)
			{
				return 5;
			}
			LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
			if (liquidVolume == null || liquidVolume.Volume < this.MinTotalDrams)
			{
				return 2;
			}
			if (liquidVolume.Proportion(this.BaseLiquid) < this.BaseLiquidPermillageNeeded)
			{
				return 3;
			}
			liquidVolume.MilliAmount(this.RegenLiquid);
			if (liquidVolume.MilliAmount(this.RegenLiquid) < this.RegenLiquidMilliDramsNeeded)
			{
				return 4;
			}
			return 1;
		}

		// Token: 0x06009FCC RID: 40908 RVA: 0x003A17DC File Offset: 0x0039F9DC
		public string GetRejuvenationStatusString()
		{
			int rejuvenationStatus = this.GetRejuvenationStatus(null);
			if (rejuvenationStatus == 1)
			{
				string statusSummary = base.GetStatusSummary();
				if (statusSummary != null)
				{
					return statusSummary;
				}
			}
			return this.GetStatusString(rejuvenationStatus);
		}

		// Token: 0x06009FCD RID: 40909 RVA: 0x003A1808 File Offset: 0x0039FA08
		public string GetLimbRegenerationStatusString()
		{
			int limbRegenerationStatus = this.GetLimbRegenerationStatus(null);
			if (limbRegenerationStatus == 1)
			{
				string statusSummary = base.GetStatusSummary();
				if (statusSummary != null)
				{
					return statusSummary;
				}
			}
			return this.GetStatusString(limbRegenerationStatus);
		}

		// Token: 0x06009FCE RID: 40910 RVA: 0x003A1834 File Offset: 0x0039FA34
		public string GetStatusString(int status)
		{
			switch (status)
			{
			case 1:
				return "{{G|ready}}";
			case 2:
				return "{{K|tank not full}}";
			case 3:
				return "{{R|improper mixture}}";
			case 4:
				return "{{K|insufficient " + LiquidVolume.GetLiquid(this.RegenLiquid).GetName().Strip() + "}}";
			case 5:
				return "{{R|improper subject}}";
			default:
				return "{{K|unknown failure}}";
			}
		}

		// Token: 0x04004250 RID: 16976
		public const int STATUS_READY = 1;

		// Token: 0x04004251 RID: 16977
		public const int STATUS_TANK_NOT_FULL = 2;

		// Token: 0x04004252 RID: 16978
		public const int STATUS_WRONG_MIXTURE = 3;

		// Token: 0x04004253 RID: 16979
		public const int STATUS_MISSING_REGEN_LIQUID = 4;

		// Token: 0x04004254 RID: 16980
		public const int STATUS_WRONG_SUBJECT = 5;

		// Token: 0x04004255 RID: 16981
		public string RulesDescription = "Rejuvenates health and regenerates lost body parts.\nMust be filled with at least 100 drams of a liquid mixture that's at least 2/3rds convalessence to be fully functional.\nIf 1+ dram of cloning draught is present in the mixture, all lost body parts are regenerated over time and 1 dram of cloning draught is consumed. Excess cloning draught is not consumed.\nMust be entered and waited inside for regeneration to take effect.";

		// Token: 0x04004256 RID: 16982
		public string RejuvenationStatusLine = "{{C|Rejuvenation status: }}";

		// Token: 0x04004257 RID: 16983
		public string LimbRegenerationStatusLine = "{{C|Limb regeneration status: }}";

		// Token: 0x04004258 RID: 16984
		public string RejuvenationTriggerNotify = "RegenTankRejuvenation";

		// Token: 0x04004259 RID: 16985
		public string LimbRegenerationTriggerNotify = "RegenTankLimbRegeneration";

		// Token: 0x0400425A RID: 16986
		public string BaseLiquid = "convalessence";

		// Token: 0x0400425B RID: 16987
		public string RegenLiquid = "cloning";

		// Token: 0x0400425C RID: 16988
		public string RejuvenationHealing = "1d3";

		// Token: 0x0400425D RID: 16989
		public string RejuvenationEvent = "Recuperating";

		// Token: 0x0400425E RID: 16990
		public string RejuvenationQuery;

		// Token: 0x0400425F RID: 16991
		public string RejuvenationNotify;

		// Token: 0x04004260 RID: 16992
		public string LimbRegenerationHealing = "5d8";

		// Token: 0x04004261 RID: 16993
		public string LimbRegenerationEvent;

		// Token: 0x04004262 RID: 16994
		public string LimbRegenerationQuery;

		// Token: 0x04004263 RID: 16995
		public string LimbRegenerationNotify;

		// Token: 0x04004264 RID: 16996
		public int MinTotalDrams = 100;

		// Token: 0x04004265 RID: 16997
		public int BaseLiquidPermillageNeeded = 666;

		// Token: 0x04004266 RID: 16998
		public int RegenLiquidMilliDramsNeeded = 850;

		// Token: 0x04004267 RID: 16999
		public int RegenLiquidDramsUsed = 1;

		// Token: 0x04004268 RID: 17000
		public int RejuvenationRegeneraLevel = 4;

		// Token: 0x04004269 RID: 17001
		public int LimbRegenerationRegeneraLevel = 4;

		// Token: 0x0400426A RID: 17002
		public bool RequiresAlive = true;

		// Token: 0x0400426B RID: 17003
		public bool LimbRegenerationMinEvents = true;

		// Token: 0x0400426C RID: 17004
		public bool LimbRegenerationMinEventsAll = true;

		// Token: 0x0400426D RID: 17005
		public bool LimbRegenerationMinEventsWhole;

		// Token: 0x0400426E RID: 17006
		public string LimbRegenerationMinEventsCategories;

		// Token: 0x0400426F RID: 17007
		public string LimbRegenerationMinEventsExceptCategories;
	}
}
