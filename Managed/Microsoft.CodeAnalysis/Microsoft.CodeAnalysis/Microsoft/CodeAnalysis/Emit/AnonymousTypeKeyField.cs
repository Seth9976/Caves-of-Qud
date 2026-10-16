using System;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000475 RID: 1141
	internal struct AnonymousTypeKeyField : IEquatable<AnonymousTypeKeyField>
	{
		// Token: 0x06002C82 RID: 11394 RVA: 0x0007DAE4 File Offset: 0x0007BCE4
		public AnonymousTypeKeyField(string name, bool isKey, bool ignoreCase)
		{
			this.Name = name;
			this.IsKey = isKey;
			this.IgnoreCase = ignoreCase;
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x0007DAFC File Offset: 0x0007BCFC
		public bool Equals(AnonymousTypeKeyField other)
		{
			return this.IsKey == other.IsKey && this.IgnoreCase == other.IgnoreCase && (this.IgnoreCase ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal).Equals(this.Name, other.Name);
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x0007DB4C File Offset: 0x0007BD4C
		public override bool Equals(object obj)
		{
			return this.Equals((AnonymousTypeKeyField)obj);
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x0007DB5A File Offset: 0x0007BD5A
		public override int GetHashCode()
		{
			return Hash.Combine(this.IsKey, Hash.Combine(this.IgnoreCase, (this.IgnoreCase ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal).GetHashCode(this.Name)));
		}

		// Token: 0x040011B0 RID: 4528
		internal readonly string Name;

		// Token: 0x040011B1 RID: 4529
		internal readonly bool IsKey;

		// Token: 0x040011B2 RID: 4530
		internal readonly bool IgnoreCase;
	}
}
