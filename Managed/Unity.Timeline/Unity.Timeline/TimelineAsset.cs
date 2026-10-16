using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000010 RID: 16
	[ExcludeFromPreset]
	[Serializable]
	public class TimelineAsset : PlayableAsset, ISerializationCallbackReceiver, ITimelineClipAsset, IPropertyPreview
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00004458 File Offset: 0x00002658
		private void UpgradeToLatestVersion()
		{
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x0000445A File Offset: 0x0000265A
		public TimelineAsset.EditorSettings editorSettings
		{
			get
			{
				return this.m_EditorSettings;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004464 File Offset: 0x00002664
		public override double duration
		{
			get
			{
				if (this.m_DurationMode != TimelineAsset.DurationMode.BasedOnClips)
				{
					return this.m_FixedDuration;
				}
				DiscreteTime discreteTime = this.CalculateItemsDuration();
				if (discreteTime <= 0)
				{
					return 0.0;
				}
				return (double)discreteTime.OneTickBefore();
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000044AC File Offset: 0x000026AC
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x000044EA File Offset: 0x000026EA
		public double fixedDuration
		{
			get
			{
				DiscreteTime discreteTime = (DiscreteTime)this.m_FixedDuration;
				if (discreteTime <= 0)
				{
					return 0.0;
				}
				return (double)discreteTime.OneTickBefore();
			}
			set
			{
				this.m_FixedDuration = Math.Max(0.0, value);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004501 File Offset: 0x00002701
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00004509 File Offset: 0x00002709
		public TimelineAsset.DurationMode durationMode
		{
			get
			{
				return this.m_DurationMode;
			}
			set
			{
				this.m_DurationMode = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004512 File Offset: 0x00002712
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				foreach (TrackAsset trackAsset in this.GetOutputTracks())
				{
					foreach (PlayableBinding playableBinding in trackAsset.outputs)
					{
						yield return playableBinding;
					}
					IEnumerator<PlayableBinding> enumerator2 = null;
				}
				IEnumerator<TrackAsset> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004524 File Offset: 0x00002724
		public ClipCaps clipCaps
		{
			get
			{
				ClipCaps clipCaps = ClipCaps.All;
				foreach (TrackAsset trackAsset in this.GetRootTracks())
				{
					foreach (TimelineClip timelineClip in trackAsset.clips)
					{
						clipCaps &= timelineClip.clipCaps;
					}
				}
				return clipCaps;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004590 File Offset: 0x00002790
		public int outputTrackCount
		{
			get
			{
				this.UpdateOutputTrackCache();
				return this.m_CacheOutputTracks.Length;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000045A0 File Offset: 0x000027A0
		public int rootTrackCount
		{
			get
			{
				this.UpdateRootTrackCache();
				return this.m_CacheRootTracks.Count;
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000045B3 File Offset: 0x000027B3
		private void OnValidate()
		{
			this.editorSettings.frameRate = TimelineAsset.GetValidFrameRate(this.editorSettings.frameRate);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000045D0 File Offset: 0x000027D0
		public TrackAsset GetRootTrack(int index)
		{
			this.UpdateRootTrackCache();
			return this.m_CacheRootTracks[index];
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000045E4 File Offset: 0x000027E4
		public IEnumerable<TrackAsset> GetRootTracks()
		{
			this.UpdateRootTrackCache();
			return this.m_CacheRootTracks;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000045F2 File Offset: 0x000027F2
		public TrackAsset GetOutputTrack(int index)
		{
			this.UpdateOutputTrackCache();
			return this.m_CacheOutputTracks[index];
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004602 File Offset: 0x00002802
		public IEnumerable<TrackAsset> GetOutputTracks()
		{
			this.UpdateOutputTrackCache();
			return this.m_CacheOutputTracks;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004610 File Offset: 0x00002810
		private static double GetValidFrameRate(double frameRate)
		{
			return Math.Min(Math.Max(frameRate, TimelineAsset.EditorSettings.kMinFrameRate), TimelineAsset.EditorSettings.kMaxFrameRate);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004628 File Offset: 0x00002828
		private void UpdateRootTrackCache()
		{
			if (this.m_CacheRootTracks == null)
			{
				if (this.m_Tracks == null)
				{
					this.m_CacheRootTracks = new List<TrackAsset>();
					return;
				}
				this.m_CacheRootTracks = new List<TrackAsset>(this.m_Tracks.Count);
				if (this.markerTrack != null)
				{
					this.m_CacheRootTracks.Add(this.markerTrack);
				}
				foreach (ScriptableObject scriptableObject in this.m_Tracks)
				{
					TrackAsset trackAsset = scriptableObject as TrackAsset;
					if (trackAsset != null)
					{
						this.m_CacheRootTracks.Add(trackAsset);
					}
				}
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000046E4 File Offset: 0x000028E4
		private void UpdateOutputTrackCache()
		{
			if (this.m_CacheOutputTracks == null)
			{
				List<TrackAsset> list = new List<TrackAsset>();
				foreach (TrackAsset trackAsset in this.flattenedTracks)
				{
					if (trackAsset != null && trackAsset.GetType() != typeof(GroupTrack) && !trackAsset.isSubTrack)
					{
						list.Add(trackAsset);
					}
				}
				this.m_CacheOutputTracks = list.ToArray();
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004754 File Offset: 0x00002954
		internal TrackAsset[] flattenedTracks
		{
			get
			{
				if (this.m_CacheFlattenedTracks == null)
				{
					List<TrackAsset> list = new List<TrackAsset>(this.m_Tracks.Count * 2);
					this.UpdateRootTrackCache();
					list.AddRange(this.m_CacheRootTracks);
					for (int i = 0; i < this.m_CacheRootTracks.Count; i++)
					{
						TimelineAsset.AddSubTracksRecursive(this.m_CacheRootTracks[i], ref list);
					}
					this.m_CacheFlattenedTracks = list.ToArray();
				}
				return this.m_CacheFlattenedTracks;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000047C9 File Offset: 0x000029C9
		public MarkerTrack markerTrack
		{
			get
			{
				return this.m_MarkerTrack;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000047D1 File Offset: 0x000029D1
		internal List<ScriptableObject> trackObjects
		{
			get
			{
				return this.m_Tracks;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000047D9 File Offset: 0x000029D9
		internal void AddTrackInternal(TrackAsset track)
		{
			this.m_Tracks.Add(track);
			track.parent = this;
			this.Invalidate();
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000047F4 File Offset: 0x000029F4
		internal void RemoveTrack(TrackAsset track)
		{
			this.m_Tracks.Remove(track);
			this.Invalidate();
			TrackAsset trackAsset = track.parent as TrackAsset;
			if (trackAsset != null)
			{
				trackAsset.RemoveSubTrack(track);
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004834 File Offset: 0x00002A34
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			bool flag = false;
			bool flag2 = graph.GetPlayableCount() == 0;
			ScriptPlayable<TimelinePlayable> scriptPlayable = TimelinePlayable.Create(graph, this.GetOutputTracks(), go, flag, flag2);
			scriptPlayable.SetDuration(this.duration);
			scriptPlayable.SetPropagateSetTime(true);
			if (!scriptPlayable.IsValid<ScriptPlayable<TimelinePlayable>>())
			{
				return Playable.Null;
			}
			return scriptPlayable;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004885 File Offset: 0x00002A85
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			this.m_Version = 0;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000488E File Offset: 0x00002A8E
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.Invalidate();
			if (this.m_Version < 0)
			{
				this.UpgradeToLatestVersion();
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000048A8 File Offset: 0x00002AA8
		private void __internalAwake()
		{
			if (this.m_Tracks == null)
			{
				this.m_Tracks = new List<ScriptableObject>();
			}
			for (int i = this.m_Tracks.Count - 1; i >= 0; i--)
			{
				TrackAsset trackAsset = this.m_Tracks[i] as TrackAsset;
				if (trackAsset != null)
				{
					trackAsset.parent = this;
				}
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004904 File Offset: 0x00002B04
		public void GatherProperties(PlayableDirector director, IPropertyCollector driver)
		{
			foreach (TrackAsset trackAsset in this.GetOutputTracks())
			{
				if (!trackAsset.mutedInHierarchy)
				{
					trackAsset.GatherProperties(director, driver);
				}
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000495C File Offset: 0x00002B5C
		public void CreateMarkerTrack()
		{
			if (this.m_MarkerTrack == null)
			{
				this.m_MarkerTrack = ScriptableObject.CreateInstance<MarkerTrack>();
				TimelineCreateUtilities.SaveAssetIntoObject(this.m_MarkerTrack, this);
				this.m_MarkerTrack.parent = this;
				this.m_MarkerTrack.name = "Markers";
				this.Invalidate();
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000049B0 File Offset: 0x00002BB0
		internal void RemoveMarkerTrack()
		{
			if (this.m_MarkerTrack != null)
			{
				Object markerTrack = this.m_MarkerTrack;
				this.m_MarkerTrack = null;
				TimelineCreateUtilities.RemoveAssetFromObject(markerTrack, this);
				this.Invalidate();
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000049D9 File Offset: 0x00002BD9
		internal void Invalidate()
		{
			this.m_CacheRootTracks = null;
			this.m_CacheOutputTracks = null;
			this.m_CacheFlattenedTracks = null;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000049F0 File Offset: 0x00002BF0
		internal void UpdateFixedDurationWithItemsDuration()
		{
			this.m_FixedDuration = (double)this.CalculateItemsDuration();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004A04 File Offset: 0x00002C04
		private DiscreteTime CalculateItemsDuration()
		{
			DiscreteTime discreteTime = new DiscreteTime(0);
			foreach (TrackAsset trackAsset in this.flattenedTracks)
			{
				if (!trackAsset.mutedInHierarchy)
				{
					discreteTime = DiscreteTime.Max(discreteTime, (DiscreteTime)trackAsset.end);
				}
			}
			if (discreteTime <= 0)
			{
				return new DiscreteTime(0);
			}
			return discreteTime;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004A64 File Offset: 0x00002C64
		private static void AddSubTracksRecursive(TrackAsset track, ref List<TrackAsset> allTracks)
		{
			if (track == null)
			{
				return;
			}
			allTracks.AddRange(track.GetChildTracks());
			foreach (TrackAsset trackAsset in track.GetChildTracks())
			{
				TimelineAsset.AddSubTracksRecursive(trackAsset, ref allTracks);
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public TrackAsset CreateTrack(Type type, TrackAsset parent, string name)
		{
			if (parent != null && parent.timelineAsset != this)
			{
				throw new InvalidOperationException("Addtrack cannot parent to a track not in the Timeline");
			}
			if (!typeof(TrackAsset).IsAssignableFrom(type))
			{
				throw new InvalidOperationException("Supplied type must be a track asset");
			}
			if (parent != null && !TimelineCreateUtilities.ValidateParentTrack(parent, type))
			{
				throw new InvalidOperationException("Cannot assign a child of type " + type.Name + " to a parent of type " + parent.GetType().Name);
			}
			string text = name;
			if (string.IsNullOrEmpty(text))
			{
				text = type.Name;
			}
			string text2;
			if (parent != null)
			{
				text2 = TimelineCreateUtilities.GenerateUniqueActorName(parent.subTracksObjects, text);
			}
			else
			{
				text2 = TimelineCreateUtilities.GenerateUniqueActorName(this.trackObjects, text);
			}
			return this.AllocateTrack(parent, text2, type);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004B8D File Offset: 0x00002D8D
		public T CreateTrack<T>(TrackAsset parent, string trackName) where T : TrackAsset, new()
		{
			return (T)((object)this.CreateTrack(typeof(T), parent, trackName));
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004BA6 File Offset: 0x00002DA6
		public T CreateTrack<T>(string trackName) where T : TrackAsset, new()
		{
			return (T)((object)this.CreateTrack(typeof(T), null, trackName));
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004BBF File Offset: 0x00002DBF
		public T CreateTrack<T>() where T : TrackAsset, new()
		{
			return (T)((object)this.CreateTrack(typeof(T), null, null));
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004BD8 File Offset: 0x00002DD8
		public bool DeleteClip(TimelineClip clip)
		{
			if (clip == null || clip.GetParentTrack() == null)
			{
				return false;
			}
			if (this != clip.GetParentTrack().timelineAsset)
			{
				Debug.LogError("Cannot delete a clip from this timeline");
				return false;
			}
			if (clip.curves != null)
			{
				TimelineUndo.PushDestroyUndo(this, clip.GetParentTrack(), clip.curves);
			}
			if (clip.asset != null)
			{
				this.DeleteRecordedAnimation(clip);
				TimelineUndo.PushDestroyUndo(this, clip.GetParentTrack(), clip.asset);
			}
			TrackAsset parentTrack = clip.GetParentTrack();
			parentTrack.RemoveClip(clip);
			parentTrack.CalculateExtrapolationTimes();
			return true;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004C74 File Offset: 0x00002E74
		public bool DeleteTrack(TrackAsset track)
		{
			if (track.timelineAsset != this)
			{
				return false;
			}
			track.parent as TrackAsset != null;
			foreach (TrackAsset trackAsset in track.GetChildTracks())
			{
				this.DeleteTrack(trackAsset);
			}
			this.DeleteRecordedAnimation(track);
			foreach (TimelineClip timelineClip in new List<TimelineClip>(track.clips))
			{
				this.DeleteClip(timelineClip);
			}
			this.RemoveTrack(track);
			TimelineUndo.PushDestroyUndo(this, this, track);
			return true;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004D44 File Offset: 0x00002F44
		internal void MoveLastTrackBefore(TrackAsset asset)
		{
			if (this.m_Tracks == null || this.m_Tracks.Count < 2 || asset == null)
			{
				return;
			}
			ScriptableObject scriptableObject = this.m_Tracks[this.m_Tracks.Count - 1];
			if (scriptableObject == asset)
			{
				return;
			}
			for (int i = 0; i < this.m_Tracks.Count - 1; i++)
			{
				if (this.m_Tracks[i] == asset)
				{
					for (int j = this.m_Tracks.Count - 1; j > i; j--)
					{
						this.m_Tracks[j] = this.m_Tracks[j - 1];
					}
					this.m_Tracks[i] = scriptableObject;
					this.Invalidate();
					return;
				}
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004E08 File Offset: 0x00003008
		private TrackAsset AllocateTrack(TrackAsset trackAssetParent, string trackName, Type trackType)
		{
			if (trackAssetParent != null && trackAssetParent.timelineAsset != this)
			{
				throw new InvalidOperationException("Addtrack cannot parent to a track not in the Timeline");
			}
			if (!typeof(TrackAsset).IsAssignableFrom(trackType))
			{
				throw new InvalidOperationException("Supplied type must be a track asset");
			}
			TrackAsset trackAsset = (TrackAsset)ScriptableObject.CreateInstance(trackType);
			trackAsset.name = trackName;
			PlayableAsset playableAsset = ((trackAssetParent != null) ? trackAssetParent : this);
			TimelineCreateUtilities.SaveAssetIntoObject(trackAsset, playableAsset);
			if (trackAssetParent != null)
			{
				trackAssetParent.AddChild(trackAsset);
			}
			else
			{
				this.AddTrackInternal(trackAsset);
			}
			return trackAsset;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004E98 File Offset: 0x00003098
		private void DeleteRecordedAnimation(TrackAsset track)
		{
			AnimationTrack animationTrack = track as AnimationTrack;
			if (animationTrack != null && animationTrack.infiniteClip != null)
			{
				TimelineUndo.PushDestroyUndo(this, track, animationTrack.infiniteClip);
			}
			if (track.curves != null)
			{
				TimelineUndo.PushDestroyUndo(this, track, track.curves);
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004EEC File Offset: 0x000030EC
		private void DeleteRecordedAnimation(TimelineClip clip)
		{
			if (clip == null)
			{
				return;
			}
			if (clip.curves != null)
			{
				TimelineUndo.PushDestroyUndo(this, clip.GetParentTrack(), clip.curves);
			}
			if (!clip.recordable)
			{
				return;
			}
			AnimationPlayableAsset animationPlayableAsset = clip.asset as AnimationPlayableAsset;
			if (animationPlayableAsset == null || animationPlayableAsset.clip == null)
			{
				return;
			}
			TimelineUndo.PushDestroyUndo(this, animationPlayableAsset, animationPlayableAsset.clip);
		}

		// Token: 0x04000063 RID: 99
		private const int k_LatestVersion = 0;

		// Token: 0x04000064 RID: 100
		[SerializeField]
		[HideInInspector]
		private int m_Version;

		// Token: 0x04000065 RID: 101
		[HideInInspector]
		[SerializeField]
		private List<ScriptableObject> m_Tracks;

		// Token: 0x04000066 RID: 102
		[HideInInspector]
		[SerializeField]
		private double m_FixedDuration;

		// Token: 0x04000067 RID: 103
		[HideInInspector]
		[NonSerialized]
		private TrackAsset[] m_CacheOutputTracks;

		// Token: 0x04000068 RID: 104
		[HideInInspector]
		[NonSerialized]
		private List<TrackAsset> m_CacheRootTracks;

		// Token: 0x04000069 RID: 105
		[HideInInspector]
		[NonSerialized]
		private TrackAsset[] m_CacheFlattenedTracks;

		// Token: 0x0400006A RID: 106
		[HideInInspector]
		[SerializeField]
		private TimelineAsset.EditorSettings m_EditorSettings = new TimelineAsset.EditorSettings();

		// Token: 0x0400006B RID: 107
		[SerializeField]
		private TimelineAsset.DurationMode m_DurationMode;

		// Token: 0x0400006C RID: 108
		[HideInInspector]
		[SerializeField]
		private MarkerTrack m_MarkerTrack;

		// Token: 0x02000069 RID: 105
		private enum Versions
		{
			// Token: 0x04000147 RID: 327
			Initial
		}

		// Token: 0x0200006A RID: 106
		private static class TimelineAssetUpgrade
		{
		}

		// Token: 0x0200006B RID: 107
		[Obsolete("MediaType has been deprecated. It is no longer required, and will be removed in a future release.", false)]
		public enum MediaType
		{
			// Token: 0x04000149 RID: 329
			Animation,
			// Token: 0x0400014A RID: 330
			Audio,
			// Token: 0x0400014B RID: 331
			Texture,
			// Token: 0x0400014C RID: 332
			[Obsolete("Use Texture MediaType instead. (UnityUpgradable) -> UnityEngine.Timeline.TimelineAsset/MediaType.Texture", false)]
			Video = 2,
			// Token: 0x0400014D RID: 333
			Script,
			// Token: 0x0400014E RID: 334
			Hybrid,
			// Token: 0x0400014F RID: 335
			Group
		}

		// Token: 0x0200006C RID: 108
		public enum DurationMode
		{
			// Token: 0x04000151 RID: 337
			BasedOnClips,
			// Token: 0x04000152 RID: 338
			FixedLength
		}

		// Token: 0x0200006D RID: 109
		[Serializable]
		public class EditorSettings
		{
			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x06000345 RID: 837 RVA: 0x0000BCEA File Offset: 0x00009EEA
			// (set) Token: 0x06000346 RID: 838 RVA: 0x0000BCF3 File Offset: 0x00009EF3
			[Obsolete("EditorSettings.fps has been deprecated. Use editorSettings.frameRate instead.", false)]
			public float fps
			{
				get
				{
					return (float)this.m_Framerate;
				}
				set
				{
					this.m_Framerate = (double)Mathf.Clamp(value, (float)TimelineAsset.EditorSettings.kMinFrameRate, (float)TimelineAsset.EditorSettings.kMaxFrameRate);
				}
			}

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x06000347 RID: 839 RVA: 0x0000BD0E File Offset: 0x00009F0E
			// (set) Token: 0x06000348 RID: 840 RVA: 0x0000BD16 File Offset: 0x00009F16
			public double frameRate
			{
				get
				{
					return this.m_Framerate;
				}
				set
				{
					this.m_Framerate = TimelineAsset.GetValidFrameRate(value);
				}
			}

			// Token: 0x06000349 RID: 841 RVA: 0x0000BD24 File Offset: 0x00009F24
			public void SetStandardFrameRate(StandardFrameRates enumValue)
			{
				FrameRate frameRate = TimeUtility.ToFrameRate(enumValue);
				if (!frameRate.IsValid())
				{
					throw new ArgumentException(string.Format("StandardFrameRates {0}, is not defined", enumValue.ToString()));
				}
				this.m_Framerate = frameRate.rate;
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x0600034A RID: 842 RVA: 0x0000BD6B File Offset: 0x00009F6B
			// (set) Token: 0x0600034B RID: 843 RVA: 0x0000BD73 File Offset: 0x00009F73
			public bool scenePreview
			{
				get
				{
					return this.m_ScenePreview;
				}
				set
				{
					this.m_ScenePreview = value;
				}
			}

			// Token: 0x04000153 RID: 339
			internal static readonly double kMinFrameRate = TimeUtility.kFrameRateEpsilon;

			// Token: 0x04000154 RID: 340
			internal static readonly double kMaxFrameRate = 1000.0;

			// Token: 0x04000155 RID: 341
			internal static readonly double kDefaultFrameRate = 60.0;

			// Token: 0x04000156 RID: 342
			[HideInInspector]
			[SerializeField]
			[FrameRateField]
			private double m_Framerate = TimelineAsset.EditorSettings.kDefaultFrameRate;

			// Token: 0x04000157 RID: 343
			[HideInInspector]
			[SerializeField]
			private bool m_ScenePreview = true;
		}
	}
}
