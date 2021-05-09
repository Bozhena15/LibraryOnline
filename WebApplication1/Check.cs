using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using System.Net.Mail;
namespace WebApplication1
{
    public static class Check
    {
        public static bool CheckPasswordAndEmail(UserModel model)
        {
            if (!String.IsNullOrEmpty(model.Email) && !String.IsNullOrEmpty(model.Password))
            {
                try
                {
                    MailAddress addr = new MailAddress(model.Email);

                    if (addr.Address == model.Email && (model.Password.Length >= 4 && model.Password.Length <= 10))
                        return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        public static bool CheckPasswordAndName(AdminModel model)
        {
            if (!String.IsNullOrEmpty(model.Name) && !String.IsNullOrEmpty(model.Password))
            {
                if ((model.Name.Length >= 4 && model.Name.Length <= 20) && (model.Password.Length >= 4 && model.Password.Length <= 10))
                    return true;
                return false;
            }
            return false;
        }
        public static bool CheckRegister(UserModel model)
        {
            if (CheckPasswordAndEmail(model))
            {
                if (!String.IsNullOrEmpty(model.Name) && !String.IsNullOrEmpty(model.Phone))
                {
                    if (model.Name.Length >= 4 && model.Name.Length <= 50 && model.Phone.Length == 13)
                        return true;
                    return false;
                }
                return false;
            }
            return false;
        }
        public static bool CheckAuthor(AuthorModel model)
        {
            if (!String.IsNullOrEmpty(model.LastName) && !String.IsNullOrEmpty(model.FirstName))
            {
                if (model.LastName.Length <= 30 && model.FirstName.Length <= 30)
                    return true;
                return false;
            }
            return false;
        }
        public static bool CheckGenre(GenreBookModel model)
        {
            if (!String.IsNullOrEmpty(model.Genre))
            {
                if (model.Genre.Length <= 30)
                    return true;
                return false;
            }
            return false;
        }
        public static bool CheckBook(BookModel model)
        {
            if (!String.IsNullOrEmpty(model.Name) && !String.IsNullOrEmpty(model.Link))
            {
                if (model.Name.Length >= 1)
                    return true;
                return false;
            }
            return false;
        }
    }
}
