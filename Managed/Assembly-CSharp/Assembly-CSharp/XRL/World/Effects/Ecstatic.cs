using System;
using XRL.Core;

namespace XRL.World.Effects
{
	// Token: 0x02000D2E RID: 3374
	[Serializable]
	public class Ecstatic : Effect
	{
		// Token: 0x06008507 RID: 34055 RVA: 0x003009BC File Offset: 0x002FEBBC
		public Ecstatic()
		{
			this.DisplayName = "{{G|ecstatic}}";
			this.Duration = 1;
		}

		// Token: 0x06008508 RID: 34056 RVA: 0x003009D6 File Offset: 0x002FEBD6
		public Ecstatic(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008509 RID: 34057 RVA: 0x003009E5 File Offset: 0x002FEBE5
		public override int GetEffectType()
		{
			return 16777218;
		}

		// Token: 0x0600850A RID: 34058 RVA: 0x003009EC File Offset: 0x002FEBEC
		public override string GetDetails()
		{
			return "+10 Quickness";
		}

		// Token: 0x0600850B RID: 34059 RVA: 0x003009F3 File Offset: 0x002FEBF3
		public override bool Apply(GameObject Object)
		{
			if (!Object.FireEvent(Event.New("ApplyEcstatic", "Duration", this.Duration)))
			{
				return false;
			}
			this.ApplyChanges();
			return true;
		}

		// Token: 0x0600850C RID: 34060 RVA: 0x00300A1B File Offset: 0x002FEC1B
		public override void Remove(GameObject Object)
		{
			this.UnapplyChanges();
			base.Remove(Object);
		}

		// Token: 0x0600850D RID: 34061 RVA: 0x00300A2A File Offset: 0x002FEC2A
		private void ApplyChanges()
		{
			base.StatShifter.SetStatShift("Speed", 10, false);
		}

		// Token: 0x0600850E RID: 34062 RVA: 0x00300A40 File Offset: 0x002FEC40
		private void UnapplyChanges()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x0600850F RID: 34063 RVA: 0x00300A4D File Offset: 0x002FEC4D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008510 RID: 34064 RVA: 0x00300A78 File Offset: 0x002FEC78
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				this.Duration--;
			}
			else if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyChanges();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyChanges();
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008511 RID: 34065 RVA: 0x00300AE0 File Offset: 0x002FECE0
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 10)
				{
					E.RenderString = "!";
					E.ColorString = "&G";
					return false;
				}
			}
			return true;
		}
	}
}
