using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace test1
{
    public class Check
    {
        public static bool Finding1(Account account)
        {
            bool BOOL1 = false;
            if (account.Age >= 18)
            {
                BOOL1 = true;
            }
            return BOOL1;
        }
        public static bool Finding2(Account account)
        {
            bool bool2 = false;
            var CharArray = account.Phone.ToCharArray();
            bool hasLetters = account.Phone.Any(char.IsLetter);
            foreach (char s in account.Phone)
            {
                if (account.Phone.StartsWith("+7") && (hasLetters == false))
                {
                    bool2 = true;
                }
            }
            return bool2;
        }
        public static bool Finding3(Cars car)
        {
            bool BOOL3 = true;
            if (car.Category != "A" && car.Category != "B" && car.Category != "D")
            {
                BOOL3 = false;
            }
            return BOOL3;
        }
    }
}

