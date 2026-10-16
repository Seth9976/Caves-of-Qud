using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Encapsulates the information about a data method used by a <see cref="T:System.Web.UI.WebControls.ModelDataSourceView" /> object.</summary>
	// Token: 0x020006AE RID: 1710
	public class ModelDataSourceMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ModelDataSourceMethod" /> class.</summary>
		/// <param name="instance">The instance on which the method will be invoked.</param>
		/// <param name="methodInfo">The method to be invoked.</param>
		// Token: 0x06004838 RID: 18488 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelDataSourceMethod(object instance, MethodInfo methodInfo)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the instance on which the method will be invoked.</summary>
		/// <returns>The instance on which the method will be invoked. For static methods, this will be null.</returns>
		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x06004839 RID: 18489 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public object Instance
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the method to be invoked</summary>
		/// <returns>The method to be invoked.</returns>
		// Token: 0x1700165F RID: 5727
		// (get) Token: 0x0600483A RID: 18490 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public MethodInfo MethodInfo
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the method parameter values</summary>
		/// <returns>The method parameter values.</returns>
		// Token: 0x17001660 RID: 5728
		// (get) Token: 0x0600483B RID: 18491 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public OrderedDictionary Parameters
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
