using System;

namespace System.Reflection
{
	/// <summary>Defines a company name custom attribute for an assembly manifest.</summary>
	// Token: 0x0200087E RID: 2174
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyCompanyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyCompanyAttribute" /> class.</summary>
		/// <param name="company">The company name information. </param>
		// Token: 0x06004854 RID: 18516 RVA: 0x000EE050 File Offset: 0x000EC250
		public AssemblyCompanyAttribute(string company)
		{
			this.Company = company;
		}

		/// <summary>Gets company name information.</summary>
		/// <returns>A string containing the company name.</returns>
		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06004855 RID: 18517 RVA: 0x000EE05F File Offset: 0x000EC25F
		public string Company { get; }
	}
}
