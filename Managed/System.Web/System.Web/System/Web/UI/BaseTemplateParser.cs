using System;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;

namespace System.Web.UI
{
	/// <summary>Implements ASP.NET template parsing for template files.</summary>
	// Token: 0x020001A0 RID: 416
	public abstract class BaseTemplateParser : TemplateParser
	{
		/// <summary>Compiles and returns the type of the <see cref="T:System.Web.UI.Page" /> or <see cref="T:System.Web.UI.UserControl" /> control that is specified by the virtual path.</summary>
		/// <returns>The type of the page or user control.</returns>
		/// <param name="virtualPath">The virtual path of the <see cref="T:System.Web.UI.Page" /> or <see cref="T:System.Web.UI.UserControl" />. </param>
		/// <exception cref="T:System.Web.HttpException">The parser does not permit a virtual reference to the resource specified by <paramref name="virtualPath" />. </exception>
		// Token: 0x06000FF2 RID: 4082 RVA: 0x00029DB0 File Offset: 0x00027FB0
		protected Type GetReferencedType(string virtualPath)
		{
			if (string.IsNullOrEmpty(virtualPath))
			{
				throw new ArgumentNullException("virtualPath");
			}
			PageParserFilter pageParserFilter = base.PageParserFilter;
			if (pageParserFilter != null)
			{
				CompilationSection compilationSection = WebConfigurationManager.GetSection("system.web/compilation") as CompilationSection;
				if (compilationSection == null)
				{
					throw new HttpException("Internal error. Missing configuration section.");
				}
				string extension = VirtualPathUtility.GetExtension(virtualPath);
				Type providerTypeForExtension = compilationSection.BuildProviders.GetProviderTypeForExtension(extension);
				VirtualReferenceType virtualReferenceType;
				if (providerTypeForExtension == null)
				{
					virtualReferenceType = VirtualReferenceType.Other;
				}
				else if (providerTypeForExtension == typeof(PageBuildProvider))
				{
					virtualReferenceType = VirtualReferenceType.Page;
				}
				else if (providerTypeForExtension == typeof(UserControlBuildProvider))
				{
					virtualReferenceType = VirtualReferenceType.UserControl;
				}
				else if (providerTypeForExtension == typeof(MasterPageBuildProvider))
				{
					virtualReferenceType = VirtualReferenceType.Master;
				}
				else
				{
					virtualReferenceType = VirtualReferenceType.SourceFile;
				}
				if (!pageParserFilter.AllowVirtualReference(virtualPath, virtualReferenceType))
				{
					throw new HttpException("The parser does not permit a virtual reference to the UserControl.");
				}
			}
			virtualPath = HostingEnvironment.VirtualPathProvider.CombineVirtualPaths(base.VirtualPath.Absolute, virtualPath);
			return BuildManager.GetCompiledType(virtualPath);
		}

		/// <summary>Compiles and returns the type of the <see cref="T:System.Web.UI.UserControl" /> object that is specified by the virtual path.</summary>
		/// <returns>The type of the user control. </returns>
		/// <param name="virtualPath">The virtual path of the <see cref="T:System.Web.UI.UserControl" />. </param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.UserControl" /> specified by <paramref name="virtualPath" /> is marked as no compile.- or -The parser does not permit a virtual reference to the <see cref="T:System.Web.UI.UserControl" />. </exception>
		// Token: 0x06000FF3 RID: 4083 RVA: 0x00029E91 File Offset: 0x00028091
		[global::System.MonoTODO("We don't do anything here with the no-compile controls.")]
		protected internal Type GetUserControlType(string virtualPath)
		{
			return this.GetReferencedType(virtualPath);
		}
	}
}
