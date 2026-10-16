using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.CodeGen
{
	// Token: 0x020005F5 RID: 1525
	internal class Optimizer
	{
		// Token: 0x060080E8 RID: 33000 RVA: 0x001D5018 File Offset: 0x001D3218
		public static BoundStatement Optimize(BoundStatement src, bool debugFriendly, out HashSet<LocalSymbol> stackLocals)
		{
			PooledDictionary<LocalSymbol, LocalDefUseInfo> instance = PooledDictionary<LocalSymbol, LocalDefUseInfo>.GetInstance();
			src = (BoundStatement)StackOptimizerPass1.Analyze(src, instance, debugFriendly);
			Optimizer.FilterValidStackLocals(instance);
			BoundStatement boundStatement;
			if (instance.Count == 0)
			{
				stackLocals = null;
				boundStatement = src;
			}
			else
			{
				stackLocals = new HashSet<LocalSymbol>(instance.Keys);
				boundStatement = StackOptimizerPass2.Rewrite(src, instance);
			}
			foreach (LocalDefUseInfo localDefUseInfo in instance.Values)
			{
				localDefUseInfo.Free();
			}
			instance.Free();
			return boundStatement;
		}

		// Token: 0x060080E9 RID: 33001 RVA: 0x001D50B0 File Offset: 0x001D32B0
		private static void FilterValidStackLocals(Dictionary<LocalSymbol, LocalDefUseInfo> info)
		{
			ArrayBuilder<LocalDefUseInfo> instance = ArrayBuilder<LocalDefUseInfo>.GetInstance();
			foreach (LocalSymbol localSymbol in Enumerable.ToArray<LocalSymbol>(info.Keys))
			{
				LocalDefUseInfo localDefUseInfo = info[localSymbol];
				if (localSymbol.SynthesizedKind == SynthesizedLocalKind.OptimizerTemp)
				{
					instance.Add(localDefUseInfo);
					info.Remove(localSymbol);
				}
				else if (localDefUseInfo.CannotSchedule)
				{
					localDefUseInfo.Free();
					info.Remove(localSymbol);
				}
			}
			if (info.Count != 0)
			{
				Optimizer.RemoveIntersectingLocals(info, instance);
			}
			foreach (LocalDefUseInfo localDefUseInfo2 in instance)
			{
				localDefUseInfo2.Free();
			}
			instance.Free();
		}

		// Token: 0x060080EA RID: 33002 RVA: 0x001D5154 File Offset: 0x001D3354
		private static void RemoveIntersectingLocals(Dictionary<LocalSymbol, LocalDefUseInfo> info, ArrayBuilder<LocalDefUseInfo> dummies)
		{
			ArrayBuilder<LocalDefUseSpan> instance = ArrayBuilder<LocalDefUseSpan>.GetInstance(dummies.Count);
			foreach (LocalDefUseInfo localDefUseInfo in dummies)
			{
				foreach (LocalDefUseSpan localDefUseSpan in localDefUseInfo.LocalDefs)
				{
					if (localDefUseSpan.Start != localDefUseSpan.End)
					{
						instance.Add(localDefUseSpan);
					}
				}
			}
			int count = instance.Count;
			foreach (var anon in Enumerable.Select(Enumerable.ThenBy(Enumerable.OrderBy(Enumerable.SelectMany(info, delegate(KeyValuePair<LocalSymbol, LocalDefUseInfo> i)
			{
				KeyValuePair<LocalSymbol, LocalDefUseInfo> keyValuePair = i;
				return keyValuePair.Value.LocalDefs;
			}, (KeyValuePair<LocalSymbol, LocalDefUseInfo> i, LocalDefUseSpan d) => new { i, d }), <>h__TransparentIdentifier0 => <>h__TransparentIdentifier0.d.End - <>h__TransparentIdentifier0.d.Start), <>h__TransparentIdentifier0 => <>h__TransparentIdentifier0.d.End), <>h__TransparentIdentifier0 => new
			{
				i = <>h__TransparentIdentifier0.i.Key,
				d = <>h__TransparentIdentifier0.d
			}))
			{
				if (info.ContainsKey(anon.i))
				{
					LocalDefUseSpan d2 = anon.d;
					int count2 = instance.Count;
					bool flag;
					if (count2 > 5000)
					{
						flag = true;
					}
					else
					{
						flag = false;
						for (int k = 0; k < count; k++)
						{
							LocalDefUseSpan localDefUseSpan2 = instance[k];
							if (d2.ConflictsWithDummy(localDefUseSpan2))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							for (int j = count; j < count2; j++)
							{
								LocalDefUseSpan localDefUseSpan3 = instance[j];
								if (d2.ConflictsWith(localDefUseSpan3))
								{
									flag = true;
									break;
								}
							}
						}
					}
					if (flag)
					{
						info[anon.i].LocalDefs.Free();
						info.Remove(anon.i);
					}
					else
					{
						instance.Add(d2);
					}
				}
			}
			instance.Free();
		}
	}
}
