using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfClientProject
{
    public class ValidName : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string name = value.ToString();
                if (name.Length > Max)
                {
                    return new ValidationResult(false, "to long");
                }
                if (name.Length < Min)
                {
                    return new ValidationResult(false, "to short");
                }
                if (name == null)
                {
                    return new ValidationResult(false, "Nothing");
                }
                if (!Char.IsLetter(name[0]))
                {
                    return new ValidationResult(false, "First letter needs to be a letter");
                }
             
                for (int i = 0; i < name.Length; i++)
                {
                    if (!(Char.IsLetter(name[i])))
                    {
                        return new ValidationResult(false, "only letters or spaces");
                    }
                    
                }

            }
            catch (Exception ex)
            {

                return new ValidationResult(false, "first name not valid---" + ex.Message);
            }
            return ValidationResult.ValidResult;
        }



    }
    public class ValidEmail : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string name = value.ToString();
               
                if (name == null)
                {
                    return new ValidationResult(false, "Nothing");
                }
                if (!Char.IsLetter(name[0]))
                {
                    return new ValidationResult(false, "First letter needs to be a letter");
                }

                for (int i = 0; i < name.Length; i++)
                {
                    if (!(Char.IsLetter(name[i])))
                    {
                        return new ValidationResult(false, "only letters or spaces");
                    }

                }
                string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

                if (!Regex.IsMatch(name, emailPattern))
                {
                    return new ValidationResult(false, "Dosent fit the patren");
                }

            }
            catch (Exception ex)
            {

                return new ValidationResult(false, "Email not valid---" + ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }

    public class ValidPhoneNum : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string name = value.ToString();

                if (name == null)
                {
                    return new ValidationResult(false, "Nothing");
                }
               


                string phonePattern = @"^(972|0)?5[2-4]\d{7}$";
              

                if (!Regex.IsMatch(name, phonePattern))
                {
                    return new ValidationResult(false, "Dosent fit the patren");
                }

            }
            catch (Exception ex)
            {

                return new ValidationResult(false, "Phone num not valid---" + ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }
    public class VaildID : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string name = value.ToString();

                if (name == null)
                {
                    return new ValidationResult(false, "Nothing");
                }



                string idPattern = @"^\d{9}$";

                if (!Regex.IsMatch(name, idPattern))
                {
                    return new ValidationResult(false, "Dosent fit the patren");
                }
                
                string israeliID = value.ToString();
                // Validate checksum
                if (israeliID.Length != 9)
                    return new ValidationResult(false, "Its not in the right length");

                long sum = 0;

                for (int i = 0; i < israeliID.Length; i++)
                {
                    var digit = israeliID[israeliID.Length - 1 - i] - '0';
                    sum += (i % 2 != 0) ? GetDouble(digit) : digit;
                }

                if (!(sum % 10 == 0))
                {
                    return new ValidationResult(false, "not a posiible id");
                }

                int GetDouble(long i)
                {
                    switch (i)
                    {
                        case 0: return 0;
                        case 1: return 2;
                        case 2: return 4;
                        case 3: return 6;
                        case 4: return 8;
                        case 5: return 1;
                        case 6: return 3;
                        case 7: return 5;
                        case 8: return 7;
                        case 9: return 9;
                        default:
                            return 0;


                    }
                }
            }
            catch (Exception ex)
            {

                return new ValidationResult(false, "Phone num not valid---" + ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }
    public class ValidBirthYear : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            try
            {
                int year = int.Parse(value.ToString());
                if (year < 1500)
                    return new ValidationResult(false, "Ok bommer");
                if (year > DateTime.Today.Year)
                    return new ValidationResult(false, "BRO how");
                
            }
            catch (Exception ex)
            {

                return new ValidationResult(false, "Error:" + ex.Message);
            }
            return ValidationResult.ValidResult;
        }
    }
    public class VaildPassword : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool isupper = false;
            bool speacialcharin = false;
            bool islower = false;
            bool number = false;
            try
            {
                string specialChar = "#$_-~!?";
                string password = value.ToString();
                if (password.Length > 16)
                {
                    return new ValidationResult(false, "to long");
                }
                if (password.Length < 6)
                {
                    return new ValidationResult(false, "to short");
                }
                if (password.IndexOf(" ") != -1)
                {
                    return new ValidationResult(false, "Cant have spaces");
                }
                for (int i = 0; i < password.Length; i++)
                {
                    if (!Char.IsLetterOrDigit(password[i]) && specialChar.IndexOf(password[i]) == -1)
                    {
                        return new ValidationResult(false, "password must contian letters digits and speacil chars");
                    }
                    if (specialChar.IndexOf(password[i]) != -1)
                    {
                        speacialcharin = true;
                    }
                    if (Char.IsUpper(password[i]))
                    {
                        isupper = true;
                    }
                    if (!Char.IsUpper(password[i]))
                    {
                        islower = true;
                    }
                    if (Char.IsDigit(password[i]))
                    {
                        number = true;
                    }
                }
                if (!(speacialcharin && isupper && islower && number))
                {
                    return new ValidationResult(false, "Password to weak");
                }
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Password not valid---" + ex.Message);

            }
            return ValidationResult.ValidResult;
        }
    }

}
