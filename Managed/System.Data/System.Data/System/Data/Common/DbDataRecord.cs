using System;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>Implements <see cref="T:System.Data.IDataRecord" /> and <see cref="T:System.ComponentModel.ICustomTypeDescriptor" />, and provides data binding support for <see cref="T:System.Data.Common.DbEnumerator" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000394 RID: 916
	public abstract class DbDataRecord : ICustomTypeDescriptor, IDataRecord
	{
		/// <summary>Indicates the number of fields within the current record. This property is read-only.</summary>
		/// <returns>The number of fields within the current record.</returns>
		/// <exception cref="T:System.NotSupportedException">Not connected to a data source to read from. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06002C7A RID: 11386
		public abstract int FieldCount { get; }

		/// <summary>Indicates the value at the specified column in its native format given the column ordinal. This property is read-only.</summary>
		/// <returns>The value at the specified column in its native format.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000790 RID: 1936
		public abstract object this[int i] { get; }

		/// <summary>Indicates the value at the specified column in its native format given the column name. This property is read-only.</summary>
		/// <returns>The value at the specified column in its native format.</returns>
		/// <param name="name">The column name. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000791 RID: 1937
		public abstract object this[string name] { get; }

		/// <summary>Returns the value of the specified column as a Boolean.</summary>
		/// <returns>true if the Boolean is true; otherwise false.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C7D RID: 11389
		public abstract bool GetBoolean(int i);

		/// <summary>Returns the value of the specified column as a byte.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C7E RID: 11390
		public abstract byte GetByte(int i);

		/// <summary>Returns the value of the specified column as a byte array.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <param name="dataIndex">The index within the field from which to start the read operation.</param>
		/// <param name="buffer">The buffer into which to read the stream of bytes.</param>
		/// <param name="bufferIndex">The index for <paramref name="buffer" /> to start the read operation.</param>
		/// <param name="length">The number of bytes to read.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002C7F RID: 11391
		public abstract long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length);

		/// <summary>Returns the value of the specified column as a character.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C80 RID: 11392
		public abstract char GetChar(int i);

		/// <summary>Returns the value of the specified column as a character array.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">Column ordinal. </param>
		/// <param name="dataIndex">Buffer to copy data into. </param>
		/// <param name="buffer">Maximum length to copy into the buffer. </param>
		/// <param name="bufferIndex">Point to start from within the buffer. </param>
		/// <param name="length">Point to start from within the source data. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002C81 RID: 11393
		public abstract long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length);

		/// <summary>Not currently supported.</summary>
		/// <returns>Not currently supported.</returns>
		/// <param name="i">Not currently supported.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002C82 RID: 11394 RVA: 0x000BEA7A File Offset: 0x000BCC7A
		public IDataReader GetData(int i)
		{
			return this.GetDbDataReader(i);
		}

		/// <summary>Returns a <see cref="T:System.Data.Common.DbDataReader" /> object for the requested column ordinal that can be overridden with a provider-specific implementation.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbDataReader" /> object.</returns>
		/// <param name="i">The zero-based column ordinal.</param>
		// Token: 0x06002C83 RID: 11395 RVA: 0x00008E4B File Offset: 0x0000704B
		protected virtual DbDataReader GetDbDataReader(int i)
		{
			throw ADP.NotSupported();
		}

		/// <summary>Returns the name of the back-end data type.</summary>
		/// <returns>The name of the back-end data type.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C84 RID: 11396
		public abstract string GetDataTypeName(int i);

		/// <summary>Returns the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C85 RID: 11397
		public abstract DateTime GetDateTime(int i);

		/// <summary>Returns the value of the specified column as a <see cref="T:System.Decimal" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C86 RID: 11398
		public abstract decimal GetDecimal(int i);

		/// <summary>Returns the value of the specified column as a double-precision floating-point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C87 RID: 11399
		public abstract double GetDouble(int i);

		/// <summary>Returns the <see cref="T:System.Type" /> that is the data type of the object.</summary>
		/// <returns>The <see cref="T:System.Type" /> that is the data type of the object.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002C88 RID: 11400
		public abstract Type GetFieldType(int i);

		/// <summary>Returns the value of the specified column as a single-precision floating-point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C89 RID: 11401
		public abstract float GetFloat(int i);

		/// <summary>Returns the GUID value of the specified field.</summary>
		/// <returns>The GUID value of the specified field.</returns>
		/// <param name="i">The index of the field to return. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C8A RID: 11402
		public abstract Guid GetGuid(int i);

		/// <summary>Returns the value of the specified column as a 16-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002C8B RID: 11403
		public abstract short GetInt16(int i);

		/// <summary>Returns the value of the specified column as a 32-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C8C RID: 11404
		public abstract int GetInt32(int i);

		/// <summary>Returns the value of the specified column as a 64-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002C8D RID: 11405
		public abstract long GetInt64(int i);

		/// <summary>Returns the name of the specified column.</summary>
		/// <returns>The name of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C8E RID: 11406
		public abstract string GetName(int i);

		/// <summary>Returns the column ordinal, given the name of the column.</summary>
		/// <returns>The column ordinal.</returns>
		/// <param name="name">The name of the column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002C8F RID: 11407
		public abstract int GetOrdinal(string name);

		/// <summary>Returns the value of the specified column as a string.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C90 RID: 11408
		public abstract string GetString(int i);

		/// <summary>Returns the value at the specified column in its native format.</summary>
		/// <returns>The value to return.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C91 RID: 11409
		public abstract object GetValue(int i);

		/// <summary>Populates an array of objects with the column values of the current record.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> to copy the attribute fields into. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002C92 RID: 11410
		public abstract int GetValues(object[] values);

		/// <summary>Used to indicate nonexistent values.</summary>
		/// <returns>true if the specified column is equivalent to <see cref="T:System.DBNull" />; otherwise false.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002C93 RID: 11411
		public abstract bool IsDBNull(int i);

		/// <summary>Returns a collection of custom attributes for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> that contains the attributes for this object. </returns>
		// Token: 0x06002C94 RID: 11412 RVA: 0x0003279E File Offset: 0x0003099E
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		/// <summary>Returns the class name of this instance of a component.</summary>
		/// <returns>The class name of the object, or null if the class does not have a name.</returns>
		// Token: 0x06002C95 RID: 11413 RVA: 0x00003E32 File Offset: 0x00002032
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		/// <summary>Returns the name of this instance of a component.</summary>
		/// <returns>The name of the object, or null if the object does not have a name.</returns>
		// Token: 0x06002C96 RID: 11414 RVA: 0x00003E32 File Offset: 0x00002032
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		/// <summary>Returns a type converter for this instance of a component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that is the converter for this object, or null if there is no <see cref="T:System.ComponentModel.TypeConverter" /> for this object.</returns>
		// Token: 0x06002C97 RID: 11415 RVA: 0x00003E32 File Offset: 0x00002032
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		/// <summary>Returns the default event for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> that represents the default event for this object, or null if this object does not have events.</returns>
		// Token: 0x06002C98 RID: 11416 RVA: 0x00003E32 File Offset: 0x00002032
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		/// <summary>Returns the default property for this instance of a component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property for this object, or null if this object does not have properties.</returns>
		// Token: 0x06002C99 RID: 11417 RVA: 0x00003E32 File Offset: 0x00002032
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		/// <summary>Returns an editor of the specified type for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.Object" /> of the specified type that is the editor for this object, or null if the editor cannot be found.</returns>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for this object.</param>
		// Token: 0x06002C9A RID: 11418 RVA: 0x00003E32 File Offset: 0x00002032
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		/// <summary>Returns the events for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the events for this component instance.</returns>
		// Token: 0x06002C9B RID: 11419 RVA: 0x000327A6 File Offset: 0x000309A6
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		/// <summary>Returns the events for this instance of a component using the specified attribute array as a filter.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the filtered events for this component instance.</returns>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		// Token: 0x06002C9C RID: 11420 RVA: 0x000327A6 File Offset: 0x000309A6
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		/// <summary>Returns the properties for this instance of a component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties for this component instance.</returns>
		// Token: 0x06002C9D RID: 11421 RVA: 0x000327AE File Offset: 0x000309AE
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		/// <summary>Returns the properties for this instance of a component using the attribute array as a filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the filtered properties for this component instance.</returns>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		// Token: 0x06002C9E RID: 11422 RVA: 0x000BEA83 File Offset: 0x000BCC83
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			return new PropertyDescriptorCollection(null);
		}

		/// <summary>Returns an object that contains the property described by the specified property descriptor.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the owner of the specified property.</returns>
		/// <param name="pd">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property whose owner is to be found.</param>
		// Token: 0x06002C9F RID: 11423 RVA: 0x00005696 File Offset: 0x00003896
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
	}
}
