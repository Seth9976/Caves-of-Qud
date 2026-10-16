using System;
using System.Text;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents an X509 extension.</summary>
	// Token: 0x020002E5 RID: 741
	public class X509Extension : AsnEncodedData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> class.</summary>
		// Token: 0x0600177E RID: 6014 RVA: 0x0005D132 File Offset: 0x0005B332
		protected X509Extension()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> class.</summary>
		/// <param name="encodedExtension">The encoded data to be used to create the extension.</param>
		/// <param name="critical">true if the extension is critical; otherwise false.</param>
		// Token: 0x0600177F RID: 6015 RVA: 0x0005D13A File Offset: 0x0005B33A
		public X509Extension(AsnEncodedData encodedExtension, bool critical)
		{
			if (encodedExtension.Oid == null)
			{
				throw new ArgumentNullException("encodedExtension.Oid");
			}
			base.Oid = encodedExtension.Oid;
			base.RawData = encodedExtension.RawData;
			this._critical = critical;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> class.</summary>
		/// <param name="oid">The object identifier used to identify the extension.</param>
		/// <param name="rawData">The encoded data used to create the extension.</param>
		/// <param name="critical">true if the extension is critical; otherwise false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="oid" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="oid" /> is an empty string ("").</exception>
		// Token: 0x06001780 RID: 6016 RVA: 0x0005D174 File Offset: 0x0005B374
		public X509Extension(Oid oid, byte[] rawData, bool critical)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			base.Oid = oid;
			base.RawData = rawData;
			this._critical = critical;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> class.</summary>
		/// <param name="oid">A string representing the object identifier.</param>
		/// <param name="rawData">The encoded data used to create the extension.</param>
		/// <param name="critical">true if the extension is critical; otherwise false.</param>
		// Token: 0x06001781 RID: 6017 RVA: 0x0005D19F File Offset: 0x0005B39F
		public X509Extension(string oid, byte[] rawData, bool critical)
			: base(oid, rawData)
		{
			this._critical = critical;
		}

		/// <summary>Gets a Boolean value indicating whether the extension is critical.</summary>
		/// <returns>true if the extension is critical; otherwise, false.</returns>
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x0005D1B0 File Offset: 0x0005B3B0
		// (set) Token: 0x06001783 RID: 6019 RVA: 0x0005D1B8 File Offset: 0x0005B3B8
		public bool Critical
		{
			get
			{
				return this._critical;
			}
			set
			{
				this._critical = value;
			}
		}

		/// <summary>Copies the extension properties of the specified <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> to be copied.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asnEncodedData" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asnEncodedData" /> does not have a valid X.509 extension.</exception>
		// Token: 0x06001784 RID: 6020 RVA: 0x0005D1C4 File Offset: 0x0005B3C4
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("encodedData");
			}
			X509Extension x509Extension = asnEncodedData as X509Extension;
			if (x509Extension == null)
			{
				throw new ArgumentException(global::Locale.GetText("Expected a X509Extension instance."));
			}
			base.CopyFrom(asnEncodedData);
			this._critical = x509Extension.Critical;
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x0005D20C File Offset: 0x0005B40C
		internal string FormatUnkownData(byte[] data)
		{
			if (data == null || data.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				stringBuilder.Append(data[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000D4A RID: 3402
		private bool _critical;
	}
}
