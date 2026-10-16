using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E3A RID: 3642
	[Serializable]
	public class PlayerOthoWander1Safeguard : IPart
	{
		// Token: 0x060093CA RID: 37834 RVA: 0x0035365C File Offset: 0x0035185C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x060093CB RID: 37835 RVA: 0x00353674 File Offset: 0x00351874
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction" && The.Game.Turns - this.startTurn > 150L)
			{
				The.Game.SetIntGameState("OmonporchReady", 1);
				this.ParentObject.RemovePart(this);
			}
			return true;
		}

		// Token: 0x04003D28 RID: 15656
		public long startTurn;
	}
}
