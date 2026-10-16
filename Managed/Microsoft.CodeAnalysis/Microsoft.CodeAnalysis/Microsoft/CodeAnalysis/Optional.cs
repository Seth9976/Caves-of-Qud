using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001C5 RID: 453
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct Optional<[Nullable(2)] T>
	{
		// Token: 0x06001301 RID: 4865 RVA: 0x00039F7A File Offset: 0x0003817A
		public Optional(T value)
		{
			this._hasValue = true;
			this._value = value;
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x00039F8A File Offset: 0x0003818A
		public bool HasValue
		{
			get
			{
				return this._hasValue;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x00039F92 File Offset: 0x00038192
		public T Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00039F9A File Offset: 0x0003819A
		[return: Nullable(new byte[] { 0, 1 })]
		public static implicit operator Optional<T>(T value)
		{
			return new Optional<T>(value);
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00039FA4 File Offset: 0x000381A4
		public override string ToString()
		{
			if (!this._hasValue)
			{
				return "unspecified";
			}
			T value = this._value;
			return ((value != null) ? value.ToString() : null) ?? "null";
		}

		// Token: 0x040005A6 RID: 1446
		private readonly bool _hasValue;

		// Token: 0x040005A7 RID: 1447
		private readonly T _value;
	}
}
