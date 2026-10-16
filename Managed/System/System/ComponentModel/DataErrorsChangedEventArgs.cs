using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.INotifyDataErrorInfo.ErrorsChanged" /> event.</summary>
	// Token: 0x02000402 RID: 1026
	public class DataErrorsChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataErrorsChangedEventArgs" /> class.</summary>
		/// <param name="propertyName">The name of the property that has an error.  null or <see cref="F:System.String.Empty" /> if the error is object-level.</param>
		// Token: 0x06002147 RID: 8519 RVA: 0x00072155 File Offset: 0x00070355
		public DataErrorsChangedEventArgs(string propertyName)
		{
			this._propertyName = propertyName;
		}

		/// <summary>Gets the name of the property that has an error.</summary>
		/// <returns>The name of the property that has an error. null or <see cref="F:System.String.Empty" /> if the error is object-level.</returns>
		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06002148 RID: 8520 RVA: 0x00072164 File Offset: 0x00070364
		public virtual string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x0400101D RID: 4125
		private readonly string _propertyName;
	}
}
