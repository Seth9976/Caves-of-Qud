using System;
using System.Collections.Generic;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x02001060 RID: 4192
	[Serializable]
	public class LowStatBooster : IPoweredPart
	{
		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x0600A6D0 RID: 42704 RVA: 0x003D00E0 File Offset: 0x003CE2E0
		// (set) Token: 0x0600A6CF RID: 42703 RVA: 0x003D00B8 File Offset: 0x003CE2B8
		public string AffectedStats
		{
			get
			{
				return this._AffectedStats;
			}
			set
			{
				if (value != this.AffectedStats)
				{
					this.UnapplyEffects(null);
					this._AffectedStats = value;
					this.CheckApplyEffects(null, false, 1);
				}
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x0600A6D2 RID: 42706 RVA: 0x003D010B File Offset: 0x003CE30B
		// (set) Token: 0x0600A6D1 RID: 42705 RVA: 0x003D00E8 File Offset: 0x003CE2E8
		public int Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				if (value != this.Amount)
				{
					this.UnapplyEffects(null);
					this._Amount = value;
					this.CheckApplyEffects(null, false, 1);
				}
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x0600A6D4 RID: 42708 RVA: 0x003D013B File Offset: 0x003CE33B
		// (set) Token: 0x0600A6D3 RID: 42707 RVA: 0x003D0113 File Offset: 0x003CE313
		public string ActiveStat
		{
			get
			{
				return this._ActiveStat;
			}
			set
			{
				if (value != this.ActiveStat)
				{
					this.UnapplyEffects(null);
					this._ActiveStat = value;
					this.CheckApplyEffects(null, false, 1);
				}
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x0600A6D5 RID: 42709 RVA: 0x003D0143 File Offset: 0x003CE343
		public List<string> AffectedStatList
		{
			get
			{
				if (this._AffectedStatList == null)
				{
					this._AffectedStatList = new List<string>(this.AffectedStats.Split(',', StringSplitOptions.None));
				}
				return this._AffectedStatList;
			}
		}

		// Token: 0x0600A6D6 RID: 42710 RVA: 0x003D016C File Offset: 0x003CE36C
		public LowStatBooster()
		{
			this.ChargeUse = 0;
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A6D7 RID: 42711 RVA: 0x003D0194 File Offset: 0x003CE394
		public override bool SameAs(IPart p)
		{
			LowStatBooster lowStatBooster = p as LowStatBooster;
			return !(lowStatBooster.AffectedStats != this.AffectedStats) && lowStatBooster.Amount == this.Amount && !(lowStatBooster.ActiveStat != this.ActiveStat) && lowStatBooster.Applied == this.Applied && base.SameAs(p);
		}

		// Token: 0x0600A6D8 RID: 42712 RVA: 0x003D01F9 File Offset: 0x003CE3F9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EffectAppliedEvent.ID || ID == EquippedEvent.ID || ID == GetShortDescriptionEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A6D9 RID: 42713 RVA: 0x003D0227 File Offset: 0x003CE427
		public override bool HandleEvent(EquippedEvent E)
		{
			this.CheckApplyEffects(E.Actor, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A6DA RID: 42714 RVA: 0x003D023E File Offset: 0x003CE43E
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.UnapplyEffects(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A6DB RID: 42715 RVA: 0x003D0254 File Offset: 0x003CE454
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.CheckApplyEffects(null, false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A6DC RID: 42716 RVA: 0x003D0268 File Offset: 0x003CE468
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.Append("\n{{rules|");
			if (this.Amount > 0)
			{
				E.Postfix.Append('+').Append(this.Amount);
			}
			else
			{
				E.Postfix.Append(this.Amount);
			}
			E.Postfix.Append(" to the lowest of ");
			List<string> list = new List<string>(this.AffectedStatList.Count);
			foreach (string text in this.AffectedStatList)
			{
				list.Add(Statistic.GetStatDisplayName(text));
			}
			E.Postfix.Append(Grammar.MakeOrList(list, true)).Append('.');
			base.AddStatusSummary(E.Postfix);
			E.Postfix.Append("}}");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A6DD RID: 42717 RVA: 0x003D0368 File Offset: 0x003CE568
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A6DE RID: 42718 RVA: 0x003D036B File Offset: 0x003CE56B
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckApplyEffects(null, this.Applied, Amount);
		}

		// Token: 0x0600A6DF RID: 42719 RVA: 0x003D037B File Offset: 0x003CE57B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CellChanged");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A6E0 RID: 42720 RVA: 0x003D0390 File Offset: 0x003CE590
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CellChanged")
			{
				this.CheckApplyEffects(null, false, 1);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A6E1 RID: 42721 RVA: 0x003D03B4 File Offset: 0x003CE5B4
		private GameObject DefaultToSubject(GameObject obj = null)
		{
			return obj ?? this.GetActivePartFirstSubject();
		}

		// Token: 0x0600A6E2 RID: 42722 RVA: 0x003D03C4 File Offset: 0x003CE5C4
		private bool ApplyEffects(GameObject obj = null)
		{
			if (this.Applied)
			{
				return false;
			}
			if ((obj = this.DefaultToSubject(obj)) == null)
			{
				return false;
			}
			if (this.ActiveStat != null && obj.HasStat(this.ActiveStat))
			{
				int num = (Statistic.IsInverseBenefit(this.ActiveStat) ? (-this.Amount) : this.Amount);
				if (num > 0)
				{
					obj.GetStat(this.ActiveStat).Bonus += num;
				}
				else
				{
					obj.GetStat(this.ActiveStat).Penalty += -num;
				}
			}
			this.Applied = true;
			return true;
		}

		// Token: 0x0600A6E3 RID: 42723 RVA: 0x003D0460 File Offset: 0x003CE660
		private bool UnapplyEffects(GameObject obj = null)
		{
			if (!this.Applied)
			{
				return false;
			}
			if ((obj = this.DefaultToSubject(obj)) == null)
			{
				return false;
			}
			if (this.ActiveStat != null && obj.HasStat(this.ActiveStat))
			{
				int num = (Statistic.IsInverseBenefit(this.ActiveStat) ? (-this.Amount) : this.Amount);
				if (num > 0)
				{
					obj.GetStat(this.ActiveStat).Bonus -= num;
				}
				else
				{
					obj.GetStat(this.ActiveStat).Penalty -= -num;
				}
			}
			this.Applied = false;
			return true;
		}

		// Token: 0x0600A6E4 RID: 42724 RVA: 0x003D04FC File Offset: 0x003CE6FC
		private string LowestStat(GameObject obj)
		{
			if ((obj = this.DefaultToSubject(obj)) == null)
			{
				return null;
			}
			string text = null;
			int num = int.MaxValue;
			foreach (string text2 in this.AffectedStatList)
			{
				if (obj.HasStat(text2))
				{
					int num2 = obj.BaseStat(text2, 0);
					if (Statistic.IsInverseBenefit(text2))
					{
						num2 = 115 - num2;
					}
					if (num2 < num)
					{
						num = num2;
						text = text2;
					}
				}
			}
			return text;
		}

		// Token: 0x0600A6E5 RID: 42725 RVA: 0x003D058C File Offset: 0x003CE78C
		public void CheckApplyEffects(GameObject obj = null, bool UseCharge = false, int MultipleCharge = 1)
		{
			if (base.IsDisabled(UseCharge, false, false, false, false, false, false, false, false, false, MultipleCharge, null, false, 0L, null))
			{
				this.UnapplyEffects(obj);
				return;
			}
			string text = this.LowestStat(obj);
			if (text != this.ActiveStat)
			{
				this.UnapplyEffects(obj);
				this._ActiveStat = text;
				this.ApplyEffects(obj);
				return;
			}
			this.ApplyEffects(obj);
		}

		// Token: 0x040044F1 RID: 17649
		public string _AffectedStats = "Strength,Agility,Toughness,Intelligence,Willpower,Ego";

		// Token: 0x040044F2 RID: 17650
		public int _Amount = 3;

		// Token: 0x040044F3 RID: 17651
		public string _ActiveStat;

		// Token: 0x040044F4 RID: 17652
		[NonSerialized]
		private List<string> _AffectedStatList;

		// Token: 0x040044F5 RID: 17653
		public bool Applied;
	}
}
