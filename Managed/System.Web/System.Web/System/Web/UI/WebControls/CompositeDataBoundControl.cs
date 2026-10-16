using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the base class for a tabular data-bound control that is composed of other server controls.</summary>
	// Token: 0x02000352 RID: 850
	public abstract class CompositeDataBoundControl : DataBoundControl, INamingContainer
	{
		/// <summary>Gets a collection of the child controls within the composite data-bound control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that represents the child controls within the composite data-bound control.</returns>
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06001F7B RID: 8059 RVA: 0x0004505A File Offset: 0x0004325A
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		/// <summary>Creates the control hierarchy that is used to render a composite data-bound control based on the values that are stored in view state.</summary>
		// Token: 0x06001F7C RID: 8060 RVA: 0x0004D97C File Offset: 0x0004BB7C
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			object obj = this.ViewState["_!ItemCount"];
			if (obj != null)
			{
				object[] array = new object[(int)obj];
				this.CreateChildControls(array, false);
				return;
			}
			if (base.RequiresDataBinding)
			{
				this.EnsureDataBound();
			}
		}

		/// <summary>Binds the data from the data source to the composite data-bound control.</summary>
		/// <param name="data">An <see cref="T:System.Collections.IEnumerable" /> that contains the values to bind to the composite data-bound control.</param>
		// Token: 0x06001F7D RID: 8061 RVA: 0x0004D9CC File Offset: 0x0004BBCC
		protected internal override void PerformDataBinding(IEnumerable data)
		{
			base.PerformDataBinding(data);
			this.Controls.Clear();
			this.ViewState["_!ItemCount"] = this.CreateChildControls(data, true);
		}

		/// <summary>When overridden in an abstract class, creates the control hierarchy that is used to render the composite data-bound control based on the values from the specified data source.</summary>
		/// <returns>The number of items created by the <see cref="M:System.Web.UI.WebControls.CompositeDataBoundControl.CreateChildControls(System.Collections.IEnumerable,System.Boolean)" />.</returns>
		/// <param name="dataSource">An <see cref="T:System.Collections.IEnumerable" /> that contains the values to bind to the control.</param>
		/// <param name="dataBinding">true to indicate that the <see cref="M:System.Web.UI.WebControls.CompositeDataBoundControl.CreateChildControls(System.Collections.IEnumerable,System.Boolean)" /> is called during data binding; otherwise, false.</param>
		// Token: 0x06001F7E RID: 8062
		protected abstract int CreateChildControls(IEnumerable dataSource, bool dataBinding);

		/// <summary>Gets or sets the name of the method to call in order to delete data.</summary>
		/// <returns>The name of the method.</returns>
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06001F80 RID: 8064 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal string DeleteMethod
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the name of the method to call in order to insert data.</summary>
		/// <returns>The name of the method.</returns>
		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06001F82 RID: 8066 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal string InsertMethod
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether model binding is in use.</summary>
		/// <returns>true if model binding is in use; otherwise, false.</returns>
		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x0004DA00 File Offset: 0x0004BC00
		protected override bool IsUsingModelBinders
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets the name of the method to call in order to update data.</summary>
		/// <returns>The name of the method.</returns>
		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06001F84 RID: 8068 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06001F85 RID: 8069 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal string UpdateMethod
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
