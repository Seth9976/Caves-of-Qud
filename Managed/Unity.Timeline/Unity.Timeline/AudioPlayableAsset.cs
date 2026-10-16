using System;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000016 RID: 22
	[Serializable]
	public class AudioPlayableAsset : PlayableAsset, ITimelineClipAsset
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00006817 File Offset: 0x00004A17
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000681F File Offset: 0x00004A1F
		internal float bufferingTime
		{
			get
			{
				return this.m_bufferingTime;
			}
			set
			{
				this.m_bufferingTime = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00006828 File Offset: 0x00004A28
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00006830 File Offset: 0x00004A30
		public AudioClip clip
		{
			get
			{
				return this.m_Clip;
			}
			set
			{
				this.m_Clip = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00006839 File Offset: 0x00004A39
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00006841 File Offset: 0x00004A41
		public bool loop
		{
			get
			{
				return this.m_Loop;
			}
			set
			{
				this.m_Loop = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000684A File Offset: 0x00004A4A
		public override double duration
		{
			get
			{
				if (this.m_Clip == null)
				{
					return base.duration;
				}
				return (double)this.m_Clip.samples / (double)this.m_Clip.frequency;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000687A File Offset: 0x00004A7A
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				yield return AudioPlayableBinding.Create(base.name, this);
				yield break;
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000688C File Offset: 0x00004A8C
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			if (this.m_Clip == null)
			{
				return Playable.Null;
			}
			AudioClipPlayable audioClipPlayable = AudioClipPlayable.Create(graph, this.m_Clip, this.m_Loop);
			audioClipPlayable.GetHandle().SetScriptInstance(this.m_ClipProperties.Clone());
			return audioClipPlayable;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600019A RID: 410 RVA: 0x000068E0 File Offset: 0x00004AE0
		public ClipCaps clipCaps
		{
			get
			{
				return ClipCaps.ClipIn | ClipCaps.SpeedMultiplier | ClipCaps.Blending | (this.m_Loop ? ClipCaps.Looping : ClipCaps.None);
			}
		}

		// Token: 0x0400008B RID: 139
		[SerializeField]
		private AudioClip m_Clip;

		// Token: 0x0400008C RID: 140
		[SerializeField]
		private bool m_Loop;

		// Token: 0x0400008D RID: 141
		[SerializeField]
		[HideInInspector]
		private float m_bufferingTime = 0.1f;

		// Token: 0x0400008E RID: 142
		[SerializeField]
		private AudioClipProperties m_ClipProperties = new AudioClipProperties();
	}
}
