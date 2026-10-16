using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000008 RID: 8
	[NativeHeader("Modules/Director/PlayableDirector.h")]
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	[RequiredByNativeCode]
	[HelpURL("https://docs.unity3d.com/ScriptReference/Playables.PlayableDirector.html")]
	public class PlayableDirector : Behaviour, IExposedPropertyTable
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000256C File Offset: 0x0000076C
		public PlayState state
		{
			get
			{
				return this.GetPlayState();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002590 File Offset: 0x00000790
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002584 File Offset: 0x00000784
		public DirectorWrapMode extrapolationMode
		{
			get
			{
				return this.GetWrapMode();
			}
			set
			{
				this.SetWrapMode(value);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000025A8 File Offset: 0x000007A8
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000025C5 File Offset: 0x000007C5
		public PlayableAsset playableAsset
		{
			get
			{
				return this.Internal_GetPlayableAsset() as PlayableAsset;
			}
			set
			{
				this.SetPlayableAsset(value);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000025D0 File Offset: 0x000007D0
		public PlayableGraph playableGraph
		{
			get
			{
				return this.GetGraphHandle();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000025E8 File Offset: 0x000007E8
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002600 File Offset: 0x00000800
		public bool playOnAwake
		{
			get
			{
				return this.GetPlayOnAwake();
			}
			set
			{
				this.SetPlayOnAwake(value);
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000260B File Offset: 0x0000080B
		public void DeferredEvaluate()
		{
			this.EvaluateNextFrame();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002615 File Offset: 0x00000815
		internal void Play(FrameRate frameRate)
		{
			this.PlayOnFrame(frameRate);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002620 File Offset: 0x00000820
		public void Play(PlayableAsset asset)
		{
			bool flag = asset == null;
			if (flag)
			{
				throw new ArgumentNullException("asset");
			}
			this.Play(asset, this.extrapolationMode);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002654 File Offset: 0x00000854
		public void Play(PlayableAsset asset, DirectorWrapMode mode)
		{
			bool flag = asset == null;
			if (flag)
			{
				throw new ArgumentNullException("asset");
			}
			this.playableAsset = asset;
			this.extrapolationMode = mode;
			this.Play();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000268F File Offset: 0x0000088F
		public void SetGenericBinding(Object key, Object value)
		{
			this.Internal_SetGenericBinding(key, value);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000026C0 File Offset: 0x000008C0
		// (set) Token: 0x06000039 RID: 57 RVA: 0x0000269C File Offset: 0x0000089C
		public DirectorUpdateMode timeUpdateMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PlayableDirector.get_timeUpdateMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PlayableDirector.set_timeUpdateMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002708 File Offset: 0x00000908
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000026E4 File Offset: 0x000008E4
		public double time
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PlayableDirector.get_time_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PlayableDirector.set_time_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002750 File Offset: 0x00000950
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000272C File Offset: 0x0000092C
		public double initialTime
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PlayableDirector.get_initialTime_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PlayableDirector.set_initialTime_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002774 File Offset: 0x00000974
		public double duration
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PlayableDirector.get_duration_Injected(intPtr);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002798 File Offset: 0x00000998
		[NativeThrows]
		public void Evaluate()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.Evaluate_Injected(intPtr);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000027BC File Offset: 0x000009BC
		[NativeThrows]
		private void PlayOnFrame(FrameRate frameRate)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.PlayOnFrame_Injected(intPtr, ref frameRate);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000027E0 File Offset: 0x000009E0
		[NativeThrows]
		public void Play()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.Play_Injected(intPtr);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002804 File Offset: 0x00000A04
		public void Stop()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.Stop_Injected(intPtr);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002828 File Offset: 0x00000A28
		public void Pause()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.Pause_Injected(intPtr);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000284C File Offset: 0x00000A4C
		public void Resume()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.Resume_Injected(intPtr);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002870 File Offset: 0x00000A70
		[NativeThrows]
		public void RebuildGraph()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.RebuildGraph_Injected(intPtr);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002894 File Offset: 0x00000A94
		public void ClearReferenceValue(PropertyName id)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.ClearReferenceValue_Injected(intPtr, ref id);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000028B8 File Offset: 0x00000AB8
		public void SetReferenceValue(PropertyName id, Object value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.SetReferenceValue_Injected(intPtr, ref id, Object.MarshalledUnityObject.Marshal<Object>(value));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000028E4 File Offset: 0x00000AE4
		public Object GetReferenceValue(PropertyName id, out bool idValid)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Object>(PlayableDirector.GetReferenceValue_Injected(intPtr, ref id, out idValid));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002910 File Offset: 0x00000B10
		[NativeMethod("GetBindingFor")]
		public Object GetGenericBinding(Object key)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Object>(PlayableDirector.GetGenericBinding_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Object>(key)));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002940 File Offset: 0x00000B40
		[NativeMethod("ClearBindingFor")]
		public void ClearGenericBinding(Object key)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.ClearGenericBinding_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Object>(key));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002968 File Offset: 0x00000B68
		[NativeThrows]
		public void RebindPlayableGraphOutputs()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.RebindPlayableGraphOutputs_Injected(intPtr);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000298C File Offset: 0x00000B8C
		internal void ProcessPendingGraphChanges()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.ProcessPendingGraphChanges_Injected(intPtr);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000029B0 File Offset: 0x00000BB0
		[NativeMethod("HasBinding")]
		internal bool HasGenericBinding(Object key)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return PlayableDirector.HasGenericBinding_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Object>(key));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000029D8 File Offset: 0x00000BD8
		private PlayState GetPlayState()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return PlayableDirector.GetPlayState_Injected(intPtr);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000029FC File Offset: 0x00000BFC
		private void SetWrapMode(DirectorWrapMode mode)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.SetWrapMode_Injected(intPtr, mode);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002A20 File Offset: 0x00000C20
		private DirectorWrapMode GetWrapMode()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return PlayableDirector.GetWrapMode_Injected(intPtr);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A44 File Offset: 0x00000C44
		[NativeThrows]
		private void EvaluateNextFrame()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.EvaluateNextFrame_Injected(intPtr);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002A68 File Offset: 0x00000C68
		private PlayableGraph GetGraphHandle()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableGraph playableGraph;
			PlayableDirector.GetGraphHandle_Injected(intPtr, out playableGraph);
			return playableGraph;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002A90 File Offset: 0x00000C90
		private void SetPlayOnAwake(bool on)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.SetPlayOnAwake_Injected(intPtr, on);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002AB4 File Offset: 0x00000CB4
		private bool GetPlayOnAwake()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return PlayableDirector.GetPlayOnAwake_Injected(intPtr);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002AD8 File Offset: 0x00000CD8
		[NativeThrows]
		private void Internal_SetGenericBinding(Object key, Object value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.Internal_SetGenericBinding_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Object>(key), Object.MarshalledUnityObject.Marshal<Object>(value));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B08 File Offset: 0x00000D08
		private void SetPlayableAsset(ScriptableObject asset)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PlayableDirector.SetPlayableAsset_Injected(intPtr, Object.MarshalledUnityObject.Marshal<ScriptableObject>(asset));
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002B30 File Offset: 0x00000D30
		private ScriptableObject Internal_GetPlayableAsset()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PlayableDirector>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<ScriptableObject>(PlayableDirector.Internal_GetPlayableAsset_Injected(intPtr));
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000059 RID: 89 RVA: 0x00002B58 File Offset: 0x00000D58
		// (remove) Token: 0x0600005A RID: 90 RVA: 0x00002B90 File Offset: 0x00000D90
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<PlayableDirector> played;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600005B RID: 91 RVA: 0x00002BC8 File Offset: 0x00000DC8
		// (remove) Token: 0x0600005C RID: 92 RVA: 0x00002C00 File Offset: 0x00000E00
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<PlayableDirector> paused;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600005D RID: 93 RVA: 0x00002C38 File Offset: 0x00000E38
		// (remove) Token: 0x0600005E RID: 94 RVA: 0x00002C70 File Offset: 0x00000E70
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<PlayableDirector> stopped;

		// Token: 0x0600005F RID: 95
		[StaticAccessor("GetDirectorManager()", StaticAccessorType.Dot)]
		[NativeHeader("Runtime/Director/Core/DirectorManager.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ResetFrameTiming();

		// Token: 0x06000060 RID: 96 RVA: 0x00002CA8 File Offset: 0x00000EA8
		[RequiredByNativeCode]
		private void SendOnPlayableDirectorPlay()
		{
			bool flag = this.played != null;
			if (flag)
			{
				this.played(this);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002CD0 File Offset: 0x00000ED0
		[RequiredByNativeCode]
		private void SendOnPlayableDirectorPause()
		{
			bool flag = this.paused != null;
			if (flag)
			{
				this.paused(this);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002CF8 File Offset: 0x00000EF8
		[RequiredByNativeCode]
		private void SendOnPlayableDirectorStop()
		{
			bool flag = this.stopped != null;
			if (flag)
			{
				this.stopped(this);
			}
		}

		// Token: 0x06000064 RID: 100
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_timeUpdateMode_Injected(IntPtr _unity_self, DirectorUpdateMode value);

		// Token: 0x06000065 RID: 101
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern DirectorUpdateMode get_timeUpdateMode_Injected(IntPtr _unity_self);

		// Token: 0x06000066 RID: 102
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_time_Injected(IntPtr _unity_self, double value);

		// Token: 0x06000067 RID: 103
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double get_time_Injected(IntPtr _unity_self);

		// Token: 0x06000068 RID: 104
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_initialTime_Injected(IntPtr _unity_self, double value);

		// Token: 0x06000069 RID: 105
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double get_initialTime_Injected(IntPtr _unity_self);

		// Token: 0x0600006A RID: 106
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double get_duration_Injected(IntPtr _unity_self);

		// Token: 0x0600006B RID: 107
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Evaluate_Injected(IntPtr _unity_self);

		// Token: 0x0600006C RID: 108
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PlayOnFrame_Injected(IntPtr _unity_self, [In] ref FrameRate frameRate);

		// Token: 0x0600006D RID: 109
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Play_Injected(IntPtr _unity_self);

		// Token: 0x0600006E RID: 110
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop_Injected(IntPtr _unity_self);

		// Token: 0x0600006F RID: 111
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Pause_Injected(IntPtr _unity_self);

		// Token: 0x06000070 RID: 112
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Resume_Injected(IntPtr _unity_self);

		// Token: 0x06000071 RID: 113
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RebuildGraph_Injected(IntPtr _unity_self);

		// Token: 0x06000072 RID: 114
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearReferenceValue_Injected(IntPtr _unity_self, [In] ref PropertyName id);

		// Token: 0x06000073 RID: 115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetReferenceValue_Injected(IntPtr _unity_self, [In] ref PropertyName id, IntPtr value);

		// Token: 0x06000074 RID: 116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetReferenceValue_Injected(IntPtr _unity_self, [In] ref PropertyName id, out bool idValid);

		// Token: 0x06000075 RID: 117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetGenericBinding_Injected(IntPtr _unity_self, IntPtr key);

		// Token: 0x06000076 RID: 118
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearGenericBinding_Injected(IntPtr _unity_self, IntPtr key);

		// Token: 0x06000077 RID: 119
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RebindPlayableGraphOutputs_Injected(IntPtr _unity_self);

		// Token: 0x06000078 RID: 120
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ProcessPendingGraphChanges_Injected(IntPtr _unity_self);

		// Token: 0x06000079 RID: 121
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasGenericBinding_Injected(IntPtr _unity_self, IntPtr key);

		// Token: 0x0600007A RID: 122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PlayState GetPlayState_Injected(IntPtr _unity_self);

		// Token: 0x0600007B RID: 123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetWrapMode_Injected(IntPtr _unity_self, DirectorWrapMode mode);

		// Token: 0x0600007C RID: 124
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern DirectorWrapMode GetWrapMode_Injected(IntPtr _unity_self);

		// Token: 0x0600007D RID: 125
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EvaluateNextFrame_Injected(IntPtr _unity_self);

		// Token: 0x0600007E RID: 126
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGraphHandle_Injected(IntPtr _unity_self, out PlayableGraph ret);

		// Token: 0x0600007F RID: 127
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPlayOnAwake_Injected(IntPtr _unity_self, bool on);

		// Token: 0x06000080 RID: 128
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetPlayOnAwake_Injected(IntPtr _unity_self);

		// Token: 0x06000081 RID: 129
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetGenericBinding_Injected(IntPtr _unity_self, IntPtr key, IntPtr value);

		// Token: 0x06000082 RID: 130
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPlayableAsset_Injected(IntPtr _unity_self, IntPtr asset);

		// Token: 0x06000083 RID: 131
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_GetPlayableAsset_Injected(IntPtr _unity_self);
	}
}
