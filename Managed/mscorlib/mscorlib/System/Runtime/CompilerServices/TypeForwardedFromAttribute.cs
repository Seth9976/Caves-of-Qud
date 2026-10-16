using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies a source <see cref="T:System.Type" /> in another assembly. </summary>
	// Token: 0x02000809 RID: 2057
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	public sealed class TypeForwardedFromAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.TypeForwardedFromAttribute" /> class. </summary>
		/// <param name="assemblyFullName">The source <see cref="T:System.Type" /> in another assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyFullName" /> is null or empty.</exception>
		// Token: 0x06004629 RID: 17961 RVA: 0x000E59BB File Offset: 0x000E3BBB
		public TypeForwardedFromAttribute(string assemblyFullName)
		{
			if (string.IsNullOrEmpty(assemblyFullName))
			{
				throw new ArgumentNullException("assemblyFullName");
			}
			this.AssemblyFullName = assemblyFullName;
		}

		/// <summary>Gets the assembly-qualified name of the source type.</summary>
		/// <returns>The assembly-qualified name of the source type.</returns>
		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x0600462A RID: 17962 RVA: 0x000E59DD File Offset: 0x000E3BDD
		public string AssemblyFullName { get; }
	}
}
