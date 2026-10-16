using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000EE9 RID: 3817
	[Serializable]
	public class NoStandUp : IPart
	{
		// Token: 0x06009899 RID: 39065 RVA: 0x003769F4 File Offset: 0x00374BF4
		public override bool SameAs(IPart p)
		{
			return (p as NoStandUp).CanBeStoodUp == this.CanBeStoodUp && base.SameAs(p);
		}

		// Token: 0x0600989A RID: 39066 RVA: 0x00376A12 File Offset: 0x00374C12
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600989B RID: 39067 RVA: 0x00376A30 File Offset: 0x00374C30
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.CanBeStoodUp && this.ParentObject.HasEffect<Prone>())
			{
				E.AddAction("Stand Up", "stand up", "BeStoodUp", null, 's', false, 100, 0, false, false, true, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600989C RID: 39068 RVA: 0x00376A80 File Offset: 0x00374C80
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "BeStoodUp" && this.CanBeStoodUp && this.ParentObject.HasEffect<Prone>() && E.Actor.CheckFrozen(false, true, false, null))
			{
				if (this.ParentObject.CanChangeBodyPosition("Standing", false, true, false, false))
				{
					IComponent<GameObject>.XDidYToZ(E.Actor, "stand", this.ParentObject, "up", null, null, null, this.ParentObject, null, false, false, false, false, false, null, null, null, false, false, false, true, false, null);
					this.ParentObject.RemoveEffect<Prone>();
					this.ParentObject.BodyPositionChanged(null, true);
				}
				else
				{
					IComponent<GameObject>.XDidYToZ(E.Actor, "try", "to stand", this.ParentObject, "up, but cannot", null, null, null, null, E.Actor, false, false, false, false, false, null, null, null, false, false, false, true, false, null);
				}
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600989D RID: 39069 RVA: 0x00376B7A File Offset: 0x00374D7A
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanStandUp");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600989E RID: 39070 RVA: 0x00376B8F File Offset: 0x00374D8F
		public override bool FireEvent(Event E)
		{
			return !(E.ID == "CanStandUp") && base.FireEvent(E);
		}

		// Token: 0x04003F90 RID: 16272
		public bool CanBeStoodUp = true;
	}
}
