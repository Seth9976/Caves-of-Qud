using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.UI
{
	/// <summary>Provides an abstract base class for a page parser filter that is used by the ASP.NET parser to determine whether an item is allowed in the page at parse time. </summary>
	// Token: 0x0200020D RID: 525
	public abstract class PageParserFilter
	{
		/// <summary>Gets a value indicating whether an ASP.NET parser filter permits code on the page. </summary>
		/// <returns>true if a parser filter permits code; otherwise, false. The default is false.</returns>
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool AllowCode
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the line number that is currently being parsed in the file.</summary>
		/// <returns>The integer value representing the line in the file that the parser filter is currently processing.</returns>
		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x0003890F File Offset: 0x00036B0F
		[global::System.MonoTODO("Need to implement support for this in the parser")]
		protected int Line
		{
			get
			{
				return this.parser.Location.BeginLine;
			}
		}

		/// <summary>Gets the maximum number of controls that a parser filter can parse for a single page.</summary>
		/// <returns>The maximum number of controls a parser filter can parse for a page. The default value is 0, which indicates that no controls are parsed.</returns>
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual int NumberOfControlsAllowed
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the maximum number of direct file dependencies that the page parser permits for a single page.</summary>
		/// <returns>The maximum number of direct file dependencies the page parser can parse for a page. The default is 0, which that indicates no dependencies are allowed.</returns>
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual int NumberOfDirectDependenciesAllowed
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the maximum number of direct and indirect file dependencies that the page parser permits for a single page.</summary>
		/// <returns>The maximum number of direct and indirect file dependencies the page parser can parse for a page. The default is 0, which indicates that no dependencies are allowed.</returns>
		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual int TotalNumberOfDependenciesAllowed
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the virtual path to the page currently being parsed.</summary>
		/// <returns>A virtual path to an ASP.NET page.</returns>
		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x00038921 File Offset: 0x00036B21
		protected string VirtualPath
		{
			get
			{
				return this.parser.VirtualPath.Absolute;
			}
		}

		/// <summary>Adds a <see cref="T:System.Web.UI.ControlBuilder" /> object in the page control tree at the current page parser position.</summary>
		/// <param name="type">The control type that the <see cref="T:System.Web.UI.ControlBuilder" /> represents.</param>
		/// <param name="attributes">The <see cref="T:System.Collections.IDictionary" /> object that holds all the specified tag attributes.</param>
		// Token: 0x0600158F RID: 5519 RVA: 0x00038933 File Offset: 0x00036B33
		protected void AddControl(Type type, IDictionary attributes)
		{
			if (this.parser == null)
			{
				return;
			}
			this.parser.AddControl(type, attributes);
		}

		/// <summary>Determines whether the page can be derived from the specified <see cref="T:System.Type" />.</summary>
		/// <returns>true if the page can inherit from the specified type; otherwise, false. The default is false.</returns>
		/// <param name="baseType">A <see cref="T:System.Type" /> that represents the potential base class of the current page.</param>
		// Token: 0x06001590 RID: 5520 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool AllowBaseType(Type baseType)
		{
			return false;
		}

		/// <summary>Gets a value indicating whether the specified control type is allowed for this page.</summary>
		/// <returns>true if the control can be used with the current page; otherwise, false. The default value is false.</returns>
		/// <param name="controlType">A <see cref="T:System.Type" /> that represents the type of control to add.</param>
		/// <param name="builder">A <see cref="T:System.Web.UI.ControlBuilder" /> used to build the specified type of control.</param>
		// Token: 0x06001591 RID: 5521 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool AllowControl(Type controlType, ControlBuilder builder)
		{
			return false;
		}

		/// <summary>Determines whether a parser permits a specific server-side include on a page.</summary>
		/// <returns>true if a parser permits the specific server-side include; otherwise, false. The default is false.</returns>
		/// <param name="includeVirtualPath">The virtual path to the included file.</param>
		// Token: 0x06001592 RID: 5522 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool AllowServerSideInclude(string includeVirtualPath)
		{
			return false;
		}

		/// <summary>Determines whether a parser permits a virtual reference to a specific type of resource on a page.</summary>
		/// <returns>true if the parser permits a virtual reference to a specific type of resource; otherwise, false.</returns>
		/// <param name="referenceVirtualPath">The virtual path to a resource, such as a master page file, ASP.NET page, or user control. </param>
		/// <param name="referenceType">A <see cref="T:System.Web.UI.VirtualReferenceType" /> value that identifies the type of resource.</param>
		// Token: 0x06001593 RID: 5523 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool AllowVirtualReference(string referenceVirtualPath, VirtualReferenceType referenceType)
		{
			return false;
		}

		/// <summary>Retrieves the current compilation mode for the page.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.CompilationMode" /> values.</returns>
		/// <param name="current">The current compilation mode for the page.</param>
		// Token: 0x06001594 RID: 5524 RVA: 0x0000207C File Offset: 0x0000027C
		public virtual CompilationMode GetCompilationMode(CompilationMode current)
		{
			return current;
		}

		/// <summary>Returns a <see cref="T:System.Type" /> that should be used for pages or controls that are not dynamically compiled.</summary>
		/// <returns>The return <see cref="T:System.Type" /> that should be used for pages or controls that are not dynamically compiled. The default is null.</returns>
		// Token: 0x06001595 RID: 5525 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public virtual Type GetNoCompileUserControlType()
		{
			return null;
		}

		/// <summary>Initializes a filter used for a page.</summary>
		// Token: 0x06001596 RID: 5526 RVA: 0x00003946 File Offset: 0x00001B46
		protected virtual void Initialize()
		{
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x0003894B File Offset: 0x00036B4B
		internal void Initialize(TemplateParser parser)
		{
			this.parser = parser;
			this.Initialize();
		}

		/// <summary>Called by an ASP.NET page parser to notify a filter when the parsing of a page is complete.</summary>
		/// <param name="rootBuilder">The <see cref="T:System.Web.UI.ControlBuilder" /> associated with the page parsing.</param>
		// Token: 0x06001598 RID: 5528 RVA: 0x00003946 File Offset: 0x00001B46
		public virtual void ParseComplete(ControlBuilder rootBuilder)
		{
		}

		/// <summary>Allows the page parser filter to preprocess page directives.</summary>
		/// <param name="directiveName">The page directive.</param>
		/// <param name="attributes">A collection of attributes and values parsed from the page.</param>
		// Token: 0x06001599 RID: 5529 RVA: 0x00003946 File Offset: 0x00001B46
		public virtual void PreprocessDirective(string directiveName, IDictionary attributes)
		{
		}

		/// <summary>Returns a value that indicates whether a code block should be processed by subsequent parser filters.</summary>
		/// <returns>true if the parser should process a code construct further; otherwise, false. The default is false.</returns>
		/// <param name="codeType">One of the <see cref="T:System.Web.UI.CodeConstructType" /> enumeration values that identifies the type of the code construct.</param>
		/// <param name="code">The string literal that contains the code inside the code construct.</param>
		// Token: 0x0600159A RID: 5530 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool ProcessCodeConstruct(CodeConstructType codeType, string code)
		{
			return false;
		}

		/// <summary>Returns a value that indicates whether the parser filter processes a data binding expression in an attribute.</summary>
		/// <returns>true if the parser filter processes data binding attributes; otherwise, false. The default is false.</returns>
		/// <param name="controlId">The ID of the control that contains the data binding attribute.</param>
		/// <param name="name">The name of the attribute with the data binding expression.</param>
		/// <param name="value">The data binding expression.</param>
		// Token: 0x0600159B RID: 5531 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool ProcessDataBindingAttribute(string controlId, string name, string value)
		{
			return false;
		}

		/// <summary>Returns a value that indicates whether event handlers should be processed further by the parser filter.</summary>
		/// <returns>true if the parser processes event handlers; otherwise, false. The default is false.</returns>
		/// <param name="controlId">The ID of the control whose event has the event handler to process.</param>
		/// <param name="eventName">The event name of the <paramref name="controlID" /> to filter on.</param>
		/// <param name="handlerName">The handler of the <paramref name="eventName" /> name to filter on.</param>
		// Token: 0x0600159C RID: 5532 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool ProcessEventHookup(string controlId, string eventName, string handlerName)
		{
			return false;
		}

		/// <summary>Sets a property on a control derived from the <see cref="T:System.Web.UI.TemplateControl" /> class, which includes the <see cref="T:System.Web.UI.Page" />, <see cref="T:System.Web.UI.UserControl" />, and <see cref="T:System.Web.UI.MasterPage" /> controls.</summary>
		/// <param name="filter">A string containing the value of the filter on an expression. For an example, see <see cref="T:System.Web.UI.PropertyEntry" />.</param>
		/// <param name="name">The name of the property to set a value for.</param>
		/// <param name="value">The value of the property to set.</param>
		// Token: 0x0600159D RID: 5533 RVA: 0x00003946 File Offset: 0x00001B46
		protected void SetPageProperty(string filter, string name, string value)
		{
		}

		/// <summary>Gets a value that indicates whether the parser filter was called from the page.</summary>
		/// <returns>true if a parser filter was called from the page; otherwise, false.</returns>
		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x0003895C File Offset: 0x00036B5C
		protected bool CalledFromParseControl
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		// Token: 0x04001530 RID: 5424
		private TemplateParser parser;
	}
}
