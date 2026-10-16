using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000127 RID: 295
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{FilePath,nq}")]
	public struct CommandLineAnalyzerReference : IEquatable<CommandLineAnalyzerReference>
	{
		// Token: 0x06000A5F RID: 2655 RVA: 0x0001E9B9 File Offset: 0x0001CBB9
		public CommandLineAnalyzerReference(string path)
		{
			this._path = path;
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x0001E9C2 File Offset: 0x0001CBC2
		public string FilePath
		{
			get
			{
				return this._path;
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0001E9CA File Offset: 0x0001CBCA
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is CommandLineAnalyzerReference && base.Equals((CommandLineAnalyzerReference)obj);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0001E9F1 File Offset: 0x0001CBF1
		public bool Equals(CommandLineAnalyzerReference other)
		{
			return this._path == other._path;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0001EA04 File Offset: 0x0001CC04
		public override int GetHashCode()
		{
			return Hash.Combine<string>(this._path, 0);
		}

		// Token: 0x040002EB RID: 747
		private readonly string _path;
	}
}
