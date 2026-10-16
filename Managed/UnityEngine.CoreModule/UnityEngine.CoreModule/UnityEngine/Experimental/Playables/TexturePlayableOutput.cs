using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x0200058C RID: 1420
	[NativeHeader("Runtime/Export/Director/TexturePlayableOutput.bindings.h")]
	[NativeHeader("Runtime/Graphics/RenderTexture.h")]
	[StaticAccessor("TexturePlayableOutputBindings", StaticAccessorType.DoubleColon)]
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Graphics/Director/TexturePlayableOutput.h")]
	public struct TexturePlayableOutput : IPlayableOutput
	{
		// Token: 0x060036E5 RID: 14053 RVA: 0x0006C62C File Offset: 0x0006A82C
		public static TexturePlayableOutput Create(PlayableGraph graph, string name, RenderTexture target)
		{
			PlayableOutputHandle playableOutputHandle;
			bool flag = !TexturePlayableGraphExtensions.InternalCreateTextureOutput(ref graph, name, out playableOutputHandle);
			TexturePlayableOutput texturePlayableOutput;
			if (flag)
			{
				texturePlayableOutput = TexturePlayableOutput.Null;
			}
			else
			{
				TexturePlayableOutput texturePlayableOutput2 = new TexturePlayableOutput(playableOutputHandle);
				texturePlayableOutput2.SetTarget(target);
				texturePlayableOutput = texturePlayableOutput2;
			}
			return texturePlayableOutput;
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x0006C66C File Offset: 0x0006A86C
		internal TexturePlayableOutput(PlayableOutputHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOutputOfType<TexturePlayableOutput>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an TexturePlayableOutput.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x060036E7 RID: 14055 RVA: 0x0006C6A8 File Offset: 0x0006A8A8
		public static TexturePlayableOutput Null
		{
			get
			{
				return new TexturePlayableOutput(PlayableOutputHandle.Null);
			}
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x0006C6C4 File Offset: 0x0006A8C4
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x0006C6DC File Offset: 0x0006A8DC
		public static implicit operator PlayableOutput(TexturePlayableOutput output)
		{
			return new PlayableOutput(output.GetHandle());
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x0006C6FC File Offset: 0x0006A8FC
		public static explicit operator TexturePlayableOutput(PlayableOutput output)
		{
			return new TexturePlayableOutput(output.GetHandle());
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x0006C71C File Offset: 0x0006A91C
		public RenderTexture GetTarget()
		{
			return TexturePlayableOutput.InternalGetTarget(ref this.m_Handle);
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x0006C739 File Offset: 0x0006A939
		public void SetTarget(RenderTexture value)
		{
			TexturePlayableOutput.InternalSetTarget(ref this.m_Handle, value);
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x0006C74C File Offset: 0x0006A94C
		[NativeThrows]
		private static RenderTexture InternalGetTarget(ref PlayableOutputHandle output)
		{
			return Unmarshal.UnmarshalUnityObject<RenderTexture>(TexturePlayableOutput.InternalGetTarget_Injected(ref output));
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x0006C764 File Offset: 0x0006A964
		[NativeThrows]
		private static void InternalSetTarget(ref PlayableOutputHandle output, RenderTexture target)
		{
			TexturePlayableOutput.InternalSetTarget_Injected(ref output, Object.MarshalledUnityObject.Marshal<RenderTexture>(target));
		}

		// Token: 0x060036EF RID: 14063
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InternalGetTarget_Injected(ref PlayableOutputHandle output);

		// Token: 0x060036F0 RID: 14064
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetTarget_Injected(ref PlayableOutputHandle output, IntPtr target);

		// Token: 0x040012DF RID: 4831
		private PlayableOutputHandle m_Handle;
	}
}
