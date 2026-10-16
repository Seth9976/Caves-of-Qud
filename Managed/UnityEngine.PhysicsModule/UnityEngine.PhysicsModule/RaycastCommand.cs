using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000047 RID: 71
	[NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
	[NativeHeader("Modules/Physics/BatchCommands/RaycastCommand.h")]
	public struct RaycastCommand
	{
		// Token: 0x06000506 RID: 1286 RVA: 0x00009A75 File Offset: 0x00007C75
		public RaycastCommand(Vector3 from, Vector3 direction, QueryParameters queryParameters, float distance = 3.4028235E+38f)
		{
			this.from = from;
			this.direction = direction;
			this.physicsScene = Physics.defaultPhysicsScene;
			this.distance = distance;
			this.queryParameters = queryParameters;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00009AA4 File Offset: 0x00007CA4
		public RaycastCommand(PhysicsScene physicsScene, Vector3 from, Vector3 direction, QueryParameters queryParameters, float distance = 3.4028235E+38f)
		{
			this.from = from;
			this.direction = direction;
			this.physicsScene = physicsScene;
			this.distance = distance;
			this.queryParameters = queryParameters;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00009AD0 File Offset: 0x00007CD0
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x00009AD8 File Offset: 0x00007CD8
		public Vector3 from { readonly get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00009AE1 File Offset: 0x00007CE1
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x00009AE9 File Offset: 0x00007CE9
		public Vector3 direction { readonly get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00009AF2 File Offset: 0x00007CF2
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x00009AFA File Offset: 0x00007CFA
		public PhysicsScene physicsScene { readonly get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00009B03 File Offset: 0x00007D03
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x00009B0B File Offset: 0x00007D0B
		public float distance { readonly get; set; }

		// Token: 0x06000510 RID: 1296 RVA: 0x00009B14 File Offset: 0x00007D14
		public static JobHandle ScheduleBatch(NativeArray<RaycastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, int maxHits, JobHandle dependsOn = default(JobHandle))
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
					BatchQueryJob<RaycastCommand, RaycastHit> batchQueryJob = new BatchQueryJob<RaycastCommand, RaycastHit>(commands, results);
					JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<RaycastCommand, RaycastHit>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<RaycastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
					jobHandle = RaycastCommand.ScheduleRaycastBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastHit>(results), results.Length, minCommandsPerJob, maxHits);
				}
			}
			return jobHandle;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00009BBC File Offset: 0x00007DBC
		public static JobHandle ScheduleBatch(NativeArray<RaycastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
		{
			return RaycastCommand.ScheduleBatch(commands, results, minCommandsPerJob, 1, dependsOn);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00009BD8 File Offset: 0x00007DD8
		[FreeFunction("ScheduleRaycastCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleRaycastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits)
		{
			JobHandle jobHandle;
			RaycastCommand.ScheduleRaycastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, maxHits, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00009BF7 File Offset: 0x00007DF7
		[Obsolete("This struct signature is no longer supported. Use struct with a QueryParameters instead", false)]
		public RaycastCommand(Vector3 from, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5, int maxHits = 1)
		{
			this.from = from;
			this.direction = direction;
			this.physicsScene = Physics.defaultPhysicsScene;
			this.queryParameters = QueryParameters.Default;
			this.distance = distance;
			this.layerMask = layerMask;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00009C32 File Offset: 0x00007E32
		[Obsolete("This struct signature is no longer supported. Use struct with a QueryParameters instead", false)]
		public RaycastCommand(PhysicsScene physicsScene, Vector3 from, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5, int maxHits = 1)
		{
			this.from = from;
			this.direction = direction;
			this.physicsScene = physicsScene;
			this.queryParameters = QueryParameters.Default;
			this.distance = distance;
			this.layerMask = layerMask;
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00009C6C File Offset: 0x00007E6C
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x0000274B File Offset: 0x0000094B
		[Obsolete("maxHits property was moved to be a part of RaycastCommand.ScheduleBatch.", false)]
		public int maxHits
		{
			get
			{
				return 1;
			}
			set
			{
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00009C80 File Offset: 0x00007E80
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x00009C9D File Offset: 0x00007E9D
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

		// Token: 0x06000519 RID: 1305
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleRaycastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits, out JobHandle ret);

		// Token: 0x04000126 RID: 294
		public QueryParameters queryParameters;
	}
}
