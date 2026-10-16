using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Indicates the base serializer to use for a root designer object. This class cannot be inherited.</summary>
	// Token: 0x02000495 RID: 1173
	[Obsolete("This attribute has been deprecated. Use DesignerSerializerAttribute instead.  For example, to specify a root designer for CodeDom, use DesignerSerializerAttribute(...,typeof(TypeCodeDomSerializer)).  https://go.microsoft.com/fwlink/?linkid=14202")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public sealed class RootDesignerSerializerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.RootDesignerSerializerAttribute" /> class using the specified attributes.</summary>
		/// <param name="serializerType">The data type of the serializer. </param>
		/// <param name="baseSerializerType">The base type of the serializer. A class can include multiple serializers as they all have different base types. </param>
		/// <param name="reloadable">true if this serializer supports dynamic reloading of the document; otherwise, false. </param>
		// Token: 0x0600255F RID: 9567 RVA: 0x0008328B File Offset: 0x0008148B
		public RootDesignerSerializerAttribute(Type serializerType, Type baseSerializerType, bool reloadable)
		{
			this.SerializerTypeName = serializerType.AssemblyQualifiedName;
			this.SerializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
			this.Reloadable = reloadable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.RootDesignerSerializerAttribute" /> class using the specified attributes.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer. </param>
		/// <param name="baseSerializerType">The name of the base type of the serializer. A class can include multiple serializers, as they all have different base types. </param>
		/// <param name="reloadable">true if this serializer supports dynamic reloading of the document; otherwise, false. </param>
		// Token: 0x06002560 RID: 9568 RVA: 0x000832B2 File Offset: 0x000814B2
		public RootDesignerSerializerAttribute(string serializerTypeName, Type baseSerializerType, bool reloadable)
		{
			this.SerializerTypeName = serializerTypeName;
			this.SerializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
			this.Reloadable = reloadable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.RootDesignerSerializerAttribute" /> class using the specified attributes.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer. </param>
		/// <param name="baseSerializerTypeName">The name of the base type of the serializer. A class can include multiple serializers as they all have different base types. </param>
		/// <param name="reloadable">true if this serializer supports dynamic reloading of the document; otherwise, false. </param>
		// Token: 0x06002561 RID: 9569 RVA: 0x000832D4 File Offset: 0x000814D4
		public RootDesignerSerializerAttribute(string serializerTypeName, string baseSerializerTypeName, bool reloadable)
		{
			this.SerializerTypeName = serializerTypeName;
			this.SerializerBaseTypeName = baseSerializerTypeName;
			this.Reloadable = reloadable;
		}

		/// <summary>Gets a value indicating whether the root serializer supports reloading of the design document without first disposing the designer host.</summary>
		/// <returns>true if the root serializer supports reloading; otherwise, false.</returns>
		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002562 RID: 9570 RVA: 0x000832F1 File Offset: 0x000814F1
		public bool Reloadable { get; }

		/// <summary>Gets the fully qualified type name of the serializer.</summary>
		/// <returns>The name of the type of the serializer.</returns>
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002563 RID: 9571 RVA: 0x000832F9 File Offset: 0x000814F9
		public string SerializerTypeName { get; }

		/// <summary>Gets the fully qualified type name of the base type of the serializer.</summary>
		/// <returns>The name of the base type of the serializer.</returns>
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06002564 RID: 9572 RVA: 0x00083301 File Offset: 0x00081501
		public string SerializerBaseTypeName { get; }

		/// <summary>Gets a unique ID for this attribute type.</summary>
		/// <returns>An object containing a unique ID for this attribute type.</returns>
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06002565 RID: 9573 RVA: 0x0008330C File Offset: 0x0008150C
		public override object TypeId
		{
			get
			{
				if (this._typeId == null)
				{
					string text = this.SerializerBaseTypeName;
					int num = text.IndexOf(',');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					this._typeId = base.GetType().FullName + text;
				}
				return this._typeId;
			}
		}

		// Token: 0x040014C5 RID: 5317
		private string _typeId;
	}
}
