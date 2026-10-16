using System;
using System.Text;

namespace System.Runtime.Versioning
{
	/// <summary>Represents the name of a version of the .NET Framework.</summary>
	// Token: 0x02000185 RID: 389
	[Serializable]
	public sealed class FrameworkName : IEquatable<FrameworkName>
	{
		/// <summary>Gets the identifier of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>The identifier of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x0002D100 File Offset: 0x0002B300
		public string Identifier
		{
			get
			{
				return this.m_identifier;
			}
		}

		/// <summary>Gets the version of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>An object that contains version information about this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0002D108 File Offset: 0x0002B308
		public Version Version
		{
			get
			{
				return this.m_version;
			}
		}

		/// <summary>Gets the profile name of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>The profile name of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x0002D110 File Offset: 0x0002B310
		public string Profile
		{
			get
			{
				return this.m_profile;
			}
		}

		/// <summary>Gets the full name of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>The full name of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0002D118 File Offset: 0x0002B318
		public string FullName
		{
			get
			{
				if (this.m_fullName == null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(this.Identifier);
					stringBuilder.Append(',');
					stringBuilder.Append("Version").Append('=');
					stringBuilder.Append('v');
					stringBuilder.Append(this.Version);
					if (!string.IsNullOrEmpty(this.Profile))
					{
						stringBuilder.Append(',');
						stringBuilder.Append("Profile").Append('=');
						stringBuilder.Append(this.Profile);
					}
					this.m_fullName = stringBuilder.ToString();
				}
				return this.m_fullName;
			}
		}

