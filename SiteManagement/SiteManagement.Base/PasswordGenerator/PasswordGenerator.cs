using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Base;

public class PasswordGenerator
{
        public static string GeneratePassword()
        {
            // Büyük harf, küçük harf ve rakamların bulunduğu karakter dizileri
            string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            string numericChars = "0123456789";

            // Rastgele bir büyük harf seç
            char upperChar = upperChars[new Random().Next(0, upperChars.Length)];

            // Rastgele bir küçük harf seç
            char lowerChar = lowerChars[new Random().Next(0, lowerChars.Length)];

            // Rastgele bir rakam seç
            char numericChar = numericChars[new Random().Next(0, numericChars.Length)];

            // Şifreyi oluştur
            string password = $"{upperChar}{lowerChar}{numericChar}";

            // Şifreyi rastgele harf ve rakamlarla tamamla
            int passwordLength = 8;
            Random random = new Random();
            for (int i = 3; i < passwordLength; i++)
            {
                string chars = upperChars + lowerChars + numericChars;
                password += chars[random.Next(0, chars.Length)];
            }

            // Şifreyi karıştırarak sonuç döndür
            return ShufflePassword(password);
        }

        // Şifreyi karıştırarak güvenlik düzeyini artırma
        private static string ShufflePassword(string password)
        {
            char[] chars = password.ToCharArray();
            Random random = new Random();
            for (int i = 0; i < password.Length; i++)
            {
                int randomIndex = random.Next(i, password.Length);
                char temp = chars[i];
                chars[i] = chars[randomIndex];
                chars[randomIndex] = temp;
            }
            return new string(chars);
        }
    }

