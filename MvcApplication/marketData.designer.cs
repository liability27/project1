﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcApplication
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="msdb1849")]
	public partial class marketDataDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertLWA(LWA instance);
    partial void UpdateLWA(LWA instance);
    partial void DeleteLWA(LWA instance);
    partial void InsertMaxSMP(MaxSMP instance);
    partial void UpdateMaxSMP(MaxSMP instance);
    partial void DeleteMaxSMP(MaxSMP instance);
    partial void InsertMinSMP(MinSMP instance);
    partial void UpdateMinSMP(MinSMP instance);
    partial void DeleteMinSMP(MinSMP instance);
    partial void InsertShadow_SMP(Shadow_SMP instance);
    partial void UpdateShadow_SMP(Shadow_SMP instance);
    partial void DeleteShadow_SMP(Shadow_SMP instance);
    partial void InsertSMP_Load(SMP_Load instance);
    partial void UpdateSMP_Load(SMP_Load instance);
    partial void DeleteSMP_Load(SMP_Load instance);
    #endregion
		
		public marketDataDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["msdb1849ConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public marketDataDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public marketDataDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public marketDataDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public marketDataDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<LWA> LWAs
		{
			get
			{
				return this.GetTable<LWA>();
			}
		}
		
		public System.Data.Linq.Table<MaxSMP> MaxSMPs
		{
			get
			{
				return this.GetTable<MaxSMP>();
			}
		}
		
		public System.Data.Linq.Table<MinSMP> MinSMPs
		{
			get
			{
				return this.GetTable<MinSMP>();
			}
		}
		
		public System.Data.Linq.Table<Shadow_SMP> Shadow_SMPs
		{
			get
			{
				return this.GetTable<Shadow_SMP>();
			}
		}
		
		public System.Data.Linq.Table<SMP_Load> SMP_Loads
		{
			get
			{
				return this.GetTable<SMP_Load>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.LWA")]
	public partial class LWA : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.DateTime _Date;
		
		private string _Run_Type;
		
		private string _Currency;
		
		private decimal _Lwa1;
		
		private decimal _SevenDayLWA;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnDateChanging(System.DateTime value);
    partial void OnDateChanged();
    partial void OnRun_TypeChanging(string value);
    partial void OnRun_TypeChanged();
    partial void OnCurrencyChanging(string value);
    partial void OnCurrencyChanged();
    partial void OnLwa1Changing(decimal value);
    partial void OnLwa1Changed();
    partial void OnSevenDayLWAChanging(decimal value);
    partial void OnSevenDayLWAChanged();
    #endregion
		
		public LWA()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date", DbType="Date NOT NULL", IsPrimaryKey=true)]
		public System.DateTime Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this.OnDateChanging(value);
					this.SendPropertyChanging();
					this._Date = value;
					this.SendPropertyChanged("Date");
					this.OnDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Run_Type", DbType="NChar(10) NOT NULL", CanBeNull=false)]
		public string Run_Type
		{
			get
			{
				return this._Run_Type;
			}
			set
			{
				if ((this._Run_Type != value))
				{
					this.OnRun_TypeChanging(value);
					this.SendPropertyChanging();
					this._Run_Type = value;
					this.SendPropertyChanged("Run_Type");
					this.OnRun_TypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Currency", DbType="NChar(10) NOT NULL", CanBeNull=false)]
		public string Currency
		{
			get
			{
				return this._Currency;
			}
			set
			{
				if ((this._Currency != value))
				{
					this.OnCurrencyChanging(value);
					this.SendPropertyChanging();
					this._Currency = value;
					this.SendPropertyChanged("Currency");
					this.OnCurrencyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Lwa", Storage="_Lwa1", DbType="Decimal(18,3) NOT NULL")]
		public decimal Lwa1
		{
			get
			{
				return this._Lwa1;
			}
			set
			{
				if ((this._Lwa1 != value))
				{
					this.OnLwa1Changing(value);
					this.SendPropertyChanging();
					this._Lwa1 = value;
					this.SendPropertyChanged("Lwa1");
					this.OnLwa1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SevenDayLWA", DbType="Decimal(18,3) NOT NULL")]
		public decimal SevenDayLWA
		{
			get
			{
				return this._SevenDayLWA;
			}
			set
			{
				if ((this._SevenDayLWA != value))
				{
					this.OnSevenDayLWAChanging(value);
					this.SendPropertyChanging();
					this._SevenDayLWA = value;
					this.SendPropertyChanged("SevenDayLWA");
					this.OnSevenDayLWAChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.MaxSMP")]
	public partial class MaxSMP : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.DateTime _Date;
		
		private string _Run_Type;
		
		private string _Currency;
		
		private decimal _MaxSMP1;
		
		private decimal _SevenDayMaxSMP;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnDateChanging(System.DateTime value);
    partial void OnDateChanged();
    partial void OnRun_TypeChanging(string value);
    partial void OnRun_TypeChanged();
    partial void OnCurrencyChanging(string value);
    partial void OnCurrencyChanged();
    partial void OnMaxSMP1Changing(decimal value);
    partial void OnMaxSMP1Changed();
    partial void OnSevenDayMaxSMPChanging(decimal value);
    partial void OnSevenDayMaxSMPChanged();
    #endregion
		
		public MaxSMP()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date", DbType="Date NOT NULL", IsPrimaryKey=true)]
		public System.DateTime Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this.OnDateChanging(value);
					this.SendPropertyChanging();
					this._Date = value;
					this.SendPropertyChanged("Date");
					this.OnDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Run_Type", DbType="NChar(10) NOT NULL", CanBeNull=false)]
		public string Run_Type
		{
			get
			{
				return this._Run_Type;
			}
			set
			{
				if ((this._Run_Type != value))
				{
					this.OnRun_TypeChanging(value);
					this.SendPropertyChanging();
					this._Run_Type = value;
					this.SendPropertyChanged("Run_Type");
					this.OnRun_TypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Currency", DbType="NChar(10) NOT NULL", CanBeNull=false)]
		public string Currency
		{
			get
			{
				return this._Currency;
			}
			set
			{
				if ((this._Currency != value))
				{
					this.OnCurrencyChanging(value);
					this.SendPropertyChanging();
					this._Currency = value;
					this.SendPropertyChanged("Currency");
					this.OnCurrencyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="MaxSMP", Storage="_MaxSMP1", DbType="Decimal(18,3) NOT NULL")]
		public decimal MaxSMP1
		{
			get
			{
				return this._MaxSMP1;
			}
			set
			{
				if ((this._MaxSMP1 != value))
				{
					this.OnMaxSMP1Changing(value);
					this.SendPropertyChanging();
					this._MaxSMP1 = value;
					this.SendPropertyChanged("MaxSMP1");
					this.OnMaxSMP1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SevenDayMaxSMP", DbType="Decimal(18,3) NOT NULL")]
		public decimal SevenDayMaxSMP
		{
			get
			{
				return this._SevenDayMaxSMP;
			}
			set
			{
				if ((this._SevenDayMaxSMP != value))
				{
					this.OnSevenDayMaxSMPChanging(value);
					this.SendPropertyChanging();
					this._SevenDayMaxSMP = value;
					this.SendPropertyChanged("SevenDayMaxSMP");
					this.OnSevenDayMaxSMPChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.MinSMP")]
	public partial class MinSMP : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.DateTime _Date;
		
		private string _Run_Type;
		
		private string _Currency;
		
		private decimal _MinSMP1;
		
		private decimal _SevenDayMinSMP;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnDateChanging(System.DateTime value);
    partial void OnDateChanged();
    partial void OnRun_TypeChanging(string value);
    partial void OnRun_TypeChanged();
    partial void OnCurrencyChanging(string value);
    partial void OnCurrencyChanged();
    partial void OnMinSMP1Changing(decimal value);
    partial void OnMinSMP1Changed();
    partial void OnSevenDayMinSMPChanging(decimal value);
    partial void OnSevenDayMinSMPChanged();
    #endregion
		
		public MinSMP()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date", DbType="Date NOT NULL", IsPrimaryKey=true)]
		public System.DateTime Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this.OnDateChanging(value);
					this.SendPropertyChanging();
					this._Date = value;
					this.SendPropertyChanged("Date");
					this.OnDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Run_Type", DbType="NChar(10) NOT NULL", CanBeNull=false)]
		public string Run_Type
		{
			get
			{
				return this._Run_Type;
			}
			set
			{
				if ((this._Run_Type != value))
				{
					this.OnRun_TypeChanging(value);
					this.SendPropertyChanging();
					this._Run_Type = value;
					this.SendPropertyChanged("Run_Type");
					this.OnRun_TypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Currency", DbType="NChar(10) NOT NULL", CanBeNull=false)]
		public string Currency
		{
			get
			{
				return this._Currency;
			}
			set
			{
				if ((this._Currency != value))
				{
					this.OnCurrencyChanging(value);
					this.SendPropertyChanging();
					this._Currency = value;
					this.SendPropertyChanged("Currency");
					this.OnCurrencyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="MinSMP", Storage="_MinSMP1", DbType="Decimal(18,3) NOT NULL")]
		public decimal MinSMP1
		{
			get
			{
				return this._MinSMP1;
			}
			set
			{
				if ((this._MinSMP1 != value))
				{
					this.OnMinSMP1Changing(value);
					this.SendPropertyChanging();
					this._MinSMP1 = value;
					this.SendPropertyChanged("MinSMP1");
					this.OnMinSMP1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SevenDayMinSMP", DbType="Decimal(18,3) NOT NULL")]
		public decimal SevenDayMinSMP
		{
			get
			{
				return this._SevenDayMinSMP;
			}
			set
			{
				if ((this._SevenDayMinSMP != value))
				{
					this.OnSevenDayMinSMPChanging(value);
					this.SendPropertyChanging();
					this._SevenDayMinSMP = value;
					this.SendPropertyChanged("SevenDayMinSMP");
					this.OnSevenDayMinSMPChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Shadow_SMP")]
	public partial class Shadow_SMP : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.DateTime _Date;
		
		private string _Run_Type;
		
		private string _Currency;
		
		private decimal _SMP;
		
		private decimal _ShadowPrice;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnDateChanging(System.DateTime value);
    partial void OnDateChanged();
    partial void OnRun_TypeChanging(string value);
    partial void OnRun_TypeChanged();
    partial void OnCurrencyChanging(string value);
    partial void OnCurrencyChanged();
    partial void OnSMPChanging(decimal value);
    partial void OnSMPChanged();
    partial void OnShadowPriceChanging(decimal value);
    partial void OnShadowPriceChanged();
    #endregion
		
		public Shadow_SMP()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date", DbType="DateTime NOT NULL", IsPrimaryKey=true)]
		public System.DateTime Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this.OnDateChanging(value);
					this.SendPropertyChanging();
					this._Date = value;
					this.SendPropertyChanged("Date");
					this.OnDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Run_Type", DbType="NChar(10) NOT NULL", CanBeNull=false)]
		public string Run_Type
		{
			get
			{
				return this._Run_Type;
			}
			set
			{
				if ((this._Run_Type != value))
				{
					this.OnRun_TypeChanging(value);
					this.SendPropertyChanging();
					this._Run_Type = value;
					this.SendPropertyChanged("Run_Type");
					this.OnRun_TypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Currency", DbType="NChar(10) NOT NULL", CanBeNull=false)]
		public string Currency
		{
			get
			{
				return this._Currency;
			}
			set
			{
				if ((this._Currency != value))
				{
					this.OnCurrencyChanging(value);
					this.SendPropertyChanging();
					this._Currency = value;
					this.SendPropertyChanged("Currency");
					this.OnCurrencyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SMP", DbType="Decimal(18,3) NOT NULL")]
		public decimal SMP
		{
			get
			{
				return this._SMP;
			}
			set
			{
				if ((this._SMP != value))
				{
					this.OnSMPChanging(value);
					this.SendPropertyChanging();
					this._SMP = value;
					this.SendPropertyChanged("SMP");
					this.OnSMPChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ShadowPrice", DbType="Decimal(18,3) NOT NULL")]
		public decimal ShadowPrice
		{
			get
			{
				return this._ShadowPrice;
			}
			set
			{
				if ((this._ShadowPrice != value))
				{
					this.OnShadowPriceChanging(value);
					this.SendPropertyChanging();
					this._ShadowPrice = value;
					this.SendPropertyChanged("ShadowPrice");
					this.OnShadowPriceChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SMP_Load")]
	public partial class SMP_Load : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.DateTime _Date;
		
		private string _Run_Type;
		
		private string _Currency;
		
		private decimal _SMP;
		
		private decimal _SystemLoad;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnDateChanging(System.DateTime value);
    partial void OnDateChanged();
    partial void OnRun_TypeChanging(string value);
    partial void OnRun_TypeChanged();
    partial void OnCurrencyChanging(string value);
    partial void OnCurrencyChanged();
    partial void OnSMPChanging(decimal value);
    partial void OnSMPChanged();
    partial void OnSystemLoadChanging(decimal value);
    partial void OnSystemLoadChanged();
    #endregion
		
		public SMP_Load()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date", DbType="DateTime NOT NULL", IsPrimaryKey=true)]
		public System.DateTime Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this.OnDateChanging(value);
					this.SendPropertyChanging();
					this._Date = value;
					this.SendPropertyChanged("Date");
					this.OnDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Run_Type", DbType="NChar(10) NOT NULL", CanBeNull=false)]
		public string Run_Type
		{
			get
			{
				return this._Run_Type;
			}
			set
			{
				if ((this._Run_Type != value))
				{
					this.OnRun_TypeChanging(value);
					this.SendPropertyChanging();
					this._Run_Type = value;
					this.SendPropertyChanged("Run_Type");
					this.OnRun_TypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Currency", DbType="NChar(10) NOT NULL", CanBeNull=false)]
		public string Currency
		{
			get
			{
				return this._Currency;
			}
			set
			{
				if ((this._Currency != value))
				{
					this.OnCurrencyChanging(value);
					this.SendPropertyChanging();
					this._Currency = value;
					this.SendPropertyChanged("Currency");
					this.OnCurrencyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SMP", DbType="Decimal(18,3) NOT NULL")]
		public decimal SMP
		{
			get
			{
				return this._SMP;
			}
			set
			{
				if ((this._SMP != value))
				{
					this.OnSMPChanging(value);
					this.SendPropertyChanging();
					this._SMP = value;
					this.SendPropertyChanged("SMP");
					this.OnSMPChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SystemLoad", DbType="Decimal(18,3) NOT NULL")]
		public decimal SystemLoad
		{
			get
			{
				return this._SystemLoad;
			}
			set
			{
				if ((this._SystemLoad != value))
				{
					this.OnSystemLoadChanging(value);
					this.SendPropertyChanging();
					this._SystemLoad = value;
					this.SendPropertyChanged("SystemLoad");
					this.OnSystemLoadChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
