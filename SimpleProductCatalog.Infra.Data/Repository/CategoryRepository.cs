﻿using Microsoft.EntityFrameworkCore;
using SimpleProductCatalog.Abstraction.Repository;
using SimpleProductCatalog.Domain.Entities;
using SimpleProductCatalog.Infra.Data.DBContext;
using SimpleProductCatalog.Infra.Data.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleProductCatalog.Infra.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(SimpleProductCatalogDBContext context) : base(context)
        {
        }
    }
}
