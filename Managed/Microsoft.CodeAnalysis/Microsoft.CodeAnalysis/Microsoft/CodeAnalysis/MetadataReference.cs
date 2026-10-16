using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001BB RID: 443
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class MetadataReference
	{
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x00039597 File Offset: 0x00037797
		public MetadataReferenceProperties Properties { get; }

		// Token: 0x06001286 RID: 4742 RVA: 0x0003959F File Offset: 0x0003779F
		protected MetadataReference(MetadataReferenceProperties properties)
		{
			this.Properties = properties;
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x000395AE File Offset: 0x000377AE
		[Nullable(2)]
		public virtual string Display
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x000395B1 File Offset: 0x000377B1
		internal virtual bool IsUnresolved
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x000395B4 File Offset: 0x000377B4
		public MetadataReference WithAliases(IEnumerable<string> aliases)
		{
			return this.WithAliases(ImmutableArray.CreateRange<string>(aliases));
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x000395C4 File Offset: 0x000377C4
		public MetadataReference WithEmbedInteropTypes(bool value)
		{
			return this.WithProperties(this.Properties.WithEmbedInteropTypes(value));
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x000395E8 File Offset: 0x000377E8
		public MetadataReference WithAliases([Nullable(new byte[] { 0, 1 })] ImmutableArray<string> aliases)
		{
			return this.WithProperties(this.Properties.WithAliases(aliases));
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0003960A File Offset: 0x0003780A
		public MetadataReference WithProperties(MetadataReferenceProperties properties)
		{
			if (properties == this.Properties)
			{
				return this;
			}
			return this.WithPropertiesImplReturningMetadataReference(properties);
		}

		// Token: 0x0600128D RID: 4749
		internal abstract MetadataReference WithPropertiesImplReturningMetadataReference(MetadataReferenceProperties properties);

		// Token: 0x0600128E RID: 4750 RVA: 0x00039623 File Offset: 0x00037823
		[NullableContext(2)]
		[return: Nullable(1)]
		public static PortableExecutableReference CreateFromImage([Nullable(0)] ImmutableArray<byte> peImage, MetadataReferenceProperties properties = default(MetadataReferenceProperties), DocumentationProvider documentation = null, string filePath = null)
		{
			return new MetadataImageReference(AssemblyMetadata.CreateFromImage(peImage), properties, documentation, filePath, null);
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00039634 File Offset: 0x00037834
		public static PortableExecutableReference CreateFromImage(IEnumerable<byte> peImage, MetadataReferenceProperties properties = default(MetadataReferenceProperties), [Nullable(2)] DocumentationProvider documentation = null, [Nullable(2)] string filePath = null)
		{
			return new MetadataImageReference(AssemblyMetadata.CreateFromImage(peImage), properties, documentation, filePath, null);
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00039645 File Offset: 0x00037845
		public static PortableExecutableReference CreateFromStream(Stream peStream, MetadataReferenceProperties properties = default(MetadataReferenceProperties), [Nullable(2)] DocumentationProvider documentation = null, [Nullable(2)] string filePath = null)
		{
			return new MetadataImageReference(AssemblyMetadata.CreateFromStream(peStream, PEStreamOptions.PrefetchEntireImage), properties, documentation, filePath, null);
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00039657 File Offset: 0x00037857
		public static PortableExecutableReference CreateFromFile(string path, MetadataReferenceProperties properties = default(MetadataReferenceProperties), [Nullable(2)] DocumentationProvider documentation = null)
		{
			return MetadataReference.CreateFromFile(StandardFileSystem.Instance.OpenFileWithNormalizedException(path, 3, 1, 1), path, properties, documentation);
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00039670 File Offset: 0x00037870
		internal static PortableExecutableReference CreateFromFile(Stream peStream, string path, MetadataReferenceProperties properties = default(MetadataReferenceProperties), [Nullable(2)] DocumentationProvider documentation = null)
		{
			ModuleMetadata moduleMetadata = ModuleMetadata.CreateFromStream(peStream, PEStreamOptions.PrefetchEntireImage);
			if (properties.Kind == MetadataImageKind.Module)
			{
				return new MetadataImageReference(moduleMetadata, properties, documentation, path, null);
			}
			return new MetadataImageReference(AssemblyMetadata.CreateFromFile(moduleMetadata, path), properties, documentation, path, null);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x000396AA File Offset: 0x000378AA
		[Obsolete("Use CreateFromFile(assembly.Location) instead", true)]
		[EditorBrowsable(1)]
		public static MetadataReference CreateFromAssembly(Assembly assembly)
		{
			return MetadataReference.CreateFromAssemblyInternal(assembly);
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x000396B4 File Offset: 0x000378B4
		internal static MetadataReference CreateFromAssemblyInternal(Assembly assembly)
		{
			return MetadataReference.CreateFromAssemblyInternal(assembly, default(MetadataReferenceProperties), null);
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x000396D1 File Offset: 0x000378D1
		[Obsolete("Use CreateFromFile(assembly.Location) instead", true)]
		[EditorBrowsable(1)]
		public static MetadataReference CreateFromAssembly(Assembly assembly, MetadataReferenceProperties properties, [Nullable(2)] DocumentationProvider documentation = null)
		{
			return MetadataReference.CreateFromAssemblyInternal(assembly, properties, documentation);
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x000396DC File Offset: 0x000378DC
		internal static PortableExecutableReference CreateFromAssemblyInternal(Assembly assembly, MetadataReferenceProperties properties, [Nullable(2)] DocumentationProvider documentation = null)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (assembly.IsDynamic)
			{
				throw new NotSupportedException(CodeAnalysisResources.CantCreateReferenceToDynamicAssembly);
			}
			if (properties.Kind != MetadataImageKind.Assembly)
			{
				throw new ArgumentException(CodeAnalysisResources.CantCreateModuleReferenceToAssembly, "properties");
			}
			string location = assembly.Location;
			if (string.IsNullOrEmpty(location))
			{
				throw new NotSupportedException(CodeAnalysisResources.CantCreateReferenceToAssemblyWithoutLocation);
			}
			return new MetadataImageReference(AssemblyMetadata.CreateFromStream(StandardFileSystem.Instance.OpenFileWithNormalizedException(location, 3, 1, 1), false), properties, documentation, location, null);
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00039760 File Offset: 0x00037960
		internal static bool HasMetadata(Assembly assembly)
		{
			return !assembly.IsDynamic && !string.IsNullOrEmpty(assembly.Location);
		}
	}
}
