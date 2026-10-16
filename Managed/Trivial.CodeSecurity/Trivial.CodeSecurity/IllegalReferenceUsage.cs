using System;
using System.Text;
using Trivial.Mono.Cecil;
using Trivial.Mono.Cecil.Cil;

namespace Trivial.CodeSecurity
{
	// Token: 0x0200000D RID: 13
	public struct IllegalReferenceUsage
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002EF6 File Offset: 0x000010F6
		public IllegalReferenceUsage.UsageType UsageContext
		{
			get
			{
				return this.usageType;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002EFE File Offset: 0x000010FE
		public ModuleDefinition UsingModule
		{
			get
			{
				return this.usingModule;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002F06 File Offset: 0x00001106
		public TypeDefinition UsingType
		{
			get
			{
				return this.usingType;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002F0E File Offset: 0x0000110E
		public MethodDefinition UsingMethod
		{
			get
			{
				return this.usingMethod;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002F16 File Offset: 0x00001116
		public MethodBody UsingMethodBody
		{
			get
			{
				return this.usingMethodBody;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002F1E File Offset: 0x0000111E
		public Instruction UsingInstruction
		{
			get
			{
				return this.usingInstruction;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002F26 File Offset: 0x00001126
		public PropertyDefinition UsingProperty
		{
			get
			{
				return this.usingProperty;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002F2E File Offset: 0x0000112E
		public FieldDefinition UsingField
		{
			get
			{
				return this.usingField;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002F36 File Offset: 0x00001136
		public EventDefinition UsingEvent
		{
			get
			{
				return this.usingEvent;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002F3E File Offset: 0x0000113E
		public VariableDefinition UsingLocal
		{
			get
			{
				return this.usingLocal;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002F46 File Offset: 0x00001146
		public ParameterDefinition UsingParameter
		{
			get
			{
				return this.usingParameter;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002F50 File Offset: 0x00001150
		internal IllegalReferenceUsage(ModuleDefinition usingModule)
		{
			this.usageType = IllegalReferenceUsage.UsageType.UsedByModule;
			this.usingModule = usingModule;
			this.usingType = null;
			this.usingMethod = null;
			this.usingMethodBody = null;
			this.usingInstruction = null;
			this.usingProperty = null;
			this.usingField = null;
			this.usingEvent = null;
			this.usingLocal = null;
			this.usingParameter = null;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002FAC File Offset: 0x000011AC
		internal IllegalReferenceUsage(TypeDefinition usingType)
		{
			this.usageType = IllegalReferenceUsage.UsageType.UsedByType;
			this.usingType = usingType;
			this.usingModule = null;
			this.usingMethod = null;
			this.usingMethodBody = null;
			this.usingInstruction = null;
			this.usingProperty = null;
			this.usingField = null;
			this.usingEvent = null;
			this.usingLocal = null;
			this.usingParameter = null;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003008 File Offset: 0x00001208
		internal IllegalReferenceUsage(MethodDefinition usingMethod)
		{
			this.usageType = IllegalReferenceUsage.UsageType.UsedByMethodSignature;
			this.usingType = usingMethod.DeclaringType;
			this.usingMethod = usingMethod;
			this.usingMethodBody = usingMethod.Body;
			this.usingModule = null;
			this.usingInstruction = null;
			this.usingProperty = null;
			this.usingField = null;
			this.usingEvent = null;
			this.usingLocal = null;
			this.usingParameter = null;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000306C File Offset: 0x0000126C
		internal IllegalReferenceUsage(MethodBody usingMethodBody, Instruction usingInstruction)
		{
			this.usageType = IllegalReferenceUsage.UsageType.UsedByMethodBody;
			this.usingType = usingMethodBody.Method.DeclaringType;
			this.usingMethod = usingMethodBody.Method;
			this.usingMethodBody = usingMethodBody;
			this.usingInstruction = usingInstruction;
			this.usingModule = null;
			this.usingProperty = null;
			this.usingField = null;
			this.usingEvent = null;
			this.usingLocal = null;
			this.usingParameter = null;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000030D8 File Offset: 0x000012D8
		internal IllegalReferenceUsage(PropertyDefinition usingProperty)
		{
			this.usageType = IllegalReferenceUsage.UsageType.UsedByPropertySignature;
			this.usingType = usingProperty.DeclaringType;
			this.usingProperty = usingProperty;
			this.usingModule = null;
			this.usingMethod = null;
			this.usingMethodBody = null;
			this.usingInstruction = null;
			this.usingField = null;
			this.usingEvent = null;
			this.usingLocal = null;
			this.usingParameter = null;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003138 File Offset: 0x00001338
		internal IllegalReferenceUsage(FieldDefinition usingField)
		{
			this.usageType = IllegalReferenceUsage.UsageType.UsedByFieldSignature;
			this.usingType = usingField.DeclaringType;
			this.usingField = usingField;
			this.usingModule = null;
			this.usingMethod = null;
			this.usingMethodBody = null;
			this.usingInstruction = null;
			this.usingProperty = null;
			this.usingEvent = null;
			this.usingLocal = null;
			this.usingParameter = null;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003198 File Offset: 0x00001398
		internal IllegalReferenceUsage(EventDefinition usingEvent)
		{
			this.usageType = IllegalReferenceUsage.UsageType.UsedByEventSignature;
			this.usingType = usingEvent.DeclaringType;
			this.usingEvent = usingEvent;
			this.usingModule = null;
			this.usingMethod = null;
			this.usingMethodBody = null;
			this.usingInstruction = null;
			this.usingProperty = null;
			this.usingField = null;
			this.usingLocal = null;
			this.usingParameter = null;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000031F8 File Offset: 0x000013F8
		internal IllegalReferenceUsage(MethodDefinition usingMethod, VariableDefinition usingLocal)
		{
			this.usageType = IllegalReferenceUsage.UsageType.UsedByLocalSignature;
			this.usingType = usingMethod.DeclaringType;
			this.usingMethod = usingMethod;
			this.usingMethodBody = usingMethod.Body;
			this.usingLocal = usingLocal;
			this.usingModule = null;
			this.usingMethod = null;
			this.usingInstruction = null;
			this.usingProperty = null;
			this.usingField = null;
			this.usingEvent = null;
			this.usingParameter = null;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003264 File Offset: 0x00001464
		internal IllegalReferenceUsage(MethodDefinition usingMethod, ParameterDefinition usingParameter)
		{
			this.usageType = IllegalReferenceUsage.UsageType.UsedByParameterSignature;
			this.usingType = usingMethod.DeclaringType;
			this.usingMethod = usingMethod;
			this.usingMethodBody = usingMethod.Body;
			this.usingParameter = usingParameter;
			this.usingModule = null;
			this.usingInstruction = null;
			this.usingProperty = null;
			this.usingField = null;
			this.usingEvent = null;
			this.usingLocal = null;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000032C9 File Offset: 0x000014C9
		public override string ToString()
		{
			return this.GetFullUsageString();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000032D4 File Offset: 0x000014D4
		public string GetFullUsageString()
		{
			switch (this.usageType)
			{
			default:
				IllegalReferenceUsage.builder.Append("Usage context is unknown");
				break;
			case IllegalReferenceUsage.UsageType.UsedByModule:
				IllegalReferenceUsage.builder.Append("Referenced by module assembly references: '");
				IllegalReferenceUsage.builder.Append(this.usingModule);
				IllegalReferenceUsage.builder.Append("'");
				break;
			case IllegalReferenceUsage.UsageType.UsedByType:
				IllegalReferenceUsage.builder.Append("Referenced in type definition: '");
				IllegalReferenceUsage.builder.Append(this.usingType);
				IllegalReferenceUsage.builder.Append("'");
				break;
			case IllegalReferenceUsage.UsageType.UsedByMethodSignature:
				IllegalReferenceUsage.builder.Append("Referenced in method definition signature: '");
				IllegalReferenceUsage.builder.Append(this.usingMethod);
				IllegalReferenceUsage.builder.Append("'");
				break;
			case IllegalReferenceUsage.UsageType.UsedByMethodBody:
				IllegalReferenceUsage.builder.Append("Referenced in method body: '");
				IllegalReferenceUsage.builder.Append(this.usingMethod);
				IllegalReferenceUsage.builder.Append("' at instruction: '");
				IllegalReferenceUsage.builder.Append(IllegalReferenceUsage.GetInstructionString(this.usingInstruction));
				IllegalReferenceUsage.builder.Append("'");
				break;
			case IllegalReferenceUsage.UsageType.UsedByPropertySignature:
				IllegalReferenceUsage.builder.Append("Referenced in property definition signature: '");
				IllegalReferenceUsage.builder.Append(this.usingProperty);
				IllegalReferenceUsage.builder.Append("'");
				break;
			case IllegalReferenceUsage.UsageType.UsedByFieldSignature:
				IllegalReferenceUsage.builder.Append("Referenced in field definition signature: '");
				IllegalReferenceUsage.builder.Append(this.usingField);
				IllegalReferenceUsage.builder.Append("'");
				break;
			case IllegalReferenceUsage.UsageType.UsedByEventSignature:
				IllegalReferenceUsage.builder.Append("Referenced in event definition signature: '");
				IllegalReferenceUsage.builder.Append(this.usingEvent);
				IllegalReferenceUsage.builder.Append("'");
				break;
			case IllegalReferenceUsage.UsageType.UsedByLocalSignature:
				IllegalReferenceUsage.builder.Append("Referenced in method body: '");
				IllegalReferenceUsage.builder.Append(this.usingMethod);
				IllegalReferenceUsage.builder.Append("' at local variable definition: '");
				IllegalReferenceUsage.builder.Append(this.usingLocal.Index);
				IllegalReferenceUsage.builder.Append("' of type: '");
				IllegalReferenceUsage.builder.Append(this.usingMethodBody.Variables[this.usingLocal.Index].VariableType);
				IllegalReferenceUsage.builder.Append("'");
				break;
			case IllegalReferenceUsage.UsageType.UsedByParameterSignature:
				IllegalReferenceUsage.builder.Append("Referenced in method definition signature: '");
				IllegalReferenceUsage.builder.Append(this.usingMethod);
				IllegalReferenceUsage.builder.Append("' as parameter definition: '");
				IllegalReferenceUsage.builder.Append(this.usingParameter);
				IllegalReferenceUsage.builder.Append("' at index: '");
				IllegalReferenceUsage.builder.Append(this.usingParameter.Sequence - 1);
				IllegalReferenceUsage.builder.Append("' of type '");
				IllegalReferenceUsage.builder.Append(this.usingParameter.ParameterType);
				IllegalReferenceUsage.builder.Append("'");
				break;
			}
			if (this.usageType != IllegalReferenceUsage.UsageType.Unknown)
			{
				CodeSecurityLocation usageLocation = this.GetUsageLocation();
				IllegalReferenceUsage.builder.Append(" in source file: '");
				IllegalReferenceUsage.builder.Append(usageLocation.FileLocation);
				IllegalReferenceUsage.builder.Append("'");
				if (usageLocation.LineNumber != -1 && usageLocation.ColumnNumber != -1)
				{
					IllegalReferenceUsage.builder.Append(" at line: '");
					IllegalReferenceUsage.builder.Append(usageLocation.LineNumber);
					IllegalReferenceUsage.builder.Append("', column: '");
					IllegalReferenceUsage.builder.Append(usageLocation.ColumnNumber);
					IllegalReferenceUsage.builder.Append("'");
				}
			}
			string text = IllegalReferenceUsage.builder.ToString();
			IllegalReferenceUsage.builder.Length = 0;
			return text;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000036C0 File Offset: 0x000018C0
		public CodeSecurityLocation GetUsageLocation()
		{
			switch (this.usageType)
			{
			case IllegalReferenceUsage.UsageType.UsedByType:
				return IllegalReferenceUsage.GetLocationForType(this.usingType);
			case IllegalReferenceUsage.UsageType.UsedByMethodSignature:
			case IllegalReferenceUsage.UsageType.UsedByMethodBody:
			case IllegalReferenceUsage.UsageType.UsedByLocalSignature:
			case IllegalReferenceUsage.UsageType.UsedByParameterSignature:
				return IllegalReferenceUsage.GetLocationForMethod(this.usingMethod, this.usingInstruction);
			case IllegalReferenceUsage.UsageType.UsedByPropertySignature:
				return IllegalReferenceUsage.GetLocationForProperty(this.usingProperty);
			case IllegalReferenceUsage.UsageType.UsedByFieldSignature:
				return IllegalReferenceUsage.GetLocationForField(this.usingField);
			case IllegalReferenceUsage.UsageType.UsedByEventSignature:
				return IllegalReferenceUsage.GetLocationForEvent(this.usingEvent);
			default:
				return CodeSecurityLocation.defaultLocation;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003748 File Offset: 0x00001948
		public static string GetInstructionString(Instruction instruction)
		{
			string text = string.Empty;
			OperandType operandType = instruction.OpCode.OperandType;
			if (operandType <= OperandType.InlineMethod)
			{
				if (operandType != OperandType.InlineField && operandType != OperandType.InlineMethod)
				{
					goto IL_0045;
				}
			}
			else if (operandType - OperandType.InlineType > 2 && operandType != OperandType.ShortInlineVar)
			{
				goto IL_0045;
			}
			text = " " + instruction.Operand.ToString();
			IL_0045:
			return string.Format("IL_{0}: {1}{2}", instruction.Offset.ToString("x4"), instruction.OpCode.Code, text);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000037CC File Offset: 0x000019CC
		public static CodeSecurityLocation GetLocationForType(TypeDefinition type)
		{
			foreach (MethodDefinition methodDefinition in type.Methods)
			{
				if (methodDefinition.DebugInformation.HasSequencePoints)
				{
					return new CodeSecurityLocation(methodDefinition.DebugInformation.SequencePoints[0].Document.Url);
				}
			}
			return CodeSecurityLocation.defaultLocation;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003850 File Offset: 0x00001A50
		public static CodeSecurityLocation GetLocationForMethod(MethodDefinition method, Instruction usingInstruction)
		{
			if (usingInstruction != null)
			{
				SequencePoint sequencePoint = method.DebugInformation.GetSequencePoint(usingInstruction);
				if (sequencePoint != null)
				{
					return new CodeSecurityLocation(sequencePoint.Document.Url, sequencePoint.StartLine, sequencePoint.StartColumn);
				}
			}
			if (method.DebugInformation.HasSequencePoints)
			{
				return new CodeSecurityLocation(method.DebugInformation.SequencePoints[0].Document.Url);
			}
			return CodeSecurityLocation.defaultLocation;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000038C0 File Offset: 0x00001AC0
		public static CodeSecurityLocation GetLocationForProperty(PropertyDefinition property)
		{
			MethodDefinition methodDefinition = property.GetMethod;
			if (methodDefinition == null)
			{
				methodDefinition = property.SetMethod;
			}
			if (methodDefinition != null && methodDefinition.HasBody && methodDefinition.DebugInformation.HasSequencePoints)
			{
				return new CodeSecurityLocation(methodDefinition.DebugInformation.SequencePoints[0].Document.Url);
			}
			return CodeSecurityLocation.defaultLocation;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000391C File Offset: 0x00001B1C
		public static CodeSecurityLocation GetLocationForField(FieldDefinition field)
		{
			return IllegalReferenceUsage.GetLocationForType(field.DeclaringType);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003929 File Offset: 0x00001B29
		public static CodeSecurityLocation GetLocationForEvent(EventDefinition evt)
		{
			return IllegalReferenceUsage.GetLocationForType(evt.DeclaringType);
		}

		// Token: 0x04000028 RID: 40
		private static StringBuilder builder = new StringBuilder();

		// Token: 0x04000029 RID: 41
		private IllegalReferenceUsage.UsageType usageType;

		// Token: 0x0400002A RID: 42
		private ModuleDefinition usingModule;

		// Token: 0x0400002B RID: 43
		private TypeDefinition usingType;

		// Token: 0x0400002C RID: 44
		private MethodDefinition usingMethod;

		// Token: 0x0400002D RID: 45
		private MethodBody usingMethodBody;

		// Token: 0x0400002E RID: 46
		private Instruction usingInstruction;

		// Token: 0x0400002F RID: 47
		private PropertyDefinition usingProperty;

		// Token: 0x04000030 RID: 48
		private FieldDefinition usingField;

		// Token: 0x04000031 RID: 49
		private EventDefinition usingEvent;

		// Token: 0x04000032 RID: 50
		private VariableDefinition usingLocal;

		// Token: 0x04000033 RID: 51
		private ParameterDefinition usingParameter;

		// Token: 0x02000021 RID: 33
		public enum UsageType
		{
			// Token: 0x04000073 RID: 115
			Unknown,
			// Token: 0x04000074 RID: 116
			UsedByModule,
			// Token: 0x04000075 RID: 117
			UsedByType,
			// Token: 0x04000076 RID: 118
			UsedByMethodSignature,
			// Token: 0x04000077 RID: 119
			UsedByMethodBody,
			// Token: 0x04000078 RID: 120
			UsedByPropertySignature,
			// Token: 0x04000079 RID: 121
			UsedByFieldSignature,
			// Token: 0x0400007A RID: 122
			UsedByEventSignature,
			// Token: 0x0400007B RID: 123
			UsedByLocalSignature,
			// Token: 0x0400007C RID: 124
			UsedByParameterSignature
		}
	}
}
