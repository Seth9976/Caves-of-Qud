using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200002C RID: 44
	[NativeHeader("Modules/Physics2D/Public/PolygonCollider2D.h")]
	public sealed class PolygonCollider2D : Collider2D
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0000BE30 File Offset: 0x0000A030
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x0000BE54 File Offset: 0x0000A054
		public bool useDelaunayMesh
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PolygonCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PolygonCollider2D.get_useDelaunayMesh_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PolygonCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PolygonCollider2D.set_useDelaunayMesh_Injected(intPtr, value);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0000BE78 File Offset: 0x0000A078
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x0000BE9C File Offset: 0x0000A09C
		public bool autoTiling
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PolygonCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PolygonCollider2D.get_autoTiling_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PolygonCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PolygonCollider2D.set_autoTiling_Injected(intPtr, value);
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000BEC0 File Offset: 0x0000A0C0
		[NativeMethod("GetPointCount")]
		public int GetTotalPointCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PolygonCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return PolygonCollider2D.GetTotalPointCount_Injected(intPtr);
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x0000BF28 File Offset: 0x0000A128
		public unsafe Vector2[] points
		{
			[NativeMethod("GetPoints_Binding")]
			get
			{
				Vector2[] array2;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PolygonCollider2D>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					BlittableArrayWrapper blittableArrayWrapper;
					PolygonCollider2D.get_points_Injected(intPtr, out blittableArrayWrapper);
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
			[NativeMethod("SetPoints_Binding")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PolygonCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Span<Vector2> span = new Span<Vector2>(value);
				fixed (Vector2* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					PolygonCollider2D.set_points_Injected(intPtr, ref managedSpanWrapper);
				}
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0000BF70 File Offset: 0x0000A170
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x0000BF94 File Offset: 0x0000A194
		public int pathCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PolygonCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return PolygonCollider2D.get_pathCount_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PolygonCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				PolygonCollider2D.set_pathCount_Injected(intPtr, value);
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000BFB8 File Offset: 0x0000A1B8
		public Vector2[] GetPath(int index)
		{
			bool flag = index >= this.pathCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Path {0} does not exist.", index));
			}
			bool flag2 = index < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Path {0} does not exist; negative path index is invalid.", index));
			}
			return this.GetPath_Internal(index);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000C018 File Offset: 0x0000A218
		[NativeMethod("GetPath_Binding")]
		private Vector2[] GetPath_Internal(int index)
		{
			Vector2[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PolygonCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				PolygonCollider2D.GetPath_Internal_Injected(intPtr, index, out blittableArrayWrapper);
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

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000C05C File Offset: 0x0000A25C
		public void SetPath(int index, Vector2[] points)
		{
			bool flag = index < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Negative path index {0} is invalid.", index));
			}
			this.SetPath_Internal(index, points);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000C094 File Offset: 0x0000A294
		[NativeMethod("SetPath_Binding")]
		private unsafe void SetPath_Internal(int index, [NotNull] Vector2[] points)
		{
			if (points == null)
			{
				ThrowHelper.ThrowArgumentNullException(points, "points");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PolygonCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector2> span = new Span<Vector2>(points);
			fixed (Vector2* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				PolygonCollider2D.SetPath_Internal_Injected(intPtr, index, ref managedSpanWrapper);
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000C0EC File Offset: 0x0000A2EC
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

		// Token: 0x060004EA RID: 1258 RVA: 0x0000C158 File Offset: 0x0000A358
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
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PolygonCollider2D>(this);
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
					pathList_Internal_Injected = PolygonCollider2D.GetPathList_Internal_Injected(intPtr, index, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<Vector2>(points);
			}
			return pathList_Internal_Injected;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000C1DC File Offset: 0x0000A3DC
		public void SetPath(int index, List<Vector2> points)
		{
			bool flag = index < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Negative path index {0} is invalid.", index));
			}
			this.SetPathList_Internal(index, points);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000C214 File Offset: 0x0000A414
		[NativeMethod("SetPathList_Binding")]
		private unsafe void SetPathList_Internal(int index, [NotNull] List<Vector2> points)
		{
			if (points == null)
			{
				ThrowHelper.ThrowArgumentNullException(points, "points");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PolygonCollider2D>(this);
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
					PolygonCollider2D.SetPathList_Internal_Injected(intPtr, index, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<Vector2>(points);
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000C294 File Offset: 0x0000A494
		[ExcludeFromDocs]
		public void CreatePrimitive(int sides)
		{
			this.CreatePrimitive(sides, Vector2.one, Vector2.zero);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000C2A9 File Offset: 0x0000A4A9
		[ExcludeFromDocs]
		public void CreatePrimitive(int sides, Vector2 scale)
		{
			this.CreatePrimitive(sides, scale, Vector2.zero);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000C2BC File Offset: 0x0000A4BC
		public void CreatePrimitive(int sides, [DefaultValue("Vector2.one")] Vector2 scale, [DefaultValue("Vector2.zero")] Vector2 offset)
		{
			bool flag = sides < 3;
			if (flag)
			{
				Debug.LogWarning("Cannot create a 2D polygon primitive collider with less than two sides.", this);
			}
			else
			{
				bool flag2 = scale.x <= 0f || scale.y <= 0f;
				if (flag2)
				{
					Debug.LogWarning("Cannot create a 2D polygon primitive collider with an axis scale less than or equal to zero.", this);
				}
				else
				{
					this.CreatePrimitive_Internal(sides, scale, offset, true);
				}
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000C320 File Offset: 0x0000A520
		[NativeMethod("CreatePrimitive")]
		private void CreatePrimitive_Internal(int sides, [DefaultValue("Vector2.one")] Vector2 scale, [DefaultValue("Vector2.zero")] Vector2 offset, bool autoRefresh)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<PolygonCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			PolygonCollider2D.CreatePrimitive_Internal_Injected(intPtr, sides, ref scale, ref offset, autoRefresh);
		}

		// Token: 0x060004F2 RID: 1266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useDelaunayMesh_Injected(IntPtr _unity_self);

		// Token: 0x060004F3 RID: 1267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useDelaunayMesh_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060004F4 RID: 1268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_autoTiling_Injected(IntPtr _unity_self);

		// Token: 0x060004F5 RID: 1269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_autoTiling_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060004F6 RID: 1270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetTotalPointCount_Injected(IntPtr _unity_self);

		// Token: 0x060004F7 RID: 1271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_points_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x060004F8 RID: 1272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_points_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x060004F9 RID: 1273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_pathCount_Injected(IntPtr _unity_self);

		// Token: 0x060004FA RID: 1274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_pathCount_Injected(IntPtr _unity_self, int value);

		// Token: 0x060004FB RID: 1275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPath_Internal_Injected(IntPtr _unity_self, int index, out BlittableArrayWrapper ret);

		// Token: 0x060004FC RID: 1276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPath_Internal_Injected(IntPtr _unity_self, int index, ref ManagedSpanWrapper points);

		// Token: 0x060004FD RID: 1277
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPathList_Internal_Injected(IntPtr _unity_self, int index, ref BlittableListWrapper points);

		// Token: 0x060004FE RID: 1278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPathList_Internal_Injected(IntPtr _unity_self, int index, ref BlittableListWrapper points);

		// Token: 0x060004FF RID: 1279
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreatePrimitive_Internal_Injected(IntPtr _unity_self, int sides, [DefaultValue("Vector2.one")] [In] ref Vector2 scale, [DefaultValue("Vector2.zero")] [In] ref Vector2 offset, bool autoRefresh);
	}
}
