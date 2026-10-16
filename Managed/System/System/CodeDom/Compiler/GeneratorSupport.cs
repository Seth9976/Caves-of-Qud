using System;

namespace System.CodeDom.Compiler
{
	/// <summary>Defines identifiers used to determine whether a code generator supports certain types of code elements.</summary>
	// Token: 0x02000353 RID: 851
	[Flags]
	public enum GeneratorSupport
	{
		/// <summary>Indicates the generator supports arrays of arrays.</summary>
		// Token: 0x04000E73 RID: 3699
		ArraysOfArrays = 1,
		/// <summary>Indicates the generator supports a program entry point method designation. This is used when building executables.</summary>
		// Token: 0x04000E74 RID: 3700
		EntryPointMethod = 2,
		/// <summary>Indicates the generator supports goto statements.</summary>
		// Token: 0x04000E75 RID: 3701
		GotoStatements = 4,
		/// <summary>Indicates the generator supports referencing multidimensional arrays. Currently, the CodeDom cannot be used to instantiate multidimensional arrays.</summary>
		// Token: 0x04000E76 RID: 3702
		MultidimensionalArrays = 8,
		/// <summary>Indicates the generator supports static constructors.</summary>
		// Token: 0x04000E77 RID: 3703
		StaticConstructors = 16,
		/// <summary>Indicates the generator supports try...catch statements.</summary>
		// Token: 0x04000E78 RID: 3704
		TryCatchStatements = 32,
		/// <summary>Indicates the generator supports return type attribute declarations.</summary>
		// Token: 0x04000E79 RID: 3705
		ReturnTypeAttributes = 64,
		/// <summary>Indicates the generator supports value type declarations.</summary>
		// Token: 0x04000E7A RID: 3706
		DeclareValueTypes = 128,
		/// <summary>Indicates the generator supports enumeration declarations.</summary>
		// Token: 0x04000E7B RID: 3707
		DeclareEnums = 256,
		/// <summary>Indicates the generator supports delegate declarations.</summary>
		// Token: 0x04000E7C RID: 3708
		DeclareDelegates = 512,
		/// <summary>Indicates the generator supports interface declarations.</summary>
		// Token: 0x04000E7D RID: 3709
		DeclareInterfaces = 1024,
		/// <summary>Indicates the generator supports event declarations.</summary>
		// Token: 0x04000E7E RID: 3710
		DeclareEvents = 2048,
		/// <summary>Indicates the generator supports assembly attributes.</summary>
		// Token: 0x04000E7F RID: 3711
		AssemblyAttributes = 4096,
		/// <summary>Indicates the generator supports parameter attributes.</summary>
		// Token: 0x04000E80 RID: 3712
		ParameterAttributes = 8192,
		/// <summary>Indicates the generator supports reference and out parameters.</summary>
		// Token: 0x04000E81 RID: 3713
		ReferenceParameters = 16384,
		/// <summary>Indicates the generator supports chained constructor arguments.</summary>
		// Token: 0x04000E82 RID: 3714
		ChainedConstructorArguments = 32768,
		/// <summary>Indicates the generator supports the declaration of nested types.</summary>
		// Token: 0x04000E83 RID: 3715
		NestedTypes = 65536,
		/// <summary>Indicates the generator supports the declaration of members that implement multiple interfaces.</summary>
		// Token: 0x04000E84 RID: 3716
		MultipleInterfaceMembers = 131072,
		/// <summary>Indicates the generator supports public static members.</summary>
		// Token: 0x04000E85 RID: 3717
		PublicStaticMembers = 262144,
		/// <summary>Indicates the generator supports complex expressions.</summary>
		// Token: 0x04000E86 RID: 3718
		ComplexExpressions = 524288,
		/// <summary>Indicates the generator supports compilation with Win32 resources.</summary>
		// Token: 0x04000E87 RID: 3719
		Win32Resources = 1048576,
		/// <summary>Indicates the generator supports compilation with .NET Framework resources. These can be default resources compiled directly into an assembly, or resources referenced in a satellite assembly.</summary>
		// Token: 0x04000E88 RID: 3720
		Resources = 2097152,
		/// <summary>Indicates the generator supports partial type declarations.</summary>
		// Token: 0x04000E89 RID: 3721
		PartialTypes = 4194304,
		/// <summary>Indicates the generator supports generic type references.</summary>
		// Token: 0x04000E8A RID: 3722
		GenericTypeReference = 8388608,
		/// <summary>Indicates the generator supports generic type declarations.</summary>
		// Token: 0x04000E8B RID: 3723
		GenericTypeDeclaration = 16777216,
		/// <summary>Indicates the generator supports the declaration of indexer properties.</summary>
		// Token: 0x04000E8C RID: 3724
		DeclareIndexerProperties = 33554432
	}
}
