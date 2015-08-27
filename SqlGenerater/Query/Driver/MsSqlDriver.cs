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
using SqlGenerater.Parser.Parts;
using SqlGenerater.Query.Parts;

namespace SqlGenerater.Query.Driver
{
    public class MsSqlDriver : AbstractSqlDriver
    {
        public override Select CreateSelect(Type type)
        {
            return new SelectQueryPart(CreateAlias(type), type);
        }

        public override Select CreateSelect(Select @select, Type type)
        {
            return new SelectQueryPart(@select, CreateAlias(type), type);
        }

        public override TableBase CreateTable(string name, Type type)
        {
            return new TableQueryPart(name, CreateAlias(type), type);
        }

        public override TableBase CreateTable(Select @select, Type type)
        {
            return new TableQueryPart(@select, CreateAlias(type), type);
        }

        public override LeftJoin CreateLeftJoin(TableBase table, Type type)
        {
            return new LeftJoin(table);
        }

        public override RightJoin CreateRightJoin(TableBase table, Type type)
        {
            return new RightJoin(table);
        }
    }
}
