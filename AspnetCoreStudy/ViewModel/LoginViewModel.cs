using System.ComponentModel.DataAnnotations;

namespace AspnetCoreStudy.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "사용자 ID을 입력하세요.")] //  Not Null 설정
        public string UserId { get; set; }

        [Required(ErrorMessage = "사용자 비밀번호를 입력하세요.")] //  Not Null 설정
        public string UserPassword { get; set; }
    }
}
