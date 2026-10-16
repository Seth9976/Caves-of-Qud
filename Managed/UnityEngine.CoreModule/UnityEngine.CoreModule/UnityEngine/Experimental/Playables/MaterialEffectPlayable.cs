using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x02000588 RID: 1416
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[StaticAccessor("MaterialEffectPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Export/Director/MaterialEffectPlayable.bindings.h")]
	[NativeHeader("Runtime/Shaders/Director/MaterialEffectPlayable.h")]
	[RequiredByNativeCode]
	public struct MaterialEffectPlayable : IPlayable, IEquatable<MaterialEffectPlayable>
	{
		// Token: 0x060036C5 RID: 14021 RVA: 0x0006C2B8 File Offset: 0x0006A4B8
		public static MaterialEffectPlayable Create(PlayableGraph graph, Material material, int pass = -1)
		{
			PlayableHandle playableHandle = MaterialEffectPlayable.CreateHandle(graph, material, pass);
			return new MaterialEffectPlayable(playableHandle);
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x0006C2DC File Offset: 0x0006A4DC
		private static PlayableHandle CreateHandle(PlayableGraph graph, Material material, int pass)
		{
			PlayableHandle @null = PlayableHandle.Null;
			bool flag = !MaterialEffectPlayable.InternalCreateMaterialEffectPlayable(ref graph, material, pass, ref @null);
			PlayableHandle playableHandle;
			if (flag)
			{
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				playableHandle = @null;
			}
			return playableHandle;
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x0006C310 File Offset: 0x0006A510
		internal MaterialEffectPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !handle.IsPlayableOfType<MaterialEffectPlayable>();
				if (flag2)
				{
					throw new InvalidCastException("Can't set handle: the playable is not an MaterialEffectPlayable.");
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x0006C34C File Offset: 0x0006A54C
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x0006C364 File Offset: 0x0006A564
		public static implicit operator Playable(MaterialEffectPlayable playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x0006C384 File Offset: 0x0006A584
		public static explicit operator MaterialEffectPlayable(Playable playable)
		{
			return new MaterialEffectPlayable(playable.GetHandle());
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x0006C3A4 File Offset: 0x0006A5A4
		public bool Equals(MaterialEffectPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x0006C3C8 File Offset: 0x0006A5C8
		public Material GetMaterial()
		{
			return MaterialEffectPlayable.GetMaterialInternal(ref this.m_Handle);
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x0006C3E5 File Offset: 0x0006A5E5
		public void SetMaterial(Material value)
		{
			MaterialEffectPlayable.SetMaterialInternal(ref this.m_Handle, value);
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x0006C3F8 File Offset: 0x0006A5F8
		public int GetPass()
		{
			return MaterialEffectPlayable.GetPassInternal(ref this.m_Handle);
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x0006C415 File Offset: 0x0006A615
		public void SetPass(int value)
		{
			MaterialEffectPlayable.SetPassInternal(ref this.m_Handle, value);
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x0006C428 File Offset: 0x0006A628
		[NativeThrows]
		private static Material GetMaterialInternal(ref PlayableHandle hdl)
		{
			return Unmarshal.UnmarshalUnityObject<Material>(MaterialEffectPlayable.GetMaterialInternal_Injected(ref hdl));
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x0006C440 File Offset: 0x0006A640
		[NativeThrows]
		private static void SetMaterialInternal(ref PlayableHandle hdl, Material material)
		{
			MaterialEffectPlayable.SetMaterialInternal_Injected(ref hdl, Object.MarshalledUnityObject.Marshal<Material>(material));
		}

		// Token: 0x060036D2 RID: 14034
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPassInternal(ref PlayableHandle hdl);

		// Token: 0x060036D3 RID: 14035
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPassInternal(ref PlayableHandle hdl, int pass);

		// Token: 0x060036D4 RID: 14036 RVA: 0x0006C45C File Offset: 0x0006A65C
		[NativeThrows]
		private static bool InternalCreateMaterialEffectPlayable(ref PlayableGraph graph, Material material, int pass, ref PlayableHandle handle)
		{
			return MaterialEffectPlayable.InternalCreateMaterialEffectPlayable_Injected(ref graph, Object.MarshalledUnityObject.Marshal<Material>(material), pass, ref handle);
		}

		// Token: 0x060036D5 RID: 14037
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ValidateType(ref PlayableHandle hdl);

		// Token: 0x060036D6 RID: 14038
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetMaterialInternal_Injected(ref PlayableHandle hdl);

		// Token: 0x060036D7 RID: 14039
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMaterialInternal_Injected(ref PlayableHandle hdl, IntPtr material);

		// Token: 0x060036D8 RID: 14040
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InternalCreateMaterialEffectPlayable_Injected(ref PlayableGraph graph, IntPtr material, int pass, ref PlayableHandle handle);

		// Token: 0x040012DD RID: 4829
		private PlayableHandle m_Handle;
	}
}
