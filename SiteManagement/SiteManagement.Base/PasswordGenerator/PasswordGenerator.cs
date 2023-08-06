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
           //Character strings with uppercase, lowercase, and numbers
            string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            string numericChars = "0123456789";

            // Pick a random uppercase letter
            char upperChar = upperChars[new Random().Next(0, upperChars.Length)];

            // Rastgele bir küçük harf seç
            char lowerChar = lowerChars[new Random().Next(0, lowerChars.Length)];

             // Pick a random lowercase letter
             char numericChar = numericChars[new Random().Next(0, numericChars.Length)];

            // create password
            string password = $"{upperChar}{lowerChar}{numericChar}";

            // Complete the password with random letters and numbers
             int passwordLength = 8;
            Random random = new Random();
            for (int i = 3; i < passwordLength; i++)
            {
                string chars = upperChars + lowerChars + numericChars;
                password += chars[random.Next(0, chars.Length)];
            }

            // Return result by hashing the password
              return ShufflePassword(password);
        }

           // Increase the security level by scrambling the password
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

