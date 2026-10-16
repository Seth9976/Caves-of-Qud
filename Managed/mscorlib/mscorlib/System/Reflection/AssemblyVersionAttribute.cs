using System;

namespace System.Reflection
{
	/// <summary>Specifies the version of the assembly being attributed.</summary>
	// Token: 0x02000891 RID: 2193
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyVersionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the AssemblyVersionAttribute class with the version number of the assembly being attributed.</summary>
		/// <param name="version">The version number of the attributed assembly. </param>
		// Token: 0x0600487B RID: 18555 RVA: 0x000EE203 File Offset: 0x000EC403
		public AssemblyVersionAttribute(string version)
		{
			this.Version = version;
		}

		/// <summary>Gets the version number of the attributed assembly.</summary>
		/// <returns>A string containing the assembly version number.</returns>
		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x0600487C RID: 18556 RVA: 0x000EE212 File Offset: 0x000EC412
		public string Version { get; }
	}
}
