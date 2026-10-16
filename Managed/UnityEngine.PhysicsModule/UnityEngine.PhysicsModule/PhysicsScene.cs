using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000043 RID: 67
	[NativeHeader("Modules/Physics/Public/PhysicsSceneHandle.h")]
	[NativeHeader("Modules/Physics/PhysicsQuery.h")]
	public struct PhysicsScene : IEquatable<PhysicsScene>
	{
		// Token: 0x060004C1 RID: 1217 RVA: 0x00009034 File Offset: 0x00007234
		public override string ToString()
		{
			return string.Format("PhysicsScene(Index: {0}, Version: {1})", this.m_index, this.m_version);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00009068 File Offset: 0x00007268
		public static bool operator ==(PhysicsScene lhs, PhysicsScene rhs)
		{
			return lhs.m_index == rhs.m_index && lhs.m_version == rhs.m_version;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000909C File Offset: 0x0000729C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(PhysicsScene lhs, PhysicsScene rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000090B8 File Offset: 0x000072B8
		public override int GetHashCode()
		{
			return HashCode.Combine<int, int>(this.m_index, this.m_version);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000090DC File Offset: 0x000072DC
		public override bool Equals(object other)
		{
			bool flag = !(other is PhysicsScene);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				PhysicsScene physicsScene = (PhysicsScene)other;
				flag2 = this == physicsScene;
			}
			return flag2;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00009118 File Offset: 0x00007318
		public bool Equals(PhysicsScene other)
		{
			return this == other;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00009138 File Offset: 0x00007338
		public bool IsValid()
		{
			return PhysicsScene.IsValid_Internal(this);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00009158 File Offset: 0x00007358
		[StaticAccessor("GetPhysicsManager()", StaticAccessorType.Dot)]
		[NativeMethod("IsPhysicsSceneValid")]
		private static bool IsValid_Internal(PhysicsScene physicsScene)
		{
			return PhysicsScene.IsValid_Internal_Injected(ref physicsScene);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000916C File Offset: 0x0000736C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static PhysicsScene GetDefaultScene()
		{
			return new PhysicsScene
			{
				m_index = 0,
				m_version = 0
			};
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00009198 File Offset: 0x00007398
		public bool IsEmpty()
		{
			bool flag = this.IsValid();
			if (flag)
			{
				return PhysicsScene.IsEmpty_Internal(this);
			}
			throw new InvalidOperationException("Cannot check if physics scene is empty as it is invalid.");
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000091CC File Offset: 0x000073CC
		[StaticAccessor("GetPhysicsManager()", StaticAccessorType.Dot)]
		[NativeMethod("IsPhysicsWorldEmpty")]
		private static bool IsEmpty_Internal(PhysicsScene physicsScene)
		{
			return PhysicsScene.IsEmpty_Internal_Injected(ref physicsScene);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000091E0 File Offset: 0x000073E0
		public void Simulate(float step)
		{
			bool flag = this.IsValid();
			if (flag)
			{
				bool flag2 = this == PhysicsScene.GetDefaultScene() && Physics.simulationMode != SimulationMode.Script;
				if (flag2)
				{
					Debug.LogWarning("PhysicsScene.Simulate(...) was called but simulation mode is not set to Script. You should set simulation mode to Script first before calling this function therefore the simulation was not run.");
				}
				else
				{
					Physics.Simulate_Internal(this, step, SimulationStage.All, SimulationOption.All);
				}
				return;
			}
			throw new InvalidOperationException("Cannot simulate the physics scene as it is invalid.");
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00009248 File Offset: 0x00007448
		public void RunSimulationStages(float step, SimulationStage stages, [DefaultValue("SimulationOption.All")] SimulationOption options = SimulationOption.All)
		{
			bool flag = !this.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("Cannot simulate the physics scene as it is invalid.");
			}
			bool flag2 = this == PhysicsScene.GetDefaultScene() && Physics.simulationMode != SimulationMode.Script;
			if (flag2)
			{
				Debug.LogWarning("PhysicsScene.Simulate(...) was called but simulation mode is not set to Script. You should set simulation mode to Script first before calling this function therefore the simulation was not run.");
			}
			else
			{
				Physics.Simulate_Internal(this, step, stages, options);
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000092B0 File Offset: 0x000074B0
		[StaticAccessor("GetPhysicsManager()", StaticAccessorType.Dot)]
		[NativeMethod("ReleasePhysicsSceneSimulationBuffers")]
		private static void ReleasePhysicsSceneSimulationBuffers_Internal(PhysicsScene handle)
		{
			PhysicsScene.ReleasePhysicsSceneSimulationBuffers_Internal_Injected(ref handle);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x000092C4 File Offset: 0x000074C4
		public void ReleaseLastSimulationStepBuffers()
		{
			PhysicsScene.ReleasePhysicsSceneSimulationBuffers_Internal(this);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000092D4 File Offset: 0x000074D4
		public void InterpolateBodies()
		{
			bool flag = !this.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("Cannot interpolate the physics scene as it is invalid.");
			}
			bool flag2 = this == Physics.defaultPhysicsScene;
			if (flag2)
			{
				Debug.LogWarning("PhysicsScene.InterpolateBodies() was called on the default Physics Scene. This is done automatically and the call will be ignored");
			}
			else
			{
				Physics.InterpolateBodies_Internal(this);
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00009328 File Offset: 0x00007528
		public void ResetInterpolationPoses()
		{
			bool flag = !this.IsValid();
			if (flag)
			{
				throw new InvalidOperationException("Cannot reset poses of the physics scene as it is invalid.");
			}
			bool flag2 = this == Physics.defaultPhysicsScene;
			if (flag2)
			{
				Debug.LogWarning("PhysicsScene.ResetInterpolationPoses() was called on the default Physics Scene. This is done automatically and the call will be ignored");
			}
			else
			{
				Physics.ResetInterpolationPoses_Internal(this);
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000937C File Offset: 0x0000757C
		public bool Raycast(Vector3 origin, Vector3 direction, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("Physics.DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			bool flag2;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				Ray ray = new Ray(origin, vector);
				flag2 = PhysicsScene.Internal_RaycastTest(this, ray, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000093CC File Offset: 0x000075CC
		[FreeFunction("Physics::RaycastTest")]
		private static bool Internal_RaycastTest(PhysicsScene physicsScene, Ray ray, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Internal_RaycastTest_Injected(ref physicsScene, ref ray, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000093E8 File Offset: 0x000075E8
		public bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("Physics.DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			hitInfo = default(RaycastHit);
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			bool flag2;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				Ray ray = new Ray(origin, vector);
				flag2 = PhysicsScene.Internal_Raycast(this, ray, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00009444 File Offset: 0x00007644
		[FreeFunction("Physics::Raycast")]
		private static bool Internal_Raycast(PhysicsScene physicsScene, Ray ray, float maxDistance, ref RaycastHit hit, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Internal_Raycast_Injected(ref physicsScene, ref ray, maxDistance, ref hit, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00009460 File Offset: 0x00007660
		public int Raycast(Vector3 origin, Vector3 direction, RaycastHit[] raycastHits, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("Physics.DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			int num;
			if (flag)
			{
				Ray ray = new Ray(origin, direction.normalized);
				num = PhysicsScene.Internal_RaycastNonAlloc(this, ray, raycastHits, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000094B0 File Offset: 0x000076B0
		[FreeFunction("Physics::RaycastNonAlloc")]
		private unsafe static int Internal_RaycastNonAlloc(PhysicsScene physicsScene, Ray ray, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			Span<RaycastHit> span = new Span<RaycastHit>(raycastHits);
			int num;
			fixed (RaycastHit* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = PhysicsScene.Internal_RaycastNonAlloc_Injected(ref physicsScene, ref ray, ref managedSpanWrapper, maxDistance, mask, queryTriggerInteraction);
			}
			return num;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000094F4 File Offset: 0x000076F4
		[FreeFunction("Physics::CapsuleCast")]
		private static bool Query_CapsuleCast(PhysicsScene physicsScene, Vector3 point1, Vector3 point2, float radius, Vector3 direction, float maxDistance, ref RaycastHit hitInfo, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Query_CapsuleCast_Injected(ref physicsScene, ref point1, ref point2, radius, ref direction, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00009518 File Offset: 0x00007718
		private static bool Internal_CapsuleCast(PhysicsScene physicsScene, Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			hitInfo = default(RaycastHit);
			bool flag = magnitude > float.Epsilon;
			bool flag2;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				flag2 = PhysicsScene.Query_CapsuleCast(physicsScene, point1, point2, radius, vector, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00009568 File Offset: 0x00007768
		public bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return PhysicsScene.Internal_CapsuleCast(this, point1, point2, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00009594 File Offset: 0x00007794
		[FreeFunction("Physics::CapsuleCastNonAlloc")]
		private unsafe static int Internal_CapsuleCastNonAlloc(PhysicsScene physicsScene, Vector3 p0, Vector3 p1, float radius, Vector3 direction, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			Span<RaycastHit> span = new Span<RaycastHit>(raycastHits);
			int num;
			fixed (RaycastHit* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = PhysicsScene.Internal_CapsuleCastNonAlloc_Injected(ref physicsScene, ref p0, ref p1, radius, ref direction, ref managedSpanWrapper, maxDistance, mask, queryTriggerInteraction);
			}
			return num;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000095DC File Offset: 0x000077DC
		public int CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			int num;
			if (flag)
			{
				num = PhysicsScene.Internal_CapsuleCastNonAlloc(this, point1, point2, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00009620 File Offset: 0x00007820
		[FreeFunction("Physics::OverlapCapsuleNonAlloc")]
		private static int OverlapCapsuleNonAlloc_Internal(PhysicsScene physicsScene, Vector3 point0, Vector3 point1, float radius, [Unmarshalled] Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.OverlapCapsuleNonAlloc_Internal_Injected(ref physicsScene, ref point0, ref point1, radius, results, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00009640 File Offset: 0x00007840
		public int OverlapCapsule(Vector3 point0, Vector3 point1, float radius, Collider[] results, [DefaultValue("AllLayers")] int layerMask = -1, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return PhysicsScene.OverlapCapsuleNonAlloc_Internal(this, point0, point1, radius, results, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00009668 File Offset: 0x00007868
		[FreeFunction("Physics::SphereCast")]
		private static bool Query_SphereCast(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, float maxDistance, ref RaycastHit hitInfo, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Query_SphereCast_Injected(ref physicsScene, ref origin, radius, ref direction, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000968C File Offset: 0x0000788C
		private static bool Internal_SphereCast(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			hitInfo = default(RaycastHit);
			bool flag = magnitude > float.Epsilon;
			bool flag2;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				flag2 = PhysicsScene.Query_SphereCast(physicsScene, origin, radius, vector, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000096D8 File Offset: 0x000078D8
		public bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return PhysicsScene.Internal_SphereCast(this, origin, radius, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00009700 File Offset: 0x00007900
		[FreeFunction("Physics::SphereCastNonAlloc")]
		private unsafe static int Internal_SphereCastNonAlloc(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, RaycastHit[] raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			Span<RaycastHit> span = new Span<RaycastHit>(raycastHits);
			int num;
			fixed (RaycastHit* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = PhysicsScene.Internal_SphereCastNonAlloc_Injected(ref physicsScene, ref origin, radius, ref direction, ref managedSpanWrapper, maxDistance, mask, queryTriggerInteraction);
			}
			return num;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00009748 File Offset: 0x00007948
		public int SphereCast(Vector3 origin, float radius, Vector3 direction, RaycastHit[] results, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			int num;
			if (flag)
			{
				num = PhysicsScene.Internal_SphereCastNonAlloc(this, origin, radius, direction, results, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000978C File Offset: 0x0000798C
		[FreeFunction("Physics::OverlapSphereNonAlloc")]
		private static int OverlapSphereNonAlloc_Internal(PhysicsScene physicsScene, Vector3 position, float radius, [Unmarshalled] Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.OverlapSphereNonAlloc_Internal_Injected(ref physicsScene, ref position, radius, results, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000097A8 File Offset: 0x000079A8
		public int OverlapSphere(Vector3 position, float radius, Collider[] results, [DefaultValue("AllLayers")] int layerMask, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.OverlapSphereNonAlloc_Internal(this, position, radius, results, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000097CC File Offset: 0x000079CC
		[FreeFunction("Physics::BoxCast")]
		private static bool Query_BoxCast(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Vector3 direction, Quaternion orientation, float maxDistance, ref RaycastHit outHit, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.Query_BoxCast_Injected(ref physicsScene, ref center, ref halfExtents, ref direction, ref orientation, maxDistance, ref outHit, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000097F0 File Offset: 0x000079F0
		private static bool Internal_BoxCast(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Quaternion orientation, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
		{
			float magnitude = direction.magnitude;
			hitInfo = default(RaycastHit);
			bool flag = magnitude > float.Epsilon;
			bool flag2;
			if (flag)
			{
				Vector3 vector = direction / magnitude;
				flag2 = PhysicsScene.Query_BoxCast(physicsScene, center, halfExtents, vector, orientation, maxDistance, ref hitInfo, layerMask, queryTriggerInteraction);
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00009840 File Offset: 0x00007A40
		public bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return PhysicsScene.Internal_BoxCast(this, center, halfExtents, orientation, direction, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000986C File Offset: 0x00007A6C
		[ExcludeFromDocs]
		public bool BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, out RaycastHit hitInfo)
		{
			return PhysicsScene.Internal_BoxCast(this, center, halfExtents, Quaternion.identity, direction, out hitInfo, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000989C File Offset: 0x00007A9C
		[FreeFunction("Physics::OverlapBoxNonAlloc")]
		private static int OverlapBoxNonAlloc_Internal(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, [Unmarshalled] Collider[] results, Quaternion orientation, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			return PhysicsScene.OverlapBoxNonAlloc_Internal_Injected(ref physicsScene, ref center, ref halfExtents, results, ref orientation, mask, queryTriggerInteraction);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x000098BC File Offset: 0x00007ABC
		public int OverlapBox(Vector3 center, Vector3 halfExtents, Collider[] results, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			return PhysicsScene.OverlapBoxNonAlloc_Internal(this, center, halfExtents, results, orientation, layerMask, queryTriggerInteraction);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000098E4 File Offset: 0x00007AE4
		[ExcludeFromDocs]
		public int OverlapBox(Vector3 center, Vector3 halfExtents, Collider[] results)
		{
			return PhysicsScene.OverlapBoxNonAlloc_Internal(this, center, halfExtents, results, Quaternion.identity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000990C File Offset: 0x00007B0C
		[FreeFunction("Physics::BoxCastNonAlloc")]
		private unsafe static int Internal_BoxCastNonAlloc(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] raycastHits, Quaternion orientation, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction)
		{
			Span<RaycastHit> span = new Span<RaycastHit>(raycastHits);
			int num;
			fixed (RaycastHit* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = PhysicsScene.Internal_BoxCastNonAlloc_Injected(ref physicsScene, ref center, ref halfExtents, ref direction, ref managedSpanWrapper, ref orientation, maxDistance, mask, queryTriggerInteraction);
			}
			return num;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00009958 File Offset: 0x00007B58
		public int BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results, [DefaultValue("Quaternion.identity")] Quaternion orientation, [DefaultValue("Mathf.Infinity")] float maxDistance = float.PositiveInfinity, [DefaultValue("DefaultRaycastLayers")] int layerMask = -5, [DefaultValue("QueryTriggerInteraction.UseGlobal")] QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
		{
			float magnitude = direction.magnitude;
			bool flag = magnitude > float.Epsilon;
			int num;
			if (flag)
			{
				num = PhysicsScene.Internal_BoxCastNonAlloc(this, center, halfExtents, direction, results, orientation, maxDistance, layerMask, queryTriggerInteraction);
			}
			else
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000999C File Offset: 0x00007B9C
		[ExcludeFromDocs]
		public int BoxCast(Vector3 center, Vector3 halfExtents, Vector3 direction, RaycastHit[] results)
		{
			return this.BoxCast(center, halfExtents, direction, results, Quaternion.identity, float.PositiveInfinity, -5, QueryTriggerInteraction.UseGlobal);
		}

		// Token: 0x060004F0 RID: 1264
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValid_Internal_Injected([In] ref PhysicsScene physicsScene);

		// Token: 0x060004F1 RID: 1265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsEmpty_Internal_Injected([In] ref PhysicsScene physicsScene);

		// Token: 0x060004F2 RID: 1266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleasePhysicsSceneSimulationBuffers_Internal_Injected([In] ref PhysicsScene handle);

		// Token: 0x060004F3 RID: 1267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_RaycastTest_Injected([In] ref PhysicsScene physicsScene, [In] ref Ray ray, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060004F4 RID: 1268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_Raycast_Injected([In] ref PhysicsScene physicsScene, [In] ref Ray ray, float maxDistance, ref RaycastHit hit, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060004F5 RID: 1269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_RaycastNonAlloc_Injected([In] ref PhysicsScene physicsScene, [In] ref Ray ray, ref ManagedSpanWrapper raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060004F6 RID: 1270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Query_CapsuleCast_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 point1, [In] ref Vector3 point2, float radius, [In] ref Vector3 direction, float maxDistance, ref RaycastHit hitInfo, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060004F7 RID: 1271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_CapsuleCastNonAlloc_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 p0, [In] ref Vector3 p1, float radius, [In] ref Vector3 direction, ref ManagedSpanWrapper raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060004F8 RID: 1272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapCapsuleNonAlloc_Internal_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 point0, [In] ref Vector3 point1, float radius, Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060004F9 RID: 1273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Query_SphereCast_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 origin, float radius, [In] ref Vector3 direction, float maxDistance, ref RaycastHit hitInfo, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060004FA RID: 1274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_SphereCastNonAlloc_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 origin, float radius, [In] ref Vector3 direction, ref ManagedSpanWrapper raycastHits, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060004FB RID: 1275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapSphereNonAlloc_Internal_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 position, float radius, Collider[] results, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060004FC RID: 1276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Query_BoxCast_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 center, [In] ref Vector3 halfExtents, [In] ref Vector3 direction, [In] ref Quaternion orientation, float maxDistance, ref RaycastHit outHit, int layerMask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060004FD RID: 1277
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int OverlapBoxNonAlloc_Internal_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 center, [In] ref Vector3 halfExtents, Collider[] results, [In] ref Quaternion orientation, int mask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x060004FE RID: 1278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_BoxCastNonAlloc_Injected([In] ref PhysicsScene physicsScene, [In] ref Vector3 center, [In] ref Vector3 halfExtents, [In] ref Vector3 direction, ref ManagedSpanWrapper raycastHits, [In] ref Quaternion orientation, float maxDistance, int mask, QueryTriggerInteraction queryTriggerInteraction);

		// Token: 0x0400011B RID: 283
		private int m_index;

		// Token: 0x0400011C RID: 284
		private int m_version;
	}
}
