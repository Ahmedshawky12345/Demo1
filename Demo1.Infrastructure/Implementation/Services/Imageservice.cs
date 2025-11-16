using Demo1.Application.Interfaces.Iservices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.Infrastructure.Implementation.Services
{
    public class Imageservice : IimageService
    {
        public async Task<string> SaveFileAsync(IFormFile file, string FolderName)
        {
            // 1️⃣ نتحقق إن في صورة جاية
            if (file == null || file.Length == 0)
                throw new Exception("No file uploaded.");

            // 2️⃣ نحدد المسار اللي هنحفظ فيه الصورة (wwwroot/images/books مثلاً)
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", FolderName);

            // 3️⃣ نعمل المجلد لو مش موجود
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            // 4️⃣ نولد اسم فريد للصورة عشان ما يحصلش تكرار
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // 5️⃣ نحدد المسار الكامل للملف
            var filePath = Path.Combine(uploadFolder, uniqueFileName);

            // 6️⃣ ننسخ الصورة فعليًا للمسار
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 7️⃣ نرجع المسار اللي نقدر نخزنه في قاعدة البيانات
            var relativePath = $"/images/{FolderName}/{uniqueFileName}";
            return relativePath;
        }
    }

    }

