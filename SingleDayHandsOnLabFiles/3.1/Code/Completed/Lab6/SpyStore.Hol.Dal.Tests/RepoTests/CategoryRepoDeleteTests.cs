﻿// Copyright Information
// ==================================
// SpyStore.Hol - SpyStore.Hol.Dal.Tests - CategoryRepoDeleteTests.cs
// All samples copyright Philip Japikse
// http://www.skimedic.com 2020/03/07
// See License.txt for more information
// ==================================

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SpyStore.Hol.Dal.EfStructures;
using SpyStore.Hol.Dal.Repos;
using SpyStore.Hol.Dal.Repos.Interfaces;
using SpyStore.Hol.Dal.Tests.RepoTests.Base;
using SpyStore.Hol.Models.Entities;
using Xunit;

namespace SpyStore.Hol.Dal.Tests.RepoTests
{
    [Collection("SpyStore.DAL")]
    public class CategoryRepoDeleteTests : RepoTestsBase
    {
        public CategoryRepoDeleteTests()
        {
            _repo = new CategoryRepo(Db);
        }

        private readonly ICategoryRepo _repo;

        public override void Dispose()
        {
            _repo.Dispose();
        }

        [Fact]
        public void ShouldDeleteACategoryEntityAndNotPersist()
        {
            _repo.AddRange(new List<Category>
            {
                new Category {CategoryName = "Foo"},
            });
            Assert.Equal(1, _repo.Table.Count());
            var category = _repo.GetAll().First();
            var count = _repo.Delete(category, false);
            Assert.Equal(0, count);
            Assert.Equal(1, _repo.Table.Count());
        }

        [Fact]
        public void ShouldDeleteACategoryEntityFromContext()
        {
            _repo.AddRange(new List<Category>
            {
                new Category {CategoryName = "Foo"},
            });
            Assert.Equal(1, _repo.Table.Count());
            var category = _repo.GetAll().First();
            _repo.Context.Remove(category);
            var count = _repo.SaveChanges();
            Assert.Equal(1, count);
            Assert.Equal(0, _repo.Table.Count());
        }

        [Fact]
        public void ShouldDeleteACategoryEntityFromDbSet()
        {
            _repo.AddRange(new List<Category>
            {
                new Category {CategoryName = "Foo"},
            });
            Assert.Equal(1, _repo.Table.Count());
            var category = _repo.GetAll().First();
            var count = _repo.Delete(category);
            Assert.Equal(1, count);
            Assert.Equal(0, _repo.Table.Count());
        }

        [Fact]
        public void ShouldDeleteACategoryFromDifferentContext()
        {
            var categories = new List<Category>
            {
                new Category {CategoryName = "Foo"},
            };
            _repo.AddRange(categories);
            Assert.Equal(1, _repo.Table.Count());
            var category = _repo.Table.First();
            using (var context = new StoreContextFactory().CreateDbContext(null))
            {
                using (CategoryRepo repo = new CategoryRepo(context))
                {
                    var catToDelete = new Category {Id = category.Id, TimeStamp = category.TimeStamp};
                    var count = repo.Delete(catToDelete, false);
                    Assert.Equal(0, count);
                    count = repo.Context.SaveChanges();
                    Assert.Equal(1, count);
                    Assert.Equal(0, repo.Table.Count());
                }
            }
        }

        [Fact]
        public void ShouldDeleteACategoryFromSameContext()
        {
            var category = new Category {CategoryName = "Foo"};
            _repo.Add(category);
            Assert.Equal(1, _repo.Table.Count());
            var count = _repo.Delete(category, false);
            Assert.Equal(0, count);
            count = _repo.SaveChanges();
            Assert.Equal(1, count);
            Assert.Equal(0, _repo.Table.Count());
        }

        [Fact]
        public void ShouldDeleteACategoryRangeAndPersistManuallyFromDbSet()
        {
            var categories = new List<Category>
            {
                new Category {CategoryName = "Foo"},
                new Category {CategoryName = "Bar"},
                new Category {CategoryName = "FooBar"}
            };
            _repo.AddRange(categories);
            Assert.Equal(3, _repo.Table.Count());
            var count = _repo.DeleteRange(categories, false);
            Assert.Equal(0, count);
            count = _repo.SaveChanges();
            Assert.Equal(3, count);
            Assert.Equal(0, _repo.Table.Count());
        }

        [Fact]
        public void ShouldDeleteACategoryRangeFromDbSet()
        {
            var categories = new List<Category>
            {
                new Category {CategoryName = "Foo"},
                new Category {CategoryName = "Bar"},
                new Category {CategoryName = "FooBar"}
            };
            _repo.AddRange(categories);
            Assert.Equal(3, _repo.Table.Count());
            var count = _repo.DeleteRange(categories);
            Assert.Equal(3, count);
            Assert.Equal(0, _repo.Table.Count());
        }
    }
}