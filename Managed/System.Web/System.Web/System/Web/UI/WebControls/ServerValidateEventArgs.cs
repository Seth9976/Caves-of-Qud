using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.CustomValidator.ServerValidate" /> event of the <see cref="T:System.Web.UI.WebControls.CustomValidator" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000301 RID: 769
	public class ServerValidateEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ServerValidateEventArgs" /> class.</summary>
		/// <param name="value">The value to validate. </param>
		/// <param name="isValid">true to indicate that the value passes validation; otherwise, false. </param>
		// Token: 0x06001BAE RID: 7086 RVA: 0x00043823 File Offset: 0x00041A23
		public ServerValidateEventArgs(string value, bool isValid)
		{
			this.isValid = isValid;
			this.value = value;
		}

		/// <summary>Gets the value to validate in the custom event handler for the <see cref="E:System.Web.UI.WebControls.CustomValidator.ServerValidate" /> event.</summary>
		/// <returns>The value to validate in the custom event handler for the <see cref="E:System.Web.UI.WebControls.CustomValidator.ServerValidate" /> event.</returns>
		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x00043839 File Offset: 0x00041A39
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Gets or sets whether the value specified by the <see cref="P:System.Web.UI.WebControls.ServerValidateEventArgs.Value" /> property passed validation.</summary>
		/// <returns>true to indicate that the value specified by the <see cref="P:System.Web.UI.WebControls.ServerValidateEventArgs.Value" /> property passed validation; otherwise, false.</returns>
		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x00043841 File Offset: 0x00041A41
		// (set) Token: 0x06001BB1 RID: 7089 RVA: 0x00043849 File Offset: 0x00041A49
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}

		// Token: 0x04001750 RID: 5968
		private bool isValid;

		// Token: 0x04001751 RID: 5969
		private string value;
	}
}
