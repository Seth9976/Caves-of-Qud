using System;
using System.Collections.Generic;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001B3 RID: 435
	internal sealed class AssemblyIdentityMap<TValue>
	{
		// Token: 0x0600124D RID: 4685 RVA: 0x00038B52 File Offset: 0x00036D52
		public AssemblyIdentityMap()
		{
			this._map = new Dictionary<string, OneOrMany<KeyValuePair<AssemblyIdentity, TValue>>>(AssemblyIdentityComparer.SimpleNameComparer);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00038B6C File Offset: 0x00036D6C
		public bool Contains(AssemblyIdentity identity, bool allowHigherVersion = true)
		{
			TValue tvalue;
			return this.TryGetValue(identity, out tvalue, allowHigherVersion);
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00038B84 File Offset: 0x00036D84
		public bool TryGetValue(AssemblyIdentity identity, out TValue value, bool allowHigherVersion = true)
		{
			OneOrMany<KeyValuePair<AssemblyIdentity, TValue>> oneOrMany;
			if (this._map.TryGetValue(identity.Name, ref oneOrMany))
			{
				int num = -1;
				for (int i = 0; i < oneOrMany.Count; i++)
				{
					AssemblyIdentity key = oneOrMany[i].Key;
					if (AssemblyIdentity.EqualIgnoringNameAndVersion(key, identity))
					{
						if (key.Version == identity.Version)
						{
							value = oneOrMany[i].Value;
							return true;
						}
						if (allowHigherVersion && !(key.Version < identity.Version) && (num == -1 || key.Version < oneOrMany[num].Key.Version))
						{
							num = i;
						}
					}
				}
				if (num >= 0)
				{
					value = oneOrMany[num].Value;
					return true;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00038C70 File Offset: 0x00036E70
		public bool TryGetValue(AssemblyIdentity identity, out TValue value, Func<Version, Version, TValue, bool> comparer)
		{
			OneOrMany<KeyValuePair<AssemblyIdentity, TValue>> oneOrMany;
			if (this._map.TryGetValue(identity.Name, ref oneOrMany))
			{
				for (int i = 0; i < oneOrMany.Count; i++)
				{
					AssemblyIdentity key = oneOrMany[i].Key;
					if (comparer.Invoke(identity.Version, key.Version, oneOrMany[i].Value) && AssemblyIdentity.EqualIgnoringNameAndVersion(key, identity))
					{
						value = oneOrMany[i].Value;
						return true;
					}
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00038D04 File Offset: 0x00036F04
		public void Add(AssemblyIdentity identity, TValue value)
		{
			KeyValuePair<AssemblyIdentity, TValue> keyValuePair = KeyValuePairUtil.Create<AssemblyIdentity, TValue>(identity, value);
			OneOrMany<KeyValuePair<AssemblyIdentity, TValue>> oneOrMany;
			this._map[identity.Name] = (this._map.TryGetValue(identity.Name, ref oneOrMany) ? oneOrMany.Add(keyValuePair) : OneOrMany.Create<KeyValuePair<AssemblyIdentity, TValue>>(keyValuePair));
		}

		// Token: 0x04000579 RID: 1401
		private readonly Dictionary<string, OneOrMany<KeyValuePair<AssemblyIdentity, TValue>>> _map;
	}
}
