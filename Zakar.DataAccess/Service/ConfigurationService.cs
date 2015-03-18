using System;
using System.Linq;
using System.Linq.Expressions;
using Zakar.Models;

namespace Zakar.DataAccess.Service
{
    public class ConfigurationService
    {
        private readonly IRepository<Setting> _repository;

        public ConfigurationService(IRepository<Setting> repository)
        {
            _repository = repository;
        }


        public void AddBooleanSetting(string settingsName, bool value)
        {
            Setting entity = SettingsFactory.BuildNewBooleanSetting();
            try
            {
                bool flag = value;
                entity.Name = settingsName;
                entity.Value = flag.ToString();
                create(entity);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void AddIntegerSetting(string settingsName, object value)
        {
            Setting entity = SettingsFactory.BuildNewIntegerSetting();
            try
            {
                int num = Convert.ToInt32(value);
                entity.Name = settingsName;
                entity.Value = num.ToString();
                create(entity);
            }
            catch (InvalidCastException exception)
            {
                throw exception;
            }
            catch (FormatException exception2)
            {
                throw exception2;
            }
            catch (OverflowException exception3)
            {
                throw exception3;
            }
        }

        public void AddStringSetting(string settingsName, string value)
        {
            Setting entity = SettingsFactory.BuildNewStringSetting();
            try
            {
                entity.Name = settingsName;
                entity.Value = value;
                create(entity);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }


        public void create(Setting entity)
        {
            if (entity != null)
            {
                _repository.Insert(entity);
            }
        }

        public IQueryable<Setting> GetAll()
        {
            return (from i in _repository.GetAll()
                orderby i.Id
                select i);
        }

        public Setting GetSettingByName(string settingsName)
        {
            return
                _repository.Find(i => i.Name == settingsName).FirstOrDefault();
        }

        public Setting GetSingle(int key)
        {
            return _repository.Find(i => i.Id == key).FirstOrDefault();
        }

       
    }
}