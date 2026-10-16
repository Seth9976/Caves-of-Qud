using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
	[NativeHeader("Physics2DScriptingClasses.h")]
	[NativeHeader("Modules/Physics2D/PhysicsManager2D.h")]
	[NativeHeader("Physics2DScriptingClasses.h")]
	public class Physics2D
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003714 File Offset: 0x00001914
		public static PhysicsScene2D defaultPhysicsScene
		{
			get
			{
				return default(PhysicsScene2D);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600009D RID: 157
		// (set) Token: 0x0600009E RID: 158
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern int velocityIterations
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600009F RID: 159
		// (set) Token: 0x060000A0 RID: 160
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern int positionIterations
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003730 File Offset: 0x00001930
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003748 File Offset: 0x00001948
		[StaticAccessor("GetPhysics2DSettings()")]
		public static Vector2 gravity
		{
			get
			{
				Vector2 vector;
				Physics2D.get_gravity_Injected(out vector);
				return vector;
			}
			set
			{
				Physics2D.set_gravity_Injected(ref value);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000A3 RID: 163
		// (set) Token: 0x060000A4 RID: 164
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern bool queriesHitTriggers
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000A5 RID: 165
		// (set) Token: 0x060000A6 RID: 166
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern bool queriesStartInColliders
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000A7 RID: 167
		// (set) Token: 0x060000A8 RID: 168
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern bool callbacksOnDisable
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000A9 RID: 169
		// (set) Token: 0x060000AA RID: 170
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern bool reuseCollisionCallbacks
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000AB RID: 171
		// (set) Token: 0x060000AC RID: 172
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern bool autoSyncTransforms
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000AD RID: 173
		// (set) Token: 0x060000AE RID: 174
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern SimulationMode2D simulationMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000375C File Offset: 0x0000195C
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003774 File Offset: 0x00001974
		[StaticAccessor("GetPhysics2DSettings()")]
		public static LayerMask simulationLayers
		{
			get
			{
				LayerMask layerMask;
				Physics2D.get_simulationLayers_Injected(out layerMask);
				return layerMask;
			}
			set
			{
				Physics2D.set_simulationLayers_Injected(ref value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000B1 RID: 177
		// (set) Token: 0x060000B2 RID: 178
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern bool useSubStepping
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000B3 RID: 179
		// (set) Token: 0x060000B4 RID: 180
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern bool useSubStepContacts
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B5 RID: 181
		// (set) Token: 0x060000B6 RID: 182
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float minSubStepFPS
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000B7 RID: 183
		// (set) Token: 0x060000B8 RID: 184
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern int maxSubStepCount
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003788 File Offset: 0x00001988
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000037A0 File Offset: 0x000019A0
		[StaticAccessor("GetPhysics2DSettings()")]
		public static PhysicsJobOptions2D jobOptions
		{
			get
			{
				PhysicsJobOptions2D physicsJobOptions2D;
				Physics2D.get_jobOptions_Injected(out physicsJobOptions2D);
				return physicsJobOptions2D;
			}
			set
			{
				Physics2D.set_jobOptions_Injected(ref value);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000BB RID: 187
		// (set) Token: 0x060000BC RID: 188
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float bounceThreshold
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000BD RID: 189
		// (set) Token: 0x060000BE RID: 190
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float contactThreshold
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000BF RID: 191
		// (set) Token: 0x060000C0 RID: 192
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float maxLinearCorrection
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000C1 RID: 193
		// (set) Token: 0x060000C2 RID: 194
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float maxAngularCorrection
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000C3 RID: 195
		// (set) Token: 0x060000C4 RID: 196
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float maxTranslationSpeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000C5 RID: 197
		// (set) Token: 0x060000C6 RID: 198
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float maxRotationSpeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000C7 RID: 199
		// (set) Token: 0x060000C8 RID: 200
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float defaultContactOffset
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000C9 RID: 201
		// (set) Token: 0x060000CA RID: 202
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float baumgarteScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000CB RID: 203
		// (set) Token: 0x060000CC RID: 204
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float baumgarteTOIScale
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000CD RID: 205
		// (set) Token: 0x060000CE RID: 206
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float timeToSleep
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000CF RID: 207
		// (set) Token: 0x060000D0 RID: 208
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float linearSleepTolerance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D1 RID: 209
		// (set) Token: 0x060000D2 RID: 210
		[StaticAccessor("GetPhysics2DSettings()")]
		public static extern float angularSleepTolerance
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000037B4 File Offset: 0x000019B4
		[ExcludeFromDocs]
		public static bool Simulate(float deltaTime)
		{
			return Physics2D.Simulate_Internal(Physics2D.defaultPhysicsScene, deltaTime, -1);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000037D4 File Offset: 0x000019D4
		public static bool Simulate(float deltaTime, [DefaultValue("Physics2D.AllLayers")] int simulationLayers = -1)
		{
			return Physics2D.Simulate_Internal(Physics2D.defaultPhysicsScene, deltaTime, simulationLayers);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000037F4 File Offset: 0x000019F4
		[NativeMethod("Simulate_Binding")]
		internal static bool Simulate_Internal(PhysicsScene2D physicsScene, float deltaTime, int simulationLayers)
		{
			return Physics2D.Simulate_Internal_Injected(ref physicsScene, deltaTime, simulationLayers);
		}

		// Token: 0x060000D6 RID: 214
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SyncTransforms();

		// Token: 0x060000D7 RID: 215 RVA: 0x0000380A File Offset: 0x00001A0A
		[ExcludeFromDocs]
		public static void IgnoreCollision(Collider2D collider1, Collider2D collider2)
		{
			Physics2D.IgnoreCollision(collider1, collider2, true);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003818 File Offset: 0x00001A18
		[StaticAccessor("PhysicsScene2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("IgnoreCollision_Binding")]
		public static void IgnoreCollision([NotNull] Collider2D collider1, [NotNull] Collider2D collider2, [DefaultValue("true")] bool ignore)
		{
			if (collider1 == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider1, "collider1");
			}
			if (collider2 == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider2, "collider2");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider1);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider1, "collider1");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider2);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider2, "collider2");
			}
			Physics2D.IgnoreCollision_Injected(intPtr, intPtr2, ignore);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003874 File Offset: 0x00001A74
		[StaticAccessor("PhysicsScene2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("GetIgnoreCollision_Binding")]
		public static bool GetIgnoreCollision([NotNull] Collider2D collider1, [NotNull] Collider2D collider2)
		{
			if (collider1 == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider1, "collider1");
			}
			if (collider2 == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider2, "collider2");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider1);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider1, "collider1");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider2);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider2, "collider2");
			}
			return Physics2D.GetIgnoreCollision_Injected(intPtr, intPtr2);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000038CE File Offset: 0x00001ACE
		[ExcludeFromDocs]
		public static void IgnoreLayerCollision(int layer1, int layer2)
		{
			Physics2D.IgnoreLayerCollision(layer1, layer2, true);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000038DC File Offset: 0x00001ADC
		public static void IgnoreLayerCollision(int layer1, int layer2, bool ignore)
		{
			bool flag = layer1 < 0 || layer1 > 31;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("layer1 is out of range. Layer numbers must be in the range 0 to 31.");
			}
			bool flag2 = layer2 < 0 || layer2 > 31;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("layer2 is out of range. Layer numbers must be in the range 0 to 31.");
			}
			Physics2D.IgnoreLayerCollision_Internal(layer1, layer2, ignore);
		}

		// Token: 0x060000DC RID: 220
		[NativeMethod("IgnoreLayerCollision")]
		[StaticAccessor("GetPhysics2DSettings()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IgnoreLayerCollision_Internal(int layer1, int layer2, bool ignore);

		// Token: 0x060000DD RID: 221 RVA: 0x0000392C File Offset: 0x00001B2C
		public static bool GetIgnoreLayerCollision(int layer1, int layer2)
		{
			bool flag = layer1 < 0 || layer1 > 31;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("layer1 is out of range. Layer numbers must be in the range 0 to 31.");
			}
			bool flag2 = layer2 < 0 || layer2 > 31;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("layer2 is out of range. Layer numbers must be in the range 0 to 31.");
			}
			return Physics2D.GetIgnoreLayerCollision_Internal(layer1, layer2);
		}

		// Token: 0x060000DE RID: 222
		[StaticAccessor("GetPhysics2DSettings()")]
		[NativeMethod("GetIgnoreLayerCollision")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIgnoreLayerCollision_Internal(int layer1, int layer2);

		// Token: 0x060000DF RID: 223 RVA: 0x0000397C File Offset: 0x00001B7C
		public static void SetLayerCollisionMask(int layer, int layerMask)
		{
			bool flag = layer < 0 || layer > 31;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("layer1 is out of range. Layer numbers must be in the range 0 to 31.");
			}
			Physics2D.SetLayerCollisionMask_Internal(layer, layerMask);
		}

		// Token: 0x060000E0 RID: 224
		[NativeMethod("SetLayerCollisionMask")]
		[StaticAccessor("GetPhysics2DSettings()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLayerCollisionMask_Internal(int layer, int layerMask);

		// Token: 0x060000E1 RID: 225 RVA: 0x000039B0 File Offset: 0x00001BB0
		public static int GetLayerCollisionMask(int layer)
		{
			bool flag = layer < 0 || layer > 31;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("layer1 is out of range. Layer numbers must be in the range 0 to 31.");
			}
			return Physics2D.GetLayerCollisionMask_Internal(layer);
		}

		// Token: 0x060000E2 RID: 226
		[NativeMethod("GetLayerCollisionMask")]
		[StaticAccessor("GetPhysics2DSettings()")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLayerCollisionMask_Internal(int layer);

		// Token: 0x060000E3 RID: 227 RVA: 0x000039E4 File Offset: 0x00001BE4
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		public static bool IsTouching([NotNull] Collider2D collider1, [NotNull] Collider2D collider2)
		{
			if (collider1 == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider1, "collider1");
			}
			if (collider2 == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider2, "collider2");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider1);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider1, "collider1");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider2);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider2, "collider2");
			}
			return Physics2D.IsTouching_Injected(intPtr, intPtr2);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003A40 File Offset: 0x00001C40
		public static bool IsTouching(Collider2D collider1, Collider2D collider2, ContactFilter2D contactFilter)
		{
			return Physics2D.IsTouching_TwoCollidersWithFilter(collider1, collider2, contactFilter);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003A5C File Offset: 0x00001C5C
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("IsTouching")]
		private static bool IsTouching_TwoCollidersWithFilter([NotNull] Collider2D collider1, [NotNull] Collider2D collider2, ContactFilter2D contactFilter)
		{
			if (collider1 == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider1, "collider1");
			}
			if (collider2 == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider2, "collider2");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider1);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider1, "collider1");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider2);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider2, "collider2");
			}
			return Physics2D.IsTouching_TwoCollidersWithFilter_Injected(intPtr, intPtr2, ref contactFilter);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public static bool IsTouching(Collider2D collider, ContactFilter2D contactFilter)
		{
			return Physics2D.IsTouching_SingleColliderWithFilter(collider, contactFilter);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003AD4 File Offset: 0x00001CD4
		[NativeMethod("IsTouching")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static bool IsTouching_SingleColliderWithFilter([NotNull] Collider2D collider, ContactFilter2D contactFilter)
		{
			if (collider == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			return Physics2D.IsTouching_SingleColliderWithFilter_Injected(intPtr, ref contactFilter);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00003B0C File Offset: 0x00001D0C
		[ExcludeFromDocs]
		public static bool IsTouchingLayers(Collider2D collider)
		{
			return Physics2D.IsTouchingLayers(collider, -1);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003B28 File Offset: 0x00001D28
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		public static bool IsTouchingLayers([NotNull] Collider2D collider, [DefaultValue("Physics2D.AllLayers")] int layerMask)
		{
			if (collider == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			return Physics2D.IsTouchingLayers_Injected(intPtr, layerMask);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003B60 File Offset: 0x00001D60
		public static ColliderDistance2D Distance(Collider2D colliderA, Collider2D colliderB)
		{
			bool flag = colliderA == colliderB;
			if (flag)
			{
				throw new ArgumentException("Cannot calculate the distance between the same collider.");
			}
			return Physics2D.Distance_Internal(colliderA, colliderB);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003B90 File Offset: 0x00001D90
		public static ColliderDistance2D Distance(Collider2D colliderA, Vector2 positionA, float angleA, Collider2D colliderB, Vector2 positionB, float angleB)
		{
			bool flag = colliderA == colliderB;
			if (flag)
			{
				throw new ArgumentException("Cannot calculate the distance between the same collider.");
			}
			bool flag2 = !colliderA.attachedRigidbody || !colliderB.attachedRigidbody;
			if (flag2)
			{
				throw new InvalidOperationException("Cannot perform a Collider Distance at a specific position and angle if the Collider is not attached to a Rigidbody2D.");
			}
			return Physics2D.DistanceFrom_Internal(colliderA, positionA, angleA, colliderB, positionB, angleB);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003BF4 File Offset: 0x00001DF4
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("Distance")]
		private static ColliderDistance2D Distance_Internal([NotNull] Collider2D colliderA, [NotNull] Collider2D colliderB)
		{
			if (colliderA == null)
			{
				ThrowHelper.ThrowArgumentNullException(colliderA, "colliderA");
			}
			if (colliderB == null)
			{
				ThrowHelper.ThrowArgumentNullException(colliderB, "colliderB");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(colliderA);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(colliderA, "colliderA");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(colliderB);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(colliderB, "colliderB");
			}
			ColliderDistance2D colliderDistance2D;
			Physics2D.Distance_Internal_Injected(intPtr, intPtr2, out colliderDistance2D);
			return colliderDistance2D;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003C54 File Offset: 0x00001E54
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("DistanceFrom")]
		private static ColliderDistance2D DistanceFrom_Internal([NotNull] Collider2D colliderA, Vector2 positionA, float angleA, [NotNull] Collider2D colliderB, Vector2 positionB, float angleB)
		{
			if (colliderA == null)
			{
				ThrowHelper.ThrowArgumentNullException(colliderA, "colliderA");
			}
			if (colliderB == null)
			{
				ThrowHelper.ThrowArgumentNullException(colliderB, "colliderB");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(colliderA);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(colliderA, "colliderA");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(colliderB);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(colliderB, "colliderB");
			}
			ColliderDistance2D colliderDistance2D;
			Physics2D.DistanceFrom_Internal_Injected(intPtr, ref positionA, angleA, intPtr2, ref positionB, angleB, out colliderDistance2D);
			return colliderDistance2D;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003CB8 File Offset: 0x00001EB8
		public static Vector2 ClosestPoint(Vector2 position, Collider2D collider)
		{
			bool flag = collider == null;
			if (flag)
			{
				throw new ArgumentNullException("Collider cannot be NULL.");
			}
			return Physics2D.ClosestPoint_Collider(position, collider);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003CE8 File Offset: 0x00001EE8
		public static Vector2 ClosestPoint(Vector2 position, Rigidbody2D rigidbody)
		{
			bool flag = rigidbody == null;
			if (flag)
			{
				throw new ArgumentNullException("Rigidbody cannot be NULL.");
			}
			return Physics2D.ClosestPoint_Rigidbody(position, rigidbody);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003D18 File Offset: 0x00001F18
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("ClosestPoint")]
		private static Vector2 ClosestPoint_Collider(Vector2 position, [NotNull] Collider2D collider)
		{
			if (collider == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			Vector2 vector;
			Physics2D.ClosestPoint_Collider_Injected(ref position, intPtr, out vector);
			return vector;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003D54 File Offset: 0x00001F54
		[NativeMethod("ClosestPoint")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static Vector2 ClosestPoint_Rigidbody(Vector2 position, [NotNull] Rigidbody2D rigidbody)
		{
			if (rigidbody == null)
			{
				ThrowHelper.ThrowArgumentNullException(rigidbody, "rigidbody");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(rigidbody);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rigidbody, "rigidbody");
			}
			Vector2 vector;
			Physics2D.ClosestPoint_Rigidbody_Injected(ref position, intPtr, out vector);
			return vector;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003D90 File Offset: 0x00001F90
		[ExcludeFromDocs]
		public static RaycastHit2D Linecast(Vector2 start, Vector2 end)
		{
			return Physics2D.defaultPhysicsScene.Linecast(start, end, -5);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003DB4 File Offset: 0x00001FB4
		[ExcludeFromDocs]
		public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.Linecast(start, end, contactFilter2D);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003DE8 File Offset: 0x00001FE8
		[ExcludeFromDocs]
		public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.Linecast(start, end, contactFilter2D);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003E18 File Offset: 0x00002018
		public static RaycastHit2D Linecast(Vector2 start, Vector2 end, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.Linecast(start, end, contactFilter2D);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003E44 File Offset: 0x00002044
		public static int Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.Linecast(start, end, contactFilter, results);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003E68 File Offset: 0x00002068
		public static int Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter, List<RaycastHit2D> results)
		{
			return Physics2D.defaultPhysicsScene.Linecast(start, end, contactFilter, results);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003E8C File Offset: 0x0000208C
		[ExcludeFromDocs]
		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.LinecastAll_Internal(Physics2D.defaultPhysicsScene, start, end, contactFilter2D);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003EC0 File Offset: 0x000020C0
		[ExcludeFromDocs]
		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.LinecastAll_Internal(Physics2D.defaultPhysicsScene, start, end, contactFilter2D);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003EF0 File Offset: 0x000020F0
		[ExcludeFromDocs]
		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.LinecastAll_Internal(Physics2D.defaultPhysicsScene, start, end, contactFilter2D);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003F1C File Offset: 0x0000211C
		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.LinecastAll_Internal(Physics2D.defaultPhysicsScene, start, end, contactFilter2D);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003F48 File Offset: 0x00002148
		[NativeMethod("LinecastAll_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D[] LinecastAll_Internal(PhysicsScene2D physicsScene, Vector2 start, Vector2 end, ContactFilter2D contactFilter)
		{
			RaycastHit2D[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Physics2D.LinecastAll_Internal_Injected(ref physicsScene, ref start, ref end, ref contactFilter, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				RaycastHit2D[] array;
				blittableArrayWrapper.Unmarshal<RaycastHit2D>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00003F84 File Offset: 0x00002184
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter2D, results);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003FBC File Offset: 0x000021BC
		[ExcludeFromDocs]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction)
		{
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, float.PositiveInfinity, -5);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003FE4 File Offset: 0x000021E4
		[ExcludeFromDocs]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance)
		{
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, -5);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004008 File Offset: 0x00002208
		[ExcludeFromDocs]
		[RequiredByNativeCode]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter2D);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000403C File Offset: 0x0000223C
		[ExcludeFromDocs]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter2D);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004070 File Offset: 0x00002270
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter2D);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000040A0 File Offset: 0x000022A0
		[ExcludeFromDocs]
		public static int Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, float.PositiveInfinity, contactFilter, results);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000040C8 File Offset: 0x000022C8
		public static int Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance)
		{
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter, results);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000040F0 File Offset: 0x000022F0
		public static int Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter, results);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004118 File Offset: 0x00002318
		[ExcludeFromDocs]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.RaycastAll_Internal(Physics2D.defaultPhysicsScene, origin, direction, float.PositiveInfinity, contactFilter2D);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004150 File Offset: 0x00002350
		[ExcludeFromDocs]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.RaycastAll_Internal(Physics2D.defaultPhysicsScene, origin, direction, distance, contactFilter2D);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004184 File Offset: 0x00002384
		[ExcludeFromDocs]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.RaycastAll_Internal(Physics2D.defaultPhysicsScene, origin, direction, distance, contactFilter2D);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000041B8 File Offset: 0x000023B8
		[ExcludeFromDocs]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.RaycastAll_Internal(Physics2D.defaultPhysicsScene, origin, direction, distance, contactFilter2D);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000041E8 File Offset: 0x000023E8
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.RaycastAll_Internal(Physics2D.defaultPhysicsScene, origin, direction, distance, contactFilter2D);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004214 File Offset: 0x00002414
		[NativeMethod("RaycastAll_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D[] RaycastAll_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			RaycastHit2D[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Physics2D.RaycastAll_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, ref contactFilter, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				RaycastHit2D[] array;
				blittableArrayWrapper.Unmarshal<RaycastHit2D>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004250 File Offset: 0x00002450
		[ExcludeFromDocs]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction)
		{
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, float.PositiveInfinity, -5);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000427C File Offset: 0x0000247C
		[ExcludeFromDocs]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance)
		{
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, -5);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000042A4 File Offset: 0x000024A4
		[ExcludeFromDocs]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter2D);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000042DC File Offset: 0x000024DC
		[ExcludeFromDocs]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter2D);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004310 File Offset: 0x00002510
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter2D);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004340 File Offset: 0x00002540
		[ExcludeFromDocs]
		public static int CircleCast(Vector2 origin, float radius, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, float.PositiveInfinity, contactFilter, results);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000436C File Offset: 0x0000256C
		public static int CircleCast(Vector2 origin, float radius, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance)
		{
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter, results);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004394 File Offset: 0x00002594
		public static int CircleCast(Vector2 origin, float radius, Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter, results);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000043BC File Offset: 0x000025BC
		[ExcludeFromDocs]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.CircleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, radius, direction, float.PositiveInfinity, contactFilter2D);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000043F4 File Offset: 0x000025F4
		[ExcludeFromDocs]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.CircleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, radius, direction, distance, contactFilter2D);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004428 File Offset: 0x00002628
		[ExcludeFromDocs]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.CircleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, radius, direction, distance, contactFilter2D);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000445C File Offset: 0x0000265C
		[ExcludeFromDocs]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.CircleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, radius, direction, distance, contactFilter2D);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000448C File Offset: 0x0000268C
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.CircleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, radius, direction, distance, contactFilter2D);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000044BC File Offset: 0x000026BC
		[NativeMethod("CircleCastAll_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D[] CircleCastAll_Internal(PhysicsScene2D physicsScene, Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			RaycastHit2D[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Physics2D.CircleCastAll_Internal_Injected(ref physicsScene, ref origin, radius, ref direction, distance, ref contactFilter, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				RaycastHit2D[] array;
				blittableArrayWrapper.Unmarshal<RaycastHit2D>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000044FC File Offset: 0x000026FC
		[ExcludeFromDocs]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction)
		{
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, float.PositiveInfinity, -5);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004528 File Offset: 0x00002728
		[ExcludeFromDocs]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance)
		{
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, -5);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004550 File Offset: 0x00002750
		[ExcludeFromDocs]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter2D);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004588 File Offset: 0x00002788
		[ExcludeFromDocs]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter2D);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000045C0 File Offset: 0x000027C0
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("Physics2D.AllLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter2D);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000045F4 File Offset: 0x000027F4
		[ExcludeFromDocs]
		public static int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, float.PositiveInfinity, contactFilter, results);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004620 File Offset: 0x00002820
		public static int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance)
		{
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter, results);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000464C File Offset: 0x0000284C
		public static int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter, results);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004678 File Offset: 0x00002878
		[ExcludeFromDocs]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.BoxCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, angle, direction, float.PositiveInfinity, contactFilter2D);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000046B0 File Offset: 0x000028B0
		[ExcludeFromDocs]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.BoxCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, angle, direction, distance, contactFilter2D);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000046E8 File Offset: 0x000028E8
		[ExcludeFromDocs]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.BoxCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, angle, direction, distance, contactFilter2D);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004720 File Offset: 0x00002920
		[ExcludeFromDocs]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.BoxCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, angle, direction, distance, contactFilter2D);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004754 File Offset: 0x00002954
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.BoxCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, angle, direction, distance, contactFilter2D);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004784 File Offset: 0x00002984
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("BoxCastAll_Binding")]
		private static RaycastHit2D[] BoxCastAll_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			RaycastHit2D[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Physics2D.BoxCastAll_Internal_Injected(ref physicsScene, ref origin, ref size, angle, ref direction, distance, ref contactFilter, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				RaycastHit2D[] array;
				blittableArrayWrapper.Unmarshal<RaycastHit2D>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000047C4 File Offset: 0x000029C4
		[ExcludeFromDocs]
		public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction)
		{
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, float.PositiveInfinity, -5);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000047F0 File Offset: 0x000029F0
		[ExcludeFromDocs]
		public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance)
		{
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, -5);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000481C File Offset: 0x00002A1C
		[ExcludeFromDocs]
		public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter2D);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004858 File Offset: 0x00002A58
		[ExcludeFromDocs]
		public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter2D);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004890 File Offset: 0x00002A90
		public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter2D);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000048C4 File Offset: 0x00002AC4
		[ExcludeFromDocs]
		public static int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, float.PositiveInfinity, contactFilter, results);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000048F4 File Offset: 0x00002AF4
		public static int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance)
		{
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004920 File Offset: 0x00002B20
		public static int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, ContactFilter2D contactFilter, List<RaycastHit2D> results, [DefaultValue("Mathf.Infinity")] float distance = float.PositiveInfinity)
		{
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000494C File Offset: 0x00002B4C
		[ExcludeFromDocs]
		public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.CapsuleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, capsuleDirection, angle, direction, float.PositiveInfinity, contactFilter2D);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004988 File Offset: 0x00002B88
		[ExcludeFromDocs]
		public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.CapsuleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, capsuleDirection, angle, direction, distance, contactFilter2D);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000049C0 File Offset: 0x00002BC0
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("CapsuleCastAll_Binding")]
		private static RaycastHit2D[] CapsuleCastAll_Internal(PhysicsScene2D physicsScene, Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
		{
			RaycastHit2D[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Physics2D.CapsuleCastAll_Internal_Injected(ref physicsScene, ref origin, ref size, capsuleDirection, angle, ref direction, distance, ref contactFilter, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				RaycastHit2D[] array;
				blittableArrayWrapper.Unmarshal<RaycastHit2D>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004A04 File Offset: 0x00002C04
		[ExcludeFromDocs]
		public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.CapsuleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, capsuleDirection, angle, direction, distance, contactFilter2D);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004A3C File Offset: 0x00002C3C
		[ExcludeFromDocs]
		public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.CapsuleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, capsuleDirection, angle, direction, distance, contactFilter2D);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004A70 File Offset: 0x00002C70
		public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.CapsuleCastAll_Internal(Physics2D.defaultPhysicsScene, origin, size, capsuleDirection, angle, direction, distance, contactFilter2D);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004AA4 File Offset: 0x00002CA4
		[ExcludeFromDocs]
		public static RaycastHit2D GetRayIntersection(Ray ray)
		{
			return Physics2D.defaultPhysicsScene.GetRayIntersection(ray, float.PositiveInfinity, -5);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004ACC File Offset: 0x00002CCC
		[ExcludeFromDocs]
		public static RaycastHit2D GetRayIntersection(Ray ray, float distance)
		{
			return Physics2D.defaultPhysicsScene.GetRayIntersection(ray, distance, -5);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public static RaycastHit2D GetRayIntersection(Ray ray, float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5)
		{
			return Physics2D.defaultPhysicsScene.GetRayIntersection(ray, distance, layerMask);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004B14 File Offset: 0x00002D14
		public static int GetRayIntersection(Ray ray, float distance, List<RaycastHit2D> results, [DefaultValue("Physics2D.DefaultRaycastLayers")] int layerMask = -5)
		{
			return Physics2D.defaultPhysicsScene.GetRayIntersection(ray, distance, results, layerMask);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004B38 File Offset: 0x00002D38
		[ExcludeFromDocs]
		public static RaycastHit2D[] GetRayIntersectionAll(Ray ray)
		{
			return Physics2D.GetRayIntersectionAll_Internal(Physics2D.defaultPhysicsScene, ray.origin, ray.direction, float.PositiveInfinity, -5);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004B6C File Offset: 0x00002D6C
		[ExcludeFromDocs]
		public static RaycastHit2D[] GetRayIntersectionAll(Ray ray, float distance)
		{
			return Physics2D.GetRayIntersectionAll_Internal(Physics2D.defaultPhysicsScene, ray.origin, ray.direction, distance, -5);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004B9C File Offset: 0x00002D9C
		[RequiredByNativeCode]
		public static RaycastHit2D[] GetRayIntersectionAll(Ray ray, float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5)
		{
			return Physics2D.GetRayIntersectionAll_Internal(Physics2D.defaultPhysicsScene, ray.origin, ray.direction, distance, layerMask);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004BC8 File Offset: 0x00002DC8
		[NativeMethod("GetRayIntersectionAll_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static RaycastHit2D[] GetRayIntersectionAll_Internal(PhysicsScene2D physicsScene, Vector3 origin, Vector3 direction, float distance, int layerMask)
		{
			RaycastHit2D[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Physics2D.GetRayIntersectionAll_Internal_Injected(ref physicsScene, ref origin, ref direction, distance, layerMask, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				RaycastHit2D[] array;
				blittableArrayWrapper.Unmarshal<RaycastHit2D>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004C04 File Offset: 0x00002E04
		[ExcludeFromDocs]
		[RequiredByNativeCode]
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results, float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5)
		{
			return Physics2D.defaultPhysicsScene.GetRayIntersection(ray, distance, results, layerMask);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004C28 File Offset: 0x00002E28
		[ExcludeFromDocs]
		public static Collider2D OverlapPoint(Vector2 point)
		{
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, -5);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004C4C File Offset: 0x00002E4C
		[ExcludeFromDocs]
		public static Collider2D OverlapPoint(Vector2 point, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, contactFilter2D);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004C80 File Offset: 0x00002E80
		[ExcludeFromDocs]
		public static Collider2D OverlapPoint(Vector2 point, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, contactFilter2D);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004CB0 File Offset: 0x00002EB0
		public static Collider2D OverlapPoint(Vector2 point, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, contactFilter2D);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004CDC File Offset: 0x00002EDC
		public static int OverlapPoint(Vector2 point, ContactFilter2D contactFilter, [Unmarshalled] Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, contactFilter, results);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004D00 File Offset: 0x00002F00
		public static int OverlapPoint(Vector2 point, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, contactFilter, results);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004D24 File Offset: 0x00002F24
		[ExcludeFromDocs]
		public static Collider2D[] OverlapPointAll(Vector2 point)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.OverlapPointAll_Internal(Physics2D.defaultPhysicsScene, point, contactFilter2D);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004D54 File Offset: 0x00002F54
		[ExcludeFromDocs]
		public static Collider2D[] OverlapPointAll(Vector2 point, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.OverlapPointAll_Internal(Physics2D.defaultPhysicsScene, point, contactFilter2D);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004D84 File Offset: 0x00002F84
		[ExcludeFromDocs]
		public static Collider2D[] OverlapPointAll(Vector2 point, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.OverlapPointAll_Internal(Physics2D.defaultPhysicsScene, point, contactFilter2D);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004DB0 File Offset: 0x00002FB0
		public static Collider2D[] OverlapPointAll(Vector2 point, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.OverlapPointAll_Internal(Physics2D.defaultPhysicsScene, point, contactFilter2D);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004DD8 File Offset: 0x00002FD8
		[NativeMethod("OverlapPointAll_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static Collider2D[] OverlapPointAll_Internal(PhysicsScene2D physicsScene, Vector2 point, ContactFilter2D contactFilter)
		{
			return Physics2D.OverlapPointAll_Internal_Injected(ref physicsScene, ref point, ref contactFilter);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004DF0 File Offset: 0x00002FF0
		[ExcludeFromDocs]
		public static Collider2D OverlapCircle(Vector2 point, float radius)
		{
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, -5);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004E14 File Offset: 0x00003014
		[ExcludeFromDocs]
		public static Collider2D OverlapCircle(Vector2 point, float radius, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, contactFilter2D);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004E48 File Offset: 0x00003048
		[ExcludeFromDocs]
		public static Collider2D OverlapCircle(Vector2 point, float radius, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, contactFilter2D);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004E78 File Offset: 0x00003078
		public static Collider2D OverlapCircle(Vector2 point, float radius, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, contactFilter2D);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004EA4 File Offset: 0x000030A4
		public static int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, contactFilter, results);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004EC8 File Offset: 0x000030C8
		public static int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, contactFilter, results);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004EEC File Offset: 0x000030EC
		[ExcludeFromDocs]
		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.OverlapCircleAll_Internal(Physics2D.defaultPhysicsScene, point, radius, contactFilter2D);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00004F20 File Offset: 0x00003120
		[ExcludeFromDocs]
		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.OverlapCircleAll_Internal(Physics2D.defaultPhysicsScene, point, radius, contactFilter2D);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004F50 File Offset: 0x00003150
		[ExcludeFromDocs]
		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.OverlapCircleAll_Internal(Physics2D.defaultPhysicsScene, point, radius, contactFilter2D);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00004F7C File Offset: 0x0000317C
		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.OverlapCircleAll_Internal(Physics2D.defaultPhysicsScene, point, radius, contactFilter2D);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00004FA8 File Offset: 0x000031A8
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("OverlapCircleAll_Binding")]
		private static Collider2D[] OverlapCircleAll_Internal(PhysicsScene2D physicsScene, Vector2 point, float radius, ContactFilter2D contactFilter)
		{
			return Physics2D.OverlapCircleAll_Internal_Injected(ref physicsScene, ref point, radius, ref contactFilter);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00004FC4 File Offset: 0x000031C4
		[ExcludeFromDocs]
		public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle)
		{
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, -5);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00004FE8 File Offset: 0x000031E8
		[ExcludeFromDocs]
		public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter2D);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000501C File Offset: 0x0000321C
		[ExcludeFromDocs]
		public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter2D);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005050 File Offset: 0x00003250
		public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter2D);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005080 File Offset: 0x00003280
		public static int OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter, results);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000050A8 File Offset: 0x000032A8
		public static int OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter, results);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000050D0 File Offset: 0x000032D0
		[ExcludeFromDocs]
		public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.OverlapBoxAll_Internal(Physics2D.defaultPhysicsScene, point, size, angle, contactFilter2D);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005104 File Offset: 0x00003304
		[ExcludeFromDocs]
		public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.OverlapBoxAll_Internal(Physics2D.defaultPhysicsScene, point, size, angle, contactFilter2D);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005138 File Offset: 0x00003338
		[ExcludeFromDocs]
		public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.OverlapBoxAll_Internal(Physics2D.defaultPhysicsScene, point, size, angle, contactFilter2D);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00005168 File Offset: 0x00003368
		public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.OverlapBoxAll_Internal(Physics2D.defaultPhysicsScene, point, size, angle, contactFilter2D);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005194 File Offset: 0x00003394
		[NativeMethod("OverlapBoxAll_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static Collider2D[] OverlapBoxAll_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter)
		{
			return Physics2D.OverlapBoxAll_Internal_Injected(ref physicsScene, ref point, ref size, angle, ref contactFilter);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000051B0 File Offset: 0x000033B0
		[ExcludeFromDocs]
		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB)
		{
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, -5);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000051D4 File Offset: 0x000033D4
		[ExcludeFromDocs]
		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter2D);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00005208 File Offset: 0x00003408
		[ExcludeFromDocs]
		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter2D);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00005238 File Offset: 0x00003438
		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter2D);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005264 File Offset: 0x00003464
		public static int OverlapArea(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter, results);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00005288 File Offset: 0x00003488
		public static int OverlapArea(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter, results);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000052AC File Offset: 0x000034AC
		[ExcludeFromDocs]
		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB)
		{
			return Physics2D.OverlapAreaAllToBox_Internal(pointA, pointB, -5, float.NegativeInfinity, float.PositiveInfinity);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000052D4 File Offset: 0x000034D4
		[ExcludeFromDocs]
		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, int layerMask)
		{
			return Physics2D.OverlapAreaAllToBox_Internal(pointA, pointB, layerMask, float.NegativeInfinity, float.PositiveInfinity);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000052F8 File Offset: 0x000034F8
		[ExcludeFromDocs]
		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth)
		{
			return Physics2D.OverlapAreaAllToBox_Internal(pointA, pointB, layerMask, minDepth, float.PositiveInfinity);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005318 File Offset: 0x00003518
		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.OverlapAreaAllToBox_Internal(pointA, pointB, layerMask, minDepth, maxDepth);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005338 File Offset: 0x00003538
		private static Collider2D[] OverlapAreaAllToBox_Internal(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth, float maxDepth)
		{
			Vector2 vector = (pointA + pointB) * 0.5f;
			Vector2 vector2 = new Vector2(Mathf.Abs(pointA.x - pointB.x), Math.Abs(pointA.y - pointB.y));
			return Physics2D.OverlapBoxAll(vector, vector2, 0f, layerMask, minDepth, maxDepth);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005398 File Offset: 0x00003598
		[ExcludeFromDocs]
		public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle)
		{
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, -5);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000053C0 File Offset: 0x000035C0
		[ExcludeFromDocs]
		public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter2D);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000053F8 File Offset: 0x000035F8
		[ExcludeFromDocs]
		public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter2D);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000542C File Offset: 0x0000362C
		public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter2D);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000545C File Offset: 0x0000365C
		public static int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter, results);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005484 File Offset: 0x00003684
		public static int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter, results);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000054AC File Offset: 0x000036AC
		[ExcludeFromDocs]
		public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.OverlapCapsuleAll_Internal(Physics2D.defaultPhysicsScene, point, size, direction, angle, contactFilter2D);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000054E0 File Offset: 0x000036E0
		[ExcludeFromDocs]
		public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.OverlapCapsuleAll_Internal(Physics2D.defaultPhysicsScene, point, size, direction, angle, contactFilter2D);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005514 File Offset: 0x00003714
		[ExcludeFromDocs]
		public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.OverlapCapsuleAll_Internal(Physics2D.defaultPhysicsScene, point, size, direction, angle, contactFilter2D);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005544 File Offset: 0x00003744
		public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.OverlapCapsuleAll_Internal(Physics2D.defaultPhysicsScene, point, size, direction, angle, contactFilter2D);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005574 File Offset: 0x00003774
		[NativeMethod("OverlapCapsuleAll_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static Collider2D[] OverlapCapsuleAll_Internal(PhysicsScene2D physicsScene, Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter)
		{
			return Physics2D.OverlapCapsuleAll_Internal_Injected(ref physicsScene, ref point, ref size, direction, angle, ref contactFilter);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005594 File Offset: 0x00003794
		public static int OverlapCollider(Collider2D collider, ContactFilter2D contactFilter, Collider2D[] results)
		{
			return PhysicsScene2D.OverlapCollider(collider, contactFilter, results);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000055B0 File Offset: 0x000037B0
		public static int OverlapCollider(Collider2D collider, ContactFilter2D contactFilter, List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapCollider(collider, contactFilter, results);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000055CC File Offset: 0x000037CC
		public static int OverlapCollider(Collider2D collider, List<Collider2D> results)
		{
			return PhysicsScene2D.OverlapCollider(collider, results);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000055E8 File Offset: 0x000037E8
		public static int GetContacts(Collider2D collider1, Collider2D collider2, ContactFilter2D contactFilter, ContactPoint2D[] contacts)
		{
			return Physics2D.GetColliderColliderContactsArray(collider1, collider2, contactFilter, contacts);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005604 File Offset: 0x00003804
		public static int GetContacts(Collider2D collider, ContactPoint2D[] contacts)
		{
			return Physics2D.GetColliderContactsArray(collider, default(ContactFilter2D).NoFilter(), contacts);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000562C File Offset: 0x0000382C
		public static int GetContacts(Collider2D collider, ContactFilter2D contactFilter, ContactPoint2D[] contacts)
		{
			return Physics2D.GetColliderContactsArray(collider, contactFilter, contacts);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005648 File Offset: 0x00003848
		public static int GetContacts(Collider2D collider, Collider2D[] colliders)
		{
			return Physics2D.GetColliderContactsCollidersOnlyArray(collider, default(ContactFilter2D).NoFilter(), colliders);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005670 File Offset: 0x00003870
		public static int GetContacts(Collider2D collider, ContactFilter2D contactFilter, Collider2D[] colliders)
		{
			return Physics2D.GetColliderContactsCollidersOnlyArray(collider, contactFilter, colliders);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000568C File Offset: 0x0000388C
		public static int GetContacts(Rigidbody2D rigidbody, ContactPoint2D[] contacts)
		{
			return Physics2D.GetRigidbodyContactsArray(rigidbody, default(ContactFilter2D).NoFilter(), contacts);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000056B4 File Offset: 0x000038B4
		public static int GetContacts(Rigidbody2D rigidbody, ContactFilter2D contactFilter, ContactPoint2D[] contacts)
		{
			return Physics2D.GetRigidbodyContactsArray(rigidbody, contactFilter, contacts);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000056D0 File Offset: 0x000038D0
		public static int GetContacts(Rigidbody2D rigidbody, Collider2D[] colliders)
		{
			return Physics2D.GetRigidbodyContactsCollidersOnlyArray(rigidbody, default(ContactFilter2D).NoFilter(), colliders);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000056F8 File Offset: 0x000038F8
		public static int GetContacts(Rigidbody2D rigidbody, ContactFilter2D contactFilter, Collider2D[] colliders)
		{
			return Physics2D.GetRigidbodyContactsCollidersOnlyArray(rigidbody, contactFilter, colliders);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005714 File Offset: 0x00003914
		[NativeMethod("GetColliderContactsArray_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private unsafe static int GetColliderContactsArray([NotNull] Collider2D collider, ContactFilter2D contactFilter, [NotNull] ContactPoint2D[] results)
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
			Span<ContactPoint2D> span = new Span<ContactPoint2D>(results);
			int colliderContactsArray_Injected;
			fixed (ContactPoint2D* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				colliderContactsArray_Injected = Physics2D.GetColliderContactsArray_Injected(intPtr, ref contactFilter, ref managedSpanWrapper);
			}
			return colliderContactsArray_Injected;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005784 File Offset: 0x00003984
		[NativeMethod("GetColliderColliderContactsArray_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private unsafe static int GetColliderColliderContactsArray([NotNull] Collider2D collider1, [NotNull] Collider2D collider2, ContactFilter2D contactFilter, [NotNull] ContactPoint2D[] results)
		{
			if (collider1 == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider1, "collider1");
			}
			if (collider2 == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider2, "collider2");
			}
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider1);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider1, "collider1");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider2);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider2, "collider2");
			}
			Span<ContactPoint2D> span = new Span<ContactPoint2D>(results);
			int colliderColliderContactsArray_Injected;
			fixed (ContactPoint2D* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				colliderColliderContactsArray_Injected = Physics2D.GetColliderColliderContactsArray_Injected(intPtr, intPtr2, ref contactFilter, ref managedSpanWrapper);
			}
			return colliderColliderContactsArray_Injected;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005818 File Offset: 0x00003A18
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("GetRigidbodyContactsArray_Binding")]
		private unsafe static int GetRigidbodyContactsArray([NotNull] Rigidbody2D rigidbody, ContactFilter2D contactFilter, [NotNull] ContactPoint2D[] results)
		{
			if (rigidbody == null)
			{
				ThrowHelper.ThrowArgumentNullException(rigidbody, "rigidbody");
			}
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(rigidbody);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rigidbody, "rigidbody");
			}
			Span<ContactPoint2D> span = new Span<ContactPoint2D>(results);
			int rigidbodyContactsArray_Injected;
			fixed (ContactPoint2D* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				rigidbodyContactsArray_Injected = Physics2D.GetRigidbodyContactsArray_Injected(intPtr, ref contactFilter, ref managedSpanWrapper);
			}
			return rigidbodyContactsArray_Injected;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005888 File Offset: 0x00003A88
		[NativeMethod("GetColliderContactsCollidersOnlyArray_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int GetColliderContactsCollidersOnlyArray([NotNull] Collider2D collider, ContactFilter2D contactFilter, [NotNull] [Unmarshalled] Collider2D[] results)
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
			return Physics2D.GetColliderContactsCollidersOnlyArray_Injected(intPtr, ref contactFilter, results);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000058D0 File Offset: 0x00003AD0
		[NativeMethod("GetRigidbodyContactsCollidersOnlyArray_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int GetRigidbodyContactsCollidersOnlyArray([NotNull] Rigidbody2D rigidbody, ContactFilter2D contactFilter, [NotNull] [Unmarshalled] Collider2D[] results)
		{
			if (rigidbody == null)
			{
				ThrowHelper.ThrowArgumentNullException(rigidbody, "rigidbody");
			}
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(rigidbody);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rigidbody, "rigidbody");
			}
			return Physics2D.GetRigidbodyContactsCollidersOnlyArray_Injected(intPtr, ref contactFilter, results);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005918 File Offset: 0x00003B18
		public static int GetContacts(Collider2D collider1, Collider2D collider2, ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
		{
			return Physics2D.GetColliderColliderContactsList(collider1, collider2, contactFilter, contacts);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005934 File Offset: 0x00003B34
		public static int GetContacts(Collider2D collider, List<ContactPoint2D> contacts)
		{
			return Physics2D.GetColliderContactsList(collider, default(ContactFilter2D).NoFilter(), contacts);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000595C File Offset: 0x00003B5C
		public static int GetContacts(Collider2D collider, ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
		{
			return Physics2D.GetColliderContactsList(collider, contactFilter, contacts);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005978 File Offset: 0x00003B78
		public static int GetContacts(Collider2D collider, List<Collider2D> colliders)
		{
			return Physics2D.GetColliderContactsCollidersOnlyList(collider, default(ContactFilter2D).NoFilter(), colliders);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000059A0 File Offset: 0x00003BA0
		public static int GetContacts(Collider2D collider, ContactFilter2D contactFilter, List<Collider2D> colliders)
		{
			return Physics2D.GetColliderContactsCollidersOnlyList(collider, contactFilter, colliders);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000059BC File Offset: 0x00003BBC
		public static int GetContacts(Rigidbody2D rigidbody, List<ContactPoint2D> contacts)
		{
			return Physics2D.GetRigidbodyContactsList(rigidbody, default(ContactFilter2D).NoFilter(), contacts);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000059E4 File Offset: 0x00003BE4
		public static int GetContacts(Rigidbody2D rigidbody, ContactFilter2D contactFilter, List<ContactPoint2D> contacts)
		{
			return Physics2D.GetRigidbodyContactsList(rigidbody, contactFilter, contacts);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005A00 File Offset: 0x00003C00
		public static int GetContacts(Rigidbody2D rigidbody, List<Collider2D> colliders)
		{
			return Physics2D.GetRigidbodyContactsCollidersOnlyList(rigidbody, default(ContactFilter2D).NoFilter(), colliders);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005A28 File Offset: 0x00003C28
		public static int GetContacts(Rigidbody2D rigidbody, ContactFilter2D contactFilter, List<Collider2D> colliders)
		{
			return Physics2D.GetRigidbodyContactsCollidersOnlyList(rigidbody, contactFilter, colliders);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005A44 File Offset: 0x00003C44
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("GetColliderContactsList_Binding")]
		private unsafe static int GetColliderContactsList([NotNull] Collider2D collider, ContactFilter2D contactFilter, [NotNull] List<ContactPoint2D> results)
		{
			if (collider == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			int colliderContactsList_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(collider, "collider");
				}
				fixed (ContactPoint2D[] array = NoAllocHelpers.ExtractArrayFromList<ContactPoint2D>(results))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, results.Count);
					colliderContactsList_Injected = Physics2D.GetColliderContactsList_Injected(intPtr, ref contactFilter, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<ContactPoint2D>(results);
			}
			return colliderContactsList_Injected;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005ADC File Offset: 0x00003CDC
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("GetColliderColliderContactsList_Binding")]
		private unsafe static int GetColliderColliderContactsList([NotNull] Collider2D collider1, [NotNull] Collider2D collider2, ContactFilter2D contactFilter, [NotNull] List<ContactPoint2D> results)
		{
			if (collider1 == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider1, "collider1");
			}
			if (collider2 == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider2, "collider2");
			}
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			int colliderColliderContactsList_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider1);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(collider1, "collider1");
				}
				IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Collider2D>(collider2);
				if (intPtr2 == 0)
				{
					ThrowHelper.ThrowArgumentNullException(collider2, "collider2");
				}
				fixed (ContactPoint2D[] array = NoAllocHelpers.ExtractArrayFromList<ContactPoint2D>(results))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, results.Count);
					colliderColliderContactsList_Injected = Physics2D.GetColliderColliderContactsList_Injected(intPtr, intPtr2, ref contactFilter, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<ContactPoint2D>(results);
			}
			return colliderColliderContactsList_Injected;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005B98 File Offset: 0x00003D98
		[NativeMethod("GetRigidbodyContactsList_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private unsafe static int GetRigidbodyContactsList([NotNull] Rigidbody2D rigidbody, ContactFilter2D contactFilter, [NotNull] List<ContactPoint2D> results)
		{
			if (rigidbody == null)
			{
				ThrowHelper.ThrowArgumentNullException(rigidbody, "rigidbody");
			}
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			int rigidbodyContactsList_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(rigidbody);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(rigidbody, "rigidbody");
				}
				fixed (ContactPoint2D[] array = NoAllocHelpers.ExtractArrayFromList<ContactPoint2D>(results))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, results.Count);
					rigidbodyContactsList_Injected = Physics2D.GetRigidbodyContactsList_Injected(intPtr, ref contactFilter, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<ContactPoint2D>(results);
			}
			return rigidbodyContactsList_Injected;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005C30 File Offset: 0x00003E30
		[NativeMethod("GetColliderContactsCollidersOnlyList_Binding")]
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		private static int GetColliderContactsCollidersOnlyList([NotNull] Collider2D collider, ContactFilter2D contactFilter, [NotNull] List<Collider2D> results)
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
			return Physics2D.GetColliderContactsCollidersOnlyList_Injected(intPtr, ref contactFilter, results);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005C78 File Offset: 0x00003E78
		[StaticAccessor("PhysicsQuery2D", StaticAccessorType.DoubleColon)]
		[NativeMethod("GetRigidbodyContactsCollidersOnlyList_Binding")]
		private static int GetRigidbodyContactsCollidersOnlyList([NotNull] Rigidbody2D rigidbody, ContactFilter2D contactFilter, [NotNull] List<Collider2D> results)
		{
			if (rigidbody == null)
			{
				ThrowHelper.ThrowArgumentNullException(rigidbody, "rigidbody");
			}
			if (results == null)
			{
				ThrowHelper.ThrowArgumentNullException(results, "results");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Rigidbody2D>(rigidbody);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(rigidbody, "rigidbody");
			}
			return Physics2D.GetRigidbodyContactsCollidersOnlyList_Injected(intPtr, ref contactFilter, results);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005CC0 File Offset: 0x00003EC0
		internal static void SetEditorDragMovement(bool dragging, GameObject[] objs)
		{
			foreach (Rigidbody2D rigidbody2D in Physics2D.m_LastDisabledRigidbody2D)
			{
				bool flag = rigidbody2D != null;
				if (flag)
				{
					rigidbody2D.SetDragBehaviour(false);
				}
			}
			Physics2D.m_LastDisabledRigidbody2D.Clear();
			bool flag2 = !dragging;
			if (!flag2)
			{
				foreach (GameObject gameObject in objs)
				{
					Rigidbody2D[] componentsInChildren = gameObject.GetComponentsInChildren<Rigidbody2D>(false);
					foreach (Rigidbody2D rigidbody2D2 in componentsInChildren)
					{
						Physics2D.m_LastDisabledRigidbody2D.Add(rigidbody2D2);
						rigidbody2D2.SetDragBehaviour(true);
					}
				}
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00005D9C File Offset: 0x00003F9C
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00005DAF File Offset: 0x00003FAF
		[Obsolete("Physics2D.raycastsHitTriggers is deprecated. Use Physics2D.queriesHitTriggers instead. (UnityUpgradable) -> queriesHitTriggers", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool raycastsHitTriggers
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00005DB4 File Offset: 0x00003FB4
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00005DAF File Offset: 0x00003FAF
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.raycastsStartInColliders is deprecated. Use Physics2D.queriesStartInColliders instead. (UnityUpgradable) -> queriesStartInColliders", true)]
		public static bool raycastsStartInColliders
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00005DC8 File Offset: 0x00003FC8
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00005DAF File Offset: 0x00003FAF
		[Obsolete("Physics2D.deleteStopsCallbacks is deprecated.(UnityUpgradable) -> changeStopsCallbacks", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool deleteStopsCallbacks
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00005DDC File Offset: 0x00003FDC
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00005DAF File Offset: 0x00003FAF
		[Obsolete("Physics2D.changeStopsCallbacks is deprecated and will always return false.", true)]
		public static bool changeStopsCallbacks
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00005DF0 File Offset: 0x00003FF0
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00005E07 File Offset: 0x00004007
		[Obsolete("Physics2D.minPenetrationForPenalty is deprecated. Use Physics2D.defaultContactOffset instead. (UnityUpgradable) -> defaultContactOffset", true)]
		public static float minPenetrationForPenalty
		{
			get
			{
				return Physics2D.defaultContactOffset;
			}
			set
			{
				Physics2D.defaultContactOffset = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00005E14 File Offset: 0x00004014
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00005E2B File Offset: 0x0000402B
		[ExcludeFromDocs]
		[Obsolete("Physics2D.velocityThreshold is deprecated. Use Physics2D.bounceThreshold instead. (UnityUpgradable) -> bounceThreshold", true)]
		public static float velocityThreshold
		{
			get
			{
				return Physics2D.bounceThreshold;
			}
			set
			{
				Physics2D.bounceThreshold = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00005E38 File Offset: 0x00004038
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00005E55 File Offset: 0x00004055
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.autoSimulation is deprecated. Use Physics2D.simulationMode instead.", true)]
		public static bool autoSimulation
		{
			get
			{
				return Physics2D.simulationMode != SimulationMode2D.Script;
			}
			set
			{
				Physics2D.simulationMode = (value ? SimulationMode2D.FixedUpdate : SimulationMode2D.Script);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00005E68 File Offset: 0x00004068
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x00005DAF File Offset: 0x00003FAF
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		[Obsolete("Physics2D.colliderAwakeColor is deprecated. This options has been moved to 2D Preferences.", true)]
		public static Color colliderAwakeColor
		{
			get
			{
				return Color.magenta;
			}
			set
			{
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00005E80 File Offset: 0x00004080
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00005DAF File Offset: 0x00003FAF
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.colliderAsleepColor is deprecated. This options has been moved to 2D Preferences.", true)]
		public static Color colliderAsleepColor
		{
			get
			{
				return Color.magenta;
			}
			set
			{
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00005E98 File Offset: 0x00004098
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00005DAF File Offset: 0x00003FAF
		[Obsolete("Physics2D.colliderContactColor is deprecated. This options has been moved to 2D Preferences.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		public static Color colliderContactColor
		{
			get
			{
				return Color.magenta;
			}
			set
			{
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00005EB0 File Offset: 0x000040B0
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00005DAF File Offset: 0x00003FAF
		[ExcludeFromDocs]
		[Obsolete("Physics2D.colliderAABBColor is deprecated. All Physics 2D colors moved to Preferences. This is now known as 'Collider Bounds Color'.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static Color colliderAABBColor
		{
			get
			{
				return Color.magenta;
			}
			set
			{
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00005EC8 File Offset: 0x000040C8
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00005DAF File Offset: 0x00003FAF
		[ExcludeFromDocs]
		[Obsolete("Physics2D.contactArrowScale is deprecated. This options has been moved to 2D Preferences.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static float contactArrowScale
		{
			get
			{
				return 0.2f;
			}
			set
			{
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00005EDF File Offset: 0x000040DF
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00005EE6 File Offset: 0x000040E6
		[Obsolete("Physics2D.alwaysShowColliders is deprecated. It is no longer available in the Editor or Builds.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		public static bool alwaysShowColliders { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00005EEE File Offset: 0x000040EE
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00005EF5 File Offset: 0x000040F5
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.showCollidersFilled is deprecated. It is no longer available in the Editor or Builds.", true)]
		public static bool showCollidersFilled { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00005EFD File Offset: 0x000040FD
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00005F04 File Offset: 0x00004104
		[ExcludeFromDocs]
		[Obsolete("Physics2D.showColliderSleep is deprecated. It is no longer available in the Editor or Builds.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool showColliderSleep { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00005F0C File Offset: 0x0000410C
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00005F13 File Offset: 0x00004113
		[Obsolete("Physics2D.showColliderContacts is deprecated. It is no longer available in the Editor or Builds.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		public static bool showColliderContacts { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00005F1C File Offset: 0x0000411C
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00005DAF File Offset: 0x00003FAF
		[Obsolete("Physics2D.showColliderAABB is deprecated. It is no longer available in the Editor or Builds.", true)]
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool showColliderAABB
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00005F30 File Offset: 0x00004130
		[ExcludeFromDocs]
		[Obsolete("Physics2D.LinecastNonAlloc is deprecated. Use Physics2D.Linecast instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.Linecast(start, end, results, -5);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00005F54 File Offset: 0x00004154
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.LinecastNonAlloc is deprecated. Use Physics2D.Linecast instead.", false)]
		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.Linecast(start, end, contactFilter2D, results);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00005F88 File Offset: 0x00004188
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.LinecastNonAlloc is deprecated. Use Physics2D.Linecast instead.", false)]
		[ExcludeFromDocs]
		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.Linecast(start, end, contactFilter2D, results);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00005FBC File Offset: 0x000041BC
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.LinecastNonAlloc is deprecated. Use Physics2D.Linecast instead.", false)]
		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.Linecast(start, end, contactFilter2D, results);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00005FEC File Offset: 0x000041EC
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.RaycastNonAlloc is deprecated. Use Physics2D.Raycast instead.", false)]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, float.PositiveInfinity, results, -5);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00006018 File Offset: 0x00004218
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.RaycastNonAlloc is deprecated. Use Physics2D.Raycast instead.", false)]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance)
		{
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, results, -5);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00006040 File Offset: 0x00004240
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.RaycastNonAlloc is deprecated. Use Physics2D.Raycast instead.", false)]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter2D, results);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00006074 File Offset: 0x00004274
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.RaycastNonAlloc is deprecated. Use Physics2D.Raycast instead.", false)]
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.Raycast(origin, direction, distance, contactFilter2D, results);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000060A4 File Offset: 0x000042A4
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.CircleCastNonAlloc is deprecated. Use Physics2D.CircleCast instead.", false)]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, float.PositiveInfinity, results, -5);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000060D0 File Offset: 0x000042D0
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.CircleCastNonAlloc is deprecated. Use Physics2D.CircleCast instead.", false)]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance)
		{
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, results, -5);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000060F8 File Offset: 0x000042F8
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.CircleCastNonAlloc is deprecated. Use Physics2D.CircleCast instead.", false)]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter2D, results);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006130 File Offset: 0x00004330
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.CircleCastNonAlloc is deprecated. Use Physics2D.CircleCast instead.", false)]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter2D, results);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00006168 File Offset: 0x00004368
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.CircleCastNonAlloc is deprecated. Use Physics2D.CircleCast instead.", false)]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.CircleCast(origin, radius, direction, distance, contactFilter2D, results);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000619C File Offset: 0x0000439C
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.BoxCastNonAlloc is deprecated. Use Physics2D.BoxCast instead.", false)]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, float.PositiveInfinity, results, -5);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000061C8 File Offset: 0x000043C8
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.BoxCastNonAlloc is deprecated. Use Physics2D.BoxCast instead.", false)]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance)
		{
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, results, -5);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000061F4 File Offset: 0x000043F4
		[Obsolete("Physics2D.BoxCastNonAlloc is deprecated. Use Physics2D.BoxCast instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter2D, results);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006230 File Offset: 0x00004430
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.BoxCastNonAlloc is deprecated. Use Physics2D.BoxCast instead.", false)]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter2D, results);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00006268 File Offset: 0x00004468
		[ExcludeFromDocs]
		[Obsolete("Physics2D.BoxCastNonAlloc is deprecated. Use Physics2D.BoxCast instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.BoxCast(origin, size, angle, direction, distance, contactFilter2D, results);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000629C File Offset: 0x0000449C
		[Obsolete("Physics2D.CapsuleCastNonAlloc is deprecated. Use Physics2D.CapsuleCast instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, float.PositiveInfinity, results, -5);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000062CC File Offset: 0x000044CC
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.CapsuleCastNonAlloc is deprecated. Use Physics2D.CapsuleCast instead.", false)]
		public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, float distance)
		{
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, results, -5);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000062F8 File Offset: 0x000044F8
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.CapsuleCastNonAlloc is deprecated. Use Physics2D.CapsuleCast instead.", false)]
		public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter2D, results);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00006334 File Offset: 0x00004534
		[Obsolete("Physics2D.CapsuleCastNonAlloc is deprecated. Use Physics2D.CapsuleCast instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter2D, results);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00006370 File Offset: 0x00004570
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.CapsuleCastNonAlloc is deprecated. Use Physics2D.CapsuleCast instead.", false)]
		public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.CapsuleCast(origin, size, capsuleDirection, angle, direction, distance, contactFilter2D, results);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000063A8 File Offset: 0x000045A8
		[Obsolete("Physics2D.GetRayIntersectionNonAlloc is deprecated. Use Physics2D.GetRayIntersection instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results)
		{
			return Physics2D.defaultPhysicsScene.GetRayIntersection(ray, float.PositiveInfinity, results, -5);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000063D0 File Offset: 0x000045D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		[Obsolete("Physics2D.GetRayIntersectionNonAlloc is deprecated. Use Physics2D.GetRayIntersection instead.", false)]
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results, float distance)
		{
			return Physics2D.defaultPhysicsScene.GetRayIntersection(ray, distance, results, -5);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000063F4 File Offset: 0x000045F4
		[Obsolete("Physics2D.OverlapPointNonAlloc is deprecated. Use Physics2D.OverlapPoint instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, results, -5);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00006418 File Offset: 0x00004618
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.OverlapPointNonAlloc is deprecated. Use Physics2D.OverlapPoint instead.", false)]
		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, contactFilter2D, results);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000644C File Offset: 0x0000464C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.OverlapPointNonAlloc is deprecated. Use Physics2D.OverlapPoint instead.", false)]
		[ExcludeFromDocs]
		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, contactFilter2D, results);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000647C File Offset: 0x0000467C
		[Obsolete("Physics2D.OverlapPointNonAlloc is deprecated. Use Physics2D.OverlapPoint instead.", false)]
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapPoint(point, contactFilter2D, results);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000064A8 File Offset: 0x000046A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		[Obsolete("Physics2D.OverlapCircleNonAlloc is deprecated. Use Physics2D.OverlapCircle instead.", false)]
		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, results, -5);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000064CC File Offset: 0x000046CC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		[Obsolete("Physics2D.OverlapCircleNonAlloc is deprecated. Use Physics2D.OverlapCircle instead.", false)]
		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, contactFilter2D, results);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006500 File Offset: 0x00004700
		[ExcludeFromDocs]
		[Obsolete("Physics2D.OverlapCircleNonAlloc is deprecated. Use Physics2D.OverlapCircle instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, contactFilter2D, results);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00006534 File Offset: 0x00004734
		[Obsolete("Physics2D.OverlapCircleNonAlloc is deprecated. Use Physics2D.OverlapCircle instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapCircle(point, radius, contactFilter2D, results);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00006564 File Offset: 0x00004764
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		[Obsolete("Physics2D.OverlapBoxNonAlloc is deprecated. Use Physics2D.OverlapBox instead.", false)]
		public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, results, -5);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000658C File Offset: 0x0000478C
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.OverlapBoxNonAlloc is deprecated. Use Physics2D.OverlapBox instead.", false)]
		public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter2D, results);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000065C4 File Offset: 0x000047C4
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.OverlapBoxNonAlloc is deprecated. Use Physics2D.OverlapBox instead.", false)]
		public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter2D, results);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000065F8 File Offset: 0x000047F8
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.OverlapBoxNonAlloc is deprecated. Use Physics2D.OverlapBox instead.", false)]
		public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapBox(point, size, angle, contactFilter2D, results);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00006628 File Offset: 0x00004828
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.OverlapAreaNonAlloc is deprecated. Use Physics2D.OverlapArea instead.", false)]
		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, results, -5);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000664C File Offset: 0x0000484C
		[Obsolete("Physics2D.OverlapAreaNonAlloc is deprecated. Use Physics2D.OverlapArea instead.", false)]
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter2D, results);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00006680 File Offset: 0x00004880
		[Obsolete("Physics2D.OverlapAreaNonAlloc is deprecated. Use Physics2D.OverlapArea instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter2D, results);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000066B4 File Offset: 0x000048B4
		[Obsolete("Physics2D.OverlapAreaNonAlloc is deprecated. Use Physics2D.OverlapArea instead.", false)]
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapArea(pointA, pointB, contactFilter2D, results);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000066E4 File Offset: 0x000048E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		[Obsolete("Physics2D.OverlapCapsuleNonAlloc is deprecated. Use Physics2D.OverlapCapsule instead.", false)]
		public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results)
		{
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, results, -5);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000670C File Offset: 0x0000490C
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.OverlapCapsuleNonAlloc is deprecated. Use Physics2D.OverlapCapsule instead.", false)]
		public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, int layerMask)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter2D, results);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006744 File Offset: 0x00004944
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ExcludeFromDocs]
		[Obsolete("Physics2D.OverlapCapsuleNonAlloc is deprecated. Use Physics2D.OverlapCapsule instead.", false)]
		public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, int layerMask, float minDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter2D, results);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000677C File Offset: 0x0000497C
		[ExcludeFromDocs]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics2D.OverlapCapsuleNonAlloc is deprecated. Use Physics2D.OverlapCapsule instead.", false)]
		public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			ContactFilter2D contactFilter2D = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
			return Physics2D.defaultPhysicsScene.OverlapCapsule(point, size, direction, angle, contactFilter2D, results);
		}

		// Token: 0x060001E7 RID: 487
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_gravity_Injected(out Vector2 ret);

		// Token: 0x060001E8 RID: 488
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_gravity_Injected([In] ref Vector2 value);

		// Token: 0x060001E9 RID: 489
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_simulationLayers_Injected(out LayerMask ret);

		// Token: 0x060001EA RID: 490
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_simulationLayers_Injected([In] ref LayerMask value);

		// Token: 0x060001EB RID: 491
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_jobOptions_Injected(out PhysicsJobOptions2D ret);

		// Token: 0x060001EC RID: 492
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_jobOptions_Injected([In] ref PhysicsJobOptions2D value);

		// Token: 0x060001ED RID: 493
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Simulate_Internal_Injected([In] ref PhysicsScene2D physicsScene, float deltaTime, int simulationLayers);

		// Token: 0x060001EE RID: 494
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IgnoreCollision_Injected(IntPtr collider1, IntPtr collider2, [DefaultValue("true")] bool ignore);

		// Token: 0x060001EF RID: 495
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIgnoreCollision_Injected(IntPtr collider1, IntPtr collider2);

		// Token: 0x060001F0 RID: 496
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsTouching_Injected(IntPtr collider1, IntPtr collider2);

		// Token: 0x060001F1 RID: 497
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsTouching_TwoCollidersWithFilter_Injected(IntPtr collider1, IntPtr collider2, [In] ref ContactFilter2D contactFilter);

		// Token: 0x060001F2 RID: 498
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsTouching_SingleColliderWithFilter_Injected(IntPtr collider, [In] ref ContactFilter2D contactFilter);

		// Token: 0x060001F3 RID: 499
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsTouchingLayers_Injected(IntPtr collider, [DefaultValue("Physics2D.AllLayers")] int layerMask);

		// Token: 0x060001F4 RID: 500
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Distance_Internal_Injected(IntPtr colliderA, IntPtr colliderB, out ColliderDistance2D ret);

		// Token: 0x060001F5 RID: 501
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DistanceFrom_Internal_Injected(IntPtr colliderA, [In] ref Vector2 positionA, float angleA, IntPtr colliderB, [In] ref Vector2 positionB, float angleB, out ColliderDistance2D ret);

		// Token: 0x060001F6 RID: 502
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClosestPoint_Collider_Injected([In] ref Vector2 position, IntPtr collider, out Vector2 ret);

		// Token: 0x060001F7 RID: 503
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClosestPoint_Rigidbody_Injected([In] ref Vector2 position, IntPtr rigidbody, out Vector2 ret);

		// Token: 0x060001F8 RID: 504
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LinecastAll_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 start, [In] ref Vector2 end, [In] ref ContactFilter2D contactFilter, out BlittableArrayWrapper ret);

		// Token: 0x060001F9 RID: 505
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RaycastAll_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 origin, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, out BlittableArrayWrapper ret);

		// Token: 0x060001FA RID: 506
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CircleCastAll_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 origin, float radius, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, out BlittableArrayWrapper ret);

		// Token: 0x060001FB RID: 507
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BoxCastAll_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 origin, [In] ref Vector2 size, float angle, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, out BlittableArrayWrapper ret);

		// Token: 0x060001FC RID: 508
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CapsuleCastAll_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 origin, [In] ref Vector2 size, CapsuleDirection2D capsuleDirection, float angle, [In] ref Vector2 direction, float distance, [In] ref ContactFilter2D contactFilter, out BlittableArrayWrapper ret);

		// Token: 0x060001FD RID: 509
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRayIntersectionAll_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector3 origin, [In] ref Vector3 direction, float distance, int layerMask, out BlittableArrayWrapper ret);

		// Token: 0x060001FE RID: 510
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D[] OverlapPointAll_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 point, [In] ref ContactFilter2D contactFilter);

		// Token: 0x060001FF RID: 511
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D[] OverlapCircleAll_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 point, float radius, [In] ref ContactFilter2D contactFilter);

		// Token: 0x06000200 RID: 512
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D[] OverlapBoxAll_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 point, [In] ref Vector2 size, float angle, [In] ref ContactFilter2D contactFilter);

		// Token: 0x06000201 RID: 513
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D[] OverlapCapsuleAll_Internal_Injected([In] ref PhysicsScene2D physicsScene, [In] ref Vector2 point, [In] ref Vector2 size, CapsuleDirection2D direction, float angle, [In] ref ContactFilter2D contactFilter);

		// Token: 0x06000202 RID: 514
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetColliderContactsArray_Injected(IntPtr collider, [In] ref ContactFilter2D contactFilter, ref ManagedSpanWrapper results);

		// Token: 0x06000203 RID: 515
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetColliderColliderContactsArray_Injected(IntPtr collider1, IntPtr collider2, [In] ref ContactFilter2D contactFilter, ref ManagedSpanWrapper results);

		// Token: 0x06000204 RID: 516
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRigidbodyContactsArray_Injected(IntPtr rigidbody, [In] ref ContactFilter2D contactFilter, ref ManagedSpanWrapper results);

		// Token: 0x06000205 RID: 517
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetColliderContactsCollidersOnlyArray_Injected(IntPtr collider, [In] ref ContactFilter2D contactFilter, Collider2D[] results);

		// Token: 0x06000206 RID: 518
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRigidbodyContactsCollidersOnlyArray_Injected(IntPtr rigidbody, [In] ref ContactFilter2D contactFilter, Collider2D[] results);

		// Token: 0x06000207 RID: 519
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetColliderContactsList_Injected(IntPtr collider, [In] ref ContactFilter2D contactFilter, ref BlittableListWrapper results);

		// Token: 0x06000208 RID: 520
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetColliderColliderContactsList_Injected(IntPtr collider1, IntPtr collider2, [In] ref ContactFilter2D contactFilter, ref BlittableListWrapper results);

		// Token: 0x06000209 RID: 521
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRigidbodyContactsList_Injected(IntPtr rigidbody, [In] ref ContactFilter2D contactFilter, ref BlittableListWrapper results);

		// Token: 0x0600020A RID: 522
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetColliderContactsCollidersOnlyList_Injected(IntPtr collider, [In] ref ContactFilter2D contactFilter, List<Collider2D> results);

		// Token: 0x0600020B RID: 523
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRigidbodyContactsCollidersOnlyList_Injected(IntPtr rigidbody, [In] ref ContactFilter2D contactFilter, List<Collider2D> results);

		// Token: 0x04000002 RID: 2
		public const int IgnoreRaycastLayer = 4;

		// Token: 0x04000003 RID: 3
		public const int DefaultRaycastLayers = -5;

		// Token: 0x04000004 RID: 4
		public const int AllLayers = -1;

		// Token: 0x04000005 RID: 5
		public const int MaxPolygonShapeVertices = 8;

		// Token: 0x04000006 RID: 6
		private static List<Rigidbody2D> m_LastDisabledRigidbody2D = new List<Rigidbody2D>();

		// Token: 0x02000005 RID: 5
		[Flags]
		internal enum GizmoOptions
		{
			// Token: 0x0400000C RID: 12
			AllColliders = 1,
			// Token: 0x0400000D RID: 13
			CollidersOutlined = 2,
			// Token: 0x0400000E RID: 14
			CollidersFilled = 4,
			// Token: 0x0400000F RID: 15
			CollidersSleeping = 8,
			// Token: 0x04000010 RID: 16
			ColliderContacts = 16,
			// Token: 0x04000011 RID: 17
			ColliderBounds = 32
		}
	}
}
