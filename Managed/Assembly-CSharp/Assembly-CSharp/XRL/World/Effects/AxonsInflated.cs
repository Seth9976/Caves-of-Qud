using System;

namespace XRL.World.Effects
{
	// Token: 0x02000C35 RID: 3125
	[Serializable]
	public class AxonsInflated : Effect, ITierInitialized
	{
		// Token: 0x06007F56 RID: 32598 RVA: 0x002EFF91 File Offset: 0x002EE191
		public AxonsInflated()
		{
			this.DisplayName = "{{g|hyper-responsive}}";
		}

		// Token: 0x06007F57 RID: 32599 RVA: 0x002EFFA4 File Offset: 0x002EE1A4
		public AxonsInflated(int Duration, int Bonus, GameObject Source)
			: this()
		{
			this.Duration = Duration;
			this.Bonus = Bonus;
			this.Source = Source.ID;
		}

		// Token: 0x06007F58 RID: 32600 RVA: 0x002EFFC6 File Offset: 0x002EE1C6
		public void Initialize(int Tier)
		{
			this.Duration = 10;
			this.Bonus = 40;
		}

		// Token: 0x06007F59 RID: 32601 RVA: 0x002EFFD8 File Offset: 0x002EE1D8
		public override int GetEffectType()
		{
			return 83894272;
		}

		// Token: 0x06007F5A RID: 32602 RVA: 0x002EFFDF File Offset: 0x002EE1DF
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06007F5B RID: 32603 RVA: 0x002EFFE2 File Offset: 0x002EE1E2
		public override string GetDetails()
		{
			return this.Bonus.Signed(false) + " Quickness\nWill become sluggish soon.";
		}

		// Token: 0x06007F5C RID: 32604 RVA: 0x002EFFFC File Offset: 0x002EE1FC
		public override bool Apply(GameObject Object)
		{
			if (!Object.FireEvent(Event.New("ApplyAxonsInflated", "Effect", this)))
			{
				return false;
			}
			if (Object.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("The hurdles that separate the will and the way begin to collapse.", 'g', true);
			}
			base.StatShifter.SetStatShift("Speed", this.Bonus, false);
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_positiveVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return true;
		}

		// Token: 0x06007F5D RID: 32605 RVA: 0x002F007E File Offset: 0x002EE27E
		public override void Remove(GameObject Object)
		{
			base.StatShifter.RemoveStatShifts();
			Object.ApplyEffect(new AxonsDeflated(10, 10, this.Source), null);
		}

		// Token: 0x06007F5E RID: 32606 RVA: 0x002F00A2 File Offset: 0x002EE2A2
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID;
		}

		// Token: 0x06007F5F RID: 32607 RVA: 0x002F00B8 File Offset: 0x002EE2B8
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

		// Token: 0x06007F60 RID: 32608 RVA: 0x002F0143 File Offset: 0x002EE343
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				E.RenderEffectIndicator("\u0003", "Items/sw_axions.bmp", "&C", "C", 10, 50);
			}
			return true;
		}

		// Token: 0x040038CA RID: 14538
		public int Bonus;

		// Token: 0x040038CB RID: 14539
		public string Source;
	}
}
