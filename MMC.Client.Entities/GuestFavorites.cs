using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Common.Core;
using FluentValidation;
using FluentValidation.Validators;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MMC.Client.Entities
{
    [DataContract(Namespace = "wadado.in")]
    public class GuestFavorites : ObjectBase
    {
        #region Private variables
        private string _guestFavouritesKey;
        private string _activityKey;
        private string _guestKey;
        private bool _isDeleted;
        #endregion

        #region Properties
        public string GuestFavouritesKey
        {
            get { return _guestFavouritesKey; }
            set
            {
                _guestFavouritesKey = value;
                OnPropertyChanged(() => GuestFavouritesKey);
            }
        }

        public string ActivityKey
        {
            get { return _activityKey; }
            set
            {
                _activityKey = value;
                OnPropertyChanged(() => ActivityKey);
            }
        }

        public string GuestKey
        {
            get { return _guestKey; }
            set
            {
                _guestKey = value;
                OnPropertyChanged(() => GuestKey);
            }
        }

        public bool IsDeleted
        {
            get { return _isDeleted; }
            set
            {
                _isDeleted = value;
                OnPropertyChanged(() => IsDeleted);
            }
        } 
        #endregion

        /// <summary>
        /// This class is used to validate the entity
        /// </summary>
        class GuestFavoritesValidator : AbstractValidator<GuestFavorites>
        {
            public GuestFavoritesValidator()
            {
                RuleFor(obj => obj.GuestFavouritesKey).NotEmpty();
                RuleFor(obj => obj.ActivityKey).NotEmpty();                
            }
        }
        /// <summary>
        /// Adding validation to an object
        /// </summary>
        /// <returns></returns>
        protected override IValidator GetValidator()
        {
            return new GuestFavoritesValidator();
        }
    }
}
