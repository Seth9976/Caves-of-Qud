using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>The data source control used by data-bound controls to perform CRUD (create, read, update, delete) operations when model binding is in use. </summary>
	// Token: 0x020006AC RID: 1708
	public class ModelDataSource : IDataSource, IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ModelDataSource" /> class.</summary>
		/// <param name="dataControl">The data-bound control.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataControl" /> parameter is null.</exception>
		// Token: 0x06004803 RID: 18435 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelDataSource(Control dataControl)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the data-bound control that is using this control as a data source when model binding is in use.</summary>
		/// <returns>The data-bound control.</returns>
		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x06004804 RID: 18436 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public Control DataControl
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x06004805 RID: 18437 RVA: 0x000C7528 File Offset: 0x000C5728
		bool IStateManager.get_IsTrackingViewState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Gets the default (and only) view object for this data source control.</summary>
		/// <returns>The view object.</returns>
		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x06004806 RID: 18438 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual ModelDataSourceView View
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Occurs when model binding is in use and data methods are being called.</summary>
		// Token: 0x14000116 RID: 278
		// (add) Token: 0x06004807 RID: 18439 RVA: 0x0000B3D8 File Offset: 0x000095D8
		// (remove) Token: 0x06004808 RID: 18440 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public event CallingDataMethodsEventHandler CallingDataMethods
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs when the underlying data source has changed. The change may be due to a change in the control's properties, or a change in the data due to an edit action performed by the data source control.</summary>
		// Token: 0x14000117 RID: 279
		// (add) Token: 0x06004809 RID: 18441 RVA: 0x0000B3D8 File Offset: 0x000095D8
		// (remove) Token: 0x0600480A RID: 18442 RVA: 0x0000B3D8 File Offset: 0x000095D8
		event EventHandler IDataSource.DataSourceChanged
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Returns a value that indicates whether the control is tracking view state.</summary>
		/// <returns>true if the control is tracking view state; otherwise, false.</returns>
		// Token: 0x0600480B RID: 18443 RVA: 0x000C7544 File Offset: 0x000C5744
		protected virtual bool IsTrackingViewState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Loads the previously saved view state.</summary>
		/// <param name="savedState">The saved view state.</param>
		// Token: 0x0600480C RID: 18444 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void LoadViewState(object savedState)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Saves the state of the control.</summary>
		/// <returns>Returns the control's current view state; otherwise, returns null, if there is no view state associated with the control.</returns>
		// Token: 0x0600480D RID: 18445 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected virtual object SaveViewState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns the view.</summary>
		/// <returns>The view.</returns>
		/// <param name="viewName">The name of the view.</param>
		// Token: 0x0600480E RID: 18446 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		DataSourceView IDataSource.GetView(string viewName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the view names.</summary>
		/// <returns>The view names.</returns>
		// Token: 0x0600480F RID: 18447 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		ICollection IDataSource.GetViewNames()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Loads the previously saved view state.</summary>
		/// <param name="savedState">The saved view state.</param>
		// Token: 0x06004810 RID: 18448 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void IStateManager.LoadViewState(object savedState)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Saves the state of the control.</summary>
		/// <returns>Returns the control's current view state; otherwise, returns null, if there is no view state associated with the control.</returns>
		// Token: 0x06004811 RID: 18449 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		object IStateManager.SaveViewState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Tracks view-state changes to the control so that they can be stored in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		// Token: 0x06004812 RID: 18450 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void IStateManager.TrackViewState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Tracks view-state changes to the control so that they can be stored in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		// Token: 0x06004813 RID: 18451 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void TrackViewState()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Updates the required properties for one-way data binding.</summary>
		/// <param name="modelTypeName">The name of the model type.</param>
		/// <param name="selectMethod">The name of the select method.</param>
		// Token: 0x06004814 RID: 18452 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void UpdateProperties(string modelTypeName, string selectMethod)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Updates the required properties for two-way data binding.</summary>
		/// <param name="modelTypeName">The name of the model type.</param>
		/// <param name="selectMethod">The name of the select method.</param>
		/// <param name="updateMethod">The name of the update method.</param>
		/// <param name="insertMethod">The name of the insert method.</param>
		/// <param name="deleteMethod">The name of the delete method.</param>
		/// <param name="dataKeyName">The data key name.</param>
		// Token: 0x06004815 RID: 18453 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void UpdateProperties(string modelTypeName, string selectMethod, string updateMethod, string insertMethod, string deleteMethod, string dataKeyName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
