using System;
using XRL.Core;

namespace XRL.World.Parts
{
	// Token: 0x02000E3B RID: 3643
	[Serializable]
	public class OthoWander1 : IPart
	{
		// Token: 0x060093CD RID: 37837 RVA: 0x003536D4 File Offset: 0x003518D4
		public static bool begin()
		{
			The.Player.AddPart<PlayerOthoWander1Safeguard>(new PlayerOthoWander1Safeguard(), true, false);
			The.Player.GetPart<PlayerOthoWander1Safeguard>().startTurn = XRLCore.Core.Game.Turns;
			GameObject gameObject = The.Player.Physics.CurrentCell.ParentZone.FindObject("Otho");
			gameObject.SetIntProperty("AllowGlobalTraversal", 1, false);
			gameObject.AddPart<OthoWander1>(true, false).startTurn = XRLCore.Core.Game.Turns;
			return true;
		}

		// Token: 0x060093CE RID: 37838 RVA: 0x00353759 File Offset: 0x00351959
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIBoredEvent>.ID || ID == GetZoneSuspendabilityEvent.ID;
		}

		// Token: 0x060093CF RID: 37839 RVA: 0x00353778 File Offset: 0x00351978
		public override bool HandleEvent(AIBoredEvent E)
		{
			if (The.Game.Turns - this.startTurn > 150L && this.ParentObject.CurrentCell != null && this.ParentObject.CurrentZone.Z == 13)
			{
				this.ParentObject.RemovePart(this);
				return base.HandleEvent(E);
			}
			if (The.Game.Turns - this.startTurn < 150L)
			{
				this.ParentObject.Brain.MoveToGlobal("JoppaWorld.22.14.1.0.14", 18, 8);
			}
			else if (The.Game.Turns - this.startTurn > 150L)
			{
				The.Game.SetIntGameState("OmonporchReady", 1);
				this.ParentObject.Brain.MoveToGlobal("JoppaWorld.22.14.1.0.13", 32, 21);
			}
			else
			{
				this.ParentObject.UseEnergy(1000);
			}
			return false;
		}

		// Token: 0x060093D0 RID: 37840 RVA: 0x0035385B File Offset: 0x00351A5B
		public override bool HandleEvent(GetZoneSuspendabilityEvent E)
		{
			E.Suspendability = Suspendability.Pinned;
			return false;
		}

		// Token: 0x04003D29 RID: 15657
		public long startTurn;
	}
}
