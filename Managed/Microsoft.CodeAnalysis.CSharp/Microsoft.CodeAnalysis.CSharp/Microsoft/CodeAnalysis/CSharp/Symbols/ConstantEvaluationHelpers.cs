using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000518 RID: 1304
	internal static class ConstantEvaluationHelpers
	{
		// Token: 0x06006D90 RID: 28048 RVA: 0x00192F1F File Offset: 0x0019111F
		internal static void OrderAllDependencies(this SourceFieldSymbolWithSyntaxReference field, ArrayBuilder<ConstantEvaluationHelpers.FieldInfo> order, bool earlyDecodingWellKnownAttributes)
		{
			PooledDictionary<SourceFieldSymbolWithSyntaxReference, ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference>> instance = PooledDictionary<SourceFieldSymbolWithSyntaxReference, ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference>>.GetInstance();
			ConstantEvaluationHelpers.CreateGraph(instance, field, earlyDecodingWellKnownAttributes);
			ConstantEvaluationHelpers.OrderGraph(instance, order);
			instance.Free();
		}

		// Token: 0x06006D91 RID: 28049 RVA: 0x00192F3C File Offset: 0x0019113C
		private static void CreateGraph(Dictionary<SourceFieldSymbolWithSyntaxReference, ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference>> graph, SourceFieldSymbolWithSyntaxReference field, bool earlyDecodingWellKnownAttributes)
		{
			ArrayBuilder<SourceFieldSymbolWithSyntaxReference> instance = ArrayBuilder<SourceFieldSymbolWithSyntaxReference>.GetInstance();
			instance.Push(field);
			while (instance.Count > 0)
			{
				field = instance.Pop<SourceFieldSymbolWithSyntaxReference>();
				ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference> node;
				if (graph.TryGetValue(field, ref node))
				{
					if (node.Dependencies != null)
					{
						continue;
					}
				}
				else
				{
					node = default(ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference>);
					node.DependedOnBy = ImmutableHashSet<SourceFieldSymbolWithSyntaxReference>.Empty;
				}
				ImmutableHashSet<SourceFieldSymbolWithSyntaxReference> constantValueDependencies = field.GetConstantValueDependencies(earlyDecodingWellKnownAttributes);
				node.Dependencies = constantValueDependencies;
				graph[field] = node;
				foreach (SourceFieldSymbolWithSyntaxReference sourceFieldSymbolWithSyntaxReference in constantValueDependencies)
				{
					instance.Push(sourceFieldSymbolWithSyntaxReference);
					if (!graph.TryGetValue(sourceFieldSymbolWithSyntaxReference, ref node))
					{
						node = default(ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference>);
						node.DependedOnBy = ImmutableHashSet<SourceFieldSymbolWithSyntaxReference>.Empty;
					}
					node.DependedOnBy = node.DependedOnBy.Add(field);
					graph[sourceFieldSymbolWithSyntaxReference] = node;
				}
			}
			instance.Free();
		}

		// Token: 0x06006D92 RID: 28050 RVA: 0x00193038 File Offset: 0x00191238
		private static void OrderGraph(Dictionary<SourceFieldSymbolWithSyntaxReference, ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference>> graph, ArrayBuilder<ConstantEvaluationHelpers.FieldInfo> order)
		{
			PooledHashSet<SourceFieldSymbolWithSyntaxReference> pooledHashSet = null;
			ArrayBuilder<SourceFieldSymbolWithSyntaxReference> arrayBuilder = null;
			while (graph.Count > 0)
			{
				IEnumerable<SourceFieldSymbolWithSyntaxReference> enumerable = pooledHashSet;
				IEnumerable<SourceFieldSymbolWithSyntaxReference> enumerable2 = enumerable ?? graph.Keys;
				ArrayBuilder<SourceFieldSymbolWithSyntaxReference> instance = ArrayBuilder<SourceFieldSymbolWithSyntaxReference>.GetInstance();
				foreach (SourceFieldSymbolWithSyntaxReference sourceFieldSymbolWithSyntaxReference in enumerable2)
				{
					ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference> node;
					if (graph.TryGetValue(sourceFieldSymbolWithSyntaxReference, ref node) && node.Dependencies.Count == 0)
					{
						instance.Add(sourceFieldSymbolWithSyntaxReference);
					}
				}
				if (pooledHashSet != null)
				{
					pooledHashSet.Free();
				}
				pooledHashSet = null;
				if (instance.Count > 0)
				{
					PooledHashSet<SourceFieldSymbolWithSyntaxReference> instance2 = PooledHashSet<SourceFieldSymbolWithSyntaxReference>.GetInstance();
					foreach (SourceFieldSymbolWithSyntaxReference sourceFieldSymbolWithSyntaxReference2 in instance)
					{
						foreach (SourceFieldSymbolWithSyntaxReference sourceFieldSymbolWithSyntaxReference3 in graph[sourceFieldSymbolWithSyntaxReference2].DependedOnBy)
						{
							ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference> node2 = graph[sourceFieldSymbolWithSyntaxReference3];
							node2.Dependencies = node2.Dependencies.Remove(sourceFieldSymbolWithSyntaxReference2);
							graph[sourceFieldSymbolWithSyntaxReference3] = node2;
							instance2.Add(sourceFieldSymbolWithSyntaxReference3);
						}
						graph.Remove(sourceFieldSymbolWithSyntaxReference2);
					}
					foreach (SourceFieldSymbolWithSyntaxReference sourceFieldSymbolWithSyntaxReference4 in instance)
					{
						order.Add(new ConstantEvaluationHelpers.FieldInfo(sourceFieldSymbolWithSyntaxReference4, false));
					}
					pooledHashSet = instance2;
				}
				else
				{
					SourceFieldSymbolWithSyntaxReference startOfFirstCycle = ConstantEvaluationHelpers.GetStartOfFirstCycle(graph, ref arrayBuilder);
					foreach (SourceFieldSymbolWithSyntaxReference sourceFieldSymbolWithSyntaxReference5 in graph[startOfFirstCycle].Dependencies)
					{
						ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference> node3 = graph[sourceFieldSymbolWithSyntaxReference5];
						node3.DependedOnBy = node3.DependedOnBy.Remove(startOfFirstCycle);
						graph[sourceFieldSymbolWithSyntaxReference5] = node3;
					}
					ref ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference> ptr = graph[startOfFirstCycle];
					PooledHashSet<SourceFieldSymbolWithSyntaxReference> instance3 = PooledHashSet<SourceFieldSymbolWithSyntaxReference>.GetInstance();
					foreach (SourceFieldSymbolWithSyntaxReference sourceFieldSymbolWithSyntaxReference6 in ptr.DependedOnBy)
					{
						ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference> node4 = graph[sourceFieldSymbolWithSyntaxReference6];
						node4.Dependencies = node4.Dependencies.Remove(startOfFirstCycle);
						graph[sourceFieldSymbolWithSyntaxReference6] = node4;
						instance3.Add(sourceFieldSymbolWithSyntaxReference6);
					}
					graph.Remove(startOfFirstCycle);
					order.Add(new ConstantEvaluationHelpers.FieldInfo(startOfFirstCycle, true));
					pooledHashSet = instance3;
				}
				instance.Free();
			}
			if (pooledHashSet != null)
			{
				pooledHashSet.Free();
			}
			if (arrayBuilder != null)
			{
				arrayBuilder.Free();
			}
		}

		// Token: 0x06006D93 RID: 28051 RVA: 0x001932E0 File Offset: 0x001914E0
		private static SourceFieldSymbolWithSyntaxReference GetStartOfFirstCycle(Dictionary<SourceFieldSymbolWithSyntaxReference, ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference>> graph, ref ArrayBuilder<SourceFieldSymbolWithSyntaxReference> fieldsInvolvedInCycles)
		{
			if (fieldsInvolvedInCycles == null)
			{
				fieldsInvolvedInCycles = ArrayBuilder<SourceFieldSymbolWithSyntaxReference>.GetInstance(graph.Count);
				fieldsInvolvedInCycles.AddRange(Enumerable.SelectMany<IGrouping<CSharpCompilation, SourceFieldSymbolWithSyntaxReference>, SourceFieldSymbolWithSyntaxReference>(Enumerable.GroupBy<SourceFieldSymbolWithSyntaxReference, CSharpCompilation>(graph.Keys, (SourceFieldSymbolWithSyntaxReference f) => f.DeclaringCompilation), (IGrouping<CSharpCompilation, SourceFieldSymbolWithSyntaxReference> g) => g.OrderByDescending<SourceFieldSymbolWithSyntaxReference>((SourceFieldSymbolWithSyntaxReference f1, SourceFieldSymbolWithSyntaxReference f2) => g.Key.CompareSourceLocations(f1.ErrorLocation, f2.ErrorLocation))));
			}
			SourceFieldSymbolWithSyntaxReference sourceFieldSymbolWithSyntaxReference;
			do
			{
				sourceFieldSymbolWithSyntaxReference = fieldsInvolvedInCycles.Pop<SourceFieldSymbolWithSyntaxReference>();
			}
			while (!graph.ContainsKey(sourceFieldSymbolWithSyntaxReference) || !ConstantEvaluationHelpers.IsPartOfCycle(graph, sourceFieldSymbolWithSyntaxReference));
			return sourceFieldSymbolWithSyntaxReference;
		}

		// Token: 0x06006D94 RID: 28052 RVA: 0x00193370 File Offset: 0x00191570
		private static bool IsPartOfCycle(Dictionary<SourceFieldSymbolWithSyntaxReference, ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference>> graph, SourceFieldSymbolWithSyntaxReference field)
		{
			PooledHashSet<SourceFieldSymbolWithSyntaxReference> instance = PooledHashSet<SourceFieldSymbolWithSyntaxReference>.GetInstance();
			ArrayBuilder<SourceFieldSymbolWithSyntaxReference> instance2 = ArrayBuilder<SourceFieldSymbolWithSyntaxReference>.GetInstance();
			SourceFieldSymbolWithSyntaxReference sourceFieldSymbolWithSyntaxReference = field;
			bool flag = false;
			instance2.Push(field);
			while (instance2.Count > 0)
			{
				field = instance2.Pop<SourceFieldSymbolWithSyntaxReference>();
				ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference> node = graph[field];
				if (node.Dependencies.Contains(sourceFieldSymbolWithSyntaxReference))
				{
					flag = true;
					break;
				}
				foreach (SourceFieldSymbolWithSyntaxReference sourceFieldSymbolWithSyntaxReference2 in node.Dependencies)
				{
					if (instance.Add(sourceFieldSymbolWithSyntaxReference2))
					{
						instance2.Push(sourceFieldSymbolWithSyntaxReference2);
					}
				}
			}
			instance2.Free();
			instance.Free();
			return flag;
		}

		// Token: 0x06006D95 RID: 28053 RVA: 0x00193424 File Offset: 0x00191624
		[Conditional("DEBUG")]
		private static void CheckGraph(Dictionary<SourceFieldSymbolWithSyntaxReference, ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference>> graph)
		{
			int num = 10;
			foreach (KeyValuePair<SourceFieldSymbolWithSyntaxReference, ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference>> keyValuePair in graph)
			{
				SourceFieldSymbolWithSyntaxReference key = keyValuePair.Key;
				ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference> value = keyValuePair.Value;
				foreach (SourceFieldSymbolWithSyntaxReference sourceFieldSymbolWithSyntaxReference in value.Dependencies)
				{
					ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference> node;
					graph.TryGetValue(sourceFieldSymbolWithSyntaxReference, ref node);
				}
				foreach (SourceFieldSymbolWithSyntaxReference sourceFieldSymbolWithSyntaxReference2 in value.DependedOnBy)
				{
					ConstantEvaluationHelpers.Node<SourceFieldSymbolWithSyntaxReference> node2;
					graph.TryGetValue(sourceFieldSymbolWithSyntaxReference2, ref node2);
				}
				num--;
				if (num == 0)
				{
					break;
				}
			}
		}

		// Token: 0x02000909 RID: 2313
		[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
		internal struct FieldInfo
		{
			// Token: 0x06008F98 RID: 36760 RVA: 0x001F9338 File Offset: 0x001F7538
			public FieldInfo(SourceFieldSymbolWithSyntaxReference field, bool startsCycle)
			{
				this.Field = field;
				this.StartsCycle = startsCycle;
			}

			// Token: 0x06008F99 RID: 36761 RVA: 0x001F9348 File Offset: 0x001F7548
			private string GetDebuggerDisplay()
			{
				string text = this.Field.ToString();
				if (this.StartsCycle)
				{
					text += " [cycle]";
				}
				return text;
			}

			// Token: 0x0400274C RID: 10060
			public readonly SourceFieldSymbolWithSyntaxReference Field;

			// Token: 0x0400274D RID: 10061
			public readonly bool StartsCycle;
		}

		// Token: 0x0200090A RID: 2314
		private struct Node<T> where T : class
		{
			// Token: 0x0400274E RID: 10062
			public ImmutableHashSet<T> Dependencies;

			// Token: 0x0400274F RID: 10063
			public ImmutableHashSet<T> DependedOnBy;
		}
	}
}
