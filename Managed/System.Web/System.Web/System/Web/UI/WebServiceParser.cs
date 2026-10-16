using System;
using System.IO;
using System.Security.Permissions;
using System.Web.Compilation;
using Unity;

namespace System.Web.UI
{
	/// <summary>Provides a parser for Web service handlers. </summary>
	// Token: 0x0200024B RID: 587
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class WebServiceParser : SimpleWebHandlerParser
	{
		// Token: 0x060017E7 RID: 6119 RVA: 0x0003E3AE File Offset: 0x0003C5AE
		private WebServiceParser(HttpContext context, string virtualPath, string physicalPath)
			: base(context, virtualPath, physicalPath)
		{
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x0003E45F File Offset: 0x0003C65F
		internal WebServiceParser(HttpContext context, VirtualPath virtualPath, TextReader reader)
			: this(context, virtualPath, null, reader)
		{
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x0003E3C5 File Offset: 0x0003C5C5
		internal WebServiceParser(HttpContext context, VirtualPath virtualPath, string physicalPath, TextReader reader)
			: base(context, virtualPath.Original, physicalPath, reader)
		{
		}

		/// <summary>Returns the compiled type for a given input file.</summary>
		/// <returns>A <see cref="T:System.Type" /> as specified by the <see cref="T:System.Web.HttpContext" />.</returns>
		/// <param name="inputFile">The file to be compiled. </param>
		/// <param name="context">The <see cref="T:System.Web.HttpContext" /> object for the current request. </param>
		// Token: 0x060017EA RID: 6122 RVA: 0x00032EEA File Offset: 0x000310EA
		public static Type GetCompiledType(string inputFile, HttpContext context)
		{
			return BuildManager.GetCompiledType(inputFile);
		}

		/// <summary>Gets the default directive name.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the default directive name.</returns>
		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x0003E46B File Offset: 0x0003C66B
		protected override string DefaultDirectiveName
		{
			get
			{
				return "webservice";
			}
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal WebServiceParser()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
