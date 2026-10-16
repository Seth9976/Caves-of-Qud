using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeHeader("Modules/Physics2D/Public/PhysicsSceneHandle2D.h")]
	public struct PhysicsScene2D : IEquatable<PhysicsScene2D>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public override string ToString()
		{
			return UnityString.Format("({0})", new object[] { this.m_Handle });
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002080 File Offset: 0x00000280
		public static bool operator ==(PhysicsScene2D lhs, PhysicsScene2D rhs)
		{
			return lhs.m_Handle == rhs.m_Handle;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020A0 File Offset: 0x000002A0
		public static bool operator !=(PhysicsScene2D lhs, PhysicsScene2D rhs)
		{
			return lhs.m_Handle != rhs.m_Handle;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020C4 File Offset: 0x000002C4
		public override int GetHashCode()
		{
			return this.m_Handle;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020DC File Offset: 0x000002DC
		public override bool Equals(object other)
		{
			bool flag = !(other is PhysicsScene2D);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				PhysicsScene2D physicsScene2D = (PhysicsScene2D)other;
				flag2 = this.m_Handle == physicsScene2D.m_Handle;
			}
			return flag2;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002118 File Offset: 0x00000318
		public bool Equals(PhysicsScene2D other)
		{
			return this.m_Handle == other.m_Handle;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002138 File Offset: 0x00000338
		public bool IsValid()
		{
			return PhysicsScene2D.IsValid_Internal(this);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002158 File Offset: 0x00000358
		[StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
		[NativeMethod("IsPhysicsSceneValid")]
		private static bool IsValid_Internal(PhysicsScene2D physicsScene)
		{
			return PhysicsScene2D.IsValid_Internal_Injected(ref physicsScene);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000216C File Offset: 0x0000036C
		public bool IsEmpty()
		{
			bool flag = this.IsValid();
			if (flag)
			{
				return PhysicsScene2D.IsEmpty_Internal(this);
			}
			throw new InvalidOperationException("Cannot check if physics scene is empty as it is invalid.");
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021A0 File Offset: 0x000003A0
		[StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
		[NativeMethod("IsPhysicsWorldEmpty")]
		private static bool IsEmpty_Internal(PhysicsScene2D physicsScene)
		{
			return PhysicsScene2D.IsEmpty_Internal_Injected(ref physicsScene);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021B4 File Offset: 0x000003B4
		public int subStepCount
		{
			get
			{
				return PhysicsScene2D.SubStepCount_Internal(this);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021D4 File Offset: 0x000003D4
		[StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
		[NativeMethod("GetSubStepCount")]
		private static int SubStepCount_Internal(PhysicsScene2D physicsScene)
		{
			return PhysicsScene2D.SubStepCount_Internal_Injected(ref physicsScene);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021E8 File Offset: 0x000003E8
		public float subStepLostTime
		{
			get
			{
				return PhysicsScene2D.SubStepLostTime_Internal(this);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002208 File Offset: 0x00000408
		[NativeMethod("GetSubStepLostTime")]
		[StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
		private static float SubStepLostTime_Internal(PhysicsScene2D physicsScene)
		{
			return PhysicsScene2D.SubStepLostTime_Internal_Injected(ref physicsScene);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000221C File Offset: 0x0000041C
		[ExcludeFromDocs]
		public bool Simulate(float deltaTime)
		{
			return this.Simulate(deltaTime, -1);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002238 File Offset: 0x00000438
		public bool Simulate(float deltaTime, [DefaultValue("Physics2D.AllLayers")] int simulationLayers = -1)
		{
			bool flag = this.IsValid();
			if (flag)
			{
				return Physics2D.Simulate_Internal(this, deltaTime, simulationLayers);
			}
			throw new InvalidOperationException("Cannot simulate the physics scene as it is invalid.");
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000226C File Offset: 0x0000046C
		public RaycastHit2D Linecast(Vector2 start, Vector2 end, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.Linecast_Internal(this, start, end, contactFilter2D);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022A0 File Offset: 0x000004A0
		public RaycastHit2D Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.Linecast_Internal(this, start, end, contactFilter);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022C0 File Offset: 0x000004C0
		public int Linecast(Vector2 start, Vector2 end, RaycastHit2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.LinecastArray_Internal(this, start, end, contactFilter2D, results);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022F4 File Offset: 0x000004F4
		public int Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return PhysicsScene2D.LinecastArray_Internal(this, start, end, contactFilter, results);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002318 File Offset: 0x00000518
		public int Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter, List<RaycastHit2D> results)
		{
			return PhysicsScene2D.LinecastList_Internal(this, start, end, contactFilter, results);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000233C File Offset: 0x0000053C
		[NativeMethod("Linecast_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D Linecast_Internal(PhysicsScene2D physicsScene, Vector2 start, Vector2 end, ContactFilter2D contactFilter)
		{
			RaycastHit2D raycastHit2D;
			PhysicsScene2D.Linecast_Internal_Injected(ref physicsScene, ref start, ref end, ref contactFilter, out raycastHit2D);
			return raycastHit2D;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000235C File Offset: 0x0000055C
		[NativeMethod("LinecastArray_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private unsafe static int LinecastArray_Internal(PhysicsScene2D physicsScene, Vector2 start, Vector2 end, ContactFilter2D contactFilter, [NotNull] RaycastHit2D[] results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			Span<RaycastHit2D> span = new Span<RaycastHit2D>(results);
			int num;
			fixed (RaycastHit2D* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = PhysicsScene2D.LinecastArray_Internal_Injected(ref physicsScene, ref start, ref end, ref contactFilter, ref managedSpanWrapper);
			}
			return num;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023B0 File Offset: 0x000005B0
		[NativeMethod("LinecastList_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private unsafe static int LinecastList_Internal(PhysicsScene2D physicsScene, Vector2 start, Vector2 end, ContactFilter2D contactFilter, [NotNull] List<RaycastHit2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			int num;
			try
			{
				fixed (RaycastHit2D[] array = NoAllocHelpers.ExtractArrayFromList<RaycastHit2D>(results))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, results.Count);
					num = PhysicsScene2D.LinecastList_Internal_Injected(ref physicsScene, ref start, ref end, ref contactFilter, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<RaycastHit2D>(results);
			}
			return num;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000242C File Offset: 0x0000062C
		public RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.Raycast_Internal(this, origin, direction, distance, contactFilter2D);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002460 File Offset: 0x00000660
		public RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.Raycast_Internal(this, origin, direction, distance, contactFilter);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002484 File Offset: 0x00000684
		public int Raycast(Vector2 origin, Vector2 direction, float distance, RaycastHit2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.RaycastArray_Internal(this, origin, direction, distance, contactFilter2D, results);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024BC File Offset: 0x000006BC
		public int Raycast(Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return PhysicsScene2D.RaycastArray_Internal(this, origin, direction, distance, contactFilter, results);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024E0 File Offset: 0x000006E0
		public int Raycast(Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter, List<RaycastHit2D> results)
		{
			return PhysicsScene2D.RaycastList_Internal(this, origin, direction, distance, contactFilter, results);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002504 File Offset: 0x00000704
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("Raycast_Binding")]
		private static RaycastHit2D Raycast_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			RaycastHit2D raycastHit2D;
			PhysicsScene2D.Raycast_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, ref contactFilter, out raycastHit2D);
			return raycastHit2D;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002524 File Offset: 0x00000724
		[NativeMethod("RaycastArray_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private unsafe static int RaycastArray_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull] RaycastHit2D[] results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			Span<RaycastHit2D> span = new Span<RaycastHit2D>(results);
			int num;
			fixed (RaycastHit2D* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = PhysicsScene2D.RaycastArray_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, ref contactFilter, ref managedSpanWrapper);
			}
			return num;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002578 File Offset: 0x00000778
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("RaycastList_Binding")]
		private unsafe static int RaycastList_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull] List<RaycastHit2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			int num;
			try
			{
				fixed (RaycastHit2D[] array = NoAllocHelpers.ExtractArrayFromList<RaycastHit2D>(results))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, results.Count);
					num = PhysicsScene2D.RaycastList_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, ref contactFilter, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<RaycastHit2D>(results);
			}
			return num;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025F4 File Offset: 0x000007F4
		public RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.CircleCast_Internal(this, origin, radius, direction, distance, contactFilter2D);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000262C File Offset: 0x0000082C
		public RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.CircleCast_Internal(this, origin, radius, direction, distance, contactFilter);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002650 File Offset: 0x00000850
		public int CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, RaycastHit2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.CircleCastArray_Internal(this, origin, radius, direction, distance, contactFilter2D, results);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002688 File Offset: 0x00000888
		public int CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return PhysicsScene2D.CircleCastArray_Internal(this, origin, radius, direction, distance, contactFilter, results);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026B0 File Offset: 0x000008B0
		public int CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter, List<RaycastHit2D> results)
		{
			return PhysicsScene2D.CircleCastList_Internal(this, origin, radius, direction, distance, contactFilter, results);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026D8 File Offset: 0x000008D8
		[NativeMethod("CircleCast_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D CircleCast_Internal(PhysicsScene2D physicsScene, Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			RaycastHit2D raycastHit2D;
			PhysicsScene2D.CircleCast_Internal_Injected(ref physicsScene, ref origin, radius, ref direction, distance, ref contactFilter, out raycastHit2D);
			return raycastHit2D;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026F8 File Offset: 0x000008F8
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("CircleCastArray_Binding")]
		private unsafe static int CircleCastArray_Internal(PhysicsScene2D physicsScene, Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull] RaycastHit2D[] results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			Span<RaycastHit2D> span = new Span<RaycastHit2D>(results);
			int num;
			fixed (RaycastHit2D* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = PhysicsScene2D.CircleCastArray_Internal_Injected(ref physicsScene, ref origin, radius, ref direction, distance, ref contactFilter, ref managedSpanWrapper);
			}
			return num;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002750 File Offset: 0x00000950
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("CircleCastList_Binding")]
		private unsafe static int CircleCastList_Internal(PhysicsScene2D physicsScene, Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull] List<RaycastHit2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			int num;
			try
			{
				fixed (RaycastHit2D[] array = NoAllocHelpers.ExtractArrayFromList<RaycastHit2D>(results))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, results.Count);
					num = PhysicsScene2D.CircleCastList_Internal_Injected(ref physicsScene, ref origin, radius, ref direction, distance, ref contactFilter, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<RaycastHit2D>(results);
			}
			return num;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000027D0 File Offset: 0x000009D0
		public RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.BoxCast_Internal(this, origin, size, angle, direction, distance, contactFilter2D);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002808 File Offset: 0x00000A08
		public RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.BoxCast_Internal(this, origin, size, angle, direction, distance, contactFilter);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002830 File Offset: 0x00000A30
		public int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, RaycastHit2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.BoxCastArray_Internal(this, origin, size, angle, direction, distance, contactFilter2D, results);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000286C File Offset: 0x00000A6C
		public int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return PhysicsScene2D.BoxCastArray_Internal(this, origin, size, angle, direction, distance, contactFilter, results);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002894 File Offset: 0x00000A94
		public int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, List<RaycastHit2D> results)
		{
			return PhysicsScene2D.BoxCastList_Internal(this, origin, size, angle, direction, distance, contactFilter, results);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000028BC File Offset: 0x00000ABC
		[NativeMethod("BoxCast_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D BoxCast_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			RaycastHit2D raycastHit2D;
			PhysicsScene2D.BoxCast_Internal_Injected(ref physicsScene, ref origin, ref size, angle, ref direction, distance, ref contactFilter, out raycastHit2D);
			return raycastHit2D;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028E0 File Offset: 0x00000AE0
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("BoxCastArray_Binding")]
		private unsafe static int BoxCastArray_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull] RaycastHit2D[] results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			Span<RaycastHit2D> span = new Span<RaycastHit2D>(results);
			int num;
			fixed (RaycastHit2D* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = PhysicsScene2D.BoxCastArray_Internal_Injected(ref physicsScene, ref origin, ref size, angle, ref direction, distance, ref contactFilter, ref managedSpanWrapper);
			}
			return num;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002938 File Offset: 0x00000B38
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("BoxCastList_Binding")]
		private unsafe static int BoxCastList_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull] List<RaycastHit2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			int num;
			try
			{
				fixed (RaycastHit2D[] array = NoAllocHelpers.ExtractArrayFromList<RaycastHit2D>(results))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, results.Count);
					num = PhysicsScene2D.BoxCastList_Internal_Injected(ref physicsScene, ref origin, ref size, angle, ref direction, distance, ref contactFilter, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<RaycastHit2D>(results);
			}
			return num;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029B8 File Offset: 0x00000BB8
		public RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.CapsuleCast_Internal(this, origin, size, capsuleDirection, angle, direction, distance, contactFilter2D);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000029F4 File Offset: 0x00000BF4
		public RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.CapsuleCast_Internal(this, origin, size, capsuleDirection, angle, direction, distance, contactFilter);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A1C File Offset: 0x00000C1C
		public int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, RaycastHit2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.CapsuleCastArray_Internal(this, origin, size, capsuleDirection, angle, direction, distance, contactFilter2D, results);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A58 File Offset: 0x00000C58
		public int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return PhysicsScene2D.CapsuleCastArray_Internal(this, origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A84 File Offset: 0x00000C84
		public int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, List<RaycastHit2D> results)
		{
			return PhysicsScene2D.CapsuleCastList_Internal(this, origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002AB0 File Offset: 0x00000CB0
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("CapsuleCast_Binding")]
		private static RaycastHit2D CapsuleCast_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			RaycastHit2D raycastHit2D;
			PhysicsScene2D.CapsuleCast_Internal_Injected(ref physicsScene, ref origin, ref size, capsuleDirection, angle, ref direction, distance, ref contactFilter, out raycastHit2D);
			return raycastHit2D;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002AD4 File Offset: 0x00000CD4
		[NativeMethod("CapsuleCastArray_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private unsafe static int CapsuleCastArray_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull] RaycastHit2D[] results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			Span<RaycastHit2D> span = new Span<RaycastHit2D>(results);
			int num;
			fixed (RaycastHit2D* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = PhysicsScene2D.CapsuleCastArray_Internal_Injected(ref physicsScene, ref origin, ref size, capsuleDirection, angle, ref direction, distance, ref contactFilter, ref managedSpanWrapper);
			}
			return num;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002B30 File Offset: 0x00000D30
		[NativeMethod("CapsuleCastList_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private unsafe static int CapsuleCastList_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, [NotNull] List<RaycastHit2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			int num;
			try
			{
				fixed (RaycastHit2D[] array = NoAllocHelpers.ExtractArrayFromList<RaycastHit2D>(results))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, results.Count);
					num = PhysicsScene2D.CapsuleCastList_Internal_Injected(ref physicsScene, ref origin, ref size, capsuleDirection, angle, ref direction, distance, ref contactFilter, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<RaycastHit2D>(results);
			}
			return num;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002BB4 File Offset: 0x00000DB4
		public RaycastHit2D GetRayIntersection(Ray ray, float distance, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			return PhysicsScene2D.GetRayIntersection_Internal(this, ray.origin, ray.direction, distance, layerMask);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002BE4 File Offset: 0x00000DE4
		public int GetRayIntersection(Ray ray, float distance, RaycastHit2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			return PhysicsScene2D.GetRayIntersectionArray_Internal(this, ray.origin, ray.direction, distance, layerMask, results);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002C14 File Offset: 0x00000E14
		public int GetRayIntersection(Ray ray, float distance, List<RaycastHit2D> results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			return PhysicsScene2D.GetRayIntersectionList_Internal(this, ray.origin, ray.direction, distance, layerMask, results);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002C44 File Offset: 0x00000E44
		[NativeMethod("GetRayIntersection_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D GetRayIntersection_Internal(PhysicsScene2D physicsScene, Vector3 origin, Vector3 direction, float distance, int layerMask)
		{
			RaycastHit2D raycastHit2D;
			PhysicsScene2D.GetRayIntersection_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, layerMask, out raycastHit2D);
			return raycastHit2D;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C64 File Offset: 0x00000E64
		[NativeMethod("GetRayIntersectionArray_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private unsafe static int GetRayIntersectionArray_Internal(PhysicsScene2D physicsScene, Vector3 origin, Vector3 direction, float distance, int layerMask, [NotNull] RaycastHit2D[] results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			Span<RaycastHit2D> span = new Span<RaycastHit2D>(results);
			int rayIntersectionArray_Internal_Injected;
			fixed (RaycastHit2D* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				rayIntersectionArray_Internal_Injected = PhysicsScene2D.GetRayIntersectionArray_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, layerMask, ref managedSpanWrapper);
			}
			return rayIntersectionArray_Internal_Injected;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002CB8 File Offset: 0x00000EB8
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("GetRayIntersectionList_Binding")]
		private unsafe static int GetRayIntersectionList_Internal(PhysicsScene2D physicsScene, Vector3 origin, Vector3 direction, float distance, int layerMask, [NotNull] List<RaycastHit2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			int rayIntersectionList_Internal_Injected;
			try
			{
				fixed (RaycastHit2D[] array = NoAllocHelpers.ExtractArrayFromList<RaycastHit2D>(results))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, results.Count);
					rayIntersectionList_Internal_Injected = PhysicsScene2D.GetRayIntersectionList_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, layerMask, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<RaycastHit2D>(results);
			}
			return rayIntersectionList_Internal_Injected;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002D34 File Offset: 0x00000F34
		public Collider2D OverlapPoint(Vector2 point, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.OverlapPoint_Internal(this, point, contactFilter2D);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002D64 File Offset: 0x00000F64
		public Collider2D OverlapPoint(Vector2 point, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.OverlapPoint_Internal(this, point, contactFilter);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002D84 File Offset: 0x00000F84
		public int OverlapPoint(Vector2 point, Collider2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.OverlapPointArray_Internal(this, point, contactFilter2D, results);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public int OverlapPoint(Vector2 point, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return PhysicsScene2D.OverlapPointArray_Internal(this, point, contactFilter, results);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public int OverlapPoint(Vector2 point, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapPointList_Internal(this, point, contactFilter, results);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002DF8 File Offset: 0x00000FF8
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("OverlapPoint_Binding")]
		private static Collider2D OverlapPoint_Internal(PhysicsScene2D physicsScene, Vector2 point, ContactFilter2D contactFilter)
		{
			return Unmarshal.UnmarshalUnityObject<Collider2D>(PhysicsScene2D.OverlapPoint_Internal_Injected(ref physicsScene, ref point, ref contactFilter));
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002E18 File Offset: 0x00001018
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("OverlapPointArray_Binding")]
		private static int OverlapPointArray_Internal(PhysicsScene2D physicsScene, Vector2 point, ContactFilter2D contactFilter, [NotNull] [Unmarshalled] Collider2D[] results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			return PhysicsScene2D.OverlapPointArray_Internal_Injected(ref physicsScene, ref point, ref contactFilter, results);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002E40 File Offset: 0x00001040
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("OverlapPointList_Binding")]
		private static int OverlapPointList_Internal(PhysicsScene2D physicsScene, Vector2 point, ContactFilter2D contactFilter, [NotNull] List<Collider2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			return PhysicsScene2D.OverlapPointList_Internal_Injected(ref physicsScene, ref point, ref contactFilter, results);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002E68 File Offset: 0x00001068
		public Collider2D OverlapCircle(Vector2 point, float radius, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.OverlapCircle_Internal(this, point, radius, contactFilter2D);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002E9C File Offset: 0x0000109C
		public Collider2D OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.OverlapCircle_Internal(this, point, radius, contactFilter);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002EBC File Offset: 0x000010BC
		public int OverlapCircle(Vector2 point, float radius, Collider2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.OverlapCircleArray_Internal(this, point, radius, contactFilter2D, results);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002EF0 File Offset: 0x000010F0
		public int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return PhysicsScene2D.OverlapCircleArray_Internal(this, point, radius, contactFilter, results);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002F14 File Offset: 0x00001114
		public int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapCircleList_Internal(this, point, radius, contactFilter, results);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002F38 File Offset: 0x00001138
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("OverlapCircle_Binding")]
		private static Collider2D OverlapCircle_Internal(PhysicsScene2D physicsScene, Vector2 point, float radius, ContactFilter2D contactFilter)
		{
			return Unmarshal.UnmarshalUnityObject<Collider2D>(PhysicsScene2D.OverlapCircle_Internal_Injected(ref physicsScene, ref point, radius, ref contactFilter));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002F58 File Offset: 0x00001158
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("OverlapCircleArray_Binding")]
		private static int OverlapCircleArray_Internal(PhysicsScene2D physicsScene, Vector2 point, float radius, ContactFilter2D contactFilter, [Unmarshalled] [NotNull] Collider2D[] results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			return PhysicsScene2D.OverlapCircleArray_Internal_Injected(ref physicsScene, ref point, radius, ref contactFilter, results);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002F84 File Offset: 0x00001184
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("OverlapCircleList_Binding")]
		private static int OverlapCircleList_Internal(PhysicsScene2D physicsScene, Vector2 point, float radius, ContactFilter2D contactFilter, [NotNull] List<Collider2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			return PhysicsScene2D.OverlapCircleList_Internal_Injected(ref physicsScene, ref point, radius, ref contactFilter, results);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002FB0 File Offset: 0x000011B0
		public Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.OverlapBox_Internal(this, point, size, angle, contactFilter2D);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002FE4 File Offset: 0x000011E4
		public Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.OverlapBox_Internal(this, point, size, angle, contactFilter);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003008 File Offset: 0x00001208
		public int OverlapBox(Vector2 point, Vector2 size, float angle, Collider2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.OverlapBoxArray_Internal(this, point, size, angle, contactFilter2D, results);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003040 File Offset: 0x00001240
		public int OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return PhysicsScene2D.OverlapBoxArray_Internal(this, point, size, angle, contactFilter, results);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003064 File Offset: 0x00001264
		public int OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapBoxList_Internal(this, point, size, angle, contactFilter, results);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003088 File Offset: 0x00001288
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("OverlapBox_Binding")]
		private static Collider2D OverlapBox_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter)
		{
			return Unmarshal.UnmarshalUnityObject<Collider2D>(PhysicsScene2D.OverlapBox_Internal_Injected(ref physicsScene, ref point, ref size, angle, ref contactFilter));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000030A8 File Offset: 0x000012A8
		[NativeMethod("OverlapBoxArray_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int OverlapBoxArray_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, [NotNull] [Unmarshalled] Collider2D[] results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			return PhysicsScene2D.OverlapBoxArray_Internal_Injected(ref physicsScene, ref point, ref size, angle, ref contactFilter, results);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000030D8 File Offset: 0x000012D8
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("OverlapBoxList_Binding")]
		private static int OverlapBoxList_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, [NotNull] List<Collider2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			return PhysicsScene2D.OverlapBoxList_Internal_Injected(ref physicsScene, ref point, ref size, angle, ref contactFilter, results);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003108 File Offset: 0x00001308
		public Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return this.OverlapAreaToBoxArray_Internal(pointA, pointB, contactFilter2D);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003134 File Offset: 0x00001334
		public Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter)
		{
			return this.OverlapAreaToBoxArray_Internal(pointA, pointB, contactFilter);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003150 File Offset: 0x00001350
		private Collider2D OverlapAreaToBoxArray_Internal(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter)
		{
			Vector2 vector = (pointA + pointB) * 0.5f;
			Vector2 vector2 = new Vector2(Mathf.Abs(pointA.x - pointB.x), Math.Abs(pointA.y - pointB.y));
			return this.OverlapBox(vector, vector2, 0f, contactFilter);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000031B0 File Offset: 0x000013B0
		public int OverlapArea(Vector2 pointA, Vector2 pointB, Collider2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return this.OverlapAreaToBoxArray_Internal(pointA, pointB, contactFilter2D, results);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000031E0 File Offset: 0x000013E0
		public int OverlapArea(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return this.OverlapAreaToBoxArray_Internal(pointA, pointB, contactFilter, results);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003200 File Offset: 0x00001400
		private int OverlapAreaToBoxArray_Internal(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, Collider2D[] results)
		{
			Vector2 vector = (pointA + pointB) * 0.5f;
			Vector2 vector2 = new Vector2(Mathf.Abs(pointA.x - pointB.x), Math.Abs(pointA.y - pointB.y));
			return this.OverlapBox(vector, vector2, 0f, contactFilter, results);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003260 File Offset: 0x00001460
		public int OverlapArea(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return this.OverlapAreaToBoxList_Internal(pointA, pointB, contactFilter, results);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003280 File Offset: 0x00001480
		private int OverlapAreaToBoxList_Internal(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			Vector2 vector = (pointA + pointB) * 0.5f;
			Vector2 vector2 = new Vector2(Mathf.Abs(pointA.x - pointB.x), Math.Abs(pointA.y - pointB.y));
			return this.OverlapBox(vector, vector2, 0f, contactFilter, results);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000032E0 File Offset: 0x000014E0
		public Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.OverlapCapsule_Internal(this, point, size, direction, angle, contactFilter2D);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003318 File Offset: 0x00001518
		public Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter)
		{
			return PhysicsScene2D.OverlapCapsule_Internal(this, point, size, direction, angle, contactFilter);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000333C File Offset: 0x0000153C
		public int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.OverlapCapsuleArray_Internal(this, point, size, direction, angle, contactFilter2D, results);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003374 File Offset: 0x00001574
		public int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return PhysicsScene2D.OverlapCapsuleArray_Internal(this, point, size, direction, angle, contactFilter, results);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000339C File Offset: 0x0000159C
		public int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapCapsuleList_Internal(this, point, size, direction, angle, contactFilter, results);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000033C4 File Offset: 0x000015C4
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("OverlapCapsule_Binding")]
		private static Collider2D OverlapCapsule_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter)
		{
			return Unmarshal.UnmarshalUnityObject<Collider2D>(PhysicsScene2D.OverlapCapsule_Internal_Injected(ref physicsScene, ref point, ref size, direction, angle, ref contactFilter));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000033E8 File Offset: 0x000015E8
		[NativeMethod("OverlapCapsuleArray_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int OverlapCapsuleArray_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, [NotNull] [Unmarshalled] Collider2D[] results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			return PhysicsScene2D.OverlapCapsuleArray_Internal_Injected(ref physicsScene, ref point, ref size, direction, angle, ref contactFilter, results);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003418 File Offset: 0x00001618
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("OverlapCapsuleList_Binding")]
		private static int OverlapCapsuleList_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, [NotNull] List<Collider2D> results)
		{
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			return PhysicsScene2D.OverlapCapsuleList_Internal_Injected(ref physicsScene, ref point, ref size, direction, angle, ref contactFilter, results);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003448 File Offset: 0x00001648
		public static int OverlapCollider(Collider2D collider, Collider2D[] results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return PhysicsScene2D.OverlapColliderFilteredArray_Internal(collider, contactFilter2D, results);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003474 File Offset: 0x00001674
		public static int OverlapCollider(Collider2D collider, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return PhysicsScene2D.OverlapColliderFilteredArray_Internal(collider, contactFilter, results);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003490 File Offset: 0x00001690
		public static int OverlapCollider(Collider2D collider, List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapColliderList_Internal(collider, results);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000034AC File Offset: 0x000016AC
		public static int OverlapCollider(Collider2D collider, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapColliderFilteredList_Internal(collider, contactFilter, results);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000034C8 File Offset: 0x000016C8
		public static int OverlapCollider(Vector2 position, float angle, Collider2D collider, List<Collider2D> results)
		{
			bool flag = collider.attachedRigidbody;
			if (flag)
			{
				return PhysicsScene2D.OverlapColliderFromList_Internal(position, angle, collider, results);
			}
			throw new InvalidOperationException("Cannot perform a Collider Overlap at a specific position and angle if the Collider is not attached to a Rigidbody2D.");
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003500 File Offset: 0x00001700
		public static int OverlapCollider(Vector2 position, float angle, Collider2D collider, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			bool flag = collider.attachedRigidbody;
			if (flag)
			{
				return PhysicsScene2D.OverlapColliderFromFilteredList_Internal(position, angle, collider, contactFilter, results);
			}
			throw new InvalidOperationException("Cannot perform a Collider Overlap at a specific position and angle if the Collider is not attached to a Rigidbody2D.");
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003538 File Offset: 0x00001738
		[NativeMethod("OverlapColliderFilteredArray_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int OverlapColliderFilteredArray_Internal([NotNull] Collider2D collider, ContactFilter2D contactFilter, [NotNull] [Unmarshalled] Collider2D[] results)
		{
			if (collider == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			return PhysicsScene2D.OverlapColliderFilteredArray_Internal_Injected(intPtr, ref contactFilter, results);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003580 File Offset: 0x00001780
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("OverlapColliderList_Binding")]
		private static int OverlapColliderList_Internal([NotNull] Collider2D collider, [NotNull] List<Collider2D> results)
		{
			if (collider == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			return PhysicsScene2D.OverlapColliderList_Internal_Injected(intPtr, results);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000035C8 File Offset: 0x000017C8
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("OverlapColliderFilteredList_Binding")]
		private static int OverlapColliderFilteredList_Internal([NotNull] Collider2D collider, ContactFilter2D contactFilter, [NotNull] List<Collider2D> results)
		{
			if (collider == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			return PhysicsScene2D.OverlapColliderFilteredList_Internal_Injected(intPtr, ref contactFilter, results);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003610 File Offset: 0x00001810
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("OverlapColliderFromList_Binding")]
		private static int OverlapColliderFromList_Internal(Vector2 position, float angle, [NotNull] Collider2D collider, [NotNull] List<Collider2D> results)
		{
			if (collider == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			return PhysicsScene2D.OverlapColliderFromList_Internal_Injected(ref position, angle, intPtr, results);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000365C File Offset: 0x0000185C
		[NativeMethod("OverlapColliderFromFilteredList_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int OverlapColliderFromFilteredList_Internal(Vector2 position, float angle, [NotNull] Collider2D collider, ContactFilter2D contactFilter, [NotNull] List<Collider2D> results)
		{
			if (collider == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			return PhysicsScene2D.OverlapColliderFromFilteredList_Internal_Injected(ref position, angle, intPtr, ref contactFilter, results);
		}

		// Token: 0x06000072 RID: 114
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValid_Internal_Injected([In] ref PhysicsScene2D physicsScene);

		// Token: 0x06000073 RID: 115
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsEmpty_Internal_Injected([In] ref PhysicsScene2D physicsScene);

		// Token: 0x06000074 RID: 116
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SubStepCount_Internal_Injected([In] ref PhysicsScene2D physicsScene);

		// Token: 0x06000075 RID: 117
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float SubStepLostTime_Internal_Injected([In] ref PhysicsScene2D physicsScene);

		// Token: 0x06000076 RID: 118
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Linecast_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 start, [In] ref Vector2 end, [In] ref ContactFilter2D contactFilter, out RaycastHit2D ret);

		// Token: 0x06000077 RID: 119
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LinecastArray_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 start, [In] ref Vector2 end, [In] ref ContactFilter2D contactFilter, ref ManagedSpanWrapper results);

		// Token: 0x06000078 RID: 120
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int LinecastList_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 start, [In] ref Vector2 end, [In] ref ContactFilter2D contactFilter, ref BlittableListWrapper results);

		// Token: 0x06000079 RID: 121
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Raycast_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 origin, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, out RaycastHit2D ret);

		// Token: 0x0600007A RID: 122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RaycastArray_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 origin, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, ref ManagedSpanWrapper results);

		// Token: 0x0600007B RID: 123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int RaycastList_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 origin, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, ref BlittableListWrapper results);

		// Token: 0x0600007C RID: 124
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CircleCast_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 origin, float radius, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, out RaycastHit2D ret);

		// Token: 0x0600007D RID: 125
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CircleCastArray_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 origin, float radius, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, ref ManagedSpanWrapper results);

		// Token: 0x0600007E RID: 126
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CircleCastList_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 origin, float radius, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, ref BlittableListWrapper results);

		// Token: 0x0600007F RID: 127
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BoxCast_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 origin, [In] ref Vector2 size, float angle, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, out RaycastHit2D ret);

		// Token: 0x06000080 RID: 128
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int BoxCastArray_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 origin, [In] ref Vector2 size, float angle, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, ref ManagedSpanWrapper results);

		// Token: 0x06000081 RID: 129
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int BoxCastList_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 origin, [In] ref Vector2 size, float angle, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, ref BlittableListWrapper results);

		// Token: 0x06000082 RID: 130
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CapsuleCast_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 origin, [In] ref Vector2 size, CapsuleDirection2D capsuleDirection, float angle, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, out RaycastHit2D ret);

		// Token: 0x06000083 RID: 131
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CapsuleCastArray_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 origin, [In] ref Vector2 size, CapsuleDirection2D capsuleDirection, float angle, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, ref ManagedSpanWrapper results);

		// Token: 0x06000084 RID: 132
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CapsuleCastList_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 origin, [In] ref Vector2 size, CapsuleDirection2D capsuleDirection, float angle, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, ref BlittableListWrapper results);

		// Token: 0x06000085 RID: 133
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRayIntersection_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector3 origin, [In] ref Vector3 direction, float distance, int layerMask, out RaycastHit2D ret);

		// Token: 0x06000086 RID: 134
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRayIntersectionArray_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector3 origin, [In] ref Vector3 direction, float distance, int layerMask, ref ManagedSpanWrapper results);

		// Token: 0x06000087 RID: 135
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRayIntersectionList_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector3 origin, [In] ref Vector3 direction, float distance, int layerMask, ref BlittableListWrapper results);

		// Token: 0x06000088 RID: 136
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr OverlapPoint_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 point, [In] ref ContactFilter2D contactFilter);

		// Token: 0x06000089 RID: 137
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapPointArray_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 point, [In] ref ContactFilter2D contactFilter, Collider2D[] results);

		// Token: 0x0600008A RID: 138
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapPointList_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 point, [In] ref ContactFilter2D contactFilter, List<Collider2D> results);

		// Token: 0x0600008B RID: 139
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr OverlapCircle_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 point, float radius, [In] ref ContactFilter2D contactFilter);

		// Token: 0x0600008C RID: 140
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapCircleArray_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 point, float radius, [In] ref ContactFilter2D contactFilter, Collider2D[] results);

		// Token: 0x0600008D RID: 141
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapCircleList_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 point, float radius, [In] ref ContactFilter2D contactFilter, List<Collider2D> results);

		// Token: 0x0600008E RID: 142
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr OverlapBox_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 point, [In] ref Vector2 size, float angle, [In] ref ContactFilter2D contactFilter);

		// Token: 0x0600008F RID: 143
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapBoxArray_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 point, [In] ref Vector2 size, float angle, [In] ref ContactFilter2D contactFilter, Collider2D[] results);

		// Token: 0x06000090 RID: 144
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapBoxList_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 point, [In] ref Vector2 size, float angle, [In] ref ContactFilter2D contactFilter, List<Collider2D> results);

		// Token: 0x06000091 RID: 145
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr OverlapCapsule_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 point, [In] ref Vector2 size, CapsuleDirection2D direction, float angle, [In] ref ContactFilter2D contactFilter);

		// Token: 0x06000092 RID: 146
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapCapsuleArray_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 point, [In] ref Vector2 size, CapsuleDirection2D direction, float angle, [In] ref ContactFilter2D contactFilter, Collider2D[] results);

		// Token: 0x06000093 RID: 147
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapCapsuleList_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 point, [In] ref Vector2 size, CapsuleDirection2D direction, float angle, [In] ref ContactFilter2D contactFilter, List<Collider2D> results);

		// Token: 0x06000094 RID: 148
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapColliderFilteredArray_Internal_Injected(IntPtr collider, [In] ref ContactFilter2D contactFilter, Collider2D[] results);

		// Token: 0x06000095 RID: 149
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapColliderList_Internal_Injected(IntPtr collider, List<Collider2D> results);

		// Token: 0x06000096 RID: 150
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapColliderFilteredList_Internal_Injected(IntPtr collider, [In] ref ContactFilter2D contactFilter, List<Collider2D> results);

		// Token: 0x06000097 RID: 151
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapColliderFromList_Internal_Injected([In] ref Vector2 position, float angle, IntPtr collider, List<Collider2D> results);

		// Token: 0x06000098 RID: 152
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapColliderFromFilteredList_Internal_Injected([In] ref Vector2 position, float angle, IntPtr collider, [In] ref ContactFilter2D contactFilter, List<Collider2D> results);

		// Token: 0x04000001 RID: 1
		private int m_Handle;
	}
}
