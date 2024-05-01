using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService.Services.BaseService
{
    public abstract class BaseService<TEntity>
    {
        public BaseService()
        {

        }
        private static ConsorcioGestContext CreateContext()
        {
            ConsorcioGestContext _dbContext = new ConsorcioGestContext();
            return _dbContext;
        }

        public static bool DBAdd<K>(K entity, ConsorcioGestContext dbContext = null) where K : class
        {
            if (dbContext == null)
            {
                using (ConsorcioGestContext ctx = new ConsorcioGestContext())
                {
                    ctx.Entry(entity).State = EntityState.Added;
                    return ctx.SaveChanges() > 0;
                }
            }
            else
            {
                dbContext.Entry(entity).State = EntityState.Added;
                return dbContext.SaveChanges() > 0;
            }
        }

        public static async Task<bool> DBAddAsync<K>(K entity, ConsorcioGestContext dbContext = null) where K : class
        {
            if (dbContext == null)
            {
                using (ConsorcioGestContext ctx = new ConsorcioGestContext())
                {
                    ctx.Entry(entity).State = EntityState.Added;
                    return (await ctx.SaveChangesAsync()) > 0;
                }
            }
            else
            {
                dbContext.Entry(entity).State = EntityState.Added;
                return (await dbContext.SaveChangesAsync()) > 0;
            }
        }

        public static bool DBAddRange<K>(IEnumerable<K> entities, ConsorcioGestContext dbContext = null) where K : class
        {
            if (dbContext == null)
            {

                using (ConsorcioGestContext ctx = CreateContext())
                {
                    foreach (var entity in entities)
                    {
                        ctx.Entry(entity).State = EntityState.Added;
                    }
                    return ctx.SaveChanges() > 0;
                }
            }
            else
            {
                foreach (var entity in entities)
                {
                    dbContext.Entry(entity).State = EntityState.Added;
                }
                return dbContext.SaveChanges() > 0;
            }
        }
        public static async Task<bool> DBAddRangeAsync<K>(IEnumerable<K> entities, ConsorcioGestContext dbContext = null) where K : class
        {
            if (dbContext == null)
            {

                using (ConsorcioGestContext ctx = CreateContext())
                {
                    foreach (var entity in entities)
                    {
                        ctx.Entry(entity).State = EntityState.Added;
                    }
                    return await ctx.SaveChangesAsync() > 0;
                }
            }
            else
            {
                foreach (var entity in entities)
                {
                    dbContext.Entry(entity).State = EntityState.Added;
                }
                return await dbContext.SaveChangesAsync() > 0;
            }
        }

         public static bool DBUpdate<K>(K entity, ConsorcioGestContext dbContext = null) where K : class
        {
            bool result = false;

            if (dbContext == null)
            {
                using (ConsorcioGestContext ctx = CreateContext())
                {
                    object[] keyNames = (new EntityKeyHelper()).GetKeys(entity, ctx);
                    var original = ctx.Set<K>().Find(keyNames);
                    if (original != null)
                    {
                        ctx.Entry(original).CurrentValues.SetValues(entity);
                        result = (ctx.SaveChanges() != 0);
                    }
                }
            }
            else
            {
                object[] keyNames = (new EntityKeyHelper()).GetKeys(entity, dbContext);
                var original = dbContext.Set<K>().Find(keyNames);
                if (original != null)
                {
                    dbContext.Entry(original).CurrentValues.SetValues(entity);
                    result = (dbContext.SaveChanges() != 0);
                }
            }

            return result;
        }
       


        public static bool DBDelete<K>(K entity, ConsorcioGestContext dbContext = null) where K : class
        {
            if (dbContext == null)
            {
                using (ConsorcioGestContext ctx = CreateContext())
                {
                    ctx.Entry(entity).State = EntityState.Deleted;
                    return ctx.SaveChanges() > 0;
                }
            }
            else
            {
                dbContext.Entry(entity).State = EntityState.Deleted;
                return dbContext.SaveChanges() > 0;
            }
        }
        public static async Task<bool> DBDeleteAsync<K>(K entity, ConsorcioGestContext dbContext = null) where K : class
        {
            if (dbContext == null)
            {
                using (ConsorcioGestContext ctx = CreateContext())
                {
                    ctx.Entry(entity).State = EntityState.Deleted;
                    return (await ctx.SaveChangesAsync()) > 0;
                }
            }
            else
            {
                dbContext.Entry(entity).State = EntityState.Deleted;
                return (await dbContext.SaveChangesAsync()) > 0;
            }
        }

    }

    public sealed class EntityKeyHelper
    {
        private readonly Dictionary<Type, string[]> _dict = new Dictionary<Type, string[]>();
        public EntityKeyHelper() { }
        public string[] GetKeyNames<T>(DbContext context) where T : class
        {
            Type t = typeof(T);

            // Retrieve the base type
            while (t.BaseType != typeof(object))
                t = t.BaseType;

            if (_dict.TryGetValue(t, out string[] keys))
                return keys;

            IEntityType entityType = context.Model.FindEntityType(t);

            IEnumerable<IProperty> keyProperties = entityType.FindPrimaryKey().Properties;

            string[] keyNames = keyProperties.Select(k => k.Name).ToArray();

            _dict.Add(t, keyNames);

            return keyNames;
        }

        public object[] GetKeys<T>(T entity, DbContext context) where T : class
        {
            object[] keys = null;
            if (entity != null)
            {
                var keyNames = GetKeyNames<T>(context);
                Type type = typeof(T);
                keys = new object[keyNames.Length];
                for (int i = 0; i < keyNames.Length; i++)
                {
                    keys[i] = type.GetProperty(keyNames[i])?.GetValue(entity);
                }
            }
            return keys;
        }
    }

}
