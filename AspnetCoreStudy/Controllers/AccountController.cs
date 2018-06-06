using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreStudy.DataContext;
using AspnetCoreStudy.Models;
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

        /// <summary>
        /// 회원 가입
        /// </summary>
        /// <returns></returns>

        public IActionResult Register()
        {
            return View();
        }

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