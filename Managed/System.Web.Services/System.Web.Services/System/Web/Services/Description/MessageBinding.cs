using System;

namespace System.Web.Services.Description
{
	/// <summary>Describes how abstract content is mapped into a concrete format.</summary>
	// Token: 0x020000EE RID: 238
	public abstract class MessageBinding : NamedItem
	{
		// Token: 0x06000665 RID: 1637 RVA: 0x0001C4A5 File Offset: 0x0001A6A5
		internal void SetParent(OperationBinding parent)
		{
			this.parent = parent;
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.OperationBinding" /> of which the current <see cref="T:System.Web.Services.Description.MessageBinding" /> is a member.</summary>
		/// <returns>An <see cref="T:System.Web.Services.Description.OperationBinding" /> of which the current <see cref="T:System.Web.Services.Description.MessageBinding" /> is a member.</returns>
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0001C4AE File Offset: 0x0001A6AE
		public OperationBinding OperationBinding
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x040003F0 RID: 1008
		private OperationBinding parent;
	}
}
