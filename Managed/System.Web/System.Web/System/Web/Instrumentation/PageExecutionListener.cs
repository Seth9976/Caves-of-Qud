using System;
using Unity;

namespace System.Web.Instrumentation
{
	/// <summary>Provides methods that are called before and after a view engine renders output.</summary>
	// Token: 0x02000699 RID: 1689
	public abstract class PageExecutionListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Instrumentation.PageExecutionListener" /> class.</summary>
		// Token: 0x06004786 RID: 18310 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected PageExecutionListener()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Called by a view engine before it renders the output for the specified context.</summary>
		/// <param name="context">The page execution context.</param>
		// Token: 0x06004787 RID: 18311
		public abstract void BeginContext(PageExecutionContext context);

		/// <summary>Called by a view engine after it renders the output for the specified context.</summary>
		/// <param name="context">The page execution context.</param>
		// Token: 0x06004788 RID: 18312
		public abstract void EndContext(PageExecutionContext context);
	}
}
