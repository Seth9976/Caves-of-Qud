using System;
using XRL.Core;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000F7E RID: 3966
	[Serializable]
	public class ConfuseOnEat : IPart
	{
		// Token: 0x06009DC1 RID: 40385 RVA: 0x00391FE0 File Offset: 0x003901E0
		public override bool SameAs(IPart p)
		{
			ConfuseOnEat confuseOnEat = p as ConfuseOnEat;
			return confuseOnEat.Strength == this.Strength && confuseOnEat.Duration == this.Duration && confuseOnEat.Level == this.Level && confuseOnEat.BuildupTimeout == this.BuildupTimeout;
		}

		// Token: 0x06009DC2 RID: 40386 RVA: 0x0039203D File Offset: 0x0039023D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009DC3 RID: 40387 RVA: 0x00392054 File Offset: 0x00390254
		public override bool FireEvent(Event E)
		{
			if (E.ID == "OnEat")
			{
				long turns = XRLCore.Core.Game.Turns;
				GameObject gameObjectParameter = E.GetGameObjectParameter("Eater");
				if (!gameObjectParameter.Property.ContainsKey("ConfuseOnEatTurn"))
				{
					gameObjectParameter.Property.Add("ConfuseOnEatTurn", XRLCore.Core.Game.Turns.ToString());
				}
				if (!gameObjectParameter.Property.ContainsKey("ConfuseOnEatAmount"))
				{
					gameObjectParameter.Property.Add("ConfuseOnEatAmount", "0");
				}
				long num = (long)gameObjectParameter.GetIntProperty("ConfuseOnEatTurn", 0);
				int num2 = gameObjectParameter.GetIntProperty("ConfuseOnEatAmount", 0);
				if (turns - num > (long)this.BuildupTimeout)
				{
					num2 = 0;
				}
				if (base.PerformMentalAttack((MentalAttackEvent MAE) => Confusion.Confuse(MAE, false, this.Level.RollCached(), this.Level.RollCached() + 2, true), gameObjectParameter, gameObjectParameter, null, "Confuse OnEat", this.Strength, 8, this.Duration.RollCached(), -2147483648, 0, num2 * 3))
				{
					E.RequestInterfaceExit();
				}
				gameObjectParameter.SetLongProperty("ConfuseOnEatTurn", turns);
				gameObjectParameter.SetStringProperty("ConfuseOnEatAmount", (num2 + 1).ToString(), false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004137 RID: 16695
		public string Strength = "1d8";

		// Token: 0x04004138 RID: 16696
		public string Duration = "5d3";

		// Token: 0x04004139 RID: 16697
		public string Level = "1";

		// Token: 0x0400413A RID: 16698
		public int BuildupTimeout = 15;
	}
}
