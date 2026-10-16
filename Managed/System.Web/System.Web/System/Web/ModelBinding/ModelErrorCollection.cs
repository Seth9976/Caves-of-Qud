using System;
using System.Collections.ObjectModel;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a container for model validation errors.</summary>
	// Token: 0x0200051E RID: 1310
	[Serializable]
	public class ModelErrorCollection : Collection<ModelError>
	{
		/// <summary>Adds a validation error to the collection using the specified exception.</summary>
		/// <param name="exception">The exception.</param>
		// Token: 0x060039BC RID: 14780 RVA: 0x0009A6D6 File Offset: 0x000988D6
		public void Add(Exception exception)
		{
			base.Add(new ModelError(exception));
		}

		/// <summary>Adds a validation error to the collection using the specified error message string.</summary>
		/// <param name="errorMessage">The error message string.</param>
		// Token: 0x060039BD RID: 14781 RVA: 0x0009A6E4 File Offset: 0x000988E4
		public void Add(string errorMessage)
		{
			base.Add(new ModelError(errorMessage));
		}
	}
}
