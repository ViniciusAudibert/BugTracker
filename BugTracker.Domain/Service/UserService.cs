using System;
using BugTracker.Domain.Entity;
using BugTracker.Domain.Interface.Repository;
using BugTracker.Domain.Interface.Service;
using System.Security.Cryptography;
using System.Text;

namespace BugTracker.Domain.Service
{
    public class UserService : IUserService
    {
        IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User FindById(int id)
        {
            return this.userRepository.FindById(id);
        }

        public User Add(User user)
        {
            if (user.Password != null)
                user.Password = Encrypt(user.Password);
            return userRepository.Add(user);
        }

        public User FindByEmail(string email)
        {
            return userRepository.FindByEmail(email);
        }

        public User FindByAuthentication(string email, string password)
        {
            return userRepository.FindByAuthentication(email, Encrypt(password));
        }

        public void ActiveAccount(User user)
        {
            userRepository.ActiveAccount(user);
        }

        public User Update(User user)
        {
            user.Password = user.Password != null ? this.Encrypt(user.Password) :
                FindById(user.IDUser).Password;

            return userRepository.Update(user);
        }

        public void UpdatePassword(User user, string password)
        {
            user.Password = Encrypt(password);

            userRepository.Update(user);
        }

        public void UpdateImage(int idUser, string imagePath)
        {
            User user = FindById(idUser);

            user.Image = imagePath;

            userRepository.Update(user);

        }

        public bool ComparePassword(String oldPassword, String newPassword)
        {
            return this.Encrypt(newPassword).Equals(oldPassword);
        }

        private string Encrypt(string password)
        {
            string IV = "iu4fli2esfjwo42p";
            string Key = "qothft5y3yrhdnvoi86dht01jamcvbxn";

            byte[] textBytes = ASCIIEncoding.ASCII.GetBytes(password);

            AesCryptoServiceProvider encdec = new AesCryptoServiceProvider();

            encdec.BlockSize = 128;
            encdec.KeySize = 256;
            encdec.Key = ASCIIEncoding.ASCII.GetBytes(Key);
            encdec.IV = ASCIIEncoding.ASCII.GetBytes(IV);
            encdec.Padding = PaddingMode.PKCS7;
            encdec.Mode = CipherMode.CBC;

            ICryptoTransform iCrypt = encdec.CreateEncryptor(encdec.Key, encdec.IV);

            byte[] enc = iCrypt.TransformFinalBlock(textBytes, 0, textBytes.Length);
            iCrypt.Dispose();

            var aesCrypted = Convert.ToBase64String(enc);

            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(aesCrypted));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }


    }
}
