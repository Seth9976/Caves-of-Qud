using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Mono.Cecil;
using MonoMod.Utils;

namespace HarmonyLib
{
	// Token: 0x02000072 RID: 114
	public class InlineSignature : ICallSiteGenerator
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000C489 File Offset: 0x0000A689
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000C491 File Offset: 0x0000A691
		public bool HasThis { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000C49A File Offset: 0x0000A69A
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000C4A2 File Offset: 0x0000A6A2
		public bool ExplicitThis { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000C4AB File Offset: 0x0000A6AB
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000C4B3 File Offset: 0x0000A6B3
		public CallingConvention CallingConvention { get; set; } = CallingConvention.Winapi;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000C4BC File Offset: 0x0000A6BC
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000C4C4 File Offset: 0x0000A6C4
		public List<object> Parameters { get; set; } = new List<object>();

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000C4CD File Offset: 0x0000A6CD
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000C4D5 File Offset: 0x0000A6D5
		public object ReturnType { get; set; } = typeof(void);

		// Token: 0x06000218 RID: 536 RVA: 0x0000C4E0 File Offset: 0x0000A6E0
		public override string ToString()
		{
			Type type = this.ReturnType as Type;
			string text;
			if (type == null)
			{
				object returnType = this.ReturnType;
				text = ((returnType != null) ? returnType.ToString() : null);
			}
			else
			{
				text = type.FullDescription();
			}
			return text + " (" + this.Parameters.Join<object>(delegate(object p)
			{
				Type type2 = p as Type;
				if (type2 != null)
				{
					return type2.FullDescription();
				}
				if (p == null)
				{
					return null;
				}
				return p.ToString();
			}, ", ") + ")";
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000C554 File Offset: 0x0000A754
		internal static TypeReference GetTypeReference(ModuleDefinition module, object param)
		{
			Type type = param as Type;
			TypeReference typeReference;
			if (type == null)
			{
				InlineSignature inlineSignature = param as InlineSignature;
				if (inlineSignature == null)
				{
					InlineSignature.ModifierType modifierType = param as InlineSignature.ModifierType;
					if (modifierType == null)
					{
						throw new NotSupportedException(string.Format("Unsupported inline signature parameter type: {0} ({1})", param, (param != null) ? param.GetType().FullDescription() : null));
					}
					typeReference = modifierType.ToTypeReference(module);
				}
				else
				{
					typeReference = inlineSignature.ToFunctionPointer(module);
				}
			}
			else
			{
				typeReference = module.ImportReference(type);
			}
			return typeReference;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000C5C4 File Offset: 0x0000A7C4
		CallSite ICallSiteGenerator.ToCallSite(ModuleDefinition module)
		{
			CallSite callSite = new CallSite(InlineSignature.GetTypeReference(module, this.ReturnType))
			{
				HasThis = this.HasThis,
				ExplicitThis = this.ExplicitThis,
				CallingConvention = (MethodCallingConvention)((byte)this.CallingConvention - 1)
			};
			foreach (object obj in this.Parameters)
			{
				callSite.Parameters.Add(new ParameterDefinition(InlineSignature.GetTypeReference(module, obj)));
			}
			return callSite;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000C664 File Offset: 0x0000A864
		private FunctionPointerType ToFunctionPointer(ModuleDefinition module)
		{
			FunctionPointerType functionPointerType = new FunctionPointerType
			{
				ReturnType = InlineSignature.GetTypeReference(module, this.ReturnType),
				HasThis = this.HasThis,
				ExplicitThis = this.ExplicitThis,
				CallingConvention = (MethodCallingConvention)((byte)this.CallingConvention - 1)
			};
			foreach (object obj in this.Parameters)
			{
				functionPointerType.Parameters.Add(new ParameterDefinition(InlineSignature.GetTypeReference(module, obj)));
			}
			return functionPointerType;
		}

		// Token: 0x02000073 RID: 115
		public class ModifierType
		{
			// Token: 0x0600021D RID: 541 RVA: 0x0000C734 File Offset: 0x0000A934
			public override string ToString()
			{
				string[] array = new string[6];
				int num = 0;
				Type type = this.Type as Type;
				string text;
				if (type == null)
				{
					object type2 = this.Type;
					text = ((type2 != null) ? type2.ToString() : null);
				}
				else
				{
					text = type.FullDescription();
				}
				array[num] = text;
				array[1] = " mod";
				array[2] = (this.IsOptional ? "opt" : "req");
				array[3] = "(";
				int num2 = 4;
				Type modifier = this.Modifier;
				array[num2] = ((modifier != null) ? modifier.FullDescription() : null);
				array[5] = ")";
				return string.Concat(array);
			}

			// Token: 0x0600021E RID: 542 RVA: 0x0000C7BC File Offset: 0x0000A9BC
			internal TypeReference ToTypeReference(ModuleDefinition module)
			{
				if (this.IsOptional)
				{
					return new OptionalModifierType(module.ImportReference(this.Modifier), InlineSignature.GetTypeReference(module, this.Type));
				}
				return new RequiredModifierType(module.ImportReference(this.Modifier), InlineSignature.GetTypeReference(module, this.Type));
			}

			// Token: 0x04000155 RID: 341
			public bool IsOptional;

			// Token: 0x04000156 RID: 342
			public Type Modifier;

			// Token: 0x04000157 RID: 343
			public object Type;
		}
	}
}
