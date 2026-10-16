using System;
using System.Collections.Generic;
using System.Globalization;

namespace System
{
	/// <summary>Parses a new URI scheme. This is an abstract class.</summary>
	// Token: 0x02000169 RID: 361
	public abstract class UriParser
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0002A321 File Offset: 0x00028521
		internal string SchemeName
		{
			get
			{
				return this.m_Scheme;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0002A329 File Offset: 0x00028529
		internal int DefaultPort
		{
			get
			{
				return this.m_Port;
			}
		}

		/// <summary>Constructs a default URI parser.</summary>
		// Token: 0x0600099D RID: 2461 RVA: 0x0002A331 File Offset: 0x00028531
		protected UriParser()
			: this(UriSyntaxFlags.MayHavePath)
		{
		}

		/// <summary>Invoked by a <see cref="T:System.Uri" /> constructor to get a <see cref="T:System.UriParser" /> instance</summary>
		/// <returns>A <see cref="T:System.UriParser" /> for the constructed <see cref="T:System.Uri" />.</returns>
		// Token: 0x0600099E RID: 2462 RVA: 0x000075E1 File Offset: 0x000057E1
		protected virtual UriParser OnNewUri()
		{
			return this;
		}

		/// <summary>Invoked by the Framework when a <see cref="T:System.UriParser" /> method is registered.</summary>
		/// <param name="schemeName">The scheme that is associated with this <see cref="T:System.UriParser" />.</param>
		/// <param name="defaultPort">The port number of the scheme.</param>
		// Token: 0x0600099F RID: 2463 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void OnRegister(string schemeName, int defaultPort)
		{
		}

		/// <summary>Initialize the state of the parser and validate the URI.</summary>
		/// <param name="uri">The T:System.Uri to validate.</param>
		/// <param name="parsingError">Validation errors, if any.</param>
		// Token: 0x060009A0 RID: 2464 RVA: 0x0002A33B File Offset: 0x0002853B
		protected virtual void InitializeAndValidate(Uri uri, out UriFormatException parsingError)
		{
			parsingError = uri.ParseMinimal();
		}

		/// <summary>Called by <see cref="T:System.Uri" /> constructors and <see cref="Overload:System.Uri.TryCreate" /> to resolve a relative URI.</summary>
		/// <returns>The string of the resolved relative <see cref="T:System.Uri" />.</returns>
		/// <param name="baseUri">A base URI.</param>
		/// <param name="relativeUri">A relative URI.</param>
		/// <param name="parsingError">Errors during the resolve process, if any.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="baseUri" /> parameter is not an absolute <see cref="T:System.Uri" />- or -<paramref name="baseUri" /> parameter requires user-driven parsing.</exception>
		// Token: 0x060009A1 RID: 2465 RVA: 0x0002A348 File Offset: 0x00028548
		protected virtual string Resolve(Uri baseUri, Uri relativeUri, out UriFormatException parsingError)
		{
			if (baseUri.UserDrivenParsing)
			{
				throw new InvalidOperationException(SR.GetString("A derived type '{0}' is responsible for parsing this Uri instance. The base implementation must not be used.", new object[] { base.GetType().FullName }));
			}
			if (!baseUri.IsAbsoluteUri)
			{
				throw new InvalidOperationException(SR.GetString("This operation is not supported for a relative URI."));
			}
			string text = null;
			bool flag = false;
			Uri uri = Uri.ResolveHelper(baseUri, relativeUri, ref text, ref flag, out parsingError);
			if (parsingError != null)
			{
				return null;
			}
			if (uri != null)
			{
				return uri.OriginalString;
			}
			return text;
		}

		/// <summary>Determines whether <paramref name="baseUri" /> is a base URI for <paramref name="relativeUri" />.</summary>
		/// <returns>true if <paramref name="baseUri" /> is a base URI for <paramref name="relativeUri" />; otherwise, false.</returns>
		/// <param name="baseUri">The base URI.</param>
		/// <param name="relativeUri">The URI to test.</param>
		// Token: 0x060009A2 RID: 2466 RVA: 0x0002A3C1 File Offset: 0x000285C1
		protected virtual bool IsBaseOf(Uri baseUri, Uri relativeUri)
		{
			return baseUri.IsBaseOfHelper(relativeUri);
		}

		/// <summary>Gets the components from a URI.</summary>
		/// <returns>A string that contains the components.</returns>
		/// <param name="uri">The URI to parse.</param>
		/// <param name="components">The <see cref="T:System.UriComponents" /> to retrieve from <paramref name="uri" />.</param>
		/// <param name="format">One of the <see cref="T:System.UriFormat" /> values that controls how special characters are escaped.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="uriFormat" /> is invalid.- or -<paramref name="uriComponents" /> is not a combination of valid <see cref="T:System.UriComponents" /> values. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="uri" /> requires user-driven parsing- or -<paramref name="uri" /> is not an absolute URI. Relative URIs cannot be used with this method.</exception>
		// Token: 0x060009A3 RID: 2467 RVA: 0x0002A3CC File Offset: 0x000285CC
		protected virtual string GetComponents(Uri uri, UriComponents components, UriFormat format)
		{
			if ((components & UriComponents.SerializationInfoString) != (UriComponents)0 && components != UriComponents.SerializationInfoString)
			{
				throw new ArgumentOutOfRangeException("components", components, SR.GetString("UriComponents.SerializationInfoString must not be combined with other UriComponents."));
			}
			if ((format & (UriFormat)(-4)) != (UriFormat)0)
			{
				throw new ArgumentOutOfRangeException("format");
			}
			if (uri.UserDrivenParsing)
			{
				throw new InvalidOperationException(SR.GetString("A derived type '{0}' is responsible for parsing this Uri instance. The base implementation must not be used.", new object[] { base.GetType().FullName }));
			}
			if (!uri.IsAbsoluteUri)
			{
				throw new InvalidOperationException(SR.GetString("This operation is not supported for a relative URI."));
			}
			return uri.GetComponentsHelper(components, format);
		}

		/// <summary>Indicates whether a URI is well-formed.</summary>
		/// <returns>true if <paramref name="uri" /> is well-formed; otherwise, false.</returns>
		/// <param name="uri">The URI to check.</param>
		// Token: 0x060009A4 RID: 2468 RVA: 0x0002A462 File Offset: 0x00028662
		protected virtual bool IsWellFormedOriginalString(Uri uri)
		{
			return uri.InternalIsWellFormedOriginalString();
		}

		/// <summary>Associates a scheme and port number with a <see cref="T:System.UriParser" />.</summary>
		/// <param name="uriParser">The URI parser to register.</param>
		/// <param name="schemeName">The name of the scheme that is associated with this parser.</param>
		/// <param name="defaultPort">The default port number for the specified scheme.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriParser" /> parameter is null- or -<paramref name="schemeName" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="schemeName" /> parameter is not valid- or -<paramref name="defaultPort" /> parameter is not valid. The <paramref name="defaultPort" /> parameter is less than -1 or greater than 65,534.</exception>
		// Token: 0x060009A5 RID: 2469 RVA: 0x0002A46C File Offset: 0x0002866C
		public static void Register(UriParser uriParser, string schemeName, int defaultPort)
		{
			if (uriParser == null)
			{
				throw new ArgumentNullException("uriParser");
			}
			if (schemeName == null)
			{
				throw new ArgumentNullException("schemeName");
			}
			if (schemeName.Length == 1)
			{
				throw new ArgumentOutOfRangeException("schemeName");
			}
			if (!Uri.CheckSchemeName(schemeName))
			{
				throw new ArgumentOutOfRangeException("schemeName");
			}
			if ((defaultPort >= 65535 || defaultPort < 0) && defaultPort != -1)
			{
				throw new ArgumentOutOfRangeException("defaultPort");
			}
			schemeName = schemeName.ToLower(CultureInfo.InvariantCulture);
			UriParser.FetchSyntax(uriParser, schemeName, defaultPort);
		}

		/// <summary>Indicates whether the parser for a scheme is registered.</summary>
		/// <returns>true if <paramref name="schemeName" /> has been registered; otherwise, false.</returns>
		/// <param name="schemeName">The scheme name to check.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="schemeName" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="schemeName" /> parameter is not valid. </exception>
		// Token: 0x060009A6 RID: 2470 RVA: 0x0002A4EC File Offset: 0x000286EC
		public static bool IsKnownScheme(string schemeName)
		{
			if (schemeName == null)
			{
				throw new ArgumentNullException("schemeName");
			}
			if (!Uri.CheckSchemeName(schemeName))
			{
				throw new ArgumentOutOfRangeException("schemeName");
			}
			UriParser syntax = UriParser.GetSyntax(schemeName.ToLower(CultureInfo.InvariantCulture));
			return syntax != null && syntax.NotAny(UriSyntaxFlags.V1_UnknownUri);
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x0002A53B File Offset: 0x0002873B
		internal static bool ShouldUseLegacyV2Quirks
		{
			get
			{
				return UriParser.s_QuirksVersion <= UriParser.UriQuirksVersion.V2;
			}
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0002A548 File Offset: 0x00028748
		static UriParser()
		{
			UriParser.m_Table[UriParser.HttpUri.SchemeName] = UriParser.HttpUri;
			UriParser.HttpsUri = new UriParser.BuiltInUriParser("https", 443, UriParser.HttpUri.m_Flags);
			UriParser.m_Table[UriParser.HttpsUri.SchemeName] = UriParser.HttpsUri;
			UriParser.WsUri = new UriParser.BuiltInUriParser("ws", 80, UriParser.HttpSyntaxFlags);
			UriParser.m_Table[UriParser.WsUri.SchemeName] = UriParser.WsUri;
			UriParser.WssUri = new UriParser.BuiltInUriParser("wss", 443, UriParser.HttpSyntaxFlags);
			UriParser.m_Table[UriParser.WssUri.SchemeName] = UriParser.WssUri;
			UriParser.FtpUri = new UriParser.BuiltInUriParser("ftp", 21, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.FtpUri.SchemeName] = UriParser.FtpUri;
			UriParser.FileUri = new UriParser.BuiltInUriParser("file", -1, UriParser.FileSyntaxFlags);
			UriParser.m_Table[UriParser.FileUri.SchemeName] = UriParser.FileUri;
			UriParser.GopherUri = new UriParser.BuiltInUriParser("gopher", 70, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.GopherUri.SchemeName] = UriParser.GopherUri;
			UriParser.NntpUri = new UriParser.BuiltInUriParser("nntp", 119, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.NntpUri.SchemeName] = UriParser.NntpUri;
			UriParser.NewsUri = new UriParser.BuiltInUriParser("news", -1, UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.NewsUri.SchemeName] = UriParser.NewsUri;
			UriParser.MailToUri = new UriParser.BuiltInUriParser("mailto", 25, UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.MailToLikeUri | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.MailToUri.SchemeName] = UriParser.MailToUri;
			UriParser.UuidUri = new UriParser.BuiltInUriParser("uuid", -1, UriParser.NewsUri.m_Flags);
			UriParser.m_Table[UriParser.UuidUri.SchemeName] = UriParser.UuidUri;
			UriParser.TelnetUri = new UriParser.BuiltInUriParser("telnet", 23, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.TelnetUri.SchemeName] = UriParser.TelnetUri;
			UriParser.LdapUri = new UriParser.BuiltInUriParser("ldap", 389, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.LdapUri.SchemeName] = UriParser.LdapUri;
			UriParser.NetTcpUri = new UriParser.BuiltInUriParser("net.tcp", 808, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.NetTcpUri.SchemeName] = UriParser.NetTcpUri;
			UriParser.NetPipeUri = new UriParser.BuiltInUriParser("net.pipe", -1, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.NetPipeUri.SchemeName] = UriParser.NetPipeUri;
			UriParser.VsMacrosUri = new UriParser.BuiltInUriParser("vsmacros", -1, UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.FileLikeUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
			UriParser.m_Table[UriParser.VsMacrosUri.SchemeName] = UriParser.VsMacrosUri;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0002A8E7 File Offset: 0x00028AE7
		internal UriSyntaxFlags Flags
		{
			get
			{
				return this.m_Flags;
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0002A8EF File Offset: 0x00028AEF
		internal bool NotAny(UriSyntaxFlags flags)
		{
			return this.IsFullMatch(flags, UriSyntaxFlags.None);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0002A8F9 File Offset: 0x00028AF9
		internal bool InFact(UriSyntaxFlags flags)
		{
			return !this.IsFullMatch(flags, UriSyntaxFlags.None);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0002A906 File Offset: 0x00028B06
		internal bool IsAllSet(UriSyntaxFlags flags)
		{
			return this.IsFullMatch(flags, flags);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0002A910 File Offset: 0x00028B10
		private bool IsFullMatch(UriSyntaxFlags flags, UriSyntaxFlags expected)
		{
			UriSyntaxFlags uriSyntaxFlags;
			if ((flags & UriSyntaxFlags.UnEscapeDotsAndSlashes) == UriSyntaxFlags.None || !this.m_UpdatableFlagsUsed)
			{
				uriSyntaxFlags = this.m_Flags;
			}
			else
			{
				uriSyntaxFlags = (this.m_Flags & ~UriSyntaxFlags.UnEscapeDotsAndSlashes) | this.m_UpdatableFlags;
			}
			return (uriSyntaxFlags & flags) == expected;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0002A955 File Offset: 0x00028B55
		internal UriParser(UriSyntaxFlags flags)
		{
			this.m_Flags = flags;
			this.m_Scheme = string.Empty;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0002A970 File Offset: 0x00028B70
		private static void FetchSyntax(UriParser syntax, string lwrCaseSchemeName, int defaultPort)
		{
			if (syntax.SchemeName.Length != 0)
			{
				throw new InvalidOperationException(SR.GetString("The URI parser instance passed into 'uriParser' parameter is already registered with the scheme name '{0}'.", new object[] { syntax.SchemeName }));
			}
			Dictionary<string, UriParser> table = UriParser.m_Table;
			lock (table)
			{
				syntax.m_Flags &= ~UriSyntaxFlags.V1_UnknownUri;
				UriParser uriParser = null;
				UriParser.m_Table.TryGetValue(lwrCaseSchemeName, out uriParser);
				if (uriParser != null)
				{
					throw new InvalidOperationException(SR.GetString("A URI scheme name '{0}' already has a registered custom parser.", new object[] { uriParser.SchemeName }));
				}
				UriParser.m_TempTable.TryGetValue(syntax.SchemeName, out uriParser);
				if (uriParser != null)
				{
					lwrCaseSchemeName = uriParser.m_Scheme;
					UriParser.m_TempTable.Remove(lwrCaseSchemeName);
				}
				syntax.OnRegister(lwrCaseSchemeName, defaultPort);
				syntax.m_Scheme = lwrCaseSchemeName;
				syntax.CheckSetIsSimpleFlag();
				syntax.m_Port = defaultPort;
				UriParser.m_Table[syntax.SchemeName] = syntax;
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0002AA70 File Offset: 0x00028C70
		internal static UriParser FindOrFetchAsUnknownV1Syntax(string lwrCaseScheme)
		{
			UriParser uriParser = null;
			UriParser.m_Table.TryGetValue(lwrCaseScheme, out uriParser);
			if (uriParser != null)
			{
				return uriParser;
			}
			UriParser.m_TempTable.TryGetValue(lwrCaseScheme, out uriParser);
			if (uriParser != null)
			{
				return uriParser;
			}
			Dictionary<string, UriParser> table = UriParser.m_Table;
			UriParser uriParser2;
			lock (table)
			{
				if (UriParser.m_TempTable.Count >= 512)
				{
					UriParser.m_TempTable = new Dictionary<string, UriParser>(25);
				}
				uriParser = new UriParser.BuiltInUriParser(lwrCaseScheme, -1, UriSyntaxFlags.OptionalAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.V1_UnknownUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing);
				UriParser.m_TempTable[lwrCaseScheme] = uriParser;
				uriParser2 = uriParser;
			}
			return uriParser2;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0002AB0C File Offset: 0x00028D0C
		internal static UriParser GetSyntax(string lwrCaseScheme)
		{
			UriParser uriParser = null;
			UriParser.m_Table.TryGetValue(lwrCaseScheme, out uriParser);
			if (uriParser == null)
			{
				UriParser.m_TempTable.TryGetValue(lwrCaseScheme, out uriParser);
			}
			return uriParser;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0002AB3B File Offset: 0x00028D3B
		internal bool IsSimple
		{
			get
			{
				return this.InFact(UriSyntaxFlags.SimpleUserSyntax);
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002AB48 File Offset: 0x00028D48
		internal void CheckSetIsSimpleFlag()
		{
			Type type = base.GetType();
			if (type == typeof(GenericUriParser) || type == typeof(HttpStyleUriParser) || type == typeof(FtpStyleUriParser) || type == typeof(FileStyleUriParser) || type == typeof(NewsStyleUriParser) || type == typeof(GopherStyleUriParser) || type == typeof(NetPipeStyleUriParser) || type == typeof(NetTcpStyleUriParser) || type == typeof(LdapStyleUriParser))
			{
				this.m_Flags |= UriSyntaxFlags.SimpleUserSyntax;
			}
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002AC13 File Offset: 0x00028E13
		internal void SetUpdatableFlags(UriSyntaxFlags flags)
		{
			this.m_UpdatableFlags = flags;
			this.m_UpdatableFlagsUsed = true;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0002AC28 File Offset: 0x00028E28
		internal UriParser InternalOnNewUri()
		{
			UriParser uriParser = this.OnNewUri();
			if (this != uriParser)
			{
				uriParser.m_Scheme = this.m_Scheme;
				uriParser.m_Port = this.m_Port;
				uriParser.m_Flags = this.m_Flags;
			}
			return uriParser;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002AC65 File Offset: 0x00028E65
		internal void InternalValidate(Uri thisUri, out UriFormatException parsingError)
		{
			this.InitializeAndValidate(thisUri, out parsingError);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0002AC6F File Offset: 0x00028E6F
		internal string InternalResolve(Uri thisBaseUri, Uri uriLink, out UriFormatException parsingError)
		{
			return this.Resolve(thisBaseUri, uriLink, out parsingError);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0002AC7A File Offset: 0x00028E7A
		internal bool InternalIsBaseOf(Uri thisBaseUri, Uri uriLink)
		{
			return this.IsBaseOf(thisBaseUri, uriLink);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0002AC84 File Offset: 0x00028E84
		internal string InternalGetComponents(Uri thisUri, UriComponents uriComponents, UriFormat uriFormat)
		{
			return this.GetComponents(thisUri, uriComponents, uriFormat);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0002AC8F File Offset: 0x00028E8F
		internal bool InternalIsWellFormedOriginalString(Uri thisUri)
		{
			return this.IsWellFormedOriginalString(thisUri);
		}

		// Token: 0x04000673 RID: 1651
		private const UriSyntaxFlags SchemeOnlyFlags = UriSyntaxFlags.MayHavePath;

		// Token: 0x04000674 RID: 1652
		private static readonly Dictionary<string, UriParser> m_Table = new Dictionary<string, UriParser>(25);

		// Token: 0x04000675 RID: 1653
		private static Dictionary<string, UriParser> m_TempTable = new Dictionary<string, UriParser>(25);

		// Token: 0x04000676 RID: 1654
		private UriSyntaxFlags m_Flags;

		// Token: 0x04000677 RID: 1655
		private volatile UriSyntaxFlags m_UpdatableFlags;

		// Token: 0x04000678 RID: 1656
		private volatile bool m_UpdatableFlagsUsed;

		// Token: 0x04000679 RID: 1657
		private const UriSyntaxFlags c_UpdatableFlags = UriSyntaxFlags.UnEscapeDotsAndSlashes;

		// Token: 0x0400067A RID: 1658
		private int m_Port;

		// Token: 0x0400067B RID: 1659
		private string m_Scheme;

		// Token: 0x0400067C RID: 1660
		internal const int NoDefaultPort = -1;

		// Token: 0x0400067D RID: 1661
		private const int c_InitialTableSize = 25;

		// Token: 0x0400067E RID: 1662
		internal static UriParser HttpUri = new UriParser.BuiltInUriParser("http", 80, UriParser.HttpSyntaxFlags);

		// Token: 0x0400067F RID: 1663
		internal static UriParser HttpsUri;

		// Token: 0x04000680 RID: 1664
		internal static UriParser WsUri;

		// Token: 0x04000681 RID: 1665
		internal static UriParser WssUri;

		// Token: 0x04000682 RID: 1666
		internal static UriParser FtpUri;

		// Token: 0x04000683 RID: 1667
		internal static UriParser FileUri;

		// Token: 0x04000684 RID: 1668
		internal static UriParser GopherUri;

		// Token: 0x04000685 RID: 1669
		internal static UriParser NntpUri;

		// Token: 0x04000686 RID: 1670
		internal static UriParser NewsUri;

		// Token: 0x04000687 RID: 1671
		internal static UriParser MailToUri;

		// Token: 0x04000688 RID: 1672
		internal static UriParser UuidUri;

		// Token: 0x04000689 RID: 1673
		internal static UriParser TelnetUri;

		// Token: 0x0400068A RID: 1674
		internal static UriParser LdapUri;

		// Token: 0x0400068B RID: 1675
		internal static UriParser NetTcpUri;

		// Token: 0x0400068C RID: 1676
		internal static UriParser NetPipeUri;

		// Token: 0x0400068D RID: 1677
		internal static UriParser VsMacrosUri;

		// Token: 0x0400068E RID: 1678
		private static readonly UriParser.UriQuirksVersion s_QuirksVersion = UriParser.UriQuirksVersion.V3;

		// Token: 0x0400068F RID: 1679
		private const int c_MaxCapacity = 512;

		// Token: 0x04000690 RID: 1680
		private const UriSyntaxFlags UnknownV1SyntaxFlags = UriSyntaxFlags.OptionalAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.V1_UnknownUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000691 RID: 1681
		private static readonly UriSyntaxFlags HttpSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | (UriParser.ShouldUseLegacyV2Quirks ? UriSyntaxFlags.UnEscapeDotsAndSlashes : UriSyntaxFlags.None) | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000692 RID: 1682
		private const UriSyntaxFlags FtpSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000693 RID: 1683
		private static readonly UriSyntaxFlags FileSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | (UriParser.ShouldUseLegacyV2Quirks ? UriSyntaxFlags.None : UriSyntaxFlags.MayHaveQuery) | UriSyntaxFlags.FileLikeUri | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000694 RID: 1684
		private const UriSyntaxFlags VsmacrosSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.FileLikeUri | UriSyntaxFlags.AllowDOSPath | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000695 RID: 1685
		private const UriSyntaxFlags GopherSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000696 RID: 1686
		private const UriSyntaxFlags NewsSyntaxFlags = UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000697 RID: 1687
		private const UriSyntaxFlags NntpSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000698 RID: 1688
		private const UriSyntaxFlags TelnetSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x04000699 RID: 1689
		private const UriSyntaxFlags LdapSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x0400069A RID: 1690
		private const UriSyntaxFlags MailtoSyntaxFlags = UriSyntaxFlags.MayHaveUserInfo | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowEmptyHost | UriSyntaxFlags.AllowUncHost | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.MailToLikeUri | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x0400069B RID: 1691
		private const UriSyntaxFlags NetPipeSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x0400069C RID: 1692
		private const UriSyntaxFlags NetTcpSyntaxFlags = UriSyntaxFlags.MustHaveAuthority | UriSyntaxFlags.MayHavePort | UriSyntaxFlags.MayHavePath | UriSyntaxFlags.MayHaveQuery | UriSyntaxFlags.MayHaveFragment | UriSyntaxFlags.AllowDnsHost | UriSyntaxFlags.AllowIPv4Host | UriSyntaxFlags.AllowIPv6Host | UriSyntaxFlags.PathIsRooted | UriSyntaxFlags.ConvertPathSlashes | UriSyntaxFlags.CompressPath | UriSyntaxFlags.CanonicalizeAsFilePath | UriSyntaxFlags.UnEscapeDotsAndSlashes | UriSyntaxFlags.AllowIdn | UriSyntaxFlags.AllowIriParsing;

		// Token: 0x0200016A RID: 362
		private enum UriQuirksVersion
		{
			// Token: 0x0400069E RID: 1694
			V2 = 2,
			// Token: 0x0400069F RID: 1695
			V3
		}

		// Token: 0x0200016B RID: 363
		private class BuiltInUriParser : UriParser
		{
			// Token: 0x060009BB RID: 2491 RVA: 0x0002AC98 File Offset: 0x00028E98
			internal BuiltInUriParser(string lwrCaseScheme, int defaultPort, UriSyntaxFlags syntaxFlags)
				: base(syntaxFlags | UriSyntaxFlags.SimpleUserSyntax | UriSyntaxFlags.BuiltInSyntax)
			{
				this.m_Scheme = lwrCaseScheme;
				this.m_Port = defaultPort;
			}
		}
	}
}
