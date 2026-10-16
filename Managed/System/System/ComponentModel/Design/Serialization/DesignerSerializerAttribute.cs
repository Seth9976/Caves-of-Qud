using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Indicates a serializer for the serialization manager to use to serialize the values of the type this attribute is applied to. This class cannot be inherited.</summary>
	// Token: 0x02000487 RID: 1159
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public sealed class DesignerSerializerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializerAttribute" /> class.</summary>
		/// <param name="serializerType">The data type of the serializer. </param>
		/// <param name="baseSerializerType">The base data type of the serializer. Multiple serializers can be supplied for a class as long as the serializers have different base types. </param>
		// Token: 0x06002517 RID: 9495 RVA: 0x00082BB6 File Offset: 0x00080DB6
		public DesignerSerializerAttribute(Type serializerType, Type baseSerializerType)
		{
			this.SerializerTypeName = serializerType.AssemblyQualifiedName;
			this.SerializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializerAttribute" /> class.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer. </param>
		/// <param name="baseSerializerType">The base data type of the serializer. Multiple serializers can be supplied for a class as long as the serializers have different base types. </param>
		// Token: 0x06002518 RID: 9496 RVA: 0x00082BD6 File Offset: 0x00080DD6
		public DesignerSerializerAttribute(string serializerTypeName, Type baseSerializerType)
		{
			this.SerializerTypeName = serializerTypeName;
			this.SerializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializerAttribute" /> class.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer. </param>
		/// <param name="baseSerializerTypeName">The fully qualified name of the base data type of the serializer. Multiple serializers can be supplied for a class as long as the serializers have different base types. </param>
		// Token: 0x06002519 RID: 9497 RVA: 0x00082BF1 File Offset: 0x00080DF1
		public DesignerSerializerAttribute(string serializerTypeName, string baseSerializerTypeName)
		{
			this.SerializerTypeName = serializerTypeName;
			this.SerializerBaseTypeName = baseSerializerTypeName;
		}

		/// <summary>Gets the fully qualified type name of the serializer.</summary>
		/// <returns>The fully qualified type name of the serializer.</returns>
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x0600251A RID: 9498 RVA: 0x00082C07 File Offset: 0x00080E07
		public string SerializerTypeName { get; }

		/// <summary>Gets the fully qualified type name of the serializer base type.</summary>
		/// <returns>The fully qualified type name of the serializer base type.</returns>
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x0600251B RID: 9499 RVA: 0x00082C0F File Offset: 0x00080E0F
		public string SerializerBaseTypeName { get; }

		/// <summary>Indicates a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x0600251C RID: 9500 RVA: 0x00082C18 File Offset: 0x00080E18
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

		// Token: 0x040014B6 RID: 5302
		private string _typeId;
	}
}
