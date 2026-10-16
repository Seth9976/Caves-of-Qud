using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020011FD RID: 4605
	[Serializable]
	public class OmniphaseWhileFrozen : IPart
	{
		// Token: 0x0600B729 RID: 46889 RVA: 0x0042B453 File Offset: 0x00429653
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterObjectCreatedEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600B72A RID: 46890 RVA: 0x0042B47C File Offset: 0x0042967C
		public override void Remove()
		{
			Omniphase linkedEffect = this.GetLinkedEffect();
			if (linkedEffect != null)
			{
				this.ParentObject.RemoveEffect(linkedEffect, true);
			}
			base.Remove();
		}

		// Token: 0x0600B72B RID: 46891 RVA: 0x0042B4A7 File Offset: 0x004296A7
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			if (!this.HasLinkedEffect())
			{
				this.ParentObject.ForceApplyEffect(new Omniphase(9999, base.Name, true), null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B72C RID: 46892 RVA: 0x0042B4D6 File Offset: 0x004296D6
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!this.HasLinkedEffect())
			{
				this.ParentObject.ForceApplyEffect(new Omniphase(9999, base.Name, true), null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B72D RID: 46893 RVA: 0x0042B505 File Offset: 0x00429705
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (!this.ParentObject.IsFrozen())
			{
				this.ParentObject.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B72E RID: 46894 RVA: 0x0042B527 File Offset: 0x00429727
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B72F RID: 46895 RVA: 0x0042B52C File Offset: 0x0042972C
		public Omniphase GetLinkedEffect()
		{
			int i = 0;
			int count = this.ParentObject.Effects.Count;
			while (i < count)
			{
				Omniphase omniphase = this.ParentObject.Effects[i] as Omniphase;
				if (omniphase != null && omniphase.SourceKey == base.Name)
				{
					return omniphase;
				}
				i++;
			}
			return null;
		}

		// Token: 0x0600B730 RID: 46896 RVA: 0x0042B588 File Offset: 0x00429788
		public bool HasLinkedEffect()
		{
			int i = 0;
			int count = this.ParentObject.Effects.Count;
			while (i < count)
			{
				Omniphase omniphase = this.ParentObject.Effects[i] as Omniphase;
				if (omniphase != null && omniphase.SourceKey == base.Name)
				{
					return true;
				}
				i++;
			}
			return false;
		}
	}
}
