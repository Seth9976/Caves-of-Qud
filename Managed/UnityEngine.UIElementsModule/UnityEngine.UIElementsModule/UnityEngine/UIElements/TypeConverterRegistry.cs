using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000028 RID: 40
	internal readonly struct TypeConverterRegistry : IEqualityComparer<TypeConverterRegistry>
	{
		// Token: 0x060000DB RID: 219 RVA: 0x000045C4 File Offset: 0x000027C4
		private TypeConverterRegistry(Dictionary<TypeConverterRegistry.ConverterKey, Delegate> storage)
		{
			this.m_Converters = storage;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000045CE File Offset: 0x000027CE
		public int ConverterCount
		{
			get
			{
				Dictionary<TypeConverterRegistry.ConverterKey, Delegate> converters = this.m_Converters;
				return (converters != null) ? converters.Count : 0;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000045E4 File Offset: 0x000027E4
		public static TypeConverterRegistry Create()
		{
			return new TypeConverterRegistry(new Dictionary<TypeConverterRegistry.ConverterKey, Delegate>(TypeConverterRegistry.k_Comparer));
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004605 File Offset: 0x00002805
		public void Register(Type source, Type destination, Delegate converter)
		{
			Dictionary<TypeConverterRegistry.ConverterKey, Delegate> converters = this.m_Converters;
			TypeConverterRegistry.ConverterKey converterKey = new TypeConverterRegistry.ConverterKey(source, destination);
			if (converter == null)
			{
				throw new ArgumentException("converter");
			}
			converters[converterKey] = converter;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000462B File Offset: 0x0000282B
		public void Unregister(Type source, Type destination)
		{
			this.m_Converters.Remove(new TypeConverterRegistry.ConverterKey(source, destination));
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004644 File Offset: 0x00002844
		internal void Apply(TypeConverterRegistry registry)
		{
			foreach (KeyValuePair<TypeConverterRegistry.ConverterKey, Delegate> keyValuePair in registry.m_Converters)
			{
				this.Register(keyValuePair.Key.SourceType, keyValuePair.Key.DestinationType, keyValuePair.Value);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000046BC File Offset: 0x000028BC
		public Delegate GetConverter(Type source, Type destination)
		{
			TypeConverterRegistry.ConverterKey converterKey = new TypeConverterRegistry.ConverterKey(source, destination);
			Delegate @delegate;
			return this.m_Converters.TryGetValue(converterKey, out @delegate) ? @delegate : null;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000046EC File Offset: 0x000028EC
		public bool TryGetConverter(Type source, Type destination, out Delegate converter)
		{
			converter = this.GetConverter(source, destination);
			return converter != null;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004710 File Offset: 0x00002910
		public void GetAllTypesConvertingToType(Type type, List<Type> result)
		{
			bool flag = this.m_Converters == null;
			if (!flag)
			{
				foreach (TypeConverterRegistry.ConverterKey converterKey in this.m_Converters.Keys)
				{
					bool flag2 = converterKey.DestinationType == type;
					if (flag2)
					{
						result.Add(converterKey.SourceType);
					}
				}
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004794 File Offset: 0x00002994
		public void GetAllTypesConvertingFromType(Type type, List<Type> result)
		{
			bool flag = this.m_Converters == null;
			if (!flag)
			{
				foreach (TypeConverterRegistry.ConverterKey converterKey in this.m_Converters.Keys)
				{
					bool flag2 = converterKey.SourceType == type;
					if (flag2)
					{
						result.Add(converterKey.DestinationType);
					}
				}
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004818 File Offset: 0x00002A18
		public void GetAllConversions(List<ValueTuple<Type, Type>> result)
		{
			bool flag = this.m_Converters == null;
			if (!flag)
			{
				foreach (TypeConverterRegistry.ConverterKey converterKey in this.m_Converters.Keys)
				{
					result.Add(new ValueTuple<Type, Type>(converterKey.SourceType, converterKey.DestinationType));
				}
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004898 File Offset: 0x00002A98
		public bool Equals(TypeConverterRegistry x, TypeConverterRegistry y)
		{
			return x.m_Converters == y.m_Converters;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000048B8 File Offset: 0x00002AB8
		public int GetHashCode(TypeConverterRegistry obj)
		{
			return (obj.m_Converters != null) ? obj.m_Converters.GetHashCode() : 0;
		}

		// Token: 0x0400005A RID: 90
		private static readonly TypeConverterRegistry.ConverterKeyComparer k_Comparer = new TypeConverterRegistry.ConverterKeyComparer();

		// Token: 0x0400005B RID: 91
		private readonly Dictionary<TypeConverterRegistry.ConverterKey, Delegate> m_Converters;

		// Token: 0x02000029 RID: 41
		private class ConverterKeyComparer : IEqualityComparer<TypeConverterRegistry.ConverterKey>
		{
			// Token: 0x060000E9 RID: 233 RVA: 0x000048EC File Offset: 0x00002AEC
			public bool Equals(TypeConverterRegistry.ConverterKey x, TypeConverterRegistry.ConverterKey y)
			{
				return x.SourceType == y.SourceType && x.DestinationType == y.DestinationType;
			}

			// Token: 0x060000EA RID: 234 RVA: 0x00004928 File Offset: 0x00002B28
			public int GetHashCode(TypeConverterRegistry.ConverterKey obj)
			{
				return (((obj.SourceType != null) ? obj.SourceType.GetHashCode() : 0) * 397) ^ ((obj.DestinationType != null) ? obj.DestinationType.GetHashCode() : 0);
			}
		}

		// Token: 0x0200002A RID: 42
		private readonly struct ConverterKey
		{
			// Token: 0x060000EC RID: 236 RVA: 0x00004979 File Offset: 0x00002B79
			public ConverterKey(Type source, Type destination)
			{
				this.SourceType = source;
				this.DestinationType = destination;
			}

			// Token: 0x0400005C RID: 92
			public readonly Type SourceType;

			// Token: 0x0400005D RID: 93
			public readonly Type DestinationType;
		}
	}
}
