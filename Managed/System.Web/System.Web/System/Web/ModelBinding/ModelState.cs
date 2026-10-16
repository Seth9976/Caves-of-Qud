using System;

namespace System.Web.ModelBinding
{
	/// <summary>Encapsulates the state of model binding.</summary>
	// Token: 0x0200051F RID: 1311
	[Serializable]
	public class ModelState
	{
		/// <summary>Gets or sets an object that encapsulates the value that was being bound during model binding.</summary>
		/// <returns>The value of the model.</returns>
		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x060039BF RID: 14783 RVA: 0x0009A6FA File Offset: 0x000988FA
		// (set) Token: 0x060039C0 RID: 14784 RVA: 0x0009A702 File Offset: 0x00098902
		public ValueProviderResult Value { get; set; }

		/// <summary>Gets a collection of errors that occurred during model binding.</summary>
		/// <returns>The collection of errors.</returns>
		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x060039C1 RID: 14785 RVA: 0x0009A70B File Offset: 0x0009890B
		public ModelErrorCollection Errors
		{
			get
			{
				return this._errors;
			}
		}

		// Token: 0x04001F4E RID: 8014
		private ModelErrorCollection _errors = new ModelErrorCollection();
	}
}
