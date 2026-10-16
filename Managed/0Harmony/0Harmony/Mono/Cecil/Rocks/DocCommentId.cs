using System;
using System.Collections.Generic;
using System.Text;

namespace Mono.Cecil.Rocks
{
	// Token: 0x02000415 RID: 1045
	internal class DocCommentId
	{
		// Token: 0x060015F5 RID: 5621 RVA: 0x00046859 File Offset: 0x00044A59
		private DocCommentId()
		{
			this.id = new StringBuilder();
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x0004686C File Offset: 0x00044A6C
		private void WriteField(FieldDefinition field)
		{
			this.WriteDefinition('F', field);
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00046877 File Offset: 0x00044A77
		private void WriteEvent(EventDefinition @event)
		{
			this.WriteDefinition('E', @event);
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00046882 File Offset: 0x00044A82
		private void WriteType(TypeDefinition type)
		{
			this.id.Append('T').Append(':');
			this.WriteTypeFullName(type, false);
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x000468A4 File Offset: 0x00044AA4
		private void WriteMethod(MethodDefinition method)
		{
			this.WriteDefinition('M', method);
			if (method.HasGenericParameters)
			{
				this.id.Append('`').Append('`');
				this.id.Append(method.GenericParameters.Count);
			}
			if (method.HasParameters)
			{
				this.WriteParameters(method.Parameters);
			}
			if (DocCommentId.IsConversionOperator(method))
			{
				this.WriteReturnType(method);
			}
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00046911 File Offset: 0x00044B11
		private static bool IsConversionOperator(MethodDefinition self)
		{
			if (self == null)
			{
				throw new ArgumentNullException("self");
			}
			return self.IsSpecialName && (self.Name == "op_Explicit" || self.Name == "op_Implicit");
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x0004694F File Offset: 0x00044B4F
		private void WriteReturnType(MethodDefinition method)
		{
			this.id.Append('~');
			this.WriteTypeSignature(method.ReturnType);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x0004696B File Offset: 0x00044B6B
		private void WriteProperty(PropertyDefinition property)
		{
			this.WriteDefinition('P', property);
			if (property.HasParameters)
			{
				this.WriteParameters(property.Parameters);
			}
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x0004698A File Offset: 0x00044B8A
		private void WriteParameters(IList<ParameterDefinition> parameters)
		{
			this.id.Append('(');
			this.WriteList<ParameterDefinition>(parameters, delegate(ParameterDefinition p)
			{
				this.WriteTypeSignature(p.ParameterType);
			});
			this.id.Append(')');
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x000469BC File Offset: 0x00044BBC
		private void WriteTypeSignature(TypeReference type)
		{
			MetadataType metadataType = type.MetadataType;
			switch (metadataType)
			{
			case MetadataType.Pointer:
				this.WriteTypeSignature(((PointerType)type).ElementType);
				this.id.Append('*');
				return;
			case MetadataType.ByReference:
				this.WriteTypeSignature(((ByReferenceType)type).ElementType);
				this.id.Append('@');
				return;
			case MetadataType.ValueType:
			case MetadataType.Class:
				break;
			case MetadataType.Var:
				this.id.Append('`');
				this.id.Append(((GenericParameter)type).Position);
				return;
			case MetadataType.Array:
				this.WriteArrayTypeSignature((ArrayType)type);
				return;
			case MetadataType.GenericInstance:
				this.WriteGenericInstanceTypeSignature((GenericInstanceType)type);
				return;
			default:
				switch (metadataType)
				{
				case MetadataType.FunctionPointer:
					this.WriteFunctionPointerTypeSignature((FunctionPointerType)type);
					return;
				case MetadataType.MVar:
					this.id.Append('`').Append('`');
					this.id.Append(((GenericParameter)type).Position);
					return;
				case MetadataType.RequiredModifier:
					this.WriteModiferTypeSignature((RequiredModifierType)type, '|');
					return;
				case MetadataType.OptionalModifier:
					this.WriteModiferTypeSignature((OptionalModifierType)type, '!');
					return;
				}
				break;
			}
			this.WriteTypeFullName(type, false);
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00046AFC File Offset: 0x00044CFC
		private void WriteGenericInstanceTypeSignature(GenericInstanceType type)
		{
			if (type.ElementType.IsTypeSpecification())
			{
				throw new NotSupportedException();
			}
			this.WriteTypeFullName(type.ElementType, true);
			this.id.Append('{');
			this.WriteList<TypeReference>(type.GenericArguments, new Action<TypeReference>(this.WriteTypeSignature));
			this.id.Append('}');
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00046B60 File Offset: 0x00044D60
		private void WriteList<T>(IList<T> list, Action<T> action)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (i > 0)
				{
					this.id.Append(',');
				}
				action(list[i]);
			}
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00046B9D File Offset: 0x00044D9D
		private void WriteModiferTypeSignature(IModifierType type, char id)
		{
			this.WriteTypeSignature(type.ElementType);
			this.id.Append(id);
			this.WriteTypeSignature(type.ModifierType);
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00046BC4 File Offset: 0x00044DC4
		private void WriteFunctionPointerTypeSignature(FunctionPointerType type)
		{
			this.id.Append("=FUNC:");
			this.WriteTypeSignature(type.ReturnType);
			if (type.HasParameters)
			{
				this.WriteParameters(type.Parameters);
			}
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00046BF8 File Offset: 0x00044DF8
		private void WriteArrayTypeSignature(ArrayType type)
		{
			this.WriteTypeSignature(type.ElementType);
			if (type.IsVector)
			{
				this.id.Append("[]");
				return;
			}
			this.id.Append("[");
			this.WriteList<ArrayDimension>(type.Dimensions, delegate(ArrayDimension dimension)
			{
				if (dimension.LowerBound != null)
				{
					this.id.Append(dimension.LowerBound.Value);
				}
				this.id.Append(':');
				if (dimension.UpperBound != null)
				{
					this.id.Append(dimension.UpperBound.Value - (dimension.LowerBound.GetValueOrDefault() + 1));
				}
			});
			this.id.Append("]");
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00046C65 File Offset: 0x00044E65
		private void WriteDefinition(char id, IMemberDefinition member)
		{
			this.id.Append(id).Append(':');
			this.WriteTypeFullName(member.DeclaringType, false);
			this.id.Append('.');
			this.WriteItemName(member.Name);
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00046CA4 File Offset: 0x00044EA4
		private void WriteTypeFullName(TypeReference type, bool stripGenericArity = false)
		{
			if (type.DeclaringType != null)
			{
				this.WriteTypeFullName(type.DeclaringType, false);
				this.id.Append('.');
			}
			if (!string.IsNullOrEmpty(type.Namespace))
			{
				this.id.Append(type.Namespace);
				this.id.Append('.');
			}
			string text = type.Name;
			if (stripGenericArity)
			{
				int num = text.LastIndexOf('`');
				if (num > 0)
				{
					text = text.Substring(0, num);
				}
			}
			this.id.Append(text);
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00046D2E File Offset: 0x00044F2E
		private void WriteItemName(string name)
		{
			this.id.Append(name.Replace('.', '#').Replace('<', '{').Replace('>', '}'));
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x00046D58 File Offset: 0x00044F58
		public override string ToString()
		{
			return this.id.ToString();
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x00046D68 File Offset: 0x00044F68
		public static string GetDocCommentId(IMemberDefinition member)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			DocCommentId docCommentId = new DocCommentId();
			TokenType tokenType = member.MetadataToken.TokenType;
			if (tokenType <= TokenType.Field)
			{
				if (tokenType == TokenType.TypeDef)
				{
					docCommentId.WriteType((TypeDefinition)member);
					goto IL_00A9;
				}
				if (tokenType == TokenType.Field)
				{
					docCommentId.WriteField((FieldDefinition)member);
					goto IL_00A9;
				}
			}
			else
			{
				if (tokenType == TokenType.Method)
				{
					docCommentId.WriteMethod((MethodDefinition)member);
					goto IL_00A9;
				}
				if (tokenType == TokenType.Event)
				{
					docCommentId.WriteEvent((EventDefinition)member);
					goto IL_00A9;
				}
				if (tokenType == TokenType.Property)
				{
					docCommentId.WriteProperty((PropertyDefinition)member);
					goto IL_00A9;
				}
			}
			throw new NotSupportedException(member.FullName);
			IL_00A9:
			return docCommentId.ToString();
		}

		// Token: 0x04000F93 RID: 3987
		private StringBuilder id;
	}
}
