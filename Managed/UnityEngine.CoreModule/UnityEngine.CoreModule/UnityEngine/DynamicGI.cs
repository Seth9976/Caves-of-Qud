using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000156 RID: 342
	[NativeHeader("Runtime/GI/DynamicGI.h")]
	public sealed class DynamicGI
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000A9B RID: 2715
		// (set) Token: 0x06000A9C RID: 2716
		public static extern float indirectScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000A9D RID: 2717
		// (set) Token: 0x06000A9E RID: 2718
		public static extern float updateThreshold
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000A9F RID: 2719
		// (set) Token: 0x06000AA0 RID: 2720
		public static extern int materialUpdateTimeSlice
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00013E04 File Offset: 0x00012004
		public static void SetEmissive(Renderer renderer, Color color)
		{
			DynamicGI.SetEmissive_Injected(Object.MarshalledUnityObject.Marshal<Renderer>(renderer), ref color);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00013E20 File Offset: 0x00012020
		[NativeThrows]
		public unsafe static void SetEnvironmentData([NotNull] float[] input)
		{
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(input, "input");
			}
			Span<float> span = new Span<float>(input);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				DynamicGI.SetEnvironmentData_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000AA3 RID: 2723
		// (set) Token: 0x06000AA4 RID: 2724
		public static extern bool synchronousMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000AA5 RID: 2725
		public static extern bool isConverged
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000AA6 RID: 2726
		internal static extern int scheduledMaterialUpdatesCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000AA7 RID: 2727
		// (set) Token: 0x06000AA8 RID: 2728
		internal static extern bool asyncMaterialUpdates
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000AA9 RID: 2729
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UpdateEnvironment();

		// Token: 0x06000AAA RID: 2730 RVA: 0x00002925 File Offset: 0x00000B25
		[Obsolete("DynamicGI.UpdateMaterials(Renderer) is deprecated; instead, use extension method from RendererExtensions: 'renderer.UpdateGIMaterials()' (UnityUpgradable).", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void UpdateMaterials(Renderer renderer)
		{
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00002925 File Offset: 0x00000B25
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("DynamicGI.UpdateMaterials(Terrain) is deprecated; instead, use extension method from TerrainExtensions: 'terrain.UpdateGIMaterials()' (UnityUpgradable).", true)]
		public static void UpdateMaterials(Object renderer)
		{
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00002925 File Offset: 0x00000B25
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("DynamicGI.UpdateMaterials(Terrain, int, int, int, int) is deprecated; instead, use extension method from TerrainExtensions: 'terrain.UpdateGIMaterials(x, y, width, height)' (UnityUpgradable).", true)]
		public static void UpdateMaterials(Object renderer, int x, int y, int width, int height)
		{
		}

		// Token: 0x06000AAE RID: 2734
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetEmissive_Injected(IntPtr renderer, [In] ref Color color);

		// Token: 0x06000AAF RID: 2735
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetEnvironmentData_Injected(ref ManagedSpanWrapper input);
	}
}
