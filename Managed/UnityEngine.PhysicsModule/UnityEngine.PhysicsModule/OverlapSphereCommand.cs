using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200004C RID: 76
	[NativeHeader("Modules/Physics/BatchCommands/OverlapSphereCommand.h")]
	public struct OverlapSphereCommand
	{
		// Token: 0x06000569 RID: 1385 RVA: 0x0000A5A1 File Offset: 0x000087A1
		public OverlapSphereCommand(Vector3 point, float radius, QueryParameters queryParameters)
		{
			this.point = point;
			this.radius = radius;
			this.queryParameters = queryParameters;
			this.physicsScene = Physics.defaultPhysicsScene;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000A5C7 File Offset: 0x000087C7
		public OverlapSphereCommand(PhysicsScene physicsScene, Vector3 point, float radius, QueryParameters queryParameters)
		{
			this.physicsScene = physicsScene;
			this.point = point;
			this.radius = radius;
			this.queryParameters = queryParameters;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0000A5EA File Offset: 0x000087EA
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0000A5F2 File Offset: 0x000087F2
		public Vector3 point { readonly get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0000A5FB File Offset: 0x000087FB
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x0000A603 File Offset: 0x00008803
		public float radius { readonly get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0000A60C File Offset: 0x0000880C
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x0000A614 File Offset: 0x00008814
		public PhysicsScene physicsScene { readonly get; set; }

		// Token: 0x06000571 RID: 1393 RVA: 0x0000A620 File Offset: 0x00008820
		public static JobHandle ScheduleBatch(NativeArray<OverlapSphereCommand> commands, NativeArray<ColliderHit> results, int minCommandsPerJob, int maxHits, JobHandle dependsOn = default(JobHandle))
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
					BatchQueryJob<OverlapSphereCommand, ColliderHit> batchQueryJob = new BatchQueryJob<OverlapSphereCommand, ColliderHit>(commands, results);
					JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<OverlapSphereCommand, ColliderHit>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<OverlapSphereCommand, ColliderHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
					jobHandle = OverlapSphereCommand.ScheduleOverlapSphereBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<OverlapSphereCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<ColliderHit>(results), results.Length, minCommandsPerJob, maxHits);
				}
			}
			return jobHandle;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000A6C8 File Offset: 0x000088C8
		[FreeFunction("ScheduleOverlapSphereCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleOverlapSphereBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits)
		{
			JobHandle jobHandle;
			OverlapSphereCommand.ScheduleOverlapSphereBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, maxHits, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000573 RID: 1395
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleOverlapSphereBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits, out JobHandle ret);

		// Token: 0x04000143 RID: 323
		public QueryParameters queryParameters;
	}
}
