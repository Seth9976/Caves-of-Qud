using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002A RID: 42
	[NativeHeader("Modules/Physics2D/Public/EdgeCollider2D.h")]
	public sealed class EdgeCollider2D : Collider2D
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x0000B9D4 File Offset: 0x00009BD4
		public void Reset()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<EdgeCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			EdgeCollider2D.Reset_Injected(intPtr);
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000B9F8 File Offset: 0x00009BF8
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x0000BA1C File Offset: 0x00009C1C
		public float edgeRadius
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<EdgeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return EdgeCollider2D.get_edgeRadius_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<EdgeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				EdgeCollider2D.set_edgeRadius_Injected(intPtr, value);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000BA40 File Offset: 0x00009C40
		public int edgeCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<EdgeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return EdgeCollider2D.get_edgeCount_Injected(intPtr);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0000BA64 File Offset: 0x00009C64
		public int pointCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<EdgeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return EdgeCollider2D.get_pointCount_Injected(intPtr);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000BA88 File Offset: 0x00009C88
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x0000BACC File Offset: 0x00009CCC
		public unsafe Vector2[] points
		{
			get
			{
				Vector2[] array2;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<EdgeCollider2D>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					BlittableArrayWrapper blittableArrayWrapper;
					EdgeCollider2D.get_points_Injected(intPtr, out blittableArrayWrapper);
				}
				finally
				{
					BlittableArrayWrapper blittableArrayWrapper;
					Vector2[] array;
					blittableArrayWrapper.Unmarshal<Vector2>(ref array);
					array2 = array;
				}
				return array2;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<EdgeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Span<Vector2> span = new Span<Vector2>(value);
				fixed (Vector2* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					EdgeCollider2D.set_points_Injected(intPtr, ref managedSpanWrapper);
				}
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000BB14 File Offset: 0x00009D14
		[NativeMethod("GetPoints_Binding")]
		public unsafe int GetPoints([NotNull] List<Vector2> points)
		{
			if (points == null)
			{
				ThrowHelper.ThrowArgumentNullException(points, "points");
			}
			int points_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<EdgeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (Vector2[] array = NoAllocHelpers.ExtractArrayFromList<Vector2>(points))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, points.Count);
					points_Injected = EdgeCollider2D.GetPoints_Injected(intPtr, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<Vector2>(points);
			}
			return points_Injected;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000BB94 File Offset: 0x00009D94
		[NativeMethod("SetPoints_Binding")]
		public unsafe bool SetPoints([NotNull] List<Vector2> points)
		{
			if (points == null)
			{
				ThrowHelper.ThrowArgumentNullException(points, "points");
			}
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<EdgeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (Vector2[] array = NoAllocHelpers.ExtractArrayFromList<Vector2>(points))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, points.Count);
					flag = EdgeCollider2D.SetPoints_Injected(intPtr, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<Vector2>(points);
			}
			return flag;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000BC14 File Offset: 0x00009E14
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x0000BC38 File Offset: 0x00009E38
		public bool useAdjacentStartPoint
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<EdgeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return EdgeCollider2D.get_useAdjacentStartPoint_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<EdgeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				EdgeCollider2D.set_useAdjacentStartPoint_Injected(intPtr, value);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000BC5C File Offset: 0x00009E5C
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x0000BC80 File Offset: 0x00009E80
		public bool useAdjacentEndPoint
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<EdgeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return EdgeCollider2D.get_useAdjacentEndPoint_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<EdgeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				EdgeCollider2D.set_useAdjacentEndPoint_Injected(intPtr, value);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000BCA4 File Offset: 0x00009EA4
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0000BCCC File Offset: 0x00009ECC
		public Vector2 adjacentStartPoint
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<EdgeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				EdgeCollider2D.get_adjacentStartPoint_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<EdgeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				EdgeCollider2D.set_adjacentStartPoint_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0000BCF0 File Offset: 0x00009EF0
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x0000BD18 File Offset: 0x00009F18
		public Vector2 adjacentEndPoint
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<EdgeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				EdgeCollider2D.get_adjacentEndPoint_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<EdgeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				EdgeCollider2D.set_adjacentEndPoint_Injected(intPtr, ref value);
			}
		}

		// Token: 0x060004BC RID: 1212
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Reset_Injected(IntPtr _unity_self);

		// Token: 0x060004BD RID: 1213
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_edgeRadius_Injected(IntPtr _unity_self);

		// Token: 0x060004BE RID: 1214
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_edgeRadius_Injected(IntPtr _unity_self, float value);

		// Token: 0x060004BF RID: 1215
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_edgeCount_Injected(IntPtr _unity_self);

		// Token: 0x060004C0 RID: 1216
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_pointCount_Injected(IntPtr _unity_self);

		// Token: 0x060004C1 RID: 1217
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_points_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x060004C2 RID: 1218
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_points_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x060004C3 RID: 1219
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPoints_Injected(IntPtr _unity_self, ref BlittableListWrapper points);

		// Token: 0x060004C4 RID: 1220
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetPoints_Injected(IntPtr _unity_self, ref BlittableListWrapper points);

		// Token: 0x060004C5 RID: 1221
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useAdjacentStartPoint_Injected(IntPtr _unity_self);

		// Token: 0x060004C6 RID: 1222
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useAdjacentStartPoint_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060004C7 RID: 1223
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useAdjacentEndPoint_Injected(IntPtr _unity_self);

		// Token: 0x060004C8 RID: 1224
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useAdjacentEndPoint_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060004C9 RID: 1225
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_adjacentStartPoint_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060004CA RID: 1226
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_adjacentStartPoint_Injected(IntPtr _unity_self, [In] ref Vector2 value);

		// Token: 0x060004CB RID: 1227
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_adjacentEndPoint_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060004CC RID: 1228
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_adjacentEndPoint_Injected(IntPtr _unity_self, [In] ref Vector2 value);
	}
}
