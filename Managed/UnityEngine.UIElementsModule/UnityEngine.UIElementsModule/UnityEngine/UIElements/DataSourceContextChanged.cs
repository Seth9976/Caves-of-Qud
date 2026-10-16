using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000047 RID: 71
	public readonly struct DataSourceContextChanged
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000B26D File Offset: 0x0000946D
		public VisualElement targetElement
		{
			get
			{
				return this.m_TargetElement;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000B275 File Offset: 0x00009475
		public BindingId bindingId
		{
			get
			{
				return this.m_BindingId;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000B27D File Offset: 0x0000947D
		public DataSourceContext previousContext
		{
			get
			{
				return this.m_PreviousContext;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000B285 File Offset: 0x00009485
		public DataSourceContext newContext
		{
			get
			{
				return this.m_NewContext;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000B28D File Offset: 0x0000948D
		internal DataSourceContextChanged(VisualElement element, in BindingId bindingId, in DataSourceContext previousContext, in DataSourceContext newContext)
		{
			this.m_TargetElement = element;
			this.m_BindingId = bindingId;
			this.m_PreviousContext = previousContext;
			this.m_NewContext = newContext;
		}

		// Token: 0x04000168 RID: 360
		private readonly VisualElement m_TargetElement;

		// Token: 0x04000169 RID: 361
		private readonly BindingId m_BindingId;

		// Token: 0x0400016A RID: 362
		private readonly DataSourceContext m_PreviousContext;

		// Token: 0x0400016B RID: 363
		private readonly DataSourceContext m_NewContext;
	}
}
