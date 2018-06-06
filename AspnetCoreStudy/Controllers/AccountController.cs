using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreStudy.DataContext;
using AspnetCoreStudy.Models;
using AspnetCoreStudy.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreStudy.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 로그인
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        ////사용하면 안되는 방법
        //[HttpGet]
        //[Route("Http://www.example.com/login/id/password")]
        //public IActionResult Login(string id, string password)
        //{
        //    return View();
        //}


        /// <summary>
        /// 로그인 전송
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            // ID, 비밀번호 - 필수
            if (ModelState.IsValid)
            {
                using (var db = new AspnetNoteDbContext())
                {
                    // Linq - 메서드 체이닝
                    // => : A Go to B 란 의미
                    //var user = db.Users.FirstOrDefault(u=>u.UserId == model.UserId && u.UserPassword == model.UserPassword);
                    var user = db.Users.FirstOrDefault(u => u.UserId.Equals(model.UserId) && u.UserPassword.Equals(model.UserPassword));

                    if(user != null)
                    {
                        //HttpContext.Session.SetInt32(key, value);
                        HttpContext.Session.SetInt32("USER_LOGIN_KEY", user.UserNo);

                        // 로그인에 성공했을 때
                        return RedirectToAction("LoginSucess", "Home"); //로그인 성공 페이지로 이동
                    }

                }
                // 로그인에 실패했을 때
                // 사용자 ID 자체가 회원가입 x 경우
                ModelState.AddModelError(string.Empty, "사용자 ID 혹은 비밀번호가 올바르지 않습니다.");

                // 사용자 ID 자체가 회원가입 x 경우
                //ModelState.AddModelError(string.Empty, "사용자 ID가 존재하지 않습니다.");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // 특정 세션만 삭제
            HttpContext.Session.Remove("USER_LOGIN_KEY");

            //// 세션 전체 삭제
            //HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home"); 
        }


        /// <summary>
        /// 회원 가입
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }


        /// <summary>
        /// 회원 가입 전송
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(User model)
        {


            // Check validation

            // C#  Java try(SqlSession){} catch(){}
            if (ModelState.IsValid)
            {
                using (var db = new AspnetNoteDbContext())
                {
                    // 메모리 상에 올라감
                    db.Users.Add(model);

                    // sql로 저장
                    db.SaveChanges();
                }
                // 돌아가는 위치
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

    }
}