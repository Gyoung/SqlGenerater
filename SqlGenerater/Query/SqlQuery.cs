﻿/**
 * Copyright (c) 2015, Harry CU 邱允根 (292350862@qq.com).
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Linq;
using System.Linq.Expressions;
using SqlGenerater.Parser.Parts;
using SqlGenerater.Query.Driver;
using SqlGenerater.Query.Expressions;

namespace SqlGenerater.Query
{
    public class SqlQuery<TModel> : SqlQueryBase, ISqlQuery<TModel>
    {
        public SqlQuery()
            : this(new MsSqlDriver())
        {

        }

        public SqlQuery(ISqlDriver driver)
            : base(driver)
        {
        }

        private SqlQuery(SqlQueryBase query)
            : base(query.Driver)
        {
            ParentSelectPart = query.SelectPart;
            SelectPart = ParentSelectPart;
        }

        private Select CreateSelect<TResult>(Expression<Func<TModel, TResult>> expression)
        {
            if (ParentSelectPart != null)
                SelectPart = Driver.CreateSelect(ParentSelectPart, typeof(TModel));
            return Translater.Translate<Select>(Driver, SelectPart, expression);
        }

        public ISqlQuery<TResult> Select<TResult>(Expression<Func<TModel, TResult>> expression)
        {
            SelectPart = CreateSelect(expression);
            return new SqlQuery<TResult>(this);
        }

        public ISqlQuery<TModel> Where(Expression<Func<TModel, bool>> expression)
        {
            SelectPart = CreateSelect(m => m);
            SelectPart.Where = Translater.Translate<Where>(Driver, SelectPart, expression);
            return new SqlQuery<TModel>(this);
        }

        public string GetQueryString()
        {
            if (SelectPart == null)
                return null;

            var visitor = new SqlQueryVisitor();
            visitor.Visit(SelectPart);
            return visitor.ToString();
        }
    }
}
