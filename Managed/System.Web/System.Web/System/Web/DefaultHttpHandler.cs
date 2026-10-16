using System;
using System.Collections.Specialized;
using System.Threading;

namespace System.Web
{
	/// <summary>Represents the properties and methods of a default HTTP handler.</summary>
	// Token: 0x02000068 RID: 104
	public class DefaultHttpHandler : IHttpAsyncHandler, IHttpHandler
	{
		/// <summary>Gets the context that is associated with the current <see cref="T:System.Web.DefaultHttpHandler" /> object.</summary>
		/// <returns>An <see cref="T:System.Web.HttpContext" /> object that contains the current context.</returns>
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00008A3C File Offset: 0x00006C3C
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x00008A44 File Offset: 0x00006C44
		private protected HttpContext Context { protected get; private set; }

		/// <summary>Gets a Boolean value indicating that another request can use the current instance of the <see cref="T:System.Web.DefaultHttpHandler" /> class.</summary>
		/// <returns>true if the <see cref="T:System.Web.DefaultHttpHandler" /> is reusable; otherwise, false.</returns>
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool IsReusable
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a collection of request headers and request values to transfer along with the request.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> containing request headers and values.</returns>
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00008A50 File Offset: 0x00006C50
		protected NameValueCollection ExecuteUrlHeaders
		{
			get
			{
				HttpContext context = this.Context;
				HttpRequest httpRequest = ((context != null) ? context.Request : null);
				if (httpRequest != null && this.executeUrlHeaders != null)
				{
					this.executeUrlHeaders = new NameValueCollection(httpRequest.Headers);
				}
				return this.executeUrlHeaders;
			}
		}

		/// <summary>Initiates an asynchronous call to the HTTP handler.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that contains information about the status of the process.</returns>
		/// <param name="context">An object that provides references to intrinsic server objects that are used to service HTTP requests.</param>
		/// <param name="callback">The method to call when the asynchronous method call is complete. If <paramref name="callback" /> is null, the delegate is not called.</param>
		/// <param name="state">Any state data that is needed to process the request.</param>
		/// <exception cref="T:System.Web.HttpException">The preconditions for processing a request fail and either the requested file has the suffix .asp or the request was sent through POST.</exception>
		// Token: 0x06000439 RID: 1081 RVA: 0x00008A94 File Offset: 0x00006C94
		public virtual IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback callback, object state)
		{
			this.Context = context;
			HttpRequest httpRequest = ((context != null) ? context.Request : null);
			string text = ((httpRequest != null) ? httpRequest.FilePath : null);
			if (!string.IsNullOrEmpty(text) && string.Compare(".asp", VirtualPathUtility.GetExtension(text), StringComparison.OrdinalIgnoreCase) == 0)
			{
				throw new HttpException(string.Format("Access to file '{0}' is forbidden.", text));
			}
			if (httpRequest != null && string.Compare("POST", httpRequest.HttpMethod, StringComparison.OrdinalIgnoreCase) == 0)
			{
				throw new HttpException(string.Format("Method '{0}' is not allowed when accessing file '{1}'", httpRequest.HttpMethod, text));
			}
			new StaticFileHandler().ProcessRequest(context);
			return new DefaultHttpHandler.DefaultHandlerAsyncResult(callback, state);
		}

		/// <summary>Provides an end method for an asynchronous process.</summary>
		/// <param name="result">An object that contains information about the status of the process.</param>
		// Token: 0x0600043A RID: 1082 RVA: 0x00003946 File Offset: 0x00001B46
		public virtual void EndProcessRequest(IAsyncResult result)
		{
		}

		/// <summary>Enables a <see cref="T:System.Web.DefaultHttpHandler" /> object to process of HTTP Web requests.</summary>
		/// <param name="context">An object that provides references to intrinsic server objects used to service HTTP requests.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.DefaultHttpHandler.ProcessRequest(System.Web.HttpContext)" /> is called synchronously.</exception>
		// Token: 0x0600043B RID: 1083 RVA: 0x00008B2D File Offset: 0x00006D2D
		public virtual void ProcessRequest(HttpContext context)
		{
			throw new InvalidOperationException("The ProcessRequest cannot be called synchronously.");
		}

		/// <summary>Called when preconditions prevent the <see cref="T:System.Web.DefaultHttpHandler" /> object from processing a request.</summary>
		// Token: 0x0600043C RID: 1084 RVA: 0x00003946 File Offset: 0x00001B46
		public virtual void OnExecuteUrlPreconditionFailure()
		{
		}

		/// <summary>Overrides the target URL for the current request.</summary>
		/// <returns>The overridden URL to use in the request; or null if an overridden URL is not provided.</returns>
		// Token: 0x0600043D RID: 1085 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public virtual string OverrideExecuteUrlPath()
		{
			return null;
		}

		// Token: 0x04000E5A RID: 3674
		private NameValueCollection executeUrlHeaders;

		// Token: 0x02000069 RID: 105
		private sealed class DefaultHandlerAsyncResult : IAsyncResult
		{
			// Token: 0x170001DF RID: 479
			// (get) Token: 0x0600043F RID: 1087 RVA: 0x00008B39 File Offset: 0x00006D39
			// (set) Token: 0x06000440 RID: 1088 RVA: 0x00008B41 File Offset: 0x00006D41
			public object AsyncState { get; private set; }

			// Token: 0x170001E0 RID: 480
			// (get) Token: 0x06000441 RID: 1089 RVA: 0x00003BF6 File Offset: 0x00001DF6
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x06000442 RID: 1090 RVA: 0x00008B4A File Offset: 0x00006D4A
			public bool CompletedSynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x06000443 RID: 1091 RVA: 0x00008B4A File Offset: 0x00006D4A
			public bool IsCompleted
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000444 RID: 1092 RVA: 0x00008B4D File Offset: 0x00006D4D
			public DefaultHandlerAsyncResult(AsyncCallback callback, object state)
			{
				this.AsyncState = state;
				if (callback != null)
				{
					callback(this);
				}
			}
		}
	}
}
