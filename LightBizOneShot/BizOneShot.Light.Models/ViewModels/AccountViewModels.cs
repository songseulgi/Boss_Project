﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BizOneShot.Light.Models.ViewModels
{
    internal class AccountViewModels
    {
    }

    public class JoinCompanyViewModel
    {
        //회원정보
        [Required]
        [Display(Name = "아이디")]
        [MaxLength(20, ErrorMessage = "{0}는 최대 {1}자 입니다..")]
        [MinLength(6, ErrorMessage = "{0}는 {1}자 이상이어야 합니다.")]
        [RegularExpression("^[0-9]*[a-zA-Z]+[a-zA-Z0-9]*$", ErrorMessage = "아이디는 알파벳 또는 알파벳 숫자 조합으로만 사용 가능합니다.")]
        public string LoginId { get; set; } // LOGIN_ID (Primary key). 로그인식별자
        public int CompSn { get; set; } // COMP_SN. 기업식별자

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "비밀번호")]
        [MaxLength(12, ErrorMessage = "{0}는 최대 {1}자 입니다..")]
        [MinLength(8, ErrorMessage = "{0}는 {1}자 이상이어야 합니다.")]
        public string LoginPw { get; set; } // LOGIN_PW. 로그인비밀번호

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "비밀번호")]
        [MaxLength(12, ErrorMessage = "{0}는 최대 {1}자 입니다..")]
        [MinLength(8, ErrorMessage = "{0}는 {1}자 이상이어야 합니다.")]
        [Compare("LoginPw", ErrorMessage = "비밀번호와 확인 비밀번호가 일치하지 않습니다.")]
        public string ConfirmLoginPw { get; set; } // LOGIN_PW. 로그인비밀번호 확인
        public string Status { get; set; } // STATUS. 상태  N: 정상(Normal)  R: 탈퇴 요청(Retired)  D: 탈퇴 완료(Deleted)
        public string UsrType { get; set; }
        // USR_TYPE. 회원유형  C: 기업회원(Company)  M:멘토(Mentor)  B: 사업회원(Business)  P:전문가회원(Professonal)  S: SCP (SCP)
        public string UsrTypeDetail { get; set; }
        // USR_TYPE_DETAIL. A :  관리자(Admi)  O: 담당자(Operator)    T: 세무/회계사(Tax Accountant)  W: 노무  L:법무  P:특허  M:마케팅  F:자금  D:기술개발  Z:기타
        public string DbType { get; set; } // DB_TYPE. DB유형

        [Required]
        [Display(Name = "담당자명")]
        [MaxLength(40, ErrorMessage = "{0}은 최대 {1}자 입니다..")]
        public string Name { get; set; } // Name. 이름
        public string FaxNo1 { get; set; } // FAX_NO. Fax
        public string FaxNo2 { get; set; } // FAX_NO. Fax
        public string FaxNo3 { get; set; } // FAX_NO. Fax
        public string TelNo1 { get; set; } // TEL_NO. 전화번호
        [MaxLength(4, ErrorMessage = "{0}는 최대 {1}자 입니다..")]
        [Range(0, 9999, ErrorMessage = "올바른 전화번호를 입력하세요")]
        public string TelNo2 { get; set; } // TEL_NO. 전화번호
        [MaxLength(4, ErrorMessage = "{0}는 최대 {1}자 입니다..")]
        [Range(0, 9999, ErrorMessage = "올바른 전화번호를 입력하세요")]
        public string TelNo3 { get; set; } // TEL_NO. 전화번호
        public string MbNo1 { get; set; } // MB_NO. 휴대폰
        [MaxLength(4, ErrorMessage = "{0}는 최대 {1}자 입니다..")]
        [Range(0, 9999, ErrorMessage = "올바른 전화번호를 입력하세요")]
        public string MbNo2 { get; set; } // MB_NO. 휴대폰
        [MaxLength(4, ErrorMessage = "{0}는 최대 {1}자 입니다..")]
        [Range(0, 9999, ErrorMessage = "올바른 전화번호를 입력하세요")]
        public string MbNo3 { get; set; } // MB_NO. 휴대폰

        [Required]
        [Display(Name = "이메일 아이디")]
        public string Email1 { get; set; } // EMAIL. 이메일

        [Required]
        [Display(Name = "이메일 도메인")]
        [MaxLength(40, ErrorMessage = "{0}은 최대 {1}자 입니다..")]
        public string Email2 { get; set; } // EMAIL. 이메일
        public string PostNo { get; set; } // POST_NO. 우편번호
        public string Addr1 { get; set; } // ADDR_1. 주소1
        public string Addr2 { get; set; } // ADDR_2. 주소2
        public string AccountNo { get; set; } // ACCOUNT_NO. 계좌번호
        public string BankNm { get; set; } // BANK_NM. 계좌은행
        public string DeptNm { get; set; } // DEPT_NM. 소속부서명
        public string RegId { get; set; } // REG_ID. 등록자
        public DateTime? RegDt { get; set; } // REG_DT. 등록일시
        public string UpdId { get; set; } // UPD_ID. 수정자
        public DateTime? UpdDt { get; set; } // UPD_DT. 수정일시

        //기업정보
        public string ComStatus { get; set; } // STATUS. 상태  N: 정상(Normal)  R: 탈퇴 요청(Retired)  D: 탈퇴 완료(Deleted)
        public string ComCompType { get; set; }
        // COMP_TYPE. 사업자유형  I: 개인사업자(Individual Company)  C: 법인사업자(Corporate Company)    다시정의해야함

        [Required]
        [Display(Name = "사업자번호")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "숫자만 입력할 수 있습니다.")]
        [StringLength(10, ErrorMessage = "{0}는 {1}자 입니다.", MinimumLength = 10)]
        public string ComRegistrationNo { get; set; } // REGISTRATION_NO. 사업자등록번호

        [Required]
        [Display(Name = "회사명")]
        [MaxLength(35, ErrorMessage = "{0}은 최대 {1}자 입니다..")]
        public string CompNm { get; set; } // COMP_NM. 회사명
        public string ComEmail { get; set; } // EMAIL. 대표이메일주소
        public string ComTelNo1 { get; set; } // TEL_NO. 대표전화번호
        [MaxLength(4, ErrorMessage = "{0}는 최대 {1}자 입니다..")]
        [Range(0, 9999, ErrorMessage = "올바른 전화번호를 입력하세요")]
        public string ComTelNo2 { get; set; } // TEL_NO. 대표전화번호
        [MaxLength(4, ErrorMessage = "{0}는 최대 {1}자 입니다..")]
        [Range(0, 9999, ErrorMessage = "올바른 전화번호를 입력하세요")]
        public string ComTelNo3 { get; set; } // TEL_NO. 대표전화번호
        public string ComPostNo { get; set; } // POST_NO. 우편번호
        public string ComAddr1 { get; set; } // ADDR_1. 주소1
        public string ComAddr2 { get; set; } // ADDR_2. 주소2

        [Required]
        [Display(Name = "대표자명")]
        [MaxLength(40, ErrorMessage = "{0}은 최대 {1}자 입니다..")]
        public string ComOwnNm { get; set; } // OWN_NM. 대표자명
        public string ComRegId { get; set; } // REG_ID. 등록자
        public DateTime? ComRegDt { get; set; } // REG_DT. 등록일시
        public string ComUpdId { get; set; } // UPD_ID. 수정자
        public DateTime? ComUpdDt { get; set; } // UPD_DT. 수정일시

       //업종, 종목
        public IList<BizTypeViewModel> BizTypes { get; set; }

        //사업관리정보
        public int MngCompSn { get; set; } // COMP_SN. 기업식별자 (관리기관 식별자)
        public int BizWorkSn { get; set; } // BIZ_WORK_SN (Primary key). 사업식별자
    }


    public class LoginViewModel
    {
        [Required]
        [Display(Name = "아이디")]
        [MaxLength(20, ErrorMessage = "{0}는 최대 {1}자 입니다..")]
        [MinLength(6, ErrorMessage = "{0}는 {1}자 이상이어야 합니다.")]
        [RegularExpression("^[0-9]*[a-zA-Z]+[a-zA-Z0-9]*$", ErrorMessage = "아이디는 알파벳 또는 알파벳 숫자 조합으로만 사용 가능합니다.")]
        public string ID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "암호")]
        [MaxLength(12, ErrorMessage = "{0}는 최대 {1}자 입니다..")]
        [MinLength(8, ErrorMessage = "{0}는 {1}자 이상이어야 합니다.")]
        public string Password { get; set; }

        [Display(Name = "아이디 저장")]
        public bool RememberMe { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name = "아이디")]
        [MaxLength(20, ErrorMessage = "{0}는 최대 {1}자 입니다..")]
        [MinLength(6, ErrorMessage = "{0}는 {1}자 이상이어야 합니다.")]
        [RegularExpression("^[0-9]*[a-zA-Z]+[a-zA-Z0-9]*$", ErrorMessage = "아이디는 알파벳 또는 알파벳 숫자 조합으로만 사용 가능합니다.")]
        public string ID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "기존 비밀번호")]
        [MaxLength(12, ErrorMessage = "{0}는 최대 {1}자 입니다..")]
        [MinLength(8, ErrorMessage = "{0}는 {1}자 이상이어야 합니다.")]
        public string LoginPw { get; set; } // LOGIN_PW. 로그인비밀번호

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "새 비밀번호")]
        [MaxLength(12, ErrorMessage = "{0}는 최대 {1}자 입니다..")]
        [MinLength(8, ErrorMessage = "{0}는 {1}자 이상이어야 합니다.")]
        public string NewLoginPw { get; set; } // LOGIN_PW. 로그인비밀번호

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "새 비밀번호 확인")]
        [MaxLength(12, ErrorMessage = "{0}는 최대 {1}자 입니다..")]
        [MinLength(8, ErrorMessage = "{0}는 {1}자 이상이어야 합니다.")]
        [Compare("NewLoginPw", ErrorMessage = "비밀번호와 확인 비밀번호가 일치하지 않습니다.")]
        public string ConfirmNewLoginPw { get; set; } // LOGIN_PW. 로그인비밀번호 확인
    }

    [Serializable]
    [Flags]
    public enum UserType
    {
        Company, //기업회원
        Mentor, //멘토
        Expert, //전문가
        SysManager, //SCP 관리자
        BizManager //사업 관리자
    }


    public class CompanyMyInfoViewModel
    {
        //회원정보
        [Required]
        [Display(Name = "아이디")]
        [MaxLength(20, ErrorMessage = "{0}는 최대 {1}자 입니다..")]
        [MinLength(6, ErrorMessage = "{0}는 {1}자 이상이어야 합니다.")]
        [RegularExpression("^[0-9]*[a-zA-Z]+[a-zA-Z0-9]*$", ErrorMessage = "아이디는 알파벳 또는 알파벳 숫자 조합으로만 사용 가능합니다.")]
        public string LoginId { get; set; } // LOGIN_ID (Primary key). 로그인식별자

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "비밀번호")]
        [MaxLength(12, ErrorMessage = "{0}는 최대 {1}자 입니다..")]
        [MinLength(8, ErrorMessage = "{0}는 {1}자 이상이어야 합니다.")]
        public string LoginPw { get; set; } // LOGIN_PW. 로그인비밀번호

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "비밀번호")]
        [MaxLength(12, ErrorMessage = "{0}는 최대 {1}자 입니다..")]
        [MinLength(8, ErrorMessage = "{0}는 {1}자 이상이어야 합니다.")]
        [Compare("LoginPw", ErrorMessage = "비밀번호와 확인 비밀번호가 일치하지 않습니다.")]
        public string ConfirmLoginPw { get; set; } // LOGIN_PW. 로그인비밀번호 확인

        [Required]
        [Display(Name = "담당자명")]
        [MaxLength(40, ErrorMessage = "{0}은 최대 {1}자 입니다..")]
        public string Name { get; set; } // Name. 이름
        public string TelNo { get; set; } // TEL_NO. 전화번호
        public string TelNo1 { get; set; }

        [Range(0, 9999, ErrorMessage = "올바른 전화번호를 입력하세요")]
        public string TelNo2 { get; set; }

        [Range(0, 9999, ErrorMessage = "올바른 전화번호를 입력하세요")]
        public string TelNo3 { get; set; }

        public string MbNo { get; set; } // MB_NO. 휴대폰
        public string MbNo1 { get; set; }

        [Range(0, 9999, ErrorMessage = "올바른 전화번호를 입력하세요")]
        public string MbNo2 { get; set; }

        [Range(0, 9999, ErrorMessage = "올바른 전화번호를 입력하세요")]
        public string MbNo3 { get; set; }

        [Required]
        [Display(Name = "이메일")]
        [EmailAddress(ErrorMessage = "올바른 이메일주소를 입력하세요")]
        public string Email { get; set; } // EMAIL. 이메일
        public string Email1 { get; set; }
        public string Email2 { get; set; }

        public string UsrType { get; set; }
        // USR_TYPE. 회원유형  C: 기업회원(Company)  M:멘토(Mentor)  B: 사업회원(Business)  P:전문가회원(Professonal)  S: SCP (SCP)
        public string UsrTypeDetail { get; set; }
        // USR_TYPE_DETAIL. A :  관리자(Admi)  O: 담당자(Operator)    T: 세무/회계사(Tax Accountant)  W: 노무  
        public string UsrTypeName { get; set; }
        // USR_TYPE. 회원유형  C: 기업회원(Company)  M:멘토(Mentor)  B: 사업회원(Business)  P:전문가회원(Professonal)  S: SCP (SCP)
        public string UsrTypeDetailName { get; set; }
        // USR_TYPE_DETAIL. A :  관리자(Admi)  O: 담당자(Operator)    T: 세무/회계사(Tax Accountant)  W: 노무  
        //public string ResumeName { get; set; }  //이력서 파일명
        //public string ResumePath { get; set; }  //이력서 경로


        //멘토정보
        //public string MentorName { get; set; } // Name. 이름
        //public string MentorTelNo { get; set; } // TEL_NO. 전화번호
        //public string MentorMbNo { get; set; } // MB_NO. 휴대폰
        //public string MentorEmail { get; set; } // EMAIL. 이메일

        //기업정보
        [Required]
        [Display(Name = "사업자번호")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "숫자만 입력할 수 있습니다.")]
        [StringLength(10, ErrorMessage = "{0}는 {1}자 입니다.", MinimumLength = 10)]
        public string ComRegistrationNo { get; set; } // REGISTRATION_NO. 사업자등록번호

        [Required]
        [Display(Name = "회사명")]
        [MaxLength(35, ErrorMessage = "{0}은 최대 {1}자 입니다..")]
        public string CompNm { get; set; } // COMP_NM. 회사명

        public string ComTelNo { get; set; } // TEL_NO. 대표전화번호
        public string ComTelNo1 { get; set; }

        [Range(0, 9999, ErrorMessage = "올바른 전화번호를 입력하세요")]
        public string ComTelNo2 { get; set; }

        [Range(0, 9999, ErrorMessage = "올바른 전화번호를 입력하세요")]
        public string ComTelNo3 { get; set; }

        public string ComAddr { get; set; } // ADDR_1. 주소1
        public string ComPostNo { get; set; }
        public string ComAddr1 { get; set; }
        public string ComAddr2 { get; set; }

        [Required]
        [Display(Name = "대표자명")]
        [MaxLength(40, ErrorMessage = "{0}은 최대 {1}자 입니다..")]
        public string ComOwnNm { get; set; } // OWN_NM. 대표자명


        //업종/종목
        public IList<BizTypeViewModel> BizTypes { get; set; }

        //public string BizWorkName { get; set; } // BIZ_WORK_SN (Primary key). 사업식별자
        //public string MngCompName { get; set; } // COMP_SN. 기업식별자 (관리기관 식별자)

        ////사업관리정보
    }

    public class BizTypeViewModel
    {
        public string BizTypeCd { get; set; }
        public string BizTypeNm { get; set; }
    }

    public class ModifyCompanyParamModel
    {
        public string LoginIdChk { get; set; }
        public string LoginPwChk { get; set; }
    }
}