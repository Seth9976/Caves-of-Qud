using System;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a cell in the rendered table of a tabular ASP.NET data-bound control, such as <see cref="T:System.Web.UI.WebControls.DetailsView" /> or <see cref="T:System.Web.UI.WebControls.GridView" />.</summary>
	// Token: 0x0200036D RID: 877
	public class DataControlFieldCell : TableCell
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> class, setting the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object as the cell's container.</summary>
		/// <param name="containingField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> that contains the current cell.</param>
		// Token: 0x06002117 RID: 8471 RVA: 0x00052A4B File Offset: 0x00050C4B
		public DataControlFieldCell(DataControlField containingField)
			: this(HtmlTextWriterTag.Td, containingField)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> class, setting the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object as the cell's container.</summary>
		/// <param name="tagKey">An <see cref="T:System.Web.UI.HtmlTextWriterTag" /> that specifies the HTML tag to render for the cell. The default tag used by the <see cref="T:System.Web.UI.WebControls.TableCell" /> class is <see cref="F:System.Web.UI.HtmlTextWriterTag.Td" />.</param>
		/// <param name="containingField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> that contains the current cell.</param>
		// Token: 0x06002118 RID: 8472 RVA: 0x00052A56 File Offset: 0x00050C56
		protected DataControlFieldCell(HtmlTextWriterTag tagKey, DataControlField containingField)
			: base(tagKey)
		{
			this.containerField = containingField;
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object that contains the current cell.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataControlField" /> that contains the current cell, or null, if no <see cref="T:System.Web.UI.WebControls.DataControlField" /> is passed to the class constructor.</returns>
		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06002119 RID: 8473 RVA: 0x00052A66 File Offset: 0x00050C66
		public DataControlField ContainingField
		{
			get
			{
				return this.containerField;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the control validates client input.</summary>
		/// <returns>true if the control validates client input; otherwise, false.</returns>
		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x00052A70 File Offset: 0x00050C70
		// (set) Token: 0x0600211B RID: 8475 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override ValidateRequestMode ValidateRequestMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ValidateRequestMode.Inherit;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x040018C2 RID: 6338
		private DataControlField containerField;
	}
}
