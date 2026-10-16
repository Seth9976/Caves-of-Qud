using System;

namespace XRL.World.Parts
{
	// Token: 0x0200123D RID: 4669
	[Serializable]
	public class Music : IPart
	{
		// Token: 0x0600B9A8 RID: 47528 RVA: 0x0043CB69 File Offset: 0x0043AD69
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<AfterGameLoadedEvent>.ID || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x0600B9A9 RID: 47529 RVA: 0x0043CB87 File Offset: 0x0043AD87
		public override bool HandleEvent(AfterGameLoadedEvent E)
		{
			if (this.ParentObject.CurrentZone == IComponent<GameObject>.TheGame.ZoneManager.ActiveZone)
			{
				this.TryMusic();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9AA RID: 47530 RVA: 0x0043CBB2 File Offset: 0x0043ADB2
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.TryMusic();
			return true;
		}

		// Token: 0x0600B9AB RID: 47531 RVA: 0x0043CBBB File Offset: 0x0043ADBB
		public void TryMusic()
		{
			SoundManager.PlayMusic(this.ParentObject.GetStringProperty("Track", null), "music", !this.ParentObject.HasTag("NoCrossfade"), 12f, 1f, null, true);
		}
	}
}
