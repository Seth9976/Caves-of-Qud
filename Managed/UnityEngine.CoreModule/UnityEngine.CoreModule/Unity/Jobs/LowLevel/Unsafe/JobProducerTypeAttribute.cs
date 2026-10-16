using System;

namespace Unity.Jobs.LowLevel.Unsafe
{
	// Token: 0x02000054 RID: 84
	[AttributeUsage(AttributeTargets.Interface)]
	public sealed class JobProducerTypeAttribute : Attribute
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00003199 File Offset: 0x00001399
		public Type ProducerType { get; }

		// Token: 0x06000114 RID: 276 RVA: 0x000031A1 File Offset: 0x000013A1
		public JobProducerTypeAttribute(Type producerType)
		{
			this.ProducerType = producerType;
		}
	}
}
