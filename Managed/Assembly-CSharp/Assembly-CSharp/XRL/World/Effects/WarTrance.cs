using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000DAB RID: 3499
	[Serializable]
	public class WarTrance : Effect, ITierInitialized
	{
		// Token: 0x06008BC4 RID: 35780 RVA: 0x0031A519 File Offset: 0x00318719
		public WarTrance()
		{
			this.Duration = 2;
			this.DisplayName = "{{r|war trance}}";
		}

		// Token: 0x06008BC5 RID: 35781 RVA: 0x0031A533 File Offset: 0x00318733
		public WarTrance(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008BC6 RID: 35782 RVA: 0x0031A542 File Offset: 0x00318742
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(50, 1000);
		}

		// Token: 0x06008BC7 RID: 35783 RVA: 0x0031A556 File Offset: 0x00318756
		public override int GetEffectType()
		{
			return 83886082;
		}

		// Token: 0x06008BC8 RID: 35784 RVA: 0x0031A55D File Offset: 0x0031875D
		public override string GetStateDescription()
		{
			return "in a {{r|war trance}}";
		}

		// Token: 0x06008BC9 RID: 35785 RVA: 0x0031A564 File Offset: 0x00318764
		public override string GetDetails()
		{
			return "+5 Quickness\n+6 Willpower";
		}

		// Token: 0x06008BCA RID: 35786 RVA: 0x0031A56B File Offset: 0x0031876B
		public override bool Apply(GameObject Object)
		{
			if (!Object.FireEvent(Event.New("ApplyWarTrance", "Duration", this.Duration)))
			{
				return false;
			}
			this.ApplyChanges();
			return true;
		}

		// Token: 0x06008BCB RID: 35787 RVA: 0x0031A593 File Offset: 0x00318793
		public override void Remove(GameObject Object)
		{
			this.UnapplyChanges();
			base.Remove(Object);
		}

		// Token: 0x06008BCC RID: 35788 RVA: 0x0031A5A2 File Offset: 0x003187A2
		private void ApplyChanges()
		{
			base.StatShifter.SetStatShift("Speed", 5, false);
			base.StatShifter.SetStatShift("Willpower", 6, false);
		}

		// Token: 0x06008BCD RID: 35789 RVA: 0x0031A5CA File Offset: 0x003187CA
		private void UnapplyChanges()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x06008BCE RID: 35790 RVA: 0x0031A5D8 File Offset: 0x003187D8
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 10)
				{
					E.RenderString = "!";
					E.ColorString = "&R";
				}
			}
			return true;
		}

		// Token: 0x06008BCF RID: 35791 RVA: 0x0031A617 File Offset: 0x00318817
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008BD0 RID: 35792 RVA: 0x0031A644 File Offset: 0x00318844
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
	}
}
