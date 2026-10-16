using System;
using System.Collections.Specialized;

namespace System.Web.UI
{
	/// <summary>Provides the default implementation of an <see cref="T:System.Web.UI.IBindableTemplate" /> object, which ASP.NET uses whenever it parses two-way data-binding within the templated content of an ASP.NET control such as <see cref="T:System.Web.UI.WebControls.FormView" />. This class cannot be inherited.</summary>
	// Token: 0x020001AD RID: 429
	public sealed class CompiledBindableTemplateBuilder : IBindableTemplate, ITemplate
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.CompiledBindableTemplateBuilder" /> class.</summary>
		/// <param name="buildTemplateMethod">A delegate used to handle the <see cref="M:System.Web.UI.CompiledBindableTemplateBuilder.InstantiateIn(System.Web.UI.Control)" /> method call.</param>
		/// <param name="extractTemplateValuesMethod">A delegate used to handle the <see cref="M:System.Web.UI.CompiledBindableTemplateBuilder.ExtractValues(System.Web.UI.Control)" /> method call.</param>
		// Token: 0x06001080 RID: 4224 RVA: 0x0002B826 File Offset: 0x00029A26
		public CompiledBindableTemplateBuilder(BuildTemplateMethod buildTemplateMethod, ExtractTemplateValuesMethod extractTemplateValuesMethod)
		{
			this.templateMethod = buildTemplateMethod;
			this.extractMethod = extractTemplateValuesMethod;
		}

		/// <summary>Defines the <see cref="T:System.Web.UI.Control" /> object that child controls and templates belong to. These child controls are in turn defined within an inline template.</summary>
		/// <param name="container">The <see cref="T:System.Web.UI.Control" /> to contain the controls that are created from the inline template. </param>
		// Token: 0x06001081 RID: 4225 RVA: 0x0002B83C File Offset: 0x00029A3C
		public void InstantiateIn(Control container)
		{
			this.templateMethod(container);
		}

		/// <summary>Retrieves a set of name/value pairs for values bound using two-way ASP.NET data-binding syntax within the templated content.</summary>
		/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> of name/value pairs. The name represents the name of the data item field specified as the first parameter to bind within templated content. The value is the current value of a property value bound using two-way ASP.NET data-binding syntax.</returns>
		/// <param name="container">The <see cref="T:System.Web.UI.Control" /> from which to extract name/value pairs, which are passed by the data-bound control to an associated data source control in two-way data-binding scenarios.</param>
		// Token: 0x06001082 RID: 4226 RVA: 0x0002B84A File Offset: 0x00029A4A
		public IOrderedDictionary ExtractValues(Control container)
		{
			if (this.extractMethod == null)
			{
				return null;
			}
			return this.extractMethod(container);
		}

		// Token: 0x04001390 RID: 5008
		private BuildTemplateMethod templateMethod;

		// Token: 0x04001391 RID: 5009
		private ExtractTemplateValuesMethod extractMethod;
	}
}
