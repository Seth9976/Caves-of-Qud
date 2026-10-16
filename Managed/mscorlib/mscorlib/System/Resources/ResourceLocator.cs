using System;

namespace System.Resources
{
	// Token: 0x0200086B RID: 2155
	internal struct ResourceLocator
	{
		// Token: 0x060047BB RID: 18363 RVA: 0x000EA5AE File Offset: 0x000E87AE
		internal ResourceLocator(int dataPos, object value)
		{
			this._dataPos = dataPos;
			this._value = value;
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x060047BC RID: 18364 RVA: 0x000EA5BE File Offset: 0x000E87BE
		internal int DataPosition
		{
			get
			{
				return this._dataPos;
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x060047BD RID: 18365 RVA: 0x000EA5C6 File Offset: 0x000E87C6
		// (set) Token: 0x060047BE RID: 18366 RVA: 0x000EA5CE File Offset: 0x000E87CE
		internal object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x000EA5D7 File Offset: 0x000E87D7
		internal static bool CanCache(ResourceTypeCode value)
		{
			return value <= ResourceTypeCode.TimeSpan;
		}

		// Token: 0x04002DEC RID: 11756
		internal object _value;

		// Token: 0x04002DED RID: 11757
		internal int _dataPos;
	}
}
