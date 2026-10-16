using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001045 RID: 4165
	[Serializable]
	public class GreaseBoots : IPart
	{
		// Token: 0x0600A5CF RID: 42447 RVA: 0x003CAE3C File Offset: 0x003C903C
		public override bool SameAs(IPart p)
		{
			return !((p as GreaseBoots).BehaviorDescription != this.BehaviorDescription) && base.SameAs(p);
		}

		// Token: 0x0600A5D0 RID: 42448 RVA: 0x003CAE5F File Offset: 0x003C905F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == GetShortDescriptionEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A5D1 RID: 42449 RVA: 0x003CAE85 File Offset: 0x003C9085
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.BehaviorDescription);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5D2 RID: 42450 RVA: 0x003CAEA0 File Offset: 0x003C90A0
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.ApplyEffect(new Greased(), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5D3 RID: 42451 RVA: 0x003CAEBB File Offset: 0x003C90BB
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.RemoveEffect<Greased>();
			return base.HandleEvent(E);
		}

		// Token: 0x0400447A RID: 17530
		public string BehaviorDescription = "You may walk freely over webs.";
	}
}
