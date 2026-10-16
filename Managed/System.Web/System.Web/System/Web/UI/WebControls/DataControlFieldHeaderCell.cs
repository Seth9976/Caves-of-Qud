using System;

namespace System.Web.UI.WebControls
{
	/// <summary>In accessibility scenarios, represents a header cell in the rendered table of a tabular ASP.NET data-bound control, such as <see cref="T:System.Web.UI.WebControls.GridView" />.</summary>
	// Token: 0x0200036F RID: 879
	public class DataControlFieldHeaderCell : DataControlFieldCell
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataControlFieldHeaderCell" /> class, setting the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object as the cell's container.</summary>
		/// <param name="containingField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> that contains the current cell.</param>
		// Token: 0x06002132 RID: 8498 RVA: 0x00052CFE File Offset: 0x00050EFE
		public DataControlFieldHeaderCell(DataControlField containingField)
			: base(HtmlTextWriterTag.Th, containingField)
		{
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x00052D09 File Offset: 0x00050F09
		internal DataControlFieldHeaderCell(DataControlField containerField, TableHeaderScope scope)
			: this(containerField)
		{
			this.scope = scope;
		}

		/// <summary>Gets or sets the header cell's scope within an HTML table.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TableHeaderScope" /> values. The default is <see cref="F:System.Web.UI.WebControls.TableHeaderScope.NotSet" />.</returns>
		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06002134 RID: 8500 RVA: 0x00052D1C File Offset: 0x00050F1C
		// (set) Token: 0x06002135 RID: 8501 RVA: 0x00052D45 File Offset: 0x00050F45
		public virtual TableHeaderScope Scope
		{
			get
			{
				object obj = this.ViewState["Scope"];
				if (obj != null)
				{
					return (TableHeaderScope)obj;
				}
				return TableHeaderScope.NotSet;
			}
			set
			{
				this.ViewState["Scope"] = value;
			}
		}

		/// <summary>Gets or sets abbreviated text, which is rendered in an HTML abbr attribute and is used by screen readers.</summary>
		/// <returns>A shortened version of the table header text, which is read by screen readers. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06002136 RID: 8502 RVA: 0x00052D60 File Offset: 0x00050F60
		// (set) Token: 0x06002137 RID: 8503 RVA: 0x00052D8D File Offset: 0x00050F8D
		public virtual string AbbreviatedText
		{
			get
			{
				object obj = this.ViewState["AbbreviatedText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["AbbreviatedText"] = value;
			}
		}

		/// <summary>Adds information about the table cell to the list of attributes to render.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream that renders HTML content to the client.</param>
		// Token: 0x06002138 RID: 8504 RVA: 0x00052DA0 File Offset: 0x00050FA0
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			TableHeaderScope tableHeaderScope = this.scope;
			if (tableHeaderScope != TableHeaderScope.Row)
			{
				if (tableHeaderScope == TableHeaderScope.Column)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col", false);
				}
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Scope, "row", false);
			}
			if (this.AbbreviatedText.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Abbr, this.AbbreviatedText);
			}
		}

		// Token: 0x040018C6 RID: 6342
		private TableHeaderScope scope;
	}
}
