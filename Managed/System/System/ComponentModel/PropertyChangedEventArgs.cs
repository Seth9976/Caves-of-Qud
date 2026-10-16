using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged" /> event.</summary>
	// Token: 0x02000406 RID: 1030
	public class PropertyChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> class.</summary>
		/// <param name="propertyName">The name of the property that changed. </param>
		// Token: 0x06002151 RID: 8529 RVA: 0x0007216C File Offset: 0x0007036C
		public PropertyChangedEventArgs(string propertyName)
		{
			this._propertyName = propertyName;
		}

		/// <summary>Gets the name of the property that changed.</summary>
		/// <returns>The name of the property that changed.</returns>
		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06002152 RID: 8530 RVA: 0x0007217B File Offset: 0x0007037B
		public virtual string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x0400101E RID: 4126
		private readonly string _propertyName;
	}
}
