using System;

namespace System.Web.UI.WebControls.Adapters
{
	/// <summary>Customizes the behavior of a <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" /> object with which this control adapter is associated, for specific browser requests.</summary>
	// Token: 0x02000458 RID: 1112
	public class HierarchicalDataBoundControlAdapter : WebControlAdapter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Adapters.HierarchicalDataBoundControlAdapter" /> class.</summary>
		// Token: 0x06003384 RID: 13188 RVA: 0x00087C93 File Offset: 0x00085E93
		public HierarchicalDataBoundControlAdapter()
		{
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x00087C9B File Offset: 0x00085E9B
		internal HierarchicalDataBoundControlAdapter(HierarchicalDataBoundControl c)
			: base(c)
		{
		}

		/// <summary>Binds the data in the data source of the associated hierarchical data-bound control to the adapter.</summary>
		// Token: 0x06003386 RID: 13190 RVA: 0x00087CD6 File Offset: 0x00085ED6
		protected internal virtual void PerformDataBinding()
		{
			this.Control.PerformDataBinding();
		}

		/// <summary>Retrieves a strongly typed reference to the <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" /> control associated with this <see cref="T:System.Web.UI.WebControls.Adapters.HierarchicalDataBoundControlAdapter" /> object.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.HierarchicalDataBoundControl" /> associated with the current instance of <see cref="T:System.Web.UI.WebControls.Adapters.HierarchicalDataBoundControlAdapter" />.</returns>
		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x06003387 RID: 13191 RVA: 0x00087CE3 File Offset: 0x00085EE3
		protected new HierarchicalDataBoundControl Control
		{
			get
			{
				return (HierarchicalDataBoundControl)this.control;
			}
		}
	}
}
