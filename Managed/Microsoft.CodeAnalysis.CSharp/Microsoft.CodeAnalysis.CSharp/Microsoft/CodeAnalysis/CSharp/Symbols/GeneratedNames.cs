using System;
using System.Globalization;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200056E RID: 1390
	internal static class GeneratedNames
	{
		// Token: 0x060074F2 RID: 29938 RVA: 0x001B3F4F File Offset: 0x001B214F
		internal static bool IsGeneratedMemberName(string memberName)
		{
			return memberName.Length > 0 && memberName.get_Chars(0) == '<';
		}

		// Token: 0x060074F3 RID: 29939 RVA: 0x001B3F67 File Offset: 0x001B2167
		internal static string MakeBackingFieldName(string propertyName)
		{
			return "<" + propertyName + ">k__BackingField";
		}

		// Token: 0x060074F4 RID: 29940 RVA: 0x001B3F79 File Offset: 0x001B2179
		internal static string MakeIteratorFinallyMethodName(int iteratorState)
		{
			return "<>m__Finally" + StringExtensions.GetNumeral(Math.Abs(iteratorState + 2));
		}

		// Token: 0x060074F5 RID: 29941 RVA: 0x001B3F92 File Offset: 0x001B2192
		internal static string MakeStaticLambdaDisplayClassName(int methodOrdinal, int generation)
		{
			return GeneratedNames.MakeMethodScopedSynthesizedName(GeneratedNameKind.LambdaDisplayClass, methodOrdinal, generation, null, null, '\0', -1, -1);
		}

		// Token: 0x060074F6 RID: 29942 RVA: 0x001B3FA2 File Offset: 0x001B21A2
		internal static string MakeLambdaDisplayClassName(int methodOrdinal, int generation, int closureOrdinal, int closureGeneration)
		{
			return GeneratedNames.MakeMethodScopedSynthesizedName(GeneratedNameKind.LambdaDisplayClass, methodOrdinal, generation, null, "DisplayClass", '\0', closureOrdinal, closureGeneration);
		}

		// Token: 0x060074F7 RID: 29943 RVA: 0x001B3FB8 File Offset: 0x001B21B8
		internal static string MakeAnonymousTypeTemplateName(int index, int submissionSlotIndex, string moduleId)
		{
			string text = "<" + moduleId + ">f__AnonymousType" + StringExtensions.GetNumeral(index);
			if (submissionSlotIndex >= 0)
			{
				text = text + "#" + StringExtensions.GetNumeral(submissionSlotIndex);
			}
			return text;
		}

		// Token: 0x060074F8 RID: 29944 RVA: 0x001B3FF3 File Offset: 0x001B21F3
		internal static bool TryParseAnonymousTypeTemplateName(string name, out int index)
		{
			if (name.StartsWith("<>f__AnonymousType", 4) && int.TryParse(name.Substring("<>f__AnonymousType".Length), 0, CultureInfo.InvariantCulture, ref index))
			{
				return true;
			}
			index = -1;
			return false;
		}

		// Token: 0x060074F9 RID: 29945 RVA: 0x001B4027 File Offset: 0x001B2227
		internal static string MakeAnonymousTypeBackingFieldName(string propertyName)
		{
			return "<" + propertyName + ">i__Field";
		}

		// Token: 0x060074FA RID: 29946 RVA: 0x001B4039 File Offset: 0x001B2239
		internal static string MakeAnonymousTypeParameterName(string propertyName)
		{
			return "<" + propertyName + ">j__TPar";
		}

		// Token: 0x060074FB RID: 29947 RVA: 0x001B404B File Offset: 0x001B224B
		internal static bool TryParseAnonymousTypeParameterName(string typeParameterName, out string propertyName)
		{
			if (typeParameterName.StartsWith("<", 4) && typeParameterName.EndsWith(">j__TPar", 4))
			{
				propertyName = typeParameterName.Substring(1, typeParameterName.Length - 9);
				return true;
			}
			propertyName = null;
			return false;
		}

		// Token: 0x060074FC RID: 29948 RVA: 0x001B4081 File Offset: 0x001B2281
		internal static string MakeStateMachineTypeName(string methodName, int methodOrdinal, int generation)
		{
			return GeneratedNames.MakeMethodScopedSynthesizedName(GeneratedNameKind.StateMachineType, methodOrdinal, generation, methodName, null, '\0', -1, -1);
		}

		// Token: 0x060074FD RID: 29949 RVA: 0x001B4091 File Offset: 0x001B2291
		internal static string MakeBaseMethodWrapperName(int uniqueId)
		{
			return "<>n__" + StringExtensions.GetNumeral(uniqueId);
		}

		// Token: 0x060074FE RID: 29950 RVA: 0x001B40A3 File Offset: 0x001B22A3
		internal static string MakeLambdaMethodName(string methodName, int methodOrdinal, int methodGeneration, int lambdaOrdinal, int lambdaGeneration)
		{
			return GeneratedNames.MakeMethodScopedSynthesizedName(GeneratedNameKind.LambdaMethod, methodOrdinal, methodGeneration, methodName, null, '\0', lambdaOrdinal, lambdaGeneration);
		}

		// Token: 0x060074FF RID: 29951 RVA: 0x001B40B4 File Offset: 0x001B22B4
		internal static string MakeLambdaCacheFieldName(int methodOrdinal, int generation, int lambdaOrdinal, int lambdaGeneration)
		{
			return GeneratedNames.MakeMethodScopedSynthesizedName(GeneratedNameKind.LambdaCacheField, methodOrdinal, generation, null, null, '\0', lambdaOrdinal, lambdaGeneration);
		}

		// Token: 0x06007500 RID: 29952 RVA: 0x001B40C4 File Offset: 0x001B22C4
		internal static string MakeLocalFunctionName(string methodName, string localFunctionName, int methodOrdinal, int methodGeneration, int lambdaOrdinal, int lambdaGeneration)
		{
			return GeneratedNames.MakeMethodScopedSynthesizedName(GeneratedNameKind.LocalFunction, methodOrdinal, methodGeneration, methodName, localFunctionName, '|', lambdaOrdinal, lambdaGeneration);
		}

		// Token: 0x06007501 RID: 29953 RVA: 0x001B40D8 File Offset: 0x001B22D8
		private static string MakeMethodScopedSynthesizedName(GeneratedNameKind kind, int methodOrdinal, int methodGeneration, string methodNameOpt = null, string suffix = null, char suffixTerminator = '\0', int entityOrdinal = -1, int entityGeneration = -1)
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			builder.Append('<');
			if (methodNameOpt != null)
			{
				builder.Append(methodNameOpt);
				if (kind.IsTypeName())
				{
					builder.Replace('.', '-');
				}
			}
			builder.Append('>');
			builder.Append((char)kind);
			if (suffix != null || methodOrdinal >= 0 || entityOrdinal >= 0)
			{
				builder.Append("__");
				builder.Append(suffix);
				if (suffixTerminator != '\0')
				{
					builder.Append(suffixTerminator);
				}
				if (methodOrdinal >= 0)
				{
					builder.Append(methodOrdinal);
					GeneratedNames.AppendOptionalGeneration(builder, methodGeneration);
				}
				if (entityOrdinal >= 0)
				{
					if (methodOrdinal >= 0)
					{
						builder.Append('_');
					}
					builder.Append(entityOrdinal);
					GeneratedNames.AppendOptionalGeneration(builder, entityGeneration);
				}
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x06007502 RID: 29954 RVA: 0x001B4194 File Offset: 0x001B2394
		private static void AppendOptionalGeneration(StringBuilder builder, int generation)
		{
			if (generation > 0)
			{
				builder.Append('#');
				builder.Append(generation);
			}
		}

		// Token: 0x06007503 RID: 29955 RVA: 0x001B41AC File Offset: 0x001B23AC
		internal static string MakeHoistedLocalFieldName(SynthesizedLocalKind kind, int slotIndex, string localNameOpt = null)
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			builder.Append('<');
			if (localNameOpt != null)
			{
				builder.Append(localNameOpt);
			}
			builder.Append('>');
			if (kind == SynthesizedLocalKind.LambdaDisplayClass)
			{
				builder.Append('8');
			}
			else if (kind == SynthesizedLocalKind.UserDefined)
			{
				builder.Append('5');
			}
			else
			{
				builder.Append('s');
			}
			builder.Append("__");
			builder.Append(slotIndex + 1);
			return instance.ToStringAndFree();
		}

		// Token: 0x06007504 RID: 29956 RVA: 0x001B4224 File Offset: 0x001B2424
		internal static GeneratedNameKind GetKind(string name)
		{
			GeneratedNameKind generatedNameKind;
			int num;
			int num2;
			if (!GeneratedNames.TryParseGeneratedName(name, out generatedNameKind, out num, out num2))
			{
				return GeneratedNameKind.None;
			}
			return generatedNameKind;
		}

		// Token: 0x06007505 RID: 29957 RVA: 0x001B4244 File Offset: 0x001B2444
		internal static bool TryParseGeneratedName(string name, out GeneratedNameKind kind, out int openBracketOffset, out int closeBracketOffset)
		{
			openBracketOffset = -1;
			if (name.StartsWith("CS$<", 4))
			{
				openBracketOffset = 3;
			}
			else if (name.StartsWith("<", 4))
			{
				openBracketOffset = 0;
			}
			if (openBracketOffset >= 0)
			{
				closeBracketOffset = name.IndexOfBalancedParenthesis(openBracketOffset, '>');
				if (closeBracketOffset >= 0 && closeBracketOffset + 1 < name.Length)
				{
					int num = (int)name.get_Chars(closeBracketOffset + 1);
					if ((num >= 49 && num <= 57) || (num >= 97 && num <= 122))
					{
						kind = (GeneratedNameKind)num;
						return true;
					}
				}
			}
			kind = GeneratedNameKind.None;
			openBracketOffset = -1;
			closeBracketOffset = -1;
			return false;
		}

		// Token: 0x06007506 RID: 29958 RVA: 0x001B42C8 File Offset: 0x001B24C8
		internal static bool TryParseSourceMethodNameFromGeneratedName(string generatedName, GeneratedNameKind requiredKind, out string methodName)
		{
			GeneratedNameKind generatedNameKind;
			int num;
			int num2;
			if (!GeneratedNames.TryParseGeneratedName(generatedName, out generatedNameKind, out num, out num2))
			{
				methodName = null;
				return false;
			}
			if (requiredKind != GeneratedNameKind.None && generatedNameKind != requiredKind)
			{
				methodName = null;
				return false;
			}
			methodName = generatedName.Substring(num + 1, num2 - num - 1);
			if (generatedNameKind.IsTypeName())
			{
				methodName = methodName.Replace('-', '.');
			}
			return true;
		}

		// Token: 0x06007507 RID: 29959 RVA: 0x001B431A File Offset: 0x001B251A
		internal static string AsyncAwaiterFieldName(int slotIndex)
		{
			return "<>u__" + StringExtensions.GetNumeral(slotIndex + 1);
		}

		// Token: 0x06007508 RID: 29960 RVA: 0x001B4330 File Offset: 0x001B2530
		internal static bool TryParseSlotIndex(string fieldName, out int slotIndex)
		{
			int num = fieldName.LastIndexOf('_');
			if (num - 1 < 0 || num == fieldName.Length || fieldName.get_Chars(num - 1) != '_')
			{
				slotIndex = -1;
				return false;
			}
			if (int.TryParse(fieldName.Substring(num + 1), 0, CultureInfo.InvariantCulture, ref slotIndex) && slotIndex >= 1)
			{
				slotIndex--;
				return true;
			}
			slotIndex = -1;
			return false;
		}

		// Token: 0x06007509 RID: 29961 RVA: 0x001B438F File Offset: 0x001B258F
		internal static string MakeCachedFrameInstanceFieldName()
		{
			return "<>9";
		}

		// Token: 0x0600750A RID: 29962 RVA: 0x001B4398 File Offset: 0x001B2598
		internal static string MakeSynthesizedLocalName(SynthesizedLocalKind kind, ref int uniqueId)
		{
			if (kind == SynthesizedLocalKind.LambdaDisplayClass)
			{
				int num = uniqueId;
				uniqueId = num + 1;
				return GeneratedNames.MakeLambdaDisplayLocalName(num);
			}
			return null;
		}

		// Token: 0x0600750B RID: 29963 RVA: 0x001B43BA File Offset: 0x001B25BA
		internal static string MakeSynthesizedInstrumentationPayloadLocalFieldName(int uniqueId)
		{
			return "CS$InstrumentationPayload" + StringExtensions.GetNumeral(uniqueId);
		}

		// Token: 0x0600750C RID: 29964 RVA: 0x001B43CC File Offset: 0x001B25CC
		internal static string MakeLambdaDisplayLocalName(int uniqueId)
		{
			return "CS$<>8__locals" + StringExtensions.GetNumeral(uniqueId);
		}

		// Token: 0x0600750D RID: 29965 RVA: 0x001B43DE File Offset: 0x001B25DE
		internal static bool IsSynthesizedLocalName(string name)
		{
			return name.StartsWith("CS$", 4);
		}

		// Token: 0x0600750E RID: 29966 RVA: 0x001B43EC File Offset: 0x001B25EC
		internal static string MakeFixedFieldImplementationName(string fieldName)
		{
			return "<" + fieldName + ">e__FixedBuffer";
		}

		// Token: 0x0600750F RID: 29967 RVA: 0x001B43FE File Offset: 0x001B25FE
		internal static string MakeStateMachineStateFieldName()
		{
			return "<>1__state";
		}

		// Token: 0x06007510 RID: 29968 RVA: 0x001B4405 File Offset: 0x001B2605
		internal static string MakeAsyncIteratorPromiseOfValueOrEndFieldName()
		{
			return "<>v__promiseOfValueOrEnd";
		}

		// Token: 0x06007511 RID: 29969 RVA: 0x001B440C File Offset: 0x001B260C
		internal static string MakeAsyncIteratorCombinedTokensFieldName()
		{
			return "<>x__combinedTokens";
		}

		// Token: 0x06007512 RID: 29970 RVA: 0x001B4413 File Offset: 0x001B2613
		internal static string MakeIteratorCurrentFieldName()
		{
			return "<>2__current";
		}

		// Token: 0x06007513 RID: 29971 RVA: 0x001B441A File Offset: 0x001B261A
		internal static string MakeDisposeModeFieldName()
		{
			return "<>w__disposeMode";
		}

		// Token: 0x06007514 RID: 29972 RVA: 0x001B4421 File Offset: 0x001B2621
		internal static string MakeIteratorCurrentThreadIdFieldName()
		{
			return "<>l__initialThreadId";
		}

		// Token: 0x06007515 RID: 29973 RVA: 0x001B4428 File Offset: 0x001B2628
		internal static string ThisProxyFieldName()
		{
			return "<>4__this";
		}

		// Token: 0x06007516 RID: 29974 RVA: 0x001B442F File Offset: 0x001B262F
		internal static string StateMachineThisParameterProxyName()
		{
			return GeneratedNames.StateMachineParameterProxyFieldName(GeneratedNames.ThisProxyFieldName());
		}

		// Token: 0x06007517 RID: 29975 RVA: 0x001B443B File Offset: 0x001B263B
		internal static string StateMachineParameterProxyFieldName(string parameterName)
		{
			return "<>3__" + parameterName;
		}

		// Token: 0x06007518 RID: 29976 RVA: 0x001B4448 File Offset: 0x001B2648
		internal static string MakeDynamicCallSiteContainerName(int methodOrdinal, int localFunctionOrdinal, int generation)
		{
			return GeneratedNames.MakeMethodScopedSynthesizedName(GeneratedNameKind.DynamicCallSiteContainerType, methodOrdinal, generation, null, (localFunctionOrdinal != -1) ? localFunctionOrdinal.ToString() : null, (localFunctionOrdinal != -1) ? '_' : '\0', -1, -1);
		}

		// Token: 0x06007519 RID: 29977 RVA: 0x001B446D File Offset: 0x001B266D
		internal static string MakeDynamicCallSiteFieldName(int uniqueId)
		{
			return "<>p__" + StringExtensions.GetNumeral(uniqueId);
		}

		// Token: 0x0600751A RID: 29978 RVA: 0x001B4480 File Offset: 0x001B2680
		internal static string MakeDynamicCallSiteDelegateName(BitVector byRefs, bool returnsVoid, int generation)
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			builder.Append(returnsVoid ? "<>A" : "<>F");
			if (!byRefs.IsNull)
			{
				builder.Append("{");
				int num = 0;
				foreach (ulong num2 in byRefs.Words())
				{
					int num3 = (int)num2;
					if (num > 0)
					{
						builder.Append(",");
					}
					builder.AppendFormat("{0:x8}", num3);
					num++;
				}
				builder.Append("}");
			}
			GeneratedNames.AppendOptionalGeneration(builder, generation);
			return instance.ToStringAndFree();
		}

		// Token: 0x0600751B RID: 29979 RVA: 0x001B4544 File Offset: 0x001B2744
		internal static string AsyncBuilderFieldName()
		{
			return "<>t__builder";
		}

		// Token: 0x0600751C RID: 29980 RVA: 0x001B454B File Offset: 0x001B274B
		internal static string ReusableHoistedLocalFieldName(int number)
		{
			return "<>7__wrap" + StringExtensions.GetNumeral(number);
		}

		// Token: 0x0600751D RID: 29981 RVA: 0x001B455D File Offset: 0x001B275D
		internal static string LambdaCopyParameterName(int ordinal)
		{
			return "<p" + StringExtensions.GetNumeral(ordinal) + ">";
		}

		// Token: 0x04001C93 RID: 7315
		internal const string SynthesizedLocalNamePrefix = "CS$";

		// Token: 0x04001C94 RID: 7316
		internal const char DotReplacementInTypeNames = '-';

		// Token: 0x04001C95 RID: 7317
		private const string SuffixSeparator = "__";

		// Token: 0x04001C96 RID: 7318
		private const char IdSeparator = '_';

		// Token: 0x04001C97 RID: 7319
		private const char GenerationSeparator = '#';

		// Token: 0x04001C98 RID: 7320
		private const char LocalFunctionNameTerminator = '|';

		// Token: 0x04001C99 RID: 7321
		internal const string AnonymousNamePrefix = "<>f__AnonymousType";
	}
}
