using System;

namespace System.ComponentModel
{
	/// <summary>Provides functionality to commit or rollback changes to an object that is used as a data source.</summary>
	// Token: 0x02000400 RID: 1024
	public interface IEditableObject
	{
		/// <summary>Begins an edit on an object.</summary>
		// Token: 0x06002143 RID: 8515
		void BeginEdit();

		/// <summary>Pushes changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit" /> or <see cref="M:System.ComponentModel.IBindingList.AddNew" /> call into the underlying object.</summary>
		// Token: 0x06002144 RID: 8516
		void EndEdit();

		/// <summary>Discards changes since the last <see cref="M:System.ComponentModel.IEditableObject.BeginEdit" /> call.</summary>
		// Token: 0x06002145 RID: 8517
		void CancelEdit();
	}
}
