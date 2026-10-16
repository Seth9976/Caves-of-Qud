using System;

namespace XRL.World
{
	// Token: 0x0200069A RID: 1690
	[GameEvent(Cache = MinEvent.Cache.Singleton)]
	public class BeforePlayMusicEvent : SingletonEvent<BeforePlayMusicEvent>
	{
		// Token: 0x06004827 RID: 18471 RVA: 0x0016B7A5 File Offset: 0x001699A5
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004828 RID: 18472 RVA: 0x0016B7AE File Offset: 0x001699AE
		public override void Reset()
		{
			base.Reset();
			this.Request = null;
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06004829 RID: 18473 RVA: 0x0016B7BD File Offset: 0x001699BD
		// (set) Token: 0x0600482A RID: 18474 RVA: 0x0016B7CA File Offset: 0x001699CA
		public string Track
		{
			get
			{
				return this.Request.Clip;
			}
			set
			{
				this.Request.Clip = value;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x0600482B RID: 18475 RVA: 0x0016B7D8 File Offset: 0x001699D8
		// (set) Token: 0x0600482C RID: 18476 RVA: 0x0016B7E5 File Offset: 0x001699E5
		public string Channel
		{
			get
			{
				return this.Request.Channel;
			}
			set
			{
				this.Request.Channel = value;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x0600482D RID: 18477 RVA: 0x0016B7F3 File Offset: 0x001699F3
		// (set) Token: 0x0600482E RID: 18478 RVA: 0x0016B800 File Offset: 0x00169A00
		public float VolumeAttenuation
		{
			get
			{
				return this.Request.Volume;
			}
			set
			{
				this.Request.Volume = value;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x0600482F RID: 18479 RVA: 0x0016B80E File Offset: 0x00169A0E
		// (set) Token: 0x06004830 RID: 18480 RVA: 0x0016B81B File Offset: 0x00169A1B
		public float CrossfadeDuration
		{
			get
			{
				return this.Request.CrossfadeDuration;
			}
			set
			{
				this.Request.CrossfadeDuration = value;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06004831 RID: 18481 RVA: 0x0016B829 File Offset: 0x00169A29
		// (set) Token: 0x06004832 RID: 18482 RVA: 0x0016B836 File Offset: 0x00169A36
		public bool Crossfade
		{
			get
			{
				return this.Request.Crossfade;
			}
			set
			{
				this.Request.Crossfade = value;
			}
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x0016B844 File Offset: 0x00169A44
		public static bool Check(SoundRequest Request)
		{
			XRLGame game = The.Game;
			if (game == null)
			{
				return true;
			}
			SingletonEvent<BeforePlayMusicEvent>.Instance.Request = Request;
			return game.HandleEvent(SingletonEvent<BeforePlayMusicEvent>.Instance);
		}

		// Token: 0x04002270 RID: 8816
		public SoundRequest Request;
	}
}
