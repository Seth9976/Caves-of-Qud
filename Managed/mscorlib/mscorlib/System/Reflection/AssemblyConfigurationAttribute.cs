using System;

namespace System.Reflection
{
	/// <summary>Specifies the build configuration, such as retail or debug, for an assembly.</summary>
	// Token: 0x0200087F RID: 2175
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyConfigurationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyConfigurationAttribute" /> class.</summary>
		/// <param name="configuration">The assembly configuration. </param>
		// Token: 0x06004856 RID: 18518 RVA: 0x000EE067 File Offset: 0x000EC267
		public AssemblyConfigurationAttribute(string configuration)
		{
			this.Configuration = configuration;
		}

		/// <summary>Gets assembly configuration information.</summary>
		/// <returns>A string containing the assembly configuration information.</returns>
		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06004857 RID: 18519 RVA: 0x000EE076 File Offset: 0x000EC276
		public string Configuration { get; }
	}
}
