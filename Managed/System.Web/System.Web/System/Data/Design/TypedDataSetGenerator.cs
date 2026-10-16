using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

namespace System.Data.Design
{
	// Token: 0x0200002E RID: 46
	public sealed class TypedDataSetGenerator
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00002050 File Offset: 0x00000250
		private TypedDataSetGenerator()
		{
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO]
		public static ICollection<Assembly> ReferencedAssemblies
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006040 File Offset: 0x00004240
		public static string Generate(DataSet dataSet, CodeNamespace codeNamespace, CodeDomProvider codeProvider)
		{
			TypedDataSetGenerator.Generate(dataSet, codeNamespace, codeProvider.CreateGenerator());
			return null;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006050 File Offset: 0x00004250
		public static string Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider)
		{
			DataSet dataSet = new DataSet();
			dataSet.ReadXmlSchema(inputFileContent);
			TypedDataSetGenerator.Generate(dataSet, mainNamespace, codeProvider.CreateGenerator());
			return null;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO]
		public static void Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, Hashtable customDBProviders)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO]
		public static void Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, DbProviderFactory specifiedFactory)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO]
		public static string Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, TypedDataSetGenerator.GenerateOption option)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO]
		public static void Generate(string inputFileContent, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeDomProvider codeProvider, Hashtable customDBProviders, TypedDataSetGenerator.GenerateOption option)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO]
		public static string GetProviderName(string inputFileContent)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO]
		public static string GetProviderName(string inputFileContent, string tableName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0200002F RID: 47
		[Flags]
		public enum GenerateOption
		{
			// Token: 0x04000D97 RID: 3479
			None = 0,
			// Token: 0x04000D98 RID: 3480
			HierarchicalUpdate = 1,
			// Token: 0x04000D99 RID: 3481
			LinqOverTypedDatasets = 2
		}
	}
}
