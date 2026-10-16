using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200002D RID: 45
	[NativeHeader("Modules/Physics2D/Public/CompositeCollider2D.h")]
	[RequireComponent(typeof(Rigidbody2D))]
	public sealed class CompositeCollider2D : Collider2D
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0000C34C File Offset: 0x0000A54C
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x0000C370 File Offset: 0x0000A570
		public CompositeCollider2D.GeometryType geometryType
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CompositeCollider2D.get_geometryType_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CompositeCollider2D.set_geometryType_Injected(intPtr, value);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0000C394 File Offset: 0x0000A594
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
		public CompositeCollider2D.GenerationType generationType
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CompositeCollider2D.get_generationType_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CompositeCollider2D.set_generationType_Injected(intPtr, value);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0000C3DC File Offset: 0x0000A5DC
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x0000C400 File Offset: 0x0000A600
		public bool useDelaunayMesh
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CompositeCollider2D.get_useDelaunayMesh_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CompositeCollider2D.set_useDelaunayMesh_Injected(intPtr, value);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0000C424 File Offset: 0x0000A624
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x0000C448 File Offset: 0x0000A648
		public float vertexDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CompositeCollider2D.get_vertexDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CompositeCollider2D.set_vertexDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0000C46C File Offset: 0x0000A66C
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x0000C490 File Offset: 0x0000A690
		public float edgeRadius
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CompositeCollider2D.get_edgeRadius_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CompositeCollider2D.set_edgeRadius_Injected(intPtr, value);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0000C4B4 File Offset: 0x0000A6B4
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x0000C4D8 File Offset: 0x0000A6D8
		public float offsetDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CompositeCollider2D.get_offsetDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CompositeCollider2D.set_offsetDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000C4FC File Offset: 0x0000A6FC
		public void GenerateGeometry()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CompositeCollider2D.GenerateGeometry_Injected(intPtr);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000C520 File Offset: 0x0000A720
		[NativeMethod("GetCompositedColliders_Binding")]
		public int GetCompositedColliders([NotNull] List<Collider2D> colliders)
		{
			if (colliders == null)
			{
				ThrowHelper.ThrowArgumentNullException(colliders, "colliders");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return CompositeCollider2D.GetCompositedColliders_Injected(intPtr, colliders);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000C554 File Offset: 0x0000A754
		public int GetPathPointCount(int index)
		{
			int num = this.pathCount - 1;
			bool flag = index < 0 || index > num;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Path index {0} must be in the range of 0 to {1}.", index, num));
			}
			return this.GetPathPointCount_Internal(index);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
		[NativeMethod("GetPathPointCount_Binding")]
		private int GetPathPointCount_Internal(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return CompositeCollider2D.GetPathPointCount_Internal_Injected(intPtr, index);
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0000C5CC File Offset: 0x0000A7CC
		public int pathCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CompositeCollider2D.get_pathCount_Injected(intPtr);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0000C5F0 File Offset: 0x0000A7F0
		public int pointCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CompositeCollider2D.get_pointCount_Injected(intPtr);
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000C614 File Offset: 0x0000A814
		public int GetPath(int index, Vector2[] points)
		{
			bool flag = index < 0 || index >= this.pathCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Path index {0} must be in the range of 0 to {1}.", index, this.pathCount - 1));
			}
			bool flag2 = points == null;
			if (flag2)
			{
				throw new ArgumentNullException("points");
			}
			return this.GetPathArray_Internal(index, points);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000C680 File Offset: 0x0000A880
		[NativeMethod("GetPathArray_Binding")]
		private unsafe int GetPathArray_Internal(int index, [NotNull] Vector2[] points)
		{
			if (points == null)
			{
				ThrowHelper.ThrowArgumentNullException(points, "points");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector2> span = new Span<Vector2>(points);
			int pathArray_Internal_Injected;
			fixed (Vector2* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				pathArray_Internal_Injected = CompositeCollider2D.GetPathArray_Internal_Injected(intPtr, index, ref managedSpanWrapper);
			}
			return pathArray_Internal_Injected;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000C6D8 File Offset: 0x0000A8D8
		public int GetPath(int index, List<Vector2> points)
		{
			bool flag = index < 0 || index >= this.pathCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Path index {0} must be in the range of 0 to {1}.", index, this.pathCount - 1));
			}
			bool flag2 = points == null;
			if (flag2)
			{
				throw new ArgumentNullException("points");
			}
			return this.GetPathList_Internal(index, points);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000C744 File Offset: 0x0000A944
		[NativeMethod("GetPathList_Binding")]
		private unsafe int GetPathList_Internal(int index, [NotNull] List<Vector2> points)
		{
			if (points == null)
			{
				ThrowHelper.ThrowArgumentNullException(points, "points");
			}
			int pathList_Internal_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CompositeCollider2D>(this);
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
					pathList_Internal_Injected = CompositeCollider2D.GetPathList_Internal_Injected(intPtr, index, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<Vector2>(points);
			}
			return pathList_Internal_Injected;
		}

		// Token: 0x06000517 RID: 1303
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CompositeCollider2D.GeometryType get_geometryType_Injected(IntPtr _unity_self);

		// Token: 0x06000518 RID: 1304
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_geometryType_Injected(IntPtr _unity_self, CompositeCollider2D.GeometryType value);

		// Token: 0x06000519 RID: 1305
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CompositeCollider2D.GenerationType get_generationType_Injected(IntPtr _unity_self);

		// Token: 0x0600051A RID: 1306
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_generationType_Injected(IntPtr _unity_self, CompositeCollider2D.GenerationType value);

		// Token: 0x0600051B RID: 1307
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useDelaunayMesh_Injected(IntPtr _unity_self);

		// Token: 0x0600051C RID: 1308
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useDelaunayMesh_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600051D RID: 1309
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_vertexDistance_Injected(IntPtr _unity_self);

		// Token: 0x0600051E RID: 1310
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_vertexDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600051F RID: 1311
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_edgeRadius_Injected(IntPtr _unity_self);

		// Token: 0x06000520 RID: 1312
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_edgeRadius_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000521 RID: 1313
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_offsetDistance_Injected(IntPtr _unity_self);

		// Token: 0x06000522 RID: 1314
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_offsetDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000523 RID: 1315
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GenerateGeometry_Injected(IntPtr _unity_self);

		// Token: 0x06000524 RID: 1316
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCompositedColliders_Injected(IntPtr _unity_self, List<Collider2D> colliders);

		// Token: 0x06000525 RID: 1317
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPathPointCount_Internal_Injected(IntPtr _unity_self, int index);

		// Token: 0x06000526 RID: 1318
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_pathCount_Injected(IntPtr _unity_self);

		// Token: 0x06000527 RID: 1319
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_pointCount_Injected(IntPtr _unity_self);

		// Token: 0x06000528 RID: 1320
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPathArray_Internal_Injected(IntPtr _unity_self, int index, ref ManagedSpanWrapper points);

		// Token: 0x06000529 RID: 1321
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPathList_Internal_Injected(IntPtr _unity_self, int index, ref BlittableListWrapper points);

		// Token: 0x0200002E RID: 46
		public enum GeometryType
		{
			// Token: 0x040000C1 RID: 193
			Outlines,
			// Token: 0x040000C2 RID: 194
			Polygons
		}

		// Token: 0x0200002F RID: 47
		public enum GenerationType
		{
			// Token: 0x040000C4 RID: 196
			Synchronous,
			// Token: 0x040000C5 RID: 197
			Manual
		}
	}
}
