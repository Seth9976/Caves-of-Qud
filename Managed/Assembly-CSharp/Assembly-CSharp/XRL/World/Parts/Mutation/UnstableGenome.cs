using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200145B RID: 5211
	[Serializable]
	public class UnstableGenome : BaseMutation
	{
		// Token: 0x0600CC30 RID: 52272 RVA: 0x004A32FA File Offset: 0x004A14FA
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600CC31 RID: 52273 RVA: 0x004A32FD File Offset: 0x004A14FD
		public override bool ShouldShowLevel()
		{
			return true;
		}

		// Token: 0x0600CC32 RID: 52274 RVA: 0x004A3300 File Offset: 0x004A1500
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600CC33 RID: 52275 RVA: 0x004A3303 File Offset: 0x004A1503
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeLevelGainedEvent>.ID;
		}

		// Token: 0x0600CC34 RID: 52276 RVA: 0x004A331C File Offset: 0x004A151C
		public override bool HandleEvent(BeforeLevelGainedEvent E)
		{
			if (E.Actor == this.ParentObject && E.Actor.IsPlayer())
			{
				if (E.Pass == 1)
				{
					if (Stat.Random(1, 100) <= 33)
					{
						E.Handlers.Enqueue(this);
					}
				}
				else
				{
					StatusScreen.BuyRandomMutation(this.ParentObject);
					if (base.Level == 1)
					{
						this.ParentObject.GetPart<Mutations>().RemoveMutation(this, true);
					}
					else
					{
						int level = base.Level;
						base.Level = level - 1;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC35 RID: 52277 RVA: 0x004A33A7 File Offset: 0x004A15A7
		public override string GetDescription()
		{
			return "You gain one extra mutation each time you buy this, but the mutations don't manifest right away.\nWhenever you gain a level, there's a 33% chance that your genome destabilizes and you get to choose from 3 random mutations.";
		}

		// Token: 0x0600CC36 RID: 52278 RVA: 0x004A33AE File Offset: 0x004A15AE
		public override string GetLevelText(int Level)
		{
			return "";
		}
	}
}
