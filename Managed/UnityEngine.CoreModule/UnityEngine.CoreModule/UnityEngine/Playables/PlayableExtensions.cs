using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000454 RID: 1108
	public static class PlayableExtensions
	{
		// Token: 0x06002A6D RID: 10861 RVA: 0x000517D0 File Offset: 0x0004F9D0
		public static bool IsNull<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().IsNull();
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x000517F8 File Offset: 0x0004F9F8
		public static bool IsValid<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().IsValid();
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x00051820 File Offset: 0x0004FA20
		public static void Destroy<U>(this U playable) where U : struct, IPlayable
		{
			playable.GetHandle().Destroy();
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x00051844 File Offset: 0x0004FA44
		public static PlayableGraph GetGraph<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetGraph();
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x0005186C File Offset: 0x0004FA6C
		[Obsolete("SetPlayState() has been deprecated. Use Play(), Pause() or SetDelay() instead", false)]
		public static void SetPlayState<U>(this U playable, PlayState value) where U : struct, IPlayable
		{
			bool flag = value == PlayState.Delayed;
			if (flag)
			{
				throw new ArgumentException("Can't set Delayed: use SetDelay() instead");
			}
			if (value != PlayState.Paused)
			{
				if (value == PlayState.Playing)
				{
					playable.GetHandle().Play();
				}
			}
			else
			{
				playable.GetHandle().Pause();
			}
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x000518CC File Offset: 0x0004FACC
		public static PlayState GetPlayState<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetPlayState();
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000518F4 File Offset: 0x0004FAF4
		public static void Play<U>(this U playable) where U : struct, IPlayable
		{
			playable.GetHandle().Play();
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x00051918 File Offset: 0x0004FB18
		public static void Pause<U>(this U playable) where U : struct, IPlayable
		{
			playable.GetHandle().Pause();
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x0005193C File Offset: 0x0004FB3C
		public static void SetSpeed<U>(this U playable, double value) where U : struct, IPlayable
		{
			playable.GetHandle().SetSpeed(value);
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x00051964 File Offset: 0x0004FB64
		public static double GetSpeed<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetSpeed();
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x0005198C File Offset: 0x0004FB8C
		public static void SetDuration<U>(this U playable, double value) where U : struct, IPlayable
		{
			playable.GetHandle().SetDuration(value);
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000519B4 File Offset: 0x0004FBB4
		public static double GetDuration<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetDuration();
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x000519DC File Offset: 0x0004FBDC
		public static void SetTime<U>(this U playable, double value) where U : struct, IPlayable
		{
			playable.GetHandle().SetTime(value);
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x00051A04 File Offset: 0x0004FC04
		public static double GetTime<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetTime();
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x00051A2C File Offset: 0x0004FC2C
		public static double GetPreviousTime<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetPreviousTime();
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x00051A54 File Offset: 0x0004FC54
		public static void SetDone<U>(this U playable, bool value) where U : struct, IPlayable
		{
			playable.GetHandle().SetDone(value);
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x00051A7C File Offset: 0x0004FC7C
		public static bool IsDone<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().IsDone();
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x00051AA4 File Offset: 0x0004FCA4
		public static void SetPropagateSetTime<U>(this U playable, bool value) where U : struct, IPlayable
		{
			playable.GetHandle().SetPropagateSetTime(value);
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x00051ACC File Offset: 0x0004FCCC
		public static bool GetPropagateSetTime<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetPropagateSetTime();
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x00051AF4 File Offset: 0x0004FCF4
		public static bool CanChangeInputs<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().CanChangeInputs();
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x00051B1C File Offset: 0x0004FD1C
		public static bool CanSetWeights<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().CanSetWeights();
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x00051B44 File Offset: 0x0004FD44
		public static bool CanDestroy<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().CanDestroy();
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x00051B6C File Offset: 0x0004FD6C
		public static void SetInputCount<U>(this U playable, int value) where U : struct, IPlayable
		{
			playable.GetHandle().SetInputCount(value);
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x00051B94 File Offset: 0x0004FD94
		public static int GetInputCount<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetInputCount();
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x00051BBC File Offset: 0x0004FDBC
		public static void SetOutputCount<U>(this U playable, int value) where U : struct, IPlayable
		{
			playable.GetHandle().SetOutputCount(value);
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x00051BE4 File Offset: 0x0004FDE4
		public static int GetOutputCount<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetOutputCount();
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x00051C0C File Offset: 0x0004FE0C
		public static Playable GetInput<U>(this U playable, int inputPort) where U : struct, IPlayable
		{
			return playable.GetHandle().GetInput(inputPort);
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x00051C34 File Offset: 0x0004FE34
		public static Playable GetOutput<U>(this U playable, int outputPort) where U : struct, IPlayable
		{
			return playable.GetHandle().GetOutput(outputPort);
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x00051C5C File Offset: 0x0004FE5C
		public static void SetInputWeight<U>(this U playable, int inputIndex, float weight) where U : struct, IPlayable
		{
			playable.GetHandle().SetInputWeight(inputIndex, weight);
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x00051C84 File Offset: 0x0004FE84
		public static void SetInputWeight<U, V>(this U playable, V input, float weight) where U : struct, IPlayable where V : struct, IPlayable
		{
			playable.GetHandle().SetInputWeight(input.GetHandle(), weight);
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x00051CB8 File Offset: 0x0004FEB8
		public static float GetInputWeight<U>(this U playable, int inputIndex) where U : struct, IPlayable
		{
			return playable.GetHandle().GetInputWeight(inputIndex);
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x00051CE0 File Offset: 0x0004FEE0
		public static void ConnectInput<U, V>(this U playable, int inputIndex, V sourcePlayable, int sourceOutputIndex) where U : struct, IPlayable where V : struct, IPlayable
		{
			playable.ConnectInput(inputIndex, sourcePlayable, sourceOutputIndex, 0f);
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x00051CF4 File Offset: 0x0004FEF4
		public static void ConnectInput<U, V>(this U playable, int inputIndex, V sourcePlayable, int sourceOutputIndex, float weight) where U : struct, IPlayable where V : struct, IPlayable
		{
			playable.GetGraph<U>().Connect<V, U>(sourcePlayable, sourceOutputIndex, playable, inputIndex);
			playable.SetInputWeight(inputIndex, weight);
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x00051D20 File Offset: 0x0004FF20
		public static void DisconnectInput<U>(this U playable, int inputPort) where U : struct, IPlayable
		{
			playable.GetGraph<U>().Disconnect<U>(playable, inputPort);
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x00051D40 File Offset: 0x0004FF40
		public static int AddInput<U, V>(this U playable, V sourcePlayable, int sourceOutputIndex, float weight = 0f) where U : struct, IPlayable where V : struct, IPlayable
		{
			int inputCount = playable.GetInputCount<U>();
			playable.SetInputCount(inputCount + 1);
			playable.ConnectInput(inputCount, sourcePlayable, sourceOutputIndex, weight);
			return inputCount;
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x00051D70 File Offset: 0x0004FF70
		[Obsolete("SetDelay is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public static void SetDelay<U>(this U playable, double delay) where U : struct, IPlayable
		{
			playable.GetHandle().SetDelay(delay);
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x00051D98 File Offset: 0x0004FF98
		[Obsolete("GetDelay is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public static double GetDelay<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetDelay();
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x00051DC0 File Offset: 0x0004FFC0
		[Obsolete("IsDelayed is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		public static bool IsDelayed<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().IsDelayed();
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x00051DE8 File Offset: 0x0004FFE8
		public static void SetLeadTime<U>(this U playable, float value) where U : struct, IPlayable
		{
			playable.GetHandle().SetLeadTime(value);
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x00051E10 File Offset: 0x00050010
		public static float GetLeadTime<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetLeadTime();
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x00051E38 File Offset: 0x00050038
		public static PlayableTraversalMode GetTraversalMode<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetTraversalMode();
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x00051E60 File Offset: 0x00050060
		public static void SetTraversalMode<U>(this U playable, PlayableTraversalMode mode) where U : struct, IPlayable
		{
			playable.GetHandle().SetTraversalMode(mode);
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x00051E88 File Offset: 0x00050088
		internal static DirectorWrapMode GetTimeWrapMode<U>(this U playable) where U : struct, IPlayable
		{
			return playable.GetHandle().GetTimeWrapMode();
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x00051EB0 File Offset: 0x000500B0
		internal static void SetTimeWrapMode<U>(this U playable, DirectorWrapMode value) where U : struct, IPlayable
		{
			playable.GetHandle().SetTimeWrapMode(value);
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x00051ED8 File Offset: 0x000500D8
		internal static int GetOutputPortFromInputConnection<U>(this U playable, int inputIndex) where U : struct, IPlayable
		{
			return playable.GetHandle().GetOutputPortFromInputConnection(inputIndex);
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x00051F00 File Offset: 0x00050100
		internal static int GetInputPortFromOutputConnection<U>(this U playable, int outputIndex) where U : struct, IPlayable
		{
			return playable.GetHandle().GetInputPortFromOutputConnection(outputIndex);
		}
	}
}
