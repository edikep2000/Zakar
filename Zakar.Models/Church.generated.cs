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
	[Table()]
	[ConcurrencyControl(OptimisticConcurrencyControlStrategy.Changed)]
	[KeyGenerator(KeyGenerator.Autoinc)]
	public partial class Church : INotifyPropertyChanging, INotifyPropertyChanged
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
		
		private int _groupId;
		[Storage("_groupId")]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual int GroupId
		{
			get
			{
				return this._groupId;
			}
			set
			{
				if(this._groupId != value)
				{
					this.OnPropertyChanging("GroupId");
					this._groupId = value;
					this.OnPropertyChanged("GroupId");
				}
			}
		}
		
		private int? _defaultCurrencyId;
		[Storage("_defaultCurrencyId")]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual int? DefaultCurrencyId
		{
			get
			{
				return this._defaultCurrencyId;
			}
			set
			{
				if(this._defaultCurrencyId != value)
				{
					this.OnPropertyChanging("DefaultCurrencyId");
					this._defaultCurrencyId = value;
					this.OnPropertyChanged("DefaultCurrencyId");
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
		
		private string _adminId;
		[Storage("_adminId")]
		[System.ComponentModel.DataAnnotations.Required()]
		public virtual string AdminId
		{
			get
			{
				return this._adminId;
			}
			set
			{
				if(this._adminId != value)
				{
					this.OnPropertyChanging("AdminId");
					this._adminId = value;
					this.OnPropertyChanged("AdminId");
				}
			}
		}
		
		private Group _group;
		[ForeignKeyAssociation(SharedFields = "GroupId", TargetFields = "Id")]
		[Storage("_group")]
		public virtual Group Group
		{
			get
			{
				return this._group;
			}
			set
			{
				if(this._group != value)
				{
					this.OnPropertyChanging("Group");
					this._group = value;
					this.OnPropertyChanged("Group");
				}
			}
		}
		
		private IList<PCF> _pCFs = new List<PCF>();
		[Collection(InverseProperty = "Church")]
		[Storage("_pCFs")]
		public virtual IList<PCF> PCFs
		{
			get
			{
				return this._pCFs;
			}
		}
		
		private IList<Partner> _partners = new List<Partner>();
		[Collection(InverseProperty = "Church")]
		[Storage("_partners")]
		public virtual IList<Partner> Partners
		{
			get
			{
				return this._partners;
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
