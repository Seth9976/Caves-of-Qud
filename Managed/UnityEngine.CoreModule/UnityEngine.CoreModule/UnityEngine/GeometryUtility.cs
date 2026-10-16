using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200014D RID: 333
	[StaticAccessor("GeometryUtilityScripting", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public sealed class GeometryUtility
	{
		// Token: 0x060009E0 RID: 2528 RVA: 0x00011F38 File Offset: 0x00010138
		public static Plane[] CalculateFrustumPlanes(Camera camera)
		{
			Plane[] array = new Plane[6];
			GeometryUtility.CalculateFrustumPlanes(camera, array);
			return array;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00011F5C File Offset: 0x0001015C
		public static Plane[] CalculateFrustumPlanes(Matrix4x4 worldToProjectionMatrix)
		{
			Plane[] array = new Plane[6];
			GeometryUtility.CalculateFrustumPlanes(worldToProjectionMatrix, array);
			return array;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00011F7E File Offset: 0x0001017E
		public static void CalculateFrustumPlanes(Camera camera, Plane[] planes)
		{
			GeometryUtility.CalculateFrustumPlanes(camera.projectionMatrix * camera.worldToCameraMatrix, planes);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00011F9C File Offset: 0x0001019C
		public static void CalculateFrustumPlanes(Matrix4x4 worldToProjectionMatrix, Plane[] planes)
		{
			bool flag = planes == null;
			if (flag)
			{
				throw new ArgumentNullException("planes");
			}
			bool flag2 = planes.Length != 6;
			if (flag2)
			{
				throw new ArgumentException("Planes array must be of length 6.", "planes");
			}
			GeometryUtility.Internal_ExtractPlanes(planes, worldToProjectionMatrix);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00011FE4 File Offset: 0x000101E4
		public static Bounds CalculateBounds(Vector3[] positions, Matrix4x4 transform)
		{
			bool flag = positions == null;
			if (flag)
			{
				throw new ArgumentNullException("positions");
			}
			bool flag2 = positions.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.", "positions");
			}
			return GeometryUtility.Internal_CalculateBounds(positions, transform);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0001202C File Offset: 0x0001022C
		public static bool TryCreatePlaneFromPolygon(Vector3[] vertices, out Plane plane)
		{
			bool flag = vertices == null || vertices.Length < 3;
			bool flag2;
			if (flag)
			{
				plane = new Plane(Vector3.up, 0f);
				flag2 = false;
			}
			else
			{
				bool flag3 = vertices.Length == 3;
				if (flag3)
				{
					Vector3 vector = vertices[0];
					Vector3 vector2 = vertices[1];
					Vector3 vector3 = vertices[2];
					plane = new Plane(vector, vector2, vector3);
					flag2 = plane.normal.sqrMagnitude > 0f;
				}
				else
				{
					Vector3 zero = Vector3.zero;
					int num = vertices.Length - 1;
					Vector3 vector4 = vertices[num];
					foreach (Vector3 vector5 in vertices)
					{
						zero.x += (vector4.y - vector5.y) * (vector4.z + vector5.z);
						zero.y += (vector4.z - vector5.z) * (vector4.x + vector5.x);
						zero.z += (vector4.x - vector5.x) * (vector4.y + vector5.y);
						vector4 = vector5;
					}
					zero.Normalize();
					float num2 = 0f;
					foreach (Vector3 vector6 in vertices)
					{
						num2 -= Vector3.Dot(zero, vector6);
					}
					num2 /= (float)vertices.Length;
					plane = new Plane(zero, num2);
					flag2 = plane.normal.sqrMagnitude > 0f;
				}
			}
			return flag2;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x000121F0 File Offset: 0x000103F0
		public unsafe static bool TestPlanesAABB(Plane[] planes, Bounds bounds)
		{
			Span<Plane> span = new Span<Plane>(planes);
			bool flag;
			fixed (Plane* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				flag = GeometryUtility.TestPlanesAABB_Injected(ref managedSpanWrapper, ref bounds);
			}
			return flag;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0001222C File Offset: 0x0001042C
		[NativeName("ExtractPlanes")]
		private unsafe static void Internal_ExtractPlanes([Out] Plane[] planes, Matrix4x4 worldToProjectionMatrix)
		{
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				if (planes != null)
				{
					fixed (Plane[] array = planes)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				GeometryUtility.Internal_ExtractPlanes_Injected(out blittableArrayWrapper, ref worldToProjectionMatrix);
			}
			finally
			{
				Plane[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Plane>(ref array);
			}
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0001227C File Offset: 0x0001047C
		[NativeName("CalculateBounds")]
		private unsafe static Bounds Internal_CalculateBounds(Vector3[] positions, Matrix4x4 transform)
		{
			Span<Vector3> span = new Span<Vector3>(positions);
			Bounds bounds;
			fixed (Vector3* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				GeometryUtility.Internal_CalculateBounds_Injected(ref managedSpanWrapper, ref transform, out bounds);
			}
			return bounds;
		}

		// Token: 0x060009EA RID: 2538
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TestPlanesAABB_Injected(ref ManagedSpanWrapper planes, [In] ref Bounds bounds);

		// Token: 0x060009EB RID: 2539
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ExtractPlanes_Injected(out BlittableArrayWrapper planes, [In] ref Matrix4x4 worldToProjectionMatrix);

		// Token: 0x060009EC RID: 2540
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CalculateBounds_Injected(ref ManagedSpanWrapper positions, [In] ref Matrix4x4 transform, out Bounds ret);
	}
}
