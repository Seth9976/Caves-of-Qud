using System;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200017A RID: 378
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LocalizableResourceString : LocalizableString, IObjectWritable
	{
		// Token: 0x06000FBB RID: 4027 RVA: 0x0002E1F7 File Offset: 0x0002C3F7
		static LocalizableResourceString()
		{
			ObjectBinder.RegisterTypeReader(typeof(LocalizableResourceString), (ObjectReader reader) => new LocalizableResourceString(reader));
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0002E218 File Offset: 0x0002C418
		public LocalizableResourceString(string nameOfLocalizableResource, ResourceManager resourceManager, Type resourceSource)
			: this(nameOfLocalizableResource, resourceManager, resourceSource, Array.Empty<string>())
		{
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0002E228 File Offset: 0x0002C428
		public LocalizableResourceString(string nameOfLocalizableResource, ResourceManager resourceManager, Type resourceSource, params string[] formatArguments)
		{
			if (nameOfLocalizableResource == null)
			{
				throw new ArgumentNullException("nameOfLocalizableResource");
			}
			if (resourceManager == null)
			{
				throw new ArgumentNullException("resourceManager");
			}
			if (resourceSource == null)
			{
				throw new ArgumentNullException("resourceSource");
			}
			if (formatArguments == null)
			{
				throw new ArgumentNullException("formatArguments");
			}
			this._resourceManager = resourceManager;
			this._nameOfLocalizableResource = nameOfLocalizableResource;
			this._resourceSource = resourceSource;
			this._formatArguments = formatArguments;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0002E298 File Offset: 0x0002C498
		private LocalizableResourceString(ObjectReader reader)
		{
			this._resourceSource = reader.ReadType();
			this._nameOfLocalizableResource = reader.ReadString();
			this._resourceManager = new ResourceManager(this._resourceSource);
			int num = reader.ReadInt32();
			if (num == 0)
			{
				this._formatArguments = Array.Empty<string>();
				return;
			}
			ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance(num);
			for (int i = 0; i < num; i++)
			{
				instance.Add(reader.ReadString());
			}
			this._formatArguments = instance.ToArrayAndFree();
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0002E315 File Offset: 0x0002C515
		bool IObjectWritable.ShouldReuseInSerialization
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0002E318 File Offset: 0x0002C518
		void IObjectWritable.WriteTo(ObjectWriter writer)
		{
			writer.WriteType(this._resourceSource);
			writer.WriteString(this._nameOfLocalizableResource);
			int num = this._formatArguments.Length;
			writer.WriteInt32(num);
			for (int i = 0; i < num; i++)
			{
				writer.WriteString(this._formatArguments[i]);
			}
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0002E368 File Offset: 0x0002C568
		protected override string GetText([Nullable(2)] IFormatProvider formatProvider)
		{
			CultureInfo cultureInfo = (formatProvider as CultureInfo) ?? CultureInfo.CurrentUICulture;
			string @string = this._resourceManager.GetString(this._nameOfLocalizableResource, cultureInfo);
			if (@string == null)
			{
				return string.Empty;
			}
			if (this._formatArguments.Length == 0)
			{
				return @string;
			}
			string text = @string;
			object[] formatArguments = this._formatArguments;
			return string.Format(text, formatArguments);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0002E3BC File Offset: 0x0002C5BC
		[NullableContext(2)]
		protected override bool AreEqual(object other)
		{
			LocalizableResourceString localizableResourceString = other as LocalizableResourceString;
			if (localizableResourceString != null && this._nameOfLocalizableResource == localizableResourceString._nameOfLocalizableResource && this._resourceManager == localizableResourceString._resourceManager && this._resourceSource == localizableResourceString._resourceSource)
			{
				return this._formatArguments.SequenceEqual<string>(localizableResourceString._formatArguments, (string a, string b) => a == b);
			}
			return false;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0002E43C File Offset: 0x0002C63C
		protected override int GetHash()
		{
			return Hash.Combine(this._nameOfLocalizableResource.GetHashCode(), Hash.Combine(this._resourceManager.GetHashCode(), Hash.Combine(this._resourceSource.GetHashCode(), Hash.CombineValues<string>(this._formatArguments, int.MaxValue))));
		}

		// Token: 0x04000450 RID: 1104
		private readonly string _nameOfLocalizableResource;

		// Token: 0x04000451 RID: 1105
		private readonly ResourceManager _resourceManager;

		// Token: 0x04000452 RID: 1106
		private readonly Type _resourceSource;

		// Token: 0x04000453 RID: 1107
		private readonly string[] _formatArguments;
	}
}
