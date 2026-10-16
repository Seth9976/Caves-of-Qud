using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020011A0 RID: 4512
	[Serializable]
	public class RandomMutations : IPart
	{
		// Token: 0x0600B377 RID: 45943 RVA: 0x00417904 File Offset: 0x00415B04
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600B378 RID: 45944 RVA: 0x00417907 File Offset: 0x00415B07
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600B379 RID: 45945 RVA: 0x00417920 File Offset: 0x00415B20
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			Mutations mutations = this.ParentObject.RequirePart<Mutations>(false);
			Random random = Stat.Rnd;
			if (!this.StateKey.IsNullOrEmpty())
			{
				int num;
				if (!The.Game.IntGameState.TryGetValue(this.StateKey, out num))
				{
					num = (The.Game.IntGameState[this.StateKey] = random.Next());
				}
				random = new Random(num);
			}
			int num2 = random.Roll(this.Mental);
			int num3 = random.Roll(this.Physical);
			List<MutationEntry> list = MutationFactory.GetMutationsOfCategory("Mental", false);
			int num4 = 0;
			int num5 = 0;
			while (num4 < num2 && num5 < 100)
			{
				MutationEntry randomElement = list.GetRandomElement<MutationEntry>(random);
				if (!mutations.HasMutation(randomElement.Class) && mutations.AddMutation(randomElement, random.Roll(this.MentalLevel)) >= 0)
				{
					num4++;
				}
				num5++;
			}
			list = MutationFactory.GetMutationsOfCategory("Physical", false);
			int num6 = 0;
			int num7 = 0;
			while (num6 < num3 && num7 < 100)
			{
				MutationEntry randomElement2 = list.GetRandomElement<MutationEntry>(random);
				if (!mutations.HasMutation(randomElement2.Class) && mutations.AddMutation(randomElement2, random.Roll(this.PhysicalLevel)) >= 0)
				{
					num6++;
				}
				num7++;
			}
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x04004855 RID: 18517
		public string Physical;

		// Token: 0x04004856 RID: 18518
		public string PhysicalLevel;

		// Token: 0x04004857 RID: 18519
		public string Mental;

		// Token: 0x04004858 RID: 18520
		public string MentalLevel;

		// Token: 0x04004859 RID: 18521
		public string StateKey;
	}
}
