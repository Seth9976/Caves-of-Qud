using System;

namespace System.ComponentModel.Design.Data
{
	/// <summary>Represents a data view in the data store.</summary>
	// Token: 0x0200016F RID: 367
	public abstract class DesignerDataView : DesignerDataTableBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataView" /> class with the specified name. </summary>
		/// <param name="name">The name of the view.</param>
		// Token: 0x06000AF4 RID: 2804 RVA: 0x0001660F File Offset: 0x0001480F
		protected DesignerDataView(string name)
			: base(name)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DesignerDataView" /> class with the specified name and owner. </summary>
		/// <param name="name">The name of the view.</param>
		/// <param name="owner">The data-store owner of the view.</param>
		// Token: 0x06000AF5 RID: 2805 RVA: 0x00016618 File Offset: 0x00014818
		protected DesignerDataView(string name, string owner)
			: base(name, owner)
		{
		}
	}
}
