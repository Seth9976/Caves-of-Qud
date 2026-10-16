using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine.Animations;
using UnityEngine.Audio;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200004E RID: 78
	public class TimelinePlayable : PlayableBehaviour
	{
		// Token: 0x060002DA RID: 730 RVA: 0x00009EBC File Offset: 0x000080BC
		public static ScriptPlayable<TimelinePlayable> Create(PlayableGraph graph, IEnumerable<TrackAsset> tracks, GameObject go, bool autoRebalance, bool createOutputs)
		{
			if (tracks == null)
			{
				throw new ArgumentNullException("Tracks list is null", "tracks");
			}
			if (go == null)
			{
				throw new ArgumentNullException("GameObject parameter is null", "go");
			}
			ScriptPlayable<TimelinePlayable> scriptPlayable = ScriptPlayable<TimelinePlayable>.Create(graph, 0);
			scriptPlayable.SetTraversalMode(PlayableTraversalMode.Passthrough);
			scriptPlayable.GetBehaviour().Compile(graph, scriptPlayable, tracks, go, autoRebalance, createOutputs);
			return scriptPlayable;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00009F20 File Offset: 0x00008120
		public void Compile(PlayableGraph graph, Playable timelinePlayable, IEnumerable<TrackAsset> tracks, GameObject go, bool autoRebalance, bool createOutputs)
		{
			if (tracks == null)
			{
				throw new ArgumentNullException("Tracks list is null", "tracks");
			}
			if (go == null)
			{
				throw new ArgumentNullException("GameObject parameter is null", "go");
			}
			List<TrackAsset> list = new List<TrackAsset>(tracks);
			int num = list.Count * 2 + list.Count;
			this.m_CurrentListOfActiveClips = new List<RuntimeElement>(num);
			this.m_ActiveClips = new List<RuntimeElement>(num);
			this.m_EvaluateCallbacks.Clear();
			this.m_AlwaysEvaluateCallbacks.Clear();
			this.m_PlayableCache.Clear();
			this.CompileTrackList(graph, timelinePlayable, list, go, createOutputs);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00009FB8 File Offset: 0x000081B8
		private void CompileTrackList(PlayableGraph graph, Playable timelinePlayable, IEnumerable<TrackAsset> tracks, GameObject go, bool createOutputs)
		{
			foreach (TrackAsset trackAsset in tracks)
			{
				if (trackAsset.IsCompilable() && !this.m_PlayableCache.ContainsKey(trackAsset))
				{
					trackAsset.SortClips();
					trackAsset.ComputeBlendsFromOverlaps(false);
					this.CreateTrackPlayable(graph, timelinePlayable, trackAsset, go, createOutputs);
				}
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000A02C File Offset: 0x0000822C
		private void CreateTrackOutput(PlayableGraph graph, TrackAsset track, GameObject go, Playable playable, int port)
		{
			if (track.isSubTrack)
			{
				return;
			}
			foreach (PlayableBinding playableBinding in track.outputs)
			{
				PlayableOutput playableOutput = playableBinding.CreateOutput(graph);
				playableOutput.SetReferenceObject(playableBinding.sourceObject);
				playableOutput.SetSourcePlayable(playable, port);
				playableOutput.SetWeight(1f);
				AnimationTrack animationTrack = track as AnimationTrack;
				if (animationTrack != null)
				{
					this.AddPlayableOutputCallbacks(animationTrack, playableOutput);
				}
				if (playableOutput.IsPlayableOutputOfType<AudioPlayableOutput>())
				{
					((AudioPlayableOutput)playableOutput).SetEvaluateOnSeek(!TimelinePlayable.muteAudioScrubbing);
				}
				if (track.timelineAsset.markerTrack == track)
				{
					PlayableDirector component = go.GetComponent<PlayableDirector>();
					playableOutput.SetUserData(component);
					foreach (INotificationReceiver notificationReceiver in go.GetComponents<INotificationReceiver>())
					{
						playableOutput.AddNotificationReceiver(notificationReceiver);
					}
				}
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000A12C File Offset: 0x0000832C
		private Playable CreateTrackPlayable(PlayableGraph graph, Playable timelinePlayable, TrackAsset track, GameObject go, bool createOutputs)
		{
			if (!track.IsCompilable())
			{
				return timelinePlayable;
			}
			Playable playable;
			if (this.m_PlayableCache.TryGetValue(track, out playable))
			{
				return playable;
			}
			if (track.name == "root")
			{
				return timelinePlayable;
			}
			TrackAsset trackAsset = track.parent as TrackAsset;
			Playable playable2 = ((trackAsset != null) ? this.CreateTrackPlayable(graph, timelinePlayable, trackAsset, go, createOutputs) : timelinePlayable);
			Playable playable3 = track.CreatePlayableGraph(graph, go, this.m_IntervalTree, timelinePlayable);
			bool flag = false;
			if (!playable3.IsValid<Playable>())
			{
				string name = track.name;
				string text = "(";
				Type type = track.GetType();
				throw new InvalidOperationException(name + text + ((type != null) ? type.ToString() : null) + ") did not produce a valid playable.");
			}
			if (playable2.IsValid<Playable>() && playable3.IsValid<Playable>())
			{
				int inputCount = playable2.GetInputCount<Playable>();
				playable2.SetInputCount(inputCount + 1);
				flag = graph.Connect<Playable, Playable>(playable3, 0, playable2, inputCount);
				playable2.SetInputWeight(inputCount, 1f);
			}
			if (createOutputs && flag)
			{
				this.CreateTrackOutput(graph, track, go, playable2, playable2.GetInputCount<Playable>() - 1);
			}
			this.CacheTrack(track, playable3);
			return playable3;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000A238 File Offset: 0x00008438
		public override void PrepareFrame(Playable playable, FrameData info)
		{
			this.Evaluate(playable, info);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000A244 File Offset: 0x00008444
		private void Evaluate(Playable playable, FrameData frameData)
		{
			if (this.m_IntervalTree == null)
			{
				return;
			}
			double time = playable.GetTime<Playable>();
			this.m_ActiveBit = ((this.m_ActiveBit == 0) ? 1 : 0);
			this.m_CurrentListOfActiveClips.Clear();
			this.m_IntervalTree.IntersectsWith(DiscreteTime.GetNearestTick(time), this.m_CurrentListOfActiveClips);
			foreach (RuntimeElement runtimeElement in this.m_CurrentListOfActiveClips)
			{
				runtimeElement.intervalBit = this.m_ActiveBit;
			}
			double num = (double)new DiscreteTime(playable.GetDuration<Playable>());
			foreach (RuntimeElement runtimeElement2 in this.m_ActiveClips)
			{
				if (runtimeElement2.intervalBit != this.m_ActiveBit)
				{
					runtimeElement2.DisableAt(time, num, frameData);
				}
			}
			this.m_ActiveClips.Clear();
			for (int i = 0; i < this.m_CurrentListOfActiveClips.Count; i++)
			{
				this.m_CurrentListOfActiveClips[i].EvaluateAt(time, frameData);
				this.m_ActiveClips.Add(this.m_CurrentListOfActiveClips[i]);
			}
			this.InvokeOutputCallbacks(this.m_CurrentListOfActiveClips);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000A3A0 File Offset: 0x000085A0
		private void CacheTrack(TrackAsset track, Playable playable)
		{
			this.m_PlayableCache[track] = playable;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000A3AF File Offset: 0x000085AF
		private static void ForAOTCompilationOnly()
		{
			new List<IntervalTree<RuntimeElement>.Entry>();
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000A3B7 File Offset: 0x000085B7
		private void AddPlayableOutputCallbacks(AnimationTrack track, PlayableOutput playableOutput)
		{
			this.AddOutputWeightProcessor(track, (AnimationPlayableOutput)playableOutput);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000A3C8 File Offset: 0x000085C8
		private void AddOutputWeightProcessor(AnimationTrack track, AnimationPlayableOutput animOutput)
		{
			AnimationOutputWeightProcessor animationOutputWeightProcessor = new AnimationOutputWeightProcessor(animOutput);
			if (track.inClipMode)
			{
				this.AddEvaluateCallback(track, animationOutputWeightProcessor);
			}
			else
			{
				this.m_AlwaysEvaluateCallbacks.Add(animationOutputWeightProcessor);
			}
			this.m_ForceEvaluateNextEvaluate.Add(animationOutputWeightProcessor);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000A408 File Offset: 0x00008608
		private void AddEvaluateCallback(AnimationTrack track, ITimelineEvaluateCallback callback)
		{
			List<ITimelineEvaluateCallback> list;
			if (this.m_EvaluateCallbacks.TryGetValue(track, out list))
			{
				list.Add(callback);
				return;
			}
			this.m_EvaluateCallbacks[track] = new List<ITimelineEvaluateCallback> { callback };
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000A448 File Offset: 0x00008648
		private void InvokeOutputCallbacks(IReadOnlyList<RuntimeElement> activeRuntimeElements)
		{
			foreach (ITimelineEvaluateCallback timelineEvaluateCallback in this.m_ForceEvaluateNextEvaluate)
			{
				timelineEvaluateCallback.Evaluate();
				this.m_InvokedThisFrame.Add(timelineEvaluateCallback);
			}
			this.m_ForceEvaluateNextEvaluate.Clear();
			if (activeRuntimeElements.Count > 0)
			{
				using (TimelinePlayable.TrackCacheManager trackCacheManager = new TimelinePlayable.TrackCacheManager(this.m_ActiveTracksToEvaluateCache, activeRuntimeElements))
				{
					using (HashSet<AnimationTrack>.Enumerator enumerator2 = trackCacheManager.trackCache.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							AnimationTrack animationTrack = enumerator2.Current;
							List<ITimelineEvaluateCallback> list;
							if (this.TryGetCallbackList(animationTrack, out list))
							{
								foreach (ITimelineEvaluateCallback timelineEvaluateCallback2 in list)
								{
									if (!this.m_InvokedThisFrame.Contains(timelineEvaluateCallback2))
									{
										timelineEvaluateCallback2.Evaluate();
										this.m_InvokedThisFrame.Add(timelineEvaluateCallback2);
										this.m_ForceEvaluateNextEvaluate.Add(timelineEvaluateCallback2);
									}
								}
							}
						}
						goto IL_0188;
					}
				}
			}
			foreach (List<ITimelineEvaluateCallback> list2 in this.m_EvaluateCallbacks.Values)
			{
				foreach (ITimelineEvaluateCallback timelineEvaluateCallback3 in list2)
				{
					if (!this.m_InvokedThisFrame.Contains(timelineEvaluateCallback3))
					{
						timelineEvaluateCallback3.Evaluate();
					}
				}
			}
			IL_0188:
			foreach (ITimelineEvaluateCallback timelineEvaluateCallback4 in this.m_AlwaysEvaluateCallbacks)
			{
				timelineEvaluateCallback4.Evaluate();
			}
			this.m_InvokedThisFrame.Clear();
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000A674 File Offset: 0x00008874
		private bool TryGetCallbackList(AnimationTrack track, out List<ITimelineEvaluateCallback> list)
		{
			if (track == null)
			{
				list = null;
				return false;
			}
			return this.m_EvaluateCallbacks.TryGetValue(track, out list) || this.TryGetCallbackList(track.parent as AnimationTrack, out list);
		}

		// Token: 0x040000FD RID: 253
		private static readonly ProfilerMarker k_CreateTimelineGraphMarker = new ProfilerMarker(ProfilerCategory.Scripts, "Timeline.CreatePlayableGraph");

		// Token: 0x040000FE RID: 254
		private static readonly ProfilerMarker k_CreateTimelineTrackMarker = new ProfilerMarker(ProfilerCategory.Scripts, "Timeline.CreateTrackPlayable");

		// Token: 0x040000FF RID: 255
		private static readonly ProfilerMarker k_CreateTimelineTrackOutputsMarker = new ProfilerMarker(ProfilerCategory.Scripts, "Timeline.CreateTrackPlayableOutputs");

		// Token: 0x04000100 RID: 256
		private static readonly ProfilerMarker m_findActiveClipsMarker = new ProfilerMarker(ProfilerCategory.Scripts, "TimelinePlayable.GetActiveClips");

		// Token: 0x04000101 RID: 257
		private static readonly ProfilerMarker m_SetClipsLocalTimeMarker = new ProfilerMarker(ProfilerCategory.Scripts, "TimelinePlayable.SetActiveClipsTime");

		// Token: 0x04000102 RID: 258
		private IntervalTree<RuntimeElement> m_IntervalTree = new IntervalTree<RuntimeElement>();

		// Token: 0x04000103 RID: 259
		private List<RuntimeElement> m_ActiveClips = new List<RuntimeElement>();

		// Token: 0x04000104 RID: 260
		private List<RuntimeElement> m_CurrentListOfActiveClips;

		// Token: 0x04000105 RID: 261
		private int m_ActiveBit;

		// Token: 0x04000106 RID: 262
		private Dictionary<TrackAsset, Playable> m_PlayableCache = new Dictionary<TrackAsset, Playable>();

		// Token: 0x04000107 RID: 263
		internal static bool muteAudioScrubbing = true;

		// Token: 0x04000108 RID: 264
		private readonly Dictionary<AnimationTrack, List<ITimelineEvaluateCallback>> m_EvaluateCallbacks = new Dictionary<AnimationTrack, List<ITimelineEvaluateCallback>>();

		// Token: 0x04000109 RID: 265
		private readonly List<ITimelineEvaluateCallback> m_AlwaysEvaluateCallbacks = new List<ITimelineEvaluateCallback>();

		// Token: 0x0400010A RID: 266
		private readonly HashSet<ITimelineEvaluateCallback> m_ForceEvaluateNextEvaluate = new HashSet<ITimelineEvaluateCallback>();

		// Token: 0x0400010B RID: 267
		private readonly HashSet<ITimelineEvaluateCallback> m_InvokedThisFrame = new HashSet<ITimelineEvaluateCallback>();

		// Token: 0x0400010C RID: 268
		private readonly HashSet<AnimationTrack> m_ActiveTracksToEvaluateCache = new HashSet<AnimationTrack>();

		// Token: 0x0200007F RID: 127
		private readonly struct TrackCacheManager : IDisposable
		{
			// Token: 0x0600038A RID: 906 RVA: 0x0000C567 File Offset: 0x0000A767
			public TrackCacheManager(HashSet<AnimationTrack> cache, IReadOnlyList<RuntimeElement> activeRuntimeElements)
			{
				this.trackCache = cache;
				this.GetTrackAssetsFromRuntimeElements(activeRuntimeElements);
			}

			// Token: 0x0600038B RID: 907 RVA: 0x0000C577 File Offset: 0x0000A777
			public void Dispose()
			{
				this.trackCache.Clear();
			}

			// Token: 0x0600038C RID: 908 RVA: 0x0000C584 File Offset: 0x0000A784
			private void GetTrackAssetsFromRuntimeElements(IReadOnlyList<RuntimeElement> activeRuntimeElements)
			{
				for (int i = 0; i < activeRuntimeElements.Count; i++)
				{
					RuntimeClip runtimeClip = activeRuntimeElements[i] as RuntimeClip;
					if (runtimeClip != null)
					{
						TimelineClip clip = runtimeClip.clip;
						AnimationTrack animationTrack = ((clip != null) ? clip.GetParentTrack() : null) as AnimationTrack;
						if (animationTrack != null)
						{
							this.trackCache.Add(animationTrack);
						}
					}
				}
			}

			// Token: 0x04000194 RID: 404
			public readonly HashSet<AnimationTrack> trackCache;
		}
	}
}
