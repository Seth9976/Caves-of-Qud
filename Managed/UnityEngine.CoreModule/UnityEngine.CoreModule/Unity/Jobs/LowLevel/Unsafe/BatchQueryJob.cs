using System;
using Unity.Collections;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000051 RID: 81
	public struct BatchQueryJob<CommandT, ResultT> where CommandT : struct where ResultT : struct
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00003129 File Offset: 0x00001329
		public BatchQueryJob(NativeArray<CommandT> commands, NativeArray<ResultT> results)
		{
			this.commands = commands;
			this.results = results;
		}

		// Token: 0x04000108 RID: 264
		[ReadOnly]
		internal NativeArray<CommandT> commands;

		// Token: 0x04000109 RID: 265
		internal NativeArray<ResultT> results;
	}
}
