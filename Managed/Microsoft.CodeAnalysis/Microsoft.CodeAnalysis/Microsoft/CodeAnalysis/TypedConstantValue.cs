using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000267 RID: 615
	internal struct TypedConstantValue : IEquatable<TypedConstantValue>
	{
		// Token: 0x06001770 RID: 6000 RVA: 0x0004618E File Offset: 0x0004438E
		[NullableContext(2)]
		internal TypedConstantValue(object value)
		{
			this._value = value;
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00046197 File Offset: 0x00044397
		internal TypedConstantValue(ImmutableArray<TypedConstant> array)
		{
			this._value = (array.IsDefault ? null : array);
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x000461B1 File Offset: 0x000443B1
		public bool IsNull
		{
			get
			{
				return this._value == null;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x000461BC File Offset: 0x000443BC
		public ImmutableArray<TypedConstant> Array
		{
			get
			{
				if (this._value != null)
				{
					return (ImmutableArray<TypedConstant>)this._value;
				}
				return default(ImmutableArray<TypedConstant>);
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x000461E6 File Offset: 0x000443E6
		[Nullable(2)]
		public object Object
		{
			[NullableContext(2)]
			get
			{
				return this._value;
			}
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x000461EE File Offset: 0x000443EE
		public override int GetHashCode()
		{
			object value = this._value;
			if (value == null)
			{
				return 0;
			}
			return value.GetHashCode();
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00046201 File Offset: 0x00044401
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is TypedConstantValue && this.Equals((TypedConstantValue)obj);
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00046219 File Offset: 0x00044419
		public bool Equals(TypedConstantValue other)
		{
			return object.Equals(this._value, other._value);
		}

		// Token: 0x04000988 RID: 2440
		[Nullable(2)]
		private readonly object _value;
	}
}
