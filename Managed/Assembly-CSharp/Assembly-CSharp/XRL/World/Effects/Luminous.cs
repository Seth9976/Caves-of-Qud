using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D63 RID: 3427
	[Serializable]
	public class Luminous : Effect, ITierInitialized
	{
		// Token: 0x0600879B RID: 34715 RVA: 0x0030A7C8 File Offset: 0x003089C8
		public Luminous()
		{
		}

		// Token: 0x0600879C RID: 34716 RVA: 0x0030A7D0 File Offset: 0x003089D0
		public Luminous(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x0600879D RID: 34717 RVA: 0x0030A7DF File Offset: 0x003089DF
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(200, 700);
		}

		// Token: 0x0600879E RID: 34718 RVA: 0x0030A7F6 File Offset: 0x003089F6
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x0600879F RID: 34719 RVA: 0x0030A7F9 File Offset: 0x003089F9
		public override int GetEffectType()
		{
			return 83886144;
		}

		// Token: 0x060087A0 RID: 34720 RVA: 0x0030A800 File Offset: 0x00308A00
		public override string GetDescription()
		{
			return "{{m|phosphorescent}}";
		}

		// Token: 0x060087A1 RID: 34721 RVA: 0x0030A807 File Offset: 0x00308A07
		public override string GetDetails()
		{
			return "Radiates light in radius 3.";
		}

		// Token: 0x060087A2 RID: 34722 RVA: 0x0030A810 File Offset: 0x00308A10
		public override bool Apply(GameObject Object)
		{
			Object.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_lightManipulation_activate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			base.DidX("start", "to glow", null, null, null, null, null, false, false, null, null, false, false, false, true, false, null);
			return true;
		}

		// Token: 0x060087A3 RID: 34723 RVA: 0x0030A867 File Offset: 0x00308A67
		public override void Remove(GameObject Object)
		{
			IComponent<GameObject>.EmitMessage(Object, Object.Poss("glow", true) + " dims until it's extinguished.", ' ', false, false, false, null, null);
			base.Remove(Object);
		}

		// Token: 0x060087A4 RID: 34724 RVA: 0x0030A893 File Offset: 0x00308A93
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID;
		}

		// Token: 0x060087A5 RID: 34725 RVA: 0x0030A8A9 File Offset: 0x00308AA9
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (this.Duration > 0)
			{
				base.AddLight(2, LightLevel.Light, false);
			}
			return base.HandleEvent(E);
		}
	}
}
