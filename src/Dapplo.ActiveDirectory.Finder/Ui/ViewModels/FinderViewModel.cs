// Dapplo - building blocks for desktop applications
// Copyright (C) 2017-2019  Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Finder
// 
// Finder is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Finder is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Finder. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

using System;
using System.Linq;
using Caliburn.Micro;
using Dapplo.ActiveDirectory.Entities;
using Dapplo.ActiveDirectory.Enums;
using Dapplo.ActiveDirectory.Finder.Entities;
using Dapplo.ActiveDirectory.Finder.Entities.Impl;
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
            var userResult = query.Execute<User>(Environment.UserDomainName).FirstOrDefault();

            if (userResult == null)
            {
                return;
            }
            query = Query.AND.WhereIsUser().WhereEqualTo(UserProperties.Department, userResult.Department);
            var departmentResult = query.Execute<User>(Environment.UserDomainName);
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
