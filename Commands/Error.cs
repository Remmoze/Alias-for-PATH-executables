﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRunCommands.Commands
{
    public enum CommandOutput
    {
        Success,
        Fail,
        Too_Few_Arguments,
        Too_Many_Arguments,
        Incorrect_Argument
    }

    public class Error
    {
        public CommandOutput Type;
        public string DefaultDiscription { get { return GetDefaultDiscription(); } }
        public string ExtraInformation;

        public Error(CommandOutput type, string extra = "") => (Type, ExtraInformation) = (type, extra);

        private string GetDefaultDiscription()
        {
            switch (Type) {
                case CommandOutput.Too_Few_Arguments: return "Too few arguments.";
                case CommandOutput.Too_Many_Arguments: return "Too many arguments.";
                case CommandOutput.Incorrect_Argument: return "Incorrect arguments.";

                case CommandOutput.Success:
                case CommandOutput.Fail: return "";

                default: return "Unknown error.";
            }
        }

        public override string ToString()
        {
            return $"[FAIL]: {DefaultDiscription} {ExtraInformation}";
        }
    }
}
