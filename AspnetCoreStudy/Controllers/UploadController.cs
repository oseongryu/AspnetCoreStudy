using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreStudy.Controllers
{
    public class UploadController : Controller
    {
        private readonly IHostingEnvironment _environment;

        public UploadController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        // http://www.example.com/Upload/ImageUpload
        // http://www.example.com/api/upload
        [HttpPost, Route("api/upload")]
        public async Task<IActionResult> ImageUpload(IFormFile file)
        {
            // # 이미지나 파일을 업로드할 때 필요한 구성
            // 1. Path(경로) - 어디에다 저장할 지 결정
            //var path = Path.Combine("1/", "2/", "3/", "4/");  // 1/2/3/4/ 

            var path = Path.Combine(_environment.WebRootPath,@"images\upload");
            // 2. Name(이름) - DateTime, GUID + GUID
            // 3. Extension(확장자) - jpg, png... txt

            // 파일 이름 image.jpg
            var fileFullName = file.FileName.Split('.');

            var fileName = $"{Guid.NewGuid()}.{fileFullName[1]}";

            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                //file.CopyTo(fileStream);
                await file.CopyToAsync(fileStream);
            }
            
            return Ok(new { file = "/images/upload/" + fileName, success = true });


            // # URL 접근 방식
            // ASP.NET - 호스트명/api/upload
            // JavaScript - 호스트명 + api/upload  => http://www.example.comapi/upload
            // JavaScript - 호스트명 +  / + api/upload => http://www.example.com/api/upload
        }
    }
}