using System;

namespace System.Web.Compilation
{
	/// <summary>Defines an attribute that specifies the scope where a build provider will be applied when a resource is located. This class cannot be inherited.</summary>
	// Token: 0x020005FD RID: 1533
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class BuildProviderAppliesToAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.BuildProviderAppliesToAttribute" /> class that applies to the specified resource location. </summary>
		/// <param name="appliesTo">One of the <see cref="T:System.Web.Compilation.BuildProviderAppliesTo" /> values.</param>
		// Token: 0x06004261 RID: 16993 RVA: 0x000AD0E9 File Offset: 0x000AB2E9
		public BuildProviderAppliesToAttribute(BuildProviderAppliesTo appliesTo)
		{
			this._appliesTo = appliesTo;
		}

		/// <summary>Gets a value that indicates where the specified <see cref="T:System.Web.Compilation.BuildProvider" /> class will be applied when a resource with the appropriate extension is found.</summary>
		/// <returns>A <see cref="T:System.Web.Compilation.BuildProviderAppliesTo" /> value that indicates where the specified <see cref="T:System.Web.Compilation.BuildProvider" /> class will be applied when a resource with the appropriate extension is found.</returns>
		// Token: 0x17001520 RID: 5408
		// (get) Token: 0x06004262 RID: 16994 RVA: 0x000AD0F8 File Offset: 0x000AB2F8
		public BuildProviderAppliesTo AppliesTo
		{
			get
			{
				return this._appliesTo;
			}
		}

		// Token: 0x040023A4 RID: 9124
		private BuildProviderAppliesTo _appliesTo;
	}
}
