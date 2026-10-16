using System;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001E1 RID: 481
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class GeneratorDriver
	{
		// Token: 0x060013E2 RID: 5090 RVA: 0x0003E09C File Offset: 0x0003C29C
		internal GeneratorDriver(GeneratorDriverState state)
		{
			this._state = state;
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0003E0AB File Offset: 0x0003C2AB
		internal GeneratorDriver(ParseOptions parseOptions, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ISourceGenerator> generators, AnalyzerConfigOptionsProvider optionsProvider, [Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalTexts)
		{
			this._state = new GeneratorDriverState(parseOptions, optionsProvider, generators, additionalTexts, ImmutableArray.Create<GeneratorState>(new GeneratorState[generators.Length]), ImmutableArray<PendingEdit>.Empty, true);
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0003E0DC File Offset: 0x0003C2DC
		public GeneratorDriver RunGenerators(Compilation compilation, CancellationToken cancellationToken = default(CancellationToken))
		{
			GeneratorDriverState generatorDriverState = this.RunGeneratorsCore(compilation, null, cancellationToken);
			return this.FromState(generatorDriverState);
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0003E0FC File Offset: 0x0003C2FC
		public GeneratorDriver RunGeneratorsAndUpdateCompilation(Compilation compilation, out Compilation outputCompilation, [Nullable(new byte[] { 0, 1 })] out ImmutableArray<Diagnostic> diagnostics, CancellationToken cancellationToken = default(CancellationToken))
		{
			DiagnosticBag instance = DiagnosticBag.GetInstance();
			GeneratorDriverState generatorDriverState = this.RunGeneratorsCore(compilation, instance, cancellationToken);
			diagnostics = instance.ToReadOnlyAndFree();
			return this.BuildFinalCompilation(compilation, out outputCompilation, generatorDriverState, cancellationToken);
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0003E134 File Offset: 0x0003C334
		internal GeneratorDriver TryApplyEdits(Compilation compilation, out Compilation outputCompilation, out bool success, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this._state.EditsFailed || this._state.Edits.Length == 0)
			{
				outputCompilation = compilation;
				success = !this._state.EditsFailed;
				return this;
			}
			GeneratorDriverState generatorDriverState = this._state;
			foreach (PendingEdit pendingEdit in this._state.Edits)
			{
				generatorDriverState = this.ApplyPartialEdit(generatorDriverState, pendingEdit, cancellationToken);
				if (generatorDriverState.EditsFailed)
				{
					outputCompilation = compilation;
					success = false;
					return this;
				}
			}
			compilation = GeneratorDriver.RemoveGeneratedSyntaxTrees(this._state, compilation);
			success = true;
			return this.BuildFinalCompilation(compilation, out outputCompilation, generatorDriverState, cancellationToken);
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0003E1E0 File Offset: 0x0003C3E0
		public GeneratorDriver AddGenerators([Nullable(new byte[] { 0, 1 })] ImmutableArray<ISourceGenerator> generators)
		{
			ImmutableArray<ISourceGenerator>? immutableArray = new ImmutableArray<ISourceGenerator>?(this._state.Generators.AddRange(generators));
			ImmutableArray<GeneratorState>? immutableArray2 = new ImmutableArray<GeneratorState>?(this._state.GeneratorStates.AddRange(new GeneratorState[generators.Length]));
			bool? flag = new bool?(true);
			GeneratorDriverState generatorDriverState = this._state.With(immutableArray, immutableArray2, default(ImmutableArray<AdditionalText>?), default(ImmutableArray<PendingEdit>?), flag);
			return this.FromState(generatorDriverState);
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0003E25C File Offset: 0x0003C45C
		public GeneratorDriver RemoveGenerators([Nullable(new byte[] { 0, 1 })] ImmutableArray<ISourceGenerator> generators)
		{
			ImmutableArray<ISourceGenerator> immutableArray = this._state.Generators;
			ImmutableArray<GeneratorState> immutableArray2 = this._state.GeneratorStates;
			for (int i = 0; i < immutableArray.Length; i++)
			{
				if (generators.Contains(immutableArray[i]))
				{
					immutableArray = immutableArray.RemoveAt(i);
					immutableArray2 = immutableArray2.RemoveAt(i);
					i--;
				}
			}
			return this.FromState(this._state.With(new ImmutableArray<ISourceGenerator>?(immutableArray), new ImmutableArray<GeneratorState>?(immutableArray2), default(ImmutableArray<AdditionalText>?), default(ImmutableArray<PendingEdit>?), default(bool?)));
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public GeneratorDriver AddAdditionalTexts([Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalTexts)
		{
			ImmutableArray<AdditionalText>? immutableArray = new ImmutableArray<AdditionalText>?(this._state.AdditionalTexts.AddRange(additionalTexts));
			GeneratorDriverState generatorDriverState = this._state.With(default(ImmutableArray<ISourceGenerator>?), default(ImmutableArray<GeneratorState>?), immutableArray, default(ImmutableArray<PendingEdit>?), default(bool?));
			return this.FromState(generatorDriverState);
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0003E358 File Offset: 0x0003C558
		public GeneratorDriver RemoveAdditionalTexts([Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalTexts)
		{
			ImmutableArray<AdditionalText>? immutableArray = new ImmutableArray<AdditionalText>?(this._state.AdditionalTexts.RemoveRange(additionalTexts));
			GeneratorDriverState generatorDriverState = this._state.With(default(ImmutableArray<ISourceGenerator>?), default(ImmutableArray<GeneratorState>?), immutableArray, default(ImmutableArray<PendingEdit>?), default(bool?));
			return this.FromState(generatorDriverState);
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0003E3BC File Offset: 0x0003C5BC
		public GeneratorDriverRunResult GetRunResult()
		{
			return new GeneratorDriverRunResult(this._state.Generators.ZipAsArray<ISourceGenerator, GeneratorState, GeneratorRunResult>(this._state.GeneratorStates, delegate(ISourceGenerator generator, GeneratorState generatorState)
			{
				ImmutableArray<Diagnostic> diagnostics = generatorState.Diagnostics;
				Exception exception = generatorState.Exception;
				return new GeneratorRunResult(generator, GeneratorDriver.<GetRunResult>g__getGeneratorSources|10_1(generatorState), diagnostics, exception);
			}));
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0003E408 File Offset: 0x0003C608
		internal GeneratorDriverState RunGeneratorsCore(Compilation compilation, [Nullable(2)] DiagnosticBag diagnosticsBag, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this._state.Generators.IsEmpty)
			{
				return this._state;
			}
			GeneratorDriverState generatorDriverState = GeneratorDriver.StateWithPendingEditsApplied(this._state);
			ArrayBuilder<GeneratorState> instance = ArrayBuilder<GeneratorState>.GetInstance(generatorDriverState.Generators.Length);
			ArrayBuilder<SyntaxTree> instance2 = ArrayBuilder<SyntaxTree>.GetInstance();
			ArrayBuilder<GeneratorSyntaxWalker> instance3 = ArrayBuilder<GeneratorSyntaxWalker>.GetInstance(generatorDriverState.Generators.Length, null);
			int num = 0;
			for (int i = 0; i < generatorDriverState.Generators.Length; i++)
			{
				ISourceGenerator sourceGenerator = generatorDriverState.Generators[i];
				GeneratorState generatorState = generatorDriverState.GeneratorStates[i];
				if (!generatorState.Info.Initialized)
				{
					GeneratorInitializationContext generatorInitializationContext = new GeneratorInitializationContext(cancellationToken);
					Exception ex = null;
					try
					{
						sourceGenerator.Initialize(generatorInitializationContext);
					}
					catch (Exception ex2) when (FatalError.ReportAndCatchUnlessCanceled(ex2, cancellationToken))
					{
						ex = ex2;
					}
					generatorState = ((ex == null) ? new GeneratorState(generatorInitializationContext.InfoBuilder.ToImmutable()) : GeneratorDriver.SetGeneratorException(this.MessageProvider, GeneratorState.Uninitialized, sourceGenerator, ex, diagnosticsBag, true));
					if (generatorState.Info.PostInitCallback != null)
					{
						AdditionalSourcesCollection additionalSourcesCollection = this.CreateSourcesCollection();
						GeneratorPostInitializationContext generatorPostInitializationContext = new GeneratorPostInitializationContext(additionalSourcesCollection, cancellationToken);
						try
						{
							generatorState.Info.PostInitCallback.Invoke(generatorPostInitializationContext);
						}
						catch (Exception ex)
						{
						}
						generatorState = ((ex == null) ? new GeneratorState(generatorState.Info, this.ParseAdditionalSources(sourceGenerator, additionalSourcesCollection.ToImmutableAndFree(), cancellationToken)) : GeneratorDriver.SetGeneratorException(this.MessageProvider, generatorState, sourceGenerator, ex, diagnosticsBag, true));
					}
				}
				if (generatorState.Info.SyntaxContextReceiverCreator != null && generatorState.Exception == null)
				{
					ISyntaxContextReceiver syntaxContextReceiver = null;
					try
					{
						syntaxContextReceiver = generatorState.Info.SyntaxContextReceiverCreator();
					}
					catch (Exception ex3)
					{
						generatorState = GeneratorDriver.SetGeneratorException(this.MessageProvider, generatorState, sourceGenerator, ex3, diagnosticsBag, false);
					}
					if (syntaxContextReceiver != null)
					{
						instance3.SetItem(i, new GeneratorSyntaxWalker(syntaxContextReceiver));
						generatorState = generatorState.WithReceiver(syntaxContextReceiver);
						num++;
					}
				}
				if (generatorState.Exception == null && generatorState.PostInitTrees.Length > 0)
				{
					instance2.AddRange(generatorState.PostInitTrees.Select<GeneratedSyntaxTree, SyntaxTree>((GeneratedSyntaxTree t) => t.Tree));
				}
				instance.Add(generatorState);
			}
			if (instance2.Count > 0)
			{
				compilation = compilation.AddSyntaxTrees(instance2);
			}
			instance2.Free();
			if (num > 0)
			{
				foreach (SyntaxTree syntaxTree in compilation.SyntaxTrees)
				{
					SyntaxNode root = syntaxTree.GetRoot(cancellationToken);
					SemanticModel semanticModel = compilation.GetSemanticModel(syntaxTree, false);
					for (int j = 0; j < instance3.Count; j++)
					{
						GeneratorSyntaxWalker generatorSyntaxWalker = instance3[j];
						if (generatorSyntaxWalker != null)
						{
							try
							{
								generatorSyntaxWalker.VisitWithModel(semanticModel, root);
							}
							catch (Exception ex4)
							{
								instance[j] = GeneratorDriver.SetGeneratorException(this.MessageProvider, instance[j], generatorDriverState.Generators[j], ex4, diagnosticsBag, false);
								instance3.SetItem(j, null);
							}
						}
					}
				}
			}
			instance3.Free();
			for (int k = 0; k < generatorDriverState.Generators.Length; k++)
			{
				ISourceGenerator sourceGenerator2 = generatorDriverState.Generators[k];
				GeneratorState generatorState2 = instance[k];
				if (generatorState2.Exception == null)
				{
					GeneratorExecutionContext generatorExecutionContext = new GeneratorExecutionContext(compilation, generatorDriverState.ParseOptions, generatorDriverState.AdditionalTexts.NullToEmpty<AdditionalText>(), generatorDriverState.OptionsProvider, generatorState2.SyntaxReceiver, this.CreateSourcesCollection(), cancellationToken);
					try
					{
						sourceGenerator2.Execute(generatorExecutionContext);
					}
					catch (Exception ex5) when (FatalError.ReportAndCatchUnlessCanceled(ex5, cancellationToken))
					{
						instance[k] = GeneratorDriver.SetGeneratorException(this.MessageProvider, generatorState2, sourceGenerator2, ex5, diagnosticsBag, false);
						goto IL_042D;
					}
					ValueTuple<ImmutableArray<GeneratedSourceText>, ImmutableArray<Diagnostic>> valueTuple = generatorExecutionContext.ToImmutableAndFree();
					ImmutableArray<GeneratedSourceText> item = valueTuple.Item1;
					ImmutableArray<Diagnostic> immutableArray = valueTuple.Item2;
					immutableArray = GeneratorDriver.FilterDiagnostics(compilation, immutableArray, diagnosticsBag, cancellationToken);
					instance[k] = new GeneratorState(generatorState2.Info, generatorState2.PostInitTrees, this.ParseAdditionalSources(sourceGenerator2, item, cancellationToken), immutableArray);
				}
				IL_042D:;
			}
			ImmutableArray<GeneratorState>? immutableArray2 = new ImmutableArray<GeneratorState>?(instance.ToImmutableAndFree());
			generatorDriverState = generatorDriverState.With(default(ImmutableArray<ISourceGenerator>?), immutableArray2, default(ImmutableArray<AdditionalText>?), default(ImmutableArray<PendingEdit>?), default(bool?));
			return generatorDriverState;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0003E8EC File Offset: 0x0003CAEC
		internal GeneratorDriver WithPendingEdits([Nullable(new byte[] { 0, 1 })] ImmutableArray<PendingEdit> edits)
		{
			ImmutableArray<PendingEdit>? immutableArray = new ImmutableArray<PendingEdit>?(this._state.Edits.AddRange(edits));
			GeneratorDriverState generatorDriverState = this._state.With(default(ImmutableArray<ISourceGenerator>?), default(ImmutableArray<GeneratorState>?), default(ImmutableArray<AdditionalText>?), immutableArray, default(bool?));
			return this.FromState(generatorDriverState);
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0003E950 File Offset: 0x0003CB50
		private GeneratorDriverState ApplyPartialEdit(GeneratorDriverState state, PendingEdit edit, CancellationToken cancellationToken = default(CancellationToken))
		{
			GeneratorDriverState generatorDriverState = state;
			PooledDictionary<ISourceGenerator, GeneratorState>.GetInstance();
			for (int i = 0; i < generatorDriverState.Generators.Length; i++)
			{
				ISourceGenerator sourceGenerator = generatorDriverState.Generators[i];
				GeneratorState generatorState = generatorDriverState.GeneratorStates[i];
				if (edit.AcceptedBy(generatorState.Info))
				{
					AdditionalSourcesCollection additionalSourcesCollection = this.CreateSourcesCollection();
					additionalSourcesCollection.AddRange(generatorState.GeneratedTrees);
					GeneratorEditContext generatorEditContext = new GeneratorEditContext(additionalSourcesCollection, cancellationToken);
					bool? flag;
					ImmutableArray<GeneratorState>? immutableArray2;
					if (!edit.TryApply(generatorState.Info, generatorEditContext))
					{
						flag = new bool?(true);
						ImmutableArray<ISourceGenerator>? immutableArray = default(ImmutableArray<ISourceGenerator>?);
						immutableArray2 = default(ImmutableArray<GeneratorState>?);
						return generatorDriverState.With(immutableArray, immutableArray2, default(ImmutableArray<AdditionalText>?), default(ImmutableArray<PendingEdit>?), flag);
					}
					ImmutableArray<GeneratedSourceText> immutableArray3 = additionalSourcesCollection.ToImmutableAndFree();
					immutableArray2 = new ImmutableArray<GeneratorState>?(state.GeneratorStates.SetItem(i, new GeneratorState(generatorState.Info, generatorState.PostInitTrees, this.ParseAdditionalSources(sourceGenerator, immutableArray3, cancellationToken), ImmutableArray<Diagnostic>.Empty)));
					ImmutableArray<ISourceGenerator>? immutableArray4 = default(ImmutableArray<ISourceGenerator>?);
					ImmutableArray<GeneratorState>? immutableArray5 = immutableArray2;
					ImmutableArray<AdditionalText>? immutableArray6 = default(ImmutableArray<AdditionalText>?);
					ImmutableArray<PendingEdit>? immutableArray7 = default(ImmutableArray<PendingEdit>?);
					flag = default(bool?);
					state = state.With(immutableArray4, immutableArray5, immutableArray6, immutableArray7, flag);
				}
			}
			state = edit.Commit(state);
			return state;
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0003EAA4 File Offset: 0x0003CCA4
		private static GeneratorDriverState StateWithPendingEditsApplied(GeneratorDriverState state)
		{
			if (state.Edits.Length == 0)
			{
				return state;
			}
			foreach (PendingEdit pendingEdit in state.Edits)
			{
				state = pendingEdit.Commit(state);
			}
			ImmutableArray<PendingEdit>? immutableArray = new ImmutableArray<PendingEdit>?(ImmutableArray<PendingEdit>.Empty);
			bool? flag = new bool?(false);
			return state.With(default(ImmutableArray<ISourceGenerator>?), default(ImmutableArray<GeneratorState>?), default(ImmutableArray<AdditionalText>?), immutableArray, flag);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0003EB28 File Offset: 0x0003CD28
		private static Compilation RemoveGeneratedSyntaxTrees(GeneratorDriverState state, Compilation compilation)
		{
			ArrayBuilder<SyntaxTree> instance = ArrayBuilder<SyntaxTree>.GetInstance();
			foreach (GeneratorState generatorState in state.GeneratorStates)
			{
				foreach (GeneratedSyntaxTree generatedSyntaxTree in generatorState.GeneratedTrees)
				{
					if (generatedSyntaxTree.Tree != null && compilation.ContainsSyntaxTree(generatedSyntaxTree.Tree))
					{
						instance.Add(generatedSyntaxTree.Tree);
					}
				}
			}
			Compilation compilation2 = compilation.RemoveSyntaxTrees(instance);
			instance.Free();
			return compilation2;
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0003EBB8 File Offset: 0x0003CDB8
		[NullableContext(0)]
		private ImmutableArray<GeneratedSyntaxTree> ParseAdditionalSources([Nullable(1)] ISourceGenerator generator, ImmutableArray<GeneratedSourceText> generatedSources, CancellationToken cancellationToken)
		{
			ArrayBuilder<GeneratedSyntaxTree> instance = ArrayBuilder<GeneratedSyntaxTree>.GetInstance(generatedSources.Length);
			generator.GetType();
			string filePathPrefixForGenerator = GeneratorDriver.GetFilePathPrefixForGenerator(generator);
			foreach (GeneratedSourceText generatedSourceText in generatedSources)
			{
				SyntaxTree syntaxTree = this.ParseGeneratedSourceText(generatedSourceText, Path.Combine(filePathPrefixForGenerator, generatedSourceText.HintName), cancellationToken);
				instance.Add(new GeneratedSyntaxTree(generatedSourceText.HintName, generatedSourceText.Text, syntaxTree));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0003EC34 File Offset: 0x0003CE34
		private GeneratorDriver BuildFinalCompilation(Compilation compilation, out Compilation outputCompilation, GeneratorDriverState state, CancellationToken cancellationToken)
		{
			ArrayBuilder<SyntaxTree> instance = ArrayBuilder<SyntaxTree>.GetInstance();
			foreach (GeneratorState generatorState in state.GeneratorStates)
			{
				instance.AddRange(generatorState.PostInitTrees.Select<GeneratedSyntaxTree, SyntaxTree>((GeneratedSyntaxTree t) => t.Tree));
				instance.AddRange(generatorState.GeneratedTrees.Select<GeneratedSyntaxTree, SyntaxTree>((GeneratedSyntaxTree t) => t.Tree));
			}
			outputCompilation = compilation.AddSyntaxTrees(instance);
			instance.Free();
			ImmutableArray<PendingEdit>? immutableArray = new ImmutableArray<PendingEdit>?(ImmutableArray<PendingEdit>.Empty);
			bool? flag = new bool?(false);
			state = state.With(default(ImmutableArray<ISourceGenerator>?), default(ImmutableArray<GeneratorState>?), default(ImmutableArray<AdditionalText>?), immutableArray, flag);
			return this.FromState(state);
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0003ED20 File Offset: 0x0003CF20
		private static GeneratorState SetGeneratorException(CommonMessageProvider provider, GeneratorState generatorState, ISourceGenerator generator, Exception e, [Nullable(2)] DiagnosticBag diagnosticBag, bool isInit = false)
		{
			int num = (isInit ? provider.WRN_GeneratorFailedDuringInitialization : provider.WRN_GeneratorFailedDuringGeneration);
			string text = string.Format(provider.GetDescription(num).ToString(CultureInfo.CurrentUICulture), e);
			Diagnostic diagnostic = Diagnostic.Create(new DiagnosticDescriptor(provider.GetIdForErrorCode(num), provider.GetTitle(num), provider.GetMessageFormat(num), "Compiler", DiagnosticSeverity.Warning, true, text, null, new string[] { "AnalyzerException" }), Location.None, new object[]
			{
				generator.GetType().Name,
				e.GetType().Name,
				e.Message
			});
			if (diagnosticBag != null)
			{
				diagnosticBag.Add(diagnostic);
			}
			return new GeneratorState(generatorState.Info, e, diagnostic);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0003EDE0 File Offset: 0x0003CFE0
		[return: Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<Diagnostic> FilterDiagnostics(Compilation compilation, [Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> generatorDiagnostics, [Nullable(2)] DiagnosticBag driverDiagnostics, CancellationToken cancellationToken)
		{
			ArrayBuilder<Diagnostic> instance = ArrayBuilder<Diagnostic>.GetInstance();
			foreach (Diagnostic diagnostic in generatorDiagnostics)
			{
				Diagnostic diagnostic2 = compilation.Options.FilterDiagnostic(diagnostic, cancellationToken);
				if (diagnostic2 != null)
				{
					instance.Add(diagnostic2);
					if (driverDiagnostics != null)
					{
						driverDiagnostics.Add(diagnostic2);
					}
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0003EE38 File Offset: 0x0003D038
		internal static string GetFilePathPrefixForGenerator(ISourceGenerator generator)
		{
			Type type = generator.GetType();
			return Path.Combine(type.Assembly.GetName().Name ?? string.Empty, type.FullName);
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060013F6 RID: 5110
		internal abstract CommonMessageProvider MessageProvider { get; }

		// Token: 0x060013F7 RID: 5111
		internal abstract GeneratorDriver FromState(GeneratorDriverState state);

		// Token: 0x060013F8 RID: 5112
		internal abstract SyntaxTree ParseGeneratedSourceText(GeneratedSourceText input, string fileName, CancellationToken cancellationToken);

		// Token: 0x060013F9 RID: 5113
		internal abstract AdditionalSourcesCollection CreateSourcesCollection();

		// Token: 0x060013FA RID: 5114 RVA: 0x0003EE70 File Offset: 0x0003D070
		[NullableContext(0)]
		[CompilerGenerated]
		internal static ImmutableArray<GeneratedSourceResult> <GetRunResult>g__getGeneratorSources|10_1(GeneratorState generatorState)
		{
			ArrayBuilder<GeneratedSourceResult> instance = ArrayBuilder<GeneratedSourceResult>.GetInstance(generatorState.PostInitTrees.Length + generatorState.GeneratedTrees.Length);
			foreach (GeneratedSyntaxTree generatedSyntaxTree in generatorState.PostInitTrees)
			{
				instance.Add(new GeneratedSourceResult(generatedSyntaxTree.Tree, generatedSyntaxTree.Text, generatedSyntaxTree.HintName));
			}
			foreach (GeneratedSyntaxTree generatedSyntaxTree2 in generatorState.GeneratedTrees)
			{
				instance.Add(new GeneratedSourceResult(generatedSyntaxTree2.Tree, generatedSyntaxTree2.Text, generatedSyntaxTree2.HintName));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x04000611 RID: 1553
		internal readonly GeneratorDriverState _state;
	}
}
