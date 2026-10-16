using System;
using System.Collections.Generic;
using Unity.Properties.Internal;

namespace Unity.Properties
{
	// Token: 0x0200007D RID: 125
	public abstract class PropertyVisitor : IPropertyBagVisitor, IListPropertyBagVisitor, IDictionaryPropertyBagVisitor, IPropertyVisitor, ICollectionPropertyVisitor, IListPropertyVisitor, ISetPropertyVisitor, IDictionaryPropertyVisitor
	{
		// Token: 0x060001EC RID: 492 RVA: 0x00006A98 File Offset: 0x00004C98
		public void AddAdapter(IPropertyVisitorAdapter adapter)
		{
			this.m_Adapters.Add(adapter);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006AA7 File Offset: 0x00004CA7
		public void RemoveAdapter(IPropertyVisitorAdapter adapter)
		{
			this.m_Adapters.Remove(adapter);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00006AB8 File Offset: 0x00004CB8
		void IPropertyBagVisitor.Visit<TContainer>(IPropertyBag<TContainer> properties, ref TContainer container)
		{
			foreach (IProperty<TContainer> property in properties.GetProperties(ref container))
			{
				property.Accept(this, ref container);
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006B18 File Offset: 0x00004D18
		void IListPropertyBagVisitor.Visit<TList, TElement>(IListPropertyBag<TList, TElement> properties, ref TList container)
		{
			foreach (IProperty<TList> property in properties.GetProperties(ref container))
			{
				property.Accept(this, ref container);
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006B78 File Offset: 0x00004D78
		void IDictionaryPropertyBagVisitor.Visit<TDictionary, TKey, TValue>(IDictionaryPropertyBag<TDictionary, TKey, TValue> properties, ref TDictionary container)
		{
			foreach (IProperty<TDictionary> property in properties.GetProperties(ref container))
			{
				property.Accept(this, ref container);
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006BD8 File Offset: 0x00004DD8
		void IPropertyVisitor.Visit<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container)
		{
			TValue value = property.GetValue(ref container);
			bool flag = this.IsExcluded<TContainer, TValue>(property, new ReadOnlyAdapterCollection(this.m_Adapters).GetEnumerator(), ref container, ref value);
			if (!flag)
			{
				bool flag2 = this.IsExcluded<TContainer, TValue>(property, ref container, ref value);
				if (!flag2)
				{
					this.ContinueVisitation<TContainer, TValue>(property, new ReadOnlyAdapterCollection(this.m_Adapters).GetEnumerator(), ref container, ref value);
					bool flag3 = !property.IsReadOnly;
					if (flag3)
					{
						property.SetValue(ref container, value);
					}
				}
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006C58 File Offset: 0x00004E58
		internal void ContinueVisitation<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
		{
			IPropertyBag propertyBag;
			bool flag = PropertyBagStore.TryGetPropertyBagForValue<TValue>(ref value, out propertyBag);
			if (flag)
			{
				IPropertyBag propertyBag2 = propertyBag;
				IPropertyBag propertyBag3 = propertyBag2;
				IDictionaryPropertyAccept<TValue> dictionaryPropertyAccept = propertyBag3 as IDictionaryPropertyAccept<TValue>;
				if (dictionaryPropertyAccept != null)
				{
					dictionaryPropertyAccept.Accept<TContainer>(this, property, ref container, ref value);
					return;
				}
				IListPropertyAccept<TValue> listPropertyAccept = propertyBag3 as IListPropertyAccept<TValue>;
				if (listPropertyAccept != null)
				{
					listPropertyAccept.Accept<TContainer>(this, property, ref container, ref value);
					return;
				}
				ISetPropertyAccept<TValue> setPropertyAccept = propertyBag3 as ISetPropertyAccept<TValue>;
				if (setPropertyAccept != null)
				{
					setPropertyAccept.Accept<TContainer>(this, property, ref container, ref value);
					return;
				}
				ICollectionPropertyAccept<TValue> collectionPropertyAccept = propertyBag3 as ICollectionPropertyAccept<TValue>;
				if (collectionPropertyAccept != null)
				{
					collectionPropertyAccept.Accept<TContainer>(this, property, ref container, ref value);
					return;
				}
			}
			this.VisitProperty<TContainer, TValue>(property, ref container, ref value);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00006CF5 File Offset: 0x00004EF5
		void ICollectionPropertyVisitor.Visit<TContainer, TCollection, TElement>(Property<TContainer, TCollection> property, ref TContainer container, ref TCollection collection)
		{
			this.VisitCollection<TContainer, TCollection, TElement>(property, ref container, ref collection);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00006D02 File Offset: 0x00004F02
		void IListPropertyVisitor.Visit<TContainer, TList, TElement>(Property<TContainer, TList> property, ref TContainer container, ref TList list)
		{
			this.VisitList<TContainer, TList, TElement>(property, ref container, ref list);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00006D0F File Offset: 0x00004F0F
		void ISetPropertyVisitor.Visit<TContainer, TSet, TElement>(Property<TContainer, TSet> property, ref TContainer container, ref TSet set)
		{
			this.VisitSet<TContainer, TSet, TElement>(property, ref container, ref set);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00006D1C File Offset: 0x00004F1C
		void IDictionaryPropertyVisitor.Visit<TContainer, TDictionary, TKey, TValue>(Property<TContainer, TDictionary> property, ref TContainer container, ref TDictionary dictionary)
		{
			this.VisitDictionary<TContainer, TDictionary, TKey, TValue>(property, ref container, ref dictionary);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00006D2C File Offset: 0x00004F2C
		protected virtual bool IsExcluded<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
		{
			return false;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006D40 File Offset: 0x00004F40
		protected virtual void VisitProperty<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
		{
			PropertyContainer.TryAccept<TValue>(this, ref value, default(VisitParameters));
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006D5F File Offset: 0x00004F5F
		protected virtual void VisitCollection<TContainer, TCollection, TElement>(Property<TContainer, TCollection> property, ref TContainer container, ref TCollection value) where TCollection : ICollection<TElement>
		{
			this.VisitProperty<TContainer, TCollection>(property, ref container, ref value);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006CF5 File Offset: 0x00004EF5
		protected virtual void VisitList<TContainer, TList, TElement>(Property<TContainer, TList> property, ref TContainer container, ref TList value) where TList : IList<TElement>
		{
			this.VisitCollection<TContainer, TList, TElement>(property, ref container, ref value);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006CF5 File Offset: 0x00004EF5
		protected virtual void VisitSet<TContainer, TSet, TValue>(Property<TContainer, TSet> property, ref TContainer container, ref TSet value) where TSet : ISet<TValue>
		{
			this.VisitCollection<TContainer, TSet, TValue>(property, ref container, ref value);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006D6C File Offset: 0x00004F6C
		protected virtual void VisitDictionary<TContainer, TDictionary, TKey, TValue>(Property<TContainer, TDictionary> property, ref TContainer container, ref TDictionary value) where TDictionary : IDictionary<TKey, TValue>
		{
			this.VisitCollection<TContainer, TDictionary, KeyValuePair<TKey, TValue>>(property, ref container, ref value);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006D7C File Offset: 0x00004F7C
		private bool IsExcluded<TContainer, TValue>(Property<TContainer, TValue> property, ReadOnlyAdapterCollection.Enumerator enumerator, ref TContainer container, ref TValue value)
		{
			while (enumerator.MoveNext())
			{
				IPropertyVisitorAdapter propertyVisitorAdapter = enumerator.Current;
				IPropertyVisitorAdapter propertyVisitorAdapter2 = propertyVisitorAdapter;
				IPropertyVisitorAdapter propertyVisitorAdapter3 = propertyVisitorAdapter2;
				IExcludePropertyAdapter<TContainer, TValue> excludePropertyAdapter = propertyVisitorAdapter3 as IExcludePropertyAdapter<TContainer, TValue>;
				if (excludePropertyAdapter == null)
				{
					IExcludeContravariantPropertyAdapter<TContainer, TValue> excludeContravariantPropertyAdapter = propertyVisitorAdapter3 as IExcludeContravariantPropertyAdapter<TContainer, TValue>;
					if (excludeContravariantPropertyAdapter == null)
					{
						IExcludePropertyAdapter<TValue> excludePropertyAdapter2 = propertyVisitorAdapter3 as IExcludePropertyAdapter<TValue>;
						if (excludePropertyAdapter2 == null)
						{
							IExcludeContravariantPropertyAdapter<TValue> excludeContravariantPropertyAdapter2 = propertyVisitorAdapter3 as IExcludeContravariantPropertyAdapter<TValue>;
							if (excludeContravariantPropertyAdapter2 == null)
							{
								IExcludePropertyAdapter excludePropertyAdapter3 = propertyVisitorAdapter3 as IExcludePropertyAdapter;
								if (excludePropertyAdapter3 != null)
								{
									IExcludePropertyAdapter excludePropertyAdapter4 = excludePropertyAdapter3;
									ExcludeContext<TContainer, TValue> excludeContext = ExcludeContext<TContainer, TValue>.FromProperty(this, property);
									bool flag = excludePropertyAdapter4.IsExcluded<TContainer, TValue>(in excludeContext, ref container, ref value);
									if (flag)
									{
										return true;
									}
								}
							}
							else
							{
								IExcludeContravariantPropertyAdapter<TValue> excludeContravariantPropertyAdapter3 = excludeContravariantPropertyAdapter2;
								ExcludeContext<TContainer> excludeContext2 = ExcludeContext<TContainer>.FromProperty<TValue>(this, property);
								bool flag2 = excludeContravariantPropertyAdapter3.IsExcluded<TContainer>(in excludeContext2, ref container, value);
								value = property.GetValue(ref container);
								bool flag3 = flag2;
								if (flag3)
								{
									return true;
								}
							}
						}
						else
						{
							IExcludePropertyAdapter<TValue> excludePropertyAdapter5 = excludePropertyAdapter2;
							ExcludeContext<TContainer, TValue> excludeContext = ExcludeContext<TContainer, TValue>.FromProperty(this, property);
							bool flag4 = excludePropertyAdapter5.IsExcluded<TContainer>(in excludeContext, ref container, ref value);
							if (flag4)
							{
								return true;
							}
						}
					}
					else
					{
						IExcludeContravariantPropertyAdapter<TContainer, TValue> excludeContravariantPropertyAdapter4 = excludeContravariantPropertyAdapter;
						ExcludeContext<TContainer> excludeContext2 = ExcludeContext<TContainer>.FromProperty<TValue>(this, property);
						bool flag5 = excludeContravariantPropertyAdapter4.IsExcluded(in excludeContext2, ref container, value);
						value = property.GetValue(ref container);
						bool flag6 = flag5;
						if (flag6)
						{
							return true;
						}
					}
				}
				else
				{
					IExcludePropertyAdapter<TContainer, TValue> excludePropertyAdapter6 = excludePropertyAdapter;
					ExcludeContext<TContainer, TValue> excludeContext = ExcludeContext<TContainer, TValue>.FromProperty(this, property);
					bool flag7 = excludePropertyAdapter6.IsExcluded(in excludeContext, ref container, ref value);
					if (flag7)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006EF0 File Offset: 0x000050F0
		internal void ContinueVisitation<TContainer, TValue>(Property<TContainer, TValue> property, ReadOnlyAdapterCollection.Enumerator enumerator, ref TContainer container, ref TValue value)
		{
			while (enumerator.MoveNext())
			{
				IPropertyVisitorAdapter propertyVisitorAdapter = enumerator.Current;
				IPropertyVisitorAdapter propertyVisitorAdapter2 = propertyVisitorAdapter;
				IPropertyVisitorAdapter propertyVisitorAdapter3 = propertyVisitorAdapter2;
				IVisitPropertyAdapter<TContainer, TValue> visitPropertyAdapter = propertyVisitorAdapter3 as IVisitPropertyAdapter<TContainer, TValue>;
				if (visitPropertyAdapter == null)
				{
					IVisitContravariantPropertyAdapter<TContainer, TValue> visitContravariantPropertyAdapter = propertyVisitorAdapter3 as IVisitContravariantPropertyAdapter<TContainer, TValue>;
					if (visitContravariantPropertyAdapter == null)
					{
						IVisitPropertyAdapter<TValue> visitPropertyAdapter2 = propertyVisitorAdapter3 as IVisitPropertyAdapter<TValue>;
						if (visitPropertyAdapter2 == null)
						{
							IVisitContravariantPropertyAdapter<TValue> visitContravariantPropertyAdapter2 = propertyVisitorAdapter3 as IVisitContravariantPropertyAdapter<TValue>;
							if (visitContravariantPropertyAdapter2 == null)
							{
								IVisitPropertyAdapter visitPropertyAdapter3 = propertyVisitorAdapter3 as IVisitPropertyAdapter;
								if (visitPropertyAdapter3 == null)
								{
									continue;
								}
								IVisitPropertyAdapter visitPropertyAdapter4 = visitPropertyAdapter3;
								VisitContext<TContainer, TValue> visitContext = VisitContext<TContainer, TValue>.FromProperty(this, enumerator, property);
								visitPropertyAdapter4.Visit<TContainer, TValue>(in visitContext, ref container, ref value);
							}
							else
							{
								IVisitContravariantPropertyAdapter<TValue> visitContravariantPropertyAdapter3 = visitContravariantPropertyAdapter2;
								VisitContext<TContainer> visitContext2 = VisitContext<TContainer>.FromProperty<TValue>(this, enumerator, property);
								visitContravariantPropertyAdapter3.Visit<TContainer>(in visitContext2, ref container, value);
								value = property.GetValue(ref container);
							}
						}
						else
						{
							IVisitPropertyAdapter<TValue> visitPropertyAdapter5 = visitPropertyAdapter2;
							VisitContext<TContainer, TValue> visitContext = VisitContext<TContainer, TValue>.FromProperty(this, enumerator, property);
							visitPropertyAdapter5.Visit<TContainer>(in visitContext, ref container, ref value);
						}
					}
					else
					{
						IVisitContravariantPropertyAdapter<TContainer, TValue> visitContravariantPropertyAdapter4 = visitContravariantPropertyAdapter;
						VisitContext<TContainer> visitContext2 = VisitContext<TContainer>.FromProperty<TValue>(this, enumerator, property);
						visitContravariantPropertyAdapter4.Visit(in visitContext2, ref container, value);
						value = property.GetValue(ref container);
					}
				}
				else
				{
					IVisitPropertyAdapter<TContainer, TValue> visitPropertyAdapter6 = visitPropertyAdapter;
					VisitContext<TContainer, TValue> visitContext = VisitContext<TContainer, TValue>.FromProperty(this, enumerator, property);
					visitPropertyAdapter6.Visit(in visitContext, ref container, ref value);
				}
				return;
			}
			this.ContinueVisitationWithoutAdapters<TContainer, TValue>(property, enumerator, ref container, ref value);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00007023 File Offset: 0x00005223
		internal void ContinueVisitationWithoutAdapters<TContainer, TValue>(Property<TContainer, TValue> property, ReadOnlyAdapterCollection.Enumerator enumerator, ref TContainer container, ref TValue value)
		{
			this.ContinueVisitation<TContainer, TValue>(property, ref container, ref value);
		}

		// Token: 0x0400009A RID: 154
		private readonly List<IPropertyVisitorAdapter> m_Adapters = new List<IPropertyVisitorAdapter>();
	}
}
