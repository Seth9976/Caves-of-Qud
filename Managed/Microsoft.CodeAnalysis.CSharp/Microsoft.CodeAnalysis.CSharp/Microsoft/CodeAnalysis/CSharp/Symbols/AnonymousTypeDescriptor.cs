using System;
using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004C1 RID: 1217
	internal readonly struct AnonymousTypeDescriptor : IEquatable<AnonymousTypeDescriptor>
	{
		// Token: 0x060067E9 RID: 26601 RVA: 0x00187201 File Offset: 0x00185401
		public AnonymousTypeDescriptor(ImmutableArray<AnonymousTypeField> fields, Location location)
		{
			this.Fields = fields;
			this.Location = location;
			this.Key = AnonymousTypeDescriptor.ComputeKey<AnonymousTypeField>(fields, (AnonymousTypeField f) => f.Name);
		}

		// Token: 0x060067EA RID: 26602 RVA: 0x0018723C File Offset: 0x0018543C
		internal static string ComputeKey<T>(ImmutableArray<T> fields, Func<T, string> getName)
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			foreach (T t in fields)
			{
				instance.Builder.Append('|');
				instance.Builder.Append(getName.Invoke(t));
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x060067EB RID: 26603 RVA: 0x00187294 File Offset: 0x00185494
		[Conditional("DEBUG")]
		internal void AssertIsGood()
		{
			foreach (AnonymousTypeField anonymousTypeField in this.Fields)
			{
			}
		}

		// Token: 0x060067EC RID: 26604 RVA: 0x001872C3 File Offset: 0x001854C3
		public bool Equals(AnonymousTypeDescriptor desc)
		{
			return this.Equals(desc, TypeCompareKind.ConsiderEverything);
		}

		// Token: 0x060067ED RID: 26605 RVA: 0x001872D0 File Offset: 0x001854D0
		internal bool Equals(AnonymousTypeDescriptor other, TypeCompareKind comparison)
		{
			if (this.Key != other.Key)
			{
				return false;
			}
			ImmutableArray<AnonymousTypeField> fields = this.Fields;
			int length = fields.Length;
			ImmutableArray<AnonymousTypeField> fields2 = other.Fields;
			for (int i = 0; i < length; i++)
			{
				if (!fields[i].TypeWithAnnotations.Equals(fields2[i].TypeWithAnnotations, comparison))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060067EE RID: 26606 RVA: 0x0018733D File Offset: 0x0018553D
		public override bool Equals(object obj)
		{
			return obj is AnonymousTypeDescriptor && this.Equals((AnonymousTypeDescriptor)obj, TypeCompareKind.ConsiderEverything);
		}

		// Token: 0x060067EF RID: 26607 RVA: 0x00187356 File Offset: 0x00185556
		public override int GetHashCode()
		{
			return this.Key.GetHashCode();
		}

		// Token: 0x060067F0 RID: 26608 RVA: 0x00187363 File Offset: 0x00185563
		internal AnonymousTypeDescriptor WithNewFieldsTypes(ImmutableArray<TypeWithAnnotations> newFieldTypes)
		{
			return new AnonymousTypeDescriptor(this.Fields.SelectAsArray((AnonymousTypeField field, int i, ImmutableArray<TypeWithAnnotations> types) => new AnonymousTypeField(field.Name, field.Location, types[i]), newFieldTypes), this.Location);
		}

		// Token: 0x040019C7 RID: 6599
		public readonly Location Location;

		// Token: 0x040019C8 RID: 6600
		public readonly ImmutableArray<AnonymousTypeField> Fields;

		// Token: 0x040019C9 RID: 6601
		public readonly string Key;
	}
}
