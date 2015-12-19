using Core.Common.Core;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Client.Entities
{   
    public class UserApplicationActivityDetails : ObjectBase
    {
        private string _SessionKey;
        private DateTime _LastUpdateSessionTime;
        private bool _UserLoggedIn;
        private string _LoginMethod;

        public string SessionKey
        {
            get { return _SessionKey; }
            set
            {
                _SessionKey = value;
                OnPropertyChanged(() => SessionKey);
            }
        }

        public DateTime LastUpdateSessionTime
        {
            get { return _LastUpdateSessionTime; }
            set
            {
                _LastUpdateSessionTime = value;
                OnPropertyChanged(() => LastUpdateSessionTime);
            }
        }

        public bool UserLoggedIn
        {
            get { return _UserLoggedIn; }
            set
            {
                _UserLoggedIn = value;
                OnPropertyChanged(() => UserLoggedIn);
            }
        }       

        public string LoginMethod
        {
            get { return _LoginMethod; }
            set
            {
                _LoginMethod = value;
                OnPropertyChanged(() => LoginMethod);
            }
        }

        class UserApplicationActivityDetailsValidator : AbstractValidator<UserApplicationActivityDetails>
        {
            public UserApplicationActivityDetailsValidator()
            {
                RuleFor(obj => obj.SessionKey).NotNull();               
                RuleFor(obj => obj.LastUpdateSessionTime).NotNull();               
            }
        }

        protected override IValidator GetValidator()
        {
            return new UserApplicationActivityDetailsValidator();
        }

    }
}
