using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x0200005D RID: 93
	[StaticAccessor("AnimationScriptPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationScriptPlayable.bindings.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	public struct AnimationScriptPlayable : IAnimationJobPlayable, IPlayable, IEquatable<AnimationScriptPlayable>
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x00009C00 File Offset: 0x00007E00
		public static AnimationScriptPlayable Null
		{
			get
			{
				return AnimationScriptPlayable.m_NullPlayable;
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00009C18 File Offset: 0x00007E18
		public static AnimationScriptPlayable Create<T>(PlayableGraph graph, T jobData, int inputCount = 0) where T : struct, IAnimationJob
		{
			PlayableHandle playableHandle = AnimationScriptPlayable.CreateHandle<T>(graph, inputCount);
			AnimationScriptPlayable animationScriptPlayable = new AnimationScriptPlayable(playableHandle);
			animationScriptPlayable.SetJobData<T>(jobData);
			return animationScriptPlayable;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00009C44 File Offset: 0x00007E44
		private static PlayableHandle CreateHandle<T>(PlayableGraph graph, int inputCount) where T : struct, IAnimationJob
		{
			IntPtr jobReflectionData = ProcessAnimationJobStruct<T>.GetJobReflectionData();
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !AnimationScriptPlayable.CreateHandleInternal(graph, ref @null, jobReflectionData);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				@null.SetInputCount(inputCount);
				playableHandle = @null;
			}
			return playableHandle;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00009C84 File Offset: 0x00007E84
		internal AnimationScriptPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<AnimationScriptPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationScriptPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00009CC0 File Offset: 0x00007EC0
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00009CD8 File Offset: 0x00007ED8
		private void CheckJobTypeValidity<T>()
		{
			Type jobType = this.GetHandle().GetJobType();
			bool flag = jobType != typeof(T);
			if (flag)
			{
				throw new ArgumentException(string.Format("Wrong type: the given job type ({0}) is different from the creation job type ({1}).", typeof(T).FullName, jobType.FullName));
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00009D30 File Offset: 0x00007F30
		public unsafe T GetJobData<T>() where T : struct, IAnimationJob
		{
			this.CheckJobTypeValidity<T>();
			T t;
			UnsafeUtility.CopyPtrToStructure<T>((void*)this.GetHandle().GetJobData(), out t);
			return t;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00009D68 File Offset: 0x00007F68
		public unsafe void SetJobData<T>(T jobData) where T : struct, IAnimationJob
		{
			this.CheckJobTypeValidity<T>();
			UnsafeUtility.CopyStructureToPtr<T>(ref jobData, (void*)this.GetHandle().GetJobData());
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00009D98 File Offset: 0x00007F98
		public static implicit operator Playable(AnimationScriptPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00009DB8 File Offset: 0x00007FB8
		public static explicit operator AnimationScriptPlayable(Playable playable)
		{
			return new AnimationScriptPlayable(playable.GetHandle());
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00009DD8 File Offset: 0x00007FD8
		public bool Equals(AnimationScriptPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00009DFC File Offset: 0x00007FFC
		public void SetProcessInputs(bool value)
		{
			AnimationScriptPlayable.SetProcessInputsInternal(this.GetHandle(), value);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00009E0C File Offset: 0x0000800C
		public bool GetProcessInputs()
		{
			return AnimationScriptPlayable.GetProcessInputsInternal(this.GetHandle());
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00009E2C File Offset: 0x0000802C
		[NativeThrows]
		private static bool CreateHandleInternal(PlayableGraph graph, ref PlayableHandle handle, IntPtr jobReflectionData)
		{
			return AnimationScriptPlayable.CreateHandleInternal_Injected(ref graph, ref handle, jobReflectionData);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00009E44 File Offset: 0x00008044
		[NativeThrows]
		private static void SetProcessInputsInternal(PlayableHandle handle, bool value)
		{
			AnimationScriptPlayable.SetProcessInputsInternal_Injected(ref handle, value);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00009E5C File Offset: 0x0000805C
		[NativeThrows]
		private static bool GetProcessInputsInternal(PlayableHandle handle)
		{
			return AnimationScriptPlayable.GetProcessInputsInternal_Injected(ref handle);
		}

		// Token: 0x06000522 RID: 1314
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CreateHandleInternal_Injected([In] ref PlayableGraph graph, ref PlayableHandle handle, IntPtr jobReflectionData);

		// Token: 0x06000523 RID: 1315
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetProcessInputsInternal_Injected([In] ref PlayableHandle handle, bool value);

		// Token: 0x06000524 RID: 1316
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetProcessInputsInternal_Injected([In] ref PlayableHandle handle);

		// Token: 0x04000166 RID: 358
		private PlayableHandle m_Handle;

		// Token: 0x04000167 RID: 359
		private static readonly AnimationScriptPlayable m_NullPlayable = new AnimationScriptPlayable(PlayableHandle.Null);
	}
}
