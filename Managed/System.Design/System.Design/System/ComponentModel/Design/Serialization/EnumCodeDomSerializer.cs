using System;
using System.CodeDom;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000154 RID: 340
	internal class EnumCodeDomSerializer : CodeDomSerializer
	{
		// Token: 0x06000A6B RID: 2667 RVA: 0x00015554 File Offset: 0x00013754
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			TypeConverter converter = TypeDescriptor.GetConverter(value);
			Enum[] array;
			if (converter.CanConvertTo(typeof(Enum[])))
			{
				array = (Enum[])converter.ConvertTo(value, typeof(Enum[]));
			}
			else
			{
				array = new Enum[] { (Enum)value };
			}
			CodeExpression codeExpression = null;
			foreach (Enum @enum in array)
			{
				CodeExpression enumExpression = this.GetEnumExpression(@enum);
				if (codeExpression == null)
				{
					codeExpression = enumExpression;
				}
				else
				{
					codeExpression = new CodeBinaryOperatorExpression(codeExpression, CodeBinaryOperatorType.BitwiseOr, enumExpression);
				}
			}
			return codeExpression;
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00015600 File Offset: 0x00013800
		private CodeExpression GetEnumExpression(Enum e)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(e);
			if (converter != null && converter.CanConvertTo(typeof(string)))
			{
				return new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(e.GetType().FullName), (string)converter.ConvertTo(e, typeof(string)));
			}
			return null;
		}
	}
}
