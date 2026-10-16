using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000158 RID: 344
	[NativeHeader("Runtime/Export/Gizmos/Gizmos.bindings.h")]
	[StaticAccessor("GizmoBindings", StaticAccessorType.DoubleColon)]
	public sealed class Gizmos
	{
		// Token: 0x06000ABF RID: 2751 RVA: 0x00013F50 File Offset: 0x00012150
		[NativeThrows]
		public static void DrawLine(Vector3 from, Vector3 to)
		{
			Gizmos.DrawLine_Injected(ref from, ref to);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00013F68 File Offset: 0x00012168
		[NativeThrows]
		public unsafe static void DrawLineStrip(ReadOnlySpan<Vector3> points, bool looped)
		{
			ReadOnlySpan<Vector3> readOnlySpan = points;
			fixed (Vector3* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				Gizmos.DrawLineStrip_Injected(ref managedSpanWrapper, looped);
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00013F9C File Offset: 0x0001219C
		[NativeMethod(Name = "DrawLineList", ThrowsException = true)]
		internal unsafe static void DrawLineListInternal(ReadOnlySpan<Vector3> points)
		{
			ReadOnlySpan<Vector3> readOnlySpan = points;
			fixed (Vector3* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				Gizmos.DrawLineListInternal_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00013FD0 File Offset: 0x000121D0
		public static void DrawLineList(ReadOnlySpan<Vector3> points)
		{
			bool flag = (points.Length & 1) != 0;
			if (flag)
			{
				throw new UnityException("You cannot draw a line list from an odd number of points, with two points per line the number of points must be even");
			}
			Gizmos.DrawLineListInternal(points);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00014000 File Offset: 0x00012200
		[NativeThrows]
		public static void DrawWireSphere(Vector3 center, float radius)
		{
			Gizmos.DrawWireSphere_Injected(ref center, radius);
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00014018 File Offset: 0x00012218
		[NativeThrows]
		public static void DrawSphere(Vector3 center, float radius)
		{
			Gizmos.DrawSphere_Injected(ref center, radius);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00014030 File Offset: 0x00012230
		[NativeThrows]
		public static void DrawWireCube(Vector3 center, Vector3 size)
		{
			Gizmos.DrawWireCube_Injected(ref center, ref size);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00014048 File Offset: 0x00012248
		[NativeThrows]
		public static void DrawCube(Vector3 center, Vector3 size)
		{
			Gizmos.DrawCube_Injected(ref center, ref size);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00014060 File Offset: 0x00012260
		[NativeThrows]
		public static void DrawMesh(Mesh mesh, int submeshIndex, [DefaultValue("Vector3.zero")] Vector3 position, [DefaultValue("Quaternion.identity")] Quaternion rotation, [DefaultValue("Vector3.one")] Vector3 scale)
		{
			Gizmos.DrawMesh_Injected(Object.MarshalledUnityObject.Marshal<Mesh>(mesh), submeshIndex, ref position, ref rotation, ref scale);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00014080 File Offset: 0x00012280
		[NativeThrows]
		public static void DrawWireMesh(Mesh mesh, int submeshIndex, [DefaultValue("Vector3.zero")] Vector3 position, [DefaultValue("Quaternion.identity")] Quaternion rotation, [DefaultValue("Vector3.one")] Vector3 scale)
		{
			Gizmos.DrawWireMesh_Injected(Object.MarshalledUnityObject.Marshal<Mesh>(mesh), submeshIndex, ref position, ref rotation, ref scale);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0001409F File Offset: 0x0001229F
		[NativeThrows]
		public static void DrawIcon(Vector3 center, string name, [DefaultValue("true")] bool allowScaling)
		{
			Gizmos.DrawIcon(center, name, allowScaling, Color.white);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000140B0 File Offset: 0x000122B0
		[NativeThrows]
		public unsafe static void DrawIcon(Vector3 center, string name, [DefaultValue("true")] bool allowScaling, [DefaultValue("Color(255,255,255,255)")] Color tint)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Gizmos.DrawIcon_Injected(ref center, ref managedSpanWrapper, allowScaling, ref tint);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0001410C File Offset: 0x0001230C
		[NativeThrows]
		public static void DrawGUITexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] Material mat)
		{
			Gizmos.DrawGUITexture_Injected(ref screenRect, Object.MarshalledUnityObject.Marshal<Texture>(texture), leftBorder, rightBorder, topBorder, bottomBorder, Object.MarshalledUnityObject.Marshal<Material>(mat));
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00014134 File Offset: 0x00012334
		// (set) Token: 0x06000ACD RID: 2765 RVA: 0x0001414C File Offset: 0x0001234C
		public static Color color
		{
			get
			{
				Color color;
				Gizmos.get_color_Injected(out color);
				return color;
			}
			set
			{
				Gizmos.set_color_Injected(ref value);
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00014160 File Offset: 0x00012360
		// (set) Token: 0x06000ACF RID: 2767 RVA: 0x00014178 File Offset: 0x00012378
		public static Matrix4x4 matrix
		{
			get
			{
				Matrix4x4 matrix4x;
				Gizmos.get_matrix_Injected(out matrix4x);
				return matrix4x;
			}
			set
			{
				Gizmos.set_matrix_Injected(ref value);
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0001418C File Offset: 0x0001238C
		// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x000141A4 File Offset: 0x000123A4
		public static Texture exposure
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<Texture>(Gizmos.get_exposure_Injected());
			}
			set
			{
				Gizmos.set_exposure_Injected(Object.MarshalledUnityObject.Marshal<Texture>(value));
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000AD2 RID: 2770
		public static extern float probeSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x000141BC File Offset: 0x000123BC
		public static void DrawFrustum(Vector3 center, float fov, float maxRange, float minRange, float aspect)
		{
			Gizmos.DrawFrustum_Injected(ref center, fov, maxRange, minRange, aspect);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x000141D8 File Offset: 0x000123D8
		public static float CalculateLOD(Vector3 position, float radius)
		{
			return Gizmos.CalculateLOD_Injected(ref position, radius);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x000141ED File Offset: 0x000123ED
		public static void DrawRay(Ray r)
		{
			Gizmos.DrawLine(r.origin, r.origin + r.direction);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00014210 File Offset: 0x00012410
		public static void DrawRay(Vector3 from, Vector3 direction)
		{
			Gizmos.DrawLine(from, from + direction);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00014224 File Offset: 0x00012424
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation)
		{
			Vector3 one = Vector3.one;
			Gizmos.DrawMesh(mesh, position, rotation, one);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00014244 File Offset: 0x00012444
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, Vector3 position)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Gizmos.DrawMesh(mesh, position, identity, one);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00014268 File Offset: 0x00012468
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Vector3 zero = Vector3.zero;
			Gizmos.DrawMesh(mesh, zero, identity, one);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00014292 File Offset: 0x00012492
		public static void DrawMesh(Mesh mesh, [DefaultValue("Vector3.zero")] Vector3 position, [DefaultValue("Quaternion.identity")] Quaternion rotation, [DefaultValue("Vector3.one")] Vector3 scale)
		{
			Gizmos.DrawMesh(mesh, -1, position, rotation, scale);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000142A0 File Offset: 0x000124A0
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, int submeshIndex, Vector3 position, Quaternion rotation)
		{
			Vector3 one = Vector3.one;
			Gizmos.DrawMesh(mesh, submeshIndex, position, rotation, one);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000142C0 File Offset: 0x000124C0
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, int submeshIndex, Vector3 position)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Gizmos.DrawMesh(mesh, submeshIndex, position, identity, one);
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x000142E8 File Offset: 0x000124E8
		[ExcludeFromDocs]
		public static void DrawMesh(Mesh mesh, int submeshIndex)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Vector3 zero = Vector3.zero;
			Gizmos.DrawMesh(mesh, submeshIndex, zero, identity, one);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00014314 File Offset: 0x00012514
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, Vector3 position, Quaternion rotation)
		{
			Vector3 one = Vector3.one;
			Gizmos.DrawWireMesh(mesh, position, rotation, one);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00014334 File Offset: 0x00012534
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, Vector3 position)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Gizmos.DrawWireMesh(mesh, position, identity, one);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00014358 File Offset: 0x00012558
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Vector3 zero = Vector3.zero;
			Gizmos.DrawWireMesh(mesh, zero, identity, one);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00014382 File Offset: 0x00012582
		public static void DrawWireMesh(Mesh mesh, [DefaultValue("Vector3.zero")] Vector3 position, [DefaultValue("Quaternion.identity")] Quaternion rotation, [DefaultValue("Vector3.one")] Vector3 scale)
		{
			Gizmos.DrawWireMesh(mesh, -1, position, rotation, scale);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x00014390 File Offset: 0x00012590
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, int submeshIndex, Vector3 position, Quaternion rotation)
		{
			Vector3 one = Vector3.one;
			Gizmos.DrawWireMesh(mesh, submeshIndex, position, rotation, one);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x000143B0 File Offset: 0x000125B0
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, int submeshIndex, Vector3 position)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Gizmos.DrawWireMesh(mesh, submeshIndex, position, identity, one);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x000143D8 File Offset: 0x000125D8
		[ExcludeFromDocs]
		public static void DrawWireMesh(Mesh mesh, int submeshIndex)
		{
			Vector3 one = Vector3.one;
			Quaternion identity = Quaternion.identity;
			Vector3 zero = Vector3.zero;
			Gizmos.DrawWireMesh(mesh, submeshIndex, zero, identity, one);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00014404 File Offset: 0x00012604
		[ExcludeFromDocs]
		public static void DrawIcon(Vector3 center, string name)
		{
			bool flag = true;
			Gizmos.DrawIcon(center, name, flag);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00014420 File Offset: 0x00012620
		[ExcludeFromDocs]
		public static void DrawGUITexture(Rect screenRect, Texture texture)
		{
			Material material = null;
			Gizmos.DrawGUITexture(screenRect, texture, material);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00014439 File Offset: 0x00012639
		public static void DrawGUITexture(Rect screenRect, Texture texture, [DefaultValue("null")] Material mat)
		{
			Gizmos.DrawGUITexture(screenRect, texture, 0, 0, 0, 0, mat);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0001444C File Offset: 0x0001264C
		[ExcludeFromDocs]
		public static void DrawGUITexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
		{
			Material material = null;
			Gizmos.DrawGUITexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, material);
		}

		// Token: 0x06000AEA RID: 2794
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawLine_Injected([In] ref Vector3 from, [In] ref Vector3 to);

		// Token: 0x06000AEB RID: 2795
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawLineStrip_Injected(ref ManagedSpanWrapper points, bool looped);

		// Token: 0x06000AEC RID: 2796
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawLineListInternal_Injected(ref ManagedSpanWrapper points);

		// Token: 0x06000AED RID: 2797
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawWireSphere_Injected([In] ref Vector3 center, float radius);

		// Token: 0x06000AEE RID: 2798
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawSphere_Injected([In] ref Vector3 center, float radius);

		// Token: 0x06000AEF RID: 2799
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawWireCube_Injected([In] ref Vector3 center, [In] ref Vector3 size);

		// Token: 0x06000AF0 RID: 2800
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawCube_Injected([In] ref Vector3 center, [In] ref Vector3 size);

		// Token: 0x06000AF1 RID: 2801
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawMesh_Injected(IntPtr mesh, int submeshIndex, [DefaultValue("Vector3.zero")] [In] ref Vector3 position, [DefaultValue("Quaternion.identity")] [In] ref Quaternion rotation, [DefaultValue("Vector3.one")] [In] ref Vector3 scale);

		// Token: 0x06000AF2 RID: 2802
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawWireMesh_Injected(IntPtr mesh, int submeshIndex, [DefaultValue("Vector3.zero")] [In] ref Vector3 position, [DefaultValue("Quaternion.identity")] [In] ref Quaternion rotation, [DefaultValue("Vector3.one")] [In] ref Vector3 scale);

		// Token: 0x06000AF3 RID: 2803
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawIcon_Injected([In] ref Vector3 center, ref ManagedSpanWrapper name, [DefaultValue("true")] bool allowScaling, [DefaultValue("Color(255,255,255,255)")] [In] ref Color tint);

		// Token: 0x06000AF4 RID: 2804
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawGUITexture_Injected([In] ref Rect screenRect, IntPtr texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] IntPtr mat);

		// Token: 0x06000AF5 RID: 2805
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_color_Injected(out Color ret);

		// Token: 0x06000AF6 RID: 2806
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_color_Injected([In] ref Color value);

		// Token: 0x06000AF7 RID: 2807
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_matrix_Injected(out Matrix4x4 ret);

		// Token: 0x06000AF8 RID: 2808
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_matrix_Injected([In] ref Matrix4x4 value);

		// Token: 0x06000AF9 RID: 2809
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_exposure_Injected();

		// Token: 0x06000AFA RID: 2810
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_exposure_Injected(IntPtr value);

		// Token: 0x06000AFB RID: 2811
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawFrustum_Injected([In] ref Vector3 center, float fov, float maxRange, float minRange, float aspect);

		// Token: 0x06000AFC RID: 2812
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float CalculateLOD_Injected([In] ref Vector3 position, float radius);
	}
}
