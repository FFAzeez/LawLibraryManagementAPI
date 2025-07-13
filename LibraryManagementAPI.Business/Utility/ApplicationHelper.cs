using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace LibraryManagementAPI.Business.Utility
{
    public static class ApplicationHelper
    {
        public static string GenerateCode(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                string chars = $"0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                ch = chars[random.Next(chars.Length)];
                builder.Append(ch);
            }
            return $"{DateTime.Now.ToString("ddMMyyHHmmss")}{builder.ToString()}".Substring(0, 20);
        }



        public static int get_age(DateTime dob)
        {
            int age = 0;
            age = DateTime.Now.Subtract(dob).Days;
            age = age / 365;
            return age;
        }
        
        public static string GetSHA512(this string source)
        {
            if (string.IsNullOrWhiteSpace(source)) return source;
            using (SHA512 sha512Hash = SHA512.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
                byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                return hash;
            }
        }
        
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var parameter = left.Parameters[0];
            var visitor = new SubstituteParameterVisitor(parameter);
            var body = Expression.AndAlso(visitor.Visit(left.Body), visitor.Visit(right.Body));
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        private class SubstituteParameterVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _parameter;

            public SubstituteParameterVisitor(ParameterExpression parameter)
            {
                _parameter = parameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return _parameter;
            }
        }
    }
}