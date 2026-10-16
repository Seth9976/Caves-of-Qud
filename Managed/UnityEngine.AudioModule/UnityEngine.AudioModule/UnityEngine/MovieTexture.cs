using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000021 RID: 33
	[ExcludeFromObjectFactory]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[ExcludeFromPreset]
	[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
	public sealed class MovieTexture : Texture
	{
		// Token: 0x0600021C RID: 540 RVA: 0x00004CB7 File Offset: 0x00002EB7
		private static void FeatureRemoved()
		{
			throw new Exception("MovieTexture has been removed from Unity. Use VideoPlayer instead.");
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00004CC4 File Offset: 0x00002EC4
		private MovieTexture()
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00004CCE File Offset: 0x00002ECE
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public void Play()
		{
			MovieTexture.FeatureRemoved();
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00004CCE File Offset: 0x00002ECE
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public void Stop()
		{
			MovieTexture.FeatureRemoved();
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00004CCE File Offset: 0x00002ECE
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public void Pause()
		{
			MovieTexture.FeatureRemoved();
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00004CD8 File Offset: 0x00002ED8
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public AudioClip audioClip
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return null;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00004CF4 File Offset: 0x00002EF4
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00004CCE File Offset: 0x00002ECE
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public bool loop
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return false;
			}
			set
			{
				MovieTexture.FeatureRemoved();
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00004D10 File Offset: 0x00002F10
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public bool isPlaying
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return false;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00004D2C File Offset: 0x00002F2C
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public bool isReadyToPlay
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return false;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00004D48 File Offset: 0x00002F48
		[Obsolete("MovieTexture is removed. Use VideoPlayer instead.", true)]
		public float duration
		{
			get
			{
				MovieTexture.FeatureRemoved();
				return 1f;
			}
		}
	}
}
