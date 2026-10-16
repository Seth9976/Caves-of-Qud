using System;
using UnityEngine.Animations;

namespace UnityEngine.Playables
{
	// Token: 0x02000044 RID: 68
	public static class AnimationPlayableUtilities
	{
		// Token: 0x060003A7 RID: 935 RVA: 0x00007D20 File Offset: 0x00005F20
		[Obsolete("This function is no longer used as it overrides the Time Update Mode of the Playable Graph. Refer to the documentation for an example of an equivalent function.")]
		public static void Play(Animator animator, Playable playable, PlayableGraph graph)
		{
			AnimationPlayableOutput animationPlayableOutput = AnimationPlayableOutput.Create(graph, "AnimationClip", animator);
			animationPlayableOutput.SetSourcePlayable(playable, 0);
			graph.SyncUpdateAndTimeMode(animator);
			graph.Play();
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00007D54 File Offset: 0x00005F54
		public static AnimationClipPlayable PlayClip(Animator animator, AnimationClip clip, out PlayableGraph graph)
		{
			graph = PlayableGraph.Create();
			AnimationPlayableOutput animationPlayableOutput = AnimationPlayableOutput.Create(graph, "AnimationClip", animator);
			AnimationClipPlayable animationClipPlayable = AnimationClipPlayable.Create(graph, clip);
			animationPlayableOutput.SetSourcePlayable(animationClipPlayable);
			graph.SyncUpdateAndTimeMode(animator);
			graph.Play();
			return animationClipPlayable;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00007DB0 File Offset: 0x00005FB0
		public static AnimationMixerPlayable PlayMixer(Animator animator, int inputCount, out PlayableGraph graph)
		{
			graph = PlayableGraph.Create();
			AnimationPlayableOutput animationPlayableOutput = AnimationPlayableOutput.Create(graph, "Mixer", animator);
			AnimationMixerPlayable animationMixerPlayable = AnimationMixerPlayable.Create(graph, inputCount);
			animationPlayableOutput.SetSourcePlayable(animationMixerPlayable);
			graph.SyncUpdateAndTimeMode(animator);
			graph.Play();
			return animationMixerPlayable;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00007E0C File Offset: 0x0000600C
		public static AnimationLayerMixerPlayable PlayLayerMixer(Animator animator, int inputCount, out PlayableGraph graph)
		{
			graph = PlayableGraph.Create();
			AnimationPlayableOutput animationPlayableOutput = AnimationPlayableOutput.Create(graph, "Mixer", animator);
			AnimationLayerMixerPlayable animationLayerMixerPlayable = AnimationLayerMixerPlayable.Create(graph, inputCount);
			animationPlayableOutput.SetSourcePlayable(animationLayerMixerPlayable);
			graph.SyncUpdateAndTimeMode(animator);
			graph.Play();
			return animationLayerMixerPlayable;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00007E68 File Offset: 0x00006068
		public static AnimatorControllerPlayable PlayAnimatorController(Animator animator, RuntimeAnimatorController controller, out PlayableGraph graph)
		{
			graph = PlayableGraph.Create();
			AnimationPlayableOutput animationPlayableOutput = AnimationPlayableOutput.Create(graph, "AnimatorControllerPlayable", animator);
			AnimatorControllerPlayable animatorControllerPlayable = AnimatorControllerPlayable.Create(graph, controller);
			animationPlayableOutput.SetSourcePlayable(animatorControllerPlayable);
			graph.SyncUpdateAndTimeMode(animator);
			graph.Play();
			return animatorControllerPlayable;
		}
	}
}
