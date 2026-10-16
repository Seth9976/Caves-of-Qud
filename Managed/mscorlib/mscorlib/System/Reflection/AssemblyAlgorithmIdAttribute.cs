using System;
using System.Configuration.Assemblies;

namespace System.Reflection
{
	/// <summary>Specifies an algorithm to hash all files in an assembly. This class cannot be inherited.</summary>
	// Token: 0x0200087D RID: 2173
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyAlgorithmIdAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyAlgorithmIdAttribute" /> class with the specified hash algorithm, using one of the members of <see cref="T:System.Configuration.Assemblies.AssemblyHashAlgorithm" /> to represent the hash algorithm.</summary>
		/// <param name="algorithmId">A member of AssemblyHashAlgorithm that represents the hash algorithm. </param>
		// Token: 0x06004851 RID: 18513 RVA: 0x000EE039 File Offset: 0x000EC239
		public AssemblyAlgorithmIdAttribute(AssemblyHashAlgorithm algorithmId)
		{
			this.AlgorithmId = (uint)algorithmId;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyAlgorithmIdAttribute" /> class with the specified hash algorithm, using an unsigned integer to represent the hash algorithm.</summary>
		/// <param name="algorithmId">An unsigned integer representing the hash algorithm. </param>
		// Token: 0x06004852 RID: 18514 RVA: 0x000EE039 File Offset: 0x000EC239
		[CLSCompliant(false)]
		public AssemblyAlgorithmIdAttribute(uint algorithmId)
		{
			this.AlgorithmId = algorithmId;
		}

		/// <summary>Gets the hash algorithm of an assembly manifest's contents.</summary>
		/// <returns>An unsigned integer representing the assembly hash algorithm.</returns>
		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06004853 RID: 18515 RVA: 0x000EE048 File Offset: 0x000EC248
		[CLSCompliant(false)]
		public uint AlgorithmId { get; }
	}
}
