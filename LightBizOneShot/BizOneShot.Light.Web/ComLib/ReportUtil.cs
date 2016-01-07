﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BizOneShot.Light.Models.DareModels;
using BizOneShot.Light.Services;
using BizOneShot.Light.Models.ViewModels;
using System.Configuration;

namespace BizOneShot.Light.Web.ComLib
{
    public class ReportUtil
    {
        private readonly IScBizWorkService scBizWorkService;
        private readonly IQuesResult1Service quesResult1Service;
        private readonly IQuesResult2Service quesResult2Service;
        private readonly IQuesMasterService quesMasterService;
        private readonly ISboFinancialIndexTService sboFinancialIndexTService;

        public ReportUtil(
            IScBizWorkService scBizWorkService,
            IQuesResult1Service quesResult1Service,
            IQuesResult2Service quesResult2Service,
            IQuesMasterService quesMasterService,
            ISboFinancialIndexTService sboFinancialIndexTService)
        {
            this.scBizWorkService = scBizWorkService;
            this.quesResult1Service = quesResult1Service;
            this.quesResult2Service = quesResult2Service;
            this.quesMasterService = quesMasterService;
            this.sboFinancialIndexTService = sboFinancialIndexTService;
        }



        public async Task<IList<CheckListViewModel>> getGrowthStepPointCheckList(BasicSurveyReportViewModel paramModel, string quesCheckListSmallClassCd)
        {
            //사업참여 기업들의 레벨(창업보육, 보육성장, 자립성장) 분류
            Dictionary<int, int> dicStartUp = new Dictionary<int, int>();
            Dictionary<int, int> dicGrowth = new Dictionary<int, int>();
            Dictionary<int, int> dicIndependent = new Dictionary<int, int>();

            var curBizWork = await scBizWorkService.GetBizWorkByBizWorkSn(paramModel.BizWorkSn);
            {
                var compMappings = curBizWork.ScCompMappings;
                foreach (var compMapping in compMappings)
                {
                    var quesMasters = await quesMasterService.GetQuesMasterAsync(compMapping.ScCompInfo.RegistrationNo, paramModel.BizWorkYear);
                    if (quesMasters == null)
                    {
                        continue;
                    }

                    //다래 재무정보 유무 체크하는 로직 추가해야함.(문진표정보, 재무정보가 있어야 보고서 생성가능.)
                    //다래 재무정보 조회해야 함.
                    var sboFinacialIndexT = await sboFinancialIndexTService.GetSHUSER_SboFinancialIndexT(compMapping.ScCompInfo.RegistrationNo, ConfigurationManager.AppSettings["CorpCode"], ConfigurationManager.AppSettings["BizCode"], paramModel.BizWorkYear.ToString());
                    if (sboFinacialIndexT == null)
                    {
                        continue;
                    }

                    //종합점수 조회하여 분류별로 딕셔너리 저장
                    var point = await GetCompanyTotalPoint(quesMasters.QuestionSn, sboFinacialIndexT);

                    if (point >= 0 && point <= 50)
                        dicStartUp.Add(compMapping.CompSn, quesMasters.QuestionSn);
                    else if (point > 50 && point <= 75)
                        dicGrowth.Add(compMapping.CompSn, quesMasters.QuestionSn);
                    else
                        dicIndependent.Add(compMapping.CompSn, quesMasters.QuestionSn);
                }
            }



            //리스트 데이터 생성
            var quesResult1s = await quesResult1Service.GetQuesResult1sAsync(paramModel.QuestionSn, quesCheckListSmallClassCd);

            int count = 1;
            var CheckList = new List<CheckListViewModel>();
            foreach (var item in quesResult1s)
            {
                CheckListViewModel checkListViewModel = new CheckListViewModel();
                checkListViewModel.Count = count.ToString();
                checkListViewModel.AnsVal = item.AnsVal.Value;
                checkListViewModel.DetailCd = item.QuesCheckList.DetailCd;
                checkListViewModel.Title = item.QuesCheckList.ReportTitle;
                //창업보육단계 평균
                int startUpCnt = await GetCheckListCnt(dicStartUp, checkListViewModel.DetailCd);
                checkListViewModel.StartUpAvg = Math.Round(((startUpCnt + item.QuesCheckList.StartUpStep.Value + 0.0) / (39 + dicStartUp.Count + dicGrowth.Count + dicIndependent.Count)) * 100, 0).ToString();
                //보육성장단계 평균
                int growthCnt = await GetCheckListCnt(dicGrowth, checkListViewModel.DetailCd);
                checkListViewModel.GrowthAvg = Math.Round(((growthCnt + item.QuesCheckList.GrowthStep.Value + 0.0) / (39 + dicStartUp.Count + dicGrowth.Count + dicIndependent.Count)) * 100, 0).ToString();
                //자립성장단계 평균
                int IndependentCnt = await GetCheckListCnt(dicIndependent, checkListViewModel.DetailCd);
                checkListViewModel.IndependentAvg = Math.Round(((IndependentCnt + item.QuesCheckList.IndependentStep.Value + 0.0) / (39 + dicStartUp.Count + dicGrowth.Count + dicIndependent.Count)) * 100, 0).ToString();
                //참여기업 평균
                checkListViewModel.BizInCompanyAvg = Math.Round(((IndependentCnt + growthCnt + startUpCnt + 0.0) / (dicStartUp.Count + dicGrowth.Count + dicIndependent.Count)) * 100, 0).ToString();
                //전체 평균
                checkListViewModel.TotalAvg = Math.Round(((IndependentCnt + growthCnt + startUpCnt + item.QuesCheckList.TotalStep.Value + 0.0) / (39 + dicStartUp.Count + dicGrowth.Count + dicIndependent.Count)) * 100, 0).ToString();
                CheckList.Add(checkListViewModel);
                count++;
            }

            return CheckList;
        }

