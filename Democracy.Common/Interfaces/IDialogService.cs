﻿using System;

namespace Democracy.Common.Interfaces
{
    public interface IDialogService
    {
        void Alert(string message, string title, string okbtnText);

        void Confirm(string title, 
            string message, 
            string okButtonTitle, 
            string dismissButtonTitle, 
            Action confirmed, 
            Action dismissed);
    }
}
