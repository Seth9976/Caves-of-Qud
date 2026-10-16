using System;
using System.Collections.Generic;

namespace Roslyn.Utilities
{
	// Token: 0x02000071 RID: 113
	internal static class ObjectBinder
	{
		// Token: 0x0600029B RID: 667 RVA: 0x000095C4 File Offset: 0x000077C4
		public static ObjectBinderSnapshot GetSnapshot()
		{
			object obj = ObjectBinder.s_gate;
			ObjectBinderSnapshot value;
			lock (obj)
			{
				if (ObjectBinder.s_lastSnapshot == null)
				{
					ObjectBinder.s_lastSnapshot = new ObjectBinderSnapshot?(new ObjectBinderSnapshot(ObjectBinder.s_typeToIndex, ObjectBinder.s_types, ObjectBinder.s_typeReaders));
				}
				value = ObjectBinder.s_lastSnapshot.Value;
			}
			return value;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00009634 File Offset: 0x00007834
		public static void RegisterTypeReader(Type type, Func<ObjectReader, IObjectWritable> typeReader)
		{
			object obj = ObjectBinder.s_gate;
			lock (obj)
			{
				if (!ObjectBinder.s_typeToIndex.ContainsKey(type))
				{
					int count = ObjectBinder.s_typeReaders.Count;
					ObjectBinder.s_types.Add(type);
					ObjectBinder.s_typeReaders.Add(typeReader);
					ObjectBinder.s_typeToIndex.Add(type, count);
					ObjectBinder.s_lastSnapshot = default(ObjectBinderSnapshot?);
				}
			}
		}

		// Token: 0x040000B4 RID: 180
		private static readonly object s_gate = new object();

		// Token: 0x040000B5 RID: 181
		private static ObjectBinderSnapshot? s_lastSnapshot = default(ObjectBinderSnapshot?);

		// Token: 0x040000B6 RID: 182
		private static readonly Dictionary<Type, int> s_typeToIndex = new Dictionary<Type, int>();

		// Token: 0x040000B7 RID: 183
		private static readonly List<Type> s_types = new List<Type>();

		// Token: 0x040000B8 RID: 184
		private static readonly List<Func<ObjectReader, IObjectWritable>> s_typeReaders = new List<Func<ObjectReader, IObjectWritable>>();
	}
}
