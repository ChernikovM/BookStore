﻿using BookStore.BusinessLogicLayer.Configurations.Interfaces;
using BookStore.BusinessLogicLayer.Exceptions;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using BookStore.DataAccessLayer.Entities;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BookStore.BusinessLogicLayer.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IEmailSenderConfiguration _config;
        private readonly UserManager<User> _userManager;

        public EmailSenderService(IEmailSenderConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        private MimeMessage GetMessage(string toName, string toAddress, string subject, string body)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_config.FromName, _config.FromAddress));
            emailMessage.To.Add(new MailboxAddress(toName, toAddress));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart()
            {
                Text = body,
            };

            return emailMessage;
        }

        private void IsEmailConfirmed(User user)
        {
            if (user.EmailConfirmed == false)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Email is not confirmed.");
            }
        }

        private async Task SendEmailAsync(MimeMessage message)
        {
            using var smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync(_config.SmtpHost, _config.SmtpPort, true);
            await smtpClient.AuthenticateAsync(_config.SmtpUsername, _config.SmtpPassword);
            await smtpClient.SendAsync(message);

            await smtpClient.DisconnectAsync(true);
        }

        public async Task SendEmailConfirmationLinkAsync(User user)
        {
            string messageSubject = "Account Registration";

            StringBuilder sb = new StringBuilder();
            sb.Append($"Hi, {user.FirstName} {user.LastName}.");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append($"Thank you for registration.");
            sb.Append(Environment.NewLine);
            sb.Append($"To complete the registration of your account follow the next link: ");
            sb.Append(Environment.NewLine);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var emailConfirmationUrl = new UriBuilder
            {
                Port = _config.Port,
                Path = _config.PathEmailConfirmation,
                Query = $"userId={user.Id}&token={HttpUtility.UrlEncode(token)}"
            };

            sb.Append(emailConfirmationUrl);

            var message = GetMessage(user.UserName, user.Email, messageSubject, sb.ToString());

            await SendEmailAsync(message);
        }

        public async Task SendPasswordResettingLinkAsync(User user)
        {
            IsEmailConfirmed(user);

            string messageSubject = "Reset Password";

            StringBuilder sb = new StringBuilder();
            sb.Append($"Hi, {user.UserName}.");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append($"A request has been received to change the password for your account.");
            sb.Append(Environment.NewLine);
            sb.Append($"Follow the next link if you want to change password: ");
            sb.Append(Environment.NewLine);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetPasswordLink = new UriBuilder
            {
                Port = _config.Port,
                Path = _config.PathPasswordChange,
                Query = $"userId={user.Id}&token={HttpUtility.UrlEncode(token)}"
            };

            sb.Append(resetPasswordLink);

            var message = GetMessage(user.UserName, user.Email, messageSubject, sb.ToString());

            await SendEmailAsync(message);
        }
    }
}