        public async Task<double> GetCompanyTotalPoint(int qustionSn, SHUSER_SboFinancialIndexT sboFinancialIndexT)
        {
            double totalPoint = 0;

            totalPoint = totalPoint + await GetOverAllManagementTotalPoint(qustionSn);
            totalPoint = totalPoint + await GetTechMng(qustionSn, sboFinancialIndexT);
            totalPoint = totalPoint + await GetHumanResourceMng(qustionSn, sboFinancialIndexT);

            return totalPoint;
        }

        /// <summary>
        /// 경영일반
        /// </summary>
        /// <param name="questionSn"></param>
        /// <returns></returns>
        /// 
        public async Task<double> GetOverAllManagementTotalPoint(int questionSn)
        {
            double totalPoint = 0;
            //경영목표 및 전략
            // A1A101 : 경영목표 및 전략 코드
            var quesResult1sPurpose = await quesResult1Service.GetQuesResult1sAsync(questionSn, "A1A101");
            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeA(ReportHelper.CalcCheckCount(quesResult1sPurpose)), 0.5);


            // A1A102 : 경영자의 리더쉽 코드
            var quesResult1sLeadership = await quesResult1Service.GetQuesResult1sAsync(questionSn, "A1A102");
            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeA(ReportHelper.CalcCheckCount(quesResult1sLeadership)), 0.5);


