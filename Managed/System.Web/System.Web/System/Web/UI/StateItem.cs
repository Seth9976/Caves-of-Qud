using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Represents an item that is saved in the <see cref="T:System.Web.UI.StateBag" /> class when view state information is persisted between Web requests. This class cannot be inherited.</summary>
	// Token: 0x02000224 RID: 548
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class StateItem
	{
		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.StateItem" /> object has been modified.</summary>
		/// <returns>true if the stored <see cref="T:System.Web.UI.StateItem" /> object has been modified; otherwise, false.</returns>
		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x0003A71C File Offset: 0x0003891C
		// (set) Token: 0x0600167B RID: 5755 RVA: 0x0003A724 File Offset: 0x00038924
		public bool IsDirty
		{
			get
			{
				return this._isDirty;
			}
			set
			{
				this._isDirty = value;
			}
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Web.UI.StateItem" /> object that is stored in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		/// <returns>The value of the <see cref="T:System.Web.UI.StateItem" /> stored in the <see cref="T:System.Web.UI.StateBag" />.</returns>
		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x0003A72D File Offset: 0x0003892D
		// (set) Token: 0x0600167D RID: 5757 RVA: 0x0003A735 File Offset: 0x00038935
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00002050 File Offset: 0x00000250
		private StateItem()
		{
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0003A73E File Offset: 0x0003893E
		internal StateItem(object value)
		{
			this._value = value;
		}

		// Token: 0x04001576 RID: 5494
		private bool _isDirty;

		// Token: 0x04001577 RID: 5495
		private object _value;
	}
}
