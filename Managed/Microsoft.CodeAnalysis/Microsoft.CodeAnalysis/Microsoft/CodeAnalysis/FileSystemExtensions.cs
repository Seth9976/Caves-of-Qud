using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using Microsoft.CodeAnalysis.Emit;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200018E RID: 398
	public static class FileSystemExtensions
	{
		// Token: 0x06001043 RID: 4163 RVA: 0x0002FA08 File Offset: 0x0002DC08
		[NullableContext(1)]
		public static EmitResult Emit(this Compilation compilation, string outputPath, [Nullable(2)] string pdbPath = null, [Nullable(2)] string xmlDocPath = null, [Nullable(2)] string win32ResourcesPath = null, [Nullable(new byte[] { 2, 1 })] IEnumerable<ResourceDescription> manifestResources = null, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (compilation == null)
			{
				throw new ArgumentNullException("compilation");
			}
			EmitResult emitResult;
			using (Stream stream = FileUtilities.CreateFileStreamChecked(new Func<string, Stream>(File.Create), outputPath, "outputPath"))
			{
				using (Stream stream2 = ((pdbPath == null) ? null : FileUtilities.CreateFileStreamChecked(new Func<string, Stream>(File.Create), pdbPath, "pdbPath")))
				{
					using (Stream stream3 = ((xmlDocPath == null) ? null : FileUtilities.CreateFileStreamChecked(new Func<string, Stream>(File.Create), xmlDocPath, "xmlDocPath")))
					{
						using (Stream stream4 = ((win32ResourcesPath == null) ? null : FileUtilities.CreateFileStreamChecked(new Func<string, Stream>(File.OpenRead), win32ResourcesPath, "win32ResourcesPath")))
						{
							emitResult = compilation.Emit(stream, stream2, stream3, stream4, manifestResources, new EmitOptions(false, (DebugInformationFormat)0, pdbPath, null, 0, 0UL, false, default(SubsystemVersion), null, false, true, default(ImmutableArray<InstrumentationKind>), default(HashAlgorithmName?), null, null), cancellationToken);
						}
					}
				}
			}
			return emitResult;
		}
	}
}
