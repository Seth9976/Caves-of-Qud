using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200004A RID: 74
	[NativeHeader("Modules/Physics/BatchCommands/BoxcastCommand.h")]
	[NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
	public struct BoxcastCommand
	{
		// Token: 0x06000544 RID: 1348 RVA: 0x0000A1CC File Offset: 0x000083CC
		public BoxcastCommand(Vector3 center, Vector3 halfExtents, Quaternion orientation, Vector3 direction, QueryParameters queryParameters, float distance = 3.4028235E+38f)
		{
			this.center = center;
			this.halfExtents = halfExtents;
			this.orientation = orientation;
			this.direction = direction;
			this.distance = distance;
			this.physicsScene = Physics.defaultPhysicsScene;
			this.queryParameters = queryParameters;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0000A218 File Offset: 0x00008418
		public BoxcastCommand(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Quaternion orientation, Vector3 direction, QueryParameters queryParameters, float distance = 3.4028235E+38f)
		{
			this.center = center;
			this.halfExtents = halfExtents;
			this.orientation = orientation;
			this.direction = direction;
			this.distance = distance;
			this.physicsScene = physicsScene;
			this.queryParameters = queryParameters;
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0000A256 File Offset: 0x00008456
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x0000A25E File Offset: 0x0000845E
		public Vector3 center { readonly get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0000A267 File Offset: 0x00008467
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x0000A26F File Offset: 0x0000846F
		public Vector3 halfExtents { readonly get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0000A278 File Offset: 0x00008478
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x0000A280 File Offset: 0x00008480
		public Quaternion orientation { readonly get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0000A289 File Offset: 0x00008489
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x0000A291 File Offset: 0x00008491
		public Vector3 direction { readonly get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0000A29A File Offset: 0x0000849A
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x0000A2A2 File Offset: 0x000084A2
		public float distance { readonly get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0000A2AB File Offset: 0x000084AB
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x0000A2B3 File Offset: 0x000084B3
		public PhysicsScene physicsScene { readonly get; set; }

		// Token: 0x06000552 RID: 1362 RVA: 0x0000A2BC File Offset: 0x000084BC
		public static JobHandle ScheduleBatch(NativeArray<BoxcastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, int maxHits, JobHandle dependsOn = default(JobHandle))
		{
			bool flag = maxHits < 1;
			JobHandle jobHandle;
			if (flag)
			{
				Debug.LogWarning("maxHits should be greater than 0.");
				jobHandle = default(JobHandle);
			}
			else
			{
				bool flag2 = results.Length < maxHits * commands.Length;
				if (flag2)
				{
					Debug.LogWarning("The supplied results buffer is too small, there should be at least maxHits space per each command in the batch.");
					jobHandle = default(JobHandle);
				}
				else
				{
					BatchQueryJob<BoxcastCommand, RaycastHit> batchQueryJob = new BatchQueryJob<BoxcastCommand, RaycastHit>(commands, results);
					JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<BoxcastCommand, RaycastHit>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<BoxcastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
					jobHandle = BoxcastCommand.ScheduleBoxcastBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<BoxcastCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastHit>(results), results.Length, minCommandsPerJob, maxHits);
				}
			}
			return jobHandle;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000A364 File Offset: 0x00008564
		public static JobHandle ScheduleBatch(NativeArray<BoxcastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
		{
			return BoxcastCommand.ScheduleBatch(commands, results, minCommandsPerJob, 1, dependsOn);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0000A380 File Offset: 0x00008580
		[FreeFunction("ScheduleBoxcastCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleBoxcastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits)
		{
			JobHandle jobHandle;
			BoxcastCommand.ScheduleBoxcastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, maxHits, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000A3A0 File Offset: 0x000085A0
		[Obsolete("This struct signature is no longer supported. Use struct with a QueryParameters instead", false)]
		public BoxcastCommand(Vector3 center, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5)
		{
			this.center = center;
			this.halfExtents = halfExtents;
			this.orientation = orientation;
			this.direction = direction;
			this.distance = distance;
			this.physicsScene = Physics.defaultPhysicsScene;
			this.queryParameters = QueryParameters.Default;
			this.layerMask = layerMask;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0000A3F8 File Offset: 0x000085F8
		[Obsolete("This struct signature is no longer supported. Use struct with a QueryParameters instead", false)]
		public BoxcastCommand(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Quaternion orientation, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5)
		{
			this.center = center;
			this.halfExtents = halfExtents;
			this.orientation = orientation;
			this.direction = direction;
			this.distance = distance;
			this.physicsScene = physicsScene;
			this.queryParameters = QueryParameters.Default;
			this.layerMask = layerMask;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0000A450 File Offset: 0x00008650
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x0000A46D File Offset: 0x0000866D
		[Obsolete("Layer Mask is now a part of QueryParameters struct", false)]
		public int layerMask
		{
			get
			{
				return this.queryParameters.layerMask;
			}
			set
			{
				this.queryParameters.layerMask = value;
			}
		}

		// Token: 0x06000559 RID: 1369
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleBoxcastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits, out JobHandle ret);

		// Token: 0x0400013A RID: 314
		public QueryParameters queryParameters;
	}
}
