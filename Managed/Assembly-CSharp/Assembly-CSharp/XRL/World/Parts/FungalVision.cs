using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020011ED RID: 4589
	[Serializable]
	public class FungalVision : IPart
	{
		// Token: 0x0600B6BC RID: 46780 RVA: 0x0042A2E0 File Offset: 0x004284E0
		public override bool SameAs(IPart p)
		{
			FungalVision fungalVision = p as FungalVision;
			return fungalVision.originalSolid == this.originalSolid && fungalVision.originalOccluding == this.originalOccluding && base.SameAs(p);
		}

		// Token: 0x0600B6BD RID: 46781 RVA: 0x0042A31B File Offset: 0x0042851B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == CanHaveConversationEvent.ID || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600B6BE RID: 46782 RVA: 0x0042A349 File Offset: 0x00428549
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			return (FungalVisionary.VisionLevel > 0 || this.ParentObject != E.Object || this.ParentObject.IsPlayer()) && base.HandleEvent(E);
		}

		// Token: 0x0600B6BF RID: 46783 RVA: 0x0042A377 File Offset: 0x00428577
		public override bool HandleEvent(CanHaveConversationEvent E)
		{
			return FungalVisionary.VisionLevel > 0 && base.HandleEvent(E);
		}

		// Token: 0x0600B6C0 RID: 46784 RVA: 0x0042A38C File Offset: 0x0042858C
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (FungalVisionary.VisionLevel <= 0)
			{
				if (this.ParentObject.Render.Occluding && this.ParentObject.Physics.CurrentCell != null)
				{
					this.ParentObject.CurrentCell.ClearOccludeCache();
				}
				this.ParentObject.Render.Visible = false;
				this.ParentObject.Physics.Solid = false;
				this.ParentObject.Render.Occluding = false;
			}
			else
			{
				if (this.ParentObject.Render.Occluding != this.originalOccluding && this.ParentObject.Physics.CurrentCell != null)
				{
					this.ParentObject.CurrentCell.ClearOccludeCache();
				}
				this.ParentObject.Render.Visible = true;
				this.ParentObject.Physics.Solid = this.originalSolid;
				this.ParentObject.Render.Occluding = this.originalOccluding;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B6C1 RID: 46785 RVA: 0x0042A488 File Offset: 0x00428688
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.originalSolid = this.ParentObject.Physics.Solid;
			this.originalOccluding = this.ParentObject.Render.Occluding;
			if (FungalVisionary.VisionLevel <= 0)
			{
				this.ParentObject.Physics.Solid = false;
				this.ParentObject.Render.Occluding = false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B6C2 RID: 46786 RVA: 0x0042A4F2 File Offset: 0x004286F2
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B6C3 RID: 46787 RVA: 0x0042A4F5 File Offset: 0x004286F5
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforePhysicsRejectObjectEntringCell");
			Registrar.Register("CanHypersensesDetect");
			Registrar.Register("PreventSmartUse");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B6C4 RID: 46788 RVA: 0x0042A520 File Offset: 0x00428720
		public override bool FireEvent(Event E)
		{
			if (E.ID == "PreventSmartUse" || E.ID == "CanHypersensesDetect")
			{
				if (FungalVisionary.VisionLevel <= 0)
				{
					return false;
				}
			}
			else if (E.ID == "BeforePhysicsRejectObjectEntringCell" && FungalVisionary.VisionLevel <= 0)
			{
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x040049B2 RID: 18866
		public bool originalSolid;

		// Token: 0x040049B3 RID: 18867
		public bool originalOccluding;
	}
}
