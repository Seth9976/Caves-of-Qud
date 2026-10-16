using System;
using System.Collections;

namespace System.Web.UI.WebControls.Adapters
{
	/// <summary>Customizes the behavior of a <see cref="T:System.Web.UI.WebControls.DataBoundControl" /> object with which the adapter is associated for specific browser requests.</summary>
	// Token: 0x02000456 RID: 1110
	public class DataBoundControlAdapter : WebControlAdapter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Adapters.DataBoundControlAdapter" /> class.</summary>
		// Token: 0x0600337D RID: 13181 RVA: 0x00087C93 File Offset: 0x00085E93
		public DataBoundControlAdapter()
		{
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x00087C9B File Offset: 0x00085E9B
		internal DataBoundControlAdapter(DataBoundControl c)
			: base(c)
		{
		}

		/// <summary>Binds the data in the data source of the associated <see cref="T:System.Web.UI.WebControls.DataBoundControl" /> object to the control adapter.</summary>
		/// <param name="data">An <see cref="T:System.Collections.IEnumerable" /> of <see cref="T:System.Object" /> to be bound to the derived <see cref="T:System.Web.UI.WebControls.DataBoundControl" />.</param>
		// Token: 0x0600337F RID: 13183 RVA: 0x00087CA4 File Offset: 0x00085EA4
		protected internal virtual void PerformDataBinding(IEnumerable data)
		{
			this.Control.PerformDataBinding(data);
		}

		/// <summary>Retrieves a strongly-typed reference to the <see cref="T:System.Web.UI.WebControls.DataBoundControl" /> object associated with this control adapter.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.DataBoundControl" /> to which this <see cref="T:System.Web.UI.WebControls.Adapters.DataBoundControlAdapter" /> is attached.</returns>
		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x06003380 RID: 13184 RVA: 0x00087CB2 File Offset: 0x00085EB2
		protected new DataBoundControl Control
		{
			get
			{
				return (DataBoundControl)this.control;
			}
		}
	}
}
