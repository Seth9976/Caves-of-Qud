using System;
using XRL.Core;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200144C RID: 5196
	[Serializable]
	public class Metamorphed : Effect
	{
		// Token: 0x0600CAFD RID: 51965 RVA: 0x0049CA25 File Offset: 0x0049AC25
		public Metamorphed()
		{
			this.DisplayName = "metamorphed";
		}

		// Token: 0x0600CAFE RID: 51966 RVA: 0x0049CA38 File Offset: 0x0049AC38
		public Metamorphed(GameObject OriginalBody, int Duration)
			: this()
		{
			this.OriginalBody = OriginalBody;
			this.Duration = Duration;
		}

		// Token: 0x0600CAFF RID: 51967 RVA: 0x0049CA4E File Offset: 0x0049AC4E
		public override int GetEffectType()
		{
			return 1024;
		}

		// Token: 0x0600CB00 RID: 51968 RVA: 0x0049CA55 File Offset: 0x0049AC55
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600CB01 RID: 51969 RVA: 0x0049CA58 File Offset: 0x0049AC58
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AwardedXPEvent.ID;
		}

		// Token: 0x0600CB02 RID: 51970 RVA: 0x0049CA6E File Offset: 0x0049AC6E
		public override bool HandleEvent(AwardedXPEvent E)
		{
			this.XPAwarded += E.Amount;
			return base.HandleEvent(E);
		}

		// Token: 0x0600CB03 RID: 51971 RVA: 0x0049CA8A File Offset: 0x0049AC8A
		public override string GetDetails()
		{
			return "Assuming another creature's form.";
		}

		// Token: 0x0600CB04 RID: 51972 RVA: 0x0049CA94 File Offset: 0x0049AC94
		public override bool Apply(GameObject Object)
		{
			this.AAID = base.AddMyActivatedAbility("End Metamorphosis", "CommandEndMetamorphosis", "Physical Mutations", null, "\u0002", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return true;
		}

		// Token: 0x0600CB05 RID: 51973 RVA: 0x0049CAD6 File Offset: 0x0049ACD6
		public override void Remove(GameObject Object)
		{
			base.RemoveMyActivatedAbility(ref this.AAID, null);
		}

		// Token: 0x0600CB06 RID: 51974 RVA: 0x0049CAE6 File Offset: 0x0049ACE6
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandEndMetamorphosis");
			Registrar.Register("EndAction");
		}

		// Token: 0x0600CB07 RID: 51975 RVA: 0x0049CB00 File Offset: 0x0049AD00
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandEndMetamorphosis")
			{
				Cell currentCell = base.Object.Physics.CurrentCell;
				currentCell.RemoveObject(base.Object, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
				currentCell.AddObject(this.OriginalBody, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				XRLCore.Core.Game.Player.Body = this.OriginalBody;
				Metamorphosis.TransferInventory(base.Object, this.OriginalBody, false);
				base.Object.MakeInactive();
				this.OriginalBody.MakeActive(false);
				this.OriginalBody.AwardXP(this.XPAwarded, -1, 0, int.MaxValue, null, null, null, null, null, null);
				IComponent<GameObject>.XDidY(this.OriginalBody, "revert", "to " + this.OriginalBody.its + " original form", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			}
			else if (E.ID == "EndAction" && this.Duration <= 0)
			{
				base.Object.FireEvent("CommandEndMetamorphosis");
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004F03 RID: 20227
		public Guid AAID;

		// Token: 0x04004F04 RID: 20228
		public int XPAwarded;

		// Token: 0x04004F05 RID: 20229
		public GameObject OriginalBody;
	}
}
