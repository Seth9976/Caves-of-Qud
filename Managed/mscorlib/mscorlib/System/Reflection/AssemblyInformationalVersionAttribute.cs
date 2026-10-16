using System;

namespace System.Reflection
{
	/// <summary>Defines additional version information for an assembly manifest.</summary>
	// Token: 0x02000888 RID: 2184
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyInformationalVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyInformationalVersionAttribute" /> class.</summary>
		/// <param name="informationalVersion">The assembly version information. </param>
		// Token: 0x06004869 RID: 18537 RVA: 0x000EE12D File Offset: 0x000EC32D
		public AssemblyInformationalVersionAttribute(string informationalVersion)
		{
			this.InformationalVersion = informationalVersion;
		}

		/// <summary>Gets version information.</summary>
		/// <returns>A string containing the version information.</returns>
		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x0600486A RID: 18538 RVA: 0x000EE13C File Offset: 0x000EC33C
		public string InformationalVersion { get; }
	}
}
