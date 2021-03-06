using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqToAnything
{
    public class DelegateQueryable<T> : IQueryable<T>
    {
        QueryProvider<T> provider;
        Expression expression;

        public DelegateQueryable(DataQuery<T> dataQuery)
        {
            this.provider = new QueryProvider<T>(dataQuery);
            this.expression = Expression.Constant(this);
        }

        public DelegateQueryable(DataQuery<T> dataQuery, Expression expression, QueryVisitor ev)
        {
            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type)) throw new ArgumentOutOfRangeException("expression");

            this.provider = new QueryProvider<T>(dataQuery, ev);
            this.expression = expression;
            
        }

        Expression IQueryable.Expression
        {
            get { return this.expression; }
        }

        Type IQueryable.ElementType
        {
            get { return typeof(T); }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return this.provider; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.provider.GetEnumerable<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


    }
}
