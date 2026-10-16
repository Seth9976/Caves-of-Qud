using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays the first model error for a given key from the page's model state.</summary>
	// Token: 0x02000796 RID: 1942
	[ParseChildren(true)]
	[PersistChildren(false)]
	[ToolboxData("<{0}:ModelErrorMessage runat=\"server\" Key=\"ModelStateKey\"></{0}:ModelErrorMessage>")]
	[DefaultProperty("Key")]
	public class ModelErrorMessage : Label
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ModelErrorMessage" /> class.</summary>
		// Token: 0x06004E65 RID: 20069 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelErrorMessage()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the key for which the first error from the page's model state is to be displayed.</summary>
		/// <returns>The key.</returns>
		// Token: 0x170017E1 RID: 6113
		// (get) Token: 0x06004E66 RID: 20070 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004E67 RID: 20071 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string ModelStateKey
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that specifies whether the focus should be set on the associated control when an error is displayed.</summary>
		/// <returns>true if the focus should be set; otherwise, false.</returns>
		// Token: 0x170017E2 RID: 6114
		// (get) Token: 0x06004E68 RID: 20072 RVA: 0x000C8AEC File Offset: 0x000C6CEC
		// (set) Token: 0x06004E69 RID: 20073 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public bool SetFocusOnError
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
