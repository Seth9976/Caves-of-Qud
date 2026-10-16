using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UnityEngine.Serialization
{
	// Token: 0x02000359 RID: 857
	public class UnitySurrogateSelector : ISurrogateSelector
	{
		// Token: 0x0600288A RID: 10378 RVA: 0x0004E4AC File Offset: 0x0004C6AC
		public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
		{
			bool isGenericType = type.IsGenericType;
			if (isGenericType)
			{
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				bool flag = genericTypeDefinition == typeof(List<>);
				if (flag)
				{
					selector = this;
					return ListSerializationSurrogate.Default;
				}
				bool flag2 = genericTypeDefinition == typeof(Dictionary<, >);
				if (flag2)
				{
					selector = this;
					Type type2 = typeof(DictionarySerializationSurrogate<, >).MakeGenericType(type.GetGenericArguments());
					return (ISerializationSurrogate)Activator.CreateInstance(type2);
				}
			}
			selector = null;
			return null;
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x00028AD4 File Offset: 0x00026CD4
		public void ChainSelector(ISurrogateSelector selector)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x00028AD4 File Offset: 0x00026CD4
		public ISurrogateSelector GetNextSelector()
		{
			throw new NotImplementedException();
		}
	}
}
