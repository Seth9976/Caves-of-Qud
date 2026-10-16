using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace Unity.Properties
{
	// Token: 0x02000083 RID: 131
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal readonly struct ConversionRegistry : IEqualityComparer<ConversionRegistry>
	{
		// Token: 0x06000217 RID: 535 RVA: 0x000071E4 File Offset: 0x000053E4
		private ConversionRegistry(Dictionary<ConversionRegistry.ConverterKey, Delegate> storage)
		{
			this.m_Converters = storage;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000071F0 File Offset: 0x000053F0
		public static ConversionRegistry Create()
		{
			return new ConversionRegistry(new Dictionary<ConversionRegistry.ConverterKey, Delegate>(ConversionRegistry.Comparer));
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00007211 File Offset: 0x00005411
		public void Register(Type source, Type destination, Delegate converter)
		{
			Dictionary<ConversionRegistry.ConverterKey, Delegate> converters = this.m_Converters;
			ConversionRegistry.ConverterKey converterKey = new ConversionRegistry.ConverterKey(source, destination);
			if (converter == null)
			{
				throw new ArgumentException("converter");
			}
			converters[converterKey] = converter;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007238 File Offset: 0x00005438
		public Delegate GetConverter(Type source, Type destination)
		{
			ConversionRegistry.ConverterKey converterKey = new ConversionRegistry.ConverterKey(source, destination);
			Delegate @delegate;
			return this.m_Converters.TryGetValue(converterKey, out @delegate) ? @delegate : null;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007268 File Offset: 0x00005468
		public bool TryGetConverter(Type source, Type destination, out Delegate converter)
		{
			converter = this.GetConverter(source, destination);
			return converter != null;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000728C File Offset: 0x0000548C
		public bool Equals(ConversionRegistry x, ConversionRegistry y)
		{
			return x.m_Converters == y.m_Converters;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000072AC File Offset: 0x000054AC
		public int GetHashCode(ConversionRegistry obj)
		{
			return (obj.m_Converters != null) ? obj.m_Converters.GetHashCode() : 0;
		}

		// Token: 0x040000A6 RID: 166
		private static readonly ConversionRegistry.ConverterKeyComparer Comparer = new ConversionRegistry.ConverterKeyComparer();

		// Token: 0x040000A7 RID: 167
		private readonly Dictionary<ConversionRegistry.ConverterKey, Delegate> m_Converters;

		// Token: 0x02000084 RID: 132
		private class ConverterKeyComparer : IEqualityComparer<ConversionRegistry.ConverterKey>
		{
			// Token: 0x0600021F RID: 543 RVA: 0x000072E0 File Offset: 0x000054E0
			public bool Equals(ConversionRegistry.ConverterKey x, ConversionRegistry.ConverterKey y)
			{
				return x.SourceType == y.SourceType && x.DestinationType == y.DestinationType;
			}

			// Token: 0x06000220 RID: 544 RVA: 0x0000731C File Offset: 0x0000551C
			public int GetHashCode(ConversionRegistry.ConverterKey obj)
			{
				return (((obj.SourceType != null) ? obj.SourceType.GetHashCode() : 0) * 397) ^ ((obj.DestinationType != null) ? obj.DestinationType.GetHashCode() : 0);
			}
		}

		// Token: 0x02000085 RID: 133
		private readonly struct ConverterKey
		{
			// Token: 0x06000222 RID: 546 RVA: 0x0000736D File Offset: 0x0000556D
			public ConverterKey(Type source, Type destination)
			{
				this.SourceType = source;
				this.DestinationType = destination;
			}

			// Token: 0x040000A8 RID: 168
			public readonly Type SourceType;

			// Token: 0x040000A9 RID: 169
			public readonly Type DestinationType;
		}
	}
}
