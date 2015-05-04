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
	[Table(UpdateSchema = true)]
	[KeyGenerator(KeyGenerator.Autoinc)]
	public partial class Cell : INotifyPropertyChanging, INotifyPropertyChanged
	{
		private int _id;
		[Column(IsPrimaryKey = true)]
		[Storage("_id")]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual int Id
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
		
		private string _uniqueId;
		[Storage("_uniqueId")]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual string UniqueId
		{
			get
			{
				return this._uniqueId;
			}
			set
			{
				if(this._uniqueId != value)
				{
					this.OnPropertyChanging("UniqueId");
					this._uniqueId = value;
					this.OnPropertyChanged("UniqueId");
				}
			}
		}
		
		private int _pCFId;
		[Storage("_pCFId")]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual int PCFId
		{
			get
			{
				return this._pCFId;
			}
			set
			{
				if(this._pCFId != value)
				{
					this.OnPropertyChanging("PCFId");
					this._pCFId = value;
					this.OnPropertyChanged("PCFId");
				}
			}
		}
		
		private string _name;
		[Storage("_name")]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if(this._name != value)
				{
					this.OnPropertyChanging("Name");
					this._name = value;
					this.OnPropertyChanged("Name");
				}
			}
		}
		
		private PCF _pCF;
		[ForeignKeyAssociation(SharedFields = "PCFId", TargetFields = "Id", IsManaged = true)]
		[Storage("_pCF")]
		public virtual PCF PCF
		{
			get
			{
				return this._pCF;
			}
			set
			{
				if(this._pCF != value)
				{
					this.OnPropertyChanging("PCF");
					this._pCF = value;
					this.OnPropertyChanged("PCF");
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
