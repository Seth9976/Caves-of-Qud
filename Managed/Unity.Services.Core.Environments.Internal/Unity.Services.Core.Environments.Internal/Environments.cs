using System;
using Unity.Services.Core.Internal;

namespace Unity.Services.Core.Environments.Internal
{
	// Token: 0x02000003 RID: 3
	internal class Environments : IEnvironments, IServiceComponent
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020B8 File Offset: 0x000002B8
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020C0 File Offset: 0x000002C0
		public string Current
		{
			get
			{
				return this.m_Current;
			}
			internal set
			{
				this.m_Current = value;
			}
		}

		// Token: 0x04000001 RID: 1
		private string m_Current;
	}
}
