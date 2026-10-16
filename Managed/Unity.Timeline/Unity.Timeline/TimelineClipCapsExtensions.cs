using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000019 RID: 25
	internal static class TimelineClipCapsExtensions
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x00006B87 File Offset: 0x00004D87
		public static bool SupportsLooping(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.Looping) > ClipCaps.None;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006B99 File Offset: 0x00004D99
		public static bool SupportsExtrapolation(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.Extrapolation) > ClipCaps.None;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006BAB File Offset: 0x00004DAB
		public static bool SupportsClipIn(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.ClipIn) > ClipCaps.None;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006BBD File Offset: 0x00004DBD
		public static bool SupportsSpeedMultiplier(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.SpeedMultiplier) > ClipCaps.None;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006BCF File Offset: 0x00004DCF
		public static bool SupportsBlending(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.Blending) > ClipCaps.None;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006BE2 File Offset: 0x00004DE2
		public static bool HasAll(this ClipCaps caps, ClipCaps flags)
		{
			return (caps & flags) == flags;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006BEA File Offset: 0x00004DEA
		public static bool HasAny(this ClipCaps caps, ClipCaps flags)
		{
			return (caps & flags) > ClipCaps.None;
		}
	}
}
