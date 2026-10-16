using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000048 RID: 72
	public interface IDataSourceProvider
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600026C RID: 620
		object dataSource { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600026D RID: 621
		PropertyPath dataSourcePath { get; }
	}
}
