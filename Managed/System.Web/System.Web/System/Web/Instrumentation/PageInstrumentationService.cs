using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.Instrumentation
{
	/// <summary>Provides information about page execution listeners and instrumentation.</summary>
	// Token: 0x02000698 RID: 1688
	public sealed class PageInstrumentationService
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Instrumentation.PageInstrumentationService" /> class.</summary>
		// Token: 0x06004782 RID: 18306 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public PageInstrumentationService()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a list of listeners that are subscribed to the page execution process.</summary>
		/// <returns>The list of listeners that are subscribed to the page execution process.</returns>
		// Token: 0x17001619 RID: 5657
		// (get) Token: 0x06004783 RID: 18307 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public IList<PageExecutionListener> ExecutionListeners
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets or sets a value that indicates whether instrumentation is active for the entire application.</summary>
		/// <returns>true to indicate that instrumentation is active for the entire application; otherwise, false.</returns>
		// Token: 0x1700161A RID: 5658
		// (get) Token: 0x06004784 RID: 18308 RVA: 0x000C73F4 File Offset: 0x000C55F4
		// (set) Token: 0x06004785 RID: 18309 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static bool IsEnabled
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
