﻿using Back_End.Models;

namespace Back_End.IServices
{
    public interface IMailService
    {
       Task SendEmailAutorization(User user, string url);
        Task SendEmailForgotAutorization(User user, string url);
        Task SendEmailPurchases(List<Purchase> PurchaseList, User user);
    }
}
