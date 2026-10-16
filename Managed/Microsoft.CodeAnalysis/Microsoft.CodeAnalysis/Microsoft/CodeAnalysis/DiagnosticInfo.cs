using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000173 RID: 371
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal class DiagnosticInfo : IFormattable, IObjectWritable
	{
		// Token: 0x06000F66 RID: 3942 RVA: 0x0002D708 File Offset: 0x0002B908
		static DiagnosticInfo()
		{
			ObjectBinder.RegisterTypeReader(typeof(DiagnosticInfo), (ObjectReader r) => new DiagnosticInfo(r));
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0002D76B File Offset: 0x0002B96B
		internal DiagnosticInfo(CommonMessageProvider messageProvider, int errorCode)
		{
			this._messageProvider = messageProvider;
			this._errorCode = errorCode;
			this._defaultSeverity = messageProvider.GetSeverity(errorCode);
			this._effectiveSeverity = this._defaultSeverity;
			this._arguments = Array.Empty<object>();
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0002D7A5 File Offset: 0x0002B9A5
		internal DiagnosticInfo(CommonMessageProvider messageProvider, int errorCode, params object[] arguments)
			: this(messageProvider, errorCode)
		{
			this._arguments = arguments;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0002D7B6 File Offset: 0x0002B9B6
		protected DiagnosticInfo(DiagnosticInfo original, DiagnosticSeverity overriddenSeverity)
		{
			this._messageProvider = original.MessageProvider;
			this._errorCode = original._errorCode;
			this._defaultSeverity = original.DefaultSeverity;
			this._arguments = original._arguments;
			this._effectiveSeverity = overriddenSeverity;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0002D7F8 File Offset: 0x0002B9F8
		internal static DiagnosticDescriptor GetDescriptor(int errorCode, CommonMessageProvider messageProvider)
		{
			DiagnosticSeverity severity = messageProvider.GetSeverity(errorCode);
			return DiagnosticInfo.GetOrCreateDescriptor(errorCode, severity, messageProvider);
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0002D818 File Offset: 0x0002BA18
		private static DiagnosticDescriptor GetOrCreateDescriptor(int errorCode, DiagnosticSeverity defaultSeverity, CommonMessageProvider messageProvider)
		{
			return ImmutableInterlocked.GetOrAdd<int, DiagnosticDescriptor>(ref DiagnosticInfo.s_errorCodeToDescriptorMap, errorCode, (int code) => DiagnosticInfo.CreateDescriptor(code, defaultSeverity, messageProvider));
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0002D850 File Offset: 0x0002BA50
		private static DiagnosticDescriptor CreateDescriptor(int errorCode, DiagnosticSeverity defaultSeverity, CommonMessageProvider messageProvider)
		{
			string idForErrorCode = messageProvider.GetIdForErrorCode(errorCode);
			LocalizableString title = messageProvider.GetTitle(errorCode);
			LocalizableString description = messageProvider.GetDescription(errorCode);
			LocalizableString messageFormat = messageProvider.GetMessageFormat(errorCode);
			string helpLink = messageProvider.GetHelpLink(errorCode);
			string category = messageProvider.GetCategory(errorCode);
			ImmutableArray<string> customTags = DiagnosticInfo.GetCustomTags(defaultSeverity);
			return new DiagnosticDescriptor(idForErrorCode, title, messageFormat, category, defaultSeverity, true, description, helpLink, customTags);
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0002D8A4 File Offset: 0x0002BAA4
		[Conditional("DEBUG")]
		internal static void AssertMessageSerializable(object[] args)
		{
			foreach (object obj in args)
			{
				if (!(obj is IFormattable))
				{
					Type type = obj.GetType();
					if (!(type == typeof(string)) && !(type == typeof(AssemblyIdentity)) && !IntrospectionExtensions.GetTypeInfo(type).IsPrimitive)
					{
						throw ExceptionUtilities.UnexpectedValue(type);
					}
				}
			}
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0002D90C File Offset: 0x0002BB0C
		internal DiagnosticInfo(CommonMessageProvider messageProvider, bool isWarningAsError, int errorCode, params object[] arguments)
			: this(messageProvider, errorCode, arguments)
		{
			if (isWarningAsError)
			{
				this._effectiveSeverity = DiagnosticSeverity.Error;
			}
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0002D922 File Offset: 0x0002BB22
		internal virtual DiagnosticInfo GetInstanceWithSeverity(DiagnosticSeverity severity)
		{
			return new DiagnosticInfo(this, severity);
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0002D92B File Offset: 0x0002BB2B
		bool IObjectWritable.ShouldReuseInSerialization
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0002D92E File Offset: 0x0002BB2E
		void IObjectWritable.WriteTo(ObjectWriter writer)
		{
			this.WriteTo(writer);
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0002D938 File Offset: 0x0002BB38
		protected virtual void WriteTo(ObjectWriter writer)
		{
			writer.WriteValue(this._messageProvider);
			writer.WriteUInt32((uint)this._errorCode);
			writer.WriteInt32((int)this._effectiveSeverity);
			writer.WriteInt32((int)this._defaultSeverity);
			int num = this._arguments.Length;
			writer.WriteUInt32((uint)num);
			if (num > 0)
			{
				foreach (object obj in this._arguments)
				{
					writer.WriteString(obj.ToString());
				}
			}
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0002D9B0 File Offset: 0x0002BBB0
		protected DiagnosticInfo(ObjectReader reader)
		{
			this._messageProvider = (CommonMessageProvider)reader.ReadValue();
			this._errorCode = (int)reader.ReadUInt32();
			this._effectiveSeverity = (DiagnosticSeverity)reader.ReadInt32();
			this._defaultSeverity = (DiagnosticSeverity)reader.ReadInt32();
			int num = (int)reader.ReadUInt32();
			if (num > 0)
			{
				object[] array = new string[num];
				this._arguments = array;
				for (int i = 0; i < num; i++)
				{
					this._arguments[i] = reader.ReadString();
				}
				return;
			}
			this._arguments = Array.Empty<object>();
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x0002DA37 File Offset: 0x0002BC37
		public int Code
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x0002DA3F File Offset: 0x0002BC3F
		public virtual DiagnosticDescriptor Descriptor
		{
			get
			{
				return DiagnosticInfo.GetOrCreateDescriptor(this._errorCode, this._defaultSeverity, this._messageProvider);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0002DA58 File Offset: 0x0002BC58
		public DiagnosticSeverity Severity
		{
			get
			{
				return this._effectiveSeverity;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0002DA60 File Offset: 0x0002BC60
		public DiagnosticSeverity DefaultSeverity
		{
			get
			{
				return this._defaultSeverity;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x0002DA68 File Offset: 0x0002BC68
		public int WarningLevel
		{
			get
			{
				if (this._effectiveSeverity != this._defaultSeverity)
				{
					return Diagnostic.GetDefaultWarningLevel(this._effectiveSeverity);
				}
				return this._messageProvider.GetWarningLevel(this._errorCode);
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x0002DA95 File Offset: 0x0002BC95
		public bool IsWarningAsError
		{
			get
			{
				return this.DefaultSeverity == DiagnosticSeverity.Warning && this.Severity == DiagnosticSeverity.Error;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x0002DAAB File Offset: 0x0002BCAB
		public string Category
		{
			get
			{
				return this._messageProvider.GetCategory(this._errorCode);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0002DABE File Offset: 0x0002BCBE
		[Nullable(new byte[] { 0, 1 })]
		internal ImmutableArray<string> CustomTags
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return DiagnosticInfo.GetCustomTags(this._defaultSeverity);
			}
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0002DACB File Offset: 0x0002BCCB
		[return: Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<string> GetCustomTags(DiagnosticSeverity defaultSeverity)
		{
			if (defaultSeverity != DiagnosticSeverity.Error)
			{
				return DiagnosticInfo.s_compilerNonErrorCustomTags;
			}
			return DiagnosticInfo.s_compilerErrorCustomTags;
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0002DADC File Offset: 0x0002BCDC
		internal bool IsNotConfigurable()
		{
			return this._defaultSeverity == DiagnosticSeverity.Error;
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0002DAE7 File Offset: 0x0002BCE7
		public virtual IReadOnlyList<Location> AdditionalLocations
		{
			get
			{
				return SpecializedCollections.EmptyReadOnlyList<Location>();
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0002DAEE File Offset: 0x0002BCEE
		public virtual string MessageIdentifier
		{
			get
			{
				return this._messageProvider.GetIdForErrorCode(this._errorCode);
			}
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0002DB04 File Offset: 0x0002BD04
		public virtual string GetMessage([Nullable(2)] IFormatProvider formatProvider = null)
		{
			string text = this._messageProvider.LoadMessage(this._errorCode, formatProvider as CultureInfo);
			if (string.IsNullOrEmpty(text))
			{
				return string.Empty;
			}
			if (this._arguments.Length == 0)
			{
				return text;
			}
			return string.Format(formatProvider, text, this.GetArgumentsToUse(formatProvider));
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0002DB50 File Offset: 0x0002BD50
		protected object[] GetArgumentsToUse([Nullable(2)] IFormatProvider formatProvider)
		{
			object[] array = null;
			for (int i = 0; i < this._arguments.Length; i++)
			{
				DiagnosticInfo diagnosticInfo = this._arguments[i] as DiagnosticInfo;
				if (diagnosticInfo != null)
				{
					array = this.InitializeArgumentListIfNeeded(array);
					array[i] = diagnosticInfo.GetMessage(formatProvider);
				}
				else
				{
					ISymbol symbol;
					if ((symbol = this._arguments[i] as ISymbol) == null)
					{
						ISymbolInternal symbolInternal = this._arguments[i] as ISymbolInternal;
						symbol = ((symbolInternal != null) ? symbolInternal.GetISymbol() : null);
					}
					ISymbol symbol2 = symbol;
					if (symbol2 != null)
					{
						array = this.InitializeArgumentListIfNeeded(array);
						array[i] = this._messageProvider.GetErrorDisplayString(symbol2);
					}
				}
			}
			return array ?? this._arguments;
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0002DBE8 File Offset: 0x0002BDE8
		private object[] InitializeArgumentListIfNeeded([Nullable(new byte[] { 2, 1 })] object[] argumentsToUse)
		{
			if (argumentsToUse != null)
			{
				return argumentsToUse;
			}
			object[] array = new object[this._arguments.Length];
			Array.Copy(this._arguments, array, array.Length);
			return array;
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0002DC18 File Offset: 0x0002BE18
		internal object[] Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x0002DC20 File Offset: 0x0002BE20
		internal CommonMessageProvider MessageProvider
		{
			get
			{
				return this._messageProvider;
			}
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0002DC28 File Offset: 0x0002BE28
		[NullableContext(2)]
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0002DC31 File Offset: 0x0002BE31
		public string ToString([Nullable(2)] IFormatProvider formatProvider)
		{
			return this.ToString(null, formatProvider);
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0002DC3B File Offset: 0x0002BE3B
		[NullableContext(2)]
		[return: Nullable(1)]
		string IFormattable.ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format(formatProvider, "{0}: {1}", this._messageProvider.GetMessagePrefix(this.MessageIdentifier, this.Severity, this.IsWarningAsError, formatProvider as CultureInfo), this.GetMessage(formatProvider));
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0002DC74 File Offset: 0x0002BE74
		public sealed override int GetHashCode()
		{
			int num = this._errorCode;
			for (int i = 0; i < this._arguments.Length; i++)
			{
				num = Hash.Combine<object>(this._arguments[i], num);
			}
			return num;
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0002DCAC File Offset: 0x0002BEAC
		[NullableContext(2)]
		public sealed override bool Equals(object obj)
		{
			DiagnosticInfo diagnosticInfo = obj as DiagnosticInfo;
			bool flag = false;
			if (diagnosticInfo != null && diagnosticInfo._errorCode == this._errorCode && diagnosticInfo.GetType() == base.GetType() && this._arguments.Length == diagnosticInfo._arguments.Length)
			{
				flag = true;
				for (int i = 0; i < this._arguments.Length; i++)
				{
					if (!object.Equals(this._arguments[i], diagnosticInfo._arguments[i]))
					{
						flag = false;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0002DD2C File Offset: 0x0002BF2C
		[NullableContext(2)]
		private string GetDebuggerDisplay()
		{
			int code = this.Code;
			if (code == -2)
			{
				return "Void DiagnosticInfo";
			}
			if (code == -1)
			{
				return "Unresolved DiagnosticInfo";
			}
			return this.ToString();
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0002DD5B File Offset: 0x0002BF5B
		internal virtual DiagnosticInfo GetResolvedInfo()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x04000437 RID: 1079
		private readonly CommonMessageProvider _messageProvider;

		// Token: 0x04000438 RID: 1080
		private readonly int _errorCode;

		// Token: 0x04000439 RID: 1081
		private readonly DiagnosticSeverity _defaultSeverity;

		// Token: 0x0400043A RID: 1082
		private readonly DiagnosticSeverity _effectiveSeverity;

		// Token: 0x0400043B RID: 1083
		private readonly object[] _arguments;

		// Token: 0x0400043C RID: 1084
		private static ImmutableDictionary<int, DiagnosticDescriptor> s_errorCodeToDescriptorMap = ImmutableDictionary<int, DiagnosticDescriptor>.Empty;

		// Token: 0x0400043D RID: 1085
		[Nullable(new byte[] { 0, 1 })]
		private static readonly ImmutableArray<string> s_compilerErrorCustomTags = ImmutableArray.Create<string>("Compiler", "Telemetry", "NotConfigurable");

		// Token: 0x0400043E RID: 1086
		[Nullable(new byte[] { 0, 1 })]
		private static readonly ImmutableArray<string> s_compilerNonErrorCustomTags = ImmutableArray.Create<string>("Compiler", "Telemetry");
	}
}
