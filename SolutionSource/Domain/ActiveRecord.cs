using SolutionSource.Services;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolutionSource.Domain
{
    public abstract class ActiveRecord
    {
        public abstract int ID { get; set; }
        public abstract DateTime Created { get; set; }

        public static List<T> GetAll<T>() where T : ActiveRecord, new()
        {
            using (var cn = DB.Get())
            {
                return cn.GetAll<T>().ToList();
            }
        }

        public static List<T> GetAllWithSQL<T>(string query)
        {
            using (var cn = DB.Get())
            {
                return cn.Query<T>(query).ToList();
            }
        }

        protected internal static void Create<T>(T instance) where T : ActiveRecord, new()
        {
            instance.Created = DateTime.Now;
            using (var cn = DB.Get())
            {
                var x = cn.Insert(instance);
            }
        }

        protected internal static void Delete<T>(T instance) where T : ActiveRecord, new()
        {
            if (instance.ID == 0)
            {
                throw new Exception("ActiveRecord Delete: The instance ID is missing");
            }

            using (var cn = DB.Get())
            {
                var x = cn.Delete(instance);
            }
        }

        public static T Where<T>(string where)
        {
            // TODO: need to be improved
            using (var cn = DB.Get())
            {
                return cn.Query<T>($"SELECT * FROM {typeof(T).Name}s WHERE {where}").FirstOrDefault();
            }
        }

        protected internal static void Update<T>(T instance) where T : ActiveRecord, new()
        {

            if (instance.ID == 0)
            {
                throw new Exception("ActiveRecord Delete: The instance ID is missing");
            }
            using (var cn = DB.Get())
            {
                var x = cn.Update(instance);
            }
        }

        internal static T Get<T>(int id) where T : ActiveRecord, new()
        {
            using (var cn = DB.Get())
            {
                return cn.Get<T>(id);
            }
        }

        public void Update()
        {
            ActiveRecord.Update((dynamic)this);
        }

        public void Delete()
        {
            ActiveRecord.Delete((dynamic)this);
        }
    }
}