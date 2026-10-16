using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200004B RID: 75
	[NativeHeader("Runtime/Jobs/ScriptBindings/JobsBindingsTypes.h")]
	[NativeHeader("Modules/Physics/BatchCommands/ClosestPointCommand.h")]
	public struct ClosestPointCommand
	{
		// Token: 0x0600055A RID: 1370 RVA: 0x0000A47C File Offset: 0x0000867C
		public ClosestPointCommand(Vector3 point, int colliderInstanceID, Vector3 position, Quaternion rotation, Vector3 scale)
		{
			this.point = point;
			this.colliderInstanceID = colliderInstanceID;
			this.position = position;
			this.rotation = rotation;
			this.scale = scale;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0000A4A9 File Offset: 0x000086A9
		public ClosestPointCommand(Vector3 point, Collider collider, Vector3 position, Quaternion rotation, Vector3 scale)
		{
			this.point = point;
			this.colliderInstanceID = collider.GetInstanceID();
			this.position = position;
			this.rotation = rotation;
			this.scale = scale;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0000A4DB File Offset: 0x000086DB
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x0000A4E3 File Offset: 0x000086E3
		public Vector3 point { readonly get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x0000A4EC File Offset: 0x000086EC
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x0000A4F4 File Offset: 0x000086F4
		public int colliderInstanceID { readonly get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0000A4FD File Offset: 0x000086FD
		// (set) Token: 0x06000561 RID: 1377 RVA: 0x0000A505 File Offset: 0x00008705
		public Vector3 position { readonly get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x0000A50E File Offset: 0x0000870E
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x0000A516 File Offset: 0x00008716
		public Quaternion rotation { readonly get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x0000A51F File Offset: 0x0000871F
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x0000A527 File Offset: 0x00008727
		public Vector3 scale { readonly get; set; }

		// Token: 0x06000566 RID: 1382 RVA: 0x0000A530 File Offset: 0x00008730
		public static JobHandle ScheduleBatch(NativeArray<ClosestPointCommand> commands, NativeArray<Vector3> results, int minCommandsPerJob, JobHandle dependsOn = default(JobHandle))
		{
			BatchQueryJob<ClosestPointCommand, Vector3> batchQueryJob = new BatchQueryJob<ClosestPointCommand, Vector3>(commands, results);
			JobsUtility.JobScheduleParameters jobScheduleParameters = new JobsUtility.JobScheduleParameters(UnsafeUtility.AddressOf<BatchQueryJob<ClosestPointCommand, Vector3>>(ref batchQueryJob), BatchQueryJobStruct<BatchQueryJob<ClosestPointCommand, Vector3>>.Initialize(), dependsOn, ScheduleMode.Batched);
			return ClosestPointCommand.ScheduleClosestPointCommandBatch(ref jobScheduleParameters, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<ClosestPointCommand>(commands), commands.Length, NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<Vector3>(results), results.Length, minCommandsPerJob);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0000A584 File Offset: 0x00008784
		[FreeFunction("ScheduleClosestPointCommandBatch", ThrowsException = true)]
		private unsafe static JobHandle ScheduleClosestPointCommandBatch(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob)
		{
			JobHandle jobHandle;
			ClosestPointCommand.ScheduleClosestPointCommandBatch_Injected(ref parameters, commands, commandLen, result, resultLen, minCommandsPerJob, out jobHandle);
			return jobHandle;
		}

		// Token: 0x06000568 RID: 1384
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ScheduleClosestPointCommandBatch_Injected(ref JobsUtility.JobScheduleParameters parameters, void* commands, int commandLen, void* result, int resultLen, int minCommandsPerJob, out JobHandle ret);
	}
}
