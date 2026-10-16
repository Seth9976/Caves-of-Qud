using System;
using System.Reflection;

namespace XRL.World.Effects
{
	// Token: 0x02000CE1 RID: 3297
	[Serializable]
	public class ProceduralCookingEffectUnit : IComposite
	{
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060082EE RID: 33518 RVA: 0x002F92A1 File Offset: 0x002F74A1
		public virtual bool WantFieldReflection
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060082EF RID: 33519 RVA: 0x002F92A4 File Offset: 0x002F74A4
		public virtual string GetDescription()
		{
			return "[effect]";
		}

		// Token: 0x060082F0 RID: 33520 RVA: 0x002F92AB File Offset: 0x002F74AB
		public virtual string GetTemplatedDescription()
		{
			return this.GetDescription();
		}

		// Token: 0x060082F1 RID: 33521 RVA: 0x002F92B3 File Offset: 0x002F74B3
		public virtual void Apply(GameObject go, Effect parent)
		{
		}

		// Token: 0x060082F2 RID: 33522 RVA: 0x002F92B5 File Offset: 0x002F74B5
		public virtual void Remove(GameObject go, Effect parent)
		{
		}

		// Token: 0x060082F3 RID: 33523 RVA: 0x002F92B7 File Offset: 0x002F74B7
		public virtual void FireEvent(Event E)
		{
		}

		// Token: 0x060082F4 RID: 33524 RVA: 0x002F92B9 File Offset: 0x002F74B9
		public virtual void Init(GameObject target)
		{
		}

		// Token: 0x060082F5 RID: 33525 RVA: 0x002F92BB File Offset: 0x002F74BB
		public virtual void Write(SerializationWriter Writer)
		{
		}

		// Token: 0x060082F6 RID: 33526 RVA: 0x002F92BD File Offset: 0x002F74BD
		public virtual void Read(SerializationReader Reader)
		{
		}

		// Token: 0x060082F7 RID: 33527 RVA: 0x002F92C0 File Offset: 0x002F74C0
		public virtual ProceduralCookingEffectUnit DeepCopy(ProceduralCookingEffect Parent)
		{
			Type type = base.GetType();
			ProceduralCookingEffectUnit proceduralCookingEffectUnit = (ProceduralCookingEffectUnit)Activator.CreateInstance(type);
			proceduralCookingEffectUnit.parent = Parent;
			foreach (FieldInfo fieldInfo in type.GetCachedFields())
			{
				if ((fieldInfo.Attributes & (FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized)) == FieldAttributes.PrivateScope)
				{
					fieldInfo.SetValue(proceduralCookingEffectUnit, fieldInfo.GetValue(this));
				}
			}
			return proceduralCookingEffectUnit;
		}

		// Token: 0x04003949 RID: 14665
		[NonSerialized]
		public ProceduralCookingEffect parent;
	}
}
