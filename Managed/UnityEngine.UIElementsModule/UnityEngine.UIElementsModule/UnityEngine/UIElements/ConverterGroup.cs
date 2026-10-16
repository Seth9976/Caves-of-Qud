using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000027 RID: 39
	public class ConverterGroup
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000443F File Offset: 0x0000263F
		public string id { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004447 File Offset: 0x00002647
		public string displayName { get; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000444F File Offset: 0x0000264F
		public string description { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004457 File Offset: 0x00002657
		internal TypeConverterRegistry registry { get; }

		// Token: 0x060000D7 RID: 215 RVA: 0x0000445F File Offset: 0x0000265F
		public ConverterGroup(string id, string displayName = null, string description = null)
		{
			this.id = id;
			this.displayName = displayName;
			this.description = description;
			this.registry = TypeConverterRegistry.Create();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000448C File Offset: 0x0000268C
		public void AddConverter<TSource, TDestination>(TypeConverter<TSource, TDestination> converter)
		{
			this.registry.Register(typeof(TSource), typeof(TDestination), converter);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000044C0 File Offset: 0x000026C0
		public bool TryConvert<TSource, TDestination>(ref TSource source, out TDestination destination)
		{
			Delegate @delegate;
			TypeConverter<TSource, TDestination> typeConverter;
			bool flag;
			if (this.registry.TryGetConverter(typeof(TSource), typeof(TDestination), out @delegate))
			{
				typeConverter = @delegate as TypeConverter<TSource, TDestination>;
				flag = typeConverter != null;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			bool flag3;
			if (flag2)
			{
				destination = typeConverter(ref source);
				flag3 = true;
			}
			else
			{
				destination = default(TDestination);
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004528 File Offset: 0x00002728
		public bool TrySetValue<TContainer, TValue>(ref TContainer container, in PropertyPath path, TValue value, out VisitReturnCode returnCode)
		{
			bool isEmpty = path.IsEmpty;
			bool flag;
			if (isEmpty)
			{
				returnCode = VisitReturnCode.InvalidPath;
				flag = false;
			}
			else
			{
				SetValueVisitor<TValue> setValueVisitor = SetValueVisitor<TValue>.Pool.Get();
				setValueVisitor.group = this;
				setValueVisitor.Path = path;
				setValueVisitor.Value = value;
				try
				{
					bool flag2 = !PropertyContainer.TryAccept<TContainer>(setValueVisitor, ref container, out returnCode, default(VisitParameters));
					if (flag2)
					{
						return false;
					}
					returnCode = setValueVisitor.ReturnCode;
				}
				finally
				{
					SetValueVisitor<TValue>.Pool.Release(setValueVisitor);
				}
				flag = returnCode == VisitReturnCode.Ok;
			}
			return flag;
		}
	}
}
