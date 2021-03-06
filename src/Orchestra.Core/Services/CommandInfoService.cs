﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandInfoService.cs" company="WildGums">
//   Copyright (c) 2008 - 2015 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orchestra.Services
{
    using System.Collections.Generic;
    using Catel;
    using Catel.MVVM;
    using Models;

    public class CommandInfoService : ICommandInfoService
    {
        private readonly ICommandManager _commandManager;
        private readonly Dictionary<string, ICommandInfo> _commandInfo = new Dictionary<string, ICommandInfo>();

        #region Constructors
        public CommandInfoService(ICommandManager commandManager)
        {
            Argument.IsNotNull(() => commandManager);

            _commandManager = commandManager;
        }
        #endregion

        public ICommandInfo GetCommandInfo(string commandName)
        {
            Argument.IsNotNull(() => commandName);

            if (!_commandInfo.ContainsKey(commandName))
            {
                var inputGesture = _commandManager.GetInputGesture(commandName);

                _commandInfo[commandName] = new CommandInfo(commandName, inputGesture);
            }

            return _commandInfo[commandName];
        }

        public void UpdateCommandInfo(string commandName, ICommandInfo commandInfo)
        {
            Argument.IsNotNull(() => commandName);
            Argument.IsNotNull(() => commandInfo);

            _commandInfo[commandName] = commandInfo;
        }

        public void Invalidate()
        {
            _commandInfo.Clear();
        }
    }
}