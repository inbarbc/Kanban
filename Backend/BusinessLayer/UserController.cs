using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using IntroSE.Kanban.Backend.BusinessLayer;
using log4net;


namespace IntroSE.Backend.Fronted.BusinessLayer
{
    public class UserController
    {
        private Dictionary<string, User> users; //Contains all registered users by email key

        private static readonly ILog log = LogManager.GetLogger("UserController");

        public UserController()
        {
            this.users = new Dictionary<string, User>();
        }

        /// <summary>
        /// This method create a new user to the system.
        /// </summary>
        /// <param name="email">The user email address, used as the username for logging the system.</param>
        /// <param name="password">The user password.</param>
        /// <returns>void.</returns>
        internal void CreateUser(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                log.Debug("User with null email attempted register");
                throw new Exception("Email is null");
            }
            if (isExists(email))
            {
                throw new Exception($"Email {email} already exists");
            }
            if (!isValidEmail(email))
            {
                log.Debug("User with invalid email attempted register");
                throw new Exception("Email is invalid");
            }
            if (!isValidPassword(password))
            {
                log.Debug("User with invalid password attempted register");
                throw new Exception("Email is password");
            }
            User u = new User(email, password);
            users.Add(email, u);
        }

        /// <summary>
        /// This method delete a user from the system.
        /// </summary>
        /// <param name="email">The user email address.</param>
        /// <param name="password">The user password.</param>
        /// <returns>void.</returns>
        internal void DeleteUser(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                log.Debug("User with null email attempted delete user");
                throw new Exception("Email is null");
            }
            if (!users.ContainsKey(email))
            {
                log.Debug("User with wrong email attempted delete user");
                throw new Exception($"Email {email} is not contain in this form");
            }
            if (password is null || email.Equals(""))
            {
                log.Debug("User with null password attempted delete user");
                throw new Exception("Password is null");
            }
            if (users[email].Password != password)
            {
                log.Debug("User with wrong password attempted delete user");
                throw new Exception($"Password {password} is not contain in this form");
            }
            users.Remove(email);
        }

        /// <summary>
        /// This method check if a user exists in the system.
        /// </summary>
        /// <param name="email">The user email address.</param>
        /// <returns>boolean if the user exists in the system.</returns>
        internal bool IsExists(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                log.Debug("User with null email attempted to find a user");
                throw new Exception("Email is null");
            }
            return users.ContainsKey(email);
        }

        /// <summary>
        /// This method return the user class if he exists in the system.
        /// </summary>
        /// <param name="email">The user email address.</param>
        /// <returns>User class.</returns>
        internal User GetUser(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                log.Debug("Attempted to get user with null email");
                throw new Exception($"Email is null");
            }
            if (!users.ContainsKey(email))
            {
                log.Debug("There is no user exist with this email");
                throw new Exception($"Email {email} is not contain in this form");
            }
            return users[email];
        }

        /// <summary>
        /// This method check if the password is valid.
        /// </summary>
        /// <param name="password">The password that need to be checked.</param>
        /// <returns>boolean if the password is valid.</returns>
        internal bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) //if password is null
            {
                log.Warn("Password cannot be null");
                throw new Exception("Password cannot be null");
            }

            bool existUpper = false;
            bool existSmall = false;
            bool existNumber = false;
            if (password.Length < 6 | password.Length > 20) //Length check
            {
                log.Warn("Invalid password length");
                throw new Exception("Invalid password length");
            }
            for (int i = 0; i < password.Length; i++) //Contains capital letter, small letter, and a digit
            {
                if ((password[i] <= 'Z') & (password[i]) >= 'A')
                    existUpper = true;
                if ((password[i] <= 'z') & (password[i] >= 'a'))
                    existSmall = true;
                if ((password[i] <= '9') & (password[i] >= '0'))
                    existNumber = true;
            }
            if (!existUpper) //Missing capital letter
            {
                log.Warn("Invalid password : must contains a uppercase letter");
                throw new Exception("Invalid password : it must contains a uppercase letter");
            }
            if (!existSmall) //Missing small letter
            {
                log.Warn("Invalid password : must contains a small character");
                throw new Exception("Invalid password : must contains a small character");
            }
            if (!existNumber) //Missing digit
            {
                log.Warn("Invalid password : must contains a digit");
                throw new Exception("Invalid password : must contains a digit");
            }
            return true;
        }

        /// <summary>
        /// This method check if the email is valid.
        /// </summary>
        /// <param name="email">The email that need to be checked.</param>
        /// <returns>boolean if the email is valid.</returns>
        internal bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                log.Warn("Email string cannot be null or contain white space");
                return false;
            }

            try
            {
                string pattern = "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$";
                var regex = new Regex(pattern);
                return regex.IsMatch(email);
            }
            catch (RegexMatchTimeoutException)
            {
                log.Debug("Email address is incorrent"); 
                return false; 
            }
            catch (ArgumentException) {

                log.Debug("Email address is incorrent");
                return false; 
            }
        }

        /// <summary>
        /// This method return if the user is log in.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>boolean if the user is log in.</returns>
        internal bool IsLogIn(string email)
        {
            if(!users[email].Status)
                log.Warn("User is not log in");
            return users[email].Status;
        }
    }
}
