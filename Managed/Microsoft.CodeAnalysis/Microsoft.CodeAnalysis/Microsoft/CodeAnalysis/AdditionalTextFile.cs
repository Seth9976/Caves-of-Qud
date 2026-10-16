using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000106 RID: 262
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class AdditionalTextFile : AdditionalText
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x00019DCC File Offset: 0x00017FCC
		public AdditionalTextFile(CommandLineSourceFile sourceFile, CommonCompiler compiler)
		{
			if (compiler == null)
			{
				throw new ArgumentNullException("compiler");
			}
			this._sourceFile = sourceFile;
			this._compiler = compiler;
			this._diagnostics = SpecializedCollections.EmptyList<DiagnosticInfo>();
			this._text = new Lazy<SourceText>(new Func<SourceText>(this.ReadText));
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00019E20 File Offset: 0x00018020
		[NullableContext(2)]
		private SourceText ReadText()
		{
			List<DiagnosticInfo> list = new List<DiagnosticInfo>();
			SourceText sourceText = this._compiler.TryReadFileContent(this._sourceFile, list);
			this._diagnostics = list;
			return sourceText;
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00019E4C File Offset: 0x0001804C
		public override string Path
		{
			get
			{
				return this._sourceFile.Path;
			}
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00019E59 File Offset: 0x00018059
		[NullableContext(2)]
		public override SourceText GetText(CancellationToken cancellationToken = default(CancellationToken))
		{
			return this._text.Value;
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00019E66 File Offset: 0x00018066
		internal IList<DiagnosticInfo> Diagnostics
		{
			get
			{
				return this._diagnostics;
			}
		}

		// Token: 0x0400028C RID: 652
		private readonly CommandLineSourceFile _sourceFile;

		// Token: 0x0400028D RID: 653
		private readonly CommonCompiler _compiler;

		// Token: 0x0400028E RID: 654
		[Nullable(new byte[] { 1, 2 })]
		private readonly Lazy<SourceText> _text;

		// Token: 0x0400028F RID: 655
		private IList<DiagnosticInfo> _diagnostics;
	}
}
