namespace LHelper.Services.Implementation
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    class CategoryService : ICategoryService
    {
        private readonly LHelperDbContext db;

        public CategoryService(LHelperDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CategoryListingServiceModel>> AllAsyn()
         => await this.db
            .Categories
            .OrderByDescending(c => c.Id)
            .ProjectTo<CategoryListingServiceModel>()
            .ToListAsync();
    }
}
