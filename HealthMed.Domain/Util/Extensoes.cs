using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Domain.Util
{
    public static class Extensoes
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        public static bool NullOrEmpty(this string objeto)
        {
            return string.IsNullOrWhiteSpace(objeto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool Nulo<TSource>(this IEnumerable<TSource> source)
        {
            return source == null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool Vazio<TSource>(this IEnumerable<TSource> source)
        {
            return source.Nulo() || !source.Any();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool NullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source.Nulo() || source.Vazio();
        }

        public static TResult RetornarEnum<TResult>(this IComparable valor)
        {
            if (typeof(TResult).BaseType != typeof(Enum))
            {
                throw new Exception("O tipo informado não é do tipo 'enum'.");
            }

            try
            {
                IEnumerable<TResult> source = Enum.GetValues(typeof(TResult)).OfType<TResult>();
                Func<TResult, bool> predicate = null;
                if (valor == null)
                {
                    return source.FirstOrDefault();
                }

                if (valor.GetType().Equals(typeof(string)) && valor.ToString().Length > 1)
                {
                    predicate = (TResult item) => item.ToString().Equals(valor);
                }
                else if (valor.GetType().Equals(typeof(char)) || (valor.GetType().Equals(typeof(string)) && valor.ToString().Length.Equals(1)))
                {
                    predicate = (TResult item) => Convert.ToInt32(item).ToString().Equals(valor.ToString());
                }
                else if (valor.GetType().Equals(typeof(int)))
                {
                    predicate = (TResult item) => Convert.ToInt32(item).Equals(Convert.ToInt32(valor));
                }

                return source.Where(predicate).FirstOrDefault();
            }
            catch
            {
                return default(TResult);
            }
        }

    }
}
