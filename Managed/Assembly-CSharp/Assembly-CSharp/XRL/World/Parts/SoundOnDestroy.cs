using System;

namespace XRL.World.Parts
{
	// Token: 0x0200123F RID: 4671
	[Serializable]
	public class SoundOnDestroy : ISoundPart
	{
		// Token: 0x0600B9B1 RID: 47537 RVA: 0x0043CC37 File Offset: 0x0043AE37
		public override bool WantEvent(int ID, int cascade)
		{
			return ID == BeforeDestroyObjectEvent.ID || base.WantEvent(ID, cascade);
		}

		// Token: 0x0600B9B2 RID: 47538 RVA: 0x0043CC4B File Offset: 0x0043AE4B
		public override bool HandleEvent(BeforeDestroyObjectEvent E)
		{
			this.Trigger();
			return base.HandleEvent(E);
		}
	}
}
