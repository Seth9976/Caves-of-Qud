using System;
using System.Collections.Generic;
using Unity.Properties.Internal;

namespace Unity.Properties
{
	// Token: 0x0200007A RID: 122
	public abstract class PathVisitor : IPropertyBagVisitor, IPropertyVisitor
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00006642 File Offset: 0x00004842
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000664A File Offset: 0x0000484A
		public PropertyPath Path { get; set; }

		// Token: 0x060001DF RID: 479 RVA: 0x00006654 File Offset: 0x00004854
		public virtual void Reset()
		{
			this.m_PathIndex = 0;
			this.Path = default(PropertyPath);
			this.ReturnCode = VisitReturnCode.Ok;
			this.ReadonlyVisit = false;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00006689 File Offset: 0x00004889
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00006691 File Offset: 0x00004891
		private IProperty Property { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x0000669A File Offset: 0x0000489A
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x000066A2 File Offset: 0x000048A2
		public bool ReadonlyVisit { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000066AB File Offset: 0x000048AB
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x000066B3 File Offset: 0x000048B3
		public VisitReturnCode ReturnCode { get; protected set; }

		// Token: 0x060001E6 RID: 486 RVA: 0x000066BC File Offset: 0x000048BC
		void IPropertyBagVisitor.Visit<TContainer>(IPropertyBag<TContainer> properties, ref TContainer container)
		{
			PropertyPath path = this.Path;
			int pathIndex = this.m_PathIndex;
			this.m_PathIndex = pathIndex + 1;
			PropertyPathPart propertyPathPart = path[pathIndex];
			switch (propertyPathPart.Kind)
			{
			case PropertyPathPartKind.Name:
			{
				INamedProperties<TContainer> namedProperties = properties as INamedProperties<TContainer>;
				IProperty<TContainer> property;
				bool flag = namedProperties != null && namedProperties.TryGetProperty(ref container, propertyPathPart.Name, out property);
				if (flag)
				{
					property.Accept(this, ref container);
				}
				else
				{
					this.ReturnCode = VisitReturnCode.InvalidPath;
				}
				break;
			}
			case PropertyPathPartKind.Index:
			{
				IIndexedProperties<TContainer> indexedProperties = properties as IIndexedProperties<TContainer>;
				bool flag2 = indexedProperties != null;
				if (flag2)
				{
					IIndexedCollectionPropertyBagEnumerator<TContainer> indexedCollectionPropertyBagEnumerator = properties as IIndexedCollectionPropertyBagEnumerator<TContainer>;
					bool flag3 = indexedCollectionPropertyBagEnumerator != null && propertyPathPart.Index < indexedCollectionPropertyBagEnumerator.GetCount(ref container);
					if (flag3)
					{
						IndexedCollectionSharedPropertyState sharedPropertyState = indexedCollectionPropertyBagEnumerator.GetSharedPropertyState();
						indexedCollectionPropertyBagEnumerator.SetSharedPropertyState(new IndexedCollectionSharedPropertyState
						{
							Index = propertyPathPart.Index,
							IsReadOnly = false
						});
						IProperty<TContainer> sharedProperty = indexedCollectionPropertyBagEnumerator.GetSharedProperty();
						IAttributes attributes = sharedProperty as IAttributes;
						AttributesScope? attributesScope = ((attributes != null) ? new AttributesScope?(attributes.CreateAttributesScope(this.Property as IAttributes)) : null);
						try
						{
							sharedProperty.Accept(this, ref container);
						}
						finally
						{
							if (attributesScope != null)
							{
								((IDisposable)attributesScope.GetValueOrDefault()).Dispose();
							}
						}
						indexedCollectionPropertyBagEnumerator.SetSharedPropertyState(sharedPropertyState);
					}
					else
					{
						IProperty<TContainer> property;
						bool flag4 = indexedProperties.TryGetProperty(ref container, propertyPathPart.Index, out property);
						if (flag4)
						{
							IAttributes attributes2 = property as IAttributes;
							AttributesScope? attributesScope2 = ((attributes2 != null) ? new AttributesScope?(attributes2.CreateAttributesScope(this.Property as IAttributes)) : null);
							try
							{
								property.Accept(this, ref container);
							}
							finally
							{
								if (attributesScope2 != null)
								{
									((IDisposable)attributesScope2.GetValueOrDefault()).Dispose();
								}
							}
						}
						else
						{
							this.ReturnCode = VisitReturnCode.InvalidPath;
						}
					}
				}
				else
				{
					this.ReturnCode = VisitReturnCode.InvalidPath;
				}
				break;
			}
			case PropertyPathPartKind.Key:
			{
				IKeyedProperties<TContainer, object> keyedProperties = properties as IKeyedProperties<TContainer, object>;
				IProperty<TContainer> property;
				bool flag5 = keyedProperties != null && keyedProperties.TryGetProperty(ref container, propertyPathPart.Key, out property);
				if (flag5)
				{
					using ((property as IAttributes).CreateAttributesScope(this.Property as IAttributes))
					{
						property.Accept(this, ref container);
					}
				}
				else
				{
					this.ReturnCode = VisitReturnCode.InvalidPath;
				}
				break;
			}
			default:
				this.ReturnCode = VisitReturnCode.InvalidPath;
				break;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006964 File Offset: 0x00004B64
		void IPropertyVisitor.Visit<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container)
		{
			TValue value = property.GetValue(ref container);
			bool flag = this.m_PathIndex >= this.Path.Length;
			if (flag)
			{
				this.VisitPath<TContainer, TValue>(property, ref container, ref value);
			}
			else
			{
				IPropertyBag propertyBag;
				bool flag2 = PropertyBag.TryGetPropertyBagForValue<TValue>(ref value, out propertyBag);
				if (flag2)
				{
					bool flag3 = TypeTraits<TValue>.CanBeNull && EqualityComparer<TValue>.Default.Equals(value, default(TValue));
					if (flag3)
					{
						this.ReturnCode = VisitReturnCode.InvalidPath;
					}
					else
					{
						using (new PathVisitor.PropertyScope(this, property))
						{
							PropertyContainer.Accept<TValue>(this, ref value, default(VisitParameters));
						}
						bool flag4 = !property.IsReadOnly && !this.ReadonlyVisit;
						if (flag4)
						{
							property.SetValue(ref container, value);
						}
					}
				}
				else
				{
					this.ReturnCode = VisitReturnCode.InvalidPath;
				}
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00005447 File Offset: 0x00003647
		protected virtual void VisitPath<TContainer, TValue>(Property<TContainer, TValue> property, ref TContainer container, ref TValue value)
		{
		}

		// Token: 0x04000093 RID: 147
		private int m_PathIndex;

		// Token: 0x0200007B RID: 123
		private readonly struct PropertyScope : IDisposable
		{
			// Token: 0x060001EA RID: 490 RVA: 0x00006A5C File Offset: 0x00004C5C
			public PropertyScope(PathVisitor visitor, IProperty property)
			{
				this.m_Visitor = visitor;
				this.m_Property = this.m_Visitor.Property;
				this.m_Visitor.Property = property;
			}

			// Token: 0x060001EB RID: 491 RVA: 0x00006A84 File Offset: 0x00004C84
			public void Dispose()
			{
				this.m_Visitor.Property = this.m_Property;
			}

			// Token: 0x04000098 RID: 152
			private readonly PathVisitor m_Visitor;

			// Token: 0x04000099 RID: 153
			private readonly IProperty m_Property;
		}
	}
}
