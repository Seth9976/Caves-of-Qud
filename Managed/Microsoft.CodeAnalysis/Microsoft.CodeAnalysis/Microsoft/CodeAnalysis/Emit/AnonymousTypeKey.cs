using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000476 RID: 1142
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal struct AnonymousTypeKey : IEquatable<AnonymousTypeKey>
	{
		// Token: 0x06002C86 RID: 11398 RVA: 0x0007DB91 File Offset: 0x0007BD91
		internal AnonymousTypeKey(ImmutableArray<AnonymousTypeKeyField> fields, bool isDelegate = false)
		{
			this.IsDelegate = isDelegate;
			this.Fields = fields;
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x0007DBA1 File Offset: 0x0007BDA1
		public bool Equals(AnonymousTypeKey other)
		{
			return this.IsDelegate == other.IsDelegate && this.Fields.SequenceEqual<AnonymousTypeKeyField, AnonymousTypeKeyField>(other.Fields, null);
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x0007DBC5 File Offset: 0x0007BDC5
		public override bool Equals(object obj)
		{
			return this.Equals((AnonymousTypeKey)obj);
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x0007DBD4 File Offset: 0x0007BDD4
		public override int GetHashCode()
		{
			return Hash.Combine(this.IsDelegate.GetHashCode(), Hash.CombineValues<AnonymousTypeKeyField>(this.Fields, int.MaxValue));
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x0007DC04 File Offset: 0x0007BE04
		private string GetDebuggerDisplay()
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			for (int i = 0; i < this.Fields.Length; i++)
			{
				if (i > 0)
				{
					builder.Append("|");
				}
				builder.Append(this.Fields[i]);
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x040011B3 RID: 4531
		internal readonly bool IsDelegate;

		// Token: 0x040011B4 RID: 4532
		internal readonly ImmutableArray<AnonymousTypeKeyField> Fields;
	}
}
