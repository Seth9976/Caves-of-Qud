using System;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200004A RID: 74
	internal class MissingComponent : IServiceComponent
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000037F1 File Offset: 0x000019F1
		public Type IntendedType { get; }

		// Token: 0x0600013D RID: 317 RVA: 0x000037F9 File Offset: 0x000019F9
		internal MissingComponent(Type intendedType)
		{
			this.IntendedType = intendedType;
		}
	}
}
