using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200034A RID: 842
	[NullableContext(2)]
	[Nullable(0)]
	internal readonly struct SourceOrAdditionalFile : IEquatable<SourceOrAdditionalFile>
	{
		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06002443 RID: 9283 RVA: 0x00071230 File Offset: 0x0006F430
		public SyntaxTree SourceTree { get; }

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06002444 RID: 9284 RVA: 0x00071238 File Offset: 0x0006F438
		public AdditionalText AdditionalFile { get; }

		// Token: 0x06002445 RID: 9285 RVA: 0x00071240 File Offset: 0x0006F440
		[NullableContext(1)]
		public SourceOrAdditionalFile(SyntaxTree tree)
		{
			this.SourceTree = tree;
			this.AdditionalFile = null;
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x00071250 File Offset: 0x0006F450
		[NullableContext(1)]
		public SourceOrAdditionalFile(AdditionalText file)
		{
			this.AdditionalFile = file;
			this.SourceTree = null;
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x00071260 File Offset: 0x0006F460
		public override bool Equals(object obj)
		{
			if (obj is SourceOrAdditionalFile)
			{
				SourceOrAdditionalFile sourceOrAdditionalFile = (SourceOrAdditionalFile)obj;
				return this.Equals(sourceOrAdditionalFile);
			}
			return false;
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x00071285 File Offset: 0x0006F485
		public bool Equals(SourceOrAdditionalFile other)
		{
			return this.SourceTree == other.SourceTree && this.AdditionalFile == other.AdditionalFile;
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x000712A7 File Offset: 0x0006F4A7
		public static bool operator ==(SourceOrAdditionalFile left, SourceOrAdditionalFile right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x000712BA File Offset: 0x0006F4BA
		public static bool operator !=(SourceOrAdditionalFile left, SourceOrAdditionalFile right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x000712D0 File Offset: 0x0006F4D0
		public override int GetHashCode()
		{
			if (this.SourceTree != null)
			{
				return Hash.Combine(true, this.SourceTree.GetHashCode());
			}
			return Hash.Combine(false, this.AdditionalFile.GetHashCode());
		}
	}
}
