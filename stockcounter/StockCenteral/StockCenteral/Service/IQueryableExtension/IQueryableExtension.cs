using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service.IQueryableExtension
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> queryable, string filter)
        {
            Expression Expressions = null;
            List<Expression> andExpressionList = new List<Expression>();
            ParameterExpression parameter = Expression.Parameter(typeof(T), "c");



            var filteredObject = JsonConvert.DeserializeObject<T>(filter);

            filter = filter.Replace("{", "").Replace("}", "");
            List<string> filterPropertyName = filter.Split(',').Select(o => o.Split(':')[0]).ToList();


            PropertyInfo[] propertyInfo = filteredObject.GetType().GetProperties();
            MethodInfo equalsMethod = null;
            foreach (PropertyInfo info in propertyInfo)
            {
                object value = info.GetValue(filteredObject, null);
                if (value != null && filterPropertyName.Contains(info.Name))
                {
                    equalsMethod = value.GetType().GetMethod("Equals", new Type[] { value.GetType() });
                    var _value = Expression.Constant(value);
                    Expression nameParmeter = Expression.PropertyOrField(parameter, info.Name);
                    Type typeIfNullable = Nullable.GetUnderlyingType(info.PropertyType);


                    if (typeIfNullable != null)
                    {
                        nameParmeter = Expression.Convert(nameParmeter, typeIfNullable);
                    }

                    andExpressionList.Add(Expression.Call(nameParmeter, equalsMethod, _value));
                }
            }

            if (andExpressionList.Count > 0)
            {
                for (int i = 0; i < andExpressionList.Count; i++)
                {
                    if (i == 0)
                    {
                        Expressions = andExpressionList[i];
                        continue;
                    }
                    Expressions = Expression.And(Expressions, andExpressionList[i]);
                }
            }




            MethodCallExpression whereCallExpression = Expression.Call(
            typeof(Queryable),
            "Where",
            new Type[] { queryable.ElementType },
            queryable.Expression,
            Expression.Lambda<Func<T, bool>>(Expressions, new ParameterExpression[] { parameter }));

            return queryable.Provider.CreateQuery<T>(whereCallExpression);

        }

        public static IQueryable<T> FullTextSearch<T>(this IQueryable<T> queryable, string searchKey,
                                                     bool exactMatch)
        {

            ParameterExpression parameter = Expression.Parameter(typeof(T), "c");
            MethodInfo containsMethod = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
            MethodInfo equalsMethod = typeof(string).GetMethod("Equals", new Type[] { typeof(string) });
            MethodInfo trimMethod = typeof(string).GetMethod("Trim", Type.EmptyTypes);
            var stringConvertMethodInfo =
                typeof(SqlFunctions).GetMethod("StringConvert", new Type[] { typeof(double?) });

            var publicProperties =
                typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                          .Where(p => p.PropertyType == typeof(string) || p.PropertyType == typeof(int));

            List<Expression> orExpressionList = new List<Expression>();
            Expression Expressions = null;
            string verification = null;
            int temp = 0;
            string[] searchKeyParts;

            if (searchKey == null)
            {
                searchKey = "0";
            }
            searchKeyParts = !exactMatch ? searchKey.Split(' ') : new[] { searchKey };




            foreach (var callContainsMethod in from searchKeyPart in searchKeyParts
                                               let searchKeyExpression = Expression.Constant(searchKeyPart)
                                               let containsParamConverted = Expression.Convert(searchKeyExpression, typeof(string))
                                               select containsParamConverted into g
                                               from property in publicProperties
                                               let nameProperty = Expression.Property(parameter, property)
                                               select new
                                               {
                                                   expression =
                                                      nameProperty.Type.Name == "String" ? Expression.Call(nameProperty, containsMethod, (Expression)g)
                                                        : Expression.Call(Expression.Call(Expression.Call(stringConvertMethodInfo, Expression.Convert(
                                                                                  nameProperty, typeof(double?))), trimMethod),
                                                                              equalsMethod, (Expression)g)
                                               })
            {


                if (Expressions == null)
                {
                    Expressions = callContainsMethod.expression;
                }
                else
                {
                    if (searchKeyParts.Length == 1)
                    {
                        Expressions = Expression.Or(Expressions, callContainsMethod.expression);
                        continue;
                    }

                    if (verification == callContainsMethod.expression.Arguments[0].ToString() || temp == 0)
                    {
                        Expressions = Expression.Or(Expressions, callContainsMethod.expression);
                        verification = callContainsMethod.expression.Arguments[0].ToString();
                        temp++;
                        continue;
                    }
                    orExpressionList.Add(Expressions);
                    Expressions = callContainsMethod.expression;
                    verification = callContainsMethod.expression.Arguments[0].ToString();
                }

            }

            if (orExpressionList.Count > 0)
            {
                orExpressionList.Add(Expressions);

                for (int i = 0; i < orExpressionList.Count; i++)
                {
                    if (i == 0)
                    {
                        Expressions = orExpressionList[i];
                        continue;
                    }
                    Expressions = Expression.And(Expressions, orExpressionList[i]);
                }
            }
            MethodCallExpression whereCallExpression = Expression.Call(
                typeof(Queryable),
                "Where",
                new Type[] { queryable.ElementType },
                queryable.Expression,
                Expression.Lambda<Func<T, bool>>(Expressions, new ParameterExpression[] { parameter }));





            return queryable.Provider.CreateQuery<T>(whereCallExpression);
        }
        private static MethodInfo orderbyInfo = null;
        private static MethodInfo orderbyDecInfo = null;

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string property) where T : class
        {
            if (property == null)
                return null;
            Type entityType = typeof(T);
            Type entityPropertyType = entityType.GetProperty(property).PropertyType;
            var orderPara = Expression.Parameter(entityType, "o");
            var orderExpr = Expression.Lambda(Expression.Property(orderPara, property), orderPara);

            if (orderbyInfo == null)
            {
                //因為呼叫OrderBy需要知道型別，不知道的情況下無法直接呼叫，所以用反射的方式呼叫
                //泛型的GetMethod很難，所以用GetMethods在用Linq取出Method，找到後快取。
                orderbyInfo = typeof(Queryable).GetMethods().Single(x => x.Name == "OrderBy" && x.GetParameters().Length == 2);
            }

            //因為是泛型Mehtod要呼叫MakeGenericMethod決定泛型型別
            return orderbyInfo.MakeGenericMethod(new Type[] { entityType, entityPropertyType }).Invoke(null, new object[] { query, orderExpr }) as IQueryable<T>;
        }

        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> query, string property)
        {
            if (property == null)
                return null;

            Type entityType = typeof(T);
            Type entityPropertyType = entityType.GetProperty(property).PropertyType;

            var orderPara = Expression.Parameter(entityType, "o");
            var orderExpr = Expression.Lambda(Expression.Property(orderPara, property), orderPara);

            if (orderbyDecInfo == null)
            {
                orderbyDecInfo = typeof(Queryable).GetMethods().Single(x => x.Name == "OrderByDescending" && x.GetParameters().Length == 2);
            }

            return orderbyDecInfo.MakeGenericMethod(new Type[] { entityType, entityPropertyType }).Invoke(null, new object[] { query, orderExpr }) as IQueryable<T>;
        }


    }
}
