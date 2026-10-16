using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200005A RID: 90
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationPlayableOutput.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableGraph.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	[StaticAccessor("AnimationPlayableOutputBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	[NativeHeader("Modules/Animation/Animator.h")]
	[NativeHeader("Modules/Animation/Director/AnimationPlayableOutput.h")]
	public struct AnimationPlayableOutput : IPlayableOutput
	{
		// Token: 0x060004E3 RID: 1251 RVA: 0x00009770 File Offset: 0x00007970
		public static AnimationPlayableOutput Create(PlayableGraph graph, string name, Animator target)
		{
			PlayableOutputHandle playableOutputHandle;
			bool flag = !AnimationPlayableGraphExtensions.InternalCreateAnimationOutput(ref graph, name, out playableOutputHandle);
			AnimationPlayableOutput animationPlayableOutput;
			if (flag)
			{
				animationPlayableOutput = AnimationPlayableOutput.Null;
			}
			else
			{
				AnimationPlayableOutput animationPlayableOutput2 = new AnimationPlayableOutput(playableOutputHandle);
				animationPlayableOutput2.SetTarget(target);
				animationPlayableOutput = animationPlayableOutput2;
			}
			return animationPlayableOutput;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000097B0 File Offset: 0x000079B0
		internal AnimationPlayableOutput(PlayableOutputHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOutputOfType<AnimationPlayableOutput>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an AnimationPlayableOutput.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x000097EC File Offset: 0x000079EC
		public static AnimationPlayableOutput Null
		{
			get
			{
				return new AnimationPlayableOutput(PlayableOutputHandle.Null);
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00009808 File Offset: 0x00007A08
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00009820 File Offset: 0x00007A20
		public static implicit operator PlayableOutput(AnimationPlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00009840 File Offset: 0x00007A40
		public static explicit operator AnimationPlayableOutput(PlayableOutput output)
		{
			return new AnimationPlayableOutput(output.GetHandle());
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00009860 File Offset: 0x00007A60
		public Animator GetTarget()
		{
			return AnimationPlayableOutput.InternalGetTarget(ref this.m_Handle);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000987D File Offset: 0x00007A7D
		public void SetTarget(Animator value)
		{
			AnimationPlayableOutput.InternalSetTarget(ref this.m_Handle, value);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00009890 File Offset: 0x00007A90
		[NativeThrows]
		private static Animator InternalGetTarget(ref PlayableOutputHandle handle)
		{
			return Unmarshal.UnmarshalUnityObject<Animator>(AnimationPlayableOutput.InternalGetTarget_Injected(ref handle));
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000098A8 File Offset: 0x00007AA8
		[NativeThrows]
		private static void InternalSetTarget(ref PlayableOutputHandle handle, Animator target)
		{
			AnimationPlayableOutput.InternalSetTarget_Injected(ref handle, Object.MarshalledUnityObject.Marshal<Animator>(target));
		}

		// Token: 0x060004ED RID: 1261
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalGetTarget_Injected(ref PlayableOutputHandle handle);

		// Token: 0x060004EE RID: 1262
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetTarget_Injected(ref PlayableOutputHandle handle, IntPtr target);

		// Token: 0x04000161 RID: 353
		private PlayableOutputHandle m_Handle;
	}
}
