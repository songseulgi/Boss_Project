﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IQuesMasterRepository : IRepository<QuesMaster>
    {
        Task<IList<QuesMaster>> GetQuesMastersAsync(Expression<Func<QuesMaster, bool>> where);
        Task<QuesMaster> GetQuesMasterAsync(Expression<Func<QuesMaster, bool>> where);
        QuesMaster Insert(QuesMaster quesMaster);
        Task<QuesMaster> GetQuesCompInfoAsync(Expression<Func<QuesMaster, bool>> where);
        Task<QuesMaster> GetQuesCompExtentionAsync(Expression<Func<QuesMaster, bool>> where);
        Task<QuesMaster> GetQuesResult1Async(Expression<Func<QuesMaster, bool>> where);
        Task<QuesMaster> GetQuesResult2Async(Expression<Func<QuesMaster, bool>> where);

        Task<QuesMaster> GetQuesOgranAnalysisAsync(Expression<Func<QuesMaster, bool>> where);
    }


    public class QuesMasterRepository : RepositoryBase<QuesMaster>, IQuesMasterRepository
    {
        public QuesMasterRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<IList<QuesMaster>> GetQuesMastersAsync(Expression<Func<QuesMaster, bool>> where)
        {
            return await DbContext.QuesMasters.Include("QuesWriter").Where(where).ToListAsync();
        }


        public async Task<QuesMaster> GetQuesMasterAsync(Expression<Func<QuesMaster, bool>> where)
        {
            return await DbContext.QuesMasters.Include("QuesWriter").Where(where).SingleOrDefaultAsync();
        }

        public async Task<QuesMaster> GetQuesCompInfoAsync(Expression<Func<QuesMaster, bool>> where)
        {
            return await DbContext.QuesMasters.Include("QuesCompInfo").Where(where).SingleOrDefaultAsync();
        }

        public async Task<QuesMaster> GetQuesCompExtentionAsync(Expression<Func<QuesMaster, bool>> where)
        {
            return await DbContext.QuesMasters.Include("QuesCompExtention").Where(where).SingleOrDefaultAsync();
        }

        public async Task<QuesMaster> GetQuesResult1Async(Expression<Func<QuesMaster, bool>> where)
        {
            return await DbContext.QuesMasters.Include("QuesResult1").Where(where).SingleOrDefaultAsync();
        }

        public async Task<QuesMaster> GetQuesResult2Async(Expression<Func<QuesMaster, bool>> where)
        {
            return await DbContext.QuesMasters.Include("QuesResult2").Where(where).SingleOrDefaultAsync();
        }

        public async Task<QuesMaster> GetQuesOgranAnalysisAsync(Expression<Func<QuesMaster, bool>> where)
        {
            return await DbContext.QuesMasters.Include("QuesOgranAnalysis").Where(where).SingleOrDefaultAsync();
        }

        public QuesMaster Insert(QuesMaster quesMaster)
        {
            return DbContext.QuesMasters.Add(quesMaster);
        }
    }
}