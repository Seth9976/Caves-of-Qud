using System;
using System.Collections.Generic;
using System.IO;
using Unity;

namespace System.Web.UI
{
	/// <summary>Provides the abstract base class for an object that monitors as controls are rendering during a page request.</summary>
	// Token: 0x02000788 RID: 1928
	public abstract class RenderTraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.RenderTraceListener" /> class.</summary>
		// Token: 0x06004E26 RID: 20006 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected RenderTraceListener()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a list of functions that are called to instantiate <see cref="T:System.Web.UI.RenderTraceListener" /> objects for each request.</summary>
		/// <returns>A list of functions that are called to instantiate <see cref="T:System.Web.UI.RenderTraceListener" /> objects for each request.</returns>
		// Token: 0x170017D3 RID: 6099
		// (get) Token: 0x06004E27 RID: 20007 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public static IList<Func<RenderTraceListener>> ListenerFactories
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Called when an object is about to be rendered.</summary>
		/// <param name="writer">An object that can write a sequential series of characters.</param>
		/// <param name="renderedObject">The rendered object.</param>
		// Token: 0x06004E28 RID: 20008 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void BeginRendering(TextWriter writer, object renderedObject)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Called when an object is finished rendering.</summary>
		/// <param name="writer">An object that can write a sequential series of characters.</param>
		/// <param name="renderedObject">The rendered object.</param>
		// Token: 0x06004E29 RID: 20009 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void EndRendering(TextWriter writer, object renderedObject)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes the listener objects.</summary>
		/// <param name="context">The HTTP-specific information about an individual HTTP request.</param>
		// Token: 0x06004E2A RID: 20010 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void Initialize(HttpContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Associates a data object with either the controls or other object that will be rendered.</summary>
		/// <param name="tracedObject">The object to be traced.</param>
		/// <param name="traceDataKey">The key field of a record in a data-bound control.</param>
		/// <param name="traceDataValue">The value of the extracted data.</param>
		// Token: 0x06004E2B RID: 20011 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void SetTraceData(object tracedObject, object traceDataKey, object traceDataValue)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Associates the trace data from one object to another object.</summary>
		/// <param name="source">The object from which to share trace data.</param>
		/// <param name="destination">The object to which trace data is shared. </param>
		// Token: 0x06004E2C RID: 20012 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void ShareTraceData(object source, object destination)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
