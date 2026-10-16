using System;

namespace XRL.World.Parts
{
	// Token: 0x02001019 RID: 4121
	[Serializable]
	public class DiggingTool : IActivePart
	{
		// Token: 0x0600A42E RID: 42030 RVA: 0x003C0ACF File Offset: 0x003BECCF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == PooledEvent<PartSupportEvent>.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A42F RID: 42031 RVA: 0x003C0AF5 File Offset: 0x003BECF5
		public override bool HandleEvent(EquippedEvent E)
		{
			if (E.Item.IsEquippedProperly(null))
			{
				E.Actor.RequirePart<Digging>(false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A430 RID: 42032 RVA: 0x003C0B19 File Offset: 0x003BED19
		public override bool HandleEvent(UnequippedEvent E)
		{
			NeedPartSupportEvent.Send(E.Actor, "Digging", this);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A431 RID: 42033 RVA: 0x003C0B33 File Offset: 0x003BED33
		public override bool HandleEvent(PartSupportEvent E)
		{
			return (E.Skip == this || !(E.Type == "Digging") || !this.ParentObject.IsEquippedProperly(null)) && base.HandleEvent(E);
		}

		// Token: 0x0600A432 RID: 42034 RVA: 0x003C0B67 File Offset: 0x003BED67
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
