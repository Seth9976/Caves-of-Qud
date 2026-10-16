using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000458 RID: 1112
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Runtime/Export/Director/PlayableHandle.bindings.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	public struct PlayableHandle : IEquatable<PlayableHandle>
	{
		// Token: 0x06002AD1 RID: 10961 RVA: 0x0005223C File Offset: 0x0005043C
		internal T GetObject<T>() where T : class, IPlayableBehaviour
		{
			bool flag = !this.IsValid();
			T t;
			if (flag)
			{
				t = default(T);
			}
			else
			{
				object scriptInstance = this.GetScriptInstance();
				bool flag2 = scriptInstance == null;
				if (flag2)
				{
					t = default(T);
				}
				else
				{
					t = (T)((object)scriptInstance);
				}
			}
			return t;
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x0005228C File Offset: 0x0005048C
		[VisibleToOtherModules(new string[] { "UnityEngine.DirectorModule" })]
		internal T GetPayload<T>() where T : struct
		{
			bool flag = !this.IsValid();
			T t;
			if (flag)
			{
				t = default(T);
			}
			else
			{
				object scriptInstance = this.GetScriptInstance();
				bool flag2 = scriptInstance == null;
				if (flag2)
				{
					t = default(T);
				}
				else
				{
					t = (T)((object)scriptInstance);
				}
			}
			return t;
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x000522DC File Offset: 0x000504DC
		[VisibleToOtherModules(new string[] { "UnityEngine.DirectorModule" })]
		internal void SetPayload<T>(T payload) where T : struct
		{
			bool flag = !this.IsValid();
			if (!flag)
			{
				this.SetScriptInstance(payload);
			}
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x00052308 File Offset: 0x00050508
		[VisibleToOtherModules]
		internal bool IsPlayableOfType<T>()
		{
			return this.GetPlayableType() == typeof(T);
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06002AD5 RID: 10965 RVA: 0x00052330 File Offset: 0x00050530
		public static PlayableHandle Null
		{
			get
			{
				return PlayableHandle.m_Null;
			}
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x00052348 File Offset: 0x00050548
		internal Playable GetInput(int inputPort)
		{
			return new Playable(this.GetInputHandle(inputPort));
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x00052368 File Offset: 0x00050568
		internal Playable GetOutput(int outputPort)
		{
			return new Playable(this.GetOutputHandle(outputPort));
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x00052388 File Offset: 0x00050588
		internal int GetOutputPortFromInputConnection(int inputPort)
		{
			return this.GetOutputPortFromInputIndex(inputPort);
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x000523A4 File Offset: 0x000505A4
		internal int GetInputPortFromOutputConnection(int inputPort)
		{
			return this.GetInputPortFromOutputIndex(inputPort);
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x000523C0 File Offset: 0x000505C0
		internal bool SetInputWeight(int inputIndex, float weight)
		{
			bool flag = this.CheckInputBounds(inputIndex);
			bool flag2;
			if (flag)
			{
				this.SetInputWeightFromIndex(inputIndex, weight);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x000523EC File Offset: 0x000505EC
		internal float GetInputWeight(int inputIndex)
		{
			bool flag = this.CheckInputBounds(inputIndex);
			float num;
			if (flag)
			{
				num = this.GetInputWeightFromIndex(inputIndex);
			}
			else
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x0005241C File Offset: 0x0005061C
		internal void Destroy()
		{
			this.GetGraph().DestroyPlayable<Playable>(new Playable(this));
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x00052444 File Offset: 0x00050644
		public static bool operator ==(PlayableHandle x, PlayableHandle y)
		{
			return PlayableHandle.CompareVersion(x, y);
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x00052460 File Offset: 0x00050660
		public static bool operator !=(PlayableHandle x, PlayableHandle y)
		{
			return !PlayableHandle.CompareVersion(x, y);
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x0005247C File Offset: 0x0005067C
		public override bool Equals(object p)
		{
			return p is PlayableHandle && this.Equals((PlayableHandle)p);
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x000524A8 File Offset: 0x000506A8
		public bool Equals(PlayableHandle other)
		{
			return PlayableHandle.CompareVersion(this, other);
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x000524C8 File Offset: 0x000506C8
		public override int GetHashCode()
		{
			return this.m_Handle.GetHashCode() ^ this.m_Version.GetHashCode();
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x000524F4 File Offset: 0x000506F4
		internal static bool CompareVersion(PlayableHandle lhs, PlayableHandle rhs)
		{
			return lhs.m_Handle == rhs.m_Handle && lhs.m_Version == rhs.m_Version;
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x0005252C File Offset: 0x0005072C
		internal bool CheckInputBounds(int inputIndex)
		{
			return this.CheckInputBounds(inputIndex, false);
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x00052548 File Offset: 0x00050748
		internal bool CheckInputBounds(int inputIndex, bool acceptAny)
		{
			bool flag = inputIndex == -1 && acceptAny;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				bool flag3 = inputIndex < 0;
				if (flag3)
				{
					throw new IndexOutOfRangeException("Index must be greater than 0");
				}
				bool flag4 = this.GetInputCount() <= inputIndex;
				if (flag4)
				{
					throw new IndexOutOfRangeException(string.Concat(new string[]
					{
						"inputIndex ",
						inputIndex.ToString(),
						" is greater than the number of available inputs (",
						this.GetInputCount().ToString(),
						")."
					}));
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06002AE5 RID: 10981
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsNull();

		// Token: 0x06002AE6 RID: 10982
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsValid();

		// Token: 0x06002AE7 RID: 10983
		[FreeFunction("PlayableHandleBindings::GetPlayableType", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Type GetPlayableType();

		// Token: 0x06002AE8 RID: 10984
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetJobType", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Type GetJobType();

		// Token: 0x06002AE9 RID: 10985
		[FreeFunction("PlayableHandleBindings::SetScriptInstance", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetScriptInstance(object scriptInstance);

		// Token: 0x06002AEA RID: 10986
		[FreeFunction("PlayableHandleBindings::CanChangeInputs", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool CanChangeInputs();

		// Token: 0x06002AEB RID: 10987
		[FreeFunction("PlayableHandleBindings::CanSetWeights", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool CanSetWeights();

		// Token: 0x06002AEC RID: 10988
		[FreeFunction("PlayableHandleBindings::CanDestroy", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool CanDestroy();

		// Token: 0x06002AED RID: 10989
		[FreeFunction("PlayableHandleBindings::GetPlayState", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern PlayState GetPlayState();

		// Token: 0x06002AEE RID: 10990
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::Play", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Play();

		// Token: 0x06002AEF RID: 10991
		[FreeFunction("PlayableHandleBindings::Pause", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void Pause();

		// Token: 0x06002AF0 RID: 10992
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetSpeed", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern double GetSpeed();

		// Token: 0x06002AF1 RID: 10993
		[FreeFunction("PlayableHandleBindings::SetSpeed", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetSpeed(double value);

		// Token: 0x06002AF2 RID: 10994
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetTime", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern double GetTime();

		// Token: 0x06002AF3 RID: 10995
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetTime", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetTime(double value);

		// Token: 0x06002AF4 RID: 10996
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::IsDone", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsDone();

		// Token: 0x06002AF5 RID: 10997
		[FreeFunction("PlayableHandleBindings::SetDone", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetDone(bool value);

		// Token: 0x06002AF6 RID: 10998
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetDuration", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern double GetDuration();

		// Token: 0x06002AF7 RID: 10999
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetDuration", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetDuration(double value);

		// Token: 0x06002AF8 RID: 11000
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetPropagateSetTime", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool GetPropagateSetTime();

		// Token: 0x06002AF9 RID: 11001
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetPropagateSetTime", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetPropagateSetTime(bool value);

		// Token: 0x06002AFA RID: 11002 RVA: 0x000525D4 File Offset: 0x000507D4
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetGraph", HasExplicitThis = true, ThrowsException = true)]
		internal PlayableGraph GetGraph()
		{
			PlayableGraph playableGraph;
			PlayableHandle.GetGraph_Injected(ref this, out playableGraph);
			return playableGraph;
		}

		// Token: 0x06002AFB RID: 11003
		[FreeFunction("PlayableHandleBindings::GetInputCount", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetInputCount();

		// Token: 0x06002AFC RID: 11004
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetOutputPortFromInputIndex", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetOutputPortFromInputIndex(int index);

		// Token: 0x06002AFD RID: 11005
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetInputPortFromOutputIndex", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetInputPortFromOutputIndex(int index);

		// Token: 0x06002AFE RID: 11006
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetInputCount", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetInputCount(int value);

		// Token: 0x06002AFF RID: 11007
		[FreeFunction("PlayableHandleBindings::GetOutputCount", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetOutputCount();

		// Token: 0x06002B00 RID: 11008
		[FreeFunction("PlayableHandleBindings::SetOutputCount", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetOutputCount(int value);

		// Token: 0x06002B01 RID: 11009 RVA: 0x000525EC File Offset: 0x000507EC
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetInputWeight", HasExplicitThis = true, ThrowsException = true)]
		internal void SetInputWeight(PlayableHandle input, float weight)
		{
			PlayableHandle.SetInputWeight_Injected(ref this, ref input, weight);
		}

		// Token: 0x06002B02 RID: 11010
		[FreeFunction("PlayableHandleBindings::SetDelay", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetDelay(double delay);

		// Token: 0x06002B03 RID: 11011
		[FreeFunction("PlayableHandleBindings::GetDelay", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern double GetDelay();

		// Token: 0x06002B04 RID: 11012
		[FreeFunction("PlayableHandleBindings::IsDelayed", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsDelayed();

		// Token: 0x06002B05 RID: 11013
		[FreeFunction("PlayableHandleBindings::GetPreviousTime", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern double GetPreviousTime();

		// Token: 0x06002B06 RID: 11014
		[FreeFunction("PlayableHandleBindings::SetLeadTime", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetLeadTime(float value);

		// Token: 0x06002B07 RID: 11015
		[FreeFunction("PlayableHandleBindings::GetLeadTime", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern float GetLeadTime();

		// Token: 0x06002B08 RID: 11016
		[FreeFunction("PlayableHandleBindings::GetTraversalMode", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern PlayableTraversalMode GetTraversalMode();

		// Token: 0x06002B09 RID: 11017
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::SetTraversalMode", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetTraversalMode(PlayableTraversalMode mode);

		// Token: 0x06002B0A RID: 11018
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetJobData", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetJobData();

		// Token: 0x06002B0B RID: 11019
		[VisibleToOtherModules]
		[FreeFunction("PlayableHandleBindings::GetTimeWrapMode", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern DirectorWrapMode GetTimeWrapMode();

		// Token: 0x06002B0C RID: 11020
		[FreeFunction("PlayableHandleBindings::SetTimeWrapMode", HasExplicitThis = true, ThrowsException = true)]
		[VisibleToOtherModules]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetTimeWrapMode(DirectorWrapMode mode);

		// Token: 0x06002B0D RID: 11021
		[FreeFunction("PlayableHandleBindings::GetScriptInstance", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object GetScriptInstance();

		// Token: 0x06002B0E RID: 11022 RVA: 0x00052604 File Offset: 0x00050804
		[FreeFunction("PlayableHandleBindings::GetInputHandle", HasExplicitThis = true, ThrowsException = true)]
		private PlayableHandle GetInputHandle(int index)
		{
			PlayableHandle playableHandle;
			PlayableHandle.GetInputHandle_Injected(ref this, index, out playableHandle);
			return playableHandle;
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x0005261C File Offset: 0x0005081C
		[FreeFunction("PlayableHandleBindings::GetOutputHandle", HasExplicitThis = true, ThrowsException = true)]
		private PlayableHandle GetOutputHandle(int index)
		{
			PlayableHandle playableHandle;
			PlayableHandle.GetOutputHandle_Injected(ref this, index, out playableHandle);
			return playableHandle;
		}

		// Token: 0x06002B10 RID: 11024
		[FreeFunction("PlayableHandleBindings::SetInputWeightFromIndex", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetInputWeightFromIndex(int index, float weight);

		// Token: 0x06002B11 RID: 11025
		[FreeFunction("PlayableHandleBindings::GetInputWeightFromIndex", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetInputWeightFromIndex(int index);

		// Token: 0x06002B13 RID: 11027
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGraph_Injected(ref PlayableHandle _unity_self, out PlayableGraph ret);

		// Token: 0x06002B14 RID: 11028
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInputWeight_Injected(ref PlayableHandle _unity_self, [In] ref PlayableHandle input, float weight);

		// Token: 0x06002B15 RID: 11029
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetInputHandle_Injected(ref PlayableHandle _unity_self, int index, out PlayableHandle ret);

		// Token: 0x06002B16 RID: 11030
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOutputHandle_Injected(ref PlayableHandle _unity_self, int index, out PlayableHandle ret);

		// Token: 0x04000C35 RID: 3125
		internal IntPtr m_Handle;

		// Token: 0x04000C36 RID: 3126
		internal uint m_Version;

		// Token: 0x04000C37 RID: 3127
		private static readonly PlayableHandle m_Null = default(PlayableHandle);
	}
}
