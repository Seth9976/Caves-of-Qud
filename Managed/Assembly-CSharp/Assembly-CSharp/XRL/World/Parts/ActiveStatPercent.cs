using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.Collections;

namespace XRL.World.Parts
{
	// Token: 0x02000FEB RID: 4075
	[Serializable]
	public class ActiveStatPercent : IActivePart
	{
		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600A264 RID: 41572 RVA: 0x003B57CB File Offset: 0x003B39CB
		// (set) Token: 0x0600A265 RID: 41573 RVA: 0x003B57D7 File Offset: 0x003B39D7
		public bool Applied
		{
			get
			{
				return this.ApplyTurn != 0L;
			}
			set
			{
				long num;
				if (!value)
				{
					num = 0L;
				}
				else
				{
					XRLGame game = The.Game;
					num = ((game != null) ? game.Turns : 1L);
				}
				this.ApplyTurn = num;
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x0600A266 RID: 41574 RVA: 0x003B57F8 File Offset: 0x003B39F8
		public StringMap<float> Parsed
		{
			get
			{
				if (this._Parsed == null)
				{
					this.ParseBoosts();
				}
				return this._Parsed;
			}
		}

		// Token: 0x0600A267 RID: 41575 RVA: 0x003B580E File Offset: 0x003B3A0E
		public ActiveStatPercent()
		{
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A268 RID: 41576 RVA: 0x003B5830 File Offset: 0x003B3A30
		public void CheckStatus(GameObject Object = null)
		{
			if (Object == null)
			{
				Object = this.GetActivePartFirstSubject();
				if (Object == null)
				{
					return;
				}
			}
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.Unapply(Object);
				return;
			}
			this.Apply(Object, false);
		}

		// Token: 0x0600A269 RID: 41577 RVA: 0x003B5884 File Offset: 0x003B3A84
		public void Apply(GameObject Object, bool Force = false)
		{
			if (this.Applied && !Force)
			{
				XRLGame game = The.Game;
				if (game == null || this.ApplyTurn > game.Turns - 10L)
				{
					return;
				}
			}
			this.Applied = true;
			foreach (KeyValuePair<string, float> keyValuePair in this.Parsed)
			{
				Statistic statistic;
				if (Object.Statistics.TryGetValue(keyValuePair.Key, out statistic))
				{
					int num = statistic.BaseValue;
					if (this.CountAsBase && !statistic.Shifts.IsNullOrEmpty<Statistic.StatShift>())
					{
						foreach (Statistic.StatShift statShift in statistic.Shifts)
						{
							if (statShift.BaseValue)
							{
								num -= statShift.Amount;
							}
						}
					}
					base.StatShifter.SetStatShift(Object, statistic.Name, Mathf.RoundToInt((float)num * keyValuePair.Value), this.CountAsBase);
				}
			}
		}

		// Token: 0x0600A26A RID: 41578 RVA: 0x003B59B8 File Offset: 0x003B3BB8
		public void Unapply(GameObject Object)
		{
			if (!this.Applied)
			{
				return;
			}
			this.Applied = false;
			base.StatShifter.RemoveStatShifts(Object);
		}

		// Token: 0x0600A26B RID: 41579 RVA: 0x003B59D8 File Offset: 0x003B3BD8
		public void ParseBoosts()
		{
			if (this._Parsed == null)
			{
				this._Parsed = new StringMap<float>();
			}
			this._Parsed.ClearValues();
			foreach (ReadOnlySpan<char> readOnlySpan in this.Boosts.DelimitedBy(';'))
			{
				ReadOnlySpan<char> readOnlySpan2;
				ReadOnlySpan<char> readOnlySpan3;
				readOnlySpan.Split(':', out readOnlySpan2, out readOnlySpan3);
				int num;
				if (int.TryParse(readOnlySpan3, out num))
				{
					float value = this._Parsed.GetValue(readOnlySpan2);
					this._Parsed[readOnlySpan2] = value + (float)num / 100f;
				}
			}
		}

		// Token: 0x0600A26C RID: 41580 RVA: 0x003B5A66 File Offset: 0x003B3C66
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A26D RID: 41581 RVA: 0x003B5A6C File Offset: 0x003B3C6C
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.Applied && !base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
			}
			this.CheckStatus(null);
		}

