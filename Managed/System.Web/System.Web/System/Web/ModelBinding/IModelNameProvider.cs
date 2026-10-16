using System;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a way to specify an alternate name to use for model binding instead of using the parameter name.</summary>
	// Token: 0x02000517 RID: 1303
	public interface IModelNameProvider
	{
		/// <summary>When implemented in a class, gets the model name.</summary>
		/// <returns>The model name.</returns>
		// Token: 0x060039A6 RID: 14758
		string GetModelName();
	}
}
