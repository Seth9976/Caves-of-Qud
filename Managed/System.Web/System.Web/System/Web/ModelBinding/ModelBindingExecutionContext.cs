using System;
using System.Collections.Generic;

namespace System.Web.ModelBinding
{
	/// <summary>Encapsulates all of the information that is external to the model binding system that the model binding system needs. </summary>
	// Token: 0x0200051C RID: 1308
	public class ModelBindingExecutionContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelBindingExecutionContext" /> class, using the HTTP context and the model state.</summary>
		/// <param name="httpContext">The HTTP context.</param>
		/// <param name="modelState">The model state.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="httpContext" /> or <paramref name="modelState" /> parameter is null.</exception>
		// Token: 0x060039AF RID: 14767 RVA: 0x0009A5A5 File Offset: 0x000987A5
		public ModelBindingExecutionContext(HttpContextBase httpContext, ModelStateDictionary modelState)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (modelState == null)
			{
				throw new ArgumentNullException("modelState");
			}
			this._httpContext = httpContext;
			this._modelState = modelState;
		}

		/// <summary>Gets the HTTP context.</summary>
		/// <returns>The HTTP context.</returns>
		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x060039B0 RID: 14768 RVA: 0x0009A5E2 File Offset: 0x000987E2
		public virtual HttpContextBase HttpContext
		{
			get
			{
				return this._httpContext;
			}
		}

		/// <summary>Gets the model state.</summary>
		/// <returns>The model state.</returns>
		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x060039B1 RID: 14769 RVA: 0x0009A5EA File Offset: 0x000987EA
		public virtual ModelStateDictionary ModelState
		{
			get
			{
				return this._modelState;
			}
		}

		/// <summary>Stores an object that contains values that are used for model binding and that will be accessed by using the <see cref="M:System.Web.ModelBinding.ModelBindingExecutionContext.GetService``1" /> method.</summary>
		/// <param name="service">The object that contains values to store.</param>
		/// <typeparam name="TService">The type of the object that contains values to store.</typeparam>
		// Token: 0x060039B2 RID: 14770 RVA: 0x0009A5F2 File Offset: 0x000987F2
		public virtual void PublishService<TService>(TService service)
		{
			this._services[typeof(TService)] = service;
		}

		/// <summary>Gets an object that contains values that are used for model binding and that have been stored by using the <see cref="M:System.Web.ModelBinding.ModelBindingExecutionContext.PublishService``1(``0)" /> method. </summary>
		/// <returns>The object that contains values that are used for model binding.</returns>
		/// <typeparam name="TService">The type of the object that contains values that are used for model binding.</typeparam>
		// Token: 0x060039B3 RID: 14771 RVA: 0x0009A60F File Offset: 0x0009880F
		public virtual TService GetService<TService>()
		{
			return (TService)((object)this._services[typeof(TService)]);
		}

		/// <summary>Gets an object that contains values that are used for model binding and that have been stored by using the <see cref="M:System.Web.ModelBinding.ModelBindingExecutionContext.PublishService``1(``0)" /> method.</summary>
		/// <returns>The object that contains values that are used for model binding, or the default value of <paramref name="TService" /> if the requested object is not found.</returns>
		/// <typeparam name="TService">The type of the object that contains values that are used for model binding.</typeparam>
		// Token: 0x060039B4 RID: 14772 RVA: 0x0009A62C File Offset: 0x0009882C
		public virtual TService TryGetService<TService>()
		{
			if (this._services.ContainsKey(typeof(TService)))
			{
				return (TService)((object)this._services[typeof(TService)]);
			}
			return default(TService);
		}

		// Token: 0x04001F49 RID: 8009
		private Dictionary<Type, object> _services = new Dictionary<Type, object>();

		// Token: 0x04001F4A RID: 8010
		private HttpContextBase _httpContext;

		// Token: 0x04001F4B RID: 8011
		private ModelStateDictionary _modelState;
	}
}
