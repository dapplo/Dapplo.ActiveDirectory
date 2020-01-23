// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using Caliburn.Micro;
using Dapplo.ActiveDirectory.Entities;
using Dapplo.ActiveDirectory.Enums;
using Dapplo.ActiveDirectory.Finder.Entities;
using Dapplo.CaliburnMicro;

namespace Dapplo.ActiveDirectory.Finder.Ui.ViewModels
{
    /// <summary>
    /// The ViewModel for the Finder
    /// </summary>
    public class FinderViewModel : Screen, IShell
    {
        private IUser _selectedUser;

        /// <summary>
        /// Used from the View
        /// </summary>
        public IObservableCollection<IUser> Users { get; } = new BindableCollection<IUser>();

        /// <summary>
        /// Constructor for the dependencies
        /// </summary>
        public FinderViewModel()
        {
            // Limit to 100 items
            ActiveDirectoryGlobals.SizeLimit = 100;
            ActiveDirectoryGlobals.PageSize = 0;
            var query = Query.AND.WhereIsUser().WhereEqualTo(UserProperties.Username, Environment.UserName);
            var userResult = query.Execute<IUser>().FirstOrDefault();

            if (userResult == null)
            {
                return;
            }
            query = Query.AND.WhereIsUser().WhereEqualTo(UserProperties.Department, userResult.Department);
            var departmentResult = query.Execute<IUser>();
            // Just something to generate some output
            Users.AddRange(departmentResult);
        }

        /// <summary>
        /// Used from the View
        /// </summary>
        public IUser SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (_selectedUser == value)
                {
                    return;
                }

                _selectedUser = value;
                NotifyOfPropertyChange(nameof(SelectedUser));
            }
        }
    }
}
