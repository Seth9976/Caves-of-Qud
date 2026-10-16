using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Symbols;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200017C RID: 380
	[NullableContext(2)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	public abstract class Location
	{
		// Token: 0x06000FD4 RID: 4052 RVA: 0x0002E617 File Offset: 0x0002C817
		internal Location()
		{
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000FD5 RID: 4053
		public abstract LocationKind Kind { get; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x0002E61F File Offset: 0x0002C81F
		[MemberNotNullWhen(true, "SourceTree")]
		public bool IsInSource
		{
			[MemberNotNullWhen(true, "SourceTree")]
			get
			{
				return this.SourceTree != null;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0002E62A File Offset: 0x0002C82A
		public bool IsInMetadata
		{
			get
			{
				return this.MetadataModuleInternal != null;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x0002E635 File Offset: 0x0002C835
		public virtual SyntaxTree SourceTree
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0002E638 File Offset: 0x0002C838
		public IModuleSymbol MetadataModule
		{
			get
			{
				IModuleSymbolInternal metadataModuleInternal = this.MetadataModuleInternal;
				return (IModuleSymbol)((metadataModuleInternal != null) ? metadataModuleInternal.GetISymbol() : null);
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x0002E651 File Offset: 0x0002C851
		internal virtual IModuleSymbolInternal MetadataModuleInternal
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0002E654 File Offset: 0x0002C854
		public virtual TextSpan SourceSpan
		{
			get
			{
				return default(TextSpan);
			}
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0002E66C File Offset: 0x0002C86C
		public virtual FileLinePositionSpan GetLineSpan()
		{
			return default(FileLinePositionSpan);
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0002E684 File Offset: 0x0002C884
		public virtual FileLinePositionSpan GetMappedLineSpan()
		{
			return default(FileLinePositionSpan);
		}

		// Token: 0x06000FDE RID: 4062
		public abstract override bool Equals(object obj);

		// Token: 0x06000FDF RID: 4063
		public abstract override int GetHashCode();

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0002E69C File Offset: 0x0002C89C
		[NullableContext(1)]
		public override string ToString()
		{
			string text = this.Kind.ToString();
			if (this.IsInSource)
			{
				string[] array = new string[5];
				array[0] = text;
				array[1] = "(";
				int num = 2;
				SyntaxTree sourceTree = this.SourceTree;
				array[num] = ((sourceTree != null) ? sourceTree.FilePath : null);
				array[3] = this.SourceSpan.ToString();
				array[4] = ")";
				text = string.Concat(array);
			}
			else if (this.IsInMetadata)
			{
				if (this.MetadataModuleInternal != null)
				{
					text = text + "(" + this.MetadataModuleInternal.Name + ")";
				}
			}
			else
			{
				FileLinePositionSpan lineSpan = this.GetLineSpan();
				if (lineSpan.Path != null)
				{
					text = string.Concat(new string[]
					{
						text,
						"(",
						lineSpan.Path,
						"@",
						(lineSpan.StartLinePosition.Line + 1).ToString(),
						":",
						(lineSpan.StartLinePosition.Character + 1).ToString(),
						")"
					});
				}
			}
			return text;
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0002E7D2 File Offset: 0x0002C9D2
		public static bool operator ==(Location left, Location right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0002E7E3 File Offset: 0x0002C9E3
		public static bool operator !=(Location left, Location right)
		{
			return !(left == right);
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0002E7F0 File Offset: 0x0002C9F0
		[NullableContext(1)]
		protected virtual string GetDebuggerDisplay()
		{
			string text = base.GetType().Name;
			FileLinePositionSpan lineSpan = this.GetLineSpan();
			if (lineSpan.Path != null)
			{
				text = string.Concat(new string[]
				{
					text,
					"(",
					lineSpan.Path,
					"@",
					(lineSpan.StartLinePosition.Line + 1).ToString(),
					":",
					(lineSpan.StartLinePosition.Character + 1).ToString(),
					")"
				});
			}
			return text;
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x0002E88C File Offset: 0x0002CA8C
		[Nullable(1)]
		public static Location None
		{
			[NullableContext(1)]
			get
			{
				return NoLocation.Singleton;
			}
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0002E893 File Offset: 0x0002CA93
		[NullableContext(1)]
		public static Location Create(SyntaxTree syntaxTree, TextSpan textSpan)
		{
			if (syntaxTree == null)
			{
				throw new ArgumentNullException("syntaxTree");
			}
			return new SourceLocation(syntaxTree, textSpan);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0002E8AA File Offset: 0x0002CAAA
		[NullableContext(1)]
		public static Location Create(string filePath, TextSpan textSpan, LinePositionSpan lineSpan)
		{
			if (filePath == null)
			{
				throw new ArgumentNullException("filePath");
			}
			return new ExternalFileLocation(filePath, textSpan, lineSpan);
		}
	}
}
