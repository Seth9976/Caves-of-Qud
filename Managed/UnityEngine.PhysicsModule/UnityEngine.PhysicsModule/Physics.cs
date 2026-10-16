using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000023 RID: 35
	[NativeHeader("Modules/Physics/PhysicsQuery.h")]
	[NativeHeader("Modules/Physics/PhysicsManager.h")]
	[StaticAccessor("GetPhysicsManager()", StaticAccessorType.Dot)]
	public class Physics
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060002BA RID: 698 RVA: 0x00005E10 File Offset: 0x00004010
		// (remove) Token: 0x060002BB RID: 699 RVA: 0x00005E44 File Offset: 0x00004044
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<PhysicsScene, NativeArray<ModifiableContactPair>> ContactModifyEvent;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060002BC RID: 700 RVA: 0x00005E78 File Offset: 0x00004078
		// (remove) Token: 0x060002BD RID: 701 RVA: 0x00005EAC File Offset: 0x000040AC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<PhysicsScene, NativeArray<ModifiableContactPair>> ContactModifyEventCCD;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060002BE RID: 702 RVA: 0x00005EE0 File Offset: 0x000040E0
		// (remove) Token: 0x060002BF RID: 703 RVA: 0x00005F14 File Offset: 0x00004114
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal static event Action<PhysicsScene, IntPtr, int, bool> GenericContactModifyEvent;

		// Token: 0x060002C0 RID: 704 RVA: 0x00005F47 File Offset: 0x00004147
		[RequiredByNativeCode]
		private static void OnSceneContactModify(PhysicsScene scene, IntPtr buffer, int count, bool isCCD)
		{
			Action<PhysicsScene, IntPtr, int, bool> genericContactModifyEvent = Physics.GenericContactModifyEvent;
			if (genericContactModifyEvent != null)
			{
				genericContactModifyEvent(scene, buffer, count, isCCD);
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00005F60 File Offset: 0x00004160
		private static void PhysXOnSceneContactModify(PhysicsScene scene, IntPtr buffer, int count, bool isCCD)
		{
			NativeArray<ModifiableContactPair> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<ModifiableContactPair>(buffer.ToPointer(), count, Allocator.None);
			bool flag = !isCCD;
			if (flag)
			{
				Action<PhysicsScene, NativeArray<ModifiableContactPair>> contactModifyEvent = Physics.ContactModifyEvent;
				if (contactModifyEvent != null)
				{
					contactModifyEvent(scene, nativeArray);
				}
			}
			else
			{
				Action<PhysicsScene, NativeArray<ModifiableContactPair>> contactModifyEventCCD = Physics.ContactModifyEventCCD;
				if (contactModifyEventCCD != null)
				{
					contactModifyEventCCD(scene, nativeArray);
				}
			}
		}

		// Token: 0x060002C2 RID: 706
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetIntegrationInfos(out IntPtr integrations, out ulong integrationCount);

		// Token: 0x060002C3 RID: 707
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCurrentIntegrationInfo(out IntPtr integration);

		// Token: 0x060002C4 RID: 708 RVA: 0x00005FB0 File Offset: 0x000041B0
		internal static ReadOnlySpan<IntegrationInfo> GetIntegrationInfos()
		{
			IntPtr intPtr;
			ulong num;
			Physics.GetIntegrationInfos(out intPtr, out num);
			return new ReadOnlySpan<IntegrationInfo>(intPtr.ToPointer(), (int)num);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00005FDC File Offset: 0x000041DC
		public unsafe static IntegrationInfo GetCurrentIntegrationInfo()
		{
			IntPtr intPtr;
			Physics.GetCurrentIntegrationInfo(out intPtr);
			return *(IntegrationInfo*)intPtr.ToPointer();
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00006004 File Offset: 0x00004204
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0000601C File Offset: 0x0000421C
		public static Vector3 gravity
		{
			[ThreadSafe]
			get
			{
				Vector3 vector;
				Physics.get_gravity_Injected(out vector);
				return vector;
			}
			set
			{
				Physics.set_gravity_Injected(ref value);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002C8 RID: 712
		// (set) Token: 0x060002C9 RID: 713
		public static extern float defaultContactOffset
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002CA RID: 714
		// (set) Token: 0x060002CB RID: 715
		public static extern float sleepThreshold
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002CC RID: 716
		// (set) Token: 0x060002CD RID: 717
		public static extern bool queriesHitTriggers
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002CE RID: 718
		// (set) Token: 0x060002CF RID: 719
		public static extern bool queriesHitBackfaces
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002D0 RID: 720
		// (set) Token: 0x060002D1 RID: 721
		public static extern float bounceThreshold
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002D2 RID: 722
		// (set) Token: 0x060002D3 RID: 723
		public static extern float defaultMaxDepenetrationVelocity
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002D4 RID: 724
		// (set) Token: 0x060002D5 RID: 725
		public static extern int defaultSolverIterations
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002D6 RID: 726
		// (set) Token: 0x060002D7 RID: 727
		public static extern int defaultSolverVelocityIterations
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002D8 RID: 728
		// (set) Token: 0x060002D9 RID: 729
		public static extern SimulationMode simulationMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002DA RID: 730
		// (set) Token: 0x060002DB RID: 731
		public static extern float defaultMaxAngularSpeed
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002DC RID: 732
		// (set) Token: 0x060002DD RID: 733
		public static extern bool improvedPatchFriction
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002DE RID: 734
		// (set) Token: 0x060002DF RID: 735
		public static extern bool invokeCollisionCallbacks
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00006030 File Offset: 0x00004230
		public static PhysicsScene defaultPhysicsScene
		{
			get
			{
				return PhysicsScene.GetDefaultScene();
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00006038 File Offset: 0x00004238
		public static void IgnoreCollision([NotNull] Collider collider1, [NotNull] Collider collider2, [DefaultValue("true")] bool ignore)
		{
			if (collider1 == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider1, "collider1");
			}
			if (collider2 == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider2, "collider2");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(collider1);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider1, "collider1");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Collider>(collider2);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider2, "collider2");
			}
			Physics.IgnoreCollision_Injected(intPtr, intPtr2, ignore);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00006093 File Offset: 0x00004293
		[ExcludeFromDocs]
		public static void IgnoreCollision(Collider collider1, Collider collider2)
		{
			Physics.IgnoreCollision(collider1, collider2, true);
		}

		// Token: 0x060002E3 RID: 739
		[NativeName("IgnoreCollision")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void IgnoreLayerCollision(int layer1, int layer2, [DefaultValue("true")] bool ignore);

		// Token: 0x060002E4 RID: 740 RVA: 0x0000609F File Offset: 0x0000429F
		[ExcludeFromDocs]
		public static void IgnoreLayerCollision(int layer1, int layer2)
		{
			Physics.IgnoreLayerCollision(layer1, layer2, true);
		}

		// Token: 0x060002E5 RID: 741
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetIgnoreLayerCollision(int layer1, int layer2);

		// Token: 0x060002E6 RID: 742 RVA: 0x000060AC File Offset: 0x000042AC
		public static bool GetIgnoreCollision([NotNull] Collider collider1, [NotNull] Collider collider2)
		{
			if (collider1 == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider1, "collider1");
			}
			if (collider2 == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider2, "collider2");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(collider1);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider1, "collider1");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Collider>(collider2);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider2, "collider2");
			}
			return Physics.GetIgnoreCollision_Injected(intPtr, intPtr2);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00006108 File Offset: 0x00004308
		public static bool Raycast(Vector3 origin, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00006130 File Offset: 0x00004330
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance, int layerMask)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00006154 File Offset: 0x00004354
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, float maxDistance)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000617C File Offset: 0x0000437C
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000061A8 File Offset: 0x000043A8
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000061D0 File Offset: 0x000043D0
		[RequiredByNativeCode]
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000061F8 File Offset: 0x000043F8
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, out hitInfo, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00006220 File Offset: 0x00004420
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000624C File Offset: 0x0000444C
		public static bool Raycast(Ray ray, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000627C File Offset: 0x0000447C
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, float maxDistance, int layerMask)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000062AC File Offset: 0x000044AC
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, float maxDistance)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000062E0 File Offset: 0x000044E0
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00006318 File Offset: 0x00004518
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000634C File Offset: 0x0000454C
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return Physics.Raycast(ray.origin, ray.direction, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00006378 File Offset: 0x00004578
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, out hitInfo, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000063AC File Offset: 0x000045AC
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000063E4 File Offset: 0x000045E4
		public static bool Linecast(Vector3 start, Vector3 end, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			Vector3 vector = end - start;
			return Physics.defaultPhysicsScene.Raycast(start, vector, vector.magnitude, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00006418 File Offset: 0x00004618
		[ExcludeFromDocs]
		public static bool Linecast(Vector3 start, Vector3 end, int layerMask)
		{
			return Physics.Linecast(start, end, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00006434 File Offset: 0x00004634
		[ExcludeFromDocs]
		public static bool Linecast(Vector3 start, Vector3 end)
		{
			return Physics.Linecast(start, end, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00006450 File Offset: 0x00004650
		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			Vector3 vector = end - start;
			return Physics.defaultPhysicsScene.Raycast(start, vector, out hitInfo, vector.magnitude, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00006484 File Offset: 0x00004684
		[ExcludeFromDocs]
		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, int layerMask)
		{
			return Physics.Linecast(start, end, out hitInfo, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000064A0 File Offset: 0x000046A0
		[ExcludeFromDocs]
		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo)
		{
			return Physics.Linecast(start, end, out hitInfo, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000064C0 File Offset: 0x000046C0
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			RaycastHit raycastHit;
			return Physics.defaultPhysicsScene.CapsuleCast(point1, point2, radius, direction, out raycastHit, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000064EC File Offset: 0x000046EC
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, int layerMask)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000650C File Offset: 0x0000470C
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000652C File Offset: 0x0000472C
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00006550 File Offset: 0x00004750
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000657C File Offset: 0x0000477C
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000065A0 File Offset: 0x000047A0
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, out hitInfo, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000065C4 File Offset: 0x000047C4
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo)
		{
			return Physics.CapsuleCast(point1, point2, radius, direction, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000065EC File Offset: 0x000047EC
		public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.SphereCast(origin, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00006618 File Offset: 0x00004818
		[ExcludeFromDocs]
		public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return Physics.SphereCast(origin, radius, direction, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00006638 File Offset: 0x00004838
		[ExcludeFromDocs]
		public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance)
		{
			return Physics.SphereCast(origin, radius, direction, out hitInfo, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00006658 File Offset: 0x00004858
		[ExcludeFromDocs]
		public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo)
		{
			return Physics.SphereCast(origin, radius, direction, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000667C File Offset: 0x0000487C
		public static bool SphereCast(Ray ray, float radius, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			RaycastHit raycastHit;
			return Physics.SphereCast(ray.origin, radius, ray.direction, out raycastHit, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000066A8 File Offset: 0x000048A8
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius, float maxDistance, int layerMask)
		{
			return Physics.SphereCast(ray, radius, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000066C4 File Offset: 0x000048C4
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius, float maxDistance)
		{
			return Physics.SphereCast(ray, radius, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000066E4 File Offset: 0x000048E4
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius)
		{
			return Physics.SphereCast(ray, radius, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00006708 File Offset: 0x00004908
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.SphereCast(ray.origin, radius, ray.direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00006734 File Offset: 0x00004934
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return Physics.SphereCast(ray, radius, out hitInfo, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00006754 File Offset: 0x00004954
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, float maxDistance)
		{
			return Physics.SphereCast(ray, radius, out hitInfo, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00006774 File Offset: 0x00004974
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo)
		{
			return Physics.SphereCast(ray, radius, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00006798 File Offset: 0x00004998
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			RaycastHit raycastHit;
			return Physics.defaultPhysicsScene.BoxCast(center, halfExtents, direction, out raycastHit, orientation, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000067C4 File Offset: 0x000049C4
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, int layerMask)
		{
			return Physics.BoxCast(center, halfExtents, direction, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000067E4 File Offset: 0x000049E4
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance)
		{
			return Physics.BoxCast(center, halfExtents, direction, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00006804 File Offset: 0x00004A04
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation)
		{
			return Physics.BoxCast(center, halfExtents, direction, orientation, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00006828 File Offset: 0x00004A28
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction)
		{
			return Physics.BoxCast(center, halfExtents, direction, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00006850 File Offset: 0x00004A50
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000687C File Offset: 0x00004A7C
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation, float maxDistance, int layerMask)
		{
			return Physics.BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000068A0 File Offset: 0x00004AA0
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation, float maxDistance)
		{
			return Physics.BoxCast(center, halfExtents, direction, out hitInfo, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000068C4 File Offset: 0x00004AC4
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, Quaternion orientation)
		{
			return Physics.BoxCast(center, halfExtents, direction, out hitInfo, orientation, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000068EC File Offset: 0x00004AEC
		[ExcludeFromDocs]
		public static bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo)
		{
			return Physics.BoxCast(center, halfExtents, direction, out hitInfo, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00006914 File Offset: 0x00004B14
		[FreeFunction("Physics::RaycastAll")]
		private static RaycastHit[] Internal_RaycastAll(PhysicsScene physicsScene, Ray ray, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			RaycastHit[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Physics.Internal_RaycastAll_Injected(ref physicsScene, ref ray, maxDistance, mask, queryTriggerInteraction, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				RaycastHit[] array;
				blittableArrayWrapper.Unmarshal<RaycastHit>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00006950 File Offset: 0x00004B50
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			RaycastHit[] array;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				Ray ray = new Ray(origin, vector);
				array = Physics.Internal_RaycastAll(Physics.defaultPhysicsScene, ray, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				array = new RaycastHit[0];
			}
			return array;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000069A4 File Offset: 0x00004BA4
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float maxDistance, int layerMask)
		{
			return Physics.RaycastAll(origin, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000069C0 File Offset: 0x00004BC0
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float maxDistance)
		{
			return Physics.RaycastAll(origin, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000069E0 File Offset: 0x00004BE0
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction)
		{
			return Physics.RaycastAll(origin, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00006A04 File Offset: 0x00004C04
		public static RaycastHit[] RaycastAll(Ray ray, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.RaycastAll(ray.origin, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00006A2C File Offset: 0x00004C2C
		[ExcludeFromDocs]
		[RequiredByNativeCode]
		public static RaycastHit[] RaycastAll(Ray ray, float maxDistance, int layerMask)
		{
			return Physics.RaycastAll(ray.origin, ray.direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00006A54 File Offset: 0x00004C54
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Ray ray, float maxDistance)
		{
			return Physics.RaycastAll(ray.origin, ray.direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00006A80 File Offset: 0x00004C80
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Ray ray)
		{
			return Physics.RaycastAll(ray.origin, ray.direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00006AB0 File Offset: 0x00004CB0
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00006AE4 File Offset: 0x00004CE4
		[ExcludeFromDocs]
		[RequiredByNativeCode]
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance, int layerMask)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00006B18 File Offset: 0x00004D18
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results, float maxDistance)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00006B4C File Offset: 0x00004D4C
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Ray ray, RaycastHit[] results)
		{
			return Physics.defaultPhysicsScene.Raycast(ray.origin, ray.direction, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00006B84 File Offset: 0x00004D84
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00006BAC File Offset: 0x00004DAC
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00006BD4 File Offset: 0x00004DD4
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results, float maxDistance)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00006BFC File Offset: 0x00004DFC
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector3 origin, Vector3 direction, RaycastHit[] results)
		{
			return Physics.defaultPhysicsScene.Raycast(origin, direction, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00006C28 File Offset: 0x00004E28
		[FreeFunction("Physics::CapsuleCastAll")]
		private static RaycastHit[] Query_CapsuleCastAll(PhysicsScene physicsScene, Vector3 p0, Vector3 p1, float radius, Vector3 direction, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			RaycastHit[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Physics.Query_CapsuleCastAll_Injected(ref physicsScene, ref p0, ref p1, radius, ref direction, maxDistance, mask, queryTriggerInteraction, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				RaycastHit[] array;
				blittableArrayWrapper.Unmarshal<RaycastHit>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00006C6C File Offset: 0x00004E6C
		public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			RaycastHit[] array;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				array = Physics.Query_CapsuleCastAll(Physics.defaultPhysicsScene, point1, point2, radius, vector, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				array = new RaycastHit[0];
			}
			return array;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00006CBC File Offset: 0x00004EBC
		[ExcludeFromDocs]
		public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, int layerMask)
		{
			return Physics.CapsuleCastAll(point1, point2, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00006CDC File Offset: 0x00004EDC
		[ExcludeFromDocs]
		public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance)
		{
			return Physics.CapsuleCastAll(point1, point2, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00006CFC File Offset: 0x00004EFC
		[ExcludeFromDocs]
		public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction)
		{
			return Physics.CapsuleCastAll(point1, point2, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00006D20 File Offset: 0x00004F20
		[FreeFunction("Physics::SphereCastAll")]
		private static RaycastHit[] Query_SphereCastAll(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			RaycastHit[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Physics.Query_SphereCastAll_Injected(ref physicsScene, ref origin, radius, ref direction, maxDistance, mask, queryTriggerInteraction, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				RaycastHit[] array;
				blittableArrayWrapper.Unmarshal<RaycastHit>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00006D60 File Offset: 0x00004F60
		public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			RaycastHit[] array;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				array = Physics.Query_SphereCastAll(Physics.defaultPhysicsScene, origin, radius, vector, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				array = new RaycastHit[0];
			}
			return array;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00006DAC File Offset: 0x00004FAC
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, float maxDistance, int layerMask)
		{
			return Physics.SphereCastAll(origin, radius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00006DCC File Offset: 0x00004FCC
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, float maxDistance)
		{
			return Physics.SphereCastAll(origin, radius, direction, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00006DEC File Offset: 0x00004FEC
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction)
		{
			return Physics.SphereCastAll(origin, radius, direction, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00006E10 File Offset: 0x00005010
		public static RaycastHit[] SphereCastAll(Ray ray, float radius, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.SphereCastAll(ray.origin, radius, ray.direction, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00006E3C File Offset: 0x0000503C
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Ray ray, float radius, float maxDistance, int layerMask)
		{
			return Physics.SphereCastAll(ray, radius, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00006E58 File Offset: 0x00005058
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Ray ray, float radius, float maxDistance)
		{
			return Physics.SphereCastAll(ray, radius, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00006E78 File Offset: 0x00005078
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Ray ray, float radius)
		{
			return Physics.SphereCastAll(ray, radius, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00006E9C File Offset: 0x0000509C
		[FreeFunction("Physics::OverlapCapsule")]
		private static Collider[] OverlapCapsule_Internal(PhysicsScene physicsScene, Vector3 point0, Vector3 point1, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.OverlapCapsule_Internal_Injected(ref physicsScene, ref point0, ref point1, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00006EBC File Offset: 0x000050BC
		public static Collider[] OverlapCapsule(Vector3 point0, Vector3 point1, float radius, [DefaultValue("AllLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.OverlapCapsule_Internal(Physics.defaultPhysicsScene, point0, point1, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00006EE0 File Offset: 0x000050E0
		[ExcludeFromDocs]
		public static Collider[] OverlapCapsule(Vector3 point0, Vector3 point1, float radius, int layerMask)
		{
			return Physics.OverlapCapsule(point0, point1, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00006EFC File Offset: 0x000050FC
		[ExcludeFromDocs]
		public static Collider[] OverlapCapsule(Vector3 point0, Vector3 point1, float radius)
		{
			return Physics.OverlapCapsule(point0, point1, radius, -1, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00006F18 File Offset: 0x00005118
		[FreeFunction("Physics::OverlapSphere")]
		private static Collider[] OverlapSphere_Internal(PhysicsScene physicsScene, Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.OverlapSphere_Internal_Injected(ref physicsScene, ref position, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00006F34 File Offset: 0x00005134
		public static Collider[] OverlapSphere(Vector3 position, float radius, [DefaultValue("AllLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.OverlapSphere_Internal(Physics.defaultPhysicsScene, position, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00006F54 File Offset: 0x00005154
		[ExcludeFromDocs]
		public static Collider[] OverlapSphere(Vector3 position, float radius, int layerMask)
		{
			return Physics.OverlapSphere(position, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00006F70 File Offset: 0x00005170
		[ExcludeFromDocs]
		public static Collider[] OverlapSphere(Vector3 position, float radius)
		{
			return Physics.OverlapSphere(position, radius, -1, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00006F8C File Offset: 0x0000518C
		[NativeName("Simulate")]
		internal static void Simulate_Internal(PhysicsScene physicsScene, float step, SimulationStage stages, SimulationOption options)
		{
			Physics.Simulate_Internal_Injected(ref physicsScene, step, stages, options);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00006FA4 File Offset: 0x000051A4
		public static void Simulate(float step)
		{
			bool flag = Physics.simulationMode != SimulationMode.Script;
			if (flag)
			{
				Debug.LogWarning("Physics.Simulate(...) was called but simulation mode is not set to Script. You should set simulation mode to Script first before calling this function therefore the simulation was not run.");
			}
			else
			{
				Physics.Simulate_Internal(Physics.defaultPhysicsScene, step, SimulationStage.All, SimulationOption.All);
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00006FE0 File Offset: 0x000051E0
		[NativeName("InterpolateBodies")]
		internal static void InterpolateBodies_Internal(PhysicsScene physicsScene)
		{
			Physics.InterpolateBodies_Internal_Injected(ref physicsScene);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00006FF4 File Offset: 0x000051F4
		[NativeName("ResetInterpolatedTransformPosition")]
		internal static void ResetInterpolationPoses_Internal(PhysicsScene physicsScene)
		{
			Physics.ResetInterpolationPoses_Internal_Injected(ref physicsScene);
		}

		// Token: 0x06000346 RID: 838
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SyncTransforms();

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000347 RID: 839
		// (set) Token: 0x06000348 RID: 840
		public static extern bool autoSyncTransforms
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000349 RID: 841
		// (set) Token: 0x0600034A RID: 842
		public static extern bool reuseCollisionCallbacks
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00007008 File Offset: 0x00005208
		[FreeFunction("Physics::ComputePenetration")]
		private static bool Query_ComputePenetration([NotNull] Collider colliderA, Vector3 positionA, Quaternion rotationA, [NotNull] Collider colliderB, Vector3 positionB, Quaternion rotationB, ref Vector3 direction, ref float distance)
		{
			if (colliderA == null)
			{
				ThrowHelper.ThrowArgumentNullException(colliderA, "colliderA");
			}
			if (colliderB == null)
			{
				ThrowHelper.ThrowArgumentNullException(colliderB, "colliderB");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(colliderA);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(colliderA, "colliderA");
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Collider>(colliderB);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(colliderB, "colliderB");
			}
			return Physics.Query_ComputePenetration_Injected(intPtr, ref positionA, ref rotationA, intPtr2, ref positionB, ref rotationB, ref direction, ref distance);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00007070 File Offset: 0x00005270
		public static bool ComputePenetration(Collider colliderA, Vector3 positionA, Quaternion rotationA, Collider colliderB, Vector3 positionB, Quaternion rotationB, out Vector3 direction, out float distance)
		{
			direction = Vector3.zero;
			distance = 0f;
			return Physics.Query_ComputePenetration(colliderA, positionA, rotationA, colliderB, positionB, rotationB, ref direction, ref distance);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000070A8 File Offset: 0x000052A8
		[FreeFunction("Physics::ClosestPoint")]
		private static Vector3 Query_ClosestPoint([NotNull] Collider collider, Vector3 position, Quaternion rotation, Vector3 point)
		{
			if (collider == null)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Collider>(collider);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(collider, "collider");
			}
			Vector3 vector;
			Physics.Query_ClosestPoint_Injected(intPtr, ref position, ref rotation, ref point, out vector);
			return vector;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000070E8 File Offset: 0x000052E8
		public static Vector3 ClosestPoint(Vector3 point, Collider collider, Vector3 position, Quaternion rotation)
		{
			return Physics.Query_ClosestPoint(collider, position, rotation, point);
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600034F RID: 847
		// (set) Token: 0x06000350 RID: 848
		[StaticAccessor("GetPhysicsManager()")]
		public static extern float interCollisionDistance
		{
			[NativeName("GetClothInterCollisionDistance")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SetClothInterCollisionDistance")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000351 RID: 849
		// (set) Token: 0x06000352 RID: 850
		[StaticAccessor("GetPhysicsManager()")]
		public static extern float interCollisionStiffness
		{
			[NativeName("GetClothInterCollisionStiffness")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SetClothInterCollisionStiffness")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000353 RID: 851
		// (set) Token: 0x06000354 RID: 852
		[StaticAccessor("GetPhysicsManager()")]
		public static extern bool interCollisionSettingsToggle
		{
			[NativeName("GetClothInterCollisionSettingsToggle")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeName("SetClothInterCollisionSettingsToggle")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00007104 File Offset: 0x00005304
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000711C File Offset: 0x0000531C
		public static Vector3 clothGravity
		{
			[ThreadSafe]
			get
			{
				Vector3 vector;
				Physics.get_clothGravity_Injected(out vector);
				return vector;
			}
			set
			{
				Physics.set_clothGravity_Injected(ref value);
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00007130 File Offset: 0x00005330
		public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results, [DefaultValue("AllLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.OverlapSphere(position, radius, results, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00007158 File Offset: 0x00005358
		[ExcludeFromDocs]
		public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results, int layerMask)
		{
			return Physics.OverlapSphereNonAlloc(position, radius, results, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00007174 File Offset: 0x00005374
		[ExcludeFromDocs]
		public static int OverlapSphereNonAlloc(Vector3 position, float radius, Collider[] results)
		{
			return Physics.OverlapSphereNonAlloc(position, radius, results, -1, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00007190 File Offset: 0x00005390
		[FreeFunction("Physics::SphereTest")]
		private static bool CheckSphere_Internal(PhysicsScene physicsScene, Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CheckSphere_Internal_Injected(ref physicsScene, ref position, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000071AC File Offset: 0x000053AC
		public static bool CheckSphere(Vector3 position, float radius, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CheckSphere_Internal(Physics.defaultPhysicsScene, position, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x000071CC File Offset: 0x000053CC
		[ExcludeFromDocs]
		public static bool CheckSphere(Vector3 position, float radius, int layerMask)
		{
			return Physics.CheckSphere(position, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x000071E8 File Offset: 0x000053E8
		[ExcludeFromDocs]
		public static bool CheckSphere(Vector3 position, float radius)
		{
			return Physics.CheckSphere(position, radius, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00007204 File Offset: 0x00005404
		public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.CapsuleCast(point1, point2, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00007230 File Offset: 0x00005430
		[ExcludeFromDocs]
		public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask)
		{
			return Physics.CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00007254 File Offset: 0x00005454
		[ExcludeFromDocs]
		public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, float maxDistance)
		{
			return Physics.CapsuleCastNonAlloc(point1, point2, radius, direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00007278 File Offset: 0x00005478
		[ExcludeFromDocs]
		public static int CapsuleCastNonAlloc(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results)
		{
			return Physics.CapsuleCastNonAlloc(point1, point2, radius, direction, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000072A0 File Offset: 0x000054A0
		public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.SphereCast(origin, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000072CC File Offset: 0x000054CC
		[ExcludeFromDocs]
		public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, float maxDistance, int layerMask)
		{
			return Physics.SphereCastNonAlloc(origin, radius, direction, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000072EC File Offset: 0x000054EC
		[ExcludeFromDocs]
		public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, float maxDistance)
		{
			return Physics.SphereCastNonAlloc(origin, radius, direction, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000730C File Offset: 0x0000550C
		[ExcludeFromDocs]
		public static int SphereCastNonAlloc(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results)
		{
			return Physics.SphereCastNonAlloc(origin, radius, direction, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00007330 File Offset: 0x00005530
		public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.SphereCastNonAlloc(ray.origin, radius, ray.direction, results, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000735C File Offset: 0x0000555C
		[ExcludeFromDocs]
		public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, float maxDistance, int layerMask)
		{
			return Physics.SphereCastNonAlloc(ray, radius, results, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000737C File Offset: 0x0000557C
		[ExcludeFromDocs]
		public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results, float maxDistance)
		{
			return Physics.SphereCastNonAlloc(ray, radius, results, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000739C File Offset: 0x0000559C
		[ExcludeFromDocs]
		public static int SphereCastNonAlloc(Ray ray, float radius, RaycastHit[] results)
		{
			return Physics.SphereCastNonAlloc(ray, radius, results, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000073C0 File Offset: 0x000055C0
		[FreeFunction("Physics::CapsuleTest")]
		private static bool CheckCapsule_Internal(PhysicsScene physicsScene, Vector3 start, Vector3 end, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CheckCapsule_Internal_Injected(ref physicsScene, ref start, ref end, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000073E0 File Offset: 0x000055E0
		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CheckCapsule_Internal(Physics.defaultPhysicsScene, start, end, radius, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00007404 File Offset: 0x00005604
		[ExcludeFromDocs]
		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, int layerMask)
		{
			return Physics.CheckCapsule(start, end, radius, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00007420 File Offset: 0x00005620
		[ExcludeFromDocs]
		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius)
		{
			return Physics.CheckCapsule(start, end, radius, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00007440 File Offset: 0x00005640
		[FreeFunction("Physics::BoxTest")]
		private static bool CheckBox_Internal(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Quaternion orientation, int layermask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CheckBox_Internal_Injected(ref physicsScene, ref center, ref halfExtents, ref orientation, layermask, queryTriggerInteraction);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00007460 File Offset: 0x00005660
		public static bool CheckBox(Vector3 center, Vector3 halfExtents, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("DefaultRaycastLayers")] int layermask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.CheckBox_Internal(Physics.defaultPhysicsScene, center, halfExtents, orientation, layermask, queryTriggerInteraction);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00007484 File Offset: 0x00005684
		[ExcludeFromDocs]
		public static bool CheckBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask)
		{
			return Physics.CheckBox(center, halfExtents, orientation, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000074A0 File Offset: 0x000056A0
		[ExcludeFromDocs]
		public static bool CheckBox(Vector3 center, Vector3 halfExtents, Quaternion orientation)
		{
			return Physics.CheckBox(center, halfExtents, orientation, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000074C0 File Offset: 0x000056C0
		[ExcludeFromDocs]
		public static bool CheckBox(Vector3 center, Vector3 halfExtents)
		{
			return Physics.CheckBox(center, halfExtents, Quaternion.identity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000074E4 File Offset: 0x000056E4
		[FreeFunction("Physics::OverlapBox")]
		private static Collider[] OverlapBox_Internal(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.OverlapBox_Internal_Injected(ref physicsScene, ref center, ref halfExtents, ref orientation, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00007504 File Offset: 0x00005704
		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("AllLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.OverlapBox_Internal(Physics.defaultPhysicsScene, center, halfExtents, orientation, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00007528 File Offset: 0x00005728
		[ExcludeFromDocs]
		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask)
		{
			return Physics.OverlapBox(center, halfExtents, orientation, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00007544 File Offset: 0x00005744
		[ExcludeFromDocs]
		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation)
		{
			return Physics.OverlapBox(center, halfExtents, orientation, -1, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00007560 File Offset: 0x00005760
		[ExcludeFromDocs]
		public static Collider[] OverlapBox(Vector3 center, Vector3 halfExtents)
		{
			return Physics.OverlapBox(center, halfExtents, Quaternion.identity, -1, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00007580 File Offset: 0x00005780
		public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("AllLayers")] int mask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.OverlapBox(center, halfExtents, results, orientation, mask, queryTriggerInteraction);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000075A8 File Offset: 0x000057A8
		[ExcludeFromDocs]
		public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, Quaternion orientation, int mask)
		{
			return Physics.OverlapBoxNonAlloc(center, halfExtents, results, orientation, mask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000075C8 File Offset: 0x000057C8
		[ExcludeFromDocs]
		public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results, Quaternion orientation)
		{
			return Physics.OverlapBoxNonAlloc(center, halfExtents, results, orientation, -1, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000075E8 File Offset: 0x000057E8
		[ExcludeFromDocs]
		public static int OverlapBoxNonAlloc(Vector3 center, Vector3 halfExtents, Collider[] results)
		{
			return Physics.OverlapBoxNonAlloc(center, halfExtents, results, Quaternion.identity, -1, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000760C File Offset: 0x0000580C
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.BoxCast(center, halfExtents, direction, results, orientation, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00007638 File Offset: 0x00005838
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation)
		{
			return Physics.BoxCastNonAlloc(center, halfExtents, direction, results, orientation, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00007660 File Offset: 0x00005860
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation, float maxDistance)
		{
			return Physics.BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00007684 File Offset: 0x00005884
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, Quaternion orientation, float maxDistance, int layerMask)
		{
			return Physics.BoxCastNonAlloc(center, halfExtents, direction, results, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000076A8 File Offset: 0x000058A8
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results)
		{
			return Physics.BoxCastNonAlloc(center, halfExtents, direction, results, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000076D0 File Offset: 0x000058D0
		[FreeFunction("Physics::BoxCastAll")]
		private static RaycastHit[] Internal_BoxCastAll(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			RaycastHit[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Physics.Internal_BoxCastAll_Injected(ref physicsScene, ref center, ref halfExtents, ref direction, ref orientation, maxDistance, layerMask, queryTriggerInteraction, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				RaycastHit[] array;
				blittableArrayWrapper.Unmarshal<RaycastHit>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00007714 File Offset: 0x00005914
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("Mathf.Infinity")] float maxDistance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			RaycastHit[] array;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				array = Physics.Internal_BoxCastAll(Physics.defaultPhysicsScene, center, halfExtents, vector, orientation, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				array = new RaycastHit[0];
			}
			return array;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00007764 File Offset: 0x00005964
		[ExcludeFromDocs]
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, int layerMask)
		{
			return Physics.BoxCastAll(center, halfExtents, direction, orientation, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00007784 File Offset: 0x00005984
		[ExcludeFromDocs]
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance)
		{
			return Physics.BoxCastAll(center, halfExtents, direction, orientation, maxDistance, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000077A4 File Offset: 0x000059A4
		[ExcludeFromDocs]
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation)
		{
			return Physics.BoxCastAll(center, halfExtents, direction, orientation, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000077C8 File Offset: 0x000059C8
		[ExcludeFromDocs]
		public static RaycastHit[] BoxCastAll(Vector3 center, Vector3 halfExtents, Vector3 direction)
		{
			return Physics.BoxCastAll(center, halfExtents, direction, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000077F0 File Offset: 0x000059F0
		public static int OverlapCapsuleNonAlloc(Vector3 point0, Vector3 point1, float radius, Collider[] results, [DefaultValue("AllLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return Physics.defaultPhysicsScene.OverlapCapsule(point0, point1, radius, results, layerMask, queryTriggerInteraction);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00007818 File Offset: 0x00005A18
		[ExcludeFromDocs]
		public static int OverlapCapsuleNonAlloc(Vector3 point0, Vector3 point1, float radius, Collider[] results, int layerMask)
		{
			return Physics.OverlapCapsuleNonAlloc(point0, point1, radius, results, layerMask, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00007838 File Offset: 0x00005A38
		[ExcludeFromDocs]
		public static int OverlapCapsuleNonAlloc(Vector3 point0, Vector3 point1, float radius, Collider[] results)
		{
			return Physics.OverlapCapsuleNonAlloc(point0, point1, radius, results, -1, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00007858 File Offset: 0x00005A58
		[StaticAccessor("GetPhysicsManager()")]
		public static void RebuildBroadphaseRegions(Bounds worldBounds, int subdivisions)
		{
			Physics.RebuildBroadphaseRegions_Injected(ref worldBounds, subdivisions);
		}

		// Token: 0x0600038B RID: 907
		[StaticAccessor("GetPhysicsManager()")]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BakeMesh(int meshID, bool convex, MeshColliderCookingOptions cookingOptions);

		// Token: 0x0600038C RID: 908 RVA: 0x0000786D File Offset: 0x00005A6D
		public static void BakeMesh(int meshID, bool convex)
		{
			Physics.BakeMesh(meshID, convex, MeshColliderCookingOptions.CookForFasterSimulation | MeshColliderCookingOptions.EnableMeshCleaning | MeshColliderCookingOptions.WeldColocatedVertices | MeshColliderCookingOptions.UseFastMidphase);
		}

		// Token: 0x0600038D RID: 909
		[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool ConnectPhysicsSDKVisualDebugger();

		// Token: 0x0600038E RID: 910
		[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DisconnectPhysicsSDKVisualDebugger();

		// Token: 0x0600038F RID: 911 RVA: 0x0000787C File Offset: 0x00005A7C
		[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
		internal static Collider GetColliderByInstanceID(int instanceID)
		{
			return Unmarshal.UnmarshalUnityObject<Collider>(Physics.GetColliderByInstanceID_Injected(instanceID));
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00007894 File Offset: 0x00005A94
		[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
		internal static Component GetBodyByInstanceID(int instanceID)
		{
			return Unmarshal.UnmarshalUnityObject<Component>(Physics.GetBodyByInstanceID_Injected(instanceID));
		}

		// Token: 0x06000391 RID: 913
		[ThreadSafe]
		[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint TranslateTriangleIndexFromID(int instanceID, uint faceIndex);

		// Token: 0x06000392 RID: 914 RVA: 0x000078AC File Offset: 0x00005AAC
		[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
		private static void SendOnCollisionEnter(Component component, Collision collision)
		{
			Physics.SendOnCollisionEnter_Injected(Object.MarshalledUnityObject.Marshal<Component>(component), collision);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000078C8 File Offset: 0x00005AC8
		[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
		private static void SendOnCollisionStay(Component component, Collision collision)
		{
			Physics.SendOnCollisionStay_Injected(Object.MarshalledUnityObject.Marshal<Component>(component), collision);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000078E4 File Offset: 0x00005AE4
		[StaticAccessor("PhysicsManager", StaticAccessorType.DoubleColon)]
		private static void SendOnCollisionExit(Component component, Collision collision)
		{
			Physics.SendOnCollisionExit_Injected(Object.MarshalledUnityObject.Marshal<Component>(component), collision);
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000395 RID: 917 RVA: 0x00007900 File Offset: 0x00005B00
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000274B File Offset: 0x0000094B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use Physics.defaultContactOffset or Collider.contactOffset instead.", true)]
		public static float minPenetrationForPenalty
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000397 RID: 919 RVA: 0x00007918 File Offset: 0x00005B18
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0000792F File Offset: 0x00005B2F
		[Obsolete("Please use bounceThreshold instead. (UnityUpgradable) -> bounceThreshold")]
		public static float bounceTreshold
		{
			get
			{
				return Physics.bounceThreshold;
			}
			set
			{
				Physics.bounceThreshold = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000793C File Offset: 0x00005B3C
		// (set) Token: 0x0600039A RID: 922 RVA: 0x0000274B File Offset: 0x0000094B
		[Obsolete("The sleepVelocity is no longer supported. Use sleepThreshold. Note that sleepThreshold is energy but not velocity.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static float sleepVelocity
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00007954 File Offset: 0x00005B54
		// (set) Token: 0x0600039C RID: 924 RVA: 0x0000274B File Offset: 0x0000094B
		[Obsolete("The sleepAngularVelocity is no longer supported. Use sleepThreshold. Note that sleepThreshold is energy but not velocity.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static float sleepAngularVelocity
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000796C File Offset: 0x00005B6C
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0000274B File Offset: 0x0000094B
		[Obsolete("Use Rigidbody.maxAngularVelocity instead.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static float maxAngularVelocity
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00007984 File Offset: 0x00005B84
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x0000799B File Offset: 0x00005B9B
		[Obsolete("Please use Physics.defaultSolverIterations instead. (UnityUpgradable) -> defaultSolverIterations")]
		public static int solverIterationCount
		{
			get
			{
				return Physics.defaultSolverIterations;
			}
			set
			{
				Physics.defaultSolverIterations = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x000079A8 File Offset: 0x00005BA8
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x000079BF File Offset: 0x00005BBF
		[Obsolete("Please use Physics.defaultSolverVelocityIterations instead. (UnityUpgradable) -> defaultSolverVelocityIterations")]
		public static int solverVelocityIterationCount
		{
			get
			{
				return Physics.defaultSolverVelocityIterations;
			}
			set
			{
				Physics.defaultSolverVelocityIterations = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x000079CC File Offset: 0x00005BCC
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x0000274B File Offset: 0x0000094B
		[Obsolete("penetrationPenaltyForce has no effect.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static float penetrationPenaltyForce
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x000079E4 File Offset: 0x00005BE4
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x00007A01 File Offset: 0x00005C01
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Physics.autoSimulation has been replaced by Physics.simulationMode", false)]
		public static bool autoSimulation
		{
			get
			{
				return Physics.simulationMode != SimulationMode.Script;
			}
			set
			{
				Physics.simulationMode = (value ? SimulationMode.FixedUpdate : SimulationMode.Script);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060003A7 RID: 935 RVA: 0x00007A14 File Offset: 0x00005C14
		// (remove) Token: 0x060003A8 RID: 936 RVA: 0x00007A48 File Offset: 0x00005C48
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Physics.ContactEventDelegate ContactEvent;

		// Token: 0x060003A9 RID: 937 RVA: 0x00007A7C File Offset: 0x00005C7C
		[RequiredByNativeCode]
		private static void OnSceneContact(PhysicsScene scene, IntPtr buffer, int count)
		{
			bool flag = count == 0;
			if (!flag)
			{
				NativeArray<ContactPairHeader> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<ContactPairHeader>(buffer.ToPointer(), count, Allocator.None);
				try
				{
					Physics.ContactEventDelegate contactEvent = Physics.ContactEvent;
					if (contactEvent != null)
					{
						contactEvent(scene, nativeArray.AsReadOnly());
					}
				}
				catch (Exception ex)
				{
					Debug.LogError(ex);
				}
				finally
				{
					Physics.ReportContacts(nativeArray.AsReadOnly());
				}
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00007AFC File Offset: 0x00005CFC
		private static void ReportContacts(NativeArray<ContactPairHeader>.ReadOnly array)
		{
			bool flag = !Physics.invokeCollisionCallbacks;
			if (!flag)
			{
				for (int i = 0; i < array.Length; i++)
				{
					ContactPairHeader contactPairHeader = array[i];
					bool hasRemovedBody = contactPairHeader.hasRemovedBody;
					if (!hasRemovedBody)
					{
						int num = 0;
						while ((long)num < (long)((ulong)contactPairHeader.m_NbPairs))
						{
							readonly ref ContactPair contactPair = ref contactPairHeader.GetContactPair(num);
							bool hasRemovedCollider = contactPair.hasRemovedCollider;
							if (!hasRemovedCollider)
							{
								Component body = contactPairHeader.body;
								Component otherBody = contactPairHeader.otherBody;
								Component component = ((body != null) ? body : contactPair.collider);
								Component component2 = ((otherBody != null) ? otherBody : contactPair.otherCollider);
								bool flag2 = !component || !component2;
								if (!flag2)
								{
									bool isCollisionEnter = contactPair.isCollisionEnter;
									if (isCollisionEnter)
									{
										Physics.SendOnCollisionEnter(component, Physics.GetCollisionToReport(in contactPairHeader, in contactPair, false));
										Physics.SendOnCollisionEnter(component2, Physics.GetCollisionToReport(in contactPairHeader, in contactPair, true));
									}
									bool isCollisionStay = contactPair.isCollisionStay;
									if (isCollisionStay)
									{
										Physics.SendOnCollisionStay(component, Physics.GetCollisionToReport(in contactPairHeader, in contactPair, false));
										Physics.SendOnCollisionStay(component2, Physics.GetCollisionToReport(in contactPairHeader, in contactPair, true));
									}
									bool isCollisionExit = contactPair.isCollisionExit;
									if (isCollisionExit)
									{
										Physics.SendOnCollisionExit(component, Physics.GetCollisionToReport(in contactPairHeader, in contactPair, false));
										Physics.SendOnCollisionExit(component2, Physics.GetCollisionToReport(in contactPairHeader, in contactPair, true));
									}
								}
							}
							num++;
						}
					}
				}
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00007C90 File Offset: 0x00005E90
		private static Collision GetCollisionToReport(in ContactPairHeader header, in ContactPair pair, bool flipped)
		{
			bool reuseCollisionCallbacks = Physics.reuseCollisionCallbacks;
			Collision collision;
			if (reuseCollisionCallbacks)
			{
				Physics.s_ReusableCollision.Reuse(in header, in pair);
				Physics.s_ReusableCollision.Flipped = flipped;
				collision = Physics.s_ReusableCollision;
			}
			else
			{
				collision = new Collision(in header, in pair, flipped);
			}
			return collision;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00007CD6 File Offset: 0x00005ED6
		// Note: this type is marked as 'beforefieldinit'.
		static Physics()
		{
			Physics.GenericContactModifyEvent = new Action<PhysicsScene, IntPtr, int, bool>(Physics.PhysXOnSceneContactModify);
			Physics.s_ReusableCollision = new Collision();
		}

		// Token: 0x060003AE RID: 942
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_gravity_Injected(out Vector3 ret);

		// Token: 0x060003AF RID: 943
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_gravity_Injected([In] ref Vector3 value);

		// Token: 0x060003B0 RID: 944
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IgnoreCollision_Injected(IntPtr collider1, IntPtr collider2, [DefaultValue("true")] bool ignore);

		// Token: 0x060003B1 RID: 945
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIgnoreCollision_Injected(IntPtr collider1, IntPtr collider2);

		// Token: 0x060003B2 RID: 946
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_RaycastAll_Injected([In] ref PhysicsScene physicsScene, [In] ref Ray ray, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction, out BlittableArrayWrapper ret);

		// Token: 0x060003B3 RID: 947
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Query_CapsuleCastAll_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 p0, [In] ref Vector3 p1, float radius, [In] ref Vector3 direction, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction, out BlittableArrayWrapper ret);

		// Token: 0x060003B4 RID: 948
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Query_SphereCastAll_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 origin, float radius, [In] ref Vector3 direction, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction, out BlittableArrayWrapper ret);

		// Token: 0x060003B5 RID: 949
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider[] OverlapCapsule_Internal_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 point0, [In] ref Vector3 point1, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060003B6 RID: 950
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider[] OverlapSphere_Internal_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060003B7 RID: 951
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Simulate_Internal_Injected([In] ref PhysicsScene physicsScene, float step, SimulationStage stages, SimulationOption options);

		// Token: 0x060003B8 RID: 952
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InterpolateBodies_Internal_Injected([In] ref PhysicsScene physicsScene);

		// Token: 0x060003B9 RID: 953
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetInterpolationPoses_Internal_Injected([In] ref PhysicsScene physicsScene);

		// Token: 0x060003BA RID: 954
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Query_ComputePenetration_Injected(IntPtr colliderA, [In] ref Vector3 positionA, [In] ref Quaternion rotationA, IntPtr colliderB, [In] ref Vector3 positionB, [In] ref Quaternion rotationB, ref Vector3 direction, ref float distance);

		// Token: 0x060003BB RID: 955
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Query_ClosestPoint_Injected(IntPtr collider, [In] ref Vector3 position, [In] ref Quaternion rotation, [In] ref Vector3 point, out Vector3 ret);

		// Token: 0x060003BC RID: 956
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_clothGravity_Injected(out Vector3 ret);

		// Token: 0x060003BD RID: 957
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_clothGravity_Injected([In] ref Vector3 value);

		// Token: 0x060003BE RID: 958
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CheckSphere_Internal_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 position, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060003BF RID: 959
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CheckCapsule_Internal_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 start, [In] ref Vector3 end, float radius, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060003C0 RID: 960
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CheckBox_Internal_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 center, [In] ref Vector3 halfExtents, [In] ref Quaternion orientation, int layermask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060003C1 RID: 961
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider[] OverlapBox_Internal_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 center, [In] ref Vector3 halfExtents, [In] ref Quaternion orientation, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060003C2 RID: 962
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BoxCastAll_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 center, [In] ref Vector3 halfExtents, [In] ref Vector3 direction, [In] ref Quaternion orientation, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction, out BlittableArrayWrapper ret);

		// Token: 0x060003C3 RID: 963
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RebuildBroadphaseRegions_Injected([In] ref Bounds worldBounds, int subdivisions);

		// Token: 0x060003C4 RID: 964
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetColliderByInstanceID_Injected(int instanceID);

		// Token: 0x060003C5 RID: 965
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetBodyByInstanceID_Injected(int instanceID);

		// Token: 0x060003C6 RID: 966
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendOnCollisionEnter_Injected(IntPtr component, Collision collision);

		// Token: 0x060003C7 RID: 967
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendOnCollisionStay_Injected(IntPtr component, Collision collision);

		// Token: 0x060003C8 RID: 968
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendOnCollisionExit_Injected(IntPtr component, Collision collision);

		// Token: 0x04000075 RID: 117
		internal const float k_MaxFloatMinusEpsilon = 3.4028233E+38f;

		// Token: 0x04000076 RID: 118
		public const int IgnoreRaycastLayer = 4;

		// Token: 0x04000077 RID: 119
		public const int DefaultRaycastLayers = -5;

		// Token: 0x04000078 RID: 120
		public const int AllLayers = -1;

		// Token: 0x04000079 RID: 121
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Please use Physics.IgnoreRaycastLayer instead. (UnityUpgradable) -> IgnoreRaycastLayer", true)]
		public const int kIgnoreRaycastLayer = 4;

		// Token: 0x0400007A RID: 122
		[Obsolete("Please use Physics.DefaultRaycastLayers instead. (UnityUpgradable) -> DefaultRaycastLayers", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public const int kDefaultRaycastLayers = -5;

		// Token: 0x0400007B RID: 123
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Please use Physics.AllLayers instead. (UnityUpgradable) -> AllLayers", true)]
		public const int kAllLayers = -1;

		// Token: 0x0400007D RID: 125
		private static readonly Collision s_ReusableCollision;

		// Token: 0x02000024 RID: 36
		// (Invoke) Token: 0x060003CA RID: 970
		public delegate void ContactEventDelegate(PhysicsScene scene, NativeArray<ContactPairHeader>.ReadOnly headerArray);
	}
}
