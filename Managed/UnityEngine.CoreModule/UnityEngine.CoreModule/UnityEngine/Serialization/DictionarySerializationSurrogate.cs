using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UnityEngine.Serialization
{
	// Token: 0x0200035B RID: 859
	internal class DictionarySerializationSurrogate<TKey, TValue> : ISerializationSurrogate
	{
		// Token: 0x06002893 RID: 10387 RVA: 0x0004E660 File Offset: 0x0004C860
		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			Dictionary<TKey, TValue> dictionary = (Dictionary<TKey, TValue>)obj;
			dictionary.GetObjectData(info, context);
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x0004E680 File Offset: 0x0004C880
		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			IEqualityComparer<TKey> equalityComparer = (IEqualityComparer<TKey>)info.GetValue("Comparer", typeof(IEqualityComparer<TKey>));
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(equalityComparer);
			bool flag = info.MemberCount > 3;
			if (flag)
			{
				KeyValuePair<TKey, TValue>[] array = (KeyValuePair<TKey, TValue>[])info.GetValue("KeyValuePairs", typeof(KeyValuePair<TKey, TValue>[]));
				bool flag2 = array != null;
				if (flag2)
				{
					foreach (KeyValuePair<TKey, TValue> keyValuePair in array)
					{
						dictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			return dictionary;
		}
	}
}
