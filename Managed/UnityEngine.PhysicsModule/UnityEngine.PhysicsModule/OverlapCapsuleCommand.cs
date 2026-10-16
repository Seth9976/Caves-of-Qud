using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200004E RID: 78
	[NativeHeader("Modules/Physics/BatchCommands/OverlapCapsuleCommand.h")]
	public struct OverlapCapsuleCommand
	{
		// Token: 0x06000581 RID: 1409 RVA: 0x0000A84F File Offset: 0x00008A4F
		public OverlapCapsuleCommand(Vector3 point0, Vector3 point1, float radius, QueryParameters queryParameters)
		{
			this.point0 = point0;
			this.point1 = point1;
			this.radius = radius;
			this.queryParameters = queryParameters;
			this.physicsScene = Physics.defaultPhysicsScene;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000A87E File Offset: 0x00008A7E
		public OverlapCapsuleCommand(PhysicsScene physicsScene, Vector3 point0, Vector3 point1, float radius, QueryParameters queryParameters)
		{
			this.physicsScene = physicsScene;
			this.point0 = point0;
			this.point1 = point1;
			this.radius = radius;
			this.queryParameters = queryParameters;
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x0000A8AA File Offset: 0x00008AAA
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x0000A8B2 File Offset: 0x00008AB2
		public Vector3 point0 { readonly get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0000A8BB File Offset: 0x00008ABB
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x0000A8C3 File Offset: 0x00008AC3
		public Vector3 point1 { readonly get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0000A8CC File Offset: 0x00008ACC
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x0000A8D4 File Offset: 0x00008AD4
		public float radius { readonly get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0000A8DD File Offset: 0x00008ADD
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x0000A8E5 File Offset: 0x00008AE5
		public PhysicsScene physicsScene { readonly get; set; }

		// Token: 0x0600058B RID: 1419 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		public static JobHandle ScheduleBatch(NativeArray<OverlapCapsuleCommand> commands, NativeArray<ColliderHit> results, int minCommandsPerJob, int maxHits, JobHandle dependsOn = default(JobHandle))
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
					BatchQueryJob<OverlapCapsuleCommand, ColliderHit> batchQueryJob = new BatchQueryJob<OverlapCapsuleCommand, ColliderHit>(commands, results);
					JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<OverlapCapsuleCommand, ColliderHit>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<OverlapCapsuleCommand, ColliderHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
					jobHandle = OverlapCapsuleCommand.ScheduleOverlapCapsuleBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<OverlapCapsuleCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<ColliderHit>(results), results.Length, minCommandsPerJob, maxHits);
				}
			}
			return jobHandle;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0000A998 File Offset: 0x00008B98
		[FreeFunction("ScheduleOverlapCapsuleCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleOverlapCapsuleBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits)
		{
			JobHandle jobHandle;
			OverlapCapsuleCommand.ScheduleOverlapCapsuleBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, maxHits, out jobHandle);
			return jobHandle;
		}

		// Token: 0x0600058D RID: 1421
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleOverlapCapsuleBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits, out JobHandle ret);

		// Token: 0x0400014D RID: 333
		public QueryParameters queryParameters;
	}
}
