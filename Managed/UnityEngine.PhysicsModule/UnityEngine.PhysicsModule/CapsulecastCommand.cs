using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000049 RID: 73
	[NativeHeader("Modules/Physics/BatchCommands/CapsulecastCommand.h")]
	[NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
	public struct CapsulecastCommand
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x00009F1C File Offset: 0x0000811C
		public CapsulecastCommand(Vector3 p1, Vector3 p2, float radius, Vector3 direction, QueryParameters queryParameters, float distance = 3.4028235E+38f)
		{
			this.point1 = p1;
			this.point2 = p2;
			this.direction = direction;
			this.radius = radius;
			this.distance = distance;
			this.physicsScene = Physics.defaultPhysicsScene;
			this.queryParameters = queryParameters;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00009F68 File Offset: 0x00008168
		public CapsulecastCommand(PhysicsScene physicsScene, Vector3 p1, Vector3 p2, float radius, Vector3 direction, QueryParameters queryParameters, float distance = 3.4028235E+38f)
		{
			this.point1 = p1;
			this.point2 = p2;
			this.direction = direction;
			this.radius = radius;
			this.distance = distance;
			this.physicsScene = physicsScene;
			this.queryParameters = queryParameters;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00009FA6 File Offset: 0x000081A6
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x00009FAE File Offset: 0x000081AE
		public Vector3 point1 { readonly get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00009FB7 File Offset: 0x000081B7
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x00009FBF File Offset: 0x000081BF
		public Vector3 point2 { readonly get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00009FC8 File Offset: 0x000081C8
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x00009FD0 File Offset: 0x000081D0
		public float radius { readonly get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00009FD9 File Offset: 0x000081D9
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x00009FE1 File Offset: 0x000081E1
		public Vector3 direction { readonly get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00009FEA File Offset: 0x000081EA
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x00009FF2 File Offset: 0x000081F2
		public float distance { readonly get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x00009FFB File Offset: 0x000081FB
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x0000A003 File Offset: 0x00008203
		public PhysicsScene physicsScene { readonly get; set; }

		// Token: 0x0600053C RID: 1340 RVA: 0x0000A00C File Offset: 0x0000820C
		public static JobHandle ScheduleBatch(NativeArray<CapsulecastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, int maxHits, JobHandle dependsOn = default(JobHandle))
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
					BatchQueryJob<CapsulecastCommand, RaycastHit> batchQueryJob = new BatchQueryJob<CapsulecastCommand, RaycastHit>(commands, results);
					JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<CapsulecastCommand, RaycastHit>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<CapsulecastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
					jobHandle = CapsulecastCommand.ScheduleCapsulecastBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<CapsulecastCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastHit>(results), results.Length, minCommandsPerJob, maxHits);
				}
			}
			return jobHandle;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000A0B4 File Offset: 0x000082B4
		public static JobHandle ScheduleBatch(NativeArray<CapsulecastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
		{
			return CapsulecastCommand.ScheduleBatch(commands, results, minCommandsPerJob, 1, dependsOn);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000A0D0 File Offset: 0x000082D0
		[FreeFunction("ScheduleCapsulecastCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleCapsulecastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits)
		{
			JobHandle jobHandle;
			CapsulecastCommand.ScheduleCapsulecastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, maxHits, out jobHandle);
			return jobHandle;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000A0F0 File Offset: 0x000082F0
		[Obsolete("This struct signature is no longer supported. Use struct with a QueryParameters instead", false)]
		public CapsulecastCommand(Vector3 p1, Vector3 p2, float radius, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5)
		{
			this.point1 = p1;
			this.point2 = p2;
			this.direction = direction;
			this.radius = radius;
			this.distance = distance;
			this.physicsScene = Physics.defaultPhysicsScene;
			this.queryParameters = QueryParameters.Default;
			this.layerMask = layerMask;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000A148 File Offset: 0x00008348
		[Obsolete("This struct signature is no longer supported. Use struct with a QueryParameters instead", false)]
		public CapsulecastCommand(PhysicsScene physicsScene, Vector3 p1, Vector3 p2, float radius, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5)
		{
			this.point1 = p1;
			this.point2 = p2;
			this.direction = direction;
			this.radius = radius;
			this.distance = distance;
			this.physicsScene = physicsScene;
			this.queryParameters = QueryParameters.Default;
			this.layerMask = layerMask;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000A1A0 File Offset: 0x000083A0
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x0000A1BD File Offset: 0x000083BD
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

		// Token: 0x06000543 RID: 1347
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleCapsulecastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits, out JobHandle ret);

		// Token: 0x04000133 RID: 307
		public QueryParameters queryParameters;
	}
}
