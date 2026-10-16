using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is set to be
	/// true, which it is not by default, the standard power load bonus (i.e.
	/// 2 for the standard overload power load of 400) is treated as an
	/// additional stat bonus to attempt to distribute according to the
	/// pattern established by the configured stat bonuses.  If there is only
	/// one stat being modified, the bonus is added to that stat.  If a number
	/// of stats equal to the bonus is being modified, each is increased by 1.
	/// Otherwise, the configured bonuses are sorted first by descending order
	/// of bonus size, then alphabetically, and the bonus amount is distributed
	/// by moving down this list adding one at a time, starting over at the top
	/// if the end is reached, until the bonus is exhausted.
	/// </remarks>
	// Token: 0x02001026 RID: 4134
	[Serializable]
	public class EquipStatBoost : IActivePart
	{
		// Token: 0x0600A4A1 RID: 42145 RVA: 0x003C301D File Offset: 0x003C121D
		public EquipStatBoost()
		{
			base.IsTechScannable = true;
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A4A2 RID: 42146 RVA: 0x003C303E File Offset: 0x003C123E
		public EquipStatBoost(string Boosts)
			: this()
		{
			this.Boosts = Boosts;
		}

		// Token: 0x0600A4A3 RID: 42147 RVA: 0x003C3050 File Offset: 0x003C1250
		public static void AppendBoostOnEquip(GameObject obj, string boost, string nameForStatus = null, bool techScan = false)
		{
			EquipStatBoost equipStatBoost;
			if (obj.HasPart<EquipStatBoost>())
			{
				equipStatBoost = obj.GetPart<EquipStatBoost>();
				equipStatBoost.AddBonuses(boost);
			}
			else
			{
				equipStatBoost = new EquipStatBoost(boost);
				equipStatBoost.DescribeStatusForProperty = null;
				if (!techScan && equipStatBoost.IsTechScannable)
				{
					equipStatBoost.IsTechScannable = false;
				}
				obj.AddPart<EquipStatBoost>(equipStatBoost, true, false);
			}
			if (equipStatBoost.NameForStatus == null && nameForStatus != null)
			{
				equipStatBoost.NameForStatus = nameForStatus;
			}
		}

		// Token: 0x0600A4A4 RID: 42148 RVA: 0x003C30B4 File Offset: 0x003C12B4
		public void AddBonuses(string Spec)
		{
			Dictionary<string, int> bonusList = this.GetBonusList(false, true);
			Dictionary<string, int> dictionary = this.DetermineBonusList(Spec, true);
			foreach (string text in dictionary.Keys)
			{
				if (bonusList.ContainsKey(text))
				{
					Dictionary<string, int> dictionary2 = bonusList;
					string text2 = text;
					dictionary2[text2] += dictionary[text];
				}
				else
				{
					bonusList.Add(text, dictionary[text]);
				}
			}
			this.Boosts = EquipStatBoost.BonusesToString(bonusList);
		}

		// Token: 0x0600A4A5 RID: 42149 RVA: 0x003C3158 File Offset: 0x003C1358
		private Dictionary<string, int> DetermineBonusList(string Spec, bool Unmodified = false)
		{
			if (Spec.IsNullOrEmpty())
			{
				return new Dictionary<string, int>(0);
			}
			string[] array = Spec.Split(';', StringSplitOptions.None);
			Dictionary<string, int> Result = new Dictionary<string, int>(array.Length);
			foreach (string text in array)
			{
				if (!text.IsNullOrEmpty())
				{
					string[] array3 = text.Split(':', StringSplitOptions.None);
					if (Result.ContainsKey(array3[0]))
					{
						Dictionary<string, int> dictionary = Result;
						string text2 = array3[0];
						dictionary[text2] += Convert.ToInt32(array3[1]);
					}
					else
					{
						Result.Add(array3[0], Convert.ToInt32(array3[1]));
					}
				}
			}
			int j = ((Unmodified || !this.IsPowerLoadSensitive) ? 0 : this.MyPowerLoadBonus(int.MinValue, 100, 150));
			if (j > 0 && Result.Count > 0)
			{
				if (Result.Count == 1)
				{
					using (List<string>.Enumerator enumerator = Result.Keys.ToList<string>().GetEnumerator())
					{
						if (!enumerator.MoveNext())
						{
							goto IL_0243;
						}
						string text3 = enumerator.Current;
						Dictionary<string, int> dictionary = Result;
						string text2 = text3;
						dictionary[text2] += j;
						goto IL_0243;
					}
				}
				if (Result.Count == j)
				{
					using (List<string>.Enumerator enumerator = Result.Keys.ToList<string>().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text4 = enumerator.Current;
							Dictionary<string, int> result = Result;
							string text2 = text4;
							int i = result[text2];
							result[text2] = i + 1;
						}
						goto IL_0243;
					}
				}
				List<string> list = new List<string>(Result.Keys);
				list.Sort(delegate(string a, string b)
				{
					int num = Result[a].CompareTo(Result[b]);
					if (num != 0)
					{
						return -num;
					}
					return a.CompareTo(b);
				});
				while (j > 0)
				{
					foreach (string text5 in list)
					{
						Dictionary<string, int> result2 = Result;
						string text2 = text5;
						int i = result2[text2];
						result2[text2] = i + 1;
						j--;
						if (j <= 0)
						{
							break;
						}
					}
				}
			}
			IL_0243:
			return Result;
		}

		// Token: 0x0600A4A6 RID: 42150 RVA: 0x003C33D8 File Offset: 0x003C15D8
		private static string BonusesToString(Dictionary<string, int> Bonuses)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			int num = 0;
			foreach (KeyValuePair<string, int> keyValuePair in Bonuses)
			{
				if (num++ > 0)
				{
					stringBuilder.Append(';');
				}
				stringBuilder.Append(keyValuePair.Key).Append(':').Append(keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600A4A7 RID: 42151 RVA: 0x003C3460 File Offset: 0x003C1660
		public Dictionary<string, int> GetBonusList(bool ForceRebuild = false, bool Unmodified = false)
		{
			if (Unmodified)
			{
				return this.DetermineBonusList(this.Boosts, true);
			}
			if (ForceRebuild || this._BonusList == null)
			{
				this._BonusList = this.DetermineBonusList(this.Boosts, false);
			}
			return this._BonusList;
		}

		// Token: 0x0600A4A8 RID: 42152 RVA: 0x003C3498 File Offset: 0x003C1698
		public void Apply(GameObject Object)
		{
			if (this.Applied)
			{
				return;
			}
			foreach (KeyValuePair<string, int> keyValuePair in this.GetBonusList(false, false))
			{
				base.StatShifter.SetStatShift(Object, keyValuePair.Key, keyValuePair.Value, this.CountAsBase);
			}
			this.Applied = true;
		}

		// Token: 0x0600A4A9 RID: 42153 RVA: 0x003C3518 File Offset: 0x003C1718
		public void UnapplyEffects(GameObject Object)
		{
			if (!this.Applied || Object == null)
			{
				return;
			}
			base.StatShifter.RemoveStatShifts();
			this.Applied = false;
		}

		// Token: 0x0600A4AA RID: 42154 RVA: 0x003C3538 File Offset: 0x003C1738
		public override bool SameAs(IPart p)
		{
			return !((p as EquipStatBoost).Boosts != this.Boosts) && base.SameAs(p);
		}

		// Token: 0x0600A4AB RID: 42155 RVA: 0x003C355B File Offset: 0x003C175B
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A4AC RID: 42156 RVA: 0x003C3560 File Offset: 0x003C1760
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.Applied && !base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
			}
			this.CheckApplyEffects(null);
		}

		// Token: 0x0600A4AD RID: 42157 RVA: 0x003C35AC File Offset: 0x003C17AC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BootSequenceDoneEvent.ID || ID == BootSequenceInitializedEvent.ID || ID == CellChangedEvent.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == EquippedEvent.ID || ID == ExamineCriticalFailureEvent.ID || ID == PooledEvent<GetDisplayStatBonusEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<ModificationAppliedEvent>.ID || ID == PowerSwitchFlippedEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A4AE RID: 42158 RVA: 0x003C3630 File Offset: 0x003C1830
		public override bool HandleEvent(GetDisplayStatBonusEvent E)
		{
			int num;
			if (this.Applied && this.GetBonusList(false, false).TryGetValue(E.Stat, out num) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Amount += num;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4AF RID: 42159 RVA: 0x003C3699 File Offset: 0x003C1899
		public override bool HandleEvent(ModificationAppliedEvent E)
		{
			this.UnapplyEffects(this.ParentObject.Equipped);
			this.GetBonusList(true, false);
			this.CheckApplyEffects(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4B0 RID: 42160 RVA: 0x003C36C3 File Offset: 0x003C18C3
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.CheckApplyEffects(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4B1 RID: 42161 RVA: 0x003C36D3 File Offset: 0x003C18D3
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.CheckApplyEffects(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4B2 RID: 42162 RVA: 0x003C36E4 File Offset: 0x003C18E4
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			bool flag = this.IsPowerSwitchSensitive && this.ParentObject.HasPart<PowerSwitch>();
			foreach (KeyValuePair<string, int> keyValuePair in this.GetBonusList(false, false))
			{
				E.Postfix.Append("\n{{rules|");
				Statistic.AppendStatAdjustDescription(E.Postfix, keyValuePair.Key, keyValuePair.Value, flag, false);
				base.AddStatusSummary(E.Postfix);
				E.Postfix.Append("}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4B3 RID: 42163 RVA: 0x003C379C File Offset: 0x003C199C
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				foreach (KeyValuePair<string, int> keyValuePair in this.GetBonusList(false, false))
				{
					if (keyValuePair.Key == "Strength")
					{
						if (keyValuePair.Value > 0)
						{
							E.Add("might", keyValuePair.Value.DiminishingReturns(1.0));
						}
					}
					else if (keyValuePair.Key == "Intelligence")
					{
						if (keyValuePair.Value > 0)
						{
							E.Add("scholarship", keyValuePair.Value.DiminishingReturns(1.0));
						}
					}
					else if (keyValuePair.Key == "Quickness")
					{
						if (keyValuePair.Value != 0)
						{
							E.Add("time", Math.Abs(keyValuePair.Value).DiminishingReturns(1.0));
						}
					}
					else if (keyValuePair.Key == "MoveSpeed" && keyValuePair.Value < 0)
					{
						E.Add("travel", Math.Abs(keyValuePair.Value).DiminishingReturns(1.0));
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4B4 RID: 42164 RVA: 0x003C3924 File Offset: 0x003C1B24
		public override bool HandleEvent(EquippedEvent E)
		{
			this.CheckApplyEffects(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4B5 RID: 42165 RVA: 0x003C3939 File Offset: 0x003C1B39
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.UnapplyEffects(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4B6 RID: 42166 RVA: 0x003C394E File Offset: 0x003C1B4E
		public override bool HandleEvent(BootSequenceInitializedEvent E)
		{
			this.CheckApplyEffects(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4B7 RID: 42167 RVA: 0x003C395E File Offset: 0x003C1B5E
		public override bool HandleEvent(BootSequenceDoneEvent E)
		{
			this.CheckApplyEffects(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4B8 RID: 42168 RVA: 0x003C396E File Offset: 0x003C1B6E
		public override bool HandleEvent(CellChangedEvent E)
		{
			this.CheckApplyEffects(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4B9 RID: 42169 RVA: 0x003C397E File Offset: 0x003C1B7E
		public override bool HandleEvent(PowerSwitchFlippedEvent E)
		{
			this.CheckApplyEffects(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4BA RID: 42170 RVA: 0x003C398E File Offset: 0x003C1B8E
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return !this.ExamineFailure(E, 50) && base.HandleEvent(E);
		}

		// Token: 0x0600A4BB RID: 42171 RVA: 0x003C39A4 File Offset: 0x003C1BA4
		private bool ExamineFailure(IExamineEvent E, int Chance)
		{
			int num;
			if (E.Pass == 1 && GlobalConfig.GetBoolSetting("ContextualExamineFailures", false) && Chance.in100() && this.GetBonusList(false, false).TryGetValue("Strength", out num) && base.IsReady(true, false, false, false, false, false, false, false, true, false, 1, null, false, 0L, null))
			{
				base.EmitMessage(string.Concat(new string[]
				{
					"Suddenly ",
					this.ParentObject.does("start", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
					" flailing around and battering ",
					E.Actor.IsPlayer() ? "you" : E.Actor.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
					"!"
				}), ' ', false, E.Actor.IsPlayer(), false, null, null);
				E.Actor.TakeDamage((num.ToString() + "d6").RollCached(), "from %t pummeling!", "Melee Unarmed", null, null, E.Actor, null, this.ParentObject, null, null, false, false, true, false, false, false, false, false, E.Actor.IsPlayer(), 5, null);
				E.Identify = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600A4BC RID: 42172 RVA: 0x003C3B28 File Offset: 0x003C1D28
		public void CheckApplyEffects(GameObject obj = null)
		{
			if (obj == null)
			{
				obj = this.ParentObject.Equipped;
				if (obj == null)
				{
					return;
				}
			}
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.UnapplyEffects(obj);
				return;
			}
			this.Apply(obj);
		}

		// Token: 0x04004432 RID: 17458
		public string Boosts = "";

		// Token: 0x04004433 RID: 17459
		[NonSerialized]
		public Dictionary<string, int> _BonusList;

		// Token: 0x04004434 RID: 17460
		public bool Applied;

		// Token: 0x04004435 RID: 17461
		public bool CountAsBase;
	}
}
