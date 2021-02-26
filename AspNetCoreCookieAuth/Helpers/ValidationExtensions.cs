using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspNetCoreCookieAuth.Helpers
{
    /// <summary>
    /// HTML ヘルパーにバリデーション用の拡張メソッドを追加するためのクラス
    /// </summary>
    public static class ValidationExtensions
    {
        /// <summary>
        /// ビューモデルのフィールドにエラーが含まれている場合に true を返すメソッド
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static bool hasValidationError<TModel, TProperty>(this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            var name = memberExpression.Member.Name;
            return helper.ViewData.ModelState[name] != null && helper.ViewData.ModelState[name].Errors.Any();
        }
    }
}
