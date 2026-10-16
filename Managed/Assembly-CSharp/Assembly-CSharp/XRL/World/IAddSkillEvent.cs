using System;
using XRL.World.Parts.Skill;

namespace XRL.World
{
	// Token: 0x020007BB RID: 1979
	[GameEvent(Base = true)]
	public abstract class IAddSkillEvent : MinEvent
	{
		// Token: 0x0600503C RID: 20540 RVA: 0x00186266 File Offset: 0x00184466
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600503D RID: 20541 RVA: 0x00186270 File Offset: 0x00184470
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Source = null;
			this.Skill = null;
			this.Entry = null;
			this.Context = null;
			this.WaterRitualContext = null;
			this.InclusionContext = null;
			this.BookContext = null;
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600503E RID: 20542 RVA: 0x001862CC File Offset: 0x001844CC
		public bool IsWaterRitual
		{
			get
			{
				bool flag = this.WaterRitualContext.GetValueOrDefault();
				if (this.WaterRitualContext == null)
				{
					flag = this.Context.HasDelimitedSubstring(',', "WaterRitual", StringComparison.Ordinal);
					this.WaterRitualContext = new bool?(flag);
					return flag;
				}
				return flag;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600503F RID: 20543 RVA: 0x00186318 File Offset: 0x00184518
		public bool IsInclusion
		{
			get
			{
				bool flag = this.InclusionContext.GetValueOrDefault();
				if (this.InclusionContext == null)
				{
					flag = this.Context.HasDelimitedSubstring(',', "Inclusion", StringComparison.Ordinal);
					this.InclusionContext = new bool?(flag);
					return flag;
				}
				return flag;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06005040 RID: 20544 RVA: 0x00186364 File Offset: 0x00184564
		public bool IsBook
		{
			get
			{
				bool flag = this.BookContext.GetValueOrDefault();
				if (this.BookContext == null)
				{
					flag = this.Context.HasDelimitedSubstring(',', "TrainingBook", StringComparison.Ordinal);
					this.BookContext = new bool?(flag);
					return flag;
				}
				return flag;
			}
		}

		// Token: 0x040027B8 RID: 10168
		public GameObject Actor;

		// Token: 0x040027B9 RID: 10169
		public GameObject Source;

		// Token: 0x040027BA RID: 10170
		public BaseSkill Skill;

		// Token: 0x040027BB RID: 10171
		public IBaseSkillEntry Entry;

		// Token: 0x040027BC RID: 10172
		public string Context;

		// Token: 0x040027BD RID: 10173
		private bool? WaterRitualContext;

		// Token: 0x040027BE RID: 10174
		private bool? InclusionContext;

		// Token: 0x040027BF RID: 10175
		private bool? BookContext;
	}
}
