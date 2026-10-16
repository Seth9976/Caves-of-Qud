using System;
using System.Collections.Generic;

namespace Unity.Properties
{
	// Token: 0x0200004C RID: 76
	public class KeyValuePairPropertyBag<TKey, TValue> : PropertyBag<KeyValuePair<TKey, TValue>>, INamedProperties<KeyValuePair<TKey, TValue>>
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00005950 File Offset: 0x00003B50
		public override PropertyCollection<KeyValuePair<TKey, TValue>> GetProperties()
		{
			return new PropertyCollection<KeyValuePair<TKey, TValue>>(KeyValuePairPropertyBag<TKey, TValue>.GetPropertiesEnumerable());
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000596C File Offset: 0x00003B6C
		public override PropertyCollection<KeyValuePair<TKey, TValue>> GetProperties(ref KeyValuePair<TKey, TValue> container)
		{
			return new PropertyCollection<KeyValuePair<TKey, TValue>>(KeyValuePairPropertyBag<TKey, TValue>.GetPropertiesEnumerable());
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005988 File Offset: 0x00003B88
		private static IEnumerable<IProperty<KeyValuePair<TKey, TValue>>> GetPropertiesEnumerable()
		{
			yield return KeyValuePairPropertyBag<TKey, TValue>.s_KeyProperty;
			yield return KeyValuePairPropertyBag<TKey, TValue>.s_ValueProperty;
			yield break;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005994 File Offset: 0x00003B94
		public bool TryGetProperty(ref KeyValuePair<TKey, TValue> container, string name, out IProperty<KeyValuePair<TKey, TValue>> property)
		{
			bool flag = name == "Key";
			bool flag2;
			if (flag)
			{
				property = KeyValuePairPropertyBag<TKey, TValue>.s_KeyProperty;
				flag2 = true;
			}
			else
			{
				bool flag3 = name == "Value";
				if (flag3)
				{
					property = KeyValuePairPropertyBag<TKey, TValue>.s_ValueProperty;
					flag2 = true;
				}
				else
				{
					property = null;
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x0400006F RID: 111
		private static readonly DelegateProperty<KeyValuePair<TKey, TValue>, TKey> s_KeyProperty = new DelegateProperty<KeyValuePair<TKey, TValue>, TKey>("Key", delegate(ref KeyValuePair<TKey, TValue> container)
		{
			return container.Key;
		}, null);

		// Token: 0x04000070 RID: 112
		private static readonly DelegateProperty<KeyValuePair<TKey, TValue>, TValue> s_ValueProperty = new DelegateProperty<KeyValuePair<TKey, TValue>, TValue>("Value", delegate(ref KeyValuePair<TKey, TValue> container)
		{
			return container.Value;
		}, null);
	}
}
