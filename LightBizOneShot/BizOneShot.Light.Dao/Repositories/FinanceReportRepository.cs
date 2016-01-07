﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.DareModels;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IFinanceReportRepository : IRepository<SHUSER_SboMonthlySalesSelectReturnModel>
    {
        //현금시재조회
        Task<IList<SHUSER_SboMonthlyCashSelectReturnModel>> GetMonthlyCashListAsync(object[] parameters);
        //매출조회 당월, 전월
        Task<IList<SHUSER_SboMonthlySalesSelectReturnModel>> GetMonthlySalesAsync(object[] parameters);
        //매출조회 년 누적
        Task<SHUSER_SboMonthlyYearSalesSelectReturnModel> GetYearTotalSalesAsync(object[] parameters);
        //이익분석
        Task<IList<SHUSER_SboMonthlyCostAnalysisSelectReturnModel>> GetCostAnalysisAsync(object[] parameters);

        //비용분석
        Task<IList<SHUSER_SboMonthlyExpenseCostSelectReturnModel>> GetExpenseCostAsync(object[] parameters);

        //주요매출
        Task<IList<SHUSER_SboMonthlyTaxSalesSelectReturnModel>> GetTaxSalesAsync(object[] parameters);
        //주요지출
        Task<IList<SHUSER_SboMonthlyBankOutSelectReturnModel>> GetBankOutAsync(object[] parameters);

        Task<SHUSER_SboFinancialTab1SalesSelectReturnModel> GetCompanyMonthSalesAsync(object[] parameters);
        Task<SHUSER_SboFinancialTab2SalesSelectReturnModel> GetCompanyQuarterSalesAsync(object[] parameters);
        Task<SHUSER_SboFinancialTab3SalesSelectReturnModel> GetCompanyYearSalesAsync(object[] parameters);
    }


    public class FinanceReportRepository : DareRepositoryBase<SHUSER_SboMonthlySalesSelectReturnModel>,
        IFinanceReportRepository
    {
        public FinanceReportRepository(IDareDbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<IList<SHUSER_SboMonthlyCashSelectReturnModel>> GetMonthlyCashListAsync(object[] parameters)
        {
            return
                await
                    DareDbContext.Database.SqlQuery<SHUSER_SboMonthlyCashSelectReturnModel>(
                        "SBO_MONTHLY_CASH_SELECT @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @SET_YEAR, @SET_MONTH ",
                        parameters).ToListAsync();
        }

        public async Task<IList<SHUSER_SboMonthlySalesSelectReturnModel>> GetMonthlySalesAsync(object[] parameters)
        {
            return
                await
                    DareDbContext.Database.SqlQuery<SHUSER_SboMonthlySalesSelectReturnModel>(
                        "SBO_MONTHLY_SALES_SELECT @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @SET_YEAR, @SET_MONTH ",
                        parameters).ToListAsync();
        }

        public async Task<SHUSER_SboMonthlyYearSalesSelectReturnModel> GetYearTotalSalesAsync(object[] parameters)
        {
            return
                await
                    DareDbContext.Database.SqlQuery<SHUSER_SboMonthlyYearSalesSelectReturnModel>(
                        "SBO_YEAR_SALES_SELECT @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @SET_YEAR, @SET_MONTH ",
                        parameters).SingleOrDefaultAsync();
        }

        public async Task<IList<SHUSER_SboMonthlyCostAnalysisSelectReturnModel>> GetCostAnalysisAsync(
            object[] parameters)
        {
            return
                await
                    DareDbContext.Database.SqlQuery<SHUSER_SboMonthlyCostAnalysisSelectReturnModel>(
                        "SBO_MONTHLY_COST_ANALYSIS_SELECT @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @SET_YEAR, @SET_MONTH ",
                        parameters).ToListAsync();
        }

        public async Task<IList<SHUSER_SboMonthlyExpenseCostSelectReturnModel>> GetExpenseCostAsync(object[] parameters)
        {
            return
                await
                    DareDbContext.Database.SqlQuery<SHUSER_SboMonthlyExpenseCostSelectReturnModel>(
                        "SBO_MONTHLY_EXPENSE_COST_SELECT @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @SET_YEAR, @SET_MONTH ",
                        parameters).ToListAsync();
        }

        public async Task<IList<SHUSER_SboMonthlyTaxSalesSelectReturnModel>> GetTaxSalesAsync(object[] parameters)
        {
            return
                await
                    DareDbContext.Database.SqlQuery<SHUSER_SboMonthlyTaxSalesSelectReturnModel>(
                        "SBO_MONTHLY_TAX_SALES_SELECT @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @SET_YEAR, @SET_MONTH ",
                        parameters).ToListAsync();
        }

        public async Task<IList<SHUSER_SboMonthlyBankOutSelectReturnModel>> GetBankOutAsync(object[] parameters)
        {
            return
                await
                    DareDbContext.Database.SqlQuery<SHUSER_SboMonthlyBankOutSelectReturnModel>(
                        "SBO_MONTHLY_BANK_OUT_SELECT @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @SET_YEAR, @SET_MONTH ",
                        parameters).ToListAsync();
        }

        public async Task<SHUSER_SboFinancialTab1SalesSelectReturnModel> GetCompanyMonthSalesAsync(object[] parameters)
        { 
            return await DareDbContext.Database.SqlQuery<SHUSER_SboFinancialTab1SalesSelectReturnModel>("SBO_FINANCIAL_TAB1_SALES_SELECT @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @FR_YM, @TO_YM, @BASE_DT", parameters).SingleOrDefaultAsync();
        }

        public async Task<SHUSER_SboFinancialTab2SalesSelectReturnModel> GetCompanyQuarterSalesAsync(object[] parameters)
        {
            return await DareDbContext.Database.SqlQuery<SHUSER_SboFinancialTab2SalesSelectReturnModel>("SBO_FINANCIAL_TAB2_SALES_SELECT @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @FR_YEAR, @FR_QT, @TO_YEAR, @TO_QT, @BASE_DT", parameters).SingleOrDefaultAsync();
        }

        public async Task<SHUSER_SboFinancialTab3SalesSelectReturnModel> GetCompanyYearSalesAsync(object[] parameters)
        {
            return await DareDbContext.Database.SqlQuery<SHUSER_SboFinancialTab3SalesSelectReturnModel>("SBO_FINANCIAL_TAB3_SALES_SELECT @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @FR_YEAR, @TO_YEAR, @BASE_DT", parameters).SingleOrDefaultAsync();
        }
    }
}