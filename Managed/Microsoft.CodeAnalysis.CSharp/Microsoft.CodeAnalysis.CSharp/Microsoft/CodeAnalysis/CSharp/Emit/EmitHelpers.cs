using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000484 RID: 1156
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class EmitHelpers
	{
		// Token: 0x06006078 RID: 24696 RVA: 0x00174E34 File Offset: 0x00173034
		internal static EmitDifferenceResult EmitDifference(CSharpCompilation compilation, EmitBaseline baseline, IEnumerable<SemanticEdit> edits, Func<ISymbol, bool> isAddedSymbol, Stream metadataStream, Stream ilStream, Stream pdbStream, ICollection<MethodDefinitionHandle> updatedMethods, [global::System.Runtime.CompilerServices.Nullable(2)] CompilationTestData testData, CancellationToken cancellationToken)
		{
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			EmitOptions emitOptions = EmitOptions.Default.WithDebugInformationFormat(baseline.HasPortablePdb ? DebugInformationFormat.PortablePdb : DebugInformationFormat.Pdb);
			string runtimeMetadataVersion = compilation.GetRuntimeMetadataVersion(emitOptions, instance);
			ModulePropertiesForSerialization modulePropertiesForSerialization = compilation.ConstructModuleSerializationProperties(emitOptions, runtimeMetadataVersion, baseline.ModuleVersionId);
			IEnumerable<ResourceDescription> enumerable = SpecializedCollections.EmptyEnumerable<ResourceDescription>();
			PEDeltaAssemblyBuilder pedeltaAssemblyBuilder;
			try
			{
				pedeltaAssemblyBuilder = new PEDeltaAssemblyBuilder(compilation.SourceAssembly, emitOptions, compilation.Options.OutputKind, modulePropertiesForSerialization, enumerable, baseline, edits, isAddedSymbol);
			}
			catch (NotSupportedException ex)
			{
				instance.Add(ErrorCode.ERR_ModuleEmitFailure, NoLocation.Singleton, new object[] { compilation.AssemblyName, ex.Message });
				return new EmitDifferenceResult(false, instance.ToReadOnlyAndFree(), null);
			}
			if (testData != null)
			{
				pedeltaAssemblyBuilder.SetMethodTestData(testData.Methods);
				testData.Module = pedeltaAssemblyBuilder;
			}
			CSharpDefinitionMap previousDefinitions = pedeltaAssemblyBuilder.PreviousDefinitions;
			SymbolChanges changes = pedeltaAssemblyBuilder.Changes;
			EmitBaseline emitBaseline = null;
			if (compilation.Compile(pedeltaAssemblyBuilder, true, instance, (ISymbolInternal s) => changes.RequiresCompilation(s.GetISymbol()), cancellationToken))
			{
				EmitBaseline emitBaseline2 = EmitHelpers.MapToCompilation(compilation, pedeltaAssemblyBuilder);
				emitBaseline = compilation.SerializeToDeltaStreams(pedeltaAssemblyBuilder, emitBaseline2, previousDefinitions, changes, metadataStream, ilStream, pdbStream, updatedMethods, instance, (testData != null) ? testData.SymWriterFactory : null, emitOptions.PdbFilePath, cancellationToken);
			}
			return new EmitDifferenceResult(emitBaseline != null, instance.ToReadOnlyAndFree(), emitBaseline);
		}

		// Token: 0x06006079 RID: 24697 RVA: 0x00174F98 File Offset: 0x00173198
		private static EmitBaseline MapToCompilation(CSharpCompilation compilation, PEDeltaAssemblyBuilder moduleBeingBuilt)
		{
			EmitBaseline previousGeneration = moduleBeingBuilt.PreviousGeneration;
			if (previousGeneration.Ordinal == 0)
			{
				return previousGeneration;
			}
			ImmutableDictionary<ISymbolInternal, ImmutableArray<ISymbolInternal>> allSynthesizedMembers = moduleBeingBuilt.GetAllSynthesizedMembers();
			IReadOnlyDictionary<AnonymousTypeKey, AnonymousTypeValue> anonymousTypeMap = moduleBeingBuilt.GetAnonymousTypeMap();
			SourceAssemblySymbol sourceAssembly = ((CSharpCompilation)previousGeneration.Compilation).SourceAssembly;
			EmitContext emitContext = new EmitContext((PEModuleBuilder)previousGeneration.PEModuleBuilder, null, new DiagnosticBag(), false, true);
			EmitContext emitContext2 = new EmitContext(moduleBeingBuilt, null, new DiagnosticBag(), false, true);
			ImmutableDictionary<ISymbolInternal, ImmutableArray<ISymbolInternal>> immutableDictionary = new CSharpSymbolMatcher(anonymousTypeMap, sourceAssembly, emitContext, compilation.SourceAssembly, emitContext2, allSynthesizedMembers).MapSynthesizedMembers(previousGeneration.SynthesizedMembers, allSynthesizedMembers);
			return new CSharpSymbolMatcher(anonymousTypeMap, sourceAssembly, emitContext, compilation.SourceAssembly, emitContext2, immutableDictionary).MapBaselineToCompilation(previousGeneration, compilation, moduleBeingBuilt, immutableDictionary);
		}
	}
}