		/// <summary>Returns a value that indicates whether this <see cref="T:System.Runtime.Versioning.FrameworkName" /> instance represents the same .NET Framework version as a specified object.</summary>
		/// <returns>true if every component of the current <see cref="T:System.Runtime.Versioning.FrameworkName" /> object matches the corresponding component of <paramref name="obj" />; otherwise, false.</returns>
		/// <param name="obj">The object to compare to the current instance.</param>
		// Token: 0x06000A64 RID: 2660 RVA: 0x0002D1BD File Offset: 0x0002B3BD
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FrameworkName);
		}

		/// <summary>Returns a value that indicates whether this <see cref="T:System.Runtime.Versioning.FrameworkName" /> instance represents the same .NET Framework version as a specified <see cref="T:System.Runtime.Versioning.FrameworkName" /> instance.</summary>
		/// <returns>true if every component of the current <see cref="T:System.Runtime.Versioning.FrameworkName" /> object matches the corresponding component of <paramref name="other" />; otherwise, false.</returns>
		/// <param name="other">The object to compare to the current instance.</param>
		// Token: 0x06000A65 RID: 2661 RVA: 0x0002D1CB File Offset: 0x0002B3CB
		public bool Equals(FrameworkName other)
		{
			return other != null && (this.Identifier == other.Identifier && this.Version == other.Version) && this.Profile == other.Profile;
		}

		/// <summary>Returns the hash code for the <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>A 32-bit signed integer that represents the hash code of this instance.</returns>
		// Token: 0x06000A66 RID: 2662 RVA: 0x0002D20B File Offset: 0x0002B40B
		public override int GetHashCode()
		{
			return this.Identifier.GetHashCode() ^ this.Version.GetHashCode() ^ this.Profile.GetHashCode();
		}

		/// <summary>Returns the string representation of this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</summary>
		/// <returns>A string that represents this <see cref="T:System.Runtime.Versioning.FrameworkName" /> object.</returns>
		// Token: 0x06000A67 RID: 2663 RVA: 0x0002D230 File Offset: 0x0002B430
		public override string ToString()
		{
			return this.FullName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.FrameworkName" /> class from a string and a <see cref="T:System.Version" /> object that identify a .NET Framework version.</summary>
		/// <param name="identifier">A string that identifies a .NET Framework version. </param>
		/// <param name="version">An object that contains .NET Framework version information.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identifier" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identifier" /> is null.-or-<paramref name="version" /> is null.</exception>
		// Token: 0x06000A68 RID: 2664 RVA: 0x0002D238 File Offset: 0x0002B438
		public FrameworkName(string identifier, Version version)
			: this(identifier, version, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.FrameworkName" /> class from a string, a <see cref="T:System.Version" /> object that identifies a .NET Framework version, and a profile name.</summary>
		/// <param name="identifier">A string that identifies a .NET Framework version.</param>
		/// <param name="version">An object that contains .NET Framework version information.</param>
		/// <param name="profile">A profile name.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="identifier" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="identifier" /> is null.-or-<paramref name="version" /> is null.</exception>
		// Token: 0x06000A69 RID: 2665 RVA: 0x0002D244 File Offset: 0x0002B444
		public FrameworkName(string identifier, Version version, string profile)
		{
			if (identifier == null)
			{
				throw new ArgumentNullException("identifier");
			}
			if (identifier.Trim().Length == 0)
			{
				throw new ArgumentException(SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "identifier" }), "identifier");
			}
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this.m_identifier = identifier.Trim();
			this.m_version = (Version)version.Clone();
			this.m_profile = ((profile == null) ? string.Empty : profile.Trim());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Versioning.FrameworkName" /> class from a string that contains information about a version of the .NET Framework.</summary>
		/// <param name="frameworkName">A string that contains .NET Framework version information.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="frameworkName" /> is <see cref="F:System.String.Empty" />.-or-<paramref name="frameworkName" /> has fewer than two components or more than three components.-or-<paramref name="frameworkName" /> does not include a major and minor version number.-or-<paramref name="frameworkName " />does not include a valid version number.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="frameworkName" /> is null.</exception>
		// Token: 0x06000A6A RID: 2666 RVA: 0x0002D2DC File Offset: 0x0002B4DC
		public FrameworkName(string frameworkName)
		{
			if (frameworkName == null)
			{
				throw new ArgumentNullException("frameworkName");
			}
			if (frameworkName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "frameworkName" }), "frameworkName");
			}
			string[] array = frameworkName.Split(',', StringSplitOptions.None);
			if (array.Length < 2 || array.Length > 3)
			{
				throw new ArgumentException(SR.GetString("FrameworkName cannot have less than two components or more than three components."), "frameworkName");
			}
			this.m_identifier = array[0].Trim();
			if (this.m_identifier.Length == 0)
			{
				throw new ArgumentException(SR.GetString("FrameworkName is invalid."), "frameworkName");
			}
			bool flag = false;
			this.m_profile = string.Empty;
			int i = 1;
			while (i < array.Length)
			{
				string[] array2 = array[i].Split('=', StringSplitOptions.None);
				if (array2.Length != 2)
				{
					throw new ArgumentException(SR.GetString("FrameworkName is invalid."), "frameworkName");
				}
				string text = array2[0].Trim();
				string text2 = array2[1].Trim();
				if (text.Equals("Version", StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					if (text2.Length > 0 && (text2[0] == 'v' || text2[0] == 'V'))
					{
						text2 = text2.Substring(1);
					}
					try
					{
						this.m_version = new Version(text2);
						goto IL_0181;
					}
					catch (Exception ex)
					{
						throw new ArgumentException(SR.GetString("FrameworkName version component is invalid."), "frameworkName", ex);
					}
					goto IL_014B;
				}
				goto IL_014B;
				IL_0181:
				i++;
				continue;
				IL_014B:
				if (!text.Equals("Profile", StringComparison.OrdinalIgnoreCase))
				{
					throw new ArgumentException(SR.GetString("FrameworkName is invalid."), "frameworkName");
				}
				if (!string.IsNullOrEmpty(text2))
				{
					this.m_profile = text2;
					goto IL_0181;
				}
				goto IL_0181;
			}
			if (!flag)
			{
				throw new ArgumentException(SR.GetString("FrameworkName version component is missing."), "frameworkName");
			}
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Runtime.Versioning.FrameworkName" /> objects represent the same .NET Framework version.</summary>
		/// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters represent the same .NET Framework version; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06000A6B RID: 2667 RVA: 0x0002D4A0 File Offset: 0x0002B6A0
		public static bool operator ==(FrameworkName left, FrameworkName right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Runtime.Versioning.FrameworkName" /> objects represent different .NET Framework versions.</summary>
		/// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters represent different .NET Framework versions; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06000A6C RID: 2668 RVA: 0x0002D4B1 File Offset: 0x0002B6B1
		public static bool operator !=(FrameworkName left, FrameworkName right)
		{
			return !(left == right);
		}

		// Token: 0x040006FF RID: 1791
		private readonly string m_identifier;

		// Token: 0x04000700 RID: 1792
		private readonly Version m_version;

		// Token: 0x04000701 RID: 1793
		private readonly string m_profile;

		// Token: 0x04000702 RID: 1794
		private string m_fullName;

		// Token: 0x04000703 RID: 1795
		private const char c_componentSeparator = ',';

		// Token: 0x04000704 RID: 1796
		private const char c_keyValueSeparator = '=';

		// Token: 0x04000705 RID: 1797
		private const char c_versionValuePrefix = 'v';

		// Token: 0x04000706 RID: 1798
		private const string c_versionKey = "Version";

		// Token: 0x04000707 RID: 1799
		private const string c_profileKey = "Profile";
	}
}
