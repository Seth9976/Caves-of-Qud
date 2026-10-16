using System;
using Unity.Properties.Internal;

namespace Unity.Properties
{
	// Token: 0x02000051 RID: 81
	public abstract class PropertyBag<TContainer> : IPropertyBag<TContainer>, IPropertyBag, IPropertyBagRegister, IConstructor<TContainer>, IConstructor
	{
		// Token: 0x0600017D RID: 381 RVA: 0x00005E40 File Offset: 0x00004040
		static PropertyBag()
		{
			bool flag = !TypeTraits.IsContainer(typeof(TContainer));
			if (flag)
			{
				throw new InvalidOperationException(string.Format("Failed to create a property bag for Type=[{0}]. The type is not a valid container type.", typeof(TContainer)));
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005E7F File Offset: 0x0000407F
		void IPropertyBagRegister.Register()
		{
			PropertyBagStore.AddPropertyBag<TContainer>(this);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005E8C File Offset: 0x0000408C
		public void Accept(ITypeVisitor visitor)
		{
			bool flag = visitor == null;
			if (flag)
			{
				throw new ArgumentNullException("visitor");
			}
			visitor.Visit<TContainer>();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005EB8 File Offset: 0x000040B8
		void IPropertyBag.Accept(IPropertyBagVisitor visitor, ref object container)
		{
			bool flag = container == null;
			if (flag)
			{
				throw new ArgumentNullException("container");
			}
			object obj = container;
			TContainer tcontainer;
			int num;
			if (obj is TContainer)
			{
				tcontainer = (TContainer)((object)obj);
				num = 1;
			}
			else
			{
				num = 0;
			}
			bool flag2 = num == 0;
			if (flag2)
			{
				throw new ArgumentException(string.Format("The given ContainerType=[{0}] does not match the PropertyBagType=[{1}]", container.GetType(), typeof(TContainer)));
			}
			PropertyBag.AcceptWithSpecializedVisitor<TContainer>(this, visitor, ref tcontainer);
			container = tcontainer;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005F2C File Offset: 0x0000412C
		void IPropertyBag<TContainer>.Accept(IPropertyBagVisitor visitor, ref TContainer container)
		{
			visitor.Visit<TContainer>(this, ref container);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005F38 File Offset: 0x00004138
		PropertyCollection<TContainer> IPropertyBag<TContainer>.GetProperties()
		{
			return this.GetProperties();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005F50 File Offset: 0x00004150
		PropertyCollection<TContainer> IPropertyBag<TContainer>.GetProperties(ref TContainer container)
		{
			return this.GetProperties(ref container);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00005F69 File Offset: 0x00004169
		InstantiationKind IConstructor.InstantiationKind
		{
			get
			{
				return this.InstantiationKind;
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005F74 File Offset: 0x00004174
		TContainer IConstructor<TContainer>.Instantiate()
		{
			return this.Instantiate();
		}

		// Token: 0x06000186 RID: 390
		public abstract PropertyCollection<TContainer> GetProperties();

		// Token: 0x06000187 RID: 391
		public abstract PropertyCollection<TContainer> GetProperties(ref TContainer container);

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00005F8C File Offset: 0x0000418C
		protected virtual InstantiationKind InstantiationKind { get; } = InstantiationKind.Activator;

		// Token: 0x06000189 RID: 393 RVA: 0x00005F94 File Offset: 0x00004194
		protected virtual TContainer Instantiate()
		{
			return default(TContainer);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005FAF File Offset: 0x000041AF
		public TContainer CreateInstance()
		{
			return TypeUtility.Instantiate<TContainer>();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005FB6 File Offset: 0x000041B6
		public bool TryCreateInstance(out TContainer instance)
		{
			return TypeUtility.TryInstantiate<TContainer>(out instance);
		}
	}
}