            // 조직구성 조회
            var quesMaster = await quesMasterService.GetQuesOgranAnalysisAsync(questionSn);
            int officerCnt = 0;
            foreach (var item in quesMaster.QuesOgranAnalysis)
            {
                officerCnt = officerCnt + item.OfficerCount.Value;
            }
            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeB(officerCnt), 1);


            // A1A103 : 경영자의 신뢰성
            var quesResult1s = await quesResult1Service.GetQuesResult1sAsync(questionSn, "A1A103");
            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeC(ReportHelper.CalcCheckCount(quesResult1s)), 2);


            // A1A201 : 근로환경
            var quesResult1sWorkEnv = await quesResult1Service.GetQuesResult1sAsync(questionSn, "A1A201");
            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeA(ReportHelper.CalcCheckCount(quesResult1sWorkEnv)), 1);


            // A1A202 : 조직만족도
            var quesResult2s = await quesResult2Service.GetQuesResult2sAsync(questionSn, "A1A202");
            //총직원
            var totalEmp = quesResult2s.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1A20201");
            //이직직원
            var moveEmp = quesResult2s.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1A20202");

            if (totalEmp.D451 == "0")
            {
                double avg = (int.Parse(moveEmp.D) / int.Parse(totalEmp.D)) * 100;
                totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeD(avg), 3);

            }
            else if (totalEmp.D452 == "0")
            {
                double avg = ((int.Parse(moveEmp.D) / int.Parse(totalEmp.D)) + (int.Parse(moveEmp.D451) / int.Parse(totalEmp.D451))) / 2 * 100;
                totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeD(avg), 3);
            }
            else
            {
                double avg = ((int.Parse(moveEmp.D) / int.Parse(totalEmp.D)) + (int.Parse(moveEmp.D451) / int.Parse(totalEmp.D451)) + (int.Parse(moveEmp.D452) / int.Parse(totalEmp.D452))) / 3 * 100;
                totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeD(avg), 3);
            }


            // A1A203 : 정보시스템 활용
            var quesResult1sInfoSystem = await quesResult1Service.GetQuesResult1sAsync(questionSn, "A1A203");
            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeA(ReportHelper.CalcCheckCount(quesResult1sInfoSystem)), 2);

            return totalPoint;
        }

        /// <summary>
        /// 기술경영,마케팅
        /// </summary>
        public async Task<double> GetTechMng(int questionSn, SHUSER_SboFinancialIndexT sboFinancialIndexT)
        {
            double totalPoint = 0;

            // 연구개발 투자
            {
                double avg = Convert.ToDouble(sboFinancialIndexT.ReserchAmt / sboFinancialIndexT.CurrentSale) * 100;
                totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeM(avg), 2);
            }


            //연구개발 인력의 비율
            var quesResult2sEmpRate = await quesResult2Service.GetQuesResult2sAsync(questionSn, "A1B102");
            //전체임직원수
            var TotalEmp = quesResult2sEmpRate.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10202");
            //연구개발인력
            var RndEmp = quesResult2sEmpRate.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10201");
            if (int.Parse(TotalEmp.D) != 0)
            {
                double avg = (int.Parse(RndEmp.D) / int.Parse(TotalEmp.D)) * 100;
                totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeE(avg), 1);
            }


            //연구개발 인력의 능력
            var quesResult2sEmpCapa = await quesResult2Service.GetQuesResult2sAsync(questionSn, "A1B103");
            //박사급
            var DoctorEmp = quesResult2sEmpCapa.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10301");
            //석사급
            var MasterEmp = quesResult2sEmpCapa.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10302");
            //학사급
            var CollegeEmp = quesResult2sEmpCapa.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10303");
            //기능사급
            var TechEmp = quesResult2sEmpCapa.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10304");
            //고졸이하급
            var HighEmp = quesResult2sEmpCapa.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10305");
            if ((int.Parse(DoctorEmp.D) + int.Parse(MasterEmp.D) + int.Parse(CollegeEmp.D) + int.Parse(TechEmp.D) + int.Parse(HighEmp.D)) != 0)
            {
                double avg = (int.Parse(DoctorEmp.D) * 5) + (int.Parse(MasterEmp.D) * 4) + (int.Parse(CollegeEmp.D) * 3) + (int.Parse(TechEmp.D) * 2) + (int.Parse(HighEmp.D) * 1) / (int.Parse(DoctorEmp.D) + int.Parse(MasterEmp.D) + int.Parse(CollegeEmp.D) + int.Parse(TechEmp.D) + int.Parse(HighEmp.D));
                totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeF(avg), 3);
            }


            // A1B104 : 사업화역량
            var quesResult1sBizCapa = await quesResult1Service.GetQuesResult1sAsync(questionSn, "A1B104");
            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeC(ReportHelper.CalcCheckCount(quesResult1sBizCapa)), 5);


            // A1B105 : 사업화실적
            var quesResult2sBizResult = await quesResult2Service.GetQuesResult2sAsync(questionSn, "A1B105");
            //사업화실적 총 건수
            var BizResultCnt = quesResult2sBizResult.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1B10502");
            {
                double avg = int.Parse(BizResultCnt.D) + int.Parse(BizResultCnt.D451) + int.Parse(BizResultCnt.D452) / 3;
                totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeE(avg), 4);
            }


            // A1B106 : 생산설비의 운영체제 및 관리
            var quesResult1sFacMng = await quesResult1Service.GetQuesResult1sAsync(questionSn, "A1B106");
            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeA(ReportHelper.CalcCheckCount(quesResult1sFacMng)), 2);


            // A1B107 : 공정관리
            var quesResult1sProcess = await quesResult1Service.GetQuesResult1sAsync(questionSn, "A1B107");
            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeC(ReportHelper.CalcCheckCount(quesResult1sProcess)), 2);


            // A1B108 : 품질관리
            var quesResult1sQaMng = await quesResult1Service.GetQuesResult1sAsync(questionSn, "A1B108");
            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeC(ReportHelper.CalcCheckCount(quesResult1sQaMng)), 3);


            // A1C101 : 마케팅 전략의 수립 및 실행
            var quesResult1sMarketing = await quesResult1Service.GetQuesResult1sAsync(questionSn, "A1C101");
            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeG(ReportHelper.CalcCheckCount(quesResult1sMarketing)), 8);


            // A1C102 : 고객관리
            var quesResult1sCustMng = await quesResult1Service.GetQuesResult1sAsync(questionSn, "A1C102");
            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeA(ReportHelper.CalcCheckCount(quesResult1sCustMng)), 9);

            return totalPoint;
        }
        /// <summary>
        /// 인적자원관리
        /// </summary>
        /// <param name="questionSn"></param>
        /// <returns></returns>
        public async Task<double> GetHumanResourceMng(int questionSn, SHUSER_SboFinancialIndexT sboFinancialIndexT)
        {
            double totalPoint = 0;

            // A1D101 : 인적자윈의 확보와 개발관리
            var quesResult1sHrMng = await quesResult1Service.GetQuesResult1sAsync(questionSn, "A1D101");
            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeA(ReportHelper.CalcCheckCount(quesResult1sHrMng)), 11);

            // A1D102 : 이적자원의 보상 및 유지관리
            var quesResult1sMaintenance = await quesResult1Service.GetQuesResult1sAsync(questionSn, "A1D102");
            totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeA(ReportHelper.CalcCheckCount(quesResult1sMaintenance)), 8);

            //재무적성과
            // 다래 DB를 통한 계산
            totalPoint = totalPoint + ((ReportHelper.CalcFinancialPoint(sboFinancialIndexT) / 100) * 26);

            // A1E102 : 지적재산권성과
            var quesResult2sPatent = await quesResult2Service.GetQuesResult2sAsync(questionSn, "A1E102");
            //등록 특허
            var RegPatent = quesResult2sPatent.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1E10201");
            //등록 실용신안
            var RegUtilityModel = quesResult2sPatent.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1E10202");
            //출원 특허
            var ApplyPatent = quesResult2sPatent.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1E10203");
            //출원 실용신안
            var ApplyUtilityModel = quesResult2sPatent.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1E10204");
            //기타
            var Etc = quesResult2sPatent.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1E10205");
            {
                double avg = (int.Parse(RegPatent.D) * 3) + (int.Parse(ApplyPatent.D) * 2) + (int.Parse(RegUtilityModel.D) * 2) + int.Parse(ApplyUtilityModel.D) + int.Parse(Etc.D);
                totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeH(avg), 3);
            }

            // A1E103 : 임직원 수
            var quesResult2sTotalEmp = await quesResult2Service.GetQuesResult2sAsync(questionSn, "A1E103");
            //전체 임직원
            var TotalEmploy = quesResult2sTotalEmp.SingleOrDefault(i => i.QuesCheckList.DetailCd == "A1E10301");
            if (int.Parse(TotalEmploy.D451) != 0)
            {
                double avg = (int.Parse(TotalEmploy.D) / int.Parse(TotalEmploy.D451)) - 1;
                totalPoint = totalPoint + ReportHelper.CalcPoint(ReportHelper.GetCodeTypeI(avg), 3);
            }

            return totalPoint;
        }


        public async Task<int> GetCheckListCnt(Dictionary<int, int> checkList, string detailCd)
        {
            int count = 0;

            foreach (var item in checkList.Values)
            {
                var quesResult1 = await quesResult1Service.GetQuesResult1Async(item, detailCd);
                if (quesResult1 != null && quesResult1.AnsVal == true)
                    count++;
            }

            return count;
        }
    }
}