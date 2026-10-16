using System;
using System.Web.UI;

namespace System.Web.Compilation
{
	/// <summary>Provides the context for an <see cref="T:System.Web.Compilation.ExpressionBuilder" /> object.</summary>
	// Token: 0x0200064E RID: 1614
	public sealed class ExpressionBuilderContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ExpressionBuilderContext" /> class using the specified virtual path.</summary>
		/// <param name="virtualPath">The virtual path of the file associated with the specified <see cref="T:System.Web.Compilation.ExpressionBuilder" />.</param>
		// Token: 0x06004546 RID: 17734 RVA: 0x000BBEF9 File Offset: 0x000BA0F9
		public ExpressionBuilderContext(string virtualPath)
		{
			this.vpath = virtualPath;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ExpressionBuilderContext" /> class using the specified template control.</summary>
		/// <param name="templateControl">The <see cref="T:System.Web.UI.TemplateControl" /> to use with the specified <see cref="T:System.Web.Compilation.ExpressionBuilder" />.</param>
		// Token: 0x06004547 RID: 17735 RVA: 0x000BBF08 File Offset: 0x000BA108
		public ExpressionBuilderContext(TemplateControl templateControl)
		{
			this.tcontrol = templateControl;
		}

		/// <summary>Provides an <see cref="T:System.Web.Compilation.ExpressionBuilder" /> object with a reference to a <see cref="T:System.Web.UI.TemplateControl" /> object.</summary>
		/// <returns>The <see cref="T:System.Web.UI.TemplateControl" /> that contains this expression.</returns>
		// Token: 0x170015AE RID: 5550
		// (get) Token: 0x06004548 RID: 17736 RVA: 0x000BBF17 File Offset: 0x000BA117
		public TemplateControl TemplateControl
		{
			get
			{
				return this.tcontrol;
			}
		}

		/// <summary>Returns a virtual path to the file associated with the <see cref="T:System.Web.Compilation.ExpressionBuilderContext" /> object.</summary>
		/// <returns>The virtual path of the file associated with the <see cref="T:System.Web.Compilation.ExpressionBuilderContext" />.</returns>
		// Token: 0x170015AF RID: 5551
		// (get) Token: 0x06004549 RID: 17737 RVA: 0x000BBF1F File Offset: 0x000BA11F
		public string VirtualPath
		{
			get
			{
				return this.vpath;
			}
		}

		// Token: 0x040024EE RID: 9454
		private TemplateControl tcontrol;

		// Token: 0x040024EF RID: 9455
		private string vpath;
	}
}
