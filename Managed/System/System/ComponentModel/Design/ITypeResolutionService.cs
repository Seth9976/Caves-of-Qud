using System;
using System.Reflection;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface to retrieve an assembly or type by name.</summary>
	// Token: 0x02000471 RID: 1137
	public interface ITypeResolutionService
	{
		/// <summary>Gets the requested assembly.</summary>
		/// <returns>An instance of the requested assembly, or null if no assembly can be located.</returns>
		/// <param name="name">The name of the assembly to retrieve. </param>
		// Token: 0x06002488 RID: 9352
		Assembly GetAssembly(AssemblyName name);

		/// <summary>Gets the requested assembly.</summary>
		/// <returns>An instance of the requested assembly, or null if no assembly can be located.</returns>
		/// <param name="name">The name of the assembly to retrieve. </param>
		/// <param name="throwOnError">true if this method should throw an exception if the assembly cannot be located; otherwise, false, and this method returns null if the assembly cannot be located. </param>
		// Token: 0x06002489 RID: 9353
		Assembly GetAssembly(AssemblyName name, bool throwOnError);

		/// <summary>Loads a type with the specified name.</summary>
		/// <returns>An instance of <see cref="T:System.Type" /> that corresponds to the specified name, or null if no type can be found.</returns>
		/// <param name="name">The name of the type. If the type name is not a fully qualified name that indicates an assembly, this service will search its internal set of referenced assemblies. </param>
		// Token: 0x0600248A RID: 9354
		Type GetType(string name);

		/// <summary>Loads a type with the specified name.</summary>
		/// <returns>An instance of <see cref="T:System.Type" /> that corresponds to the specified name, or null if no type can be found.</returns>
		/// <param name="name">The name of the type. If the type name is not a fully qualified name that indicates an assembly, this service will search its internal set of referenced assemblies. </param>
		/// <param name="throwOnError">true if this method should throw an exception if the assembly cannot be located; otherwise, false, and this method returns null if the assembly cannot be located. </param>
		// Token: 0x0600248B RID: 9355
		Type GetType(string name, bool throwOnError);

		/// <summary>Loads a type with the specified name.</summary>
		/// <returns>An instance of <see cref="T:System.Type" /> that corresponds to the specified name, or null if no type can be found.</returns>
		/// <param name="name">The name of the type. If the type name is not a fully qualified name that indicates an assembly, this service will search its internal set of referenced assemblies. </param>
		/// <param name="throwOnError">true if this method should throw an exception if the assembly cannot be located; otherwise, false, and this method returns null if the assembly cannot be located. </param>
		/// <param name="ignoreCase">true to ignore case when searching for types; otherwise, false. </param>
		// Token: 0x0600248C RID: 9356
		Type GetType(string name, bool throwOnError, bool ignoreCase);

		/// <summary>Adds a reference to the specified assembly.</summary>
		/// <param name="name">An <see cref="T:System.Reflection.AssemblyName" /> that indicates the assembly to reference. </param>
		// Token: 0x0600248D RID: 9357
		void ReferenceAssembly(AssemblyName name);

		/// <summary>Gets the path to the file from which the assembly was loaded.</summary>
		/// <returns>The path to the file from which the assembly was loaded.</returns>
		/// <param name="name">The name of the assembly. </param>
		// Token: 0x0600248E RID: 9358
		string GetPathOfAssembly(AssemblyName name);
	}
}
