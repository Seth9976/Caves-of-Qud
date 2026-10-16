using System;
using System.Collections;
using System.Runtime.Serialization;

namespace UnityEngine.Serialization
{
	// Token: 0x0200035A RID: 858
	internal class ListSerializationSurrogate : ISerializationSurrogate
	{
		// Token: 0x0600288E RID: 10382 RVA: 0x0004E538 File Offset: 0x0004C738
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			IList list = (IList)obj;
			info.AddValue("_size", list.Count);
			info.AddValue("_items", ListSerializationSurrogate.ArrayFromGenericList(list));
			info.AddValue("_version", 0);
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x0004E580 File Offset: 0x0004C780
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			IList list = (IList)Activator.CreateInstance(obj.GetType());
			int @int = info.GetInt32("_size");
			bool flag = @int == 0;
			object obj2;
			if (flag)
			{
				obj2 = list;
			}
			else
			{
				IEnumerator enumerator = ((IEnumerable)info.GetValue("_items", typeof(IEnumerable))).GetEnumerator();
				for (int i = 0; i < @int; i++)
				{
					bool flag2 = !enumerator.MoveNext();
					if (flag2)
					{
						throw new InvalidOperationException();
					}
					list.Add(enumerator.Current);
				}
				obj2 = list;
			}
			return obj2;
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x0004E61C File Offset: 0x0004C81C
		private static Array ArrayFromGenericList(IList list)
		{
			Array array = Array.CreateInstance(list.GetType().GetGenericArguments()[0], list.Count);
			list.CopyTo(array, 0);
			return array;
		}

		// Token: 0x04000B57 RID: 2903
		public static readonly ISerializationSurrogate Default = new ListSerializationSurrogate();
	}
}
