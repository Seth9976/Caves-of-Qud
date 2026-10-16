using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x0200109B RID: 4251
	[Serializable]
	public class Spectacles : IPart
	{
		// Token: 0x0600A923 RID: 43299 RVA: 0x003DD6B8 File Offset: 0x003DB8B8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A924 RID: 43300 RVA: 0x003DD6D6 File Offset: 0x003DB8D6
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.ApplyEffect(new Spectacles(), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A925 RID: 43301 RVA: 0x003DD6F1 File Offset: 0x003DB8F1
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.RemoveEffect<Spectacles>();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A926 RID: 43302 RVA: 0x003DD706 File Offset: 0x003DB906
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
