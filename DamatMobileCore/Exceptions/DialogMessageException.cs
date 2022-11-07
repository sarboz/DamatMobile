using System;

namespace DamatMobile.Core.Exceptions
{
    public class DialogMessageException:Exception
    {
        public DialogMessageException(string title, string message) : base(message)
        {
            Title = title;
        }
        public string Title { get; }
    }
}