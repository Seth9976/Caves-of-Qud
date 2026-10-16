using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000173 RID: 371
	[NativeHeader("Runtime/Export/Graphics/Graphics.bindings.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class LightProbes : Object
	{
		// Token: 0x06000D4F RID: 3407 RVA: 0x00008C6F File Offset: 0x00006E6F
		private LightProbes()
		{
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000D50 RID: 3408 RVA: 0x00018DAC File Offset: 0x00016FAC
		// (remove) Token: 0x06000D51 RID: 3409 RVA: 0x00018DE0 File Offset: 0x00016FE0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action lightProbesUpdated;

		// Token: 0x06000D52 RID: 3410 RVA: 0x00018E14 File Offset: 0x00017014
		[RequiredByNativeCode]
		private static void Internal_CallLightProbesUpdatedFunction()
		{
			bool flag = LightProbes.lightProbesUpdated != null;
			if (flag)
			{
				LightProbes.lightProbesUpdated();
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000D53 RID: 3411 RVA: 0x00018E3C File Offset: 0x0001703C
		// (remove) Token: 0x06000D54 RID: 3412 RVA: 0x00018E70 File Offset: 0x00017070
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action tetrahedralizationCompleted;

		// Token: 0x06000D55 RID: 3413 RVA: 0x00018EA4 File Offset: 0x000170A4
		[RequiredByNativeCode]
		private static void Internal_CallTetrahedralizationCompletedFunction()
		{
			bool flag = LightProbes.tetrahedralizationCompleted != null;
			if (flag)
			{
				LightProbes.tetrahedralizationCompleted();
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000D56 RID: 3414 RVA: 0x00018ECC File Offset: 0x000170CC
		// (remove) Token: 0x06000D57 RID: 3415 RVA: 0x00018F00 File Offset: 0x00017100
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action needsRetetrahedralization;

		// Token: 0x06000D58 RID: 3416 RVA: 0x00018F34 File Offset: 0x00017134
		[RequiredByNativeCode]
		private static void Internal_CallNeedsRetetrahedralizationFunction()
		{
			bool flag = LightProbes.needsRetetrahedralization != null;
			if (flag)
			{
				LightProbes.needsRetetrahedralization();
			}
		}

		// Token: 0x06000D59 RID: 3417
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Tetrahedralize();

		// Token: 0x06000D5A RID: 3418
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void TetrahedralizeAsync();

		// Token: 0x06000D5B RID: 3419 RVA: 0x00018F5C File Offset: 0x0001715C
		[FreeFunction]
		public static void GetInterpolatedProbe(Vector3 position, Renderer renderer, out SphericalHarmonicsL2 probe)
		{
			LightProbes.GetInterpolatedProbe_Injected(ref position, Object.MarshalledUnityObject.Marshal<Renderer>(renderer), out probe);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00018F78 File Offset: 0x00017178
		[FreeFunction]
		internal static bool AreLightProbesAllowed(Renderer renderer)
		{
			return LightProbes.AreLightProbesAllowed_Injected(Object.MarshalledUnityObject.Marshal<Renderer>(renderer));
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00018F90 File Offset: 0x00017190
		public static void CalculateInterpolatedLightAndOcclusionProbes(Vector3[] positions, SphericalHarmonicsL2[] lightProbes, Vector4[] occlusionProbes)
		{
			bool flag = positions == null;
			if (flag)
			{
				throw new ArgumentNullException("positions");
			}
			bool flag2 = lightProbes == null && occlusionProbes == null;
			if (flag2)
			{
				throw new ArgumentException("Argument lightProbes and occlusionProbes cannot both be null.");
			}
			bool flag3 = lightProbes != null && lightProbes.Length < positions.Length;
			if (flag3)
			{
				throw new ArgumentException("lightProbes", "Argument lightProbes has less elements than positions");
			}
			bool flag4 = occlusionProbes != null && occlusionProbes.Length < positions.Length;
			if (flag4)
			{
				throw new ArgumentException("occlusionProbes", "Argument occlusionProbes has less elements than positions");
			}
			LightProbes.CalculateInterpolatedLightAndOcclusionProbes_Internal(positions, positions.Length, lightProbes, occlusionProbes);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0001901C File Offset: 0x0001721C
		public static void CalculateInterpolatedLightAndOcclusionProbes(List<Vector3> positions, List<SphericalHarmonicsL2> lightProbes, List<Vector4> occlusionProbes)
		{
			bool flag = positions == null;
			if (flag)
			{
				throw new ArgumentNullException("positions");
			}
			bool flag2 = lightProbes == null && occlusionProbes == null;
			if (flag2)
			{
				throw new ArgumentException("Argument lightProbes and occlusionProbes cannot both be null.");
			}
			bool flag3 = lightProbes != null;
			if (flag3)
			{
				NoAllocHelpers.EnsureListElemCount<SphericalHarmonicsL2>(lightProbes, positions.Count);
			}
			bool flag4 = occlusionProbes != null;
			if (flag4)
			{
				NoAllocHelpers.EnsureListElemCount<Vector4>(occlusionProbes, positions.Count);
			}
			LightProbes.CalculateInterpolatedLightAndOcclusionProbes_Internal(NoAllocHelpers.ExtractArrayFromList<Vector3>(positions), positions.Count, NoAllocHelpers.ExtractArrayFromList<SphericalHarmonicsL2>(lightProbes), NoAllocHelpers.ExtractArrayFromList<Vector4>(occlusionProbes));
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x000190A0 File Offset: 0x000172A0
		[FreeFunction]
		[NativeName("CalculateInterpolatedLightAndOcclusionProbes")]
		internal unsafe static void CalculateInterpolatedLightAndOcclusionProbes_Internal(Vector3[] positions, int positionsCount, SphericalHarmonicsL2[] lightProbes, Vector4[] occlusionProbes)
		{
			Span<Vector3> span = new Span<Vector3>(positions);
			fixed (Vector3* ptr = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
				Span<SphericalHarmonicsL2> span2 = new Span<SphericalHarmonicsL2>(lightProbes);
				fixed (SphericalHarmonicsL2* ptr2 = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, span2.Length);
					Span<Vector4> span3 = new Span<Vector4>(occlusionProbes);
					fixed (Vector4* pinnableReference = span3.GetPinnableReference())
					{
						ManagedSpanWrapper managedSpanWrapper3 = new ManagedSpanWrapper((void*)pinnableReference, span3.Length);
						LightProbes.CalculateInterpolatedLightAndOcclusionProbes_Internal_Injected(ref managedSpanWrapper, positionsCount, ref managedSpanWrapper2, ref managedSpanWrapper3);
						ptr = null;
						ptr2 = null;
					}
				}
			}
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0001912C File Offset: 0x0001732C
		[NativeName("GetSharedLightProbesForScene")]
		[FreeFunction]
		public static LightProbes GetSharedLightProbesForScene(Scene scene)
		{
			return Unmarshal.UnmarshalUnityObject<LightProbes>(LightProbes.GetSharedLightProbesForScene_Injected(ref scene));
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00019148 File Offset: 0x00017348
		[NativeName("GetInstantiatedLightProbesForScene")]
		[FreeFunction]
		public static LightProbes GetInstantiatedLightProbesForScene(Scene scene)
		{
			return Unmarshal.UnmarshalUnityObject<LightProbes>(LightProbes.GetInstantiatedLightProbesForScene_Injected(ref scene));
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x00019164 File Offset: 0x00017364
		public Vector3[] positions
		{
			[FreeFunction(HasExplicitThis = true)]
			[NativeName("GetLightProbePositions")]
			get
			{
				Vector3[] array2;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbes>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					BlittableArrayWrapper blittableArrayWrapper;
					LightProbes.get_positions_Injected(intPtr, out blittableArrayWrapper);
				}
				finally
				{
					BlittableArrayWrapper blittableArrayWrapper;
					Vector3[] array;
					blittableArrayWrapper.Unmarshal<Vector3>(ref array);
					array2 = array;
				}
				return array2;
			}
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x000191A8 File Offset: 0x000173A8
		[NativeName("GetLightProbePositionsSelf")]
		[FreeFunction(HasExplicitThis = true)]
		public Vector3[] GetPositionsSelf()
		{
			Vector3[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbes>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				LightProbes.GetPositionsSelf_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Vector3[] array;
				blittableArrayWrapper.Unmarshal<Vector3>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x000191EC File Offset: 0x000173EC
		[NativeName("SetLightProbePositionsSelf")]
		[FreeFunction(HasExplicitThis = true)]
		public unsafe bool SetPositionsSelf(Vector3[] positions, bool checkForDuplicatePositions)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbes>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector3> span = new Span<Vector3>(positions);
			bool flag;
			fixed (Vector3* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				flag = LightProbes.SetPositionsSelf_Injected(intPtr, ref managedSpanWrapper, checkForDuplicatePositions);
			}
			return flag;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x00019238 File Offset: 0x00017438
		// (set) Token: 0x06000D66 RID: 3430 RVA: 0x0001927C File Offset: 0x0001747C
		public unsafe SphericalHarmonicsL2[] bakedProbes
		{
			[FreeFunction(HasExplicitThis = true)]
			[NativeName("GetBakedCoefficients")]
			get
			{
				SphericalHarmonicsL2[] array2;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbes>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					BlittableArrayWrapper blittableArrayWrapper;
					LightProbes.get_bakedProbes_Injected(intPtr, out blittableArrayWrapper);
				}
				finally
				{
					BlittableArrayWrapper blittableArrayWrapper;
					SphericalHarmonicsL2[] array;
					blittableArrayWrapper.Unmarshal<SphericalHarmonicsL2>(ref array);
					array2 = array;
				}
				return array2;
			}
			[NativeName("SetBakedCoefficients")]
			[FreeFunction(HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbes>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Span<SphericalHarmonicsL2> span = new Span<SphericalHarmonicsL2>(value);
				fixed (SphericalHarmonicsL2* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					LightProbes.set_bakedProbes_Injected(intPtr, ref managedSpanWrapper);
				}
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000D67 RID: 3431 RVA: 0x000192C4 File Offset: 0x000174C4
		public int count
		{
			[NativeName("GetLightProbeCount")]
			[FreeFunction(HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbes>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LightProbes.get_count_Injected(intPtr);
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x000192E8 File Offset: 0x000174E8
		public int countSelf
		{
			[NativeName("GetLightProbeCountSelf")]
			[FreeFunction(HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbes>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LightProbes.get_countSelf_Injected(intPtr);
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0001930C File Offset: 0x0001750C
		public int cellCount
		{
			[FreeFunction(HasExplicitThis = true)]
			[NativeName("GetTetrahedraSize")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbes>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LightProbes.get_cellCount_Injected(intPtr);
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00019330 File Offset: 0x00017530
		public int cellCountSelf
		{
			[FreeFunction(HasExplicitThis = true)]
			[NativeName("GetTetrahedraSizeSelf")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LightProbes>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LightProbes.get_cellCountSelf_Injected(intPtr);
			}
		}

		// Token: 0x06000D6B RID: 3435
		[FreeFunction]
		[NativeName("GetLightProbeCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetCount();

		// Token: 0x06000D6C RID: 3436 RVA: 0x00002925 File Offset: 0x00000B25
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use GetInterpolatedProbe instead.", true)]
		public void GetInterpolatedLightProbe(Vector3 position, Renderer renderer, float[] coefficients)
		{
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x00019354 File Offset: 0x00017554
		// (set) Token: 0x06000D6E RID: 3438 RVA: 0x00002925 File Offset: 0x00000B25
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use bakedProbes instead.", true)]
		public float[] coefficients
		{
			get
			{
				return new float[0];
			}
			set
			{
			}
		}

		// Token: 0x06000D6F RID: 3439
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetInterpolatedProbe_Injected([In] ref Vector3 position, IntPtr renderer, out SphericalHarmonicsL2 probe);

		// Token: 0x06000D70 RID: 3440
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool AreLightProbesAllowed_Injected(IntPtr renderer);

		// Token: 0x06000D71 RID: 3441
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateInterpolatedLightAndOcclusionProbes_Internal_Injected(ref ManagedSpanWrapper positions, int positionsCount, ref ManagedSpanWrapper lightProbes, ref ManagedSpanWrapper occlusionProbes);

		// Token: 0x06000D72 RID: 3442
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetSharedLightProbesForScene_Injected([In] ref Scene scene);

		// Token: 0x06000D73 RID: 3443
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetInstantiatedLightProbesForScene_Injected([In] ref Scene scene);

		// Token: 0x06000D74 RID: 3444
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_positions_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x06000D75 RID: 3445
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPositionsSelf_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x06000D76 RID: 3446
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetPositionsSelf_Injected(IntPtr _unity_self, ref ManagedSpanWrapper positions, bool checkForDuplicatePositions);

		// Token: 0x06000D77 RID: 3447
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bakedProbes_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x06000D78 RID: 3448
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bakedProbes_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x06000D79 RID: 3449
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_count_Injected(IntPtr _unity_self);

		// Token: 0x06000D7A RID: 3450
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_countSelf_Injected(IntPtr _unity_self);

		// Token: 0x06000D7B RID: 3451
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_cellCount_Injected(IntPtr _unity_self);

		// Token: 0x06000D7C RID: 3452
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_cellCountSelf_Injected(IntPtr _unity_self);
	}
}
