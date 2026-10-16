using System;

namespace XRL.World.Parts
{
	// Token: 0x0200123E RID: 4670
	[Serializable]
	public class SoundOnCreate : ISoundPart
	{
		// Token: 0x0600B9AD RID: 47533 RVA: 0x0043CBFF File Offset: 0x0043ADFF
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600B9AE RID: 47534 RVA: 0x0043CC15 File Offset: 0x0043AE15
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!this.Played)
			{
				this.Trigger();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9AF RID: 47535 RVA: 0x0043CC2C File Offset: 0x0043AE2C
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
