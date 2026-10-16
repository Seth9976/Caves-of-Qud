using System;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace RoslynCSharp.Compiler
{
	// Token: 0x02000004 RID: 4
	public static class AssemblyReferenceExtensions
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000026BC File Offset: 0x000008BC
		public static bool TryResolveReference(this IMetadataReferenceProvider provider)
		{
			MetadataReference metadataReference;
			Exception ex;
			return provider.TryResolveReference(out metadataReference, out ex);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000026D4 File Offset: 0x000008D4
		public static bool TryResolveReference(this IMetadataReferenceProvider provider, out MetadataReference reference, out Exception error)
		{
			reference = null;
			error = null;
			bool flag;
			try
			{
				reference = provider.Reference;
				flag = true;
			}
			catch (Exception ex)
			{
				error = new TargetException(string.Format("Failed to resolve assembly reference '{0}'", reference.ToString()), ex);
				flag = false;
			}
			return flag;
		}
	}
}
