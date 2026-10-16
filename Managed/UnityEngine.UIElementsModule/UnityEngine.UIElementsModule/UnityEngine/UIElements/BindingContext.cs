using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000020 RID: 32
	public readonly struct BindingContext
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003650 File Offset: 0x00001850
		public VisualElement targetElement
		{
			get
			{
				return this.m_TargetElement;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003658 File Offset: 0x00001858
		public BindingId bindingId
		{
			get
			{
				return this.m_BindingId;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003660 File Offset: 0x00001860
		public PropertyPath dataSourcePath
		{
			get
			{
				return this.m_DataSourcePath;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003668 File Offset: 0x00001868
		public object dataSource
		{
			get
			{
				return this.m_DataSource;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003670 File Offset: 0x00001870
		internal BindingContext(VisualElement targetElement, in BindingId bindingId, in PropertyPath resolvedDataSourcePath, object resolvedDataSource)
		{
			this.m_TargetElement = targetElement;
			this.m_BindingId = bindingId;
			this.m_DataSourcePath = resolvedDataSourcePath;
			this.m_DataSource = resolvedDataSource;
		}

		// Token: 0x04000041 RID: 65
		private readonly VisualElement m_TargetElement;

		// Token: 0x04000042 RID: 66
		private readonly BindingId m_BindingId;

		// Token: 0x04000043 RID: 67
		private readonly PropertyPath m_DataSourcePath;

		// Token: 0x04000044 RID: 68
		private readonly object m_DataSource;
	}
}
