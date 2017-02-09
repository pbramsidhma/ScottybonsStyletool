using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScottybonsStylist.Models.Services
{
    public class CommonServices
    {

        public string GenerateCode(string startPrefix, string value)
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            characters += alphabets + small_alphabets + numbers;

            int length = 5;
            string code = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (code.IndexOf(character) != -1);
                code += character;
            }

            //Check the database for duplicate
            return string.Format("{0}{1}{2}", startPrefix, code, value);
        }
    }
}