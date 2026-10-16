using System;
using System.Collections.Generic;
using Microsoft.Cci;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004E8 RID: 1256
	internal class Win32Resource : IWin32Resource
	{
		// Token: 0x06003293 RID: 12947 RVA: 0x0008FCD3 File Offset: 0x0008DED3
		internal Win32Resource(byte[] data, uint codePage, uint languageId, int id, string name, int typeId, string typeName)
		{
			this._data = data;
			this._codePage = codePage;
			this._languageId = languageId;
			this._id = id;
			this._name = name;
			this._typeId = typeId;
			this._typeName = typeName;
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06003294 RID: 12948 RVA: 0x0008FD10 File Offset: 0x0008DF10
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06003295 RID: 12949 RVA: 0x0008FD18 File Offset: 0x0008DF18
		public int TypeId
		{
			get
			{
				return this._typeId;
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06003296 RID: 12950 RVA: 0x0008FD20 File Offset: 0x0008DF20
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x06003297 RID: 12951 RVA: 0x0008FD28 File Offset: 0x0008DF28
		public int Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x06003298 RID: 12952 RVA: 0x0008FD30 File Offset: 0x0008DF30
		public uint LanguageId
		{
			get
			{
				return this._languageId;
			}
		}

		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x06003299 RID: 12953 RVA: 0x0008FD38 File Offset: 0x0008DF38
		public uint CodePage
		{
			get
			{
				return this._codePage;
			}
		}

		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x0600329A RID: 12954 RVA: 0x0008FD40 File Offset: 0x0008DF40
		public IEnumerable<byte> Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x0400137F RID: 4991
		private readonly byte[] _data;

		// Token: 0x04001380 RID: 4992
		private readonly uint _codePage;

		// Token: 0x04001381 RID: 4993
		private readonly uint _languageId;

		// Token: 0x04001382 RID: 4994
		private readonly int _id;

		// Token: 0x04001383 RID: 4995
		private readonly string _name;

		// Token: 0x04001384 RID: 4996
		private readonly int _typeId;

		// Token: 0x04001385 RID: 4997
		private readonly string _typeName;
	}
}
