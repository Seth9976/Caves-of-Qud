using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F80 RID: 3968
	[Serializable]
	public class FearOnEat : IPart
	{
		// Token: 0x06009DCA RID: 40394 RVA: 0x003922CC File Offset: 0x003904CC
		public override bool SameAs(IPart p)
		{
			FearOnEat fearOnEat = p as FearOnEat;
			return !(fearOnEat.Strength != this.Strength) && !(fearOnEat.Duration != this.Duration) && base.SameAs(p);
		}

		// Token: 0x06009DCB RID: 40395 RVA: 0x00392311 File Offset: 0x00390511
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("OnEat");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009DCC RID: 40396 RVA: 0x00392328 File Offset: 0x00390528
		public override bool FireEvent(Event E)
		{
			if (E.ID == "OnEat")
			{
				GameObject Eater = E.GetGameObjectParameter("Eater");
				if (base.PerformMentalAttack((MentalAttackEvent MAE) => Terrified.Attack(MAE, null, Eater.CurrentCell, false, false), Eater, Eater, null, "Terrify OnEat", this.Strength, 8, this.Duration.RollCached(), -2147483648, 0, 0) && Eater.IsPlayer())
				{
					Eater.ForfeitTurn(false);
					E.RequestInterfaceExit();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400413E RID: 16702
		public string Strength = "1d8";

		// Token: 0x0400413F RID: 16703
		public string Duration = "2d4";
	}
}
