using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Roslyn.Utilities
{
	// Token: 0x02000072 RID: 114
	internal readonly struct ObjectBinderSnapshot
	{
		// Token: 0x0600029E RID: 670 RVA: 0x000096E9 File Offset: 0x000078E9
		public ObjectBinderSnapshot(Dictionary<Type, int> typeToIndex, List<Type> types, List<Func<ObjectReader, IObjectWritable>> typeReaders)
		{
			this._typeToIndex = new Dictionary<Type, int>(typeToIndex);
			this._types = types.ToImmutableArray<Type>();
			this._typeReaders = typeReaders.ToImmutableArray<Func<ObjectReader, IObjectWritable>>();
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000970F File Offset: 0x0000790F
		public int GetTypeId(Type type)
		{
			return this._typeToIndex[type];
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00009720 File Offset: 0x00007920
		public Type GetTypeFromId(int typeId)
		{
			return this._types[typeId];
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000973C File Offset: 0x0000793C
		public Func<ObjectReader, IObjectWritable> GetTypeReaderFromId(int typeId)
		{
			return this._typeReaders[typeId];
		}

		// Token: 0x040000B9 RID: 185
		private readonly Dictionary<Type, int> _typeToIndex;

		// Token: 0x040000BA RID: 186
		private readonly ImmutableArray<Type> _types;

		// Token: 0x040000BB RID: 187
		private readonly ImmutableArray<Func<ObjectReader, IObjectWritable>> _typeReaders;
	}
}
