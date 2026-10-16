using System;
using System.Web.ModelBinding;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Used to invoke the <see cref="M:System.Web.UI.Page.UpdateModel``1(``0)" /> or <see cref="M:System.Web.UI.Page.TryUpdateModel``1(``0)" /> method when the <see cref="T:System.Web.UI.Page" /> object is not directly accessible.</summary>
	// Token: 0x02000797 RID: 1943
	public class ModelMethodContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ModelMethodContext" /> class.</summary>
		/// <param name="page">The page object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="page" /> parameter is null.</exception>
		// Token: 0x06004E6A RID: 20074 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelMethodContext(Page page)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.ModelMethodContext" /> object that corresponds to the <see cref="T:System.Web.UI.Page" /> object that is in the <see cref="P:System.Web.HttpContext.Current" /> property.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.ModelMethodContext" /> object., or null if the current request is not for a page.</returns>
		// Token: 0x170017E3 RID: 6115
		// (get) Token: 0x06004E6B RID: 20075 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static ModelMethodContext Current
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the model state.</summary>
		/// <returns>The model state.</returns>
		// Token: 0x170017E4 RID: 6116
		// (get) Token: 0x06004E6C RID: 20076 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ModelStateDictionary ModelState
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Updates the specified model instance using values from a value provider.</summary>
		/// <returns>true if model binding is successful; otherwise, false.</returns>
		/// <param name="model">The model.</param>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		// Token: 0x06004E6D RID: 20077 RVA: 0x000C8B08 File Offset: 0x000C6D08
		public virtual bool TryUpdateModel<TModel>(TModel model)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Updates the specified model instance using values from the specified value provider.</summary>
		/// <returns>true if model binding is successful; otherwise, false.</returns>
		/// <param name="model">The model.</param>
		/// <param name="valueProvider">The value provider.</param>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		// Token: 0x06004E6E RID: 20078 RVA: 0x000C8B24 File Offset: 0x000C6D24
		public virtual bool TryUpdateModel<TModel>(TModel model, IValueProvider valueProvider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Updates the specified model instance using values from a value provider.</summary>
		/// <param name="model">The model.</param>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		// Token: 0x06004E6F RID: 20079 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void UpdateModel<TModel>(TModel model)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Updates the specified model instance using values from the specified value provider.</summary>
		/// <param name="model">The model.</param>
		/// <param name="valueProvider">The value provider.</param>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		// Token: 0x06004E70 RID: 20080 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void UpdateModel<TModel>(TModel model, IValueProvider valueProvider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
