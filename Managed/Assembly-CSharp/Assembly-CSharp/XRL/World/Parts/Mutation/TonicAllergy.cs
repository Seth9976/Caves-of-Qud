using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013DD RID: 5085
	[Serializable]
	public class TonicAllergy : BaseMutation
	{
		// Token: 0x0600C476 RID: 50294 RVA: 0x004784FC File Offset: 0x004766FC
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C477 RID: 50295 RVA: 0x004784FF File Offset: 0x004766FF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C478 RID: 50296 RVA: 0x00478509 File Offset: 0x00476709
		public override string GetDescription()
		{
			return "You are allergic to tonics.\n\nThe chance your mutant physiology reacts adversely to a tonic is increased to 33%.\nIf you react adversely this way to a salve or ubernostrum tonic, the adverse reaction effect is chosen randomly from among other tonic effects. You will still heal.";
		}

		// Token: 0x0600C479 RID: 50297 RVA: 0x00478510 File Offset: 0x00476710
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C47A RID: 50298 RVA: 0x00478517 File Offset: 0x00476717
		public override bool FireEvent(Event E)
		{
			return true;
		}

		// Token: 0x0600C47B RID: 50299 RVA: 0x0047851A File Offset: 0x0047671A
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C47C RID: 50300 RVA: 0x00478523 File Offset: 0x00476723
		public static void SalveOverdose(GameObject go)
		{
			if (go.IsPlayer())
			{
				TonicAllergy.getOverdoseTonic.GetRandomElement<Func<ITonicEffect>>(null)().ApplyAllergy(go);
			}
		}

		// Token: 0x04004DD6 RID: 19926
		private static Func<ITonicEffect>[] getOverdoseTonic = new Func<ITonicEffect>[]
		{
			() => new Hoarshroom_Tonic(Stat.Roll("180-220", null)),
			() => new Blaze_Tonic(Stat.Roll("41-50", null)),
			() => new Skulk_Tonic(Stat.Roll("1001-1200", null)),
			() => new HulkHoney_Tonic(Stat.Roll("41-50", null)),
			() => new Rubbergum_Tonic(Stat.Roll("41-50", null)),
			() => new ShadeOil_Tonic(Stat.Roll("41-50", null)),
			() => new SphynxSalt_Tonic(Stat.Roll("18-22", null))
		};
	}
}
