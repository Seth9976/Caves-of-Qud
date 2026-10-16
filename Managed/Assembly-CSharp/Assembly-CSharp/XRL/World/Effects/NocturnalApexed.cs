using System;
using XRL.Core;

namespace XRL.World.Effects
{
	// Token: 0x02000D67 RID: 3431
	[Serializable]
	public class NocturnalApexed : Effect, ITierInitialized
	{
		// Token: 0x060087C4 RID: 34756 RVA: 0x0030B14A File Offset: 0x0030934A
		public NocturnalApexed()
		{
			this.DisplayName = "{{r|prowling}}";
		}

		// Token: 0x060087C5 RID: 34757 RVA: 0x0030B16C File Offset: 0x0030936C
		public NocturnalApexed(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x060087C6 RID: 34758 RVA: 0x0030B17B File Offset: 0x0030937B
		public NocturnalApexed(int Duration, int MoveSpeedShift, int AgilityShift, GameObject Source)
			: this(Duration)
		{
			this.MoveSpeedShift = MoveSpeedShift;
			this.AgilityShift = AgilityShift;
			this.Source = Source.ID;
		}

		// Token: 0x060087C7 RID: 34759 RVA: 0x0030B19F File Offset: 0x0030939F
		public void Initialize(int Tier)
		{
			this.Duration = 100;
		}

		// Token: 0x060087C8 RID: 34760 RVA: 0x0030B1A9 File Offset: 0x003093A9
		public override int GetEffectType()
		{
			return 83886084;
		}

		// Token: 0x060087C9 RID: 34761 RVA: 0x0030B1B0 File Offset: 0x003093B0
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x060087CA RID: 34762 RVA: 0x0030B1B3 File Offset: 0x003093B3
		public override string GetDetails()
		{
			return "+6 Agility\n+10 Move Speed";
		}

		// Token: 0x060087CB RID: 34763 RVA: 0x0030B1BA File Offset: 0x003093BA
		public override bool Apply(GameObject Object)
		{
			if (!Object.FireEvent(Event.New("ApplyNocturnalApex", "Event", this)))
			{
				return false;
			}
			if (Object.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("You start to prowl.", 'g', true);
			}
			this.ApplyChanges();
			return true;
		}

		// Token: 0x060087CC RID: 34764 RVA: 0x0030B1F2 File Offset: 0x003093F2
		private void ApplyChanges()
		{
			base.StatShifter.SetStatShift("MoveSpeed", this.MoveSpeedShift, false);
			base.StatShifter.SetStatShift("Agility", this.AgilityShift, false);
		}

		// Token: 0x060087CD RID: 34765 RVA: 0x0030B224 File Offset: 0x00309424
		private void UnapplyChanges()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x060087CE RID: 34766 RVA: 0x0030B231 File Offset: 0x00309431
		public override void Remove(GameObject Object)
		{
			this.UnapplyChanges();
			base.Remove(Object);
		}

		// Token: 0x060087CF RID: 34767 RVA: 0x0030B240 File Offset: 0x00309440
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0 && XRLCore.CurrentFrame % 20 > 10)
			{
				E.RenderString = "\u0003";
				E.ColorString = "&K";
			}
			return true;
		}

		// Token: 0x060087D0 RID: 34768 RVA: 0x0030B26E File Offset: 0x0030946E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID;
		}

		// Token: 0x060087D1 RID: 34769 RVA: 0x0030B284 File Offset: 0x00309484
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			GameObject @object = base.Object;
			Cell cell = ((@object != null) ? @object.CurrentCell : null);
			if (cell == null || cell.OnWorldMap())
			{
				this.Duration = 0;
			}
			else
			{
				GameObject gameObject = GameObject.FindByID(this.Source);
				if (!GameObject.Validate(ref gameObject) || gameObject.Implantee != base.Object)
				{
					this.Duration = 0;
				}
				else if (this.Duration > 0 && this.Duration != 9999)
				{
					this.Duration--;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060087D2 RID: 34770 RVA: 0x0030B30F File Offset: 0x0030950F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			base.Register(Object, Registrar);
		}

		// Token: 0x060087D3 RID: 34771 RVA: 0x0030B32F File Offset: 0x0030952F
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyChanges();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyChanges();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003A2C RID: 14892
		public int MoveSpeedShift = -10;

		// Token: 0x04003A2D RID: 14893
		public int AgilityShift = 6;

		// Token: 0x04003A2E RID: 14894
		public string Source;
	}
}
