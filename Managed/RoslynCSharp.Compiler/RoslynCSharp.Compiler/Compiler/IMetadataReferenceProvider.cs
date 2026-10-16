using System;
using Microsoft.CodeAnalysis;

namespace RoslynCSharp.Compiler
{
	// Token: 0x0200000C RID: 12
	public interface IMetadataReferenceProvider
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000045 RID: 69
		MetadataReference Reference { get; }
	}
}
