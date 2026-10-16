using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

namespace Mono.Remoting.Channels.Unix
{
	// Token: 0x02000082 RID: 130
	internal class UnixBinaryCore
	{
		// Token: 0x06000689 RID: 1673 RVA: 0x0000E584 File Offset: 0x0000C784
		public UnixBinaryCore(object owner, IDictionary properties, string[] allowedProperties)
		{
			this._properties = properties;
			foreach (object obj in properties)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (string)dictionaryEntry.Key;
				if (Array.IndexOf<string>(allowedProperties, text) == -1)
				{
					throw new RemotingException(owner.GetType().Name + " does not recognize '" + text + "' configuration property");
				}
				if (!(text == "includeVersions"))
				{
					if (text == "strictBinding")
					{
						this._strictBinding = Convert.ToBoolean(dictionaryEntry.Value);
					}
				}
				else
				{
					this._includeVersions = Convert.ToBoolean(dictionaryEntry.Value);
				}
			}
			this.Init();
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0000E66C File Offset: 0x0000C86C
		public UnixBinaryCore()
		{
			this._properties = new Hashtable();
			this.Init();
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0000E68C File Offset: 0x0000C88C
		public void Init()
		{
			RemotingSurrogateSelector remotingSurrogateSelector = new RemotingSurrogateSelector();
			StreamingContext streamingContext = new StreamingContext(StreamingContextStates.Remoting, null);
			this._serializationFormatter = new BinaryFormatter(remotingSurrogateSelector, streamingContext);
			this._deserializationFormatter = new BinaryFormatter(null, streamingContext);
			if (!this._includeVersions)
			{
				this._serializationFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
				this._deserializationFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
			}
			if (!this._strictBinding)
			{
				this._serializationFormatter.Binder = SimpleBinder.Instance;
				this._deserializationFormatter.Binder = SimpleBinder.Instance;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0000E70B File Offset: 0x0000C90B
		public BinaryFormatter Serializer
		{
			get
			{
				return this._serializationFormatter;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0000E713 File Offset: 0x0000C913
		public BinaryFormatter Deserializer
		{
			get
			{
				return this._deserializationFormatter;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0000E71B File Offset: 0x0000C91B
		public IDictionary Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x040004BA RID: 1210
		private BinaryFormatter _serializationFormatter;

		// Token: 0x040004BB RID: 1211
		private BinaryFormatter _deserializationFormatter;

		// Token: 0x040004BC RID: 1212
		private bool _includeVersions = true;

		// Token: 0x040004BD RID: 1213
		private bool _strictBinding;

		// Token: 0x040004BE RID: 1214
		private IDictionary _properties;

		// Token: 0x040004BF RID: 1215
		public static UnixBinaryCore DefaultInstance = new UnixBinaryCore();
	}
}
