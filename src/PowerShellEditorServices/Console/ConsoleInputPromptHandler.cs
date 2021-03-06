//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System;

namespace Microsoft.PowerShell.EditorServices.Console
{
    /// <summary>
    /// Provides a standard implementation of InputPromptHandler
    /// for use in the interactive console (REPL).
    /// </summary>
    public class ConsoleInputPromptHandler : InputPromptHandler
    {
        #region Private Fields

        private IConsoleHost consoleHost;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates an instance of the ConsoleInputPromptHandler class.
        /// </summary>
        /// <param name="consoleHost">
        /// The IConsoleHost implementation to use for writing to the
        /// console.
        /// </param>
        public ConsoleInputPromptHandler(IConsoleHost consoleHost)
        {
            this.consoleHost = consoleHost;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when the prompt caption and message should be
        /// displayed to the user.
        /// </summary>
        /// <param name="caption">The caption string to be displayed.</param>
        /// <param name="message">The message string to be displayed.</param>
        protected override void ShowPromptMessage(string caption, string message)
        {
            if (!string.IsNullOrEmpty(caption))
            {
                this.consoleHost.WriteOutput(caption, true);
            }

            if (!string.IsNullOrEmpty(message))
            {
                this.consoleHost.WriteOutput(message, true);
            }
        }

        /// <summary>
        /// Called when a prompt should be displayed for a specific
        /// input field.
        /// </summary>
        /// <param name="fieldDetails">The details of the field to be displayed.</param>
        protected override void ShowFieldPrompt(FieldDetails fieldDetails)
        {
            // For a simple prompt there won't be any field name.
            // In this case don't write anything
            if (!string.IsNullOrEmpty(fieldDetails.Name))
            {
                this.consoleHost.WriteOutput(
                    fieldDetails.Name + ": ",
                    false);
            }
        }

        /// <summary>
        /// Called when an error should be displayed, such as when the
        /// user types in a string with an incorrect format for the
        /// current field.
        /// </summary>
        /// <param name="e">
        /// The Exception containing the error to be displayed.
        /// </param>
        protected override void ShowErrorMessage(Exception e)
        {
            this.consoleHost.WriteOutput(
                e.Message,
                true,
                OutputType.Error);
        }

        #endregion
    }
}

