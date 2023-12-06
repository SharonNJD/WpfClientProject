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
                int[] digits = name.Select(c => int.Parse(c.ToString())).ToArray();

                // Validate checksum
                int sum = 0;
                for (int i = 0; i < 8; i++)
                {
                    int digit = digits[i];
                    if (i % 2 == 0)
                    {
                        digit *= 1;
                    }
                    else
                    {
                        digit *= 2;
                        if (digit > 9)
                        {
                            digit -= 9;
                        }
                    }
                    sum += digit;
                }

                int checksum = (10 - (sum % 10)) % 10;
                if (digits[8] == checksum)
                {
                    return new ValidationResult(false, "Not a posabile id");
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
