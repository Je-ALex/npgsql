// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Framework.DependencyInjection;
using Npgsql.EntityFramework7;

namespace Npgsql.EntityFramework7.FunctionalTests
{
    public class TestNpgsqlModelSource : NpgsqlModelSource
    {
        private readonly TestModelSource _testModelSource;

        public TestNpgsqlModelSource(Action<ModelBuilder> onModelCreating, IDbSetFinder setFinder, IModelValidator modelValidator)
            : base(setFinder, modelValidator)
        {
            _testModelSource = new TestModelSource(onModelCreating, setFinder);
        }

        public override IModel GetModel(DbContext context, IModelBuilderFactory modelBuilderFactory) 
            => _testModelSource.GetModel(context, modelBuilderFactory);

        public static Func<IServiceProvider, INpgsqlModelSource> GetFactory(Action<ModelBuilder> onModelCreating) 
            => p => new TestNpgsqlModelSource(onModelCreating, p.GetRequiredService<IDbSetFinder>(), p.GetRequiredService<IModelValidator>());
    }
}