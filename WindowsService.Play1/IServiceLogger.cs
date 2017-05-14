using System;

namespace WindowsService.Play1
{
    public interface IServiceLogger
    {
        void Debug(string message);

        void Error(string message, Exception exception = null);

        void Info(string message);
    }
}