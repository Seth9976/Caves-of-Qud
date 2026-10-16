using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000027 RID: 39
	[NativeHeader("Modules/Physics2D/Public/CustomCollider2D.h")]
	public sealed class CustomCollider2D : Collider2D
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000B24C File Offset: 0x0000944C
		[NativeMethod("CustomShapeCount_Binding")]
		public int customShapeCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CustomCollider2D.get_customShapeCount_Injected(intPtr);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000B270 File Offset: 0x00009470
		[NativeMethod("CustomVertexCount_Binding")]
		public int customVertexCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CustomCollider2D.get_customVertexCount_Injected(intPtr);
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000B294 File Offset: 0x00009494
		public int GetCustomShapes(PhysicsShapeGroup2D physicsShapeGroup)
		{
			int customShapeCount = this.customShapeCount;
			bool flag = customShapeCount > 0;
			int num;
			if (flag)
			{
				num = this.GetCustomShapes_Internal(ref physicsShapeGroup.m_GroupState, 0, customShapeCount);
			}
			else
			{
				physicsShapeGroup.Clear();
				num = 0;
			}
			return num;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000B2D0 File Offset: 0x000094D0
		public int GetCustomShapes(PhysicsShapeGroup2D physicsShapeGroup, int shapeIndex, [DefaultValue("1")] int shapeCount = 1)
		{
			int customShapeCount = this.customShapeCount;
			bool flag = shapeIndex < 0 || shapeIndex >= customShapeCount || shapeCount < 1 || shapeIndex + shapeCount > customShapeCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot get shape range from {0} to {1} as CustomCollider2D only has {2} shape(s).", shapeIndex, shapeIndex + shapeCount - 1, customShapeCount));
			}
			return this.GetCustomShapes_Internal(ref physicsShapeGroup.m_GroupState, shapeIndex, shapeCount);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000B338 File Offset: 0x00009538
		[NativeMethod("GetCustomShapes_Binding")]
		private int GetCustomShapes_Internal(ref PhysicsShapeGroup2D.GroupState physicsShapeGroupState, int shapeIndex, int shapeCount)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return CustomCollider2D.GetCustomShapes_Internal_Injected(intPtr, ref physicsShapeGroupState, shapeIndex, shapeCount);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000B360 File Offset: 0x00009560
		public int GetCustomShapes(NativeArray<PhysicsShape2D> shapes, NativeArray<Vector2> vertices)
		{
			bool flag = !shapes.IsCreated || shapes.Length != this.customShapeCount;
			if (flag)
			{
				throw new ArgumentException(string.Format("Cannot get custom shapes as the native shapes array length must be identical to the current custom shape count of {0}.", this.customShapeCount), "shapes");
			}
			bool flag2 = !vertices.IsCreated || vertices.Length != this.customVertexCount;
			if (flag2)
			{
				throw new ArgumentException(string.Format("Cannot get custom shapes as the native vertices array length must be identical to the current custom vertex count of {0}.", this.customVertexCount), "vertices");
			}
			return this.GetCustomShapesNative_Internal((IntPtr)shapes.GetUnsafeReadOnlyPtr<PhysicsShape2D>(), shapes.Length, (IntPtr)vertices.GetUnsafeReadOnlyPtr<Vector2>(), vertices.Length);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000B420 File Offset: 0x00009620
		[NativeMethod("GetCustomShapesAllNative_Binding")]
		private int GetCustomShapesNative_Internal(IntPtr shapesPtr, int shapeCount, IntPtr verticesPtr, int vertexCount)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return CustomCollider2D.GetCustomShapesNative_Internal_Injected(intPtr, shapesPtr, shapeCount, verticesPtr, vertexCount);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000B448 File Offset: 0x00009648
		public void SetCustomShapes(PhysicsShapeGroup2D physicsShapeGroup)
		{
			bool flag = physicsShapeGroup.shapeCount > 0;
			if (flag)
			{
				this.SetCustomShapesAll_Internal(ref physicsShapeGroup.m_GroupState);
			}
			else
			{
				this.ClearCustomShapes();
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000B47C File Offset: 0x0000967C
		[NativeMethod("SetCustomShapesAll_Binding")]
		private void SetCustomShapesAll_Internal(ref PhysicsShapeGroup2D.GroupState physicsShapeGroupState)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CustomCollider2D.SetCustomShapesAll_Internal_Injected(intPtr, ref physicsShapeGroupState);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000B4A0 File Offset: 0x000096A0
		public void SetCustomShapes(NativeArray<PhysicsShape2D> shapes, NativeArray<Vector2> vertices)
		{
			bool flag = !shapes.IsCreated || shapes.Length == 0;
			if (flag)
			{
				throw new ArgumentException("Cannot set custom shapes as the native shapes array is empty.", "shapes");
			}
			bool flag2 = !vertices.IsCreated || vertices.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Cannot set custom shapes as the native vertices array is empty.", "vertices");
			}
			this.SetCustomShapesNative_Internal((IntPtr)shapes.GetUnsafeReadOnlyPtr<PhysicsShape2D>(), shapes.Length, (IntPtr)vertices.GetUnsafeReadOnlyPtr<Vector2>(), vertices.Length);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000B530 File Offset: 0x00009730
		[NativeMethod("SetCustomShapesAllNative_Binding", ThrowsException = true)]
		private void SetCustomShapesNative_Internal(IntPtr shapesPtr, int shapeCount, IntPtr verticesPtr, int vertexCount)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CustomCollider2D.SetCustomShapesNative_Internal_Injected(intPtr, shapesPtr, shapeCount, verticesPtr, vertexCount);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000B558 File Offset: 0x00009758
		public void SetCustomShape(PhysicsShapeGroup2D physicsShapeGroup, int srcShapeIndex, int dstShapeIndex)
		{
			bool flag = srcShapeIndex < 0 || srcShapeIndex >= physicsShapeGroup.shapeCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot set custom shape at {0} as the shape group only has {1} shape(s).", srcShapeIndex, physicsShapeGroup.shapeCount));
			}
			PhysicsShape2D shape = physicsShapeGroup.GetShape(srcShapeIndex);
			bool flag2 = shape.vertexStartIndex < 0 || shape.vertexStartIndex >= physicsShapeGroup.vertexCount || shape.vertexCount < 1 || shape.vertexStartIndex + shape.vertexCount > physicsShapeGroup.vertexCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot set custom shape at {0} as its shape indices are out of the available vertices ranges.", srcShapeIndex));
			}
			bool flag3 = dstShapeIndex < 0 || dstShapeIndex >= this.customShapeCount;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot set custom shape at destination {0} as CustomCollider2D only has {1} custom shape(s). The destination index must be within the existing shape range.", dstShapeIndex, this.customShapeCount));
			}
			this.SetCustomShape_Internal(ref physicsShapeGroup.m_GroupState, srcShapeIndex, dstShapeIndex);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000B644 File Offset: 0x00009844
		[NativeMethod("SetCustomShape_Binding")]
		private void SetCustomShape_Internal(ref PhysicsShapeGroup2D.GroupState physicsShapeGroupState, int srcShapeIndex, int dstShapeIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CustomCollider2D.SetCustomShape_Internal_Injected(intPtr, ref physicsShapeGroupState, srcShapeIndex, dstShapeIndex);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000B66C File Offset: 0x0000986C
		public void SetCustomShape(NativeArray<PhysicsShape2D> shapes, NativeArray<Vector2> vertices, int srcShapeIndex, int dstShapeIndex)
		{
			bool flag = !shapes.IsCreated || shapes.Length == 0;
			if (flag)
			{
				throw new ArgumentException("Cannot set custom shapes as the native shapes array is empty.", "shapes");
			}
			bool flag2 = !vertices.IsCreated || vertices.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Cannot set custom shapes as the native vertices array is empty.", "vertices");
			}
			bool flag3 = srcShapeIndex < 0 || srcShapeIndex >= shapes.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot set custom shape at {0} as the shape native array only has {1} shape(s).", srcShapeIndex, shapes.Length));
			}
			PhysicsShape2D physicsShape2D = shapes[srcShapeIndex];
			bool flag4 = physicsShape2D.vertexStartIndex < 0 || physicsShape2D.vertexStartIndex >= vertices.Length || physicsShape2D.vertexCount < 1 || physicsShape2D.vertexStartIndex + physicsShape2D.vertexCount > vertices.Length;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot set custom shape at {0} as its shape indices are out of the available vertices ranges.", srcShapeIndex));
			}
			bool flag5 = dstShapeIndex < 0 || dstShapeIndex >= this.customShapeCount;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot set custom shape at destination {0} as CustomCollider2D only has {1} custom shape(s). The destination index must be within the existing shape range.", dstShapeIndex, this.customShapeCount));
			}
			this.SetCustomShapeNative_Internal((IntPtr)shapes.GetUnsafeReadOnlyPtr<PhysicsShape2D>(), shapes.Length, (IntPtr)vertices.GetUnsafeReadOnlyPtr<Vector2>(), vertices.Length, srcShapeIndex, dstShapeIndex);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000B7D8 File Offset: 0x000099D8
		[NativeMethod("SetCustomShapeNative_Binding", ThrowsException = true)]
		private void SetCustomShapeNative_Internal(IntPtr shapesPtr, int shapeCount, IntPtr verticesPtr, int vertexCount, int srcShapeIndex, int dstShapeIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CustomCollider2D.SetCustomShapeNative_Internal_Injected(intPtr, shapesPtr, shapeCount, verticesPtr, vertexCount, srcShapeIndex, dstShapeIndex);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000B804 File Offset: 0x00009A04
		public void ClearCustomShapes(int shapeIndex, int shapeCount)
		{
			int customShapeCount = this.customShapeCount;
			bool flag = shapeIndex < 0 || shapeIndex >= customShapeCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot clear custom shape(s) at index {0} as the CustomCollider2D only has {1} shape(s).", shapeIndex, customShapeCount));
			}
			bool flag2 = shapeIndex + shapeCount < 0 || shapeIndex + shapeCount > this.customShapeCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot clear custom shape(s) in the range (index {0}, count {1}) as this range is outside range of the existing {2} shape(s).", shapeIndex, shapeCount, this.customShapeCount));
			}
			this.ClearCustomShapes_Internal(shapeIndex, shapeCount);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000B890 File Offset: 0x00009A90
		[NativeMethod("ClearCustomShapes_Binding")]
		private void ClearCustomShapes_Internal(int shapeIndex, int shapeCount)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CustomCollider2D.ClearCustomShapes_Internal_Injected(intPtr, shapeIndex, shapeCount);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000B8B4 File Offset: 0x00009AB4
		[NativeMethod("ClearCustomShapes_Binding")]
		public void ClearCustomShapes()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<CustomCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CustomCollider2D.ClearCustomShapes_Injected(intPtr);
		}

		// Token: 0x06000490 RID: 1168
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_customShapeCount_Injected(IntPtr _unity_self);

		// Token: 0x06000491 RID: 1169
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_customVertexCount_Injected(IntPtr _unity_self);

		// Token: 0x06000492 RID: 1170
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCustomShapes_Internal_Injected(IntPtr _unity_self, ref PhysicsShapeGroup2D.GroupState physicsShapeGroupState, int shapeIndex, int shapeCount);

		// Token: 0x06000493 RID: 1171
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCustomShapesNative_Internal_Injected(IntPtr _unity_self, IntPtr shapesPtr, int shapeCount, IntPtr verticesPtr, int vertexCount);

		// Token: 0x06000494 RID: 1172
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCustomShapesAll_Internal_Injected(IntPtr _unity_self, ref PhysicsShapeGroup2D.GroupState physicsShapeGroupState);

		// Token: 0x06000495 RID: 1173
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCustomShapesNative_Internal_Injected(IntPtr _unity_self, IntPtr shapesPtr, int shapeCount, IntPtr verticesPtr, int vertexCount);

		// Token: 0x06000496 RID: 1174
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCustomShape_Internal_Injected(IntPtr _unity_self, ref PhysicsShapeGroup2D.GroupState physicsShapeGroupState, int srcShapeIndex, int dstShapeIndex);

		// Token: 0x06000497 RID: 1175
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCustomShapeNative_Internal_Injected(IntPtr _unity_self, IntPtr shapesPtr, int shapeCount, IntPtr verticesPtr, int vertexCount, int srcShapeIndex, int dstShapeIndex);

		// Token: 0x06000498 RID: 1176
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearCustomShapes_Internal_Injected(IntPtr _unity_self, int shapeIndex, int shapeCount);

		// Token: 0x06000499 RID: 1177
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearCustomShapes_Injected(IntPtr _unity_self);
	}
}
