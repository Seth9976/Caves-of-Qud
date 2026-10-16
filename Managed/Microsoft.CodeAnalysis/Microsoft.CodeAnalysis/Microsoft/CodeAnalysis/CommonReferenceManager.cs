using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001CA RID: 458
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class CommonReferenceManager<[Nullable(0)] TCompilation, TAssemblySymbol> : CommonReferenceManager where TCompilation : Compilation where TAssemblySymbol : class, IAssemblySymbolInternal
	{
		// Token: 0x0600131E RID: 4894 RVA: 0x0003A6F8 File Offset: 0x000388F8
		[NullableContext(0)]
		[return: Nullable(new byte[] { 1, 0, 0, 0 })]
		protected CommonReferenceManager<TCompilation, TAssemblySymbol>.BoundInputAssembly[] Bind([Nullable(1)] TCompilation compilation, [Nullable(new byte[] { 0, 1, 0, 0 })] ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData> explicitAssemblies, [Nullable(new byte[] { 0, 1 })] ImmutableArray<PEModule> explicitModules, [Nullable(new byte[] { 0, 1 })] ImmutableArray<MetadataReference> explicitReferences, ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference> explicitReferenceMap, [Nullable(2)] MetadataReferenceResolver resolverOpt, MetadataImportOptions importOptions, bool supersedeLowerVersions, [Nullable(new byte[] { 1, 1, 1, 0, 0, 0 })] [In] [Out] Dictionary<string, List<CommonReferenceManager<TCompilation, TAssemblySymbol>.ReferencedAssemblyIdentity>> assemblyReferencesBySimpleName, [Nullable(new byte[] { 0, 1, 0, 0 })] out ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData> allAssemblies, [Nullable(new byte[] { 0, 1 })] out ImmutableArray<MetadataReference> implicitlyResolvedReferences, out ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference> implicitlyResolvedReferenceMap, [Nullable(new byte[] { 1, 1, 2 })] ref ImmutableDictionary<AssemblyIdentity, PortableExecutableReference> implicitReferenceResolutions, [Nullable(1)] [In] [Out] DiagnosticBag resolutionDiagnostics, out bool hasCircularReference, out int corLibraryIndex)
		{
			ArrayBuilder<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding[]> instance = ArrayBuilder<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding[]>.GetInstance();
			CommonReferenceManager<TCompilation, TAssemblySymbol>.BoundInputAssembly[] array3;
			try
			{
				for (int i = 0; i < explicitAssemblies.Length; i++)
				{
					instance.Add(explicitAssemblies[i].BindAssemblyReferences(explicitAssemblies, this.IdentityComparer));
				}
				if (resolverOpt != null && resolverOpt.ResolveMissingAssemblies)
				{
					this.ResolveAndBindMissingAssemblies(compilation, explicitAssemblies, explicitModules, explicitReferences, explicitReferenceMap, resolverOpt, importOptions, supersedeLowerVersions, instance, assemblyReferencesBySimpleName, out allAssemblies, out implicitlyResolvedReferences, out implicitlyResolvedReferenceMap, ref implicitReferenceResolutions, resolutionDiagnostics);
				}
				else
				{
					allAssemblies = explicitAssemblies;
					implicitlyResolvedReferences = ImmutableArray<MetadataReference>.Empty;
					implicitlyResolvedReferenceMap = ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference>.Empty;
				}
				hasCircularReference = CommonReferenceManager<TCompilation, TAssemblySymbol>.CheckCircularReference(instance);
				corLibraryIndex = CommonReferenceManager<TCompilation, TAssemblySymbol>.IndexOfCorLibrary(explicitAssemblies, assemblyReferencesBySimpleName, supersedeLowerVersions);
				CommonReferenceManager<TCompilation, TAssemblySymbol>.BoundInputAssembly[] array = new CommonReferenceManager<TCompilation, TAssemblySymbol>.BoundInputAssembly[instance.Count];
				for (int j = 0; j < instance.Count; j++)
				{
					array[j].ReferenceBinding = instance[j];
				}
				TAssemblySymbol[] array2 = new TAssemblySymbol[allAssemblies.Length];
				if (!hasCircularReference && this.ReuseAssemblySymbolsWithNoPiaLocalTypes(array, array2, allAssemblies, corLibraryIndex))
				{
					array3 = array;
				}
				else
				{
					this.ReuseAssemblySymbols(array, array2, allAssemblies, corLibraryIndex);
					array3 = array;
				}
			}
			finally
			{
				instance.Free();
			}
			return array3;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0003A83C File Offset: 0x00038A3C
		private void ResolveAndBindMissingAssemblies(TCompilation compilation, [Nullable(new byte[] { 0, 1, 0, 0 })] ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData> explicitAssemblies, [Nullable(new byte[] { 0, 1 })] ImmutableArray<PEModule> explicitModules, [Nullable(new byte[] { 0, 1 })] ImmutableArray<MetadataReference> explicitReferences, [Nullable(0)] ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference> explicitReferenceMap, MetadataReferenceResolver resolver, MetadataImportOptions importOptions, bool supersedeLowerVersions, [Nullable(new byte[] { 1, 1, 0, 0, 0 })] [In] [Out] ArrayBuilder<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding[]> referenceBindings, [Nullable(new byte[] { 1, 1, 1, 0, 0, 0 })] [In] [Out] Dictionary<string, List<CommonReferenceManager<TCompilation, TAssemblySymbol>.ReferencedAssemblyIdentity>> assemblyReferencesBySimpleName, [Nullable(new byte[] { 0, 1, 0, 0 })] out ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData> allAssemblies, [Nullable(new byte[] { 0, 1 })] out ImmutableArray<MetadataReference> metadataReferences, [Nullable(0)] out ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference> resolvedReferences, [Nullable(new byte[] { 1, 1, 2 })] ref ImmutableDictionary<AssemblyIdentity, PortableExecutableReference> implicitReferenceResolutions, DiagnosticBag resolutionDiagnostics)
		{
			int num = explicitAssemblies.Length - 1;
			ArrayBuilder<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData> instance = ArrayBuilder<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData>.GetInstance();
			PooledHashSet<AssemblyIdentity> instance2 = PooledHashSet<AssemblyIdentity>.GetInstance();
			ArrayBuilder<MetadataReference> instance3 = ArrayBuilder<MetadataReference>.GetInstance();
			Dictionary<MetadataReference, MergedAliases> dictionary = null;
			ArrayBuilder<ValueTuple<MetadataReference, ArraySegment<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding>>> instance4 = ArrayBuilder<ValueTuple<MetadataReference, ArraySegment<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding>>>.GetInstance();
			this.GetInitialReferenceBindingsToProcess(explicitModules, explicitReferences, explicitReferenceMap, referenceBindings, num, instance4);
			int length = explicitAssemblies.Length;
			try
			{
				while (instance4.Count > 0)
				{
					ValueTuple<MetadataReference, ArraySegment<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding>> valueTuple = instance4.Pop<ValueTuple<MetadataReference, ArraySegment<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding>>>();
					MetadataReference item = valueTuple.Item1;
					ArraySegment<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding> item2 = valueTuple.Item2;
					foreach (CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding assemblyReferenceBinding in item2)
					{
						if (!assemblyReferenceBinding.IsBound)
						{
							AssemblyIdentity assemblyIdentity;
							AssemblyMetadata assemblyMetadata;
							PortableExecutableReference portableExecutableReference;
							if (!this.TryResolveMissingReference(item, assemblyReferenceBinding.ReferenceIdentity, ref implicitReferenceResolutions, resolver, resolutionDiagnostics, out assemblyIdentity, out assemblyMetadata, out portableExecutableReference))
							{
								instance2.Add(assemblyReferenceBinding.ReferenceIdentity);
							}
							else
							{
								instance2.Remove(assemblyReferenceBinding.ReferenceIdentity);
								int num2 = length - 1 + instance3.Count;
								MetadataReference metadataReference = this.TryAddAssembly(assemblyIdentity, portableExecutableReference, num2, resolutionDiagnostics, Location.None, assemblyReferencesBySimpleName, supersedeLowerVersions);
								if (metadataReference != null)
								{
									this.MergeReferenceProperties(metadataReference, portableExecutableReference, resolutionDiagnostics, ref dictionary);
								}
								else
								{
									instance3.Add(portableExecutableReference);
									CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData assemblyData = this.CreateAssemblyDataForResolvedMissingAssembly(assemblyMetadata, portableExecutableReference, importOptions);
									instance.Add(assemblyData);
									CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding[] array = assemblyData.BindAssemblyReferences(explicitAssemblies, this.IdentityComparer);
									referenceBindings.Add(array);
									instance4.Push(new ValueTuple<MetadataReference, ArraySegment<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding>>(portableExecutableReference, new ArraySegment<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding>(array)));
								}
							}
						}
					}
				}
				foreach (AssemblyIdentity assemblyIdentity2 in instance2)
				{
					implicitReferenceResolutions = implicitReferenceResolutions.Add(assemblyIdentity2, null);
				}
				if (instance.Count == 0)
				{
					resolvedReferences = ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference>.Empty;
					metadataReferences = ImmutableArray<MetadataReference>.Empty;
					allAssemblies = explicitAssemblies;
				}
				else
				{
					allAssemblies = explicitAssemblies.AddRange(instance);
					for (int i = 0; i < referenceBindings.Count; i++)
					{
						CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding[] array2 = referenceBindings[i];
						for (int j = 0; j < array2.Length; j++)
						{
							CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding assemblyReferenceBinding2 = array2[j];
							if (!assemblyReferenceBinding2.IsBound)
							{
								array2[j] = CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolveReferencedAssembly(assemblyReferenceBinding2.ReferenceIdentity, allAssemblies, length, this.IdentityComparer);
							}
						}
					}
					CommonReferenceManager<TCompilation, TAssemblySymbol>.UpdateBindingsOfAssemblyBeingBuilt(referenceBindings, length, instance);
					metadataReferences = instance3.ToImmutable();
					resolvedReferences = CommonReferenceManager<TCompilation, TAssemblySymbol>.ToResolvedAssemblyReferences(metadataReferences, dictionary, length);
				}
			}
			finally
			{
				instance.Free();
				instance4.Free();
				instance3.Free();
				instance2.Free();
			}
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0003AB30 File Offset: 0x00038D30
		[NullableContext(0)]
		private void GetInitialReferenceBindingsToProcess([Nullable(new byte[] { 0, 1 })] ImmutableArray<PEModule> explicitModules, [Nullable(new byte[] { 0, 1 })] ImmutableArray<MetadataReference> explicitReferences, ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference> explicitReferenceMap, [Nullable(new byte[] { 1, 1, 0, 0, 0 })] ArrayBuilder<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding[]> referenceBindings, int totalReferencedAssemblyCount, [Nullable(new byte[] { 1, 0, 1, 0, 0, 0, 0 })] [Out] ArrayBuilder<ValueTuple<MetadataReference, ArraySegment<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding>>> result)
		{
			ImmutableArray<int> immutableArray = CommonReferenceManager<TCompilation, TAssemblySymbol>.CalculateModuleToReferenceMap(explicitModules, explicitReferenceMap);
			CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding[] array = referenceBindings[0];
			int num = totalReferencedAssemblyCount;
			for (int i = 0; i < explicitModules.Length; i++)
			{
				MetadataReference metadataReference = explicitReferences[immutableArray[i]];
				int length = explicitModules[i].ReferencedAssemblies.Length;
				result.Add(new ValueTuple<MetadataReference, ArraySegment<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding>>(metadataReference, new ArraySegment<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding>(array, num, length)));
				num += length;
			}
			for (int j = 0; j < explicitReferenceMap.Length; j++)
			{
				CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference resolvedReference = explicitReferenceMap[j];
				if (!resolvedReference.IsSkipped && resolvedReference.Kind != MetadataImageKind.Module)
				{
					result.Add(new ValueTuple<MetadataReference, ArraySegment<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding>>(explicitReferences[j], new ArraySegment<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding>(referenceBindings[resolvedReference.Index + 1])));
				}
			}
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0003AC08 File Offset: 0x00038E08
		[NullableContext(0)]
		private static ImmutableArray<int> CalculateModuleToReferenceMap([Nullable(new byte[] { 0, 1 })] ImmutableArray<PEModule> modules, ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference> resolvedReferences)
		{
			if (modules.Length == 0)
			{
				return ImmutableArray<int>.Empty;
			}
			ArrayBuilder<int> instance = ArrayBuilder<int>.GetInstance(modules.Length);
			instance.ZeroInit(modules.Length);
			for (int i = 0; i < resolvedReferences.Length; i++)
			{
				CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference resolvedReference = resolvedReferences[i];
				if (!resolvedReference.IsSkipped && resolvedReference.Kind == MetadataImageKind.Module)
				{
					instance[resolvedReference.Index] = i;
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0003AC80 File Offset: 0x00038E80
		[NullableContext(0)]
		private static ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference> ToResolvedAssemblyReferences([Nullable(new byte[] { 0, 1 })] ImmutableArray<MetadataReference> references, [Nullable(new byte[] { 2, 1, 1 })] Dictionary<MetadataReference, MergedAliases> propertyMapOpt, int explicitAssemblyCount)
		{
			ArrayBuilder<CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference> instance = ArrayBuilder<CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference>.GetInstance(references.Length);
			for (int i = 0; i < references.Length; i++)
			{
				instance.Add(CommonReferenceManager<TCompilation, TAssemblySymbol>.GetResolvedReferenceAndFreePropertyMapEntry(references[i], explicitAssemblyCount - 1 + i, MetadataImageKind.Assembly, propertyMapOpt));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0003ACCC File Offset: 0x00038ECC
		private static void UpdateBindingsOfAssemblyBeingBuilt([Nullable(new byte[] { 1, 1, 0, 0, 0 })] ArrayBuilder<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding[]> referenceBindings, int explicitAssemblyCount, [Nullable(new byte[] { 1, 1, 0, 0 })] ArrayBuilder<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData> implicitAssemblies)
		{
			CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding[] array = referenceBindings[0];
			ArrayBuilder<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding> instance = ArrayBuilder<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding>.GetInstance(array.Length + implicitAssemblies.Count);
			instance.AddRange(array, explicitAssemblyCount - 1);
			for (int i = 0; i < implicitAssemblies.Count; i++)
			{
				instance.Add(new CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding(implicitAssemblies[i].Identity, explicitAssemblyCount + i, 0));
			}
			instance.AddRange(array, explicitAssemblyCount - 1, array.Length - explicitAssemblyCount + 1);
			referenceBindings[0] = instance.ToArrayAndFree();
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0003AD48 File Offset: 0x00038F48
		private bool TryResolveMissingReference(MetadataReference requestingReference, AssemblyIdentity referenceIdentity, [Nullable(new byte[] { 1, 1, 2 })] ref ImmutableDictionary<AssemblyIdentity, PortableExecutableReference> implicitReferenceResolutions, MetadataReferenceResolver resolver, DiagnosticBag resolutionDiagnostics, [Nullable(2)] [NotNullWhen(true)] out AssemblyIdentity resolvedAssemblyIdentity, [Nullable(2)] [NotNullWhen(true)] out AssemblyMetadata resolvedAssemblyMetadata, [Nullable(2)] [NotNullWhen(true)] out PortableExecutableReference resolvedReference)
		{
			resolvedAssemblyIdentity = null;
			resolvedAssemblyMetadata = null;
			bool flag = false;
			if (!implicitReferenceResolutions.TryGetValue(referenceIdentity, out resolvedReference))
			{
				resolvedReference = resolver.ResolveMissingAssembly(requestingReference, referenceIdentity);
				flag = true;
			}
			if (resolvedReference == null)
			{
				return false;
			}
			resolvedAssemblyMetadata = this.GetAssemblyMetadata(resolvedReference, resolutionDiagnostics);
			if (resolvedAssemblyMetadata == null)
			{
				return false;
			}
			PEAssembly assembly = resolvedAssemblyMetadata.GetAssembly();
			if (flag && this.IdentityComparer.Compare(referenceIdentity, assembly.Identity) == AssemblyIdentityComparer.ComparisonResult.NotEquivalent)
			{
				return false;
			}
			resolvedAssemblyIdentity = assembly.Identity;
			implicitReferenceResolutions = implicitReferenceResolutions.Add(referenceIdentity, resolvedReference);
			return true;
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0003ADD0 File Offset: 0x00038FD0
		[return: Nullable(new byte[] { 1, 0, 0 })]
		private CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData CreateAssemblyDataForResolvedMissingAssembly(AssemblyMetadata assemblyMetadata, PortableExecutableReference peReference, MetadataImportOptions importOptions)
		{
			PEAssembly assembly = assemblyMetadata.GetAssembly();
			return this.CreateAssemblyDataForFile(assembly, assemblyMetadata.CachedSymbols, peReference.DocumentationProvider, this.SimpleAssemblyName, importOptions, peReference.Properties.EmbedInteropTypes);
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0003AE0C File Offset: 0x0003900C
		private bool ReuseAssemblySymbolsWithNoPiaLocalTypes([Nullable(new byte[] { 1, 0, 0, 0 })] CommonReferenceManager<TCompilation, TAssemblySymbol>.BoundInputAssembly[] boundInputs, TAssemblySymbol[] candidateInputAssemblySymbols, [Nullable(new byte[] { 0, 1, 0, 0 })] ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData> assemblies, int corLibraryIndex)
		{
			int length = assemblies.Length;
			for (int i = 1; i < length; i++)
			{
				if (assemblies[i].ContainsNoPiaLocalTypes)
				{
					foreach (TAssemblySymbol tassemblySymbol in assemblies[i].AvailableSymbols)
					{
						if (this.IsLinked(tassemblySymbol) == assemblies[i].IsLinked)
						{
							ImmutableArray<TAssemblySymbol> noPiaResolutionAssemblies = this.GetNoPiaResolutionAssemblies(tassemblySymbol);
							if (!noPiaResolutionAssemblies.IsDefault)
							{
								Array.Clear(candidateInputAssemblySymbols, 0, candidateInputAssemblySymbols.Length);
								bool flag = true;
								foreach (TAssemblySymbol tassemblySymbol2 in noPiaResolutionAssemblies)
								{
									flag = false;
									for (int j = 1; j < length; j++)
									{
										if (assemblies[j].IsMatchingAssembly(tassemblySymbol2) && this.IsLinked(tassemblySymbol2) == assemblies[j].IsLinked)
										{
											candidateInputAssemblySymbols[j] = tassemblySymbol2;
											flag = true;
										}
									}
									if (!flag)
									{
										break;
									}
								}
								if (flag)
								{
									for (int k = 1; k < length; k++)
									{
										if (candidateInputAssemblySymbols[k] == null)
										{
											flag = false;
											break;
										}
										if (corLibraryIndex < 0)
										{
											if (this.GetCorLibrary(candidateInputAssemblySymbols[k]) != null)
											{
												flag = false;
												break;
											}
										}
										else if (candidateInputAssemblySymbols[corLibraryIndex] != this.GetCorLibrary(candidateInputAssemblySymbols[k]))
										{
											flag = false;
											break;
										}
									}
									if (flag)
									{
										for (int l = 1; l < length; l++)
										{
											boundInputs[l].AssemblySymbol = candidateInputAssemblySymbols[l];
										}
										return true;
									}
								}
							}
						}
					}
					Array.Clear(candidateInputAssemblySymbols, 0, candidateInputAssemblySymbols.Length);
					break;
				}
			}
			return false;
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0003AFEC File Offset: 0x000391EC
		private void ReuseAssemblySymbols([Nullable(new byte[] { 1, 0, 0, 0 })] CommonReferenceManager<TCompilation, TAssemblySymbol>.BoundInputAssembly[] boundInputs, TAssemblySymbol[] candidateInputAssemblySymbols, [Nullable(new byte[] { 0, 1, 0, 0 })] ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData> assemblies, int corLibraryIndex)
		{
			Queue<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceCandidate> queue = new Queue<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceCandidate>();
			int length = assemblies.Length;
			List<TAssemblySymbol> list = new List<TAssemblySymbol>(1024);
			for (int i = 1; i < length; i++)
			{
				if (boundInputs[i].AssemblySymbol == null && !assemblies[i].ContainsNoPiaLocalTypes)
				{
					foreach (TAssemblySymbol tassemblySymbol in assemblies[i].AvailableSymbols)
					{
						bool flag = true;
						Array.Clear(candidateInputAssemblySymbols, 0, candidateInputAssemblySymbols.Length);
						queue.Clear();
						queue.Enqueue(new CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceCandidate(i, tassemblySymbol));
						while (flag && queue.Count > 0)
						{
							CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceCandidate assemblyReferenceCandidate = queue.Dequeue();
							int definitionIndex = assemblyReferenceCandidate.DefinitionIndex;
							TAssemblySymbol tassemblySymbol2 = boundInputs[definitionIndex].AssemblySymbol;
							if (tassemblySymbol2 == null)
							{
								tassemblySymbol2 = candidateInputAssemblySymbols[definitionIndex];
							}
							if (tassemblySymbol2 != null)
							{
								if (tassemblySymbol2 != assemblyReferenceCandidate.AssemblySymbol)
								{
									flag = false;
									break;
								}
							}
							else
							{
								if (this.IsLinked(assemblyReferenceCandidate.AssemblySymbol) != assemblies[definitionIndex].IsLinked)
								{
									flag = false;
									break;
								}
								candidateInputAssemblySymbols[definitionIndex] = assemblyReferenceCandidate.AssemblySymbol;
								CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding[] referenceBinding = boundInputs[definitionIndex].ReferenceBinding;
								list.Clear();
								this.GetActualBoundReferencesUsedBy(assemblyReferenceCandidate.AssemblySymbol, list);
								int count = list.Count;
								for (int j = 0; j < count; j++)
								{
									if (!referenceBinding[j].IsBound)
									{
										if (list[j] != null)
										{
											flag = false;
											break;
										}
									}
									else
									{
										TAssemblySymbol tassemblySymbol3 = list[j];
										if (tassemblySymbol3 == null)
										{
											flag = false;
											break;
										}
										int definitionIndex2 = referenceBinding[j].DefinitionIndex;
										if (definitionIndex2 == 0)
										{
											flag = false;
											break;
										}
										if (!assemblies[definitionIndex2].IsMatchingAssembly(tassemblySymbol3))
										{
											flag = false;
											break;
										}
										if (assemblies[definitionIndex2].ContainsNoPiaLocalTypes)
										{
											flag = false;
											break;
										}
										if (this.IsLinked(tassemblySymbol3) != assemblies[definitionIndex2].IsLinked)
										{
											flag = false;
											break;
										}
										queue.Enqueue(new CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceCandidate(definitionIndex2, tassemblySymbol3));
									}
								}
								if (flag)
								{
									TAssemblySymbol corLibrary = this.GetCorLibrary(assemblyReferenceCandidate.AssemblySymbol);
									if (corLibrary == null)
									{
										if (corLibraryIndex >= 0)
										{
											flag = false;
											break;
										}
									}
									else
									{
										if (corLibraryIndex < 0)
										{
											flag = false;
											break;
										}
										if (!assemblies[corLibraryIndex].IsMatchingAssembly(corLibrary))
										{
											flag = false;
											break;
										}
										queue.Enqueue(new CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceCandidate(corLibraryIndex, corLibrary));
									}
								}
							}
						}
						if (flag)
						{
							for (int k = 0; k < length; k++)
							{
								if (candidateInputAssemblySymbols[k] != null)
								{
									boundInputs[k].AssemblySymbol = candidateInputAssemblySymbols[k];
								}
							}
							break;
						}
					}
				}
			}
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0003B2F4 File Offset: 0x000394F4
		private static bool CheckCircularReference([Nullable(new byte[] { 1, 1, 0, 0, 0 })] IReadOnlyList<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding[]> referenceBindings)
		{
			for (int i = 1; i < referenceBindings.Count; i++)
			{
				foreach (CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding assemblyReferenceBinding in referenceBindings[i])
				{
					if (assemblyReferenceBinding.BoundToAssemblyBeingBuilt)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0003B33C File Offset: 0x0003953C
		private static bool IsSuperseded(AssemblyIdentity identity, [Nullable(new byte[] { 1, 1, 1, 0, 0, 0 })] IReadOnlyDictionary<string, List<CommonReferenceManager<TCompilation, TAssemblySymbol>.ReferencedAssemblyIdentity>> assemblyReferencesBySimpleName)
		{
			return assemblyReferencesBySimpleName[identity.Name][0].Identity.Version != identity.Version;
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0003B368 File Offset: 0x00039568
		private static int IndexOfCorLibrary([Nullable(new byte[] { 0, 1, 0, 0 })] ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData> assemblies, [Nullable(new byte[] { 1, 1, 1, 0, 0, 0 })] IReadOnlyDictionary<string, List<CommonReferenceManager<TCompilation, TAssemblySymbol>.ReferencedAssemblyIdentity>> assemblyReferencesBySimpleName, bool supersedeLowerVersions)
		{
			ArrayBuilder<int> arrayBuilder = null;
			for (int i = 1; i < assemblies.Length; i++)
			{
				CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData assemblyData = assemblies[i];
				if (!assemblyData.IsLinked && assemblyData.AssemblyReferences.Length == 0 && !assemblyData.ContainsNoPiaLocalTypes && (!supersedeLowerVersions || !CommonReferenceManager<TCompilation, TAssemblySymbol>.IsSuperseded(assemblyData.Identity, assemblyReferencesBySimpleName)) && assemblyData.DeclaresTheObjectClass)
				{
					if (arrayBuilder == null)
					{
						arrayBuilder = ArrayBuilder<int>.GetInstance();
					}
					arrayBuilder.Add(i);
				}
			}
			if (arrayBuilder != null)
			{
				if (arrayBuilder.Count == 1)
				{
					int num = arrayBuilder[0];
					arrayBuilder.Free();
					return num;
				}
				arrayBuilder.Free();
			}
			if (assemblies.Length == 1 && assemblies[0].AssemblyReferences.Length == 0)
			{
				return 0;
			}
			return -1;
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0003B423 File Offset: 0x00039623
		internal static bool InternalsMayBeVisibleToAssemblyBeingCompiled(string compilationName, PEAssembly assembly)
		{
			return !assembly.GetInternalsVisibleToPublicKeys(compilationName).IsEmpty<ImmutableArray<byte>>();
		}

		// Token: 0x0600132C RID: 4908
		protected abstract void GetActualBoundReferencesUsedBy(TAssemblySymbol assemblySymbol, [Nullable(new byte[] { 1, 2 })] List<TAssemblySymbol> referencedAssemblySymbols);

		// Token: 0x0600132D RID: 4909
		[return: Nullable(new byte[] { 0, 1 })]
		protected abstract ImmutableArray<TAssemblySymbol> GetNoPiaResolutionAssemblies(TAssemblySymbol candidateAssembly);

		// Token: 0x0600132E RID: 4910
		protected abstract bool IsLinked(TAssemblySymbol candidateAssembly);

		// Token: 0x0600132F RID: 4911
		[return: Nullable(2)]
		protected abstract TAssemblySymbol GetCorLibrary(TAssemblySymbol candidateAssembly);

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001330 RID: 4912
		protected abstract CommonMessageProvider MessageProvider { get; }

		// Token: 0x06001331 RID: 4913
		[return: Nullable(new byte[] { 1, 0, 0 })]
		protected abstract CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData CreateAssemblyDataForFile(PEAssembly assembly, WeakList<IAssemblySymbolInternal> cachedSymbols, DocumentationProvider documentationProvider, string sourceAssemblySimpleName, MetadataImportOptions importOptions, bool embedInteropTypes);

		// Token: 0x06001332 RID: 4914
		[return: Nullable(new byte[] { 1, 0, 0 })]
		protected abstract CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData CreateAssemblyDataForCompilation(CompilationReference compilationReference);

		// Token: 0x06001333 RID: 4915
		protected abstract bool CheckPropertiesConsistency(MetadataReference primaryReference, MetadataReference duplicateReference, DiagnosticBag diagnostics);

		// Token: 0x06001334 RID: 4916
		protected abstract bool WeakIdentityPropertiesEquivalent(AssemblyIdentity identity1, AssemblyIdentity identity2);

		// Token: 0x06001335 RID: 4917 RVA: 0x0003B434 File Offset: 0x00039634
		[return: Nullable(0)]
		protected ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference> ResolveMetadataReferences(TCompilation compilation, [Nullable(new byte[] { 1, 1, 1, 0, 0, 0 })] [Out] Dictionary<string, List<CommonReferenceManager<TCompilation, TAssemblySymbol>.ReferencedAssemblyIdentity>> assemblyReferencesBySimpleName, [Nullable(new byte[] { 0, 1 })] out ImmutableArray<MetadataReference> references, [Nullable(new byte[] { 1, 0, 1, 1, 1 })] out IDictionary<ValueTuple<string, string>, MetadataReference> boundReferenceDirectiveMap, [Nullable(new byte[] { 0, 1 })] out ImmutableArray<MetadataReference> boundReferenceDirectives, [Nullable(new byte[] { 0, 1, 0, 0 })] out ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData> assemblies, [Nullable(new byte[] { 0, 1 })] out ImmutableArray<PEModule> modules, DiagnosticBag diagnostics)
		{
			ImmutableArray<Location> immutableArray;
			this.GetCompilationReferences(compilation, diagnostics, out references, out boundReferenceDirectiveMap, out immutableArray);
			int length = references.Length;
			int num = ((new ImmutableArray<Location>?(immutableArray) != default(ImmutableArray<Location>?)) ? immutableArray.Length : 0);
			CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference[] array = new CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference[length];
			Dictionary<MetadataReference, MergedAliases> dictionary = null;
			Dictionary<MetadataReference, MetadataReference> dictionary2 = new Dictionary<MetadataReference, MetadataReference>(CommonReferenceManager<TCompilation, TAssemblySymbol>.MetadataReferenceEqualityComparer.Instance);
			ArrayBuilder<MetadataReference> arrayBuilder = ((new ImmutableArray<Location>?(immutableArray) != default(ImmutableArray<Location>?)) ? ArrayBuilder<MetadataReference>.GetInstance() : null);
			ArrayBuilder<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData> instance = ArrayBuilder<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData>.GetInstance();
			ArrayBuilder<PEModule> arrayBuilder2 = null;
			bool referencesSupersedeLowerVersions = compilation.Options.ReferencesSupersedeLowerVersions;
			for (int i = length - 1; i >= 0; i--)
			{
				MetadataReference metadataReference = references[i];
				if (metadataReference != null)
				{
					MetadataReference metadataReference2;
					if (dictionary2.TryGetValue(metadataReference, ref metadataReference2))
					{
						if (metadataReference != metadataReference2)
						{
							this.MergeReferenceProperties(metadataReference2, metadataReference, diagnostics, ref dictionary);
						}
					}
					else
					{
						dictionary2.Add(metadataReference, metadataReference);
						Location location;
						if (i < num)
						{
							location = immutableArray[i];
							arrayBuilder.Add(metadataReference);
						}
						else
						{
							location = Location.None;
						}
						CompilationReference compilationReference = metadataReference as CompilationReference;
						if (compilationReference != null)
						{
							if (compilationReference.Properties.Kind != MetadataImageKind.Assembly)
							{
								throw ExceptionUtilities.UnexpectedValue(compilationReference.Properties.Kind);
							}
							metadataReference2 = this.TryAddAssembly(compilationReference.Compilation.Assembly.Identity, metadataReference, -instance.Count - 1, diagnostics, location, assemblyReferencesBySimpleName, referencesSupersedeLowerVersions);
							if (metadataReference2 != null)
							{
								this.MergeReferenceProperties(metadataReference2, metadataReference, diagnostics, ref dictionary);
							}
							else
							{
								CommonReferenceManager<TCompilation, TAssemblySymbol>.AddAssembly(this.CreateAssemblyDataForCompilation(compilationReference), i, array, instance);
							}
						}
						else
						{
							PortableExecutableReference portableExecutableReference = (PortableExecutableReference)metadataReference;
							Metadata metadata = this.GetMetadata(portableExecutableReference, this.MessageProvider, location, diagnostics);
							if (metadata != null)
							{
								MetadataImageKind kind = portableExecutableReference.Properties.Kind;
								if (kind != MetadataImageKind.Assembly)
								{
									if (kind != MetadataImageKind.Module)
									{
										throw ExceptionUtilities.UnexpectedValue(portableExecutableReference.Properties.Kind);
									}
									ModuleMetadata moduleMetadata = (ModuleMetadata)metadata;
									if (moduleMetadata.Module.IsLinkedModule)
									{
										if (!moduleMetadata.Module.IsEntireImageAvailable)
										{
											diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_LinkedNetmoduleMetadataMustProvideFullPEImage, location, new object[] { portableExecutableReference.Display ?? "" }));
										}
										CommonReferenceManager<TCompilation, TAssemblySymbol>.AddModule(moduleMetadata.Module, i, array, ref arrayBuilder2);
									}
									else
									{
										diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_MetadataFileNotModule, location, new object[] { portableExecutableReference.Display ?? "" }));
									}
								}
								else
								{
									AssemblyMetadata assemblyMetadata = (AssemblyMetadata)metadata;
									WeakList<IAssemblySymbolInternal> cachedSymbols = assemblyMetadata.CachedSymbols;
									if (assemblyMetadata.IsValidAssembly())
									{
										PEAssembly assembly = assemblyMetadata.GetAssembly();
										metadataReference2 = this.TryAddAssembly(assembly.Identity, portableExecutableReference, -instance.Count - 1, diagnostics, location, assemblyReferencesBySimpleName, referencesSupersedeLowerVersions);
										if (metadataReference2 != null)
										{
											this.MergeReferenceProperties(metadataReference2, metadataReference, diagnostics, ref dictionary);
											goto IL_0383;
										}
										CommonReferenceManager<TCompilation, TAssemblySymbol>.AddAssembly(this.CreateAssemblyDataForFile(assembly, cachedSymbols, portableExecutableReference.DocumentationProvider, this.SimpleAssemblyName, compilation.Options.MetadataImportOptions, portableExecutableReference.Properties.EmbedInteropTypes), i, array, instance);
									}
									else
									{
										diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_MetadataFileNotAssembly, location, new object[] { portableExecutableReference.Display ?? "" }));
									}
									GC.KeepAlive(assemblyMetadata);
								}
							}
						}
					}
				}
				IL_0383:;
			}
			if (arrayBuilder != null)
			{
				arrayBuilder.ReverseContents();
				boundReferenceDirectives = arrayBuilder.ToImmutableAndFree();
			}
			else
			{
				boundReferenceDirectives = ImmutableArray<MetadataReference>.Empty;
			}
			for (int j = 0; j < array.Length; j++)
			{
				if (!array[j].IsSkipped)
				{
					int num2 = ((array[j].Kind == MetadataImageKind.Assembly) ? instance.Count : ((arrayBuilder2 != null) ? arrayBuilder2.Count : 0)) - 1 - array[j].Index;
					array[j] = CommonReferenceManager<TCompilation, TAssemblySymbol>.GetResolvedReferenceAndFreePropertyMapEntry(references[j], num2, array[j].Kind, dictionary);
				}
			}
			instance.ReverseContents();
			assemblies = instance.ToImmutableAndFree();
			if (arrayBuilder2 == null)
			{
				modules = ImmutableArray<PEModule>.Empty;
			}
			else
			{
				arrayBuilder2.ReverseContents();
				modules = arrayBuilder2.ToImmutableAndFree();
			}
			return ImmutableArray.CreateRange<CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference>(array);
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0003B8BC File Offset: 0x00039ABC
		[NullableContext(0)]
		private static CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference GetResolvedReferenceAndFreePropertyMapEntry([Nullable(1)] MetadataReference reference, int index, MetadataImageKind kind, [Nullable(new byte[] { 2, 1, 1 })] Dictionary<MetadataReference, MergedAliases> propertyMapOpt)
		{
			ImmutableArray<MetadataReference> immutableArray = ImmutableArray<MetadataReference>.Empty;
			MergedAliases mergedAliases;
			ImmutableArray<string> immutableArray2;
			ImmutableArray<string> immutableArray3;
			if (propertyMapOpt != null && propertyMapOpt.TryGetValue(reference, ref mergedAliases))
			{
				ArrayBuilder<string> aliasesOpt = mergedAliases.AliasesOpt;
				immutableArray2 = ((aliasesOpt != null) ? aliasesOpt.ToImmutableAndFree() : default(ImmutableArray<string>));
				ArrayBuilder<string> recursiveAliasesOpt = mergedAliases.RecursiveAliasesOpt;
				immutableArray3 = ((recursiveAliasesOpt != null) ? recursiveAliasesOpt.ToImmutableAndFree() : default(ImmutableArray<string>));
				if (mergedAliases.MergedReferencesOpt != null)
				{
					immutableArray = mergedAliases.MergedReferencesOpt.ToImmutableAndFree();
				}
			}
			else if (reference.Properties.HasRecursiveAliases)
			{
				immutableArray2 = default(ImmutableArray<string>);
				immutableArray3 = reference.Properties.Aliases;
			}
			else
			{
				immutableArray2 = reference.Properties.Aliases;
				immutableArray3 = default(ImmutableArray<string>);
			}
			return new CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference(index, kind, immutableArray2, immutableArray3, immutableArray);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0003B978 File Offset: 0x00039B78
		[return: Nullable(2)]
		private Metadata GetMetadata(PortableExecutableReference peReference, CommonMessageProvider messageProvider, Location location, DiagnosticBag diagnostics)
		{
			Dictionary<MetadataReference, object> dictionary = this.ObservedMetadata;
			lock (dictionary)
			{
				Metadata metadata;
				if (this.TryGetObservedMetadata(peReference, diagnostics, out metadata))
				{
					return metadata;
				}
			}
			Diagnostic diagnostic = null;
			Metadata metadata2;
			try
			{
				metadata2 = peReference.GetMetadataNoCopy();
				AssemblyMetadata assemblyMetadata = metadata2 as AssemblyMetadata;
				if (assemblyMetadata != null)
				{
					assemblyMetadata.IsValidAssembly();
				}
				else
				{
					bool isLinkedModule = ((ModuleMetadata)metadata2).Module.IsLinkedModule;
				}
			}
			catch (Exception ex) when (ex is BadImageFormatException || ex is IOException)
			{
				diagnostic = PortableExecutableReference.ExceptionToDiagnostic(ex, messageProvider, location, peReference.Display ?? "", peReference.Properties.Kind);
				metadata2 = null;
			}
			dictionary = this.ObservedMetadata;
			Metadata metadata3;
			lock (dictionary)
			{
				Metadata metadata;
				if (this.TryGetObservedMetadata(peReference, diagnostics, out metadata))
				{
					metadata3 = metadata;
				}
				else
				{
					if (diagnostic != null)
					{
						diagnostics.Add(diagnostic);
					}
					this.ObservedMetadata.Add(peReference, metadata2 ?? diagnostic);
					metadata3 = metadata2;
				}
			}
			return metadata3;
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0003BABC File Offset: 0x00039CBC
		private bool TryGetObservedMetadata(PortableExecutableReference peReference, DiagnosticBag diagnostics, [Nullable(2)] out Metadata metadata)
		{
			object obj;
			if (this.ObservedMetadata.TryGetValue(peReference, ref obj))
			{
				metadata = obj as Metadata;
				if (metadata == null)
				{
					diagnostics.Add((Diagnostic)obj);
				}
				return true;
			}
			metadata = null;
			return false;
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0003BAF8 File Offset: 0x00039CF8
		[return: Nullable(2)]
		internal AssemblyMetadata GetAssemblyMetadata(PortableExecutableReference peReference, DiagnosticBag diagnostics)
		{
			Metadata metadata = this.GetMetadata(peReference, this.MessageProvider, Location.None, diagnostics);
			if (metadata == null)
			{
				return null;
			}
			AssemblyMetadata assemblyMetadata = metadata as AssemblyMetadata;
			if (assemblyMetadata == null || !assemblyMetadata.IsValidAssembly())
			{
				diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_MetadataFileNotAssembly, Location.None, new object[] { peReference.Display ?? "" }));
				return null;
			}
			return assemblyMetadata;
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0003BB74 File Offset: 0x00039D74
		private void MergeReferenceProperties(MetadataReference primaryReference, MetadataReference newReference, DiagnosticBag diagnostics, [Nullable(new byte[] { 2, 1, 1 })] ref Dictionary<MetadataReference, MergedAliases> lazyAliasMap)
		{
			if (!this.CheckPropertiesConsistency(newReference, primaryReference, diagnostics))
			{
				return;
			}
			if (lazyAliasMap == null)
			{
				lazyAliasMap = new Dictionary<MetadataReference, MergedAliases>();
			}
			MergedAliases mergedAliases;
			if (!lazyAliasMap.TryGetValue(primaryReference, ref mergedAliases))
			{
				mergedAliases = new MergedAliases();
				lazyAliasMap.Add(primaryReference, mergedAliases);
				mergedAliases.Merge(primaryReference);
			}
			mergedAliases.Merge(newReference);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0003BBC5 File Offset: 0x00039DC5
		private static void AddAssembly([Nullable(new byte[] { 1, 0, 0 })] CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData data, int referenceIndex, [Nullable(new byte[] { 1, 0, 0, 0 })] CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference[] referenceMap, [Nullable(new byte[] { 1, 1, 0, 0 })] ArrayBuilder<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData> assemblies)
		{
			referenceMap[referenceIndex] = new CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference(assemblies.Count, MetadataImageKind.Assembly);
			assemblies.Add(data);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0003BBE1 File Offset: 0x00039DE1
		private static void AddModule(PEModule module, int referenceIndex, [Nullable(new byte[] { 1, 0, 0, 0 })] CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference[] referenceMap, [Nullable(new byte[] { 2, 1 })] [NotNull] ref ArrayBuilder<PEModule> modules)
		{
			if (modules == null)
			{
				modules = ArrayBuilder<PEModule>.GetInstance();
			}
			referenceMap[referenceIndex] = new CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference(modules.Count, MetadataImageKind.Module);
			modules.Add(module);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0003BC0C File Offset: 0x00039E0C
		[return: Nullable(2)]
		private MetadataReference TryAddAssembly(AssemblyIdentity identity, MetadataReference reference, int assemblyIndex, DiagnosticBag diagnostics, Location location, [Nullable(new byte[] { 1, 1, 1, 0, 0, 0 })] Dictionary<string, List<CommonReferenceManager<TCompilation, TAssemblySymbol>.ReferencedAssemblyIdentity>> referencesBySimpleName, bool supersedeLowerVersions)
		{
			CommonReferenceManager<TCompilation, TAssemblySymbol>.ReferencedAssemblyIdentity referencedAssemblyIdentity = new CommonReferenceManager<TCompilation, TAssemblySymbol>.ReferencedAssemblyIdentity(identity, reference, assemblyIndex);
			List<CommonReferenceManager<TCompilation, TAssemblySymbol>.ReferencedAssemblyIdentity> list;
			if (!referencesBySimpleName.TryGetValue(identity.Name, ref list))
			{
				string name = identity.Name;
				List<CommonReferenceManager<TCompilation, TAssemblySymbol>.ReferencedAssemblyIdentity> list2 = new List<CommonReferenceManager<TCompilation, TAssemblySymbol>.ReferencedAssemblyIdentity>();
				list2.Add(referencedAssemblyIdentity);
				referencesBySimpleName.Add(name, list2);
				return null;
			}
			if (supersedeLowerVersions)
			{
				foreach (CommonReferenceManager<TCompilation, TAssemblySymbol>.ReferencedAssemblyIdentity referencedAssemblyIdentity2 in list)
				{
					if (identity.Version == referencedAssemblyIdentity2.Identity.Version)
					{
						return referencedAssemblyIdentity2.Reference;
					}
				}
				if (list[0].Identity.Version > identity.Version)
				{
					list.Add(referencedAssemblyIdentity);
				}
				else
				{
					list.Add(list[0]);
					list[0] = referencedAssemblyIdentity;
				}
				return null;
			}
			CommonReferenceManager<TCompilation, TAssemblySymbol>.ReferencedAssemblyIdentity referencedAssemblyIdentity3 = default(CommonReferenceManager<TCompilation, TAssemblySymbol>.ReferencedAssemblyIdentity);
			if (identity.IsStrongName)
			{
				using (List<CommonReferenceManager<TCompilation, TAssemblySymbol>.ReferencedAssemblyIdentity>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CommonReferenceManager<TCompilation, TAssemblySymbol>.ReferencedAssemblyIdentity referencedAssemblyIdentity4 = enumerator.Current;
						if (referencedAssemblyIdentity4.Identity.IsStrongName && this.IdentityComparer.ReferenceMatchesDefinition(identity, referencedAssemblyIdentity4.Identity) && this.IdentityComparer.ReferenceMatchesDefinition(referencedAssemblyIdentity4.Identity, identity))
						{
							referencedAssemblyIdentity3 = referencedAssemblyIdentity4;
							break;
						}
					}
					goto IL_0193;
				}
			}
			foreach (CommonReferenceManager<TCompilation, TAssemblySymbol>.ReferencedAssemblyIdentity referencedAssemblyIdentity5 in list)
			{
				if (!referencedAssemblyIdentity5.Identity.IsStrongName && this.WeakIdentityPropertiesEquivalent(identity, referencedAssemblyIdentity5.Identity))
				{
					referencedAssemblyIdentity3 = referencedAssemblyIdentity5;
					break;
				}
			}
			IL_0193:
			if (referencedAssemblyIdentity3.Identity == null)
			{
				list.Add(referencedAssemblyIdentity);
				return null;
			}
			if (identity.IsStrongName)
			{
				if (identity != referencedAssemblyIdentity3.Identity)
				{
					this.MessageProvider.ReportDuplicateMetadataReferenceStrong(diagnostics, location, reference, identity, referencedAssemblyIdentity3.Reference, referencedAssemblyIdentity3.Identity);
				}
			}
			else if (identity != referencedAssemblyIdentity3.Identity)
			{
				this.MessageProvider.ReportDuplicateMetadataReferenceWeak(diagnostics, location, reference, identity, referencedAssemblyIdentity3.Reference, referencedAssemblyIdentity3.Identity);
			}
			return referencedAssemblyIdentity3.Reference;
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0003BE54 File Offset: 0x0003A054
		protected void GetCompilationReferences(TCompilation compilation, DiagnosticBag diagnostics, [Nullable(new byte[] { 0, 1 })] out ImmutableArray<MetadataReference> references, [Nullable(new byte[] { 1, 0, 1, 1, 1 })] out IDictionary<ValueTuple<string, string>, MetadataReference> boundReferenceDirectives, [Nullable(new byte[] { 0, 1 })] out ImmutableArray<Location> referenceDirectiveLocations)
		{
			ArrayBuilder<MetadataReference> instance = ArrayBuilder<MetadataReference>.GetInstance();
			ArrayBuilder<Location> arrayBuilder = null;
			IDictionary<ValueTuple<string, string>, MetadataReference> dictionary = null;
			try
			{
				foreach (ReferenceDirective referenceDirective in compilation.ReferenceDirectives)
				{
					if (compilation.Options.MetadataReferenceResolver == null)
					{
						diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_MetadataReferencesNotSupported, referenceDirective.Location));
						break;
					}
					if (dictionary == null || !dictionary.ContainsKey(new ValueTuple<string, string>(referenceDirective.Location.SourceTree.FilePath, referenceDirective.File)))
					{
						MetadataReference metadataReference = CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolveReferenceDirective(referenceDirective.File, referenceDirective.Location, compilation);
						if (metadataReference == null)
						{
							diagnostics.Add(this.MessageProvider.CreateDiagnostic(this.MessageProvider.ERR_MetadataFileNotFound, referenceDirective.Location, new object[] { referenceDirective.File }));
						}
						else
						{
							if (dictionary == null)
							{
								dictionary = new Dictionary<ValueTuple<string, string>, MetadataReference>();
								arrayBuilder = ArrayBuilder<Location>.GetInstance();
							}
							instance.Add(metadataReference);
							arrayBuilder.Add(referenceDirective.Location);
							dictionary.Add(new ValueTuple<string, string>(referenceDirective.Location.SourceTree.FilePath, referenceDirective.File), metadataReference);
						}
					}
				}
				instance.AddRange(compilation.ExternalReferences);
				ScriptCompilationInfo scriptCompilationInfo = compilation.ScriptCompilationInfo;
				Compilation compilation2 = ((scriptCompilationInfo != null) ? scriptCompilationInfo.PreviousScriptCompilation : null);
				if (compilation2 != null)
				{
					instance.AddRange(compilation2.GetBoundReferenceManager().ExplicitReferences);
				}
				if (dictionary == null)
				{
					dictionary = SpecializedCollections.EmptyDictionary<ValueTuple<string, string>, MetadataReference>();
				}
				boundReferenceDirectives = dictionary;
				references = instance.ToImmutable();
				referenceDirectiveLocations = ((arrayBuilder != null) ? arrayBuilder.ToImmutableAndFree() : ImmutableArray<Location>.Empty);
			}
			finally
			{
				instance.Free();
			}
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0003C04C File Offset: 0x0003A24C
		[return: Nullable(2)]
		private static PortableExecutableReference ResolveReferenceDirective(string reference, Location location, TCompilation compilation)
		{
			SyntaxTree sourceTree = location.SourceTree;
			string text = ((sourceTree != null && sourceTree.FilePath.Length > 0) ? sourceTree.FilePath : null);
			ImmutableArray<PortableExecutableReference> immutableArray = compilation.Options.MetadataReferenceResolver.ResolveReference(reference, text, MetadataReferenceProperties.Assembly.WithRecursiveAliases(true));
			if (immutableArray.IsDefaultOrEmpty)
			{
				return null;
			}
			if (immutableArray.Length > 1)
			{
				throw new NotSupportedException();
			}
			return immutableArray[0];
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0003C0C4 File Offset: 0x0003A2C4
		[return: Nullable(new byte[] { 1, 0, 0, 0 })]
		internal static CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding[] ResolveReferencedAssemblies([Nullable(new byte[] { 0, 1 })] ImmutableArray<AssemblyIdentity> references, [Nullable(new byte[] { 0, 1, 0, 0 })] ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData> definitions, int definitionStartIndex, AssemblyIdentityComparer assemblyIdentityComparer)
		{
			CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding[] array = new CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding[references.Length];
			for (int i = 0; i < references.Length; i++)
			{
				array[i] = CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolveReferencedAssembly(references[i], definitions, definitionStartIndex, assemblyIdentityComparer);
			}
			return array;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0003C108 File Offset: 0x0003A308
		[return: Nullable(0)]
		internal static CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding ResolveReferencedAssembly(AssemblyIdentity reference, [Nullable(new byte[] { 0, 1, 0, 0 })] ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData> definitions, int definitionStartIndex, AssemblyIdentityComparer assemblyIdentityComparer)
		{
			int num = -1;
			int num2 = -1;
			bool flag = definitionStartIndex == 0;
			definitionStartIndex = Math.Max(definitionStartIndex, 1);
			for (int i = definitionStartIndex; i < definitions.Length; i++)
			{
				AssemblyIdentity identity = definitions[i].Identity;
				switch (assemblyIdentityComparer.Compare(reference, identity))
				{
				case AssemblyIdentityComparer.ComparisonResult.NotEquivalent:
					break;
				case AssemblyIdentityComparer.ComparisonResult.Equivalent:
					return new CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding(reference, i, 0);
				case AssemblyIdentityComparer.ComparisonResult.EquivalentIgnoringVersion:
					if (reference.Version < identity.Version)
					{
						if (num == -1 || identity.Version < definitions[num].Identity.Version)
						{
							num = i;
						}
					}
					else if (num2 == -1 || identity.Version > definitions[num2].Identity.Version)
					{
						num2 = i;
					}
					break;
				default:
					throw ExceptionUtilities.Unreachable;
				}
			}
			if (num != -1)
			{
				return new CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding(reference, num, 1);
			}
			if (num2 != -1)
			{
				return new CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding(reference, num2, -1);
			}
			if (reference.IsWindowsComponent())
			{
				for (int j = definitionStartIndex; j < definitions.Length; j++)
				{
					if (definitions[j].Identity.IsWindowsRuntime())
					{
						return new CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding(reference, j, 0);
					}
				}
			}
			if (reference.ContentType == 1)
			{
				for (int k = definitionStartIndex; k < definitions.Length; k++)
				{
					AssemblyIdentity identity2 = definitions[k].Identity;
					Compilation sourceCompilation = definitions[k].SourceCompilation;
					if (identity2.ContentType == null && sourceCompilation != null && sourceCompilation.Options.OutputKind == OutputKind.WindowsRuntimeMetadata && AssemblyIdentityComparer.SimpleNameComparer.Equals(reference.Name, identity2.Name) && reference.Version.Equals(identity2.Version) && reference.IsRetargetable == identity2.IsRetargetable && AssemblyIdentityComparer.CultureComparer.Equals(reference.CultureName, identity2.CultureName) && AssemblyIdentity.KeysEqual(reference, identity2))
					{
						return new CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding(reference, k, 0);
					}
				}
			}
			if (flag && AssemblyIdentityComparer.SimpleNameComparer.Equals(reference.Name, definitions[0].Identity.Name))
			{
				return new CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding(reference, 0, 0);
			}
			return new CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding(reference);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0003C33B File Offset: 0x0003A53B
		public CommonReferenceManager(string simpleAssemblyName, AssemblyIdentityComparer identityComparer, [Nullable(new byte[] { 2, 1, 1 })] Dictionary<MetadataReference, object> observedMetadata)
		{
			this.SimpleAssemblyName = simpleAssemblyName;
			this.IdentityComparer = identityComparer;
			this.ObservedMetadata = observedMetadata ?? new Dictionary<MetadataReference, object>();
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x0003C361 File Offset: 0x0003A561
		[Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<Diagnostic> Diagnostics
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._lazyDiagnostics;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x0003C369 File Offset: 0x0003A569
		internal bool HasCircularReference
		{
			get
			{
				return this._lazyHasCircularReference == ThreeState.True;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x0003C374 File Offset: 0x0003A574
		internal Dictionary<MetadataReference, int> ReferencedAssembliesMap
		{
			get
			{
				return this._lazyReferencedAssembliesMap;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x0003C37C File Offset: 0x0003A57C
		internal Dictionary<MetadataReference, int> ReferencedModuleIndexMap
		{
			get
			{
				return this._lazyReferencedModuleIndexMap;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x0003C384 File Offset: 0x0003A584
		[Nullable(new byte[] { 1, 0, 1, 1, 1 })]
		internal IDictionary<ValueTuple<string, string>, MetadataReference> ReferenceDirectiveMap
		{
			[return: Nullable(new byte[] { 1, 0, 1, 1, 1 })]
			get
			{
				return this._lazyReferenceDirectiveMap;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x0003C38C File Offset: 0x0003A58C
		[Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<MetadataReference> DirectiveReferences
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._lazyDirectiveReferences;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x0003C394 File Offset: 0x0003A594
		[Nullable(new byte[] { 1, 1, 2 })]
		internal override ImmutableDictionary<AssemblyIdentity, PortableExecutableReference> ImplicitReferenceResolutions
		{
			[return: Nullable(new byte[] { 1, 1, 2 })]
			get
			{
				return this._lazyImplicitReferenceResolutions;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x0003C39C File Offset: 0x0003A59C
		[Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<MetadataReference> ExplicitReferences
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._lazyExplicitReferences;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x0003C3A4 File Offset: 0x0003A5A4
		[Nullable(2)]
		internal TAssemblySymbol CorLibraryOpt
		{
			[NullableContext(2)]
			get
			{
				return this._lazyCorLibraryOpt;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x0003C3AC File Offset: 0x0003A5AC
		[Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<PEModule> ReferencedModules
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._lazyReferencedModules;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x0003C3B4 File Offset: 0x0003A5B4
		[Nullable(new byte[] { 0, 1, 1 })]
		internal ImmutableArray<ModuleReferences<TAssemblySymbol>> ReferencedModulesReferences
		{
			[return: Nullable(new byte[] { 0, 1, 1 })]
			get
			{
				return this._lazyReferencedModulesReferences;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x0003C3BC File Offset: 0x0003A5BC
		[Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<TAssemblySymbol> ReferencedAssemblies
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._lazyReferencedAssemblies;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x0003C3C4 File Offset: 0x0003A5C4
		[Nullable(new byte[] { 0, 0, 1 })]
		internal ImmutableArray<ImmutableArray<string>> AliasesOfReferencedAssemblies
		{
			[return: Nullable(new byte[] { 0, 0, 1 })]
			get
			{
				return this._lazyAliasesOfReferencedAssemblies;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x0003C3CC File Offset: 0x0003A5CC
		[Nullable(new byte[] { 1, 1, 0, 1 })]
		internal ImmutableDictionary<MetadataReference, ImmutableArray<MetadataReference>> MergedAssemblyReferencesMap
		{
			[return: Nullable(new byte[] { 1, 1, 0, 1 })]
			get
			{
				return this._lazyMergedAssemblyReferencesMap;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x0003C3D4 File Offset: 0x0003A5D4
		[Nullable(new byte[] { 0, 0, 1 })]
		internal ImmutableArray<UnifiedAssembly<TAssemblySymbol>> UnifiedAssemblies
		{
			[return: Nullable(new byte[] { 0, 0, 1 })]
			get
			{
				return this._lazyUnifiedAssemblies;
			}
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0003C3DC File Offset: 0x0003A5DC
		[Conditional("DEBUG")]
		internal void AssertUnbound()
		{
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0003C3DE File Offset: 0x0003A5DE
		[Conditional("DEBUG")]
		[MemberNotNull(new string[] { "_lazyReferencedAssembliesMap", "_lazyReferencedModuleIndexMap", "_lazyReferenceDirectiveMap", "_lazyImplicitReferenceResolutions" })]
		internal void AssertBound()
		{
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0003C3E0 File Offset: 0x0003A5E0
		[Conditional("DEBUG")]
		internal void AssertCanReuseForCompilation(TCompilation compilation)
		{
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x0003C3E2 File Offset: 0x0003A5E2
		internal bool IsBound
		{
			get
			{
				return this._isBound != 0;
			}
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0003C3F0 File Offset: 0x0003A5F0
		internal void InitializeNoLock(Dictionary<MetadataReference, int> referencedAssembliesMap, Dictionary<MetadataReference, int> referencedModulesMap, [Nullable(new byte[] { 1, 0, 1, 1, 1 })] IDictionary<ValueTuple<string, string>, MetadataReference> boundReferenceDirectiveMap, [Nullable(new byte[] { 0, 1 })] ImmutableArray<MetadataReference> directiveReferences, [Nullable(new byte[] { 0, 1 })] ImmutableArray<MetadataReference> explicitReferences, [Nullable(new byte[] { 1, 1, 2 })] ImmutableDictionary<AssemblyIdentity, PortableExecutableReference> implicitReferenceResolutions, bool containsCircularReferences, [Nullable(new byte[] { 0, 1 })] ImmutableArray<Diagnostic> diagnostics, [Nullable(2)] TAssemblySymbol corLibraryOpt, [Nullable(new byte[] { 0, 1 })] ImmutableArray<PEModule> referencedModules, [Nullable(new byte[] { 0, 1, 1 })] ImmutableArray<ModuleReferences<TAssemblySymbol>> referencedModulesReferences, [Nullable(new byte[] { 0, 1 })] ImmutableArray<TAssemblySymbol> referencedAssemblies, [Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<ImmutableArray<string>> aliasesOfReferencedAssemblies, [Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<UnifiedAssembly<TAssemblySymbol>> unifiedAssemblies, [Nullable(new byte[] { 2, 1, 0, 1 })] Dictionary<MetadataReference, ImmutableArray<MetadataReference>> mergedAssemblyReferencesMapOpt)
		{
			this._lazyReferencedAssembliesMap = referencedAssembliesMap;
			this._lazyReferencedModuleIndexMap = referencedModulesMap;
			this._lazyDiagnostics = diagnostics;
			this._lazyReferenceDirectiveMap = boundReferenceDirectiveMap;
			this._lazyDirectiveReferences = directiveReferences;
			this._lazyExplicitReferences = explicitReferences;
			this._lazyImplicitReferenceResolutions = implicitReferenceResolutions;
			this._lazyCorLibraryOpt = corLibraryOpt;
			this._lazyReferencedModules = referencedModules;
			this._lazyReferencedModulesReferences = referencedModulesReferences;
			this._lazyReferencedAssemblies = referencedAssemblies;
			this._lazyAliasesOfReferencedAssemblies = aliasesOfReferencedAssemblies;
			this._lazyMergedAssemblyReferencesMap = ((mergedAssemblyReferencesMapOpt != null) ? mergedAssemblyReferencesMapOpt.ToImmutableDictionary<MetadataReference, ImmutableArray<MetadataReference>>() : null) ?? ImmutableDictionary<MetadataReference, ImmutableArray<MetadataReference>>.Empty;
			this._lazyUnifiedAssemblies = unifiedAssemblies;
			this._lazyHasCircularReference = containsCircularReferences.ToThreeState();
			Interlocked.Exchange(ref this._isBound, 1);
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0003C49C File Offset: 0x0003A69C
		protected static void BuildReferencedAssembliesAndModulesMaps([Nullable(new byte[] { 1, 0, 0, 0 })] CommonReferenceManager<TCompilation, TAssemblySymbol>.BoundInputAssembly[] bindingResult, [Nullable(new byte[] { 0, 1 })] ImmutableArray<MetadataReference> references, [Nullable(0)] ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference> referenceMap, int referencedModuleCount, int explicitlyReferencedAssemblyCount, [Nullable(new byte[] { 1, 1, 1, 0, 0, 0 })] IReadOnlyDictionary<string, List<CommonReferenceManager<TCompilation, TAssemblySymbol>.ReferencedAssemblyIdentity>> assemblyReferencesBySimpleName, bool supersedeLowerVersions, out Dictionary<MetadataReference, int> referencedAssembliesMap, out Dictionary<MetadataReference, int> referencedModulesMap, [Nullable(new byte[] { 0, 0, 1 })] out ImmutableArray<ImmutableArray<string>> aliasesOfReferencedAssemblies, [Nullable(new byte[] { 2, 1, 0, 1 })] out Dictionary<MetadataReference, ImmutableArray<MetadataReference>> mergedAssemblyReferencesMapOpt)
		{
			referencedAssembliesMap = new Dictionary<MetadataReference, int>(referenceMap.Length);
			referencedModulesMap = new Dictionary<MetadataReference, int>(referencedModuleCount);
			ArrayBuilder<ImmutableArray<string>> instance = ArrayBuilder<ImmutableArray<string>>.GetInstance(referenceMap.Length - referencedModuleCount);
			bool flag = false;
			mergedAssemblyReferencesMapOpt = null;
			for (int i = 0; i < referenceMap.Length; i++)
			{
				if (!referenceMap[i].IsSkipped)
				{
					if (referenceMap[i].Kind == MetadataImageKind.Module)
					{
						int num = 1 + referenceMap[i].Index;
						referencedModulesMap.Add(references[i], num);
					}
					else
					{
						int index = referenceMap[i].Index;
						MetadataReference metadataReference = references[i];
						referencedAssembliesMap.Add(metadataReference, index);
						instance.Add(referenceMap[i].AliasesOpt);
						if (!referenceMap[i].MergedReferences.IsEmpty)
						{
							Dictionary<MetadataReference, ImmutableArray<MetadataReference>> dictionary;
							if ((dictionary = mergedAssemblyReferencesMapOpt) == null)
							{
								Dictionary<MetadataReference, ImmutableArray<MetadataReference>> dictionary2;
								mergedAssemblyReferencesMapOpt = (dictionary2 = new Dictionary<MetadataReference, ImmutableArray<MetadataReference>>());
								dictionary = dictionary2;
							}
							dictionary.Add(metadataReference, referenceMap[i].MergedReferences);
						}
						flag |= !referenceMap[i].RecursiveAliasesOpt.IsDefault;
					}
				}
			}
			if (flag)
			{
				CommonReferenceManager<TCompilation, TAssemblySymbol>.PropagateRecursiveAliases(bindingResult, referenceMap, instance);
			}
			if (supersedeLowerVersions)
			{
				foreach (KeyValuePair<string, List<CommonReferenceManager<TCompilation, TAssemblySymbol>.ReferencedAssemblyIdentity>> keyValuePair in assemblyReferencesBySimpleName)
				{
					for (int j = 1; j < keyValuePair.Value.Count; j++)
					{
						int assemblyIndex = keyValuePair.Value[j].GetAssemblyIndex(explicitlyReferencedAssemblyCount);
						instance[assemblyIndex] = CommonReferenceManager<TCompilation, TAssemblySymbol>.s_supersededAlias;
					}
				}
			}
			aliasesOfReferencedAssemblies = instance.ToImmutableAndFree();
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0003C680 File Offset: 0x0003A880
		internal static ImmutableDictionary<AssemblyIdentity, AssemblyIdentity> GetAssemblyReferenceIdentityBaselineMap([Nullable(new byte[] { 0, 1 })] ImmutableArray<TAssemblySymbol> symbols, [Nullable(new byte[] { 0, 1 })] ImmutableArray<AssemblyIdentity> originalIdentities)
		{
			ImmutableDictionary<AssemblyIdentity, AssemblyIdentity>.Builder builder = null;
			for (int i = 0; i < originalIdentities.Length; i++)
			{
				AssemblyIdentity identity = symbols[i].Identity;
				Version assemblyVersionPattern = symbols[i].AssemblyVersionPattern;
				AssemblyIdentity assemblyIdentity = originalIdentities[i];
				if (assemblyVersionPattern != null)
				{
					builder = builder ?? ImmutableDictionary.CreateBuilder<AssemblyIdentity, AssemblyIdentity>();
					AssemblyIdentity assemblyIdentity2 = identity.WithVersion(assemblyVersionPattern);
					if (builder.ContainsKey(assemblyIdentity2))
					{
						throw new NotSupportedException(CodeAnalysisResources.CompilationReferencesAssembliesWithDifferentAutoGeneratedVersion);
					}
					builder.Add(assemblyIdentity2, assemblyIdentity);
				}
			}
			return ((builder != null) ? builder.ToImmutable() : null) ?? ImmutableDictionary<AssemblyIdentity, AssemblyIdentity>.Empty;
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0003C71C File Offset: 0x0003A91C
		internal static bool CompareVersionPartsSpecifiedInSource(Version version, Version candidateVersion, TAssemblySymbol candidateSymbol)
		{
			if (version.Major != candidateVersion.Major || version.Minor != candidateVersion.Minor)
			{
				return false;
			}
			Version assemblyVersionPattern = candidateSymbol.AssemblyVersionPattern;
			return ((assemblyVersionPattern != null && assemblyVersionPattern.Build >= 65535) || version.Build == candidateVersion.Build) && (assemblyVersionPattern != null || version.Revision == candidateVersion.Revision);
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0003C788 File Offset: 0x0003A988
		[NullableContext(0)]
		private static void PropagateRecursiveAliases([Nullable(new byte[] { 1, 0, 0, 0 })] CommonReferenceManager<TCompilation, TAssemblySymbol>.BoundInputAssembly[] bindingResult, ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference> referenceMap, [Nullable(new byte[] { 1, 0, 1 })] ArrayBuilder<ImmutableArray<string>> aliasesOfReferencedAssembliesBuilder)
		{
			ArrayBuilder<int> instance = ArrayBuilder<int>.GetInstance();
			BitVector bitVector = BitVector.Create(bindingResult.Length);
			foreach (CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference resolvedReference in referenceMap)
			{
				if (!resolvedReference.IsSkipped && !resolvedReference.RecursiveAliasesOpt.IsDefault)
				{
					ImmutableArray<string> recursiveAliasesOpt = resolvedReference.RecursiveAliasesOpt;
					bitVector.Clear();
					instance.Add(resolvedReference.Index);
					while (instance.Count > 0)
					{
						int num = instance.Pop<int>();
						bitVector[num] = true;
						aliasesOfReferencedAssembliesBuilder[num] = MergedAliases.Merge(aliasesOfReferencedAssembliesBuilder[num], recursiveAliasesOpt);
						foreach (CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding assemblyReferenceBinding in bindingResult[num + 1].ReferenceBinding)
						{
							if (assemblyReferenceBinding.IsBound)
							{
								int num2 = assemblyReferenceBinding.DefinitionIndex - 1;
								if (!bitVector[num2])
								{
									instance.Add(num2);
								}
							}
						}
					}
				}
			}
			for (int j = 0; j < aliasesOfReferencedAssembliesBuilder.Count; j++)
			{
				if (aliasesOfReferencedAssembliesBuilder[j].IsDefault)
				{
					aliasesOfReferencedAssembliesBuilder[j] = ImmutableArray<string>.Empty;
				}
			}
			instance.Free();
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x0003C8CB File Offset: 0x0003AACB
		internal IEnumerable<string> ExternAliases
		{
			get
			{
				return Enumerable.SelectMany<ImmutableArray<string>, string>(this.AliasesOfReferencedAssemblies, (ImmutableArray<string> aliases) => aliases);
			}
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0003C8FC File Offset: 0x0003AAFC
		[return: Nullable(new byte[] { 1, 0, 1, 1 })]
		internal sealed override IEnumerable<KeyValuePair<MetadataReference, IAssemblySymbolInternal>> GetReferencedAssemblies()
		{
			return Enumerable.Select<KeyValuePair<MetadataReference, int>, KeyValuePair<MetadataReference, IAssemblySymbolInternal>>(this.ReferencedAssembliesMap, (KeyValuePair<MetadataReference, int> ra) => KeyValuePairUtil.Create<MetadataReference, IAssemblySymbolInternal>(ra.Key, this.ReferencedAssemblies[ra.Value]));
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0003C918 File Offset: 0x0003AB18
		[return: Nullable(2)]
		internal TAssemblySymbol GetReferencedAssemblySymbol(MetadataReference reference)
		{
			int num;
			if (!this.ReferencedAssembliesMap.TryGetValue(reference, ref num))
			{
				return default(TAssemblySymbol);
			}
			return this.ReferencedAssemblies[num];
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0003C950 File Offset: 0x0003AB50
		internal int GetReferencedModuleIndex(MetadataReference reference)
		{
			int num;
			if (!this.ReferencedModuleIndexMap.TryGetValue(reference, ref num))
			{
				return -1;
			}
			return num;
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0003C970 File Offset: 0x0003AB70
		[NullableContext(2)]
		internal override MetadataReference GetMetadataReference(IAssemblySymbolInternal assemblySymbol)
		{
			foreach (KeyValuePair<MetadataReference, int> keyValuePair in this.ReferencedAssembliesMap)
			{
				if (this.ReferencedAssemblies[keyValuePair.Value] == assemblySymbol)
				{
					return keyValuePair.Key;
				}
			}
			return null;
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0003C9E8 File Offset: 0x0003ABE8
		[return: TupleElementNames(new string[] { "AssemblySymbol", "Aliases" })]
		[return: Nullable(new byte[] { 1, 0, 1, 0, 1 })]
		internal override IEnumerable<ValueTuple<IAssemblySymbolInternal, ImmutableArray<string>>> GetReferencedAssemblyAliases()
		{
			int num;
			for (int i = 0; i < this.ReferencedAssemblies.Length; i = num + 1)
			{
				yield return new ValueTuple<IAssemblySymbolInternal, ImmutableArray<string>>(this.ReferencedAssemblies[i], this.AliasesOfReferencedAssemblies[i]);
				num = i;
			}
			yield break;
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0003C9F8 File Offset: 0x0003ABF8
		public bool DeclarationsAccessibleWithoutAlias(int referencedAssemblyIndex)
		{
			ImmutableArray<string> immutableArray = this.AliasesOfReferencedAssemblies[referencedAssemblyIndex];
			return immutableArray.Length == 0 || immutableArray.IndexOf(MetadataReferenceProperties.GlobalAlias, StringComparer.Ordinal) >= 0;
		}

		// Token: 0x040005B3 RID: 1459
		internal readonly string SimpleAssemblyName;

		// Token: 0x040005B4 RID: 1460
		internal readonly AssemblyIdentityComparer IdentityComparer;

		// Token: 0x040005B5 RID: 1461
		internal readonly Dictionary<MetadataReference, object> ObservedMetadata;

		// Token: 0x040005B6 RID: 1462
		private int _isBound;

		// Token: 0x040005B7 RID: 1463
		private ThreeState _lazyHasCircularReference;

		// Token: 0x040005B8 RID: 1464
		[Nullable(new byte[] { 2, 1 })]
		private Dictionary<MetadataReference, int> _lazyReferencedAssembliesMap;

		// Token: 0x040005B9 RID: 1465
		[Nullable(new byte[] { 2, 1 })]
		private Dictionary<MetadataReference, int> _lazyReferencedModuleIndexMap;

		// Token: 0x040005BA RID: 1466
		[Nullable(new byte[] { 2, 0, 1, 1, 1 })]
		private IDictionary<ValueTuple<string, string>, MetadataReference> _lazyReferenceDirectiveMap;

		// Token: 0x040005BB RID: 1467
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<MetadataReference> _lazyDirectiveReferences;

		// Token: 0x040005BC RID: 1468
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<MetadataReference> _lazyExplicitReferences;

		// Token: 0x040005BD RID: 1469
		[Nullable(new byte[] { 2, 1, 2 })]
		private ImmutableDictionary<AssemblyIdentity, PortableExecutableReference> _lazyImplicitReferenceResolutions;

		// Token: 0x040005BE RID: 1470
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<Diagnostic> _lazyDiagnostics;

		// Token: 0x040005BF RID: 1471
		[Nullable(2)]
		private TAssemblySymbol _lazyCorLibraryOpt;

		// Token: 0x040005C0 RID: 1472
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<PEModule> _lazyReferencedModules;

		// Token: 0x040005C1 RID: 1473
		[Nullable(new byte[] { 0, 1, 1 })]
		private ImmutableArray<ModuleReferences<TAssemblySymbol>> _lazyReferencedModulesReferences;

		// Token: 0x040005C2 RID: 1474
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<TAssemblySymbol> _lazyReferencedAssemblies;

		// Token: 0x040005C3 RID: 1475
		[Nullable(new byte[] { 0, 0, 1 })]
		private ImmutableArray<ImmutableArray<string>> _lazyAliasesOfReferencedAssemblies;

		// Token: 0x040005C4 RID: 1476
		[Nullable(new byte[] { 2, 1, 0, 1 })]
		private ImmutableDictionary<MetadataReference, ImmutableArray<MetadataReference>> _lazyMergedAssemblyReferencesMap;

		// Token: 0x040005C5 RID: 1477
		[Nullable(new byte[] { 0, 0, 1 })]
		private ImmutableArray<UnifiedAssembly<TAssemblySymbol>> _lazyUnifiedAssemblies;

		// Token: 0x040005C6 RID: 1478
		[Nullable(new byte[] { 0, 1 })]
		private static readonly ImmutableArray<string> s_supersededAlias = ImmutableArray.Create<string>("<superseded>");

		// Token: 0x020005D3 RID: 1491
		[Nullable(0)]
		[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
		internal abstract class AssemblyData
		{
			// Token: 0x17000F6D RID: 3949
			// (get) Token: 0x06003688 RID: 13960
			public abstract AssemblyIdentity Identity { get; }

			// Token: 0x17000F6E RID: 3950
			// (get) Token: 0x06003689 RID: 13961
			[Nullable(new byte[] { 0, 1 })]
			public abstract ImmutableArray<AssemblyIdentity> AssemblyReferences
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get;
			}

			// Token: 0x17000F6F RID: 3951
			// (get) Token: 0x0600368A RID: 13962
			public abstract IEnumerable<TAssemblySymbol> AvailableSymbols { get; }

			// Token: 0x0600368B RID: 13963
			[NullableContext(2)]
			public abstract bool IsMatchingAssembly(TAssemblySymbol assembly);

			// Token: 0x0600368C RID: 13964
			[return: Nullable(new byte[] { 1, 0, 0, 0 })]
			public abstract CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding[] BindAssemblyReferences([Nullable(new byte[] { 0, 1, 0, 0 })] ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData> assemblies, AssemblyIdentityComparer assemblyIdentityComparer);

			// Token: 0x17000F70 RID: 3952
			// (get) Token: 0x0600368D RID: 13965
			public abstract bool ContainsNoPiaLocalTypes { get; }

			// Token: 0x17000F71 RID: 3953
			// (get) Token: 0x0600368E RID: 13966
			public abstract bool IsLinked { get; }

			// Token: 0x17000F72 RID: 3954
			// (get) Token: 0x0600368F RID: 13967
			public abstract bool DeclaresTheObjectClass { get; }

			// Token: 0x17000F73 RID: 3955
			// (get) Token: 0x06003690 RID: 13968
			[Nullable(2)]
			public abstract Compilation SourceCompilation
			{
				[NullableContext(2)]
				get;
			}

			// Token: 0x06003691 RID: 13969 RVA: 0x000997F9 File Offset: 0x000979F9
			private string GetDebuggerDisplay()
			{
				return base.GetType().Name + ": [" + this.Identity.GetDisplayName(false) + "]";
			}
		}

		// Token: 0x020005D4 RID: 1492
		[Nullable(0)]
		protected sealed class AssemblyDataForAssemblyBeingBuilt : CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData
		{
			// Token: 0x06003693 RID: 13971 RVA: 0x0009982C File Offset: 0x00097A2C
			public AssemblyDataForAssemblyBeingBuilt(AssemblyIdentity identity, [Nullable(new byte[] { 0, 1, 0, 0 })] ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData> referencedAssemblyData, [Nullable(new byte[] { 0, 1 })] ImmutableArray<PEModule> modules)
			{
				this._assemblyIdentity = identity;
				this._referencedAssemblyData = referencedAssemblyData;
				ArrayBuilder<AssemblyIdentity> instance = ArrayBuilder<AssemblyIdentity>.GetInstance(referencedAssemblyData.Length + modules.Length);
				foreach (CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData assemblyData in referencedAssemblyData)
				{
					instance.Add(assemblyData.Identity);
				}
				for (int i = 1; i <= modules.Length; i++)
				{
					instance.AddRange(modules[i - 1].ReferencedAssemblies);
				}
				this._referencedAssemblies = instance.ToImmutableAndFree();
			}

			// Token: 0x17000F74 RID: 3956
			// (get) Token: 0x06003694 RID: 13972 RVA: 0x000998BC File Offset: 0x00097ABC
			public override AssemblyIdentity Identity
			{
				get
				{
					return this._assemblyIdentity;
				}
			}

			// Token: 0x17000F75 RID: 3957
			// (get) Token: 0x06003695 RID: 13973 RVA: 0x000998C4 File Offset: 0x00097AC4
			[Nullable(new byte[] { 0, 1 })]
			public override ImmutableArray<AssemblyIdentity> AssemblyReferences
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return this._referencedAssemblies;
				}
			}

			// Token: 0x17000F76 RID: 3958
			// (get) Token: 0x06003696 RID: 13974 RVA: 0x000998CC File Offset: 0x00097ACC
			public override IEnumerable<TAssemblySymbol> AvailableSymbols
			{
				get
				{
					throw ExceptionUtilities.Unreachable;
				}
			}

			// Token: 0x06003697 RID: 13975 RVA: 0x000998D4 File Offset: 0x00097AD4
			[return: Nullable(new byte[] { 1, 0, 0, 0 })]
			public override CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding[] BindAssemblyReferences([Nullable(new byte[] { 0, 1, 0, 0 })] ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData> assemblies, AssemblyIdentityComparer assemblyIdentityComparer)
			{
				CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding[] array = new CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding[this._referencedAssemblies.Length];
				for (int i = 0; i < this._referencedAssemblyData.Length; i++)
				{
					array[i] = new CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding(assemblies[i + 1].Identity, i + 1, 0);
				}
				for (int j = this._referencedAssemblyData.Length; j < this._referencedAssemblies.Length; j++)
				{
					array[j] = CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolveReferencedAssembly(this._referencedAssemblies[j], assemblies, 1, assemblyIdentityComparer);
				}
				return array;
			}

			// Token: 0x06003698 RID: 13976 RVA: 0x00099978 File Offset: 0x00097B78
			[NullableContext(2)]
			public override bool IsMatchingAssembly(TAssemblySymbol assembly)
			{
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x17000F77 RID: 3959
			// (get) Token: 0x06003699 RID: 13977 RVA: 0x0009997F File Offset: 0x00097B7F
			public override bool ContainsNoPiaLocalTypes
			{
				get
				{
					throw ExceptionUtilities.Unreachable;
				}
			}

			// Token: 0x17000F78 RID: 3960
			// (get) Token: 0x0600369A RID: 13978 RVA: 0x00099986 File Offset: 0x00097B86
			public override bool IsLinked
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F79 RID: 3961
			// (get) Token: 0x0600369B RID: 13979 RVA: 0x00099989 File Offset: 0x00097B89
			public override bool DeclaresTheObjectClass
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F7A RID: 3962
			// (get) Token: 0x0600369C RID: 13980 RVA: 0x0009998C File Offset: 0x00097B8C
			[Nullable(2)]
			public override Compilation SourceCompilation
			{
				[NullableContext(2)]
				get
				{
					return null;
				}
			}

			// Token: 0x040016B5 RID: 5813
			private readonly AssemblyIdentity _assemblyIdentity;

			// Token: 0x040016B6 RID: 5814
			[Nullable(new byte[] { 0, 1, 0, 0 })]
			private readonly ImmutableArray<CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyData> _referencedAssemblyData;

			// Token: 0x040016B7 RID: 5815
			[Nullable(new byte[] { 0, 1 })]
			private readonly ImmutableArray<AssemblyIdentity> _referencedAssemblies;
		}

		// Token: 0x020005D5 RID: 1493
		[Nullable(0)]
		[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
		internal readonly struct AssemblyReferenceBinding
		{
			// Token: 0x0600369D RID: 13981 RVA: 0x0009998F File Offset: 0x00097B8F
			public AssemblyReferenceBinding(AssemblyIdentity referenceIdentity)
			{
				this._referenceIdentity = referenceIdentity;
				this._definitionIndex = -1;
				this._versionDifference = 0;
			}

			// Token: 0x0600369E RID: 13982 RVA: 0x000999A6 File Offset: 0x00097BA6
			public AssemblyReferenceBinding(AssemblyIdentity referenceIdentity, int definitionIndex, int versionDifference = 0)
			{
				this._referenceIdentity = referenceIdentity;
				this._definitionIndex = definitionIndex;
				this._versionDifference = versionDifference;
			}

			// Token: 0x17000F7B RID: 3963
			// (get) Token: 0x0600369F RID: 13983 RVA: 0x000999BD File Offset: 0x00097BBD
			internal bool BoundToAssemblyBeingBuilt
			{
				get
				{
					return this._definitionIndex == 0;
				}
			}

			// Token: 0x17000F7C RID: 3964
			// (get) Token: 0x060036A0 RID: 13984 RVA: 0x000999C8 File Offset: 0x00097BC8
			internal bool IsBound
			{
				get
				{
					return this._definitionIndex >= 0;
				}
			}

			// Token: 0x17000F7D RID: 3965
			// (get) Token: 0x060036A1 RID: 13985 RVA: 0x000999D6 File Offset: 0x00097BD6
			internal int VersionDifference
			{
				get
				{
					return this._versionDifference;
				}
			}

			// Token: 0x17000F7E RID: 3966
			// (get) Token: 0x060036A2 RID: 13986 RVA: 0x000999DE File Offset: 0x00097BDE
			internal int DefinitionIndex
			{
				get
				{
					return this._definitionIndex;
				}
			}

			// Token: 0x17000F7F RID: 3967
			// (get) Token: 0x060036A3 RID: 13987 RVA: 0x000999E6 File Offset: 0x00097BE6
			[Nullable(2)]
			internal AssemblyIdentity ReferenceIdentity
			{
				[NullableContext(2)]
				get
				{
					return this._referenceIdentity;
				}
			}

			// Token: 0x060036A4 RID: 13988 RVA: 0x000999F0 File Offset: 0x00097BF0
			private string GetDebuggerDisplay()
			{
				AssemblyIdentity referenceIdentity = this.ReferenceIdentity;
				string text = ((referenceIdentity != null) ? referenceIdentity.GetDisplayName(false) : null) ?? "";
				if (!this.IsBound)
				{
					return "unbound";
				}
				return text + " -> #" + this.DefinitionIndex.ToString() + ((this.VersionDifference != 0) ? (" VersionDiff=" + this.VersionDifference.ToString()) : "");
			}

			// Token: 0x040016B8 RID: 5816
			[Nullable(2)]
			private readonly AssemblyIdentity _referenceIdentity;

			// Token: 0x040016B9 RID: 5817
			private readonly int _definitionIndex;

			// Token: 0x040016BA RID: 5818
			private readonly int _versionDifference;
		}

		// Token: 0x020005D6 RID: 1494
		[NullableContext(0)]
		private readonly struct AssemblyReferenceCandidate
		{
			// Token: 0x060036A5 RID: 13989 RVA: 0x00099A68 File Offset: 0x00097C68
			[NullableContext(1)]
			public AssemblyReferenceCandidate(int definitionIndex, TAssemblySymbol symbol)
			{
				this.DefinitionIndex = definitionIndex;
				this.AssemblySymbol = symbol;
			}

			// Token: 0x040016BB RID: 5819
			public readonly int DefinitionIndex;

			// Token: 0x040016BC RID: 5820
			[Nullable(2)]
			public readonly TAssemblySymbol AssemblySymbol;
		}

		// Token: 0x020005D7 RID: 1495
		[NullableContext(2)]
		[Nullable(0)]
		[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
		internal struct BoundInputAssembly
		{
			// Token: 0x060036A6 RID: 13990 RVA: 0x00099A78 File Offset: 0x00097C78
			private string GetDebuggerDisplay()
			{
				if (this.AssemblySymbol != null)
				{
					return this.AssemblySymbol.ToString();
				}
				return "?";
			}

			// Token: 0x040016BD RID: 5821
			internal TAssemblySymbol AssemblySymbol;

			// Token: 0x040016BE RID: 5822
			[Nullable(new byte[] { 2, 0, 0, 0 })]
			internal CommonReferenceManager<TCompilation, TAssemblySymbol>.AssemblyReferenceBinding[] ReferenceBinding;
		}

		// Token: 0x020005D8 RID: 1496
		[Nullable(0)]
		[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
		protected struct ResolvedReference
		{
			// Token: 0x060036A7 RID: 13991 RVA: 0x00099A9D File Offset: 0x00097C9D
			public ResolvedReference(int index, MetadataImageKind kind)
			{
				this._index = index + 1;
				this._kind = kind;
				this._aliasesOpt = default(ImmutableArray<string>);
				this._recursiveAliasesOpt = default(ImmutableArray<string>);
				this._mergedReferencesOpt = default(ImmutableArray<MetadataReference>);
			}

			// Token: 0x060036A8 RID: 13992 RVA: 0x00099AD3 File Offset: 0x00097CD3
			public ResolvedReference(int index, MetadataImageKind kind, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> aliasesOpt, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> recursiveAliasesOpt, [Nullable(new byte[] { 0, 1 })] ImmutableArray<MetadataReference> mergedReferences)
			{
				this = new CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference(index, kind);
				this._aliasesOpt = aliasesOpt;
				this._recursiveAliasesOpt = recursiveAliasesOpt;
				this._mergedReferencesOpt = mergedReferences;
			}

			// Token: 0x17000F80 RID: 3968
			// (get) Token: 0x060036A9 RID: 13993 RVA: 0x00099AF4 File Offset: 0x00097CF4
			private bool IsUninitialized
			{
				get
				{
					return (this._aliasesOpt.IsDefault && this._recursiveAliasesOpt.IsDefault) || this._mergedReferencesOpt.IsDefault;
				}
			}

			// Token: 0x17000F81 RID: 3969
			// (get) Token: 0x060036AA RID: 13994 RVA: 0x00099B31 File Offset: 0x00097D31
			[Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<string> AliasesOpt
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return this._aliasesOpt;
				}
			}

			// Token: 0x17000F82 RID: 3970
			// (get) Token: 0x060036AB RID: 13995 RVA: 0x00099B39 File Offset: 0x00097D39
			[Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<string> RecursiveAliasesOpt
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return this._recursiveAliasesOpt;
				}
			}

			// Token: 0x17000F83 RID: 3971
			// (get) Token: 0x060036AC RID: 13996 RVA: 0x00099B41 File Offset: 0x00097D41
			[Nullable(new byte[] { 0, 1 })]
			public ImmutableArray<MetadataReference> MergedReferences
			{
				[return: Nullable(new byte[] { 0, 1 })]
				get
				{
					return this._mergedReferencesOpt;
				}
			}

			// Token: 0x17000F84 RID: 3972
			// (get) Token: 0x060036AD RID: 13997 RVA: 0x00099B49 File Offset: 0x00097D49
			public bool IsSkipped
			{
				get
				{
					return this._index == 0;
				}
			}

			// Token: 0x17000F85 RID: 3973
			// (get) Token: 0x060036AE RID: 13998 RVA: 0x00099B54 File Offset: 0x00097D54
			public MetadataImageKind Kind
			{
				get
				{
					return this._kind;
				}
			}

			// Token: 0x17000F86 RID: 3974
			// (get) Token: 0x060036AF RID: 13999 RVA: 0x00099B5C File Offset: 0x00097D5C
			public int Index
			{
				get
				{
					return this._index - 1;
				}
			}

			// Token: 0x060036B0 RID: 14000 RVA: 0x00099B68 File Offset: 0x00097D68
			private string GetDebuggerDisplay()
			{
				if (!this.IsSkipped)
				{
					return string.Format("{0}[{1}]:{2}{3}", new object[]
					{
						(this._kind == MetadataImageKind.Assembly) ? "A" : "M",
						this.Index,
						CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference.DisplayAliases(this._aliasesOpt, "aliases"),
						CommonReferenceManager<TCompilation, TAssemblySymbol>.ResolvedReference.DisplayAliases(this._recursiveAliasesOpt, "recursive-aliases")
					});
				}
				return "<skipped>";
			}

			// Token: 0x060036B1 RID: 14001 RVA: 0x00099BE0 File Offset: 0x00097DE0
			private static string DisplayAliases([Nullable(new byte[] { 0, 1 })] ImmutableArray<string> aliasesOpt, string name)
			{
				if (!aliasesOpt.IsDefault)
				{
					return string.Concat(new string[]
					{
						" ",
						name,
						" = '",
						string.Join("','", aliasesOpt),
						"'"
					});
				}
				return "";
			}

			// Token: 0x040016BF RID: 5823
			private readonly MetadataImageKind _kind;

			// Token: 0x040016C0 RID: 5824
			private readonly int _index;

			// Token: 0x040016C1 RID: 5825
			[Nullable(new byte[] { 0, 1 })]
			private readonly ImmutableArray<string> _aliasesOpt;

			// Token: 0x040016C2 RID: 5826
			[Nullable(new byte[] { 0, 1 })]
			private readonly ImmutableArray<string> _recursiveAliasesOpt;

			// Token: 0x040016C3 RID: 5827
			[Nullable(new byte[] { 0, 1 })]
			private readonly ImmutableArray<MetadataReference> _mergedReferencesOpt;
		}

		// Token: 0x020005D9 RID: 1497
		[NullableContext(2)]
		[Nullable(0)]
		protected readonly struct ReferencedAssemblyIdentity
		{
			// Token: 0x060036B2 RID: 14002 RVA: 0x00099C36 File Offset: 0x00097E36
			public int GetAssemblyIndex(int explicitlyReferencedAssemblyCount)
			{
				if (this.RelativeAssemblyIndex < 0)
				{
					return explicitlyReferencedAssemblyCount + this.RelativeAssemblyIndex;
				}
				return this.RelativeAssemblyIndex;
			}

			// Token: 0x060036B3 RID: 14003 RVA: 0x00099C50 File Offset: 0x00097E50
			[NullableContext(1)]
			public ReferencedAssemblyIdentity(AssemblyIdentity identity, MetadataReference reference, int relativeAssemblyIndex)
			{
				this.Identity = identity;
				this.Reference = reference;
				this.RelativeAssemblyIndex = relativeAssemblyIndex;
			}

			// Token: 0x040016C4 RID: 5828
			public readonly AssemblyIdentity Identity;

			// Token: 0x040016C5 RID: 5829
			public readonly MetadataReference Reference;

			// Token: 0x040016C6 RID: 5830
			public readonly int RelativeAssemblyIndex;
		}

		// Token: 0x020005DA RID: 1498
		[NullableContext(0)]
		internal sealed class MetadataReferenceEqualityComparer : IEqualityComparer<MetadataReference>
		{
			// Token: 0x060036B4 RID: 14004 RVA: 0x00099C68 File Offset: 0x00097E68
			[NullableContext(2)]
			public bool Equals(MetadataReference x, MetadataReference y)
			{
				if (x == y)
				{
					return true;
				}
				CompilationReference compilationReference = x as CompilationReference;
				if (compilationReference != null)
				{
					CompilationReference compilationReference2 = y as CompilationReference;
					if (compilationReference2 != null)
					{
						return compilationReference.Compilation == compilationReference2.Compilation;
					}
				}
				return false;
			}

			// Token: 0x060036B5 RID: 14005 RVA: 0x00099CA0 File Offset: 0x00097EA0
			[NullableContext(1)]
			public int GetHashCode(MetadataReference reference)
			{
				CompilationReference compilationReference = reference as CompilationReference;
				if (compilationReference != null)
				{
					return RuntimeHelpers.GetHashCode(compilationReference.Compilation);
				}
				return RuntimeHelpers.GetHashCode(reference);
			}

			// Token: 0x040016C7 RID: 5831
			[Nullable(new byte[] { 1, 0, 0 })]
			internal static readonly CommonReferenceManager<TCompilation, TAssemblySymbol>.MetadataReferenceEqualityComparer Instance = new CommonReferenceManager<TCompilation, TAssemblySymbol>.MetadataReferenceEqualityComparer();
		}
	}
}
