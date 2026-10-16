using System;
using System.ComponentModel.Design;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

namespace System.Resources
{
	// Token: 0x0200002B RID: 43
	internal class SerializedFromResXHandler : ResXDataNodeHandler, IWritableHandler
	{
		// Token: 0x06000106 RID: 262 RVA: 0x00005C68 File Offset: 0x00003E68
		public SerializedFromResXHandler(string data, string _mime_type)
		{
			this.dataString = data;
			this.mime_type = _mime_type;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005C7E File Offset: 0x00003E7E
		public override object GetValue(ITypeResolutionService typeResolver)
		{
			return this.DeserializeObject(typeResolver);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005C87 File Offset: 0x00003E87
		public override object GetValue(AssemblyName[] assemblyNames)
		{
			return this.DeserializeObject(new AssemblyNamesTypeResolutionService(assemblyNames));
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005C95 File Offset: 0x00003E95
		public override string GetValueTypeName(ITypeResolutionService typeResolver)
		{
			return this.InternalGetValueType(typeResolver);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005C9E File Offset: 0x00003E9E
		public override string GetValueTypeName(AssemblyName[] assemblyNames)
		{
			return this.InternalGetValueType(null);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00005CA7 File Offset: 0x00003EA7
		public string DataString
		{
			get
			{
				return this.dataString;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005CB0 File Offset: 0x00003EB0
		private string InternalGetValueType(ITypeResolutionService typeResolver)
		{
			object obj;
			try
			{
				obj = this.DeserializeObject(typeResolver);
			}
			catch
			{
				return typeof(object).AssemblyQualifiedName;
			}
			if (obj == null)
			{
				return null;
			}
			return obj.GetType().AssemblyQualifiedName;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005CFC File Offset: 0x00003EFC
		private object DeserializeObject(ITypeResolutionService typeResolver)
		{
			object obj;
			try
			{
				if (this.mime_type == ResXResourceWriter.SoapSerializedObjectMimeType)
				{
					SoapFormatter soapFormatter = new SoapFormatter();
					if (this.binder == null)
					{
						this.binder = new SerializedFromResXHandler.CustomBinder(typeResolver);
					}
					soapFormatter.Binder = this.binder;
					using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(this.dataString)))
					{
						return soapFormatter.Deserialize(memoryStream);
					}
				}
				if (this.mime_type == ResXResourceWriter.BinSerializedObjectMimeType)
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					if (this.binder == null)
					{
						this.binder = new SerializedFromResXHandler.CustomBinder(typeResolver);
					}
					binaryFormatter.Binder = this.binder;
					using (MemoryStream memoryStream2 = new MemoryStream(Convert.FromBase64String(this.dataString)))
					{
						return binaryFormatter.Deserialize(memoryStream2);
					}
				}
				obj = null;
			}
			catch (SerializationException ex)
			{
				if (ex.Message.StartsWith("Couldn't find assembly"))
				{
					throw new ArgumentException(ex.Message);
				}
				throw ex;
			}
			return obj;
		}

		// Token: 0x04000D8F RID: 3471
		private string dataString;

		// Token: 0x04000D90 RID: 3472
		private string mime_type;

		// Token: 0x04000D91 RID: 3473
		private SerializedFromResXHandler.CustomBinder binder;

		// Token: 0x0200002C RID: 44
		private sealed class CustomBinder : SerializationBinder
		{
			// Token: 0x0600010E RID: 270 RVA: 0x00005E1C File Offset: 0x0000401C
			public CustomBinder(ITypeResolutionService _typeResolver)
			{
				this.typeResolver = _typeResolver;
			}

			// Token: 0x0600010F RID: 271 RVA: 0x00005E2C File Offset: 0x0000402C
			public override Type BindToType(string assemblyName, string typeName)
			{
				Type type = null;
				string text = string.Format("{0}, {1}", typeName, assemblyName);
				if (this.typeResolver != null)
				{
					type = this.typeResolver.GetType(text);
				}
				if (type == null)
				{
					type = Type.GetType(text);
				}
				return type;
			}

			// Token: 0x04000D92 RID: 3474
			private ITypeResolutionService typeResolver;
		}
	}
}
