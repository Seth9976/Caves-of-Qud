using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Defines members that data entity classes can implement to provide custom synchronous and asynchronous validation support.</summary>
	// Token: 0x02000403 RID: 1027
	public interface INotifyDataErrorInfo
	{
		/// <summary>Gets a value that indicates whether the entity has validation errors. </summary>
		/// <returns>true if the entity currently has validation errors; otherwise, false.</returns>
		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06002149 RID: 8521
		bool HasErrors { get; }

		/// <summary>Gets the validation errors for a specified property or for the entire entity.</summary>
		/// <returns>The validation errors for the property or entity.</returns>
		/// <param name="propertyName">The name of the property to retrieve validation errors for; or null or <see cref="F:System.String.Empty" />, to retrieve entity-level errors.</param>
		// Token: 0x0600214A RID: 8522
		IEnumerable GetErrors(string propertyName);

		/// <summary>Occurs when the validation errors have changed for a property or for the entire entity. </summary>
		// Token: 0x1400002D RID: 45
		// (add) Token: 0x0600214B RID: 8523
		// (remove) Token: 0x0600214C RID: 8524
		event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
	}
}
