using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000046 RID: 70
	public readonly struct DataSourceContext
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000B247 File Offset: 0x00009447
		public object dataSource { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000B24F File Offset: 0x0000944F
		public PropertyPath dataSourcePath { get; }

		// Token: 0x06000266 RID: 614 RVA: 0x0000B257 File Offset: 0x00009457
		public DataSourceContext(object dataSource, in PropertyPath dataSourcePath)
		{
			this.dataSource = dataSource;
			this.dataSourcePath = dataSourcePath;
		}
	}
}
