using System;
using System.Collections.Generic;
using Unity.Properties;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x02000722 RID: 1826
	internal class AutoCompletePathVisitor : ITypeVisitor, IPropertyVisitor, IPropertyBagVisitor, IListPropertyVisitor
	{
		// Token: 0x17000CEF RID: 3311
		// (set) Token: 0x06003500 RID: 13568 RVA: 0x000CD569 File Offset: 0x000CB769
		public List<PropertyPathInfo> propertyPathList
		{
			set
			{
				this.m_VisitContext.propertyPathInfos = value;
			}
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06003501 RID: 13569 RVA: 0x000CD578 File Offset: 0x000CB778
		// (set) Token: 0x06003502 RID: 13570 RVA: 0x000CD580 File Offset: 0x000CB780
		public int maxDepth { get; set; }

		// Token: 0x06003503 RID: 13571 RVA: 0x000CD589 File Offset: 0x000CB789
		private bool HasReachedEnd(Type containerType)
		{
			return this.m_VisitContext.currentDepth >= this.maxDepth || this.m_VisitContext.types.Contains(containerType);
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x000CD5B4 File Offset: 0x000CB7B4
		public void Reset()
		{
			this.m_VisitContext.current = default(PropertyPath);
			this.m_VisitContext.propertyPathInfos = null;
			this.m_VisitContext.types.Clear();
			this.m_VisitContext.currentDepth = 0;
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x000CD604 File Offset: 0x000CB804
		void ITypeVisitor.Visit<TContainer>()
		{
			bool flag = this.HasReachedEnd(typeof(TContainer));
			if (!flag)
			{
				using (new AutoCompletePathVisitor.InspectedTypeScope<TContainer>(this.m_VisitContext))
				{
					IPropertyBag<TContainer> propertyBag = PropertyBag.GetPropertyBag<TContainer>();
					bool flag2 = propertyBag == null;
					if (!flag2)
					{
						foreach (IProperty<TContainer> property in propertyBag.GetProperties())
						{
							using (new AutoCompletePathVisitor.VisitedPropertyScope(this.m_VisitContext, property))
							{
								this.VisitPropertyType(property.DeclaredValueType());
							}
						}
					}
				}
			}
		}

		// Token: 0x06003506 RID: 13574 RVA: 0x000CD6E8 File Offset: 0x000CB8E8
		void IPropertyBagVisitor.Visit<TContainer>(IPropertyBag<TContainer> properties, ref TContainer container)
		{
			bool flag = this.HasReachedEnd(typeof(TContainer));
			if (!flag)
			{
				using (new AutoCompletePathVisitor.InspectedTypeScope<TContainer>(this.m_VisitContext))
				{
					IIndexedProperties<TContainer> indexedProperties = properties as IIndexedProperties<TContainer>;
					if (indexedProperties == null)
					{
						if (!(properties is IKeyedProperties<TContainer, object>))
						{
							foreach (IProperty<TContainer> property in properties.GetProperties(ref container))
							{
								using (new AutoCompletePathVisitor.VisitedPropertyScope(this.m_VisitContext, property))
								{
									property.Accept(this, ref container);
								}
							}
						}
					}
					else
					{
						IProperty<TContainer> property2;
						bool flag2 = indexedProperties.TryGetProperty(ref container, 0, out property2);
						if (flag2)
						{
							using (new AutoCompletePathVisitor.VisitedPropertyScope(this.m_VisitContext, 0, property2.DeclaredValueType()))
							{
								property2.Accept(this, ref container);
							}
						}
						else
						{
							this.VisitPropertyType(typeof(TContainer));
						}
					}
				}
			}
		}

		// Token: 0x06003507 RID: 13575 RVA: 0x000CD840 File Offset: 0x000CBA40
		void IPropertyVisitor.Visit<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container)
		{
			bool flag = !TypeTraits.IsContainer(typeof(TValue));
			if (!flag)
			{
				TValue value = property.GetValue(ref container);
				IPropertyBag propertyBag;
				bool flag2 = (!TypeTraits<TValue>.CanBeNull || !EqualityComparer<TValue>.Default.Equals(value, default(TValue))) && PropertyBag.TryGetPropertyBagForValue<TValue>(ref value, out propertyBag);
				if (flag2)
				{
					IPropertyBag propertyBag2 = propertyBag;
					IPropertyBag propertyBag3 = propertyBag2;
					IListPropertyAccept<TValue> listPropertyAccept = propertyBag3 as IListPropertyAccept<TValue>;
					if (listPropertyAccept == null)
					{
						PropertyContainer.TryAccept<TValue>(this, ref value, default(VisitParameters));
					}
					else
					{
						listPropertyAccept.Accept<TContainer>(this, property, ref container, ref value);
					}
				}
				else
				{
					this.VisitPropertyType(property.DeclaredValueType());
				}
			}
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x000CD8F8 File Offset: 0x000CBAF8
		void IListPropertyVisitor.Visit<TContainer, TList, TElement>(Property<TContainer, TList> property, ref TContainer container, ref TList list)
		{
			PropertyContainer.TryAccept<TList>(this, ref list, default(VisitParameters));
		}

		// Token: 0x06003509 RID: 13577 RVA: 0x000CD918 File Offset: 0x000CBB18
		private void VisitPropertyType(Type type)
		{
			bool flag = this.HasReachedEnd(type);
			if (!flag)
			{
				bool isArray = type.IsArray;
				if (isArray)
				{
					bool flag2 = type.GetArrayRank() != 1;
					if (!flag2)
					{
						Type elementType = type.GetElementType();
						IPropertyBag propertyBag = PropertyBag.GetPropertyBag(elementType);
						using (new AutoCompletePathVisitor.VisitedPropertyScope(this.m_VisitContext, 0, elementType))
						{
							if (propertyBag != null)
							{
								propertyBag.Accept(this);
							}
						}
					}
				}
				else
				{
					bool isGenericType = type.IsGenericType;
					if (isGenericType)
					{
						bool flag3 = type.GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>)) || type.GetGenericTypeDefinition().IsAssignableFrom(typeof(IList<>));
						if (flag3)
						{
							Type type2 = type.GenericTypeArguments[0];
							IPropertyBag propertyBag2 = PropertyBag.GetPropertyBag(type2);
							using (new AutoCompletePathVisitor.VisitedPropertyScope(this.m_VisitContext, 0, type2))
							{
								if (propertyBag2 != null)
								{
									propertyBag2.Accept(this);
								}
							}
						}
					}
					else
					{
						IPropertyBag propertyBag3 = PropertyBag.GetPropertyBag(type);
						if (propertyBag3 != null)
						{
							propertyBag3.Accept(this);
						}
					}
				}
			}
		}

		// Token: 0x04001AFC RID: 6908
		private AutoCompletePathVisitor.VisitContext m_VisitContext = new AutoCompletePathVisitor.VisitContext();

		// Token: 0x02000723 RID: 1827
		private class VisitContext
		{
			// Token: 0x17000CF1 RID: 3313
			// (get) Token: 0x0600350B RID: 13579 RVA: 0x000CDA6C File Offset: 0x000CBC6C
			// (set) Token: 0x0600350C RID: 13580 RVA: 0x000CDA74 File Offset: 0x000CBC74
			public List<PropertyPathInfo> propertyPathInfos { get; set; }

			// Token: 0x17000CF2 RID: 3314
			// (get) Token: 0x0600350D RID: 13581 RVA: 0x000CDA7D File Offset: 0x000CBC7D
			public HashSet<Type> types { get; } = new HashSet<Type>();

			// Token: 0x17000CF3 RID: 3315
			// (get) Token: 0x0600350E RID: 13582 RVA: 0x000CDA85 File Offset: 0x000CBC85
			// (set) Token: 0x0600350F RID: 13583 RVA: 0x000CDA8D File Offset: 0x000CBC8D
			public PropertyPath current { get; set; }

			// Token: 0x17000CF4 RID: 3316
			// (get) Token: 0x06003510 RID: 13584 RVA: 0x000CDA96 File Offset: 0x000CBC96
			// (set) Token: 0x06003511 RID: 13585 RVA: 0x000CDA9E File Offset: 0x000CBC9E
			public int currentDepth { get; set; }
		}

		// Token: 0x02000724 RID: 1828
		private struct InspectedTypeScope<TContainer> : IDisposable
		{
			// Token: 0x06003513 RID: 13587 RVA: 0x000CDABB File Offset: 0x000CBCBB
			public InspectedTypeScope(AutoCompletePathVisitor.VisitContext context)
			{
				this.m_VisitContext = context;
				this.m_VisitContext.types.Add(typeof(TContainer));
			}

			// Token: 0x06003514 RID: 13588 RVA: 0x000CDAE0 File Offset: 0x000CBCE0
			public void Dispose()
			{
				this.m_VisitContext.types.Remove(typeof(TContainer));
			}

			// Token: 0x04001B02 RID: 6914
			private AutoCompletePathVisitor.VisitContext m_VisitContext;
		}

		// Token: 0x02000725 RID: 1829
		private struct VisitedPropertyScope : IDisposable
		{
			// Token: 0x06003515 RID: 13589 RVA: 0x000CDB00 File Offset: 0x000CBD00
			public VisitedPropertyScope(AutoCompletePathVisitor.VisitContext context, IProperty property)
			{
				this.m_VisitContext = context;
				AutoCompletePathVisitor.VisitContext visitContext = this.m_VisitContext;
				PropertyPath propertyPath = this.m_VisitContext.current;
				visitContext.current = PropertyPath.AppendProperty(in propertyPath, property);
				propertyPath = this.m_VisitContext.current;
				PropertyPathInfo propertyPathInfo = new PropertyPathInfo(in propertyPath, property.DeclaredValueType());
				List<PropertyPathInfo> propertyPathInfos = this.m_VisitContext.propertyPathInfos;
				if (propertyPathInfos != null)
				{
					propertyPathInfos.Add(propertyPathInfo);
				}
				AutoCompletePathVisitor.VisitContext visitContext2 = this.m_VisitContext;
				int currentDepth = visitContext2.currentDepth;
				visitContext2.currentDepth = currentDepth + 1;
			}

			// Token: 0x06003516 RID: 13590 RVA: 0x000CDB80 File Offset: 0x000CBD80
			public VisitedPropertyScope(AutoCompletePathVisitor.VisitContext context, int index, Type type)
			{
				this.m_VisitContext = context;
				AutoCompletePathVisitor.VisitContext visitContext = this.m_VisitContext;
				PropertyPath propertyPath = this.m_VisitContext.current;
				visitContext.current = PropertyPath.AppendIndex(in propertyPath, index);
				propertyPath = this.m_VisitContext.current;
				PropertyPathInfo propertyPathInfo = new PropertyPathInfo(in propertyPath, type);
				List<PropertyPathInfo> propertyPathInfos = this.m_VisitContext.propertyPathInfos;
				if (propertyPathInfos != null)
				{
					propertyPathInfos.Add(propertyPathInfo);
				}
				AutoCompletePathVisitor.VisitContext visitContext2 = this.m_VisitContext;
				int currentDepth = visitContext2.currentDepth;
				visitContext2.currentDepth = currentDepth + 1;
			}

			// Token: 0x06003517 RID: 13591 RVA: 0x000CDBF8 File Offset: 0x000CBDF8
			public void Dispose()
			{
				AutoCompletePathVisitor.VisitContext visitContext = this.m_VisitContext;
				PropertyPath current = this.m_VisitContext.current;
				visitContext.current = PropertyPath.Pop(in current);
				AutoCompletePathVisitor.VisitContext visitContext2 = this.m_VisitContext;
				int currentDepth = visitContext2.currentDepth;
				visitContext2.currentDepth = currentDepth - 1;
			}

			// Token: 0x04001B03 RID: 6915
			private AutoCompletePathVisitor.VisitContext m_VisitContext;
		}
	}
}
