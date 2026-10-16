using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000048 RID: 72
	[NativeHeader("Modules/Physics/BatchCommands/SpherecastCommand.h")]
	[NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
	public struct SpherecastCommand
	{
		// Token: 0x0600051A RID: 1306 RVA: 0x00009CAC File Offset: 0x00007EAC
		public SpherecastCommand(Vector3 origin, float radius, Vector3 direction, QueryParameters queryParameters, float distance = 3.4028235E+38f)
		{
			this.origin = origin;
			this.direction = direction;
			this.radius = radius;
			this.distance = distance;
			this.physicsScene = Physics.defaultPhysicsScene;
			this.queryParameters = queryParameters;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00009CE4 File Offset: 0x00007EE4
		public SpherecastCommand(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, QueryParameters queryParameters, float distance = 3.4028235E+38f)
		{
			this.origin = origin;
			this.direction = direction;
			this.radius = radius;
			this.distance = distance;
			this.physicsScene = physicsScene;
			this.queryParameters = queryParameters;
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00009D19 File Offset: 0x00007F19
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x00009D21 File Offset: 0x00007F21
		public Vector3 origin { readonly get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x00009D2A File Offset: 0x00007F2A
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x00009D32 File Offset: 0x00007F32
		public float radius { readonly get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00009D3B File Offset: 0x00007F3B
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x00009D43 File Offset: 0x00007F43
		public Vector3 direction { readonly get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00009D4C File Offset: 0x00007F4C
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x00009D54 File Offset: 0x00007F54
		public float distance { readonly get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00009D5D File Offset: 0x00007F5D
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x00009D65 File Offset: 0x00007F65
		public PhysicsScene physicsScene { readonly get; set; }

		// Token: 0x06000526 RID: 1318 RVA: 0x00009D70 File Offset: 0x00007F70
		public static JobHandle ScheduleBatch(NativeArray<SpherecastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, int maxHits, JobHandle dependsOn = default(JobHandle))
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
					BatchQueryJob<SpherecastCommand, RaycastHit> batchQueryJob = new BatchQueryJob<SpherecastCommand, RaycastHit>(commands, results);
					JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<SpherecastCommand, RaycastHit>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<SpherecastCommand, RaycastHit>>.Initialize(), dependsOn, ScheduleMode.Batched);
					jobHandle = SpherecastCommand.ScheduleSpherecastBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<SpherecastCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<RaycastHit>(results), results.Length, minCommandsPerJob, maxHits);
				}
			}
			return jobHandle;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00009E18 File Offset: 0x00008018
		public static JobHandle ScheduleBatch(NativeArray<SpherecastCommand> commands, NativeArray<RaycastHit> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
		{
			return SpherecastCommand.ScheduleBatch(commands, results, minCommandsPerJob, 1, dependsOn);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00009E34 File Offset: 0x00008034
		[FreeFunction("ScheduleSpherecastCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleSpherecastBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits)
		{
			JobHandle jobHandle;
			SpherecastCommand.ScheduleSpherecastBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, maxHits, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00009E54 File Offset: 0x00008054
		[Obsolete("This struct signature is no longer supported. Use struct with a QueryParameters instead", false)]
		public SpherecastCommand(Vector3 origin, float radius, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5)
		{
			this.origin = origin;
			this.direction = direction;
			this.radius = radius;
			this.distance = distance;
			this.physicsScene = Physics.defaultPhysicsScene;
			this.queryParameters = QueryParameters.Default;
			this.layerMask = layerMask;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00009EA4 File Offset: 0x000080A4
		[Obsolete("This struct signature is no longer supported. Use struct with a QueryParameters instead", false)]
		public SpherecastCommand(PhysicsScene physicsScene, Vector3 origin, float radius, Vector3 direction, float distance = 3.4028235E+38f, int layerMask = -5)
		{
			this.origin = origin;
			this.direction = direction;
			this.radius = radius;
			this.distance = distance;
			this.physicsScene = physicsScene;
			this.queryParameters = QueryParameters.Default;
			this.layerMask = layerMask;
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00009EF0 File Offset: 0x000080F0
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x00009F0D File Offset: 0x0000810D
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

		// Token: 0x0600052D RID: 1325
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleSpherecastBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, int maxHits, out JobHandle ret);

		// Token: 0x0400012C RID: 300
		public QueryParameters queryParameters;
	}
}
