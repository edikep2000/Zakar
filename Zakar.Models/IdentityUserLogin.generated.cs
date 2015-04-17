#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ClassGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using System.ComponentModel;
using Zakar.Models;

namespace Zakar.Models	
{
	[Table("IdentityUserLogin", UpdateSchema = true)]
	public partial class IdentityUserLogin : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private string _id;
		[Column("Id", OpenAccessType = OpenAccessType.StringVariableLength, IsPrimaryKey = true, Length = 255, Scale = 0, SqlType = "varchar", Converter = "OpenAccessRuntime.Data.VariableLengthAnsiStringConverter")]
		[Storage("_id")]
		[System.ComponentModel.DataAnnotations.StringLength(255)]
		[System.ComponentModel.DataAnnotations.Required()]
		[System.ComponentModel.DataAnnotations.Key()]
		public virtual string Id
		{
			get
			{
				return this._id;
			}
			set
			{
				if(this._id != value)
				{
					this.OnPropertyChanging("Id");
					this._id = value;
					this.OnPropertyChanged("Id");
				}
			}
		}
		
		private string _loginProvider;
		[Column("LoginProvider", OpenAccessType = OpenAccessType.StringVariableLength, IsNullable = true, Length = 255, Scale = 0, SqlType = "varchar", Converter = "OpenAccessRuntime.Data.VariableLengthAnsiStringConverter")]
		[Storage("_loginProvider")]
		[System.ComponentModel.DataAnnotations.StringLength(255)]
		public virtual string LoginProvider
		{
			get
			{
				return this._loginProvider;
			}
			set
			{
				if(this._loginProvider != value)
				{
					this.OnPropertyChanging("LoginProvider");
					this._loginProvider = value;
					this.OnPropertyChanged("LoginProvider");
				}
			}
		}
		
		private string _providerKey;
		[Column("ProviderKey", OpenAccessType = OpenAccessType.StringVariableLength, IsNullable = true, Length = 255, Scale = 0, SqlType = "varchar", Converter = "OpenAccessRuntime.Data.VariableLengthAnsiStringConverter")]
		[Storage("_providerKey")]
		[System.ComponentModel.DataAnnotations.StringLength(255)]
		public virtual string ProviderKey
		{
			get
			{
				return this._providerKey;
			}
			set
			{
				if(this._providerKey != value)
				{
					this.OnPropertyChanging("ProviderKey");
					this._providerKey = value;
					this.OnPropertyChanged("ProviderKey");
				}
			}
		}
		
		private IdentityUser _identityUser;
		[ForeignKeyAssociation(SharedFields = "Id", TargetFields = "Id", IsManaged = true)]
		[Storage("_identityUser")]
		public virtual IdentityUser IdentityUser
		{
			get
			{
				return this._identityUser;
			}
			set
			{
				if(this._identityUser != value)
				{
					this.OnPropertyChanging("IdentityUser");
					this._identityUser = value;
					this.OnPropertyChanged("IdentityUser");
				}
			}
		}
		
		#region INotifyPropertyChanging members
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		protected virtual void OnPropertyChanging(string propertyName)
		{
			if(this.PropertyChanging != null)
			{
				this.PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
			}
		}
		
		#endregion
		
		#region INotifyPropertyChanged members
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if(this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		#endregion
		
	}
}
#pragma warning restore 1591