		// Token: 0x0600A26E RID: 41582 RVA: 0x003B5AB8 File Offset: 0x003B3CB8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == EquippedEvent.ID && this.WorksOnEquipper) || (ID == UnequippedEvent.ID && this.WorksOnEquipper) || (ID == BootSequenceDoneEvent.ID && this.IsBootSensitive) || (ID == BootSequenceInitializedEvent.ID && this.IsBootSensitive) || (ID == PowerSwitchFlippedEvent.ID && this.IsPowerSwitchSensitive) || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID || (ID == GetShortDescriptionEvent.ID && this.Describe);
		}

		// Token: 0x0600A26F RID: 41583 RVA: 0x003B5B49 File Offset: 0x003B3D49
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.CheckStatus(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A270 RID: 41584 RVA: 0x003B5B59 File Offset: 0x003B3D59
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.CheckStatus(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A271 RID: 41585 RVA: 0x003B5B6C File Offset: 0x003B3D6C
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.Describe)
			{
				bool flag = this.IsPowerSwitchSensitive && this.ParentObject.HasPart<PowerSwitch>();
				foreach (KeyValuePair<string, float> keyValuePair in this.Parsed)
				{
					E.Postfix.Append("\n{{rules|");
					Statistic.AppendStatAdjustDescription(E.Postfix, keyValuePair.Key, Mathf.RoundToInt(keyValuePair.Value * 100f), flag, true);
					base.AddStatusSummary(E.Postfix);
					E.Postfix.Append("}}");
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A272 RID: 41586 RVA: 0x003B5C38 File Offset: 0x003B3E38
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				float num;
				if (this.Parsed.TryGetValue("Strength", out num) && num > 0f)
				{
					E.Add("might", 1);
				}
				else if (this.Parsed.TryGetValue("Intelligence", out num) && num > 0f)
				{
					E.Add("scholarship", 1);
				}
				else if (this.Parsed.TryGetValue("Speed", out num) && num > 0f)
				{
					E.Add("time", 1);
				}
				else if (this.Parsed.TryGetValue("MoveSpeed", out num) && num > 0f)
				{
					E.Add("travel", 1);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A273 RID: 41587 RVA: 0x003B5D03 File Offset: 0x003B3F03
		public override bool HandleEvent(EquippedEvent E)
		{
			this.CheckStatus(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A274 RID: 41588 RVA: 0x003B5D18 File Offset: 0x003B3F18
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.Unapply(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A275 RID: 41589 RVA: 0x003B5D2D File Offset: 0x003B3F2D
		public override bool HandleEvent(BootSequenceInitializedEvent E)
		{
			this.CheckStatus(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A276 RID: 41590 RVA: 0x003B5D3D File Offset: 0x003B3F3D
		public override bool HandleEvent(BootSequenceDoneEvent E)
		{
			this.CheckStatus(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A277 RID: 41591 RVA: 0x003B5D4D File Offset: 0x003B3F4D
		public override bool HandleEvent(CellChangedEvent E)
		{
			this.CheckStatus(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A278 RID: 41592 RVA: 0x003B5D5D File Offset: 0x003B3F5D
		public override bool HandleEvent(PowerSwitchFlippedEvent E)
		{
			this.CheckStatus(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0400434E RID: 17230
		public string Boosts = "";

		// Token: 0x0400434F RID: 17231
		public long ApplyTurn;

		// Token: 0x04004350 RID: 17232
		public bool Describe = true;

		// Token: 0x04004351 RID: 17233
		public bool CountAsBase;

		// Token: 0x04004352 RID: 17234
		[NonSerialized]
		private StringMap<float> _Parsed;
	}
}
