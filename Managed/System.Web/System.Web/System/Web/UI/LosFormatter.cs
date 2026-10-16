using System;
using System.Configuration;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.Util;

namespace System.Web.UI
{
	/// <summary>Serializes the view state for a Web Forms page. This class cannot be inherited.</summary>
	// Token: 0x020001E1 RID: 481
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class LosFormatter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.LosFormatter" /> class using default values.</summary>
		// Token: 0x06001368 RID: 4968 RVA: 0x0003297D File Offset: 0x00030B7D
		public LosFormatter()
		{
			this.osf = new ObjectStateFormatter();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.LosFormatter" /> class using the specified enable flag and machine authentication code (MAC) key modifier.</summary>
		/// <param name="enableMac">true to enable view-state MAC; otherwise, false. </param>
		/// <param name="macKeyModifier">The modifier for the MAC key. </param>
		// Token: 0x06001369 RID: 4969 RVA: 0x00032990 File Offset: 0x00030B90
		public LosFormatter(bool enableMac, string macKeyModifier)
			: this(enableMac, string.IsNullOrEmpty(macKeyModifier) ? null : Encoding.ASCII.GetBytes(macKeyModifier))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.LosFormatter" /> class using the specified enable flag and machine authentication code (MAC) key modifier.</summary>
		/// <param name="enableMac">true to enable view-state MAC; otherwise, false.</param>
		/// <param name="macKeyModifier">The modifier for the MAC key.</param>
		// Token: 0x0600136A RID: 4970 RVA: 0x000329AF File Offset: 0x00030BAF
		public LosFormatter(bool enableMac, byte[] macKeyModifier)
		{
			this.osf = new ObjectStateFormatter();
			if (enableMac && macKeyModifier != null)
			{
				this.SetMacKey(macKeyModifier);
			}
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x000329D0 File Offset: 0x00030BD0
		private void SetMacKey(byte[] macKeyModifier)
		{
			try
			{
				this.osf.Section.ValidationKey = MachineKeySectionUtils.GetHexString(macKeyModifier);
			}
			catch (ArgumentException)
			{
			}
			catch (ConfigurationErrorsException)
			{
			}
		}

		/// <summary>Transforms the view-state value contained in a <see cref="T:System.IO.Stream" /> object to a limited object serialization (LOS)-formatted object.</summary>
		/// <returns>A LOS-formatted object.</returns>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the view-state value to transform. </param>
		// Token: 0x0600136C RID: 4972 RVA: 0x00032A18 File Offset: 0x00030C18
		public object Deserialize(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			object obj;
			using (StreamReader streamReader = new StreamReader(stream))
			{
				obj = this.Deserialize(streamReader.ReadToEnd());
			}
			return obj;
		}

		/// <summary>Transforms the view-state value contained in a <see cref="T:System.IO.TextReader" /> object to a limited object serialization (LOS)-formatted object.</summary>
		/// <returns>A LOS-formatted object.</returns>
		/// <param name="input">A <see cref="T:System.IO.TextReader" /> that contains the view-state value to transform. </param>
		// Token: 0x0600136D RID: 4973 RVA: 0x00032A64 File Offset: 0x00030C64
		public object Deserialize(TextReader input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return this.Deserialize(input.ReadToEnd());
		}

		/// <summary>Transforms the specified view-state value to a limited object serialization (LOS)-formatted object.</summary>
		/// <returns>A LOS-formatted object.</returns>
		/// <param name="input">The view-state value to transform. </param>
		/// <exception cref="T:System.Web.HttpException">The view state is invalid. </exception>
		// Token: 0x0600136E RID: 4974 RVA: 0x00032A80 File Offset: 0x00030C80
		public object Deserialize(string input)
		{
			if (input == null)
			{
				return null;
			}
			return this.osf.Deserialize(input);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00032A93 File Offset: 0x00030C93
		internal string SerializeToBase64(object value)
		{
			return this.osf.Serialize(value);
		}

		/// <summary>Transforms a limited object serialization (LOS)-formatted object into a view-state value and places the results into a <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> to receive the transformed value. </param>
		/// <param name="value">The LOS-formatted object to transform into a view-state value. </param>
		// Token: 0x06001370 RID: 4976 RVA: 0x00032AA4 File Offset: 0x00030CA4
		public void Serialize(Stream stream, object value)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanSeek)
			{
				throw new NotSupportedException();
			}
			string text = this.SerializeToBase64(value);
			byte[] bytes = Encoding.ASCII.GetBytes(text);
			stream.Write(bytes, 0, bytes.Length);
		}

		/// <summary>Transforms a limited object serialization (LOS)-formatted object into a view-state value and places the results into a <see cref="T:System.IO.TextWriter" /> object.</summary>
		/// <param name="output">The <see cref="T:System.IO.TextWriter" /> to receive the transformed value. </param>
		/// <param name="value">The LOS-formatted object to transform into a view-state value. </param>
		// Token: 0x06001371 RID: 4977 RVA: 0x00032AEC File Offset: 0x00030CEC
		public void Serialize(TextWriter output, object value)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			output.Write(this.SerializeToBase64(value));
		}

		// Token: 0x0400146C RID: 5228
		private ObjectStateFormatter osf;
	}
}
