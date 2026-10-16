using System;

namespace XRL.World.Effects
{
	// Token: 0x02000DB8 RID: 3512
	[Serializable]
	public class Inspired : Effect, ITierInitialized
	{
		// Token: 0x06008C1F RID: 35871 RVA: 0x0031B805 File Offset: 0x00319A05
		public Inspired()
		{
			this.Duration = 1;
			this.DisplayName = "{{M|inspired}}";
		}

		// Token: 0x06008C20 RID: 35872 RVA: 0x0031B81F File Offset: 0x00319A1F
		public Inspired(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008C21 RID: 35873 RVA: 0x0031B82E File Offset: 0x00319A2E
		public override int GetEffectType()
		{
			return 2;
		}

		// Token: 0x06008C22 RID: 35874 RVA: 0x0031B831 File Offset: 0x00319A31
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008C23 RID: 35875 RVA: 0x0031B834 File Offset: 0x00319A34
		public override bool Apply(GameObject Object)
		{
			return !Object.HasEffect(typeof(Inspired)) && Object.ShowSuccess("You swell with inspiration to cook a mouthwatering meal.", true);
		}

		// Token: 0x06008C24 RID: 35876 RVA: 0x0031B856 File Offset: 0x00319A56
		public override string GetDetails()
		{
			return "The next time you cook a meal by choosing ingredients, you get a choice of three dynamically-generated effects to apply. You create a recipe for the chosen effect.";
		}
	}
}
