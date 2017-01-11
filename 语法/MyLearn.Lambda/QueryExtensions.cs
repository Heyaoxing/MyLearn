using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Lambda
{
    public static class QueryExtensions
    {
        public static string Where<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            var expression = Expression.Call(null, ((MethodInfo)MethodBase.GetCurrentMethod()).MakeGenericMethod(new Type[] { typeof(TSource) })
                , new Expression[] { source.Expression, Expression.Quote(predicate) });

            var translator = new QueryTranslator();
            return translator.Translate(expression);
        }
    }


    public class QueryTranslator : ExpressionVisitor
    {
        StringBuilder sb = new StringBuilder();
        internal string Translate(Expression expression)
        {
            this.Visit(expression);
            return sb.ToString();
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.DeclaringType == typeof(QueryExtensions) && m.Method.Name == "Where")
            {
                sb.Append(" SELECT * FROM ");
                this.Visit(m.Arguments[0]);
                sb.Append(" as T WHERE ");
                LambdaExpression lambda = (LambdaExpression)m.Arguments[1];
                this.Visit(lambda.Body);
                return m;
            }
            throw new NotSupportedException(string.Format("方法{0}不支持", m.Method.Name));
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            sb.Append("(");
            this.Visit(b.Left);
            switch (b.NodeType)
            {
                case ExpressionType.And:
                    sb.Append(" AND ");
                    break;
                case ExpressionType.Or:
                    sb.Append(" OR");
                    break;
                case ExpressionType.Equal:
                    sb.Append(" = ");
                    break;
                case ExpressionType.NotEqual:
                    sb.Append(" <> ");
                    break;
                case ExpressionType.LessThan:
                    sb.Append(" < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    sb.Append(" <= ");
                    break;
                case ExpressionType.GreaterThan:
                    sb.Append(" > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    sb.Append(" >= ");
                    break;
                default:
                    throw new NotSupportedException(string.Format("二元运算符{0}不支持", b.NodeType));
            }
            this.Visit(b.Right);
            sb.Append(")");
            return b;
        }
    }
}
