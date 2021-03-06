﻿using BookStore.BusinessLogicLayer.Configurations.Interfaces;
using BookStore.BusinessLogicLayer.Services.Interfaces;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BusinessLogicLayer.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly ILoggerConfiguration _loggerConfiguration;

        public LoggerService(ILoggerConfiguration loggerConfiguration)
        {
            _loggerConfiguration = loggerConfiguration;
        }

        public async Task LogAsync(Exception ex)
        {
            using StreamWriter sr = new StreamWriter(_loggerConfiguration.FilePath, true);

            StringBuilder sb = new StringBuilder();

            sb.Append(new string('_', 20)).Append(Environment.NewLine);
            sb.Append($"- Date: {DateTime.Now}").Append(Environment.NewLine);
            sb.Append($"- Exception : {ex.GetType()}").Append(Environment.NewLine);
            sb.Append($"- Error : {ex.Message}").Append(Environment.NewLine);
            sb.Append($"- Stack Trace: {Environment.NewLine}{ex.StackTrace}").Append(Environment.NewLine);

            await sr.WriteLineAsync(sb.ToString());
        }
    }
}
