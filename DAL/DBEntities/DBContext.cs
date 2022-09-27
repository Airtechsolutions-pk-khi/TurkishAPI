using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.DBEntities
{
    public partial class db_a8354f_turkishpizzaEntities
    {
        public void Reload(object entityEntry)
        {
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Entry(entityEntry).Reload();
            this.Configuration.AutoDetectChangesEnabled = true;
        }
        public void UpdateExcept<TEntity>(object entityEntry, params Expression<Func<TEntity, object>>[] properties) where TEntity : class
        {
            var dbEntityEntry = this.Entry(entityEntry);

            List<string> prop = PropExpressionToStrLst<TEntity>(properties);

            foreach (var name in dbEntityEntry.CurrentValues.PropertyNames.Except(prop))
            {
                dbEntityEntry.Property(name).IsModified = true;
            }
        }

        public void UpdateOnly<TEntity>(object entityEntry, params Expression<Func<TEntity, object>>[] properties) where TEntity : class
        {
            var dbEntityEntry = this.Entry(entityEntry);

            List<string> prop = PropExpressionToStrLst<TEntity>(properties);

            foreach (var name in dbEntityEntry.CurrentValues.PropertyNames.Intersect(prop))
            {
                dbEntityEntry.Property(name).IsModified = true;
            }
        }

        List<string> PropExpressionToStrLst<TEntity>(params Expression<Func<TEntity, object>>[] properties)
        {
            List<string> proplst = new List<string>();
            properties.ToList().ForEach((property) =>
            {
                var propertyName = string.Empty;
                var bodyExpression = property.Body;
                if (bodyExpression.NodeType == ExpressionType.Convert
                    && bodyExpression is UnaryExpression)
                {
                    Expression operand = ((UnaryExpression)property.Body).Operand;
                    propertyName = ((MemberExpression)operand).Member.Name;
                }
                else
                {
                    propertyName = System.Web.Mvc.ExpressionHelper.GetExpressionText(property);
                }

                proplst.Add(propertyName);
            });

            return proplst;
        }


    }

}
