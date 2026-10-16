using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200004D RID: 77
	[NativeHeader("Modules/Physics/BatchCommands/OverlapBoxCommand.h")]
	public struct OverlapBoxCommand
	{
		// Token: 0x06000574 RID: 1396 RVA: 0x0000A6E7 File Offset: 0x000088E7
		public OverlapBoxCommand(Vector3 center, Vector3 halfExtents, Quaternion orientation, QueryParameters queryParameters)
		{
			this.center = center;
			this.halfExtents = halfExtents;
			this.orientation = orientation;
			this.queryParameters = queryParameters;
			this.physicsScene = Physics.defaultPhysicsScene;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0000A716 File Offset: 0x00008916
		public OverlapBoxCommand(PhysicsScene physicsScene, Vector3 center, Vector3 halfExtents, Quaternion orientation, QueryParameters queryParameters)
		{
			this.physicsScene = physicsScene;
			this.center = center;
			this.halfExtents = halfExtents;
			this.orientation = orientation;
			this.queryParameters = queryParameters;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0000A742 File Offset: 0x00008942
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x0000A74A File Offset: 0x0000894A
		public Vector3 center { readonly get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0000A753 File Offset: 0x00008953
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x0000A75B File Offset: 0x0000895B
		public Vector3 halfExtents { readonly get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0000A764 File Offset: 0x00008964
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x0000A76C File Offset: 0x0000896C
		public Quaternion orientation { readonly get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0000A775 File Offset: 0x00008975
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x0000A77D File Offset: 0x0000897D
		public PhysicsScene physicsScene { readonly get; set; }

		// Token: 0x0600057E RID: 1406 RVA: 0x0000A788 File Offset: 0x00008988
		public static JobHandle ScheduleBatch(NativeArray<OverlapBoxCommand> commands, NativeArray<ColliderHit> results, int minCommandsPerJob, int maxHits, JobHandle dependsOn = default(JobHandle))
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
					BatchQueryJob<OverlapBoxCommand, ColliderHit> batchQueryJob = new BatchQueryJob<OverlapBoxCommand, ColliderHit>(commands, results);
					JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<OverlapBoxCommand, ColliderHit>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<OverlapBoxCommand, ColliderHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
					jobHandle = OverlapBoxCommand.ScheduleOverlapBoxBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<OverlapBoxCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<ColliderHit>(results), results.Length, minCommandsPerJob, maxHits);
				}
			}
			return jobHandle;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000A830 File Offset: 0x00008A30
		[FreeFunction("ScheduleOverlapBoxCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleOverlapBoxBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits)
		{
			JobHandle jobHandle;
			OverlapBoxCommand.ScheduleOverlapBoxBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, maxHits, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000580 RID: 1408
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleOverlapBoxBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits, out JobHandle ret);

		// Token: 0x04000148 RID: 328
		public QueryParameters queryParameters;
	}
}
