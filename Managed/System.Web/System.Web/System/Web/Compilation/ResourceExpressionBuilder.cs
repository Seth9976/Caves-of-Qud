using System;
using System.CodeDom;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Web.UI;

namespace System.Web.Compilation
{
	/// <summary>Provides code to the page parser for assigning property values on a control.</summary>
	// Token: 0x02000660 RID: 1632
	[ExpressionEditor("System.Web.UI.Design.ResourceExpressionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ExpressionPrefix("Resources")]
	public class ResourceExpressionBuilder : ExpressionBuilder
	{
		/// <summary>Returns a value from a resource file.</summary>
		/// <returns>An <see cref="T:System.Object" /> associated with the parsed expression. The parsed expression contains the class name and resource key.</returns>
		/// <param name="target">The object containing the expression.</param>
		/// <param name="entry">The object that represents information about the property bound to by the expression.</param>
		/// <param name="parsedData">The object containing parsed data as returned by the <see cref="Overload:System.Web.Compilation.ResourceExpressionBuilder.ParseExpression" /> method.</param>
		/// <param name="context">Contextual information for the evaluation of the expression.</param>
		// Token: 0x060045E2 RID: 17890 RVA: 0x000BE59C File Offset: 0x000BC79C
		public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			ResourceExpressionFields resourceExpressionFields = parsedData as ResourceExpressionFields;
			return HttpContext.GetGlobalResourceObject(resourceExpressionFields.ClassKey, resourceExpressionFields.ResourceKey);
		}

		/// <summary>Returns a code expression to evaluate during page execution.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that invokes a method.</returns>
		/// <param name="entry">The property name of the object.</param>
		/// <param name="parsedData">The parsed value of the expression.</param>
		/// <param name="context">Properties for the control or page.</param>
		// Token: 0x060045E3 RID: 17891 RVA: 0x000BE5C4 File Offset: 0x000BC7C4
		public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
		{
			ResourceExpressionFields resourceExpressionFields = parsedData as ResourceExpressionFields;
			if (entry == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(resourceExpressionFields.ClassKey))
			{
				return ResourceExpressionBuilder.CreateGetLocalResourceObject(entry, resourceExpressionFields.ResourceKey);
			}
			if (entry.PropertyInfo == null)
			{
				return null;
			}
			CodeExpression[] array = new CodeExpression[]
			{
				new CodePrimitiveExpression(resourceExpressionFields.ClassKey),
				new CodePrimitiveExpression(resourceExpressionFields.ResourceKey)
			};
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "GetGlobalResourceObject", array);
			return new CodeCastExpression(entry.PropertyInfo.PropertyType, codeMethodInvokeExpression);
		}

		/// <summary>Returns an object that represents the parsed expression.</summary>
		/// <returns>The <see cref="T:System.Web.Compilation.ResourceExpressionFields" /> for the expression.</returns>
		/// <param name="expression">The expression value to be parsed.</param>
		// Token: 0x060045E4 RID: 17892 RVA: 0x000BE648 File Offset: 0x000BC848
		public static ResourceExpressionFields ParseExpression(string expression)
		{
			int num = expression.IndexOf(',');
			if (num == -1)
			{
				return new ResourceExpressionFields(expression.Trim());
			}
			return new ResourceExpressionFields(expression.Substring(0, num).Trim(), expression.Substring(num + 1).Trim());
		}

		/// <summary>Returns an object that represents the parsed expression.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the parsed expression.</returns>
		/// <param name="expression">The value of the declarative expression.</param>
		/// <param name="propertyType">The type of the property bound to by the expression.</param>
		/// <param name="context">Contextual information for the evaluation of the expression.</param>
		/// <exception cref="T:System.Web.HttpException">The resource expression cannot be found or is invalid.</exception>
		// Token: 0x060045E5 RID: 17893 RVA: 0x000BE68E File Offset: 0x000BC88E
		public override object ParseExpression(string expression, Type propertyType, ExpressionBuilderContext context)
		{
			return ResourceExpressionBuilder.ParseExpression(expression);
		}

		/// <summary>Returns a value indicating whether an expression can be evaluated in a page that uses the no-compile feature.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x060045E6 RID: 17894 RVA: 0x00008B4A File Offset: 0x00006D4A
		public override bool SupportsEvaluate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060045E7 RID: 17895 RVA: 0x000BE696 File Offset: 0x000BC896
		internal static CodeExpression CreateGetLocalResourceObject(BoundPropertyEntry bpe, string resname)
		{
			if (bpe == null || string.IsNullOrEmpty(resname))
			{
				return null;
			}
			if (bpe.UseSetAttribute)
			{
				return ResourceExpressionBuilder.CreateGetLocalResourceObject(bpe.Type, typeof(string), null, resname);
			}
			return ResourceExpressionBuilder.CreateGetLocalResourceObject(bpe.PropertyInfo, resname);
		}

		// Token: 0x060045E8 RID: 17896 RVA: 0x000BE6D4 File Offset: 0x000BC8D4
		internal static CodeExpression CreateGetLocalResourceObject(MemberInfo mi, string resname)
		{
			if (string.IsNullOrEmpty(resname))
			{
				return null;
			}
			Type type;
			if (mi is PropertyInfo)
			{
				type = ((PropertyInfo)mi).PropertyType;
			}
			else
			{
				if (!(mi is FieldInfo))
				{
					return null;
				}
				type = ((FieldInfo)mi).FieldType;
			}
			return ResourceExpressionBuilder.CreateGetLocalResourceObject(type, mi.DeclaringType, mi.Name, resname);
		}

		// Token: 0x060045E9 RID: 17897 RVA: 0x000BE730 File Offset: 0x000BC930
		private static CodeExpression CreateGetLocalResourceObject(Type member_type, Type declaringType, string memberName, string resname)
		{
			TypeConverter typeConverter;
			if (!string.IsNullOrEmpty(memberName))
			{
				typeConverter = TypeDescriptor.GetProperties(declaringType)[memberName].Converter;
			}
			else
			{
				typeConverter = null;
			}
			if (member_type != typeof(Color) && (typeConverter == null || typeConverter.CanConvertFrom(typeof(string))))
			{
				CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "GetLocalResourceObject", new CodeExpression[]
				{
					new CodePrimitiveExpression(resname)
				});
				return TemplateControlCompiler.CreateConvertToCall(Type.GetTypeCode(member_type), codeMethodInvokeExpression);
			}
			if (!string.IsNullOrEmpty(memberName))
			{
				CodeMethodInvokeExpression codeMethodInvokeExpression2 = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "GetLocalResourceObject", new CodeExpression[]
				{
					new CodePrimitiveExpression(resname),
					new CodeTypeOfExpression(new CodeTypeReference(declaringType)),
					new CodePrimitiveExpression(memberName)
				});
				return new CodeCastExpression(member_type, codeMethodInvokeExpression2);
			}
			return null;
		}
	}
}
