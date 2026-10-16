using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace UnityEngine.Timeline
{
	// Token: 0x0200000D RID: 13
	[TrackClipType(typeof(AnimationPlayableAsset), false)]
	[TrackBindingType(typeof(Animator))]
	[ExcludeFromPreset]
	[Serializable]
	public class AnimationTrack : TrackAsset, ILayerable
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000028EB File Offset: 0x00000AEB
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000028F3 File Offset: 0x00000AF3
		public Vector3 position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000028FC File Offset: 0x00000AFC
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002909 File Offset: 0x00000B09
		public Quaternion rotation
		{
			get
			{
				return Quaternion.Euler(this.m_EulerAngles);
			}
			set
			{
				this.m_EulerAngles = value.eulerAngles;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002918 File Offset: 0x00000B18
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002920 File Offset: 0x00000B20
		public Vector3 eulerAngles
		{
			get
			{
				return this.m_EulerAngles;
			}
			set
			{
				this.m_EulerAngles = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002929 File Offset: 0x00000B29
		// (set) Token: 0x06000048 RID: 72 RVA: 0x0000292C File Offset: 0x00000B2C
		[Obsolete("applyOffset is deprecated. Use trackOffset instead", true)]
		public bool applyOffsets
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000292E File Offset: 0x00000B2E
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002936 File Offset: 0x00000B36
		public TrackOffset trackOffset
		{
			get
			{
				return this.m_TrackOffset;
			}
			set
			{
				this.m_TrackOffset = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000293F File Offset: 0x00000B3F
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002947 File Offset: 0x00000B47
		public MatchTargetFields matchTargetFields
		{
			get
			{
				return this.m_MatchTargetFields;
			}
			set
			{
				this.m_MatchTargetFields = value & (MatchTargetFields.PositionX | MatchTargetFields.PositionY | MatchTargetFields.PositionZ | MatchTargetFields.RotationX | MatchTargetFields.RotationY | MatchTargetFields.RotationZ);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002953 File Offset: 0x00000B53
		// (set) Token: 0x0600004E RID: 78 RVA: 0x0000295B File Offset: 0x00000B5B
		public AnimationClip infiniteClip
		{
			get
			{
				return this.m_InfiniteClip;
			}
			internal set
			{
				this.m_InfiniteClip = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002964 File Offset: 0x00000B64
		// (set) Token: 0x06000050 RID: 80 RVA: 0x0000296C File Offset: 0x00000B6C
		internal bool infiniteClipRemoveOffset
		{
			get
			{
				return this.m_InfiniteClipRemoveOffset;
			}
			set
			{
				this.m_InfiniteClipRemoveOffset = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002975 File Offset: 0x00000B75
		// (set) Token: 0x06000052 RID: 82 RVA: 0x0000297D File Offset: 0x00000B7D
		public AvatarMask avatarMask
		{
			get
			{
				return this.m_AvatarMask;
			}
			set
			{
				this.m_AvatarMask = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002986 File Offset: 0x00000B86
		// (set) Token: 0x06000054 RID: 84 RVA: 0x0000298E File Offset: 0x00000B8E
		public bool applyAvatarMask
		{
			get
			{
				return this.m_ApplyAvatarMask;
			}
			set
			{
				this.m_ApplyAvatarMask = value;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002997 File Offset: 0x00000B97
		internal override bool CanCompileClips()
		{
			return !base.muted && (this.m_Clips.Count > 0 || (this.m_InfiniteClip != null && !this.m_InfiniteClip.empty));
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000029D1 File Offset: 0x00000BD1
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				yield return AnimationPlayableBinding.Create(base.name, this);
				yield break;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000029E1 File Offset: 0x00000BE1
		public bool inClipMode
		{
			get
			{
				return base.clips != null && base.clips.Length != 0;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000029F7 File Offset: 0x00000BF7
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000029FF File Offset: 0x00000BFF
		public Vector3 infiniteClipOffsetPosition
		{
			get
			{
				return this.m_InfiniteClipOffsetPosition;
			}
			set
			{
				this.m_InfiniteClipOffsetPosition = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002A08 File Offset: 0x00000C08
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002A15 File Offset: 0x00000C15
		public Quaternion infiniteClipOffsetRotation
		{
			get
			{
				return Quaternion.Euler(this.m_InfiniteClipOffsetEulerAngles);
			}
			set
			{
				this.m_InfiniteClipOffsetEulerAngles = value.eulerAngles;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002A24 File Offset: 0x00000C24
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002A2C File Offset: 0x00000C2C
		public Vector3 infiniteClipOffsetEulerAngles
		{
			get
			{
				return this.m_InfiniteClipOffsetEulerAngles;
			}
			set
			{
				this.m_InfiniteClipOffsetEulerAngles = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002A35 File Offset: 0x00000C35
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002A3D File Offset: 0x00000C3D
		internal bool infiniteClipApplyFootIK
		{
			get
			{
				return this.m_InfiniteClipApplyFootIK;
			}
			set
			{
				this.m_InfiniteClipApplyFootIK = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002A46 File Offset: 0x00000C46
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002A4E File Offset: 0x00000C4E
		internal double infiniteClipTimeOffset
		{
			get
			{
				return this.m_InfiniteClipTimeOffset;
			}
			set
			{
				this.m_InfiniteClipTimeOffset = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002A57 File Offset: 0x00000C57
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002A5F File Offset: 0x00000C5F
		public TimelineClip.ClipExtrapolation infiniteClipPreExtrapolation
		{
			get
			{
				return this.m_InfiniteClipPreExtrapolation;
			}
			set
			{
				this.m_InfiniteClipPreExtrapolation = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002A68 File Offset: 0x00000C68
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002A70 File Offset: 0x00000C70
		public TimelineClip.ClipExtrapolation infiniteClipPostExtrapolation
		{
			get
			{
				return this.m_InfiniteClipPostExtrapolation;
			}
			set
			{
				this.m_InfiniteClipPostExtrapolation = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002A79 File Offset: 0x00000C79
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002A81 File Offset: 0x00000C81
		internal AnimationPlayableAsset.LoopMode infiniteClipLoop
		{
			get
			{
				return this.mInfiniteClipLoop;
			}
			set
			{
				this.mInfiniteClipLoop = value;
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002A8A File Offset: 0x00000C8A
		[ContextMenu("Reset Offsets")]
		private void ResetOffsets()
		{
			this.m_Position = Vector3.zero;
			this.m_EulerAngles = Vector3.zero;
			this.UpdateClipOffsets();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public TimelineClip CreateClip(AnimationClip clip)
		{
			if (clip == null)
			{
				return null;
			}
			TimelineClip timelineClip = base.CreateClip<AnimationPlayableAsset>();
			this.AssignAnimationClip(timelineClip, clip);
			return timelineClip;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public void CreateInfiniteClip(string infiniteClipName)
		{
			if (this.inClipMode)
			{
				Debug.LogWarning("CreateInfiniteClip cannot create an infinite clip for an AnimationTrack that contains one or more Timeline Clips.");
				return;
			}
			if (this.m_InfiniteClip != null)
			{
				return;
			}
			this.m_InfiniteClip = TimelineCreateUtilities.CreateAnimationClipForTrack(string.IsNullOrEmpty(infiniteClipName) ? "Recorded" : infiniteClipName, this, false);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002B1C File Offset: 0x00000D1C
		public TimelineClip CreateRecordableClip(string animClipName)
		{
			AnimationClip animationClip = TimelineCreateUtilities.CreateAnimationClipForTrack(string.IsNullOrEmpty(animClipName) ? "Recorded" : animClipName, this, false);
			TimelineClip timelineClip = this.CreateClip(animationClip);
			timelineClip.displayName = animClipName;
			timelineClip.recordable = true;
			timelineClip.start = 0.0;
			timelineClip.duration = 1.0;
			AnimationPlayableAsset animationPlayableAsset = timelineClip.asset as AnimationPlayableAsset;
			if (animationPlayableAsset != null)
			{
				animationPlayableAsset.removeStartOffset = false;
			}
			return timelineClip;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002B90 File Offset: 0x00000D90
		protected override void OnCreateClip(TimelineClip clip)
		{
			TimelineClip.ClipExtrapolation clipExtrapolation = TimelineClip.ClipExtrapolation.None;
			if (!base.isSubTrack)
			{
				clipExtrapolation = TimelineClip.ClipExtrapolation.Hold;
			}
			clip.preExtrapolationMode = clipExtrapolation;
			clip.postExtrapolationMode = clipExtrapolation;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002BB7 File Offset: 0x00000DB7
		protected internal override int CalculateItemsHash()
		{
			return TrackAsset.GetAnimationClipHash(this.m_InfiniteClip).CombineHash(base.CalculateItemsHash());
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002BCF File Offset: 0x00000DCF
		internal void UpdateClipOffsets()
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002BD4 File Offset: 0x00000DD4
		private Playable CompileTrackPlayable(PlayableGraph graph, AnimationTrack track, GameObject go, IntervalTree<RuntimeElement> tree, AppliedOffsetMode mode)
		{
			AnimationMixerPlayable animationMixerPlayable = AnimationMixerPlayable.Create(graph, track.clips.Length);
			for (int i = 0; i < track.clips.Length; i++)
			{
				TimelineClip timelineClip = track.clips[i];
				PlayableAsset playableAsset = timelineClip.asset as PlayableAsset;
				if (!(playableAsset == null))
				{
					AnimationPlayableAsset animationPlayableAsset = playableAsset as AnimationPlayableAsset;
					if (animationPlayableAsset != null)
					{
						animationPlayableAsset.appliedOffsetMode = mode;
					}
					Playable playable = playableAsset.CreatePlayable(graph, go);
					if (playable.IsValid<Playable>())
					{
						RuntimeClip runtimeClip = new RuntimeClip(timelineClip, playable, animationMixerPlayable);
						tree.Add(runtimeClip);
						graph.Connect<Playable, AnimationMixerPlayable>(playable, 0, animationMixerPlayable, i);
						animationMixerPlayable.SetInputWeight(i, 0f);
					}
				}
			}
			if (!track.AnimatesRootTransform())
			{
				return animationMixerPlayable;
			}
			return this.ApplyTrackOffset(graph, animationMixerPlayable, go, mode);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002CA6 File Offset: 0x00000EA6
		Playable ILayerable.CreateLayerMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return Playable.Null;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002CB0 File Offset: 0x00000EB0
		internal override Playable CreateMixerPlayableGraph(PlayableGraph graph, GameObject go, IntervalTree<RuntimeElement> tree)
		{
			if (base.isSubTrack)
			{
				throw new InvalidOperationException("Nested animation tracks should never be asked to create a graph directly");
			}
			List<AnimationTrack> list = new List<AnimationTrack>();
			if (this.CanCompileClips())
			{
				list.Add(this);
			}
			Transform genericRootNode = this.GetGenericRootNode(go);
			bool flag = this.AnimatesRootTransform();
			bool flag2 = flag && !this.IsRootTransformDisabledByMask(go, genericRootNode);
			foreach (TrackAsset trackAsset in base.GetChildTracks())
			{
				AnimationTrack animationTrack = trackAsset as AnimationTrack;
				if (animationTrack != null && animationTrack.CanCompileClips())
				{
					bool flag3 = animationTrack.AnimatesRootTransform();
					flag |= animationTrack.AnimatesRootTransform();
					flag2 |= flag3 && !animationTrack.IsRootTransformDisabledByMask(go, genericRootNode);
					list.Add(animationTrack);
				}
			}
			AppliedOffsetMode offsetMode = this.GetOffsetMode(go, flag2);
			int defaultBlendCount = this.GetDefaultBlendCount();
			AnimationLayerMixerPlayable animationLayerMixerPlayable = AnimationTrack.CreateGroupMixer(graph, go, list.Count + defaultBlendCount);
			for (int i = 0; i < list.Count; i++)
			{
				int num = i + defaultBlendCount;
				AppliedOffsetMode appliedOffsetMode = offsetMode;
				if (offsetMode != AppliedOffsetMode.NoRootTransform && list[i].IsRootTransformDisabledByMask(go, genericRootNode))
				{
					appliedOffsetMode = AppliedOffsetMode.NoRootTransform;
				}
				Playable playable = (list[i].inClipMode ? this.CompileTrackPlayable(graph, list[i], go, tree, appliedOffsetMode) : list[i].CreateInfiniteTrackPlayable(graph, go, tree, appliedOffsetMode));
				graph.Connect<Playable, AnimationLayerMixerPlayable>(playable, 0, animationLayerMixerPlayable, num);
				animationLayerMixerPlayable.SetInputWeight(num, (float)(list[i].inClipMode ? 0 : 1));
				if (list[i].applyAvatarMask && list[i].avatarMask != null)
				{
					animationLayerMixerPlayable.SetLayerMaskFromAvatarMask((uint)num, list[i].avatarMask);
				}
			}
			bool flag4 = this.RequiresMotionXPlayable(offsetMode, go);
			flag4 |= defaultBlendCount > 0 && this.RequiresMotionXPlayable(this.GetOffsetMode(go, flag), go);
			this.AttachDefaultBlend(graph, animationLayerMixerPlayable, flag4);
			Playable playable2 = animationLayerMixerPlayable;
			if (flag4)
			{
				AnimationMotionXToDeltaPlayable animationMotionXToDeltaPlayable = AnimationMotionXToDeltaPlayable.Create(graph);
				graph.Connect<Playable, AnimationMotionXToDeltaPlayable>(playable2, 0, animationMotionXToDeltaPlayable, 0);
				animationMotionXToDeltaPlayable.SetInputWeight(0, 1f);
				animationMotionXToDeltaPlayable.SetAbsoluteMotion(AnimationTrack.UsesAbsoluteMotion(offsetMode));
				playable2 = animationMotionXToDeltaPlayable;
			}
			return playable2;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002F0C File Offset: 0x0000110C
		private int GetDefaultBlendCount()
		{
			return 0;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002F0F File Offset: 0x0000110F
		private void AttachDefaultBlend(PlayableGraph graph, AnimationLayerMixerPlayable mixer, bool requireOffset)
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002F14 File Offset: 0x00001114
		private Playable AttachOffsetPlayable(PlayableGraph graph, Playable playable, Vector3 pos, Quaternion rot)
		{
			AnimationOffsetPlayable animationOffsetPlayable = AnimationOffsetPlayable.Create(graph, pos, rot, 1);
			animationOffsetPlayable.SetInputWeight(0, 1f);
			graph.Connect<Playable, AnimationOffsetPlayable>(playable, 0, animationOffsetPlayable, 0);
			return animationOffsetPlayable;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002F4C File Offset: 0x0000114C
		private bool RequiresMotionXPlayable(AppliedOffsetMode mode, GameObject gameObject)
		{
			if (mode == AppliedOffsetMode.NoRootTransform)
			{
				return false;
			}
			if (mode == AppliedOffsetMode.SceneOffsetLegacy)
			{
				Animator binding = this.GetBinding((gameObject != null) ? gameObject.GetComponent<PlayableDirector>() : null);
				return binding != null && binding.hasRootMotion;
			}
			return true;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002F8E File Offset: 0x0000118E
		private static bool UsesAbsoluteMotion(AppliedOffsetMode mode)
		{
			return mode != AppliedOffsetMode.SceneOffset && mode != AppliedOffsetMode.SceneOffsetLegacy;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002FA0 File Offset: 0x000011A0
		private bool HasController(GameObject gameObject)
		{
			Animator binding = this.GetBinding((gameObject != null) ? gameObject.GetComponent<PlayableDirector>() : null);
			return binding != null && binding.runtimeAnimatorController != null;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002FE0 File Offset: 0x000011E0
		internal Animator GetBinding(PlayableDirector director)
		{
			if (director == null)
			{
				return null;
			}
			Object @object = this;
			if (base.isSubTrack)
			{
				@object = base.parent;
			}
			Object object2 = null;
			if (director != null)
			{
				object2 = director.GetGenericBinding(@object);
			}
			Animator animator = null;
			if (object2 != null)
			{
				animator = object2 as Animator;
				GameObject gameObject = object2 as GameObject;
				if (animator == null && gameObject != null)
				{
					animator = gameObject.GetComponent<Animator>();
				}
			}
			return animator;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000304F File Offset: 0x0000124F
		private static AnimationLayerMixerPlayable CreateGroupMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			return AnimationLayerMixerPlayable.Create(graph, inputCount, false);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000305C File Offset: 0x0000125C
		private Playable CreateInfiniteTrackPlayable(PlayableGraph graph, GameObject go, IntervalTree<RuntimeElement> tree, AppliedOffsetMode mode)
		{
			if (this.m_InfiniteClip == null)
			{
				return Playable.Null;
			}
			AnimationMixerPlayable animationMixerPlayable = AnimationMixerPlayable.Create(graph, 1);
			Playable playable = AnimationPlayableAsset.CreatePlayable(graph, this.m_InfiniteClip, this.m_InfiniteClipOffsetPosition, this.m_InfiniteClipOffsetEulerAngles, false, mode, this.infiniteClipApplyFootIK, AnimationPlayableAsset.LoopMode.Off);
			if (playable.IsValid<Playable>())
			{
				tree.Add(new InfiniteRuntimeClip(playable));
				graph.Connect<Playable, AnimationMixerPlayable>(playable, 0, animationMixerPlayable, 0);
				animationMixerPlayable.SetInputWeight(0, 1f);
			}
			if (!this.AnimatesRootTransform())
			{
				return animationMixerPlayable;
			}
			return (base.isSubTrack ? ((AnimationTrack)base.parent) : this).ApplyTrackOffset(graph, animationMixerPlayable, go, mode);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003108 File Offset: 0x00001308
		private Playable ApplyTrackOffset(PlayableGraph graph, Playable root, GameObject go, AppliedOffsetMode mode)
		{
			if (mode == AppliedOffsetMode.SceneOffsetLegacy || mode == AppliedOffsetMode.SceneOffset || mode == AppliedOffsetMode.NoRootTransform)
			{
				return root;
			}
			Vector3 position = this.position;
			Quaternion rotation = this.rotation;
			AnimationOffsetPlayable animationOffsetPlayable = AnimationOffsetPlayable.Create(graph, position, rotation, 1);
			graph.Connect<Playable, AnimationOffsetPlayable>(root, 0, animationOffsetPlayable, 0);
			animationOffsetPlayable.SetInputWeight(0, 1f);
			return animationOffsetPlayable;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000315B File Offset: 0x0000135B
		internal override void GetEvaluationTime(out double outStart, out double outDuration)
		{
			if (this.inClipMode)
			{
				base.GetEvaluationTime(out outStart, out outDuration);
				return;
			}
			outStart = 0.0;
			outDuration = TimelineClip.kMaxTimeValue;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003180 File Offset: 0x00001380
		internal override void GetSequenceTime(out double outStart, out double outDuration)
		{
			if (this.inClipMode)
			{
				base.GetSequenceTime(out outStart, out outDuration);
				return;
			}
			outStart = 0.0;
			outDuration = Math.Max(base.GetNotificationDuration(), TimeUtility.GetAnimationClipLength(this.m_InfiniteClip));
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000031B8 File Offset: 0x000013B8
		private void AssignAnimationClip(TimelineClip clip, AnimationClip animClip)
		{
			if (clip == null || animClip == null)
			{
				return;
			}
			if (animClip.legacy)
			{
				throw new InvalidOperationException("Legacy Animation Clips are not supported");
			}
			AnimationPlayableAsset animationPlayableAsset = clip.asset as AnimationPlayableAsset;
			if (animationPlayableAsset != null)
			{
				animationPlayableAsset.clip = animClip;
				animationPlayableAsset.name = animClip.name;
				double duration = animationPlayableAsset.duration;
				if (!double.IsInfinity(duration) && duration >= TimelineClip.kMinDuration && duration < TimelineClip.kMaxTimeValue)
				{
					clip.duration = duration;
				}
			}
			clip.displayName = animClip.name;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000323F File Offset: 0x0000143F
		public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003244 File Offset: 0x00001444
		private void GetAnimationClips(List<AnimationClip> animClips)
		{
			TimelineClip[] clips = base.clips;
			for (int i = 0; i < clips.Length; i++)
			{
				AnimationPlayableAsset animationPlayableAsset = clips[i].asset as AnimationPlayableAsset;
				if (animationPlayableAsset != null && animationPlayableAsset.clip != null)
				{
					animClips.Add(animationPlayableAsset.clip);
				}
			}
			if (this.m_InfiniteClip != null)
			{
				animClips.Add(this.m_InfiniteClip);
			}
			foreach (TrackAsset trackAsset in base.GetChildTracks())
			{
				AnimationTrack animationTrack = trackAsset as AnimationTrack;
				if (animationTrack != null)
				{
					animationTrack.GetAnimationClips(animClips);
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003304 File Offset: 0x00001504
		private AppliedOffsetMode GetOffsetMode(GameObject go, bool animatesRootTransform)
		{
			if (!animatesRootTransform)
			{
				return AppliedOffsetMode.NoRootTransform;
			}
			if (this.m_TrackOffset == TrackOffset.ApplyTransformOffsets)
			{
				return AppliedOffsetMode.TransformOffset;
			}
			if (this.m_TrackOffset == TrackOffset.ApplySceneOffsets)
			{
				if (!Application.isPlaying)
				{
					return AppliedOffsetMode.SceneOffsetEditor;
				}
				return AppliedOffsetMode.SceneOffset;
			}
			else
			{
				if (!this.HasController(go))
				{
					return AppliedOffsetMode.TransformOffsetLegacy;
				}
				if (!Application.isPlaying)
				{
					return AppliedOffsetMode.SceneOffsetLegacyEditor;
				}
				return AppliedOffsetMode.SceneOffsetLegacy;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003340 File Offset: 0x00001540
		private bool IsRootTransformDisabledByMask(GameObject gameObject, Transform genericRootNode)
		{
			if (this.avatarMask == null || !this.applyAvatarMask)
			{
				return false;
			}
			Animator binding = this.GetBinding((gameObject != null) ? gameObject.GetComponent<PlayableDirector>() : null);
			if (binding == null)
			{
				return false;
			}
			if (binding.isHuman)
			{
				return !this.avatarMask.GetHumanoidBodyPartActive(AvatarMaskBodyPart.Root);
			}
			if (this.avatarMask.transformCount == 0)
			{
				return false;
			}
			if (genericRootNode == null)
			{
				return string.IsNullOrEmpty(this.avatarMask.GetTransformPath(0)) && !this.avatarMask.GetTransformActive(0);
			}
			for (int i = 0; i < this.avatarMask.transformCount; i++)
			{
				if (genericRootNode == binding.transform.Find(this.avatarMask.GetTransformPath(i)))
				{
					return !this.avatarMask.GetTransformActive(i);
				}
			}
			return false;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003424 File Offset: 0x00001624
		private Transform GetGenericRootNode(GameObject gameObject)
		{
			Animator binding = this.GetBinding((gameObject != null) ? gameObject.GetComponent<PlayableDirector>() : null);
			if (binding == null)
			{
				return null;
			}
			if (binding.isHuman)
			{
				return null;
			}
			if (binding.avatar == null)
			{
				return null;
			}
			string rootMotionBoneName = binding.avatar.humanDescription.m_RootMotionBoneName;
			if (rootMotionBoneName == binding.name || string.IsNullOrEmpty(rootMotionBoneName))
			{
				return null;
			}
			return AnimationTrack.FindInHierarchyBreadthFirst(binding.transform, rootMotionBoneName);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000034A4 File Offset: 0x000016A4
		internal bool AnimatesRootTransform()
		{
			if (AnimationPlayableAsset.HasRootTransforms(this.m_InfiniteClip))
			{
				return true;
			}
			foreach (TimelineClip timelineClip in base.GetClips())
			{
				AnimationPlayableAsset animationPlayableAsset = timelineClip.asset as AnimationPlayableAsset;
				if (animationPlayableAsset != null && animationPlayableAsset.hasRootTransforms)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000351C File Offset: 0x0000171C
		private static Transform FindInHierarchyBreadthFirst(Transform t, string name)
		{
			AnimationTrack.s_CachedQueue.Clear();
			AnimationTrack.s_CachedQueue.Enqueue(t);
			while (AnimationTrack.s_CachedQueue.Count > 0)
			{
				Transform transform = AnimationTrack.s_CachedQueue.Dequeue();
				if (transform.name == name)
				{
					return transform;
				}
				for (int i = 0; i < transform.childCount; i++)
				{
					AnimationTrack.s_CachedQueue.Enqueue(transform.GetChild(i));
				}
			}
			return null;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000358B File Offset: 0x0000178B
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00003593 File Offset: 0x00001793
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("openClipOffsetPosition has been deprecated. Use infiniteClipOffsetPosition instead. (UnityUpgradable) -> infiniteClipOffsetPosition", true)]
		public Vector3 openClipOffsetPosition
		{
			get
			{
				return this.infiniteClipOffsetPosition;
			}
			set
			{
				this.infiniteClipOffsetPosition = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000359C File Offset: 0x0000179C
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000035A4 File Offset: 0x000017A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("openClipOffsetRotation has been deprecated. Use infiniteClipOffsetRotation instead. (UnityUpgradable) -> infiniteClipOffsetRotation", true)]
		public Quaternion openClipOffsetRotation
		{
			get
			{
				return this.infiniteClipOffsetRotation;
			}
			set
			{
				this.infiniteClipOffsetRotation = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000035AD File Offset: 0x000017AD
		// (set) Token: 0x0600008B RID: 139 RVA: 0x000035B5 File Offset: 0x000017B5
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("openClipOffsetEulerAngles has been deprecated. Use infiniteClipOffsetEulerAngles instead. (UnityUpgradable) -> infiniteClipOffsetEulerAngles", true)]
		public Vector3 openClipOffsetEulerAngles
		{
			get
			{
				return this.infiniteClipOffsetEulerAngles;
			}
			set
			{
				this.infiniteClipOffsetEulerAngles = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000035BE File Offset: 0x000017BE
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000035C6 File Offset: 0x000017C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("openClipPreExtrapolation has been deprecated. Use infiniteClipPreExtrapolation instead. (UnityUpgradable) -> infiniteClipPreExtrapolation", true)]
		public TimelineClip.ClipExtrapolation openClipPreExtrapolation
		{
			get
			{
				return this.infiniteClipPreExtrapolation;
			}
			set
			{
				this.infiniteClipPreExtrapolation = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000035CF File Offset: 0x000017CF
		// (set) Token: 0x0600008F RID: 143 RVA: 0x000035D7 File Offset: 0x000017D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("openClipPostExtrapolation has been deprecated. Use infiniteClipPostExtrapolation instead. (UnityUpgradable) -> infiniteClipPostExtrapolation", true)]
		public TimelineClip.ClipExtrapolation openClipPostExtrapolation
		{
			get
			{
				return this.infiniteClipPostExtrapolation;
			}
			set
			{
				this.infiniteClipPostExtrapolation = value;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000035E0 File Offset: 0x000017E0
		internal override void OnUpgradeFromVersion(int oldVersion)
		{
			if (oldVersion < 1)
			{
				AnimationTrack.AnimationTrackUpgrade.ConvertRotationsToEuler(this);
			}
			if (oldVersion < 2)
			{
				AnimationTrack.AnimationTrackUpgrade.ConvertRootMotion(this);
			}
			if (oldVersion < 3)
			{
				AnimationTrack.AnimationTrackUpgrade.ConvertInfiniteTrack(this);
			}
		}

		// Token: 0x0400002F RID: 47
		private const string k_DefaultInfiniteClipName = "Recorded";

		// Token: 0x04000030 RID: 48
		private const string k_DefaultRecordableClipName = "Recorded";

		// Token: 0x04000031 RID: 49
		[SerializeField]
		[FormerlySerializedAs("m_OpenClipPreExtrapolation")]
		private TimelineClip.ClipExtrapolation m_InfiniteClipPreExtrapolation;

		// Token: 0x04000032 RID: 50
		[SerializeField]
		[FormerlySerializedAs("m_OpenClipPostExtrapolation")]
		private TimelineClip.ClipExtrapolation m_InfiniteClipPostExtrapolation;

		// Token: 0x04000033 RID: 51
		[SerializeField]
		[FormerlySerializedAs("m_OpenClipOffsetPosition")]
		private Vector3 m_InfiniteClipOffsetPosition = Vector3.zero;

		// Token: 0x04000034 RID: 52
		[SerializeField]
		[FormerlySerializedAs("m_OpenClipOffsetEulerAngles")]
		private Vector3 m_InfiniteClipOffsetEulerAngles = Vector3.zero;

		// Token: 0x04000035 RID: 53
		[SerializeField]
		[FormerlySerializedAs("m_OpenClipTimeOffset")]
		private double m_InfiniteClipTimeOffset;

		// Token: 0x04000036 RID: 54
		[SerializeField]
		[FormerlySerializedAs("m_OpenClipRemoveOffset")]
		private bool m_InfiniteClipRemoveOffset;

		// Token: 0x04000037 RID: 55
		[SerializeField]
		private bool m_InfiniteClipApplyFootIK = true;

		// Token: 0x04000038 RID: 56
		[SerializeField]
		[HideInInspector]
		private AnimationPlayableAsset.LoopMode mInfiniteClipLoop;

		// Token: 0x04000039 RID: 57
		[SerializeField]
		private MatchTargetFields m_MatchTargetFields = MatchTargetFields.PositionX | MatchTargetFields.PositionY | MatchTargetFields.PositionZ | MatchTargetFields.RotationX | MatchTargetFields.RotationY | MatchTargetFields.RotationZ;

		// Token: 0x0400003A RID: 58
		[SerializeField]
		private Vector3 m_Position = Vector3.zero;

		// Token: 0x0400003B RID: 59
		[SerializeField]
		private Vector3 m_EulerAngles = Vector3.zero;

		// Token: 0x0400003C RID: 60
		[SerializeField]
		private AvatarMask m_AvatarMask;

		// Token: 0x0400003D RID: 61
		[SerializeField]
		private bool m_ApplyAvatarMask = true;

		// Token: 0x0400003E RID: 62
		[SerializeField]
		private TrackOffset m_TrackOffset;

		// Token: 0x0400003F RID: 63
		[SerializeField]
		[HideInInspector]
		private AnimationClip m_InfiniteClip;

		// Token: 0x04000040 RID: 64
		private static readonly Queue<Transform> s_CachedQueue = new Queue<Transform>(100);

		// Token: 0x04000041 RID: 65
		[SerializeField]
		[Obsolete("Use m_InfiniteClipOffsetEulerAngles Instead", false)]
		[HideInInspector]
		private Quaternion m_OpenClipOffsetRotation = Quaternion.identity;

		// Token: 0x04000042 RID: 66
		[SerializeField]
		[Obsolete("Use m_RotationEuler Instead", false)]
		[HideInInspector]
		private Quaternion m_Rotation = Quaternion.identity;

		// Token: 0x04000043 RID: 67
		[SerializeField]
		[Obsolete("Use m_RootTransformOffsetMode", false)]
		[HideInInspector]
		private bool m_ApplyOffsets;

		// Token: 0x02000063 RID: 99
		private static class AnimationTrackUpgrade
		{
			// Token: 0x06000339 RID: 825 RVA: 0x0000BB7F File Offset: 0x00009D7F
			public static void ConvertRotationsToEuler(AnimationTrack track)
			{
				track.m_EulerAngles = track.m_Rotation.eulerAngles;
				track.m_InfiniteClipOffsetEulerAngles = track.m_OpenClipOffsetRotation.eulerAngles;
			}

			// Token: 0x0600033A RID: 826 RVA: 0x0000BBA3 File Offset: 0x00009DA3
			public static void ConvertRootMotion(AnimationTrack track)
			{
				track.m_TrackOffset = TrackOffset.Auto;
				if (!track.m_ApplyOffsets)
				{
					track.m_Position = Vector3.zero;
					track.m_EulerAngles = Vector3.zero;
				}
			}

			// Token: 0x0600033B RID: 827 RVA: 0x0000BBCA File Offset: 0x00009DCA
			public static void ConvertInfiniteTrack(AnimationTrack track)
			{
				track.m_InfiniteClip = track.m_AnimClip;
				track.m_AnimClip = null;
			}
		}
	}
}
